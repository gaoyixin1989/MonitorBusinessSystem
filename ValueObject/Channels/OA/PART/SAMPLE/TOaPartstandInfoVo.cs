using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品信息
    /// 创建日期：2013-09-12
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_OA_PARTSTAND_INFO_TABLE = "T_OA_PARTSTAND_INFO";
        //静态字段引用
        /// <summary>
        /// 编号
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 物料编码
        /// </summary>
        public static string SAMPLE_CODE_FIELD = "SAMPLE_CODE";
        /// <summary>
        /// 物料类别
        /// </summary>
        public static string SAMPLE_TYPE_FIELD = "SAMPLE_TYPE";
        /// <summary>
        /// 物料名称
        /// </summary>
        public static string SAMPLE_NAME_FIELD = "SAMPLE_NAME";
        /// <summary>
        /// 单位
        /// </summary>
        public static string UNIT_FIELD = "UNIT";
        /// <summary>
        /// 规格型号
        /// </summary>
        public static string CLASS_TYPE_FIELD = "CLASS_TYPE";
        /// <summary>
        /// 库存量
        /// </summary>
        public static string INVENTORY_FIELD = "INVENTORY";
        /// <summary>
        /// 介质/基体
        /// </summary>
        public static string TOTAL_INVENTORY_FIELD = "TOTAL_INVENTORY";
        /// <summary>
        /// 
        /// </summary>
        public static string SAMPLE_SOURCE_FIELD = "SAMPLE_SOURCE";
        /// <summary>
        /// 
        /// </summary>
        public static string POTENCY_FIELD = "POTENCY";
        /// <summary>
        /// 分析纯/化学纯
        /// </summary>
        public static string BUY_DATE_FIELD = "BUY_DATE";
        /// <summary>
        /// 报警值
        /// </summary>
        public static string EFF_DATE_FIELD = "EFF_DATE";
        /// <summary>
        /// 用途
        /// </summary>
        public static string LEVEL_FIELD = "LEVEL";
        /// <summary>
        /// 技术要求
        /// </summary>
        public static string CARER_FIELD = "CARER";
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

        public TOaPartstandInfoVo()
        {
            this.ID = "";
            this.SAMPLE_CODE = "";
            this.SAMPLE_TYPE = "";
            this.SAMPLE_NAME = "";
            this.UNIT = "";
            this.CLASS_TYPE = "";
            this.INVENTORY = "";
            this.TOTAL_INVENTORY = "";
            this.SAMPLE_SOURCE = "";
            this.POTENCY = "";
            this.BUY_DATE = "";
            this.EFF_DATE = "";
            this.LEVEL = "";
            this.CARER = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

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
        /// 物料编码
        /// </summary>
        public string SAMPLE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 物料类别
        /// </summary>
        public string SAMPLE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string SAMPLE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string UNIT
        {
            set;
            get;
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string CLASS_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 库存量
        /// </summary>
        public string INVENTORY
        {
            set;
            get;
        }
        /// <summary>
        /// 介质/基体
        /// </summary>
        public string TOTAL_INVENTORY
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAMPLE_SOURCE
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// 分析纯/化学纯
        /// </summary>
        public string BUY_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 报警值
        /// </summary>
        public string EFF_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string LEVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 技术要求
        /// </summary>
        public string CARER
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
