using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：职位管理
    /// 创建日期：2012-10-23
    /// 创建人：潘德军
    /// </summary>
    public class TSysPostVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_POST_TABLE = "T_SYS_POST";
		//静态字段引用
		/// <summary>
		/// 角色编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 职位名
		/// </summary>
		public static string POST_NAME_FIELD = "POST_NAME";
		/// <summary>
		/// 上级职位ID
		/// </summary>
		public static string PARENT_POST_ID_FIELD = "PARENT_POST_ID";
		/// <summary>
		/// 行政级别
		/// </summary>
		public static string POST_LEVEL_ID_FIELD = "POST_LEVEL_ID";
		/// <summary>
		/// 所属部门
		/// </summary>
		public static string POST_DEPT_ID_FIELD = "POST_DEPT_ID";
		/// <summary>
		/// 角色说明
		/// </summary>
		public static string ROLE_NOTE_FIELD = "ROLE_NOTE";
		/// <summary>
		/// 树深度编号
		/// </summary>
		public static string TREE_LEVEL_FIELD = "TREE_LEVEL";
		/// <summary>
		/// 排序
		/// </summary>
		public static string NUM_FIELD = "NUM";
		/// <summary>
		/// 删除标记,1为删除
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
		/// 隐藏标记,对用户屏蔽
		/// </summary>
		public static string IS_HIDE_FIELD = "IS_HIDE";
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
		
		#endregion
		
		public TSysPostVo()
		{
			this.ID = "";
			this.POST_NAME = "";
			this.PARENT_POST_ID = "";
			this.POST_LEVEL_ID = "";
			this.POST_DEPT_ID = "";
			this.ROLE_NOTE = "";
			this.TREE_LEVEL = "";
			this.NUM = "";
			this.IS_DEL = "";
			this.CREATE_ID = "";
			this.CREATE_TIME = "";
			this.IS_HIDE = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 角色编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 职位名
		/// </summary>
		public string POST_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 上级职位ID
		/// </summary>
		public string PARENT_POST_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 行政级别
		/// </summary>
		public string POST_LEVEL_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所属部门
		/// </summary>
		public string POST_DEPT_ID
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
		/// 树深度编号
		/// </summary>
		public string TREE_LEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排序
		/// </summary>
		public string NUM
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
		/// 隐藏标记,对用户屏蔽
		/// </summary>
		public string IS_HIDE
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
	
		
		#endregion
		
    }
}