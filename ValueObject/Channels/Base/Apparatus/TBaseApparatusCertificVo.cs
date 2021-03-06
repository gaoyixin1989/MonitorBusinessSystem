using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器鉴定证书
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusCertificVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_APPARATUS_CERTIFIC_TABLE = "T_BASE_APPARATUS_CERTIFIC";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 检定名称
		/// </summary>
		public static string APPRAISAL_NAME_FIELD = "APPRAISAL_NAME";
		/// <summary>
		/// 仪器ID
		/// </summary>
		public static string APPARATUS_ID_FIELD = "APPARATUS_ID";
		/// <summary>
		/// 仪器检定时间
		/// </summary>
		public static string APPRAISAL_DATE_FIELD = "APPRAISAL_DATE";
		/// <summary>
		/// 检定证书路径
		/// </summary>
		public static string APPRAISAL_URL_FIELD = "APPRAISAL_URL";
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
		
		public TBaseApparatusCertificVo()
		{
			this.ID = "";
			this.APPRAISAL_NAME = "";
			this.APPARATUS_ID = "";
			this.APPRAISAL_DATE = "";
			this.APPRAISAL_URL = "";
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
		/// 检定名称
		/// </summary>
		public string APPRAISAL_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器ID
		/// </summary>
		public string APPARATUS_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器检定时间
		/// </summary>
		public string APPRAISAL_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检定证书路径
		/// </summary>
		public string APPRAISAL_URL
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