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
    /// 功能：现场信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleLocaleAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_LOCALE " + this.BuildWhereStatement(tMisMonitorSampleLocale);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleLocaleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_LOCALE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleLocaleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleLocaleVo Details(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_LOCALE " + this.BuildWhereStatement(tMisMonitorSampleLocale));
           return SqlHelper.ExecuteObject(new TMisMonitorSampleLocaleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleLocaleVo> SelectByObject(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_LOCALE " + this.BuildWhereStatement(tMisMonitorSampleLocale));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleLocale, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_LOCALE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleLocale));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleLocale"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_LOCALE " + this.BuildWhereStatement(tMisMonitorSampleLocale);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleLocaleVo SelectByObject(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_LOCALE " + this.BuildWhereStatement(tMisMonitorSampleLocale);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleLocaleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleLocale, TMisMonitorSampleLocaleVo.T_MIS_MONITOR_SAMPLE_LOCALE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleLocale, TMisMonitorSampleLocaleVo.T_MIS_MONITOR_SAMPLE_LOCALE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleLocale.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleLocale_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleLocale_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale_UpdateSet, TMisMonitorSampleLocaleVo tMisMonitorSampleLocale_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleLocale_UpdateSet, TMisMonitorSampleLocaleVo.T_MIS_MONITOR_SAMPLE_LOCALE_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleLocale_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_LOCALE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_LOCALE ";
	    strSQL += this.BuildWhereStatement(tMisMonitorSampleLocale);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleLocale"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleLocale)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleLocale.ID.ToString()));
				}	
				//监测子任务ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.SUBTASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSampleLocale.SUBTASK_ID.ToString()));
				}	
				//企业工况
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.WORK_CONDITION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WORK_CONDITION = '{0}'", tMisMonitorSampleLocale.WORK_CONDITION.ToString()));
				}	
				//环保设施运行情况
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.ENVT_EQUT_STATUS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENVT_EQUT_STATUS = '{0}'", tMisMonitorSampleLocale.ENVT_EQUT_STATUS.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleLocale.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleLocale.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleLocale.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleLocale.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorSampleLocale.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleLocale.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
