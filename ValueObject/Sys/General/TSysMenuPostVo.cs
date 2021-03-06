using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：权限管理
    /// 创建日期：2012-10-25
    /// 创建人：潘德军
    /// </summary>
    public class TSysMenuPostVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_MENU_POST_TABLE = "T_SYS_MENU_POST";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 人员ID
		/// </summary>
		public static string POST_ID_FIELD = "POST_ID";
		/// <summary>
		/// 人员职位类型,1,人员，2，职位
		/// </summary>
		public static string RIGHT_TYPE_FIELD = "RIGHT_TYPE";
		/// <summary>
		/// 菜单编号
		/// </summary>
		public static string MENU_ID_FIELD = "MENU_ID";
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
		
		public TSysMenuPostVo()
		{
			this.ID = "";
			this.POST_ID = "";
			this.RIGHT_TYPE = "";
			this.MENU_ID = "";
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
		/// 人员ID
		/// </summary>
		public string POST_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 人员职位类型,1,人员，2，职位
		/// </summary>
		public string RIGHT_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 菜单编号
		/// </summary>
		public string MENU_ID
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