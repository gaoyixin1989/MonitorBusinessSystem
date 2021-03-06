using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.DataAccess.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskLogic : LogicBase
    {

        TMisMonitorSubtaskVo tMisMonitorSubtask = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskAccess access;

        public TMisMonitorSubtaskLogic()
        {
            access = new TMisMonitorSubtaskAccess();
        }

        public TMisMonitorSubtaskLogic(TMisMonitorSubtaskVo _tMisMonitorSubtask)
        {
            tMisMonitorSubtask = _tMisMonitorSubtask;
            access = new TMisMonitorSubtaskAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.GetSelectResultCount(tMisMonitorSubtask);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskVo Details(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.Details(tMisMonitorSubtask);
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
            return access.SelectByObject(tMisMonitorSubtask, iIndex, iCount);

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
            return access.SelectByTable(tMisMonitorSubtask, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.SelectByTable(tMisMonitorSubtask);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns></returns>
        public TMisMonitorSubtaskVo SelectByObject(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.SelectByObject(tMisMonitorSubtask);
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
            return access.SelectByTableWithTask(tMisMonitorSubtask, strMonitorID, strTaskStatus, strTestedCompanyID, strContractCode, strUserID, iIndex, iCount);
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectWithTaskResultCount(TMisMonitorSubtaskVo tMisMonitorSubtask, string strTestedCompanyID, string strContractCode)
        {
            return access.GetSelectWithTaskResultCount(tMisMonitorSubtask, strTestedCompanyID, strContractCode);
        }

        /// <summary>
        /// 获取监测子任务→监测任务 信息  含 环境质量类  胡方扬 2013-05-07
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableWithAllTask(TMisMonitorSubtaskVo tMisMonitorSubtask, string strMonitorID, string strTaskStatus, string strTestedCompanyID, string strContractCode, string strUserID, int iIndex, int iCount)
        {
            return access.SelectByTableWithAllTask(tMisMonitorSubtask, strMonitorID, strTaskStatus, strTestedCompanyID, strContractCode, strUserID, iIndex, iCount);
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
            return access.SelectByTableWithAllTaskForFatherTree(tMisMonitorSubtask, strMonitorID, strTaskStatus, strTestedCompanyID, strContractCode, strUserID, iIndex, iCount);
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
            return access.SelectByTableWithAllTaskRecordCount(tMisMonitorSubtask, strMonitorID, strTaskStatus, strTestedCompanyID, strContractCode, strUserID);
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.Create(tMisMonitorSubtask);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.Edit(tMisMonitorSubtask);
        }

        public bool Edit_One(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.Edit_One(tMisMonitorSubtask);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubtask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskVo tMisMonitorSubtask_UpdateSet, TMisMonitorSubtaskVo tMisMonitorSubtask_UpdateWhere)
        {
            return access.Edit(tMisMonitorSubtask_UpdateSet, tMisMonitorSubtask_UpdateWhere);
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
        public bool Delete(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.Delete(tMisMonitorSubtask);
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
            return access.GetCountWithTask(tMisMonitorSubtask, strUserID);
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
            return access.selectAllContractType(strTask);
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
            return access.CombackTaskToAnalyse(strTaskId);
        }

        /// <summary>
        /// 判断该任务下的子任务是否都已经结束 Create By weilin 2013-11-09
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="b">true:发送后判断，false:发送前判断</param>
        /// <returns></returns>
        public bool isFinishSubTask(string strTaskId, bool b)
        {
            return access.isFinishSubTask(strTaskId, b);
        }

        /// <summary>
        /// 判断该任务是否存在分析类监测项目 Create By weilin 2013-11-09
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool isExistAnyscene(string strTaskId, string strSubTaskID)
        {
            return access.isExistAnyscene(strTaskId, strSubTaskID);
        }

        /// <summary>
        /// 判断该任务是否存在分析类现场监测项目 Create By weilin 2014-02-13
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool isExistAnysceneDept(string strTaskId, string strSubTaskID)
        {
            return access.isExistAnysceneDept(strTaskId, strSubTaskID);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目的子任务信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleSubTaskForQY(string strSubTaskID, string strTaskId, string strSubTaskStatus, string strDutyCode)
        {
            return access.SelectSampleSubTaskForQY(strSubTaskID, strTaskId, strSubTaskStatus, strDutyCode);
        }
        public DataTable SelectSampleSubTaskForMAS(string strSubTaskID, string strTaskId, bool b)
        {
            return access.SelectSampleSubTaskForMAS(strSubTaskID, strTaskId, b);
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
            return access.getMonitorCountByTask(strTaskID);
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
            return access.getMonitorByTask(strTaskID);
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
            return access.GetSubTaskObjectBySample(strSampleId);
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
            return access.GetNoSampleSubTaskInfo(strTaskID, strMonitorID);
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
            return access.GetSampleSubTaskInfo(strTaskID, strMonitorID);
        }

                /// <summary>
        /// 创建原因：环境质量类别分析完成后自动导入对应类别的环境数据填报表中
        /// 创建时间：2013-06-21
        /// 创建人：胡方扬
        /// </summary>
        /// <param name="tMisMonitorSubtask">监测子任务</param>
        /// <param name="blFlag">是否为噪声填报 true 是 false 否</param>
        /// <param name="strBC">是否补测：'true'-是 'false'-否</param>
        /// <returns></returns>
        public bool SetEnvFillData(TMisMonitorSubtaskVo tMisMonitorSubtask, bool blFlag, string strBC)
        {
            return access.SetEnvFillData(tMisMonitorSubtask, blFlag, strBC);
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
            return access.GetPointMapForSubTask(tMisMonitorSubtask);
        }
        /// 创建原因：根据子任务ID获取当前子任务所属企业基础资料ID
        /// 创建人：胡方扬
        /// 创建日期：2013-07-03
        /// </summary>
        /// <param name="tMisMonitorSubtask"></param>
        /// <returns></returns>
        public DataTable GetCompanyIDForSubTask(TMisMonitorSubtaskVo tMisMonitorSubtask)
        {
            return access.GetCompanyIDForSubTask(tMisMonitorSubtask);
        }
        public int GetResultCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime)
        {
            return access.GetResultCount(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime);
        }
        public DataTable SearchDataEx(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, int iIndex, int iCount)
        {
            return access.SearchDataEx(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime, iIndex, iCount);
        }
        public int Get_TaskDoPlanList_QCStep_Count(TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.Get_TaskDoPlanList_QCStep_Count(tMisMonitorTask);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorSubtask.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测计划ID
            if (tMisMonitorSubtask.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测计划ID不能为空");
                return false;
            }
            //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
            if (tMisMonitorSubtask.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
            //采样要求时间
            if (tMisMonitorSubtask.SAMPLE_ASK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样要求时间不能为空");
                return false;
            }
            //分析完成时间
            if (tMisMonitorSubtask.SAMPLE_FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样完成时间不能为空");
                return false;
            }
            //采样方式
            if (tMisMonitorSubtask.SAMPLING_METHOD.Trim() == "")
            {
                this.Tips.AppendLine("采样方式不能为空");
                return false;
            }
            //采样负责人
            if (tMisMonitorSubtask.SAMPLING_MANAGER_ID.Trim() == "")
            {
                this.Tips.AppendLine("采样负责人不能为空");
                return false;
            }
            //采样协同人ID
            if (tMisMonitorSubtask.SAMPLING_ID.Trim() == "")
            {
                this.Tips.AppendLine("采样协同人ID不能为空");
                return false;
            }
            //采样人
            if (tMisMonitorSubtask.SAMPLING_MAN.Trim() == "")
            {
                this.Tips.AppendLine("采样人不能为空");
                return false;
            }
            //样品接收时间
            if (tMisMonitorSubtask.SAMPLE_ACCESS_DATE.Trim() == "")
            {
                this.Tips.AppendLine("样品接收时间不能为空");
                return false;
            }
            //样品接收人ID
            if (tMisMonitorSubtask.SAMPLE_ACCESS_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品接收人ID不能为空");
                return false;
            }
            //接样意见
            if (tMisMonitorSubtask.SAMPLE_APPROVE_INFO.Trim() == "")
            {
                this.Tips.AppendLine("接样意见不能为空");
                return false;
            }
            //分析完成时间
            if (tMisMonitorSubtask.ANALYSE_FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("分析完成时间不能为空");
                return false;
            }
            //监测结论
            if (tMisMonitorSubtask.PROJECT_CONCLUSION.Trim() == "")
            {
                this.Tips.AppendLine("监测结论不能为空");
                return false;
            }
            //项目完成时间
            if (tMisMonitorSubtask.PROJECT_FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("项目完成时间不能为空");
                return false;
            }
            //任务状态类别(发送，退回)
            if (tMisMonitorSubtask.TASK_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("任务状态类别(发送，退回)不能为空");
                return false;
            }
            //任务状态
            if (tMisMonitorSubtask.TASK_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("任务状态不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorSubtask.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorSubtask.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorSubtask.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorSubtask.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorSubtask.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// 获取对象DataTable 数据汇总表
        /// </summary>
        /// <param name="tMisMonitorTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSummary(TMisMonitorSubtaskVo objSubTask, bool isLocal, int iIndex, int iCount)
        {
            return access.SelectByTable_ForSummary(objSubTask, isLocal);
        }

        /// <summary>
        /// 当包含分析类现场监测项目时现场结果复核的退回事件 Create By weilin 2013-11-11
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strSubTaskStatus">子任务的退回状态</param>
        /// <param name="strResultStatus">监测结果的退回状态</param>
        /// <param name="strSubTaskType"></param>
        /// <returns></returns>
        public bool SampleResultCheckBackTo(string strTaskId, string strSubTaskID, string strSubTaskStatus, string strResultStatus, string strSubTaskType)
        {
            return access.SampleResultCheckBackTo(strTaskId, strSubTaskID, strSubTaskStatus, strResultStatus, strSubTaskType);
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
            return access.SampleResultQcCheckBackTo(strTaskId, strCurrentStatus, strBackStatus, strSubTaskType);
        }

        /// <summary>
        /// 获取子任务所有测点的监测项目信息 Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable getItemBySubTaskID(string strSubTaskID)
        {
            return access.getItemBySubTaskID(strSubTaskID);
        }
        // <summary>
        /// 获取子任务所有测点的监测项目结果值信息 Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <returns></returns>
        public DataTable getItemValueBySampleID(string strSampleID)
        {
            return access.getItemValueBySampleID(strSampleID);
        }
        /// <summary>
        /// 获取现场监测结果复核数据列表 Create By weilin 2014-03-27
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strSubTaskStatus"></param>
        /// <returns></returns>
        public DataTable SelectSamplingCheckList(string strUserID, string strSubTaskStatus, int intPageIndex, int intPageSize, bool b)
        {
            return access.SelectSamplingCheckList(strUserID, strSubTaskStatus, intPageIndex, intPageSize, b);
        }
        public int SelectSamplingCheckListCount(string strUserID, string strSubTaskStatus, bool b)
        {
            return access.SelectSamplingCheckListCount(strUserID, strSubTaskStatus, b);
        }
    }
}
