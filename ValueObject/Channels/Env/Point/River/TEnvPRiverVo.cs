using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject.Channels.Env.Point.River
{
    /// <summary>
    /// 功能：河流
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_P_RIVER_TABLE = "T_ENV_P_RIVER";
        //静态字段引用
        /// <summary>
        /// 主键ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 月度
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 测站ID（字典项）
        /// </summary>
        public static string SATAIONS_ID_FIELD = "SATAIONS_ID";
        /// <summary>
        /// 断面代码
        /// </summary>
        public static string SECTION_CODE_FIELD = "SECTION_CODE";
        /// <summary>
        /// 断面名称
        /// </summary>
        public static string SECTION_NAME_FIELD = "SECTION_NAME";
        /// <summary>
        /// 所在地区ID（字典项）
        /// </summary>
        public static string AREA_ID_FIELD = "AREA_ID";
        /// <summary>
        /// 所属省份ID（字典项）
        /// </summary>
        public static string PROVINCE_ID_FIELD = "PROVINCE_ID";
        /// <summary>
        /// 控制级别ID（字典项）
        /// </summary>
        public static string CONTRAL_LEVEL_FIELD = "CONTRAL_LEVEL";
        /// <summary>
        /// 河流ID（字典项）
        /// </summary>
        public static string RIVER_ID_FIELD = "RIVER_ID";
        /// <summary>
        /// 流域ID（字典项）
        /// </summary>
        public static string VALLEY_ID_FIELD = "VALLEY_ID";
        /// <summary>
        /// 水质目标ID（字典项）
        /// </summary>
        public static string WATER_QUALITY_GOALS_ID_FIELD = "WATER_QUALITY_GOALS_ID";
        /// <summary>
        /// 每月监测、单月监测
        /// </summary>
        public static string MONITOR_TIMES_FIELD = "MONITOR_TIMES";
        /// <summary>
        /// 类别ID（字典项）
        /// </summary>
        public static string CATEGORY_ID_FIELD = "CATEGORY_ID";
        /// <summary>
        /// 是否交接（0-否，1-是）
        /// </summary>
        public static string IS_HANDOVER_FIELD = "IS_HANDOVER";
        /// <summary>
        /// 经度（度）
        /// </summary>
        public static string LONGITUDE_DEGREE_FIELD = "LONGITUDE_DEGREE";
        /// <summary>
        /// 经度（分）
        /// </summary>
        public static string LONGITUDE_MINUTE_FIELD = "LONGITUDE_MINUTE";
        /// <summary>
        /// 经度（秒）
        /// </summary>
        public static string LONGITUDE_SECOND_FIELD = "LONGITUDE_SECOND";
        /// <summary>
        /// 纬度（度）
        /// </summary>
        public static string LATITUDE_DEGREE_FIELD = "LATITUDE_DEGREE";
        /// <summary>
        /// 纬度（分）
        /// </summary>
        public static string LATITUDE_MINUTE_FIELD = "LATITUDE_MINUTE";
        /// <summary>
        /// 纬度（秒）
        /// </summary>
        public static string LATITUDE_SECOND_FIELD = "LATITUDE_SECOND";
        /// <summary>
        /// 条件项
        /// </summary>
        public static string CONDITION_ID_FIELD = "CONDITION_ID";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
        /// <summary>
        /// 断面性质
        /// </summary>
        public static string SECTION_PORPERTIES_ID_FIELD = "SECTION_PORPERTIES_ID";
        /// <summary>
        /// 序号
        /// </summary>
        public static string NUM_FIELD = "NUM";
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
        /// <summary>
        /// 所选的监测月度
        /// </summary>
        public static string SelectMonths_FIELD = "SelectMonths";

        #endregion

        public TEnvPRiverVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.SATAIONS_ID = "";
            this.SECTION_CODE = "";
            this.SECTION_NAME = "";
            this.AREA_ID = "";
            this.PROVINCE_ID = "";
            this.CONTRAL_LEVEL = "";
            this.RIVER_ID = "";
            this.VALLEY_ID = "";
            this.WATER_QUALITY_GOALS_ID = "";
            this.MONITOR_TIMES = "";
            this.CATEGORY_ID = "";
            this.IS_HANDOVER = "";
            this.LONGITUDE_DEGREE = "";
            this.LONGITUDE_MINUTE = "";
            this.LONGITUDE_SECOND = "";
            this.LATITUDE_DEGREE = "";
            this.LATITUDE_MINUTE = "";
            this.LATITUDE_SECOND = "";
            this.CONDITION_ID = "";
            this.IS_DEL = "";
            this.SECTION_PORPERTIES_ID = "";
            this.NUM = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.SelectMonths = "";
        }

        #region 属性
        /// <summary>
        /// 主键ID
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
        /// 测站ID（字典项）
        /// </summary>
        public string SATAIONS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 断面代码
        /// </summary>
        public string SECTION_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 断面名称
        /// </summary>
        public string SECTION_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 所在地区ID（字典项）
        /// </summary>
        public string AREA_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 所属省份ID（字典项）
        /// </summary>
        public string PROVINCE_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 控制级别ID（字典项）
        /// </summary>
        public string CONTRAL_LEVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 河流ID（字典项）
        /// </summary>
        public string RIVER_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 流域ID（字典项）
        /// </summary>
        public string VALLEY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 水质目标ID（字典项）
        /// </summary>
        public string WATER_QUALITY_GOALS_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 每月监测、单月监测
        /// </summary>
        public string MONITOR_TIMES
        {
            set;
            get;
        }
        /// <summary>
        /// 类别ID（字典项）
        /// </summary>
        public string CATEGORY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 是否交接（0-否，1-是）
        /// </summary>
        public string IS_HANDOVER
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
        /// 条件项
        /// </summary>
        public string CONDITION_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }
        /// <summary>
        /// 断面性质
        /// </summary>
        public string SECTION_PORPERTIES_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string NUM
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
        public string SelectMonths
        {
            set;
            get;
        }

        #endregion

    }

}
