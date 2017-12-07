using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：采样样品子样
    /// 创建日期：2013-04-08
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorSubsampleInfoVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_SUBSAMPLE_INFO_TABLE = "T_MIS_MONITOR_SUBSAMPLE_INFO";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string SUBSAMPLE_NAME_FIELD = "SUBSAMPLE_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string SAMPLEID_FIELD = "SAMPLEID";
        /// <summary>
        /// 
        /// </summary>
        public static string ACTIONDATE_FIELD = "ACTIONDATE";

        #endregion

        public TMisMonitorSubsampleInfoVo()
        {
            this.ID = "";
            this.SUBSAMPLE_NAME = "";
            this.SAMPLEID = "";
            this.ACTIONDATE = "";

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
        public string SUBSAMPLE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAMPLEID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACTIONDATE
        {
            set;
            get;
        }


        #endregion

    }
}