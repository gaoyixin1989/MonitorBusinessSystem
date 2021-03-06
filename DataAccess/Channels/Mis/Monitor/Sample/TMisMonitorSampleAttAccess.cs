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
    /// 功能：采样原始数据附件表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleAttAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_ATT " + this.BuildWhereStatement(tMisMonitorSampleAtt);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleAttVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_ATT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleAttVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleAttVo Details(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_ATT " + this.BuildWhereStatement(tMisMonitorSampleAtt));
           return SqlHelper.ExecuteObject(new TMisMonitorSampleAttVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleAttVo> SelectByObject(TMisMonitorSampleAttVo tMisMonitorSampleAtt, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_ATT " + this.BuildWhereStatement(tMisMonitorSampleAtt));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleAtt, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleAttVo tMisMonitorSampleAtt, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_ATT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleAtt));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_ATT " + this.BuildWhereStatement(tMisMonitorSampleAtt);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleAttVo SelectByObject(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_ATT " + this.BuildWhereStatement(tMisMonitorSampleAtt);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleAttVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleAtt, TMisMonitorSampleAttVo.T_MIS_MONITOR_SAMPLE_ATT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleAtt, TMisMonitorSampleAttVo.T_MIS_MONITOR_SAMPLE_ATT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleAtt.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleAtt_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleAttVo tMisMonitorSampleAtt_UpdateSet, TMisMonitorSampleAttVo tMisMonitorSampleAtt_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleAtt_UpdateSet, TMisMonitorSampleAttVo.T_MIS_MONITOR_SAMPLE_ATT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleAtt_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_ATT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_ATT ";
	    strSQL += this.BuildWhereStatement(tMisMonitorSampleAtt);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleAtt"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleAtt)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleAtt.ID.ToString()));
				}	
				//监测子任务ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.SUBTASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSampleAtt.SUBTASK_ID.ToString()));
				}	
				//原始数据附件ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.ATTACH_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTACH_ID = '{0}'", tMisMonitorSampleAtt.ATTACH_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleAtt.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleAtt.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleAtt.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleAtt.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorSampleAtt.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleAtt.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
