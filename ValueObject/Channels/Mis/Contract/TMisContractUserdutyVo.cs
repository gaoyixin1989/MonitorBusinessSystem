using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：监测计划岗位信息
    /// 创建日期：2012-12-17
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractUserdutyVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_CONTRACT_USERDUTY_TABLE = "T_MIS_CONTRACT_USERDUTY";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测计划ID
        /// </summary>
        public static string PLAN_ID_FIELD = "PLAN_ID";
        /// <summary>
        /// 监测类别
        /// </summary>
        public static string MONITOR_TYPE_ID_FIELD = "MONITOR_TYPE_ID";
        /// <summary>
        /// 默认负责人ID
        /// </summary>
        public static string SAMPLING_MANAGER_ID_FIELD = "SAMPLING_MANAGER_ID";
        /// <summary>
        /// 默认协同人ID
        /// </summary>
        public static string SAMPLING_ID_FIELD = "SAMPLING_ID";

        #endregion

        public TMisContractUserdutyVo()
        {
            this.ID = "";
            this.CONTRACT_PLAN_ID = "";
            this.CONTRACT_ID = "";
            this.MONITOR_TYPE_ID = "";
            this.SAMPLING_MANAGER_ID = "";
            this.SAMPLING_ID = "";

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
        /// 监测计划ID
        /// </summary>
        public string CONTRACT_PLAN_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测委托书ID
        /// </summary>
        public string CONTRACT_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测类别
        /// </summary>
        public string MONITOR_TYPE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 默认负责人ID
        /// </summary>
        public string SAMPLING_MANAGER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 默认协同人ID
        /// </summary>
        public string SAMPLING_ID
        {
            set;
            get;
        }


        #endregion

    }
}