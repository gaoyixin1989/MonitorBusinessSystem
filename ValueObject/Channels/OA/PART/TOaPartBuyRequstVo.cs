using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料采购申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_PART_BUY_REQUST_TABLE = "T_OA_PART_BUY_REQUST";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 申请科室
		/// </summary>
		public static string APPLY_DEPT_ID_FIELD = "APPLY_DEPT_ID";
		/// <summary>
		/// 申请人
		/// </summary>
		public static string APPLY_USER_ID_FIELD = "APPLY_USER_ID";
		/// <summary>
		/// 申请时间
		/// </summary>
		public static string APPLY_DATE_FIELD = "APPLY_DATE";
		/// <summary>
		/// 申请标题
		/// </summary>
		public static string APPLY_TITLE_FIELD = "APPLY_TITLE";
		/// <summary>
		/// 部门审批人
		/// </summary>
		public static string APP_DEPT_ID_FIELD = "APP_DEPT_ID";
		/// <summary>
		/// 部门审批时间
		/// </summary>
		public static string APP_DEPT_DATE_FIELD = "APP_DEPT_DATE";
		/// <summary>
		/// 部门审批意见
		/// </summary>
		public static string APP_DEPT_INFO_FIELD = "APP_DEPT_INFO";
		/// <summary>
		/// 技术负责人审批人
		/// </summary>
		public static string APP_MANAGER_ID_FIELD = "APP_MANAGER_ID";
		/// <summary>
		/// 技术负责人审批时间
		/// </summary>
		public static string APP_MANAGER_DATE_FIELD = "APP_MANAGER_DATE";
		/// <summary>
		/// 技术负责人审批意见
		/// </summary>
		public static string APP_MANAGER_INFO_FIELD = "APP_MANAGER_INFO";
		/// <summary>
		/// 站长审批人
		/// </summary>
		public static string APP_LEADER_ID_FIELD = "APP_LEADER_ID";
		/// <summary>
		/// 站长审批时间
		/// </summary>
		public static string APP_LEADER_DATE_FIELD = "APP_LEADER_DATE";
		/// <summary>
		/// 站长审批意见
		/// </summary>
		public static string APP_LEADER_INFO_FIELD = "APP_LEADER_INFO";
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
        /// <summary>
        /// 办公室意见
        /// </summary>
        public static string APP_OFFER_INFO_FIELD = "APP_OFFER_INFO";
        /// <summary>
        /// 办公室人
        /// </summary>
        public static string APP_OFFER_ID_FIELD = "APP_OFFER_ID";
        /// <summary>
        /// 办公室时间
        /// </summary>
        public static string APP_OFFER_TIME_FIELD = "APP_OFFER_TIME";
        /// <summary>
        /// 物料类别(01:办公用品 02:实验用品 03:标准物质)
        /// </summary>
        public static string APPLY_TYPE_FIELD = "APPLY_TYPE";
        /// <summary>
        /// 申购内容
        /// </summary>
        public static string APPLY_CONTENT_FIELD = "APPLY_CONTENT";

		#endregion
		
		public TOaPartBuyRequstVo()
		{
			this.ID = "";
			this.APPLY_DEPT_ID = "";
			this.APPLY_USER_ID = "";
			this.APPLY_DATE = "";
			this.APPLY_TITLE = "";
			this.APP_DEPT_ID = "";
			this.APP_DEPT_DATE = "";
			this.APP_DEPT_INFO = "";
			this.APP_MANAGER_ID = "";
			this.APP_MANAGER_DATE = "";
			this.APP_MANAGER_INFO = "";
			this.APP_LEADER_ID = "";
			this.APP_LEADER_DATE = "";
			this.APP_LEADER_INFO = "";
            this.APP_OFFER_INFO = "";
            this.APP_OFFER_ID = "";
            this.APP_OFFER_TIME = "";
			this.STATUS = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
            this.APPLY_TYPE = "";
            this.APPLY_CONTENT = "";
			
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
		/// 申请科室
		/// </summary>
		public string APPLY_DEPT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 申请人
		/// </summary>
		public string APPLY_USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 申请时间
		/// </summary>
		public string APPLY_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 申请标题
		/// </summary>
		public string APPLY_TITLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批人
		/// </summary>
		public string APP_DEPT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批时间
		/// </summary>
		public string APP_DEPT_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批意见
		/// </summary>
		public string APP_DEPT_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 技术负责人审批人
		/// </summary>
		public string APP_MANAGER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 技术负责人审批时间
		/// </summary>
		public string APP_MANAGER_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 技术负责人审批意见
		/// </summary>
		public string APP_MANAGER_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批人
		/// </summary>
		public string APP_LEADER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批时间
		/// </summary>
		public string APP_LEADER_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批意见
		/// </summary>
		public string APP_LEADER_INFO
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
        /// <summary>
        /// 办公室意见
        /// </summary>
        public string APP_OFFER_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 办公室人
        /// </summary>
        public string APP_OFFER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 办公室时间
        /// </summary>
        public string APP_OFFER_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 物料类别
        /// </summary>
        public string APPLY_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 申购内容
        /// </summary>
        public string APPLY_CONTENT
        {
            set;
            get;
        }
	
		
		#endregion
		
    }
}