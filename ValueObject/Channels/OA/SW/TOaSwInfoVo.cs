using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_SW_INFO_TABLE = "T_OA_SW_INFO";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 原文编号
        /// </summary>
        public static string FROM_CODE_FIELD = "FROM_CODE";
        /// <summary>
        /// 收文编号
        /// </summary>
        public static string SW_CODE_FIELD = "SW_CODE";
        /// <summary>
        /// 来文单位
        /// </summary>
        public static string SW_FROM_FIELD = "SW_FROM";
        /// <summary>
        /// 收文份数
        /// </summary>
        public static string SW_COUNT_FIELD = "SW_COUNT";
        /// <summary>
        /// 收文日期
        /// </summary>
        public static string SW_DATE_FIELD = "SW_DATE";
        /// <summary>
        /// 密级
        /// </summary>
        public static string SW_MJ_FIELD = "SW_MJ";
        /// <summary>
        /// 收文标题
        /// </summary>
        public static string SW_TITLE_FIELD = "SW_TITLE";
        /// <summary>
        /// 收文类别
        /// </summary>
        public static string SW_TYPE_FIELD = "SW_TYPE";
        /// <summary>
        /// 签收人ID
        /// </summary>
        public static string SW_SIGN_ID_FIELD = "SW_SIGN_ID";
        /// <summary>
        /// 签收日期
        /// </summary>
        public static string SW_SIGN_DATE_FIELD = "SW_SIGN_DATE";
        /// <summary>
        /// 登记人ID
        /// </summary>
        public static string SW_REG_ID_FIELD = "SW_REG_ID";
        /// <summary>
        /// 登记日期
        /// </summary>
        public static string SW_REG_DATE_FIELD = "SW_REG_DATE";
        /// <summary>
        /// 拟办人ID
        /// </summary>
        public static string SW_PLAN_ID_FIELD = "SW_PLAN_ID";
        /// <summary>
        /// 拟办日期
        /// </summary>
        public static string SW_PLAN_DATE_FIELD = "SW_PLAN_DATE";
        /// <summary>
        /// 拟办意见
        /// </summary>
        public static string SW_PLAN_INFO_FIELD = "SW_PLAN_INFO";
        /// <summary>
        /// 批办人ID
        /// </summary>
        public static string SW_PLAN_APP_ID_FIELD = "SW_PLAN_APP_ID";
        /// <summary>
        /// 批办日期
        /// </summary>
        public static string SW_PLAN_APP_DATE_FIELD = "SW_PLAN_APP_DATE";
        /// <summary>
        /// 批办意见
        /// </summary>
        public static string SW_PLAN_APP_INFO_FIELD = "SW_PLAN_APP_INFO";
        /// <summary>
        /// 主办人ID
        /// </summary>
        public static string SW_APP_ID_FIELD = "SW_APP_ID";
        /// <summary>
        /// 主办日期
        /// </summary>
        public static string SW_APP_DATE_FIELD = "SW_APP_DATE";
        /// <summary>
        /// 主办意见
        /// </summary>
        public static string SW_APP_INFO_FIELD = "SW_APP_INFO";
        /// <summary>
        /// 归档人ID
        /// </summary>
        public static string PIGONHOLE_ID_FIELD = "PIGONHOLE_ID";
        /// <summary>
        /// 归档时间
        /// </summary>
        public static string PIGONHOLE_DATE_FIELD = "PIGONHOLE_DATE";
        /// <summary>
        /// 办理状态(0:未提交 1：主任阅示 2：站长阅示 3：分管阅办 4：科室办结 5：待完结 6：已办结)
        /// </summary>
        public static string SW_STATUS_FIELD = "SW_STATUS";
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
        /// 主题词
        /// </summary>
        public static string SUBJECT_WORD_FIELD = "SUBJECT_WORD";

        #endregion

        public TOaSwInfoVo()
        {
            this.ID = "";
            this.FROM_CODE = "";
            this.SW_CODE = "";
            this.SW_FROM = "";
            this.SW_COUNT = "";
            this.SW_DATE = "";
            this.SW_MJ = "";
            this.SW_TITLE = "";
            this.SW_TYPE = "";
            this.SW_SIGN_ID = "";
            this.SW_SIGN_DATE = "";
            this.SW_REG_ID = "";
            this.SW_REG_DATE = "";
            this.SW_PLAN_ID = "";
            this.SW_PLAN_DATE = "";
            this.SW_PLAN_INFO = "";
            this.SW_PLAN_APP_ID = "";
            this.SW_PLAN_APP_DATE = "";
            this.SW_PLAN_APP_INFO = "";
            this.SW_APP_ID = "";
            this.SW_APP_DATE = "";
            this.SW_APP_INFO = "";
            this.PIGONHOLE_ID = "";
            this.PIGONHOLE_DATE = "";
            this.SW_STATUS = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.SUBJECT_WORD = "";

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
        /// 原文编号
        /// </summary>
        public string FROM_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 收文编号
        /// </summary>
        public string SW_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 来文单位
        /// </summary>
        public string SW_FROM
        {
            set;
            get;
        }
        /// <summary>
        /// 收文份数
        /// </summary>
        public string SW_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 收文日期
        /// </summary>
        public string SW_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 密级
        /// </summary>
        public string SW_MJ
        {
            set;
            get;
        }
        /// <summary>
        /// 收文标题
        /// </summary>
        public string SW_TITLE
        {
            set;
            get;
        }
        /// <summary>
        /// 收文类别
        /// </summary>
        public string SW_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 签收人ID
        /// </summary>
        public string SW_SIGN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 签收日期
        /// </summary>
        public string SW_SIGN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 登记人ID
        /// </summary>
        public string SW_REG_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 登记日期
        /// </summary>
        public string SW_REG_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 拟办人ID
        /// </summary>
        public string SW_PLAN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 拟办日期
        /// </summary>
        public string SW_PLAN_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 拟办意见
        /// </summary>
        public string SW_PLAN_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 批办人ID
        /// </summary>
        public string SW_PLAN_APP_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 批办日期
        /// </summary>
        public string SW_PLAN_APP_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 批办意见
        /// </summary>
        public string SW_PLAN_APP_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 主办人ID
        /// </summary>
        public string SW_APP_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 主办日期
        /// </summary>
        public string SW_APP_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 主办意见
        /// </summary>
        public string SW_APP_INFO
        {
            set;
            get;
        }
        /// <summary>
        /// 归档人ID
        /// </summary>
        public string PIGONHOLE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 归档时间
        /// </summary>
        public string PIGONHOLE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 办理状态
        /// </summary>
        public string SW_STATUS
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
        /// 主题词
        /// </summary>
        public string SUBJECT_WORD
        {
            set;
            get;
        }


        #endregion


    }
}