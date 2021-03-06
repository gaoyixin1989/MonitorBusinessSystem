using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.DataAccess.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Report;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskLogic : LogicBase
    {

        TMisMonitorTaskVo tMisMonitorTask = new TMisMonitorTaskVo();
        TMisMonitorTaskAccess access;

        public TMisMonitorTaskLogic()
        {
            access = new TMisMonitorTaskAccess();
        }

        public TMisMonitorTaskLogic(TMisMonitorTaskVo _tMisMonitorTask)
        {
            tMisMonitorTask = _tMisMonitorTask;
            access = new TMisMonitorTaskAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.GetSelectResultCount(tMisMonitorTask);
        }
        public int GetSelectResultCountByTicketNum(string strTicketNum)
        {
            return access.GetSelectResultCountByTicketNum(strTicketNum);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskVo Details(string id)
        {
            return access.Details(id);
        }

        public string GetPlanIDByContractID(string strContractID)
        {
            TMisMonitorTaskVo obj = new TMisMonitorTaskVo();
            obj.CONTACT_ID = strContractID;
            obj = access.Details(obj);

            return obj.PLAN_ID;
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTask">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskVo Details(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.Details(tMisMonitorTask);
        }

        public DataTable Details_One(long workID)
        {
            return access.Details_One(workID);
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
            return access.SelectByObject(tMisMonitorTask, iIndex, iCount);

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
            return access.SelectByTable(tMisMonitorTask, iIndex, iCount);
        }

        /// <summary>
        /// 根据WORK_ID获取常规数据信息
        ///  黄进军 add 20150917
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetEnvInfo(string work_id)
        {
            return access.GetEnvInfo(work_id);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.SelectByTable(tMisMonitorTask);
        }

        /// <summary>
        /// //黄飞 20150924 更新附件保存数据
        /// </summary>
        /// <param name="filenameA"></param>
        /// <param name="strContratID"></param>
        /// <returns></returns>
        public DataTable UpdatAtt(string filenameA, string strContratID)
        {
            return access.UpdatAtt(filenameA, strContratID);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskVo SelectByObject(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.SelectByObject(tMisMonitorTask);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.Create(tMisMonitorTask);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.Edit(tMisMonitorTask);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskVo tMisMonitorTask_UpdateSet, TMisMonitorTaskVo tMisMonitorTask_UpdateWhere)
        {
            return access.Edit(tMisMonitorTask_UpdateSet, tMisMonitorTask_UpdateWhere);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.Delete(tMisMonitorTask);
        }

        public DataTable GetDocNo(string CONTRACT_TYPE,int iIndex, int iCount)
        {
            return access.GetDocNo(CONTRACT_TYPE, iIndex, iCount);
        }
        public int GetSelectResultCounts(string CONTRACT_TYPE)
        {
            return access.GetSelectResultCounts(CONTRACT_TYPE);
        }
        public DataTable GetPhone(string strWorkTask_id, string strCompanyIdFrim)
        {
            return access.GetPhone(strWorkTask_id, strCompanyIdFrim);
        }
              /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>是否成功</returns>
        public bool Creates(TMisReportVo vo)
        {
            return access.Creates(vo);
        }
        public DataTable GetInfo(string year, string month, int iIndex, int iCount)
        {
            return access.GetInfo(year, month, iIndex, iCount);
        }
        public int GetInfoCount(string year, string month)
        {
            return access.GetInfoCount(year, month);
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
            return access.GetContractTaskInfo(strTaskID);
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
            return access.SaveTrans(tMisMonitorTask, tMisMonitorTaskCompanyA, tMisMonitorTaskCompanyB, objReportVo, arrTaskPoint, arrSubtask, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp);
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
            return access.SaveTrans(tMisMonitorTask,objReportVo, arrTaskPoint, arrSubtask, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp);
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
        public bool SaveTrans(TMisMonitorTaskVo tMisMonitorTask, ArrayList arrTaskPoint, ArrayList arrSubtask, ArrayList arrSubTaskApp, ArrayList arrPointItem, ArrayList arrSample, ArrayList arrSampleResult, ArrayList arrSampleResultApp)
        {
            return access.SaveTrans(tMisMonitorTask, arrTaskPoint, arrSubtask, arrSubTaskApp, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp);
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
            return access.SaveTrans(tMisMonitorTask, tMisMonitorTaskCompanyA, tMisMonitorTaskCompanyB, objReportVo);
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
            return access.SaveTrans(tMisMonitorTask, tMisMonitorTaskCompanyA, tMisMonitorTaskCompanyB, objReportVo, arrTaskPoint, arrSubtask, arrSubtaskApp, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp);
        }

        /// <summary>
        /// 功能描述：保存监测任务、样品所有信息（自送样预约）
        /// 创建时间：2012-12-20
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
            return access.SaveSampleTrans(tMisMonitorTask, tMisMonitorTaskCompanyA, tMisMonitorTaskCompanyB, objReportVo, arrSubtask, arrSample, arrSampleResult, arrSampleResultApp);
        }

        /// <summary>
        /// 获取监测任务办理情况统计列表 饼图  Create By 胡方扬 2013-01-02
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetTaskFinishedChart(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, string Dept, bool isFinished)
        {
            return access.GetTaskFinishedChart(tMisMonitorTask, tMisMonitorTaskCompany, Dept, isFinished);
        }


        /// <summary>
        /// 根据完成状态 获取监测任务完成、未完成、总和
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <returns></returns>
        public DataTable GetTaskChartCountWithStatus(TMisMonitorTaskVo tMisMonitorTask, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, string Dept)
        {
            return access.GetTaskChartCountWithStatus(tMisMonitorTask, tMisMonitorTaskCompany, Dept);
        }

        #region QHD
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForQHD(TMisMonitorTaskVo tMisMonitorTask, string strUserID)
        {
            return access.GetSelectResultCountForQHD(tMisMonitorTask, strUserID);
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
            return access.SelectByTableForQHD(tMisMonitorTask, strUserID, iIndex, iCount);
        }

         /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForZZ(TMisMonitorTaskVo tMisMonitorTask, string strUserID,bool bHasAccept)
        {
            return access.GetSelectResultCountForZZ(tMisMonitorTask, strUserID,bHasAccept);
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
            return access.SelectByTableForZZ(tMisMonitorTask, strUserID, iIndex, iCount, bHasAccept);
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
            return access.SelectByTableForReportAccept(tMisMonitorTask, iIndex, iCount);
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
            return access.SelectCountForReportAccept(tMisMonitorTask);
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
            return access.SeletByTableReportFinished(tMisMonitorTask, tMisContract, type);
        }

        /// <summary>
        /// 获取办理及时率 胡方扬 2013-03-07
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SeletByTableReportFinishedCount(TMisMonitorTaskVo tMisMonitorTask, TMisContractVo tMisContract)
        {
            return access.SeletByTableReportFinishedCount(tMisMonitorTask, tMisContract);
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
            return access.SelectSampleTaskForQY(strSubTaskID, strUserId, strDutyCode, strSubTaskStatus, intPageIndex, intPageSize);
        }

        public DataTable SelectSampleTaskForMAS(string strSubTaskID, bool b, int intPageIndex, int intPageSize)
        {
            return access.SelectSampleTaskForMAS(strSubTaskID, b, intPageIndex, intPageSize);
        }
        public int SelectSampleTaskCountForMAS(string strSubTaskID, bool b)
        {
            return access.SelectSampleTaskCountForMAS(strSubTaskID, b);
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
            return access.SelectSampleTaskForQCQY(strSubTaskID, strUserId, strDutyCode, strSubTaskStatus, intPageIndex, intPageSize);
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
            return access.SelectSampleTaskForWithSampleAnalysisQY(strUserId, strDutyCode, strSubTaskStatus,strQc,strResultStatus,intPageIndex, intPageSize);
        }
        public DataTable SelectSampleTaskForWithSampleAnalysisMAS(string strResultID, int intPageIndex, int intPageSize)
        {
            return access.SelectSampleTaskForWithSampleAnalysisMAS(strResultID, intPageIndex, intPageSize);
        }
        public int SelectSampleTaskForWithSampleAnalysisCountMAS(string strResultID)
        {
            return access.SelectSampleTaskForWithSampleAnalysisCountMAS(strResultID);
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
            return access.SelectSampleTaskForWithSampleAnalysisCountQY(strUserId, strDutyCode, strSubTaskStatus, strQc, strResultStatus);
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
            return access.SelectSampleSubTaskForWithSampleAnalysisQY(strTaskId);
        }
        public DataTable SelectSampleSubTaskForWithSampleAnalysisMAS(string strTaskId, string strResultID)
        {
            return access.SelectSampleSubTaskForWithSampleAnalysisMAS(strTaskId, strResultID);
        }
        /// <summary>
        /// 功能描述：获取分析类现场监测项目流程任务的包含分析类现场监测子任务信息条目数
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
            return access.SelectSampleSubTaskForWithSampleAnalysisCountQY(strTaskId);
        }

        /// <summary>
        /// 获取任务下的分析类监测项目信息 Create by weilin
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool SetAnysceneItem(string strTaskId, string strStatus)
        {
            return access.SetAnysceneItem(strTaskId, strStatus);
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
            return access.SelectSampleTaskCountForQCQY(strSubTaskID, strUserId, strDutyCode, strSubTaskStatus);
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
            return access.SelectSampleTaskCountForQY(strSubTaskID, strUserId, strDutyCode, strSubTaskStatus);
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
            return access.SelectByTableForTaskTraking(strTaskId);
        }

        /// <summary>
        /// 获取环境质量任务预约信息 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectEnvPlanTaskList(TMisMonitorTaskVo tMisMonitorTask, int PageIndex, int PageSize)
        {
            return access.SelectEnvPlanTaskList(tMisMonitorTask, PageIndex, PageSize);
        }

        /// <summary>
        /// 获取环境质量任务预约信息条数 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectEnvPlanTaskListCount(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.SelectEnvPlanTaskListCount(tMisMonitorTask);
        }

        /// <summary>
        /// 获取环境质量任务预约信息 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectEnvPlanTaskAbList(TMisMonitorTaskVo tMisMonitorTask, TMisContractPlanVo tMisContractPlan,int PageIndex, int PageSize)
        {
            return access.SelectEnvPlanTaskAbList(tMisMonitorTask,tMisContractPlan, PageIndex, PageSize);
        }

        /// <summary>
        /// 获取环境质量任务预约信息条数 胡方扬 2013-05-06
        /// </summary>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectEnvPlanTaskAbListCount(TMisMonitorTaskVo tMisMonitorTask, TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectEnvPlanTaskAbListCount(tMisMonitorTask, tMisContractPlan);
        }

        /// <summary>
        /// 获取项目负责人
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable GetProjectID(string strTaskID)
        {
            return access.GetProjectID(strTaskID);
        }

        /// 获取对象DataTable 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSummary(TMisMonitorTaskVo tMisMonitorTask, bool isLocal, int iIndex, int iCount)
        {
            return access.SelectByTable_ForSummary(tMisMonitorTask, isLocal, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSummary(TMisMonitorTaskVo tMisMonitorTask, bool isLocal)
        {
            return access.GetSelectResultCount_ForSummary(tMisMonitorTask, isLocal);
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
            return access.SelectByUnionTaskTable(tMisTaskContract, intPageIndex, intPageSize);
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
            return access.SetSubTask(objDt, strTaskStatus, strTaskType);
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
            return access.SelectSampleTaskForQCQY(strSubTaskID, strUserId, strDutyCode, strSubTaskStatus, strTaskId);
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
            return access.SelectByUnionTaskTableResult(tMisTaskContract);
        }

        /// <summary>
        /// 获取自送样采样计划
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetContractInforUnionSamplePlan(TMisMonitorTaskVo tMisTaskContract, TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.GetContractInforUnionSamplePlan(tMisTaskContract, tMisContractSamplePlan);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorTask.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tMisMonitorTask.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //预约ID
            if (tMisMonitorTask.PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("预约ID不能为空");
                return false;
            }
            //委托书编
            if (tMisMonitorTask.CONTRACT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("委托书编不能为空");
                return false;
            }
            //委托年度
            if (tMisMonitorTask.CONTRACT_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("委托年度不能为空");
                return false;
            }
            //项目名称
            if (tMisMonitorTask.PROJECT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("项目名称不能为空");
                return false;
            }
            //委托类型
            if (tMisMonitorTask.CONTRACT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("委托类型不能为空");
                return false;
            }
            //报告类型
            if (tMisMonitorTask.TEST_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("报告类型不能为空");
                return false;
            }
            //监测目的
            if (tMisMonitorTask.TEST_PURPOSE.Trim() == "")
            {
                this.Tips.AppendLine("监测目的不能为空");
                return false;
            }
            //委托企业ID
            if (tMisMonitorTask.CLIENT_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托企业ID不能为空");
                return false;
            }
            //受检企业ID
            if (tMisMonitorTask.TESTED_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("受检企业ID不能为空");
                return false;
            }
            //合同签订日期
            if (tMisMonitorTask.CONSIGN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("合同签订日期不能为空");
                return false;
            }
            //要求完成日期
            if (tMisMonitorTask.ASKING_DATE.Trim() == "")
            {
                this.Tips.AppendLine("要求完成日期不能为空");
                return false;
            }
            //完成日期
            if (tMisMonitorTask.FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("完成日期不能为空");
                return false;
            }
            //样品来源,1,抽样，2，自送样
            if (tMisMonitorTask.SAMPLE_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("样品来源,1,抽样，2，自送样不能为空");
                return false;
            }
            //送样人ID
            if (tMisMonitorTask.CONTACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("送样人ID不能为空");
                return false;
            }
            //接样人ID
            if (tMisMonitorTask.MANAGER_ID.Trim() == "")
            {
                this.Tips.AppendLine("接样人ID不能为空");
                return false;
            }
            //计划制定人ID
            if (tMisMonitorTask.CREATOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("计划制定人ID不能为空");
                return false;
            }
            //项目负责人ID
            if (tMisMonitorTask.PROJECT_ID.Trim() == "")
            {
                this.Tips.AppendLine("项目负责人ID不能为空");
                return false;
            }
            //计划制定日期
            if (tMisMonitorTask.CREATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("计划制定日期不能为空");
                return false;
            }
            //状态
            if (tMisMonitorTask.STATE.Trim() == "")
            {
                this.Tips.AppendLine("状态不能为空");
                return false;
            }
            //计划状态
            if (tMisMonitorTask.TASK_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("计划状态不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorTask.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorTask.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorTask.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorTask.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorTask.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 监测结果补录
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompleteTask(int iIndex, int iCount) 
        {
            return access.GetCompleteTask(iIndex, iCount);
        }

        /// <summary>
        /// 监测结果补录 分页
        /// </summary>
        public int GetTaskCount()
        {
            return access.GetTaskCount();
        }

        /// <summary>
        /// 删除已下达的监测任务 Create By: weilin 2014-8-6
        /// </summary>
        /// <param name="strPlanID"></param>
        /// <returns></returns>
        public bool DelTaskTrans(string strPlanID)
        {
            return access.DelTaskTrans(strPlanID);
        }
    }
}