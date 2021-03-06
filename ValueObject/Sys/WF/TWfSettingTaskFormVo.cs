using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流节点表单集
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskFormVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_TASK_FORM_TABLE = "T_WF_SETTING_TASK_FORM";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 表单内编号
		/// </summary>
		public static string WF_TF_ID_FIELD = "WF_TF_ID";
		/// <summary>
		/// 流程编号
		/// </summary>
		public static string WF_ID_FIELD = "WF_ID";
		/// <summary>
		/// 节点编号
		/// </summary>
		public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
		/// <summary>
		/// 主表单编号
		/// </summary>
		public static string UCM_ID_FIELD = "UCM_ID";
		/// <summary>
		/// 主表单类型
		/// </summary>
		public static string UCM_TYPE_FIELD = "UCM_TYPE";
		
		#endregion
		
		public TWfSettingTaskFormVo()
		{
			this.ID = "";
			this.WF_TF_ID = "";
			this.WF_ID = "";
			this.WF_TASK_ID = "";
			this.UCM_ID = "";
			this.UCM_TYPE = "";
			
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
		/// 表单内编号
		/// </summary>
		public string WF_TF_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 流程编号
		/// </summary>
		public string WF_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 节点编号
		/// </summary>
		public string WF_TASK_ID
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
		/// 主表单类型
		/// </summary>
		public string UCM_TYPE
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}