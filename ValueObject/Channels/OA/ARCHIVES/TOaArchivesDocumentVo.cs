using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：目录文件管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDocumentVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_ARCHIVES_DOCUMENT_TABLE = "T_OA_ARCHIVES_DOCUMENT";
        //静态字段引用
        /// <summary>
        /// 主键
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 目录ID
        /// </summary>
        public static string DIRECTORY_ID_FIELD = "DIRECTORY_ID";
        /// <summary>
        /// 文件名称
        /// </summary>
        public static string DOCUMENT_NAME_FIELD = "DOCUMENT_NAME";
        /// <summary>
        /// 文件类型
        /// </summary>
        public static string DOCUMENT_TYPE_FIELD = "DOCUMENT_TYPE";
        /// <summary>
        /// 主题词/关键字
        /// </summary>
        public static string P_KEY_FIELD = "P_KEY";
        /// <summary>
        /// 条形码
        /// </summary>
        public static string BAR_CODE_FIELD = "BAR_CODE";
        /// <summary>
        /// 文件大小
        /// </summary>
        public static string DOCUMENT_SIZE_FIELD = "DOCUMENT_SIZE";
        /// <summary>
        /// 上传日期
        /// </summary>
        public static string UPLOADING_DATE_FIELD = "UPLOADING_DATE";
        /// <summary>
        /// 存放位置
        /// </summary>
        public static string DOCUMENT_LOCATION_FIELD = "DOCUMENT_LOCATION";
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string DOCUMENT_PATH_FIELD = "DOCUMENT_PATH";
        /// <summary>
        /// 文件描述
        /// </summary>
        public static string DOCUMENT_DESCRIPTION_FIELD = "DOCUMENT_DESCRIPTION";
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

        //Add By SSZ BEGIN
        /// <summary>
        /// 档案编号
        /// </summary>
        public static string DOCUMENT_CODE_FIELD = "DOCUMENT_CODE";
        /// <summary>
        /// 版本号/修订数
        /// </summary>
        public static string VERSION_FIELD = "VERSION";
        /// <summary>
        /// 页码
        /// </summary>
        public static string PAGE_CODE_FIELD = "PAGE_CODE";
        /// <summary>
        /// 总页数
        /// </summary>
        public static string PAGE_SIZE_FIELD = "PAGE_SIZE";
        /// <summary>
        /// 颁布时间/修订时间
        /// </summary>
        public static string UPDATE_DATE_FIELD = "UPDATE_DATE";
        /// <summary>
        /// 结束标记/颁布机构
        /// </summary>
        public static string END_UNIT_FIELD = "END_UNIT";
        /// <summary>
        /// 保存类型
        /// </summary>
        public static string SAVE_TYPE_FIELD = "SAVE_TYPE";
        /// <summary>
        /// 保存年份
        /// </summary>
        public static string SAVE_YEAR_FIELD = "SAVE_YEAR";
        /// <summary>
        /// 是否废止
        /// </summary>
        public static string IS_OVER_FIELD = "IS_OVER";
        /// <summary>
        /// 是否销毁
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
        /// <summary>
        /// 操作人
        /// </summary>
        public static string OPERATOR_FIELD = "OPERATOR";
        /// <summary>
        /// 操作时间
        /// </summary>
        public static string OPERATE_TIME_FIELD = "OPERATE_TIME";
        //ADD END
        #endregion

        public TOaArchivesDocumentVo()
        {
            this.ID = "";
            this.DIRECTORY_ID = "";
            this.DOCUMENT_NAME = "";
            this.DOCUMENT_TYPE = "";
            this.BAR_CODE = "";
            this.DOCUMENT_SIZE = "";
            this.UPLOADING_DATE = "";
            this.DOCUMENT_LOCATION = "";
            this.DOCUMENT_PATH = "";
            this.DOCUMENT_DESCRIPTION = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

            this.P_KEY = "";
            this.DOCUMENT_CODE = "";
            this.VERSION = "";
            this.PAGE_CODE = "";
            this.PAGE_SIZE = "";
            this.UPDATE_DATE = "";
            this.END_UNIT = "";
            this.SAVE_TYPE = "";
            this.SAVE_YEAR = "";
            this.IS_OVER = "";
            this.IS_DEL = "";
            this.OPERATOR = "";
            this.OPERATE_TIME = "";
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
        /// 目录ID
        /// </summary>
        public string DIRECTORY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string DOCUMENT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string DOCUMENT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 条形码
        /// </summary>
        public string BAR_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string DOCUMENT_SIZE
        {
            set;
            get;
        }
        /// <summary>
        /// 主题词/关键字
        /// </summary>
        public string P_KEY
        {
            set;
            get;
        }
        /// <summary>
        /// 上传日期
        /// </summary>
        public string UPLOADING_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string DOCUMENT_LOCATION
        {
            set;
            get;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string DOCUMENT_PATH
        {
            set;
            get;
        }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string DOCUMENT_DESCRIPTION
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
        /// 档案编号
        /// </summary>
        public string DOCUMENT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 版本号/修订数
        /// </summary>
        public string VERSION
        {
            set;
            get;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public string PAGE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public string PAGE_SIZE
        {
            set;
            get;
        }
        /// <summary>
        /// 颁布时间/修订时间
        /// </summary>
        public string UPDATE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 结束标记/颁布机构
        /// </summary>
        public string END_UNIT
        {
            set;
            get;
        }
        /// <summary>
        /// 保存类型
        /// </summary>
        public string SAVE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 保存年份
        /// </summary>
        public string SAVE_YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 是否废止
        /// </summary>
        public string IS_OVER
        {
            set;
            get;
        }
        /// <summary>
        /// 是否销毁
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OPERATOR
        {
            set;
            get;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPERATE_TIME
        {
            set;
            get;
        }
        #endregion

    }
}