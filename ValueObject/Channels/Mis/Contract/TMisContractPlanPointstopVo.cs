using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：点位停产
    /// 创建日期：2013-03-13
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractPlanPointstopVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_CONTRACT_PLAN_POINTSTOP_TABLE = "T_MIS_CONTRACT_PLAN_POINTSTOP";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string CONTRACT_POINT_ID_FIELD = "CONTRACT_POINT_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string CONTRACT_COMPANY_ID_FIELD = "CONTRACT_COMPANY_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string STOPRESON_FIELD = "STOPRESON";
        /// <summary>
        /// 
        /// </summary>
        public static string ACTIONDATE_FIELD = "ACTIONDATE";
        /// <summary>
        /// 
        /// </summary>
        public static string ACTION_USERID_FIELD = "ACTION_USERID";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK3_FIELD = "REMARK3";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK4_FIELD = "REMARK4";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";

        #endregion

        public TMisContractPlanPointstopVo()
        {
            this.ID = "";
            this.CONTRACT_ID = "";
            this.CONTRACT_POINT_ID = "";
            this.CONTRACT_COMPANY_ID = "";
            this.STOPRESON = "";
            this.ACTIONDATE = "";
            this.ACTION_USERID = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CONTRACT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CONTRACT_POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CONTRACT_COMPANY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string STOPRESON
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACTIONDATE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACTION_USERID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }


        #endregion

    }
}