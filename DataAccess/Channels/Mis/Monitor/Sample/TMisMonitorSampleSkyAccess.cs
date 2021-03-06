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
    /// 功能：天气情况表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleSkyAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_SKY " + this.BuildWhereStatement(tMisMonitorSampleSky);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleSkyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_SKY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleSkyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleSkyVo Details(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_SKY " + this.BuildWhereStatement(tMisMonitorSampleSky));
           return SqlHelper.ExecuteObject(new TMisMonitorSampleSkyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleSkyVo> SelectByObject(TMisMonitorSampleSkyVo tMisMonitorSampleSky, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_SKY " + this.BuildWhereStatement(tMisMonitorSampleSky));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleSky, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleSkyVo tMisMonitorSampleSky, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_SKY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleSky));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleSky"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_SKY " + this.BuildWhereStatement(tMisMonitorSampleSky);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleSkyVo SelectByObject(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_SKY " + this.BuildWhereStatement(tMisMonitorSampleSky);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleSkyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleSky, TMisMonitorSampleSkyVo.T_MIS_MONITOR_SAMPLE_SKY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleSky">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleSky, TMisMonitorSampleSkyVo.T_MIS_MONITOR_SAMPLE_SKY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleSky.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleSky_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleSky_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleSkyVo tMisMonitorSampleSky_UpdateSet, TMisMonitorSampleSkyVo tMisMonitorSampleSky_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleSky_UpdateSet, TMisMonitorSampleSkyVo.T_MIS_MONITOR_SAMPLE_SKY_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleSky_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_SKY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_SKY ";
	    strSQL += this.BuildWhereStatement(tMisMonitorSampleSky);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleSky"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleSky)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleSky.ID.ToString()));
				}	
				//监测子任务ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.SUBTASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSampleSky.SUBTASK_ID.ToString()));
				}	
				//天气项目
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.WEATHER_ITEM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WEATHER_ITEM = '{0}'", tMisMonitorSampleSky.WEATHER_ITEM.ToString()));
				}	
				//天气信息
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.WEATHER_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WEATHER_INFO = '{0}'", tMisMonitorSampleSky.WEATHER_INFO.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleSky.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleSky.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleSky.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleSky.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorSampleSky.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleSky.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
