using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：结果数据可追溯性表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultLogAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_RESULT_LOG " + this.BuildWhereStatement(tMisMonitorResultLog);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultLogVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_LOG  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorResultLogVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultLogVo Details(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_LOG " + this.BuildWhereStatement(tMisMonitorResultLog));
            return SqlHelper.ExecuteObject(new TMisMonitorResultLogVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultLogVo> SelectByObject(TMisMonitorResultLogVo tMisMonitorResultLog, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_LOG " + this.BuildWhereStatement(tMisMonitorResultLog));
            return SqlHelper.ExecuteObjectList(tMisMonitorResultLog, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultLogVo tMisMonitorResultLog, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_RESULT_LOG {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorResultLog));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultLog"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_LOG " + this.BuildWhereStatement(tMisMonitorResultLog);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <returns></returns>
        public TMisMonitorResultLogVo SelectByObject(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_LOG " + this.BuildWhereStatement(tMisMonitorResultLog);
            return SqlHelper.ExecuteObject(new TMisMonitorResultLogVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorResultLog, TMisMonitorResultLogVo.T_MIS_MONITOR_RESULT_LOG_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultLog">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultLog, TMisMonitorResultLogVo.T_MIS_MONITOR_RESULT_LOG_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorResultLog.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultLog_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultLog_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultLogVo tMisMonitorResultLog_UpdateSet, TMisMonitorResultLogVo tMisMonitorResultLog_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultLog_UpdateSet, TMisMonitorResultLogVo.T_MIS_MONITOR_RESULT_LOG_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorResultLog_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_RESULT_LOG where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            string strSQL = "delete from T_MIS_MONITOR_RESULT_LOG ";
            strSQL += this.BuildWhereStatement(tMisMonitorResultLog);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        public string GetPlanID(string strResultID)
        {

            string strSQL = "select D.PLAN_ID from T_MIS_MONITOR_RESULT as A left join T_MIS_MONITOR_SAMPLE_INFO as B on (A.SAMPLE_ID=B.ID) left join T_MIS_MONITOR_SUBTASK as C on (B.SUBTASK_ID = C.ID) left join T_MIS_MONITOR_TASK as D on (C.TASK_ID = D.ID) where A.ID ='{0}'";

            strSQL = String.Format(strSQL, strResultID);

            DataTable dt= SqlHelper.ExecuteDataTable(strSQL);

            if (dt.Rows.Count != 1)
            {
                return "error";
            }
            else
            {
                return dt.Rows[0]["PLAN_ID"].ToString().Trim();
            }
        }
        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorResultLog"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorResultLog)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorResultLog.ID.ToString()));
                }
                //样品结果表ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisMonitorResultLog.RESULT_ID.ToString()));
                }
                //原结果数据
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.OLD_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OLD_RESULT = '{0}'", tMisMonitorResultLog.OLD_RESULT.ToString()));
                }
                //新结果数据
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.NEW_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NEW_RESULT = '{0}'", tMisMonitorResultLog.NEW_RESULT.ToString()));
                }
                //分析负责人员ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.HEAD_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HEAD_USERID = '{0}'", tMisMonitorResultLog.HEAD_USERID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.ASSISTANT_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ASSISTANT_USERID = '{0}'", tMisMonitorResultLog.ASSISTANT_USERID.ToString()));
                }
                //完成时间
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FINISH_DATE = '{0}'", tMisMonitorResultLog.FINISH_DATE.ToString()));
                }
                //校核人ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.CHECK_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_USERID = '{0}'", tMisMonitorResultLog.CHECK_USERID.ToString()));
                }
                //校核时间
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.CHECK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_DATE = '{0}'", tMisMonitorResultLog.CHECK_DATE.ToString()));
                }
                //校核意见
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.CHECK_OPINION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_OPINION = '{0}'", tMisMonitorResultLog.CHECK_OPINION.ToString()));
                }
                //复核人ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.APPROVE_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_USERID = '{0}'", tMisMonitorResultLog.APPROVE_USERID.ToString()));
                }
                //复核时间
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.APPROVE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_DATE = '{0}'", tMisMonitorResultLog.APPROVE_DATE.ToString()));
                }
                //复核意见
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.APPROVE_OPINION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_OPINION = '{0}'", tMisMonitorResultLog.APPROVE_OPINION.ToString()));
                }
                //质控手段审核人ID
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.QC_APP_USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_USER_ID = '{0}'", tMisMonitorResultLog.QC_APP_USER_ID.ToString()));
                }
                //质控手段审核时间
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.QC_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_DATE = '{0}'", tMisMonitorResultLog.QC_APP_DATE.ToString()));
                }
                //质控手段审核意见
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.QC_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_INFO = '{0}'", tMisMonitorResultLog.QC_APP_INFO.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorResultLog.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorResultLog.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorResultLog.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorResultLog.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorResultLog.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorResultLog.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
