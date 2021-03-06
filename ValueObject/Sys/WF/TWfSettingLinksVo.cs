using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：流程节点连接线
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingLinksVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_WF_SETTING_LINKS_TABLE = "T_WF_SETTING_LINKS";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 连接编号
		/// </summary>
		public static string WF_LINK_ID_FIELD = "WF_LINK_ID";
		/// <summary>
		/// 流程编号
		/// </summary>
		public static string WF_ID_FIELD = "WF_ID";
		/// <summary>
		/// 起始环节编号
		/// </summary>
		public static string START_TASK_ID_FIELD = "START_TASK_ID";
		/// <summary>
		/// 结束环节编号
		/// </summary>
		public static string END_TASK_ID_FIELD = "END_TASK_ID";
		/// <summary>
		/// 条件描述
		/// </summary>
		public static string CONDITION_DES_FIELD = "CONDITION_DES";
		/// <summary>
		/// 文字简述
		/// </summary>
		public static string NOTE_DES_FIELD = "NOTE_DES";
		/// <summary>
		/// 命令描述
		/// </summary>
		public static string CMD_DES_FIELD = "CMD_DES";
		/// <summary>
		/// 优先级
		/// </summary>
		public static string PRIORITY_FIELD = "PRIORITY";
		
		#endregion
		
		public TWfSettingLinksVo()
		{
			this.ID = "";
			this.WF_LINK_ID = "";
			this.WF_ID = "";
			this.START_TASK_ID = "";
			this.END_TASK_ID = "";
			this.CONDITION_DES = "";
			this.NOTE_DES = "";
			this.CMD_DES = "";
			this.PRIORITY = "";
			
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
		/// 连接编号
		/// </summary>
		public string WF_LINK_ID
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
		/// 起始环节编号
		/// </summary>
		public string START_TASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束环节编号
		/// </summary>
		public string END_TASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 条件描述
		/// </summary>
		public string CONDITION_DES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文字简述
		/// </summary>
		public string NOTE_DES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 命令描述
		/// </summary>
		public string CMD_DES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 优先级
		/// </summary>
		public string PRIORITY
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}