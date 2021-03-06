using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：评价标准条件项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_EVALUATION_CON_INFO_TABLE = "T_BASE_EVALUATION_CON_INFO";
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
		/// 条件项编号
		/// </summary>
		public static string CONDITION_CODE_FIELD = "CONDITION_CODE";
		/// <summary>
		/// 父节点ID，如果为根节点，则父节点为“0”
		/// </summary>
		public static string PARENT_ID_FIELD = "PARENT_ID";
		/// <summary>
		/// 条件项名称
		/// </summary>
		public static string CONDITION_NAME_FIELD = "CONDITION_NAME";
		/// <summary>
		/// 条件项说明
		/// </summary>
		public static string CONDITION_REMARK_FIELD = "CONDITION_REMARK";
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
		
		public TBaseEvaluationConInfoVo()
		{
			this.ID = "";
			this.STANDARD_ID = "";
			this.CONDITION_CODE = "";
			this.PARENT_ID = "";
			this.CONDITION_NAME = "";
			this.CONDITION_REMARK = "";
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
		/// 条件项编号
		/// </summary>
		public string CONDITION_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 父节点ID，如果为根节点，则父节点为“0”
		/// </summary>
		public string PARENT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 条件项名称
		/// </summary>
		public string CONDITION_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 条件项说明
		/// </summary>
		public string CONDITION_REMARK
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