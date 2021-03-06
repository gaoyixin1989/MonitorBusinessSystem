using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.RPT
{
    /// <summary>
    /// 功能：印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptSignatureVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_RPT_SIGNATURE_TABLE = "T_RPT_SIGNATURE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 印章名称
		/// </summary>
		public static string MARK_NAME_FIELD = "MARK_NAME";
		/// <summary>
		/// 印章文件
		/// </summary>
		public static string MARK_BODY_FIELD = "MARK_BODY";
		/// <summary>
		/// 文件类型
		/// </summary>
		public static string MARK_TYPE_FIELD = "MARK_TYPE";
		/// <summary>
		/// 文件路径
		/// </summary>
		public static string MARK_PATH_FIELD = "MARK_PATH";
		/// <summary>
		/// 文件大小
		/// </summary>
		public static string MARK_SIZE_FIELD = "MARK_SIZE";
		/// <summary>
		/// 用户名
		/// </summary>
		public static string USER_NAME_FIELD = "USER_NAME";
		/// <summary>
		/// 用户密码
		/// </summary>
		public static string PASS_WORD_FIELD = "PASS_WORD";
		/// <summary>
		/// 添加日期
		/// </summary>
		public static string ADD_TIME_FIELD = "ADD_TIME";
		/// <summary>
		/// 添加IP
		/// </summary>
		public static string ADD_USER_FIELD = "ADD_USER";
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
		
		public TRptSignatureVo()
		{
			this.ID = "";
			this.MARK_NAME = "";
            this.MARK_BODY = null;
			this.MARK_TYPE = "";
			this.MARK_PATH = "";
			this.MARK_SIZE = "";
			this.USER_NAME = "";
			this.PASS_WORD = "";
			this.ADD_TIME = "";
			this.ADD_USER = "";
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
		/// 印章名称
		/// </summary>
		public string MARK_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 印章文件
		/// </summary>
		public byte[] MARK_BODY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件类型
		/// </summary>
		public string MARK_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件路径
		/// </summary>
		public string MARK_PATH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文件大小
		/// </summary>
		public string MARK_SIZE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string USER_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用户密码
		/// </summary>
		public string PASS_WORD
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
		public string ADD_USER
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