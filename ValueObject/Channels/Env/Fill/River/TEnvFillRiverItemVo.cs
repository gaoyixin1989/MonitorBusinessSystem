using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.River
{
    /// <summary>
    /// 功能：河流填报
    /// 创建日期：2013-06-18
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverItemVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_RIVER_ITEM_TABLE = "T_ENV_FILL_RIVER_ITEM";
        //静态字段引用
        /// <summary>
        /// 主键ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 数据填报ID
        /// </summary>
        public static string FILL_ID_FIELD = "FILL_ID";
        /// <summary>
        /// 监测项ID
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";
        /// <summary>
        /// 监测值
        /// </summary>
        public static string ITEM_VALUE_FIELD = "ITEM_VALUE";
        /// <summary>
        /// 评价
        /// </summary>
        public static string JUDGE_FIELD = "JUDGE";
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

        public TEnvFillRiverItemVo()
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
        /// 主键ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 数据填报ID
        /// </summary>
        public string FILL_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测项ID
        /// </summary>
        public string ITEM_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测值
        /// </summary>
        public string ITEM_VALUE
        {
            set;
            get;
        }
        /// <summary>
        /// 评价
        /// </summary>
        public string JUDGE
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
