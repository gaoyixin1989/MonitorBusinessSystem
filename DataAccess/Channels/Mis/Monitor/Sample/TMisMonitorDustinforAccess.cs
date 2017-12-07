using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：颗粒物原始记录表-基本信息表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustinforAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_DUSTINFOR " + this.BuildWhereStatement(tMisMonitorDustinfor);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustinforVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTINFOR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorDustinforVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustinforVo Details(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTINFOR " + this.BuildWhereStatement(tMisMonitorDustinfor));
            return SqlHelper.ExecuteObject(new TMisMonitorDustinforVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustinforVo> SelectByObject(TMisMonitorDustinforVo tMisMonitorDustinfor, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTINFOR " + this.BuildWhereStatement(tMisMonitorDustinfor));
            return SqlHelper.ExecuteObjectList(tMisMonitorDustinfor, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustinforVo tMisMonitorDustinfor, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_DUSTINFOR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorDustinfor));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustinfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTINFOR " + this.BuildWhereStatement(tMisMonitorDustinfor);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <returns></returns>
        public TMisMonitorDustinforVo SelectByObject(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTINFOR " + this.BuildWhereStatement(tMisMonitorDustinfor);
            return SqlHelper.ExecuteObject(new TMisMonitorDustinforVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorDustinfor, TMisMonitorDustinforVo.T_MIS_MONITOR_DUSTINFOR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustinfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustinfor, TMisMonitorDustinforVo.T_MIS_MONITOR_DUSTINFOR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorDustinfor.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑，空值都可以修改 黄进军20141106
        /// </summary>
        /// <param name="tMisMonitorDustinfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool ObjEditNull(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = "update T_MIS_MONITOR_DUSTINFOR set ";
            strSQL += "SUBTASK_ID='" + tMisMonitorDustinfor.SUBTASK_ID + "',";
            strSQL += "ITEM_ID='" + tMisMonitorDustinfor.ITEM_ID + "',";
            strSQL += "METHOLD_NAME='" + tMisMonitorDustinfor.METHOLD_NAME + "',";
            strSQL += "METHOLD_ID='" + tMisMonitorDustinfor.METHOLD_ID + "',";
            strSQL += "PURPOSE='" + tMisMonitorDustinfor.PURPOSE + "',";
            strSQL += "SAMPLE_DATE='" + tMisMonitorDustinfor.SAMPLE_DATE + "',";
            strSQL += "BOILER_NAME='" + tMisMonitorDustinfor.BOILER_NAME + "',";
            strSQL += "FUEL_TYPE='" + tMisMonitorDustinfor.FUEL_TYPE + "',";
            strSQL += "HEIGHT='" + tMisMonitorDustinfor.HEIGHT + "',";
            strSQL += "POSITION='" + tMisMonitorDustinfor.POSITION + "',";
            strSQL += "SECTION_DIAMETER='" + tMisMonitorDustinfor.SECTION_DIAMETER + "',";
            strSQL += "SECTION_AREA='" + tMisMonitorDustinfor.SECTION_AREA + "',";
            strSQL += "MODUL_NUM='" + tMisMonitorDustinfor.MODUL_NUM + "',";
            strSQL += "MECHIE_MODEL='" + tMisMonitorDustinfor.MECHIE_MODEL + "',";
            strSQL += "MECHIE_CODE='" + tMisMonitorDustinfor.MECHIE_CODE + "',";
            strSQL += "SAMPLE_POSITION_DIAMETER='" + tMisMonitorDustinfor.SAMPLE_POSITION_DIAMETER + "',";
            strSQL += "ENV_TEMPERATURE='" + tMisMonitorDustinfor.ENV_TEMPERATURE + "',";
            strSQL += "AIR_PRESSURE='" + tMisMonitorDustinfor.AIR_PRESSURE + "',";
            strSQL += "GOVERM_METHOLD='" + tMisMonitorDustinfor.GOVERM_METHOLD + "',";
            strSQL += "MECHIE_WIND_MEASURE='" + tMisMonitorDustinfor.MECHIE_WIND_MEASURE + "',";
            strSQL += "HUMIDITY_MEASURE='" + tMisMonitorDustinfor.HUMIDITY_MEASURE + "',";
            strSQL += "WEATHER='" + tMisMonitorDustinfor.WEATHER + "',";
            strSQL += "WINDDRICT='" + tMisMonitorDustinfor.WINDDRICT + "'";
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorDustinfor.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustinfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustinfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustinforVo tMisMonitorDustinfor_UpdateSet, TMisMonitorDustinforVo tMisMonitorDustinfor_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustinfor_UpdateSet, TMisMonitorDustinforVo.T_MIS_MONITOR_DUSTINFOR_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorDustinfor_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_DUSTINFOR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            string strSQL = "delete from T_MIS_MONITOR_DUSTINFOR ";
            strSQL += this.BuildWhereStatement(tMisMonitorDustinfor);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        public DataTable SelectTableByID(string strIDs)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTINFOR where SUBTASK_ID in(" + strIDs + ")";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorDustinfor"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorDustinfor)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorDustinfor.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.SUBTASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBTASK_ID in ({0})", tMisMonitorDustinfor.SUBTASK_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisMonitorDustinfor.ITEM_ID.ToString()));
                }
                //方法依据
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.METHOLD_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND METHOLD_NAME = '{0}'", tMisMonitorDustinfor.METHOLD_NAME.ToString()));
                }
                //方法依据ID
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.METHOLD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND METHOLD_ID = '{0}'", tMisMonitorDustinfor.METHOLD_ID.ToString()));
                }
                //监测目的
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.PURPOSE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PURPOSE = '{0}'", tMisMonitorDustinfor.PURPOSE.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.SAMPLE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_DATE = '{0}'", tMisMonitorDustinfor.SAMPLE_DATE.ToString()));
                }
                //锅炉（炉窑）名称/蒸吨
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.BOILER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BOILER_NAME = '{0}'", tMisMonitorDustinfor.BOILER_NAME.ToString()));
                }
                //燃料种类
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.FUEL_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUEL_TYPE = '{0}'", tMisMonitorDustinfor.FUEL_TYPE.ToString()));
                }
                //烟囱高度M
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.HEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HEIGHT = '{0}'", tMisMonitorDustinfor.HEIGHT.ToString()));
                }
                //采样位置
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.POSITION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tMisMonitorDustinfor.POSITION.ToString()));
                }
                //断面直径
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.SECTION_DIAMETER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_DIAMETER = '{0}'", tMisMonitorDustinfor.SECTION_DIAMETER.ToString()));
                }
                //断面面积
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.SECTION_AREA.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_AREA = '{0}'", tMisMonitorDustinfor.SECTION_AREA.ToString()));
                }
                //治理措施
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.GOVERM_METHOLD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND GOVERM_METHOLD = '{0}'", tMisMonitorDustinfor.GOVERM_METHOLD.ToString()));
                }
                //风机风量
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.MECHIE_WIND_MEASURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MECHIE_WIND_MEASURE = '{0}'", tMisMonitorDustinfor.MECHIE_WIND_MEASURE.ToString()));
                }
                //烟气含湿量
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.HUMIDITY_MEASURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HUMIDITY_MEASURE = '{0}'", tMisMonitorDustinfor.HUMIDITY_MEASURE.ToString()));
                }
                //折算系数
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.MODUL_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODUL_NUM = '{0}'", tMisMonitorDustinfor.MODUL_NUM.ToString()));
                }
                //仪器型号
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.MECHIE_MODEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MECHIE_MODEL = '{0}'", tMisMonitorDustinfor.MECHIE_MODEL.ToString()));
                }
                //仪器编码
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.MECHIE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MECHIE_CODE = '{0}'", tMisMonitorDustinfor.MECHIE_CODE.ToString()));
                }
                //采样嘴直径
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.SAMPLE_POSITION_DIAMETER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_POSITION_DIAMETER = '{0}'", tMisMonitorDustinfor.SAMPLE_POSITION_DIAMETER.ToString()));
                }
                //环境温度
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.ENV_TEMPERATURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_TEMPERATURE = '{0}'", tMisMonitorDustinfor.ENV_TEMPERATURE.ToString()));
                }
                //大气压力
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.AIR_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIR_PRESSURE = '{0}'", tMisMonitorDustinfor.AIR_PRESSURE.ToString()));
                }
                //风向
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.WINDDRICT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WINDDRICT = '{0}'", tMisMonitorDustinfor.WINDDRICT.ToString()));
                }
                //天气情况
                if (!String.IsNullOrEmpty(tMisMonitorDustinfor.WEATHER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEATHER = '{0}'", tMisMonitorDustinfor.WEATHER.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
