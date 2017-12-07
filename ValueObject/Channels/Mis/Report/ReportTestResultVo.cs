using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Report
{
    /// <summary>
    /// 功能描述：监测项目信息查询
    /// 创建时间：2012-12-8
    /// 创建人：邵世卓
    /// </summary>
    public class ReportTestResultVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        public static string REPORT_TEST_RESULT_TABLE = "REPORT_TEST_RESULT";
        //静态字段引用
        public static string ID_FIELD = "ID";
        public static string TEST_TYPE_FIELD = "TEST_TYPE";
        public static string TEST_TYPE_NAME_FIELD = "TEST_TYPE_NAME";
        public static string POINT_ID_FIELD = "POINT_ID";
        public static string POINT_NAME_FIELD = "POINT_NAME";
        public static string SAMPLE_ID_FIELD = "SAMPLE_ID";
        public static string SAMPLE_NAME_FIELD = "SAMPLE_NAME";
        public static string TEST_ITEM_FIELD = "TEST_ITEM";
        public static string TEST_RESULT_FIELD = "TEST_RESULT";
        public static string REMARK1_FIELD = "REMARK1";
        public static string REMARK2_FIELD = "REMARK2";
        public static string REMARK3_FIELD = "REMARK3";

        #endregion

        public ReportTestResultVo()
        {
            this.ID = "";
            this.TEST_TYPE = "";
            this.TEST_TYPE_NAME = "";
            this.POINT_ID = "";
            this.POINT_NAME = "";
            this.SAMPLE_ID = "";
            this.SAMPLE_NAME = "";
            this.TEST_ITEM = "";
            this.TEST_RESULT = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
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
        /// 监测类别编号
        /// </summary>
        public string TEST_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 监测类别名称
        /// </summary>
        public string TEST_TYPE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 点位编号
        /// </summary>
        public string POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 点位名称
        /// </summary>
        public string POINT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SAMPLE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string SAMPLE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 监测项目
        /// </summary>
        public string TEST_ITEM
        {
            set;
            get;
        }
        /// <summary>
        /// 监测结果
        /// </summary>
        public string TEST_RESULT
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

        #endregion
    }
}
