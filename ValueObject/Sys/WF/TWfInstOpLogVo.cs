using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程操作记录表
    /// 创建日期：2012-11-07
    /// 创建人：石磊
    /// </summary>
    public class TWfInstOpLogVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_INST_OP_LOG_TABLE = "T_WF_INST_OP_LOG";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public static string WF_INST_ID_FIELD = "WF_INST_ID";
        /// <summary>
        /// 环节实例编号
        /// </summary>
        public static string WF_INST_TASK_ID_FIELD = "WF_INST_TASK_ID";
        /// <summary>
        /// 操作用户
        /// </summary>
        public static string OP_USER_FIELD = "OP_USER";
        /// <summary>
        /// 操作动作
        /// </summary>
        public static string OP_ACTION_FIELD = "OP_ACTION";
        /// <summary>
        /// 流程简码
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 环节简码
        /// </summary>
        public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
        /// <summary>
        /// 操作时间
        /// </summary>
        public static string OP_TIME_FIELD = "OP_TIME";
        /// <summary>
        /// 操作描述
        /// </summary>
        public static string OP_NOTE_FIELD = "OP_NOTE";
        /// <summary>
        /// 是否代理
        /// </summary>
        public static string IS_AGENT_FIELD = "IS_AGENT";
        /// <summary>
        /// 被代理人
        /// </summary>
        public static string AGENT_USER_FIELD = "AGENT_USER";

        #endregion

        public TWfInstOpLogVo()
        {
            this.ID = "";
            this.WF_INST_ID = "";
            this.WF_INST_TASK_ID = "";
            this.OP_USER = "";
            this.OP_ACTION = "";
            this.WF_ID = "";
            this.WF_TASK_ID = "";
            this.OP_TIME = "";
            this.OP_NOTE = "";
            this.IS_AGENT = "";
            this.AGENT_USER = "";

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
        /// 流程实例编号
        /// </summary>
        public string WF_INST_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节实例编号
        /// </summary>
        public string WF_INST_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 操作用户
        /// </summary>
        public string OP_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 操作动作
        /// </summary>
        public string OP_ACTION
        {
            set;
            get;
        }
        /// <summary>
        /// 流程简码
        /// </summary>
        public string WF_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 环节简码
        /// </summary>
        public string WF_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OP_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string OP_NOTE
        {
            set;
            get;
        }
        /// <summary>
        /// 是否代理
        /// </summary>
        public string IS_AGENT
        {
            set;
            get;
        }
        /// <summary>
        /// 被代理人
        /// </summary>
        public string AGENT_USER
        {
            set;
            get;
        }


        #endregion

    }
}