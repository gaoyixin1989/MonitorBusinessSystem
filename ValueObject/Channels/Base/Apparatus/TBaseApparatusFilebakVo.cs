using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器资料备份
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusFilebakVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_APPARATUS_FILEBAK_TABLE = "T_BASE_APPARATUS_FILEBAK";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 仪器ID
		/// </summary>
		public static string APPARATUS_CODE_FIELD = "APPARATUS_CODE";
		/// <summary>
		/// 资料编号
		/// </summary>
		public static string APPARATUS_FILE_CODE_FIELD = "APPARATUS_FILE_CODE";
		/// <summary>
		/// 资料名称
		/// </summary>
		public static string APPARATUS_ATT_NAME_FIELD = "APPARATUS_ATT_NAME";
		/// <summary>
		/// 资料保存目录ID
		/// </summary>
		public static string APPARATUS_ATT_FOLDER_ID_FIELD = "APPARATUS_ATT_FOLDER_ID";
		/// <summary>
		/// 资料保存文件ID
		/// </summary>
		public static string APPARATUS_ATT_FILE_ID_FIELD = "APPARATUS_ATT_FILE_ID";
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
		
		public TBaseApparatusFilebakVo()
		{
			this.ID = "";
			this.APPARATUS_CODE = "";
			this.APPARATUS_FILE_CODE = "";
			this.APPARATUS_ATT_NAME = "";
			this.APPARATUS_ATT_FOLDER_ID = "";
			this.APPARATUS_ATT_FILE_ID = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
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
		/// 仪器ID
		/// </summary>
		public string APPARATUS_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 资料编号
		/// </summary>
		public string APPARATUS_FILE_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 资料名称
		/// </summary>
		public string APPARATUS_ATT_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 资料保存目录ID
		/// </summary>
		public string APPARATUS_ATT_FOLDER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 资料保存文件ID
		/// </summary>
		public string APPARATUS_ATT_FILE_ID
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