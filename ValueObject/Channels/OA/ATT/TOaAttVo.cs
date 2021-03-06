using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ATT
{
    /// <summary>
    /// 功能：附件信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttVo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_ATT_TABLE = "T_OA_ATT";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 业务ID
        /// </summary>
        public static string BUSINESS_ID_FIELD = "BUSINESS_ID";
        /// <summary>
        /// 业务类型
        /// </summary>
        public static string BUSINESS_TYPE_FIELD = "BUSINESS_TYPE";
        /// <summary>
        /// 附件名称
        /// </summary>
        public static string ATTACH_NAME_FIELD = "ATTACH_NAME";
        /// <summary>
        /// 附件类型
        /// </summary>
        public static string ATTACH_TYPE_FIELD = "ATTACH_TYPE";
        /// <summary>
        /// 附件路径
        /// </summary>
        public static string UPLOAD_PATH_FIELD = "UPLOAD_PATH";
        /// <summary>
        /// 上传日期
        /// </summary>
        public static string UPLOAD_DATE_FIELD = "UPLOAD_DATE";
        /// <summary>
        /// 上传人ID
        /// </summary>
        public static string UPLOAD_PERSON_FIELD = "UPLOAD_PERSON";
        /// <summary>
        /// 附件说明
        /// </summary>
        public static string DESCRIPTION_FIELD = "DESCRIPTION";
        /// <summary>
        /// 备注
        /// </summary>
        public static string REMARKS_FIELD = "REMARKS";

        /// <summary>
        /// 
        /// </summary>
        public static string FILL_ID_FIELD = "FILL_ID";
        #endregion
        public TOaAttVo()
        {
            this.ID = "";
            this.BUSINESS_ID = "";
            this.BUSINESS_TYPE = "";
            this.ATTACH_NAME = "";
            this.ATTACH_TYPE = "";
            this.UPLOAD_PATH = "";
            this.UPLOAD_DATE = "";
            this.UPLOAD_PERSON = "";
            this.DESCRIPTION = "";
            this.REMARKS = "";
            this.FILL_ID = "";
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
        /// 业务ID
        /// </summary>
        public string BUSINESS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BUSINESS_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string ATTACH_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string ATTACH_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string UPLOAD_PATH
        {
            set;
            get;
        }
        /// <summary>
        /// 上传日期
        /// </summary>
        public string UPLOAD_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 上传人ID
        /// </summary>
        public string UPLOAD_PERSON
        {
            set;
            get;
        }
        /// <summary>
        /// 附件说明
        /// </summary>
        public string DESCRIPTION
        {
            set;
            get;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARKS
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FILL_ID
        {
            set;
            get;
        }
	

        #endregion
    }
}