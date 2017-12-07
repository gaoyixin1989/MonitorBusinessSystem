using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：档案文件修订
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesCheckVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_ARCHIVES_CHECK_TABLE = "T_OA_ARCHIVES_CHECK";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 档案文件ID
        /// </summary>
        public static string DOCUMENT_ID_FIELD = "DOCUMENT_ID";
        /// <summary>
        /// 修订类别（换页、改版）
        /// </summary>
        public static string UPDATE_TYPE_FIELD = "UPDATE_TYPE";
        /// <summary>
        /// 页号
        /// </summary>
        public static string PAGE_NUM_FIELD = "PAGE_NUM";
        /// <summary>
        /// 改版前名称
        /// </summary>
        public static string OLD_FILE_NAME_FIELD = "OLD_FILE_NAME";
        /// <summary>
        /// 原附件名
        /// </summary>
        public static string OLD_ATT_NAME_FIELD = "OLD_ATT_NAME";
        /// <summary>
        /// 原附件说明
        /// </summary>
        public static string OLD_ATT_INFO_FIELD = "OLD_ATT_INFO";
        /// <summary>
        /// 原附件路径
        /// </summary>
        public static string OLD_ATT_URL_FIELD = "OLD_ATT_URL";
        /// <summary>
        /// 修改人ID
        /// </summary>
        public static string UPDATE_ID_FIELD = "UPDATE_ID";
        /// <summary>
        /// 修改日期
        /// </summary>
        public static string UPDATE_DATE_FIELD = "UPDATE_DATE";
        /// <summary>
        /// 是否销毁
        /// </summary>
        public static string IS_DESTROY_FIELD = "IS_DESTROY";
        /// <summary>
        /// 申请内容
        /// </summary>
        public static string UPDATE_INFO_FIELD = "UPDATE_INFO";
        /// <summary>
        /// 备注
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
        /// <summary>
        /// 版本号
        /// </summary>
        public static string VERSION_FIELD = "VERSION";

        #endregion

        public TOaArchivesCheckVo()
        {
            this.ID = "";
            this.DOCUMENT_ID = "";
            this.UPDATE_TYPE = "";
            this.PAGE_NUM = "";
            this.OLD_FILE_NAME = "";
            this.OLD_ATT_NAME = "";
            this.OLD_ATT_INFO = "";
            this.OLD_ATT_URL = "";
            this.UPDATE_ID = "";
            this.UPDATE_DATE = "";
            this.IS_DESTROY = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.UPDATE_INFO = "";
            this.REMARK = "";
            this.VERSION = "";

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
        /// 档案文件ID
        /// </summary>
        public string DOCUMENT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 修订类别（换页、改版）
        /// </summary>
        public string UPDATE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 页号
        /// </summary>
        public string PAGE_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 改版前名称
        /// </summary>
        public string OLD_FILE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 原附件名
        /// </summary>
        public string OLD_ATT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 原附件说明
        /// </summary>
        public string OLD_ATT_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 原附件路径
        /// </summary>
        public string OLD_ATT_URL
        {
            set;
            get;
        }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string UPDATE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string UPDATE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 是否销毁
        /// </summary>
        public string IS_DESTROY
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
        /// <summary>
        /// 申请内容
        /// </summary>
        public string UPDATE_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            set;
            get;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VERSION
        {
            set;
            get;
        }
        #endregion

    }
}