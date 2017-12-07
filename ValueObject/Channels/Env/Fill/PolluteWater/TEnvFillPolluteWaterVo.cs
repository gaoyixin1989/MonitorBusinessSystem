using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：污染源常规（废水）填报
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_POLLUTE_WATER_TABLE = "T_ENV_FILL_POLLUTE_WATER";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string POINT_ID_FIELD = "POINT_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string ENTERPRISE_CODE_FIELD = "ENTERPRISE_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string ENTERPRISE_NAME_FIELD = "ENTERPRISE_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
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
        public static string HOUR_FIELD = "HOUR";
        /// <summary>
        /// 
        /// </summary>
        public static string SEASON_FIELD = "SEASON";
        /// <summary>
        /// 
        /// </summary>
        public static string TIMES_FIELD = "TIMES";
        /// <summary>
        /// 
        /// </summary>
        public static string WATERQTY_FIELD = "WATERQTY";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_RUN_FIELD = "IS_RUN";
        /// <summary>
        /// 
        /// </summary>
        public static string LOAD_MODE_FIELD = "LOAD_MODE";
        /// <summary>
        /// 
        /// </summary>
        public static string IN_WATER_QTY_FIELD = "IN_WATER_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_EVALUATE_FIELD = "IS_EVALUATE";
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
        /// <summary>
        /// 
        /// </summary>
        public static string WATER_NAME_FIELD = "WATER_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string WATER_CODE_FIELD = "WATER_CODE";
        #endregion

        public TEnvFillPolluteWaterVo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.ENTERPRISE_CODE = "";
            this.ENTERPRISE_NAME = "";
            this.YEAR = "";
            this.MONTH = "";
            this.DAY = "";
            this.HOUR = "";
            this.SEASON = "";
            this.TIMES = "";
            this.WATERQTY = "";
            this.IS_RUN = "";
            this.LOAD_MODE = "";
            this.IN_WATER_QTY = "";
            this.IS_EVALUATE = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.WATER_NAME = "";
            this.WATER_CODE = "";
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
        public string POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ENTERPRISE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ENTERPRISE_NAME
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
        public string HOUR
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
        public string TIMES
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string WATERQTY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IS_RUN
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string LOAD_MODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IN_WATER_QTY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IS_EVALUATE
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
        /// <summary>
        /// 
        /// </summary>
        public string WATER_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string WATER_CODE
        {
            set;
            get;
        }

        #endregion

    }

}
