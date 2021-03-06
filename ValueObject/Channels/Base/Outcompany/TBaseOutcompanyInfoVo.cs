using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Outcompany
{
    /// <summary>
    /// 功能：分包单位
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_OUTCOMPANY_INFO_TABLE = "T_BASE_OUTCOMPANY_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 公司法人代码
		/// </summary>
		public static string COMPANY_CODE_FIELD = "COMPANY_CODE";
		/// <summary>
		/// 公司名称
		/// </summary>
		public static string COMPANY_NAME_FIELD = "COMPANY_NAME";
		/// <summary>
		/// 拼音编码
		/// </summary>
		public static string PINYIN_FIELD = "PINYIN";
		/// <summary>
		/// 联系人
		/// </summary>
		public static string LINK_MAN_FIELD = "LINK_MAN";
		/// <summary>
		/// 联系
		/// </summary>
		public static string PHONE_FIELD = "PHONE";
		/// <summary>
		/// 邮编
		/// </summary>
		public static string POST_FIELD = "POST";
		/// <summary>
		/// 详细地址
		/// </summary>
		public static string ADDRESS_FIELD = "ADDRESS";
		/// <summary>
		/// 外包公司资质
		/// </summary>
		public static string APTITUDE_FIELD = "APTITUDE";
		/// <summary>
        /// 删除标记
		/// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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
		
		public TBaseOutcompanyInfoVo()
		{
			this.ID = "";
			this.COMPANY_CODE = "";
			this.COMPANY_NAME = "";
			this.PINYIN = "";
			this.LINK_MAN = "";
			this.PHONE = "";
			this.POST = "";
			this.ADDRESS = "";
			this.APTITUDE = "";
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
		/// 公司法人代码
		/// </summary>
		public string COMPANY_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 公司名称
		/// </summary>
		public string COMPANY_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 拼音编码
		/// </summary>
		public string PINYIN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string LINK_MAN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联系
		/// </summary>
		public string PHONE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string POST
		{
			set ;
			get ;
		}
		/// <summary>
		/// 详细地址
		/// </summary>
		public string ADDRESS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 外包公司资质
		/// </summary>
		public string APTITUDE
		{
			set ;
			get ;
		}
		/// <summary>
        /// 删除标记
		/// </summary>
        public string IS_DEL
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