using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：常规污染源废气填报
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_POLLUTE_AIR_TABLE = "T_ENV_FILL_POLLUTE_AIR";
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
        public static string OQTY_FIELD = "OQTY";
        /// <summary>
        /// 
        /// </summary>
        public static string POLLUTEPER_FIELD = "POLLUTEPER";
        /// <summary>
        /// 
        /// </summary>
        public static string POLLUTECALPER_FIELD = "POLLUTECALPER";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_STANDARD_FIELD = "IS_STANDARD";
        /// <summary>
        /// 
        /// </summary>
        public static string AIRQTY_FIELD = "AIRQTY";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_DATE_FIELD = "MO_DATE";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_TYPE_FIELD = "FUEL_TYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_QTY_FIELD = "FUEL_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_MODEL_FIELD = "FUEL_MODEL";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_TECH_FIELD = "FUEL_TECH";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_FUEL_FIELD = "IS_FUEL";
        /// <summary>
        /// 
        /// </summary>
        public static string DISCHARGE_WAY_FIELD = "DISCHARGE_WAY";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_HOUR_QTY_FIELD = "MO_HOUR_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string LOAD_MODE_FIELD = "LOAD_MODE";
        /// <summary>
        /// 
        /// </summary>
        public static string POINT_TEMP_FIELD = "POINT_TEMP";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_RUN_FIELD = "IS_RUN";
        /// <summary>
        /// 
        /// </summary>
        public static string MEASURED_FIELD = "MEASURED";
        /// <summary>
        /// 
        /// </summary>
        public static string WASTE_AIR_QTY_FIELD = "WASTE_AIR_QTY";
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
        public static string SEASON_FIELD = "SEASON";
        /// <summary>
        /// 
        /// </summary>
        public static string TIMES_FIELD = "TIMES";
        #endregion

        public TEnvFillPolluteAirVo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.ENTERPRISE_CODE = "";
            this.ENTERPRISE_NAME = "";
            this.YEAR = "";
            this.MONTH = "";
            this.DAY = "";
            this.HOUR = "";
            this.OQTY = "";
            this.POLLUTEPER = "";
            this.POLLUTECALPER = "";
            this.IS_STANDARD = "";
            this.AIRQTY = "";
            this.MO_DATE = "";
            this.FUEL_TYPE = "";
            this.FUEL_QTY = "";
            this.FUEL_MODEL = "";
            this.FUEL_TECH = "";
            this.IS_FUEL = "";
            this.DISCHARGE_WAY = "";
            this.MO_HOUR_QTY = "";
            this.LOAD_MODE = "";
            this.POINT_TEMP = "";
            this.IS_RUN = "";
            this.MEASURED = "";
            this.WASTE_AIR_QTY = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.SEASON = "";
            this.TIMES = "";
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
        public string OQTY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string POLLUTEPER
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string POLLUTECALPER
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IS_STANDARD
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AIRQTY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MO_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FUEL_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FUEL_QTY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FUEL_MODEL
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FUEL_TECH
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IS_FUEL
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DISCHARGE_WAY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MO_HOUR_QTY
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
        public string POINT_TEMP
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
        public string MEASURED
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string WASTE_AIR_QTY
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
