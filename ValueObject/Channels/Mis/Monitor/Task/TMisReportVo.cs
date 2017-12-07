using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 委托任务统计表
    /// </summary>
    public class TMisReportVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_TASK_TABLE = "T_MIS_REPORT";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 月度
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 委托类别
        /// </summary>
        public static string TYPE_FIELD = "TYPE";
        /// <summary>
        /// 委托单号
        /// </summary>
        public static string TICKET_NUM_FIELD = "TICKET_NUM";
 
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
        /// 生成报告的处理人（清远）
        /// </summary>
        public static string REPORT_HANDLE_FIELD = "REPORT_HANDLE";

        #endregion

        public TMisReportVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.TYPE = "";
            this.TICKET_NUM = "";
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
            set;
            get;
        }
        /// <summary>
        /// 委托书ID
        /// </summary>
        public string YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 预约ID
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书编
        /// </summary>
        public string TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 委托年度
        /// </summary>
        public string TICKET_NUM
        {
            set;
            get;
        }
       
        /// <summary>
        /// 备注1
        /// </summary>
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



        #endregion
    }
}
