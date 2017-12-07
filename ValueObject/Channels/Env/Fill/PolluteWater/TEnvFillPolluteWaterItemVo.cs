using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：污染源常规（废水）填报监测项目
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterItemVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_POLLUTE_WATER_ITEM_TABLE = "T_ENV_FILL_POLLUTE_WATER_ITEM";
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
        public static string UPLINE_FIELD = "UPLINE";
        /// <summary>
        /// 
        /// </summary>
        public static string DOWNLINE_FIELD = "DOWNLINE";
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
        public static string ITEM_VALUE_FIELD = "ITEM_VALUE";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_STANDARD_FIELD = "IS_STANDARD";
        /// <summary>
        /// 
        /// </summary>
        public static string WATER_PER_FIELD = "WATER_PER";
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

        public TEnvFillPolluteWaterItemVo()
        {
            this.ID = "";
            this.FILL_ID = "";
            this.ITEM_ID = "";
            this.UPLINE = "";
            this.DOWNLINE = "";
            this.UOM = "";
            this.STANDARD = "";
            this.ITEM_VALUE = "";
            this.IS_STANDARD = "";
            this.WATER_PER = "";
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
        public string UPLINE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOWNLINE
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
        public string ITEM_VALUE
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
        public string WATER_PER
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
