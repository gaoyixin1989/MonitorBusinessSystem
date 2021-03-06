using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EXAMINE_INFO_TABLE = "T_OA_EXAMINE_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 用户ID
		/// </summary>
		public static string USERID_FIELD = "USERID";
		/// <summary>
		/// 考核类型，1事业单位工作人员年度考核，2专业技术人员年度考核
		/// </summary>
		public static string EXAMINE_TYPE_FIELD = "EXAMINE_TYPE";
		/// <summary>
		/// 考核时间
		/// </summary>
		public static string EXAMINE_DATE_FIELD = "EXAMINE_DATE";
		/// <summary>
		/// 考核年度
		/// </summary>
		public static string EXAMINE_YEAR_FIELD = "EXAMINE_YEAR";
		/// <summary>
		/// 考核状态,1未发送，2待审批，3已审批
		/// </summary>
		public static string EXAMINE_STATUS_FIELD = "EXAMINE_STATUS";
		/// <summary>
		/// 部门考核评语
		/// </summary>
		public static string DEPT_APP_FIELD = "DEPT_APP";
		/// <summary>
		/// 单位考核评语
		/// </summary>
		public static string LEADER_APP_FIELD = "LEADER_APP";
		/// <summary>
		/// 主管单位意见
		/// </summary>
		public static string SUPERIOR_APP_FIELD = "SUPERIOR_APP";
		/// <summary>
		/// 个人意见
		/// </summary>
		public static string OPINION_FIELD = "OPINION";
		/// <summary>
		/// 复核或申诉情况说明
		/// </summary>
		public static string APPEAL_FIELD = "APPEAL";
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
		
		public TOaExamineInfoVo()
		{
			this.ID = "";
			this.USERID = "";
			this.EXAMINE_TYPE = "";
			this.EXAMINE_DATE = "";
			this.EXAMINE_YEAR = "";
			this.EXAMINE_STATUS = "";
            this.EXAMINE_CONTENT = "";
            this.EXAMINE_LEVEL = "";
			this.DEPT_APP = "";
            this.DEPT_APP_ID = "";
            this.DEPT_APP_DATE = "";
			this.LEADER_APP = "";
            this.LEADER_APP_ID = "";
            this.LEADER_APP_DATE = "";
			this.SUPERIOR_APP = "";
            this.SUPERIOR_APP_ID = "";
            this.SUPERIOR_APP_DATE = "";
			this.OPINION = "";
            this.OPINION_DATE = "";
			this.APPEAL = "";
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
		/// 用户ID
		/// </summary>
		public string USERID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核类型，1事业单位工作人员年度考核，2专业技术人员年度考核
		/// </summary>
		public string EXAMINE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核时间
		/// </summary>
		public string EXAMINE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核年度
		/// </summary>
		public string EXAMINE_YEAR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核状态,1未发送，2待审批，3已审批
		/// </summary>
		public string EXAMINE_STATUS
		{
			set ;
			get ;
		}

        /// <summary>
        /// 个人考核内容
        /// </summary>
        public string EXAMINE_CONTENT
        {
            set;
            get;
        }

        /// <summary>
        /// 考核等级
        /// </summary>
        public string EXAMINE_LEVEL
        {
            set;
            get;
        }
		/// <summary>
		/// 部门考核评语
		/// </summary>
		public string DEPT_APP
		{
			set ;
			get ;
		}
        /// <summary>
        /// 部门考核负责人ID
        /// </summary>
        public string DEPT_APP_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 部门考核时间
        /// </summary>
        public string DEPT_APP_DATE
        {
            set;
            get;
        }
		/// <summary>
		/// 单位考核评语
		/// </summary>
        public string LEADER_APP
		{
			set ;
			get ;
		}
        /// <summary>
        /// 单位考核负责人ID
        /// </summary>
        public string LEADER_APP_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 单位考核时间
        /// </summary>
        public string LEADER_APP_DATE
        {
            set;
            get;
        }
		/// <summary>
		/// 主管单位意见
		/// </summary>
		public string SUPERIOR_APP
		{
			set ;
			get ;
		}

        /// <summary>
        /// 主管单位意见负责人ID
        /// </summary>
        public string SUPERIOR_APP_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 主管单位意见时间
        /// </summary>
        public string SUPERIOR_APP_DATE
        {
            set;
            get;
        }
		/// <summary>
		/// 个人意见
		/// </summary>
		public string OPINION
		{
			set ;
			get ;
		}

        /// <summary>
        /// 个人意见填入时间
        /// </summary>
        public string OPINION_DATE
        {
            set;
            get;
        }
		/// <summary>
		/// 复核或申诉情况说明
		/// </summary>
		public string APPEAL
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