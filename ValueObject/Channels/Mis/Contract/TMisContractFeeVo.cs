using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书缴费表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractFeeVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_FEE_TABLE = "T_MIS_CONTRACT_FEE";
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
		/// 监测费用，监测费用明细总和
		/// </summary>
		public static string TEST_FEE_FIELD = "TEST_FEE";
		/// <summary>
		/// 附加总费用，附加费用明细总和
		/// </summary>
		public static string ATT_FEE_FIELD = "ATT_FEE";
		/// <summary>
		/// 预算总费用，监测总费用+附加总费用
		/// </summary>
		public static string BUDGET_FIELD = "BUDGET";
		/// <summary>
		/// 实际收费
		/// </summary>
		public static string INCOME_FIELD = "INCOME";
		/// <summary>
		/// 是否缴费
		/// </summary>
		public static string IF_PAY_FIELD = "IF_PAY";
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
		
		public TMisContractFeeVo()
		{
			this.ID = "";
			this.CONTRACT_ID = "";
			this.TEST_FEE = "";
			this.ATT_FEE = "";
			this.BUDGET = "";
			this.INCOME = "";
			this.IF_PAY = "";
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
		/// 监测费用，监测费用明细总和
		/// </summary>
		public string TEST_FEE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附加总费用，附加费用明细总和
		/// </summary>
		public string ATT_FEE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 预算总费用，监测总费用+附加总费用
		/// </summary>
		public string BUDGET
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实际收费
		/// </summary>
		public string INCOME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否缴费
		/// </summary>
		public string IF_PAY
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