using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.Notice
{
    /// <summary>
    /// 功能：公告管理
    /// 创建日期：2013-02-23
    /// 创建人：熊卫华
    /// </summary>
    public class TOaNoticeVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_NOTICE_TABLE = "T_OA_NOTICE";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 公告标题
		/// </summary>
		public static string TITLE_FIELD = "TITLE";
		/// <summary>
		/// 公告内容
		/// </summary>
		public static string CONTENT_FIELD = "CONTENT";
		/// <summary>
		/// 公告类别
		/// </summary>
		public static string NOTICE_TYPE_FIELD = "NOTICE_TYPE";
		/// <summary>
		/// 发布时间
		/// </summary>
		public static string RELEASE_TIME_FIELD = "RELEASE_TIME";
		/// <summary>
		/// 发布人
		/// </summary>
		public static string RELIEASER_FIELD = "RELIEASER";
		/// <summary>
		/// 发布方式
		/// </summary>
		public static string RELIEASER_TYPE_FIELD = "RELIEASER_TYPE";
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
		
		public TOaNoticeVo()
		{
			this.ID = "";
			this.TITLE = "";
			this.CONTENT = "";
			this.NOTICE_TYPE = "";
			this.RELEASE_TIME = "";
			this.RELIEASER = "";
			this.RELIEASER_TYPE = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 公告标题
		/// </summary>
		public string TITLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 公告内容
		/// </summary>
		public string CONTENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 公告类别
		/// </summary>
		public string NOTICE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发布时间
		/// </summary>
		public string RELEASE_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发布人
		/// </summary>
		public string RELIEASER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发布方式
		/// </summary>
		public string RELIEASER_TYPE
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