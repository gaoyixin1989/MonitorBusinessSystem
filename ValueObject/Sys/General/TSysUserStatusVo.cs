using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：在线用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserStatusVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_USER_STATUS_TABLE = "T_SYS_USER_STATUS";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 用户编号
		/// </summary>
		public static string USER_ID_FIELD = "USER_ID";
		/// <summary>
		/// 最后一次访问时间
		/// </summary>
		public static string LAST_OPTIME_FIELD = "LAST_OPTIME";
		/// <summary>
		/// 最后一次登陆IP
		/// </summary>
		public static string LAST_LOGIN_IP_FIELD = "LAST_LOGIN_IP";
		/// <summary>
		/// 最后一次访问页面
		/// </summary>
		public static string LAST_PAGE_FIELD = "LAST_PAGE";
		/// <summary>
		/// 最后一次操作记录
		/// </summary>
		public static string LAST_OPERATION_FIELD = "LAST_OPERATION";
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
		
		public TSysUserStatusVo()
		{
			this.ID = "";
			this.USER_ID = "";
			this.LAST_OPTIME = "";
			this.LAST_LOGIN_IP = "";
			this.LAST_PAGE = "";
			this.LAST_OPERATION = "";
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
		/// 用户编号
		/// </summary>
		public string USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最后一次访问时间
		/// </summary>
		public string LAST_OPTIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最后一次登陆IP
		/// </summary>
		public string LAST_LOGIN_IP
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最后一次访问页面
		/// </summary>
		public string LAST_PAGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最后一次操作记录
		/// </summary>
		public string LAST_OPERATION
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