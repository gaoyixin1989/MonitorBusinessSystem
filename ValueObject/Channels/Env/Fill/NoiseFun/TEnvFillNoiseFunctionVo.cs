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
    public class TEnvFillNoiseFunctionVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_NOISE_FUNCTION_TABLE = "T_ENV_FILL_NOISE_FUNCTION";
        //静态字段引用
        /// <summary>
        /// 主键ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测点ID
        /// </summary>
        public static string POINT_ID_FIELD = "POINT_ID";
        /// <summary>
        /// 季度
        /// </summary>
        public static string QUARTER_FIELD = "QUARTER";
        /// <summary>
        /// 测量时间
        /// </summary>
        public static string MEASURE_TIME_FIELD = "MEASURE_TIME";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 开始月
        /// </summary>
        public static string BEGIN_MONTH_FIELD = "BEGIN_MONTH";
        /// <summary>
        /// 开始日
        /// </summary>
        public static string BEGIN_DAY_FIELD = "BEGIN_DAY";
        /// <summary>
        /// 开始时
        /// </summary>
        public static string BEGIN_HOUR_FIELD = "BEGIN_HOUR";
        /// <summary>
        /// 开始分
        /// </summary>
        public static string BEGIN_MINUTE_FIELD = "BEGIN_MINUTE";
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

        #endregion

        public TEnvFillNoiseFunctionVo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.QUARTER = "";
            this.MEASURE_TIME = "";
            this.YEAR = "";
            this.BEGIN_MONTH = "";
            this.BEGIN_DAY = "";
            this.BEGIN_HOUR = "";
            this.BEGIN_MINUTE = "";
            this.JUDGE = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

        }

        #region 属性
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测点ID
        /// </summary>
        public string POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 季度
        /// </summary>
        public string QUARTER
        {
            set;
            get;
        }
        /// <summary>
        /// 测量时间
        /// </summary>
        public string MEASURE_TIME
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
        /// 开始月
        /// </summary>
        public string BEGIN_MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 开始日
        /// </summary>
        public string BEGIN_DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 开始时
        /// </summary>
        public string BEGIN_HOUR
        {
            set;
            get;
        }
        /// <summary>
        /// 开始分
        /// </summary>
        public string BEGIN_MINUTE
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


        #endregion

    }

}
