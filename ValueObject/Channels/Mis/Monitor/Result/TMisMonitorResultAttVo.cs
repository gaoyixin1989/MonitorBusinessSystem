using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析原始数据附件表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultAttVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_RESULT_ATT_TABLE = "T_MIS_MONITOR_RESULT_ATT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 样品结果ID
		/// </summary>
		public static string RESULT_ID_FIELD = "RESULT_ID";
		/// <summary>
		/// 附件ID
		/// </summary>
		public static string FILE_ID_FIELD = "FILE_ID";
		/// <summary>
		/// 备注1
		/// </summary>
		public static string REMARK_1_FIELD = "REMARK_1";
		/// <summary>
		/// 备注2
		/// </summary>
		public static string REMARK_2_FIELD = "REMARK_2";
		/// <summary>
		/// 备注3
		/// </summary>
		public static string REMARK_3_FIELD = "REMARK_3";
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK_4_FIELD = "REMARK_4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK_5_FIELD = "REMARK_5";
		
		#endregion
		
		public TMisMonitorResultAttVo()
		{
			this.ID = "";
			this.RESULT_ID = "";
			this.FILE_ID = "";
			this.REMARK_1 = "";
			this.REMARK_2 = "";
			this.REMARK_3 = "";
			this.REMARK_4 = "";
			this.REMARK_5 = "";
			
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
		/// 样品结果ID
		/// </summary>
		public string RESULT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件ID
		/// </summary>
		public string FILE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注1
		/// </summary>
		public string REMARK_1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注2
		/// </summary>
		public string REMARK_2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK_3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注4
		/// </summary>
		public string REMARK_4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注5
		/// </summary>
		public string REMARK_5
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}