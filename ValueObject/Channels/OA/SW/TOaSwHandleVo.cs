using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文阅办
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwHandleVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_SW_HANDLE_TABLE = "T_OA_SW_HANDLE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 收文ID
		/// </summary>
		public static string SW_ID_FIELD = "SW_ID";
		/// <summary>
		/// 拟办人ID
		/// </summary>
		public static string SW_PLAN_ID_FIELD = "SW_PLAN_ID";
		/// <summary>
		/// 拟办日期
		/// </summary>
		public static string SW_PLAN_DATE_FIELD = "SW_PLAN_DATE";
		/// <summary>
		/// 批办意见
		/// </summary>
		public static string SW_PLAN_APP_INFO_FIELD = "SW_PLAN_APP_INFO";
		/// <summary>
		/// 是否已办
		/// </summary>
		public static string IS_OK_FIELD = "IS_OK";
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
        /// 处理人标志 1：主任阅示人 2：站长阅示人 3：分管阅办人 4：科室办结人 5：(完结)归档人
        /// </summary>
        public static string SW_HANDER_FIELD = "SW_HANDER";
        /// <summary>
        /// 发送人ID
        /// </summary>
        public static string STR_USERID_FIELD = "STR_USERID";
        /// <summary>
        /// 发送时间
        /// </summary>
        public static string STR_DATE_FIELD = "STR_DATE";
		
		#endregion
		
		public TOaSwHandleVo()
		{
			this.ID = "";
			this.SW_ID = "";
			this.SW_PLAN_ID = "";
			this.SW_PLAN_DATE = "";
			this.SW_PLAN_APP_INFO = "";
			this.IS_OK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
            this.SW_HANDER = "";
            this.STR_USERID = "";
            this.STR_DATE = "";
			
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
		/// 收文ID
		/// </summary>
		public string SW_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 拟办人ID
		/// </summary>
		public string SW_PLAN_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 拟办日期
		/// </summary>
		public string SW_PLAN_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 批办意见
		/// </summary>
		public string SW_PLAN_APP_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否已办
		/// </summary>
		public string IS_OK
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
        /// 处理人标志0：处理人 1：阅办人 2：办结人
        /// </summary>
        public string SW_HANDER
        {
            set;
            get;
        }
        /// <summary>
        /// 发送人ID
        /// </summary>
        public string STR_USERID
        {
            set;
            get;
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string STR_DATE
        {
            set;
            get;
        }
		
		#endregion
		
    }
}