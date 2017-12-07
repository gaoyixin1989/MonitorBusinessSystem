using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：档案文件查新
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesUpdateVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_ARCHIVES_UPDATE_TABLE = "T_OA_ARCHIVES_UPDATE";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 查新人ID
        /// </summary>
        public static string PERSON_ID_FIELD = "PERSON_ID";
        /// <summary>
        /// 查新日期
        /// </summary>
        public static string UPDATE_TIME_FIELD = "UPDATE_TIME";
        /// <summary>
        /// 方式（1、废止2、替换）
        /// </summary>
        public static string UPDATE_WAY_FIELD = "UPDATE_WAY";
        /// <summary>
        /// 查新前档案ID
        /// </summary>
        public static string BEFORE_NAME_FIELD = "BEFORE_NAME";
        /// <summary>
        /// 查新后档案ID
        /// </summary>
        public static string AFTER_NAME_FIELD = "AFTER_NAME";
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
        /// 删除标识
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";

        #endregion

        public TOaArchivesUpdateVo()
        {
            this.ID = "";
            this.PERSON_ID = "";
            this.UPDATE_TIME = "";
            this.UPDATE_WAY = "";
            this.BEFORE_NAME = "";
            this.AFTER_NAME = "";
            this.REMARK = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.IS_DEL = "";

        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 查新人ID
        /// </summary>
        public string PERSON_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 查新日期
        /// </summary>
        public string UPDATE_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 方式（1、废止2、替换）
        /// </summary>
        public string UPDATE_WAY
        {
            set;
            get;
        }
        /// <summary>
        /// 查新前档案ID
        /// </summary>
        public string BEFORE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 查新后档案ID
        /// </summary>
        public string AFTER_NAME
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
        /// 删除标识
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }


        #endregion

    }
}