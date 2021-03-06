using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：菜单管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysMenuVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_MENU_TABLE = "T_SYS_MENU";
		//静态字段引用
		/// <summary>
		/// 页面中重点显示(9宫格)1为重点展示
		/// </summary>
		public static string IS_IMPORTANT_FIELD = "IS_IMPORTANT";
		/// <summary>
		/// 启用标记,1为启用
		/// </summary>
		public static string IS_USE_FIELD = "IS_USE";
		/// <summary>
		/// 删除标记,1为删除
		/// </summary>
		public static string IS_DEL_FIELD = "IS_DEL";
        /// <summary>
        /// 隐藏标记
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
		/// <summary>
		/// 菜单编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 显示名称
		/// </summary>
		public static string MENU_TEXT_FIELD = "MENU_TEXT";
		/// <summary>
		/// 超链接或地址
		/// </summary>
		public static string MENU_URL_FIELD = "MENU_URL";
		/// <summary>
		/// 菜单说明
		/// </summary>
		public static string MENU_COMMENT_FIELD = "MENU_COMMENT";
		/// <summary>
		/// 图片位置(小图标)
		/// </summary>
		public static string MENU_IMGURL_FIELD = "MENU_IMGURL";
		/// <summary>
		/// 重点展示图片位置
		/// </summary>
		public static string MENU_BIGIMGURL_FIELD = "MENU_BIGIMGURL";
		/// <summary>
		/// 父节点ID(如果为0,为主节点)
		/// </summary>
		public static string PARENT_ID_FIELD = "PARENT_ID";
		/// <summary>
		/// 排序(本父节点内)
		/// </summary>
		public static string ORDER_ID_FIELD = "ORDER_ID";

        /// <summary>
        /// 菜单类型
        /// </summary>
        public static string MENU_TYPE_FIELD = "MENU_TYPE";

        /// <summary>
        /// 是否快捷菜单
        /// </summary>
        public static string IS_SHORTCUT_FIELD = "IS_SHORTCUT";
		
		#endregion
		
		public TSysMenuVo()
		{
			this.IS_IMPORTANT = "";
			this.IS_USE = "";
			this.IS_DEL = "";
            this.IS_HIDE = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			this.ID = "";
			this.MENU_TEXT = "";
			this.MENU_URL = "";
			this.MENU_COMMENT = "";
			this.MENU_IMGURL = "";
			this.MENU_BIGIMGURL = "";
            this.ORDER_ID = "";
			this.PARENT_ID = "";
            this.MENU_TYPE = "";
            this.IS_SHORTCUT = "";
		}
		
		#region 属性
			/// <summary>
		/// 页面中重点显示(9宫格)1为重点展示
		/// </summary>
		public string IS_IMPORTANT
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
        /// 隐藏标记
        /// </summary>
        public string IS_HIDE
        {
            set;
            get;
        }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MENU_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 是否快捷菜单
        /// </summary>
        public string IS_SHORTCUT
        {
            set;
            get;
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
		/// 菜单编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 显示名称
		/// </summary>
		public string MENU_TEXT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 超链接或地址
		/// </summary>
		public string MENU_URL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 菜单说明
		/// </summary>
		public string MENU_COMMENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 图片位置(小图标)
		/// </summary>
		public string MENU_IMGURL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 重点展示图片位置
		/// </summary>
		public string MENU_BIGIMGURL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 父节点ID(如果为0,为主节点)
		/// </summary>
		public string PARENT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排序(本父节点内)
		/// </summary>
		public string ORDER_ID
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}