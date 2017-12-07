using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill
{
    /// <summary>
    /// 功能：环境质量附件信息表
    /// 创建日期：2014-08-04
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillAttVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_ATT_TABLE = "T_ENV_FILL_ATT";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 
        /// </summary>
        public static string SEASON_FIELD = "SEASON";
        /// <summary>
        /// 
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 
        /// </summary>
        public static string DAY_FIELD = "DAY";
        /// <summary>
        /// 
        /// </summary>
        public static string ENVTYPE_FIELD = "ENVTYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK_FIELD = "REMARK";
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

        public TEnvFillAttVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.SEASON = "";
            this.MONTH = "";
            this.DAY = "";
            this.ENVTYPE = "";
            this.REMARK = "";
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
        public string YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEASON
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ENVTYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK
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
