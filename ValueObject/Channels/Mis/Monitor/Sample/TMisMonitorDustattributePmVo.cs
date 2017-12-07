using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：PM10和悬浮颗粒物原始记录表
    /// 创建日期：2013-08-29
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributePmVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_DUSTATTRIBUTE_PM_TABLE = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string BASEINFOR_ID_FIELD = "BASEINFOR_ID";
        /// <summary>
        /// 采样序号
        /// </summary>
        public static string SAMPLE_CODE_FIELD = "SAMPLE_CODE";
        /// <summary>
        /// 滤筒编号
        /// </summary>
        public static string FITER_CODE_FIELD = "FITER_CODE";
        /// <summary>
        /// 采样开始日期
        /// </summary>
        public static string SAMPLE_BEGINDATE_FIELD = "SAMPLE_BEGINDATE";
        /// <summary>
        /// 采样结束日期
        /// </summary>
        public static string SAMPLE_ENDDATE_FIELD = "SAMPLE_ENDDATE";
        /// <summary>
        /// 采样累计时间
        /// </summary>
        public static string ACCTIME_FIELD = "ACCTIME";
        /// <summary>
        /// 采样体积
        /// </summary>
        public static string SAMPLE_L_STAND_FIELD = "SAMPLE_L_STAND";
        /// <summary>
        /// 标况采样体积
        /// </summary>
        public static string L_STAND_FIELD = "L_STAND";
        /// <summary>
        /// 标态流量
        /// </summary>
        public static string NM_SPEED_FIELD = "NM_SPEED";
        /// <summary>
        /// 样品初重
        /// </summary>
        public static string SAMPLE_FWEIGHT_FIELD = "SAMPLE_FWEIGHT";
        /// <summary>
        /// 样品终重
        /// </summary>
        public static string SAMPLE_EWEIGHT_FIELD = "SAMPLE_EWEIGHT";
        /// <summary>
        /// 样品重量
        /// </summary>
        public static string SAMPLE_WEIGHT_FIELD = "SAMPLE_WEIGHT";
        /// <summary>
        /// 样品浓度
        /// </summary>
        public static string SAMPLE_CONCENT_FIELD = "SAMPLE_CONCENT";
        /// <summary>
        /// 采样地点
        /// </summary>
        public static string SAMPLE_POINT_FIELD = "SAMPLE_POINT";
        /// <summary>
        /// 采样介质编号
        /// </summary>
        public static string SAMPLE_MEDCODE_FIELD = "SAMPLE_MEDCODE";
        /// <summary>
        /// 废气排放量
        /// </summary>
        public static string FQPFL_FIELD = "FQPFL";
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

        public TMisMonitorDustattributePmVo()
        {
            this.ID = "";
            this.BASEINFOR_ID = "";
            this.SAMPLE_CODE = "";
            this.FITER_CODE = "";
            this.SAMPLE_BEGINDATE = "";
            this.SAMPLE_ENDDATE = "";
            this.ACCTIME = "";
            this.SAMPLE_L_STAND = "";
            this.L_STAND = "";
            this.SAMPLE_FWEIGHT = "";
            this.SAMPLE_EWEIGHT = "";
            this.SAMPLE_WEIGHT = "";
            this.SAMPLE_CONCENT = "";
            this.NM_SPEED = "";
            this.SAMPLE_POINT = "";
            this.SAMPLE_MEDCODE = "";
            this.FQPFL = "";
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
        public string BASEINFOR_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 采样序号
        /// </summary>
        public string SAMPLE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 滤筒编号
        /// </summary>
        public string FITER_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样开始日期
        /// </summary>
        public string SAMPLE_BEGINDATE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样结束日期
        /// </summary>
        public string SAMPLE_ENDDATE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样累计时间
        /// </summary>
        public string ACCTIME
        {
            set;
            get;
        }
        /// <summary>
        /// 采样体积
        /// </summary>
        public string SAMPLE_L_STAND
        {
            set;
            get;
        }
        /// <summary>
        /// 标况采样体积
        /// </summary>
        public string L_STAND
        {
            set;
            get;
        }
        /// <summary>
        /// 标态流量
        /// </summary>
        public string NM_SPEED
        {
            set;
            get;
        }
        /// <summary>
        /// 样品初重
        /// </summary>
        public string SAMPLE_FWEIGHT
        {
            set;
            get;
        }
        /// <summary>
        /// 样品终重
        /// </summary>
        public string SAMPLE_EWEIGHT
        {
            set;
            get;
        }
        /// <summary>
        /// 样品重量
        /// </summary>
        public string SAMPLE_WEIGHT
        {
            set;
            get;
        }
        /// <summary>
        /// 样品浓度
        /// </summary>
        public string SAMPLE_CONCENT
        {
            set;
            get;
        }
        /// <summary>
        /// 采样地点
        /// </summary>
        public string SAMPLE_POINT
        {
            set;
            get;
        }
        /// <summary>
        /// 采样介质编号
        /// </summary>
        public string SAMPLE_MEDCODE
        {
            set;
            get;
        }
        /// <summary>
        /// 废气排放量
        /// </summary>
        public string FQPFL
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