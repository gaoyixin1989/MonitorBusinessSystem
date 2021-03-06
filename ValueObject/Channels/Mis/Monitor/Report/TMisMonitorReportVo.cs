using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Report
{
    /// <summary>
    /// 功能：监测报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorReportVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_REPORT_TABLE = "T_MIS_MONITOR_REPORT";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测计划ID
        /// </summary>
        public static string TASK_ID_FIELD = "TASK_ID";
        /// <summary>
        /// 报告单号
        /// </summary>
        public static string REPORT_CODE_FIELD = "REPORT_CODE";
        /// <summary>
        /// 报告完成日期
        /// </summary>
        public static string REPORT_DATE_FIELD = "REPORT_DATE";
        /// <summary>
        /// 报告要求完成时间
        /// </summary>
        public static string RPT_ASK_DATE_FIELD = "RPT_ASK_DATE";

        /// <summary>
        /// 验收报告附件ID
        /// </summary>
        public static string REPORT_EX_ATTACHE_ID_FIELD = "REPORT_EX_ATTACHE_ID";
        /// <summary>
        /// 是否领取
        /// </summary>
        public static string IF_GET_FIELD = "IF_GET";
        /// <summary>
        /// 是否发送分配
        /// </summary>
        public static string IF_SEND_FIELD = "IF_SEND";
        /// <summary>
        /// 是否确认
        /// </summary>
        public static string IF_ACCEPT_FIELD = "IF_ACCEPT";

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
        /// 报告编制人
        /// </summary>
        public static string REPORT_SCHEDULER_FIELD = "REPORT_SCHEDULER";
        /// <summary>
        /// 报告签发人
        /// </summary>
        public static string REPORT_APPROVER_FIELD = "REPORT_APPROVER";

        #endregion

        public TMisMonitorReportVo()
        {
            this.ID = "";
            this.TASK_ID = "";
            this.REPORT_CODE = "";
            this.REPORT_DATE = "";
            this.RPT_ASK_DATE = "";

            this.REPORT_EX_ATTACHE_ID = "";
            this.IF_GET = "";
            this.IF_SEND = "";
            this.IF_ACCEPT = "";

            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.REPORT_SCHEDULER = "";
            this.REPORT_APPROVER = "";

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
        /// 监测计划ID
        /// </summary>
        public string TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 报告单号
        /// </summary>
        public string REPORT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 报告完成日期
        /// </summary>
        public string REPORT_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 报告要求完成时间
        /// </summary>
        public string RPT_ASK_DATE
        {
            set;
            get;
        }

        /// <summary>
        /// 验收报告附件ID
        /// </summary>
        public string REPORT_EX_ATTACHE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 是否领取
        /// </summary>
        public string IF_GET
        {
            set;
            get;
        }
        /// <summary>
        /// 是否发送分配
        /// </summary>
        public string IF_SEND
        {
            set;
            get;
        }
        /// <summary>
        /// 是否确认
        /// </summary>
        public string IF_ACCEPT
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
        /// 报告编制人
        /// </summary>
        public string REPORT_SCHEDULER
        {
            get;
            set;
        }
        /// <summary>
        /// 报告签发人
        /// </summary>
        public string REPORT_APPROVER
        {
            set;
            get;
        }

        #endregion

    }
}