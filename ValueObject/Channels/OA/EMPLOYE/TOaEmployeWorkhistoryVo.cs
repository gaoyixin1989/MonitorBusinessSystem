using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工工作经历
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeWorkhistoryVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EMPLOYE_WORKHISTORY_TABLE = "T_OA_EMPLOYE_WORKHISTORY";
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
		/// 所在单位
		/// </summary>
		public static string WORKCOMPANY_FIELD = "WORKCOMPANY";
		/// <summary>
		/// 时间
		/// </summary>
		public static string WORKDATE_FIELD = "WORKDATE";
		/// <summary>
		/// 岗位
		/// </summary>
		public static string POSITION_FIELD = "POSITION";
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
		
		public TOaEmployeWorkhistoryVo()
		{
			this.ID = "";
			this.EMPLOYEID = "";
			this.WORKCOMPANY = "";
			this.WORKBEGINDATE = "";
            this.WORKENDDATE = "";
			this.POSITION = "";
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
		/// 所在单位
		/// </summary>
		public string WORKCOMPANY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public string WORKBEGINDATE
		{
			set ;
			get ;
		}

        /// <summary>
        /// 截止时间
        /// </summary>
        public string WORKENDDATE
        {
            set;
            get;
        }
		/// <summary>
		/// 岗位
		/// </summary>
		public string POSITION
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