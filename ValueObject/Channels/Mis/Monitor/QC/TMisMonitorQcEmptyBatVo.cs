using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：实验室空白批次表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyBatVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_QC_EMPTY_BAT_TABLE = "T_MIS_MONITOR_QC_EMPTY_BAT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 空白批次
		/// </summary>
		public static string QC_EMPTY_IN_NUM_FIELD = "QC_EMPTY_IN_NUM";
		/// <summary>
		/// 空白测试日期
		/// </summary>
		public static string QC_EMPTY_IN_DATE_FIELD = "QC_EMPTY_IN_DATE";
		/// <summary>
		/// 实验室空白个数
		/// </summary>
		public static string QC_EMPTY_IN_COUNT_FIELD = "QC_EMPTY_IN_COUNT";
		/// <summary>
		/// 实验室空白值
		/// </summary>
		public static string QC_EMPTY_IN_RESULT_FIELD = "QC_EMPTY_IN_RESULT";
		/// <summary>
		/// 相对偏差（%）
		/// </summary>
		public static string QC_EMPTY_OFFSET_FIELD = "QC_EMPTY_OFFSET";
		/// <summary>
		/// 空白是否合格
		/// </summary>
		public static string QC_EMPTY_ISOK_FIELD = "QC_EMPTY_ISOK";
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
		
		public TMisMonitorQcEmptyBatVo()
		{
			this.ID = "";
			this.QC_EMPTY_IN_NUM = "";
			this.QC_EMPTY_IN_DATE = "";
			this.QC_EMPTY_IN_COUNT = "";
			this.QC_EMPTY_IN_RESULT = "";
			this.QC_EMPTY_OFFSET = "";
			this.QC_EMPTY_ISOK = "";
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
		/// 空白批次
		/// </summary>
		public string QC_EMPTY_IN_NUM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 空白测试日期
		/// </summary>
		public string QC_EMPTY_IN_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实验室空白个数
		/// </summary>
		public string QC_EMPTY_IN_COUNT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实验室空白值
		/// </summary>
		public string QC_EMPTY_IN_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 相对偏差（%）
		/// </summary>
		public string QC_EMPTY_OFFSET
		{
			set ;
			get ;
		}
		/// <summary>
		/// 空白是否合格
		/// </summary>
		public string QC_EMPTY_ISOK
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