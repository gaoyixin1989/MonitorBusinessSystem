using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：结果数据可追溯性表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultLogVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_RESULT_LOG_TABLE = "T_MIS_MONITOR_RESULT_LOG";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 样品结果表ID
		/// </summary>
		public static string RESULT_ID_FIELD = "RESULT_ID";
		/// <summary>
		/// 原结果数据
		/// </summary>
		public static string OLD_RESULT_FIELD = "OLD_RESULT";
		/// <summary>
		/// 新结果数据
		/// </summary>
		public static string NEW_RESULT_FIELD = "NEW_RESULT";
		/// <summary>
		/// 分析负责人员ID
		/// </summary>
		public static string HEAD_USERID_FIELD = "HEAD_USERID";
		/// <summary>
		/// 
		/// </summary>
		public static string ASSISTANT_USERID_FIELD = "ASSISTANT_USERID";
		/// <summary>
		/// 完成时间
		/// </summary>
		public static string FINISH_DATE_FIELD = "FINISH_DATE";
		/// <summary>
		/// 校核人ID
		/// </summary>
		public static string CHECK_USERID_FIELD = "CHECK_USERID";
		/// <summary>
		/// 校核时间
		/// </summary>
		public static string CHECK_DATE_FIELD = "CHECK_DATE";
		/// <summary>
		/// 校核意见
		/// </summary>
		public static string CHECK_OPINION_FIELD = "CHECK_OPINION";
		/// <summary>
		/// 复核人ID
		/// </summary>
		public static string APPROVE_USERID_FIELD = "APPROVE_USERID";
		/// <summary>
		/// 复核时间
		/// </summary>
		public static string APPROVE_DATE_FIELD = "APPROVE_DATE";
		/// <summary>
		/// 复核意见
		/// </summary>
		public static string APPROVE_OPINION_FIELD = "APPROVE_OPINION";
		/// <summary>
		/// 质控手段审核人ID
		/// </summary>
		public static string QC_APP_USER_ID_FIELD = "QC_APP_USER_ID";
		/// <summary>
		/// 质控手段审核时间
		/// </summary>
		public static string QC_APP_DATE_FIELD = "QC_APP_DATE";
		/// <summary>
		/// 质控手段审核意见
		/// </summary>
		public static string QC_APP_INFO_FIELD = "QC_APP_INFO";
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
		
		public TMisMonitorResultLogVo()
		{
			this.ID = "";
			this.RESULT_ID = "";
			this.OLD_RESULT = "";
			this.NEW_RESULT = "";
			this.HEAD_USERID = "";
			this.ASSISTANT_USERID = "";
			this.FINISH_DATE = "";
			this.CHECK_USERID = "";
			this.CHECK_DATE = "";
			this.CHECK_OPINION = "";
			this.APPROVE_USERID = "";
			this.APPROVE_DATE = "";
			this.APPROVE_OPINION = "";
			this.QC_APP_USER_ID = "";
			this.QC_APP_DATE = "";
			this.QC_APP_INFO = "";
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
		/// 样品结果表ID
		/// </summary>
		public string RESULT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 原结果数据
		/// </summary>
		public string OLD_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 新结果数据
		/// </summary>
		public string NEW_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分析负责人员ID
		/// </summary>
		public string HEAD_USERID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string ASSISTANT_USERID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 完成时间
		/// </summary>
		public string FINISH_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 校核人ID
		/// </summary>
		public string CHECK_USERID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 校核时间
		/// </summary>
		public string CHECK_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 校核意见
		/// </summary>
		public string CHECK_OPINION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 复核人ID
		/// </summary>
		public string APPROVE_USERID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 复核时间
		/// </summary>
		public string APPROVE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 复核意见
		/// </summary>
		public string APPROVE_OPINION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质控手段审核人ID
		/// </summary>
		public string QC_APP_USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质控手段审核时间
		/// </summary>
		public string QC_APP_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质控手段审核意见
		/// </summary>
		public string QC_APP_INFO
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