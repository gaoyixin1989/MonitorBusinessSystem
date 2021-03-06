using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核总结文件
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineFileVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EXAMINE_FILE_TABLE = "T_OA_EXAMINE_FILE";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 业务编码
		/// </summary>
		public static string EXAMINE_ID_FIELD = "EXAMINE_ID";
		/// <summary>
		/// 文件类型
		/// </summary>
		public static string FILE_TYPE_FIELD = "FILE_TYPE";
		/// <summary>
		/// 文件大小
		/// </summary>
		public static string FILE_SIZE_FIELD = "FILE_SIZE";
		/// <summary>
		/// 文件内容
		/// </summary>
		public static string FILE_BODY_FIELD = "FILE_BODY";
		/// <summary>
		/// 文件路径
		/// </summary>
		public static string FILE_PATH_FIELD = "FILE_PATH";
		/// <summary>
		/// 文件描述
		/// </summary>
		public static string FILE_DESC_FIELD = "FILE_DESC";
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
		
		public TOaExamineFileVo()
		{
			this.ID = "";
			this.EXAMINE_ID = "";
			this.FILE_TYPE = "";
			this.FILE_SIZE = "";
			this.FILE_PATH = "";
			this.FILE_DESC = "";
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
		/// 业务编码
		/// </summary>
		public string EXAMINE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件类型
		/// </summary>
		public string FILE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件大小
		/// </summary>
		public string FILE_SIZE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件内容
		/// </summary>
		public string FILE_BODY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件路径
		/// </summary>
		public string FILE_PATH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件描述
		/// </summary>
		public string FILE_DESC
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