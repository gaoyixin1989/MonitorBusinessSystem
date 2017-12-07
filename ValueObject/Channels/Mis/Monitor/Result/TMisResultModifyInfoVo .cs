using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：数据补录表
    /// 创建日期：2014-05-27
    /// 创建人：黄进军
    /// </summary>
    public class TMisResultModifyInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_RESULT_MODIFY_INFO_TABLE = "T_MIS_RESULT_MODIFY_INFO";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测结果ID
        /// </summary>
        public static string RESULT_ID_FIELD = "RESULT_ID";
        /// <summary>
        /// 修改人
        /// </summary>
        public static string MODIFY_USER_FIELD = "MODIFY_USER";
        /// <summary>
        /// 修改时间
        /// </summary>
        public static string MODIFY_TIME_FIELD = "MODIFY_TIME";
        /// <summary>
        /// 批准人
        /// </summary>
        public static string CHECK_USER_FIELD = "CHECK_USER";
        /// <summary>
        /// 修改原因
        /// </summary>
        public static string MODIFY_SUGGESTION_FIELD = "MODIFY_SUGGESTION";
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

        public TMisResultModifyInfoVo()
        {
            this.ID = "";
            this.RESULT_ID = "";
            this.MODIFY_USER = "";
            this.MODIFY_TIME = "";
            this.CHECK_USER = "";
            this.MODIFY_SUGGESTION = "";
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
        /// 监测结果ID
        /// </summary>
        public string RESULT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string MODIFY_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string MODIFY_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 批准人
        /// </summary>
        public string CHECK_USER
        {
            set;
            get;
        }
        /// <summary>
        /// 修改原因
        /// </summary>
        public string MODIFY_SUGGESTION
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
