using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书附加费用单价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeitemVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_ATTFEEITEM_TABLE = "T_MIS_CONTRACT_ATTFEEITEM";
		//静态字段引用
		/// <summary>
		/// 
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 附加项目
		/// </summary>
		public static string ATT_FEE_ITEM_FIELD = "ATT_FEE_ITEM";
		/// <summary>
		/// 费用单价
		/// </summary>
		public static string PRICE_FIELD = "PRICE";
		/// <summary>
		/// 费用描述
		/// </summary>
		public static string INFO_FIELD = "INFO";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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
		
		public TMisContractAttfeeitemVo()
		{
			this.ID = "";
			this.ATT_FEE_ITEM = "";
			this.PRICE = "";
			this.INFO = "";
            this.IS_DEL = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附加项目
		/// </summary>
		public string ATT_FEE_ITEM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 费用单价
		/// </summary>
		public string PRICE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 费用描述
		/// </summary>
		public string INFO
		{
			set ;
			get ;
		}
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
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