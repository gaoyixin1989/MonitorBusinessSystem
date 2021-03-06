using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例附件明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstFileListVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_WF_INST_FILE_LIST_TABLE = "T_WF_INST_FILE_LIST";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public static string WF_INST_ID_FIELD = "WF_INST_ID";
        /// <summary>
        /// 流程编号
        /// </summary>
        public static string WF_ID_FIELD = "WF_ID";
        /// <summary>
        /// 流水号
        /// </summary>
        public static string WF_SERIAL_NO_FIELD = "WF_SERIAL_NO";
        /// <summary>
        /// 文件全路径
        /// </summary>
        public static string WF_FILE_FULLNAME_FIELD = "WF_FILE_FULLNAME";
        /// <summary>
        /// 文件名称
        /// </summary>
        public static string WF_FILE_NAME_FIELD = "WF_FILE_NAME";

        /// <summary>
        /// 上传用户
        /// </summary>
        public static string UPLOAD_USER_FIELD = "UPLOAD_USER";

        /// <summary>
        ///  上传时间
        /// </summary>
        public static string UPLOAD_TIME_FIELD = "UPLOAD_TIME";

        /// <summary>
        /// 文件图标
        /// </summary>
        public static string WF_FILE_ICO_FIELD = "WF_FILE_ICO";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";

        #endregion

        public TWfInstFileListVo()
        {
            this.ID = "";
            this.WF_INST_ID = "";
            this.WF_ID = "";
            this.WF_SERIAL_NO = "";
            this.WF_FILE_FULLNAME = "";
            this.WF_FILE_NAME = "";
            this.UPLOAD_USER = "";
            this.UPLOAD_TIME = "";
            this.WF_FILE_ICO = "";
            this.IS_DEL = "";

        }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string WF_INST_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string WF_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public string WF_SERIAL_NO
        {
            set;
            get;
        }
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string WF_FILE_FULLNAME
        {
            set;
            get;
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string WF_FILE_NAME
        {
            set;
            get;
        }

        /// <summary>
        /// 上传用户
        /// </summary>
        public string UPLOAD_USER
        {
            set;
            get;
        }


        /// <summary>
        /// 上传时间
        /// </summary>
        public string UPLOAD_TIME
        {
            set;
            get;
        }

        /// <summary>
        /// 文件图标
        /// </summary>
        public string WF_FILE_ICO
        {
            set;
            get;
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }


        #endregion

    }
}