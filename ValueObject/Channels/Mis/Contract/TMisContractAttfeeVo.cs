using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书附加费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_ATTFEE_TABLE = "T_MIS_CONTRACT_ATTFEE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 委托书ID
		/// </summary>
		public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
		/// <summary>
		/// 频次次数
		/// </summary>
		public static string ATT_FEE_ITEM_ID_FIELD = "ATT_FEE_ITEM_ID";
		/// <summary>
		/// 实际附加费用
		/// </summary>
		public static string FEE_FIELD = "FEE";
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
		
		public TMisContractAttfeeVo()
		{
			this.ID = "";
			this.CONTRACT_ID = "";
			this.ATT_FEE_ITEM_ID = "";
			this.FEE = "";
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
		/// 委托书ID
		/// </summary>
		public string CONTRACT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 频次次数
		/// </summary>
		public string ATT_FEE_ITEM_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实际附加费用
		/// </summary>
		public string FEE
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