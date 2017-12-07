using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书样品
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSampleVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_CONTRACT_SAMPLE_TABLE = "T_MIS_CONTRACT_SAMPLE";
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
        /// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
        /// </summary>
        public static string MONITOR_ID_FIELD = "MONITOR_ID";
        /// <summary>
        /// 样品类型
        /// </summary>
        public static string SAMPLE_TYPE_FIELD = "SAMPLE_TYPE";
        /// <summary>
        /// 样品名称
        /// </summary>
        public static string SAMPLE_NAME_FIELD = "SAMPLE_NAME";
        /// <summary>
        /// 样品数量
        /// </summary>
        public static string SAMPLE_COUNT_FIELD = "SAMPLE_COUNT";
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

        public TMisContractSampleVo()
        {
            this.ID = "";
            this.CONTRACT_ID = "";
            this.MONITOR_ID = "";
            this.SAMPLE_TYPE = "";
            this.SAMPLE_NAME = "";
            this.SAMPLE_COUNT = "";
            this.SAMPLE_ACCEPT_DATEORACC = "";
            this.SRC_CODEORNAME = "";
            this.SAMPLE_STATUS = "";
            this.SAMPLE_PLAN_ID = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

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
        /// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
        /// </summary>
        public string MONITOR_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 样品类型
        /// </summary>
        public string SAMPLE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string SAMPLE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 样品数量
        /// </summary>
        public string SAMPLE_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 自送样样品接收时间/位置
        /// </summary>
        public string SAMPLE_ACCEPT_DATEORACC
        {
            set;
            get;
        }
        /// <summary>
        /// 自送样样品原编号/名称
        /// </summary>
        public string SRC_CODEORNAME
        {
            set;
            get;
        }
        /// <summary>
        /// 自送样样品原始状态
        /// </summary>
        public string SAMPLE_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 自送样监测计划ID
        /// </summary>
        public string SAMPLE_PLAN_ID
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