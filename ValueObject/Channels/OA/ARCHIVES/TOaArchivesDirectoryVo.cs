using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：文件目录管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDirectoryVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_ARCHIVES_DIRECTORY_TABLE = "T_OA_ARCHIVES_DIRECTORY";
		//静态字段引用
		/// <summary>
		/// 主键
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 目录名称
		/// </summary>
		public static string DIRECTORY_NAME_FIELD = "DIRECTORY_NAME";
		/// <summary>
		/// 父目录ID，如果为根目录，则存储“0”
		/// </summary>
		public static string PARENT_ID_FIELD = "PARENT_ID";
		/// <summary>
		/// 使用状态(0为启用、1为停用)
		/// </summary>
		public static string IS_USE_FIELD = "IS_USE";
		/// <summary>
		/// 序号
		/// </summary>
		public static string NUM_FIELD = "NUM";
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
		
		public TOaArchivesDirectoryVo()
		{
			this.ID = "";
			this.DIRECTORY_NAME = "";
			this.PARENT_ID = "";
			this.IS_USE = "";
			this.NUM = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 主键
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 目录名称
		/// </summary>
		public string DIRECTORY_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 父目录ID，如果为根目录，则存储“0”
		/// </summary>
		public string PARENT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 使用状态(0为启用、1为停用)
		/// </summary>
		public string IS_USE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 序号
		/// </summary>
		public string NUM
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