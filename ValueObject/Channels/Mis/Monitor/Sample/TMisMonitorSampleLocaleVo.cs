using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：现场信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleLocaleVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_SAMPLE_LOCALE_TABLE = "T_MIS_MONITOR_SAMPLE_LOCALE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测子任务ID
		/// </summary>
		public static string SUBTASK_ID_FIELD = "SUBTASK_ID";
		/// <summary>
		/// 企业工况
		/// </summary>
		public static string WORK_CONDITION_FIELD = "WORK_CONDITION";
		/// <summary>
		/// 环保设施运行情况
		/// </summary>
		public static string ENVT_EQUT_STATUS_FIELD = "ENVT_EQUT_STATUS";
		/// <summary>
		/// 备注1
		/// </summary>
		public static string REMARK1_FIELD = "REMARK1";
		/// <summary>
		/// 备注2
		/// </summary>
		public static string REMARK2_FIELD = "REMARK2";
		/// <summary>
		/// 备注3
		/// </summary>
		public static string REMARK3_FIELD = "REMARK3";
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		
		#endregion
		
		public TMisMonitorSampleLocaleVo()
		{
			this.ID = "";
			this.SUBTASK_ID = "";
			this.WORK_CONDITION = "";
			this.ENVT_EQUT_STATUS = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
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
		/// 监测子任务ID
		/// </summary>
		public string SUBTASK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 企业工况
		/// </summary>
		public string WORK_CONDITION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 环保设施运行情况
		/// </summary>
		public string ENVT_EQUT_STATUS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注1
		/// </summary>
		public string REMARK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注2
		/// </summary>
		public string REMARK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注4
		/// </summary>
		public string REMARK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注5
		/// </summary>
		public string REMARK5
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}