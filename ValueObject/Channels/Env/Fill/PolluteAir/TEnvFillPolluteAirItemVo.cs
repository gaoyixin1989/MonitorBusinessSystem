using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：常规污染源废气填报监测项目
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirItemVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_POLLUTE_AIR_ITEM_TABLE = "T_ENV_FILL_POLLUTE_AIR_ITEM";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string FILL_ID_FIELD = "FILL_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string ITEM_VALUE_FIELD = "ITEM_VALUE";
        /// <summary>
        /// 
        /// </summary>
        public static string UP_LINE_FIELD = "UP_LINE";
        /// <summary>
        /// 
        /// </summary>
        public static string DOWN_LINE_FIELD = "DOWN_LINE";
        /// <summary>
        /// 
        /// </summary>
        public static string UOM_FIELD = "UOM";
        /// <summary>
        /// 
        /// </summary>
        public static string STANDARD_FIELD = "STANDARD";
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

        public TEnvFillPolluteAirItemVo()
        {
            this.ID = "";
            this.FILL_ID = "";
            this.ITEM_ID = "";
            this.ITEM_VALUE = "";
            this.UP_LINE = "";
            this.DOWN_LINE = "";
            this.UOM = "";
            this.STANDARD = "";
            this.OQTY = "";
            this.POLLUTEPER = "";
            this.POLLUTECALPER = "";
            this.IS_STANDARD = "";
            this.AIRQTY = "";
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
        public string FILL_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ITEM_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ITEM_VALUE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string UP_LINE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOWN_LINE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string UOM
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string STANDARD
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
