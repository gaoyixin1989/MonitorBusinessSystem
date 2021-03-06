using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.Message
{
    /// <summary>
    /// 功能：短消息已阅表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoDelVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_MESSAGE_INFO_DEL_TABLE = "T_OA_MESSAGE_INFO_DEL";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 消息表ID、消息接收表ID，具体对哪个ID进行清除，根据接收发送类型决定
		/// </summary>
		public static string MESSAGE_INFO_ID_FIELD = "MESSAGE_INFO_ID";
		/// <summary>
		/// 接收发送类型（发送、接收）
		/// </summary>
		public static string SEND_OR_ACCEPT_FIELD = "SEND_OR_ACCEPT";
		/// <summary>
		/// 清除标识
		/// </summary>
		public static string IS_DEL_FIELD = "IS_DEL";
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
		
		public TOaMessageInfoDelVo()
		{
			this.ID = "";
			this.MESSAGE_INFO_ID = "";
			this.SEND_OR_ACCEPT = "";
			this.IS_DEL = "";
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
		/// 消息表ID、消息接收表ID，具体对哪个id进行清除，根据接收发送类型决定
		/// </summary>
		public string MESSAGE_INFO_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 接收发送类型（发送、接收）
		/// </summary>
		public string SEND_OR_ACCEPT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 清除标识
		/// </summary>
		public string IS_DEL
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