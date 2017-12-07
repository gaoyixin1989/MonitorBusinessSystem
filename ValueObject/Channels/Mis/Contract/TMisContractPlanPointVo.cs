using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：监测任务预约点位表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanPointVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_CONTRACT_PLAN_POINT_TABLE = "T_MIS_CONTRACT_PLAN_POINT";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测任务预约表ID
        /// </summary>
        public static string PLAN_ID_FIELD = "PLAN_ID";
        /// <summary>
        /// 委托书监测点位ID
        /// </summary>
        public static string CONTRACT_POINT_ID_FIELD = "CONTRACT_POINT_ID";
        /// <summary>
        /// 委托书点位频次表ID
        /// </summary>
        public static string POINT_FREQ_ID_FIELD = "POINT_FREQ_ID";
        /// <summary>
        /// REAMRK1
        /// </summary>
        public static string REAMRK1_FIELD = "REAMRK1";
        /// <summary>
        /// REAMRK2
        /// </summary>
        public static string REAMRK2_FIELD = "REAMRK2";
        /// <summary>
        /// REAMRK3
        /// </summary>
        public static string REAMRK3_FIELD = "REAMRK3";
        /// <summary>
        /// REAMRK4
        /// </summary>
        public static string REAMRK4_FIELD = "REAMRK4";
        /// <summary>
        /// REAMRK5
        /// </summary>
        public static string REAMRK5_FIELD = "REAMRK5";

        #endregion

        public TMisContractPlanPointVo()
        {
            this.ID = "";
            this.PLAN_ID = "";
            this.CONTRACT_POINT_ID = "";
            this.POINT_FREQ_ID = "";
            this.REAMRK1 = "";
            this.REAMRK2 = "";
            this.REAMRK3 = "";
            this.REAMRK4 = "";
            this.REAMRK5 = "";

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
        /// 监测任务预约表ID
        /// </summary>
        public string PLAN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书监测点位ID
        /// </summary>
        public string CONTRACT_POINT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书点位频次表ID
        /// </summary>
        public string POINT_FREQ_ID
        {
            set;
            get;
        }
        /// <summary>
        /// REAMRK1
        /// </summary>
        public string REAMRK1
        {
            set;
            get;
        }
        /// <summary>
        /// REAMRK2
        /// </summary>
        public string REAMRK2
        {
            set;
            get;
        }
        /// <summary>
        /// REAMRK3
        /// </summary>
        public string REAMRK3
        {
            set;
            get;
        }
        /// <summary>
        /// REAMRK4
        /// </summary>
        public string REAMRK4
        {
            set;
            get;
        }
        /// <summary>
        /// REAMRK5
        /// </summary>
        public string REAMRK5
        {
            set;
            get;
        }


        #endregion

    }
}