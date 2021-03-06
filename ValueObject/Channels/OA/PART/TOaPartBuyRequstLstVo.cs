using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料采购申请清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstLstVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_PART_BUY_REQUST_LST_TABLE = "T_OA_PART_BUY_REQUST_LST";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 申请单ID
		/// </summary>
		public static string REQUST_ID_FIELD = "REQUST_ID";
		/// <summary>
		/// 物料ID
		/// </summary>
		public static string PART_ID_FIELD = "PART_ID";
		/// <summary>
		/// 需求数量
		/// </summary>
		public static string NEED_QUANTITY_FIELD = "NEED_QUANTITY";
		/// <summary>
		/// 采购用途
		/// </summary>
		public static string USERDO_FIELD = "USERDO";
		/// <summary>
		/// 要求交货期限
		/// </summary>
		public static string DELIVERY_DATE_FIELD = "DELIVERY_DATE";
		/// <summary>
		/// 计划资金
		/// </summary>
		public static string BUDGET_MONEY_FIELD = "BUDGET_MONEY";
		/// <summary>
		/// 状态,1待审批，2待采购，3已采购
		/// </summary>
		public static string STATUS_FIELD = "STATUS";
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
		
		public TOaPartBuyRequstLstVo()
		{
			this.ID = "";
			this.REQUST_ID = "";
			this.PART_ID = "";
			this.NEED_QUANTITY = "";
			this.USERDO = "";
			this.DELIVERY_DATE = "";
			this.BUDGET_MONEY = "";
			this.STATUS = "";
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
		/// 申请单ID
		/// </summary>
		public string REQUST_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string PART_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 需求数量
		/// </summary>
		public string NEED_QUANTITY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 采购用途
		/// </summary>
		public string USERDO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 要求交货期限
		/// </summary>
		public string DELIVERY_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 计划资金
		/// </summary>
		public string BUDGET_MONEY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 状态,1待审批，2待采购，3已采购
		/// </summary>
		public string STATUS
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