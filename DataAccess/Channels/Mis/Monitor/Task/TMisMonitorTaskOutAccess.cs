using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务外包单位表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskOutAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK_OUT " + this.BuildWhereStatement(tMisMonitorTaskOut);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskOutVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_OUT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskOutVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskOutVo Details(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_OUT " + this.BuildWhereStatement(tMisMonitorTaskOut));
           return SqlHelper.ExecuteObject(new TMisMonitorTaskOutVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskOutVo> SelectByObject(TMisMonitorTaskOutVo tMisMonitorTaskOut, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_OUT " + this.BuildWhereStatement(tMisMonitorTaskOut));
            return SqlHelper.ExecuteObjectList(tMisMonitorTaskOut, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskOutVo tMisMonitorTaskOut, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_TASK_OUT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTaskOut));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskOut"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_OUT " + this.BuildWhereStatement(tMisMonitorTaskOut);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskOutVo SelectByObject(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_OUT " + this.BuildWhereStatement(tMisMonitorTaskOut);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskOutVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTaskOut, TMisMonitorTaskOutVo.T_MIS_MONITOR_TASK_OUT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskOut">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskOut, TMisMonitorTaskOutVo.T_MIS_MONITOR_TASK_OUT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTaskOut.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskOut_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorTaskOut_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskOutVo tMisMonitorTaskOut_UpdateSet, TMisMonitorTaskOutVo tMisMonitorTaskOut_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskOut_UpdateSet, TMisMonitorTaskOutVo.T_MIS_MONITOR_TASK_OUT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTaskOut_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK_OUT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK_OUT ";
	    strSQL += this.BuildWhereStatement(tMisMonitorTaskOut);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTaskOut"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTaskOut)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskOut.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTaskOut.ID.ToString()));
				}	
				//监测计划ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskOut.TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorTaskOut.TASK_ID.ToString()));
				}	
				//外包ID
				if (!String.IsNullOrEmpty(tMisMonitorTaskOut.OUTCOMPANY_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OUTCOMPANY_ID = '{0}'", tMisMonitorTaskOut.OUTCOMPANY_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tMisMonitorTaskOut.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tMisMonitorTaskOut.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
