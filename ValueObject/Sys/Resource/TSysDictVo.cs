using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Resource
{
    /// <summary>
    /// 功能：字典项管理
    /// 创建日期：2012-10-25
    /// 创建人：熊卫华
    /// </summary>
    public class TSysDictVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_SYS_DICT_TABLE = "T_SYS_DICT";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string DICT_TYPE_FIELD = "DICT_TYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string DICT_TEXT_FIELD = "DICT_TEXT";
        /// <summary>
        /// 
        /// </summary>
        public static string DICT_CODE_FIELD = "DICT_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string DICT_GROUP_FIELD = "DICT_GROUP";
        /// <summary>
        /// 
        /// </summary>
        public static string PARENT_TYPE_FIELD = "PARENT_TYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string PARENT_CODE_FIELD = "PARENT_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string RELATION_TYPE_FIELD = "RELATION_TYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string GROUP_CODE_FIELD = "GROUP_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string ORDER_ID_FIELD = "ORDER_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string AUTO_LOAD_FIELD = "AUTO_LOAD";
        /// <summary>
        /// 
        /// </summary>
        public static string EXTENDION_FIELD = "EXTENDION";
        /// <summary>
        /// 
        /// </summary>
        public static string EXTENDION_CODE_FIELD = "EXTENDION_CODE";
        /// <summary>
        /// 隐藏标记,对用户屏蔽
        /// </summary>
        public static string IS_HIDE_FIELD = "IS_HIDE";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK_FIELD = "REMARK";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK1_FIELD = "REMARK1";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK2_FIELD = "REMARK2";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK3_FIELD = "REMARK3";

        #endregion

        public TSysDictVo()
        {
            this.ID = "";
            this.DICT_TYPE = "";
            this.DICT_TEXT = "";
            this.DICT_CODE = "";
            this.DICT_GROUP = "";
            this.PARENT_TYPE = "";
            this.PARENT_CODE = "";
            this.RELATION_TYPE = "";
            this.GROUP_CODE = "";
            this.ORDER_ID = "";
            this.AUTO_LOAD = "";
            this.EXTENDION = "";
            this.EXTENDION_CODE = "";
            this.IS_HIDE = "";
            this.REMARK = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";

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
        /// 
        /// </summary>
        public string DICT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICT_TEXT
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICT_GROUP
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARENT_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARENT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RELATION_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORDER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AUTO_LOAD
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string EXTENDION
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string EXTENDION_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 隐藏标记,对用户屏蔽
        /// </summary>
        public string IS_HIDE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK1
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }


        #endregion

    }
}