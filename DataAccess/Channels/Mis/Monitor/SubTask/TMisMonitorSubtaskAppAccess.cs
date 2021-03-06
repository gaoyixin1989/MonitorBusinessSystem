using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务审核表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskAppAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SUBTASK_APP " + this.BuildWhereStatement(tMisMonitorSubtaskApp);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskAppVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK_APP  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskAppVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskAppVo Details(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK_APP " + this.BuildWhereStatement(tMisMonitorSubtaskApp));
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskAppVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSubtaskAppVo> SelectByObject(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK_APP " + this.BuildWhereStatement(tMisMonitorSubtaskApp));
            return SqlHelper.ExecuteObjectList(tMisMonitorSubtaskApp, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_SUBTASK_APP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSubtaskApp));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBTASK_APP " + this.BuildWhereStatement(tMisMonitorSubtaskApp);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <returns></returns>
        public TMisMonitorSubtaskAppVo SelectByObject(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBTASK_APP " + this.BuildWhereStatement(tMisMonitorSubtaskApp);
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskAppVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSubtaskApp, TMisMonitorSubtaskAppVo.T_MIS_MONITOR_SUBTASK_APP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubtaskApp, TMisMonitorSubtaskAppVo.T_MIS_MONITOR_SUBTASK_APP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSubtaskApp.ID);
           return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubtaskApp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp_UpdateSet, TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubtaskApp_UpdateSet, TMisMonitorSubtaskAppVo.T_MIS_MONITOR_SUBTASK_APP_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSubtaskApp_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SUBTASK_APP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            string strSQL = "delete from T_MIS_MONITOR_SUBTASK_APP ";
            strSQL += this.BuildWhereStatement(tMisMonitorSubtaskApp);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSubtaskApp)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSubtaskApp.ID.ToString()));
                }
                //监测子任务ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SUBTASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSubtaskApp.SUBTASK_ID.ToString()));
                }
                //采样任务分配人
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SAMPLE_ASSIGN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ASSIGN_ID = '{0}'", tMisMonitorSubtaskApp.SAMPLE_ASSIGN_ID.ToString()));
                }
                //采样任务分配时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SAMPLE_ASSIGN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ASSIGN_DATE = '{0}'", tMisMonitorSubtaskApp.SAMPLE_ASSIGN_DATE.ToString()));
                }
                //质控手段设置人
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.QC_USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_USER_ID = '{0}'", tMisMonitorSubtaskApp.QC_USER_ID.ToString()));
                }
                //质控手段设置时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.QC_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_DATE = '{0}'", tMisMonitorSubtaskApp.QC_DATE.ToString()));
                }
                //分析任务分配人
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.ANALYSE_ASSIGN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSE_ASSIGN_ID = '{0}'", tMisMonitorSubtaskApp.ANALYSE_ASSIGN_ID.ToString()));
                }
                //分析任务分配时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.ANALYSE_ASSIGN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSE_ASSIGN_DATE = '{0}'", tMisMonitorSubtaskApp.ANALYSE_ASSIGN_DATE.ToString()));
                }
                //质控手段审核人ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.QC_APP_USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_USER_ID = '{0}'", tMisMonitorSubtaskApp.QC_APP_USER_ID.ToString()));
                }
                //质控手段审核时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.QC_APP_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_DATE = '{0}'", tMisMonitorSubtaskApp.QC_APP_DATE.ToString()));
                }
                //质控手段审核意见
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.QC_APP_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_APP_INFO = '{0}'", tMisMonitorSubtaskApp.QC_APP_INFO.ToString()));
                }
                //数据审核
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.RESULT_AUDIT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_AUDIT = '{0}'", tMisMonitorSubtaskApp.RESULT_AUDIT.ToString()));
                }
                //分析室主任审核
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.RESULT_CHECK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_CHECK = '{0}'", tMisMonitorSubtaskApp.RESULT_CHECK.ToString()));
                }
                //分析室主任审核时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.RESULT_CHECK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_CHECK_DATE = '{0}'", tMisMonitorSubtaskApp.RESULT_CHECK_DATE.ToString()));
                }
                //技术室审核
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.RESULT_QC_CHECK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_QC_CHECK = '{0}'", tMisMonitorSubtaskApp.RESULT_QC_CHECK.ToString()));
                }
                //技术室审核时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.RESULT_QC_CHECK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_QC_CHECK_DATE = '{0}'", tMisMonitorSubtaskApp.RESULT_QC_CHECK_DATE.ToString()));
                }
                //现场复核
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SAMPLING_CHECK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_CHECK = '{0}'", tMisMonitorSubtaskApp.SAMPLING_CHECK.ToString()));
                }
                //现场审核
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SAMPLING_QC_CHECK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_QC_CHECK = '{0}'", tMisMonitorSubtaskApp.SAMPLING_QC_CHECK.ToString()));
                }
                //采样后质控
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.SAMPLING_END_QC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_END_QC = '{0}'", tMisMonitorSubtaskApp.SAMPLING_END_QC.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSubtaskApp.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSubtaskApp.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSubtaskApp.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSubtaskApp.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorSubtaskApp.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSubtaskApp.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
