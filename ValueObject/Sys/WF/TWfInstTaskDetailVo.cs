using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskDetailVo : i3.Core.ValueObject.ObjectBase, ICloneable
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_INST_TASK_DETAIL_TABLE = "T_WF_INST_TASK_DETAIL";
        //静态字段引用
        /// <summary>
        /// 环节实例编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public static string WF_INST_ID_FIELD = "WF_INST_ID";
        /// <summary>
        /// 流水号
        /// </summary>
        public static string WF_SERIAL_NO_FIELD = "WF_SERIAL_NO";
        /// <summary>
        /// 流程编号
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 环节编号
        /// </summary>
        public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
        /// <summary>
        /// 上环节实例编号
        /// </summary>
        public static string PRE_INST_TASK_ID_FIELD = "PRE_INST_TASK_ID";
        /// <summary>
        /// 上环节编号
        /// </summary>
        public static string PRE_TASK_ID_FIELD = "PRE_TASK_ID";
        /// <summary>
        /// 此环节简述
        /// </summary>
        public static string INST_TASK_CAPTION_FIELD = "INST_TASK_CAPTION";
        /// <summary>
        /// 此节点详细描述
        /// </summary>
        public static string INST_NOTE_FIELD = "INST_NOTE";
        /// <summary>
        /// 此环节开始时间
        /// </summary>
        public static string INST_TASK_STARTTIME_FIELD = "INST_TASK_STARTTIME";
        /// <summary>
        /// 此环节结束时间
        /// </summary>
        public static string INST_TASK_ENDTIME_FIELD = "INST_TASK_ENDTIME";
        /// <summary>
        /// 此环节状态
        /// </summary>
        public static string INST_TASK_STATE_FIELD = "INST_TASK_STATE";

        /// <summary>
        /// 环节处理状态
        /// </summary>
        public static string INST_TASK_DEAL_STATE_FIELD = "INST_TASK_DEAL_STATE";
        /// <summary>
        /// 目标操作人
        /// </summary>
        public static string OBJECT_USER_FIELD = "OBJECT_USER";
        /// <summary>
        /// 实际操作人
        /// </summary>
        public static string REAL_USER_FIELD = "REAL_USER";
        /// <summary>
        /// 发送人
        /// </summary>
        public static string SRC_USER_FIELD = "SRC_USER";
        /// <summary>
        /// 环节提示信息
        /// </summary>
        public static string INST_TASK_MSG_FIELD = "INST_TASK_MSG";
        /// <summary>
        /// 是否超时
        /// </summary>
        public static string IS_OVERTIME_FIELD = "IS_OVERTIME";
        /// <summary>
        /// 是否提醒
        /// </summary>
        public static string IS_REMIND_FIELD = "IS_REMIND";

        /// <summary>
        /// 确认人
        /// </summary>
        public static string CFM_USER_FIELD = "CFM_USER";


        /// <summary>
        /// 确认时间
        /// </summary>
        public static string CFM_TIME_FIELD = "CFM_TIME";

        /// <summary>
        /// 撤销人
        /// </summary>
        public static string CFM_UNTIME_FIELD = "CFM_UNTIME";

        #endregion

        public TWfInstTaskDetailVo()
        {
            this.ID = "";
            this.WF_INST_ID = "";
            this.WF_SERIAL_NO = "";
            this.WF_ID = "";
            this.WF_TASK_ID = "";
            this.PRE_INST_TASK_ID = "";
            this.PRE_TASK_ID = "";
            this.INST_TASK_CAPTION = "";
            this.INST_NOTE = "";
            this.INST_TASK_STARTTIME = "";
            this.INST_TASK_ENDTIME = "";
            this.INST_TASK_STATE = "";
            this.INST_TASK_DEAL_STATE = "";
            this.OBJECT_USER = "";
            this.REAL_USER = "";
            this.SRC_USER = "";
            this.INST_TASK_MSG = "";
            this.IS_OVERTIME = "";
            this.IS_REMIND = "";
            this.CFM_TIME = "";
            this.CFM_UNTIME = "";
            this.CFM_USER = "";

        }

        #region 属性
        /// <summary>
        /// 环节实例编号
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
        /// 流水号
        /// </summary>
        public string WF_SERIAL_NO
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
        /// 环节编号
        /// </summary>
        public string WF_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 上环节实例编号
        /// </summary>
        public string PRE_INST_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 上环节编号
        /// </summary>
        public string PRE_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 此环节简述
        /// </summary>
        public string INST_TASK_CAPTION
        {
            set;
            get;
        }
        /// <summary>
        /// 此节点详细描述
        /// </summary>
        public string INST_NOTE
        {
            set;
            get;
        }
        /// <summary>
        /// 此环节开始时间
        /// </summary>
        public string INST_TASK_STARTTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 此环节结束时间
        /// </summary>
        public string INST_TASK_ENDTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 此环节状态
        /// </summary>
        public string INST_TASK_STATE
        {
            set;
            get;
        }

        /// <summary>
        /// 环节处理状态
        /// </summary>
        public string INST_TASK_DEAL_STATE
        {
            get;
            set;
        }
        /// <summary>
        /// 目标操作人
        /// </summary>
        public string OBJECT_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 实际操作人
        /// </summary>
        public string REAL_USER
        {
            set;
            get;
        }

        /// <summary>
        /// 发送人
        /// </summary>
        public string SRC_USER
        {
            get;
            set;
        }

        /// <summary>
        /// 环节提示信息
        /// </summary>
        public string INST_TASK_MSG
        {
            set;
            get;
        }
        /// <summary>
        /// 是否超时
        /// </summary>
        public string IS_OVERTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 是否提醒
        /// </summary>
        public string IS_REMIND
        {
            set;
            get;
        }

        /// <summary>
        /// 确认人
        /// </summary>
        public string CFM_USER
        {
            set;
            get;
        }

        /// <summary>
        /// 确认时间
        /// </summary>
        public string CFM_TIME
        {
            set;
            get;
        }

        /// <summary>
        /// 撤销时间
        /// </summary>
        public string CFM_UNTIME
        {
            set;
            get;
        }


        #endregion


        /// <summary>
        /// 对象克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.Clone();
        }
    }
}