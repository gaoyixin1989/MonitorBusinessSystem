using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.AIR30
{
    /// <summary>
    /// 功能：双三十废气填报表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// modify : 刘静楠
    /// time:2013-6-25
    /// </summary>
    public class TEnvFillAir30Vo : i3.Core.ValueObject.ObjectBase
    {
        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_ENV_FILL_AIR30_TABLE = "T_ENV_FILL_AIR30";
        //静态字段引用
        /// <summary>
        /// 主键ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 监测点ID
        /// </summary>
        public static string POINT_ID_FIELD = "POINT_ID";
        /// <summary>
        /// 年度
        /// </summary>
        public static string YEAR_FIELD = "YEAR";
        /// <summary>
        /// 月
        /// </summary>
        public static string MONTH_FIELD = "MONTH";
        /// <summary>
        /// 日
        /// </summary>
        public static string DAY_FIELD = "DAY";
        /// <summary>
        /// 周
        /// </summary>
        public static string WEEK_FIELD = "WEEK";
        /// <summary>
        /// 测点号
        /// </summary>
        public static string POINT_NUM_FIELD = "POINT_NUM";
        /// <summary>
        /// 测点名称
        /// </summary>
        public static string POINT_NAME_FIELD = "POINT_NAME";
        /// <summary>
        /// 气温
        /// </summary>
        public static string TEMPERATRUE_FIELD = "TEMPERATRUE";
        /// <summary>
        /// 气压
        /// </summary>
        public static string PRESSURE_FIELD = "PRESSURE";
        /// <summary>
        /// 风速
        /// </summary>
        public static string WIND_SPEED_FIELD = "WIND_SPEED";
        /// <summary>
        /// 风向
        /// </summary>
        public static string WIND_DIRECTION_FIELD = "WIND_DIRECTION";
        /// <summary>
        /// API指数
        /// </summary>
        public static string API_CODE_FIELD = "API_CODE";
        /// <summary>
        /// 空气质量指数
        /// </summary>
        public static string AQI_CODE_FIELD = "AQI_CODE";
        /// <summary>
        /// 空气级别
        /// </summary>
        public static string AIR_LEVEL_FIELD = "AIR_LEVEL";
        /// <summary>
        /// 空气质量状况
        /// </summary>
        public static string AIR_STATE_FIELD = "AIR_STATE";
        /// <summary>
        /// 主要污染物
        /// </summary>
        public static string MAIN_AIR_FIELD = "MAIN_AIR";
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
        /// 状态(0或空：登记  1：待审核  2：待签发  9：已归档 )
        /// </summary>
        public static string STATUS_FIELD = "STATUS";

        #endregion

        public TEnvFillAir30Vo()
        {
            this.ID = "";
            this.POINT_ID = "";
            this.YEAR = "";
            this.MONTH = "";
            this.DAY = "";
            this.WEEK = "";
            this.POINT_NUM = "";
            this.POINT_NAME = "";
            this.TEMPERATRUE = "";
            this.PRESSURE = "";
            this.WIND_SPEED = "";
            this.WIND_DIRECTION = "";
            this.API_CODE = "";
            this.AQI_CODE = "";
            this.AIR_LEVEL = "";
            this.AIR_STATE = "";
            this.MAIN_AIR = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";
            this.STATUS = "";
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
        /// 监测点ID
        /// </summary>
        public string POINT_ID
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
        /// 月
        /// </summary>
        public string MONTH
        {
            set;
            get;
        }
        /// <summary>
        /// 日
        /// </summary>
        public string DAY
        {
            set;
            get;
        }
        /// <summary>
        /// 周
        /// </summary>
        public string WEEK
        {
            set;
            get;
        }
        /// <summary>
        /// 测点号
        /// </summary>
        public string POINT_NUM
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
        /// 气温
        /// </summary>
        public string TEMPERATRUE
        {
            set;
            get;
        }
        /// <summary>
        /// 气压
        /// </summary>
        public string PRESSURE
        {
            set;
            get;
        }
        /// <summary>
        /// 风速
        /// </summary>
        public string WIND_SPEED
        {
            set;
            get;
        }
        /// <summary>
        /// 风向
        /// </summary>
        public string WIND_DIRECTION
        {
            set;
            get;
        }
        /// <summary>
        /// API指数
        /// </summary>
        public string API_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 空气质量指数
        /// </summary>
        public string AQI_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 空气级别
        /// </summary>
        public string AIR_LEVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 空气质量状况
        /// </summary>
        public string AIR_STATE
        {
            set;
            get;
        }
        /// <summary>
        /// 主要污染物
        /// </summary>
        public string MAIN_AIR
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
        /// 状态(0或空：登记  1：待审核  2：待签发  9：已归档 )
        /// </summary>
        public string STATUS
        {
            set;
            get;
        }

        #endregion
		


    }
}