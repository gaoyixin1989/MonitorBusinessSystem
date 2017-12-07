using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPEnterinfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_P_ENTERINFO_TABLE = "T_ENV_P_ENTERINFO";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string ENTER_NAME_FIELD = "ENTER_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string ENTER_CODE_FIELD = "ENTER_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string PROVINCE_ID_FIELD = "PROVINCE_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string AREA_ID_FIELD = "AREA_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string LEVEL1_FIELD = "LEVEL1";
        /// <summary>
        /// 
        /// </summary>
        public static string LEVEL2_FIELD = "LEVEL2";
        /// <summary>
        /// 
        /// </summary>
        public static string LEVEL3_FIELD = "LEVEL3";
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
        public static string IS_DEL_FIELD = "IS_DEL";
        #endregion

        public TEnvPEnterinfoVo()
        {
            this.ID = "";
            this.ENTER_NAME = "";
            this.ENTER_CODE = "";
            this.PROVINCE_ID = "";
            this.AREA_ID = "";
            this.LEVEL1 = "";
            this.LEVEL2 = "";
            this.LEVEL3 = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.IS_DEL = "";
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
        ///  企业名称
        /// </summary>
        public string ENTER_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string ENTER_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 所属省份ID
        /// </summary>
        public string PROVINCE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 所在地区ID
        /// </summary>
        public string AREA_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 一级行业名称
        /// </summary>
        public string LEVEL1
        {
            set;
            get;
        }
        /// <summary>
        /// 二级行业名称
        /// </summary>
        public string LEVEL2
        {
            set;
            get;
        }
        /// <summary>
        /// 三级行业名称
        /// </summary>
        public string LEVEL3
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
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }

        #endregion

    }

}
