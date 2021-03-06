using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析结果表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public partial class TMisMonitorResultLogic : LogicBase
    {
        #region 分析环节公共方法
        /// <summary>
        /// 根据环节代码、监测类型、监测项目查找有权限的用户信息 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strMonitorType">监测类型代码，如废水、废气等</param>
        /// <param name="strItemId">监测项目代码</param>
        /// <returns></returns>
        public DataTable getUsersInfo(string strFlowCode, string strMonitorType, string strItemId)
        {
            return access.getUsersInfo(strFlowCode, strMonitorType, strItemId);
        }
        #endregion

        #region 外控管理方法
        /// <summary>
        /// 获取现场加标质控家标量数据方法
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strQcType">质控类型</param>
        /// <param name="strItemId">监测项目ID</param>
        /// <returns></returns>
        public DataTable getQcAddValue(string strSampleId, string strQcType, string strItemId)
        {
            return access.getQcAddValue(strSampleId, strQcType, strItemId);
        }
        /// <summary>
        /// 删除样品质控信息以及样品信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <returns></returns>
        public bool deleteSampleInfo(string strSampleId)
        {
            return access.deleteSampleInfo(strSampleId);
        }
        /// <summary>
        /// 删除质控信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <param name="strQcType">质控类型</param>
        /// <returns></returns>
        public bool deleteQcInfo(string strSampleId, string strQcType)
        {
            return access.deleteQcInfo(strSampleId, strQcType);
        }
        #endregion

        #region 采样前质控【秦皇岛】
        /// <summary>
        /// 获取采样前质控任务信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数量</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcTaskInfo(string strQcStatus, string strSubTaskStatus, int iIndex, int iCount)
        {
            return access.getSamplingBeginQcTaskInfo(strQcStatus, strSubTaskStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取采样前质控任务数量
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingBeginQcTaskCount(string strQcStatus, string strSubTaskStatus)
        {
            return access.getSamplingBeginQcTaskCount(strQcStatus, strSubTaskStatus);
        }
        /// <summary>
        /// 获取采样前质控监测类别
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcItemTypeInfo(string strTaskId, string strSubTaskStatus)
        {
            return access.getSamplingBeginQcItemTypeInfo(strTaskId, strSubTaskStatus);
        }
        /// <summary>
        /// 获取采样前质控样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcSampleInfo(string strSubTaskId)
        {
            return access.getSamplingBeginQcSampleInfo(strSubTaskId);
        }
        /// <summary>
        /// 获取采样前质控监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcItemInfo(string strSampleId)
        {
            return access.getSamplingBeginQcItemInfo(strSampleId);
        }

        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strSubTaskNextStatus">下一环节状态</param>
        /// <returns></returns>
        public bool sendSamplingBeginQcTaskToNext(string strSampleId, string strSubTaskNextStatus)
        {
            return access.sendSamplingBeginQcTaskToNext(strSampleId, strSubTaskNextStatus);
        }
        #endregion

        #region 质控平行、空白加标方法【秦皇岛】

        /// <summary>
        /// 获取质控平行【9】，空白加标【10】监测项目信息
        /// </summary>
        /// <param name="strSubTaskId">子任务编号</param>
        /// <param name="strQcType">质控类型</param>
        /// <returns></returns>
        public DataTable getQcItemInfo_QHD(string strSubTaskId, string strQcType)
        {
            return access.getQcItemInfo_QHD(strSubTaskId, strQcType);
        }

        #endregion

        #region 标准盲样【郑州】
        /// <summary>
        /// 根据样品ID获取标准盲样信息
        /// </summary>
        /// <param name="strampleId">样品ID</param>
        /// <returns></returns>
        public string getBlindString(string strSampleId)
        {
            return access.getBlindString(strSampleId);
        }
        #endregion

        #region 采样【郑州】
        /// <summary>
        /// 获取采样任务退回信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strQcStatus">采样环节状态</param>
        /// <returns></returns>
        public DataTable getSamplingBackInfo_ZZ(string strSubTaskStatus, string strQcStatus)
        {
            return access.getSamplingBackInfo_ZZ(strSubTaskStatus, strQcStatus);
        }
        #endregion

        #region  分析任务分配
        /// <summary>
        /// 获取任务信息 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskInfo(strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取任务信息 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskInfo_QHD(string strTaskStatus, string strResultStatus, string strSampleStatus, string iSample, int iIndex, int iCount)
        {
            return access.getTaskInfo_QHD(strTaskStatus, strResultStatus, strSampleStatus, iSample, iIndex, iCount);
        }
        /// <summary>
        /// 获取任务信息总记录数量 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCount(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            return access.getTaskInfoCount(strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus);
        }

        public DataTable getTaskInfo_MAS(string strTaskID, string strSubTaskID, int iIndex, int iCount)
        {
            return access.getTaskInfo_MAS(strTaskID, strSubTaskID, iIndex, iCount);
        }
        public int getTaskInfoCount_MAS(string strTaskID, string strSubTaskID)
        {
            return access.getTaskInfoCount_MAS(strTaskID, strSubTaskID);
        }
        
        //huangjinjun add 2015-12-21 承德数据汇总节点
        public DataTable getTaskInfo_CD(string strTaskID, string strSubTaskID, int iIndex, int iCount)
        {
            return access.getTaskInfo_CD(strTaskID, strSubTaskID, iIndex, iCount);
        }

        /// <summary>
        /// 获取任务信息总记录数量 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCount_QHD(string strTaskStatus, string strResultStatus, string strSampleStatus, string iSample)
        {
            return access.getTaskInfoCount_QHD(strTaskStatus, strResultStatus, strSampleStatus, iSample);
        }
        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getItemTypeInfo(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strResultStatus)
        {
            return access.getItemTypeInfo(strCurrentUserId, strFlowCode, strTaskId, strTaskStatus, strResultStatus);
        }
        public DataTable getItemTypeInfo_MAS(string strTaskId, string strSubTaskID)
        {
            return access.getItemTypeInfo_MAS(strTaskId, strSubTaskID);
        }
        /// <summary>
        /// 获取监测类别信息 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getItemTypeInfo_QHD(string strTaskId, string strTaskStatus, string strResultStatus, string strSampleStatus, bool b)
        {
            return access.getItemTypeInfo_QHD(strTaskId, strTaskStatus, strResultStatus, strSampleStatus, b);
        }
        /// <summary>
        /// 获取监测项目信息 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            return access.getItemInfo(strSubTaskId, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取监测项目数量 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getItemInfoCount(string strSubTaskId, string strResultStatus)
        {
            return access.getItemInfoCount(strSubTaskId, strResultStatus);
        }

        /// <summary>
        /// 获取监测项目附加信息,如分析负责人、分析协同人，分析完成时间 by 熊卫华 2013.04.23
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <returns></returns>
        public DataTable getItemExInfo(string strResultId)
        {
            return access.getItemExInfo(strResultId);
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如分析负责人、分析协同人，分析完成时间  by 熊卫华 2013.04.23
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <param name="strValue">需要设置的值</param>
        /// <param name="strColumnName">列名</param>
        /// <returns></returns>
        public bool SaveItemExInfo(string strResultId, string strValue, string strColumnName)
        {
            return access.SaveItemExInfo(strResultId, strValue, strColumnName);
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如分析负责人、分析协同人，分析完成时间  by weilin 2014.05.06
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_QHD(string strSampleIDs, string strItemID, string strValue, string strColumnName)
        {
            return access.SaveItemExInfo_QHD(strSampleIDs, strItemID, strValue, strColumnName);
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如:分析完成时间  by weilin 2014.09.19
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_QY(string strSampleID, string strStatus, string strValue, string strColumnName)
        {
            return access.SaveItemExInfo_QY(strSampleID, strStatus, strValue, strColumnName);
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如:分析完成时间  by weilin 2015.01.21
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_MAS(string strSampleID, string strValue, string strColumnName)
        {
            return access.SaveItemExInfo_MAS(strSampleID, strValue, strColumnName);
        }

        /// <summary>
        /// 获取已分配任务的默认负责人信息
        /// </summary>
        /// <param name="strTaskId">总任务Id</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultUser(string strTaskId)
        {
            return access.getAssignedDefaultUser(strTaskId);
        }

        /// <summary>
        /// 获取已分配任务的负责项目
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认负责人ID</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultItem(string strTaskId, string strDefaultUser)
        {
            return access.getAssignedDefaultItem(strTaskId, strDefaultUser);
        }
        /// <summary>
        /// 获取已分配任务的协同项目
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认协同人ID</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultItemEx(string strTaskId, string strDefaultUser)
        {
            return access.getAssignedDefaultItemEx(strTaskId, strDefaultUser);
        }
        /// <summary>
        /// 获取已分配任务的样品号
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认协同人ID</param>
        /// <returns></returns>
        public DataTable getAssignedSampleCode(string strTaskId, string strDefaultUser)
        {
            return access.getAssignedSampleCode(strTaskId, strDefaultUser);
        }
        /// <summary>
        /// 判断任务是否可以返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool IsCanGoToBack(string strTaskId, string strCurrentUserId, string strFlowCode, string strResultStatus)
        {
            return access.IsCanGoToBack(strTaskId, strCurrentUserId, strFlowCode, strResultStatus);
        }
        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strBackStatus">回退的状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool subTaskGoToBack(string strTaskId, string strCurrentUserId, string strBackStatus, string strFlowCode)
        {
            return access.subTaskGoToBack(strTaskId, strCurrentUserId, strBackStatus, strFlowCode);
        }
        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strBackStatus">回退的状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool subTaskGoToBack_QHD(string strTaskId, string strSubTaskStatus, string strBackStatus, string strSampleStatus, string strResultStatus)
        {
            return access.subTaskGoToBack_QHD(strTaskId, strSubTaskStatus, strBackStatus, strSampleStatus, strResultStatus);
        }
        /// <summary>
        /// 样品退回
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strSampleStatus"></param>
        /// <param name="strBackStatus"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool SampleGoToBack_QHD(string strTaskId, string strSampleStatus, string strBackStatus, string strResultStatus)
        {
            return access.SampleGoToBack_QHD(strTaskId, strSampleStatus, strBackStatus, strResultStatus);
        }

        /// <summary>
        /// 子任务返回 Create By weilin 2013-11-12
        /// </summary>
        public bool subTaskGoToBackEx(string strTaskId, string strCurrentStatus, string strCurrentUserId, string strBackStatus, string strFlowCode)
        {
            return access.subTaskGoToBackEx(strTaskId, strCurrentStatus, strCurrentUserId, strBackStatus, strFlowCode);
        }

        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToNextFlow(string strTaskId, string strCurrentUserId, string strFlowCode, string strSubTaskStatus, string strCurrenteResultStatus, string strNextResultStatus)
        {
            return access.sendToNextFlow(strTaskId, strCurrentUserId, strFlowCode, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToNextFlow_QHD(string strTaskId, string strCurrentUserId, string strSubTaskStatus, string strSampleStatus, string strCurrenteResultStatus, string strNextResultStatus, string iSample)
        {
            return access.sendToNextFlow_QHD(strTaskId, strCurrentUserId, strSubTaskStatus, strSampleStatus, strCurrenteResultStatus, strNextResultStatus, iSample);
        }
        /// <summary>
        /// 获取监测结果所属的监测类别
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <returns></returns>
        public DataTable getMonitorTypeByResultId(string strResultId)
        {
            return access.getMonitorTypeByResultId(strResultId);
        }
        #endregion

        #region 分析任务分配【秦皇岛】
        /// <summary>
        /// 获取分析任务分配环节中的样品号 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strCurrentUserId">当前用户Id</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInAlloction_QHD(string strSubTaskId, string strCurrentUserId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID)
        {
            return access.getSimpleCodeInAlloction_QHD(strSubTaskId, strCurrentUserId, strResultStatus, strSampleStatus, iSample, strMonitorID);
        }
        /// <summary>
        /// 根据子任务ID获取需要分析任务分配的样品信息 Create By :weilin 2015-01-22
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public DataTable getSampleCodeInAlloction_MAS(string strTaskId, string strSubTaskId)
        {
            return access.getSampleCodeInAlloction_MAS(strTaskId, strSubTaskId);
        }

        /// <summary>
        /// 根据子任务ID获取相关信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="b">true:分析项目 false:现场项目</param>
        /// <returns></returns>
        public DataTable getItemInfoBySubTaskID_MAS(string strTaskId, string strSubTaskId, bool b)
        {
            return access.getItemInfoBySubTaskID_MAS(strTaskId, strSubTaskId, b);
        }

        /// <summary>
        /// 获取监测项目信息 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getItemInfoInAlloction_QHD(string strSampleId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID, int iIndex, int iCount)
        {
            return access.getItemInfoInAlloction_QHD(strSampleId, strResultStatus, strSampleStatus, iSample, strMonitorID, iIndex, iCount);
        }
        /// <summary>
        /// 获取监测项目数量 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getItemInfoCountInAlloction_QHD(string strSampleId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID)
        {
            return access.getItemInfoCountInAlloction_QHD(strSampleId, strResultStatus, strSampleStatus, iSample, strMonitorID);
        }
        /// <summary>
        /// 获取分析监测项目信息 Create By : weilin 2015-01-22
        /// </summary>
        /// <param name="strSampleId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable getItemInfoInAlloction_MAS(string strSampleId, int iIndex, int iCount, string ItemCondition_WithOut = "1")
        {
            return access.getItemInfoInAlloction_MAS(strSampleId, iIndex, iCount, ItemCondition_WithOut);
        }
        /// <summary>
        /// 获取分析监测项目数量 Create By : weilin 2015-01-22
        /// </summary>
        /// <param name="strSampleId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public int getItemInfoCountInAlloction_MAS(string strSampleId, string ItemCondition_WithOut = "1")
        {
            return access.getItemInfoCountInAlloction_MAS(strSampleId, ItemCondition_WithOut);
        }
        /// <summary>
        /// 将监测结果发送至下一个环节
        /// </summary>
        /// <param name="strResultId">监测结果Id</param>
        /// <param name="strNextResultStatus">下一环节结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendResultToNext_QHD(string strResultId, string strNextResultStatus)
        {
            return access.SendResultToNext_QHD(strResultId, strNextResultStatus);
        }

        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_QHD(string strTaskId, string strCurrentUserId, string strTaskStatus, string strFlowCode, string strSampleStatus, string strCurrResultStatus, string strNextResultStatus, bool b, string iSample)
        {
            return access.SendTaskCheckToNextFlow_QHD(strTaskId, strCurrentUserId, strTaskStatus, strFlowCode, strSampleStatus, strCurrResultStatus, strNextResultStatus, b, iSample);
        }
        public bool SendTaskSampleCheckToNext_QHD(string strTaskID, string strCurrResultStatus, string strNextResultStatus)
        {
            return access.SendTaskSampleCheckToNext_QHD(strTaskID, strCurrResultStatus, strNextResultStatus);
        }
        /// <summary>
        /// 判断一个任务所有分析类项目是否已经全部发送到同一环节 Create By :weilin 2014-04-24
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public bool isAllSend(string strTaskID, string strStatus, bool b)
        {
            return access.isAllSend(strTaskID, strStatus, b);
        }

        /// <summary>
        /// 判断任务是否有外控样品
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool CheckTaskHasOuterQcSample(string strTaskId, string strSubTaskID)
        {
            return access.CheckTaskHasOuterQcSample(strTaskId, strSubTaskID);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_QY(string strTaskId, string strCurrentUserId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus)
        {
            return access.SendTaskCheckToNextFlow_QY(strTaskId, strCurrentUserId, strTaskStatus, strCurrResultStatus, strNextResultStatus);
        }
        #endregion

        #region 分析结果录入方法
        /// <summary>
        /// 分析录入环节获取样品号
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            return access.getSimpleCodeInResult(strCurrentUserId, strResultStatus, strSampleStatus);
        }
        /// <summary>
        /// 获取分析录入环节获取样品数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCount(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            return access.getSimpleCodeInResultCount(strCurrentUserId, strResultStatus, strSampleStatus);
        }
        /// <summary>
        /// 分析录入环节获取样品号
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult_QHD(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            return access.getSimpleCodeInResult_QHD(strCurrentUserId, strResultStatus, strSampleStatus);
        }
        /// <summary>
        /// 获取分析录入环节获取样品数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCount_QHD(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            return access.getSimpleCodeInResultCount_QHD(strCurrentUserId, strResultStatus, strSampleStatus);
        }
        /// <summary>
        /// 分析录入环节获取样品号(不受用户限制)
        /// </summary>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResultEx(string strResultStatus)
        {
            return access.getSimpleCodeInResultEx(strResultStatus);
        }
        /// <summary>
        /// 获取分析录入环节获取样品数量(不受用户限制)
        /// </summary>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCountEx(string strResultStatus)
        {
            return access.getSimpleCodeInResultCountEx(strResultStatus);
        }

        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            return access.getResultInResultFlow(strSimpleId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow_QHD(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            return access.getResultInResultFlow_QHD(strSimpleId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息记录数
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getResultInResultFlowCount(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            return access.getResultInResultFlowCount(strSimpleId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息(不受用户限制)
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlowEx(string strSimpleId, string strResultStatus)
        {
            return access.getResultInResultFlowEx(strSimpleId, strResultStatus);
        }
        /// <summary>
        /// 根据监测项目获取分析方法信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <returns></returns>
        public DataTable getAnalysisByItemId(string strItemId)
        {
            return access.getAnalysisByItemId(strItemId);
        }
        /// <summary>
        /// 获取质控详细信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfo(string strResultId, string strQcType)
        {
            return access.getQcDetailInfo(strResultId, strQcType);
        }
        /// <summary>
        /// 获取质控详细信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfo_QY(string strResultId, string strQcType, string strSubTaskID, string strItemID)
        {
            return access.getQcDetailInfo_QY(strResultId, strQcType, strSubTaskID, strItemID);
        }
        /// <summary>
        /// 获取质控详细信息(由于在清远监测分析环节的实验室空白需求添加多个样，所以获取数据需要扩展)
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfoEx(string strResultId, string strQcType)
        {
            return access.getQcDetailInfoEx(strResultId, strQcType);
        }
        /// <summary>
        /// 删除实验室质控样 Create By：weilin 2013-12-13
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strQcType">实验室质控类型</param>
        /// <returns></returns>
        public bool deleteQcAnalysis(string strID, string strQcType)
        {
            return access.deleteQcAnalysis(strID, strQcType);
        }
        /// <summary>
        /// 计算平行样均值与相对偏差
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strValue1">测定值1</param>
        /// <param name="strValue2">测定值2</param>
        /// <returns></returns>
        public string getQcTwinValue(string strResultId, string strValue1, string strValue2)
        {
            return access.getQcTwinValue(strResultId, strValue1, strValue2);
        }

        /// <summary>
        /// 计算平行样均值与相对偏差(清远) Create By weilin 2014-03-20
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strValue1">测定值1</param>
        /// <param name="strValue2">测定值2</param>
        /// <returns></returns>
        public string getQcTwinValueEx(string strResultId, string strValue1, string strValue2)
        {
            return access.getQcTwinValueEx(strResultId, strValue1, strValue2);
        }

        /// <summary>
        /// 将实验室质控数据保存至数据库
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="chkQcEmpty">实验室空白</param>
        /// <param name="strEmptyValue">空白值</param>
        /// <param name="strEmptyCount">空白个数</param>
        /// <param name="chkQcSt">标准样</param>
        /// <param name="strSrcResult">标准值</param>
        /// <param name="strStResult">测定值</param>
        /// <param name="chkQcAdd">实验室加标</param>
        /// <param name="strAddResultEx">测定值</param>
        /// <param name="strQcAdd">加标量</param>
        /// <param name="strAddBack">回收率</param>
        /// <param name="chkQcTwin">实验室明码平行</param>
        /// <param name="strTwinResult1">测定值1</param>
        /// <param name="strTwinResult2">测定值2</param>
        /// <param name="strTwinAvg">测定均值</param>
        /// <param name="strTwinOffSet">相对偏差</param>
        /// <returns></returns>
        public bool saveQcValue(string strResultId, string chkQcEmpty, string strEmptyValue, string strEmptyCount, string chkQcSt,
                                            string strSrcResult, string strStResult, string chkQcAdd, string strAddResultEx, string strQcAdd, string strAddBack,
                                            string chkQcTwin, string strTwinResult1, string strTwinResult2, string strTwinAvg, string strTwinOffSet)
        {
            return access.saveQcValue(strResultId, chkQcEmpty, strEmptyValue, strEmptyCount, chkQcSt,
                                             strSrcResult, strStResult, chkQcAdd, strAddResultEx, strQcAdd, strAddBack,
                                             chkQcTwin, strTwinResult1, strTwinResult2, strTwinAvg, strTwinOffSet);
        }

        /// <summary>
        /// 将实验室质控数据保存至数据库 Create By：weilin 2013-12-13
        /// </summary>
        /// <param name="strResultIds">结果ID(用于空白样和标准样)</param>
        /// <param name="chkQcEmpty">实验室空白</param>
        /// <param name="strEmptyValue">空白值</param>
        /// <param name="strEmptyCount">空白个数</param>
        /// <param name="chkQcSt">标准样</param>
        /// <param name="strSrcResult">标准值</param>
        /// <param name="strStResult">测定值</param>
        /// <param name="strAddResultId">加标样的结果ID</param>
        /// <param name="chkQcAdd">实验室加标</param>
        /// <param name="strAddResultEx">测定值</param>
        /// <param name="strQcAdd">加标量</param>
        /// <param name="strAddBack">回收率</param>
        /// <param name="strTwinResultId">明码平行的结果ID</param>
        /// <param name="chkQcTwin">实验室明码平行</param>
        /// <param name="strTwinResult1">测定值1</param>
        /// <param name="strTwinResult2">测定值2</param>
        /// <param name="strTwinAvg">测定均值</param>
        /// <param name="strTwinOffSet">相对偏差</param>
        /// <returns></returns>
        public bool saveQcValueEx(string strResultIds, string chkQcEmpty, string strEmptyValue1, string strEmptyValue2, string strEmptyValue3, string strEmptyValue, string strEmptyCount, string chkQcSt,
                                            string strSrcResult, string strStResult, string strSRC_IN_VALUE1, string strSRC_IN_VALUE2, string strSRC_IN_VALUE3, string strAddResultId, string chkQcAdd, string strAddResultEx, string strQcAdd, string strAddBack,
                                            string strTwinResultId, string chkQcTwin, string strTwinResult1, string strTwinResult2, string strTwinAvg, string strTwinOffSet)
        {
            return access.saveQcValueEx(strResultIds, chkQcEmpty, strEmptyValue1, strEmptyValue2, strEmptyValue3, strEmptyValue, strEmptyCount, chkQcSt,
                                             strSrcResult, strStResult, strSRC_IN_VALUE1, strSRC_IN_VALUE2, strSRC_IN_VALUE3, strAddResultId, chkQcAdd, strAddResultEx, strQcAdd, strAddBack,
                                             strTwinResultId, chkQcTwin, strTwinResult1, strTwinResult2, strTwinAvg, strTwinOffSet);
        }
        /// <summary>
        /// 将分析数据录入发送至下一个环节
        /// </summary>
        /// <param name="strSimpleId">样品id</param>
        /// <param name="strCurrentUserId">当前登录用户的ID</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool ResultSendToNext(string strSimpleId, string strCurrentUserId, string strCurrResultStatus, string strNextResultStatus)
        {
            return access.ResultSendToNext(strSimpleId, strCurrentUserId, strCurrResultStatus, strNextResultStatus);
        }

        /// <summary>
        /// 将分析数据录入发送至下一个环节
        /// </summary>
        /// <param name="strSimpleId">样品id</param>
        /// <param name="strCurrentUserId">当前登录用户的ID</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public string ResultSendToNext_QHD(string strSimpleIds, string strCurrentUserId, string strCurrResultStatus, string strNextResultStatus)
        {
            return access.ResultSendToNext_QHD(strSimpleIds, strCurrentUserId, strCurrResultStatus, strNextResultStatus);
        }

        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <param name="ItemCondition_WithIn">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResult(string strSubtaskID, string strTaskPointID, string ItemCondition_WithIn = "1")
        {
            return access.SelectSampleDeptResult(strSubtaskID, strTaskPointID, ItemCondition_WithIn);
        }
        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResult_QHD(string strSubtaskID, string strTaskPointID)
        {
            return access.SelectSampleDeptResult_QHD(strSubtaskID, strTaskPointID);
        }

        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <param name="strSAMPLEID">样品ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResultEx(string strSubtaskID, string strSAMPLEID)
        {
            return access.SelectSampleDeptResultEx(strSubtaskID, strSAMPLEID);
        }
        #endregion

        #region 分析结果录入方法【清远】
        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow_QY(string strCurrentUserId, string strResultStatus)
        {
            return access.getResultInResultFlow_QY(strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息记录数
        /// </summary>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public int getResultInResultFlowCount_QY(string strCurrentUserId, string strResultStatus)
        {
            return access.getResultInResultFlowCount_QY(strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 根据监测项目获取样品信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult_QY(string strItemId, string strCurrentUserId, string strResultStatus)
        {
            return access.getSimpleCodeInResult_QY(strItemId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 根据监测项目获取样品信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSampleCodeInResult_MAS(string strItemId)
        {
            return access.getSampleCodeInResult_MAS(strItemId);
        }

        /// <summary>
        /// 根据监测项目获取样品信息  (批量处理)  黄飞20150721
        /// </summary>
        /// <param name="strOneGridId">监测项目ID</param>
        /// <param name="strResultID">批量处理ID</param>
        /// <returns></returns>
        public DataTable getSampleCodeInResult_MAS_Batch(string strOneGridId, string strResultID)
        {
            return access.getSampleCodeInResult_MAS_Batch(strOneGridId, strResultID);
        }

        /// <summary>
        /// 将监测结果发送至下一个环节
        /// </summary>
        /// <param name="strSumResultId">监测结果ＩＤ总和</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strNextFlowUserId">要发送的用户ＩＤ</param>
        /// <param name="strNextResultStatus">下一环节结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public bool SendResultToNext_QY(string strSumResultId, string strCurrentUserId, string strNextFlowUserId, string strNextResultStatus)
        {
            return access.SendResultToNext_QY(strSumResultId, strCurrentUserId, strNextFlowUserId, strNextResultStatus);
        }
        /// <summary>
        /// 创建原因：按列更新列数据值
        /// 创建人：胡方扬
        /// 创建时间：2013-07-11
        /// </summary>
        /// <param name="strCellName"></param>
        /// <param name="strCellValue"></param>
        /// <param name="strInforId"></param>
        /// <returns></returns>
        public bool UpdateCellValue(string strCellName, string strCellValue, string strInforId)
        {
            return access.UpdateCellValue(strCellName, strCellValue, strInforId);
        }
        #endregion

        #region 分析结果录入方法【郑州】
        /// <summary>
        /// 设置郑州样品已经领取方法
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool setReceiveSample_ZZ(string strSampleId, string strCurrentUserId, string strResultStatus)
        {
            return access.setReceiveSample_ZZ(strSampleId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取样品已领取状态
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool getReceiveSampleStatus(string strSampleId, string strCurrentUserId, string strResultStatus)
        {
            return access.getReceiveSampleStatus(strSampleId, strCurrentUserId, strResultStatus);
        }
        /// <summary>
        /// 获取默认负责人信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <returns></returns>
        public string getEntire_QC(string strSampleId)
        {
            return access.getEntire_QC(strSampleId);
        }

        /// <summary>
        /// 将结果发送至下一个环节
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strCurrentResultStatus">当前环节结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <param name="strNextResultStatus">下一环节结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <returns></returns>
        public bool SendResultToNext_ZZ(string strResultId, string strCurrentUserId, string strCurrentResultStatus, string strNextResultStatus)
        {
            return access.SendResultToNext_ZZ(strResultId, strCurrentUserId, strCurrentResultStatus, strNextResultStatus);
        }
        #endregion

        #region 分析校核方法

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2012.12.10
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, int iIndex, int iCount)
        {
            return access.getTaskCheckInfo(strCurrentUserId, strFlowCode, strTaskStatus, iIndex, iCount);
        }

        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2012.12.10
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount(string strCurrentUserId, string strFlowCode, string strTaskStatus)
        {
            return access.getTaskInfoCheckCount(strCurrentUserId, strFlowCode, strTaskStatus);
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo(string strTaskId, string strCurrentUserId, string strFlowCode, int iIndex, int iCount)
        {
            return access.getTaskSimpleCheckInfo(strTaskId, strCurrentUserId, strFlowCode, iIndex, iCount);
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount(string strTaskId, string strCurrentUserId, string strFlowCode)
        {
            return access.getTaskSimpleCheckInfoCount(strTaskId, strCurrentUserId, strFlowCode);
        }
        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getTaskItemCheckInfo(string strUserId, string strSampleId, string strResultStatus, string iSample, string strMonitorID)
        {
            return access.getTaskItemCheckInfo(strUserId, strSampleId, strResultStatus, iSample, strMonitorID);
        }
        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getTaskItemCheckInfo_QY(string strUserId, string strSampleId, string strResultStatus)
        {
            return access.getTaskItemCheckInfo_QY(strUserId, strSampleId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果校核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskItemCheckInfoCount(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemCheckInfoCount(strSampleId, strResultStatus);
        }
        /// <summary>
        /// 判断任务是否可以发送
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool IsCanSendTaskCheckToNextFlow(string strTaskId, string strCurrentUserId, string strFlowCode, string strResultStatus)
        {
            return access.IsCanSendTaskCheckToNextFlow(strTaskId, strCurrentUserId, strFlowCode, strResultStatus);
        }
        /// <summary>
        /// 判断一个任务中的分析类现场项目的状态情况 Create By : weilin 2014-06-19
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strCurrentUserId"></param>
        /// <param name="strFlowCode"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool IsAnalySampleItemFlow(string strTaskId, string strResultStatus)
        {
            return access.IsAnalySampleItemFlow(strTaskId, strResultStatus);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">子任务状态，质控审核:04</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow(string strTaskId, string strCurrentUserId, string strTaskStatus, string strFlowCode)
        {
            return access.SendTaskCheckToNextFlow(strTaskId, strCurrentUserId, strTaskStatus, strFlowCode);
        }
        # endregion

        #region 分析数据审核（分析室主任审核）方法【秦皇岛】

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo_QHD(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, string iSample, int iIndex, int iCount)
        {
            return access.getTaskCheckInfo_QHD(strCurrentUserId, strFlowCode, strTaskStatus, strSampleStatus, strResultStatus, iSample, iIndex, iCount);
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount_QHD(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, string iSample)
        {
            return access.getTaskInfoCheckCount_QHD(strCurrentUserId, strFlowCode, strTaskStatus, strSampleStatus, strResultStatus, iSample);
        }
        public DataTable getSampleTaskCheck_QHD(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getSampleTaskCheck_QHD(strTaskStatus, strResultStatus, iIndex, iCount);
        }
        public int getSampleTaskCheckCount_QHD(string strTaskStatus, string strResultStatus)
        {
            return access.getSampleTaskCheckCount_QHD(strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfo_QHD(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            return access.geResultChecktItemTypeInfo_QHD(strCurrentUserId, strFlowCode, strTaskId, strTaskStatus, strSampleStatus, strResultStatus);
        }
        /// <summary>
        /// 获取监测类别信息 
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfoEx_QHD(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            return access.geResultChecktItemTypeInfoEx_QHD(strCurrentUserId, strFlowCode, strTaskId, strTaskStatus, strSampleStatus, strResultStatus);
        }

        public DataTable getSampleItemCheckMonitorType_QHD(string strTaskId, string strTaskStatus, string strResultStatus)
        {
            return access.getSampleItemCheckMonitorType_QHD(strTaskId, strTaskStatus, strResultStatus);
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo_QHD(string strSubTaskId, string strSampleStatus, string strResultStatus, int iIndex, int iCount, string iSample, string strMonitorID)
        {
            return access.getTaskSimpleCheckInfo_QHD(strSubTaskId, strSampleStatus, strResultStatus, iIndex, iCount, iSample, strMonitorID);
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount_QHD(string strSubTaskId, string strSampleStatus, string strResultStatus, string iSample, string strMonitorID)
        {
            return access.getTaskSimpleCheckInfoCount_QHD(strSubTaskId, strSampleStatus, strResultStatus, iSample, strMonitorID);
        }

        public DataTable getSampleCheckInfo_QHD(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            return access.getSampleCheckInfo_QHD(strSubTaskId, strResultStatus, iIndex, iCount);
        }
        public int getSampleCheckInfoCount_QHD(string strSubTaskId, string strResultStatus)
        {
            return access.getSampleCheckInfoCount_QHD(strSubTaskId, strResultStatus);
        }
        #endregion

        #region 分析数据审核（分析室主任审核）方法【清远】

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo_QY(string strCurrentUserId, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskCheckInfo_QY(strCurrentUserId, strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount_QY(string strCurrentUserId, string strTaskStatus, string strResultStatus)
        {
            return access.getTaskInfoCheckCount_QY(strCurrentUserId, strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfo_QY(string strCurrentUserId, string strTaskId, string strTaskStatus, string strResultStatus)
        {
            return access.geResultChecktItemTypeInfo_QY(strCurrentUserId, strTaskId, strTaskStatus, strResultStatus);
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo_QY(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskSimpleCheckInfo_QY(strSubTaskId, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount_QY(string strSubTaskId, string strResultStatus)
        {
            return access.getTaskSimpleCheckInfoCount_QY(strSubTaskId, strResultStatus);
        }
        #endregion

        #region 分析结果质控审核
        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskQcCheckInfo(strCurrentUserId, strFlowCode, strTaskStatus, strSampleStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            return access.getTaskInfoQcCheckCount(strCurrentUserId, strFlowCode, strTaskStatus, strSampleStatus, strResultStatus);
        }
        public DataTable getTaskQcCheckInfo_QHD(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskQcCheckInfo_QHD(strTaskStatus, strResultStatus, iIndex, iCount);
        }
        public int getTaskQcCheckInfoCount_QHD(string strTaskStatus, string strResultStatus)
        {
            return access.getTaskQcCheckInfoCount_QHD(strTaskStatus, strResultStatus);
        }
        public DataTable getMonitorQcCheckInfo_QHD(string strTaskID, string strTaskStatus, string strResultStatus)
        {
            return access.getMonitorQcCheckInfo_QHD(strTaskID, strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo(string strTaskId, string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskSimpleQcCheckInfo(strTaskId, strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount(string strTaskId, string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            return access.getTaskSimpleQcCheckInfoCount(strTaskId, strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus);
        }
        public DataTable getTaskSampleInfo(string strTaskID)
        {
            return access.getTaskSampleInfo(strTaskID);
        }

        public DataTable getTaskSampleInfo_One(string strTaskID)
        {
            return access.getTaskSampleInfo_One(strTaskID);
        }

        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemQcCheckInfo(strSampleId, strResultStatus);
        }
        public DataTable getTaskItemQcCheckInfo_MAS(string strResultID)
        {
            return access.getTaskItemQcCheckInfo_MAS(strResultID);
        }
        public DataTable getTaskItemQcCheckInfo(string strSampleID)
        {
            return access.getTaskItemQcCheckInfo(strSampleID);
        }

        public DataTable getTaskItemQcCheckInfo_One(string strSampleID)
        {
            return access.getTaskItemQcCheckInfo(strSampleID);
        }


        public DataTable getTaskItemQcCheckInfo_MAS_ONE(string strResultID, string strSampleId)
        {
            return access.getTaskItemQcCheckInfo_MAS_ONE(strResultID, strSampleId);
        }

        /// <summary>
        /// 获取分析结果质控审核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskItemCheckQcInfoCount(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemCheckQcInfoCount(strSampleId, strResultStatus);
        }

        /// <summary>
        /// 发送任务至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strSendStatus">发送至下一环节状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendQcTaskToNextFlow(string strTaskId, string strCurrentUserId, string strSendStatus, string strFlowCode)
        {
            return access.SendQcTaskToNextFlow(strTaskId, strCurrentUserId, strSendStatus, strFlowCode);
        }
        #endregion

        #region 分析结果质控审核【清远】
        /// <summary>
        /// 发送任务至下一环节 update by ssz
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strSendStatus">发送至下一环节状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendQcTaskToNextFlowForQy(string strTaskId, string strCurrentUserId, string strSendStatus, string strFlowCode)
        {
            return access.SendQcTaskToNextFlowForQy(strTaskId, strCurrentUserId, strSendStatus, strFlowCode);
        }

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：analysis_result_qc_check,分析室主任审核：analysis_result_check</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo_QY(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskQcCheckInfo_QY(strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：analysis_result_qc_check,分析室主任审核：analysis_result_check</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount_QY(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            return access.getTaskInfoQcCheckCount_QY(strCurrentUserId, strFlowCode, strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 将质控审核环节数据发送至下一环节
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <param name="strCurrResultStatus">当前环节。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strMark">0: 无限制更新；1：只更新分析类现场项目；2：只更新分析类项目</param>
        /// <returns></returns>
        public bool sendTaskQcCheckInfoToNext_QY(string strTaskId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus, string strType, string strMark)
        {
            return access.sendTaskQcCheckInfoToNext_QY(strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strType, strMark);
        }
        #endregion

        #region 技术室主任审核【秦皇岛】
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo_QHD(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskSimpleQcCheckInfo_QHD(strSubTaskId, strResultStatus, iIndex, iCount);
        }

        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount_QHD(string strSubTaskId, string strResultStatus)
        {
            return access.getTaskSimpleQcCheckInfoCount_QHD(strSubTaskId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo_QHD(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemQcCheckInfo_QHD(strSampleId, strResultStatus);
        }
        /// <summary>
        /// 判断任务是否可以发送至下环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool TaskCanSendInQcCheck_QHD(string strTaskId, string strCurrentUserId, string strResultStatus, bool b)
        {
            return access.TaskCanSendInQcCheck_QHD(strTaskId, strCurrentUserId, strResultStatus, b);
        }
        /// <summary>
        /// 判断任务是否可以发送到下一环节（状态不一致无法发送） Create By：weilin 2014-09-22
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strResultStatus"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsCanSendToNext(string strTaskId, string strResultStatus, bool b)
        {
            return access.IsCanSendToNext(strTaskId, strResultStatus, b);
        }
        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strBackResultStatus">上一环节结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool TaskGoBackInQcCheck_QHD(string strTaskId, string strCurrentUserId, string strFlowCode, string strSampleStatus, string strCurrResultStatus, string strBackResultStatus)
        {
            return access.TaskGoBackInQcCheck_QHD(strTaskId, strCurrentUserId, strFlowCode, strSampleStatus, strCurrResultStatus, strBackResultStatus);
        }
        public bool Update_Info(string strTaskId, string Rtn_Content)
        {
            return access.Update_Info(strTaskId, Rtn_Content);
        }
        #endregion

        #region 采样环节现场监测项目审核方法【郑州】
        /// <summary>
        /// 根据现场监测项目获取采样环节的任务信息
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页条码</param>
        /// <returns></returns>
        public DataTable getTaskInfoByDeptItem(string strCurrentUserId, string strFlowCode, string strTaskStatus, int iIndex, int iCount)
        {
            return access.getTaskInfoByDeptItem(strCurrentUserId, strFlowCode, strTaskStatus, iIndex, iCount);
        }
        /// <summary>
        /// 根据现场监测项目获取采样环节的任务信息数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <returns></returns>
        public int getTaskInfoByDeptItemCount(string strCurrentUserId, string strFlowCode, string strTaskStatus)
        {
            return access.getTaskInfoByDeptItemCount(strCurrentUserId, strFlowCode, strTaskStatus);
        }
        /// <summary>
        /// 根据现场监测项目获取采样环节的样品信息
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strCurrentUserId">任务状态</param>
        /// <returns></returns>
        public DataTable getSampleInfoByDeptItem(string strTaskId, string strFlowCode, string strCurrentUserId)
        {
            return access.getSampleInfoByDeptItem(strTaskId, strFlowCode, strCurrentUserId);
        }
        /// <summary>
        /// 获取现场项目审核环节中的现场项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <returns></returns>
        public DataTable getItemInfoByDeptItem(string strSampleId)
        {
            return access.getItemInfoByDeptItem(strSampleId);
        }
        /// <summary>
        /// 将现场项目审核任务发送【退回】给下环节【上环节】
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strCurrentSubTaskStatus"></param>
        /// <param name="strNextSubTaskStatus"></param>
        /// <returns></returns>
        public bool TaskGoBackInSampleResultQcCheck(string strTaskId, string strCurrentSubTaskStatus, string strNextSubTaskStatus)
        {
            return access.TaskGoBackInSampleResultQcCheck(strTaskId, strCurrentSubTaskStatus, strNextSubTaskStatus);
        }
        /// <summary>
        /// 根据任务设置现场采样项目在分析结果表中的状态
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strResultStatus">要设置的结果状态</param>
        /// <returns></returns>
        public bool SetSampleResultStatus(string strTaskId, string strSubTaskStatus, string strResultStatus)
        {
            return access.SetSampleResultStatus(strTaskId, strSubTaskStatus, strResultStatus);
        }
        #endregion

        #region 质量负责人审核【郑州】
        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo_ZZ(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskQcCheckInfo_ZZ(strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount_ZZ(string strTaskStatus, string strResultStatus)
        {
            return access.getTaskInfoQcCheckCount_ZZ(strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo_ZZ(string strTaskId, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            return access.getTaskSimpleQcCheckInfo_ZZ(strTaskId, strTaskStatus, strResultStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount_ZZ(string strTaskId, string strTaskStatus, string strResultStatus)
        {
            return access.getTaskSimpleQcCheckInfoCount_ZZ(strTaskId, strTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo_ZZ(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemQcCheckInfo_ZZ(strSampleId, strResultStatus);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskItemCheckQcInfoCount_ZZ(string strSampleId, string strResultStatus)
        {
            return access.getTaskItemCheckQcInfoCount_ZZ(strSampleId, strResultStatus);
        }
        /// <summary>
        /// 在质量负责人审核环节判断任务是否可以发送
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strResultStatus">结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <returns></returns>
        public bool TaskCanSendInQcManagerAudit_ZZ(string strTaskId, string strResultStatus)
        {
            return access.TaskCanSendInQcManagerAudit_ZZ(strTaskId, strResultStatus);
        }
        /// <summary>
        /// 质量负责人审核环节判断现场采样项目审核环节和分析流程是否已经全部完毕
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">现场项目审核完毕之后子任务状态【24】</param>
        /// <param name="strResultStatus">分析环节完毕之后结果状态【50】</param>
        /// <returns></returns>
        public bool allTaskIsFinish_ZZ(string strTaskId, string strSubTaskStatus, string strResultStatus)
        {
            return access.allTaskIsFinish_ZZ(strTaskId, strSubTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_ZZ(string strTaskId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus)
        {
            return access.SendTaskCheckToNextFlow_ZZ(strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus);
        }
        #endregion

        #region 样品分析通知单打印功能【郑州】
        /// <summary>
        /// 获取郑州样品分析通知单信息
        /// </summary>
        /// <param name="strSubTaskStatus1">样品交接状态</param>
        /// <param name="strSubTaskStatus2">分析环节状态</param>
        /// <returns></returns>
        public DataTable getSamplesOrderInfo_ZZ(string strSubTaskStatus1, string strSubTaskStatus2)
        {
            return access.getSamplesOrderInfo_ZZ(strSubTaskStatus1, strSubTaskStatus2);
        }
        /// <summary>
        /// 获取样品交接环节样品分析通知单信息
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public DataTable getSampleOrderInfo_ZZ(string strTaskId)
        {
            return access.getSampleOrderInfo_ZZ(strTaskId);
        }
        #endregion

        #region 任务状态查看【郑州】
        /// <summary>
        /// 获取分析任务分配情况状态情况
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public DataTable getAnalysisTaskAllocationViewSampleInfo(string strSubTaskStatus, string strResultStatus)
        {
            return access.getAnalysisTaskAllocationViewSampleInfo(strSubTaskStatus, strResultStatus);
        }
        /// <summary>
        /// 获取分析任务分配情况状态情况（监测项目）
        /// </summary>
        /// <param name="strSampeleId">样品ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public DataTable getAnalysisTaskAllocationViewResultInfo(string strSampeleId, string strResultStatus)
        {
            return access.getAnalysisTaskAllocationViewResultInfo(strSampeleId, strResultStatus);
        }
        #endregion

        /// <summary>
        /// 样品交接发送到监测分析环节【郑州】
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToAnalysis(string strTaskId, string strSubTaskStatus, string strCurrenteResultStatus, string strNextResultStatus)
        {
            return access.sendToAnalysis(strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus);
        }

        /// <summary>
        /// 获取现场质控和实验质控的详细信息 Create By weilin 2014-03-21
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <param name="strResultID"></param>
        /// <param name="strItemID"></param>
        /// <param name="strMark">0: 现场质控与实验质控 1：实验质控</param>
        /// <returns></returns>
        public DataTable GetQcInfo_QY(string strSubTaskID, string strResultID, string strItemID, string strMark)
        {
            return access.GetQcInfo_QY(strSubTaskID, strResultID, strItemID, strMark);
        }
        /// <summary>
        /// 更新某些样品的监测项目的结果值 Create By :weilin 2014-8-3
        /// </summary>
        /// <param name="strSampleIDs"></param>
        /// <param name="strItemID"></param>
        /// <param name="strItemResult"></param>
        /// <returns></returns>
        public bool FinishAllItem(string strSampleIDs, string strItemID, string strItemResult)
        {
            return access.FinishAllItem(strSampleIDs, strItemID, strItemResult);
        }
        /// <summary>
        /// 实验室质控时获取相关信息 Create By:weilin
        /// </summary>
        /// <param name="strResultIDs"></param>
        /// <returns></returns>
        public DataTable getItemInfoForQC(string strResultIDs)
        {
            return access.getItemInfoForQC(strResultIDs);
        }
        /// <summary>
        /// 根据样品获取任务
        /// </summary>
        /// <param name="strResultId"></param>
        /// <param name="strQcType"></param>
        /// <param name="strSubTaskID"></param>
        /// <param name="strItemID"></param>
        /// <returns></returns>
        public DataTable getTaskID(string strSubtask)
        {
            return access.getTaskID(strSubtask);
        }
        public DataTable getItemId(string strSubtask)
        {
            return access.getItemId(strSubtask);
        }
    }
}
