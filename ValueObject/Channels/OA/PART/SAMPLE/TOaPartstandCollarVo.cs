using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品领用信息
    /// 创建日期：2013-09-13
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandCollarVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_PARTSTAND_COLLAR_TABLE = "T_OA_PARTSTAND_COLLAR";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 物料ID
        /// </summary>
        public static string SAMPLE_ID_FIELD = "SAMPLE_ID";
        /// <summary>
        /// 领用数量
        /// </summary>
        public static string USED_QUANTITY_FIELD = "USED_QUANTITY";
        /// <summary>
        /// 领用人ID
        /// </summary>
        public static string USER_ID_FIELD = "USER_ID";
        /// <summary>
        /// 领用日期
        /// </summary>
        public static string LASTIN_DATE_FIELD = "LASTIN_DATE";
        /// <summary>
        /// 领用理由
        /// </summary>
        public static string REASON_FIELD = "REASON";
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

        public TOaPartstandCollarVo()
        {
            this.ID = "";
            this.SAMPLE_ID = "";
            this.USED_QUANTITY = "";
            this.USER_ID = "";
            this.LASTIN_DATE = "";
            this.REASON = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

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
        /// 物料ID
        /// </summary>
        public string SAMPLE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 领用数量
        /// </summary>
        public string USED_QUANTITY
        {
            set;
            get;
        }
        /// <summary>
        /// 领用人ID
        /// </summary>
        public string USER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 领用日期
        /// </summary>
        public string LASTIN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 领用理由
        /// </summary>
        public string REASON
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
