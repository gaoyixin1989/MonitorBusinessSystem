using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：现场空白结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyOutVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_QC_EMPTY_OUT_TABLE = "T_MIS_MONITOR_QC_EMPTY_OUT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 原始样分析结果 ID
		/// </summary>
		public static string RESULT_ID_SRC_FIELD = "RESULT_ID_SRC";
		/// <summary>
		/// 空白样分析结果 ID
		/// </summary>
		public static string RESULT_ID_EMPTY_FIELD = "RESULT_ID_EMPTY";
		/// <summary>
		/// 
		/// </summary>
		public static string RESULT_EMPTY_FIELD = "RESULT_EMPTY";
		/// <summary>
		/// 相对偏差（%）
		/// </summary>
		public static string EMPTY_OFFSET_FIELD = "EMPTY_OFFSET";
		/// <summary>
		/// 空白是否合格
		/// </summary>
		public static string EMPTY_ISOK_FIELD = "EMPTY_ISOK";
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
		
		public TMisMonitorQcEmptyOutVo()
		{
			this.ID = "";
			this.RESULT_ID_SRC = "";
			this.RESULT_ID_EMPTY = "";
			this.RESULT_EMPTY = "";
			this.EMPTY_OFFSET = "";
			this.EMPTY_ISOK = "";
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
		/// 原始样分析结果 ID
		/// </summary>
		public string RESULT_ID_SRC
		{
			set ;
			get ;
		}
		/// <summary>
		/// 空白样分析结果 ID
		/// </summary>
		public string RESULT_ID_EMPTY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string RESULT_EMPTY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 相对偏差（%）
		/// </summary>
		public string EMPTY_OFFSET
		{
			set ;
			get ;
		}
		/// <summary>
		/// 空白是否合格
		/// </summary>
		public string EMPTY_ISOK
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