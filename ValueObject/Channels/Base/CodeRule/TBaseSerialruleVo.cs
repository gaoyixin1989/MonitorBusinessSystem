using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.CodeRule
{
    /// <summary>
    /// 功能：编号规则
    /// 创建日期：2013-04-22
    /// 创建人：胡方扬
    /// </summary>
    public class TBaseSerialruleVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_BASE_SERIALRULE_TABLE = "T_BASE_SERIALRULE";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string SERIAL_NAME_FIELD = "SERIAL_NAME";
        /// <summary>
        /// 编码规则
        /// </summary>
        public static string SERIAL_RULE_FIELD = "SERIAL_RULE";
        /// <summary>
        /// 类型
        /// </summary>
        public static string SERIAL_TYPE_FIELD = "SERIAL_TYPE";
        /// <summary>
        /// 编码位数
        /// </summary>
        public static string SERIAL_NUMBER_BIT_FIELD = "SERIAL_NUMBER_BIT";
        /// <summary>
        /// 使用该编码的监测类别
        /// </summary>
        public static string SERIAL_TYPE_ID_FIELD = "SERIAL_TYPE_ID";
        /// <summary>
        /// 样品来源,1为抽样，2为自送样
        /// </summary>
        public static string SAMPLE_SOURCE_FIELD = "SAMPLE_SOURCE";

        #endregion

        public TBaseSerialruleVo()
        {
            this.ID = "";
            this.SERIAL_NAME = "";
            this.SERIAL_RULE = "";
            this.SERIAL_TYPE = "";
            this.SERIAL_NUMBER_BIT = "";
            this.SERIAL_TYPE_ID = "";
            this.SAMPLE_SOURCE = "";
            this.SERIAL_START_NUM = "";
            this.SERIAL_MAX_NUM = "";
            this.STATUS = "";
            this.SERIAL_YEAR = "";
            this.IS_UNION = "";
            this.UNION_SEARIAL_ID = "";
            this.UNION_DEFAULT = "";
            this.DAY_STATUS = "";
            this.SERIAL_DAY = "";
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
        public string SERIAL_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 编码规则
        /// </summary>
        public string SERIAL_RULE
        {
            set;
            get;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string SERIAL_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 编码位数
        /// </summary>
        public string SERIAL_NUMBER_BIT
        {
            set;
            get;
        }
        /// <summary>
        /// 使用该编码的监测类别
        /// </summary>
        public string SERIAL_TYPE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 样品来源,1为抽样，2为自送样
        /// </summary>
        public string SAMPLE_SOURCE
        {
            set;
            get;
        }

        /// <summary>
        /// 初始编号
        /// </summary>
        public string SERIAL_START_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 最大编号
        /// </summary>
        public string SERIAL_MAX_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 是否启用年度重新编号
        /// </summary>
        public string STATUS
        {
            set;
            get;
        }
        //当前年度
        public string SERIAL_YEAR
        {
            set;
            get;
        }

        //是否联合编号
        public string IS_UNION
        {
            set;
            get;
        }

        //样品联合编号的组合编号ID
        public string UNION_SEARIAL_ID
        {
            set;
            get;
        }
        //辅助规则缺省值
        public string UNION_DEFAULT
        {
            set;
            get;
        }
        //跨天是否从新编号
        public string DAY_STATUS
        {
            set;
            get;
        }

        //编号当天日期
        public string SERIAL_DAY
        {
            set;
            get;
        }
        #endregion

    }
}