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
    public class ReportTestItemVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        public static string REPORT_TEST_ITEM_TABLE = "REPORT_TEST_ITEM";
        //静态字段引用
        public static string ID_FIELD = "ID";
        public static string TYPE_CODE_FIELD = "TYPE_CODE";
        public static string TYPE_NAME_FIELD = "TYPE_NAME";
        public static string TEST_ITEM_FIELD = "TEST_ITEM";
        public static string TEST_METHOD_FIELD = "TEST_METHOD";
        public static string TEST_DEVICE_FIELD = "TEST_DEVICE";
        public static string TEST_MIN_FIELD = "TEST_MIN";
        public static string REMARK1_FIELD = "REMARK1";
        public static string REMARK2_FIELD = "REMARK2";
        public static string REMARK3_FIELD = "REMARK3";

        #endregion

        public ReportTestItemVo()
        {
            this.ID = "";
            this.TYPE_CODE = "";
            this.TYPE_NAME = "";
            this.TEST_ITEM = "";
            this.TEST_METHOD = "";
            this.TEST_DEVICE = "";
            this.TEST_MIN = "";
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
        /// 监测类型编码
        /// </summary>
        public string TYPE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 监测类型名称
        /// </summary>
        public string TYPE_NAME
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
        /// 监测方法依据
        /// </summary>
        public string TEST_METHOD
        {
            set;
            get;
        }
        /// <summary>
        /// 监测仪器
        /// </summary>
        public string TEST_DEVICE
        {
            set;
            get;
        }
        /// <summary>
        /// 最低检出限
        /// </summary>
        public string TEST_MIN
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
