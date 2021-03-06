using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Point.Rain
{
    /// <summary>
    /// 功能：降水监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointRainVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_POINT_RAIN_TABLE = "T_ENV_P_RAIN";
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
        /// 月份
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 测站ID（字典项）
        /// </summary>
        public static string SATAIONS_ID_FIELD = "SATAIONS_ID";
        /// <summary>
        /// 测点编号
        /// </summary>
        public static string POINT_CODE_FIELD = "POINT_CODE";
        /// <summary>
        /// 测点名称
        /// </summary>
        public static string POINT_NAME_FIELD = "POINT_NAME";
        /// <summary>
        /// 行政区ID（字典项）
        /// </summary>
        public static string AREA_ID_FIELD = "AREA_ID";
        /// <summary>
        /// 控制级别ID（字典项）
        /// </summary>
        public static string CONTRAL_LEVEL_FIELD = "CONTRAL_LEVEL";
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
        /// 具体位置
        /// </summary>
        public static string LOCATION_FIELD = "LOCATION";
        /// <summary>
        /// 条件项
        /// </summary>
        public static string CONDITION_ID_FIELD = "CONDITION_ID";
        /// <summary>
        /// 是否删除
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
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

        public TEnvPointRainVo()
        {
            this.ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.SATAIONS_ID = "";
            this.POINT_CODE = "";
            this.POINT_NAME = "";
            this.AREA_ID = "";
            this.CONTRAL_LEVEL = "";
            this.LONGITUDE_DEGREE = "";
            this.LONGITUDE_MINUTE = "";
            this.LONGITUDE_SECOND = "";
            this.LATITUDE_DEGREE = "";
            this.LATITUDE_MINUTE = "";
            this.LATITUDE_SECOND = "";
            this.LOCATION = "";
            this.CONDITION_ID = "";
            this.IS_DEL = "";
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
        /// 月份
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
        /// 测点编号
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
        /// 行政区ID（字典项）
        /// </summary>
        public string AREA_ID
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
        /// 具体位置
        /// </summary>
        public string LOCATION
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
        /// 是否删除
        /// </summary>
        public string IS_DEL
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