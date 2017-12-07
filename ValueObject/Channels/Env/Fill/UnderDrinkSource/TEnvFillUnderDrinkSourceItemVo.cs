using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.UnderDrinkSource
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-26
    /// 创建人：
    /// </summary>
    public class TEnvFillUnderdrinkSrcItemVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_UNDERDRINK_SRC_ITEM_TABLE = "T_ENV_FILL_UNDERDRINK_SRC_ITEM";
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
        public static string JUDGE_FIELD = "JUDGE";
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

        public TEnvFillUnderdrinkSrcItemVo()
        {
            this.ID = "";
            this.FILL_ID = "";
            this.ITEM_ID = "";
            this.ITEM_VALUE = "";
            this.JUDGE = "";
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
        public string JUDGE
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
