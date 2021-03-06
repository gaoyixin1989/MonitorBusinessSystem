using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程配置主表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFlowVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_SETTING_FLOW_TABLE = "T_WF_SETTING_FLOW";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 工作流编号
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 工作流简称
        /// </summary>
        public static string WF_CAPTION_FIELD = "WF_CAPTION";
        /// <summary>
        /// 类别归属
        /// </summary>
        public static string WF_CLASS_ID_FIELD = "WF_CLASS_ID";
        /// <summary>
        /// 生成的版本
        /// </summary>
        public static string WF_VERSION_FIELD = "WF_VERSION";
        /// <summary>
        /// 存在状态
        /// </summary>
        public static string WF_STATE_FIELD = "WF_STATE";
        /// <summary>
        /// 工作流描述
        /// </summary>
        public static string WF_NOTE_FIELD = "WF_NOTE";
        /// <summary>
        /// 主表单
        /// </summary>
        public static string WF_FORM_MAIN_FIELD = "WF_FORM_MAIN";
        /// <summary>
        /// 创建人
        /// </summary>
        public static string CREATE_USER_FIELD = "CREATE_USER";
        /// <summary>
        /// 创建日期
        /// </summary>
        public static string CREATE_DATE_FIELD = "CREATE_DATE";
        /// <summary>
        /// 处理类型
        /// </summary>
        public static string DEAL_TYPE_FIELD = "DEAL_TYPE";
        /// <summary>
        /// 删除人
        /// </summary>
        public static string DEAL_USER_FIELD = "DEAL_USER";
        /// <summary>
        /// 删除日期
        /// </summary>
        public static string DEAL_DATE_FIELD = "DEAL_DATE";

        /// <summary>
        /// 首环节跳转页面
        /// </summary>
        public static string FSTEP_RETURN_URL_FIELD = "FSTEP_RETURN_URL";

        /// <summary>
        /// 备注
        /// </summary>
        public static string REMARK_FIELD = "REMARK";

        #endregion

        public TWfSettingFlowVo()
        {
            this.ID = "";
            this.WF_ID = "";
            this.WF_CAPTION = "";
            this.WF_CLASS_ID = "";
            this.WF_VERSION = "";
            this.WF_STATE = "";
            this.WF_NOTE = "";
            this.WF_FORM_MAIN = "";
            this.CREATE_USER = "";
            this.CREATE_DATE = "";
            this.DEAL_TYPE = "";
            this.DEAL_USER = "";
            this.DEAL_DATE = "";
            this.FSTEP_RETURN_URL = "";
            this.REMARK = "";

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
        /// 工作流编号
        /// </summary>
        public string WF_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 工作流简称
        /// </summary>
        public string WF_CAPTION
        {
            set;
            get;
        }
        /// <summary>
        /// 类别归属
        /// </summary>
        public string WF_CLASS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 生成的版本
        /// </summary>
        public string WF_VERSION
        {
            set;
            get;
        }
        /// <summary>
        /// 存在状态
        /// </summary>
        public string WF_STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 工作流描述
        /// </summary>
        public string WF_NOTE
        {
            set;
            get;
        }
        /// <summary>
        /// 主表单
        /// </summary>
        public string WF_FORM_MAIN
        {
            set;
            get;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CREATE_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CREATE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 处理类型
        /// </summary>
        public string DEAL_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 删除人
        /// </summary>
        public string DEAL_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 删除日期
        /// </summary>
        public string DEAL_DATE
        {
            set;
            get;
        }

        /// <summary>
        /// 首环节跳转页面
        /// </summary>
        public string FSTEP_RETURN_URL
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            set;
            get;
        }


        #endregion

    }
}