using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程节点集合表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_SETTING_TASK_TABLE = "T_WF_SETTING_TASK";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 节点编号
        /// </summary>
        public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
        /// <summary>
        /// 流程编号
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 节点类型
        /// </summary>
        public static string TASK_TYPE_FIELD = "TASK_TYPE";
        /// <summary>
        /// 节点简称
        /// </summary>
        public static string TASK_CAPTION_FIELD = "TASK_CAPTION";
        /// <summary>
        /// 节点描述
        /// </summary>
        public static string TASK_NOTE_FIELD = "TASK_NOTE";
        /// <summary>
        /// 节点与非类型
        /// </summary>
        public static string TASK_AND_OR_FIELD = "TASK_AND_OR";
        /// <summary>
        /// 操作人类型
        /// </summary>
        public static string OPER_TYPE_FIELD = "OPER_TYPE";
        /// <summary>
        /// 操作人值
        /// </summary>
        public static string OPER_VALUE_FIELD = "OPER_VALUE";
        /// <summary>
        /// 命令名称
        /// </summary>
        public static string COMMAND_NAME_FIELD = "COMMAND_NAME";
        /// <summary>
        /// 附加功能
        /// </summary>
        public static string FUNCTION_LIST_FIELD = "FUNCTION_LIST";
        /// <summary>
        /// 节点排序
        /// </summary>
        public static string TASK_ORDER_FIELD = "TASK_ORDER";
        /// <summary>
        /// 跳过自己
        /// </summary>
        public static string SELF_DEAL_FIELD = "SELF_DEAL";
        /// <summary>
        /// 绘图X坐标
        /// </summary>
        public static string POSITION_IX_FIELD = "POSITION_IX";
        /// <summary>
        /// 绘图Y坐标
        /// </summary>
        public static string POSITION_IY_FIELD = "POSITION_IY";

        #endregion

        public TWfSettingTaskVo()
        {
            this.ID = "";
            this.WF_TASK_ID = "";
            this.WF_ID = "";
            this.TASK_TYPE = "";
            this.TASK_CAPTION = "";
            this.TASK_NOTE = "";
            this.TASK_AND_OR = "";
            this.OPER_TYPE = "";
            this.OPER_VALUE = "";
            this.COMMAND_NAME = "";
            this.FUNCTION_LIST = "";
            this.TASK_ORDER = "";
            this.SELF_DEAL = "";
            this.POSITION_IX = "";
            this.POSITION_IY = "";

        }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 节点编号
        /// </summary>
        public string WF_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string WF_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        public string TASK_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 节点简称
        /// </summary>
        public string TASK_CAPTION
        {
            set;
            get;
        }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string TASK_NOTE
        {
            set;
            get;
        }
        /// <summary>
        /// 节点与非类型
        /// </summary>
        public string TASK_AND_OR
        {
            set;
            get;
        }
        /// <summary>
        /// 操作人类型
        /// </summary>
        public string OPER_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 操作人值
        /// </summary>
        public string OPER_VALUE
        {
            set;
            get;
        }
        /// <summary>
        /// 命令名称 
        /// </summary>
        public string COMMAND_NAME
        {
            set;
            get;
        }

        /// <summary>
        /// 附加功能
        /// </summary>
        public string FUNCTION_LIST
        {
            set;
            get;
        }
        /// <summary>
        /// 节点排序
        /// </summary>
        public string TASK_ORDER
        {
            set;
            get;
        }

        /// <summary>
        /// 跳过自己
        /// </summary>
        public string SELF_DEAL
        {
            set;
            get;
        }
        /// <summary>
        /// 绘图X坐标
        /// </summary>
        public string POSITION_IX
        {
            set;
            get;
        }
        /// <summary>
        /// 绘图Y坐标
        /// </summary>
        public string POSITION_IY
        {
            set;
            get;
        }


        #endregion

    }
}