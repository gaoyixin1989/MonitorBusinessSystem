using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.ProcessMgm;
using i3.ValueObject;


namespace i3.DataAccess.Channels.Mis.ProcessMgm
{
    /// <summary>
    /// 功能：
    /// 创建日期：2015-09-02
    /// 创建人：
    /// </summary>
    public class TMisMonitorProcessMgmAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_PROCESS_MGM " + this.BuildWhereStatement(tMisMonitorProcessMgm);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorProcessMgmVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_PROCESS_MGM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorProcessMgmVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorProcessMgmVo Details(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_PROCESS_MGM " + this.BuildWhereStatement(tMisMonitorProcessMgm));
            return SqlHelper.ExecuteObject(new TMisMonitorProcessMgmVo(), strSQL);
        }

        public TMisMonitorProcessMgmVo DetailsByCCFlowIDAndType(string ccflowID, string mgmType)
        {
            string strSQL = String.Format(@"select a.* from T_MIS_MONITOR_PROCESS_MGM a inner join T_MIS_CONTRACT_PLAN b
on a.TASK_ID=b.ID and b.CCFLOW_ID1='{0}' and a.MONITOR_TYPE='{1}'", ccflowID, mgmType);
            return SqlHelper.ExecuteObject(new TMisMonitorProcessMgmVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorProcessMgmVo> SelectByObject(TMisMonitorProcessMgmVo tMisMonitorProcessMgm, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_PROCESS_MGM " + this.BuildWhereStatement(tMisMonitorProcessMgm));
            return SqlHelper.ExecuteObjectList(tMisMonitorProcessMgm, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorProcessMgmVo tMisMonitorProcessMgm, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_PROCESS_MGM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorProcessMgm));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorProcessMgm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = "select * from T_MIS_MONITOR_PROCESS_MGM " + this.BuildWhereStatement(tMisMonitorProcessMgm) + " order by ID";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <returns></returns>
        public TMisMonitorProcessMgmVo SelectByObject(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = "select * from T_MIS_MONITOR_PROCESS_MGM " + this.BuildWhereStatement(tMisMonitorProcessMgm);
            return SqlHelper.ExecuteObject(new TMisMonitorProcessMgmVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorProcessMgm, TMisMonitorProcessMgmVo.T_MIS_MONITOR_PROCESS_MGM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorProcessMgm, TMisMonitorProcessMgmVo.T_MIS_MONITOR_PROCESS_MGM_TABLE);
            strSQL += string.Format(" where TASK_ID='{0}' and MONITOR_TYPE='{1}' ", tMisMonitorProcessMgm.TASK_ID, tMisMonitorProcessMgm.MONITOR_TYPE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_MIS_MONITOR_PROCESS_MGM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">条件，每个条件间要用and/or来连接
        /// <returns></returns>
        public void DeleteByWhere(string where)
        {
            string strSql = string.Format("delete from T_MIS_MONITOR_PROCESS_MGM where 1=1 {0}", where);
            SqlHelper.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 根据条件返回集合
        /// </summary>
        /// <param name="where">自己拼接的条件，每个条件要用and/or连接</param>
        /// <returns></returns>
        public List<TMisMonitorProcessMgmVo> SelectByObject(string where)
        {
            string strWhere = string.Format("select * from T_MIS_MONITOR_PROCESS_MGM where 1=1 {0}", where);
            return SqlHelper.ExecuteObjectList(new TMisMonitorProcessMgmVo(), strWhere);
        }

        /// <summary>
        /// 根据条件返回DataTable
        /// </summary>
        /// <param name="where">自己拼接的条件，每个条件要用and/or连接</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            string strWhere = string.Format("select * from T_MIS_MONITOR_PROCESS_MGM where 1=1 {0}", where);
            return SqlHelper.ExecuteDataTable(strWhere);
        }

        public DataTable SelectByTable_One(string where)
        {
            string strWhere = string.Format("select * from T_MIS_MONITOR_PROCESS_MGM where TASK_ID = '{0}' AND MONITOR_TYPE='SAMPLE_DATE' ", where);
            return SqlHelper.ExecuteDataTable(strWhere);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="model">条件实体</param>
        /// <returns></returns>
        public List<TMisMonitorProcessMgmVo> SelectByList(TMisMonitorProcessMgmVo model)
        {
            string strSql = string.Format("select * from T_MIS_MONITOR_PROCESS_MGM {0}", this.BuildWhereStatement(model));
            return SqlHelper.ExecuteObjectList(new TMisMonitorProcessMgmVo(), strSql);
        }
        #endregion

        public void UpdatePlanIDOfContractTask(string strContractID, string strPlanID)
        {
            string strSQL = string.Format("update T_MIS_MONITOR_PROCESS_MGM set TASK_ID = '{0}' where TASK_ID='{1}'", strPlanID, strContractID);

            SqlHelper.ExecuteNonQuery(strSQL);
        }

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorProcessMgm"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorProcessMgm)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorProcessMgm.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorProcessMgm.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorProcessMgm.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorProcessMgm.TASK_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorProcessMgm.MONITOR_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TYPE = '{0}'", tMisMonitorProcessMgm.MONITOR_TYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorProcessMgm.MONITOR_TIME_START.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TIME_START = '{0}'", tMisMonitorProcessMgm.MONITOR_TIME_START.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorProcessMgm.MONITOR_TIME_FINISH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TIME_FINISH = '{0}'", tMisMonitorProcessMgm.MONITOR_TIME_FINISH.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
