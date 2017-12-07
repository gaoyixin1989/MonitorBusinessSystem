using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseRoadSummaryVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE = "T_ENV_FILL_NOISE_ROAD_SUMMARY";
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
        /// 道路交通干线数目
        /// </summary>
        public static string LINE_COUNT_FIELD = "LINE_COUNT";
        /// <summary>
        /// 有效测点数
        /// </summary>
        public static string VALID_COUNT_FIELD = "VALID_COUNT";
        /// <summary>
        /// 监测起始月
        /// </summary>
        public static string BEGIN_MONTH_FIELD = "BEGIN_MONTH";
        /// <summary>
        /// 监测起始日
        /// </summary>
        public static string BEGIN_DAY_FIELD = "BEGIN_DAY";
        /// <summary>
        /// 监测结束月
        /// </summary>
        public static string END_MONTH_FIELD = "END_MONTH";
        /// <summary>
        /// 监测结束日
        /// </summary>
        public static string END_DAY_FIELD = "END_DAY";
        /// <summary>
        /// 机动车拥有量
        /// </summary>
        public static string TRAFFIC_COUNT_FIELD = "TRAFFIC_COUNT";
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

        public TEnvFillNoiseRoadSummaryVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.LINE_COUNT = "";
            this.VALID_COUNT = "";
            this.BEGIN_MONTH = "";
            this.BEGIN_DAY = "";
            this.END_MONTH = "";
            this.END_DAY = "";
            this.TRAFFIC_COUNT = "";
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
        /// 道路交通干线数目
        /// </summary>
        public string LINE_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 有效测点数
        /// </summary>
        public string VALID_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 监测起始月
        /// </summary>
        public string BEGIN_MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 监测起始日
        /// </summary>
        public string BEGIN_DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 监测结束月
        /// </summary>
        public string END_MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 监测结束日
        /// </summary>
        public string END_DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 机动车拥有量
        /// </summary>
        public string TRAFFIC_COUNT
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
