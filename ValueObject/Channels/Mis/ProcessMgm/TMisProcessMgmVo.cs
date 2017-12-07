using System;
using System.Collections.Generic;
using System.Text;


namespace i3.ValueObject.Channels.Mis.ProcessMgm
{
    /// <summary>
    /// 功能：
    /// 创建日期：2015-09-02
    /// 创建人：
    /// </summary>
    public class TMisMonitorProcessMgmVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_PROCESS_MGM_TABLE = "T_MIS_MONITOR_PROCESS_MGM";
		//静态字段引用
		/// <summary>
		/// 
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 
		/// </summary>
		public static string TASK_ID_FIELD = "TASK_ID";
		/// <summary>
		/// 
		/// </summary>
		public static string MONITOR_TYPE_FIELD = "MONITOR_TYPE";
		/// <summary>
		/// 
		/// </summary>
		public static string MONITOR_TIME_START_FIELD = "MONITOR_TIME_START";
		/// <summary>
		/// 
		/// </summary>
		public static string MONITOR_TIME_FINISH_FIELD = "MONITOR_TIME_FINISH";
		
		#endregion
		
		public TMisMonitorProcessMgmVo()
		{
            Init();			
		}
		
        public void Init()
        {
            this.ID = "";
            this.TASK_ID = "";
            this.MONITOR_TYPE = "";
            this.MONITOR_TIME_START = "";
            this.MONITOR_TIME_FINISH = "";
        }
		#region 属性
			/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string TASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string MONITOR_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string MONITOR_TIME_START
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string MONITOR_TIME_FINISH
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}