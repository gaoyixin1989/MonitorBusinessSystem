using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：维护工程师管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysEngineerVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_ENGINEER_TABLE = "T_SYS_ENGINEER";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 工程师编码
		/// </summary>
		public static string ENGINEER_CODE_FIELD = "ENGINEER_CODE";
		/// <summary>
		/// 真实姓名
		/// </summary>
		public static string ENGINEERL_NAME_FIELD = "ENGINEERL_NAME";
		/// <summary>
		/// 手机号码
		/// </summary>
		public static string PHONE_MOBILE_FIELD = "PHONE_MOBILE";
		/// <summary>
		/// 办公电话
		/// </summary>
		public static string PHONE_OFFICE_FIELD = "PHONE_OFFICE";
		/// <summary>
		/// 家庭电话
		/// </summary>
		public static string PHONE_HOME_FIELD = "PHONE_HOME";
		/// <summary>
		/// 业务代码
		/// </summary>
		public static string BUSINESS_CODE_FIELD = "BUSINESS_CODE";
		/// <summary>
		/// 单位编码
		/// </summary>
		public static string UNITS_CODE_FIELD = "UNITS_CODE";
		/// <summary>
		/// 地区编码
		/// </summary>
		public static string REGION_CODE_FIELD = "REGION_CODE";
		/// <summary>
		/// 职务编码
		/// </summary>
		public static string DUTY_CODE_FIELD = "DUTY_CODE";
		/// <summary>
		/// 启用标记,1启用,0停用
		/// </summary>
		public static string IS_USE_FIELD = "IS_USE";
		/// <summary>
		/// 删除标记,1为删除,
		/// </summary>
		public static string IS_DEL_FIELD = "IS_DEL";
		/// <summary>
		/// 创建人ID
		/// </summary>
		public static string CREATE_ID_FIELD = "CREATE_ID";
		/// <summary>
		/// 创建时间
		/// </summary>
		public static string CREATE_TIME_FIELD = "CREATE_TIME";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
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
		
		public TSysEngineerVo()
		{
			this.ID = "";
			this.ENGINEER_CODE = "";
			this.ENGINEERL_NAME = "";
			this.PHONE_MOBILE = "";
			this.PHONE_OFFICE = "";
			this.PHONE_HOME = "";
			this.BUSINESS_CODE = "";
			this.UNITS_CODE = "";
			this.REGION_CODE = "";
			this.DUTY_CODE = "";
			this.IS_USE = "";
			this.IS_DEL = "";
			this.CREATE_ID = "";
			this.CREATE_TIME = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			
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
		/// 工程师编码
		/// </summary>
		public string ENGINEER_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string ENGINEERL_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string PHONE_MOBILE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 办公电话
		/// </summary>
		public string PHONE_OFFICE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 家庭电话
		/// </summary>
		public string PHONE_HOME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 业务代码
		/// </summary>
		public string BUSINESS_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单位编码
		/// </summary>
		public string UNITS_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 地区编码
		/// </summary>
		public string REGION_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 职务编码
		/// </summary>
		public string DUTY_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 启用标记,1启用,0停用
		/// </summary>
		public string IS_USE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 删除标记,1为删除,
		/// </summary>
		public string IS_DEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建人ID
		/// </summary>
		public string CREATE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public string CREATE_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string REMARK
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