using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.Dust
{
    /// <summary>
    /// 功能：降尘数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillDustVo : i3.Core.ValueObject.ObjectBase
    {


        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_DUST_TABLE = "T_ENV_FILL_DUST";
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
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 月度
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 监测起始月
        /// </summary>
        public static string BEGIN_MONTH_FIELD = "BEGIN_MONTH";
        /// <summary>
        /// 监测起始日
        /// </summary>
        public static string BEGIN_DAY_FIELD = "BEGIN_DAY";
        /// <summary>
        /// 监测起始时
        /// </summary>
        public static string BEGIN_HOUR_FIELD = "BEGIN_HOUR";
        /// <summary>
        /// 监测起始分
        /// </summary>
        public static string BEGIN_MINUTE_FIELD = "BEGIN_MINUTE";
        /// <summary>
        /// 监测结束月
        /// </summary>
        public static string END_MONTH_FIELD = "END_MONTH";
        /// <summary>
        /// 监测结束日
        /// </summary>
        public static string END_DAY_FIELD = "END_DAY";
        /// <summary>
        /// 监测结束时
        /// </summary>
        public static string END_HOUR_FIELD = "END_HOUR";
        /// <summary>
        /// 监测结束分
        /// </summary>
        public static string END_MINUTE_FIELD = "END_MINUTE";
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

        public TEnvFillDustVo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.BEGIN_MONTH = "";
            this.BEGIN_DAY = "";
            this.BEGIN_HOUR = "";
            this.BEGIN_MINUTE = "";
            this.END_MONTH = "";
            this.END_DAY = "";
            this.END_HOUR = "";
            this.END_MINUTE = "";
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
        /// 年度
        /// </summary>
        public string YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 月度
        /// </summary>
        public string MONTH
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
        /// 监测起始时
        /// </summary>
        public string BEGIN_HOUR
        {
            set;
            get;
        }
        /// <summary>
        /// 监测起始分
        /// </summary>
        public string BEGIN_MINUTE
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
        /// 监测结束时
        /// </summary>
        public string END_HOUR
        {
            set;
            get;
        }
        /// <summary>
        /// 监测结束分
        /// </summary>
        public string END_MINUTE
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


        #region//暂不用

        #region 静态引用
        ////静态表格引用
        ///// <summary>
        ///// 对象对应的表格名称
        ///// </summary>
        //public static string T_ENV_FILL_DUST_TABLE = "T_ENV_FILL_DUST";
        ////静态字段引用
        ///// <summary>
        ///// 主键ID
        ///// </summary>
        //public static string ID_FIELD = "ID";
        ///// <summary>
        ///// 降尘监测点ID，对应T_BAS_POINT_DUST表主键
        ///// </summary>
        //public static string DUST_POINT_ID_FIELD = "DUST_POINT_ID";
        ///// <summary>
        ///// 一月
        ///// </summary>
        //public static string MONTH1_FIELD = "MONTH1";
        ///// <summary>
        ///// 二月
        ///// </summary>
        //public static string MONTH2_FIELD = "MONTH2";
        ///// <summary>
        ///// 三月
        ///// </summary>
        //public static string MONTH3_FIELD = "MONTH3";
        ///// <summary>
        ///// 四月
        ///// </summary>
        //public static string MONTH4_FIELD = "MONTH4";
        ///// <summary>
        ///// 五月
        ///// </summary>
        //public static string MONTH5_FIELD = "MONTH5";
        ///// <summary>
        ///// 六月
        ///// </summary>
        //public static string MONTH6_FIELD = "MONTH6";
        ///// <summary>
        ///// 七月
        ///// </summary>
        //public static string MONTH7_FIELD = "MONTH7";
        ///// <summary>
        ///// 八月
        ///// </summary>
        //public static string MONTH8_FIELD = "MONTH8";
        ///// <summary>
        ///// 九月
        ///// </summary>
        //public static string MONTH9_FIELD = "MONTH9";
        ///// <summary>
        ///// 十月
        ///// </summary>
        //public static string MONTH10_FIELD = "MONTH10";
        ///// <summary>
        ///// 十一月
        ///// </summary>
        //public static string MONTH11_FIELD = "MONTH11";
        ///// <summary>
        ///// 十二月
        ///// </summary>
        //public static string MONTH12_FIELD = "MONTH12";
        ///// <summary>
        ///// 备注1
        ///// </summary>
        //public static string REMARK1_FIELD = "REMARK1";
        ///// <summary>
        ///// 备注2
        ///// </summary>
        //public static string REMARK2_FIELD = "REMARK2";
        ///// <summary>
        ///// 备注3
        ///// </summary>
        //public static string REMARK3_FIELD = "REMARK3";
        ///// <summary>
        ///// 备注4
        ///// </summary>
        //public static string REMARK4_FIELD = "REMARK4";
        ///// <summary>
        ///// 备注5
        ///// </summary>
        //public static string REMARK5_FIELD = "REMARK5";

        #endregion

        #region//字段
        //public TEnvFillDustVo()
        //{
        //    this.ID = "";
        //    this.DUST_POINT_ID = "";
        //    this.MONTH1 = "";
        //    this.MONTH2 = "";
        //    this.MONTH3 = "";
        //    this.MONTH4 = "";
        //    this.MONTH5 = "";
        //    this.MONTH6 = "";
        //    this.MONTH7 = "";
        //    this.MONTH8 = "";
        //    this.MONTH9 = "";
        //    this.MONTH10 = "";
        //    this.MONTH11 = "";
        //    this.MONTH12 = "";
        //    this.REMARK1 = "";
        //    this.REMARK2 = "";
        //    this.REMARK3 = "";
        //    this.REMARK4 = "";
        //    this.REMARK5 = "";
        //}
        #endregion

        #region 属性
        ///// <summary>
        ///// 主键ID
        ///// </summary>
        //public string ID
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 降尘监测点ID，对应T_BAS_POINT_DUST表主键
        ///// </summary>
        //public string DUST_POINT_ID
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 一月
        ///// </summary>
        //public string MONTH1
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 二月
        ///// </summary>
        //public string MONTH2
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 三月
        ///// </summary>
        //public string MONTH3
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 四月
        ///// </summary>
        //public string MONTH4
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 五月
        ///// </summary>
        //public string MONTH5
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 六月
        ///// </summary>
        //public string MONTH6
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 七月
        ///// </summary>
        //public string MONTH7
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 八月
        ///// </summary>
        //public string MONTH8
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 九月
        ///// </summary>
        //public string MONTH9
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 十月
        ///// </summary>
        //public string MONTH10
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 十一月
        ///// </summary>
        //public string MONTH11
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 十二月
        ///// </summary>
        //public string MONTH12
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注1
        ///// </summary>
        //public string REMARK1
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注2
        ///// </summary>
        //public string REMARK2
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注3
        ///// </summary>
        //public string REMARK3
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注4
        ///// </summary>
        //public string REMARK4
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注5
        ///// </summary>
        //public string REMARK5
        //{
        //    set ;
        //    get ;
        //}


        #endregion
        #endregion

    }
}