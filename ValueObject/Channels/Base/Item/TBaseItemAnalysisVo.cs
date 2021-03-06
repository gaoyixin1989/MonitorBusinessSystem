using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemAnalysisVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_ITEM_ANALYSIS_TABLE = "T_BASE_ITEM_ANALYSIS";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测项目ID
		/// </summary>
		public static string ITEM_ID_FIELD = "ITEM_ID";
		/// <summary>
		/// 分析方法ID
		/// </summary>
		public static string ANALYSIS_METHOD_ID_FIELD = "ANALYSIS_METHOD_ID";
		/// <summary>
		/// 监测仪器ID
		/// </summary>
		public static string INSTRUMENT_ID_FIELD = "INSTRUMENT_ID";
		/// <summary>
		/// 单位
		/// </summary>
		public static string UNIT_FIELD = "UNIT";
		/// <summary>
		/// 小数点精度
		/// </summary>
		public static string PRECISION_FIELD = "PRECISION";
		/// <summary>
		/// 检测上限
		/// </summary>
		public static string UPPER_LIMIT_FIELD = "UPPER_LIMIT";
		/// <summary>
		/// 检测下限
		/// </summary>
		public static string LOWER_LIMIT_FIELD = "LOWER_LIMIT";
		/// <summary>
		/// 最低检出限
		/// </summary>
		public static string LOWER_CHECKOUT_FIELD = "LOWER_CHECKOUT";
		/// <summary>
		/// 0为不默认，1为默认
		/// </summary>
		public static string IS_DEFAULT_FIELD = "IS_DEFAULT";
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
		
		public TBaseItemAnalysisVo()
		{
			this.ID = "";
			this.ITEM_ID = "";
			this.ANALYSIS_METHOD_ID = "";
			this.INSTRUMENT_ID = "";
			this.UNIT = "";
			this.PRECISION = "";
			this.UPPER_LIMIT = "";
			this.LOWER_LIMIT = "";
			this.LOWER_CHECKOUT = "";
			this.IS_DEFAULT = "";
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
		/// 监测项目ID
		/// </summary>
		public string ITEM_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分析方法ID
		/// </summary>
		public string ANALYSIS_METHOD_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测仪器ID
		/// </summary>
		public string INSTRUMENT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string UNIT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 小数点精度
		/// </summary>
		public string PRECISION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检测上限
		/// </summary>
		public string UPPER_LIMIT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检测下限
		/// </summary>
		public string LOWER_LIMIT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最低检出限
		/// </summary>
		public string LOWER_CHECKOUT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 0为不默认，1为默认
		/// </summary>
		public string IS_DEFAULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 0为在使用、1为停用
		/// </summary>
        public string IS_DEL
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
	
		
		#endregion
		
    }
}