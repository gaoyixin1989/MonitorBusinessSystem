using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Outcompany
{
    /// <summary>
    /// 功能：分包单位资质
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyAllowVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_OUTCOMPANY_ALLOW_TABLE = "T_BASE_OUTCOMPANY_ALLOW";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 分包单位ID
		/// </summary>
		public static string OUTCOMPANY_ID_FIELD = "OUTCOMPANY_ID";
		/// <summary>
		/// 资质变化情况
		/// </summary>
		public static string QUALIFICATIONS_INFO_FIELD = "QUALIFICATIONS_INFO";
		/// <summary>
		/// 主要项目情况
		/// </summary>
		public static string PROJECT_INFO_FIELD = "PROJECT_INFO";
		/// <summary>
		/// 质保体系情况(1,符合国标；2，符合地标)
		/// </summary>
		public static string QC_INFO_FIELD = "QC_INFO";
		/// <summary>
		/// 经办人
		/// </summary>
		public static string CHECK_USER_ID_FIELD = "CHECK_USER_ID";
		/// <summary>
		/// 经办日期
		/// </summary>
		public static string CHECK_DATE_FIELD = "CHECK_DATE";
		/// <summary>
		/// 备注
		/// </summary>
		public static string INFO_FIELD = "INFO";
		/// <summary>
		/// 附件ID
		/// </summary>
		public static string ATT_ID_FIELD = "ATT_ID";
		/// <summary>
		/// 委托完成情况
		/// </summary>
		public static string COMPLETE_INFO_FIELD = "COMPLETE_INFO";
		/// <summary>
		/// 是否通过评审
		/// </summary>
		public static string IS_OK_FIELD = "IS_OK";
		/// <summary>
		/// 评审意见
		/// </summary>
		public static string APP_INFO_FIELD = "APP_INFO";
		/// <summary>
		/// 评审人ID
		/// </summary>
		public static string APP_USER_ID_FIELD = "APP_USER_ID";
		/// <summary>
		/// 评审时间
		/// </summary>
		public static string APP_DATE_FIELD = "APP_DATE";
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
		
		public TBaseOutcompanyAllowVo()
		{
			this.ID = "";
			this.OUTCOMPANY_ID = "";
			this.QUALIFICATIONS_INFO = "";
			this.PROJECT_INFO = "";
			this.QC_INFO = "";
			this.CHECK_USER_ID = "";
			this.CHECK_DATE = "";
			this.INFO = "";
			this.ATT_ID = "";
			this.COMPLETE_INFO = "";
			this.IS_OK = "";
			this.APP_INFO = "";
			this.APP_USER_ID = "";
			this.APP_DATE = "";
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
		/// 分包单位ID
		/// </summary>
		public string OUTCOMPANY_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 资质变化情况
		/// </summary>
		public string QUALIFICATIONS_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主要项目情况
		/// </summary>
		public string PROJECT_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质保体系情况(1,符合国标；2，符合地标)
		/// </summary>
		public string QC_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 经办人
		/// </summary>
		public string CHECK_USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 经办日期
		/// </summary>
		public string CHECK_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件ID
		/// </summary>
		public string ATT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托完成情况
		/// </summary>
		public string COMPLETE_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否通过评审
		/// </summary>
		public string IS_OK
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评审意见
		/// </summary>
		public string APP_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评审人ID
		/// </summary>
		public string APP_USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评审时间
		/// </summary>
		public string APP_DATE
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