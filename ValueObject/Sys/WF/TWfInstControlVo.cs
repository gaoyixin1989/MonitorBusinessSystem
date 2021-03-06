using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例控制表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstControlVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_INST_CONTROL_TABLE = "T_WF_INST_CONTROL";
        //静态字段引用
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 流程编号
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 流水号
        /// </summary>
        public static string WF_SERIAL_NO_FIELD = "WF_SERIAL_NO";
        /// <summary>
        /// 当前环节编号
        /// </summary>
        public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
        /// <summary>
        /// 当前实例环节编号
        /// </summary>
        public static string WF_INST_TASK_ID_FIELD = "WF_INST_TASK_ID";
        /// <summary>
        /// 流程简述
        /// </summary>
        public static string WF_CAPTION_FIELD = "WF_CAPTION";
        /// <summary>
        /// 流程备注
        /// </summary>
        public static string WF_NOTE_FIELD = "WF_NOTE";

        /// <summary>
        /// 业务代码
        /// </summary>
        public static string WF_SERVICE_CODE_FIELD = "WF_SERVICE_CODE";

        /// <summary>
        /// 业务名称
        /// </summary>
        public static string WF_SERVICE_NAME_FIELD = "WF_SERVICE_NAME";

        /// <summary>
        /// 优先级
        /// </summary>
        public static string WF_PRIORITY_FIELD = "WF_PRIORITY";
        /// <summary>
        /// 流程状态
        /// </summary>
        public static string WF_STATE_FIELD = "WF_STATE";
        /// <summary>
        /// 开始时间
        /// </summary>
        public static string WF_STARTTIME_FIELD = "WF_STARTTIME";
        /// <summary>
        /// 约定结束时间
        /// </summary>
        public static string WF_ENDTIME_FIELD = "WF_ENDTIME";
        /// <summary>
        /// 挂起时间
        /// </summary>
        public static string WF_SUSPEND_TIME_FIELD = "WF_SUSPEND_TIME";
        /// <summary>
        /// 挂起状态
        /// </summary>
        public static string WF_SUSPEND_STATE_FIELD = "WF_SUSPEND_STATE";
        /// <summary>
        /// 挂起的结束时间
        /// </summary>
        public static string WF_SUSPEND_ENDTIME_FIELD = "WF_SUSPEND_ENDTIME";
        /// <summary>
        /// 是否子流程
        /// </summary>
        public static string IS_SUB_FLOW_FIELD = "IS_SUB_FLOW";
        /// <summary>
        /// 父流程实例编号
        /// </summary>
        public static string PARENT_INST_FLOW_ID_FIELD = "PARENT_INST_FLOW_ID";
        /// <summary>
        /// 父流程编号
        /// </summary>
        public static string PARENT_FLOW_ID_FIELD = "PARENT_FLOW_ID";
        /// <summary>
        /// 父流程环节实例编号
        /// </summary>
        public static string PARENT_INST_TASK_ID_FIELD = "PARENT_INST_TASK_ID";
        /// <summary>
        /// 父流程环节编号
        /// </summary>
        public static string PARENT_TASK_ID_FIELD = "PARENT_TASK_ID";
        /// <summary>
        /// 其他备注
        /// </summary>
        public static string REMARK_FIELD = "REMARK";

        #endregion

        public TWfInstControlVo()
        {
            this.ID = "";
            this.WF_ID = "";
            this.WF_SERIAL_NO = "";
            this.WF_TASK_ID = "";
            this.WF_INST_TASK_ID = "";
            this.WF_CAPTION = "";
            this.WF_NOTE = "";
            this.WF_SERVICE_CODE = "";
            this.WF_SERVICE_NAME = "";
            this.WF_PRIORITY = "";
            this.WF_STATE = "";
            this.WF_STARTTIME = "";
            this.WF_ENDTIME = "";
            this.WF_SUSPEND_TIME = "";
            this.WF_SUSPEND_STATE = "";
            this.WF_SUSPEND_ENDTIME = "";
            this.IS_SUB_FLOW = "";
            this.PARENT_INST_FLOW_ID = "";
            this.PARENT_FLOW_ID = "";
            this.PARENT_INST_TASK_ID = "";
            this.PARENT_TASK_ID = "";
            this.REMARK = "";

        }

        #region 属性
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string ID
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
        /// 流水号
        /// </summary>
        public string WF_SERIAL_NO
        {
            set;
            get;
        }
        /// <summary>
        /// 当前环节编号
        /// </summary>
        public string WF_TASK_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 当前实例环节编号
        /// </summary>
        public string WF_INST_TASK_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 流程简述
        /// </summary>
        public string WF_CAPTION
        {
            set;
            get;
        }
        /// <summary>
        /// 流程备注
        /// </summary>
        public string WF_NOTE
        {
            set;
            get;
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string WF_SERVICE_CODE
        {
            get;
            set;
        }

        /// <summary>
        /// 业务代码
        /// </summary>
        public string WF_SERVICE_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public string WF_PRIORITY
        {
            set;
            get;
        }
        /// <summary>
        /// 流程状态
        /// </summary>
        public string WF_STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string WF_STARTTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 约定结束时间
        /// </summary>
        public string WF_ENDTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 挂起时间
        /// </summary>
        public string WF_SUSPEND_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 挂起状态
        /// </summary>
        public string WF_SUSPEND_STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 挂起的结束时间
        /// </summary>
        public string WF_SUSPEND_ENDTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 是否子流程
        /// </summary>
        public string IS_SUB_FLOW
        {
            set;
            get;
        }
        /// <summary>
        /// 父流程实例编号
        /// </summary>
        public string PARENT_INST_FLOW_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 父流程编号
        /// </summary>
        public string PARENT_FLOW_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 父流程环节实例编号
        /// </summary>
        public string PARENT_INST_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 父流程环节编号
        /// </summary>
        public string PARENT_TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 其他备注
        /// </summary>
        public string REMARK
        {
            set;
            get;
        }


        #endregion

    }
}