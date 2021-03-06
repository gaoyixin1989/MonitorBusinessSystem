using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：角色监测点权限
    /// 创建日期：2011-04-13
    /// 创建人：郑义
    /// </summary>
    public class TSysBaseRolePointVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_BASE_ROLE_POINT_TABLE = "T_SYS_BASE_ROLE_POINT";
		//静态字段引用
		/// <summary>
		/// 备注3
		/// </summary>
		public static string REMARK3_FIELD = "REMARK3";
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 角色编号
		/// </summary>
		public static string ROLE_ID_FIELD = "ROLE_ID";
		/// <summary>
		/// 监测点编号
		/// </summary>
		public static string POINT_ID_FIELD = "POINT_ID";
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
		
		#endregion
		
		public TSysBaseRolePointVo()
		{
			this.REMARK3 = "";
			this.ID = "";
			this.ROLE_ID = "";
			this.POINT_ID = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 角色编号
		/// </summary>
		public string ROLE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测点编号
		/// </summary>
		public string POINT_ID
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
	
		
		#endregion
		
    }
}