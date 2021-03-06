using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务外包单位表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskOutVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_TASK_OUT_TABLE = "T_MIS_MONITOR_TASK_OUT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测计划ID
		/// </summary>
		public static string TASK_ID_FIELD = "TASK_ID";
		/// <summary>
		/// 外包ID
		/// </summary>
		public static string OUTCOMPANY_ID_FIELD = "OUTCOMPANY_ID";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
		
		#endregion
		
		public TMisMonitorTaskOutVo()
		{
			this.ID = "";
			this.TASK_ID = "";
			this.OUTCOMPANY_ID = "";
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
		/// 监测计划ID
		/// </summary>
		public string TASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 外包ID
		/// </summary>
		public string OUTCOMPANY_ID
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