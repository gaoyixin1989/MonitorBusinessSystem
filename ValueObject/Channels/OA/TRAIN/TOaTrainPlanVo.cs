using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训计划
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainPlanVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_TRAIN_PLAN_TABLE = "T_OA_TRAIN_PLAN";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 培训分类
		/// </summary>
		public static string TRAIN_TYPE_FIELD = "TRAIN_TYPE";
		/// <summary>
		/// 培训对象
		/// </summary>
		public static string TRAIN_TO_FIELD = "TRAIN_TO";
		/// <summary>
		/// 培训内容
		/// </summary>
		public static string TRAIN_INFO_FIELD = "TRAIN_INFO";
		/// <summary>
		/// 培训目标
		/// </summary>
		public static string TRAIN_TARGET_FIELD = "TRAIN_TARGET";
		/// <summary>
		/// 培训时间
		/// </summary>
		public static string TRAIN_DATE_FIELD = "TRAIN_DATE";
		/// <summary>
		/// 负责部门
		/// </summary>
		public static string DEPT_ID_FIELD = "DEPT_ID";
		/// <summary>
		/// 考核办法
		/// </summary>
		public static string EXAMINE_METHOD_FIELD = "EXAMINE_METHOD";
		/// <summary>
		/// 计划年度
		/// </summary>
		public static string PLAN_YEAR_FIELD = "PLAN_YEAR";
		/// <summary>
		/// 编制人
		/// </summary>
		public static string DRAFT_ID_FIELD = "DRAFT_ID";
		/// <summary>
		/// 编制时间
		/// </summary>
		public static string DRAFT_DATE_FIELD = "DRAFT_DATE";
		/// <summary>
		/// 审批人
		/// </summary>
		public static string APP_ID_FIELD = "APP_ID";
		/// <summary>
		/// 审批时间
		/// </summary>
		public static string APP_DATE_FIELD = "APP_DATE";
		/// <summary>
		/// 审批意见
		/// </summary>
		public static string APP_INFO_FIELD = "APP_INFO";
		/// <summary>
		/// 审批结果
		/// </summary>
		public static string APP_RESULT_FIELD = "APP_RESULT";
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
		
		public TOaTrainPlanVo()
		{
			this.ID = "";
            this.TRAIN_BT = "";
			this.TRAIN_TYPE = "";
			this.TRAIN_TO = "";
			this.TRAIN_INFO = "";
			this.TRAIN_TARGET = "";
			this.TRAIN_DATE = "";
			this.DEPT_ID = "";
			this.EXAMINE_METHOD = "";
			this.PLAN_YEAR = "";
			this.DRAFT_ID = "";
			this.DRAFT_DATE = "";
			this.APP_ID = "";
			this.APP_DATE = "";
			this.APP_INFO = "";
			this.APP_RESULT = "";
            this.RESULT_ID = "";
            this.RESULT_DATE = "";
            this.APP_FLOW = "";
            this.FLOW_STATUS = "";
            this.TYPES="";
            this.TRAIN_RESULT = "";
            this.TECH_APP = "";
            this.TECH_APP_ID = "";
            this.TECH_APP_DATE = "";
            this.TRAIN_COMPANY = "";
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
        /// 培训主题
        /// </summary>
        public string TRAIN_BT
        {
            set;
            get;
        }
		/// <summary>
		/// 培训分类
		/// </summary>
		public string TRAIN_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训对象
		/// </summary>
		public string TRAIN_TO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训内容
		/// </summary>
		public string TRAIN_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训目标
		/// </summary>
		public string TRAIN_TARGET
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训时间
		/// </summary>
		public string TRAIN_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 负责部门
		/// </summary>
		public string DEPT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核办法
		/// </summary>
		public string EXAMINE_METHOD
		{
			set ;
			get ;
		}

        /// <summary>
        /// 培训机构名称
        /// </summary>
        public string TRAIN_COMPANY
        {
            set;
            get;
        }
		/// <summary>
		/// 计划年度
		/// </summary>
		public string PLAN_YEAR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 编制人
		/// </summary>
		public string DRAFT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 编制时间
		/// </summary>
		public string DRAFT_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 审批人
		/// </summary>
		public string APP_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 审批时间
		/// </summary>
		public string APP_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 审批意见
		/// </summary>
		public string APP_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 审批结果
		/// </summary>
		public string APP_RESULT
		{
			set ;
			get ;
		}
        /// <summary>
        /// 审批环节
        /// </summary>
        public string APP_FLOW
        {
            set;
            get;
        }
        /// <summary>
        ///流转状态
        /// </summary>
        public string FLOW_STATUS
        {
            set;
            get;
        }

        /// <summary>
        ///培训类别1、年度，2、具体
        /// </summary>
        public string TYPES
        {
            set;
            get;
        }

        /// <summary>
        ///培训结果
        /// </summary>
        public string TRAIN_RESULT
        {
            set;
            get;
        }
        /// <summary>
        ///培训录入人
        /// </summary>
        public string RESULT_ID
        { 
            get; 
            set; 
        }

        /// <summary>
        ///培训结果录入时间
        /// </summary>
        public string RESULT_DATE 
        { 
            get;
            set;
        }
        /// <summary>
        //技术负责人意见
        /// </summary>
        public string TECH_APP
        {
            set;
            get;
        }
        /// <summary>
        //技术负责人
        /// </summary>
        public string TECH_APP_ID
        {
            set;
            get;
        }
        /// <summary>
        //技术负责人日期
        /// </summary>
        public string TECH_APP_DATE
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