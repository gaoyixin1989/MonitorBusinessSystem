using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Diagnostics;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Import
{
    /// <summary>
    /// 常规监测Excel导入导出功能 Created by 熊卫华 2013年6月20日
    /// </summary>
    public class ImportExcel : SqlHelper
    {
        private string getPointId(string strTableName, string strTableColumn, string strPointName, string strYear, string strMonth)
        {
            string strPointId = "";
            string strSql = @"select ID from {0} where IS_DEL='0' and  {1}='{2}' and YEAR='{3}' and MONTH='{4}'";
            strSql = string.Format(strSql, strTableName, strTableColumn, strPointName, strYear, strMonth);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
                strPointId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
            return strPointId;
        }
        private DataTable getPointIdEx(string strPointName, string strYear, string strMonth, string EnterPrise_Value)
        {
            StringBuilder sb = new StringBuilder(256);
            sb.Append("select a.id from T_ENV_P_POLLUTE a left join T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.SATAIONS_ID=c.id ");
            sb.Append("  where b.type_name='废水' and a.IS_DEL='0' and a.point_name='" + strPointName + "' and a.year='" + strYear + "' and a.month='" + strMonth + "' and c.enter_name='" + EnterPrise_Value + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        private DataTable getPointIdExs(string strPointName, string strYear, string strMonth, string EnterPrise_Value)
        {
            StringBuilder sb = new StringBuilder(256);
            sb.Append("select a.id from T_ENV_P_POLLUTE a left join T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.SATAIONS_ID=c.id ");
            sb.Append("  where b.type_name='废气' and a.IS_DEL='0' and a.point_name='" + strPointName + "' and a.year='" + strYear + "' and a.month='" + strMonth + "' and c.enter_name='" + EnterPrise_Value + "'");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        private string getVerticalId(string TableName, string strPointColumnName, string strPointId, string strVerticalColumnName, string strVerticalName)
        {
            string strVerticalId = "";
            string strSql = @"select ID from {0} where {1}='{2}' and {3}='{4}'";
            strSql = string.Format(strSql, TableName, strPointColumnName, strPointId, strVerticalColumnName, strVerticalName);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
                strVerticalId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
            return strVerticalId;
        }
        private DataTable getFillItemInfo(string strFillItemTableName, string strFillTableId, string strItemId)
        {
            string strSql = "select * from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
            strSql = string.Format(strSql, strFillItemTableName, strFillTableId, strItemId);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            return objTable;
        }
        /// <summary>
        /// 根据监测项目名称获取ID
        /// </summary>
        /// <param name="strPointItemTableName">监测点监测项目表</param>
        /// <param name="strPointId">监测点（垂线）ID</param>
        /// <param name="strItemName">监测项目名称</param>
        /// <returns></returns>
        private string getItemIdByName(string strPointItemTableName, string strPointId, string strItemName)
        {
            string strItemId = "";
            string strSql = @"select ITEM_ID
                                from {0}
                                where POINT_ID = '{1}'
                                and ITEM_ID in (select ID
                                                    from T_BASE_ITEM_INFO
                                                where ITEM_NAME = '{2}'
                                                    and IS_DEL = '0'
                                                    and IS_SUB = '1')";
            strSql = string.Format(strSql, strPointItemTableName, strPointId, strItemName);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
                strItemId = objTable.Rows[0]["ITEM_ID"] == null ? "" : objTable.Rows[0]["ITEM_ID"].ToString();
            return strItemId;
        }

        #region//将Excel表导入数据库中，只能支持按月导入【支持导入河流、湖泊类】
        /// <summary>
        /// 将Excel表导入数据库中，只能支持按月导入【支持导入河流、湖泊类】
        /// </summary>
        /// <param name="strXmlUrl">xml模板位置</param>
        /// <param name="sheet">EXCEL工作薄NPOI对象</param>
        /// <returns></returns>
        public bool importExcel(string strXmlUrl, ISheet sheet)
        {
            StringBuilder sb = new StringBuilder();

            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);

            //垂线表【非必须】
            string strVerticalTableName = new XmlQuery().getElementAttribute(elementPoint, "verticaltable");
            if (strVerticalTableName == "") sb.AppendLine("xml配置文档没有配置垂线表名称【非必须】" + Environment.NewLine);

            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);

            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表名称" + Environment.NewLine);

            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报监测项目表名称" + Environment.NewLine);

            //数据填报表监测点字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表断面字段名称" + Environment.NewLine);

            //数据填报表垂线字段名称【非必须】
            string strFillVerticalColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTableVerticalColumnName")[0].InnerText;
            if (strFillVerticalColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表垂线字段名称【非必须】" + Environment.NewLine);

            //数据填报表序列号
            string strFillTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillTableSerialNum")[0].InnerText;
            if (strFillTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填报表序列号字段" + Environment.NewLine);

            //数据填报监测项目表序列号
            string FillItemTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableSerialNum")[0].InnerText;
            if (strFillTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填报监测项目表序列号字段" + Environment.NewLine);

            //定义批量数据查询语句容器
            ArrayList arrayList = new ArrayList();

            //搜索监测点配置信息
            List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "SubPoint");
            foreach (XmlElement elementPointTemp in pointConfigList)
            {
                List<XmlElement> pointCodeList = new XmlQuery().xmlElementSearch(elementPointTemp, "SubPointCode");
                List<XmlElement> pointNameList = new XmlQuery().xmlElementSearch(elementPointTemp, "SubPointName");
                List<XmlElement> verticalList = new XmlQuery().xmlElementSearch(elementPointTemp, "Vertical");

                string strPointCode = pointCodeList.Count == 1 ? pointCodeList[0].InnerText : "";
                string strPointCodeRow = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("row") : "";
                string strPointCodeColum = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("column") : "";
                string strPointCodeTableColum = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("tablecolumn") : "";

                string strPointName = pointNameList.Count == 1 ? pointNameList[0].InnerText : "";
                string strPointNameRow = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("row") : "";
                if (strPointName == "") sb.AppendLine("xml配置文件中【PointInfo】配置节监测点名称无效" + Environment.NewLine);
                if (strPointNameRow == "") sb.AppendLine("xml配置文件中【PointInfo】配置节获取行号无效" + Environment.NewLine);

                string strPointNameColum = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("column") : "";
                string strPointNameTableColum = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("tablecolumn") : "";
                if (strPointNameColum == "") sb.AppendLine("xml配置文件中【PointInfo】配置节获取列号无效" + Environment.NewLine);
                if (strPointNameTableColum == "") sb.AppendLine("xml配置文件中【PointInfo】配置节监测名字段无效" + Environment.NewLine);

                string strVertical = verticalList.Count == 1 ? verticalList[0].InnerText : "";
                string strVerticalRow = verticalList.Count == 1 ? verticalList[0].GetAttribute("row") : "";
                string strVerticalColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("column") : "";
                string strVerticalPointTableColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("pointcolumn") : "";
                string strVerticalTableColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("tablecolumn") : "";

                bool configYear = false;
                string intConfigYearColum = "";

                bool configMonth = false;
                string intConfigMonthColum = "";
                //搜索数据填报基础信息
                List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                {
                    string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn");
                    string intColumn = fillBaseTemp.GetAttribute("column");
                    if (strTableColumn.Trim().ToLower() == "year")
                    {
                        configYear = true;
                        intConfigYearColum = intColumn;
                    }
                    if (strTableColumn.Trim().ToLower() == "month")
                    {
                        configMonth = true;
                        intConfigMonthColum = intColumn;
                    }
                }
                if (!configYear) sb.AppendLine("xml配置文档【FillInfo】配置节没有配置年度信息" + Environment.NewLine);
                if (!configMonth) sb.AppendLine("xml配置文档【FillInfo】配置节没有配置月份信息" + Environment.NewLine);

                //如果已经配置了年份和月份
                if (configYear && configMonth)
                {
                    //定义监测点和垂线ID
                    string strPointId = "";
                    string strVerticalId = "";

                    //获取年份和月份的值
                    string intYear = sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intConfigYearColum)).ToString();
                    string intMonth = sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intConfigMonthColum)).ToString();

                    if (intYear == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的年度信息无效" + Environment.NewLine);
                    if (intMonth == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的月度信息无效" + Environment.NewLine);

                    //根据断面（或者垂线）获取断面或者垂线ID
                    if (intYear != "" && intMonth != "")
                    {
                        if (strVerticalTableName != "")
                        {
                            //如果存在垂线情况下
                            strPointId = getPointId(strPointTableName, strPointNameTableColum, strPointName, intYear, intMonth);
                            strVerticalId = getVerticalId(strVerticalTableName, strVerticalPointTableColum, strPointId, strVerticalTableColum, strVertical);
                        }
                        else
                        {
                            //如果不存在垂线情况下
                            strPointId = getPointId(strPointTableName, strPointNameTableColum, strPointName, intYear, intMonth);
                        }
                    }
                    if (strPointId == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的监测点ID无效" + Environment.NewLine);
                    if (strVerticalId == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的垂线ID无效【非必须】" + Environment.NewLine);

                    if (strPointId != "")
                    {
                        string strSql = "";
                        string strSql1 = "";
                        string strSql2 = "";
                        string strDeleteSql = "";
                        //获取数据填报表序列号
                        string strFillPointSerialNum = GetSerialNumber(strFillTableSerialNum);
                        if (strFillPointSerialNum == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的数据填报表序列号无效" + Environment.NewLine);

                        if (strVerticalTableName != "")
                        {
                            //如果有垂线
                            strDeleteSql = @"delete from {0}
                                                 where exists (select *
                                                          from {1}
                                                         where {1}.ID = {0}.FILL_ID
                                                           and {1}.{2} = '{6}'
                                                           and {1}.{3} = '{7}'
                                                           and {1}.YEAR = '{4}'
                                                           and {1}.MONTH = '{5}')
                                                ";
                            strDeleteSql = string.Format(strDeleteSql, strFillItemTableName, strFillTableName, strFillPointColumName, strFillVerticalColumName, intYear, int.Parse(intMonth).ToString(), strPointId, strVerticalId);
                            arrayList.Add(strDeleteSql);

                            strDeleteSql = @"delete from {0}
                                                 where {1} = '{5}'
                                                   and {2} = '{6}'
                                                   and YEAR = '{3}'
                                                   and MONTH = '{4}'";
                            strDeleteSql = string.Format(strDeleteSql, strFillTableName, strFillPointColumName, strFillVerticalColumName, intYear, int.Parse(intMonth).ToString(), strPointId, strVerticalId);
                            arrayList.Add(strDeleteSql);

                            strSql1 = @"insert into " + strFillTableName + "(ID," + strFillPointColumName + "," + strFillVerticalColumName + "";
                            strSql2 = @" values('" + strFillPointSerialNum + "','" + strPointId + "','" + strVerticalId + "'";
                        }
                        else
                        {
                            //如果无垂线
                            strDeleteSql = @"delete from {0}
                                                 where exists (select *
                                                          from {1}
                                                         where {1}.ID = {0}.FILL_ID
                                                           and {1}.{2} = '{5}'
                                                           and {1}.YEAR = '{3}'
                                                           and {1}.MONTH = '{4}')
                                                ";
                            strDeleteSql = string.Format(strDeleteSql, strFillItemTableName, strFillTableName, strFillPointColumName, intYear, int.Parse(intMonth).ToString(), strPointId);
                            arrayList.Add(strDeleteSql);

                            strDeleteSql = @"delete from {0}
                                                 where {1} = '{4}'
                                                   and YEAR = '{2}'
                                                   and MONTH = '{3}'";
                            strDeleteSql = string.Format(strDeleteSql, strFillTableName, strFillPointColumName, intYear, int.Parse(intMonth).ToString(), strPointId);
                            arrayList.Add(strDeleteSql);

                            strSql1 = @"insert into " + strFillTableName + "(ID," + strFillPointColumName + "";
                            strSql2 = @" values('" + strFillPointSerialNum + "','" + strPointId + "'";
                        }
                        foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                        {
                            string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn");
                            string intColumn = fillBaseTemp.GetAttribute("column");
                            string strValue = sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intColumn)).ToString();
                            strSql1 += "," + strTableColumn + "";
                            strSql2 += ",'" + strValue + "'";
                        }
                        strSql1 += ")";
                        strSql2 += ")";
                        strSql = strSql1 + strSql2;
                        arrayList.Add(strSql);

                        //搜索数据填报监测项目信息
                        List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                        foreach (XmlElement fillItemTemp in fillItemConfigList)
                        {
                            string strItemName = fillItemTemp.InnerText;
                            string intColumn = fillItemTemp.GetAttribute("column");
                            if (intColumn == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的【" + strItemName + "】监测项目列号无效" + Environment.NewLine);

                            //根据监测项目在监测项目表中搜索监测项目的ID
                            string strPointIdTemp = strVerticalTableName == "" ? strPointId : strVerticalId;
                            string strItemId = getItemIdByName(strItemTableName, strPointIdTemp, strItemName);
                            if (strItemId == "") sb.AppendLine("EXCEL文档在第" + strPointNameRow + "行获取的【" + strItemName + "】监测项目ID无效" + Environment.NewLine);

                            string strItemValue = sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intColumn)).ToString();
                            if (strItemId != "")
                            {
                                string strFillPointItemSerialNum = GetSerialNumber(FillItemTableSerialNum);
                                string strItemSql = "insert into " + strFillItemTableName + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('" + strFillPointItemSerialNum + "','" + strFillPointSerialNum + "','" + strItemId + "','" + strItemValue + "')";
                                arrayList.Add(strItemSql);
                            }
                        }

                    }
                }
            }
            CreateLog(sb, "import");
            bool flag = false;
            if (arrayList.Count > 0)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(arrayList);
            }
            return flag;
        }
        #endregion

        #region//将Excel表导入数据库中，只能支持按年度、月份、日期、小时导入【支持导入噪声、降尘、空气类】
        /// <summary>
        /// 将Excel表导入数据库中，只能支持按年度、月份、日期、小时导入【支持导入噪声、降尘、空气类】
        /// </summary>
        /// <param name="strXmlUrl"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public bool importExcelSpecial(string strXmlUrl, ISheet sheet)
        {
            StringBuilder sb = new StringBuilder();

            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);

            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);

            //起始行
            string strStartRow = new XmlQuery().getElementAttribute(elementPoint, "startrow");
            if (strStartRow == "") sb.AppendLine("xml配置文档没有配置起始行信息" + Environment.NewLine);
            //结束行
            string strEndRow = new XmlQuery().getElementAttribute(elementPoint, "endrow");
            if (strEndRow == "") sb.AppendLine("xml配置文档没有配置结束行" + Environment.NewLine);

            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表" + Environment.NewLine);

            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报项目表" + Environment.NewLine);

            //数据填报表监测点字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表监测点字段" + Environment.NewLine);

            //数据填报表序列号
            string strFillTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillTableSerialNum")[0].InnerText;
            if (strFillTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填报表序列号" + Environment.NewLine);

            //数据填报监测项目表序列号
            string FillItemTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableSerialNum")[0].InnerText;
            if (FillItemTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填监测项目表序列号" + Environment.NewLine);

            //定义批量数据查询语句容器
            ArrayList arrayList = new ArrayList();

            int intStartRow = strStartRow == "" ? 0 : int.Parse(strStartRow);
            int intEndRow = strEndRow == "" ? 0 : int.Parse(strEndRow);

            //获取xml配置表中监测点关键项信息
            string strPointTableColumnName = "";
            string strPointTableColumn = "";

            //搜索监测点配置信息
            List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "Column");
            foreach (XmlElement elementPointTemp in pointConfigList)
            {
                string strTableCoumnName = elementPointTemp.GetAttribute("tablecolumn");
                string strTableCoumn = elementPointTemp.GetAttribute("column");
                string strPrimary = elementPointTemp.GetAttribute("primary").ToLower();
                if (strPrimary == "true")
                {
                    strPointTableColumnName = strTableCoumnName;
                    strPointTableColumn = strTableCoumn;
                    break;
                }
            }
            if (strPointTableColumnName == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项字段名称" + Environment.NewLine);
            if (strPointTableColumn == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项列号信息" + Environment.NewLine);

            //遍历行
            for (int i = intStartRow; i <= intEndRow; i++)
            {
                //定义根据行搜索到的年度和月份信息
                string strYear = "";
                string strMonth = "";
                string strWhereSql = "";
                string Primary = "";

                //搜索数据填报基础信息
                List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                {
                    string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn").ToLower();
                    string strPrimary = fillBaseTemp.GetAttribute("primary").ToLower();
                    string intColumn = fillBaseTemp.GetAttribute("column");
                    string strValue = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();

                    //组装删除数据填报监测项目表信息
                    if (strTableColumn == "year")
                    {
                        strWhereSql += " and YEAR='" + strValue + "'";
                        strYear = strValue;
                    }
                    if (strTableColumn == "month")
                    {
                        strWhereSql += " and MONTH='" + strValue + "'";
                        strMonth = strValue;
                    }
                    if (strPrimary == "true")
                    {
                        strWhereSql += " and Isnull(" + strTableColumn + ", '')='" + strValue + "'";
                        Primary = "true";
                    }
                }
                if (strYear == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置年度字段信息" + Environment.NewLine);
                if (strMonth == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置月度字段信息" + Environment.NewLine);
                if (Primary == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置关键项信息" + Environment.NewLine);

                //获取关键项的值
                string strPrimaryValue = sheet.GetRow(i).GetCell(int.Parse(strPointTableColumn)).ToString();
                if (strPrimaryValue == "") sb.AppendLine("Excel文档第" + i + "行获取关键项值失败" + Environment.NewLine);

                //根据监测点关键项信息、年度、月份获取监测点ID
                string strPointId = getPointId(strPointTableName, strPointTableColumnName, strPrimaryValue, strYear, strMonth);
                if (strPointId == "") sb.AppendLine("Excel文档第" + i + "行获取监测点ID失败" + Environment.NewLine);

                //如果已经设置监测点
                if (strPointId != "")
                {
                    string strDeleteSqlFillItem = @"delete from {0}
                                         where exists (select *
                                                  from {1}
                                                 where {1}.ID =
                                                       {0}.FILL_ID
                                                       and {3}='{4}' 
                                                       {2})";
                    strDeleteSqlFillItem = string.Format(strDeleteSqlFillItem, strFillItemTableName, strFillTableName, strWhereSql, strFillPointColumName, strPointId);
                    arrayList.Add(strDeleteSqlFillItem);

                    string strDeleteSqlFill = "delete from {0} where {2}='{3}' {1}";
                    strDeleteSqlFill = string.Format(strDeleteSqlFill, strFillTableName, strWhereSql, strFillPointColumName, strPointId);
                    arrayList.Add(strDeleteSqlFill);

                    //获取数据填报表序列号
                    string strFillPointSerialNum = GetSerialNumber(strFillTableSerialNum);
                    if (strFillPointSerialNum == "") sb.AppendLine("Excel文档第" + i + "行获取数据填报序列号失败" + Environment.NewLine);
                    if (fillBaseConfigList.Count == 0) sb.AppendLine("xml配置文档【FillInfo】没有配置信息" + Environment.NewLine);

                    //生成数据填报表数据写入SQL语句
                    string strSql1 = @"insert into " + strFillTableName + "(ID," + strFillPointColumName + "";
                    string strSql2 = @" values('" + strFillPointSerialNum + "','" + strPointId + "'";
                    foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                    {
                        string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn");
                        string intColumn = fillBaseTemp.GetAttribute("column");
                        string strValue = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                        strSql1 += "," + strTableColumn + "";
                        strSql2 += ",'" + strValue + "'";
                    }
                    strSql1 += ")";
                    strSql2 += ")";
                    string strSql = strSql1 + strSql2;
                    arrayList.Add(strSql);
                    //搜索数据填报监测项目信息
                    List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                    foreach (XmlElement fillItemTemp in fillItemConfigList)
                    {
                        string intColumn = fillItemTemp.GetAttribute("column");
                        if (intColumn == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目列号失败" + Environment.NewLine);

                        string strItemName = fillItemTemp.InnerText;
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目名称失败" + Environment.NewLine);

                        //根据监测项目在监测项目表中搜索监测项目的ID
                        string strPointIdTemp = strPointId;
                        string strItemId = getItemIdByName(strItemTableName, strPointIdTemp, strItemName);
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目【" + strItemName + "】监测点ID失败" + Environment.NewLine);

                        string strItemValue = sheet.GetRow(i).GetCell(int.Parse(intColumn)) == null ? "" : sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                        if (strItemId != "")
                        {
                            string strFillPointItemSerialNum = GetSerialNumber(FillItemTableSerialNum);
                            string strItemSql = "insert into " + strFillItemTableName + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('" + strFillPointItemSerialNum + "','" + strFillPointSerialNum + "','" + strItemId + "','" + strItemValue + "')";
                            arrayList.Add(strItemSql);
                        }
                    }
                }
            }
            CreateLog(sb, "import");
            bool flag = false;
            if (arrayList.Count > 0)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(arrayList);
            }
            return flag;
        }
        #endregion

        #region//将Excel表导入数据库中，只能支持按年度、月份、日期、小时导入【污染源常规（废水）】
        public bool ExcelSpecial(string strXmlUrl, ISheet sheet)
        {
            ArrayList arrayList = new ArrayList();
            StringBuilder sb = new StringBuilder();
            #region//判断
            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);
            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);
            //起始行
            string strStartRow = new XmlQuery().getElementAttribute(elementPoint, "startrow");
            if (strStartRow == "") sb.AppendLine("xml配置文档没有配置起始行信息" + Environment.NewLine);
            //结束行
            string strEndRow = new XmlQuery().getElementAttribute(elementPoint, "endrow");
            if (strEndRow == "") sb.AppendLine("xml配置文档没有配置结束行" + Environment.NewLine);
            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表" + Environment.NewLine);
            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报项目表" + Environment.NewLine);
            //数据填报表监测点字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表监测点字段" + Environment.NewLine);
            //数据填报表序列号
            string strFillTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillTableSerialNum")[0].InnerText;
            if (strFillTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填报表序列号" + Environment.NewLine);
            //数据填报监测项目表序列号
            string FillItemTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableSerialNum")[0].InnerText;
            if (FillItemTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填监测项目表序列号" + Environment.NewLine);
            #endregion
            //定义批量数据查询语句容器
            int intStartRow = strStartRow == "" ? 0 : int.Parse(strStartRow);
            int intEndRow = strEndRow == "" ? 0 : int.Parse(strEndRow);
            //获取xml配置表中监测点关键项信息
            string strPointTableColumnName = "";
            string strPointTableColumn = "";
            //搜索监测点配置信息
            List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "Column");
            foreach (XmlElement elementPointTemp in pointConfigList)
            {
                string strTableCoumnName = elementPointTemp.GetAttribute("tablecolumn");
                string strTableCoumn = elementPointTemp.GetAttribute("column");
                string strPrimary = elementPointTemp.GetAttribute("primary").ToLower();
                if (strPrimary == "true")
                {
                    strPointTableColumnName = strTableCoumnName;
                    strPointTableColumn = strTableCoumn;
                    break;
                }
            }
            if (strPointTableColumnName == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项字段名称" + Environment.NewLine);
            if (strPointTableColumn == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项列号信息" + Environment.NewLine);
            //遍历行
            for (int i = intStartRow; i <= intEndRow; i++)
            {
                //定义根据行搜索到的年度和月份信息
                string strYear = "";
                string strMonth = "";
                string strWhereSql = "";
                string Primary = "";
                string strValue = "";
                string strTableColumn = "";
                string strPrimary = "";
                string intColumn = "";
                //搜索数据填报基础信息
                List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                {
                    strTableColumn = fillBaseTemp.GetAttribute("tablecolumn").ToLower();
                    strPrimary = fillBaseTemp.GetAttribute("primary").ToLower();
                    intColumn = fillBaseTemp.GetAttribute("column");
                    string Value = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                    //组装删除数据填报监测项目表信息
                    if (strTableColumn == "year")
                    {
                        strWhereSql += " and YEAR='" + Value + "'";
                        strYear = Value;
                    }
                    if (strTableColumn == "month")
                    {
                        strWhereSql += " and MONTH='" + Value + "'";
                        strMonth = Value;
                    }
                    if (strPrimary == "true")
                    {
                        strValue = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                        strWhereSql += " and " + strTableColumn + "='" + strValue + "'";
                        Primary = "true";
                    }
                }
                if (strYear == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置年度字段信息" + Environment.NewLine);
                if (strMonth == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置月度字段信息" + Environment.NewLine);
                if (Primary == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置关键项信息" + Environment.NewLine);
                //获取关键项的值
                string strPrimaryValue = sheet.GetRow(i).GetCell(int.Parse(strPointTableColumn)).ToString();
                if (strPrimaryValue == "") sb.AppendLine("Excel文档第" + i + "行获取关键项值失败" + Environment.NewLine);
                //根据监测点关键项信息、年度、月份获取监测点ID
                DataTable objTable = getPointIdEx(strPrimaryValue, strYear, strMonth, strValue);
                if (objTable.Rows.Count == 0) sb.AppendLine("Excel文档第" + i + "行获取监测点ID失败" + Environment.NewLine);
                if (objTable.Rows.Count == 1)
                {
                    string strPointId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
                    string strDeleteSqlFillItem = @"delete from {0}
                                         where exists (select *
                                                  from {1}
                                                 where {1}.ID =
                                                       {0}.FILL_ID
                                                       and {3}='{4}' 
                                                       {2})";
                    strDeleteSqlFillItem = string.Format(strDeleteSqlFillItem, strFillItemTableName, strFillTableName, strWhereSql, strFillPointColumName, strPointId);
                    arrayList.Add(strDeleteSqlFillItem);//删除填报监测项目数据
                    string strDeleteSqlFill = "delete from {0} where {2}='{3}' {1}";
                    strDeleteSqlFill = string.Format(strDeleteSqlFill, strFillTableName, strWhereSql, strFillPointColumName, strPointId);//删除填报数据
                    arrayList.Add(strDeleteSqlFill);
                    //获取数据填报表序列号
                    string strFillPointSerialNum = GetSerialNumber(strFillTableSerialNum);
                    if (strFillPointSerialNum == "") sb.AppendLine("Excel文档第" + i + "行获取数据填报序列号失败" + Environment.NewLine);
                    if (fillBaseConfigList.Count == 0) sb.AppendLine("xml配置文档【FillInfo】没有配置信息" + Environment.NewLine);
                    //生成数据填报表数据写入SQL语句
                    string strSql1 = @"insert into " + strFillTableName + "(ID," + strFillPointColumName + "";
                    string strSql2 = @" values('" + strFillPointSerialNum + "','" + strPointId + "'";
                    foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                    {
                        string strTableColumns = fillBaseTemp.GetAttribute("tablecolumn");
                        string intColumns = fillBaseTemp.GetAttribute("column");
                        string strValues = sheet.GetRow(i).GetCell(int.Parse(intColumns)).ToString();
                        strSql1 += "," + strTableColumns + "";
                        strSql2 += ",'" + strValues + "'";
                    }
                    strSql1 += ")";
                    strSql2 += ")";
                    string strSql = strSql1 + strSql2;
                    arrayList.Add(strSql);
                    #region //搜索数据填报监测项目信息
                    List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                    foreach (XmlElement fillItemTemp in fillItemConfigList)
                    {
                        string intColumns = fillItemTemp.GetAttribute("column");
                        if (intColumns == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目列号失败" + Environment.NewLine);
                        string strItemName = fillItemTemp.InnerText;
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目名称失败" + Environment.NewLine);
                        //根据监测项目在监测项目表中搜索监测项目的ID
                        string strPointIdTemp = strPointId;
                        string strItemId = getItemIdByName(strItemTableName, strPointIdTemp, strItemName);
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目【" + strItemName + "】监测点ID失败" + Environment.NewLine);
                        string strItemValue = sheet.GetRow(i).GetCell(int.Parse(intColumns)).ToString();
                        if (strItemId != "")
                        {
                            string strFillPointItemSerialNum = GetSerialNumber(FillItemTableSerialNum);
                            string strItemSql = "insert into " + strFillItemTableName + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('" + strFillPointItemSerialNum + "','" + strFillPointSerialNum + "','" + strItemId + "','" + strItemValue + "')";
                            arrayList.Add(strItemSql);
                        }
                    }
                    #endregion
                }
            }
            CreateLog(sb, "import");
            bool flag = false;
            if (arrayList.Count > 0)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(arrayList);
            }
            return flag;
        }
        #endregion

        #region//将Excel表导入数据库中，只能支持按年度、月份、日期、小时导入【污染源常规（废气）】
        public bool ExcelSpecialAir(string strXmlUrl, ISheet sheet)
        {
            ArrayList arrayList = new ArrayList();
            StringBuilder sb = new StringBuilder();
            #region//判断
            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);
            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);
            //起始行
            string strStartRow = new XmlQuery().getElementAttribute(elementPoint, "startrow");
            if (strStartRow == "") sb.AppendLine("xml配置文档没有配置起始行信息" + Environment.NewLine);
            //结束行
            string strEndRow = new XmlQuery().getElementAttribute(elementPoint, "endrow");
            if (strEndRow == "") sb.AppendLine("xml配置文档没有配置结束行" + Environment.NewLine);
            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表" + Environment.NewLine);
            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报项目表" + Environment.NewLine);
            //数据填报表监测点字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表监测点字段" + Environment.NewLine);
            //数据填报表序列号
            string strFillTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillTableSerialNum")[0].InnerText;
            if (strFillTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填报表序列号" + Environment.NewLine);
            //数据填报监测项目表序列号
            string FillItemTableSerialNum = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableSerialNum")[0].InnerText;
            if (FillItemTableSerialNum == "") sb.AppendLine("xml配置文档没有配置数据填监测项目表序列号" + Environment.NewLine);
            #endregion
            //定义批量数据查询语句容器
            int intStartRow = strStartRow == "" ? 0 : int.Parse(strStartRow);
            int intEndRow = strEndRow == "" ? 0 : int.Parse(strEndRow);
            //获取xml配置表中监测点关键项信息
            string strPointTableColumnName = "";
            string strPointTableColumn = "";
            //搜索监测点配置信息
            List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "Column");
            foreach (XmlElement elementPointTemp in pointConfigList)
            {
                string strTableCoumnName = elementPointTemp.GetAttribute("tablecolumn");
                string strTableCoumn = elementPointTemp.GetAttribute("column");
                string strPrimary = elementPointTemp.GetAttribute("primary").ToLower();
                if (strPrimary == "true")
                {
                    strPointTableColumnName = strTableCoumnName;
                    strPointTableColumn = strTableCoumn;
                    break;
                }
            }
            if (strPointTableColumnName == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项字段名称" + Environment.NewLine);
            if (strPointTableColumn == "") sb.AppendLine("xml配置文档【PointInfo】配置节没有配置监测点关键项列号信息" + Environment.NewLine);
            //遍历行
            for (int i = intStartRow; i <= intEndRow; i++)
            {
                //定义根据行搜索到的年度和月份信息
                string strYear = "";
                string strMonth = "";
                string strWhereSql = "";
                string Primary = "";
                string strValue = "";
                string strTableColumn = "";
                string strPrimary = "";
                string intColumn = "";
                //搜索数据填报基础信息
                List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                {
                    strTableColumn = fillBaseTemp.GetAttribute("tablecolumn").ToLower();
                    strPrimary = fillBaseTemp.GetAttribute("primary").ToLower();
                    intColumn = fillBaseTemp.GetAttribute("column");
                    string Value = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                    //组装删除数据填报监测项目表信息
                    if (strTableColumn == "year")
                    {
                        strWhereSql += " and YEAR='" + Value + "'";
                        strYear = Value;
                    }
                    if (strTableColumn == "month")
                    {
                        strWhereSql += " and MONTH='" + Value + "'";
                        strMonth = Value;
                    }
                    if (strPrimary == "true")
                    {
                        strValue = sheet.GetRow(i).GetCell(int.Parse(intColumn)).ToString();
                        strWhereSql += " and " + strTableColumn + "='" + strValue + "'";
                        Primary = "true";
                    }
                }
                if (strYear == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置年度字段信息" + Environment.NewLine);
                if (strMonth == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置月度字段信息" + Environment.NewLine);
                if (Primary == "") sb.AppendLine("xml配置文档【FillInfo】配置节没有配置关键项信息" + Environment.NewLine);
                //获取关键项的值
                string strPrimaryValue = sheet.GetRow(i).GetCell(int.Parse(strPointTableColumn)).ToString();
                if (strPrimaryValue == "") sb.AppendLine("Excel文档第" + i + "行获取关键项值失败" + Environment.NewLine);
                //根据监测点关键项信息、年度、月份获取监测点ID
                DataTable objTable = getPointIdExs(strPrimaryValue, strYear, strMonth, strValue);
                if (objTable.Rows.Count == 0) sb.AppendLine("Excel文档第" + i + "行获取监测点ID失败" + Environment.NewLine);
                if (objTable.Rows.Count == 1)
                {
                    string strPointId = objTable.Rows[0]["ID"] == null ? "" : objTable.Rows[0]["ID"].ToString();
                    string strDeleteSqlFillItem = @"delete from {0}
                                         where exists (select *
                                                  from {1}
                                                 where {1}.ID =
                                                       {0}.FILL_ID
                                                       and {3}='{4}' 
                                                       {2})";
                    strDeleteSqlFillItem = string.Format(strDeleteSqlFillItem, strFillItemTableName, strFillTableName, strWhereSql, strFillPointColumName, strPointId);
                    arrayList.Add(strDeleteSqlFillItem);//删除填报监测项目数据
                    string strDeleteSqlFill = "delete from {0} where {2}='{3}' {1}";
                    strDeleteSqlFill = string.Format(strDeleteSqlFill, strFillTableName, strWhereSql, strFillPointColumName, strPointId);//删除填报数据
                    arrayList.Add(strDeleteSqlFill);
                    //获取数据填报表序列号
                    string strFillPointSerialNum = GetSerialNumber(strFillTableSerialNum);
                    if (strFillPointSerialNum == "") sb.AppendLine("Excel文档第" + i + "行获取数据填报序列号失败" + Environment.NewLine);
                    if (fillBaseConfigList.Count == 0) sb.AppendLine("xml配置文档【FillInfo】没有配置信息" + Environment.NewLine);
                    //生成数据填报表数据写入SQL语句
                    string strSql1 = @"insert into " + strFillTableName + "(ID," + strFillPointColumName + "";
                    string strSql2 = @" values('" + strFillPointSerialNum + "','" + strPointId + "'";
                    foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                    {
                        string strTableColumns = fillBaseTemp.GetAttribute("tablecolumn");
                        string intColumns = fillBaseTemp.GetAttribute("column");
                        string strValues = sheet.GetRow(i).GetCell(int.Parse(intColumns)).ToString();
                        strSql1 += "," + strTableColumns + "";
                        strSql2 += ",'" + strValues + "'";
                    }
                    strSql1 += ")";
                    strSql2 += ")";
                    string strSql = strSql1 + strSql2;
                    arrayList.Add(strSql);
                    #region //搜索数据填报监测项目信息
                    List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                    foreach (XmlElement fillItemTemp in fillItemConfigList)
                    {
                        string OQty = string.Empty; string Up_Line = string.Empty; string Down_Line = string.Empty; string Uom = string.Empty; string Standard = string.Empty;
                        string PollutePer = string.Empty; string PolluteCalPer = string.Empty; string Is_Standard = string.Empty; string AirQty = string.Empty;
                        string intColumns = fillItemTemp.GetAttribute("column");
                        if (intColumns == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目列号失败" + Environment.NewLine);
                        string strItemName = fillItemTemp.GetAttribute("ItemName");
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目名称失败" + Environment.NewLine);
                        //根据监测项目在监测项目表中搜索监测项目的ID
                        string strPointIdTemp = strPointId;
                        string strItemId = getItemIdByName(strItemTableName, strPointIdTemp, strItemName);
                        if (strItemName == "") sb.AppendLine("Excel文档第" + i + "行获取监测项目【" + strItemName + "】监测点ID失败" + Environment.NewLine);
                        string strItemValue = sheet.GetRow(i).GetCell(int.Parse(intColumns)).ToString();
                        if (strItemId != "")
                        {
                            string strFillPointItemSerialNum = GetSerialNumber(FillItemTableSerialNum);
                            //搜索监测项目下的评价配置信息
                            List<XmlElement> fillItemEvaluationConfigList = new XmlQuery().xmlElementSearch(fillItemTemp, "Evaluation");
                            foreach (XmlElement fillItemEvaluationTemp in fillItemEvaluationConfigList)
                            {
                                string strEvaluationRowType = fillItemEvaluationTemp.GetAttribute("rowtype").Trim();
                                string strEvaluationTableColumn = fillItemEvaluationTemp.GetAttribute("tablecolumn").Trim();
                                string strEvaluationColumn = fillItemEvaluationTemp.GetAttribute("column").Trim();
                                if (strEvaluationRowType != "" && strEvaluationTableColumn != "" && strEvaluationColumn != "")
                                {
                                    if (strEvaluationTableColumn.Equals("OQty"))
                                    {
                                        OQty = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("Up_Line"))
                                    {
                                        Up_Line = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("Down_Line"))
                                    {
                                        Down_Line = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("Uom"))
                                    {
                                        Uom = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("Standard"))
                                    {
                                        Standard = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("PollutePer"))
                                    {
                                        PollutePer = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("PolluteCalPer"))
                                    {
                                        PolluteCalPer = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("Is_Standard"))
                                    {
                                        Is_Standard = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                    else if (strEvaluationTableColumn.Equals("AirQty"))
                                    {
                                        AirQty = sheet.GetRow(i).GetCell(int.Parse(strEvaluationColumn)).ToString();
                                    }
                                }
                            }

                            string strItemSql = "insert into " + strFillItemTableName + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE,OQty,Up_Line,Down_Line,Uom,Standard,PollutePer,PolluteCalPer,Is_Standard,AirQty) values('" + strFillPointItemSerialNum + "','" + strFillPointSerialNum + "','" + strItemId + "','" + strItemValue + "','" + OQty + "','" + Up_Line + "','" + Down_Line + "','" + Uom + "','" + Standard + "','" + PollutePer + "','" + PolluteCalPer + "','" + Is_Standard + "','" + AirQty + "')";
                            arrayList.Add(strItemSql);
                        }
                    }
                    #endregion
                }
            }
            CreateLog(sb, "import");
            bool flag = false;
            if (arrayList.Count > 0)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(arrayList);
            }
            return flag;
        }
        #endregion

        #region//获取需要导出的工作簿对象【支持导入河流、湖泊类】
        /// <summary>
        /// 获取需要导出的工作簿对象【支持导入河流、湖泊类】
        /// </summary>
        /// <param name="strXmlUrl">配置文件路径</param>
        /// <param name="strExcelTempleUrl">需要导出的Excel模板路径</param>
        /// <param name="strSheetName">工作薄名称</param>
        /// <param name="strYear">年份</param>
        /// <param name="strMonth">月份</param>
        /// <returns></returns>
        public ISheet GetExportExcelSheet(string strXmlUrl, string strExcelTempleUrl, string strSheetName, string strYear, string strMonth)
        {
            StringBuilder sb = new StringBuilder();
            strMonth = int.Parse(strMonth).ToString();

            if (strYear == "") sb.AppendLine("输入的年度无效" + Environment.NewLine);
            if (strMonth == "") sb.AppendLine("输入的月度无效" + Environment.NewLine);

            FileStream file = new FileStream(strExcelTempleUrl, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet(strSheetName);
            if (sheet == null) sb.AppendLine("获取的Excel无效" + Environment.NewLine);

            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);

            //垂线表【非必须】
            string strVerticalTableName = new XmlQuery().getElementAttribute(elementPoint, "verticaltable");
            if (strVerticalTableName == "") sb.AppendLine("xml配置文档没有配置垂线表名称名称【非必须】" + Environment.NewLine);

            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);

            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表名称" + Environment.NewLine);

            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报项目表名称" + Environment.NewLine);

            //数据填报表断面字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表断面字段名称" + Environment.NewLine);

            //数据填报表垂线字段名称【非必须】
            string strFillVerticalColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTableVerticalColumnName")[0].InnerText;
            if (strFillVerticalColumName == "") sb.AppendLine("xml配置文档没有配置数据填报表垂线字段名称【非必须】" + Environment.NewLine);

            //搜索监测点配置信息
            List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "SubPoint");
            foreach (XmlElement elementPointTemp in pointConfigList)
            {
                List<XmlElement> pointCodeList = new XmlQuery().xmlElementSearch(elementPointTemp, "SubPointCode");
                List<XmlElement> pointNameList = new XmlQuery().xmlElementSearch(elementPointTemp, "SubPointName");
                List<XmlElement> verticalList = new XmlQuery().xmlElementSearch(elementPointTemp, "Vertical");

                string strPointCode = pointCodeList.Count == 1 ? pointCodeList[0].InnerText : "";
                string strPointCodeRow = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("row") : "";
                string strPointCodeColum = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("column") : "";
                string strPointCodeTableColum = pointCodeList.Count == 1 ? pointCodeList[0].GetAttribute("tablecolumn") : "";

                string strPointName = pointNameList.Count == 1 ? pointNameList[0].InnerText : "";
                string strPointNameRow = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("row") : "";
                string strPointNameColum = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("column") : "";
                string strPointNameTableColum = pointNameList.Count == 1 ? pointNameList[0].GetAttribute("tablecolumn") : "";

                string strVertical = verticalList.Count == 1 ? verticalList[0].InnerText : "";
                string strVerticalRow = verticalList.Count == 1 ? verticalList[0].GetAttribute("row") : "";
                string strVerticalColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("column") : "";
                string strVerticalPointTableColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("pointcolumn") : "";
                string strVerticalTableColum = verticalList.Count == 1 ? verticalList[0].GetAttribute("tablecolumn") : "";

                //定义监测点和垂线ID
                string strPointId = "";
                string strVerticalId = "";
                string strSql = "";
                //获取断面和垂线ID
                if (strVerticalTableName != "")
                {
                    //如果存在垂线情况下
                    strPointId = getPointId(strPointTableName, strPointNameTableColum, strPointName, strYear, strMonth);
                    strVerticalId = getVerticalId(strVerticalTableName, strVerticalPointTableColum, strPointId, strVerticalTableColum, strVertical);

                    //根据断面或者垂线获取数据填报信息
                    strSql = @"select * from {0} where {1}='{2}' and {3}='{4}' and YEAR='{5}' and MONTH='{6}'";
                    strSql = string.Format(strSql, strFillTableName, strFillPointColumName, strPointId, strFillVerticalColumName, strVerticalId, strYear, strMonth);
                }
                else
                {
                    //如果不存在垂线的情况下
                    strPointId = getPointId(strPointTableName, strPointNameTableColum, strPointName, strYear, strMonth);

                    //根据断面或者垂线获取数据填报信息
                    strSql = @"select * from {0} where {1}='{2}' and YEAR='{3}' and MONTH='{4}'";
                    strSql = string.Format(strSql, strFillTableName, strFillPointColumName, strPointId, strYear, strMonth);
                }

                if (strPointId == "") sb.AppendLine("获取断面【" + strPointName + "】ID失败" + Environment.NewLine);
                if (strVerticalId == "") sb.AppendLine("获取垂线【" + strVertical + "】ID失败" + Environment.NewLine);

                DataTable objFillTable = SqlHelper.ExecuteDataTable(strSql);
                if (objFillTable.Rows.Count == 0) sb.AppendLine("断面【" + strPointName + "】获取数据填报信息失败" + Environment.NewLine);

                if (objFillTable.Rows.Count == 1)
                {
                    //获取数据填报ID
                    string fillTableId = objFillTable.Rows[0]["ID"].ToString();
                    //搜索数据填报基础信息
                    List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                    foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                    {
                        string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn");
                        string intColumn = fillBaseTemp.GetAttribute("column");
                        if (intColumn == "") sb.AppendLine("断面【" + strPointName + "】获取字段【" + strTableColumn + "】列信息失败" + Environment.NewLine);

                        string strValue = objFillTable.Rows[0][strTableColumn] == null ? "" : objFillTable.Rows[0][strTableColumn].ToString();
                        sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intColumn)).SetCellValue(strValue);
                    }
                    //搜索数据填报监测项目信息
                    List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                    foreach (XmlElement fillItemTemp in fillItemConfigList)
                    {
                        string intColumn = fillItemTemp.GetAttribute("column");
                        string strItemName = fillItemTemp.GetAttribute("ItemName");

                        if (strItemName == "") sb.AppendLine("断面【" + strPointName + "】获取监测项目名称失败" + Environment.NewLine);
                        if (intColumn == "") sb.AppendLine("断面【" + strPointName + "】获取监测项目【" + strItemName + "】列信息失败" + Environment.NewLine);

                        //根据监测项目在监测项目表中搜索监测项目的ID
                        string strPointIdTemp = strVerticalTableName == "" ? strPointId : strVerticalId;
                        string strItemId = getItemIdByName(strItemTableName, strPointIdTemp, strItemName);
                        if (strItemId == "") sb.AppendLine("断面【" + strPointName + "】获取监测项目【" + strItemName + "】ID失败" + Environment.NewLine);

                        //根据数据填报ID和监测项目ID获取数据填报监测项目信息
                        DataTable objFillItemTable = getFillItemInfo(strFillItemTableName, fillTableId, strItemId);
                        if (objFillItemTable.Rows.Count == 0) sb.AppendLine("断面【" + strPointName + "】获取【" + strItemName + "】数据失败" + Environment.NewLine);

                        if (objFillItemTable.Rows.Count == 1)
                        {
                            //获取监测项目值
                            string strItemValue = objFillItemTable.Rows[0]["ITEM_VALUE"] == null ? "" : objFillItemTable.Rows[0]["ITEM_VALUE"].ToString();
                            sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(intColumn)).SetCellValue(strItemValue);

                            //搜索监测项目下的评价配置信息
                            List<XmlElement> fillItemEvaluationConfigList = new XmlQuery().xmlElementSearch(fillItemTemp, "Evaluation");
                            if (fillItemEvaluationConfigList.Count == 0) sb.AppendLine("断面【" + strPointName + "】【" + strItemName + "】评价配置信息不存在" + Environment.NewLine);

                            foreach (XmlElement fillItemEvaluationTemp in fillItemEvaluationConfigList)
                            {
                                string strEvaluationRowType = fillItemEvaluationTemp.GetAttribute("rowtype").Trim();
                                string strEvaluationTableColumn = fillItemEvaluationTemp.GetAttribute("tablecolumn").Trim();
                                string strEvaluationColumn = fillItemEvaluationTemp.GetAttribute("column").Trim();
                                if (strEvaluationRowType != "" && strEvaluationTableColumn != "" && strEvaluationColumn != "")
                                {
                                    string strEvaluationValue = objFillItemTable.Rows[0][strEvaluationTableColumn] == null ? "" : objFillItemTable.Rows[0][strEvaluationTableColumn].ToString();
                                    if (strEvaluationRowType == "bottom")
                                    {
                                        sheet.GetRow(int.Parse(strPointNameRow) + 1).GetCell(int.Parse(strEvaluationColumn)).SetCellValue(strEvaluationValue);
                                    }
                                    else
                                    {
                                        sheet.GetRow(int.Parse(strPointNameRow)).GetCell(int.Parse(strEvaluationColumn)).SetCellValue(strEvaluationValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CreateLog(sb, "export");
            return sheet;
        }
        #endregion

        #region//获取需要导出的工作簿对象【支持导入噪声、降尘、空气类】
        /// <summary>
        /// 获取需要导出的工作簿对象【支持导入噪声、降尘、空气类】
        /// </summary>
        /// <param name="strXmlUrl">配置文件路径</param>
        /// <param name="strExcelTempleUrl">需要导出的Excel模板路径</param>
        /// <param name="strSheetName">工作薄名称</param>
        /// <param name="strYear">年份</param>
        /// <param name="strMonth">月份</param>
        /// <returns></returns>
        public ISheet GetExportExcelSheetSpecial(string strXmlUrl, string strExcelTempleUrl, string strSheetName, string strYear, string strMonth)
        {
            StringBuilder sb = new StringBuilder();
            strMonth = int.Parse(strMonth).ToString();

            if (strYear == "") sb.AppendLine("输入的年度无效" + Environment.NewLine);
            if (strMonth == "") sb.AppendLine("输入的月度无效" + Environment.NewLine);

            FileStream file = new FileStream(strExcelTempleUrl, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet(strSheetName);
            if (sheet == null) sb.AppendLine("获取的Excel无效" + Environment.NewLine);

            XmlDocument document = new XmlQuery().createXmlDocument(strXmlUrl);
            //基础监测点配置信息
            XmlElement elementPoint = new XmlQuery().getRootElementInfo(document, "Point");
            //数据填报配置信息
            XmlElement elementFill = new XmlQuery().getRootElementInfo(document, "FillData");
            //基础点位表【必须】
            string strPointTableName = new XmlQuery().getElementAttribute(elementPoint, "pointtable");
            if (strPointTableName == "") sb.AppendLine("xml配置文档没有配置点位表名称" + Environment.NewLine);

            //监测项目表
            string strItemTableName = new XmlQuery().getElementAttribute(elementPoint, "itemtable");
            if (strItemTableName == "") sb.AppendLine("xml配置文档没有配置监测项目表名称" + Environment.NewLine);

            //数据填报表【必须】
            string strFillTableName = new XmlQuery().xmlElementSearch(elementFill, "FillTableName")[0].InnerText;
            if (strFillTableName == "") sb.AppendLine("xml配置文档没有配置数据填报表名称" + Environment.NewLine);

            //数据填报项目表【必须】
            string strFillItemTableName = new XmlQuery().xmlElementSearch(elementFill, "FillItemTableName")[0].InnerText;
            if (strFillItemTableName == "") sb.AppendLine("xml配置文档没有配置数据填报项目表名称" + Environment.NewLine);

            //数据填报表监测点字段名称【必须】
            string strFillPointColumName = new XmlQuery().xmlElementSearch(elementFill, "FillTablePointColumnName")[0].InnerText;
            if (strFillPointColumName == "") sb.AppendLine("xml配置文档没有配置数据填报监测点字段名称" + Environment.NewLine);

            //起始行
            string strStartRow = new XmlQuery().getElementAttribute(elementPoint, "startrow");
            if (strStartRow == "") sb.AppendLine("xml配置文档没有配置起始行信息" + Environment.NewLine);

            //根据监测点、年份、月份查询监测点和数据填报信息
            string strSql = @"select * from {0} where YEAR='{1}' and MONTH='{2}'";
            strSql = string.Format(strSql, strFillTableName, strYear, strMonth);
            DataTable objFillTable = SqlHelper.ExecuteDataTable(strSql);
            if (objFillTable.Rows.Count == 0) sb.AppendLine("未查询到数据填报表数据信息" + Environment.NewLine);

            //遍历结果填报表
            for (int i = 0; i < objFillTable.Rows.Count; i++)
            {
                string strFillId = objFillTable.Rows[i]["ID"].ToString();
                string strPointId = objFillTable.Rows[i][strFillPointColumName].ToString();
                //查询监测点详细信息
                strSql = "select * from {0} where ID='{1}'";
                strSql = string.Format(strSql, strPointTableName, strPointId);
                DataTable objPointInfoTable = SqlHelper.ExecuteDataTable(strSql);
                if (objPointInfoTable.Rows.Count == 0) sb.AppendLine("未查询到第" + i.ToString() + "行填报数据对应的监测点信息" + Environment.NewLine);

                if (objPointInfoTable.Rows.Count > 0)
                {
                    //录入基础数据信息
                    //搜索监测点配置信息
                    List<XmlElement> pointConfigList = new XmlQuery().xmlElementSearch(elementPoint, "Column");
                    IRow excelRow;
                    ICell excelCell;
                    foreach (XmlElement elementPointTemp in pointConfigList)
                    {
                        string strTableCoumnName = elementPointTemp.GetAttribute("tablecolumn");
                        if (strTableCoumnName == "") sb.AppendLine("xml配置文档没有配置【PointInfo】配置节数据表字段名称" + Environment.NewLine);

                        string strTableCoumn = elementPointTemp.GetAttribute("column");
                        if (strTableCoumn == "") sb.AppendLine("xml配置文档没有配置【PointInfo】配置节EXCEL列信息" + Environment.NewLine);

                        string strPointValue = objPointInfoTable.Rows[0][strTableCoumnName] == null ? "" : objPointInfoTable.Rows[0][strTableCoumnName].ToString();

                        excelRow = sheet.GetRow(int.Parse(strStartRow) + i);
                        if (excelRow != null)
                        {
                            excelCell = excelRow.GetCell(int.Parse(strTableCoumn));
                            if (excelCell == null)
                            {
                                excelCell = excelRow.CreateCell(int.Parse(strTableCoumn));
                            }
                        }
                        else
                        {
                            excelCell = sheet.CreateRow(int.Parse(strStartRow) + i).CreateCell(int.Parse(strTableCoumn));
                        }
                        excelCell.SetCellValue(strPointValue);
                    }
                    //搜索数据填报基础信息
                    List<XmlElement> fillBaseConfigList = new XmlQuery().xmlElementSearch(elementFill, "FillData");
                    foreach (XmlElement fillBaseTemp in fillBaseConfigList)
                    {
                        string strTableColumn = fillBaseTemp.GetAttribute("tablecolumn");
                        if (strTableColumn == "") sb.AppendLine("xml配置文档没有配置【FillInfo】配置节数据表字段名称" + Environment.NewLine);

                        string intColumn = fillBaseTemp.GetAttribute("column");
                        if (intColumn == "") sb.AppendLine("xml配置文档没有配置【FillInfo】配置节列信息" + Environment.NewLine);

                        string strFillValue = objFillTable.Rows[i][strTableColumn] == null ? "" : objFillTable.Rows[i][strTableColumn].ToString();

                        excelRow = sheet.GetRow(int.Parse(strStartRow) + i);
                        if (excelRow != null)
                        {
                            excelCell = excelRow.GetCell(int.Parse(intColumn));
                            if (excelCell == null)
                            {
                                excelCell = excelRow.CreateCell(int.Parse(intColumn));
                            }
                        }
                        else
                        {
                            excelCell = sheet.CreateRow(int.Parse(strStartRow) + i).CreateCell(int.Parse(intColumn));
                        }
                        excelCell.SetCellValue(strFillValue);

                    }
                    //搜索数据填报监测项目信息
                    List<XmlElement> fillItemConfigList = new XmlQuery().xmlElementSearch(elementFill, "Item");
                    foreach (XmlElement fillItemTemp in fillItemConfigList)
                    {
                        string strItemName = fillItemTemp.GetAttribute("ItemName");
                        if (strItemName == "") sb.AppendLine("xml配置文档没有配置【Item】配置节获取监测项目名称失败" + Environment.NewLine);

                        string intColumn = fillItemTemp.GetAttribute("column");
                        if (intColumn == "") sb.AppendLine("xml配置文档没有配置【Item】配置节获取监测项目列信息失败" + Environment.NewLine);

                        //根据监测项目在监测项目表中搜索监测项目的ID
                        string strItemId = getItemIdByName(strItemTableName, strPointId, strItemName);
                        if (strItemId == "") sb.AppendLine("获取第" + i.ToString() + "行" + strItemName + "监测项目ID失败" + Environment.NewLine);

                        //根据数据填报ID和监测项目ID获取数据填报监测项目信息
                        DataTable objFillItemTable = getFillItemInfo(strFillItemTableName, strFillId, strItemId);
                        if (objFillItemTable.Rows.Count == 0) sb.AppendLine("获取第" + i.ToString() + "行数据填报监测项目信息失败" + Environment.NewLine);

                        if (objFillItemTable.Rows.Count == 1)
                        {
                            //获取监测项目值
                            string strItemValue = objFillItemTable.Rows[0]["ITEM_VALUE"] == null ? "" : objFillItemTable.Rows[0]["ITEM_VALUE"].ToString();

                            excelRow = sheet.GetRow(int.Parse(strStartRow) + i);
                            if (excelRow != null)
                            {
                                excelCell = excelRow.GetCell(int.Parse(intColumn));
                                if (excelCell == null)
                                {
                                    excelCell = excelRow.CreateCell(int.Parse(intColumn));
                                }
                            }
                            else
                            {
                                excelCell = sheet.CreateRow(int.Parse(strStartRow) + i).CreateCell(int.Parse(intColumn));
                            }
                            excelCell.SetCellValue(strItemValue);
                            
                            //搜索监测项目下的评价配置信息
                            List<XmlElement> fillItemEvaluationConfigList = new XmlQuery().xmlElementSearch(fillItemTemp, "Evaluation");
                            if (fillItemEvaluationConfigList.Count == 0) sb.AppendLine("【" + strItemName + "】评价配置信息不存在" + Environment.NewLine);

                            foreach (XmlElement fillItemEvaluationTemp in fillItemEvaluationConfigList)
                            {
                                string strEvaluationRowType = fillItemEvaluationTemp.GetAttribute("rowtype").Trim();
                                string strEvaluationTableColumn = fillItemEvaluationTemp.GetAttribute("tablecolumn").Trim();
                                string strEvaluationColumn = fillItemEvaluationTemp.GetAttribute("column").Trim();
                                if (strEvaluationRowType != "" && strEvaluationTableColumn != "" && strEvaluationColumn != "")
                                {
                                    string strEvaluationValue = objFillItemTable.Rows[0][strEvaluationTableColumn] == null ? "" : objFillItemTable.Rows[0][strEvaluationTableColumn].ToString();
                                    if (strEvaluationRowType == "bottom")
                                    {
                                        excelRow = sheet.GetRow(int.Parse(strStartRow) + i + 1);
                                        if (excelRow != null)
                                        {
                                            excelCell = excelRow.GetCell(int.Parse(strEvaluationColumn));
                                            if (excelCell == null)
                                            {
                                                excelCell = excelRow.CreateCell(int.Parse(strEvaluationColumn));
                                            }
                                        }
                                        else
                                        {
                                            excelCell = sheet.CreateRow(int.Parse(strStartRow) + i + 1).CreateCell(int.Parse(strEvaluationColumn));
                                        }
                                        excelCell.SetCellValue(strEvaluationValue);
                                        
                                    }
                                    else
                                    {
                                        excelRow = sheet.GetRow(int.Parse(strStartRow) + i);
                                        if (excelRow != null)
                                        {
                                            excelCell = excelRow.GetCell(int.Parse(strEvaluationColumn));
                                            if (excelCell == null)
                                            {
                                                excelCell = excelRow.CreateCell(int.Parse(strEvaluationColumn));
                                            }
                                        }
                                        else
                                        {
                                            excelCell = sheet.CreateRow(int.Parse(strStartRow) + i).CreateCell(int.Parse(strEvaluationColumn));
                                        }
                                        excelCell.SetCellValue(strEvaluationValue);
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CreateLog(sb, "export");
            return sheet;
        }
        #endregion
        /// <summary>
        /// 创建EXCEL数据导入导出日志
        /// </summary>
        /// <param name="sb">日志集</param>
        /// <param name="type"></param>
        private void CreateLog(StringBuilder sb, string type)
        {
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Channels\\Env\\Log";
            string fileName = strPath + "\\" + type + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            if (sb.Length != 0)
            {
                StreamWriter sr = File.CreateText(fileName);
                sr.WriteLine(sb.ToString());
                sr.Close();
            }
        }
    }
}