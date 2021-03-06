using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_SUPPLIER_INFO_TABLE = "T_OA_SUPPLIER_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 供应商名称
		/// </summary>
		public static string SUPPLIER_NAME_FIELD = "SUPPLIER_NAME";
		/// <summary>
		/// 供应物质类别
		/// </summary>
		public static string SUPPLIER_TYPE_FIELD = "SUPPLIER_TYPE";
		/// <summary>
		/// 经营范围
		/// </summary>
		public static string PRODUCTS_FIELD = "PRODUCTS";
		/// <summary>
		/// 质量体系认证
		/// </summary>
		public static string QUANTITYSYSTEM_FIELD = "QUANTITYSYSTEM";
		/// <summary>
		/// 联系人
		/// </summary>
		public static string LINK_MAN_FIELD = "LINK_MAN";
		/// <summary>
		/// 地址
		/// </summary>
		public static string ADDRESS_FIELD = "ADDRESS";
		/// <summary>
		/// 电话
		/// </summary>
		public static string TEL_FIELD = "TEL";
		/// <summary>
		/// 传真
		/// </summary>
		public static string FAX_FIELD = "FAX";
		/// <summary>
		/// 邮件
		/// </summary>
		public static string EMAIL_FIELD = "EMAIL";
		/// <summary>
		/// 邮政编码
		/// </summary>
		public static string POST_CODE_FIELD = "POST_CODE";
		/// <summary>
		/// 开户行
		/// </summary>
		public static string BANK_FIELD = "BANK";
		/// <summary>
		/// 帐号
		/// </summary>
		public static string ACCOUNT_FOR_FIELD = "ACCOUNT_FOR";
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
		
		public TOaSupplierInfoVo()
		{
			this.ID = "";
			this.SUPPLIER_NAME = "";
			this.SUPPLIER_TYPE = "";
			this.PRODUCTS = "";
			this.QUANTITYSYSTEM = "";
			this.LINK_MAN = "";
			this.ADDRESS = "";
			this.TEL = "";
			this.FAX = "";
			this.EMAIL = "";
			this.POST_CODE = "";
			this.BANK = "";
			this.ACCOUNT_FOR = "";
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
		/// 供应商名称
		/// </summary>
		public string SUPPLIER_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 供应物质类别
		/// </summary>
		public string SUPPLIER_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 经营范围
		/// </summary>
		public string PRODUCTS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质量体系认证
		/// </summary>
		public string QUANTITYSYSTEM
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
		/// 地址
		/// </summary>
		public string ADDRESS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string TEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 传真
		/// </summary>
		public string FAX
		{
			set ;
			get ;
		}
		/// <summary>
		/// 邮件
		/// </summary>
		public string EMAIL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 邮政编码
		/// </summary>
		public string POST_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开户行
		/// </summary>
		public string BANK
		{
			set ;
			get ;
		}
		/// <summary>
		/// 帐号
		/// </summary>
		public string ACCOUNT_FOR
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