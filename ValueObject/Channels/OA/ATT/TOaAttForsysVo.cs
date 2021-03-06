using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ATT
{
    /// <summary>
    /// 功能：附件业务登记
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttForsysVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_ATT_FORSYS_TABLE = "T_OA_ATT_FORSYS";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 业务ID
		/// </summary>
		public static string BUSINESSID_FIELD = "BUSINESSID";
		/// <summary>
		/// 附件名称
		/// </summary>
		public static string ATTACHEMNTNAME_FIELD = "ATTACHEMNTNAME";
		/// <summary>
		/// 附件路径
		/// </summary>
		public static string ATTACHPATH_FIELD = "ATTACHPATH";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARKS_FIELD = "REMARKS";
		
		#endregion
		
		public TOaAttForsysVo()
		{
			this.ID = "";
			this.BUSINESSID = "";
			this.ATTACHEMNTNAME = "";
			this.ATTACHPATH = "";
			this.REMARKS = "";
			
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
		/// 业务ID
		/// </summary>
		public string BUSINESSID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件名称
		/// </summary>
		public string ATTACHEMNTNAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件路径
		/// </summary>
		public string ATTACHPATH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string REMARKS
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}