using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseFunctionSummaryVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE = "T_ENV_FILL_NOISE_FUNCTION_SUMMARY";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 季度
        /// </summary>
        public static string QUTER_FIELD = "QUTER";
        /// <summary>
        /// 监测点ID
        /// </summary>
        public static string POINT_CODE_FIELD = "POINT_CODE";
        /// <summary>
        /// 气象条件
        /// </summary>
        public static string WEATHER_FIELD = "WEATHER";
        /// <summary>
        /// 监测日期(月)
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 监测日期(日)
        /// </summary>
        public static string DAY_FIELD = "DAY";
        /// <summary>
        /// 白昼均值
        /// </summary>
        public static string LD_FIELD = "LD";
        /// <summary>
        /// 夜间均值
        /// </summary>
        public static string LN_FIELD = "LN";
        /// <summary>
        /// 日均值
        /// </summary>
        public static string LDN_FIELD = "LDN";
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
        /// <summary>
        /// 状态(0或空：登记  1：待审核  2：待签发  9：已归档 )
        /// </summary>
        public static string STATUS_FIELD = "STATUS";

        #endregion

        public TEnvFillNoiseFunctionSummaryVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.QUTER = "";
            this.POINT_CODE = "";
            this.WEATHER = "";
            this.MONTH = "";
            this.DAY = "";
            this.LD = "";
            this.LN = "";
            this.LDN = "";
            this.JUDGE = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.STATUS = "";
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
        /// 年度
        /// </summary>
        public string YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 季度
        /// </summary>
        public string QUTER
        {
            set;
            get;
        }
        /// <summary>
        /// 监测点ID
        /// </summary>
        public string POINT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 气象条件
        /// </summary>
        public string WEATHER
        {
            set;
            get;
        }
        /// <summary>
        /// 监测日期(月)
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 监测日期(日)
        /// </summary>
        public string DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 白昼均值
        /// </summary>
        public string LD
        {
            set;
            get;
        }
        /// <summary>
        /// 夜间均值
        /// </summary>
        public string LN
        {
            set;
            get;
        }
        /// <summary>
        /// 日均值
        /// </summary>
        public string LDN
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
        /// <summary>
        /// 状态(0或空：登记  1：待审核  2：待签发  9：已归档 )
        /// </summary>
        public string STATUS
        {
            set;
            get;
        }

        #endregion

    }

}
