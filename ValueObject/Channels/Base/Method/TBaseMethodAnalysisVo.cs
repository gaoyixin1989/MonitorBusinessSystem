using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Method
{
    /// <summary>
    /// 功能：分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMethodAnalysisVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_METHOD_ANALYSIS_TABLE = "T_BASE_METHOD_ANALYSIS";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 分析方法名称
		/// </summary>
		public static string ANALYSIS_NAME_FIELD = "ANALYSIS_NAME";
		/// <summary>
		/// 分析方法描述
		/// </summary>
		public static string DESCRIPTION_FIELD = "DESCRIPTION";
		/// <summary>
		/// 方法依据ID
		/// </summary>
		public static string METHOD_ID_FIELD = "METHOD_ID";
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
		
		public TBaseMethodAnalysisVo()
		{
			this.ID = "";
			this.ANALYSIS_NAME = "";
			this.DESCRIPTION = "";
			this.METHOD_ID = "";
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
		/// 分析方法名称
		/// </summary>
		public string ANALYSIS_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分析方法描述
		/// </summary>
		public string DESCRIPTION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 方法依据ID
		/// </summary>
		public string METHOD_ID
		{
			set ;
			get ;
		}
		/// <summary>
        /// 删除标记
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