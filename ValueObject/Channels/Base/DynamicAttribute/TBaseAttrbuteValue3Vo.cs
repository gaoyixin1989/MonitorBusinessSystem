using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性值表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttrbuteValue3Vo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_BASE_ATTRBUTE_VALUE3_TABLE = "T_BASE_ATTRBUTE_VALUE3";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 对象类型
        /// </summary>
        public static string OBJECT_TYPE_FIELD = "OBJECT_TYPE";
        /// <summary>
        /// 对象ID
        /// </summary>
        public static string OBJECT_ID_FIELD = "OBJECT_ID";
        /// <summary>
        /// 属性名称
        /// </summary>
        public static string ATTRBUTE_CODE_FIELD = "ATTRBUTE_CODE";
        /// <summary>
        /// 属性值
        /// </summary>
        public static string ATTRBUTE_VALUE_FIELD = "ATTRBUTE_VALUE";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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

        public TBaseAttrbuteValue3Vo()
        {
            this.ID = "";
            this.OBJECT_TYPE = "";
            this.OBJECT_ID = "";
            this.ATTRBUTE_CODE = "";
            this.ATTRBUTE_VALUE = "";
            this.IS_DEL = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

        }

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string OBJECT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 对象ID
        /// </summary>
        public string OBJECT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string ATTRBUTE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public string ATTRBUTE_VALUE
        {
            set;
            get;
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
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