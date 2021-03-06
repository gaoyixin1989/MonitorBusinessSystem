using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务审核表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskAppVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_SUBTASK_APP_TABLE = "T_MIS_MONITOR_SUBTASK_APP";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测子任务ID
        /// </summary>
        public static string SUBTASK_ID_FIELD = "SUBTASK_ID";
        /// <summary>
        /// 采样任务分配人
        /// </summary>
        public static string SAMPLE_ASSIGN_ID_FIELD = "SAMPLE_ASSIGN_ID";
        /// <summary>
        /// 采样任务分配时间
        /// </summary>
        public static string SAMPLE_ASSIGN_DATE_FIELD = "SAMPLE_ASSIGN_DATE";
        /// <summary>
        /// 质控手段设置人
        /// </summary>
        public static string QC_USER_ID_FIELD = "QC_USER_ID";
        /// <summary>
        /// 质控手段设置时间
        /// </summary>
        public static string QC_DATE_FIELD = "QC_DATE";
        /// <summary>
        /// 分析任务分配人
        /// </summary>
        public static string ANALYSE_ASSIGN_ID_FIELD = "ANALYSE_ASSIGN_ID";
        /// <summary>
        /// 分析任务分配时间
        /// </summary>
        public static string ANALYSE_ASSIGN_DATE_FIELD = "ANALYSE_ASSIGN_DATE";
        /// <summary>
        /// 质控手段审核人ID
        /// </summary>
        public static string QC_APP_USER_ID_FIELD = "QC_APP_USER_ID";
        /// <summary>
        /// 质控手段审核时间
        /// </summary>
        public static string QC_APP_DATE_FIELD = "QC_APP_DATE";
        /// <summary>
        /// 质控手段审核意见
        /// </summary>
        public static string QC_APP_INFO_FIELD = "QC_APP_INFO";
        /// <summary>
        /// 数据审核
        /// </summary>
        public static string RESULT_AUDIT_FIELD = "RESULT_AUDIT";
        /// <summary>
        /// 分析室主任审核
        /// </summary>
        public static string RESULT_CHECK_FIELD = "RESULT_CHECK";
        /// <summary>
        /// 分析室主任审核时间
        /// </summary>
        public static string RESULT_CHECK_DATE_FIELD = "RESULT_CHECK_DATE";
        /// <summary>
        /// 技术室审核
        /// </summary>
        public static string RESULT_QC_CHECK_FIELD = "RESULT_QC_CHECK";
        /// <summary>
        /// 技术室审核时间
        /// </summary>
        public static string RESULT_QC_CHECK_DATE_FIELD = "RESULT_QC_CHECK_DATE";
        /// <summary>
        /// 现场复核
        /// </summary>
        public static string SAMPLING_CHECK_FIELD = "SAMPLING_CHECK";
        /// <summary>
        /// 现场审核
        /// </summary>
        public static string SAMPLING_QC_CHECK_FIELD = "SAMPLING_QC_CHECK";
        /// <summary>
        /// 采样后质控
        /// </summary>
        public static string SAMPLING_END_QC_FIELD = "SAMPLING_END_QC";
        /// <summary>
        /// 备注1 (采样时间)
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 备注2 (现场复核时间)
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 备注3 (现场审核时间)
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

        #endregion

        public TMisMonitorSubtaskAppVo()
        {
            this.ID = "";
            this.SUBTASK_ID = "";
            this.SAMPLE_ASSIGN_ID = "";
            this.SAMPLE_ASSIGN_DATE = "";
            this.QC_USER_ID = "";
            this.QC_DATE = "";
            this.ANALYSE_ASSIGN_ID = "";
            this.ANALYSE_ASSIGN_DATE = "";
            this.QC_APP_USER_ID = "";
            this.QC_APP_DATE = "";
            this.QC_APP_INFO = "";
            this.RESULT_AUDIT = "";
            this.RESULT_CHECK = "";
            this.RESULT_CHECK_DATE = "";
            this.RESULT_QC_CHECK = "";
            this.RESULT_QC_CHECK_DATE = "";
            this.SAMPLING_CHECK = "";
            this.SAMPLING_QC_CHECK = "";
            this.SAMPLING_END_QC = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

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
        /// 监测子任务ID
        /// </summary>
        public string SUBTASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样任务分配人
        /// </summary>
        public string SAMPLE_ASSIGN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样任务分配时间
        /// </summary>
        public string SAMPLE_ASSIGN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 质控手段设置人
        /// </summary>
        public string QC_USER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 质控手段设置时间
        /// </summary>
        public string QC_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 分析任务分配人
        /// </summary>
        public string ANALYSE_ASSIGN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 分析任务分配时间
        /// </summary>
        public string ANALYSE_ASSIGN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 质控手段审核人ID
        /// </summary>
        public string QC_APP_USER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 质控手段审核时间
        /// </summary>
        public string QC_APP_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 质控手段审核意见
        /// </summary>
        public string QC_APP_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 数据审核
        /// </summary>
        public string RESULT_AUDIT
        {
            set;
            get;
        }
        /// <summary>
        /// 分析室主任审核
        /// </summary>
        public string RESULT_CHECK
        {
            set;
            get;
        }
        /// <summary>
        /// 分析室主任审核时间
        /// </summary>
        public string RESULT_CHECK_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 技术室审核
        /// </summary>
        public string RESULT_QC_CHECK
        {
            set;
            get;
        }
        /// <summary>
        /// 技术室审核时间
        /// </summary>
        public string RESULT_QC_CHECK_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 现场复核
        /// </summary>
        public string SAMPLING_CHECK
        {
            set;
            get;
        }
        /// <summary>
        /// 现场审核 
        /// </summary>
        public string SAMPLING_QC_CHECK
        {
            set;
            get;
        }
        /// <summary>
        /// 采样后质控
        /// </summary>
        public string SAMPLING_END_QC
        {
            set;
            get;
        }
        /// <summary>
        /// 备注1(采样时间)
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 备注2 (现场复核时间)
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3(现场审核时间)
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
        #endregion
    }
}