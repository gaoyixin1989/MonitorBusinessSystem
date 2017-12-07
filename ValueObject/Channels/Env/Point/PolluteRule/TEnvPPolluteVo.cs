using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_P_POLLUTE_TABLE = "T_ENV_P_POLLUTE";
        //静态字段引用
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 
        /// </summary>
        public static string TYPE_ID_FIELD = "TYPE_ID";
        /// <summary>
        /// 
        /// </summary>
        public static string POINT_CODE_FIELD = "POINT_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string POINT_NAME_FIELD = "POINT_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string WATER_CODE_FIELD = "WATER_CODE";
        /// <summary>
        /// 
        /// </summary>
        public static string WATER_NAME_FIELD = "WATER_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string SEWERAGE_NAME_FIELD = "SEWERAGE_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string EQUIPMENT_NAME_FIELD = "EQUIPMENT_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_NAME_FIELD = "MO_NAME";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_CAPACITY_FIELD = "MO_CAPACITY";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_UOM_FIELD = "MO_UOM";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_DATE_FIELD = "MO_DATE";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_TYPE_FIELD = "FUEL_TYPE";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_QTY_FIELD = "FUEL_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_MODEL_FIELD = "FUEL_MODEL";
        /// <summary>
        /// 
        /// </summary>
        public static string FUEL_TECH_FIELD = "FUEL_TECH";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_FUEL_FIELD = "IS_FUEL";
        /// <summary>
        /// 
        /// </summary>
        public static string DISCHARGE_WAY_FIELD = "DISCHARGE_WAY";
        /// <summary>
        /// 
        /// </summary>
        public static string MO_HOUR_QTY_FIELD = "MO_HOUR_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string LOAD_MODE_FIELD = "LOAD_MODE";
        /// <summary>
        /// 
        /// </summary>
        public static string POINT_TEMP_FIELD = "POINT_TEMP";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_RUN_FIELD = "IS_RUN";
        /// <summary>
        /// 
        /// </summary>
        public static string MEASURED_FIELD = "MEASURED";
        /// <summary>
        /// 
        /// </summary>
        public static string WASTE_AIR_QTY_FIELD = "WASTE_AIR_QTY";
        /// <summary>
        /// 
        /// </summary>
        public static string LONGITUDE_DEGREE_FIELD = "LONGITUDE_DEGREE";
        /// <summary>
        /// 
        /// </summary>
        public static string LONGITUDE_MINUTE_FIELD = "LONGITUDE_MINUTE";
        /// <summary>
        /// 
        /// </summary>
        public static string LONGITUDE_SECOND_FIELD = "LONGITUDE_SECOND";
        /// <summary>
        /// 
        /// </summary>
        public static string LATITUDE_DEGREE_FIELD = "LATITUDE_DEGREE";
        /// <summary>
        /// 
        /// </summary>
        public static string LATITUDE_MINUTE_FIELD = "LATITUDE_MINUTE";
        /// <summary>
        /// 
        /// </summary>
        public static string LATITUDE_SECOND_FIELD = "LATITUDE_SECOND";
        /// <summary>
        /// 
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK4_FIELD = "REMARK4";
        /// <summary>
        /// 
        /// </summary>
        public static string REMARK5_FIELD = "REMARK5";
        /// <summary>
        /// 所选的监测月度
        /// </summary>
        public static string SelectMonths_FIELD = "SelectMonths";
        #endregion

        public TEnvPPolluteVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.TYPE_ID = "";
            this.POINT_CODE = "";
            this.POINT_NAME = "";
            this.WATER_CODE = "";
            this.WATER_NAME = "";
            this.SEWERAGE_NAME = "";
            this.EQUIPMENT_NAME = "";
            this.MO_NAME = "";
            this.MO_CAPACITY = "";
            this.MO_UOM = "";
            this.MO_DATE = "";
            this.FUEL_TYPE = "";
            this.FUEL_QTY = "";
            this.FUEL_MODEL = "";
            this.FUEL_TECH = "";
            this.IS_FUEL = "";
            this.DISCHARGE_WAY = "";
            this.MO_HOUR_QTY = "";
            this.LOAD_MODE = "";
            this.POINT_TEMP = "";
            this.IS_RUN = "";
            this.MEASURED = "";
            this.WASTE_AIR_QTY = "";
            this.LONGITUDE_DEGREE = "";
            this.LONGITUDE_MINUTE = "";
            this.LONGITUDE_SECOND = "";
            this.LATITUDE_DEGREE = "";
            this.LATITUDE_MINUTE = "";
            this.LATITUDE_SECOND = "";
            this.IS_DEL = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.SelectMonths = "";
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
        /// 年度
        /// </summary>
        public string YEAR
        {
            set;
            get;
        }
        /// <summary>
        /// 月度
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string TYPE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 测点代码
        /// </summary>
        public string POINT_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 测点名称
        /// </summary>
        public string POINT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 受纳水体代码
        /// </summary>
        public string WATER_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 受纳水体名称
        /// </summary>
        public string WATER_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 排污设备名称
        /// </summary>
        public string SEWERAGE_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 排放设备名称
        /// </summary>
        public string EQUIPMENT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 生产产品
        /// </summary>
        public string MO_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 生产能力
        /// </summary>
        public string MO_CAPACITY
        {
            set;
            get;
        }
        /// <summary>
        /// 生产能力单位
        /// </summary>
        public string MO_UOM
        {
            set;
            get;
        }
        /// <summary>
        /// 投产日期
        /// </summary>
        public string MO_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 燃料类型
        /// </summary>
        public string FUEL_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 燃料年消耗量
        /// </summary>
        public string FUEL_QTY
        {
            set;
            get;
        }
        /// <summary>
        /// 锅炉燃烧方式
        /// </summary>
        public string FUEL_MODEL
        {
            set;
            get;
        }
        /// <summary>
        /// 低炭燃烧技术
        /// </summary>
        public string FUEL_TECH
        {
            set;
            get;
        }
        /// <summary>
        /// 是否循环流化床锅炉
        /// </summary>
        public string IS_FUEL
        {
            set;
            get;
        }
        /// <summary>
        /// 排放规律
        /// </summary>
        public string DISCHARGE_WAY
        {
            set;
            get;
        }
        /// <summary>
        /// 日生产小时数
        /// </summary>
        public string MO_HOUR_QTY
        {
            set;
            get;
        }
        /// <summary>
        /// 工况负荷
        /// </summary>
        public string LOAD_MODE
        {
            set;
            get;
        }
        /// <summary>
        /// 测点温度
        /// </summary>
        public string POINT_TEMP
        {
            set;
            get;
        }
        /// <summary>
        /// 治理设施是否正常运行
        /// </summary>
        public string IS_RUN
        {
            set;
            get;
        }
        /// <summary>
        /// 处理设施前实测浓度 
        /// </summary>
        public string MEASURED
        {
            set;
            get;
        }
        /// <summary>
        /// 处理设施前实测废气排放量
        /// </summary>
        public string WASTE_AIR_QTY
        {
            set;
            get;
        }
        /// <summary>
        /// 经度（度）
        /// </summary>
        public string LONGITUDE_DEGREE
        {
            set;
            get;
        }
        /// <summary>
        /// 经度（分）
        /// </summary>
        public string LONGITUDE_MINUTE
        {
            set;
            get;
        }
        /// <summary>
        /// 经度（秒）
        /// </summary>
        public string LONGITUDE_SECOND
        {
            set;
            get;
        }
        /// <summary>
        /// 纬度（度）
        /// </summary>
        public string LATITUDE_DEGREE
        {
            set;
            get;
        }
        /// <summary>
        /// 纬度（分）
        /// </summary>
        public string LATITUDE_MINUTE
        {
            set;
            get;
        }
        /// <summary>
        /// 纬度（秒）
        /// </summary>
        public string LATITUDE_SECOND
        {
            set;
            get;
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IS_DEL
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
        /// </summry>
        public string REMARK5
        {
            set;
            get;
        }
        /// <summary>
        /// 月度监测
        /// </summary>
        public string SelectMonths
        {
            set;
            get;
        }


        #endregion


    }

}
