using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：环境质量质控设置监测项目信息
    /// 创建日期：2013-06-25
    /// 创建人：胡方扬
    /// </summary>
    public class TMisPointitemQcsettingVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_POINTITEM_QCSETTING_TABLE = "T_MIS_POINTITEM_QCSETTING";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 环境质量质控计划ID
        /// </summary>
        public static string POINT_QCSETTING_ID_FIELD = "POINT_QCSETTING_ID";
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";

        /// <summary>
        /// 监测项目名称
        /// </summary>
        public static string ITEM_NAME_FIELD = "ITEM_NAME";
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public static string QC_TYPE_FIELD = "QC_TYPE";
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

        public TMisPointitemQcsettingVo()
        {
            this.ID = "";
            this.POINT_QCSETTING_ID = "";
            this.ITEM_ID = "";
            this.ITEM_NAME = "";
            this.QC_TYPE = "";
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
        /// 环境质量质控计划ID
        /// </summary>
        public string POINT_QCSETTING_ID
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
        /// 监测项目名称
        /// </summary>
        public string ITEM_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 现场平行0，现场空白1
        /// </summary>
        public string QC_TYPE
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