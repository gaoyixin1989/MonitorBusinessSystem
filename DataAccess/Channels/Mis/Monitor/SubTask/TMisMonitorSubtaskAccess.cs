using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;

namespace i3.DataAccess.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(tMisMonitorSubtask);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskVo Details(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(tMisMonitorSubtask));
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSubtaskVo> SelectByObject(TMisMonitorSubtaskVo tMisMonitorSubtask, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(tMisMonitorSubtask));
            return SqlHelper.ExecuteObjectList(tMisMonitorSubtask, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSubtaskVo tMisMonitorSubtask, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_SUBTASK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSubtask));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(tMisMonitorSubtask);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns></returns>
        public TMisMonitorSubtaskVo SelectByObject(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = "select * from T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(tMisMonitorSubtask);
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskVo(), strSQL);
        }

        /// <summary>
        /// 获取监测子任务→监测任务 信息
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableWithTask(TMisMonitorSubtaskVo tMisMonitorSubtask, string strMonitorID, string strTaskStatus, string strTestedCompanyID, string strContractCode, string strUserID, int iIndex, int iCount)
        {
            string strSQL = @"SELECT     t1.ID, t1.SAMPLE_ASK_DATE AS REMARK1, t1.MONITOR_ID AS REMARK2,t1.SAMPLING_MANAGER_ID as SAMPLING_MANAGER_ID, t2.CONTRACT_ID, t2.PLAN_ID, t2.CONTRACT_CODE, t2.CONTRACT_YEAR, 
                                t2.PROJECT_NAME, t2.CONTRACT_TYPE, t2.TEST_TYPE, t2.TEST_PURPOSE, t2.CLIENT_COMPANY_ID, t2.TESTED_COMPANY_ID, t2.CONSIGN_DATE, 
                                t2.ASKING_DATE, t2.FINISH_DATE, t2.SAMPLE_SOURCE, t2.CONTACT_ID, t2.MANAGER_ID, t2.CREATOR_ID, t2.PROJECT_ID, t2.CREATE_DATE, t2.STATE, 
                                t2.TASK_STATUS
                                FROM  T_MIS_MONITOR_SUBTASK AS t1 
                                INNER JOIN  T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID  
                                INNER JOIN T_MIS_MONITOR_TASK_COMPANY AS t3 ON t3.ID = t2.TESTED_COMPANY_ID  
                                where 1=1 ";
            strSQL += (strMonitorID.Length > 0) ? " and t1.MONITOR_ID='" + strMonitorID + "'" : "";
            strSQL += (strTaskStatus.Length > 0) ? " and t1.TASK_STATUS='" + strTaskStatus + "'" : "";
            strSQL += (strTestedCompanyID.Length > 0) ? " and t3.COMPANY_NAME like '%" + strTestedCompanyID + "%'" : "";
            strSQL += (strContractCode.Length > 0) ? " and t2.CONTRACT_CODE='" + strContractCode + "'" : "";
            strSQL += (strUserID.Length > 0) ? " and (t1.SAMPLING_MANAGER_ID='" + strUserID + "'or t1.SAMPLING_MANAGER_ID in(select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID = '" + strUserID + "'))" : "";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorSubtask, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectWithTaskResultCount(TMisMonitorSubtaskVo tMisMonitorSubtask, string strTestedCompanyID, string strContractCode)
        {
            string strSQL = @"SELECT     count(*)
                                FROM         T_MIS_MONITOR_SUBTASK AS t1 INNER JOIN
                                T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID ";
            strSQL += String.Format(strSQL, BuildWhereStatement(tMisMonitorSubtask));
            strSQL += (strTestedCompanyID.Length > 0) ? " and t2.TESTED_COMPANY_ID='" + strTestedCompanyID + "'" : "";
            strSQL += (strContractCode.Length > 0) ? " and t2.CONTRACT_CODE='" + strContractCode + "'" : "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取监测子任务→监测任务 信息  含 环境质量类  胡方扬 2013-05-07
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        /// 黄进军添加20140901 t1.SAMPLE_FINISH_DATE 作为采样要求完成时间
        public DataTable SelectByTableWithAllTask(TMisMonitorSubtaskVo tMisMonitorSubtask, string strMonitorID, string strTaskStatus, string strTestedCompanyID, string strContractCode, string strUserID, int iIndex, int iCount)
        {
            string strSQL = @"SELECT     t1.ID, t1.SAMPLE_ASK_DATE AS REMARK1, t1.MONITOR_ID AS REMARK2,t1.SAMPLING_MANAGER_ID as SAMPLING_MANAGER_ID,t1.SAMPLE_FINISH_DATE, t2.ID AS TASK_ID,t2.CONTRACT_ID, t2.PLAN_ID, t2.CONTRACT_CODE, t2.CONTRACT_YEAR, 
                                t2.PROJECT_NAME, t2.CONTRACT_TYPE, t2.TEST_TYPE, t2.TEST_PURPOSE, t2.CLIENT_COMPANY_ID, t2.TESTED_COMPANY_ID, t2.CONSIGN_DATE, 
                                t2.ASKING_DATE, t2.FINISH_DATE, t2.SAMPLE_SOURCE, t2.CONTACT_ID, t2.MANAGER_ID, t2.CREATOR_ID, t2.PROJECT_ID, t2.CREATE_DATE, t2.STATE, 
                                t2.TASK_STATUS,t2.QC_STATUS,t1.TASK_TYPE,t2.TICKET_NUM,t1.REMARK2 as IS_RECEIVE,TM.MONITOR_TYPE_NAME AS MONITOR_NAME,t1.REMARK1 as SOURCE_ID, t1.MONITOR_ID
                                FROM  T_MIS_MONITOR_SUBTASK AS t1 
                                INNER JOIN  T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID --and isnull(t1.REMARK1,'')=''
                                INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO TM ON TM.ID=t1.MONITOR_ID    
                                where 1=1 ";
            strSQL += (strMonitorID.Length > 0) ? " and t1.MONITOR_ID='" + strMonitorID + "'" : "";
            strSQL += (strTaskStatus.Length > 0) ? " and t1.TASK_STATUS='" + strTaskStatus + "'" : "";
            strSQL += (strTestedCompanyID.Length > 0) ? " and t3.COMPANY_NAME like '%" + strTestedCompanyID + "%'" : "";
            strSQL += (strContractCode.Length > 0) ? " and t2.CONTRACT_CODE='" + strContractCode + "'" : "";
            strSQL += (strUserID.Length > 0) ? " and (t1.SAMPLING_MANAGER_ID='" + strUserID + "'or t1.SAMPLING_MANAGER_ID in(select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID = '" + strUserID + "'))" : "";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorSubtask, strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 获取监测子任务→树形监测任务父节点数据信息  含 环境质量类  胡方扬 2013-06-03
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableWithAllTaskForFatherTree(TMisMonitorSubtaskVo tMisMonitorSubtask, string strMonitorID, string strTaskStatus, string strTestedCompanyID, string strContractCode, string strUserID, int iIndex, int iCount)
        {
            string strSQL = @"SELECT     t1.ID, t1.MONITOR_ID AS REMARK2,t1.SAMPLING_MANAGER_ID as SAMPLING_MANAGER_ID,t2.ID AS TASK_ID, t2.CONTRACT_ID, t2.PLAN_ID, t2.CONTRACT_CODE, t2.CONTRACT_YEAR, 
                                t2.PROJECT_NAME, t2.CONTRACT_TYPE, t2.TEST_TYPE, t2.TEST_PURPOSE, t2.CLIENT_COMPANY_ID, t2.TESTED_COMPANY_ID, t2.CONSIGN_DATE, 
                                t2.ASKING_DATE, t2.FINISH_DATE, t2.SAMPLE_SOURCE, t2.CONTACT_ID, t2.MANAGER_ID, t2.CREATOR_ID, t2.PROJECT_ID, t2.CREATE_DATE, t2.STATE, 
                                t2.TASK_STATUS,t2.QC_STATUS,t2.TASK_TYPE,t2.TICKET_NUM,t1.REMARK2 as IS_RECEIVE,TM.MONITOR_TYPE_NAME AS MONITOR_NAME
                                FROM  T_MIS_MONITOR_SUBTASK AS t1 
                                INNER JOIN  T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID --and isnull(t1.REMARK1,'')=''
								INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO TM ON TM.ID=t1.MONITOR_ID  
                                where 1=1 ";
            strSQL += (strMonitorID.Length > 0) ? " and t1.MONITOR_ID='" + strMonitorID + "'" : "";
            strSQL += (strTaskStatus.Length > 0) ? " and t1.TASK_STATUS='" + strTaskStatus + "'" : "";
            strSQL += (strTestedCompanyID.Length > 0) ? " and t3.COMPANY_NAME like '%" + strTestedCompanyID + "%'" : "";
            strSQL += (strContractCode.Length > 0) ? " and t2.CONTRACT_CODE='" + strContractCode + "'" : "";
            strSQL += (strUserID.Length > 0) ? " and (t1.SAMPLING_MANAGER_ID='" + strUserID + "'or t1.SAMPLING_MANAGER_ID in(SELECT USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID = '" + strUserID + "'))" : "";

            strSQL = String.Format(@"SELECT TASK_ID,PROJECT_NAME,TICKET_NUM,'' REMARK1,SAMPLING_MANAGER_ID,ASKING_DATE,CONTRACT_ID,PLAN_ID,CREATE_DATE,MONITOR_NAME=stuff((SELECT ','+ MONITOR_NAME FROM ({0}) TTC WHERE TASK_ID=TB.TASK_ID for xml path('')), 1, 1, '')
FROM ({1})TB
GROUP BY TASK_ID,PROJECT_NAME,TICKET_NUM,SAMPLING_MANAGER_ID,ASKING_DATE,CONTRACT_ID,PLAN_ID,CREATE_DATE", strSQL, strSQL);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorSubtask, strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 获取监测子任务→监测任务信息总数  含 环境质量类  胡方扬 2013-05-07
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int SelectByTableWithAllTaskRecordCount(TMisMonitorSubtaskVo tMisMonitorSubtask, string strMonitorID, string strTaskStatus, string strTestedCompanyID, string strContractCode, string strUserID)
        {
            string strSQL = @"SELECT     t1.ID, t1.SAMPLE_ASK_DATE AS REMARK1, t1.MONITOR_ID AS REMARK2,t1.SAMPLING_MANAGER_ID as SAMPLING_MANAGER_ID, t2.CONTRACT_ID, t2.PLAN_ID, t2.CONTRACT_CODE, t2.CONTRACT_YEAR, 
                                t2.PROJECT_NAME, t2.CONTRACT_TYPE, t2.TEST_TYPE, t2.TEST_PURPOSE, t2.CLIENT_COMPANY_ID, t2.TESTED_COMPANY_ID, t2.CONSIGN_DATE, 
                                t2.ASKING_DATE, t2.FINISH_DATE, t2.SAMPLE_SOURCE, t2.CONTACT_ID, t2.MANAGER_ID, t2.CREATOR_ID, t2.PROJECT_ID, t2.CREATE_DATE, t2.STATE, 
                                t2.TASK_STATUS,t2.QC_STATUS,t2.TASK_TYPE,t2.TICKET_NUM
                                FROM  T_MIS_MONITOR_SUBTASK AS t1 
                                INNER JOIN  T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID  
                                where 1=1 ";
            strSQL += (strMonitorID.Length > 0) ? " and t1.MONITOR_ID='" + strMonitorID + "'" : "";
            strSQL += (strTaskStatus.Length > 0) ? " and t1.TASK_STATUS='" + strTaskStatus + "'" : "";
            strSQL += (strTestedCompanyID.Length > 0) ? " and t3.COMPANY_NAME like '%" + strTestedCompanyID + "%'" : "";
            strSQL += (strContractCode.Length > 0) ? " and t2.CONTRACT_CODE='" + strContractCode + "'" : "";
            strSQL += (strUserID.Length > 0) ? " and (t1.SAMPLING_MANAGER_ID='" + strUserID + "'or t1.SAMPLING_MANAGER_ID in(select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID = '" + strUserID + "'))" : "";

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }


        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSubtask, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubtask, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSubtask.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        public bool Edit_One(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubtask, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE);
            strSQL += string.Format(" where TASK_ID='{0}' ", tMisMonitorSubtask.TASK_ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubtask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskVo tMisMonitorSubtask_UpdateSet, TMisMonitorSubtaskVo tMisMonitorSubtask_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSubtask_UpdateSet, TMisMonitorSubtaskVo.T_MIS_MONITOR_SUBTASK_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSubtask_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SUBTASK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = "delete from T_MIS_MONITOR_SUBTASK ";
            strSQL += this.BuildWhereStatement(tMisMonitorSubtask);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获取采样任务统计数
        /// 创建时间：2012-12-23
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int GetCountWithTask(TMisMonitorSubtaskVo tMisMonitorSubtask, string strUserID)
        {
            string strSQL = @"SELECT  count(*)
                                                FROM  T_MIS_MONITOR_SUBTASK ";
            strSQL += BuildWhereStatement(tMisMonitorSubtask);
            strSQL += (strUserID.Length > 0) ? " and (SAMPLING_MANAGER_ID='" + strUserID + "'or SAMPLING_MANAGER_ID in(select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID = '" + strUserID + "'))" : "";

            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }


        /// <summary>
        /// 功能描述：获得监测任务所有的监测类别
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTask">监测任务</param>
        /// <returns></returns>
        public DataTable selectAllContractType(string strTask)
        {
            //            string strSQL = @"select DISTINCT subtask.MONITOR_ID,type.MONITOR_TYPE_NAME from T_MIS_MONITOR_SUBTASK subtask 
            //                                                INNER JOIN T_BASE_MONITOR_TYPE_INFO type on subtask.MONITOR_ID=type.ID
            //                                                where subtask.TASK_ID='{0}'";
            //胡方扬 2013-04-18  版本合并
            string strSQL = @"select DISTINCT subtask.MONITOR_ID,type.MONITOR_TYPE_NAME from T_MIS_MONITOR_SUBTASK subtask 
                                                INNER JOIN T_BASE_MONITOR_TYPE_INFO type on subtask.MONITOR_ID=type.ID
                                                where subtask.TASK_ID='{0}'";
            strSQL = string.Format(strSQL, strTask);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：回退报告编制任务
        /// 创建时间：2013-2-4
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <returns></returns>
        public bool CombackTaskToAnalyse(string strTaskId)
        {
            string strSQL = "";
            ArrayList arrSql = new ArrayList();
            //级联修改任务状态
            //监测任务状态修改SQL
            strSQL = string.Format("update T_MIS_MONITOR_TASK set TASK_STATUS='01' where ID='{0}'", strTaskId);
            arrSql.Add(strSQL);
            //监测子任务状态修改SQL
            strSQL = string.Format("update T_MIS_MONITOR_SUBTASK set TASK_STATUS='03' where TASK_ID='{0}'", strTaskId);
            arrSql.Add(strSQL);
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 判断该任务下的子任务是否都已经结束 Create By weilin 2013-11-09
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="b">true:发送后判断，false:发送前判断</param>
        /// <returns></returns>
        public bool isFinishSubTask(string strTaskId, bool b)
        {
            string strSql = @"select distinct TASK_STATUS from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and TASK_STATUS not in('09','24')";
            strSql = string.Format(strSql, strTaskId);

            if (b)
                return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? false : true;
            else
                return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 1 ? false : true;
        }

        /// <summary>
        /// 判断该任务是否存在分析类监测项目 Create By weilin 2013-11-09
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool isExistAnyscene(string strTaskId, string strSubTaskID)
        {
            string strSql = @"select 1 from dbo.T_MIS_MONITOR_SUBTASK A
                                INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
                                INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
                                INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
                                INNER JOIN dbo.T_BASE_ITEM_INFO E  ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0' 
                                           AND E.IS_SAMPLEDEPT='否'  AND E.IS_ANYSCENE_ITEM<>'1'
                                WHERE {0} and {1}";
            strSql = string.Format(strSql, (strTaskId != "" ? "A.TASK_ID='" + strTaskId + "'" : "1=1"), (strSubTaskID != "" ? "A.ID='" + strSubTaskID + "'" : "1=1"));

            return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// 判断该任务是否存在分析类现场监测项目 Create By weilin 2014-02-13
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool isExistAnysceneDept(string strTaskId, string strSubTaskID)
        {
            string strSql = @"select 1 from dbo.T_MIS_MONITOR_SUBTASK A
                                INNER JOIN dbo.T_MIS_MONITOR_TASK B ON B.ID=A.TASK_ID
                                INNER JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO C ON C.SUBTASK_ID=A.ID
                                INNER JOIN dbo.T_MIS_MONITOR_RESULT D ON D.SAMPLE_ID=C.ID
                                INNER JOIN dbo.T_BASE_ITEM_INFO E  ON E.ID=D.ITEM_ID  AND E.HAS_SUB_ITEM = '0' 
                                           AND E.IS_ANYSCENE_ITEM='1'
                                WHERE {0} and {1}";
            strSql = string.Format(strSql, (strTaskId != "" ? "A.TASK_ID='" + strTaskId + "'" : "1=1"), (strSubTaskID != "" ? "A.ID='" + strSubTaskID + "'" : "1=1"));

            return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获取现场监测项目的子任务信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleSubTaskForQY(string strSubTaskID, string strTaskId, string strSubTaskStatus, string strDutyCode)
        {
            //            string strSQL = @"SELECT subtask.* FROM
            //                                            T_MIS_MONITOR_SUBTASK subtask
            //                                            INNER JOIN
            //                                            T_SYS_DUTY duty ON duty.DICT_CODE = '{2}' AND duty.MONITOR_TYPE_ID =subtask.MONITOR_ID
            //                                            WHERE subtask.TASK_STATUS='{1}' AND subtask.TASK_ID='{0}'";
            string strSQL = @"select distinct a.ID,a.TASK_ID,a.MONITOR_ID,a.TASK_STATUS, '' Suggestion from (
                            select subtask.ID,subtask.TASK_ID,subtask.MONITOR_ID,subtask.TASK_STATUS,c.ID SAMPLEID
                            FROM T_MIS_MONITOR_SUBTASK subtask 
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                            union all
                            select subtask.ID,subtask.TASK_ID,subtask.MONITOR_ID,subtask.TASK_STATUS,c.ID SAMPLEID
                            FROM T_MIS_MONITOR_SUBTASK subtask 
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                            ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                            inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                            where a.TASK_STATUS='{1}' and a.TASK_ID='{0}' 
                            and b.RESULT_STATUS in('01','02') AND (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') and a.ID='{2}'";

            strSQL = string.Format(strSQL, strTaskId, strSubTaskStatus, strSubTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectSampleSubTaskForMAS(string strSubTaskID, string strTaskId, bool b)
        {
            string strWhere = "";
            if (b)
            {
                strWhere = "a.ID='" + strSubTaskID + "'";
            }
            else
            {
                strWhere = "b.ID='" + strSubTaskID + "'";
            }

            string strSQL = @"select distinct a.ID,a.TASK_ID,a.MONITOR_ID,a.TASK_STATUS, '' Suggestion from (
                            select subtask.ID,subtask.TASK_ID,subtask.MONITOR_ID,subtask.TASK_STATUS,c.ID SAMPLEID
                            FROM T_MIS_MONITOR_SUBTASK subtask 
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                            union all
                            select subtask.ID,subtask.TASK_ID,subtask.MONITOR_ID,subtask.TASK_STATUS,c.ID SAMPLEID
                            FROM T_MIS_MONITOR_SUBTASK subtask 
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                            ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                            inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                            where a.TASK_ID='{0}' 
                            and (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1') and {1}";

            strSQL = string.Format(strSQL, strTaskId, strWhere);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取监测类别数
        /// 创建时间：2013-4-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public int getMonitorCountByTask(string strTaskID)
        {
            string strSQL = @"SELECT     COUNT(DISTINCT MONITOR_ID)
                                FROM         T_MIS_MONITOR_SUBTASK WHERE TASK_ID='{0}'";
            strSQL = string.Format(strSQL, strTaskID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 功能描述：获取监测类别
        /// 创建时间：2013-4-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getMonitorByTask(string strTaskID)
        {
            string strSQL = @"SELECT    DISTINCT subtask.MONITOR_ID,typeinfo.MONITOR_TYPE_NAME
                                FROM         T_MIS_MONITOR_SUBTASK subtask
                                INNER JOIN T_BASE_MONITOR_TYPE_INFO typeinfo ON typeinfo.ID=subtask.MONITOR_ID
                                WHERE subtask.TASK_ID='{0}'";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取子任务对象
        /// 创建时间：2013-4-8
        /// 创建人;邵世卓
        /// </summary>
        /// <param name="strSampleId"></param>
        /// <returns></returns>
        public DataTable GetSubTaskObjectBySample(string strSampleId)
        {
            string strSQL = @"SELECT  subtask.*
                                FROM         T_MIS_MONITOR_SUBTASK subtask
                                INNER JOIN T_MIS_MONITOR_SAMPLE_INFO sample ON sample.ID='{0}' AND sample.SUBTASK_ID=subtask.ID";
            strSQL = string.Format(strSQL, strSampleId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取无现场监测项目的子任务
        /// 创建时间：2013-5-11
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">任务ID</param>
        /// <param name="strMonitorID">类别ID</param>
        /// <returns></returns>
        public TMisMonitorSubtaskVo GetNoSampleSubTaskInfo(string strTaskID, string strMonitorID)
        {
            string strSQL = @"SELECT * FROM T_MIS_MONITOR_SUBTASK WHERE 1=1 AND REMARK1 IS NULL";
            if (!string.IsNullOrEmpty(strTaskID))
            {
                strSQL += string.Format(" AND TASK_ID='{0}'", strTaskID);
            }
            if (!string.IsNullOrEmpty(strMonitorID))
            {
                strSQL += string.Format(" AND MONITOR_ID='{0}'", strMonitorID);
            }
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskVo(), strSQL);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目的子任务
        /// 创建时间：2013-5-11
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">任务ID</param>
        /// <param name="strMonitorID">类别ID</param>
        /// <returns></returns>
        public TMisMonitorSubtaskVo GetSampleSubTaskInfo(string strTaskID, string strMonitorID)
        {
            string strSQL = @"SELECT subtask1.* FROM T_MIS_MONITOR_SUBTASK subtask1
                                                INNER JOIN T_MIS_MONITOR_SUBTASK subtask2 ON subtask1.REMARK1=subtask2.ID
                                                WHERE 1=1 ";
            if (!string.IsNullOrEmpty(strTaskID))
            {
                strSQL += string.Format(" AND subtask1.TASK_ID='{0}'", strTaskID);
            }
            if (!string.IsNullOrEmpty(strMonitorID))
            {
                strSQL += string.Format(" AND subtask1.MONITOR_ID='{0}'", strMonitorID);
            }
            return SqlHelper.ExecuteObject(new TMisMonitorSubtaskVo(), strSQL);
        }

        /// <summary>
        /// 创建原因：环境质量类别分析完成后自动导入对应类别的环境数据填报表中
        /// 创建时间：2013-06-21
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="tMisMonitorSubtask">监测子任务信息 子任务监测类别、子任务ID必传</param>
        /// <param name="blFlag">是否为噪声填报 true 是 false 否</param>
        /// <param name="strBC">是否补测：'true'-是 'false'-否</param>
        /// <returns></returns>
        public bool SetEnvFillData(TMisMonitorSubtaskVo tMisMonitorSubtask,bool blFlag, string strBC)
        {
            string strSQL = "";
            DataTable objDt = new DataTable();
            ArrayList objArry = new ArrayList();
            if (!String.IsNullOrEmpty(tMisMonitorSubtask.MONITOR_ID))
            {
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.ID))
                {
                    //根据子任务ID获取采样信息
                    strSQL = String.Format(@" SELECT ID,SUBTASK_ID,POINT_ID,SAMPLE_CODE,SAMPLE_TYPE,SAMPLE_NAME,STATUS,ENV_MONTH,ENV_DAY,ENV_HOUR,ENV_MINUTE FROM T_MIS_MONITOR_SAMPLE_INFO WHERE SUBTASK_ID='{0}'", tMisMonitorSubtask.ID);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    if (objDt.Rows.Count > 0)
                    {
                        foreach (DataRow objDr in objDt.Rows)
                        {
                            //循环获取每个子任务对应的监测点位
                            strSQL = String.Format(@" SELECT ID,MONITOR_ID,POINT_ID,POINT_NAME,DYNAMIC_ATTRIBUTE_ID,FREQ,NATIONAL_ST_CONDITION_ID,INDUSTRY_ST_CONDITION_ID,LOCAL_ST_CONDITION_ID,SAMPLE_FREQ FROM T_MIS_MONITOR_TASK_POINT WHERE IS_DEL='0'");
                            strSQL += String.Format(@" AND ID='{0}'", objDr["POINT_ID"].ToString());
                            DataTable objPointDt = new DataTable();
                            objPointDt = SqlHelper.ExecuteDataTable(strSQL);
                            if (objPointDt.Rows.Count > 0) {
                                DataTable objEnvPointDt = new DataTable();
                                //根据获取的监测点位判断所属环境监测类别，并返回指定的环境监测点位的数据集合
                                objEnvPointDt = ReturnEnvTypeSql(tMisMonitorSubtask.MONITOR_ID, objPointDt.Rows[0]["POINT_ID"].ToString());
                                if (objEnvPointDt.Rows.Count>0) {
                                    //清除已存在的填报数据 Add By:weilin
                                    //strSQL = "DELETE FillItem FROM {0} Fill left join {1} FillItem on(Fill.ID=FillItem.FILL_ID) WHERE POINT_ID='{2}' and YEAR='{3}' and MONTH='{4}'";
                                    //strSQL = string.Format(strSQL, objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["FILLITEMTABLE"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString());
                                    //objArry.Add(strSQL);
                                    //strSQL = "DELETE Fill FROM {0} Fill left join {1} FillItem on(Fill.ID=FillItem.FILL_ID) WHERE POINT_ID='{2}' and YEAR='{3}' and MONTH='{4}'";
                                    //strSQL = string.Format(strSQL, objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["FILLITEMTABLE"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString());
                                    //objArry.Add(strSQL);

                                    string strDay = "";
                                    if (tMisMonitorSubtask.SAMPLE_ASK_DATE != "")
                                    {
                                        strDay = DateTime.Parse(tMisMonitorSubtask.SAMPLE_ASK_DATE).Day.ToString();
                                    }
                                    //根据年、月、日返回星期几
                                    string strWeek = DateTimeOfWeek(objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objDr["ENV_DAY"].ToString());

                                    string strFillTableId = "";
                                    if (strBC != "true")
                                    {
                                        if (tMisMonitorSubtask.MONITOR_ID == "FunctionNoise")
                                        {
                                            strSQL = "SELECT * FROM {0} WHERE POINT_ID='{1}' and YEAR='{2}' and MONTH='{3}' and BEGIN_HOUR='{4}'";
                                            strSQL = string.Format(strSQL, objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objDr["ENV_HOUR"].ToString());
                                        }
                                        else
                                        {
                                            strSQL = "SELECT * FROM {0} WHERE POINT_ID='{1}' and YEAR='{2}' and MONTH='{3}'";
                                            strSQL = string.Format(strSQL, objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString());
                                        }
                                        DataTable dtFillPoint = SqlHelper.ExecuteDataTable(strSQL);
                                        if (dtFillPoint.Rows.Count > 0)
                                        {
                                            strFillTableId = dtFillPoint.Rows[0]["ID"].ToString();
                                            if (tMisMonitorSubtask.MONITOR_ID == "FunctionNoise")
                                                strSQL = String.Format(@" UPDATE {0} SET {1}='{2}',{4}='{5}',{6}='{7}',{8}='{9}' WHERE ID='{3}'", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString());
                                            else if (tMisMonitorSubtask.MONITOR_ID == "AreaNoise" || tMisMonitorSubtask.MONITOR_ID == "EnvRoadNoise")
                                                strSQL = String.Format(@" UPDATE {0} SET {1}='{2}',{4}='{5}',{6}='{7}',{8}='{9}',{10}='{11}' WHERE ID='{3}'", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString(), objEnvPointDt.Rows[0]["WEEK_FILE"].ToString(), strWeek);
                                            else 
                                                strSQL = String.Format(@" UPDATE {0} SET {1}='{2}' WHERE ID='{3}'", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), strDay, strFillTableId);
                                            
                                            objArry.Add(strSQL);
                                        }
                                        else
                                        {
                                            //如果包含SECTION_ID列，则表示包含断面信息的点位
                                            strFillTableId = GetSerialNumber(objEnvPointDt.Rows[0]["FILLSEARIL"].ToString());
                                            if (objEnvPointDt.Columns.Contains("SECTION_ID"))
                                            {
                                                strSQL = String.Format(@" INSERT INTO {0} (ID,SECTION_ID,POINT_ID,{7},YEAR,MONTH) VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["SECTION_ID"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString());
                                                objArry.Add(strSQL);
                                            }
                                            else
                                            {
                                                //如果为噪声 则插入的是测量时间
                                                if (blFlag)
                                                {
                                                    if (tMisMonitorSubtask.MONITOR_ID == "FunctionNoise")
                                                        strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{3},{5},{7},{9},YEAR,MONTH) VALUES('{1}','{2}','{4}','{6}','{8}','{10}','{11}','{12}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString());
                                                    else if (tMisMonitorSubtask.MONITOR_ID == "AreaNoise" || tMisMonitorSubtask.MONITOR_ID == "EnvRoadNoise")
                                                        strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{3},{5},{7},{9},YEAR,MONTH,{13}) VALUES('{1}','{2}','{4}','{6}','{8}','{10}','{11}','{12}','{14}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["WEEK_FILE"].ToString(), strWeek);
                                                    else
                                                        strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{6},YEAR,MONTH) VALUES('{1}','{2}','{3}','{4}','{5}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString());
                                                }
                                                else
                                                {
                                                    //非噪声类插入的是采样时间
                                                    strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{6},YEAR,MONTH) VALUES('{1}','{2}','{3}','{4}','{5}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString());
                                                }
                                                objArry.Add(strSQL);
                                            }
                                        }
                                    }
                                    else 
                                    {
                                        //如果包含SECTION_ID列，则表示包含断面信息的点位
                                        strFillTableId = GetSerialNumber(objEnvPointDt.Rows[0]["FILLSEARIL"].ToString());
                                        if (objEnvPointDt.Columns.Contains("SECTION_ID"))
                                        {
                                            strSQL = String.Format(@" INSERT INTO {0} (ID,SECTION_ID,POINT_ID,{7},YEAR,MONTH,REMARK1) VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{8}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["SECTION_ID"].ToString(), objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), strBC);
                                            objArry.Add(strSQL);
                                        }
                                        else
                                        {
                                            //如果为噪声 则插入的是测量时间
                                            if (blFlag)
                                            {
                                                if (tMisMonitorSubtask.MONITOR_ID == "FunctionNoise")
                                                    strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{3},{5},{7},{9},YEAR,MONTH,REMARK1) VALUES('{1}','{2}','{4}','{6}','{8}','{10}','{11}','{12}','{13}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), strBC);
                                                else if (tMisMonitorSubtask.MONITOR_ID == "AreaNoise" || tMisMonitorSubtask.MONITOR_ID == "EnvRoadNoise")
                                                    strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{3},{5},{7},{9},{14},YEAR,MONTH,REMARK1) VALUES('{1}','{2}','{4}','{6}','{8}','{10}','{11}','{12}','{13}','{15}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), objEnvPointDt.Rows[0]["MONTH_FILE"].ToString(), objDr["ENV_MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), objDr["ENV_DAY"].ToString(), objEnvPointDt.Rows[0]["HOUR_FILE"].ToString(), objDr["ENV_HOUR"].ToString(), objEnvPointDt.Rows[0]["MINUTE_FILE"].ToString(), objDr["ENV_MINUTE"].ToString(), objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), strBC, objEnvPointDt.Rows[0]["WEEK_FILE"].ToString(), strWeek);
                                                else
                                                    strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{6},YEAR,MONTH,REMARK1) VALUES('{1}','{2}','{3}','{4}','{5}','{7}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), strBC);
                                            }
                                            else
                                            {
                                                //非噪声类插入的是采样时间
                                                strSQL = String.Format(@" INSERT INTO {0} (ID,POINT_ID,{6},YEAR,MONTH,REMARK1) VALUES('{1}','{2}','{3}','{4}','{5}','{7}')", objEnvPointDt.Rows[0]["FILLTABLE"].ToString(), strFillTableId, objEnvPointDt.Rows[0]["ID"].ToString(), strDay, objEnvPointDt.Rows[0]["YEAR"].ToString(), objEnvPointDt.Rows[0]["MONTH"].ToString(), objEnvPointDt.Rows[0]["DAY_FILE"].ToString(), strBC);
                                            }
                                            objArry.Add(strSQL);
                                        }
                                    }
                                    strSQL = String.Format(@" SELECT ID,SAMPLE_ID,ITEM_ID,ITEM_RESULT,ANALYSIS_METHOD_ID,STANDARD_ID FROM T_MIS_MONITOR_RESULT WHERE SAMPLE_ID='{0}'",objDr["ID"].ToString());
                                    DataTable objResultDt = new DataTable();
                                    objResultDt = SqlHelper.ExecuteDataTable(strSQL);
                                    if (objResultDt.Rows.Count > 0) {
                                        foreach (DataRow objItemDr in objResultDt.Rows)
                                        {
                                            //如果非补测的任务删除监测项目后再添加（替换）
                                            if (strBC != "true")
                                            {
                                                strSQL = String.Format(@" DELETE FROM {0} WHERE FILL_ID='{1}' AND ITEM_ID='{2}'", objEnvPointDt.Rows[0]["FILLITEMTABLE"].ToString(), strFillTableId, objItemDr["ITEM_ID"].ToString());
                                                objArry.Add(strSQL);
                                            }

                                            strSQL = String.Format(@" INSERT INTO {0} (ID,FILL_ID,ITEM_ID,ITEM_VALUE,REMARK1) VALUES('{1}','{2}','{3}','{4}','{5}')", objEnvPointDt.Rows[0]["FILLITEMTABLE"].ToString(), GetSerialNumber(objEnvPointDt.Rows[0]["FILLITEMSEARIL"].ToString()), strFillTableId, objItemDr["ITEM_ID"].ToString(), objItemDr["ITEM_RESULT"].ToString(), strBC == "true" ? strBC : "");
                                            objArry.Add(strSQL);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(objArry) ;
        }

        /// <summary>
        /// 根据年、月、日返回星期几
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <returns></returns>
        public string DateTimeOfWeek(string strYear, string strMonth, string strDay)
        {
            string strWeek = "";
            if (strYear != "" && strMonth != "" && strDay != "")
            {
                strWeek = new DateTime(int.Parse(strYear), int.Parse(strMonth), int.Parse(strDay)).DayOfWeek.ToString();
                switch (strWeek)
                {
                    case "Monday":
                        strWeek = "1";
                        break;
                    case "Tuesday":
                        strWeek = "2";
                        break;
                    case "Wednesday":
                        strWeek = "3";
                        break;
                    case "Thursday":
                        strWeek = "4";
                        break;
                    case "Friday":
                        strWeek = "5";
                        break;
                    case "Saturday":
                        strWeek = "6";
                        break;
                    case "Sunday":
                        strWeek = "7";
                        break;
                    default:
                        strWeek = "week";
                        break;
                }
            }
            return strWeek;
        }

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSubtask)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSubtask.ID.ToString()));
                }
                //监测计划ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorSubtask.TASK_ID.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tMisMonitorSubtask.MONITOR_ID.ToString()));
                }
                //采样要求时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLE_ASK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ASK_DATE = '{0}'", tMisMonitorSubtask.SAMPLE_ASK_DATE.ToString()));
                }
                //分析完成时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLE_FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FINISH_DATE = '{0}'", tMisMonitorSubtask.SAMPLE_FINISH_DATE.ToString()));
                }
                //采样方式
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLING_METHOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_METHOD = '{0}'", tMisMonitorSubtask.SAMPLING_METHOD.ToString()));
                }
                //采样负责人
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLING_MANAGER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_MANAGER_ID = '{0}'", tMisMonitorSubtask.SAMPLING_MANAGER_ID.ToString()));
                }
                //采样协同人ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLING_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_ID = '{0}'", tMisMonitorSubtask.SAMPLING_ID.ToString()));
                }
                //采样人
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLING_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_MAN = '{0}'", tMisMonitorSubtask.SAMPLING_MAN.ToString()));
                }
                //样品接收时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLE_ACCESS_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ACCESS_DATE = '{0}'", tMisMonitorSubtask.SAMPLE_ACCESS_DATE.ToString()));
                }
                //样品接收人ID
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLE_ACCESS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ACCESS_ID = '{0}'", tMisMonitorSubtask.SAMPLE_ACCESS_ID.ToString()));
                }
                //接样意见
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.SAMPLE_APPROVE_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_APPROVE_INFO = '{0}'", tMisMonitorSubtask.SAMPLE_APPROVE_INFO.ToString()));
                }
                //分析完成时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.ANALYSE_FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSE_FINISH_DATE = '{0}'", tMisMonitorSubtask.ANALYSE_FINISH_DATE.ToString()));
                }
                //监测结论
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.PROJECT_CONCLUSION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_CONCLUSION = '{0}'", tMisMonitorSubtask.PROJECT_CONCLUSION.ToString()));
                }
                //项目完成时间
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.PROJECT_FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_FINISH_DATE = '{0}'", tMisMonitorSubtask.PROJECT_FINISH_DATE.ToString()));
                }
                //任务状态类别(发送，退回)
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.TASK_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_TYPE = '{0}'", tMisMonitorSubtask.TASK_TYPE.ToString()));
                }
                //任务状态
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.TASK_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_STATUS = '{0}'", tMisMonitorSubtask.TASK_STATUS.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSubtask.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSubtask.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSubtask.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSubtask.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSubtask.REMARK5.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisMonitorSubtask.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisMonitorSubtask.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisMonitorSubtask.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisMonitorSubtask.CCFLOW_ID3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        #region 判断所属环境质量监测类 胡方扬 2013-06-21
        /// <summary>
        /// 创建原因：根据传入的环境质量类别和监测任务点位原始点位ID获取当前环境质量基础资料点位的信息
        /// 创建时间：2013-06-21
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="strEnvTypeCode">环境质量类别</param>
        /// <param name="strPointId">监测任务点位原始ID</param>
        /// <returns>返回DataTable数据集</returns>
        public DataTable ReturnEnvTypeSql(string strEnvTypeCode,string strPointId) 
        {
            string strSQL = "";
            DataTable objDt = new DataTable();
            switch (strEnvTypeCode)
            {
                /*==========包含断面的 Start==========*/
                case "EnvRiver"://河流
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_RIVER' AS FILLTABLE,'T_ENV_FILL_RIVER_ITEM' AS FILLITEMTABLE,'river_fill_id' AS FILLSEARIL,'river_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_RIVER_V A
                                LEFT JOIN   T_ENV_P_RIVER B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvRiverCity"://城考
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_RIVER_CITY' AS FILLTABLE,'T_ENV_FILL_RIVER_CITY_ITEM' AS FILLITEMTABLE,'river_city_fill_id' AS FILLSEARIL,'river_city_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_RIVER_CITY_V A
                                LEFT JOIN   T_ENV_P_RIVER_CITY B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvRiverTarget"://责任目标
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_RIVER_TARGET' AS FILLTABLE,'T_ENV_FILL_RIVER_ITEM_TARGET' AS FILLITEMTABLE,'river_target_fill_id' AS FILLSEARIL,'river_target_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_RIVER_TARGET_V A
                                LEFT JOIN   T_ENV_P_RIVER_TARGET B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvRiverPlan"://规划断面
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_RIVER_PLAN' AS FILLTABLE,'T_ENV_FILL_RIVER_PLAN_ITEM' AS FILLITEMTABLE,'river_plan_fill_id' AS FILLSEARIL,'river_plan_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_RIVER_PLAN_V A
                                LEFT JOIN   T_ENV_P_RIVER_PLAN B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvReservoir"://湖库
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_LAKE' AS FILLTABLE,'T_ENV_FILL_LAKE_ITEM' AS FILLITEMTABLE,'lake_fill_id' AS FILLSEARIL,'lake_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_LAKE_V A
                                LEFT JOIN   T_ENV_FILL_LAKE B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvDrinkingSource"://饮用水源地（河流、湖库）
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_DRINK_SRC' AS FILLTABLE,'T_ENV_FILL_DRINK_SRC_ITEM' AS FILLITEMTABLE,'drink_src_fill_id' AS FILLSEARIL,'drink_src_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE FROM T_ENV_P_DRINK_SRC_V A
                                INNER JOIN T_ENV_FILL_DRINK_SRC B ON B.SECTION_ID=A.SECTION_ID
                                WHERE A.ID='" + strPointId + "' AND B.POINT_ID='" + strPointId + "' ");
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvMudRiver"://沉积物（河流）
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_MUD_RIVER' AS FILLTABLE,'T_ENV_FILL_MUD_RIVER_ITEM' AS FILLITEMTABLE,'mud_river_fill_id' AS FILLSEARIL,'mud_river_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_MUD_RIVER_V A
                                LEFT JOIN T_ENV_FILL_MUD_RIVER B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvMudSea"://沉积物（海水）
                    strSQL = String.Format(@" SELECT A.ID,A.SECTION_ID,B.YEAR,B.MONTH,'T_ENV_FILL_MUD_SEA' AS FILLTABLE,'T_ENV_FILL_MUD_SEA_ITEM' AS FILLITEMTABLE,'mud_sea_fill_id' AS FILLSEARIL,'mud_sea_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_MUD_SEA_V A
                                LEFT JOIN T_ENV_FILL_MUD_SEA B ON B.ID=A.SECTION_ID
                                WHERE A.ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvDWWater": //双三十水 不做
                    break;
                /*==========包含断面的 End==========*/
                /*==========不包含断面的 Star=======*/
                case "EnvStbc": //生态补偿
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_PAYFOR' AS FILLTABLE,'T_ENV_FILL_PAYFOR_ITEM' AS FILLITEMTABLE,'payfor_fill_id' AS FILLSEARIL,'payfor_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_PAYFOR WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvDrinking": //地下饮用水 
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_DRINK_UNDER' AS FILLTABLE,'T_ENV_FILL_DRINK_UNDER_ITEM' AS FILLITEMTABLE,'drink_under_fill_id' AS FILLSEARIL,'drink_under_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_DRINK_UNDER WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvSoil": //土壤
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_SOIL' AS FILLTABLE,'T_ENV_FILL_SOIL_ITEM' AS FILLITEMTABLE,'soil_fill_id' AS FILLSEARIL,'soil_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_SOIL WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvPSoild": //固废
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_SOLID' AS FILLTABLE,'T_ENV_FILL_SOLID_ITEM' AS FILLITEMTABLE,'soild_fill_id' AS FILLSEARIL,'soild_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_SOLID WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvEstuaries": //入海河口
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_ESTUARIES' AS FILLTABLE,'T_ENV_FILL_ESTUARIES_ITEM' AS FILLITEMTABLE,'estuaries_fill_id' AS FILLSEARIL,'estuaries_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_ESTUARIES WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvSear": //近岸海域
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_SEA' AS FILLTABLE,'T_ENV_FILL_SEA_ITEM' AS FILLITEMTABLE,'sea_fill_id' AS FILLSEARIL,'sea_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_SEA WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvSource": //近岸直排 
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_OFFSHORE' AS FILLTABLE,'T_ENV_FILL_OFFSHORE_ITEM' AS FILLITEMTABLE,'offshore_fill_id' AS FILLSEARIL,'offshore_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_OFFSHORE WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvSeaBath": //海水浴场
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_SEABATH' AS FILLTABLE,'T_ENV_FILL_SEABATH' AS FILLITEMTABLE,'seabath_fill_id' AS FILLSEARIL,'seabath_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_SEABATH WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvDust": //降尘
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_DUST' AS FILLTABLE,'T_ENV_FILL_DUST_ITEM' AS FILLITEMTABLE,'dust_fill_id' AS FILLSEARIL,'dust_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_DAY' as DAY_FILE  FROM T_ENV_P_DUST WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvRain": //降水
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_RAIN' AS FILLTABLE,'T_ENV_FILL_RAIN_ITEM' AS FILLITEMTABLE,'rain_fill_id' AS FILLSEARIL,'rain_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_DAY' as DAY_FILE  FROM T_ENV_P_RAIN WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                /*==========不包含断面的 End=========*/
                /*===========噪声 Start==============*/
                case "EnvRoadNoise": //道路交通噪声
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_NOISE_ROAD' AS FILLTABLE,'T_ENV_FILL_NOISE_ROAD_ITEM' AS FILLITEMTABLE,'noise_road_fill_id' AS FILLSEARIL,'noise_road_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_MONTH' as MONTH_FILE, 'BEGIN_DAY' as DAY_FILE, 'BEGIN_HOUR' as HOUR_FILE, 'BEGIN_MINUTE' as MINUTE_FILE, 'WEEK' as WEEK_FILE FROM T_ENV_P_NOISE_ROAD WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "FunctionNoise": //功能区噪声
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_NOISE_FUNCTION' AS FILLTABLE,'T_ENV_FILL_NOISE_FUNCTION_ITEM' AS FILLITEMTABLE,'noise_function_fill_id' AS FILLSEARIL,'noise_function_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_MONTH' as MONTH_FILE, 'BEGIN_DAY' as DAY_FILE, 'BEGIN_HOUR' as HOUR_FILE, 'BEGIN_MINUTE' as MINUTE_FILE  FROM T_ENV_P_NOISE_FUNCTION WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "AreaNoise": //区域环境噪声
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_NOISE_AREA' AS FILLTABLE,'T_ENV_FILL_NOISE_AREA_ITEM' AS FILLITEMTABLE,'noise_area_fill_id' AS FILLSEARIL,'noise_area_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_MONTH' as MONTH_FILE, 'BEGIN_DAY' as DAY_FILE, 'BEGIN_HOUR' as HOUR_FILE, 'BEGIN_MINUTE' as MINUTE_FILE, 'WEEK' as WEEK_FILE FROM T_ENV_P_NOISE_AREA WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                /*=============噪声 End============*/
                /*==========环境空气 Start=========*/
                case "EnvAir": //环境空气
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_AIR' AS FILLTABLE,'T_ENV_FILL_AIR_ITEM' AS FILLITEMTABLE,'air_fill_id' AS FILLSEARIL,'air_fill_item_id'AS FILLITEMSEARIL, 'DAY' as DAY_FILE  FROM T_ENV_P_AIR WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvSpeed": //硫酸盐化速率
                    strSQL = String.Format(@" SELECT ID,YEAR,MONTH,'T_ENV_FILL_ALKALI' AS FILLTABLE,'T_ENV_FILL_ALKALI_ITEM' AS FILLITEMTABLE,'aikalt_fill_id' AS FILLSEARIL,'aikalt_fill_item_id'AS FILLITEMSEARIL, 'BEGIN_DAY' as DAY_FILE  FROM T_ENV_P_ALKALI WHERE ID='{0}' ", strPointId);
                    objDt = SqlHelper.ExecuteDataTable(strSQL);
                    break;
                case "EnvDWAir": //双三十废气 不做
                    break;
                /*=========环境空气 End==========*/
                default:
                    break;
            }
            return objDt;
        }

        /// <summary>
        /// 创建原因：根据子任务ID获取当前子任务所属企业的噪声点位图
        /// 创建人：胡方扬
        /// 创建日期：2013-07-03
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public DataTable GetPointMapForSubTask(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = String.Format(@"SELECT D.*,A.ID AS TASK_ID,C.COMPANY_ID FROM dbo.T_MIS_MONITOR_TASK  A
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY B ON B.ID=A.TESTED_COMPANY_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=B.COMPANY_ID
LEFT JOIN dbo.T_OA_ATT D ON D.BUSINESS_ID=C.COMPANY_ID AND D.BUSINESS_TYPE='PointMap'
WHERE A.ID='{0}'",tMisMonitorSubtask.TASK_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// 创建原因：根据子任务ID获取当前子任务所属企业基础资料ID
        /// 创建人：胡方扬
        /// 创建日期：2013-07-03
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public DataTable GetCompanyIDForSubTask(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            string strSQL = String.Format(@"SELECT A.ID AS TASK_ID,C.COMPANY_ID FROM dbo.T_MIS_MONITOR_TASK  A
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY B ON B.ID=A.TESTED_COMPANY_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=B.COMPANY_ID
WHERE A.ID='{0}'", tMisMonitorSubtask.TASK_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region//采样及时率统计查询行数
        public int GetResultCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  count(*) ");
            sb.Append(" from T_MIS_MONITOR_SUBTASK a   left join T_MIS_MONITOR_TASK b on a.TASK_ID=b.id ");
            sb.Append(" left join T_SYS_USER c on a.SAMPLING_MANAGER_ID=c.id");
            sb.Append(" left join T_BASE_MONITOR_TYPE_INFO d on a.MONITOR_ID=d.id  where 1=1 ");
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120) between '" + StartTime + "' and '" + EndTime + "'");
            }
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            { 
                sb.Append(" and c.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and b.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(OverTime))////是否超期完成{0：全部；1：是(实际完成时间超过要求完成时间)；2：否（实际完成时间没有超过要求完成时间）}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120) >CONVERT(varchar(11),a.SAMPLE_ASK_DATE,111)  ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120) <CONVERT(varchar(11),a.SAMPLE_ASK_DATE,111)  ");
                }
            }
            return int.Parse(SqlHelper.ExecuteScalar(sb.ToString()).ToString());
        }
        #endregion

        #region//采样及时率统计查询
        public DataTable SearchDataEx(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  a.ID,b.TICKET_NUM,d.MONITOR_TYPE_NAME,c.REAL_NAME,CONVERT(varchar(11),a.SAMPLE_ASK_DATE,120)  as SAMPLE_ASK_DATE, CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120)  as SAMPLE_FINISH_DATE,  ");
            sb.Append(" (case  when CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120) >CONVERT(varchar(11),a.SAMPLE_ASK_DATE,120) then '是' when CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120) <CONVERT(varchar(11),a.SAMPLE_ASK_DATE,120) then '否'  end) as Is_OverTime "); 
            sb.Append(" from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_TASK b on a.TASK_ID=b.ID  ");//监测子任务,监测任务
            sb.Append("  left join T_SYS_USER c on a.SAMPLING_MANAGER_ID=c.id");//用户表
            sb.Append("  left join T_BASE_MONITOR_TYPE_INFO d on a.MONITOR_ID=d.id  where 1=1 ");//监测类别
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,120)  between '" + StartTime + "' and '" + EndTime + "'");
            }
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            {
                sb.Append(" and c.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and b.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(OverTime))////是否超期完成{0：全部；1：是(实际完成时间超过要求完成时间)；2：否（实际完成时间没有超过要求完成时间）}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,111) >CONVERT(varchar(11),a.SAMPLE_ASK_DATE,120)  ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.SAMPLE_FINISH_DATE,111) <CONVERT(varchar(11),a.SAMPLE_ASK_DATE,120)  ");
                }
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }
        #endregion

        /// <summary>
        /// 获取对象DataTable 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSummary(TMisMonitorSubtaskVo objSubTask, bool isLocal)
        {

            string strSQL = "select * from T_MIS_MONITOR_SUBTASK " + this.BuildWhereStatement(objSubTask);
            string strIS_SAMPLEDEPT = "否";
            string strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where remark_4='1' or  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            if (isLocal)
            {
                strIS_SAMPLEDEPT = "是";
                strSubSql = "select SAMPLE_ID from T_MIS_MONITOR_RESULT where (remark_4<>'1' or remark_4 is null) and  ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            }
            strSQL += @" and  ID in (select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where
                    ID in (" + strSubSql + "))";
//            strSQL += @" and  ID in (select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where
//                    ID in (select SAMPLE_ID from T_MIS_MONITOR_RESULT where
//                        ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')))";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 当包含分析类现场监测项目时现场结果复核的退回事件 Create By weilin 2013-11-11
        /// </summary>
        /// <param name="strSubTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">子任务的退回状态</param>
        /// <param name="strResultStatus">监测结果的退回状态</param>
        /// <param name="strSubTaskType"></param>
        /// <returns></returns>
        public bool SampleResultCheckBackTo(string strTaskId, string strSubTaskID, string strSubTaskStatus, string strResultStatus, string strSubTaskType)
        {
            string strSQL = "";
            strSQL = @"update a set TASK_STATUS='{1}',TASK_TYPE='{3}' from T_MIS_MONITOR_SUBTASK a
                        inner join T_MIS_MONITOR_SAMPLE_INFO b on(a.REMARK1=b.SUBTASK_ID)
                        inner join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                        inner join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                        where a.TASK_ID='{0}' and a.ID='{4}' and d.IS_SAMPLEDEPT='是'";
            strSQL += @"update c set RESULT_STATUS='{2}',TASK_TYPE='{3}' from T_MIS_MONITOR_SUBTASK a
                        inner join T_MIS_MONITOR_SAMPLE_INFO b on(a.REMARK1=b.SUBTASK_ID)
                        inner join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                        inner join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                        where a.TASK_ID='{0}' and a.ID='{4}' and d.IS_ANYSCENE_ITEM='1' and c.RESULT_STATUS='02'";
            strSQL = string.Format(strSQL, strTaskId, strSubTaskStatus, strResultStatus, strSubTaskType, strSubTaskID);

            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 现场主任审核退回到现场结果复核环节 Create By weilin 2013-11-12
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strCurrentStatus"></param>
        /// <param name="strBackStatus"></param>
        /// <param name="strSubTaskType"></param>
        /// <returns></returns>
        public bool SampleResultQcCheckBackTo(string strTaskId, string strCurrentStatus, string strBackStatus, string strSubTaskType)
        {
            string strSQL = "";
            strSQL = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{2}',TASK_TYPE='{3}'
                     where TASK_ID='{0}' and TASK_STATUS='{1}'";
            strSQL = string.Format(strSQL, strTaskId, strCurrentStatus, strBackStatus, strSubTaskType);

            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }
        #region//采样质控
        public int Get_TaskDoPlanList_QCStep_Count(TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = String.Format(@"SELECT count(*)
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1 AND A.PLAN_YEAR IS NOT NULL  AND A.PLAN_MONTH IS NOT NULL  AND A.PLAN_DAY IS NOT NULL AND A.HAS_DONE IS NULL AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE IN ('0','1'))  ");
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion

        /// <summary>
        /// 获取子任务所有测点的监测项目信息 Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable getItemBySubTaskID(string strSubTaskID)
        {
            string strSQL = "";

            strSQL = @"select distinct d.ID,d.ITEM_NAME from T_MIS_MONITOR_SUBTASK a 
                     inner join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID) 
                     inner join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID) 
                     inner join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                     where a.ID='{0}'";
            strSQL = string.Format(strSQL, strSubTaskID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取子任务所有测点的监测项目结果值信息 Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <returns></returns>
        public DataTable getItemValueBySampleID(string strSampleID)
        {
            string strSQL = "";

            strSQL = @"select c.ID,c.ITEM_NAME,b.ITEM_RESULT
                        from T_MIS_MONITOR_SAMPLE_INFO a 
                        inner join T_MIS_MONITOR_RESULT b on(a.ID=b.SAMPLE_ID)
                        inner join T_BASE_ITEM_INFO c on(b.ITEM_ID=c.ID)
                        where a.ID='{0}'";
            strSQL = string.Format(strSQL, strSampleID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取现场监测结果复核数据列表 Create By weilin 2014-03-27
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strSubTaskStatus"></param>
        /// <param name="b">true: 指定用户权限，false: 不用指定用户权限</param>
        /// <returns></returns>
        public DataTable SelectSamplingCheckList(string strUserID, string strSubTaskStatus, int intPageIndex, int intPageSize, bool b)
        {
            string strSQL = @"select distinct a.SUBTASKID ID,a.ID TASKID,a.TICKET_NUM,a.CONTRACT_CODE,a.PROJECT_NAME,a.MONITOR_ID,a.TASK_TYPE from (
                            select task.ID,task.TICKET_NUM,task.CONTRACT_CODE,task.PROJECT_NAME,c.ID SAMPLEID,subtask.ID SUBTASKID,subtask_app.SAMPLING_CHECK,subtask_app.SAMPLING_QC_CHECK,subtask.TASK_STATUS,subtask.MONITOR_ID,subtask.TASK_TYPE
                            FROM T_MIS_MONITOR_TASK task
                            INNER JOIN 
                            T_MIS_MONITOR_SUBTASK subtask on subtask.TASK_ID = task.ID
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                            inner join T_MIS_MONITOR_SUBTASK_APP subtask_app on(subtask.ID=subtask_app.SUBTASK_ID)
                            union all
                            select task.ID,task.TICKET_NUM,task.CONTRACT_CODE,task.PROJECT_NAME,c.ID SAMPLEID,subtask.ID SUBTASKID,subtask_app.SAMPLING_CHECK,subtask_app.SAMPLING_QC_CHECK,subtask.TASK_STATUS,subtask.MONITOR_ID,subtask.TASK_TYPE
                            FROM T_MIS_MONITOR_TASK task
                            INNER JOIN 
                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID = task.ID
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                            inner join T_MIS_MONITOR_SUBTASK_APP subtask_app on(subtask.ID=subtask_app.SUBTASK_ID)
                            ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                            inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                            where a.TASK_STATUS='{0}' and b.RESULT_STATUS in('01','02') AND (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1')";
            if (b)
            {
                strSQL += " and a.SAMPLING_CHECK='{1}'";
                strSQL = string.Format(strSQL, strSubTaskStatus, strUserID);
            }
            else
            {
                strSQL += " and a.SAMPLING_QC_CHECK='{1}'";
                strSQL = string.Format(strSQL, strSubTaskStatus, strUserID);
            }
            
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        public int SelectSamplingCheckListCount(string strUserID, string strSubTaskStatus, bool b)
        {
            string strSQL = @"select COUNT(distinct a.SUBTASKID) from (
                            select task.ID,task.TICKET_NUM,task.CONTRACT_CODE,task.PROJECT_NAME,c.ID SAMPLEID,subtask.ID SUBTASKID,subtask_app.SAMPLING_CHECK,subtask_app.SAMPLING_QC_CHECK,subtask.TASK_STATUS,subtask.MONITOR_ID
                            FROM T_MIS_MONITOR_TASK task
                            INNER JOIN 
                            T_MIS_MONITOR_SUBTASK subtask on subtask.TASK_ID = task.ID
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.ID)
                            inner join T_MIS_MONITOR_SUBTASK_APP subtask_app on(subtask.ID=subtask_app.SUBTASK_ID)
                            union all
                            select task.ID,task.TICKET_NUM,task.CONTRACT_CODE,task.PROJECT_NAME,c.ID SAMPLEID,subtask.ID SUBTASKID,subtask_app.SAMPLING_CHECK,subtask_app.SAMPLING_QC_CHECK,subtask.TASK_STATUS,subtask.MONITOR_ID
                            FROM T_MIS_MONITOR_TASK task
                            INNER JOIN 
                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID = task.ID
                            inner join T_MIS_MONITOR_SAMPLE_INFO c on(c.SUBTASK_ID=subtask.REMARK1)
                            inner join T_MIS_MONITOR_SUBTASK_APP subtask_app on(subtask.ID=subtask_app.SUBTASK_ID)
                            ) a inner join T_MIS_MONITOR_RESULT b on(b.SAMPLE_ID=a.SAMPLEID)
                            inner join T_BASE_ITEM_INFO e on(b.ITEM_ID=e.ID)
                            where a.TASK_STATUS='{0}' and b.RESULT_STATUS in('01','02') AND (e.IS_SAMPLEDEPT='是' or e.IS_ANYSCENE_ITEM='1')";

            if (b)
            {
                strSQL += " and a.SAMPLING_CHECK='{1}'";
                strSQL = string.Format(strSQL, strSubTaskStatus, strUserID);
            }
            else
            {
                strSQL += " and a.SAMPLING_QC_CHECK='{1}'";
                strSQL = string.Format(strSQL, strSubTaskStatus, strUserID);
            }

            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }
    }
}
