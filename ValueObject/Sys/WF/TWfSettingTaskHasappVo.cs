using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：纸质数据审核记录
    /// 创建日期：2013-05-03
    /// 创建人：潘德军
    /// </summary>
    public class TWfSettingTaskHasappVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_SETTING_TASK_HASAPP_TABLE = "T_WF_SETTING_TASK_HASAPP";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 环节ID
        /// </summary>
        public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
        /// <summary>
        /// 环节名
        /// </summary>
        public static string WF_TASK_NAME_FIELD = "WF_TASK_NAME";
        /// <summary>
        /// 流程ID或者流程类别（监测分析用流程类别）
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 实例ID
        /// </summary>
        public static string TASK_ID_FIELD = "TASK_ID";
        /// <summary>
        /// 是否已审核
        /// </summary>
        public static string HAS_APP_FIELD = "HAS_APP";

        #endregion

        public TWfSettingTaskHasappVo()
        {
            this.ID = "";
            this.WF_TASK_ID = "";
            this.WF_TASK_NAME = "";
            this.WF_ID = "";
            this.TASK_ID = "";
            this.HAS_APP = "";

        }

        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节ID
        /// </summary>
        public string WF_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节名
        /// </summary>
        public string WF_TASK_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 流程ID或者流程类别（监测分析用流程类别）
        /// </summary>
        public string WF_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 实例ID
        /// </summary>
        public string TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 是否已审核
        /// </summary>
        public string HAS_APP
        {
            set;
            get;
        }


        #endregion

    }
}