using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Point
{
    /// <summary>
    /// 功能：监测点项目明细表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyPointItemVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_COMPANY_POINT_ITEM_TABLE = "T_BASE_COMPANY_POINT_ITEM";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测点ID
		/// </summary>
		public static string POINT_ID_FIELD = "POINT_ID";
		/// <summary>
		/// 监测项目ID
		/// </summary>
		public static string ITEM_ID_FIELD = "ITEM_ID";
		/// <summary>
		/// 已选条件项ID
		/// </summary>
		public static string CONDITION_ID_FIELD = "CONDITION_ID";
		/// <summary>
		/// 条件项类型（1，国标；2，行标；3，地标）
		/// </summary>
		public static string CONDITION_TYPE_FIELD = "CONDITION_TYPE";
		/// <summary>
		/// 国标上限
		/// </summary>
		public static string ST_UPPER_FIELD = "ST_UPPER";
		/// <summary>
		/// 国标下限
		/// </summary>
		public static string ST_LOWER_FIELD = "ST_LOWER";
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
		
		public TBaseCompanyPointItemVo()
		{
			this.ID = "";
			this.POINT_ID = "";
			this.ITEM_ID = "";
			this.CONDITION_ID = "";
			this.CONDITION_TYPE = "";
			this.ST_UPPER = "";
			this.ST_LOWER = "";
            this.IS_DEL = "";
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
		/// 监测点ID
		/// </summary>
		public string POINT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测项目ID
		/// </summary>
		public string ITEM_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 已选条件项ID
		/// </summary>
		public string CONDITION_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 条件项类型（1，国标；2，行标；3，地标）
		/// </summary>
		public string CONDITION_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 国标上限
		/// </summary>
		public string ST_UPPER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 国标下限
		/// </summary>
		public string ST_LOWER
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