using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Duty
{
    /// <summary>
    /// 功能：上岗资质管理
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysUserDutyVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_SYS_USER_DUTY_TABLE = "T_SYS_USER_DUTY";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 用户表ID
        /// </summary>
        public static string USERID_FIELD = "USERID";
        /// <summary>
        /// 已关联岗位职责ID
        /// </summary>
        public static string DUTY_ID_FIELD = "DUTY_ID";

        /// <summary>
        /// 是否默认负责人
        /// </summary>
        public static string IF_DEFAULT_FIELD = "IF_DEFAULT";
        /// <summary>
        /// 是否默认协同人
        /// </summary>
        public static string IF_DEFAULT_EX_FIELD = "IF_DEFAULT_EX";
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

        public TSysUserDutyVo()
        {
            this.ID = "";
            this.USERID = "";
            this.DUTY_ID = "";
            this.IF_DEFAULT = "";
            this.IF_DEFAULT_EX = "";
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
        /// 用户表ID
        /// </summary>
        public string USERID
        {
            set;
            get;
        }
        /// <summary>
        /// 已关联岗位职责ID
        /// </summary>
        public string DUTY_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 是否默认负责人
        /// </summary>
        public string IF_DEFAULT
        {
            set;
            get;
        }
        /// <summary>
        /// 是否默认协同人
        /// </summary>
        public string IF_DEFAULT_EX
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