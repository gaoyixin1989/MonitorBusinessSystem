using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工考核历史
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeExaminehistoryVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EMPLOYE_EXAMINEHISTORY_TABLE = "T_OA_EMPLOYE_EXAMINEHISTORY";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 员工编号
		/// </summary>
		public static string EMPLOYEID_FIELD = "EMPLOYEID";
        /// <summary>
        /// 年度
        /// </summary>
        public static string EX_YEAR_FIELD = "EX_YEAR";
        /// <summary>
        /// 评价
        /// </summary>
        public static string EX_INFO_FIELD = "EX_INFO";
		/// <summary>
		/// 所在单位
		/// </summary>
		public static string ATT_NAME_FIELD = "ATT_NAME";
		/// <summary>
		/// 附件路径
		/// </summary>
		public static string ATT_URL_FIELD = "ATT_URL";
		/// <summary>
		/// 附件说明
		/// </summary>
		public static string ATT_INFO_FIELD = "ATT_INFO";
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
		
		public TOaEmployeExaminehistoryVo()
		{
			this.ID = "";
			this.EMPLOYEID = "";
            this.EX_YEAR = "";
            this.EX_INFO = "";
			this.ATT_NAME = "";
			this.ATT_URL = "";
			this.ATT_INFO = "";
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
		/// 员工编号
		/// </summary>
		public string EMPLOYEID
		{
			set ;
			get ;
		}
        /// <summary>
        /// 年度
        /// </summary>
        public string EX_YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 评价
        /// </summary>
        public string EX_INFO
        {
            set;
            get;
        }
		/// <summary>
		/// 所在单位
		/// </summary>
		public string ATT_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件路径
		/// </summary>
		public string ATT_URL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 附件说明
		/// </summary>
		public string ATT_INFO
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