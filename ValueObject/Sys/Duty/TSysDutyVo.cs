using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Duty
{
    /// <summary>
    /// 功能：岗位职责
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysDutyVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_SYS_DUTY_TABLE = "T_SYS_DUTY";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测类别ID
        /// </summary>
        public static string MONITOR_TYPE_ID_FIELD = "MONITOR_TYPE_ID";
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public static string MONITOR_ITEM_ID_FIELD = "MONITOR_ITEM_ID";

        /// <summary>
        /// 岗位职责编码(字典项目获取)
        /// </summary>
        public static string DICT_CODE_FIELD = "DICT_CODE";
        /// <summary>
        /// 所有者
        /// </summary>
        public static string OWNER_FIELD = "OWNER";
        /// <summary>
        /// 备注
        /// </summary>
        public static string REMARK_FIELD = "REMARK";
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

        public TSysDutyVo()
        {
            this.ID = "";
            this.MONITOR_TYPE_ID = "";
            this.MONITOR_ITEM_ID = "";
            this.DICT_CODE = "";
            this.OWNER = "";
            this.REMARK = "";
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
        /// 监测类别ID
        /// </summary>
        public string MONITOR_TYPE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public string MONITOR_ITEM_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 岗位职责编码(字典项目获取)
        /// </summary>
        public string DICT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 所有者
        /// </summary>
        public string OWNER
        {
            set;
            get;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
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