using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_SUBTASK_TABLE = "T_MIS_MONITOR_SUBTASK";
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
        /// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
        /// </summary>
        public static string MONITOR_ID_FIELD = "MONITOR_ID";
        /// <summary>
        /// 采样要求时间
        /// </summary>
        public static string SAMPLE_ASK_DATE_FIELD = "SAMPLE_ASK_DATE";
        /// <summary>
        /// 分析完成时间
        /// </summary>
        public static string SAMPLE_FINISH_DATE_FIELD = "SAMPLE_FINISH_DATE";
        /// <summary>
        /// 采样方式
        /// </summary>
        public static string SAMPLING_METHOD_FIELD = "SAMPLING_METHOD";
        /// <summary>
        /// 采样负责人
        /// </summary>
        public static string SAMPLING_MANAGER_ID_FIELD = "SAMPLING_MANAGER_ID";
        /// <summary>
        /// 采样协同人ID
        /// </summary>
        public static string SAMPLING_ID_FIELD = "SAMPLING_ID";
        /// <summary>
        /// 采样人
        /// </summary>
        public static string SAMPLING_MAN_FIELD = "SAMPLING_MAN";
        /// <summary>
        /// 样品接收时间
        /// </summary>
        public static string SAMPLE_ACCESS_DATE_FIELD = "SAMPLE_ACCESS_DATE";
        /// <summary>
        /// 样品接收人ID
        /// </summary>
        public static string SAMPLE_ACCESS_ID_FIELD = "SAMPLE_ACCESS_ID";
        /// <summary>
        /// 接样意见
        /// </summary>
        public static string SAMPLE_APPROVE_INFO_FIELD = "SAMPLE_APPROVE_INFO";
        /// <summary>
        /// 分析完成时间
        /// </summary>
        public static string ANALYSE_FINISH_DATE_FIELD = "ANALYSE_FINISH_DATE";
        /// <summary>
        /// 监测结论
        /// </summary>
        public static string PROJECT_CONCLUSION_FIELD = "PROJECT_CONCLUSION";
        /// <summary>
        /// 项目完成时间
        /// </summary>
        public static string PROJECT_FINISH_DATE_FIELD = "PROJECT_FINISH_DATE";
        /// <summary>
        /// 任务状态类别(发送，退回)
        /// </summary>
        public static string TASK_TYPE_FIELD = "TASK_TYPE";
        /// <summary>
        /// 任务状态
        /// </summary>
        public static string TASK_STATUS_FIELD = "TASK_STATUS";
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

        public TMisMonitorSubtaskVo()
        {
            this.ID = "";
            this.TASK_ID = "";
            this.MONITOR_ID = "";
            this.SAMPLE_ASK_DATE = "";
            this.SAMPLE_FINISH_DATE = "";
            this.SAMPLING_METHOD = "";
            this.SAMPLING_MANAGER_ID = "";
            this.SAMPLING_ID = "";
            this.SAMPLING_MAN = "";
            this.SAMPLE_ACCESS_DATE = "";
            this.SAMPLE_ACCESS_ID = "";
            this.SAMPLE_APPROVE_INFO = "";
            this.ANALYSE_FINISH_DATE = "";
            this.PROJECT_CONCLUSION = "";
            this.PROJECT_FINISH_DATE = "";
            this.TASK_TYPE = "";
            this.TASK_STATUS = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
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
        /// 监测计划ID
        /// </summary>
        public string TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
        /// </summary>
        public string MONITOR_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样要求时间
        /// </summary>
        public string SAMPLE_ASK_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 分析完成时间
        /// </summary>
        public string SAMPLE_FINISH_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样方式
        /// </summary>
        public string SAMPLING_METHOD
        {
            set;
            get;
        }
        /// <summary>
        /// 采样负责人
        /// </summary>
        public string SAMPLING_MANAGER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样协同人ID
        /// </summary>
        public string SAMPLING_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样人
        /// </summary>
        public string SAMPLING_MAN
        {
            set;
            get;
        }
        /// <summary>
        /// 样品接收时间
        /// </summary>
        public string SAMPLE_ACCESS_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品接收人ID
        /// </summary>
        public string SAMPLE_ACCESS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 接样意见
        /// </summary>
        public string SAMPLE_APPROVE_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 分析完成时间
        /// </summary>
        public string ANALYSE_FINISH_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 监测结论
        /// </summary>
        public string PROJECT_CONCLUSION
        {
            set;
            get;
        }
        /// <summary>
        /// 项目完成时间
        /// </summary>
        public string PROJECT_FINISH_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 任务状态类别(发送，退回)
        /// </summary>
        public string TASK_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string TASK_STATUS
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