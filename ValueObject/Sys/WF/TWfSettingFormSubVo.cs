using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程子表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormSubVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_FORM_SUB_TABLE = "T_WF_SETTING_FORM_SUB";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 子表单编号
		/// </summary>
		public static string UCS_ID_FIELD = "UCS_ID";
		/// <summary>
		/// 子表单简称
		/// </summary>
		public static string UCS_CAPTION_FIELD = "UCS_CAPTION";
		/// <summary>
		/// 子表单类型
		/// </summary>
		public static string UCS_TYPE_FIELD = "UCS_TYPE";
		/// <summary>
		/// 相对路径
		/// </summary>
		public static string UCS_PATH_FIELD = "UCS_PATH";
		/// <summary>
		/// 子表单全名
		/// </summary>
		public static string UCS_NAME_FIELD = "UCS_NAME";
		/// <summary>
		/// 子表单内编码
		/// </summary>
		public static string UCS_NOTE_FIELD = "UCS_NOTE";
		/// <summary>
		/// 子表单描述
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TWfSettingFormSubVo()
		{
			this.ID = "";
			this.UCS_ID = "";
			this.UCS_CAPTION = "";
			this.UCS_TYPE = "";
			this.UCS_PATH = "";
			this.UCS_NAME = "";
			this.UCS_NOTE = "";
			this.REMARK = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单编号
		/// </summary>
		public string UCS_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单简称
		/// </summary>
		public string UCS_CAPTION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单类型
		/// </summary>
		public string UCS_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 相对路径
		/// </summary>
		public string UCS_PATH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单全名
		/// </summary>
		public string UCS_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单内编码
		/// </summary>
		public string UCS_NOTE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单描述
		/// </summary>
		public string REMARK
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}