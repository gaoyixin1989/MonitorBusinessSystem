﻿using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.Alkali
{
    /// <summary>
    /// 功能：碳酸盐化速率填报
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// Modify by 刘静楠,  2013/6/21
    /// </summary>
    public class TEnvFillAlkaliVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_ENV_FILL_ALKALI_TABLE = "T_ENV_FILL_ALKALI";
		//静态字段引用
		/// <summary>
		/// 主键ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 年度
		/// </summary>
		public static string YEAR_FIELD = "YEAR";
		/// <summary>
		/// 监测点ID
		/// </summary>
		public static string POINT_ID_FIELD = "POINT_ID";
        /// <summary>
        /// 月度
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
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
		/// 结束月
		/// </summary>
		public static string END_MONTH_FIELD = "END_MONTH";
		/// <summary>
		/// 结束日
		/// </summary>
		public static string END_DAY_FIELD = "END_DAY";
		/// <summary>
		/// 结束时
		/// </summary>
		public static string END_HOUR_FIELD = "END_HOUR";
		/// <summary>
		/// 结束分
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
		
		public TEnvFillAlkaliVo()
		{
			this.ID = "";
			this.YEAR = "";
			this.POINT_ID = "";
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
			set ;
			get ;
		}
		/// <summary>
		/// 年度
		/// </summary>
		public string YEAR
		{
			set ;
			get ;
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
		/// 监测点ID
		/// </summary>
		public string POINT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始月
		/// </summary>
		public string BEGIN_MONTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始日
		/// </summary>
		public string BEGIN_DAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始时
		/// </summary>
		public string BEGIN_HOUR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始分
		/// </summary>
		public string BEGIN_MINUTE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束月
		/// </summary>
		public string END_MONTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束日
		/// </summary>
		public string END_DAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束时
		/// </summary>
		public string END_HOUR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束分
		/// </summary>
		public string END_MINUTE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注1
		/// </summary>
		public string REMARK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注2
		/// </summary>
		public string REMARK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注4
		/// </summary>
		public string REMARK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注5
		/// </summary>
		public string REMARK5
		{
			set ;
			get ;
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
