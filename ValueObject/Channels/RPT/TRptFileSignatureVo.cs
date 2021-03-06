using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.RPT
{
    /// <summary>
    /// 功能：报告文件印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileSignatureVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_RPT_FILE_SIGNATURE_TABLE = "T_RPT_FILE_SIGNATURE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 文件ID
		/// </summary>
		public static string FILE_ID_FIELD = "FILE_ID";
		/// <summary>
		/// 印章名称
		/// </summary>
		public static string MARK_NAME_FIELD = "MARK_NAME";
		/// <summary>
		/// 添加用户
		/// </summary>
		public static string ADD_USER_FIELD = "ADD_USER";
		/// <summary>
		/// 添加日期
		/// </summary>
		public static string ADD_TIME_FIELD = "ADD_TIME";
		/// <summary>
		/// 添加IP
		/// </summary>
		public static string ADD_IP_FIELD = "ADD_IP";
		/// <summary>
		/// 印章
		/// </summary>
		public static string MARK_GUID_FIELD = "MARK_GUID";
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
		
		public TRptFileSignatureVo()
		{
			this.ID = "";
			this.FILE_ID = "";
			this.MARK_NAME = "";
			this.ADD_USER = "";
			this.ADD_TIME = "";
			this.ADD_IP = "";
			this.MARK_GUID = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// ID
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件ID
		/// </summary>
		public string FILE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 印章名称
		/// </summary>
		public string MARK_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 添加用户
		/// </summary>
		public string ADD_USER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 添加日期
		/// </summary>
		public string ADD_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 添加IP
		/// </summary>
		public string ADD_IP
		{
			set ;
			get ;
		}
		/// <summary>
		/// 印章
		/// </summary>
		public string MARK_GUID
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