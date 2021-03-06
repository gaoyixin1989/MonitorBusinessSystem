using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程表单列表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormListVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_FORM_LIST_TABLE = "T_WF_SETTING_FORM_LIST";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 列表编号
		/// </summary>
		public static string UC_LIST_ID_FIELD = "UC_LIST_ID";
		/// <summary>
		/// 主表单编号
		/// </summary>
		public static string UCM_ID_FIELD = "UCM_ID";
		/// <summary>
		/// 子表单编号
		/// </summary>
		public static string UCS_ID_FIELD = "UCS_ID";
		/// <summary>
		/// 内部排序
		/// </summary>
		public static string CTRL_ORDER_FIELD = "CTRL_ORDER";
		/// <summary>
		/// 子表单状态
		/// </summary>
		public static string CTRL_STATE_FIELD = "CTRL_STATE";
		
		#endregion
		
		public TWfSettingFormListVo()
		{
			this.ID = "";
			this.UC_LIST_ID = "";
			this.UCM_ID = "";
			this.UCS_ID = "";
			this.CTRL_ORDER = "";
			this.CTRL_STATE = "";
			
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
		/// 列表编号
		/// </summary>
		public string UC_LIST_ID
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
		/// 子表单编号
		/// </summary>
		public string UCS_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 内部排序
		/// </summary>
		public string CTRL_ORDER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 子表单状态
		/// </summary>
		public string CTRL_STATE
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}