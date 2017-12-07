using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：出差代理
    /// 创建日期：2012-11-15
    /// 创建人：潘德军
    /// </summary>
    public class TSysUserProxyVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_SYS_USER_PROXY_TABLE = "T_SYS_USER_PROXY";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string USER_ID_FIELD = "USER_ID";
        /// <summary>
        /// 被代理人ID
        /// </summary>
        public static string PROXY_USER_ID_FIELD = "PROXY_USER_ID";
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

        #endregion

        public TSysUserProxyVo()
        {
            this.ID = "";
            this.USER_ID = "";
            this.PROXY_USER_ID = "";
            this.REMARK = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";

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
        /// 用户编号
        /// </summary>
        public string USER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 被代理人ID
        /// </summary>
        public string PROXY_USER_ID
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


        #endregion

    }
}