using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Report
{
    /// <summary>
    /// 功能描述：监测信息管理
    /// 创建时间：2012-12-8
    /// 创建人：邵世卓
    /// </summary>
    public class ReportTestInfoVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        public static string REPORT_TEST_INFO_TABLE = "REPORT_TEST_INFO";
        //静态字段引用
        public static string ID_FIELD = "ID";
        public static string TEST_TYPE_HEADER_FIELD = "TEST_TYPE_HEADER";
        public static string TEST_TYPE_FIELD = "TEST_TYPE";
        public static string REPORT_SERIAL_HEADER_FIELD = "REPORT_SERIAL_HEADER";
        public static string REPORT_YEAR_FIELD = "REPORT_YEAR";
        public static string REPORT_YEAR_HEADER_FIELD = "REPORT_YEAR_HEADER";
        public static string REPORT_SERIAL_FIELD = "REPORT_SERIAL";
        public static string PROJECT_NAME_FIELD = "PROJECT_NAME";
        public static string CLIENT_COMPANY_FIELD = "CLIENT_COMPANY";
        public static string TESTED_COMPANY_FIELD = "TESTED_COMPANY";
        public static string CONTRACT_TYPE_FIELD = "CONTRACT_TYPE";
        public static string REPORT_DATE_FIELD = "REPORT_DATE";
        public static string TEST_PURPOSE_FIELD = "TEST_PURPOSE";
        public static string CUSTOMER_NAME_FIELD = "CUSTOMER_NAME";
        public static string OUTLET_AND_POINT_FIELD = "OUTLET_AND_POINT";
        public static string SAMPLE_METHOD_FIELD = "SAMPLE_METHOD";
        public static string SAMPLE_SERIAL_FIELD = "SAMPLE_SERIAL";
        public static string SAMPLE_TYPE_FIELD = "SAMPLE_TYPE";
        public static string SAMPLE_TIME_FIELD = "SAMPLE_TIME";
        public static string SAMPLE_USER_FIELD = "SAMPLE_USER";
        public static string ANALYSE_TIME_FIELD = "ANALYSE_TIME";
        public static string ANALYSE_USER_FIELD = "ANALYSE_USER";
        public static string SAMPLE_SEND_USER_FIELD = "SAMPLE_SEND_USER";
        public static string SAMPLE_RECEIVE_USER_FIELD = "SAMPLE_RECEIVE_USER";
        public static string WEATHER_INFO_FIELD = "WEATHER_INFO";
        public static string SOUND_POSITION_FIELD = "SOUND_POSITION";
        public static string SHAKE_POSITION_FIELD = "SHAKE_POSITION";
        public static string REMARK1_FIELD = "REMARK1";
        public static string REMARK2_FIELD = "REMARK2";
        public static string REMARK3_FIELD = "REMARK3";

        #endregion

        public ReportTestInfoVo()
        {
            this.ID = "";
            this.TEST_TYPE_HEADER = "";
            this.TEST_TYPE = "";
            this.REPORT_SERIAL_HEADER = "";
            this.REPORT_YEAR = "";
            this.REPORT_YEAR_HEADER = "";
            this.REPORT_SERIAL = "";
            this.PROJECT_NAME = "";
            this.CLIENT_COMPANY = "";
            this.TESTED_COMPANY = "";
            this.CONTRACT_TYPE = "";
            this.REPORT_DATE = "";
            this.TEST_PURPOSE = "";
            this.CUSTOMER_NAME = "";
            this.OUTLET_AND_POINT = "";
            this.SAMPLE_METHOD = "";
            this.SAMPLE_SERIAL = "";
            this.SAMPLE_TYPE = "";
            this.SAMPLE_TIME = "";
            this.SAMPLE_USER = "";
            this.ANALYSE_TIME = "";
            this.ANALYSE_USER = "";
            this.SAMPLE_SEND_USER = "";
            this.SAMPLE_RECEIVE_USER = "";
            this.WEATHER_INFO = "";
            this.SOUND_POSITION = "";
            this.SHAKE_POSITION = "";
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
        /// 页眉检测类型
        /// </summary>
        public string TEST_TYPE_HEADER
        {
            set;
            get;
        }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string TEST_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 页眉报告编号
        /// </summary>
        public string REPORT_SERIAL_HEADER
        {
            set;
            get;
        }
        /// <summary>
        /// 报告年度
        /// </summary>
        public string REPORT_YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 页眉报告年度
        /// </summary>
        public string REPORT_YEAR_HEADER
        {
            set;
            get;
        }
        /// <summary>
        /// 报告编号
        /// </summary>
        public string REPORT_SERIAL
        {
            set;
            get;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PROJECT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 委托单位
        /// </summary>
        public string CLIENT_COMPANY
        {
            set;
            get;
        }
        /// <summary>
        /// 检测单位
        /// </summary>
        public string TESTED_COMPANY
        {
            set;
            get;
        }
        /// <summary>
        /// 监测类别
        /// </summary>
        public string CONTRACT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 报告日期
        /// </summary>
        public string REPORT_DATE
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
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUSTOMER_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 监测位置
        /// </summary>
        public string OUTLET_AND_POINT
        {
            set;
            get;
        }
        /// <summary>
        /// 采样方式
        /// </summary>
        public string SAMPLE_METHOD
        {
            set;
            get;
        }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SAMPLE_SERIAL
        {
            set;
            get;
        }
        /// <summary>
        /// 样品类型
        /// </summary>
        public string SAMPLE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样时间
        /// </summary>
        public string SAMPLE_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 采样人员
        /// </summary>
        public string SAMPLE_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 分析时间
        /// </summary>
        public string ANALYSE_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 分析人员
        /// </summary>
        public string ANALYSE_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 送样人员
        /// </summary>
        public string SAMPLE_SEND_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 接样人员
        /// </summary>
        public string SAMPLE_RECEIVE_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 天气状况
        /// </summary>
        public string WEATHER_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 主要声源
        /// </summary>
        public string SOUND_POSITION
        {
            set;
            get;
        }
        /// <summary>
        /// 主要振源
        /// </summary>
        public string SHAKE_POSITION
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
