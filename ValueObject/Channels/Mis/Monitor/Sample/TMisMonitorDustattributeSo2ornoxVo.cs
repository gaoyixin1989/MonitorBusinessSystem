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
    public class TMisMonitorDustattributeSo2ornoxVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX_TABLE = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
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
        /// 采样日期
        /// </summary>
        public static string SAMPLE_DATE_FIELD = "SAMPLE_DATE";
        /// <summary>
        /// 烟气动压
        /// </summary>
        public static string SMOKE_MOVE_PRESSURE_FIELD = "SMOKE_MOVE_PRESSURE";
        /// <summary>
        /// 烟气静压
        /// </summary>
        public static string SMOKE_STATIC_PRESSURE_FIELD = "SMOKE_STATIC_PRESSURE";
        /// <summary>
        /// 烟气全压
        /// </summary>
        public static string SMOKE_ALL_PRESSURE_FIELD = "SMOKE_ALL_PRESSURE";
        /// <summary>
        /// 烟气计压
        /// </summary>
        public static string SMOKE_K_PRESSURE_FIELD = "SMOKE_K_PRESSURE";
        /// <summary>
        /// 烟气温度
        /// </summary>
        public static string SMOKE_TEMPERATURE_FIELD = "SMOKE_TEMPERATURE";
        /// <summary>
        /// 烟气含氧量
        /// </summary>
        public static string SMOKE_OXYGEN_FIELD = "SMOKE_OXYGEN";
        /// <summary>
        /// 烟气流速
        /// </summary>
        public static string SMOKE_SPEED_FIELD = "SMOKE_SPEED";
        /// <summary>
        /// 标态流量
        /// </summary>
        public static string NM_SPEED_FIELD = "NM_SPEED";
        /// <summary>
        /// SO2浓度
        /// </summary>
        public static string SO2_POTENCY_FIELD = "SO2_POTENCY";
        /// <summary>
        /// SO2折算浓度
        /// </summary>
        public static string SO2_PER_POTENCY_FIELD = "SO2_PER_POTENCY";
        /// <summary>
        /// SO2排放量
        /// </summary>
        public static string SO2_DISCHARGE_FIELD = "SO2_DISCHARGE";
        /// <summary>
        /// NOX浓度
        /// </summary>
        public static string NOX_POTENCY_FIELD = "NOX_POTENCY";
        /// <summary>
        /// NOX折算浓度
        /// </summary>
        public static string NOX_PER_POTENCY_FIELD = "NOX_PER_POTENCY";
        /// <summary>
        /// NOX排放量
        /// </summary>
        public static string NOX_DISCHARGE_FIELD = "NOX_DISCHARGE";
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

        public TMisMonitorDustattributeSo2ornoxVo()
        {
            this.ID = "";
            this.BASEINFOR_ID = "";
            this.SAMPLE_CODE = "";
            this.FITER_CODE = "";
            this.SAMPLE_DATE = "";
            this.SMOKE_MOVE_PRESSURE = "";
            this.SMOKE_STATIC_PRESSURE = "";
            this.SMOKE_ALL_PRESSURE = "";
            this.SMOKE_K_PRESSURE = "";
            this.SMOKE_TEMPERATURE = "";
            this.SMOKE_OXYGEN = "";
            this.SMOKE_SPEED = "";
            this.NM_SPEED = "";
            this.SO2_POTENCY = "";
            this.SO2_PER_POTENCY = "";
            this.SO2_DISCHARGE = "";
            this.NOX_POTENCY = "";
            this.NOX_PER_POTENCY = "";
            this.NOX_DISCHARGE = "";
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
        /// 采样日期
        /// </summary>
        public string SAMPLE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气动压
        /// </summary>
        public string SMOKE_MOVE_PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气静压
        /// </summary>
        public string SMOKE_STATIC_PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气全压
        /// </summary>
        public string SMOKE_ALL_PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气计压
        /// </summary>
        public string SMOKE_K_PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气温度
        /// </summary>
        public string SMOKE_TEMPERATURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气含氧量
        /// </summary>
        public string SMOKE_OXYGEN
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气流速
        /// </summary>
        public string SMOKE_SPEED
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
        /// SO2浓度
        /// </summary>
        public string SO2_POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// SO2折算浓度
        /// </summary>
        public string SO2_PER_POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// SO2排放量
        /// </summary>
        public string SO2_DISCHARGE
        {
            set;
            get;
        }
        /// <summary>
        /// NOX浓度
        /// </summary>
        public string NOX_POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// NOX折算浓度
        /// </summary>
        public string NOX_PER_POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// NOX排放量
        /// </summary>
        public string NOX_DISCHARGE
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