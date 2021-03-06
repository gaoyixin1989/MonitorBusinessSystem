using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_TASK_TABLE = "T_MIS_MONITOR_TASK";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 委托书ID
        /// </summary>
        public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
        /// <summary>
        /// 预约ID
        /// </summary>
        public static string PLAN_ID_FIELD = "PLAN_ID";
        /// <summary>
        /// 委托书编
        /// </summary>
        public static string CONTRACT_CODE_FIELD = "CONTRACT_CODE";
        /// <summary>
        /// 委托年度
        /// </summary>
        public static string CONTRACT_YEAR_FIELD = "CONTRACT_YEAR";
        /// <summary>
        /// 项目名称
        /// </summary>
        public static string PROJECT_NAME_FIELD = "PROJECT_NAME";
        /// <summary>
        /// 委托类型
        /// </summary>
        public static string CONTRACT_TYPE_FIELD = "CONTRACT_TYPE";
        /// <summary>
        /// 报告类型
        /// </summary>
        public static string TEST_TYPE_FIELD = "TEST_TYPE";
        /// <summary>
        /// 监测目的
        /// </summary>
        public static string TEST_PURPOSE_FIELD = "TEST_PURPOSE";
        /// <summary>
        /// 任务单号
        /// </summary>
        public static string TICKET_NUM_FIELD = "TICKET_NUM";
        /// <summary>
        /// 委托企业ID
        /// </summary>
        public static string CLIENT_COMPANY_ID_FIELD = "CLIENT_COMPANY_ID";
        /// <summary>
        /// 受检企业ID
        /// </summary>
        public static string TESTED_COMPANY_ID_FIELD = "TESTED_COMPANY_ID";
        /// <summary>
        /// 合同签订日期
        /// </summary>
        public static string CONSIGN_DATE_FIELD = "CONSIGN_DATE";
        /// <summary>
        /// 要求完成日期
        /// </summary>
        public static string ASKING_DATE_FIELD = "ASKING_DATE";
        /// <summary>
        /// 完成日期
        /// </summary>
        public static string FINISH_DATE_FIELD = "FINISH_DATE";
        /// <summary>
        /// 样品来源,1,抽样，2，自送样
        /// </summary>
        public static string SAMPLE_SOURCE_FIELD = "SAMPLE_SOURCE";
        /// <summary>
        /// 送样人ID
        /// </summary>
        public static string CONTACT_ID_FIELD = "CONTACT_ID";
        /// <summary>
        /// 接样人ID
        /// </summary>
        public static string MANAGER_ID_FIELD = "MANAGER_ID";
        /// <summary>
        /// 计划制定人ID
        /// </summary>
        public static string CREATOR_ID_FIELD = "CREATOR_ID";
        /// <summary>
        /// 项目负责人ID
        /// </summary>
        public static string PROJECT_ID_FIELD = "PROJECT_ID";
        /// <summary>
        /// 计划制定日期
        /// </summary>
        public static string CREATE_DATE_FIELD = "CREATE_DATE";
        /// <summary>
        /// 状态
        /// </summary>
        public static string STATE_FIELD = "STATE";
        /// <summary>
        /// 计划状态
        /// </summary>
        public static string TASK_STATUS_FIELD = "TASK_STATUS";
        /// <summary>
        /// 确认状态
        /// </summary>
        public static string COMFIRM_STATUS_FIELD = "COMFIRM_STATUS";
        /// <summary>
        /// 备注1
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 备注2
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 备注3
        /// </summary>
        public static string REMARK3_FIELD = "REMARK3";
        /// <summary>
        /// 备注4
        /// </summary>
        public static string REMARK4_FIELD = "REMARK4";
        /// <summary>
        /// 备注5
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";
        /// <summary>
        /// 生成报告的处理人（清远）
        /// </summary>
        public static string REPORT_HANDLE_FIELD = "REPORT_HANDLE";
        /// <summary>
        /// 送样人
        /// </summary>
        public static string SAMPLE_SEND_MAN_FIELD = "SAMPLE_SEND_MAN";
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public static string CCFLOW_ID1_FIELD = "CCFLOW_ID1";
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public static string CCFLOW_ID2_FIELD = "CCFLOW_ID2";
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public static string CCFLOW_ID3_FIELD = "CCFLOW_ID3";

        #endregion

        public TMisMonitorTaskVo()
        {
            this.ID = "";
            this.CONTRACT_ID = "";
            this.PLAN_ID = "";
            this.CONTRACT_CODE = "";
            this.CONTRACT_YEAR = "";
            this.PROJECT_NAME = "";
            this.CONTRACT_TYPE = "";
            this.TEST_TYPE = "";
            this.TEST_PURPOSE = "";
            this.TICKET_NUM = "";
            this.CLIENT_COMPANY_ID = "";
            this.TESTED_COMPANY_ID = "";
            this.CONSIGN_DATE = "";
            this.ASKING_DATE = "";
            this.FINISH_DATE = "";
            this.SAMPLE_SOURCE = "";
            this.CONTACT_ID = "";
            this.MANAGER_ID = "";
            this.CREATOR_ID = "";
            this.PROJECT_ID = "";
            this.CREATE_DATE = "";
            this.STATE = "";
            this.QC_STATUS = "";
            this.ALLQC_STATUS = "";
            this.TASK_STATUS = "";
            this.TASK_TYPE = "";
            this.COMFIRM_STATUS = "";
            this.SEND_STATUS = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.REPORT_HANDLE = "";
            this.SAMPLE_SEND_MAN = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";
        }

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书ID
        /// </summary>
        public string CONTRACT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 预约ID
        /// </summary>
        public string PLAN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书编
        /// </summary>
        public string CONTRACT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 委托年度
        /// </summary>
        public string CONTRACT_YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PROJECT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 委托类型
        /// </summary>
        public string CONTRACT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 报告类型
        /// </summary>
        public string TEST_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 监测目的
        /// </summary>
        public string TEST_PURPOSE
        {
            set;
            get;
        }
        /// <summary>
        /// 任务单号
        /// </summary>
        public string TICKET_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 委托企业ID
        /// </summary>
        public string CLIENT_COMPANY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 受检企业ID
        /// </summary>
        public string TESTED_COMPANY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 合同签订日期
        /// </summary>
        public string CONSIGN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 要求完成日期
        /// </summary>
        public string ASKING_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public string FINISH_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品来源,1,抽样，2，自送样
        /// </summary>
        public string SAMPLE_SOURCE
        {
            set;
            get;
        }
        /// <summary>
        /// 送样人ID
        /// </summary>
        public string CONTACT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 接样人ID
        /// </summary>
        public string MANAGER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 计划制定人ID
        /// </summary>
        public string CREATOR_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 项目负责人ID
        /// </summary>
        public string PROJECT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 计划制定日期
        /// </summary>
        public string CREATE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 计划状态
        /// </summary>
        public string TASK_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 质控环节状态
        /// </summary>
        public string QC_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 是否全程质控
        /// </summary>
        public string ALLQC_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 任务类型，1表示环境质量，普通委托书类任务为空
        /// </summary>
        public string TASK_TYPE
        {
            set;
            get;
        }
        /// <summary>
        ///确认状态（1：已确认；0或NULL：未确认；2：已办理）
        /// </summary>
        public string COMFIRM_STATUS
        {
            set;
            get;
        }
        /// <summary>
        ///环境质量任务状态
        /// </summary>
        public string SEND_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 备注1
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }
        /// <summary>
        /// 备注4
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 备注5
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }
        /// <summary>
        /// 生成报告的处理人（清远）
        /// </summary>
        public string REPORT_HANDLE
        {
            set;
            get;
        }
        /// <summary>
        /// 送样人
        /// </summary>
        public string SAMPLE_SEND_MAN
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public string CCFLOW_ID1
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public string CCFLOW_ID2
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public string CCFLOW_ID3
        {
            set;
            get;
        }

        #endregion

    }
}