using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料验收清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartAcceptedVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_PART_ACCEPTED_TABLE = "T_OA_PART_ACCEPTED";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 物料ID
		/// </summary>
		public static string PART_ID_FIELD = "PART_ID";
		/// <summary>
		/// 需求数量
		/// </summary>
		public static string NEED_QUANTITY_FIELD = "NEED_QUANTITY";
		/// <summary>
		/// 用途
		/// </summary>
		public static string USERDO_FIELD = "USERDO";
		/// <summary>
		/// 供应商名称
		/// </summary>
		public static string ENTERPRISE_NAME_FIELD = "ENTERPRISE_NAME";
		/// <summary>
		/// 浓度范围
		/// </summary>
		public static string RANGE_FIELD = "RANGE";
		/// <summary>
		/// 标准值/不确定度
		/// </summary>
		public static string STANDARD_FIELD = "STANDARD";
		/// <summary>
		/// 稀释倍数
		/// </summary>
		public static string RATIO_FIELD = "RATIO";
		/// <summary>
		/// 单价
		/// </summary>
		public static string PRICE_FIELD = "PRICE";
		/// <summary>
		/// 金额
		/// </summary>
		public static string AMOUNT_FIELD = "AMOUNT";
		/// <summary>
		/// 收货日期
		/// </summary>
		public static string RECIVEPART_DATE_FIELD = "RECIVEPART_DATE";
		/// <summary>
		/// 检验日期
		/// </summary>
		public static string CHECK_DATE_FIELD = "CHECK_DATE";
		/// <summary>
		/// 验收情况
		/// </summary>
		public static string CHECK_RESULT_FIELD = "CHECK_RESULT";
		/// <summary>
		/// 验收人ID
		/// </summary>
		public static string CHECK_USERID_FIELD = "CHECK_USERID";
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
        /// <summary>
        /// 标识
        /// </summary>
        public static string FLAG_FIELD = "FLAG";
		#endregion
		
		public TOaPartAcceptedVo()
		{
			this.ID = "";
			this.PART_ID = "";
			this.NEED_QUANTITY = "";
			this.USERDO = "";
			this.ENTERPRISE_NAME = "";
			this.RANGE = "";
			this.STANDARD = "";
			this.RATIO = "";
			this.PRICE = "";
			this.AMOUNT = "";
			this.RECIVEPART_DATE = "";
			this.CHECK_DATE = "";
			this.CHECK_RESULT = "";
			this.CHECK_USERID = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
            this.FLAG = "";
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
		/// 用途
		/// </summary>
		public string USERDO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string ENTERPRISE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 浓度范围
		/// </summary>
		public string RANGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 标准值/不确定度
		/// </summary>
		public string STANDARD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 稀释倍数
		/// </summary>
		public string RATIO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单价
		/// </summary>
		public string PRICE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 金额
		/// </summary>
		public string AMOUNT
		{
			set ;
			get ;
		}
        /// <summary>
        /// 标识
        /// </summary>
        public string FLAG
        {
            set;
            get;
        }
		/// <summary>
		/// 收货日期
		/// </summary>
		public string RECIVEPART_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检验日期
		/// </summary>
		public string CHECK_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 验收情况
		/// </summary>
		public string CHECK_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 验收人ID
		/// </summary>
		public string CHECK_USERID
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