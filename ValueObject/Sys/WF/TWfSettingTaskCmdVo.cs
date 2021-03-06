using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程节点命令集合
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskCmdVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_TASK_CMD_TABLE = "T_WF_SETTING_TASK_CMD";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 命令编号
		/// </summary>
		public static string WF_CMD_ID_FIELD = "WF_CMD_ID";
		/// <summary>
		/// 流程编号
		/// </summary>
		public static string WF_ID_FIELD = "WF_ID";
		/// <summary>
		/// 节点编号
		/// </summary>
		public static string WF_TASK_ID_FIELD = "WF_TASK_ID";
		/// <summary>
		/// 命令名称
		/// </summary>
		public static string CMD_NAME_FIELD = "CMD_NAME";
		/// <summary>
		/// 命令描述
		/// </summary>
		public static string CMD_NOTE_FIELD = "CMD_NOTE";
		
		#endregion
		
		public TWfSettingTaskCmdVo()
		{
			this.ID = "";
			this.WF_CMD_ID = "";
			this.WF_ID = "";
			this.WF_TASK_ID = "";
			this.CMD_NAME = "";
			this.CMD_NOTE = "";
			
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
		/// 命令编号
		/// </summary>
		public string WF_CMD_ID
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
		/// 命令名称
		/// </summary>
		public string CMD_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 命令描述
		/// </summary>
		public string CMD_NOTE
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}