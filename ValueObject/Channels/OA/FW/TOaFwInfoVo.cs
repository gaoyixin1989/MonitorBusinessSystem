using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.FW
{
    /// <summary>
    /// 功能：发文信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaFwInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_FW_INFO_TABLE = "T_OA_FW_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 原文编号
		/// </summary>
		public static string YWNO_FIELD = "YWNO";
		/// <summary>
		/// 发文编号
		/// </summary>
		public static string FWNO_FIELD = "FWNO";
		/// <summary>
		/// 发文标题
		/// </summary>
		public static string FW_TITLE_FIELD = "FW_TITLE";
		/// <summary>
		/// 主办单位
		/// </summary>
		public static string ZB_DEPT_FIELD = "ZB_DEPT";
        /// <summary>
        /// 主送单位
        /// </summary>
        public static string ZS_DEPT_FIELD = "ZS_DEPT";
        /// <summary>
        /// 抄报单位
        /// </summary>
        public static string CB_DEPT_FIELD = "CB_DEPT";
        /// <summary>
        /// 抄送单位
        /// </summary>
        public static string CS_DEPT_FIELD = "CS_DEPT";
        /// <summary>
        /// 主题词
        /// </summary>
        public static string SUBJECT_WORD_FIELD = "SUBJECT_WORD";
		/// <summary>
		/// 密级
		/// </summary>
		public static string MJ_FIELD = "MJ";
		/// <summary>
		/// 发文日期
		/// </summary>
		public static string FW_DATE_FIELD = "FW_DATE";
        /// <summary>
        /// 办理限期-开始时间
        /// </summary>
        public static string START_DATE_FIELD = "START_DATE";
        /// <summary>
        /// 办理限期-结束时间
        /// </summary>
        public static string END_DATE_FIELD = "END_DATE";
		/// <summary>
		/// 拟稿人
		/// </summary>
		public static string DRAFT_ID_FIELD = "DRAFT_ID";
		/// <summary>
		/// 拟稿日期
		/// </summary>
		public static string DRAFT_DATE_FIELD = "DRAFT_DATE";
		/// <summary>
		/// 核稿人ID
		/// </summary>
		public static string APP_ID_FIELD = "APP_ID";
		/// <summary>
		/// 核稿日期
		/// </summary>
		public static string APP_DATE_FIELD = "APP_DATE";
		/// <summary>
		/// 核稿意见
		/// </summary>
		public static string APP_INFO_FIELD = "APP_INFO";
        /// <summary>
        /// 会签人ID
        /// </summary>
        public static string CTS_ID_FIELD = "CTS_ID";
        /// <summary>
        /// 会签日期
        /// </summary>
        public static string CTS_DATE_FIELD = "CTS_DATE";
        /// <summary>
        /// 会签意见
        /// </summary>
        public static string CTS_INFO_FIELD = "CTS_INFO";
		/// <summary>
		/// 签发人ID
		/// </summary>
		public static string ISSUE_ID_FIELD = "ISSUE_ID";
		/// <summary>
		/// 签发日期
		/// </summary>
		public static string ISSUE_DATE_FIELD = "ISSUE_DATE";
		/// <summary>
		/// 签发意见
		/// </summary>
		public static string ISSUE_INFO_FIELD = "ISSUE_INFO";
        /// <summary>
        /// 登记意见
        /// </summary>
        public static string REG_INFO_FIELD = "REG_INFO";
		/// <summary>
		/// 登记人ID
		/// </summary>
		public static string REG_ID_FIELD = "REG_ID";
		/// <summary>
		/// 登记日期
		/// </summary>
		public static string REG_DATE_FIELD = "REG_DATE";
		/// <summary>
		/// 校对人
		/// </summary>
		public static string CHECK_ID_FIELD = "CHECK_ID";
		/// <summary>
		/// 用印人ID
		/// </summary>
		public static string SEAL_ID_FIELD = "SEAL_ID";
		/// <summary>
		/// 缮印人ID
		/// </summary>
		public static string PRINT_ID_FIELD = "PRINT_ID";
		/// <summary>
		/// 归档人ID
		/// </summary>
		public static string PIGONHOLE_ID_FIELD = "PIGONHOLE_ID";
		/// <summary>
		/// 归档时间
		/// </summary>
		public static string PIGONHOLE_DATE_FIELD = "PIGONHOLE_DATE";
        /// <summary>
        /// 发文状态
        /// </summary>
        public static string FW_STATUS_FIELD = "FW_STATUS";
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
		
		public TOaFwInfoVo()
		{
			this.ID = "";
			this.YWNO = "";
			this.FWNO = "";
			this.FW_TITLE = "";
			this.ZB_DEPT = "";
            this.ZS_DEPT = "";
            this.CB_DEPT = "";
            this.CS_DEPT = "";
            this.SUBJECT_WORD = "";
			this.MJ = "";
			this.FW_DATE = "";
            this.START_DATE = "";
            this.END_DATE = "";
			this.DRAFT_ID = "";
			this.DRAFT_DATE = "";
			this.APP_ID = "";
			this.APP_DATE = "";
			this.APP_INFO = "";
            this.CTS_ID = "";
            this.CTS_DATE = "";
            this.CTS_INFO = "";
			this.ISSUE_ID = "";
			this.ISSUE_DATE = "";
			this.ISSUE_INFO = "";
            this.REG_INFO = "";
			this.REG_ID = "";
			this.REG_DATE = "";
			this.CHECK_ID = "";
			this.SEAL_ID = "";
			this.PRINT_ID = "";
			this.PIGONHOLE_ID = "";
			this.PIGONHOLE_DATE = "";
            this.FW_STATUS = "";
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
		/// 原文编号
		/// </summary>
		public string YWNO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发文编号
		/// </summary>
		public string FWNO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发文标题
		/// </summary>
		public string FW_TITLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 主办单位
		/// </summary>
		public string ZB_DEPT
		{
			set ;
			get ;
		}
        /// <summary>
        /// 主送单位
        /// </summary>
        public string ZS_DEPT
        {
            set;
            get;
        }
        /// <summary>
        /// 抄报单位
        /// </summary>
        public string CB_DEPT
        {
            set;
            get;
        }
        /// <summary>
        /// 抄送单位
        /// </summary>
        public string CS_DEPT
        {
            set;
            get;
        }
        /// <summary>
        /// 主题词
        /// </summary>
        public string SUBJECT_WORD
        {
            set;
            get;
        }
		/// <summary>
        /// 办理限期-开始时间
		/// </summary>
        public string START_DATE
		{
			set ;
			get ;
		}
        /// <summary>
        /// 办理限期-结束时间
        /// </summary>
        public string END_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 密级
        /// </summary>
        public string MJ
        {
            set;
            get;
        }
		/// <summary>
		/// 发文日期
		/// </summary>
		public string FW_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 拟稿人
		/// </summary>
		public string DRAFT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 拟稿日期
		/// </summary>
		public string DRAFT_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 核稿人ID
		/// </summary>
		public string APP_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 核稿日期
		/// </summary>
		public string APP_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 核稿意见
		/// </summary>
		public string APP_INFO
		{
			set ;
			get ;
		}
        /// <summary>
        /// 会签人ID
        /// </summary>
        public string CTS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 会签日期
        /// </summary>
        public string CTS_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 会签意见
        /// </summary>
        public string CTS_INFO
        {
            set;
            get;
        }
		/// <summary>
		/// 签发人ID
		/// </summary>
		public string ISSUE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 签发日期
		/// </summary>
		public string ISSUE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 签发意见
		/// </summary>
		public string ISSUE_INFO
		{
			set ;
			get ;
		}
        /// <summary>
        /// 登记意见
        /// </summary>
        public string REG_INFO
        {
            set;
            get;
        }
		/// <summary>
		/// 登记人ID
		/// </summary>
		public string REG_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 登记日期
		/// </summary>
		public string REG_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 校对人
		/// </summary>
		public string CHECK_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用印人ID
		/// </summary>
		public string SEAL_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 缮印人ID
		/// </summary>
		public string PRINT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 归档人ID
		/// </summary>
		public string PIGONHOLE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 归档时间
		/// </summary>
		public string PIGONHOLE_DATE
		{
			set ;
			get ;
		}
        /// <summary>
		/// 发文状态
		/// </summary>
        public string FW_STATUS
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