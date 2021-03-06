using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.Message
{
    /// <summary>
    /// 功能：短消息信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_MESSAGE_INFO_TABLE = "T_OA_MESSAGE_INFO";
		//静态字段引用
		/// <summary>
		/// 消息编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 消息标题
		/// </summary>
		public static string MESSAGE_TITLE_FIELD = "MESSAGE_TITLE";
		/// <summary>
		/// 短消息内容
		/// </summary>
		public static string MESSAGE_CONTENT_FIELD = "MESSAGE_CONTENT";
		/// <summary>
		/// 消息发送人
		/// </summary>
		public static string SEND_BY_FIELD = "SEND_BY";
		/// <summary>
		/// 消息发送时间
		/// </summary>
		public static string SEND_DATE_FIELD = "SEND_DATE";
		/// <summary>
		/// 发送时分（时分秒）
		/// </summary>
		public static string SEND_TIME_FIELD = "SEND_TIME";
		/// <summary>
		/// 消息发送方式(4：定期，3：立即)，暂不支持周期循环发送，客户处暂未发现类似需求
		/// </summary>
		public static string SEND_TYPE_FIELD = "SEND_TYPE";
		/// <summary>
		/// 接收类别(1：全站；2：按人)
		/// </summary>
		public static string ACCEPT_TYPE_FIELD = "ACCEPT_TYPE";
		/// <summary>
		/// 所有接收人ID以中文逗号串联，仅为查询方便
		/// </summary>
		public static string ACCEPT_USERIDS_FIELD = "ACCEPT_USERIDS";
		/// <summary>
		/// 所有接收人REALNAME以中文逗号串联，仅为查询方便
		/// </summary>
		public static string ACCEPT_REALNAMES_FIELD = "ACCEPT_REALNAMES";
		/// <summary>
		/// 所有接收部门ID以中文逗号串联，仅为查询方便
		/// </summary>
		public static string ACCEPT_DEPTIDS_FIELD = "ACCEPT_DEPTIDS";
		/// <summary>
		/// 所有接收部门NAME以中文逗号串联串联，仅为查询方便
		/// </summary>
		public static string ACCEPT_DEPTNAMES_FIELD = "ACCEPT_DEPTNAMES";
		/// <summary>
		/// 工作流任务ID，将消息和任务关联，方便任务办理后消除消息，方便直接点消息办理任务
		/// </summary>
		public static string TASK_ID_FIELD = "TASK_ID";
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
		
		#endregion
		
		public TOaMessageInfoVo()
		{
			this.ID = "";
			this.MESSAGE_TITLE = "";
			this.MESSAGE_CONTENT = "";
			this.SEND_BY = "";
			this.SEND_DATE = "";
			this.SEND_TIME = "";
			this.SEND_TYPE = "";
			this.ACCEPT_TYPE = "";
			this.ACCEPT_USERIDS = "";
			this.ACCEPT_REALNAMES = "";
			this.ACCEPT_DEPTIDS = "";
			this.ACCEPT_DEPTNAMES = "";
			this.TASK_ID = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 消息编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息标题
		/// </summary>
		public string MESSAGE_TITLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 短消息内容
		/// </summary>
		public string MESSAGE_CONTENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息发送人
		/// </summary>
		public string SEND_BY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息发送时间
		/// </summary>
		public string SEND_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发送时分（时分秒）
		/// </summary>
		public string SEND_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息发送方式(4：定期，3：立即)，暂不支持周期循环发送，客户处暂未发现类似需求
		/// </summary>
		public string SEND_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 接收类别(1：全站；2：按人)
		/// </summary>
		public string ACCEPT_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所有接收人id以中文逗号串联，仅为查询方便
		/// </summary>
		public string ACCEPT_USERIDS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所有接收人REALNAME以中文逗号串联，仅为查询方便
		/// </summary>
		public string ACCEPT_REALNAMES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所有接收部门id以中文逗号串联，仅为查询方便
		/// </summary>
		public string ACCEPT_DEPTIDS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所有接收部门NAME以中文逗号串联串联，仅为查询方便
		/// </summary>
		public string ACCEPT_DEPTNAMES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 工作流任务id，将消息和任务关联，方便任务办理后消除消息，方便直接点消息办理任务
		/// </summary>
		public string TASK_ID
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
	
		
		#endregion
		
    }
}