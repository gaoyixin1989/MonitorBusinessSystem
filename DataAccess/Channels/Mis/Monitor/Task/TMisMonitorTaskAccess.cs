using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Report;

namespace i3.DataAccess.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        public int GetSelectResultCountByTicketNum(string strTicketNum)
        {
            string strSQL = string.Format("select Count(*) from T_MIS_MONITOR_TASK where TICKET_NUM='{0}'", strTicketNum);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTask">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskVo Details(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask));
            return SqlHelper.ExecuteObject(new TMisMonitorTaskVo(), strSQL);
        }

        public DataTable Details_One(long workID)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK where CCFLOW_ID1='" + workID + "'");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskVo> SelectByObject(TMisMonitorTaskVo tMisMonitorTask, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask));
            return SqlHelper.ExecuteObjectList(tMisMonitorTask, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskVo tMisMonitorTask, int iIndex, int iCount)
        {

            string strSQL = @" select * from T_MIS_MONITOR_TASK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask));
            //return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorTask, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据WORK_ID获取常规数据信息
        ///  黄进军 add 20150917
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetEnvInfo(string work_id)
        {
            string strSQL = "select A.*,B.PLAN_YEAR,B.PLAN_MONTH,B.PLAN_TYPE from T_MIS_MONITOR_TASK A inner join T_MIS_CONTRACT_PLAN B on A.PLAN_ID=B.ID where A.CCFLOW_ID1=" + work_id;
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskVo SelectByObject(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskVo(), strSQL);
        }


        /// <summary>
        /// //黄飞 20150924 更新附件保存数据
        /// </summary>
        /// <param name="filenameA"></param>
        /// <param name="strContratID"></param>
        /// <returns></returns>
        public DataTable UpdatAtt(string filenameA, string strContratID)
        {
            string strSQL = @"update T_OA_ATT set BUSINESS_ID='{0}' where BUSINESS_TYPE='{1}'";
            strSQL = string.Format(strSQL, strContratID, filenameA);
            return SqlHelper.ExecuteDataTable(strSQL);
        }



        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTask.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskVo tMisMonitorTask_UpdateSet, TMisMonitorTaskVo tMisMonitorTask_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTask_UpdateSet, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTask_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK ";
            strSQL += this.BuildWhereStatement(tMisMonitorTask);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        //参数没用
        public DataTable GetDocNo(string CONTRACT_TYPE, int iIndex, int iCount)
        {
            string SQL = "select id,TICKET_NUM from T_Mis_Report order by id desc";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(SQL, iIndex, iCount));
        }
        //参数没用
        public int GetSelectResultCounts(string CONTRACT_TYPE)
        {
            string strSQL = "select Count(*) from T_Mis_Report ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        public DataTable GetPhone(string strWorkTask_id, string strCompanyIdFrim)
        {
            string sql = "select top 1  Contact_Name,link_phone,PHONE from T_MIS_MONITOR_TASK_COMPANY where " + (strCompanyIdFrim == "" ? "1=1" : "ID='" + strCompanyIdFrim + "'") + " and task_id='" + strWorkTask_id + "'";
            return SqlHelper.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>是否成功</returns>
        public bool Creates(TMisReportVo vo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(vo, TMisReportVo.T_MIS_MONITOR_TASK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        public DataTable GetInfo(string year, string month, int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select YEAR,MONTH,TYPE,COUNT(*) as COUNT FROM T_Mis_Report WHERE 1=1 ");
            if (!string.IsNullOrEmpty(year))
            {
                sb.Append(" and year='" + year + "'");
            }
            if (!string.IsNullOrEmpty(month))
            {
                sb.Append(" and month='" + month + "'");
            }
            sb.Append("  group by  year,month,type ");
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }
        public int GetInfoCount(string year, string month)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select COUNT(*) as COUNT FROM T_Mis_Report WHERE 1=1 ");
            if (!string.IsNullOrEmpty(year))
            {
                sb.Append(" and year='" + year + "'");
            }
            if (!string.IsNullOrEmpty(month))
            {
                sb.Append(" and month='" + month + "'");
            }
            sb.Append("  group by  year,month,type ");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sb.ToString()));
        }

        /// <summary>
        /// 功能描述：获得监测任务信息
        /// 创建时间:2012-12-5
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns></returns>
        public TMisMonitorTaskVo GetContractTaskInfo(string strTaskID)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK where ID='{0}'";

            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskVo(), strSQL);
        }

        /// <summary>
        /// 功能描述：保存监测任务、样品所有信息（预约）
        /// 创建时间：2012-12-20
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="tMisMonitorTaskCompanyA">监测任务委托单位</param>
        /// <param name="tMisMonitorTaskCompanyB">监测任务受检单位</param>
        /// <param name="arrTaskPoint">监测任务点位集合</param>
        /// <param name="arrSubtask">监测子任务集合</param>
        /// <param name="arrPointItem">监测任务点位明细集合</param>
        /// <param name="arrSample">样品集合</param>
        /// <param name="arrSampleResult">样品结果集合</param>
        /// <param name="arrSampleResultApp">分析任务执行集合</param>
        /// <returns>布尔值</returns>
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyB, TMisMonitorReportVo objReportVo, ArrayList arrTaskPoint, ArrayList arrSubtask, ArrayList arrPointItem, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            ArrayList arrSql = new ArrayList();
            //插入监测任务
            if (!string.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                //arrSql.Add(string.Format("delete from T_MIS_MONITOR_TASK where PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID));
            }
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            //插入监测任务委托单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            //插入监测任务受检单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyB, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            arrSql.Add(SqlHelper.BuildInsertExpress(objReportVo, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE));
            //插入监测子任务 根据监测类型进行分别插入
            if (arrSubtask.Count > 0)
            {
                foreach (TMisMonitorSubtaskVo objSubVo in arrSubtask)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubVo, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE));
                }
            }
            //插入监测点位
            if (arrTaskPoint.Count > 0)
            {
                foreach (TMisMonitorTaskPointVo objPointVo in arrTaskPoint)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objPointVo, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE));
                }
            }
            //插入监测点位明细
            if (arrPointItem.Count > 0)
            {
                foreach (TMisMonitorTaskItemVo objTaskItemVo in arrPointItem)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objTaskItemVo, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE));
                }
            }
            //插入样品
            if (arrSample.Count > 0)
            {
                foreach (TMisMonitorSampleInfoVo objSampleInfo in arrSample)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE));
                }
            }
            //插入样品结果 
            if (arrSampleResult.Count > 0)
            {
                foreach (TMisMonitorResultVo objResult in arrSampleResult)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE));
                }
            }
            //插入分析执行
            if (arrSampleResultApp.Count > 0)
            {
                foreach (TMisMonitorResultAppVo objResultApp in arrSampleResultApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE));
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 功能描述：临时性委托 保存监测任务、样品所有信息（预约）
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="arrTaskPoint">监测任务点位集合</param>
        /// <param name="arrSubtask">监测子任务集合</param>
        /// <param name="arrPointItem">监测任务点位明细集合</param>
        /// <param name="arrSample">样品集合</param>
        /// <param name="arrSampleResult">样品结果集合</param>
        /// <param name="arrSampleResultApp">分析任务执行集合</param>
        /// <returns>布尔值</returns>
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorReportVo objReportVo, ArrayList arrTaskPoint, ArrayList arrSubtask, ArrayList arrPointItem, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            ArrayList arrSql = new ArrayList();
            //插入监测任务
            if (!string.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                arrSql.Add(string.Format("delete from T_MIS_MONITOR_TASK where PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID));
            }
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            arrSql.Add(SqlHelper.BuildInsertExpress(objReportVo, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE));
            //插入监测子任务 根据监测类型进行分别插入
            if (arrSubtask.Count > 0)
            {
                foreach (TMisMonitorSubtaskVo objSubVo in arrSubtask)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubVo, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE));
                }
            }
            //插入监测点位
            if (arrTaskPoint.Count > 0)
            {
                foreach (TMisMonitorTaskPointVo objPointVo in arrTaskPoint)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objPointVo, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE));
                }
            }
            //插入监测点位明细
            if (arrPointItem.Count > 0)
            {
                foreach (TMisMonitorTaskItemVo objTaskItemVo in arrPointItem)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objTaskItemVo, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE));
                }
            }
            //插入样品
            if (arrSample.Count > 0)
            {
                foreach (TMisMonitorSampleInfoVo objSampleInfo in arrSample)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE));
                }
            }
            //插入样品结果 
            if (arrSampleResult.Count > 0)
            {
                foreach (TMisMonitorResultVo objResult in arrSampleResult)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE));
                }
            }
            //插入分析执行
            if (arrSampleResultApp.Count > 0)
            {
                foreach (TMisMonitorResultAppVo objResultApp in arrSampleResultApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE));
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }


        /// <summary>
        /// 功能描述：保存监测任务、样品所有信息（预约）
        /// 创建时间：2012-12-20
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="tMisMonitorTaskCompanyA">监测任务委托单位</param>
        /// <param name="tMisMonitorTaskCompanyB">监测任务受检单位</param>
        /// <param name="arrTaskPoint">监测任务点位集合</param>
        /// <param name="arrSubtask">监测子任务集合</param>
        /// <param name="arrPointItem">监测任务点位明细集合</param>
        /// <param name="arrSample">样品集合</param>
        /// <param name="arrSampleResult">样品结果集合</param>
        /// <param name="arrSampleResultApp">分析任务执行集合</param>
        /// <returns>布尔值</returns>
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, ArrayList arrTaskPoint, ArrayList arrSubtask, ArrayList arrSubtaskApp, ArrayList arrPointItem, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            ArrayList arrSql = new ArrayList();
            //插入监测任务
            if (!string.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                arrSql.Add(string.Format("delete from T_MIS_MONITOR_TASK where PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID));
            }
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            //插入监测子任务 根据监测类型进行分别插入
            if (arrSubtask.Count > 0)
            {
                foreach (TMisMonitorSubtaskVo objSubVo in arrSubtask)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubVo, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE));
                }
            }
            if (arrSubtaskApp.Count > 0)
            {
                foreach (TMisMonitorSubtaskAppVo objSubAppVo in arrSubtaskApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubAppVo, TMisMonitorSubtaskAppVo.T_MIS_MONITOR_SUBTASK_APP_TABLE));
                }
            }
            //插入监测点位
            if (arrTaskPoint.Count > 0)
            {
                foreach (TMisMonitorTaskPointVo objPointVo in arrTaskPoint)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objPointVo, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE));
                }
            }
            //插入监测点位明细
            if (arrPointItem.Count > 0)
            {
                foreach (TMisMonitorTaskItemVo objTaskItemVo in arrPointItem)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objTaskItemVo, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE));
                }
            }
            //插入样品
            if (arrSample.Count > 0)
            {
                foreach (TMisMonitorSampleInfoVo objSampleInfo in arrSample)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE));
                }
            }
            //插入样品结果 
            if (arrSampleResult.Count > 0)
            {
                foreach (TMisMonitorResultVo objResult in arrSampleResult)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE));
                }
            }
            //插入分析执行
            if (arrSampleResultApp.Count > 0)
            {
                foreach (TMisMonitorResultAppVo objResultApp in arrSampleResultApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE));
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 功能描述：保存监测任务、监测报告（验收委托）
        /// 创建时间：2012-12-29
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="tMisMonitorTaskCompanyA">监测任务委托单位</param>
        /// <param name="tMisMonitorTaskCompanyB">监测任务受检单位</param>
        /// <param name="objReportVo">监测报告</param>
        /// <returns></returns>
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyB, TMisMonitorReportVo objReportVo)
        {
            ArrayList arrSql = new ArrayList();
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            //插入监测任务委托单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            //插入监测任务受检单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyB, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            //插入监测报告 
            arrSql.Add(SqlHelper.BuildInsertExpress(objReportVo, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE));
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 功能描述：保存监测任务、样品所有信息（验收委托拆分）
        /// 创建时间：2014-12-05
        /// 创建人：魏林
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="tMisMonitorTaskCompanyA">监测任务委托单位</param>
        /// <param name="tMisMonitorTaskCompanyB">监测任务受检单位</param>
        /// <param name="arrTaskPoint">监测任务点位集合</param>
        /// <param name="arrSubtask">监测子任务集合</param>
        /// <param name="arrSubtaskApp"></param>
        /// <param name="arrPointItem">监测任务点位明细集合</param>
        /// <param name="arrSample">样品集合</param>
        /// <param name="arrSampleResult">样品结果集合</param>
        /// <param name="arrSampleResultApp">分析任务执行集合</param>
        /// <returns>布尔值</returns>
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyB, TMisMonitorReportVo objReportVo, ArrayList arrTaskPoint, ArrayList arrSubtask, ArrayList arrSubtaskApp, ArrayList arrPointItem, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            ArrayList arrSql = new ArrayList();
            //插入监测任务
            if (!string.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                //arrSql.Add(string.Format("delete from T_MIS_MONITOR_TASK where PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID));
            }
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            //插入监测任务委托单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            //插入监测任务受检单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyB, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            arrSql.Add(SqlHelper.BuildInsertExpress(objReportVo, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE));
            //插入监测子任务 根据监测类型进行分别插入
            if (arrSubtask.Count > 0)
            {
                foreach (TMisMonitorSubtaskVo objSubVo in arrSubtask)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubVo, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE));
                }
            }
            if (arrSubtaskApp.Count > 0)
            {
                foreach (TMisMonitorSubtaskAppVo objSubAppVo in arrSubtaskApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubAppVo, TMisMonitorSubtaskAppVo.T_MIS_MONITOR_SUBTASK_APP_TABLE));
                }
            }
            //插入监测点位
            if (arrTaskPoint.Count > 0)
            {
                foreach (TMisMonitorTaskPointVo objPointVo in arrTaskPoint)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objPointVo, TMisMonitorTaskPointVo.T_MIS_MONITOR_TASK_POINT_TABLE));
                }
            }
            //插入监测点位明细
            if (arrPointItem.Count > 0)
            {
                foreach (TMisMonitorTaskItemVo objTaskItemVo in arrPointItem)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objTaskItemVo, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE));
                }
            }
            //插入样品
            if (arrSample.Count > 0)
            {
                foreach (TMisMonitorSampleInfoVo objSampleInfo in arrSample)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE));
                }
            }
            //插入样品结果 
            if (arrSampleResult.Count > 0)
            {
                foreach (TMisMonitorResultVo objResult in arrSampleResult)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE));
                }
            }
            //插入分析执行
            if (arrSampleResultApp.Count > 0)
            {
                foreach (TMisMonitorResultAppVo objResultApp in arrSampleResultApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE));
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 功能描述：保存监测任务、样品所有信息（自送样预约）
        /// 创建时间：2012-12-29
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="tMisMonitorTask">监测任务</param>
        /// <param name="tMisMonitorTaskCompanyA">监测任务委托单位</param>
        /// <param name="tMisMonitorTaskCompanyB">监测任务受检单位</param>
        /// <param name="arrSubtask">监测子任务集合</param>
        /// <param name="arrSample">样品集合</param>
        /// <param name="arrSampleResult">样品结果集合</param>
        /// <param name="arrSampleResultApp">分析任务执行集合</param>
        /// <returns>布尔值</returns>
        public bool SaveSampleTrans(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo tMisMonitorTaskCompanyB, TMisMonitorReportVo objReportVo, ArrayList arrSubtask, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            ArrayList arrSql = new ArrayList();
            //插入监测任务
            if (!string.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                arrSql.Add(string.Format("delete from T_MIS_MONITOR_TASK where PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID));
            }
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTask, TMisMonitorTaskVo.T_MIS_MONITOR_TASK_TABLE));
            //插入监测任务委托单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyA, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            //插入监测任务受检单位
            arrSql.Add(SqlHelper.BuildInsertExpress(tMisMonitorTaskCompanyB, TMisMonitorTaskCompanyVo.T_MIS_MONITOR_TASK_COMPANY_TABLE));
            arrSql.Add(SqlHelper.BuildInsertExpress(objReportVo, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE));
            //插入监测子任务 根据监测类型进行分别插入
            if (arrSubtask.Count > 0)
            {
                foreach (TMisMonitorSubtaskVo objSubVo in arrSubtask)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSubVo, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE));
                }
            }
            //插入样品
            if (arrSample.Count > 0)
            {
                foreach (TMisMonitorSampleInfoVo objSampleInfo in arrSample)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE));
                }
            }
            //插入样品结果 
            if (arrSampleResult.Count > 0)
            {
                foreach (TMisMonitorResultVo objResult in arrSampleResult)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE));
                }
            }
            //插入分析执行
            if (arrSampleResultApp.Count > 0)
            {
                foreach (TMisMonitorResultAppVo objResultApp in arrSampleResultApp)
                {
                    arrSql.Add(SqlHelper.BuildInsertExpress(objResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE));
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 获取监测任务办理情况统计列表 饼图  Create By 胡方扬 2013-01-02
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetTaskFinishedChart(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, string Dept, bool isFinished)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format("SELECT ROW_NUMBER() OVER(ORDER BY A.ID) AS ROWNUM, A.ID AS TASK_ID,A.CONTRACT_ID,A.CONTRACT_CODE,A.PLAN_ID,A.PROJECT_NAME,A.CONTRACT_TYPE,A.CLIENT_COMPANY_ID,A.TESTED_COMPANY_ID,CONVERT(varchar(100), A.ASKING_DATE, 23) AS ASKING_DATE,CONVERT(varchar(100), A.FINISH_DATE, 23) AS FINISH_DATE,A.TASK_STATUS," +
                                      " B.ID AS REPORT_ID,B.REPORT_CODE,B.IF_GET,C.BOOKTYPE," +
                                      " CASE WHEN C.BOOKTYPE='2' THEN (SELECT COUNT(*) FROM dbo.T_MIS_CONTRACT_SAMPLE_PLAN D WHERE D.ID=A.PLAN_ID) ELSE (SELECT COUNT(*) FROM dbo.T_MIS_CONTRACT_PLAN E WHERE E.ID=A.PLAN_ID) END AS CTNUM," +
                                      " CASE WHEN C.BOOKTYPE='2' THEN (SELECT '第'+NUM+'次' FROM dbo.T_MIS_CONTRACT_SAMPLE_PLAN D WHERE D.ID=A.PLAN_ID) ELSE (SELECT '第'+PLAN_NUM+'次' FROM dbo.T_MIS_CONTRACT_PLAN E WHERE E.ID=A.PLAN_ID) END AS NUM," +
                                      " CASE WHEN C.BOOKTYPE='2' THEN (SELECT COUNT(*)  FROM dbo.T_MIS_CONTRACT_SAMPLEITEM G WHERE G.CONTRACT_SAMPLE_ID=A.PLAN_ID) ELSE (SELECT COUNT(*) FROM dbo.T_MIS_CONTRACT_PLAN_POINT F WHERE F.PLAN_ID=A.PLAN_ID) END AS ITEMNUM" +
                                      " FROM dbo.T_MIS_MONITOR_TASK A INNER JOIN " +
                                      " dbo.T_MIS_MONITOR_REPORT B ON A.ID=B.TASK_ID " +
                                      " LEFT JOIN dbo.T_MIS_CONTRACT C ON A.CONTRACT_ID=C.ID " +
                                      " LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY H ON H.ID=A.TESTED_COMPANY_ID" +
                                      " LEFT JOIN dbo.T_SYS_USER_POST I ON I.USER_ID=B.REPORT_SCHEDULER" +
                                      " LEFT JOIN dbo.T_SYS_POST J ON J.ID=I.POST_ID" +
                                      " LEFT JOIN dbo.T_SYS_DICT K ON K.DICT_CODE=J.POST_DEPT_ID" +
                                      " WHERE 1=1");
            //获取已完成的
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_STATUS) && isFinished)
            {
                strSQL += String.Format(" AND A.TASK_STATUS='{0}'", tMisMonitorTask.TASK_STATUS);
            }
            //获取未完成的
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_STATUS) && !isFinished)
            {
                strSQL += String.Format(" AND A.TASK_STATUS!='{0}'", tMisMonitorTask.TASK_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND A.CONTRACT_CODE='{0}'", tMisMonitorTask.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND CONVERT(varchar(100), A.ASKING_DATE, 23)  ");
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && !String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorTask.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorTask.REMARK3);
                }

                strSQL += String.Format(" BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }
            //区域
            if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.AREA))
            {
                strSQL += String.Format(" AND H.AREA='{0}'", tMisMonitorTaskCompany.AREA);
            }
            //受检单位名称
            if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_NAME))
            {
                strSQL += String.Format(" AND H.COMPANY_NAME LIKE '%{0}%'", tMisMonitorTaskCompany.COMPANY_NAME);
            }
            //部门
            if (!String.IsNullOrEmpty(Dept))
            {
                strSQL += String.Format(" AND K.DICT_TEXT LIKE '%{0}%'", Dept);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 根据完成状态 获取监测任务完成、未完成、总和
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetTaskChartCountWithStatus(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, string Dept)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format("SELECT CASE WHEN  A.TASK_STATUS!='11' THEN '未完成任务' ELSE  '已完成任务' END as FINISHTYPE,COUNT(*) AS FINISHSUM" +
                                                            " FROM dbo.T_MIS_MONITOR_TASK A  " +
                                                            " LEFT JOIN dbo.T_MIS_MONITOR_REPORT B ON A.ID=B.TASK_ID" +
                                                            " LEFT JOIN dbo.T_MIS_CONTRACT C ON A.CONTRACT_ID=C.ID" +
                                                           " LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY H ON H.ID=A.TESTED_COMPANY_ID" +
                                                           " LEFT JOIN dbo.T_SYS_USER_POST I ON I.USER_ID=B.REPORT_EX_ATTACHE_ID" +
                                                           " LEFT JOIN dbo.T_SYS_POST J ON J.ID=I.POST_ID" +
                                                           " LEFT JOIN dbo.T_SYS_DICT K ON K.DICT_CODE=J.POST_DEPT_ID" +
                                                            " WHERE 1=1");

            if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND A.CONTRACT_CODE='{0}'", tMisMonitorTask.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND CONVERT(varchar(100), A.ASKING_DATE, 23)  ");
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && !String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorTask.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorTask.REMARK3);
                }

                strSQL += String.Format(" BETWEEN '{0}' AND '{1}' ", StartDate, EndDate);
            }
            //区域
            if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.AREA))
            {
                strSQL += String.Format(" AND H.AREA='{0}'", tMisMonitorTaskCompany.AREA);
            }
            //受检单位名称
            if (!String.IsNullOrEmpty(tMisMonitorTaskCompany.COMPANY_NAME))
            {
                strSQL += String.Format(" AND H.COMPANY_NAME LIKE '%{0}%'", tMisMonitorTaskCompany.COMPANY_NAME);
            }
            //部门
            if (!String.IsNullOrEmpty(Dept))
            {
                strSQL += String.Format(" AND K.DICT_TEXT LIKE '%{0}%'", Dept);
            }

            strSQL += String.Format(" GROUP BY A.TASK_STATUS");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region QHD
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForQHD(TMisMonitorTaskVo tMisMonitorTask, string strUserID)
        {
            string strSQL = @" select count(*) from T_MIS_MONITOR_TASK {0} and ID in 
                                                (select task_id from T_MIS_MONITOR_REPORT where IF_SEND='1' and (REPORT_SCHEDULER='{1}'
                                                    or REPORT_SCHEDULER in (select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID='{1}')))";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask), strUserID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForQHD(TMisMonitorTaskVo tMisMonitorTask, string strUserID, int iIndex, int iCount)
        {
            string strSQL = @" select task.*,report.ID as report_id from (select * from T_MIS_MONITOR_TASK {0}) task
                                                JOIN (select * from T_MIS_MONITOR_REPORT where IF_SEND='1' and (REPORT_SCHEDULER='{1}'
                                                or REPORT_SCHEDULER in (select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID='{1}'))) report
                                                ON task.ID=report.task_id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask), strUserID);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForZZ(TMisMonitorTaskVo tMisMonitorTask, string strUserID, bool bHasAccept)
        {
            string strsqlEx = "";
            if (bHasAccept)
                strsqlEx = " and IF_ACCEPT='1'";
            else
                strsqlEx = " and (IF_ACCEPT='0' or IF_ACCEPT is null)";
            string strSQL = @" select count(*) from T_MIS_MONITOR_TASK {0} and ID in 
                                                (select task_id from T_MIS_MONITOR_REPORT where IF_SEND='1' and (REPORT_SCHEDULER='{1}'
                                                    or REPORT_SCHEDULER in (select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID='{1}')) {2})";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask), strUserID, strsqlEx);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForZZ(TMisMonitorTaskVo tMisMonitorTask, string strUserID, int iIndex, int iCount, bool bHasAccept)
        {
            string strsqlEx = "";
            if (bHasAccept)
                strsqlEx = " and IF_ACCEPT='1'";
            else
                strsqlEx = " and (IF_ACCEPT='0' or IF_ACCEPT is null)";
            string strSQL = @" select task.*,report.ID as report_id from (select * from T_MIS_MONITOR_TASK {0}) task
                                                JOIN (select * from T_MIS_MONITOR_REPORT where IF_SEND='1' and (REPORT_SCHEDULER='{1}' 
                                                or REPORT_SCHEDULER in (select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID='{1}')) {2}) report
                                                ON task.ID=report.task_id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask), strUserID, strsqlEx);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 功能描述：报告任务分配
        /// 创建时间：2013-2-25
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForReportAccept(TMisMonitorTaskVo tMisMonitorTask, int iIndex, int iCount)
        {
            string strSQL = @" select task.*,report.ID as report_id,report.REPORT_SCHEDULER,report.RPT_ASK_DATE from (select * from T_MIS_MONITOR_TASK {0}) task
                                               INNER JOIN  T_MIS_MONITOR_REPORT report ON (report.IF_SEND='0' or report.IF_SEND is null) and task.ID=report.task_id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 功能描述：报告任务分配总数
        /// 创建时间：2013-2-25
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回结果</returns>
        public int SelectCountForReportAccept(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = @" select COUNT(*) from (select * from T_MIS_MONITOR_TASK {0}) task
                                               LEFT JOIN  T_MIS_MONITOR_REPORT report ON (report.IF_SEND='0' or report.IF_SEND is null) and task.ID=report.task_id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask));
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }
        #endregion

        /// <summary>
        /// 获取报告完成情况统计报表 胡方扬 2013-03-07 
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContract"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable SeletByTableReportFinished(TMisMonitorTaskVo tMisMonitorTask, TMisContractVo tMisContract, bool type)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT ROW_NUMBER() OVER(ORDER BY A.ID) AS ROWNUM,B.ID, CONVERT(VARCHAR(100), B.ASKING_DATE,23) AS ASKING_DATE,
CONVERT(VARCHAR(100), B.FINISH_DATE,23) AS FINISH_DATE,B.TASK_STATUS,B.PROJECT_ID,B.CONTRACT_ID,B.PROJECT_NAME,B.CONTRACT_CODE,B.CONTRACT_TYPE,H.REPORT_CODE,J.REAL_NAME,M.DICT_TEXT AS DEPT_NAME,N.DICT_TEXT AS CONTRACTTYPENAME
FROM dbo.T_MIS_MONITOR_TASK B
LEFT JOIN T_MIS_CONTRACT A ON B.CONTRACT_ID=A.ID 
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=A.TESTED_COMPANY_ID 
LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=B.ID 
LEFT JOIN dbo.T_SYS_USER J ON J.ID=B.PROJECT_ID 
LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID 
LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID 
LEFT JOIN T_SYS_DICT M ON M.DICT_CODE=L.POST_DEPT_ID AND M.DICT_TYPE='dept' 
LEFT JOIN T_SYS_DICT N ON N.DICT_CODE=B.CONTRACT_TYPE AND N.DICT_TYPE='Contract_Type'
WHERE B.TASK_STATUS IN ('11') AND  B.TASK_STATUS IS NOT NULL 
AND B.ASKING_DATE IS NOT NULL ");
            if (type)
            {
                //已正常完成的
                strSQL += String.Format("  AND CONVERT(DATETIME, CONVERT(VARCHAR(100), B.ASKING_DATE,23),111)>=CONVERT(DATETIME, CONVERT(VARCHAR(100),B.FINISH_DATE,23),111) ");
            }
            else
            {
                //超时完成的
                strSQL += String.Format("  AND CONVERT(DATETIME, CONVERT(VARCHAR(100), B.ASKING_DATE,23),111)<CONVERT(DATETIME, CONVERT(VARCHAR(100),B.FINISH_DATE,23),111) ");
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE='{0}'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), B.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && !String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorTask.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorTask.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            //执行部门
            if (!String.IsNullOrEmpty(tMisContract.REMARK5))
            {
                strSQL += String.Format(" AND M.DICT_TEXT LIKE '%{0}%'", tMisContract.REMARK5);
            }

            //委托书类别
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }
            //企业名称
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND G.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            strSQL += String.Format(" ORDER BY B.ASKING_DATE");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取办理及时率 胡方扬 2013-03-07
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SeletByTableReportFinishedCount(TMisMonitorTaskVo tMisMonitorTask, TMisContractVo tMisContract)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT CASE WHEN  CONVERT(DATETIME, CONVERT(varchar(100), B.ASKING_DATE,23),111)>=CONVERT(DATETIME, CONVERT(varchar(100), B.FINISH_DATE,23),111) THEN '正常完成' ELSE '超时完成' END AS FINISHTYPE,COUNT(*) AS FINISHSUM
FROM dbo.T_MIS_MONITOR_TASK B
LEFT JOIN T_MIS_CONTRACT A ON B.CONTRACT_ID=A.ID 
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=A.TESTED_COMPANY_ID 
LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=B.ID 
LEFT JOIN dbo.T_SYS_USER J ON J.ID=B.PROJECT_ID 
LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID 
LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID 
LEFT JOIN T_SYS_DICT M ON M.DICT_CODE=L.POST_DEPT_ID AND M.DICT_TYPE='dept' 
LEFT JOIN T_SYS_DICT N ON N.DICT_CODE=B.CONTRACT_TYPE AND N.DICT_TYPE='Contract_Type'
WHERE B.TASK_STATUS IN ('11') AND  B.TASK_STATUS IS NOT NULL 
AND B.ASKING_DATE IS NOT NULL ");
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE='{0}'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), B.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && !String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorTask.REMARK3, tMisMonitorTask.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorTask.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorTask.REMARK4) && String.IsNullOrEmpty(tMisMonitorTask.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorTask.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorTask.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            //执行部门
            if (!String.IsNullOrEmpty(tMisContract.REMARK5))
            {
                strSQL += String.Format(" AND M.DICT_TEXT LIKE '%{0}%'", tMisContract.REMARK5);
            }

            //委托书类别
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }
            //企业名称
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND G.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            strSQL += String.Format(" GROUP BY B.ASKING_DATE,B.FINISH_DATE");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目流程任务信息
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectSampleTaskForQY(string strSubTaskID, string strUserId, string strDutyCode, string strSubTaskStatus, int intPageIndex, int intPageSize)
        {
            //string strSQL = @"SELECT task.*,subtask.ID as subTaskId
            string strSQL = @"select DISTINCT a.ID,a.TICKET_NUM from (
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                                union all
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                                ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                                inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                                where b.RESULT_STATUS in('01','02') AND (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') AND a.subTaskId='{1}'
                                           ";
            strSQL = string.Format(strSQL, strSubTaskStatus, strSubTaskID);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        public DataTable SelectSampleTaskForMAS(string strSubTaskID, bool b, int intPageIndex, int intPageSize)
        {
            //string strSQL = @"SELECT task.*,subtask.ID as subTaskId
            string strWhere = "";
            if (b)
            {
                strWhere = "a.subTaskId='" + strSubTaskID + "'";
            }
            else
            {
                strWhere = "b.ID='" + strSubTaskID + "'";
            }
            string strSQL = @"select DISTINCT a.ID,a.TICKET_NUM from (
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                                union all
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                                ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                                inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                                where (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') AND {0}
                                           ";
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        public int SelectSampleTaskCountForMAS(string strSubTaskID, bool b)
        {
            string strWhere = "";
            if (b)
            {
                strWhere = "a.subTaskId='" + strSubTaskID + "'";
            }
            else
            {
                strWhere = "b.ID='" + strSubTaskID + "'";
            }
            string strSQL = @"select COUNT(DISTINCT a.ID) from (
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                                union all
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                                ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                                inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                                where (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') AND {0}";

            strSQL = string.Format(strSQL, strWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取现场监测项目流程任务信息
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectSampleTaskForQCQY(string strSubTaskID, string strUserId, string strDutyCode, string strSubTaskStatus, int intPageIndex, int intPageSize)
        {
            //            string strSQL = @"SELECT task.*,subtask.ID as subTaskId
            //                                            FROM T_MIS_MONITOR_TASK task
            //                                            INNER JOIN 
            //                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{2}' AND subtask.TASK_ID = task.ID
            //                                            INNER JOIN
            //                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{1}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
            //                                            INNER JOIN
            //                                            (SELECT * FROM T_SYS_USER_DUTY WHERE (USERID = '{0}' or USERID IN (select USER_ID
            //                                                                               from T_SYS_USER_PROXY
            //                                                                              where PROXY_USER_ID = '{0}'))) user_duty ON user_duty.DUTY_ID = duty.ID";
            //            strSQL = string.Format(strSQL, strUserId, strDutyCode, strSubTaskStatus);
            //            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));

            string strSQL = @"SELECT  DISTINCT(task.ID) as TASK_ID,task.*--,subtask.ID subTaskId
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND subtask.TASK_ID = task.ID
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.MONITOR_TYPE_ID =subtask.MONITOR_ID where subtask.ID='{1}'
                                            ";
            strSQL = string.Format(strSQL, strSubTaskStatus, strSubTaskID);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获取现场监测项目流程任务子任务信息
        /// 创建时间：2013-8-15
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public DataTable SelectSampleTaskForQCQY(string strSubTaskID, string strUserId, string strDutyCode, string strSubTaskStatus, string strTaskId)
        {
            string strSQL = @"UPDATE T_MIS_MONITOR_SUBTASK_APP SET SAMPLING_QC_CHECK='{1}',REMARK3='{2}' WHERE SUBTASK_ID='{0}'";
            strSQL = string.Format(strSQL, strSubTaskID, strUserId, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            SqlHelper.ExecuteNonQuery(strSQL);

            strSQL = @"SELECT distinct task.*,subtask.ID as subTaskId
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND subtask.TASK_ID = task.ID
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
                                            where task.ID='{1}' and subtask.ID='{2}'";
            strSQL = string.Format(strSQL, strSubTaskStatus, strTaskId, strSubTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取分析类现场监测项目流程任务信息
        /// 创建人：胡方扬
        /// 创建时间：2013-7-10
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectSampleTaskForWithSampleAnalysisQY(string strUserId, string strDutyCode, string strSubTaskStatus, string strQc, string strResultStatus, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT DISTINCT(task.ID),task.TICKET_NUM, '' REMARK1
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS in({2}) AND subtask.TASK_ID = task.ID 
                                            AND subtask.SAMPLING_MANAGER_ID='{0}'
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{1}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
--                                            INNER JOIN
--                                            (SELECT * FROM T_SYS_USER_DUTY WHERE (USERID = '{0}' or USERID IN (select USER_ID
--                                                                               from T_SYS_USER_PROXY
--                                                                              where PROXY_USER_ID = '{0}'))) user_duty ON user_duty.DUTY_ID = duty.ID
INNER JOIN (SELECT A.*,C.ID AS SUBTASK_ID,C.TASK_ID FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.QC_TYPE = '{3}' 
AND A.RESULT_STATUS = '{4}' AND D.IS_ANYSCENE_ITEM='1') TEMP ON TEMP.TASK_ID=TASK.ID";
            strSQL = string.Format(strSQL, strUserId, strDutyCode, strSubTaskStatus, strQc, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获取分析类现场监测项目流程任务信息总数
        /// 创建人：胡方扬
        /// 创建时间：2013-7-10
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public int SelectSampleTaskForWithSampleAnalysisCountQY(string strUserId, string strDutyCode, string strSubTaskStatus, string strQc, string strResultStatus)
        {
            string strSQL = @"SELECT DISTINCT(task.ID),task.TICKET_NUM, '' REMARK1
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS in({2}) AND subtask.TASK_ID = task.ID 
                                            AND subtask.SAMPLING_MANAGER_ID='{0}'
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{1}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
--                                            INNER JOIN
--                                            (SELECT * FROM T_SYS_USER_DUTY WHERE (USERID = '{0}' or USERID IN (select USER_ID
--                                                                               from T_SYS_USER_PROXY
--                                                                              where PROXY_USER_ID = '{0}'))) user_duty ON user_duty.DUTY_ID = duty.ID
INNER JOIN (SELECT A.*,C.ID AS SUBTASK_ID,C.TASK_ID FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.QC_TYPE = '{3}' 
AND A.RESULT_STATUS = '{4}' AND D.IS_ANYSCENE_ITEM='1') TEMP ON TEMP.TASK_ID=TASK.ID";
            strSQL = string.Format(strSQL, strUserId, strDutyCode, strSubTaskStatus, strQc, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        public DataTable SelectSampleTaskForWithSampleAnalysisMAS(string strResultID, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT DISTINCT(task.ID),task.TICKET_NUM, '' REMARK1
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID = task.ID 
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
INNER JOIN (SELECT A.*,C.ID AS SUBTASK_ID,C.TASK_ID FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.ID = '{0}' AND D.IS_ANYSCENE_ITEM='1') TEMP ON TEMP.TASK_ID=TASK.ID";
            strSQL = string.Format(strSQL, strResultID);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        public int SelectSampleTaskForWithSampleAnalysisCountMAS(string strResultID)
        {
            string strSQL = @"SELECT DISTINCT(task.ID),task.TICKET_NUM, '' REMARK1
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID = task.ID 
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
INNER JOIN (SELECT A.*,C.ID AS SUBTASK_ID,C.TASK_ID FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.ID = '{0}' AND D.IS_ANYSCENE_ITEM='1') TEMP ON TEMP.TASK_ID=TASK.ID";
            strSQL = string.Format(strSQL, strResultID);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 功能描述：获取分析类现场监测项目流程任务的包含分析类现场监测子任务信息
        /// 创建人：胡方扬
        /// 创建时间：2013-7-10
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectSampleSubTaskForWithSampleAnalysisQY(string strTaskId)
        {
            string strSQL = @"select DISTINCT (A.ID) AS SUBTASK_ID,A.* from dbo.T_MIS_MONITOR_SUBTASK A
INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
INNER JOIN dbo.T_BASE_ITEM_INFO E ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0'  AND E.IS_ANYSCENE_ITEM='1'
WHERE A.TASK_ID='{0}'";
            strSQL = string.Format(strSQL, strTaskId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectSampleSubTaskForWithSampleAnalysisMAS(string strTaskId, string strResultID)
        {
            string strSQL = @"select DISTINCT (A.ID) AS SUBTASK_ID,A.* from dbo.T_MIS_MONITOR_SUBTASK A
INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
INNER JOIN dbo.T_BASE_ITEM_INFO E ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0'  AND E.IS_ANYSCENE_ITEM='1'
WHERE A.TASK_ID='{0}' and D.ID='{1}'";
            strSQL = string.Format(strSQL, strTaskId, strResultID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取分析类现场监测项目流程任务的包含分析类现场监测子任务信息
        /// 创建人：胡方扬
        /// 创建时间：2013-7-10
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public int SelectSampleSubTaskForWithSampleAnalysisCountQY(string strTaskId)
        {
            string strSQL = @"select DISTINCT (A.ID) AS SUBTASK_ID,A.* from dbo.T_MIS_MONITOR_SUBTASK A
INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
INNER JOIN dbo.T_BASE_ITEM_INFO E ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0'  AND E.IS_ANYSCENE_ITEM='1'
WHERE A.TASK_ID='{0}'";
            strSQL = string.Format(strSQL, strTaskId);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 设置任务下的分析类监测项目信息状态 Create By weilin
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool SetAnysceneItem(string strTaskId, string strStatus)
        {
            string strSql = @"UPDATE D SET RESULT_STATUS='{1}'
                        from dbo.T_MIS_MONITOR_SUBTASK A
                        INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
                        INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
                        INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
                        INNER JOIN dbo.T_BASE_ITEM_INFO E ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0'  AND E.IS_ANYSCENE_ITEM='1'
                        WHERE A.TASK_ID='{0}'";
            strSql = string.Format(strSql, strTaskId, strStatus);

            return SqlHelper.ExecuteNonQuery(strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获取现场监测项目流程任务信息总数
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int SelectSampleTaskCountForQY(string strSubTaskID, string strUserId, string strDutyCode, string strSubTaskStatus)
        {
            //            string strSQL = @"SELECT COUNT(DISTINCT(task.ID))
            //                                            FROM T_MIS_MONITOR_TASK task
            //                                            INNER JOIN 
            //                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND subtask.TASK_ID = task.ID
            //                                            INNER JOIN
            //                                            T_SYS_DUTY duty ON duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
            //                                            ";
            string strSQL = @"select COUNT(DISTINCT a.ID) from (
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                                union all
                                select task.ID,task.TICKET_NUM,c.ID SAMPLEID,subtask.ID subTaskId
                                FROM T_MIS_MONITOR_TASK task
                                INNER JOIN 
                                T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{0}' AND 
                                subtask.TASK_ID = task.ID
                                inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                                ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                                inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                                where b.RESULT_STATUS in('01','02') AND (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') AND a.subTaskId='{1}'";

            strSQL = string.Format(strSQL, strSubTaskStatus, strSubTaskID);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取现场监测项目流程任务信息总数
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="strDutyCode">职责权限</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int SelectSampleTaskCountForQCQY(string strSubTaskID, string strUserId, string strDutyCode, string strSubTaskStatus)
        {
            //            string strSQL = @"SELECT COUNT(*)
            //                                            FROM T_MIS_MONITOR_TASK task
            //                                            INNER JOIN 
            //                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{2}' AND subtask.TASK_ID = task.ID
            //                                            INNER JOIN
            //                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{1}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
            //                                            INNER JOIN
            //                                            (SELECT * FROM T_SYS_USER_DUTY WHERE (USERID = '{0}' or USERID IN (select USER_ID
            //                                                                               from T_SYS_USER_PROXY
            //                                                                              where PROXY_USER_ID = '{0}'))) user_duty ON user_duty.DUTY_ID = duty.ID";
            //            strSQL = string.Format(strSQL, strUserId, strDutyCode, strSubTaskStatus);
            //            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());

            string strSQL = @"SELECT COUNT(DISTINCT(task.ID))
                                            FROM T_MIS_MONITOR_TASK task
                                            INNER JOIN 
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{2}' AND subtask.TASK_ID = task.ID
                                            INNER JOIN
                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{1}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
                                            INNER JOIN
                                            (SELECT * FROM T_SYS_USER_DUTY WHERE (USERID = '{0}' or USERID IN (select USER_ID
                                                                               from T_SYS_USER_PROXY
                                                                              where PROXY_USER_ID = '{0}'))) user_duty ON user_duty.DUTY_ID = duty.ID where subtask.ID='{3}'";
            strSQL = string.Format(strSQL, strUserId, strDutyCode, strSubTaskStatus, strSubTaskID);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取所有任务的环节信息
        /// 创建时间：2013-4-1
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public DataTable SelectByTableForTaskTraking(string strTaskId)
        {
            string strSQL = @"select DISTINCT task.ID as taskId,task.TASK_STATUS as taskStatus,
                                                                            subtask.ID as subTaskId,subtask.TASK_STATUS as subTaskStatus,subtask.MONITOR_ID,
                                                                            result.RESULT_STATUS
                                                        FROM dbo.T_MIS_MONITOR_TASK task
                                                        LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID=task.ID
                                                        LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO sample ON sample.SUBTASK_ID=subtask.ID
                                                        LEFT JOIN 
                                                        (
                                                        SELECT a.ID,a.SAMPLE_ID,a.RESULT_STATUS 
                                                        FROM dbo.T_MIS_MONITOR_RESULT a
                                                        INNER JOIN 
                                                        T_BASE_ITEM_INFO b ON b.HAS_SUB_ITEM = '0' and b.IS_SAMPLEDEPT = '否' and b.ID =a.ITEM_ID
                                                        ) result ON result.SAMPLE_ID=sample.ID
                                                        where task.ID='{0}'";
            strSQL = string.Format(strSQL, strTaskId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取环境质量任务预约详细信息 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectEnvPlanTaskList(TMisMonitorTaskVo tMisMonitorTask, int PageIndex, int PageSize)
        {
            string strSQL = @"SELECT A.ID,A.MONITOR_ID,C.ID AS PLAN_ID,C.PLAN_TYPE,C.PLAN_YEAR,C.PLAN_MONTH,C.PLAN_DAY,B.CONTRACT_POINT_ID,A.POINT_ID AS ITEM_ID,A.POINT_NAME,
                                        E.ID AS TASK_ID,E.PROJECT_NAME,E.TICKET_NUM
                                        FROM  dbo.T_MIS_CONTRACT_POINT  A
                                        LEFT JOIN dbo.T_MIS_CONTRACT_PLAN_POINT B ON B.CONTRACT_POINT_ID=A.ID
                                        LEFT JOIN dbo.T_MIS_CONTRACT_PLAN C ON C.ID=B.PLAN_ID
                                        LEFT JOIN dbo.T_MIS_CONTRACT_POINTITEM D ON D.ITEM_ID=A.POINT_ID AND D.CONTRACT_POINT_ID=A.ID
                                        RIGHT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=C.ID
                                        WHERE (C.PLAN_TYPE!='' OR C.PLAN_TYPE IS NOT NULL) ";
            if (!String.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                strSQL += String.Format(" AND E.PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.ID))
            {
                strSQL += String.Format(" AND E.ID='{0}'", tMisMonitorTask.ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, PageIndex, PageSize));
        }


        /// <summary>
        /// 获取环境质量任务预约信息详细条数 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectEnvPlanTaskListCount(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = @"SELECT A.ID,A.MONITOR_ID,C.ID AS PLAN_ID,C.PLAN_TYPE,C.PLAN_YEAR,C.PLAN_MONTH,C.PLAN_DAY,B.CONTRACT_POINT_ID,A.POINT_ID AS ITEM_ID,A.POINT_NAME,
                                        E.ID AS TASK_ID,E.PROJECT_NAME,E.TICKET_NUM
                                        FROM  dbo.T_MIS_CONTRACT_POINT  A
                                        LEFT JOIN dbo.T_MIS_CONTRACT_PLAN_POINT B ON B.CONTRACT_POINT_ID=A.ID
                                        LEFT JOIN dbo.T_MIS_CONTRACT_PLAN C ON C.ID=B.PLAN_ID
                                        LEFT JOIN dbo.T_MIS_CONTRACT_POINTITEM D ON D.ITEM_ID=A.POINT_ID AND D.CONTRACT_POINT_ID=A.ID
                                        RIGHT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=C.ID
                                        WHERE (C.PLAN_TYPE!='' OR C.PLAN_TYPE IS NOT NULL) ";
            if (!String.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                strSQL += String.Format(" AND E.PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.ID))
            {
                strSQL += String.Format(" AND E.ID='{0}'", tMisMonitorTask.ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取环境质量任务预约信息 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectEnvPlanTaskAbList(TMisMonitorTaskVo tMisMonitorTask, TMisContractPlanVo tMisContractPlan, int PageIndex, int PageSize)
        {
            string strSQL = @"SELECT A.ID,A.PLAN_TYPE,A.PLAN_YEAR,A.PLAN_MONTH,A.PLAN_DAY,B.ID AS TASK_ID,
                                        B.PROJECT_NAME,B.TICKET_NUM,B.ASKING_DATE,B.TASK_TYPE
                                        FROM dbo.T_MIS_CONTRACT_PLAN A 
                                        INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.PLAN_ID=A.ID
                                        WHERE (A.PLAN_TYPE!='' OR A.PLAN_TYPE IS NOT NULL) ";
            if (!String.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                strSQL += String.Format(" AND B.PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID);
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_TYPE))
            {
                strSQL += String.Format(" AND A.PLAN_TYPE='{0}'", tMisContractPlan.PLAN_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.ID))
            {
                strSQL += String.Format(" AND B.ID='{0}'", tMisMonitorTask.ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            else
            {
                strSQL += " AND A.PLAN_YEAR IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_MONTH))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            else
            {
                strSQL += " AND A.PLAN_MONTH IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_DAY))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            else
            {
                strSQL += " AND A.PLAN_DAY IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                strSQL += String.Format(" AND A.HAS_DONE ='{0}' ", tMisContractPlan.HAS_DONE);
            }
            //else
            //{
            //    strSQL += " AND A.HAS_DONE IS  NULL ";
            //}

            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND B.TICKET_NUM LIKE '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisMonitorTask.PROJECT_NAME);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.SEND_STATUS))
            {
                strSQL += String.Format(" AND B.SEND_STATUS = '{0}'", tMisMonitorTask.SEND_STATUS);
                if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
                {
                    strSQL += String.Format(" OR B.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
                }
            }
            else
            {
                strSQL += String.Format(" AND B.SEND_STATUS IS NULL ");
                strSQL += String.Format(" AND B.QC_STATUS IS NULL");
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, PageIndex, PageSize));
        }


        /// <summary>
        /// 获取环境质量任务预约信息条数 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <param name="flag">是否已完成的计划</param>
        /// <returns></returns>
        public int SelectEnvPlanTaskAbListCount(TMisMonitorTaskVo tMisMonitorTask, TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = @"SELECT A.ID,A.PLAN_TYPE,A.PLAN_YEAR,A.PLAN_MONTH,A.PLAN_DAY,B.ID AS TASK_ID,
                                        B.PROJECT_NAME,B.TICKET_NUM
                                        FROM dbo.T_MIS_CONTRACT_PLAN A 
                                        INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.PLAN_ID=A.ID
                                        WHERE (A.PLAN_TYPE!='' OR A.PLAN_TYPE IS NOT NULL) ";
            if (!String.IsNullOrEmpty(tMisMonitorTask.PLAN_ID))
            {
                strSQL += String.Format(" AND A.PLAN_ID='{0}'", tMisMonitorTask.PLAN_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_TYPE))
            {
                strSQL += String.Format(" AND A.PLAN_TYPE='{0}'", tMisContractPlan.PLAN_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.ID))
            {
                strSQL += String.Format(" AND B.ID='{0}'", tMisMonitorTask.ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            else
            {
                strSQL += " AND A.PLAN_YEAR IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_MONTH))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            else
            {
                strSQL += " AND A.PLAN_MONTH IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_DAY))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            else
            {
                strSQL += " AND A.PLAN_DAY IS NOT NULL ";
            }

            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                strSQL += String.Format(" AND A.HAS_DONE ='{0}' ", tMisContractPlan.HAS_DONE);
            }
            //else
            //{
            //    strSQL += " AND A.HAS_DONE IS  NULL ";
            //}

            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND B.TICKET_NUM LIKE '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisMonitorTask.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.SEND_STATUS))
            {
                strSQL += String.Format(" AND B.SEND_STATUS = '{0}'", tMisMonitorTask.SEND_STATUS);
                if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
                {
                    strSQL += String.Format(" OR B.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
                }
            }
            else
            {
                strSQL += String.Format(" AND B.SEND_STATUS IS NULL ");
                strSQL += String.Format(" AND B.QC_STATUS IS NULL");
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 创建原因：更新子任务状态
        /// 创建人：胡方扬
        /// 创建日期：2013-08-15
        /// </summary>
        /// <param name="objDt"></param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <param name="strTaskType">任务发送类型</param>
        /// <returns></returns>
        public bool SetSubTask(DataTable objDt, string strTaskStatus, string strTaskType)
        {
            ArrayList objArr = new ArrayList();
            if (objDt.Rows.Count > 0)
            {
                foreach (DataRow dr in objDt.Rows)
                {
                    string strSQL = @" UPDATE dbo.T_MIS_MONITOR_SUBTASK SET TASK_STATUS='{0}',TASK_TYPE='{1}' WHERE ID='{2}'";
                    strSQL = String.Format(strSQL, strTaskStatus, strTaskType, dr["subTaskId"].ToString());
                    objArr.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(objArr);
        }

        /// <summary>
        /// 功能描述：获取项目负责人
        /// 创建时间：2013-5-9
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">任务ID</param>
        /// <returns></returns>
        public DataTable GetProjectID(string strTaskID)
        {
            string strSQL = @"SELECT u.ID,u.REAL_NAME 
                                                FROM dbo.T_MIS_MONITOR_TASK task 
                                                INNER JOIN dbo.T_MIS_CONTRACT contract ON task.CONTRACT_ID=contract.ID
                                                INNER JOIN dbo.T_SYS_USER u ON u.ID = contract.PROJECT_ID
                                                WHERE task.ID='{0}'";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书和委托企业关联信息
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTaskTable(TMisMonitorTaskVo tMisTaskContract, int intPageIndex, int intPageSize)
        {

            string strSQL = " SELECT A.*,B.COMPANY_NAME FROM  dbo.T_MIS_MONITOR_TASK A INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY B ON A.CLIENT_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisTaskContract.CLIENT_COMPANY_ID))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%" + tMisTaskContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTACT_ID))
            {
                strSQL += " AND A.CONTACT_ID='" + tMisTaskContract.CONTACT_ID + "'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisTaskContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTRACT_TYPE))
            {
                strSQL += " AND A.CONTRACT_TYPE  = '" + tMisTaskContract.CONTRACT_TYPE + "'";
            }

            if (!String.IsNullOrEmpty(tMisTaskContract.SAMPLE_SOURCE))
            {
                strSQL += " AND A.SAMPLE_SOURCE='" + tMisTaskContract.SAMPLE_SOURCE + "'";
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取委托书和委托企业关联信息条数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        public int SelectByUnionTaskTableResult(TMisMonitorTaskVo tMisTaskContract)
        {

            string strSQL = " SELECT A.*,B.COMPANY_NAME FROM  dbo.T_MIS_MONITOR_TASK A INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY B ON A.CLIENT_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisTaskContract.CLIENT_COMPANY_ID))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%" + tMisTaskContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTACT_ID))
            {
                strSQL += " AND A.CONTACT_ID='" + tMisTaskContract.CONTACT_ID + "'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisTaskContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.CONTRACT_TYPE))
            {
                strSQL += " AND A.CONTRACT_TYPE  = '" + tMisTaskContract.CONTRACT_TYPE + "'";
            }

            if (!String.IsNullOrEmpty(tMisTaskContract.SAMPLE_SOURCE))
            {
                strSQL += " AND A.SAMPLE_SOURCE='" + tMisTaskContract.SAMPLE_SOURCE + "'";
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取自送样采样计划
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetContractInforUnionSamplePlan(TMisMonitorTaskVo tMisTaskContract, TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = String.Format(@" SELECT distinct A.ID,A.TICKET_NUM,A.CONTRACT_ID,A.CONTRACT_CODE,A.CONTRACT_YEAR,A.PROJECT_NAME,A.CONTRACT_TYPE,A.TEST_TYPE,A.CLIENT_COMPANY_ID,A.TESTED_COMPANY_ID,A.ASKING_DATE,A.PROJECT_ID,A.SAMPLE_SOURCE,
                                            B.ID AS SAMPLE_ID,B.FREQ,B.NUM AS SAMPLENUM,B.IF_PLAN,C.COMPANY_NAME,
                                            C.CONTACT_NAME,C.PHONE,
                                            --D.ID AS SUBTASK_ID,
                                            E.COMPANY_NAME AS CLIENT_COMPANY_NAME,E.CONTACT_NAME AS CLIENT_CONTACT_NAME,E.PHONE AS CLIENT_PNONE,
                                            F.ID AS REPORT_ID,F.REPORT_CODE 
                                            FROM dbo.T_MIS_MONITOR_TASK A
                                            INNER JOIN  dbo.T_MIS_CONTRACT_SAMPLE_PLAN B ON A.PLAN_ID=B.ID
                                            INNER JOIN  dbo.T_MIS_MONITOR_TASK_COMPANY C ON C.ID=A.TESTED_COMPANY_ID
                                            INNER JOIN dbo.T_MIS_MONITOR_SUBTASK D ON D.TASK_ID=A.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY E ON E.ID=A.CLIENT_COMPANY_ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=A.ID 
                                            WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisTaskContract.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tMisTaskContract.ID);
            }
            if (!String.IsNullOrEmpty(tMisTaskContract.SAMPLE_SOURCE))
            {
                strSQL += String.Format("  AND A.SAMPLE_SOURCE='{0}' ", tMisTaskContract.SAMPLE_SOURCE);
            }
            if (!String.IsNullOrEmpty(tMisContractSamplePlan.IF_PLAN))
            {
                strSQL += String.Format("  AND B.IF_PLAN='{0}' ", tMisContractSamplePlan.IF_PLAN);
            }

            if (!String.IsNullOrEmpty(tMisContractSamplePlan.ID))
            {
                strSQL += String.Format("  AND B.ID='{0}' ", tMisContractSamplePlan.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSummary(TMisMonitorTaskVo tMisMonitorTask, bool isLocal, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_TASK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTask));
            string strIS_SAMPLEDEPT = "否";
            string strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where remark_4='1' or  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            if (isLocal)
            {
                strIS_SAMPLEDEPT = "是";
                strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where (remark_4<>'1' or remark_4 is null) and  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            }
            strSQL += @" and ID in (select TASK_ID from T_MIS_MONITOR_SUBTASK where
                ID in (select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where
                    ID in (" + strSubSql + ")))";
            //            strSQL += @" and ID in (select TASK_ID from T_MIS_MONITOR_SUBTASK where
            //                ID in (select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where
            //                    ID in (select SAMPLE_ID from T_MIS_MONITOR_RESULT where
            //                        ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "'))))";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorTask, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSummary(TMisMonitorTaskVo tMisMonitorTask, bool isLocal)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK " + this.BuildWhereStatement(tMisMonitorTask);
            string strIS_SAMPLEDEPT = "否";
            string strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where remark_4='1' or  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            if (isLocal)
            {
                strIS_SAMPLEDEPT = "是";
                strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where (remark_4<>'1' or remark_4 is null) and  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            }
            strSQL += @" and ID in (select TASK_ID from T_MIS_MONITOR_SUBTASK where
                ID in (select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where
                    ID in (" + strSubSql + ")))";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        #endregion

        /// <summary>
        /// 监测结果补录
        /// </summary>
        public DataTable GetCompleteTask(int iIndex, int iCount)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK WHERE TASK_STATUS = '09' OR TASK_STATUS = '10'";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
            //return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 监测结果补录 分页
        /// </summary>
        public int GetTaskCount()
        {
            string strSql = "select count(*) from T_MIS_MONITOR_TASK WHERE TASK_STATUS = '09' OR TASK_STATUS = '10'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 删除已下达的监测任务 Create By: weilin 2014-8-6
        /// </summary>
        /// <param name="strPlanID"></param>
        /// <returns></returns>
        public bool DelTaskTrans(string strPlanID)
        {
            ArrayList arrSql = new ArrayList();
            string strSql = string.Empty;
            string strSqlTemp = "delete from T_MIS_MONITOR_TASK where PLAN_ID = {0}";//何海亮添加
            strSql = @"delete {1} from T_MIS_CONTRACT_PLAN pl
                        left join T_MIS_MONITOR_TASK task on(pl.ID=task.PLAN_ID)
                        left join T_MIS_MONITOR_SUBTASK sub on(task.ID=sub.TASK_ID)
                        left join T_MIS_MONITOR_SUBTASK_APP subapp on(sub.ID=subapp.SUBTASK_ID)
                        left join T_MIS_MONITOR_SAMPLE_INFO sample on(sub.ID=sample.SUBTASK_ID)
                        left join T_MIS_MONITOR_RESULT result on(sample.ID=result.SAMPLE_ID)
                        left join T_MIS_MONITOR_RESULT_APP resapp on(result.ID=resapp.RESULT_ID)
                        where pl.ID='{0}'";

            arrSql.Add(string.Format(strSql, strPlanID, "resapp"));

            arrSql.Add(string.Format(strSql, strPlanID, "result"));

            arrSql.Add(string.Format(strSql, strPlanID, "sample"));

            arrSql.Add(string.Format(strSql, strPlanID, "subapp"));

            arrSql.Add(string.Format(strSql, strPlanID, "sub"));

            arrSql.Add(string.Format(strSql, strPlanID, "task"));

            arrSql.Add(string.Format(strSql, strPlanID, "pl"));

            arrSql.Add(string.Format(strSqlTemp, strPlanID));

            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }


        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskVo tMisMonitorTask)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTask)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTask.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisMonitorTask.CONTRACT_ID.ToString()));
                }
                //预约ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_ID = '{0}'", tMisMonitorTask.PLAN_ID.ToString()));
                }
                //委托书编
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_CODE = '{0}'", tMisMonitorTask.CONTRACT_CODE.ToString()));
                }
                //委托年度
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_YEAR = '{0}'", tMisMonitorTask.CONTRACT_YEAR.ToString()));
                }
                //项目名称
                if (!String.IsNullOrEmpty(tMisMonitorTask.PROJECT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_NAME like '%{0}%'", tMisMonitorTask.PROJECT_NAME.ToString()));
                }
                //委托类型
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONTRACT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_TYPE in ('{0}')", tMisMonitorTask.CONTRACT_TYPE.ToString()));
                }
                //报告类型
                if (!String.IsNullOrEmpty(tMisMonitorTask.TEST_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_TYPE = '{0}'", tMisMonitorTask.TEST_TYPE.ToString()));
                }
                //监测目的
                if (!String.IsNullOrEmpty(tMisMonitorTask.TEST_PURPOSE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_PURPOSE = '{0}'", tMisMonitorTask.TEST_PURPOSE.ToString()));
                }
                //任务单号
                if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TICKET_NUM like '%{0}%'", tMisMonitorTask.TICKET_NUM.ToString()));
                }
                //委托企业ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.CLIENT_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CLIENT_COMPANY_ID = '{0}'", tMisMonitorTask.CLIENT_COMPANY_ID.ToString()));
                }
                //受检企业ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.TESTED_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TESTED_COMPANY_ID = '{0}'", tMisMonitorTask.TESTED_COMPANY_ID.ToString()));
                }
                //合同签订日期
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONSIGN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONSIGN_DATE = '{0}'", tMisMonitorTask.CONSIGN_DATE.ToString()));
                }
                //要求完成日期
                if (!String.IsNullOrEmpty(tMisMonitorTask.ASKING_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ASKING_DATE = '{0}'", tMisMonitorTask.ASKING_DATE.ToString()));
                }
                //完成日期
                if (!String.IsNullOrEmpty(tMisMonitorTask.FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FINISH_DATE = '{0}'", tMisMonitorTask.FINISH_DATE.ToString()));
                }
                //样品来源,1,抽样，2，自送样
                if (!String.IsNullOrEmpty(tMisMonitorTask.SAMPLE_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SOURCE = '{0}'", tMisMonitorTask.SAMPLE_SOURCE.ToString()));
                }
                //送样人ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.CONTACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTACT_ID = '{0}'", tMisMonitorTask.CONTACT_ID.ToString()));
                }
                //接样人ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.MANAGER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MANAGER_ID = '{0}'", tMisMonitorTask.MANAGER_ID.ToString()));
                }
                //计划制定人ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.CREATOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATOR_ID = '{0}'", tMisMonitorTask.CREATOR_ID.ToString()));
                }
                //项目负责人ID
                if (!String.IsNullOrEmpty(tMisMonitorTask.PROJECT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_ID = '{0}'", tMisMonitorTask.PROJECT_ID.ToString()));
                }
                //计划制定日期
                if (!String.IsNullOrEmpty(tMisMonitorTask.CREATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_DATE = '{0}'", tMisMonitorTask.CREATE_DATE.ToString()));
                }
                //状态
                if (!String.IsNullOrEmpty(tMisMonitorTask.STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATE = '{0}'", tMisMonitorTask.STATE.ToString()));
                }
                //计划状态
                if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_STATUS.ToString().Trim()))
                {
                    if (tMisMonitorTask.COMFIRM_STATUS.ToString() == "0")
                    {
                        strWhereStatement.Append(string.Format(" AND TASK_STATUS = '{0}'", tMisMonitorTask.TASK_STATUS.ToString()));
                    }
                    else if (tMisMonitorTask.COMFIRM_STATUS.ToString() == "1")
                    {
                        strWhereStatement.Append(string.Format(" AND TASK_STATUS <> '{0}'", tMisMonitorTask.TASK_STATUS.ToString()));
                    }
                }
                //质控环节状态
                if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS.ToString()));
                }
                //是否全程质控，1为是
                if (!String.IsNullOrEmpty(tMisMonitorTask.ALLQC_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ALLQC_STATUS = '{0}'", tMisMonitorTask.ALLQC_STATUS.ToString()));
                }
                //任务类型，1表示环境质量，普通委托书类任务为空
                if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE.ToString()));
                }
                //任务确认状态 add by ssz
                //if (!String.IsNullOrEmpty(tMisMonitorTask.COMFIRM_STATUS.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND COMFIRM_STATUS = '{0}'", tMisMonitorTask.COMFIRM_STATUS.ToString()));
                //}

                if (!String.IsNullOrEmpty(tMisMonitorTask.SEND_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEND_STATUS = '{0}'", tMisMonitorTask.SEND_STATUS.ToString()));
                }

                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorTask.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorTask.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorTask.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorTask.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorTask.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorTask.REMARK5.ToString()));
                }
                //生成报告的处理人
                if (!String.IsNullOrEmpty(tMisMonitorTask.REPORT_HANDLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REPORT_HANDLE = '{0}'", tMisMonitorTask.REPORT_HANDLE.ToString()));
                }
                //送样人
                if (!String.IsNullOrEmpty(tMisMonitorTask.SAMPLE_SEND_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SEND_MAN = '{0}'", tMisMonitorTask.SAMPLE_SEND_MAN.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisMonitorTask.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisMonitorTask.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisMonitorTask.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisMonitorTask.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisMonitorTask.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisMonitorTask.CCFLOW_ID3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
