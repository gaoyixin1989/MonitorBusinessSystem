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
    /// 功能：样品交接明细表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandsAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_HANDS " + this.BuildWhereStatement(tMisMonitorSampleHands);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandsVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HANDS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleHandsVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandsVo Details(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HANDS " + this.BuildWhereStatement(tMisMonitorSampleHands));
           return SqlHelper.ExecuteObject(new TMisMonitorSampleHandsVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleHandsVo> SelectByObject(TMisMonitorSampleHandsVo tMisMonitorSampleHands, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HANDS " + this.BuildWhereStatement(tMisMonitorSampleHands));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleHands, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleHandsVo tMisMonitorSampleHands, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_HANDS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleHands));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleHands"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_HANDS " + this.BuildWhereStatement(tMisMonitorSampleHands);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleHandsVo SelectByObject(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_HANDS " + this.BuildWhereStatement(tMisMonitorSampleHands);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleHandsVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleHands, TMisMonitorSampleHandsVo.T_MIS_MONITOR_SAMPLE_HANDS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHands">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleHands, TMisMonitorSampleHandsVo.T_MIS_MONITOR_SAMPLE_HANDS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleHands.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHands_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleHands_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandsVo tMisMonitorSampleHands_UpdateSet, TMisMonitorSampleHandsVo tMisMonitorSampleHands_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleHands_UpdateSet, TMisMonitorSampleHandsVo.T_MIS_MONITOR_SAMPLE_HANDS_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleHands_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_HANDS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_HANDS ";
	    strSQL += this.BuildWhereStatement(tMisMonitorSampleHands);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleHands"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleHands)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleHands.ID.ToString()));
				}	
				//监测子任务ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.HANDOVER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND HANDOVER_ID = '{0}'", tMisMonitorSampleHands.HANDOVER_ID.ToString()));
				}	
				//样品ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.SAMPLE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SAMPLE_ID = '{0}'", tMisMonitorSampleHands.SAMPLE_ID.ToString()));
				}	
				//样品数量
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.SAMPLE_NUMBER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SAMPLE_NUMBER = '{0}'", tMisMonitorSampleHands.SAMPLE_NUMBER.ToString()));
				}	
				//是否移交
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.IS_HANDOVER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tMisMonitorSampleHands.IS_HANDOVER.ToString()));
				}	
				//样品是否齐全完整
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.IF_INTEGRITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IF_INTEGRITY = '{0}'", tMisMonitorSampleHands.IF_INTEGRITY.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleHands.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleHands.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleHands.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleHands.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorSampleHands.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleHands.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
