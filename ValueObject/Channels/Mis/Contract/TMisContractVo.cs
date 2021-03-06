using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_CONTRACT_TABLE = "T_MIS_CONTRACT";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 合同编号
		/// </summary>
		public static string CONTRACT_CODE_FIELD = "CONTRACT_CODE";
		/// <summary>
		/// 委托年度
		/// </summary>
		public static string CONTRACT_YEAR_FIELD = "CONTRACT_YEAR";
		/// <summary>
		/// 项目名称
		/// </summary>
		public static string PROJECT_NAME_FIELD = "PROJECT_NAME";
		/// <summary>
		/// 委托类型
		/// </summary>
		public static string CONTRACT_TYPE_FIELD = "CONTRACT_TYPE";
		/// <summary>
		/// 监测类型,冗余字段，如：废水,废气,噪声
		/// </summary>
		public static string TEST_TYPES_FIELD = "TEST_TYPES";
		/// <summary>
		/// 报告类型
		/// </summary>
		public static string TEST_TYPE_FIELD = "TEST_TYPE";
		/// <summary>
		/// 监测目的
		/// </summary>
		public static string TEST_PURPOSE_FIELD = "TEST_PURPOSE";
		/// <summary>
		/// 委托企业ID
		/// </summary>
		public static string CLIENT_COMPANY_ID_FIELD = "CLIENT_COMPANY_ID";
		/// <summary>
		/// 受检企业ID
		/// </summary>
		public static string TESTED_COMPANY_ID_FIELD = "TESTED_COMPANY_ID";
		/// <summary>
		/// 要求完成日期
		/// </summary>
		public static string ASKING_DATE_FIELD = "ASKING_DATE";
		/// <summary>
		/// 报告领取方式
		/// </summary>
		public static string RPT_WAY_FIELD = "RPT_WAY";
		/// <summary>
		/// 是否同意分包
		/// </summary>
		public static string AGREE_OUTSOURCING_FIELD = "AGREE_OUTSOURCING";
		/// <summary>
		/// 是否同意使用的监测方法
		/// </summary>
		public static string AGREE_METHOD_FIELD = "AGREE_METHOD";
		/// <summary>
		/// 是否同意使用非标准方法
		/// </summary>
		public static string AGREE_NONSTANDARD_FIELD = "AGREE_NONSTANDARD";
		/// <summary>
		/// 是否同意其他
		/// </summary>
		public static string AGREE_OTHER_FIELD = "AGREE_OTHER";
		/// <summary>
		/// 样品来源,1,抽样，2，自送样
		/// </summary>
		public static string SAMPLE_SOURCE_FIELD = "SAMPLE_SOURCE";
		/// <summary>
		/// 送样人
		/// </summary>
		public static string SAMPLE_SEND_MAN_FIELD = "SAMPLE_SEND_MAN";
		/// <summary>
		/// 接样人ID
		/// </summary>
		public static string SAMPLE_ACCEPTER_ID_FIELD = "SAMPLE_ACCEPTER_ID";
        /// <summary>
        /// 送样频次
        /// </summary>
        public static string SAMPLE_FREQ_FIELD = "SAMPLE_FREQ";
		/// <summary>
		/// 项目负责人ID
		/// </summary>
		public static string PROJECT_ID_FIELD = "PROJECT_ID";
		/// <summary>
		/// 状态
		/// </summary>
		public static string STATE_FIELD = "STATE";
		/// <summary>
		/// 委托书状态
		/// </summary>
		public static string CONTRACT_STATUS_FIELD = "CONTRACT_STATUS";
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
        /// CCFLOW_ID1
        /// </summary>
        public static string CCFLOW_ID1_FIELD = "CCFLOW_ID1";
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public static string CCFLOW_ID2_FIELD = "CCFLOW_ID2";
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public static string CCFLOW_ID3_FIELD = "CCFLOW_ID3";
		
		#endregion
		
		public TMisContractVo()
		{
			this.ID = "";
			this.CONTRACT_CODE = "";
			this.CONTRACT_YEAR = "";
			this.PROJECT_NAME = "";
			this.CONTRACT_TYPE = "";
            this.BOOKTYPE = "";
			this.TEST_TYPES = "";
			this.TEST_TYPE = "";
			this.TEST_PURPOSE = "";
			this.CLIENT_COMPANY_ID = "";
			this.TESTED_COMPANY_ID = "";
			this.ASKING_DATE = "";
			this.RPT_WAY = "";
			this.AGREE_OUTSOURCING = "";
			this.AGREE_METHOD = "";
			this.AGREE_NONSTANDARD = "";
			this.AGREE_OTHER = "";
			this.SAMPLE_SOURCE = "";
			this.SAMPLE_SEND_MAN = "";
			this.SAMPLE_ACCEPTER_ID = "";
            this.SAMPLE_FREQ = "";
			this.PROJECT_ID = "";
			this.STATE = "";
			this.CONTRACT_STATUS = "";
            this.ISQUICKLY = "";
            this.PROVIDE_DATA = "";
            this.OTHER_ASKING = "";
            this.MONITOR_ACCORDING = "";
            this.QC_STEP = "";
            this.QCRULE = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";
			
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
		/// 合同编号
		/// </summary>
		public string CONTRACT_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托年度
		/// </summary>
		public string CONTRACT_YEAR
		{
			set ;
			get ;
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string PROJECT_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托类型
		/// </summary>
		public string CONTRACT_TYPE
		{
			set ;
			get ;
		}

        /// <summary>
        /// 委托书类别
        /// </summary>
        public string BOOKTYPE
        {
            set;
            get;
        }
		/// <summary>
		/// 监测类型,冗余字段，如：废水,废气,噪声
		/// </summary>
		public string TEST_TYPES
		{
			set ;
			get ;
		}
		/// <summary>
		/// 报告类型
		/// </summary>
		public string TEST_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 监测目的
		/// </summary>
		public string TEST_PURPOSE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托企业ID
		/// </summary>
		public string CLIENT_COMPANY_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 受检企业ID
		/// </summary>
		public string TESTED_COMPANY_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 要求完成日期
		/// </summary>
		public string ASKING_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 报告领取方式
		/// </summary>
		public string RPT_WAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否同意分包
		/// </summary>
		public string AGREE_OUTSOURCING
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否同意使用的监测方法
		/// </summary>
		public string AGREE_METHOD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否同意使用非标准方法
		/// </summary>
		public string AGREE_NONSTANDARD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否同意其他
		/// </summary>
		public string AGREE_OTHER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品来源,1,抽样，2，自送样
		/// </summary>
		public string SAMPLE_SOURCE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 送样人
		/// </summary>
		public string SAMPLE_SEND_MAN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 接样人ID
		/// </summary>
		public string SAMPLE_ACCEPTER_ID
		{
			set ;
			get ;
		}
        /// <summary>
        /// 送样频次
        /// </summary>
        public string SAMPLE_FREQ
        {
            set;
            get;
        }
		/// <summary>
		/// 项目负责人ID
		/// </summary>
		public string PROJECT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 状态
		/// </summary>
		public string STATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 委托书状态
		/// </summary>
		public string CONTRACT_STATUS
		{
			set ;
			get ;
		}
        /// <summary>
        /// 是否为快捷录入
        /// </summary>
        public string ISQUICKLY
        {
            set;
            get;
        }
        /// <summary>
        /// 提供资料
        /// </summary>
        public string PROVIDE_DATA
        {
            set;
            get;
        }
        /// <summary>
        /// 委托方其他要求
        /// </summary>
        public string OTHER_ASKING
        {
            set;
            get;
        }
        /// <summary>
        /// 监测依据
        /// </summary>
        public string MONITOR_ACCORDING
        {
            set;
            get;
        }
         /// <summary>
        /// 质控要求
        /// </summary>
        public string QC_STEP
        {
            set;
            get;
        }

        /// <summary>
        /// 质控样设置
        /// </summary>
        public string QCRULE
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
        /// <summary>
        /// CCFLOW_ID1
        /// </summary>
        public string CCFLOW_ID1
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID2
        /// </summary>
        public string CCFLOW_ID2
        {
            set;
            get;
        }
        /// <summary>
        /// CCFLOW_ID3
        /// </summary>
        public string CCFLOW_ID3
        {
            set;
            get;
        }
		
		#endregion
		
    }
}