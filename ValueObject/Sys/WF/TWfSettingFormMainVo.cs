using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程主表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormMainVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_FORM_MAIN_TABLE = "T_WF_SETTING_FORM_MAIN";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 主表单编号
		/// </summary>
		public static string UCM_ID_FIELD = "UCM_ID";
		/// <summary>
		/// 主表单简称
		/// </summary>
		public static string UCM_CAPTION_FIELD = "UCM_CAPTION";
		/// <summary>
		/// 主表单内编码
		/// </summary>
		public static string UCM_NOTE_FIELD = "UCM_NOTE";
		/// <summary>
		/// 主表单类型
		/// </summary>
		public static string UCM_TYPE_FIELD = "UCM_TYPE";
		/// <summary>
		/// 主表单描述
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TWfSettingFormMainVo()
		{
			this.ID = "";
			this.UCM_ID = "";
			this.UCM_CAPTION = "";
			this.UCM_NOTE = "";
			this.UCM_TYPE = "";
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
		/// 主表单编号
		/// </summary>
		public string UCM_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主表单简称
		/// </summary>
		public string UCM_CAPTION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主表单内编码
		/// </summary>
		public string UCM_NOTE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主表单类型
		/// </summary>
		public string UCM_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主表单描述
		/// </summary>
		public string REMARK
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}