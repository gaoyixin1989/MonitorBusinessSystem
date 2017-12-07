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
    public class ReportTestPurposeVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        public static string REPORT_TEST_PURPOSE_TABLE = "REPORT_TEST_PURPOSE";
        //静态字段引用
        public static string ID_FIELD = "ID";
        public static string TEST_PURPOSE_FIELD = "TEST_PURPOSE";

        #endregion

        public ReportTestPurposeVo()
        {
            this.TEST_PURPOSE = "";
            this.ID = "";
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
        /// 监测目的
        /// </summary>
        public string TEST_PURPOSE
        {
            set;
            get;
        }

        #endregion
    }
}
