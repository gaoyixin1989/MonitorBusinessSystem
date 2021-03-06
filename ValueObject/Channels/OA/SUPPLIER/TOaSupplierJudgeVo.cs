using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商产品评价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierJudgeVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_SUPPLIER_JUDGE_TABLE = "T_OA_SUPPLIER_JUDGE";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 供应商ID
		/// </summary>
		public static string SUPPLIER_ID_FIELD = "SUPPLIER_ID";
		/// <summary>
		/// 物料名称
		/// </summary>
		public static string PARTNAME_FIELD = "PARTNAME";
		/// <summary>
		/// 规格型号
		/// </summary>
		public static string MODEL_FIELD = "MODEL";
		/// <summary>
		/// 参考价
		/// </summary>
		public static string REFERENCEPRICE_FIELD = "REFERENCEPRICE";
		/// <summary>
		/// 产品标准
		/// </summary>
		public static string PRODUCTSTANDARD_FIELD = "PRODUCTSTANDARD";
		/// <summary>
		/// 最短供货期
		/// </summary>
		public static string DELIVERYPERIOD_FIELD = "DELIVERYPERIOD";
		/// <summary>
		/// 供货数量
		/// </summary>
		public static string QUANTITY_FIELD = "QUANTITY";
		/// <summary>
		/// 供应商编码
		/// </summary>
		public static string ENTERPRISECODE_FIELD = "ENTERPRISECODE";
		/// <summary>
		/// 质量保证体系
		/// </summary>
		public static string QUATITYSYSTEM_FIELD = "QUATITYSYSTEM";
		/// <summary>
		/// 合同信守情况
		/// </summary>
		public static string SINCERITY_FIELD = "SINCERITY";
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
		
		public TOaSupplierJudgeVo()
		{
			this.ID = "";
			this.SUPPLIER_ID = "";
			this.PARTNAME = "";
			this.MODEL = "";
			this.REFERENCEPRICE = "";
			this.PRODUCTSTANDARD = "";
			this.DELIVERYPERIOD = "";
			this.QUANTITY = "";
			this.ENTERPRISECODE = "";
			this.QUATITYSYSTEM = "";
			this.SINCERITY = "";
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
		/// 供应商ID
		/// </summary>
		public string SUPPLIER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 物料名称
		/// </summary>
		public string PARTNAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string MODEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 参考价
		/// </summary>
		public string REFERENCEPRICE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 产品标准
		/// </summary>
		public string PRODUCTSTANDARD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最短供货期
		/// </summary>
		public string DELIVERYPERIOD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 供货数量
		/// </summary>
		public string QUANTITY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 供应商编码
		/// </summary>
		public string ENTERPRISECODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质量保证体系
		/// </summary>
		public string QUATITYSYSTEM
		{
			set ;
			get ;
		}
		/// <summary>
		/// 合同信守情况
		/// </summary>
		public string SINCERITY
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