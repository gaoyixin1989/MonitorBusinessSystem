using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品交接明细表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandsVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_SAMPLE_HANDS_TABLE = "T_MIS_MONITOR_SAMPLE_HANDS";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测子任务ID
		/// </summary>
		public static string HANDOVER_ID_FIELD = "HANDOVER_ID";
		/// <summary>
		/// 样品ID
		/// </summary>
		public static string SAMPLE_ID_FIELD = "SAMPLE_ID";
		/// <summary>
		/// 样品数量
		/// </summary>
		public static string SAMPLE_NUMBER_FIELD = "SAMPLE_NUMBER";
		/// <summary>
		/// 是否移交
		/// </summary>
		public static string IS_HANDOVER_FIELD = "IS_HANDOVER";
		/// <summary>
		/// 样品是否齐全完整
		/// </summary>
		public static string IF_INTEGRITY_FIELD = "IF_INTEGRITY";
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
		
		public TMisMonitorSampleHandsVo()
		{
			this.ID = "";
			this.HANDOVER_ID = "";
			this.SAMPLE_ID = "";
			this.SAMPLE_NUMBER = "";
			this.IS_HANDOVER = "";
			this.IF_INTEGRITY = "";
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
		public string HANDOVER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品ID
		/// </summary>
		public string SAMPLE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品数量
		/// </summary>
		public string SAMPLE_NUMBER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否移交
		/// </summary>
		public string IS_HANDOVER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品是否齐全完整
		/// </summary>
		public string IF_INTEGRITY
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