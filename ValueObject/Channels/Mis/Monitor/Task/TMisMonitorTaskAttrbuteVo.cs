using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务属性值表
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskAttrbuteVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_TASK_ATTRBUTE_TABLE = "T_MIS_MONITOR_TASK_ATTRBUTE";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 对象类型
        /// </summary>
        public static string OBJECT_TYPE_FIELD = "OBJECT_TYPE";
        /// <summary>
        /// 对象ID
        /// </summary>
        public static string OBJECT_ID_FIELD = "OBJECT_ID";
        /// <summary>
        /// 属性名称
        /// </summary>
        public static string ATTRBUTE_CODE_FIELD = "ATTRBUTE_CODE";
        /// <summary>
        /// 属性值
        /// </summary>
        public static string ATTRBUTE_VALUE_FIELD = "ATTRBUTE_VALUE";
        /// <summary>
        /// 是否删除
        /// </summary>
        public static string IS_USE_FIELD = "IS_DEL";
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

        #endregion

        public TMisMonitorTaskAttrbuteVo()
        {
            this.ID = "";
            this.OBJECT_TYPE = "";
            this.OBJECT_ID = "";
            this.ATTRBUTE_CODE = "";
            this.ATTRBUTE_VALUE = "";
            this.IS_DEL = "";
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
        /// 对象类型
        /// </summary>
        public string OBJECT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 对象ID
        /// </summary>
        public string OBJECT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string ATTRBUTE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public string ATTRBUTE_VALUE
        {
            set;
            get;
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IS_DEL
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


        #endregion

    }
}