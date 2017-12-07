using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：颗粒物原始记录表-属性表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributeVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_DUSTATTRIBUTE_TABLE = "T_MIS_MONITOR_DUSTATTRIBUTE";
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
        /// 标况体积
        /// </summary>
        public static string L_STAND_FIELD = "L_STAND";
        /// <summary>
        /// 滤筒初重
        /// </summary>
        public static string FITER_BEGIN_WEIGHT_FIELD = "FITER_BEGIN_WEIGHT";
        /// <summary>
        /// 滤筒终重
        /// </summary>
        public static string FITER_AFTER_WEIGHT_FIELD = "FITER_AFTER_WEIGHT";
        /// <summary>
        /// 样品重量
        /// </summary>
        public static string SAMPLE_WEIGHT_FIELD = "SAMPLE_WEIGHT";
        /// <summary>
        /// 烟尘浓度
        /// </summary>
        public static string SMOKE_POTENCY_FIELD = "SMOKE_POTENCY";
        /// <summary>
        /// 烟尘折算浓度
        /// </summary>
        public static string SMOKE_POTENCY2_FIELD = "SMOKE_POTENCY2";
        /// <summary>
        /// 烟尘排放量
        /// </summary>
        public static string SMOKE_DISCHARGE_FIELD = "SMOKE_DISCHARGE";
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

        public TMisMonitorDustattributeVo()
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
            this.L_STAND = "";
            this.FITER_BEGIN_WEIGHT = "";
            this.FITER_AFTER_WEIGHT = "";
            this.SAMPLE_WEIGHT = "";
            this.SMOKE_POTENCY = "";
            this.SMOKE_POTENCY2 = "";
            this.SMOKE_DISCHARGE = "";
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
        /// 标况体积
        /// </summary>
        public string L_STAND
        {
            set;
            get;
        }
        /// <summary>
        /// 滤筒初重
        /// </summary>
        public string FITER_BEGIN_WEIGHT
        {
            set;
            get;
        }
        /// <summary>
        /// 滤筒终重
        /// </summary>
        public string FITER_AFTER_WEIGHT
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
        /// 烟尘浓度
        /// </summary>
        public string SMOKE_POTENCY
        {
            set;
            get;
        }
        /// <summary>
        /// 烟尘折算浓度
        /// </summary>
        public string SMOKE_POTENCY2
        {
            set;
            get;
        }
        /// <summary>
        /// 烟尘排放量
        /// </summary>
        public string SMOKE_DISCHARGE
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