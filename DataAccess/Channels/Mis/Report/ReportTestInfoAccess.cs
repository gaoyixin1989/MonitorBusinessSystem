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
    /// 功能：监测情况查询
    /// 创建日期：2012-12-8
    /// 创建人：邵世卓
    /// 修改日期：2013-4-26
    /// 修改人：潘德军
    /// 修改内容：适应可以多选监测类别的报告生成
    /// </summary>
    public class ReportTestInfoAccess : SqlHelper
    {
        #region  监测任务信息
        /// <summary>
        /// 获得报告首页信息
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <returns>数据集</returns>
        public DataTable getMonitorTaskInfo(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select (select DICT_TEXT from T_SYS_DICT where DICT_CODE='city_code') as REPORT_NUM,
                                            task.CONTRACT_YEAR as REPORT_YEAR,
                                            task.PROJECT_NAME,(case when task.TEST_PURPOSE is not null then task.TEST_PURPOSE else '排污状况监测。' end) as TEST_PURPOSE,
                                            report.REPORT_CODE as REPORT_SERIAL,
                                            com1.COMPANY_NAME as CLIENT_COMPANY,com1.CONTACT_ADDRESS as COMMUNICATION_ADDRESS,
                                            com1.CONTACT_NAME as CUSTOMER_CLIENT,com1.PHONE as TEL_CLIENT,
                                            com2.COMPANY_NAME as CUSTOMER_NAME, com2.COMPANY_NAME as TESTED_COMPANY,com2.CONTACT_ADDRESS as CUSTOMER_ADDRESS,
                                            com2.CONTACT_NAME as CUSTOMER_TESTED,com2.PHONE as TEL_TESTED,
                                            dict2.DICT_TEXT as CONTRACT_TYPE,
                                            (select DICT_TEXT from T_SYS_DICT where DICT_CODE='station') as STATION_NAME,
                                            dbo.Rpt_GetMethod_UNDER_Task(task.ID,'{1}') as TASK_METHOD,
                                            dbo.Rpt_GetStandard_UNDER_Task(task.ID,'{1}') as TASK_STANDARD,
                                            '对排放执行标准如有异议，以环保管理部门核定为准。' as RESULT_INFO,
                                            dbo.Rpt_GetItem_UNDER_Taskt(task.ID,'{1}') as RRP_ITEM,
                                            dbo.Rpt_GetItem_UNDER_Taskt(task.ID,'{1}') as RRP_ITEM_IN_TABLE,
                                            dbo.Rpt_GetPointAddress_UNDER_Task(task.ID,'{1}') as OUTLET_AND_POINT,
                                            dbo.Rpt_GetApparatus_UNDER_Task(task.ID,'{1}') as TASK_MACHINE,
                                            dbo.Rpt_GetLimit_UNDER_Task(task.ID,'{1}') as LIMIT_AIR,
                                            dbo.Report_GetSampleUsers(task.ID,'{1}') as SAMPLE_USER_Ex,
                                            ' ' as RPT_WATER_REMARK,
                                            ISNULL(task.REMARK5,'无说明。') as RPT_RESILT,
                                            ISNULL(task.REMARK5,'无说明。')  as RPT_RESILT1,
                                            task.SAMPLE_SOURCE,
                                            (com1.COMPANY_NAME + dict2.DICT_TEXT) as TASK_SOURCE,
                                            dbo.Rpt_GetAllType_UNDER_Taskt(task.ID) as CONTRACT_INFO,
                                            task.SAMPLE_SEND_MAN,task.CREATE_DATE,task.CONTRACT_TYPE CONTRACT_TYPE_CODE
                                            from T_MIS_MONITOR_TASK task 
                                            join T_MIS_MONITOR_REPORT report on report.TASK_ID = task.ID
                                            left join T_MIS_MONITOR_TASK_COMPANY com1 on  com1.ID=task.CLIENT_COMPANY_ID
                                            left join T_MIS_MONITOR_TASK_COMPANY com2 on  com2.ID=task.TESTED_COMPANY_ID
                                            left join T_SYS_DICT dict2 on dict2.DICT_TYPE='Contract_Type' and dict2.DICT_CODE=task.CONTRACT_TYPE
                                            where task.ID='{0}'";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得天气情况
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <param name="strWeatherType">通用还是噪声</param>
        /// <returns></returns>
        public DataTable getWeatherInfo(string strTaskID, string strItemTypeID, string strWeatherType)
        {
            string strSQL = @"select DICT_TEXT as name,sky.WEATHER_INFO as value from T_MIS_MONITOR_SAMPLE_SKY sky 
                                        left join T_SYS_DICT dict on dict.DICT_TYPE='{2}' and sky.WEATHER_ITEM=dict.DICT_CODE
                                        where sky.SUBTASK_ID
                                        in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and CHARINDEX(MONITOR_ID,'{1}')>0)";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID, strWeatherType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region 原函数，根据类别过滤 ,注释并备用
        /// <summary>
        /// 获得报告首页信息 
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getMonitorTaskInfo_Bak(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"select (select DICT_TEXT from T_SYS_DICT where DICT_CODE='city_code') as REPORT_NUM,task.CONTRACT_YEAR as REPORT_YEAR,
                                            task.PROJECT_NAME,(case when task.TEST_PURPOSE is not null then task.TEST_PURPOSE else '排污状况监测。' end) as TEST_PURPOSE,report.REPORT_CODE as REPORT_SERIAL,
                                            com1.COMPANY_NAME as CLIENT_COMPANY,com1.CONTACT_ADDRESS as COMMUNICATION_ADDRESS,
                                            com2.COMPANY_NAME as CUSTOMER_NAME, com2.COMPANY_NAME as TESTED_COMPANY,com2.CONTACT_ADDRESS as CUSTOMER_ADDRESS,
                                            dict2.DICT_TEXT as CONTRACT_TYPE,(select DICT_TEXT from T_SYS_DICT where DICT_CODE='station') as STATION_NAME,
                                            (select DICT_CODE from T_SYS_DICT where DICT_TYPE='noise_time' and DICT_TEXT='NOISE_DAY_TIME') as NOISE_DAY_TIME,
                                            (select DICT_CODE from T_SYS_DICT where DICT_TYPE='noise_time' and DICT_TEXT='NOISE_NIGHT_TIME') as NOISE_NIGHT_TIME,
                                            dbo.Rpt_GetMethod_UNDER_Task(task.ID,'{1}') as NOISE_METHOD,
                                            dbo.Rpt_GetStandard_UNDER_Task(task.ID,'{1}') as NOISE_STANDARD,
                                            '对排放执行标准如有异议，以环保管理部门核定为准。' as NOISE_RESULT_INFO,
                                            {2} as RRP_ITEM,
                                            {2} as RRP_ITEM_IN_TABLE,
                                            dbo.Rpt_GetPointAddress_UNDER_Task(task.ID,'{1}') as OUTLET_AND_POINT,
                                            dbo.Rpt_GetApparatus_UNDER_Task(task.ID,'{1}') as MACHINE_AIR,
                                            dbo.Rpt_GetLimit_UNDER_Task(task.ID,'{1}') as LIMIT_AIR,
                                            ' ' as RPT_WATER_REMARK,
                                            ISNULL(task .REMARK5,'无说明。') as RPT_RESILT,
                                             ISNULL(task .REMARK5,'无说明。')  as RPT_RESILT1,
                                            task.SAMPLE_SOURCE,
                                            (com1.COMPANY_NAME + dict2.DICT_TEXT) as TASK_SOURCE,
                                            dbo.Rpt_GetAllType_UNDER_Taskt(task.ID) as CONTRACT_INFO
                                            from T_MIS_MONITOR_TASK task 
                                            join T_MIS_MONITOR_REPORT report on report.TASK_ID = task.ID
                                            left join T_MIS_MONITOR_TASK_COMPANY com1 on com1.IS_DEL='0' and com1.ID=task.CLIENT_COMPANY_ID
                                            left join T_MIS_MONITOR_TASK_COMPANY com2 on com2.IS_DEL='0' and com2.ID=task.TESTED_COMPANY_ID
                                            left join T_SYS_DICT dict2 on dict2.DICT_TYPE='Contract_Type' and dict2.DICT_CODE=task.CONTRACT_TYPE
                                            where task.ID='{0}'";
            if (strItemTypeID.Length > 0 && strItemTypeID != "0")
            {
                strSQL = string.Format(strSQL, strTaskID, strItemTypeID, string.Format("dbo.Rpt_GetItem_UNDER_Taskt(task.ID,'{0}')", strItemTypeID));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, strItemTypeID, "dbo.Rpt_GetAllItem_UNDER_Taskt(task.ID)");
            }

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得天气情况
        /// 创建时间：2012-12-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strItemTypeID"></param>
        /// <param name="strWeatherType"></param>
        /// <returns></returns>
        public DataTable getWeatherInfo_bak(string strTaskID, string strItemTypeID, string strWeatherType)
        {
            string strSQL = @"select DICT_TEXT as name,sky.WEATHER_INFO as value from T_MIS_MONITOR_SAMPLE_SKY sky 
                                        left join T_SYS_DICT dict on dict.DICT_TYPE='{2}' and sky.WEATHER_ITEM=dict.DICT_CODE
                                        where sky.SUBTASK_ID
                                        in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and MONITOR_ID='{1}')";
            strSQL = string.Format(strSQL, strTaskID, strItemTypeID, strWeatherType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得样品、点位
        /// 创建时间“2012-12-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>arrlist</returns>
        public DataTable GetSampleInfoSourceByTask(string strTaskID)
        {
            string strSQL = "SELECT sample.SAMPLE_CODE,sample.ID,sample.SUBTASK_ID as SUBTASK_ID,";
            strSQL += "sample.POINT_ID,point.POINT_NAME,point.DESCRIPTION ";
            strSQL += " FROM T_MIS_MONITOR_SAMPLE_INFO sample";
            strSQL += " left join T_MIS_MONITOR_TASK_POINT point on point.ID = sample.POINT_ID";
            strSQL += " where sample.SUBTASK_ID in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}')";
            strSQL = string.Format(strSQL, strTaskID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得企业概况
        /// 创建时间“2013-1-17
        /// 创建人：邵世卓
        /// <param name="strTaskID">企业ID</param>
        /// <returns></returns>
        public DataTable getCompanyInfo(string strTestCompanyID)
        {
            string strSQL = @"SELECT company.COMPANY_NAME,company.COMPANY_LEVEL,company.COMPANY_MAN,company.COMPANY_CODE,
                                                company.CONTACT_ADDRESS as CUSTOMER_ADDRESS,company.CONTACT_NAME,company.AREA as AREA_CODE ,CONVERT(nvarchar(16),company.PRACTICE_DATE,23) as PRACTICE_DATE,
                                                company.POST,ISNULL(company.LINK_PHONE,company.PHONE) as CUSTOMER_TEL,dict1.DICT_TEXT as WATER_FOLLOW,company.WATER_FOLLOW as WATER_FOLLLOW_CODE,
                                                company.CHECK_TIME,company.ACCEPTANCE_TIME,company.STANDARD,company.MAIN_APPARATUS,company.APPARATUS_STATUS,company.MAIN_PROJECT,company.MAIN_GOOD,
                                                company.DESIGN_ANBILITY,company.ANBILITY as ACTIVE_ANBILITY,company.CONTRACT_PER,company.AVG_PER,company.WATER_COUNT,company.YEAR_TIME,
                                                (industry.INDUSTRY_NAME+'  '+industry.INDUSTRY_CODE) INDUSTRY ";
            strSQL += " FROM T_MIS_MONITOR_TASK_COMPANY company";
            strSQL += " left join T_BASE_INDUSTRY_INFO industry on company.INDUSTRY = industry.ID";
            strSQL += " left join T_SYS_DICT dict1 on dict1.DICT_TYPE='water_follow' and dict1.DICT_CODE=company.WATER_FOLLOW";
            strSQL += " where company.ID='{0}'";
            strSQL = string.Format(strSQL, strTestCompanyID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion
        #endregion

        #region 构造条件
        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="监测情况"></param>
        /// <returns></returns>
        public string BuildWhereStatement(ReportTestInfoVo reportTestInfo)
        {
            string strSQL = "";

            //ID
            if (reportTestInfo.ID.Trim() != "")
            {
                strSQL += string.Format(" AND ID = '{0}'", reportTestInfo.ID.Trim());
            }
            //页眉检测类型
            if (reportTestInfo.TEST_TYPE_HEADER.Trim() != "")
            {
                strSQL += string.Format(" AND TEST_TYPE_HEADER = '{0}'", reportTestInfo.TEST_TYPE_HEADER.Trim());
            }
            //检测类型
            if (reportTestInfo.TEST_TYPE.Trim() != "")
            {
                strSQL += string.Format(" AND TEST_TYPE = '{0}'", reportTestInfo.TEST_TYPE.Trim());
            }
            //页眉报告编号
            if (reportTestInfo.REPORT_SERIAL_HEADER.Trim() != "")
            {
                strSQL += string.Format(" AND REPORT_SERIAL_HEADER = '{0}'", reportTestInfo.REPORT_SERIAL_HEADER.Trim());
            }
            //报告年度
            if (reportTestInfo.REPORT_YEAR.Trim() != "")
            {
                strSQL += string.Format(" AND REPORT_YEAR = '{0}'", reportTestInfo.REPORT_YEAR.Trim());
            }
            //页眉报告年度
            if (reportTestInfo.REPORT_YEAR_HEADER.Trim() != "")
            {
                strSQL += string.Format(" AND REPORT_YEAR_HEADER = '{0}'", reportTestInfo.REPORT_YEAR_HEADER.Trim());
            }
            //报告编号
            if (reportTestInfo.REPORT_SERIAL.Trim() != "")
            {
                strSQL += string.Format(" AND REPORT_SERIAL = '{0}'", reportTestInfo.REPORT_SERIAL.Trim());
            }
            //项目名称
            if (reportTestInfo.PROJECT_NAME.Trim() != "")
            {
                strSQL += string.Format(" AND PROJECT_NAME = '{0}'", reportTestInfo.PROJECT_NAME.Trim());
            }
            //委托单位
            if (reportTestInfo.CLIENT_COMPANY.Trim() != "")
            {
                strSQL += string.Format(" AND CLIENT_COMPANY = '{0}'", reportTestInfo.CLIENT_COMPANY.Trim());
            }
            //检测单位
            if (reportTestInfo.TESTED_COMPANY.Trim() != "")
            {
                strSQL += string.Format(" AND TESTED_COMPANY = '{0}'", reportTestInfo.TESTED_COMPANY.Trim());
            }
            //监测类别
            if (reportTestInfo.CONTRACT_TYPE.Trim() != "")
            {
                strSQL += string.Format(" AND CONTRACT_TYPE = '{0}'", reportTestInfo.CONTRACT_TYPE.Trim());
            }
            //报告日期
            if (reportTestInfo.REPORT_DATE.Trim() != "")
            {
                strSQL += string.Format(" AND REPORT_DATE = '{0}'", reportTestInfo.REPORT_DATE.Trim());
            }
            //监测目的
            if (reportTestInfo.TEST_PURPOSE.Trim() != "")
            {
                strSQL += string.Format(" AND TEST_PURPOSE = '{0}'", reportTestInfo.TEST_PURPOSE.Trim());
            }
            //客户名称
            if (reportTestInfo.CUSTOMER_NAME.Trim() != "")
            {
                strSQL += string.Format(" AND CUSTOMER_NAME = '{0}'", reportTestInfo.CUSTOMER_NAME.Trim());
            }
            //监测位置
            if (reportTestInfo.OUTLET_AND_POINT.Trim() != "")
            {
                strSQL += string.Format(" AND OUTLET_AND_POINT = '{0}'", reportTestInfo.OUTLET_AND_POINT.Trim());
            }
            //采样方式
            if (reportTestInfo.SAMPLE_METHOD.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_METHOD = '{0}'", reportTestInfo.SAMPLE_METHOD.Trim());
            }
            //样品编号
            if (reportTestInfo.SAMPLE_SERIAL.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_SERIAL = '{0}'", reportTestInfo.SAMPLE_SERIAL.Trim());
            }
            //样品类型
            if (reportTestInfo.SAMPLE_TYPE.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_TYPE = '{0}'", reportTestInfo.SAMPLE_TYPE.Trim());
            }
            //采样时间
            if (reportTestInfo.SAMPLE_TIME.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_TIME = '{0}'", reportTestInfo.SAMPLE_TIME.Trim());
            }
            //采样人员
            if (reportTestInfo.SAMPLE_USER.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_USER = '{0}'", reportTestInfo.SAMPLE_USER.Trim());
            }
            //分析时间
            if (reportTestInfo.ANALYSE_TIME.Trim() != "")
            {
                strSQL += string.Format(" AND ANALYSE_TIME = '{0}'", reportTestInfo.ANALYSE_TIME.Trim());
            }
            //分析人员
            if (reportTestInfo.ANALYSE_USER.Trim() != "")
            {
                strSQL += string.Format(" AND ANALYSE_USER = '{0}'", reportTestInfo.ANALYSE_USER.Trim());
            }
            //送样人员
            if (reportTestInfo.SAMPLE_SEND_USER.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_SEND_USER = '{0}'", reportTestInfo.SAMPLE_SEND_USER.Trim());
            }
            //接样人员
            if (reportTestInfo.SAMPLE_RECEIVE_USER.Trim() != "")
            {
                strSQL += string.Format(" AND SAMPLE_RECEIVE_USER = '{0}'", reportTestInfo.SAMPLE_RECEIVE_USER.Trim());
            }
            //天气状况
            if (reportTestInfo.WEATHER_INFO.Trim() != "")
            {
                strSQL += string.Format(" AND WEATHER_INFO = '{0}'", reportTestInfo.WEATHER_INFO.Trim());
            }
            //主要声源
            if (reportTestInfo.SOUND_POSITION.Trim() != "")
            {
                strSQL += string.Format(" AND SOUND_POSITION = '{0}'", reportTestInfo.SOUND_POSITION.Trim());
            }
            //主要振源
            if (reportTestInfo.SHAKE_POSITION.Trim() != "")
            {
                strSQL += string.Format(" AND SHAKE_POSITION = '{0}'", reportTestInfo.SHAKE_POSITION.Trim());
            }
            //备注1
            if (reportTestInfo.REMARK1.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK1 = '{0}'", reportTestInfo.REMARK1.Trim());
            }
            //备注2
            if (reportTestInfo.REMARK2.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK2 = '{0}'", reportTestInfo.REMARK2.Trim());
            }
            //备注3
            if (reportTestInfo.REMARK3.Trim() != "")
            {
                strSQL += string.Format(" AND REMARK3 = '{0}'", reportTestInfo.REMARK3.Trim());
            }

            return strSQL;
        }
        #endregion
    }
}
