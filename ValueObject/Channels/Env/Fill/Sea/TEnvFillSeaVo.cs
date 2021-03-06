using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.Sea
{
    /// <summary>
    /// 功能：近海海域数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillSeaVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_SEA_TABLE = "T_ENV_FILL_SEA";
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
        /// 采样日期
        /// </summary>
        public static string SAMPLING_DAY_FIELD = "SAMPLING_DAY";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 月份
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 日
        /// </summary>
        public static string DAY_FIELD = "DAY";
        /// <summary>
        /// 时
        /// </summary>
        public static string HOUR_FIELD = "HOUR";
        /// <summary>
        /// 分
        /// </summary>
        public static string MINUTE_FIELD = "MINUTE";
        /// <summary>
        /// 海区代码
        /// </summary>
        public static string SEA_AREA_CODE_FIELD = "SEA_AREA_CODE";
        /// <summary>
        /// 重点海域代码
        /// </summary>
        public static string KEY_AREA_CODE_FIELD = "KEY_AREA_CODE";
        /// <summary>
        /// 水深
        /// </summary>
        public static string DEPTH_FIELD = "DEPTH";
        /// <summary>
        /// 枯水期、平水期、枯水期
        /// </summary>
        public static string KPF_FIELD = "KPF";
        /// <summary>
        /// 层次
        /// </summary>
        public static string LEVEL_FIELD = "LEVEL";
        /// <summary>
        /// 评价
        /// </summary>
        public static string JUDGE_FIELD = "JUDGE";
        /// <summary>
        /// 超标污染物
        /// </summary>
        public static string OVERPROOF_FIELD = "OVERPROOF";
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

        public TEnvFillSeaVo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.SAMPLING_DAY = "";
            this.YEAR = "";
            this.MONTH = "";
            this.DAY = "";
            this.HOUR = "";
            this.MINUTE = "";
            this.SEA_AREA_CODE = "";
            this.KEY_AREA_CODE = "";
            this.DEPTH = "";
            this.KPF = "";
            this.LEVEL = "";
            this.JUDGE = "";
            this.OVERPROOF = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.STATUS = "";
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
        /// 采样日期
        /// </summary>
        public string SAMPLING_DAY
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
        /// 月份
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 日
        /// </summary>
        public string DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 时
        /// </summary>
        public string HOUR
        {
            set;
            get;
        }
        /// <summary>
        /// 分
        /// </summary>
        public string MINUTE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SEA_AREA_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string KEY_AREA_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 水深
        /// </summary>
        public string DEPTH
        {
            set;
            get;
        }
        /// <summary>
        /// 枯水期、平水期、枯水期
        /// </summary>
        public string KPF
        {
            set;
            get;
        }
        /// <summary>
        /// 层次
        /// </summary>
        public string LEVEL
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
        /// 超标污染物
        /// </summary>
        public string OVERPROOF
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