using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.Message
{
    /// <summary>
    /// 功能：个人短消息接收设置表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageSetupVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_MESSAGE_SETUP_TABLE = "T_OA_MESSAGE_SETUP";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 
		/// </summary>
		public static string IF_RE_FIELD = "IF_RE";
		/// <summary>
		/// 提醒方式，1弹出窗口，2短信，3图标闪烁（3可以暂时不实现）
		/// </summary>
		public static string UDS_REMINDTYPE_FIELD = "UDS_REMINDTYPE";
		/// <summary>
		/// 提醒时间，即刷新间隔
		/// </summary>
		public static string UDS_REFRESHTIME_FIELD = "UDS_REFRESHTIME";
		/// <summary>
		/// 用户ID
		/// </summary>
		public static string USER_ID_FIELD = "USER_ID";
		/// <summary>
		/// 备份1
		/// </summary>
		public static string REMARK1_FIELD = "REMARK1";
		/// <summary>
		/// 备份2
		/// </summary>
		public static string REMARK2_FIELD = "REMARK2";
		/// <summary>
		/// 备份3
		/// </summary>
		public static string REMARK3_FIELD = "REMARK3";
		/// <summary>
		/// 备份4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备份5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		
		#endregion
		
		public TOaMessageSetupVo()
		{
			this.ID = "";
			this.IF_RE = "";
			this.UDS_REMINDTYPE = "";
			this.UDS_REFRESHTIME = "";
			this.USER_ID = "";
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
		/// 
		/// </summary>
		public string IF_RE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 提醒方式，1弹出窗口，2短信，3图标闪烁（3可以暂时不实现）
		/// </summary>
		public string UDS_REMINDTYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 提醒时间，即刷新间隔
		/// </summary>
		public string UDS_REFRESHTIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public string USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备份1
		/// </summary>
		public string REMARK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备份2
		/// </summary>
		public string REMARK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备份3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备份4
		/// </summary>
		public string REMARK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备份5
		/// </summary>
		public string REMARK5
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}