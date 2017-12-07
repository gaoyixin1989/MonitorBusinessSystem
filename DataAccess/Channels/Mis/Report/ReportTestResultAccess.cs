using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Report;
using i3.ValueObject;
using System.Data;

namespace i3.DataAccess.Channels.Mis.Report
{

    /// <summary>
    /// 功能：监测项目信息查询
    /// 创建日期：2012-12-8
    /// 创建人：邵世卓
    /// </summary>
    public class ReportTestResultAccess : SqlHelper
    {
        #region IReportTestResult 成员

        /// <summary>
        /// 监测项目信息 项目及父项目
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemTypeID">监测类别ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectItemAndParentItem(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select distinct i.ID as SubID,i.ITEM_NAME as SubName,tii.PARENT_ITEM_ID as ParentID,ii.ITEM_NAME as ParentName  
                                            FROM T_MIS_MONITOR_TASK_ITEM ti
                                            INNER JOIN T_MIS_MONITOR_SAMPLE_INFO    si       ON  ti.TASK_POINT_ID  = si.POINT_ID 
                                            INNER JOIN T_MIS_MONITOR_RESULT sar ON  si.ID   = sar.SAMPLE_ID 
                                            INNER JOIN T_BASE_ITEM_INFO  i       ON  sar.ITEM_ID = i.ID 
                                            Left  JOIN T_BASE_ITEM_SUB_ITEM   tii     ON  tii.ITEM_ID = i.ID 
	                                        left join T_BASE_ITEM_INFO ii on tii.PARENT_ITEM_ID=ii.ID
                                            WHERE  i.IS_DEL='0' 
                                            AND si.QC_TYPE ='0'
                                            AND sar.QC_TYPE ='0'
                                            AND ti.TASK_POINT_ID in (select ID from T_MIS_MONITOR_TASK_POINT where TASK_ID='{0}' {1})";
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and MONITOR_ID='{0}'", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 监测项目信息 分页查询(DataTable) 
        /// </summary>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页显示行数</param>
        /// <param name="reportTestResult">查询对象</param>
        /// <returns>结果集</returns>
        public DataTable SelectTable_ST(int intPageIndex, int intPageSize, ReportTestResultVo reportTestResult, string strSampleCode)
        {
            string strSQL = " FROM T_MIS_MONITOR_SAMPLE_INFO sample" +
                                   " INNER JOIN T_MIS_MONITOR_RESULT result ON  sample.ID                     = result.SAMPLE_ID  " +
                                   " left JOIN T_MIS_MONITOR_TASK_POINT point ON  sample.POINT_ID              = point.ID  " +
                                   " INNER JOIN T_BASE_ITEM_INFO item ON  result.ITEM_ID        = item.ID  " +
                                   " INNER JOIN T_BASE_MONITOR_TYPE_INFO type ON  point.MONITOR_ID = type.ID  " +
                                   " INNER JOIN T_MIS_MONITOR_TASK task ON  point.TASK_ID  = task.ID  " +
                                   " left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=result.ANALYSIS_METHOD_ID" +
                                   " left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT" +
            " WHERE LEN(point.TASK_ID)>0  ";
            //" AND SAMPLE_INFO.QC_TYPE ='0'" +
            //" AND SAMPLE_ASSAY_RESULT.QC_TYPE ='0'";

            strSQL += " and (";
            strSQL += " (result.QC_TYPE='0' and sample.QC_TYPE='0'";
            strSQL += " and not EXISTS (SELECT * FROM T_MIS_MONITOR_QC_TWIN WHERE RESULT_ID_TWIN1 = result.ID))";
            strSQL += " or ";
            strSQL += " (result.QC_TYPE='3' and sample.QC_TYPE='3'";
            strSQL += " and EXISTS (SELECT * FROM T_MIS_MONITOR_QC_TWIN WHERE RESULT_ID_TWIN2 = result.ID)";
            strSQL += " and sample.sample_code in (" + strSampleCode + "))";
            strSQL += " or ";
            strSQL += " (result.QC_TYPE='4' and sample.QC_TYPE='4'";
            strSQL += " and EXISTS (SELECT * FROM T_MIS_MONITOR_QC_TWIN WHERE RESULT_ID_TWIN2 = result.ID)";
            strSQL += " and sample.sample_code in (" + strSampleCode + "))";
            strSQL += ")";

            if (reportTestResult.TEST_TYPE_NAME.Length > 0)
            {
                strSQL += " AND point.MONITOR_ID ='" + reportTestResult.TEST_TYPE_NAME + "'";
            }

            strSQL += BuildWhereStatement(reportTestResult);

            //填充评价标准用，仅为等效噪声和倍频率噪声
            string strSQL1 = " FROM T_MIS_MONITOR_SAMPLE_INFO sample" +
                                   " INNER JOIN T_MIS_MONITOR_RESULT result ON  sample.ID= result.SAMPLE_ID  " +
                                   " left JOIN T_MIS_MONITOR_TASK_POINT point ON  sample.POINT_ID = point.ID  " +
                                   " left join T_MIS_MONITOR_TASK_ITEM task_item on point.ID=task_item.TASK_POINT_ID" +
                                   " INNER JOIN T_BASE_ITEM_INFO item ON  result.ITEM_ID = item.ID  " +
                                   " INNER JOIN T_BASE_MONITOR_TYPE_INFO type ON  point.MONITOR_ID = type.ID  " +
                                   " INNER JOIN T_MIS_MONITOR_TASK task ON  point.TASK_ID  = task.ID  " +
                                   " left join T_BASE_EVALUATION_CON_ITEM con on con.CONDITION_ID=task_item.CONDITION_ID and con.ITEM_ID=result.ITEM_ID" +
                                   " left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=result.ANALYSIS_METHOD_ID" +
                                   " left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT" +
                                   " WHERE LEN(task.CONTRACT_ID)>0 " +
                                 " AND sample.QC_TYPE ='0'" +
                                 " AND result.QC_TYPE ='0'";
            strSQL1 += BuildWhereStatement(reportTestResult);

            //根据监测结果类型进行格式区分
            string mContractType = reportTestResult.TEST_TYPE;

            switch (mContractType)
            {
                //【监测结果-废气】
                case "RESULT_AIR":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID)+'\r\n'+sample.SAMPLE_CODE AS 测点位置及样品编号, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置及样品编号", "", "监测项目", "监测结果");
                    break;
                //【监测结果-放射性】
                case "RESULT_EMISSIVE":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 放射源位置, " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColNew("放射源位置 as 放射源位置", strSQL, "放射源位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-电离辐射】
                case "RESULT_DLFS":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 测点位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-放射性】
                case "RESULT_FS":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 测点位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废气-中型】
                case "RESULT_GAS_MEDIUM":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 污染源名称, " +
                            " item.ITEM_NAME + '（' +  dict.DICT_TEXT  + '）' AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToCol_verticalNew("监测项目 as 污染源名称", strSQL, "监测项目", "", "污染源名称", "监测结果");
                    break;
                //【监测结果-废气-小型】
                case "RESULT_GAS_SMALL":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 污染源名称, " +
                            " item.ITEM_NAME+ '（' +  dict.DICT_TEXT  + '）' AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToCol_verticalNew("监测项目 as 污染源名称", strSQL, "监测项目", "", "污染源名称", "监测结果");
                    break;
                //【监测结果-噪声】
                case "RESULT_NOISE":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 测点编号及位置, " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;
                    strSQL = FormatSqlForRowToColNew("测点编号及位置 as 测点编号及位置", strSQL, "测点编号及位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-振动】
                case "RESULT_QUAKE":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 测点编号及位置, " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColNew("测点编号及位置 as 测点编号及位置", strSQL, "测点编号及位置", ",'' as 评价结果", "监测项目", "监测结果");
                    break;
                //【监测结果-室内空气质量】
                case "RESULT_ROOM":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 样品编号, " +
                            " item.ITEM_NAME + '（' +  dict.DICT_TEXT  + '）' AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColNew("样品编号 as 样品编号", strSQL, "样品编号", "", "监测项目", "监测结果");
                    break;
                //【监测结果-固体】
                case "RESULT_SOLID":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废水-不换行】
                case "RESULT_WATER_THIN":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME + case when len(dict.DICT_TEXT)>0 then case when charindex('mg/L',dict.DICT_TEXT)=0 then  '('+dict.DICT_TEXT+')' else '' end else '' end AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废水-换行】
                case "RESULT_WATER_WIDTH":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                        //" TEST_ITEM_INFO.NAME + case when charindex('mg/L',DATA_DICT.DICT_TEXT)>0 then '('+DATA_DICT.DICT_TEXT+')' else '' end AS 监测项目,   " +
                            " item.ITEM_NAME + case when len(dict.DICT_TEXT)>0 then case when charindex('mg/L',dict.DICT_TEXT)=0 then  '('+dict.DICT_TEXT+')' else '' end else '' end AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " point.NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废气-大型】
                case "RESULT_GAS_HUGE":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 窑炉名称, " +
                            " item.NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL;

                    strSQL = FormatSqlForRowToColNew("窑炉名称", strSQL, "窑炉名称", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废水-常规-换行】
                case "G_RESULT_WATER_WIDTH":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL + " AND item.ITEM_OF_AGENCY='1' ";

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废水-常规-不换行】
                case "G_RESULT_WATER_THIN":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL + " AND item.ITEM_OF_AGENCY='1' ";

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-废水-常规-竖表】
                case "G_RESULT_WATER_VERTICAL":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 采样位置, " +
                            " item.NAME AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL + " AND item.ITEM_OF_AGENCY='1' ";

                    strSQL = FormatSqlForRowToCol_verticalNew("监测项目 as 采样位置", strSQL, "监测项目", "", "采样位置", "监测结果");
                    break;
                //【监测结果-废气-常规-小型】
                case "G_RESULT_GAS_SMALL":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 污染源名称, " +
                            " item.NAME+ '（' +  DATA_DICT.DICT_TEXT  + '）' AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL + " AND item.ITEM_OF_AGENCY='1' ";

                    strSQL = FormatSqlForRowToCol_verticalNew("监测项目 as 污染源名称", strSQL, "监测项目", "", "污染源名称", "监测结果");
                    break;
                //【监测结果-废气-常规-中型】
                case "G_RESULT_GAS_MEDIUM":
                    strSQL =
                        " SELECT " +
                            " dbo.Report_GetOutletAndPoinBySampleID(sample.ID) AS 污染源名称, " +
                            " item.NAME + '（' +  DATA_DICT.DICT_TEXT  + '）' AS 监测项目,   " +
                            " item.ORDER_NUMBER AS ORDER_NUMBER,   " +
                            " BASE_OUTLET_INFO.ORDER_NUM AS ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 " + strSQL + " AND item.ITEM_OF_AGENCY='1' ";

                    strSQL = FormatSqlForRowToCol_verticalNew("监测项目 as 污染源名称", strSQL, "监测项目", "", "污染源名称", "监测结果");
                    break;


                default:
                    break;
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        private string FormatSqlForRowToCol(string strSel, String strSql, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            //string strSQL = "exec ChangeTableRowToCol '" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strJudge.Replace("'", "''")  + "'";
            string strSQL = "exec ChangeTableRowToCol '" + strSel + "','" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToCol_vertical(string strSel, String strSql, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            //string strSQL = "exec ChangeTableRowToCol '" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strJudge.Replace("'", "''")  + "'";
            string strSQL = "exec ChangeTableRowToCol_vertical '" + strSel + "','" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToColEx(String strSql, string strGroupBy, string strGroupByEx, string strJudge, string strItemName, string strItemValueName)
        {
            string strSQL = "exec ChangeTableRowToColExNew '" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strGroupByEx + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToColNew(string strSel, String strSql, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            string strSQL = "exec ChangeTableRowToColNew '" + strSel + "','" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToCol_verticalNew(string strSel, String strSql, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            string strSQL = "exec ChangeTableRowToCol_verticalNew '" + strSel + "','" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToColExNew(String strSql, string strGroupBy, string strGroupByEx, string strJudge, string strItemName, string strItemValueName)
        {
            //string strSQL = "exec ChangeTableRowToColEx '" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strJudge.Replace("'", "''") + "'";
            string strSQL = "exec ChangeTableRowToColEx '" + strSql.Substring(0, strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strSql.Substring(strSql.IndexOf("FROM")).Replace("'", "''") + "','" + strGroupBy + "','" + strGroupByEx + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        /// <summary>
        /// 取得样品号、排口、性状
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="mItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater_ST(string strTaskID, string mItemTypeID)
        {
            string strSqlSub = @"SELECT distinct point.MONITOR_ID,point.POINT_NAME,point.CONTRACT_POINT_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,attrbute_type.SORT_NAME as SN,sample.QC_TYPE,
                                             case when attrbute_info.CONTROL_NAME='dropdownlist' then dd.DICT_TEXT else attrbute_value.ATTRBUTE_VALUE end as ATTRBUTE_VALUE 
                                             FROM T_MIS_MONITOR_SAMPLE_INFO sample
                                             join T_MIS_MONITOR_TASK_POINT point on point.ID=sample.POINT_ID 
                                             left join T_MIS_MONITOR_TASK_ITEM task_item on point.ID=task_item.TASK_POINT_ID
                                             left join T_BASE_ATTRBUTE_VALUE3 attrbute_value on attrbute_value.IS_DEL='0' and attrbute_value.[OBJECT_ID]=sample.POINT_ID 
                                             left join T_BASE_ATTRIBUTE_INFO attrbute_info on attrbute_value.ATTRBUTE_CODE=attrbute_info.ID 
                                             left join T_BASE_ATTRIBUTE_TYPE_VALUE type_value on type_value.ITEM_TYPE=point.MONITOR_ID and type_value.ATTRIBUTE_ID=attrbute_info.ID and type_value.IS_DEL='0'
                                             left join T_BASE_ATTRIBUTE_TYPE attrbute_type on attrbute_type.MONITOR_ID=point.MONITOR_ID and attrbute_type.IS_DEL='0' and attrbute_type.ID=type_value.ATTRIBUTE_TYPE_ID
                                             left join T_SYS_DICT dd on dd.DICT_TYPE=attrbute_info.DICTIONARY and dd.DICT_CODE=attrbute_value.ATTRBUTE_VALUE
                                             WHERE  sample.POINT_ID IN
                                            (SELECT ID FROM T_MIS_MONITOR_TASK_POINT WHERE  TASK_ID='{0}' {1} )";
            if (!string.IsNullOrEmpty(mItemTypeID))
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", mItemTypeID));
            }
            else
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, "");
            }

            string strsql = "select distinct MONITOR_ID,point_name,CONTRACT_POINT_ID,sample_code,sample_name," +
                "STUFF((" +
                " SELECT '、' + ATTRBUTE_VALUE FROM ({0}) t1" +
                " WHERE   point_name = t.point_name and  SAMPLE_CODE=t.SAMPLE_CODE " +
                " order by SN FOR XML PATH('')   )," +
                "1, 1, '') AS ATTRBUTE_VALUE,QC_TYPE" +
                " from ({0}) t order by MONITOR_ID,sample_code";
            strsql = string.Format(strsql, strSqlSub);

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strsql, 0, 0));
        }

        /// <summary>
        /// 取得样品号、排口、性状 （清远）Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="mItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <param name="strAttType">性状的类型的ID</param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater(string strTaskID, string mItemTypeID, string strAttType)
        {
            string strSqlSub = @"SELECT distinct point.MONITOR_ID,point.POINT_NAME,point.CONTRACT_POINT_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,attrbute_type.SORT_NAME as SN,sample.QC_TYPE,
                                             case when attrbute_info.CONTROL_NAME='dropdownlist' then dd.DICT_TEXT else attrbute_value.ATTRBUTE_VALUE end as ATTRBUTE_VALUE,sample.SAMPLE_ACCEPT_DATEORACC 
                                             FROM T_MIS_MONITOR_SAMPLE_INFO sample
                                             join T_MIS_MONITOR_TASK_POINT point on point.ID=sample.POINT_ID 
                                             left join T_MIS_MONITOR_TASK_ITEM task_item on point.ID=task_item.TASK_POINT_ID
                                             left join T_BASE_ATTRBUTE_VALUE3 attrbute_value on attrbute_value.IS_DEL='0' and attrbute_value.[OBJECT_ID]=sample.POINT_ID 
                                             left join T_BASE_ATTRIBUTE_INFO attrbute_info on attrbute_value.ATTRBUTE_CODE=attrbute_info.ID 
                                             left join T_BASE_ATTRIBUTE_TYPE_VALUE type_value on type_value.ITEM_TYPE=point.MONITOR_ID and type_value.ATTRIBUTE_ID=attrbute_info.ID and type_value.IS_DEL='0'
                                             left join T_BASE_ATTRIBUTE_TYPE attrbute_type on attrbute_type.MONITOR_ID=point.MONITOR_ID and attrbute_type.IS_DEL='0' and attrbute_type.ID=type_value.ATTRIBUTE_TYPE_ID
                                             left join T_SYS_DICT dd on dd.DICT_TYPE=attrbute_info.DICTIONARY and dd.DICT_CODE=attrbute_value.ATTRBUTE_VALUE
                                             WHERE  sample.POINT_ID IN
                                            (SELECT ID FROM T_MIS_MONITOR_TASK_POINT WHERE  TASK_ID='{0}' {1} ) ";
            if (!string.IsNullOrEmpty(mItemTypeID))
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", mItemTypeID));
            }
            else
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, "");
            }

            string strSqlSub1 = strSqlSub + " and type_value.Attribute_type_id='" + strAttType + "' ";

            string strsql = "select distinct MONITOR_ID,point_name,CONTRACT_POINT_ID,sample_code,sample_name,SAMPLE_ACCEPT_DATEORACC," +
                "STUFF((" +
                " SELECT '、' + ATTRBUTE_VALUE FROM ({1}) t1" +
                " WHERE   point_name = t.point_name and  SAMPLE_CODE=t.SAMPLE_CODE " +
                " order by SN FOR XML PATH('')   )," +
                "1, 1, '') AS ATTRBUTE_VALUE,QC_TYPE" +
                " from ({0}) t order by MONITOR_ID,sample_code";
            strsql = string.Format(strsql, strSqlSub, strSqlSub1);

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strsql, 0, 0));
        }

        /// <summary>
        /// 取得监测参数
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="mItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <returns></returns>
        public DataTable SelAttribute(string strTaskID, string mItemTypeID, string strOtherWhere)
        {
            string strsql = @"SELECT distinct point.id,point.MONITOR_ID,point.POINT_NAME,sample.SAMPLE_CODE,sample.SAMPLE_NAME,attrbute_type.SORT_NAME as SN,type_value.SN as SN1,
                                             attrbute_info.ATTRIBUTE_NAME,
                                             case when attrbute_info.CONTROL_NAME='dropdownlist' then dd.DICT_TEXT else attrbute_value.ATTRBUTE_VALUE end as ATTRBUTE_VALUE 
                                             FROM T_MIS_MONITOR_SAMPLE_INFO sample
                                             join T_MIS_MONITOR_TASK_POINT point on point.ID=sample.POINT_ID 
                                             left join T_MIS_MONITOR_TASK_ITEM task_item on point.ID=task_item.TASK_POINT_ID
                                             left join T_BASE_ATTRBUTE_VALUE3 attrbute_value on attrbute_value.IS_DEL='0' and attrbute_value.[OBJECT_ID]=sample.POINT_ID 
                                             left join T_BASE_ATTRIBUTE_INFO attrbute_info on attrbute_value.ATTRBUTE_CODE=attrbute_info.ID 
                                             left join T_BASE_ATTRIBUTE_TYPE_VALUE type_value on type_value.ITEM_TYPE=point.MONITOR_ID and type_value.ATTRIBUTE_ID=attrbute_info.ID and type_value.IS_DEL='0' and type_value.ATTRIBUTE_TYPE_ID=point.DYNAMIC_ATTRIBUTE_ID 
                                             left join T_BASE_ATTRIBUTE_TYPE attrbute_type on attrbute_type.MONITOR_ID=point.MONITOR_ID and attrbute_type.IS_DEL='0' and attrbute_type.ID=type_value.ATTRIBUTE_TYPE_ID
                                             left join T_SYS_DICT dd on dd.DICT_TYPE=attrbute_info.DICTIONARY and dd.DICT_CODE=attrbute_value.ATTRBUTE_VALUE
                                             WHERE  sample.POINT_ID IN
                                            (SELECT ID FROM T_MIS_MONITOR_TASK_POINT WHERE  TASK_ID='{0}' {1} )";
            if (!string.IsNullOrEmpty(mItemTypeID))
            {
                strsql = string.Format(strsql, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", mItemTypeID));
            }
            else
            {
                strsql = string.Format(strsql, strTaskID, "");
            } 
            if (!string.IsNullOrEmpty(strOtherWhere))
            {
                strsql += " and " + strOtherWhere;
            }
            strsql += "  order by point.MONITOR_ID,point.id,type_value.SN";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strsql, 0, 0));
        }

        /// <summary>
        /// 取得样品号、排口、性状
        ///  潘德军2013-6-24增加“sample.SRC_CODEORNAME,sample.SAMPLE_STATUS,subtask.MONITOR_ID”
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater_ST_forSendSanple(string strTaskID, string mItemTypeID)
        {
            string strSqlSub = @"SELECT distinct sample.SAMPLE_CODE,sample.SAMPLE_NAME,sample.SAMPLE_ACCEPT_DATEORACC,sample.SRC_CODEORNAME,sample.SAMPLE_STATUS,subtask.MONITOR_ID 
                                             FROM T_MIS_MONITOR_SAMPLE_INFO sample
                                             left join T_MIS_MONITOR_RESULT result on sample.ID=result.SAMPLE_ID 
                                             left join T_MIS_MONITOR_SUBTASK subtask on subtask.ID=sample.SUBTASK_ID
                                             WHERE  sample.SUBTASK_ID IN
                                            (SELECT ID FROM T_MIS_MONITOR_SUBTASK WHERE TASK_ID='{0}' {1})";
            if (!string.IsNullOrEmpty(mItemTypeID))
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", mItemTypeID));
            }
            else
            {
                strSqlSub = string.Format(strSqlSub, strTaskID, "");
            }

            return SqlHelper.ExecuteDataTable(strSqlSub);
        }

        private string FormatSqlForRowToColByAttribute(string SelColumnName, String strSel, String strFrom, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            string strSQL = "exec ChangeTableRowToColForAttribute '" + SelColumnName + "','" + strSel.Replace("'", "''") + "','" + strFrom.Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        private string FormatSqlForRowToColByAttribute_vertical(string SelColumnName, String strSel, String strFrom, string strGroupBy, string strJudge, string strItemName, string strItemValueName)
        {
            string strSQL = "exec ChangeTableRowToColForAttribute_vertical '" + SelColumnName + "','" + strSel.Replace("'", "''") + "','" + strFrom.Replace("'", "''") + "','" + strGroupBy + "','" + strItemName + "','" + strItemValueName + "','" + strJudge.Replace("'", "''") + "'";

            return strSQL;
        }

        #endregion

        #region 新方法
        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// 修改时间：2013-5-19
        /// 修改人：潘德军
        /// 修改内容：排序及去除重复项
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select distinct monitor.MONITOR_TYPE_NAME as 监测类别,
                                               case when monitor.ID='000000004' and item.ITEM_NAME like '%环境%' then '环境噪声'
                                                when monitor.ID='000000004' and item.ITEM_NAME like '%建筑%' then '建筑施工场界噪声'
                                                when monitor.ID='000000004' and item.ITEM_NAME like '%铁路%' then '铁路边界噪声'
                                                when monitor.ID='000000004' and item.ITEM_NAME like '%声源%' then '声源噪声'
                                                when monitor.ID='000000004' then '厂界噪声' 
                                                when monitor.ID='000000002' and item.ITEM_NAME like '%尘%' then '烟（粉）尘、烟气参数' else item.ITEM_NAME end as 监测项目,
                                               (analysis.ANALYSIS_NAME+' ' +method.METHOD_CODE) as 监测方法,
                                               case when len(result.RESULT_CHECKOUT)>0 then (result.RESULT_CHECKOUT+'('+dict.DICT_TEXT+')') else '/' end as 检出限,ISNULL(apparatus.MODEL,'')+' ' +ISNULL(apparatus.NAME,'') as 仪器
                                                ,monitor.SORT_NUM--,item.ORDER_NUM
                                                from T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID  in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1})) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_MONITOR_TYPE_INFO monitor on monitor.IS_DEL='0' and monitor.ID=item.MONITOR_ID
                                                    left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ID=result.ANALYSIS_METHOD_ID
                                                    left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and item_analysis.ANALYSIS_METHOD_ID= analysis.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and analysis.METHOD_ID=method.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                                    left join T_BASE_APPARATUS_INFO apparatus on apparatus.Id=item_analysis.INSTRUMENT_ID";
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            strSQL = "select 监测类别,监测项目,监测方法,检出限,仪器 from (" + strSQL + ")t order by SORT_NUM--,ORDER_NUM";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测项目及是否现场监测项目
        /// 创建时间：2013-4-28
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport_andIsSampleDept(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select monitor.MONITOR_TYPE_NAME as 监测类别,item.ITEM_NAME as 监测项目,(analysis.ANALYSIS_NAME+' ' +method.METHOD_CODE) as 监测方法,
                                               case when len(item_analysis.LOWER_CHECKOUT)>0 then (item_analysis.LOWER_CHECKOUT+'('+dict.DICT_TEXT+')') else '/' end as 检出限,apparatus.NAME+' ' +apparatus.MODEL as 仪器,
                                                item.IS_SAMPLEDEPT 
                                                from T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID  in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1})) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_MONITOR_TYPE_INFO monitor on monitor.IS_DEL='0' and monitor.ID=item.MONITOR_ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                                    left join T_BASE_APPARATUS_INFO apparatus on apparatus.Id=item_analysis.INSTRUMENT_ID";
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReportQHD(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select distinct '' as 序号,item.MONITOR_ID,item.ITEM_NAME as 项目名称,(analysis.ANALYSIS_NAME+method.METHOD_CODE) as 分析方法及方法依据,
                                                (item_analysis.LOWER_CHECKOUT+dict.DICT_TEXT) as 检出限,(apparatus.NAME+'（'+APPARATUS_CODE+'）') as 仪器设备名称及编码
                                                from T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID 
                                                in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1})) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                                    left join T_BASE_APPARATUS_INFO apparatus on apparatus.IS_DEL='0' and item_analysis.INSTRUMENT_ID=apparatus.ID
                                                                        order by item.MONITOR_ID";
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and MONITOR_ID='{0}'", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测报告结果
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult(string strTaskID, string strItemTypeID, string mContractType)
        {
            string strSQL = @"FROM T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID 
                                                in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1})  and QC_TYPE='0') sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT";

            switch (mContractType)
            {
                //废气
                case "RESULT_AIR":
                    strSQL =
                         string.Format(" SELECT " +
                             " ISNULL(dbo.Rpt_GetPointAddress_UNDER_Task({0},{1})+'\r\n'+sample.SAMPLE_CODE,'') AS 测点位置及样品编号, " +
                             " sample.SAMPLE_CODE AS 样品编号,   " +
                             " item.ITEM_NAME AS 监测项目,   " +
                             " item.ORDER_NUM as ORDER_NUMBER,   " +
                             " ORDER_NUM,    " +
                             " result.ITEM_RESULT AS 监测结果 ", strTaskID, strItemTypeID) + strSQL;
                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置及样品编号", "", "监测项目", "监测结果");
                    break;
                //【监测结果-放射性】
                case "RESULT_EMISSIVE":
                    strSQL =
                        string.Format(" SELECT " +
                           " dbo.Rpt_GetPointAddress_UNDER_Task({0},{1}) AS 放射源位置, " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUM AS ORDER_NUMBER,   " +
                            " ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 ", strTaskID, strItemTypeID) + strSQL;

                    strSQL = FormatSqlForRowToColNew("放射源位置 as 放射源位置", strSQL, "放射源位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-电离辐射】
                case "RESULT_DLFS":
                    strSQL =
                        string.Format(" SELECT " +
                            " dbo.Rpt_GetPointAddress_UNDER_Task({0},{1}) AS 测点位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUM AS ORDER_NUMBER,   " +
                            " ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 ", strTaskID, strItemTypeID) + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置", "", "监测项目", "监测结果");
                    break;
                //【监测结果-放射性】
                case "RESULT_FS":
                    strSQL =
                        string.Format(" SELECT " +
                            " dbo.Rpt_GetPointAddress_UNDER_Task({0},{1}) AS 测点位置, " +
                            " sample.SAMPLE_CODE AS 样品编号,   " +
                            " item.ITEM_NAME AS 监测项目,   " +
                            " item.ORDER_NUM AS ORDER_NUMBER,   " +
                            " ORDER_NUM,    " +
                            " result.ITEM_RESULT AS 监测结果 ", strTaskID, strItemTypeID) + strSQL;

                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "测点位置", "", "监测项目", "监测结果");
                    break;
                default:
                    strSQL = "select '采样位置' as 采样位置," +
                                        " sample.SAMPLE_CODE as 样品编号," +
                                        "item.ITEM_NAME as 监测项目," +
                                        "item.ORDER_NUM as ORDER_NUMBER," +
                                        "ORDER_NUM,result.ITEM_RESULT as 监测结果" + strSQL;
                    strSQL = FormatSqlForRowToColExNew(strSQL, "样品编号", "采样位置", "", "监测项目", "监测结果");
                    break;
            }
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and MONITOR_ID=''{0}''", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResultForQHD(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"FROM T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID 
                                                in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1})) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT";
            strSQL = "select '采样位置' as 采样位置," +
                    " sample.SAMPLE_NAME as 样品名称," +
                    "item.ITEM_NAME as 监测项目," +
                    "item.ORDER_NUM as ORDER_NUMBER," +
                    "ORDER_NUM,result.ITEM_RESULT as 监测结果" + strSQL;
            strSQL = FormatSqlForRowToColExNew(strSQL, "样品名称", "采样位置", "", "监测项目", "监测结果");
            if (!string.IsNullOrEmpty(strItemTypeID))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and MONITOR_ID='{0}'", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 功能描述：获得许可证监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <returns></returns>
        public DataTable getSampleResultForLicense(string strTaskID)
        {
            string strSQL = @" FROM T_MIS_MONITOR_RESULT result
                                                join (select sample_info.*,subtask.SAMPLE_FINISH_DATE,subtask.MONITOR_ID
                                                from T_MIS_MONITOR_SAMPLE_INFO sample_info 
                                                inner join T_MIS_MONITOR_SUBTASK subtask on subtask.TASK_ID='{0}' and sample_info.SUBTASK_ID=subtask.ID) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                left join T_MIS_MONITOR_TASK_ITEM task_item on task_item.TASK_POINT_ID=sample.POINT_ID and task_item.ITEM_ID=item.ID
                                                left join T_BASE_EVALUATION_CON_INFO con_info on con_info.IS_DEL='0' and con_info.ID=task_item.CONDITION_ID
                                                left join T_BASE_EVALUATION_INFO evaluation on evaluation.IS_DEL='0' and evaluation.ID =con_info.STANDARD_ID order by 监测类别,监测点位";
            strSQL = string.Format(strSQL, strTaskID);
            strSQL = @"select sample.SAMPLE_NAME as 监测点位,
                                    sample.POINT_ID,
                                    convert(nvarchar(32),sample.SAMPLE_FINISH_DATE,23) as 监测时间,
                                    item.ITEM_NAME as 监测项目,
                                    sample.MONITOR_ID as 监测类别,
                                    dict.DICT_TEXT as 单位,
                                    result.ITEM_RESULT as 监测结果,(evaluation.STANDARD_CODE) as 执行标准号,
                                    (task_item.ST_UPPER+';'+task_item.ST_LOWER) as 标准值" + strSQL;
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 功能描述：获得验收监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResultForAcceptance(string strTaskID)
        {
            string strSQL = @" FROM T_MIS_MONITOR_RESULT result
                                                join (select sample_info.*,subtask.SAMPLE_FINISH_DATE,subtask.MONITOR_ID
                                                from T_MIS_MONITOR_SAMPLE_INFO sample_info 
                                                inner join T_MIS_MONITOR_SUBTASK subtask on subtask.TASK_ID='{0}' and sample_info.SUBTASK_ID=subtask.ID) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                left join T_MIS_MONITOR_TASK_ITEM task_item on task_item.TASK_POINT_ID=sample.POINT_ID and task_item.ITEM_ID=item.ID
                                                left join T_BASE_EVALUATION_CON_INFO con_info on con_info.IS_DEL='0' and con_info.ID=task_item.CONDITION_ID
                                                left join T_BASE_EVALUATION_INFO evaluation on evaluation.IS_DEL='0' and evaluation.ID =con_info.STANDARD_ID order by 监测类别,监测点位,item.ORDER_NUM";
            strSQL = string.Format(strSQL, strTaskID);
            strSQL = @"select sample.SAMPLE_NAME as 监测点位,
                                    sample.POINT_ID,
                                    convert(nvarchar(32),sample.SAMPLE_FINISH_DATE,23) as 监测日期,
                                    item.ITEM_NAME as 监测项目,
                                    sample.MONITOR_ID as 监测类别,
                                    dict.DICT_TEXT as 单位,
                                    result.ITEM_RESULT as 监测结果,
                                    (evaluation.STANDARD_CODE)+'    '+(task_item.ST_UPPER+';'+task_item.ST_LOWER) as 执行标准标准值" + strSQL;
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #endregion

        #region 2.0方法
        /// <summary>
        /// 功能描述：获得监测报告结果，为2.0制作，不再使用行列转换函数，需要的数据全部取出，在页面linQ
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_ForV2(string strTaskID, string strItemTypeID, string strWhere)
        {
            if (strItemTypeID.Contains(";"))
            {
                strItemTypeID = strItemTypeID.Replace(";", "','");
            }
            string strSQL = @"select t.MONITOR_ID,t.SAMPLE_FINISH_DATE,
                                r.SAMPLE_ID,s.POINT_ID,s.SAMPLE_CODE,s.SAMPLE_NAME,s.SPECIALREMARK as SAMPLE_REMARK,
                                r.ITEM_ID,i.ITEM_NAME,dict.DICT_TEXT as ITEM_UNIT,r.ITEM_RESULT,s.REMARK5 as EMISSIONS,r.ID as RESULT_ID,r.REMARK_5 as PA
                            from T_MIS_MONITOR_RESULT r
                                join T_BASE_ITEM_INFO i on i.Id=r.ITEM_ID
                                join T_MIS_MONITOR_SAMPLE_INFO s on s.id=r.SAMPLE_ID
                                join T_MIS_MONITOR_SUBTASK t on t.id=s.SUBTASK_ID
                                left join T_BASE_ITEM_ANALYSIS ia on ia.IS_DEL='0' and ia.ITEM_ID=i.ID and ia.ID=r.ANALYSIS_METHOD_ID
                                left join T_BASE_METHOD_ANALYSIS a on a.IS_DEL='0' and ia.ANALYSIS_METHOD_ID= a.ID 
                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=ia.UNIT
                            where t.TASK_ID='{0}' and t.MONITOR_ID in ('{1}') and s.QC_type='0' and {2}
                            order by t.MONITOR_ID,t.SAMPLE_FINISH_DATE,s.SAMPLE_CODE,i.ORDER_NUM";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID, strWhere);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 功能描述：获得监测报告结果(原始记录表结果)，为2.0制作
        /// 创建时间：2014-9-24
        /// 创建人：魏林
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_Dustinfor(string strTaskID, string strItemTypeID, string strWhere)
        {
            if (strItemTypeID.Contains(";"))
            {
                strItemTypeID = strItemTypeID.Replace(";", "','");
            }
            string strSQL = @"select RESULT.MONITOR_ID,RESULT.SAMPLE_FINISH_DATE,RESULT.SAMPLE_ID,RESULT.POINT_ID,RESULT.SAMPLE_REMARK,RESULT.ITEM_ID,RESULT.ITEM_NAME,
                                     RESULT.ITEM_UNIT,RESULT.RESULT_ID,RESULT.PA,PM.FITER_CODE SAMPLE_CODE,PM.SAMPLE_CONCENT ITEM_RESULT,PM.FQPFL,PM.ID,
                                     (case isnull(RESULT.PA,'') when 'Air' then PM.SAMPLE_POINT else DUSTINFOR.POSITION end) SAMPLE_NAME
                                     from
                            (select t.MONITOR_ID,t.SAMPLE_FINISH_DATE,
                                r.SAMPLE_ID,s.POINT_ID,s.SAMPLE_CODE,s.SAMPLE_NAME,s.SPECIALREMARK as SAMPLE_REMARK,
                                r.ITEM_ID,i.ITEM_NAME,dict.DICT_TEXT as ITEM_UNIT,r.ITEM_RESULT,s.REMARK5 as EMISSIONS,r.ID as RESULT_ID,r.REMARK_5 as PA,i.ORDER_NUM
                            from T_MIS_MONITOR_RESULT r
                                join T_BASE_ITEM_INFO i on i.Id=r.ITEM_ID
                                join T_MIS_MONITOR_SAMPLE_INFO s on s.id=r.SAMPLE_ID
                                join T_MIS_MONITOR_SUBTASK t on t.id=s.SUBTASK_ID
                                left join T_BASE_ITEM_ANALYSIS ia on ia.IS_DEL='0' and ia.ITEM_ID=i.ID and ia.ID=r.ANALYSIS_METHOD_ID
                                left join T_BASE_METHOD_ANALYSIS a on a.IS_DEL='0' and ia.ANALYSIS_METHOD_ID= a.ID 
                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=ia.UNIT
                            where t.TASK_ID='{0}' and t.MONITOR_ID in ('{1}') and s.QC_type='0' and {2}
                            ) RESULT
                            inner join T_MIS_MONITOR_DUSTINFOR DUSTINFOR on(RESULT.RESULT_ID=DUSTINFOR.SUBTASK_ID)
                            left join T_MIS_MONITOR_DUSTATTRIBUTE_PM PM on(DUSTINFOR.ID=pm.BASEINFOR_ID)
                            order by SAMPLE_NAME,POINT_ID,RESULT.ITEM_ID,PM.ID";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID, strWhere);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 2.0方法
        /// <summary>
        /// 功能描述：获得监测报告结果，为2.0制作，不再使用行列转换函数，需要的数据全部取出，在页面linQ
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// 修改人：weilin 2014-04-15
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_ForV2Ex(string strTaskID, string strItemTypeID)
        {
            if (strItemTypeID.Contains(";"))
            {
                strItemTypeID = strItemTypeID.Replace(";", "','");
            }
            string strSQL = @"select t.MONITOR_ID,t.SAMPLE_FINISH_DATE,
                                r.SAMPLE_ID,s.POINT_ID,s.SAMPLE_CODE,s.SAMPLE_NAME,s.SPECIALREMARK as SAMPLE_REMARK,
                                r.ITEM_ID,i.ITEM_NAME,dict.DICT_TEXT as ITEM_UNIT,r.ITEM_RESULT,s.D_SOURCE,s.N_SOURCE
                            from T_MIS_MONITOR_RESULT r
                                join T_BASE_ITEM_INFO i on i.Id=r.ITEM_ID
                                join T_MIS_MONITOR_SAMPLE_INFO s on s.id=r.SAMPLE_ID
                                join T_MIS_MONITOR_SUBTASK t on t.id=s.SUBTASK_ID
                                left join T_BASE_METHOD_ANALYSIS a on a.IS_DEL='0' and r.ANALYSIS_METHOD_ID= a.ID 
                                left join T_BASE_ITEM_ANALYSIS ia on ia.IS_DEL='0' and ia.ITEM_ID=i.ID and ia.ANALYSIS_METHOD_ID=a.ID
                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=ia.UNIT
                            where t.TASK_ID='{0}' and t.MONITOR_ID in ('{1}') and s.QC_type='0'
                            order by t.MONITOR_ID,t.SAMPLE_FINISH_DATE,s.SAMPLE_CODE,i.ORDER_NUM";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件
        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="监测项目信息"></param>
        /// <returns></returns>
        public string BuildWhereStatement(ReportTestResultVo reportTestResult)
        {
            string strSQL = "";

            //监测类别编号（暂时去掉对监测类别的限制）
            //if (reportTestResult.TEST_TYPE.Trim() != "")
            //{
            //    strSQL += string.Format(" AND CONTRACT_INFO.TEST_TYPE = '{0}'", reportTestResult.TEST_TYPE.Trim());
            //}
            //监测类别名称
            //if (reportTestResult.TEST_TYPE_NAME.Trim() != "")
            //{
            //    strSQL += string.Format(" AND TEST_TYPE_NAME = '{0}'", reportTestResult.TEST_TYPE_NAME.Trim());
            //}
            //点位编号
            if (reportTestResult.POINT_ID.Trim() != "")
            {
                strSQL += string.Format(" AND POINT_ID = '{0}'", reportTestResult.POINT_ID.Trim());
            }
            //点位名称
            if (reportTestResult.POINT_NAME.Trim() != "")
            {
                strSQL += string.Format(" AND POINT_NAME = '{0}'", reportTestResult.POINT_NAME.Trim());
            }
            //样品编号
            if (reportTestResult.SAMPLE_ID.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_ID = '{0}'", reportTestResult.SAMPLE_ID.Trim());
            }
            //样品名称
            if (reportTestResult.SAMPLE_NAME.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_NAME = '{0}'", reportTestResult.SAMPLE_NAME.Trim());
            }
            //监测项目
            if (reportTestResult.TEST_ITEM.Trim() != "")
            {
                strSQL += string.Format(" AND TEST_ITEM = '{0}'", reportTestResult.TEST_ITEM.Trim());
            }
            //监测结果
            if (reportTestResult.TEST_RESULT.Trim() != "")
            {
                strSQL += string.Format(" AND TEST_RESULT = '{0}'", reportTestResult.TEST_RESULT.Trim());
            }
            //备注1
            if (reportTestResult.REMARK1.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK1 = '{0}'", reportTestResult.REMARK1.Trim());
            }
            //备注2
            if (reportTestResult.REMARK2.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK2 = '{0}'", reportTestResult.REMARK2.Trim());
            }
            //备注3
            if (reportTestResult.REMARK3.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK3 = '{0}'", reportTestResult.REMARK3.Trim());
            }

            return strSQL;
        }
        #endregion

        public DataTable getItemByTaskID(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select d.* from 
                            T_MIS_MONITOR_SUBTASK a 
                            left join T_MIS_MONITOR_SAMPLE_INFO b on(b.SUBTASK_ID=a.ID)
                            left join T_MIS_MONITOR_RESULT c on(c.SAMPLE_ID=b.ID)
                            left join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                            where a.TASK_ID='{0}' and a.MONITOR_ID='{1}'";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取任务中油烟的信息 Create By:weilin 2014-06-26
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <returns></returns>
        public DataTable SelAttribute_YY(string strTaskID)
        {
            string strsql = @"select c.ID,a.MONITOR_ID,c.POINT_NAME,b.SAMPLE_CODE,b.SAMPLE_NAME,d.ITEM_ID,e.ITEM_NAME,d.ID RESULT_ID 
                            from T_MIS_MONITOR_SUBTASK a
                            left join T_MIS_MONITOR_SAMPLE_INFO b on(b.SUBTASK_ID=a.ID)
                            inner join T_MIS_MONITOR_TASK_POINT c on(b.POINT_ID=c.ID)
                            left join T_MIS_MONITOR_RESULT d on(d.SAMPLE_ID=b.ID)
                            left join T_BASE_ITEM_INFO e on(d.ITEM_ID=e.ID)
                            where a.TASK_ID='{0}' and e.ITEM_NAME like '%油烟%'";

            strsql = string.Format(strsql, strTaskID);
            
            return SqlHelper.ExecuteDataTable(strsql);
        }
    }
}
