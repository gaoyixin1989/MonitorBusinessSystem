using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.RPT
{
    /// <summary>
    /// 功能：报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_RPT_FILE_TABLE = "T_RPT_FILE";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 委托书ID
        /// </summary>
        public static string CONTRACT_ID_FIELD = "CONTRACT_ID";
        /// <summary>
        /// 文件名
        /// </summary>
        public static string FILE_NAME_FIELD = "FILE_NAME";
        /// <summary>
        /// 文件类型
        /// </summary>
        public static string FILE_TYPE_FIELD = "FILE_TYPE";
        /// <summary>
        /// 文件大小
        /// </summary>
        public static string FILE_SIZE_FIELD = "FILE_SIZE";
        /// <summary>
        /// 文件内容
        /// </summary>
        public static string FILE_BODY_FIELD = "FILE_BODY";
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string FILE_PATH_FIELD = "FILE_PATH";
        /// <summary>
        /// 文件描述
        /// </summary>
        public static string FILE_DESC_FIELD = "FILE_DESC";
        /// <summary>
        /// 添加日期
        /// </summary>
        public static string ADD_TIME_FIELD = "ADD_TIME";
        /// <summary>
        /// 添加人
        /// </summary>
        public static string ADD_USER_FIELD = "ADD_USER";
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

        #endregion

        public TRptFileVo()
        {
            this.ID = "";
            this.CONTRACT_ID = "";
            this.FILE_NAME = "";
            this.FILE_TYPE = "";
            this.FILE_SIZE = "";
            this.FILE_BODY = null;
            this.FILE_PATH = "";
            this.FILE_DESC = "";
            this.ADD_TIME = "";
            this.ADD_USER = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";

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
        public string CONTRACT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FILE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FILE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FILE_SIZE
        {
            set;
            get;
        }
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] FILE_BODY
        {
            set;
            get;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FILE_PATH
        {
            set;
            get;
        }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string FILE_DESC
        {
            set;
            get;
        }
        /// <summary>
        /// 添加日期
        /// </summary>
        public string ADD_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 添加人
        /// </summary>
        public string ADD_USER
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


        #endregion

    }
}