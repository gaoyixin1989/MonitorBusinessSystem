using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：自送样预约表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSamplePlanVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_CONTRACT_SAMPLE_PLAN_TABLE = "T_MIS_CONTRACT_SAMPLE_PLAN";
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
        /// 监测频次
        /// </summary>
        public static string FREQ_FIELD = "FREQ";
        /// <summary>
        /// 执行序号
        /// </summary>
        public static string NUM_FIELD = "NUM";
        /// <summary>
        /// 是否已预约
        /// </summary>
        public static string IF_PLAN_FIELD = "IF_PLAN";
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

        public TMisContractSamplePlanVo()
        {
            this.ID = "";
            this.CONTRACT_ID = "";
            this.FREQ = "";
            this.NUM = "";
            this.IF_PLAN = "";
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
        /// 监测频次
        /// </summary>
        public string FREQ
        {
            set;
            get;
        }
        /// <summary>
        /// 执行序号
        /// </summary>
        public string NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 是否已预约
        /// </summary>
        public string IF_PLAN
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