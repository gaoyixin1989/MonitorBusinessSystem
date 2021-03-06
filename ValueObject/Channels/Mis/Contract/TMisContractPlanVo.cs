using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测预约表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_PLAN_TABLE = "T_MIS_CONTRACT_PLAN";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 委托书ID
		/// </summary>
		public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
		/// <summary>
		/// 受检企业ID
		/// </summary>
		public static string CONTRACT_COMPANY_ID_FIELD = "CONTRACT_COMPANY_ID";
		/// <summary>
		/// 年度
		/// </summary>
		public static string PLAN_YEAR_FIELD = "PLAN_YEAR";
		/// <summary>
		/// 月份
		/// </summary>
		public static string PLAN_MONTH_FIELD = "PLAN_MONTH";
		/// <summary>
		/// 日期
		/// </summary>
		public static string PLAN_DAY_FIELD = "PLAN_DAY";
		/// <summary>
		/// 是否已执行
		/// </summary>
		public static string HAS_DONE_FIELD = "HAS_DONE";
		/// <summary>
        /// 执行日期
		/// </summary>
        public static string DELATE_INFO_FIELD = "DONE_DATE";
		/// <summary>
		/// REAMRK1
		/// </summary>
		public static string REAMRK1_FIELD = "REAMRK1";
		/// <summary>
		/// REAMRK2
		/// </summary>
		public static string REAMRK2_FIELD = "REAMRK2";
		/// <summary>
		/// REAMRK3
		/// </summary>
		public static string REAMRK3_FIELD = "REAMRK3";
		/// <summary>
		/// REAMRK4
		/// </summary>
		public static string REAMRK4_FIELD = "REAMRK4";
		/// <summary>
		/// REAMRK5
		/// </summary>
		public static string REAMRK5_FIELD = "REAMRK5";
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public static string CCFLOW_ID1_FIELD = "CCFLOW_ID1";
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public static string CCFLOW_ID2_FIELD = "CCFLOW_ID2";
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public static string CCFLOW_ID3_FIELD = "CCFLOW_ID3";
		
		#endregion
		
		public TMisContractPlanVo()
		{
			this.ID = "";
			this.CONTRACT_ID = "";
			this.CONTRACT_COMPANY_ID = "";

			this.PLAN_YEAR = "";
			this.PLAN_MONTH = "";
			this.PLAN_DAY = "";
			this.HAS_DONE = "";
			this.DONE_DATE = "";
            this.PLAN_NUM = "";
            this.PLAN_TYPE = "";
			this.REAMRK1 = "";
			this.REAMRK2 = "";
			this.REAMRK3 = "";
			this.REAMRK4 = "";
			this.REAMRK5 = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// ID
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托书ID
		/// </summary>
		public string CONTRACT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 受检企业ID
		/// </summary>
		public string CONTRACT_COMPANY_ID
		{
			set ;
			get ;
		}


		/// <summary>
        /// 计划年度
		/// </summary>
		public string PLAN_YEAR
		{
			set ;
			get ;
		}
		/// <summary>
        /// 计划月份
		/// </summary>
		public string PLAN_MONTH
		{
			set ;
			get ;
		}
		/// <summary>
        /// 计划日期
		/// </summary>
		public string PLAN_DAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否已执行
		/// </summary>
		public string HAS_DONE
		{
			set ;
			get ;
		}
        /// <summary>
        /// 执行日期
        /// </summary>
        public string DONE_DATE
        {
            set;
            get;
        }

        /// <summary>
        /// 执行频次
        /// </summary>
        public string PLAN_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 预约类型，环境质量为（01-17）,污染源为空
        /// </summary>
        public string PLAN_TYPE
        {
            set;
            get;
        }
		/// <summary>
		/// REAMRK1
		/// </summary>
		public string REAMRK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// REAMRK2
		/// </summary>
		public string REAMRK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// REAMRK3
		/// </summary>
		public string REAMRK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// REAMRK4
		/// </summary>
		public string REAMRK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// REAMRK5
		/// </summary>
		public string REAMRK5
		{
			set ;
			get ;
		}
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public string CCFLOW_ID1
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public string CCFLOW_ID2
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public string CCFLOW_ID3
        {
            set;
            get;
        }
		
		#endregion
		
    }
}