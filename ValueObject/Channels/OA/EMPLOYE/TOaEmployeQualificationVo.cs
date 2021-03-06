using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工资格证书
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeQualificationVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EMPLOYE_QUALIFICATION_TABLE = "T_OA_EMPLOYE_QUALIFICATION";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 员工编号
		/// </summary>
		public static string EMPLOYEID_FIELD = "EMPLOYEID";
		/// <summary>
		/// 证书名称
		/// </summary>
		public static string CERTITICATENAME_FIELD = "CERTITICATENAME";
		/// <summary>
		/// 证书编号
		/// </summary>
		public static string CERTITICATECODE_FIELD = "CERTITICATECODE";
		/// <summary>
		/// 发证单位
		/// </summary>
		public static string ISSUINGAUTHO_FIELD = "ISSUINGAUTHO";
		/// <summary>
		/// 发证时间
		/// </summary>
		public static string ISSUINDATE_FIELD = "ISSUINDATE";
		/// <summary>
		/// 有效日期
		/// </summary>
		public static string ACTIVEDATE_FIELD = "ACTIVEDATE";
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
		
		public TOaEmployeQualificationVo()
		{
			this.ID = "";
			this.EMPLOYEID = "";
			this.CERTITICATENAME = "";
			this.CERTITICATECODE = "";
			this.ISSUINGAUTHO = "";
			this.ISSUINDATE = "";
			this.ACTIVEDATE = "";
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
		/// 员工编号
		/// </summary>
		public string EMPLOYEID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 证书名称
		/// </summary>
		public string CERTITICATENAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 证书编号
		/// </summary>
		public string CERTITICATECODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发证单位
		/// </summary>
		public string ISSUINGAUTHO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发证时间
		/// </summary>
		public string ISSUINDATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 有效日期
		/// </summary>
		public string ACTIVEDATE
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