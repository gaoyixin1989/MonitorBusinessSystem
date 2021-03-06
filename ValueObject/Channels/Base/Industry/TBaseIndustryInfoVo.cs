using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Industry
{
    /// <summary>
    /// 功能：行业信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseIndustryInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_INDUSTRY_INFO_TABLE = "T_BASE_INDUSTRY_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 行业代码
		/// </summary>
		public static string INDUSTRY_CODE_FIELD = "INDUSTRY_CODE";
		/// <summary>
		/// 行业名称
		/// </summary>
		public static string INDUSTRY_NAME_FIELD = "INDUSTRY_NAME";
        /// <summary>
        /// 默认显示(1,显示）
        /// </summary>
        public static string IS_SHOW_FIELD = "IS_SHOW";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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
		
		public TBaseIndustryInfoVo()
		{
			this.ID = "";
			this.INDUSTRY_CODE = "";
			this.INDUSTRY_NAME = "";
            this.IS_SHOW = "";
            this.IS_DEL = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
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
		/// 行业代码
		/// </summary>
		public string INDUSTRY_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 行业名称
		/// </summary>
		public string INDUSTRY_NAME
		{
			set ;
			get ;
		}
        /// <summary>
        /// 默认显示(1,显示）
        /// </summary>
        public string IS_SHOW
        {
            set;
            get;
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
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
	
		
		#endregion
		
    }
}