using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.Message
{
    /// <summary>
    /// 功能：短信息接收
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoReceiveVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_MESSAGE_INFO_RECEIVE_TABLE = "T_OA_MESSAGE_INFO_RECEIVE";
		//静态字段引用
		/// <summary>
		/// 消息阅读编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 消息编号
		/// </summary>
		public static string MESSAGE_ID_FIELD = "MESSAGE_ID";
		/// <summary>
		/// 消息接收人
		/// </summary>
		public static string RECEIVER_FIELD = "RECEIVER";
		/// <summary>
		/// 是否已阅读
		/// </summary>
		public static string IS_READ_FIELD = "IS_READ";
		/// <summary>
		/// 消息查阅时间
		/// </summary>
		public static string READ_DATE_FIELD = "READ_DATE";
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
		
		public TOaMessageInfoReceiveVo()
		{
			this.ID = "";
			this.MESSAGE_ID = "";
			this.RECEIVER = "";
			this.IS_READ = "";
			this.READ_DATE = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 消息阅读编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息编号
		/// </summary>
		public string MESSAGE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息接收人
		/// </summary>
		public string RECEIVER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否已阅读
		/// </summary>
		public string IS_READ
		{
			set ;
			get ;
		}
		/// <summary>
		/// 消息查阅时间
		/// </summary>
		public string READ_DATE
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