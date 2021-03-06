using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Sys.Resource;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public partial class TMisMonitorResultLogic : LogicBase
    {

        TMisMonitorResultVo tMisMonitorResult = new TMisMonitorResultVo();
        TMisMonitorResultAccess access;

        public TMisMonitorResultLogic()
        {
            access = new TMisMonitorResultAccess();
        }

        public TMisMonitorResultLogic(TMisMonitorResultVo _tMisMonitorResult)
        {
            tMisMonitorResult = _tMisMonitorResult;
            access = new TMisMonitorResultAccess();
        }

        /// <summary>
        /// 功能描述：获得任务下所有监测结果信息 汇总表
        /// 创建时间：2013-08-12
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getTotalItemInfoByTaskID_ForSummary(string strTaskID, string strSampleCode, string strMonitorId, bool isLocal)
        {
            return access.getTotalItemInfoByTaskID_ForSummary(strTaskID, strSampleCode, strMonitorId, isLocal);
        }

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.GetSelectResultCount(tMisMonitorResult);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResult">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultVo Details(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.Details(tMisMonitorResult);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultVo> SelectByObject(TMisMonitorResultVo tMisMonitorResult, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorResult, iIndex, iCount);

        }

        public DataTable Details1(string strResultID)
        {
            return access.Details1(strResultID);
        }


        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultVo tMisMonitorResult, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorResult, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.SelectByTable(tMisMonitorResult);
        }

        /// <summary>
        /// 根据多个ID查询多条数据    黄飞20150720【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableOne(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.SelectByTableOne(tMisMonitorResult);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable SelectManagerByTable(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.SelectManagerByTable(tMisMonitorResult);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns></returns>
        public TMisMonitorResultVo SelectByObject(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.SelectByObject(tMisMonitorResult);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.Create(tMisMonitorResult);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResult">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.Edit(tMisMonitorResult);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResult_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResult_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultVo tMisMonitorResult_UpdateSet, TMisMonitorResultVo tMisMonitorResult_UpdateWhere)
        {
            return access.Edit(tMisMonitorResult_UpdateSet, tMisMonitorResult_UpdateWhere);
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
        public bool Delete(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.Delete(tMisMonitorResult);
        }
        /// <summary>
        /// 根据子任务ID获取非现场项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptWithSubtaskID(string strSubtaskID)
        {
            return access.SelectSampleDeptWithSubtaskID(strSubtaskID);
        }

        /// <summary>
        /// 根据子任务ID获取非现场项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        /// <remarks>by lhm</remarks>
        public DataTable SelectSampleDeptWithSubtaskID2(string strSubtaskID, IList<string> sampleIdList = null)
        {
            return access.SelectSampleDeptWithSubtaskID2(strSubtaskID,sampleIdList:sampleIdList);
        }

        /// <summary>
        /// 根据任务ID获取现场项目或分析项目（郑州） Create By：weilin 2014-06-10
        /// </summary>
        /// <param name="strTaskID">任务ID</param>
        /// <param name="b">true:现场项目；false：分析项目</param>
        /// <returns></returns>
        public DataTable SelectItemInfoWithTaskID(string strTaskID, bool b)
        {
            return access.SelectItemInfoWithTaskID(strTaskID, b);
        }
        /// <summary>
        /// 根据任务ID和项目结果状态获取监测项目 Create By:weilin 2014-06-10
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public DataTable SelectItemStatus(string strTaskID, string strResultStatus)
        {
            return access.SelectItemStatus(strTaskID, strResultStatus);
        }
        /// <summary>
        /// 根据样品ID获取非现场项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptWithSampleID(string strSampleID)
        {
            return access.SelectSampleDeptWithSampleID(strSampleID);
        }
        public bool updateResultBySample(string strSampleIDs, string strStatus, bool b)
        {
            return access.updateResultBySample(strSampleIDs, strStatus, b);
        }

        /// <summary>
        /// 功能描述：根据子任务ID获取现场项目 并得到执行信息
        /// 创建时间：2013-5-11
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectResultAndAppWithSubtaskID(string strSubtaskID)
        {
            return access.SelectResultAndAppWithSubtaskID(strSubtaskID);
        }
        // <summary>
        /// 功能描述：根据子任务ID获取监测项目 并得到执行信息
        /// 创建时间：2014-8-5
        /// 创建人：weilin
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectResultAndAppWithSubtaskID_QHD(string strSubtaskID)
        {
            return access.SelectResultAndAppWithSubtaskID_QHD(strSubtaskID);
        }
        #endregion

        #region 质控统计方法
        /// <summary>
        /// 按时间、类别筛选原始样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getResultWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getResultWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }

        /// <summary>
        /// 按时间、类别筛选现场空白样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCEmptyOutWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getQCEmptyOutWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }

        /// <summary>
        /// 按时间、类别筛选实验室空白样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCEmptyBatWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getQCEmptyBatWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }

        /// <summary>
        /// 按时间、类别筛选平行样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCTwinWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getQCTwinWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }

        /// <summary>
        /// 按时间、类别筛选加标样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCAddWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getQCAddWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }

        // <summary>
        /// 按时间、类别筛选加标样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCStWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            return access.getQCStWitnTimeAndType(strMontorID, strStartTime, strEndTime);
        }
        #endregion

        #region 样品分析任务信息

        /// <summary>
        /// 获取样品分析任务信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strAnalyseAssignDate">要求分析完成时间</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetInfo(string strContractType, string strMonitorType, string strAnalyseAssignDate, string strFlowCode, string strCurrentUserId, string strResultStatus)
        {
            return access.getAssignmentSheetInfo(strContractType, strMonitorType, strAnalyseAssignDate, strFlowCode, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 根据监测项目获取样品信息
        /// </summary>
        /// <param name="strSampleIds">样品ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetInfoBySample(string strSampleIds, string strResultStatus)
        {
            return access.getAssignmentSheetInfoBySample(strSampleIds, strResultStatus);
        }

        /// <summary>
        /// 根据样品ID获取分析负责人和监测项目信息
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public string getAssignmentSheetUserInfo(string strSampleIds, string strResultStatus)
        {
            return access.getAssignmentSheetUserInfo(strSampleIds, strResultStatus);
        }
        /// <summary>
        /// 根据样品ID获取分析负责人和监测项目信息 Create By weilin
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetUserInfoEx(string strSampleIds, string strResultStatus)
        {
            return access.getAssignmentSheetUserInfoEx(strSampleIds, strResultStatus);
        }
        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <param name="strPrintedStatus">打印状态</param>
        /// <returns></returns>
        public bool updateAssignmentSheetResultStatus(string strSampleIds, string strResultStatus, string strPrintedStatus)
        {
            return access.updateAssignmentSheetResultStatus(strSampleIds, strResultStatus, strPrintedStatus);
        }
        #endregion

        #region 监测项目及检出限
        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID)
        {
            return access.getItemInfoForReport(strTaskID);
        }

        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID, string strPointID, string strMonitorType)
        {
            return access.getItemInfoForReport(strTaskID, strPointID, strMonitorType);
        }

        /// <summary>
        /// 功能描述：获得任务下所有监测结果信息
        /// 创建时间：2013-05-07
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getTotalItemInfoByTaskID(string strTaskID, string strSampleCode, string strMonitorId)
        {
            return access.getTotalItemInfoByTaskID(strTaskID, strSampleCode, strMonitorId);
        }
        #endregion

        #region 扩展函数
        /// <summary>
        ///  功能描述：获取样品所有项目详细信息（自送样报告用）
        ///  创建时间：2013-1-2
        ///  创建人：邵世卓
        /// </summary>
        /// <param name="strSampelID">样品ID</param>
        /// <param name="intPageIndex">序号</param>
        /// <param name="intPageSize">页码</param>
        /// <returns></returns>
        public DataTable SelectByTableForReport(string strSampelID, string strQcType, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForReport(strSampelID, strQcType, intPageIndex, intPageSize);
        }
        /// <summary>
        /// 获取污染源列表 胡方扬 2013-03-10
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPolSourceListTable(int iIndex, int iCount)
        {
            return access.SelectPolSourceListTable(iIndex, iCount);
        }
        /// <summary>
        /// 获取污染源列表总数 胡方扬 2013-03-10
        /// </summary>
        /// <returns></returns>
        public int SelectPolSourceListTableCount()
        {
            return access.SelectPolSourceListTableCount();
        }
        /// <summary>
        /// 获取污染源明细排口明细
        /// </summary>
        /// <param name="strTask_id"></param>
        /// <returns></returns>
        public DataTable GetPolSourceDetail(string strTask_id, int iIndex, int iCount)
        {
            return access.GetPolSourceDetail(strTask_id, iIndex, iCount);
        }
        /// <summary>
        /// 获取污染源明细排口明细总数
        /// </summary>
        /// <param name="strTask_id"></param>
        /// <returns></returns>
        public int GetPolSourceDetailCount(string strTask_id)
        {
            return access.GetPolSourceDetailCount(strTask_id);
        }
        #endregion

        #region 邵世卓 清远需求
        /// <summary>
        /// 现场项目的结果信息
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectSceneItemInfo(TMisMonitorResultVo TMisMonitorResultVo)
        {
            return access.SelectSceneItemInfo(TMisMonitorResultVo);
        }
        public DataTable SelectSceneItemInfo_MAS(TMisMonitorResultVo TMisMonitorResultVo, string strSample)
        {
            return access.SelectSceneItemInfo_MAS(TMisMonitorResultVo, strSample);
        }
        /// <summary>
        /// 功能描述：判断任务是否可以发送至下环节（清远 现场项目审核）
        /// 创建时间：2013-3-17
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strCurrentUserId">人员ID</param>
        /// <param name="strSubTaskStatus1">原子监测任务分析环节状态ID</param>
        /// <param name="strSubTaskStatus2">现场监测任务完成的状态ID</param>
        /// <returns></returns>
        public bool TaskCanSendInQcCheck_QY(string strTaskId, string strCurrentUserId, string strDutyCode, string strSubTaskStatus1, string strSubTaskStatus2)
        {
            return access.TaskCanSendInQcCheck_QY(strTaskId, strCurrentUserId, strDutyCode, strSubTaskStatus1, strSubTaskStatus2);
        }

        /// <summary>
        /// 根据子任务ID获取现场项目
        /// </summary>
        /// <param name="strSubtaskID">监测子任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleItemWithSubtaskID(string strSubtaskID, IList<string> sampleIdList = null)
        {
            return access.SelectSampleItemWithSubtaskID(strSubtaskID,sampleIdList:sampleIdList);
        }

        /// <summary>
        /// 根据子任务ID把该任务下所有分析类现场监测项目的结果状态改为：00          Create By weilin
        /// </summary>
        /// <param name="strSubtaskID"></param>
        /// <returns></returns>
        public bool setSampleItemWithSubtaskID(string strSubtaskID)
        {
            return access.setSampleItemWithSubtaskID(strSubtaskID);
        }

        #endregion

        #region 胡方扬 分析类现场监测项目处理
        /// <summary>
        /// 创建原因：获取分析类现场监测项目的分析结果
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable GetSampleAnalysisResult(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.GetSampleAnalysisResult(tMisMonitorResult);
        }
        public DataTable GetSampleAnalysisResult_MAS(TMisMonitorResultVo tMisMonitorResult)
        {
            return access.GetSampleAnalysisResult_MAS(tMisMonitorResult);
        }

         /// <summary>
        /// 创建原因：根据指定的监测子任务ID获取监测样品信息
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public DataTable GetSampleAnalysisSample(string strSubTaskId, int intPageIndex, int intPageSize)
        {
            return access.GetSampleAnalysisSample(strSubTaskId, intPageIndex,intPageSize);
        }

        /// <summary>
        /// 创建原因：根据指定的监测子任务ID获取监测样品信息条目数
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public int GetSampleAnalysisSampleCount(string strSubTaskId)
        {
            return access.GetSampleAnalysisSampleCount(strSubTaskId);
        }

        public DataTable GetSampleAnalysisSample_MAS(string strSubTaskId, string strResultID, int intPageIndex, int intPageSize)
        {
            return access.GetSampleAnalysisSample_MAS(strSubTaskId, strResultID, intPageIndex, intPageSize);
        }

        public int GetSampleAnalysisSampleCount_MAS(string strSubTaskId, string strResultID)
        {
            return access.GetSampleAnalysisSampleCount_MAS(strSubTaskId, strResultID);
        }
        #endregion

        #region 潘德军 清远需求
        /// <summary>
        /// 监测数据统计表:导出已出报告的符合检索条件的报表（EXCEL格式）
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectTestDataLst(string[] strWhereArr, int iIndex, int iCount)
        {
            return access.SelectTestDataLst(strWhereArr,  iIndex,  iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectTestDataLstCount(string[] strWhereArr)
        {
            return access.GetSelectTestDataLstCount(strWhereArr);
        }
        #endregion

        /// <summary>
        /// 报告份数统计表 Create By： weilin 2014-11-21
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectReportDataLst(string strContractType, string strMonitorType, string strStartDate, string strEndDate)
        {
            return access.SelectReportDataLst(strContractType, strMonitorType, strStartDate, strEndDate);
        }

        #region 潘德军 结果数据可追溯性
        /// <summary>
        /// 根据用户ID获取结果数据DataTable
        /// </summary>
        /// <param name="strUserID">当前用户ID</param>
        /// <param name="strCurrResultStatus">20</param>
        /// <returns></returns>
        public DataTable SelectResult_WithUser(string strUserID, string strCurrResultStatus)
        {
            return access.SelectResult_WithUser(strUserID, strCurrResultStatus);
        }

        /// 填写结果日志
        /// </summary>
        /// <param name="dtResultSrc">页面载入时取得的结果数据DataTable</param>
        /// <param name="strResultIDs">发送的结果id串，多个id用半角逗号分隔</param>
        /// <param name="strSampleIDs">发送的样品id串，多个id用半角逗号分隔</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strCurrResultStatus">20</param>
        /// <param name="bIsSampleID">true,根据样品id串处理；false，根据结果id串处理</param>
        /// <returns></returns>
        public bool WriteResultLog(DataTable dtResultSrc, string strResultIDs, string strSampleIDs, string strCurrentUserId, string strCurrResultStatus,bool bIsSampleID)
        {
            if (bIsSampleID)
            {
                strSampleIDs = "'" + strSampleIDs.Replace(",", "','") + "'";

                strResultIDs = "";
                DataTable dtResult_WithSample = access.SelectResult_WithSampleIDs(strSampleIDs, strCurrentUserId, strCurrResultStatus);
                for (int i = 0; i < dtResult_WithSample.Rows.Count; i++)
                {
                    strResultIDs += (strResultIDs.Length > 0 ? "," : "") + dtResult_WithSample.Rows[i]["ID"].ToString();
                }
            }
            strResultIDs = "'" + strResultIDs.Replace(",", "','") + "'";
            
            DataTable dtResultTar = access.SelectResult_WithIDs(strResultIDs);
            DataTable dtResultApp = access.SelectResultApp_WithIDs(strResultIDs);

            string strLogs = "";
            for (int i = 0; i < dtResultTar.Rows.Count; i++)
            {
                for (int j = 0; j < dtResultSrc.Rows.Count; j++)
                {
                    if (dtResultSrc.Rows[j]["ID"].ToString() == dtResultTar.Rows[i]["ID"].ToString())
                    {
                        if (dtResultSrc.Rows[j]["ITEM_RESULT"].ToString() != dtResultTar.Rows[i]["ITEM_RESULT"].ToString())
                        {
                            DataRow[] drs = dtResultApp.Select(string.Format("RESULT_ID='{0}'", dtResultSrc.Rows[j]["ID"].ToString()));
                            string strLog = dtResultSrc.Rows[j]["ID"].ToString() + "|";
                            strLog += dtResultSrc.Rows[j]["ITEM_RESULT"].ToString() + "|";
                            strLog += dtResultSrc.Rows[j]["REMARK_2"].ToString() + "|";
                            strLog += dtResultTar.Rows[i]["ITEM_RESULT"].ToString() + "|";
                            strLog += dtResultTar.Rows[i]["REMARK_2"].ToString() + "|";//原始单号
                            strLog += drs[0]["HEAD_USERID"].ToString() + "|";
                            strLog += drs[0]["ASSISTANT_USERID"].ToString() + "|";
                            strLog += System.DateTime.Now.ToShortDateString();

                            strLogs += (strLogs.Length > 0 ? "@" : "") + strLog;
                        }
                    }
                }
            }
            if (strLogs.Length > 0)
            {
                string[] arrLog = strLogs.Split('@');
                string strSerials = new TSysSerialLogic().GetSerialNumberList("MONITOR_RESULT_LOG", strLogs.Length);
                string[] arrSerial = strSerials.Split(',');
                for (int i = 0; i < arrLog.Length; i++)
                {
                    arrLog[i] = arrSerial[i] + "|" + arrLog[i];
                }

                return access.InsertResultLog(arrLog);
            }
            else
                return true;
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public DataTable getTaskItemForSample(string strSampleId, int intPageIndex, int intPageSize)
        {
            return access.getTaskItemForSample(strSampleId, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public int getTaskItemCheckForSampleCount(string strSampleId)
        {
            return access.getTaskItemCheckForSampleCount(strSampleId);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorResult.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //样品ID
            if (tMisMonitorResult.SAMPLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品ID不能为空");
                return false;
            }
            //质控类型（原始样、现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行）
            if (tMisMonitorResult.QC_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("质控类型（原始样、现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行）不能为空");
                return false;
            }
            //质控原始样结果ID
            if (tMisMonitorResult.QC_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("质控原始样结果ID不能为空");
                return false;
            }
            //最初原始样ID，质控样可能是在原始样上做外控，然后在外控上做内控；或者在原始样上直接内控。那么最初原始样记录的是最早那个原始样的ID
            if (tMisMonitorResult.SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("最初原始样ID，质控样可能是在原始样上做外控，然后在外控上做内控；或者在原始样上直接内控。那么最初原始样记录的是最早那个原始样的ID不能为空");
                return false;
            }
            //检验项
            if (tMisMonitorResult.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("检验项不能为空");
                return false;
            }
            //检验项结果
            if (tMisMonitorResult.ITEM_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("检验项结果不能为空");
                return false;
            }
            //分析方法ID
            if (tMisMonitorResult.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //标准依据ID
            if (tMisMonitorResult.STANDARD_ID.Trim() == "")
            {
                this.Tips.AppendLine("标准依据ID不能为空");
                return false;
            }
            //使用的质控手段,对该样采用的质控手段，（现场空白1、现场加标2、现场平行4、实验室密码平行8，实验室空白16、实验室加标32、实验室明码平行64），位运算
            if (tMisMonitorResult.QC.Trim() == "")
            {
                this.Tips.AppendLine("使用的质控手段,对该样采用的质控手段，（现场空白1、现场加标2、现场平行4、实验室密码平行8，实验室空白16、实验室加标32、实验室明码平行64），位运算不能为空");
                return false;
            }
            //空白批次表ID
            if (tMisMonitorResult.EMPTY_IN_BAT_ID.Trim() == "")
            {
                this.Tips.AppendLine("空白批次表ID不能为空");
                return false;
            }
            //分样号
            if (tMisMonitorResult.SUB_SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("分样号不能为空");
                return false;
            }
            //任务状态类别(发送，退回)
            if (tMisMonitorResult.TASK_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("任务状态类别(发送，退回)不能为空");
                return false;
            }
            //结果状态(分析任务分配：01分析结果填报：02，分析结果校核：03)
            if (tMisMonitorResult.RESULT_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("结果状态(分析任务分配：01分析结果填报：02，分析结果校核：03)不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorResult.REMARK_1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorResult.REMARK_2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorResult.REMARK_3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorResult.REMARK_4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorResult.REMARK_5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        #region 黄进军 质控要求
        /// <summary>
        /// 通知样品号查找质控要求
        /// </summary>
        public DataTable getZKYQ(string strSimpleId)
        {
            return access.getZKYQ(strSimpleId);
        }
        #endregion
    }
}
