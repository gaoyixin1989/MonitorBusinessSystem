using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：评价标准条件项项目信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConItemVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_EVALUATION_CON_ITEM_TABLE = "T_BASE_EVALUATION_CON_ITEM";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 评价标准ID
		/// </summary>
		public static string STANDARD_ID_FIELD = "STANDARD_ID";
		/// <summary>
		/// 条件项ID
		/// </summary>
		public static string CONDITION_ID_FIELD = "CONDITION_ID";
		/// <summary>
		/// 监测项目ID
		/// </summary>
		public static string ITEM_ID_FIELD = "ITEM_ID";
		/// <summary>
		/// 上限运算符
		/// </summary>
		public static string UPPER_OPERATOR_FIELD = "UPPER_OPERATOR";
		/// <summary>
		/// 下限运算符
		/// </summary>
		public static string LOWER_OPERATOR_FIELD = "LOWER_OPERATOR";
		/// <summary>
		/// 排放上限
		/// </summary>
		public static string DISCHARGE_UPPER_FIELD = "DISCHARGE_UPPER";
		/// <summary>
		/// 排放下限
		/// </summary>
		public static string DISCHARGE_LOWER_FIELD = "DISCHARGE_LOWER";
		/// <summary>
		/// 单位
		/// </summary>
		public static string UNIT_FIELD = "UNIT";
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
		
		public TBaseEvaluationConItemVo()
		{
			this.ID = "";
			this.STANDARD_ID = "";
			this.CONDITION_ID = "";
            this.MONITOR_ID = "";
            this.MONITOR_VALUE_ID = "";
			this.ITEM_ID = "";
			this.UPPER_OPERATOR = "";
			this.LOWER_OPERATOR = "";
			this.DISCHARGE_UPPER = "";
			this.DISCHARGE_LOWER = "";
			this.UNIT = "";
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
		/// 评价标准ID
		/// </summary>
		public string STANDARD_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 条件项ID
		/// </summary>
		public string CONDITION_ID
		{
			set ;
			get ;
		}

        /// <summary>
        /// 监测类型ID
        /// </summary>
        public string MONITOR_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 监测值类型编码
        /// </summary>
        public string MONITOR_VALUE_ID
        {
            set;
            get;
        }
		/// <summary>
		/// 监测值项目
		/// </summary>
		public string ITEM_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 上限运算符
		/// </summary>
		public string UPPER_OPERATOR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 下限运算符
		/// </summary>
		public string LOWER_OPERATOR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排放上限
		/// </summary>
		public string DISCHARGE_UPPER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排放下限
		/// </summary>
		public string DISCHARGE_LOWER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string UNIT
		{
			set ;
			get ;
		}
		/// <summary>
        /// 删除标记
		/// </summary>
        public string IS_DEL
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