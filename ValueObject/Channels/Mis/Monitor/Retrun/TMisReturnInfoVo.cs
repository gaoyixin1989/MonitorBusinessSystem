using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Retrun
{
    /// <summary>
    /// 功能：监测分析各环节退回意见表
    /// 创建日期：2014-04-08
    /// 创建人：魏林
    /// </summary>
    public class TMisReturnInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_RETURN_INFO_TABLE = "T_MIS_RETURN_INFO";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 任务ID
        /// </summary>
        public static string TASK_ID_FIELD = "TASK_ID";
        /// <summary>
        /// 子任务ID
        /// </summary>
        public static string SUBTASK_ID_FIELD = "SUBTASK_ID";
        /// <summary>
        /// 项目结果ID
        /// </summary>
        public static string RESULT_ID_FIELD = "RESULT_ID";
        /// <summary>
        /// 当前环节号
        /// </summary>
        public static string CURRENT_STATUS_FIELD = "CURRENT_STATUS";
        /// <summary>
        /// 退回环节号
        /// </summary>
        public static string BACKTO_STATUS_FIELD = "BACKTO_STATUS";
        /// <summary>
        /// 退回意见
        /// </summary>
        public static string SUGGESTION_FIELD = "SUGGESTION";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK3_FIELD = "REMARK3";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK4_FIELD = "REMARK4";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";

        #endregion

        public TMisReturnInfoVo()
        {
            this.ID = "";
            this.TASK_ID = "";
            this.SUBTASK_ID = "";
            this.RESULT_ID = "";
            this.CURRENT_STATUS = "";
            this.BACKTO_STATUS = "";
            this.SUGGESTION = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 任务ID
        /// </summary>
        public string TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 子任务ID
        /// </summary>
        public string SUBTASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 项目结果ID
        /// </summary>
        public string RESULT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 当前环节号
        /// </summary>
        public string CURRENT_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 退回环节号
        /// </summary>
        public string BACKTO_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 退回意见
        /// </summary>
        public string SUGGESTION
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }


        #endregion

    }

}
