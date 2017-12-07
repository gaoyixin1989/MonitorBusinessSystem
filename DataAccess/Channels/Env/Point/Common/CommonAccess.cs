using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using i3.ValueObject;
using System.Data;
using System.Text.RegularExpressions;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
namespace i3.DataAccess.Channels.Env.Point.Common
{
    /// <summary>
    /// 功能：公共方法
    /// 创建日期：2013-06-08
    /// 创建人：魏林
    /// </summary>
    public class CommonAccess : SqlHelper
    {
        /// <summary>
        /// 批量保存点位监测项目数据
        /// </summary>
        /// <param name="strTableName">点位监测项目表名</param>
        ///<param name="strPoint_ID">点位ID值</param>
        ///<param name="strItemValue">监测项目ID值</param>
        /// <returns></returns>
        public bool SaveItemInfo(string strTableName, string strPoint_ID, string strItemValue, string strSerialId)
        {
            ArrayList list = new ArrayList();
            string strsql = "delete from {0} where POINT_ID='{1}'";
            strsql = string.Format(strsql, strTableName, strPoint_ID);
            list.Add(strsql);

            if (strItemValue.Trim().Length > 0)
            {
                List<string> values = strItemValue.Split(',').ToList();
                foreach (string valueTemp in values)
                {
                    strsql = "insert into {0}(ID,POINT_ID,ITEM_ID) values('{1}','{2}','{3}')";
                    strsql = string.Format(strsql, strTableName, GetSerialNumber(strSerialId), strPoint_ID, valueTemp);
                    list.Add(strsql);

                }
            }
            return ExecuteSQLByTransaction(list);
        }
        /// <summary>
        /// 点位复制功能的sql
        /// </summary>
        /// <param name="list"></param>
        /// <param name="row"></param>
        /// <param name="dt"></param>
        /// <param name="strSerialID"></param>
        public string getCopySql(string TableName, DataRow row, string Year, string Month, string PreID, string strSerialID)
        {
            string sql = string.Empty;
            string strFields = string.Empty;
            string strValues = string.Empty;

            for (int i = 0; i < row.ItemArray.Length; i++)
            {
                strFields += row.Table.Columns[i].ColumnName.ToString() + ",";

                switch (row.Table.Columns[i].ColumnName.ToString())
                {
                    case "ID":
                        strValues += "'" + strSerialID + "',";
                        break;
                    case "YEAR":
                        strValues += "'" + Year + "',";
                        break;
                    case "MONTH":
                        strValues += "'" + Month + "',";
                        break;
                    case "POINT_ID":
                        strValues += "'" + PreID + "',";
                        break;
                    case "SECTION_ID":
                        strValues += "'" + PreID + "',";
                        break;
                    default:
                        strValues += "'" + row[i].ToString() + "',";
                        break;
                }
            }
            sql = "insert into " + TableName + "(" + strFields.TrimEnd(',') + ") values(" + strValues.TrimEnd(',') + ")";
            return sql;
        }
        /// <summary>
        /// 点位复制降水监测功能的sql(Created by ljn 2013/6/18)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="row"></param>
        /// <param name="dt"></param>
        /// <param name="strSerialID"></param>
        public string GetCopyRain_ItemSql(string TableName, DataRow row, string Year, string PreID, string strSerialID)
        {
            string sql = string.Empty;
            string strFields = string.Empty;
            string strValues = string.Empty;

            //Year有值时表示主表，为空时表示子表
            if (Year != "")
                strValues += "'" + strSerialID + "','" + Year + "',";
            else
                strValues += "'" + strSerialID + "','" + PreID + "',";
            for (int i = 0; i < row.ItemArray.Length; i++)
            {
                if (i == row.ItemArray.Length - 1)
                {
                    strFields += row.Table.Columns[i].ColumnName.ToString();
                    strValues += "'" + row[i].ToString() + "'";
                }
                else
                {
                    strFields += row.Table.Columns[i].ColumnName.ToString() + ",";
                    if (row.Table.Columns[i].ColumnName.ToString() != "ID" && row.Table.Columns[i].ColumnName.ToString() != "YEAR" && row.Table.Columns[i].ColumnName.ToString() != "RAIN_POINT_ID" && row.Table.Columns[i].ColumnName.ToString() != "SECTION_ID")
                    {
                        strValues += "'" + row[i].ToString() + "',";
                    }
                }
            }
            sql = "insert into " + TableName + "(" + strFields + ") values(" + strValues + ")";
            return sql;
        }

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="strID">监测项目ID</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.ID,a.ITEM_NAME,b.ANALYSIS_METHOD_ID,d.METHOD_NAME METHOD,c.ANALYSIS_NAME ANALYSIS_METHOD,b.INSTRUMENT_ID,e.Name INSTRUMENT,f.DICT_TEXT UNIT,b.LOWER_CHECKOUT LOWER_CHECKOUT ");
            sql.Append("from T_BASE_ITEM_INFO a left join T_BASE_ITEM_ANALYSIS b on(a.ID=b.ITEM_ID) ");
            sql.Append("left join T_BASE_METHOD_ANALYSIS c on(b.ANALYSIS_METHOD_ID=c.ID) ");
            sql.Append("left join T_BASE_METHOD_INFO d on(c.METHOD_ID=d.ID) ");
            sql.Append("left join T_BASE_APPARATUS_INFO e on(b.INSTRUMENT_ID=e.ID) ");
            sql.Append("left join T_SYS_DICT f on(b.UNIT=f.ID and f.DICT_TYPE='item_unit') ");
            sql.Append("where a.ID='" + strID + "'");

            return SqlHelper.ExecuteDataTable(sql.ToString());
        }

        /// <summary>
        /// 通用方法，获取垂线监测项目资料
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strWhereColumnName">条件字段名称</param>
        /// <param name="strColumnValue">条件字段数据</param>
        /// <returns></returns>
        public DataTable getVerticalItem(string strTableName, string strWhereColumnName, string strColumnValue)
        {
            string strSql = @"select * from  {0} where {1} = '{2}'";
            strSql = string.Format(strSql, strTableName, strWhereColumnName, strColumnValue);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取更新填报的Insert语句
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ID"></param>
        /// <param name="FillTable"></param>
        /// <returns></returns>
        public string getInsertSql(DataTable dt, string ID, string FillTable)
        {
            string sql = "";
            string columns = "ID,";
            string values = "'" + ID + "',";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                columns += dt.Columns[i].ColumnName;
                values += "'" + dt.Rows[0][i].ToString() + "'";
                if (i != dt.Columns.Count - 1)
                {
                    columns += ",";
                    values += ",";
                }
            }
            sql = "insert into " + FillTable + "(" + columns + ") values(" + values + ")";

            return sql;
        }
        /// <summary>
        /// 获取更新填报的Update语句
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ID"></param>
        /// <param name="FillTable"></param>
        /// <returns></returns>
        public string getUpdateSql(DataTable dt, string ID, string FillTable)
        {
            string sql = "";
            string setValue = "";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                setValue += dt.Columns[i].ColumnName + "='" + dt.Rows[0][i].ToString() + "'";
                if (i != dt.Columns.Count - 1)
                    setValue += ",";
            }
            sql = "update " + FillTable + " set " + setValue + " where ID='" + ID + "'";

            return sql;
        }

        //判断是否为数字
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        #region// 保存填报数据的通用方法
        /// <summary>
        /// 保存填报数据的通用方法
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="FillTable"></param>
        /// <param name="FillItemTable"></param>
        /// <param name="unSureMark"></param>
        /// <param name="strFillID"></param>
        /// <param name="strSerial"></param>
        /// <param name="strSerialItem"></param>
        /// <returns></returns>
        public bool SaveFillData(DataTable dt, string FillTable, string FillItemTable, string unSureMark, ref string strFillID, string strSerial, string strSerialItem)
        {
            ArrayList list = new ArrayList();
            string sql = string.Empty;
            string ItemID = "";//监测项目ID
            string ItemValue = "";
            string SerialID = "";
            string SerialItemID = "";
            DataTable dtTemp = new DataTable();

            if (dt.Rows.Count > 0)
            {
                strFillID = dt.Rows[0]["FillID"].ToString();
                dt.Columns.Remove("FillID");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //获取需要更新的监测项目信息
                    if (dt.Columns[i].ColumnName.IndexOf(unSureMark) != -1)
                    {
                        ItemID = dt.Columns[i].ColumnName.Replace(unSureMark, "");
                        ItemValue = dt.Rows[0][dt.Columns[i].ColumnName].ToString();

                        dt.Columns.Remove(dt.Columns[i].ColumnName);
                        i--;
                    }
                }
                if (ItemID == "")
                {
                    //不是更新监测项时只需要更新填报主表
                    if (strFillID == "")
                    {
                        //不存在填报数据时
                        strFillID = SerialID = GetSerialNumber(strSerial);
                        sql = getInsertSql(dt, SerialID, FillTable);
                        list.Add(sql);
                    }
                    else
                    {
                        //已经存在填报数据时
                        sql = getUpdateSql(dt, strFillID, FillTable);
                        list.Add(sql);
                    }
                }
                else
                {
                    if (strFillID == "")
                    {
                        //不存在填报数据时
                        strFillID = SerialID = GetSerialNumber(strSerial);
                        sql = getInsertSql(dt, SerialID, FillTable);
                        list.Add(sql);
                        //更新填报监测项表
                        SerialItemID = GetSerialNumber(strSerialItem);
                        sql = "insert into " + FillItemTable + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('" + SerialItemID + "','" + SerialID + "','" + ItemID + "','" + ItemValue + "')";
                        list.Add(sql);
                    }
                    else
                    {
                        //更新填报监测项表
                        sql = "select ID from " + FillItemTable + " where FILL_ID='" + strFillID + "' and ITEM_ID='" + ItemID + "'";
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            sql = "update " + FillItemTable + " set ITEM_VALUE='" + ItemValue + "' where ID='" + dtTemp.Rows[0]["ID"].ToString() + "'";
                            list.Add(sql);
                        }
                        else
                        {
                            SerialItemID = GetSerialNumber(strSerialItem);
                            sql = "insert into " + FillItemTable + "(ID,FILL_ID,ITEM_ID,ITEM_VALUE) values('" + SerialItemID + "','" + strFillID + "','" + ItemID + "','" + ItemValue + "')";
                            list.Add(sql);
                        }
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region//获取环境质量数据填报数据
        /// <summary>
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string Columns = "";
            string PointIDs = "";
            bool b = true;
            switch (FillTable)
            {
                //功能区噪声填报
                case "T_ENV_FILL_NOISE_FUNCTION":
                    b = UpdateFillDateF(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
                //道路交通、区域环境噪声填报
                case "T_ENV_FILL_NOISE_ROAD":
                case "T_ENV_FILL_NOISE_AREA":
                    b = UpdateFillDateAR(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
                case "T_ENV_FILL_AIRHOUR"://环境空气(小时)
                    b = this.InsertAirHourEx(strWhere, ref PointIDs, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);
                    break;
                default:
                    b = UpdateFillDate(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
            }

            #region //根据点位表信息更新填报表
            if (b)
            {
                //获取填报表信息
                foreach (DataRow drShow in dtShow.Rows)
                {
                    Columns += drShow[0].ToString() + " " + FillTable + "@" + drShow[0].ToString() + "@" + drShow[1].ToString() + ",";
                }
                sql = "select ID,{0} from {1} where POINT_ID in({2}) and ISNULL(REMARK1,'')='' {3}";

                sql = string.Format(sql, Columns.TrimEnd(','), FillTable, PointIDs, Columns.Contains("SECTION_ID") ? "order by SECTION_ID" : "");
                dtMain = ExecuteDataTable(sql);
                if (dtMain.Rows.Count > 0)
                {
                    //查询要填报的监测项
                    string FillIDs = "";
                    foreach (DataRow drMain in dtMain.Rows)
                        FillIDs += "'" + drMain["ID"].ToString() + "',";

                    sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1}) and ISNULL(REMARK1,'')=''
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtAllItem = ExecuteDataTable(sql);

                    //把监测项拼接在表格中
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                    }

                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;

                    //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1}) and ISNULL(REMARK1,'')=''";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtFillItem = ExecuteDataTable(sql);

                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                        //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            string itemId = drAllItem["ID"].ToString(); //监测项ID
                            var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                            if (itemValue.Count > 0)
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                            else
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                        }
                    }
                }
            }
            #endregion

            return dtMain;
        }
        #endregion

        #region//根据点位表信息更新填报表
        /// <summary>
        /// 根据点位表信息更新填报表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public bool UpdateFillDate(string strWhere, ref string PointIDs, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            //获取断面、垂线/测点信息
            if (mark == "1")
            {
                sql = @"select a.ID SECTION_ID,a.YEAR,a.MONTH,a.SECTION_NAME,b.ID POINT_ID,b.VERTICAL_NAME POINT_NAME
                              from {0} a
                              inner join {1} b on(a.ID=b.SECTION_ID) 
                              where {2} and a.IS_DEL='0'";

                sql = string.Format(sql, SectionTable, PointTable, strWhere.Replace("ID", "a.ID"));
            }
            else
            {
                sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";

                sql = string.Format(sql, PointTable, strWhere);
            }

            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                    //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    if (mark == "1")
                    {
                        sql = "select ID from {0} where SECTION_ID='{1}' and POINT_ID='{2}' and ISNULL(REMARK1,'')=''";
                        sql = string.Format(sql, FillTable, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString());
                    }
                    else
                    {
                        sql = "select ID from {0} where POINT_ID='{1}' and ISNULL(REMARK1,'')=''";
                        sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    }
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        FillID = GetSerialNumber(FillSerial);
                        if (mark == "1")
                        {
                            sql = "insert into {0}(ID,SECTION_ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}','{5}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());

                        }
                        else
                        {
                            sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());
                        }
                        list.Add(sql);
                    }

                    //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    dtAllItem = ExecuteDataTable(sql);
                    //循环每个点位的监测项
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                        sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                        sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count == 0)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                    }
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region//获取环境质量数据填报数据(zz)
        /// <summary>
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillData_ZZ(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string PointIDs = "";
            bool b = true;
            switch (FillTable)
            {
                //功能区噪声填报
                case "T_ENV_FILL_NOISE_FUNCTION":
                    b = UpdateFillDateF(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
                //道路交通、区域环境噪声填报
                case "T_ENV_FILL_NOISE_ROAD":
                case "T_ENV_FILL_NOISE_AREA":
                    b = UpdateFillDateAR(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
                default:
                    b =this.UpdateFillDate_ZZ(strWhere, ref PointIDs, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
                    break;
            }

            #region //根据点位表信息更新填报表
            if (b)
            {
                StringBuilder sb = new StringBuilder(256);
                if (FillTable == "T_ENV_FILL_LAKE")
                {
                    #region//湖库
                    sb.Append("select a.ID,a.YEAR as  T_ENV_FILL_LAKE@YEAR@年份,a.MONTH as T_ENV_FILL_LAKE@MONTH@月份,b.SATAIONS_ID as T_ENV_FILL_LAKE@SATAIONS_ID@测站编码,'' as T_ENV_FILL_LAKE@SATAIONS_Name@测站名称 ,");
                    sb.Append(" b.LAKE_ID as T_ENV_FILL_LAKE@LAKE_ID@湖库代码,'' as T_ENV_FILL_LAKE@LAKE_Name@湖库名称, ");
                    sb.Append(" b.SECTION_Code  as T_ENV_FILL_LAKE@SECTION_Code@断面代码, a.SECTION_ID  asT_ENV_FILL_LAKE@SECTION_ID@断面名称,");
                    sb.Append(" a.DAY as T_ENV_FILL_LAKE@DAY@采样日期,a.KPF as T_ENV_FILL_LAKE@KPF@水期 ");
                    sb.Append("  from T_ENV_FILL_LAKE a left join T_ENV_P_LAKE b on a.section_id=b.id  ");
                    sb.Append("  where  POINT_ID in(" + PointIDs + ")");
                    #endregion
                }
                else if (FillTable == "T_ENV_FILL_DRINK_UNDER")
                {
                    #region//地下水
                    sb.Append("select a.ID,a.YEAR as T_ENV_FILL_DRINK_UNDER@YEAR@年份,c.dict_text as T_ENV_FILL_LAKE@dict_text@测站名称,b.SATAIONS_ID as T_ENV_FILL_LAKE@SATAIONS_ID@测站编码,");
                    sb.Append(" a.POINT_ID as T_ENV_FILL_DRINK_UNDER@POINT_ID@测点名称,   a.MONTH as T_ENV_FILL_DRINK_UNDER@MONTH@月份,a.DAY as T_ENV_FILL_DRINK_UNDER@DAY@日期");
                    sb.Append(" from T_ENV_FILL_DRINK_UNDER  a left join T_ENV_P_DRINK_UNDER b on a.point_id=b.id  Left join T_SYS_DICT c on b.SATAIONS_ID=c.dict_code");
                    sb.Append("  where  c. DICT_TYPE='SATAIONS'  and  POINT_ID in(" + PointIDs + ")");
                    #endregion
                }
                else if (FillTable == "T_ENV_FILL_RIVER")
                {
                    #region//河流
                    sb.Append("select a.ID,a.YEAR T_ENV_FILL_RIVER@YEAR@年份,b.SATAIONS_ID as T_ENV_FILL_RIVER@SATAIONS_ID@测站编码,c.dict_text as T_ENV_FILL_RIVER@dict_text@测站名称,");
                    sb.Append(" b.RIVER_ID as T_ENV_FILL_RIVER@RIVER_ID@河流编码,'' as T_ENV_FILL_RIVER@RIVER_NAME@河流名称,");
                    sb.Append("  a.SECTION_ID T_ENV_FILL_RIVER@SECTION_ID@断面名称,b.SECTION_CODE T_ENV_FILL_RIVER@SECTION_CODE@断面代码,");
                    sb.Append(" a.DAY T_ENV_FILL_RIVER@DAY@日期,a.KPF T_ENV_FILL_RIVER@KPF@水期 ");
                    sb.Append(" from T_ENV_FILL_RIVER a left join  T_ENV_P_RIVER b on a.section_id=b.id  Left join T_SYS_DICT c on b.SATAIONS_ID=c.dict_code");
                    sb.Append("  where  c. DICT_TYPE='SATAIONS'  and  POINT_ID in(" + PointIDs + ")");
                    #endregion
                }
                dtMain = ExecuteDataTable(sb.ToString());
                if (dtMain.Rows.Count > 0)
                {
                    #region//湖库
                    if (FillTable == "T_ENV_FILL_LAKE")
                    {
                        for (int i = 0; i < dtMain.Rows.Count; i++)
                        {
                            string select_SATAIONS_Name = "select dict_text from  T_SYS_DICT  WHERE DICT_TYPE='SATAIONS' and DICT_CODE='" + dtMain.Rows[i][3].ToString() + "'";
                            dtMain.Rows[i][4] = ExecuteScalar(select_SATAIONS_Name);
                            string select_LAKE_Name = "select dict_text from  T_SYS_DICT  WHERE DICT_TYPE='lake' and DICT_CODE='" + dtMain.Rows[i][5].ToString() + "'";
                            dtMain.Rows[i][6] = ExecuteScalar(select_SATAIONS_Name);
                        }
                    }
                    else if (FillTable == "T_ENV_FILL_RIVER")
                    {
                        #region//河流
                        for (int i = 0; i < dtMain.Rows.Count; i++)
                        {
                            string select_SATAIONS_Name = "select dict_text from  T_SYS_DICT  WHERE  ID='" + dtMain.Rows[i][4].ToString() + "'";
                            dtMain.Rows[i][5] = ExecuteScalar(select_SATAIONS_Name);
                        }
                        #endregion
                    }
                    #endregion

                    #region//查询要填报的监测项
                    string FillIDs = "";
                    foreach (DataRow drMain in dtMain.Rows)
                        FillIDs += "'" + drMain["ID"].ToString() + "',";

                    sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1})
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtAllItem = ExecuteDataTable(sql);
                    #endregion
                    #region //把监测项拼接在表格中
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                    }
                    DataTable dtFillItem = new DataTable(); //填报监测项数据
                    DataRow[] drFillItem;
                    #endregion
                    #region //根据条件查询所有填报监测项数据
                    sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1})";
                    sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                    dtFillItem = ExecuteDataTable(sql);
                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                        //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            string itemId = drAllItem["ID"].ToString(); //监测项ID
                            var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                            if (itemValue.Count > 0)
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                            else
                                drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                        }
                    }
                           #endregion
                }
            }
            #endregion

            return dtMain;
        }
        #endregion

        #region//根据点位表信息更新填报表
        /// <summary>
        /// 根据点位表信息更新填报表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public bool UpdateFillDate_ZZ(string strWhere, ref string PointIDs, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            //获取断面、垂线/测点信息
            if (mark == "1")
            {
                sql = @"select a.ID SECTION_ID,a.YEAR,a.MONTH,a.SECTION_NAME,b.ID POINT_ID,b.VERTICAL_NAME POINT_NAME
                              from {0} a
                              inner join {1} b on(a.ID=b.SECTION_ID) 
                              where {2} and a.IS_DEL='0'";

                sql = string.Format(sql, SectionTable, PointTable, strWhere.Replace("ID", "a.ID"));
            }
            else
            {
                sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";

                sql = string.Format(sql, PointTable, strWhere);
            }

            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                    //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    if (mark == "1")
                    {
                        sql = "select ID from {0} where SECTION_ID='{1}' and POINT_ID='{2}'";
                        sql = string.Format(sql, FillTable, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString());
                    }
                    else
                    {
                        sql = "select ID from {0} where POINT_ID='{1}'";
                        sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    }
                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        FillID = GetSerialNumber(FillSerial);
                        if (mark == "1")
                        {
                            sql = "insert into {0}(ID,SECTION_ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}','{5}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["SECTION_ID"].ToString(), drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());

                        }
                        else
                        {
                            sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}')";
                            sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());
                        }
                        list.Add(sql);
                    }

                    //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    dtAllItem = ExecuteDataTable(sql);
                    //循环每个点位的监测项
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                        sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                        sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count == 0)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                    }
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region// 根据点位表信息更新填报表(用于功能区噪声填报)
        /// <summary>
        /// 根据点位表信息更新填报表(用于功能区噪声填报)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="SummaryTable">汇总表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="SummarySerial">汇总表序列类型</param>
        /// <returns></returns>
        public bool UpdateFillDateF(string strWhere, ref string PointIDs, string SummaryTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string SummarySerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            string Quarter = "";      //季度
            //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                            from {0} a
                            where {1} and a.IS_DEL='0'";

            sql = string.Format(sql, PointTable, strWhere);

            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                Quarter = (int.Parse(dtMain.Rows[0]["MONTH"].ToString()) / 3 + (int.Parse(dtMain.Rows[0]["MONTH"].ToString()) % 3 > 0 ? 1 : 0)).ToString();
                string pointid = "";
                //查询每个点位要监测的监测项
                pointid = dtMain.Rows[0]["POINT_ID"].ToString();
                sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                sql = string.Format(sql, ItemTable, pointid);
                dtAllItem = ExecuteDataTable(sql);

                PointIDs += dtMain.Rows[0]["POINT_ID"].ToString();
                //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                sql = "select ID from {0} where POINT_ID='{1}' and ISNULL(REMARK1,'')=''";
                sql = string.Format(sql, FillTable, dtMain.Rows[0]["POINT_ID"].ToString());
                dtTemp = ExecuteDataTable(sql);
                if (dtTemp.Rows.Count > 0)
                {
                    foreach (DataRow drTemp in dtTemp.Rows)
                    {
                        FillID = drTemp["ID"].ToString();

                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                            sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                            sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                            dtTemp = ExecuteDataTable(sql);
                            if (dtTemp.Rows.Count == 0)
                            {
                                FillIID = GetSerialNumber(FillISerial);
                                sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                                sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                                list.Add(sql);
                            }
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        FillID = GetSerialNumber(FillSerial);
                        sql = "insert into {0}(ID,POINT_ID,QUARTER,YEAR,MONTH,BEGIN_HOUR) values('{1}','{2}','{3}','{4}','{5}','{6}')";
                        sql = string.Format(sql, FillTable, FillID, dtMain.Rows[0]["POINT_ID"].ToString(), Quarter, dtMain.Rows[0]["YEAR"].ToString(), dtMain.Rows[0]["MONTH"].ToString(), i.ToString());
                        list.Add(sql);

                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                    }
                }

                //判断功能区噪声汇总表中是否存在在相应的测点数据，如果没有则插入数据
                sql = "select ID from {0} where YEAR='{1}' and QUTER='{2}' and MONTH='{3}' and POINT_CODE='{4}'";
                sql = string.Format(sql, SummaryTable, dtMain.Rows[0]["YEAR"].ToString(), Quarter, dtMain.Rows[0]["MONTH"].ToString(), dtMain.Rows[0]["POINT_ID"].ToString());
                dtTemp = ExecuteDataTable(sql);
                if (dtTemp.Rows.Count == 0)
                {
                    string SumID = GetSerialNumber(SummarySerial);
                    sql = "insert into {0}(ID,YEAR,QUTER,POINT_CODE,MONTH) values('{1}','{2}','{3}','{4}','{5}')";
                    sql = string.Format(sql, SummaryTable, SumID, dtMain.Rows[0]["YEAR"].ToString(), Quarter, dtMain.Rows[0]["POINT_ID"].ToString(), dtMain.Rows[0]["MONTH"].ToString());
                    list.Add(sql);
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region // 根据点位表信息更新填报表(用于道路交通、区域环境噪声填报)
        /// <summary>
        /// 根据点位表信息更新填报表(用于道路交通、区域环境噪声填报)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="PointIDs">用于返回填报的POINT_ID值</param>
        /// <param name="SummaryTable">汇总表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="SummarySerial">汇总表序列类型</param>
        /// <returns></returns>
        public bool UpdateFillDateAR(string strWhere, ref string PointIDs, string SummaryTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string SummarySerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";

            sql = string.Format(sql, PointTable, strWhere);


            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";
                    //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    sql = "select ID from {0} where POINT_ID='{1}' and ISNULL(REMARK1,'')=''";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());

                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        FillID = GetSerialNumber(FillSerial);

                        sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH) values('{1}','{2}','{3}','{4}')";
                        sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString());


                        list.Add(sql);
                    }

                    //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    dtAllItem = ExecuteDataTable(sql);
                    //循环每个点位的监测项
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                        sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                        sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count == 0)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                    }
                }

                //判断功能区噪声汇总表中是否存在在相应的测点数据，如果没有则插入数据
                sql = "select ID from {0} where YEAR='{1}'";
                sql = string.Format(sql, SummaryTable, dtMain.Rows[0]["YEAR"].ToString());
                dtTemp = ExecuteDataTable(sql);
                if (dtTemp.Rows.Count == 0)
                {
                    string SumID = GetSerialNumber(SummarySerial);
                    sql = "insert into {0}(ID,YEAR) values('{1}','{2}')";
                    sql = string.Format(sql, SummaryTable, SumID, dtMain.Rows[0]["YEAR"].ToString());
                    list.Add(sql);
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region//获取环境质量补测的数据填报数据
        /// <summary>
        /// 获取环境质量补测的数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillBcData(string strWhere, DataTable dtShow, string FillTable, string FillITable, string mark)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string Columns = "";

            #region //根据点位表信息更新填报表
            if (mark == "1")
            {
                strWhere = strWhere.Replace("ID", "SECTION_ID");
            }
            else
            {
                strWhere = strWhere.Replace("ID", "POINT_ID");
            }

            //获取填报表信息
            foreach (DataRow drShow in dtShow.Rows)
            {
                Columns += drShow[0].ToString() + " " + FillTable + "@" + drShow[0].ToString() + "@" + drShow[1].ToString() + ",";
            }
            sql = "select ID,{0} from {1} where {2} and ISNULL(REMARK1,'')<>'' {3}";

            sql = string.Format(sql, Columns.TrimEnd(','), FillTable, strWhere, Columns.Contains("SECTION_ID") ? "order by SECTION_ID" : "");
            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                string FillIDs = "";
                foreach (DataRow drMain in dtMain.Rows)
                    FillIDs += "'" + drMain["ID"].ToString() + "',";

                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1}) and ISNULL(REMARK1,'')<>''
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(FillITable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1}) and ISNULL(REMARK1,'')<>''";
                sql = string.Format(sql, FillITable, FillIDs.TrimEnd(','));
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                            drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                        else
                            drMain[FillITable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                    }
                }
            }
            #endregion

            return dtMain;
        }
        #endregion

        #region//根据点位信息更新填报表(用于空气【小时】)

        //create by weilin 2013-09-06
        private bool InsertAirHourEx(string strWhere, ref string PointIDs, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            ArrayList list = new ArrayList();
            DataTable dtFillItem = new DataTable();
            //DataRow[] drFillItem;
            DataTable dtFill = new DataTable();
            DataRow[] drFill;
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号
            int Days = 0;

            //获取填报主表与子表的当前序列号与长度
            DataTable dt = new DataTable();
            int FillNumber = 0;
            int FillLength = 9;
            int FillINumber = 0;
            int FillILength = 9;
            sql = "select SERIAL_NUMBER,LENGTH from T_SYS_SERIAL where SERIAL_CODE='" + FillSerial + "'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                FillNumber = int.Parse(dt.Rows[0]["SERIAL_NUMBER"].ToString());
                FillLength = int.Parse(dt.Rows[0]["LENGTH"].ToString());
            }
            sql = "select SERIAL_NUMBER,LENGTH from T_SYS_SERIAL where SERIAL_CODE='" + FillISerial + "'";
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                FillINumber = int.Parse(dt.Rows[0]["SERIAL_NUMBER"].ToString());
                FillILength = int.Parse(dt.Rows[0]["LENGTH"].ToString());
            }

            //获取断面、垂线/测点信息
            sql = @"select a.ID POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                            from {0} a
                            where {1} and a.IS_DEL='0'";

            sql = string.Format(sql, PointTable, strWhere);

            dtMain = ExecuteDataTable(sql); //查询点位信息
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    Days = DateTime.DaysInMonth(int.Parse(drMain["YEAR"].ToString()), int.Parse(drMain["MONTH"].ToString()));
                    PointIDs += drMain["POINT_ID"].ToString() + ",";

                    //查询每个点位要监测的监测项
                    pointid = drMain["POINT_ID"].ToString();
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    dtAllItem = ExecuteDataTable(sql);

                    //获取某测点的填报数据
                    sql = "select a.ID,a.DAY,a.HOUR,b.ITEM_ID from {0} a left join {1} b on(a.ID=b.FILL_ID) where a.POINT_ID='{2}'";
                    sql = string.Format(sql, FillTable, FillITable, pointid);
                    dtFill = ExecuteDataTable(sql);//查询填报

                    for (int i = 0; i < Days; i++)
                    {
                        for (int j = 0; j < 24; j++)
                        {
                            //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                            drFill = dtFill.Select("DAY='" + (i + 1).ToString() + "' and HOUR='" + j + "'");

                            if (drFill.Length > 0)
                            {
                                FillID = drFill[0]["ID"].ToString();
                            }
                            else
                            {
                                FillNumber++;
                                FillID = (FillNumber).ToString().PadLeft(FillLength, '0');

                                sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,DAY,HOUR) values('{1}','{2}','{3}','{4}','{5}','{6}')";
                                sql = string.Format(sql, FillTable, FillID, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), (i + 1).ToString(), j.ToString());

                                list.Add(sql);
                            }

                            //循环每个点位的监测项
                            foreach (DataRow drAllItem in dtAllItem.Rows)
                            {
                                //判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                                var ItemValue = drFill.Where(c => c["ITEM_ID"].Equals(drAllItem["ID"].ToString())).ToList();

                                if (ItemValue.Count == 0)
                                {
                                    FillINumber++;
                                    FillIID = (FillINumber).ToString().PadLeft(FillILength, '0');
                                    sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                                    sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                                    list.Add(sql);
                                }
                            }
                        }
                        
                    }

                }
                //更新序列号表
                sql = "update T_SYS_SERIAL set SERIAL_NUMBER='" + FillNumber + "' where SERIAL_CODE='" + FillSerial + "'";
                list.Add(sql);
                sql = "update T_SYS_SERIAL set SERIAL_NUMBER='" + FillINumber + "' where SERIAL_CODE='" + FillISerial + "'";
                list.Add(sql);
            }
            PointIDs = PointIDs.TrimEnd(',');
            return SqlHelper.ExecuteSQLByTransaction(list);
        }

        private bool InsertAirHour(string strWhere, ref string PointIDs, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            string sql = "";
            bool flag = false;
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string FillID = "";     //填报表序列号
            string FillIID = "";      //填报监测项序列号

            #region//查询点位信息
            sql = @"select a.ID as  POINT_ID,a.YEAR,a.MONTH,a.POINT_NAME
                              from {0} a
                              where {1} and a.IS_DEL='0'";
            sql = string.Format(sql, PointTable, strWhere);
            #endregion

            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                string pointid = "";
                foreach (DataRow drMain in dtMain.Rows)
                {
                    PointIDs += drMain["POINT_ID"].ToString() + ",";

                    #region //判断填报表中是否存在在相应的断面、垂线/测点数据，如果没有则插入数据
                    sql = "select ID from {0} where POINT_ID='{1}'";
                    sql = string.Format(sql, FillTable, drMain["POINT_ID"].ToString());
                    #endregion

                    dtTemp = ExecuteDataTable(sql);//查询填报
                    if (dtTemp.Rows.Count > 0)
                    {
                        FillID = dtTemp.Rows[0]["ID"].ToString();

                        #region//查询每个点位要监测的监测项
                        pointid = drMain["POINT_ID"].ToString();
                        sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                        sql = string.Format(sql, ItemTable, pointid);
                        #endregion

                        dtAllItem = ExecuteDataTable(sql);
                        //循环每个点位的监测项
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                            sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                            sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                            dtTemp = ExecuteDataTable(sql);
                            if (dtTemp.Rows.Count == 0)
                            {
                                FillIID = GetSerialNumber(FillISerial);
                                sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                                sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                                list.Add(sql);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region //判断月份
                        ArrayList Sqllist = new ArrayList();
                        if (drMain["MONTH"].ToString().Equals("1") || drMain["MONTH"].ToString().Equals("3") || drMain["MONTH"].ToString().Equals("5") || drMain["MONTH"].ToString().Equals("7") || drMain["MONTH"].ToString().Equals("8") || drMain["MONTH"].ToString().Equals("10") || drMain["MONTH"].ToString().Equals("12"))
                        {
                            Sqllist = this.RntList(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 31);
                        }
                        else if (drMain["MONTH"].ToString().Equals("4") || drMain["MONTH"].ToString().Equals("6") || drMain["MONTH"].ToString().Equals("9") || drMain["MONTH"].ToString().Equals("11"))
                        {
                            Sqllist = this.RntList(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 30);
                        }
                        else if (drMain["MONTH"].ToString().Equals("2"))
                        {
                            if (((int.Parse(drMain["YEAR"].ToString()) % 4 == 0) && (int.Parse(drMain["YEAR"].ToString()) % 100 != 0)) || (int.Parse(drMain["YEAR"].ToString()) % 400 == 0))
                            {
                                Sqllist = this.RntList(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 29);//闰年
                            }
                            else
                            {
                                Sqllist = this.RntList(ItemTable, FillTable, FillITable, FillSerial, FillISerial, drMain["POINT_ID"].ToString(), drMain["YEAR"].ToString(), drMain["MONTH"].ToString(), 28);//平年
                            }
                        }
                        list.Add(Sqllist);
                        #endregion
                    }
                }
            }
            PointIDs = PointIDs.TrimEnd(',');
            if (list.Count == 0) { flag = true; }//list.count==0,说明数据库里有数据，不用插入数据；或者程序报错；
            foreach (ArrayList lists in list)
            {
                flag = SqlHelper.ExecuteSQLByTransaction(lists);
            }
            return flag;
        }

        #region//环境空气(小时)的数据
        /// <summary>
        /// 环境空气(小时)的数据
        /// </summary>
        /// <param name="FillID"></param>
        /// <param name="FillIID"></param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="POINT_ID">点位ID</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        private ArrayList RntList(string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string POINT_ID, string year, string month, int DayCount)
        {
            string FillID = string.Empty;
            string FillIID = string.Empty;
            string sql = string.Empty;
            string pointid = string.Empty;
            ArrayList list = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtAllItem = new DataTable();

            #region//每天的数据
            for (int day = 1; day <= DayCount; day++)
            {
                #region//每小时的数据
                for (int hour = 0; hour <= 23; hour++)
                {
                    FillID = GetSerialNumber(FillSerial);
                    sql = "insert into {0}(ID,POINT_ID,YEAR,MONTH,DAY,HOUR) values('{1}','{2}','{3}','{4}','{5}','{6}')";
                    sql = string.Format(sql, FillTable, FillID, POINT_ID, year, month, day.ToString(), hour.ToString());
                    list.Add(sql);

                    #region//查询每个点位要监测的监测项
                    pointid = POINT_ID;
                    sql = @"select b.ID, b.ITEM_NAME
                       from 
                       (
	                        select 
		                        ITEM_ID 
	                        from 
		                        {0}
	                        where
		                        POINT_ID in({1})
	                        group by
		                        ITEM_ID
                        ) a
                        left join 
	                        T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                        where
	                        b.is_del='0'";
                    sql = string.Format(sql, ItemTable, pointid);
                    #endregion

                    dtAllItem = ExecuteDataTable(sql);
                    //循环每个点位的监测项

                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        #region//判断填报监测项表中是否存在在相应的监测项目数据，如果没有则插入数据
                        sql = "select ID from {0} where FILL_ID='{1}' and ITEM_ID='{2}'";
                        sql = string.Format(sql, FillITable, FillID, drAllItem["ID"].ToString());
                        dtTemp = ExecuteDataTable(sql);
                        if (dtTemp.Rows.Count == 0)
                        {
                            FillIID = GetSerialNumber(FillISerial);
                            sql = "insert into {0}(ID,FILL_ID,ITEM_ID) values('{1}','{2}','{3}')";
                            sql = string.Format(sql, FillITable, FillIID, FillID, drAllItem["ID"].ToString());
                            list.Add(sql);
                        }
                        #endregion
                    }
                }
                #endregion
            }
            #endregion

            return list;
        }
        #endregion

        #endregion

        #region //根据某字段值返回某字段的值
        /// <summary>
        /// 根据某字段值返回某字段的值
        /// </summary>
        /// <param name="TabelName"></param>
        /// <param name="ColName"></param>
        /// <param name="IDName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string getNameByID(string TabelName, string ColName, string KeyName, string Value)
        {
            string strSQL = "SELECT " + ColName + " FROM " + TabelName + " WHERE " + KeyName + "='" + Value + "'";

            object objResult = ExecuteScalar(strSQL);
            return null != objResult ? objResult.ToString() : "";
        }
        #endregion

        #region//根据条件更新数据表
        /// <summary>
        /// 根据条件更新数据表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strSet"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public int UpdateTableByWhere(string TableName, string strSet, string Where)
        {
            string strSQL = "UPDATE " + TableName + " SET " + strSet + " WHERE " + Where;
            return ExecuteNonQuery(strSQL);
        }
        #endregion

        #region//填报更新方法
        /// <summary>
        /// 填报保存方法（ljn/2013/6/21）
        /// </summary>
        /// <param name="ColomnName">列名</param>
        /// <param name="ColomnValue">列值</param>
        /// <param name="Fill_ID">填报ID</param>
        /// <param name="StandardID">评价标准ID（用于判断监测项目的评价值）</param>
        /// <returns></returns>
        public bool UpdateCommonFill(string ColomnName, string ColomnValue, string Fill_ID, string StandardID)
        {
            //ColomnName = "T_ENV_FILL_DUST_ITEM@000000232";
            ArrayList list = new ArrayList();
            string[] Arry_Colomn_Name = ColomnName.Split('@');
            string Item_Name = Arry_Colomn_Name[0].ToString();//表明
            if (Item_Name.Contains("ITEM"))
            {
                string Item_ID = Arry_Colomn_Name[1].ToString();//item_id 的值
                string FillTableName = Item_Name.Replace("_ITEM", "");
                StringBuilder sb = new StringBuilder();
                string ConditionName = "";                      //监测项目评价
                string ZHEval = "";                             //综合评价

                #region 计算监测项的评价值
                //评价标准ID非空时表示当前监测项有评价标准
                if (StandardID != "")
                {
                    DataTable dtStand = new DataTable();
                    DataRow[] dr;
                    sb.Remove(0, sb.Length);
                    sb.Append("select a.ID,a.MONITOR_ID,cast(isnull(a.DISCHARGE_UPPER,'0') as float) UPPER,cast(isnull(a.DISCHARGE_LOWER,'0') as float) LOWER,b.CONDITION_NAME ");
                    sb.Append("from T_BASE_EVALUATION_CON_ITEM a left join T_BASE_EVALUATION_CON_INFO b on(a.CONDITION_ID=b.ID) ");
                    sb.Append("where a.STANDARD_ID='" + StandardID + "' and a.ITEM_ID='" + Item_ID + "' and a.IS_DEL='0'");

                    dtStand = ExecuteDataTable(sb.ToString());
                    //dtStand有数据时表示该监测项有设置评价标准
                    if (dtStand.Rows.Count > 0)
                    {
                        if (IsNumeric(ColomnValue))
                        {
                            decimal dValue = 0;
                            dValue = decimal.Parse(ColomnValue == "" ? "0" : ColomnValue);

                            dr = dtStand.Select("UPPER>=" + dValue + " and LOWER<=" + dValue);
                            if (dr.Length > 0)
                            {
                                ZHEval = SqlHelper.ExecuteDataTable("select JUDGE from " + FillTableName + " where ID='" + Fill_ID + "'").Rows[0]["JUDGE"].ToString();
                                ConditionName = dr[0]["CONDITION_NAME"].ToString();
                                ZHEval = CompareEval(ConditionName, ZHEval);
                            }
                        }

                        if (ConditionName == "")
                            ConditionName = "超出范围";
                    }
                }

                #endregion

                #region//更新填报的监测项目
                sb.Remove(0, sb.Length);
                if (Item_Name.Equals("T_ENV_FILL_AIRHOUR_ITEM") || Item_Name.Equals("T_ENV_FILL_AIRKS_ITEM"))//环境空气(小时)
                {
                    sb.Append("update " + Item_Name + " set ITEM_VALUE='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'");
                }
                else if (Item_Name.Equals("T_ENV_FILL_POLLUTE_WATER_ITEM"))//污染源常规废水
                {
                    sb.Append("update " + Item_Name + " set WATER_PER='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'");//保存废水处理设施进口浓度
                }
                else if (Item_Name.Equals("T_ENV_FILL_POLLUTE_AIR_ITEM"))
                {
                    #region//污染源常规废气
                    string ItemName = Arry_Colomn_Name[2].ToString();
                    if (ItemName.Contains("含氧量"))
                    { sb.Append("update " + Item_Name + " set OQty='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("排放上限"))
                    { sb.Append("update " + Item_Name + " set Up_Line='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("排放下限"))
                    { sb.Append("update " + Item_Name + " set Down_Line='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("排放单位"))
                    { sb.Append("update " + Item_Name + " set Uom='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("超标倍数"))
                    { sb.Append("update " + Item_Name + " set Standard='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("污染物实测浓度"))
                    { sb.Append("update " + Item_Name + " set PollutePer='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("污染物折算浓度"))
                    { sb.Append("update " + Item_Name + " set PolluteCalPer='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("浓度是否达标"))
                    { sb.Append("update " + Item_Name + " set Is_Standard='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    else if (ItemName.Contains("废气排放量"))
                    { sb.Append("update " + Item_Name + " set AirQty='" + ColomnValue + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'"); }
                    #endregion
                }
                else
                {
                    sb.Append("update " + Item_Name + " set ITEM_VALUE='" + ColomnValue + "',JUDGE='" + ConditionName + "' where ITEM_ID='" + Item_ID + "' and FILL_ID='" + Fill_ID + "'");
                    sb.Append(" update " + FillTableName + " set JUDGE='" + ZHEval + "' where ID='" + Fill_ID + "'");
                }
                list.Add(sb.ToString());
                #endregion
            }
            else
            {
                #region//更新填报
                string Get_Colomn_Name = Arry_Colomn_Name[1].ToString();//列名
                StringBuilder sb = new StringBuilder(256);
                sb.Append("update " + Item_Name + " set " + Get_Colomn_Name + "='" + ColomnValue + "' where ID='" + Fill_ID + "'");
                list.Add(sb.ToString());
                #endregion
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        #endregion

        #region//监测项目评价值的更新
        /// <summary>
        /// 监测项目评价值的更新
        /// </summary>
        /// <param name="FillIDs">填报表ID</param>
        /// <param name="FillTB">填报表名</param>
        /// <param name="FillItemTB">填报监测表名</param>
        /// <param name="PointTB">点位表名</param>
        /// <param name="PointColName">填报表关联点位的字段名</param>
        /// <returns></returns>
        public bool ModifyJUDGE(string FillIDs, string FillTB, string FillItemTB, string PointTB, string PointColName)
        {
            DataTable dt = new DataTable();
            DataTable dtStand = new DataTable();
            DataRow[] dr;
            ArrayList list = new ArrayList();
            StringBuilder sb = new StringBuilder();
            string ConditionName = ""; //监测项目评价
            string ZHEval = "";     //综合评价
            string Fill_ID = "";

            //获取需要更新评价值的监测项信息
            sb.Append("select b.ID,b.FILL_ID,b.ITEM_ID,b.ITEM_VALUE,c.CONDITION_ID,a.JUDGE ");
            sb.Append("from " + FillTB + " a ");
            sb.Append("left join " + FillItemTB + " b on(a.ID=b.FILL_ID) ");
            sb.Append("left join " + PointTB + " c on(a." + PointColName + "=c.ID) ");
            sb.Append("where a.ID in(" + FillIDs + ") order by b.FILL_ID");

            dt = ExecuteDataTable(sb.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ConditionName = "";
                if (dt.Rows[i]["FILL_ID"].ToString() != Fill_ID)
                {
                    Fill_ID = dt.Rows[i]["FILL_ID"].ToString();
                    ZHEval = dt.Rows[i]["JUDGE"].ToString();
                }

                sb.Remove(0, sb.Length);
                sb.Append("select a.ID,a.MONITOR_ID,cast(isnull(a.DISCHARGE_UPPER,'0') as float) UPPER,cast(isnull(a.DISCHARGE_LOWER,'0') as float) LOWER,b.CONDITION_NAME ");
                sb.Append("from T_BASE_EVALUATION_CON_ITEM a left join T_BASE_EVALUATION_CON_INFO b on(a.CONDITION_ID=b.ID) ");
                sb.Append("where a.STANDARD_ID='" + dt.Rows[i]["CONDITION_ID"].ToString() + "' and a.ITEM_ID='" + dt.Rows[i]["ITEM_ID"].ToString() + "' and a.IS_DEL='0'");

                dtStand = ExecuteDataTable(sb.ToString());
                //dtStand有数据时表示该监测项有设置评价标准
                if (dtStand.Rows.Count > 0)
                {
                    if (IsNumeric(dt.Rows[i]["ITEM_VALUE"].ToString()))
                    {
                        decimal dValue = 0;
                        dValue = decimal.Parse(dt.Rows[i]["ITEM_VALUE"].ToString() == "" ? "0" : dt.Rows[i]["ITEM_VALUE"].ToString());

                        dr = dtStand.Select("UPPER>=" + dValue + " and LOWER<=" + dValue);
                        if (dr.Length > 0)
                        {
                            ConditionName = dr[0]["CONDITION_NAME"].ToString();
                            ZHEval = CompareEval(ConditionName, ZHEval);
                        }
                    }

                    if (ConditionName == "")
                        ConditionName = "超出范围";
                }

                sb.Remove(0, sb.Length);
                sb.Append("update " + FillItemTB + " set JUDGE='" + ConditionName + "' where ID='" + dt.Rows[i]["ID"].ToString() + "'");
                list.Add(sb.ToString());

                if (i == dt.Rows.Count - 1 && ZHEval.Length>0)
                {
                    sb.Remove(0, sb.Length);
                    sb.Append("update " + FillTB + " set JUDGE='" + ZHEval + "' where ID='" + dt.Rows[i]["FILL_ID"].ToString() + "'");
                    list.Add(sb.ToString());
                }
                else if (dt.Rows[i + 1]["FILL_ID"].ToString() != Fill_ID && ZHEval.Length > 0)
                {
                    sb.Remove(0, sb.Length);
                    sb.Append("update " + FillTB + " set JUDGE='" + ZHEval + "' where ID='" + dt.Rows[i]["FILL_ID"].ToString() + "'");
                    list.Add(sb.ToString());
                }
            }

            return ExecuteSQLByTransaction(list);
        }
        #endregion

        #region//用于点位新增、修改时判断是否存在相同的数据
        /// <summary>
        /// 用于点位新增、修改时判断是否存在相同的数据
        /// </summary>
        /// <param name="TableName">点位表名</param>
        /// <param name="Year">年份</param>
        /// <param name="Months">月份</param>
        /// <param name="ColName">点位名称</param>
        /// <param name="ColValue">点位名称值</param>
        /// <param name="ColCode">点位编码</param>
        /// <param name="ColCodeName">点位编码值</param>
        /// <param name="ColID">点位ID</param>
        /// <param name="ColIDValue">点位ID</param>
        /// <param name="flag">监测标识 0：按月监测  1：按季度监测  2：按年监测</param>
        /// <returns></returns>
        public string isExistDatas(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeName, string ColID, string ColIDValue,int flag)
        {
            string RtnMeg = string.Empty;
            RtnMeg = this.VailidName(TableName, Year, Months, ColName, ColNameValue, ColCode, ColCodeName, ColID, ColIDValue, flag);
            return RtnMeg;
        }

        #region//监测点编码名称和名称
        private string VailidName(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeValue, string ColID, string ColIDValue, int flag)
        {
            string sql = "";
            string Message = string.Empty;
            string Where = string.Empty;
            Months = Months.Replace(";", ",");
            string[] month = Months.Split(',');
            DataTable dt = new DataTable();
            switch (flag)
            {
                case 0:
                    #region//年和月
                    foreach (string st in month)
                    {
                        Where = "YEAR='" + Year + "' and MONTH in (" + st + ") and  " + ColCode + "='" + ColCodeValue + "'  and IS_DEL='0'  and " + ColID + "!='" + ColIDValue + "'";
                        sql = "select year,month from " + TableName + " where " + Where;
                        dt = ExecuteDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Message = dt.Rows[0][0].ToString() + "年" + st + "月,测点编码存在重复";
                            break;
                        }
                        else
                        {
                            Where = "YEAR='" + Year + "' and MONTH in (" + st + ") and  " + ColName + "='" + ColNameValue + "'  and IS_DEL='0'  and " + ColID + "!='" + ColIDValue + "'";
                            sql = "select year,month from " + TableName + " where " + Where;
                            dt = ExecuteDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                Message = dt.Rows[0][0].ToString() + "年" + st + "月,测点名称存在重复";
                                break;
                            }
                            else
                            {
                                Where = "YEAR='" + Year + "' and MONTH in (" + st + ") and (" + ColName + "='" + ColNameValue + "' or " + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' order by month  ";
                                sql = "select year,month from " + TableName + " where " + Where;
                                dt = ExecuteDataTable(sql);
                                if (dt.Rows.Count > 0)
                                {
                                    Message = dt.Rows[0][0].ToString() + "年" + st.ToString() + "月,测点编码和名称存在重复";
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 1:
                    #region//季度
                    string M = "";
                    int One = 0;
                    int Two = 0;
                    int Three = 0;
                    int Four = 0;
                    int Quarter = 0; //季度

                    for (int i = 0; i < month.Length; i++)
                    {
                        Quarter = int.Parse(month[i]) / 3 + (int.Parse(month[i]) % 3 > 0 ? 1 : 0);
                        switch (Quarter)
                        {
                            case 1:
                                One++;
                                M += "'1','2','3',";
                                break;
                            case 2:
                                Two++;
                                M += "'4','5','6',";
                                break;
                            case 3:
                                Three++;
                                M += "'7','8','9',";
                                break;
                            case 4:
                                Four++;
                                M += "'10','11','12',";
                                break;
                        }
                    }
                    if (One > 1)
                    { Message = "一季度只能选择一个月代表"; }
                    else if (Two > 1)
                    { Message = "二季度只能选择一个月代表"; }
                    else if (Three > 1)
                    { Message = "三季度只能选择一个月代表"; }
                    else if (Four > 1)
                    { Message = "四季度只能选择一个月代表"; }
                    else
                    {
                        #region//编码的校验
                        Where = "YEAR='" + Year + "' and MONTH in (" + M.TrimEnd(',') + ") and (" + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' order by month   ";
                        sql = "select MONTH from " + TableName + " where " + Where;
                        dt = ExecuteDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (string strmonth in month)
                            {
                                if (int.Parse(strmonth.ToString()) >= 1 && int.Parse(strmonth.ToString()) <= 3)
                                {
                                    Message = Year + "年一季度,测点编码存在重复";
                                }
                                else if (int.Parse(strmonth.ToString()) >= 4 && int.Parse(strmonth.ToString()) <= 6)
                                {
                                    Message = Year + "年二季度,测点编码存在重复";
                                }
                                else if (int.Parse(strmonth.ToString()) >= 7 && int.Parse(strmonth.ToString()) <= 9)
                                {
                                    Message = Year + "年三季度,测点编码存在重复";
                                }
                                else if (int.Parse(strmonth.ToString()) >= 10 && int.Parse(strmonth.ToString()) <= 12)
                                {
                                    Message = Year + "年四季度,测点编码存在重复";
                                }
                            }
                        }
                        else
                        {
                            #region//名称的校验
                            Where = "YEAR='" + Year + "' and MONTH in (" + M.TrimEnd(',') + ") and (" + ColName + "='" + ColNameValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' order by month   ";
                            sql = "select MONTH from " + TableName + " where " + Where;
                            dt = ExecuteDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (string strmonth in month)
                                {
                                    if (int.Parse(strmonth.ToString()) >= 1 && int.Parse(strmonth.ToString()) <= 3)
                                    {
                                        Message = Year + "年一季度,测点名称存在重复";
                                    }
                                    else if (int.Parse(strmonth.ToString()) >= 4 && int.Parse(strmonth.ToString()) <= 6)
                                    {
                                        Message = Year + "年二季度,测点名称存在重复";
                                    }
                                    else if (int.Parse(strmonth.ToString()) >= 7 && int.Parse(strmonth.ToString()) <= 9)
                                    {
                                        Message = Year + "年三季度,测点名称存在重复";
                                    }
                                    else if (int.Parse(strmonth.ToString()) >= 10 && int.Parse(strmonth.ToString()) <= 12)
                                    {
                                        Message = Year + "年四季度,测点名称存在重复";
                                    }
                                }
                            }
                            else
                            {
                                #region//编码和名称的校验
                                Where = "YEAR='" + Year + "' and MONTH in (" + M.TrimEnd(',') + ") and (" + ColName + "='" + ColNameValue + "' or " + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' order by month   ";
                                sql = "select MONTH from " + TableName + " where " + Where;
                                dt = ExecuteDataTable(sql);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (string strmonth in month)
                                    {
                                        if (int.Parse(strmonth.ToString()) >= 1 && int.Parse(strmonth.ToString()) <= 3)
                                        {
                                            Message = Year + "年一季度,测点编码和名称存在重复";
                                        }
                                        else if (int.Parse(strmonth.ToString()) >= 4 && int.Parse(strmonth.ToString()) <= 6)
                                        {
                                            Message = Year + "年二季度,测点编码和名称存在重复";
                                        }
                                        else if (int.Parse(strmonth.ToString()) >= 7 && int.Parse(strmonth.ToString()) <= 9)
                                        {
                                            Message = Year + "年三季度,测点编码和名称存在重复";
                                        }
                                        else if (int.Parse(strmonth.ToString()) >= 10 && int.Parse(strmonth.ToString()) <= 12)
                                        {
                                            Message = Year + "年四季度,测点编码和名称存在重复";
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                    break;
                case 2:
                    #region//年
                    Where = "YEAR='" + Year + "' and (" + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "'";
                    sql = "select year from " + TableName + " where " + Where;
                    dt = ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0][0].ToString() + "年,测点编码存在重复";
                    }
                    else
                    {
                        Where = "YEAR='" + Year + "' and (" + ColName + "='" + ColNameValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "'";
                        sql = "select year from " + TableName + " where " + Where;
                        dt = ExecuteDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Message = dt.Rows[0][0].ToString() + "年,测点名称存在重复";
                        }
                        else
                        {
                            Where = "YEAR='" + Year + "' and (" + ColName + "='" + ColNameValue + "' or " + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "'";
                            sql = "select year from " + TableName + " where " + Where;
                            dt = ExecuteDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                Message = dt.Rows[0][0].ToString() + "年,测点名称和编码存在重复";
                            }
                        }
                    }
                    #endregion
                    break;
            }
            return Message;
        }
        #endregion


        #endregion

        #region// 用于点位新增、修改时判断污染源常规（废水、废气）是否存在相同的数据
        public string isExistRepeat(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeName, string ColID, string ColIDValue, string TYPE_ID)
        {
            string RtnMeg = string.Empty;
            RtnMeg = this.VailidInfo(TableName, Year, Months, ColName, ColNameValue, ColCode, ColCodeName, ColID, ColIDValue, TYPE_ID);
            return RtnMeg;
        }
        private string VailidInfo(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeValue, string ColID, string ColIDValue,string TYPE_ID)
        {
            string enter_name = string.Empty;
            string type_name = string.Empty;
            string enter_id = string.Empty;
            DataTable dt = new DataTable();
            string Message = string.Empty;
            Months = Months.Replace(";", ",");
            string[] month = Months.Split(',');
            foreach (string st in month)
            {
                if (!string.IsNullOrEmpty(ColIDValue))//为空时，为新增，否则为修改
                {
                    #region//修改时校验
                    StringBuilder sb = new StringBuilder(256);
                    sb.Append("select c.enter_name, b.TYPE_NAME  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                    sb.Append(" where a.id='" + ColIDValue + "'");
                    DataTable result = SqlHelper.ExecuteDataTable(sb.ToString());
                    if (result.Rows.Count > 0)
                    {
                         enter_name = result.Rows[0][0].ToString();//获取企业名称
                          type_name = result.Rows[0][1].ToString();//获取类型
                        //监测点编码校验
                        StringBuilder sb1 = new StringBuilder(256);
                        sb1.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                        sb1.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_code='" + ColCodeValue + "' and  a.IS_DEL='0'  and a.id!='" + ColIDValue + "'");
                        dt = ExecuteDataTable(sb1.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            Message = dt.Rows[0][0].ToString() + "年" + st + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点编码存在重复";
                            break;
                        }
                        else
                        {
                            #region//监测点名称的校验
                            StringBuilder sb2 = new StringBuilder(256);
                            sb2.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                            sb2.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_name='" + ColNameValue + "' and  a.IS_DEL='0'  and a.id!='" + ColIDValue + "'");
                            dt = ExecuteDataTable(sb2.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                Message = dt.Rows[0][0].ToString() + "年" + st + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点名称存在重复";
                                break;
                            }
                            else
                            {
                                #region//监测点 编码和名称的校验
                                StringBuilder sb3 = new StringBuilder(256);
                                sb3.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                                sb3.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and (a.point_name='" + ColNameValue + "' or a.point_code='" + ColCodeValue + "' ) and  a.IS_DEL='0'  and a.id!='" + ColIDValue + "' order by month");
                                dt = ExecuteDataTable(sb3.ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    Message = dt.Rows[0][0].ToString() + "年" + st.ToString() + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点编码和名称存在重复";
                                    break;
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    #region//新增时校验
                    if (!string.IsNullOrEmpty(TYPE_ID))
                    {
                        string str = "select a.TYPE_NAME,b.enter_name, b.ID from T_ENV_P_POLLUTE_TYPE a left join T_ENV_P_ENTERINFO b on a.sataions_id=b.id where a.id='" + TYPE_ID + "'";
                        DataTable strDT = SqlHelper.ExecuteDataTable(str);
                        if (strDT.Rows.Count > 0)
                        {
                            enter_name = strDT.Rows[0][1].ToString();//获取企业名称
                            type_name = strDT.Rows[0][0].ToString();//获取类型
                            enter_id = strDT.Rows[0][2].ToString();
                            //监测点编码校验
                            StringBuilder sb4 = new StringBuilder(256);
                            sb4.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                            //by yinchengyi 2015-9-25 企业添加污染源点位信息时报告重复
                            //sb4.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_code='" + ColCodeValue + "' and  a.IS_DEL='0' ");
                            sb4.Append(" where c.id='" + enter_id + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_code='" + ColCodeValue + "' and  a.IS_DEL='0' ");
                            dt = ExecuteDataTable(sb4.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                Message = dt.Rows[0][0].ToString() + "年" + st + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点编码存在重复";
                                break;
                            }
                            else
                            {
                                #region//监测点名称的校验
                                StringBuilder sb5 = new StringBuilder(256);
                                //by yinchengyi 2015-9-25 企业添加污染源点位信息时报告重复
                                sb5.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                                //sb5.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_name='" + ColNameValue + "' and  a.IS_DEL='0' ");
                                sb5.Append(" where c.id='" + enter_id + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and a.point_name='" + ColNameValue + "' and  a.IS_DEL='0' ");
                                dt = ExecuteDataTable(sb5.ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    Message = dt.Rows[0][0].ToString() + "年" + st + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点名称存在重复";
                                    break;
                                }
                                else
                                {
                                    #region//监测点 编码和名称的校验
                                    StringBuilder sb6 = new StringBuilder(256);
                                    sb6.Append(" select a.year,a.month  from  T_ENV_P_POLLUTE a left join  T_ENV_P_POLLUTE_TYPE b on a.type_id=b.id left join T_ENV_P_ENTERINFO c on b.sataions_id=c.id ");
                                    //by yinchengyi 2015-9-25 企业添加污染源点位信息时报告重复
                                    //sb6.Append(" where c.enter_name='" + enter_name + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and (a.point_name='" + ColNameValue + "' or a.point_code='" + ColCodeValue + "' ) and  a.IS_DEL='0'     order by month");
                                    sb6.Append(" where c.id='" + enter_id + "' and b.TYPE_NAME='" + type_name + "' and  a.YEAR='" + Year + "' and a.MONTH in (" + st + " ) and (a.point_name='" + ColNameValue + "' or a.point_code='" + ColCodeValue + "' ) and  a.IS_DEL='0'     order by month");
                                    dt = ExecuteDataTable(sb6.ToString());
                                    if (dt.Rows.Count > 0)
                                    {
                                        Message = dt.Rows[0][0].ToString() + "年" + st.ToString() + "月,企业:[" + enter_name + "],类型:[" + type_name + "]的测点编码和名称存在重复";
                                        break;
                                    }
                                    #endregion
                                }
                                #endregion
                            }



                        }
                    }
                    #endregion
                }
            }
            return Message;
        }
        #endregion

        #region//根据类别校验企业不能重复(污染源常规)
        public string Is_EnterPrise(string head, string type, string Type_id)
        {
            string meg = string.Empty;
            string strSQL = string.Empty;
            if (!string.IsNullOrEmpty(head))
            {
                 strSQL = "SELECT ENTER_NAME FROM T_ENV_P_ENTERINFO WHERE ID='" + head + "'";
                string result = SqlHelper.ExecuteScalar(strSQL).ToString();//获取企业名称
                if (!string.IsNullOrEmpty(result))
                {
                    strSQL = "select * from  T_ENV_P_POLLUTE_TYPE  a left join T_ENV_P_ENTERINFO b on a.SATAIONS_ID=b.id where a.is_del='0' and  a.type_name='" + type + "' and b.enter_name='" + result + "' and a.id!='" + Type_id + "'";
                    DataTable dt = SqlHelper.ExecuteDataTable(strSQL);
                    if (dt.Rows.Count > 0)
                    {
                        meg = "企业:" + result + "类别:" + type+"存在重复！";
                    }
                }
            }
            return meg;
        }
        #endregion

        #region//根据类别校验企业不能重复(污染源常规)
        public string Update_EnterPrise(string type, string Type_id)
        {
            string meg = string.Empty;
            string strSQL = string.Empty;
            if (!string.IsNullOrEmpty(Type_id))
            {
                strSQL = "select b.enter_name from  T_ENV_P_POLLUTE_TYPE  a left join T_ENV_P_ENTERINFO b on a.SATAIONS_ID=b.id  WHERE a.is_del='0' and a.ID='" + Type_id + "'";
                string result = SqlHelper.ExecuteScalar(strSQL).ToString();//获取企业名称
                if (!string.IsNullOrEmpty(result))
                {
                    strSQL = "select * from  T_ENV_P_POLLUTE_TYPE  a left join T_ENV_P_ENTERINFO b on a.SATAIONS_ID=b.id where a.is_del='0' and  a.type_name='" + type + "' and b.enter_name='" + result + "' and a.id!='" + Type_id + "'";
                    DataTable dt = SqlHelper.ExecuteDataTable(strSQL);
                    if (dt.Rows.Count > 0)
                    {
                        meg = "企业:" + result + "类别:" + type + "存在重复！";
                    }
                }
            }
            return meg;
        }
        #endregion

        #region//饮用水源地点位新增、修改时判断是否存在相同的数据
        /// <summary>
        /// 用于点位新增、修改时判断是否存在相同的数据
        /// </summary>
        /// <param name="TableName">点位表名</param>
        /// <param name="Year">年份</param>
        /// <param name="Months">月份</param>
        /// <param name="ColName">点位名称</param>
        /// <param name="ColValue">点位名称值</param>
        /// <param name="ColCode">点位编码</param>
        /// <param name="ColCodeName">点位编码值</param>
        /// <param name="ColID">点位ID</param>
        /// <param name="ColIDValue">点位ID</param>
        /// <param name="flag">surface:地表饮用水；under:地下饮用水</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string IsExistData(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeName, string ColID, string ColIDValue, string flag)
        {
            string RtnMeg = string.Empty;
            RtnMeg = this.Valied(TableName, Year, Months, ColName, ColNameValue, ColCode, ColCodeName, ColID, ColIDValue, flag);
            return RtnMeg;
        }
        private string Valied(string TableName, string Year, string Months, string ColName, string ColNameValue, string ColCode, string ColCodeValue, string ColID, string ColIDValue, string flag)
        {
            string sql = "";
            string Message = string.Empty;
            string Where = string.Empty;
            Months = Months.Replace(";", ",");
            string[] month = Months.Split(',');
            DataTable dt = new DataTable();
            #region//年
            Where = "YEAR='" + Year + "' and (" + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' and NUM='"+flag+"'";
            sql = "select year from " + TableName + " where " + Where;
            dt = ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                Message = dt.Rows[0][0].ToString() + "年,测点编码存在重复";
            }
            else
            {
                Where = "YEAR='" + Year + "' and (" + ColName + "='" + ColNameValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' and NUM='" + flag + "'"; ;
                sql = "select year from " + TableName + " where " + Where;
                dt = ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0][0].ToString() + "年,测点名称存在重复";
                }
                else
                {
                    Where = "YEAR='" + Year + "' and (" + ColName + "='" + ColNameValue + "' or " + ColCode + "='" + ColCodeValue + "')  and IS_DEL='0' and " + ColID + "!='" + ColIDValue + "' and NUM='" + flag + "'"; ;
                    sql = "select year from " + TableName + " where " + Where;
                    dt = ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0][0].ToString() + "年,测点名称和编码存在重复";
                    }
                }
            }
            #endregion

            return Message;
        }
        #endregion

        /// <summary>
        /// 判断是否存在某年某的点位信息
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns></returns>
        public bool ExistPointData(string TableName, string Year, string Month)
        {
            string sql = "";

            sql = "select 1 from " + TableName + " where YEAR='" + Year + "' and MONTH='" + Month + "' and IS_DEL='0'";

            object objResult = ExecuteScalar(sql);
            return null != objResult ? true : false;
        }

        /// <summary>
        /// 点位复制逻辑
        /// </summary>
        /// <param name="TableName">点位主表名</param>
        /// <param name="strSerial">断面、测点 序列号</param>
        /// <param name="strSerialV">垂线 序列号</param>
        /// <param name="strSerialVItem">监测项目 序列号</param>
        /// <param name="Year_From"></param>
        /// <param name="Month_From"></param>
        /// <param name="Year_To"></param>
        /// <param name="Month_To"></param>
        /// <returns></returns>
        public string CopyPointData(string TableName, string strSerial, string strSerialV, string strSerialVItem, string Year_From, string Month_From, string Year_To, string Month_To)
        {

            bool b = true;
            string Msg = "";

            DataTable dt = new DataTable();
            DataTable dtV = new DataTable();
            DataTable dtVI = new DataTable();
            string sql = string.Empty;
            ArrayList sqlList = new ArrayList();
            string strID = string.Empty;
            string strVID = string.Empty;
            string strVIID = string.Empty;

            sql = "select * from " + TableName + " where YEAR='" + Year_From + "' and MONTH='" + Month_From + "' and IS_DEL='0'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "Update " + TableName + " set IS_DEL='1' where YEAR='" + Year_To + "' and MONTH='" + Month_To + "'";
                sqlList.Add(sql);
                foreach (DataRow row in dt.Rows)
                {
                    //断面、测点
                    strID = GetSerialNumber(strSerial);
                    sql = getCopySql(TableName, row, Year_To, Month_To, "", strID);
                    sqlList.Add(sql);

                    if (strSerialV != "")
                    {
                        //垂线
                        sql = "select * from " + TableName + "_V" + " where SECTION_ID='" + row["ID"].ToString() + "'";
                        dtV = SqlHelper.ExecuteDataTable(sql);

                        foreach (DataRow rowV in dtV.Rows)
                        {
                            strVID = GetSerialNumber(strSerialV);
                            sql = getCopySql(TableName + "_V", rowV, "", "", strID, strVID);
                            sqlList.Add(sql);
                            //监测项目
                            sql = "select * from " + TableName + "_V_ITEM" + " where POINT_ID='" + rowV["ID"].ToString() + "'";
                            dtVI = SqlHelper.ExecuteDataTable(sql);
                            foreach (DataRow rowVI in dtVI.Rows)
                            {
                                strVIID = GetSerialNumber(strSerialVItem);
                                sql = getCopySql(TableName + "_V_ITEM", rowVI, "", "", strVID, strVIID);
                                sqlList.Add(sql);
                            }
                        }
                    }
                    else
                    {
                        //监测项目
                        sql = "select * from " + TableName + "_ITEM" + " where POINT_ID='" + row["ID"].ToString() + "'";
                        dtVI = SqlHelper.ExecuteDataTable(sql);
                        foreach (DataRow rowVI in dtVI.Rows)
                        {
                            strVIID = GetSerialNumber(strSerialVItem);
                            sql = getCopySql(TableName + "_ITEM", rowVI, "", "", strID, strVIID);
                            sqlList.Add(sql);
                        }
                    }

                }
                if (SqlHelper.ExecuteSQLByTransaction(sqlList))
                {
                    b = true;
                }
                else
                {
                    b = false;
                    Msg = "数据库更新失败";
                }
            }
            else
            {
                b = false;
                Msg = "复制的年月份没有数据";
            }

            if (b)
                return "({result:true,msg:''})";
            else
                return "({result:false,msg:'" + Msg + "'})";
        }

        /// <summary>
        /// 获取数据填报的基础信息（数据填报走审核、签发流程）
        /// </summary>
        /// <param name="PF_ID">带类型的填报ID</param>
        /// <returns></returns>
        public DataTable GetPointFillInfo(string PF_ID, char s)
        {
            string[] str = PF_ID.Split(s);
            string Type = str[0].ToString();
            string Year = str[1].ToString();
            string Month = str[2].ToString();
            string strSql = string.Empty;
            switch (Type)
            {
                case "HL":             //河流
                    strSql = @"select '河流数据填报' TITLE,'T_ENV_FILL_RIVER' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_RIVER where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "DX":            //地下饮用水
                    strSql = @"select '地下饮用水数据填报' TITLE,'T_ENV_FILL_DRINK_UNDER' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_DRINK_UNDER where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "HK":             //湖库
                    strSql = @"select '湖库数据填报' TITLE,'T_ENV_FILL_LAKE' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_LAKE where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "DS":            //饮用水源地
                    strSql = @"select '饮用水源地数据填报' TITLE,'T_ENV_FILL_DRINK_SRC' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_DRINK_SRC where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "AZ":             //空气（自动室）
                    strSql = @"select '空气自动室数据填报' TITLE,'T_ENV_FILL_AIR' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_AIR where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "AK":            //空气（科室）
                    strSql = @"select '空气科室数据填报' TITLE,'T_ENV_FILL_AIRKS' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_AIRKS where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "AH":             //空气（小时）
                    strSql = @"select '空气（小时）数据填报' TITLE,'T_ENV_FILL_AIRHOUR' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_AIRHOUR where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "AL":            //硫酸盐化速率
                    strSql = @"select '硫酸盐化速率数据填报' TITLE,'T_ENV_FILL_ALKALI' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_ALKALI where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "NA":             //区域环境噪声
                    strSql = @"select '区域环境噪声数据填报' TITLE,'T_ENV_FILL_NOISE_AREA_SUMMARY' TableName,YEAR,'' MONTH,STATUS from 
                            T_ENV_FILL_NOISE_AREA_SUMMARY where YEAR='{0}'";
                    break;
                case "NR":            //道路交通噪声
                    strSql = @"select '道路交通噪声数据填报' TITLE,'T_ENV_FILL_NOISE_ROAD_SUMMARY' TableName,YEAR,'' MONTH,STATUS from 
                            T_ENV_FILL_NOISE_ROAD_SUMMARY where YEAR='{0}'";
                    break;
                case "NF":             //功能区噪声
                    string months = string.Empty;
                    if (Month == "1")
                        months = "1,2,3";
                    else if (Month == "2")
                        months = "4,5,6";
                    else if (Month == "3")
                        months = "7,8,9";
                    else if (Month == "4")
                        months = "10,11,12";
                    strSql = "select '功能区噪声数据填报' TITLE,'T_ENV_FILL_NOISE_FUNCTION_SUMMARY' TableName,YEAR,'" + months + "'MONTH,STATUS from T_ENV_FILL_NOISE_FUNCTION_SUMMARY where YEAR='{0}' and QUTER='{1}'";
                    break;
                case "SE":            //近岸海域
                    strSql = @"select '近岸海域数据填报' TITLE,'T_ENV_FILL_SEA' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_SEA where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "ET":             //入海河口
                    strSql = @"select '入海河口数据填报' TITLE,'T_ENV_FILL_ESTUARIES' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_ESTUARIES where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "OF":            //近岸直排
                    strSql = @"select '近岸直排数据填报' TITLE,'T_ENV_FILL_OFFSHORE' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_OFFSHORE where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "SB":             //海水浴场
                    strSql = @"select '海水浴场数据填报' TITLE,'T_ENV_FILL_SEABATH' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_SEABATH where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "RA":             //降水
                    strSql = @"select '降水数据填报' TITLE,'T_ENV_FILL_RAIN' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_RAIN where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "DU":             //降尘
                    strSql = @"select '降尘数据填报' TITLE,'T_ENV_FILL_DUST' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_DUST where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "SI":             //土壤
                    strSql = @"select '土壤数据填报' TITLE,'T_ENV_FILL_SOIL' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_SOIL where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "SL":             //固废
                    strSql = @"select '固废数据填报' TITLE,'T_ENV_FILL_SOLID' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_SOLID where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "PF":             //生态补偿
                    strSql = @"select '生态补偿数据填报' TITLE,'T_ENV_FILL_PAYFOR' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_PAYFOR where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "RR":             //双三十水质
                    strSql = @"select '双三十水质数据填报' TITLE,'T_ENV_FILL_RIVER30' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_RIVER30 where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "AA":             //双三十空气
                    strSql = @"select '双三十空气数据填报' TITLE,'T_ENV_FILL_AIR30' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_AIR30 where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "MR":             //沉积物（河流）
                    strSql = @"select '沉积物（河流）数据填报' TITLE,'T_ENV_FILL_MUD_RIVER' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_MUD_RIVER where YEAR='{0}' and MONTH='{1}'";
                    break;
                case "MS":             //沉积物（海水）
                    strSql = @"select '沉积物（海水）数据填报' TITLE,'T_ENV_FILL_MUD_SEA' TableName,YEAR,MONTH,STATUS from 
                            T_ENV_FILL_MUD_SEA where YEAR='{0}' and MONTH='{1}'";
                    break;
                default:
                    break;
            }

            strSql = string.Format(strSql, Year, Month);

            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 更改数据填报的状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strTable"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateFillStatus(string ID, string strTable, string Status)
        {
            string[] str = ID.Split('^');
            string Year = str[1].ToString();
            string Month = str[2].ToString();
            string sql = string.Empty;

            if (strTable.Contains("T_ENV_FILL_NOISE_AREA") || strTable.Contains("T_ENV_FILL_NOISE_ROAD"))
            {
                sql = "update " + strTable + " set STATUS='" + Status + "' where YEAR='" + Year + "'";
            }
            else if (strTable.Contains("T_ENV_FILL_NOISE_FUNCTION"))
            {
                sql = "update " + strTable + " set STATUS='" + Status + "' where YEAR='" + Year + "' and QUTER='" + Month + "'";
            }
            else
                sql = "update " + strTable + " set STATUS='" + Status + "' where YEAR='" + Year + "' and MONTH='" + Month + "'";

            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取某年月填报数据的监测值
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strTable">填报主表</param>
        /// <param name="strItmeTable">填报项目表</param>
        /// <param name="strPointTable">测点表</param>
        /// <param name="m">1：三级结构  2：二级结构</param>
        /// <returns></returns>
        public DataTable GetFillValue(string strYear, string strMonth, string strTable, string strItmeTable, string strPointTable, string m)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";

            if (m == "1")
            {
                sql = "select a.ID,b.SECTION_NAME " + strTable + "@NAME@监测点 from {0} a left join {1} b on(a.SECTION_ID=b.ID) where a.YEAR='{2}' and a.MONTH='{3}'";
            }
            else
            {
                if (strTable == "T_ENV_FILL_NOISE_AREA" || strTable == "T_ENV_FILL_NOISE_ROAD")
                {
                    sql = "select a.ID,b.POINT_NAME " + strTable + "@NAME@监测点 from {0} a left join {1} b on(a.POINT_ID=b.ID) where a.YEAR='{2}'";
                }
                else if (strTable == "T_ENV_FILL_NOISE_FUNCTION")
                {
                    if (strMonth == "1")
                        strMonth = "1,2,3";
                    else if (strMonth == "2")
                        strMonth = "4,5,6";
                    else if (strMonth == "3")
                        strMonth = "7,8,9";
                    else if (strMonth == "4")
                        strMonth = "10,11,12";
                    sql = "select a.ID,b.POINT_NAME " + strTable + "@NAME@监测点 from {0} a left join {1} b on(a.POINT_ID=b.ID) where a.YEAR='{2}' and a.MONTH in ({3})";
                }
                else
                {
                    sql = "select a.ID,b.POINT_NAME " + strTable + "@NAME@监测点 from {0} a left join {1} b on(a.POINT_ID=b.ID) where a.YEAR='{2}' and a.MONTH='{3}'";
                }
            }
            sql = string.Format(sql, strTable, strPointTable, strYear, strMonth);

            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                string FillIDs = "";
                foreach (DataRow drMain in dtMain.Rows)
                    FillIDs += "'" + drMain["ID"].ToString() + "',";

                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1})
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                sql = string.Format(sql, strItmeTable, FillIDs.TrimEnd(','));
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(strItmeTable + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1})";
                sql = string.Format(sql, strItmeTable, FillIDs.TrimEnd(','));
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                            drMain[strItmeTable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                        else
                            drMain[strItmeTable + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                    }
                }
            }


            return dtMain;
        }

        /// <summary>
        /// 判断两个评价范围，返回较差的评价
        /// </summary>
        /// <param name="NewValue"></param>
        /// <param name="OldValue"></param>
        /// <returns></returns>
        public string CompareEval(string NewValue, string OldValue)
        {
            switch (OldValue)
            {
                case "":
                    OldValue = NewValue;
                    break;
                case "I类":
                    if (NewValue == "II类" || NewValue == "III类" || NewValue == "IV类" || NewValue == "V类")
                        OldValue = NewValue;
                    break;
                case "II类":
                    if (NewValue == "III类" || NewValue == "IV类" || NewValue == "V类")
                        OldValue = NewValue;
                    break;
                case "III类":
                    if (NewValue == "IV类" || NewValue == "V类")
                        OldValue = NewValue;
                    break;
                case "IV类":
                    if (NewValue == "V类")
                        OldValue = NewValue;
                    break;
                case "V类":
                    break;
                default:
                    OldValue = NewValue;
                    break;
            }

            return OldValue;
        }
        /// <summary>
        /// 返回环境质量的监测点位信息
        /// </summary>
        /// <param name="strTableName">点位表表名</param>
        /// <param name="strCode">点位编码字段名</param>
        /// <param name="strName">点位名称字段名</param>
        /// <param name="strPointName">查询的关键字</param>
        /// <returns></returns>
        public DataTable getPointInfo(string strTableName, string strCode, string strName, string strPointName)
        {
            string strSql = "";
            strSql = "select distinct " + strName + " NAME from " + strTableName + " where is_del='0' ";
            if (strPointName != "")
            {
                strSql += "and " + strName + " like '%" + strPointName + "%'";
            }

            return ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取企业的点位信息
        /// </summary>
        /// <param name="strCompanyID"></param>
        /// <returns></returns>
        public DataTable getCompanyPointInfo(string strCompanyID, string strPointName)
        {
            string strSql = "";
            strSql = @"select distinct a.POINT_NAME from T_MIS_MONITOR_TASK_POINT a left join T_MIS_MONITOR_TASK_COMPANY b on(a.COMPANY_ID=b.ID)
                    left join T_MIS_CONTRACT_COMPANY c on (b.COMPANY_ID=c.ID) where c.COMPANY_ID='" + strCompanyID + "'";
            if (strPointName != "")
            {
                strSql += "a.POINT_NAME like '%" + strPointName + "%'";
            }

            return ExecuteDataTable(strSql);
        }

        #region//获取环境质量监测数据展现信息
        public DataTable GetEnvMoniotrData(string strMonitorType, string strPointNames, string strDateStart, string strDateEnd, string strItemIDs)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "1=1";
            string PointFilleID = "";
            string PointFileName = "";
            string DayFileName = "";
            string PointTableName = "";
            string FillTableName = "";
            string FillITableName = "";
            string MonitorType = "";

            switch (strMonitorType)
            {
                case "EnvStbc":              //生态补偿
                    PointTableName = "T_ENV_P_PAYFOR";
                    FillTableName = "T_ENV_FILL_PAYFOR";
                    FillITableName = "T_ENV_FILL_PAYFOR_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "DAY";
                    MonitorType = "生态补偿";
                    break;
                case "EnvReservoir":         //湖库
                    PointTableName = "T_ENV_P_LAKE";
                    FillTableName = "T_ENV_FILL_LAKE";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "湖库";
                    break;
                case "EnvDrinkingSource":    //饮用水源地(湖库、河流)
                    PointTableName = "T_ENV_P_DRINK_SRC";
                    FillTableName = "T_ENV_FILL_DRINK_SRC";
                    FillITableName = "T_ENV_FILL_DRINK_SRC_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "饮用水源地(湖库、河流)";
                    break;
                case "EnvMudRiver":         //沉积物（河流）
                    PointTableName = "T_ENV_P_MUD_RIVER";
                    FillTableName = "T_ENV_FILL_MUD_RIVER";
                    FillITableName = "T_ENV_FILL_MUD_RIVER_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "沉积物（河流）";
                    break;
                case "EnvSoil":             //土壤
                    PointTableName = "T_ENV_P_SOIL";
                    FillTableName = "T_ENV_FILL_SOIL";
                    FillITableName = "T_ENV_FILL_SOIL_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "DAY";
                    MonitorType = "土壤";
                    break;
                case "EnvPSoil":            //固废
                    PointTableName = "T_ENV_P_SOLID";
                    FillTableName = "T_ENV_FILL_SOLID";
                    FillITableName = "T_ENV_FILL_SOLID_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "DAY";
                    MonitorType = "固废";
                    break;
                case "EnvRiverCity":        //城考
                    PointTableName = "T_ENV_P_RIVER_CITY";
                    FillTableName = "T_ENV_FILL_RIVER_CITY";
                    FillITableName = "T_ENV_FILL_RIVER_CITY_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "城考";
                    break;
                case "EnvRiverTarget":      //责任目标
                    PointTableName = "T_ENV_P_RIVER_TARGET";
                    FillTableName = "T_ENV_FILL_RIVER_TARGET";
                    FillITableName = "T_ENV_FILL_RIVER_TARGET_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "责任目标";
                    break;
                case "EnvRiverPlan":        //规划断面
                    PointTableName = "T_ENV_P_RIVER_PLAN";
                    FillTableName = "T_ENV_FILL_RIVER_PLAN";
                    FillITableName = "T_ENV_FILL_RIVER_PLAN_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "规划断面";
                    break;
                case "EnvRoadNoise":        //道路交通噪声
                    PointTableName = "T_ENV_P_NOISE_ROAD";
                    FillTableName = "T_ENV_FILL_NOISE_ROAD";
                    FillITableName = "T_ENV_FILL_NOISE_ROAD_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "道路交通噪声";
                    break;
                case "FunctionNoise":       //功能区噪声
                    PointTableName = "T_ENV_P_NOISE_FUNCTION";
                    FillTableName = "T_ENV_FILL_NOISE_FUNCTION";
                    FillITableName = "T_ENV_FILL_NOISE_FUNCTION_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "功能区噪声";
                    break;
                case "AreaNoise":           //区域噪声环境
                    PointTableName = "T_ENV_P_NOISE_AREA";
                    FillTableName = "T_ENV_FILL_NOISE_AREA";
                    FillITableName = "T_ENV_FILL_NOISE_AREA_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "区域噪声环境";
                    break;
                case "EnvDust":             //降尘
                    PointTableName = "T_ENV_P_DUST";
                    FillTableName = "T_ENV_FILL_DUST";
                    FillITableName = "T_ENV_FILL_DUST_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "降尘";
                    break;
                case "EnvRiver":            //河流
                    PointTableName = "T_ENV_P_RIVER";
                    FillTableName = "T_ENV_FILL_RIVER";
                    FillITableName = "T_ENV_FILL_RIVER_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "河流";
                    break;
                case "EnvDrinking":         //地下饮用水
                    PointTableName = "T_ENV_P_DRINK_UNDER";
                    FillTableName = "T_ENV_FILL_DRINK_UNDER";
                    FillITableName = "T_ENV_FILL_DRINK_UNDER_ITEM";
                    PointFileName = "SECTION_NAME";
                    PointFilleID = "SECTION_ID";
                    DayFileName = "DAY";
                    MonitorType = "地下饮用水";
                    break;
                case "EnvRain":             //降水
                    PointTableName = "T_ENV_P_RAIN";
                    FillTableName = "T_ENV_FILL_RAIN";
                    FillITableName = "T_ENV_FILL_RAIN_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "降水";
                    break;
                case "EnvAir":              //环境空气
                    PointTableName = "T_ENV_P_AIR";
                    FillTableName = "T_ENV_FILL_AIR";
                    FillITableName = "T_ENV_FILL_AIR_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "DAY";
                    MonitorType = "环境空气";
                    break;
                case "EnvSpeed":            //盐酸盐化速率
                    PointTableName = "T_ENV_P_ALKALI";
                    FillTableName = "T_ENV_FILL_ALKALI";
                    FillITableName = "T_ENV_FILL_ALKALI_ITEM";
                    PointFileName = "POINT_NAME";
                    PointFilleID = "POINT_ID";
                    DayFileName = "BEGIN_DAY";
                    MonitorType = "盐酸盐化速率";
                    break;
                default:
                    break;
            }

            if (strPointNames != "")
            {
                strWhere += " and b." + PointFileName + " in('" + strPointNames.Replace(";", "','") + "')";
            }
            if (strDateStart != "" && strDateEnd != "")
            {
                strWhere += " and cast(a.YEAR+'-'+a.MONTH+'-'+a." + DayFileName + " as datetime) >=cast('" + strDateStart + "' as datetime) and cast(a.YEAR+'-'+a.MONTH+'-'+a." + DayFileName + " as datetime) <=cast('" + strDateEnd + "' as datetime)";
            }

            #region //根据点位表信息更新填报表
            //获取填报表信息
            sql = "select a.ID,'{0}' {3}@MonitorType@监测类型,b.{1} {3}@{1}@点位名称,a.YEAR {3}@YEAR@年份,a.MONTH {3}@MONTH@月份 from {2} a left join {3} b on(a.{4}=b.ID)";
            sql += " where " + strWhere + " and b.IS_DEL='0'";

            sql = string.Format(sql, MonitorType, PointFileName, FillTableName, PointTableName, PointFilleID);
            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                string FillIDs = "";
                foreach (DataRow drMain in dtMain.Rows)
                    FillIDs += "'" + drMain["ID"].ToString() + "',";

                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            FILL_ID in({1}) {2}
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                sql = string.Format(sql, FillITableName, FillIDs.TrimEnd(','), strItemIDs == "" ? "" : (" and ITEM_ID in('" + strItemIDs.Replace(";", "','") + "')"));
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(FillITableName + "@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select ID,FILL_ID,ITEM_ID,ITEM_VALUE from {0} where FILL_ID in({1})";
                sql = string.Format(sql, FillITableName, FillIDs.TrimEnd(','));
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("FILL_ID='" + drMain["ID"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                            drMain[FillITableName + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                        else
                            drMain[FillITableName + "@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                    }
                }
            }
            #endregion

            return dtMain;
        }
        #endregion

        #region//获取污染源企业监测数据展现信息
        public DataTable GetPollMoniotrData(string strContractType, string strCompanyID, string strMonitorID, string strPointNames, string strDateStart, string strDateEnd, string strItemIDs)
        {
            DataTable dtMain = new DataTable();
            DataTable dtAllItem = new DataTable();
            string sql = "";
            string strWhere = "1=1";

            strWhere += " and a.CONTRACT_TYPE='" + strContractType + "'";
            strWhere += " and g.COMPANY_ID='" + strCompanyID + "'";
            strWhere += " and d.MONITOR_ID='" + strMonitorID + "'";
            if (strPointNames != "")
            {
                strWhere += " and k.POINT_NAME in('" + strPointNames.Replace(";", "','") + "')";
            }
            if (strDateStart != "" && strDateEnd != "")
            {
                strWhere += " and cast(j.FINISH_DATE as datetime) >=cast('" + strDateStart + "' as datetime) and cast(j.FINISH_DATE as datetime) <=cast('" + strDateEnd + "' as datetime)";
            }

            #region //根据点位表信息更新填报表
            //获取填报表信息
            sql = @"select distinct e.ID,f.DICT_TEXT a@CONTRACT_TYPE@委托类型,g.COMPANY_NAME a@COMPANY_NAME@企业名称,h.MONITOR_TYPE_NAME a@MONITOR_TYPE_NAME@监测类型,e.SAMPLE_NAME a@SAMPLE_NAME@点位名称 from 
                    T_MIS_CONTRACT a left join T_MIS_CONTRACT_PLAN b on(a.ID=b.CONTRACT_ID)
                    left join T_MIS_MONITOR_TASK c on(b.ID=c.PLAN_ID)
                    left join T_MIS_MONITOR_SUBTASK d on(c.ID=d.TASK_ID)
                    left join T_MIS_MONITOR_SAMPLE_INFO e on(d.ID=e.SUBTASK_ID)
                    left join T_SYS_DICT f on(a.CONTRACT_TYPE=f.DICT_CODE and f.DICT_TYPE='Contract_type')
                    left join T_MIS_CONTRACT_COMPANY g on(a.CLIENT_COMPANY_ID=g.ID)
                    left join T_BASE_MONITOR_TYPE_INFO h on(d.MONITOR_ID=h.ID)
                    left join T_MIS_MONITOR_RESULT i on(e.ID=i.SAMPLE_ID)
                    left join T_MIS_MONITOR_RESULT_APP j on(i.ID=j.RESULT_ID)
                    left join T_MIS_MONITOR_TASK_POINT k on(e.POINT_ID=k.ID)";
            sql += " where " + strWhere;

            dtMain = ExecuteDataTable(sql);
            if (dtMain.Rows.Count > 0)
            {
                //查询要填报的监测项
                string FillIDs = "";
                foreach (DataRow drMain in dtMain.Rows)
                    FillIDs += "'" + drMain["ID"].ToString() + "',";

                sql = @"select b.ID, b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            {0}
	                                            where
		                                            SAMPLE_ID in({1}) {2}
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
                sql = string.Format(sql, "T_MIS_MONITOR_RESULT", FillIDs.TrimEnd(','), strItemIDs == "" ? "" : (" and ITEM_ID in('" + strItemIDs.Replace(";", "','") + "')"));
                dtAllItem = ExecuteDataTable(sql);

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add("a@" + drAllItem["ID"].ToString() + "@" + drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFillItem = new DataTable(); //填报监测项数据
                DataRow[] drFillItem;

                //根据条件查询所有填报监测项数据
                sql = @"select ID,SAMPLE_ID,ITEM_ID,ITEM_RESULT from {0} where SAMPLE_ID in({1})";
                sql = string.Format(sql, "T_MIS_MONITOR_RESULT", FillIDs.TrimEnd(','));
                dtFillItem = ExecuteDataTable(sql);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFillItem = dtFillItem.Select("SAMPLE_ID='" + drMain["ID"].ToString() + "'");
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFillItem.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                            drMain["a@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = itemValue[0]["ITEM_RESULT"].ToString(); //填入监测项值
                        else
                            drMain["a@" + itemId + "@" + drAllItem["ITEM_NAME"].ToString()] = "--";
                    }
                }
            }
            #endregion

            return dtMain;
        }
        #endregion
    }
}
