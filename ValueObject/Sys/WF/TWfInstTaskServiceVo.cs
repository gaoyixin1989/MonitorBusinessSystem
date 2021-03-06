using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节附属业务明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskServiceVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_INST_TASK_SERVICE_TABLE = "T_WF_INST_TASK_SERVICE";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 环节实例编号
		/// </summary>
		public static string WF_INST_TASK_ID_FIELD = "WF_INST_TASK_ID";
		/// <summary>
		/// 流程实例编号
		/// </summary>
		public static string WF_INST_ID_FIELD = "WF_INST_ID";
		/// <summary>
		/// 业务编号
		/// </summary>
		public static string SERVICE_NAME_FIELD = "SERVICE_NAME";
		/// <summary>
		/// 业务单类型
		/// </summary>
		public static string SERVICE_KEY_NAME_FIELD = "SERVICE_KEY_NAME";
		/// <summary>
		/// 业务单主键值
		/// </summary>
		public static string SERVICE_KEY_VALUE_FIELD = "SERVICE_KEY_VALUE";
		/// <summary>
		/// 联合单据分组
		/// </summary>
		public static string SERVICE_ROW_SIGN_FIELD = "SERVICE_ROW_SIGN";
		
		#endregion
		
		public TWfInstTaskServiceVo()
		{
			this.ID = "";
			this.WF_INST_TASK_ID = "";
			this.WF_INST_ID = "";
			this.SERVICE_NAME = "";
			this.SERVICE_KEY_NAME = "";
			this.SERVICE_KEY_VALUE = "";
			this.SERVICE_ROW_SIGN = "";
			
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
		/// 环节实例编号
		/// </summary>
		public string WF_INST_TASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 流程实例编号
		/// </summary>
		public string WF_INST_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 业务编号
		/// </summary>
		public string SERVICE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 业务单类型
		/// </summary>
		public string SERVICE_KEY_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 业务单主键值
		/// </summary>
		public string SERVICE_KEY_VALUE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联合单据分组
		/// </summary>
		public string SERVICE_ROW_SIGN
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}