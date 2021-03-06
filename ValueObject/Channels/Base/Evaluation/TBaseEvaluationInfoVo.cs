using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：监测类别管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_EVALUATION_INFO_TABLE = "T_BASE_EVALUATION_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 评价标准编号
		/// </summary>
		public static string STANDARD_CODE_FIELD = "STANDARD_CODE";
		/// <summary>
		/// 评价标准名称
		/// </summary>
		public static string STANDARD_NAME_FIELD = "STANDARD_NAME";
		/// <summary>
		/// 评价标准类别(国家标准、行业标准、地方标准、国际标准)
		/// </summary>
		public static string STANDARD_TYPE_FIELD = "STANDARD_TYPE";
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public static string MONITOR_ID_FIELD = "MONITOR_ID";
		/// <summary>
		/// 生效日期
		/// </summary>
		public static string EFFECTIVE_DATE_FIELD = "EFFECTIVE_DATE";
		/// <summary>
		/// 附件路径
		/// </summary>
		public static string ATTACHMENT_URL_FIELD = "ATTACHMENT_URL";
		/// <summary>
		/// 评价标准描述
		/// </summary>
		public static string DESCRIPTION_FIELD = "DESCRIPTION";
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
		
		public TBaseEvaluationInfoVo()
		{
			this.ID = "";
			this.STANDARD_CODE = "";
			this.STANDARD_NAME = "";
			this.STANDARD_TYPE = "";
			this.MONITOR_ID = "";
			this.EFFECTIVE_DATE = "";
			this.ATTACHMENT_URL = "";
			this.DESCRIPTION = "";
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
		/// 评价标准编号
		/// </summary>
		public string STANDARD_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价标准名称
		/// </summary>
		public string STANDARD_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价标准类别(国家标准、行业标准、地方标准、国际标准)
		/// </summary>
		public string STANDARD_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public string MONITOR_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 生效日期
		/// </summary>
		public string EFFECTIVE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件路径
		/// </summary>
		public string ATTACHMENT_URL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价标准描述
		/// </summary>
		public string DESCRIPTION
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