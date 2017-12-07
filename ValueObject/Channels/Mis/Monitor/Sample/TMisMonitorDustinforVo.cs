using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：颗粒物原始记录表-基本信息表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustinforVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_DUSTINFOR_TABLE = "T_MIS_MONITOR_DUSTINFOR";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string SUBTASK_ID_FIELD = "SUBTASK_ID";
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public static string ITEM_ID_FIELD = "ITEM_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string PURPOSE_FIELD = "PURPOSE";
        /// <summary>
        /// 采样日期
        /// </summary>
        public static string SAMPLE_DATE_FIELD = "SAMPLE_DATE";
        /// <summary>
        /// 监测单位ID
        /// </summary>
        public static string TEST_COMPANY_ID_FIELD = "TEST_COMPANY_ID";
        /// <summary>
        /// 锅炉（炉窑）名称/蒸吨
        /// </summary>
        public static string BOILER_NAME_FIELD = "BOILER_NAME";
        /// <summary>
        /// 燃料种类
        /// </summary>
        public static string FUEL_TYPE_FIELD = "FUEL_TYPE";
        /// <summary>
        /// 烟囱高度M
        /// </summary>
        public static string HEIGHT_FIELD = "HEIGHT";
        /// <summary>
        /// 采样位置
        /// </summary>
        public static string POSITION_FIELD = "POSITION";
        /// <summary>
        /// 断面直径
        /// </summary>
        public static string SECTION_DIAMETER_FIELD = "SECTION_DIAMETER";
        /// <summary>
        /// 断面面积
        /// </summary>
        public static string SECTION_AREA_FIELD = "SECTION_AREA";
        /// <summary>
        /// 治理措施
        /// </summary>
        public static string GOVERM_METHOLD_FIELD = "GOVERM_METHOLD";
        /// <summary>
        /// 风机风量
        /// </summary>
        public static string MECHIE_WIND_MEASURE_FIELD = "MECHIE_WIND_MEASURE";
        /// <summary>
        /// 烟气含湿量
        /// </summary>
        public static string HUMIDITY_MEASURE_FIELD = "HUMIDITY_MEASURE";
        /// <summary>
        /// 折算系数
        /// </summary>
        public static string MODUL_NUM_FIELD = "MODUL_NUM";
        /// <summary>
        /// 仪器型号
        /// </summary>
        public static string MECHIE_MODEL_FIELD = "MECHIE_MODEL";
        /// <summary>
        /// 仪器编码
        /// </summary>
        public static string MECHIE_CODE_FIELD = "MECHIE_CODE";
        /// <summary>
        /// 采样嘴直径
        /// </summary>
        public static string SAMPLE_POSITION_DIAMETER_FIELD = "SAMPLE_POSITION_DIAMETER";
        /// <summary>
        /// 环境温度
        /// </summary>
        public static string ENV_TEMPERATURE_FIELD = "ENV_TEMPERATURE";
        /// <summary>
        /// 大气压力
        /// </summary>
        public static string AIR_PRESSURE_FIELD = "AIR_PRESSURE";

        #endregion

        public TMisMonitorDustinforVo()
        {
            this.ID = "";
            this.SUBTASK_ID = "";
            this.ITEM_ID = "";
            this.METHOLD_NAME = "";
            this.METHOLD_ID = "";
            this.PURPOSE = "";
            this.SAMPLE_DATE = "";
            this.BOILER_NAME = "";
            this.FUEL_TYPE = "";
            this.HEIGHT = "";
            this.POSITION = "";
            this.SECTION_DIAMETER = "";
            this.SECTION_AREA = "";
            this.GOVERM_METHOLD = "";
            this.MECHIE_WIND_MEASURE = "";
            this.HUMIDITY_MEASURE = "";
            this.MODUL_NUM = "";
            this.MECHIE_MODEL = "";
            this.MECHIE_CODE = "";
            this.SAMPLE_POSITION_DIAMETER = "";
            this.ENV_TEMPERATURE = "";
            this.AIR_PRESSURE = "";
            this.WINDDRICT = "";
            this.WEATHER = "";

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
        public string SUBTASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测项目ID
        /// </summary>
        public string ITEM_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 方法依据
        /// </summary>
        public string METHOLD_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 方法依据ID
        /// </summary>
        public string METHOLD_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 监测目的
        /// </summary>
        public string PURPOSE
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
        /// 锅炉（炉窑）名称/蒸吨
        /// </summary>
        public string BOILER_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 燃料种类
        /// </summary>
        public string FUEL_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟囱高度M
        /// </summary>
        public string HEIGHT
        {
            set;
            get;
        }
        /// <summary>
        /// 采样位置
        /// </summary>
        public string POSITION
        {
            set;
            get;
        }
        /// <summary>
        /// 断面直径
        /// </summary>
        public string SECTION_DIAMETER
        {
            set;
            get;
        }
        /// <summary>
        /// 断面面积
        /// </summary>
        public string SECTION_AREA
        {
            set;
            get;
        }
        /// <summary>
        /// 治理措施
        /// </summary>
        public string GOVERM_METHOLD
        {
            set;
            get;
        }
        /// <summary>
        /// 风机风量
        /// </summary>
        public string MECHIE_WIND_MEASURE
        {
            set;
            get;
        }
        /// <summary>
        /// 烟气含湿量
        /// </summary>
        public string HUMIDITY_MEASURE
        {
            set;
            get;
        }
        /// <summary>
        /// 折算系数
        /// </summary>
        public string MODUL_NUM
        {
            set;
            get;
        }
        /// <summary>
        /// 仪器型号
        /// </summary>
        public string MECHIE_MODEL
        {
            set;
            get;
        }
        /// <summary>
        /// 仪器编码
        /// </summary>
        public string MECHIE_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 采样嘴直径
        /// </summary>
        public string SAMPLE_POSITION_DIAMETER
        {
            set;
            get;
        }
        /// <summary>
        /// 环境温度
        /// </summary>
        public string ENV_TEMPERATURE
        {
            set;
            get;
        }
        /// <summary>
        /// 大气压力
        /// </summary>
        public string AIR_PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 风向
        /// </summary>
        public string WINDDRICT 
        { 
            get;
            set;
        }
        /// <summary>
        /// 天气情况
        /// </summary>
        public string WEATHER 
        { 
            get;
            set; 
        }

        #endregion

    }
}