using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.RPT
{
    /// <summary>
    /// 功能：标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptMarkVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_RPT_MARK_TABLE = "T_RPT_MARK";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 标签名称
		/// </summary>
		public static string MARK_NAME_FIELD = "MARK_NAME";
		/// <summary>
		/// 标签描述
		/// </summary>
		public static string MARK_DESC_FIELD = "MARK_DESC";
		/// <summary>
		/// 标签说明
		/// </summary>
		public static string MARK_REMARK_FIELD = "MARK_REMARK";
		/// <summary>
		/// 属性名称
		/// </summary>
		public static string ATTRIBUTE_NAME_FIELD = "ATTRIBUTE_NAME";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TRptMarkVo()
		{
			this.ID = "";
			this.MARK_NAME = "";
			this.MARK_DESC = "";
			this.MARK_REMARK = "";
			this.ATTRIBUTE_NAME = "";
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
		/// 标签名称
		/// </summary>
		public string MARK_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 标签描述
		/// </summary>
		public string MARK_DESC
		{
			set ;
			get ;
		}
		/// <summary>
		/// 标签说明
		/// </summary>
		public string MARK_REMARK
		{
			set ;
			get ;
		}
		/// <summary>
		/// 属性名称
		/// </summary>
		public string ATTRIBUTE_NAME
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