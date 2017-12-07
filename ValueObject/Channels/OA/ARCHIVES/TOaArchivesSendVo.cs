using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：档案文件分发回收
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesSendVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_ARCHIVES_SEND_TABLE = "T_OA_ARCHIVES_SEND";
        //静态字段引用
        /// <summary>
        /// 主键
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 文件ID
        /// </summary>
        public static string DOCUMENT_ID_FIELD = "DOCUMENT_ID";
        /// <summary>
        /// 分发状态，0为回收，1为分发
        /// </summary>
        public static string LENT_OUT_STATE_FIELD = "LENT_OUT_STATE";
        /// <summary>
        /// 接收人/回收人
        /// </summary>
        public static string BORROWER_FIELD = "BORROWER";
        /// <summary>
        /// 份数
        /// </summary>
        public static string HOLD_TIME_FIELD = "HOLD_TIME";
        /// <summary>
        /// 分发时间/回收时间
        /// </summary>
        public static string LOAN_TIME_FIELD = "LOAN_TIME";
        /// <summary>
        /// 借出备注/归还备注
        /// </summary>
        public static string REMARK_FIELD = "REMARK";
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

        public TOaArchivesSendVo()
        {
            this.ID = "";
            this.DOCUMENT_ID = "";
            this.LENT_OUT_STATE = "";
            this.BORROWER = "";
            this.HOLD_TIME = "";
            this.LOAN_TIME = "";
            this.REMARK = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

        }

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 文件ID
        /// </summary>
        public string DOCUMENT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 分发状态，0为回收，1为分发
        /// </summary>
        public string LENT_OUT_STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 接收人/回收人
        /// </summary>
        public string BORROWER
        {
            set;
            get;
        }
        /// <summary>
        /// 份数
        /// </summary>
        public string HOLD_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 分发时间/回收时间
        /// </summary>
        public string LOAN_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 借出备注/归还备注
        /// </summary>
        public string REMARK
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