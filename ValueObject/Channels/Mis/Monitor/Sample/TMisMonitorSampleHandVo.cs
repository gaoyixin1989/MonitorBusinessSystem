using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品交接表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_SAMPLE_HAND_TABLE = "T_MIS_MONITOR_SAMPLE_HAND";
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
		/// 交接单编号
		/// </summary>
		public static string HANDOVER_NO_FIELD = "HANDOVER_NO";
		/// <summary>
		/// 编号类型
		/// </summary>
		public static string NO_TYPE_FIELD = "NO_TYPE";
		/// <summary>
		/// 普通加急
		/// </summary>
		public static string IF_COMMON_FIELD = "IF_COMMON";
		/// <summary>
		/// 样品类型
		/// </summary>
		public static string SAMPLE_TYPE_FIELD = "SAMPLE_TYPE";
		/// <summary>
		/// 单据类型编号
		/// </summary>
		public static string TAB_TYPE_CODE_FIELD = "TAB_TYPE_CODE";
		/// <summary>
		/// 是否移交
		/// </summary>
		public static string IS_HANDOVER_FIELD = "IS_HANDOVER";
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
		
		public TMisMonitorSampleHandVo()
		{
			this.ID = "";
			this.SUBTASK_ID = "";
			this.HANDOVER_NO = "";
			this.NO_TYPE = "";
			this.IF_COMMON = "";
			this.SAMPLE_TYPE = "";
			this.TAB_TYPE_CODE = "";
			this.IS_HANDOVER = "";
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
		/// 交接单编号
		/// </summary>
		public string HANDOVER_NO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 编号类型
		/// </summary>
		public string NO_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 普通加急
		/// </summary>
		public string IF_COMMON
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品类型
		/// </summary>
		public string SAMPLE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单据类型编号
		/// </summary>
		public string TAB_TYPE_CODE
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