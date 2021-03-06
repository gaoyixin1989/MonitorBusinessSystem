using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.RPT
{
    /// <summary>
    /// 功能：模板标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptTemplateMarkVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_RPT_TEMPLATE_MARK_TABLE = "T_RPT_TEMPLATE_MARK";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 标签ID
		/// </summary>
		public static string MARK_ID_FIELD = "MARK_ID";
		/// <summary>
		/// 模板ID
		/// </summary>
		public static string TEMPLATE_ID_FIELD = "TEMPLATE_ID";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TRptTemplateMarkVo()
		{
			this.ID = "";
			this.MARK_ID = "";
			this.TEMPLATE_ID = "";
			this.REMARK = "";
			
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
		/// 标签ID
		/// </summary>
		public string MARK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 模板ID
		/// </summary>
		public string TEMPLATE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string REMARK
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}