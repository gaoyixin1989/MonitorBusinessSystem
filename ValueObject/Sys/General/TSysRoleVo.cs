using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：角色管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysRoleVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_ROLE_TABLE = "T_SYS_ROLE";
		//静态字段引用
		/// <summary>
		/// 是否为用户唯一
		/// </summary>
		public static string USER_ONLY_FIELD = "USER_ONLY";
		/// <summary>
		/// 启用标记,1为启用
		/// </summary>
		public static string IS_USE_FIELD = "IS_USE";
		/// <summary>
		/// 删除标记,1为删除
		/// </summary>
		public static string IS_DEL_FIELD = "IS_DEL";
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
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		/// <summary>
		/// 角色编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 角色名称
		/// </summary>
		public static string ROLE_NAME_FIELD = "ROLE_NAME";
		/// <summary>
		/// 角色类型
		/// </summary>
		public static string ROLE_TYPE_FIELD = "ROLE_TYPE";
		/// <summary>
		/// 角色说明
		/// </summary>
		public static string ROLE_NOTE_FIELD = "ROLE_NOTE";
		/// <summary>
		/// 创建人ID
		/// </summary>
		public static string CREATE_ID_FIELD = "CREATE_ID";
		/// <summary>
		/// 创建时间
		/// </summary>
		public static string CREATE_TIME_FIELD = "CREATE_TIME";
		
		#endregion
		
		public TSysRoleVo()
		{
			this.USER_ONLY = "";
			this.IS_USE = "";
			this.IS_DEL = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			this.ID = "";
			this.ROLE_NAME = "";
			this.ROLE_TYPE = "";
			this.ROLE_NOTE = "";
			this.CREATE_ID = "";
			this.CREATE_TIME = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 是否为用户唯一
		/// </summary>
		public string USER_ONLY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 启用标记,1为启用
		/// </summary>
		public string IS_USE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 删除标记,1为删除
		/// </summary>
		public string IS_DEL
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
		/// <summary>
		/// 角色编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string ROLE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 角色类型
		/// </summary>
		public string ROLE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 角色说明
		/// </summary>
		public string ROLE_NOTE
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
	
		
		#endregion
		
    }
}