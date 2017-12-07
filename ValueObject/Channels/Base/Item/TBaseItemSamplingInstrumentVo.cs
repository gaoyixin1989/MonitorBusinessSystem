using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Item
{
    /// <summary>
    /// 功能：采样仪器管理
    /// 创建日期：2013-06-25
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemSamplingInstrumentVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_BASE_ITEM_SAMPLING_INSTRUMENT_TABLE = "T_BASE_ITEM_SAMPLING_INSTRUMENT";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";
        /// <summary>
        /// 0为不默认，1为默认
        /// </summary>
        public static string IS_DEFAULT_FIELD = "IS_DEFAULT";
        /// <summary>
        /// 采样仪器名称
        /// </summary>
        public static string INSTRUMENT_NAME_FIELD = "INSTRUMENT_NAME";
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

        public TBaseItemSamplingInstrumentVo()
        {
            this.ID = "";
            this.ITEM_ID = "";
            this.IS_DEFAULT = "";
            this.INSTRUMENT_NAME = "";
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
        /// 监测项目ID
        /// </summary>
        public string ITEM_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 0为不默认，1为默认
        /// </summary>
        public string IS_DEFAULT
        {
            set;
            get;
        }
        /// <summary>
        /// 采样仪器名称
        /// </summary>
        public string INSTRUMENT_NAME
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