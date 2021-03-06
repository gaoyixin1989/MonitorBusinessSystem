using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractTestfeeVo : i3.Core.ValueObject.ObjectBase
    {
		
        //#region 静态引用
        ////静态表格引用
        ///// <summary>
        ///// 对象对应的表格名称
        ///// </summary>
        //public static string T_MIS_CONTRACT_TESTFEE_TABLE = "T_MIS_CONTRACT_TESTFEE";
        ////静态字段引用
        ///// <summary>
        ///// ID
        ///// </summary>
        //public static string ID_FIELD = "ID";
        ///// <summary>
        ///// 委托书ID
        ///// </summary>
        //public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
        ///// <summary>
        ///// 频次次数，合同分几次执行
        ///// </summary>
        //public static string FREQ_FIELD = "FREQ";
        ///// <summary>
        ///// 监测项目ID
        ///// </summary>
        //public static string TEST_ITEM_ID_FIELD = "TEST_ITEM_ID";
        ///// <summary>
        ///// 样品数，实际就是点位数
        ///// </summary>
        //public static string TEST_NUM_FIELD = "TEST_NUM";
        ///// <summary>
        ///// 分析单价
        ///// </summary>
        //public static string TEST_PRICE_FIELD = "TEST_PRICE";
        ///// <summary>
        ///// 分析费用，分析单价×频次×样品数
        ///// </summary>
        //public static string TEST_FEE_FIELD = "TEST_FEE";
        ///// <summary>
        ///// 开机费用单价
        ///// </summary>
        //public static string TEST_POWER_PRICE_FIELD = "TEST_POWER_PRICE";
        ///// <summary>
        ///// 开机总费用，开机费用单价×频次
        ///// </summary>
        //public static string TEST_POWER_FEE_FIELD = "TEST_POWER_FEE";
        ///// <summary>
        ///// 小计，分析总费用+开机总费用
        ///// </summary>
        //public static string FEE_COUNT_FIELD = "FEE_COUNT";
        ///// <summary>
        ///// 备注1
        ///// </summary>
        //public static string REMARK1_FIELD = "REMARK1";
        ///// <summary>
        ///// 备注2
        ///// </summary>
        //public static string REMARK2_FIELD = "REMARK2";
        ///// <summary>
        ///// 备注3
        ///// </summary>
        //public static string REMARK3_FIELD = "REMARK3";
        ///// <summary>
        ///// 备注4
        ///// </summary>
        //public static string REMARK4_FIELD = "REMARK4";
        ///// <summary>
        ///// 备注5
        ///// </summary>
        //public static string REMARK5_FIELD = "REMARK5";
		
        //#endregion
        public static string T_MIS_CONTRACT_TESTFEE_TABLE = "T_MIS_MONITOR_TASK";
        public static string ID_FIELD = "ID";
        public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
        public static string PLAN_ID_FIELD = "PLAN_ID";
        public static string CONTRACT_CODE_FIELD = "CONTRACT_CODE";
        public static string TICKET_NUM_FIELD = "TICKET_NUM";
        public static string CONTRACT_YEAR_FIELD = "CONTRACT_YEAR";
        public static string PROJECT_NAME_FIELD = "PROJECT_NAME";
        public static string CONTRACT_TYPE_FIELD = "CONTRACT_TYPE";
        public static string TEST_TYPE_FIELD = "TEST_TYPE";
        public static string TEST_PURPOSE_FIELD = "TEST_PURPOSE";
        public static string CLIENT_COMPANY_ID_FIELD = "CLIENT_COMPANY_ID";
        public static string TESTED_COMPANY_ID_FIELD = "TESTED_COMPANY_ID";
        public static string CONSIGN_DATE_FIELD = "CONSIGN_DATE";
        public static string ASKING_DATE_FIELD = "ASKING_DATE";
        public static string FINISH_DATE_FIELD = "FINISH_DATE";
        public static string SAMPLE_SOURCE_FIELD = "SAMPLE_SOURCE";
        public static string CONTACT_ID_FIELD = "CONTACT_ID";
        public static string MANAGER_ID_FIELD = "MANAGER_ID";
        public static string CREATOR_ID_FIELD = "CREATOR_ID";
        public static string PROJECT_ID_FIELD = "PROJECT_ID";
        public static string CREATE_DATE_FIELD = "CREATE_DATE";
        public static string STATE_FIELD = "STATE";
        public static string TASK_STATUS_FIELD = "TASK_STATUS";
        public static string REMARK1_FIELD = "REMARK1";
        public static string REMARK2_FIELD = "REMARK2";
        public static string REMARK3_FIELD = "REMARK3";
        public static string REMARK4_FIELD = "REMARK4";
        public static string REMARK5_FIELD = "REMARK5";
        public static string QC_STATUS_FIELD = "QC_STATUS";
        public static string TASK_TYPE_FIELD = "TASK_TYPE";
        public static string COMFIRM_STATUS_FIELD = "COMFIRM_STATUS";
        public static string ALLQC_STATUS_FIELD = "ALLQC_STATUS";
        public static string SEND_STATUS_FIELD = "SEND_STATUS";
        public static string REPORT_HANDLE_FIELD = "REPORT_HANDLE";
        public static string SAMPLE_SEND_MAN_FIELD = "SAMPLE_SEND_MAN";
        public static string CCFLOW_ID1_FIELD = "CCFLOW_ID1";
        public static string CCFLOW_ID2_FIELD = "CCFLOW_ID2";
        public static string CCFLOW_ID3_FIELD = "CCFLOW_ID3";
        public static string NUM_FIELD = "NUM";
		
		public TMisContractTestfeeVo()
		{
            this.NUM = "";
            this.ID = "";
            this.CONTRACT_ID = "";
            this.PLAN_ID = "";
            this.CONTRACT_CODE = "";
            this.TICKET_NUM = "";
            this.CONTRACT_YEAR = "";
            this.PROJECT_NAME = "";
            this.CONTRACT_TYPE = "";
            this.TEST_TYPE = "";
            this.TEST_PURPOSE = "";
            this.CLIENT_COMPANY_ID = "";
            this.TESTED_COMPANY_ID = "";
            this.CONSIGN_DATE = "";
            this.ASKING_DATE = "";
            this.FINISH_DATE = "";
            this.SAMPLE_SOURCE = "";
            this.CONTACT_ID = "";
            this.MANAGER_ID = "";
            this.CREATOR_ID = "";
            this.PROJECT_ID = "";
            this.CREATE_DATE = "";
            this.STATE = "";
            this.TASK_STATUS = "";
            //this.ID = "";
            //this.CONTRACT_ID = "";
            //this.CONTRACT_POINTITEM_ID = "";
            //this.FREQ = "";
            //this.TEST_ITEM_ID = "";
            //this.TEST_POINT_NUM = "";
            //this.TEST_ANSY_FEE = "";
            //this.TEST_NUM = "";
            //this.TEST_PRICE = "";
            //this.TEST_FEE = "";
            //this.TEST_POWER_PRICE = "";
            //this.TEST_POWER_FEE = "";
            //this.FEE_COUNT = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
            this.QC_STATUS = "";
            this.TASK_TYPE = "";
            this.COMFIRM_STATUS = "";
            this.ALLQC_STATUS = "";
            this.SEND_STATUS = "";
            this.REPORT_HANDLE = "";
            this.SAMPLE_SEND_MAN = "";
            this.CCFLOW_ID1 = "";
            this.CCFLOW_ID2 = "";
            this.CCFLOW_ID3 = "";
		}
		
        //#region 属性
        //    /// <summary>
        ///// ID
        ///// </summary>
        //public string ID
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 委托书ID
        ///// </summary>
        //public string CONTRACT_ID
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 点位ID
        ///// </summary>
        //public string CONTRACT_POINTITEM_ID
        //{
        //    set;
        //    get;
        //}
        ///// <summary>
        ///// 频次次数，合同分几次执行
        ///// </summary>
        //public string FREQ
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 监测项目ID
        ///// </summary>
        //public string TEST_ITEM_ID
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 样品数，实际就是点位数
        ///// </summary>
        //public string TEST_NUM
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 测点数，清远
        ///// </summary>
        //public string TEST_POINT_NUM
        //{
        //    set;
        //    get;
        //}

        ///// <summary>
        ///// 测试分析费
        ///// </summary>
        //public string TEST_ANSY_FEE
        //{
        //    set;
        //    get;
        //}
        ///// <summary>
        ///// 分析单价
        ///// </summary>
        //public string TEST_PRICE
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 分析费用，分析单价×频次×样品数
        ///// </summary>
        //public string TEST_FEE
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 开机费用单价
        ///// </summary>
        //public string TEST_POWER_PRICE
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 开机总费用，开机费用单价×频次
        ///// </summary>
        //public string TEST_POWER_FEE
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 小计，分析总费用+开机总费用
        ///// </summary>
        //public string FEE_COUNT
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注1
        ///// </summary>
        //public string REMARK1
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注2
        ///// </summary>
        //public string REMARK2
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注3
        ///// </summary>
        //public string REMARK3
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注4
        ///// </summary>
        //public string REMARK4
        //{
        //    set ;
        //    get ;
        //}
        ///// <summary>
        ///// 备注5
        ///// </summary>
        //public string REMARK5
        //{
        //    set ;
        //    get ;
        //}
	
		
        //#endregion

        public string NUM
        {
            set;
            get;
        }
        public string ID
        {
            set;
            get;
        }
        public string CONTRACT_ID
        {
            set;
            get;
        }
        public string PLAN_ID
        {
            set;
            get;
        }
        public string CONTRACT_CODE
        {
            set;
            get;
        }
        public string TICKET_NUM
        {
            set;
            get;
        }
        public string CONTRACT_YEAR
        {
            set;
            get;
        }
        public string PROJECT_NAME
        {
            set;
            get;
        }
        public string CONTRACT_TYPE
        {
            set;
            get;
        }
        public string TEST_TYPE
        {
            set;
            get;
        }
        public string TEST_PURPOSE
        {
            set;
            get;
        }
        public string CLIENT_COMPANY_ID
        {
            set;
            get;
        }
        public string TESTED_COMPANY_ID
        {
            set;
            get;
        }
        public string CONSIGN_DATE
        {
            set;
            get;
        }
        public string ASKING_DATE
        {
            set;
            get;
        }
        public string FINISH_DATE
        {
            set;
            get;
        }
        public string SAMPLE_SOURCE
        {
            set;
            get;
        }
        public string CONTACT_ID
        {
            set;
            get;
        }
        public string MANAGER_ID
        {
            set;
            get;
        }
        public string CREATOR_ID
        {
            set;
            get;
        }
        public string PROJECT_ID
        {
            set;
            get;
        }
        public string CREATE_DATE
        {
            set;
            get;
        }
        public string STATE
        {
            set;
            get;
        }
        public string TASK_STATUS
        {
            set;
            get;
        }
        public string QC_STATUS
        {
            set;
            get;
        }
        public string TASK_TYPE
        {
            set;
            get;
        }
        public string COMFIRM_STATUS
        {
            set;
            get;
        }
        public string ALLQC_STATUS
        {
            set;
            get;
        }
        public string SEND_STATUS
        {
            set;
            get;
        }
        public string REPORT_HANDLE
        {
            set;
            get;
        }
        public string SAMPLE_SEND_MAN
        {
            set;
            get;
        }
        public string CCFLOW_ID1
        {
            set;
            get;
        }
        public string CCFLOW_ID2
        {
            set;
            get;
        }
        public string CCFLOW_ID3
        {
            set;
            get;
        }

        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }
        /// <summary>
        /// 备注4
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 备注5
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }
    }
}