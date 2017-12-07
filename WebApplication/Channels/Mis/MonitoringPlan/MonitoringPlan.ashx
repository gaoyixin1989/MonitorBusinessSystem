<%@ WebHandler Language="C#" Class="MonitoringPlan" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Web.SessionState;
using System.Configuration;
using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Collections;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.River;

using i3.ValueObject.Channels.Mis.Monitor.Task;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：采样预约模块数据处理、调用方法
/// 创建时间：2012-12-18
/// 创建人：胡方扬
/// </summary>
public class MonitoringPlan : PageBase, IHttpHandler, IRequiresSessionState
{
    private string result = "", strMessage = "";
    private DataTable dt = new DataTable();
    private string strSortname = "", strSortorder = "", strAction = "";
    //业务
    private string strDate = "", strPlanId = "", task_id = "", strIfPlan = "", strTaskStatus = "", strTaskType = "", strSampleManagerId = "", strRemarks = "";
    private int intPageIndex = 0, intPageSize = 0;
    private string strMonitorId = "", strType = "", strUserId = "";
    private string strCompanyNameFrim = "", strAreaIdFrim = "", strContractCode = "", strCompanyId = "", strContractId = "", strContractType = "", strProjectName = "", strContratYear = "", strHaveContract = "";
    private string strFreqId = "", strPointId = "", strQulickly = "", strConfigKey = "", CONTRACT_TYPE = "";
    private string strWorkTask_Id = "", strAskFinishDate = "", strQcStatus = "", strAllQcStatus = "", strEvnTypeId = "", strPointItem = "", strPointItemName = "", strKeyColumns = "", strTableName = "", strFatherKeyColumn = "", strFatherKeyValue = "", SubKeyColumn = "", strYear = "", strPoint_Code = "", strPoint_Name = "", strPoint_Area = "", strConditionAndValue = "";
    private string strFlag = "", strHasDone = "", strTickeNum = "";
    private string strGridFatherTable = "", strGridFatherKeyColumn = "", strConKeyFatherColumn = "", strFatherTableName = "", strRiverArea = "", strValleyArea = "";
    private string strGridItemTable = "", strGridItemKeyColumn = "";
    private string strEnvTypeId = "", strEnvTypeName = "", strMonth = "", strPointQcSetting_Id = "", strAskingDate = "", strSendStatus = "";
    private string strDictCode = "", strDictType = "", strDictID = "";
    private string strLinkMan = "", strLINK_PHONE = "";
    private string strEnvYear = "", strEnvMonth = "";
    private string strSendDept = "", strState = "";
    private string strCCFLOW_WORKID = "", strSY = "";
    private string strEnvTypes = "", strRegionCode = "", strFunctional = "";
    //潘德军 2013-12-23
    private string strTaskNum = "";
    private string strFXS = "";

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID))
        {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
        GetRequest(context);
        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                //更新预约计划
                case "UpdateContractPlan":
                    context.Response.Write(UpdateContractPlan());
                    context.Response.End();
                    break;
                //按日期获取采样预约计划列表
                case "GetPlanListForDate":
                    context.Response.Write(GetPlanListForDate());
                    context.Response.End();
                    break;
                //获取指定监测类别的岗位职责信息
                case "GetMonitorDutyInfor":
                    context.Response.Write(GetMonitorDutyInfor());
                    context.Response.End();
                    break;
                //保存监测计划岗位
                case "SavePlanPeople":
                    context.Response.Write(SavePlanPeople());
                    context.Response.End();
                    break;
                //获取监测计划月度详细
                case "GetMonthPlan":
                    context.Response.Write(GetMonthPlan());
                    context.Response.End();
                    break;
                //获取监测计划月度详细
                case "GetWeekPlan":
                    context.Response.Write(GetWeekPlan());
                    context.Response.End();
                    break;
                //删除委托书预约计划
                case "DelContractPlan":
                    context.Response.Write(DelContractPlan());
                    context.Response.End();
                    break;
                case "DelTaskPlan":
                    context.Response.Write(DelTaskPlan());
                    context.Response.End();
                    break;
                //获取待预约的委托书点位频次信息
                case "GetContractPlanPointInfor":
                    context.Response.Write(GetContractPlanPointInfor());
                    context.Response.End();
                    break;
                //获取委托书监测点位信息
                case "GetPointInfors":
                    context.Response.Write(GetPointInfors());
                    context.Response.End();
                    break;
                // 按照监测频次获取委托书当前需要监测点位的信息  清远需求 胡方扬 2013-03-13
                case "GetPointInforsForFreq":
                    context.Response.Write(GetPointInforsForFreq());
                    context.Response.End();
                    break;
                //存监测预约计划
                case "SavePlan":
                    context.Response.Write(SavePlan());
                    context.Response.End();
                    break;
                //插入监测任务预约点位表信息
                case "SavePlanPoint":
                    context.Response.Write(SavePlanPoint());
                    context.Response.End();
                    break;
                //根据计划ID获取具体监测预约计划
                case "GetPlanPointSetted":
                    context.Response.Write(GetPlanPointSetted());
                    context.Response.End();
                    break;
                //获取指定监测预约计划的委托书信息
                case "GetContractInfor":
                    context.Response.Write(GetContractInfor());
                    context.Response.End();
                    break;
                //获取监测计划监测类型负责人信息
                case "GetContractDutyUser":
                    context.Response.Write(GetContractDutyUser());
                    context.Response.End();
                    break;
                //获取委托书下下有监测点位的所有监测类别 
                case "GetPointMonitorInfor":
                    context.Response.Write(GetPointMonitorInfor());
                    context.Response.End();
                    break;
                //获取委托书下预约办理环节的所有监测类别 
                case "GetPointMonitorInfoEx":
                    context.Response.Write(GetPointMonitorInfoEx());
                    context.Response.End();
                    break;
                //获取企业监测点位
                case "GetPointList":
                    context.Response.Write(GetPointList());
                    context.Response.End();
                    break;
                //根据任务下达日期，委托书等其他信息 获取计划列表
                case "GetPlanListForQuickly":
                    context.Response.Write(GetPlanListForQuickly());
                    context.Response.End();
                    break;
                //获取指定预约排口下的监测项目
                case "GetItemsForPoint":
                    context.Response.Write(GetItemsForPoint());
                    context.Response.End();
                    break;
                // 获取委托书已完成预约的监测点位频次信息
                case "GetPlanFinishedPointInfor":
                    context.Response.Write(GetPlanFinishedPointInfor());
                    context.Response.End();
                    break;
                // 获取web.config配置信息
                case "GetConfigSetting":
                    context.Response.Write(GetConfigSetting());
                    context.Response.End();
                    break;
                //获取指定条件下委托书监测计划任务列表（待预约的，已预约的）
                case "GetPendingPlanList":
                    context.Response.Write(GetPendingPlanList());
                    context.Response.End();
                    break;
                //更新监测计划任务下达日期
                case "UpdatePlan":
                    context.Response.Write(UpdatePlan());
                    context.Response.End();
                    break;
                case "UpdateTask":
                    context.Response.Write(UpdateTask());
                    context.Response.End();
                    break;
                case "GetPoint":
                    context.Response.Write(GetPoint());
                    context.Response.End();
                    break;
                case "GetPointForSome":
                    context.Response.Write(GetPointForSome());
                    context.Response.End();
                    break;
                case "GetPlanPointForEvn":
                    context.Response.Write(GetPlanPointForEvn());
                    context.Response.End();
                    break;
                case "GetPlanPointForEvnForCX":
                    context.Response.Write(GetPlanPointForEvnForCX());
                    context.Response.End();
                    break;
                case "GetPlanPoints":
                    context.Response.Write(GetPlanPoints());
                    context.Response.End();
                    break;
                //修改任务信息的要求完成时间
                case "SaveFinishData":
                    context.Response.Write(SaveFinishData());
                    context.Response.End();
                    break;
                //生成环境质量的监测计划 2013-05-03
                case "InsertEnvPlan":
                    context.Response.Write(InsertEnvPlan());
                    context.Response.End();
                    break;
                case "InsertEnvPlanByCreate":
                    context.Response.Write(InsertEnvPlanByCreate());
                    context.Response.End();
                    break;
                //生成环境质量监测垂线或测点 2013-05-03
                case "InsertEnvPlanPointItem":
                    context.Response.Write(InsertEnvPlanPointItem());
                    context.Response.End();
                    break;
                //生成环境质量点位、项目等信息 2013-05-06
                case "InsertEnvContractPoint":
                    context.Response.Write(InsertEnvContractPoint());
                    context.Response.End();
                    break;
                case "SaveDivEnvItemData":
                    context.Response.Write(SaveDivEnvItemData());
                    context.Response.End();
                    break;
                //生成环境质量监测任务
                case "doPlanTask":
                    context.Response.Write(doPlanTask());
                    context.Response.End();
                    break;
                //获取环境质量类监测任务
                case "GetEnvPlanTaskAbList":
                    context.Response.Write(GetEnvPlanTaskAbList());
                    context.Response.End();
                    break;
                //获取环境质量类监测任务详细
                case "GetEnvPlanTaskListDetail":
                    context.Response.Write(GetEnvPlanTaskListDetail());
                    context.Response.End();
                    break;
                //删除环境质量监测计划
                case "DelEnvTaskPlan":
                    context.Response.Write(DelEnvTaskPlan());
                    context.Response.End();
                    break;
                // 根据监测计划ID获取计划点位信息
                case "GetPointInforsForPlan":
                    context.Response.Write(GetPointInforsForPlan());
                    context.Response.End();
                    break;
                // 获取采样前质控任务列表
                case "GetPendingPlanListForAnyTaskStatus":
                    context.Response.Write(GetPendingPlanListForAnyTaskStatus());
                    context.Response.End();
                    break;
                // 获取预约办理任务列表
                case "GetPendingPlanListDoTask":
                    context.Response.Write(GetPendingPlanListDoTask());
                    context.Response.End();
                    break;
                // 获取已预约办理任务列表
                case "GetPendingPlanListDoTask_Done":
                    context.Response.Write(GetPendingPlanListDoTask_Done());
                    context.Response.End();
                    break;
                //获取指令性已经下达如流程办理的书
                case "GetPendingPlanListDoOrderTask":
                    context.Response.Write(GetPendingPlanListDoOrderTask());
                    context.Response.End();
                    break;
                //在指令性（常规）任务下，如果没有设置质控，则直接进行采样任务发布进入采样环节
                case "SendTask":
                    context.Response.Write(SendTask());
                    context.Response.End();
                    break;
                //返回已经设置了质控的环境质量类点位列表
                case "GetEnvQCSettingPointList":
                    context.Response.Write(GetEnvQCSettingPointList());
                    context.Response.End();
                    break;
                //返回符合条件的环境质控质控监测项目
                case "GetEnvQCSettingPointItemList":
                    context.Response.Write(GetEnvQCSettingPointItemList());
                    context.Response.End();
                    break;
                //插入选择的环境质量点位到质控点位表中
                case "InsertQcSettingPoint":
                    context.Response.Write(InsertQcSettingPoint());
                    context.Response.End();
                    break;
                //插入选择的环境质量点位到质控点位表中
                case "InsertQcSettingPointItems":
                    context.Response.Write(InsertQcSettingPointItems());
                    context.Response.End();
                    break;
                //动态获取环境质量监测点位的监测项目信息
                case "GetEnvPointItems":
                    context.Response.Write(GetEnvPointItems());
                    context.Response.End();
                    break;
                //获取指定质控点位下的监测项目信息
                case "GetEnvPointItemsForQcSetting":
                    context.Response.Write(GetEnvPointItemsForQcSetting());
                    context.Response.End();
                    break;
                //删除环境质量点位质控计划
                case "DelPointQcSetting":
                    context.Response.Write(DelPointQcSetting());
                    context.Response.End();
                    break;
                // 获取年份
                case "GetYear":
                    context.Response.Write(GetYear());
                    context.Response.End();
                    break;
                //获取月份
                case "GetMonth":
                    context.Response.Write(GetMonth());
                    context.Response.End();
                    break;
                //获取指定委托书的尚未预约监测点位
                case "GetContractPointList":
                    context.Response.Write(GetContractPointList());
                    context.Response.End();
                    break;
                case "InsertPointFreq":
                    context.Response.Write(InsertPointFreq());
                    context.Response.End();
                    break;
                //取指定指令性任务尚未预约的点位
                case "GetUnContractPointList":
                    context.Response.Write(GetUnContractPointList());
                    context.Response.End();
                    break;
                //插入指令性任务指定监测点位到监测点位频次表
                case "InsertUnContractPointFreq":
                    context.Response.Write(InsertUnContractPointFreq());
                    context.Response.End();
                    break;
                //取指定监测预约计划尚未预约的点位
                case "GetPlanPointList":
                    context.Response.Write(GetPlanPointList());
                    context.Response.End();
                    break;
                //取指定送样监测预约计划尚未预约的点位
                case "GetSamplePlanPointList":
                    context.Response.Write(GetSamplePlanPointList());
                    context.Response.End();
                    break;
                //设置任务质控状态
                case "SetTaskQcStatus":
                    context.Response.Write(SetTaskQcStatus());
                    context.Response.End();
                    break;
                //获取指定字典项名称
                case "GetDictName":
                    context.Response.Write(GetDictName());
                    context.Response.End();
                    break;
                //获取指定字典项名称
                case "GetDictNameForID":
                    context.Response.Write(GetDictNameForID());
                    context.Response.End();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 创建原因：获取前后年份
    /// 创建人：胡方扬
    /// 创建日期：2013-06-26
    /// </summary>
    /// <returns></returns>
    public string GetYear()
    {
        result = "";
        result = getYearInfo(3, 3);
        return result;
    }

    /// <summary>
    /// 创建原因：获取月份
    /// 创建人：胡方扬
    /// 创建日期：2013-06-26
    /// </summary>
    /// <returns></returns>
    public string GetMonth()
    {
        result = "";
        result = getMonthInfo();
        return result;
    }
    /// <summary>
    /// 获取web.config配置信息
    /// </summary>
    /// <returns></returns>
    private string GetConfigSetting()
    {
        string result = "";
        if (!String.IsNullOrEmpty(strConfigKey))
        {
            string strConfigApp = ConfigurationManager.AppSettings[strConfigKey].ToString();
            if (!String.IsNullOrEmpty(strConfigApp))
            {
                result = strConfigApp;
            }
        }
        return result;
    }

    /// <summary>
    /// 创建原因：根据字典编码 和字典类型 返回字典项名称
    /// 创建人：胡方扬
    /// 创建日期：2013-08-23
    /// </summary>
    /// <returns></returns>
    private string GetDictName()
    {
        result = "";
        result = PageBase.getDictName(strDictCode, strDictType);
        return result;
    }
    /// <summary>
    /// 创建原因：根据字典编码 和字典类型 返回字典项名称
    /// 创建人：胡方扬
    /// 创建日期：2013-08-23
    /// </summary>
    /// <returns></returns>
    private string GetDictNameForID()
    {
        result = "";
        TSysDictVo objDict = new TSysDictVo();
        objDict.ID = strDictID;
        DataTable objDt = new TSysDictLogic().SelectByTable(objDict);
        result = LigerGridDataToJson(objDt, objDt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取待预约的委托书点位频次信息
    /// </summary>
    /// <returns></returns>
    private string GetContractPlanPointInfor()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        TMisContractVo objCv = new TMisContractVo();
        TMisContractCompanyVo objCom = new TMisContractCompanyVo();
        objCv.CONTRACT_STATUS = "9";
        objCom.COMPANY_NAME = strCompanyNameFrim;
        objCom.AREA = strAreaIdFrim;
        objCv.CONTRACT_CODE = strContractCode;
        objCv.TEST_TYPES = strMonitorId;
        objCv.CONTRACT_TYPE = strContractType;
        if (!String.IsNullOrEmpty(strIfPlan))
        {
            objItems.IF_PLAN = strIfPlan;
            dt = new TMisContractPointFreqLogic().SelectContractPlanInfor(objItems, objCv, objCom, intPageIndex, intPageSize);
            int CountNum = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objItems, objCv, objCom);
            result = PageBase.LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }
    //更新监测计划
    private string UpdateContractPlan()
    {
        result = "";
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            if (!String.IsNullOrEmpty(strDate))
            {
                string[] dates = strDate.Split('-');
                objItems.PLAN_YEAR = dates[0].ToString();
                objItems.PLAN_MONTH = dates[1].ToString();
                objItems.PLAN_DAY = dates[2].ToString();
            }
            if (new TMisContractPlanLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑采样预约计划" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractPlanInfo, "", strMessage);
            }
        }
        return result;
    }

    //根据日期获取计划
    private string GetPlanListForDate()
    {
        result = "";
        dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }

        dt = new TMisContractPlanLogic().SelectByTableContractPlan(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    //根据预约日期，委托书等其他信息 获取计划列表
    private string GetPlanListForQuickly()
    {
        result = "";
        dt = new DataTable();
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.QC_STATUS = strQcStatus;
        objTask.TICKET_NUM = strTickeNum;

        TMisContractPlanVo objItems = new TMisContractPlanVo();
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }

        dt = new TMisContractPlanLogic().SelectByTableContractPlanForQuickly(objItems, objItemContract, objTask);
        int CountNum = new TMisContractPlanLogic().GetContractPlanForQuicklyCount(objItems, objItemContract, objTask);
        result = PageBase.LigerGridDataToJson(dt, CountNum);
        return result;
    }

    /// <summary>
    /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的）2013-04-01
    /// </summary>
    /// <returns></returns>
    private string GetPendingPlanList()
    {
        result = "";
        dt = new DataTable();

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.QC_STATUS = strQcStatus;
        objTask.TICKET_NUM = strTickeNum;
        objTask.TASK_TYPE = strTaskType;
        objTask.QC_STATUS = strQcStatus;
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }
        if (!String.IsNullOrEmpty(strType))
        {
            bool flag = Convert.ToBoolean(strType);

            //dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems, objItemContract,objTask,flag, intPageIndex, intPageSize);
            //int CountNum = new TMisContractPlanLogic().SelectByTableContractPlanForPendingCount(objItems, objItemContract,objTask,flag);
            dt = new TMisContractPlanLogic().SelectByTablePlanForTask(objItems, objItemContract, objTask, flag, intPageIndex, intPageSize);
            int CountNum = new TMisContractPlanLogic().GetSelectByTablePlanForTaskCount(objItems, objItemContract, objTask, flag);
            result = LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }

    /// <summary>
    /// 获取指定条件下监测计划任务列表（采样前质控的）2013-06-08
    /// </summary>
    /// <returns></returns>
    private string GetPendingPlanListForAnyTaskStatus()
    {
        result = "";
        dt = new DataTable();

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.TICKET_NUM = strTickeNum;
        objTask.QC_STATUS = strQcStatus;
        objTask.TASK_TYPE = strTaskType;
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }
        if (!String.IsNullOrEmpty(strType))
        {
            bool flag = Convert.ToBoolean(strType);
            dt = new TMisContractPlanLogic().SelectByTablePlanForTaskAnyTaskStatus(objItems, objItemContract, objTask, flag, intPageIndex, intPageSize);
            int CountNum = new TMisContractPlanLogic().SelectByTablePlanForTaskAnyTaskStatusCount(objItems, objItemContract, objTask, flag);
            result = LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }

    /// <summary>
    /// 获取指定条件下要进行预约办理的监测计划任务列表 2013-06-08
    /// </summary>
    /// <returns></returns>
    private string GetPendingPlanListDoTask()
    {
        result = "";
        dt = new DataTable();

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.TICKET_NUM = strTickeNum;
        objTask.QC_STATUS = strQcStatus;
        objTask.STATE = strState;
        objTask.TASK_TYPE = strTaskType;
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.HAS_DONE = strHasDone;
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }
        if (!String.IsNullOrEmpty(strType))
        {
            bool flag = Convert.ToBoolean(strType);
            dt = new TMisContractPlanLogic().SelectByTablePlanForDoTask(objItems, objItemContract, objTask, flag, intPageIndex, intPageSize);
            int CountNum = new TMisContractPlanLogic().GetSelectByTablePlanForDoTaskCount(objItems, objItemContract, objTask, flag);
            result = LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }
    /// <summary>
    /// 获取指定条件下已预约办理的监测计划任务列表 2014-09-30
    /// </summary>
    /// <returns></returns>
    private string GetPendingPlanListDoTask_Done()
    {
        result = "";
        dt = new DataTable();

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.TICKET_NUM = strTickeNum;
        objTask.QC_STATUS = strQcStatus;
        objTask.STATE = strState;
        objTask.TASK_TYPE = strTaskType;
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.HAS_DONE = strHasDone;
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }

        dt = new TMisContractPlanLogic().SelectByTablePlanForDoTask_Done(objItems, objItemContract, objTask, intPageIndex, intPageSize);
        int CountNum = new TMisContractPlanLogic().GetSelectByTablePlanForDoTaskCount_Done(objItems, objItemContract, objTask);
        result = LigerGridDataToJson(dt, CountNum);

        return result;
    }

    private string GetPendingPlanListDoOrderTask()
    {
        result = "";
        dt = new DataTable();

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.QC_STATUS = strQcStatus;
        objTask.TICKET_NUM = strTickeNum;
        objTask.TASK_TYPE = strTaskType;
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.HAS_DONE = strHasDone;
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.ISQUICKLY = strQulickly;
        objItemContract.CONTRACT_STATUS = "9";
        objItemContract.PROJECT_NAME = strProjectName;
        objItemContract.CONTRACT_CODE = strContractCode;
        objItemContract.CONTRACT_YEAR = strContratYear;
        objItemContract.CONTRACT_TYPE = strContractType;
        objItemContract.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItemContract.REMARK4 = strAreaIdFrim;
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }
        if (!String.IsNullOrEmpty(strType))
        {
            bool flag = Convert.ToBoolean(strType);
            dt = new TMisContractPlanLogic().SelectByTablePlanForDoOrderTask(objItems, objItemContract, objTask, flag, intPageIndex, intPageSize);
            int CountNum = new TMisContractPlanLogic().GetSelectByTablePlanForDoOrderTaskCount(objItems, objItemContract, objTask, flag);
            result = LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }
    /// <summary>
    /// 获取指定监测类别的岗位职责信息
    /// </summary>
    /// <returns></returns>
    public string GetMonitorDutyInfor()
    {
        result = "";
        dt = new DataTable();
        if (!String.IsNullOrEmpty(strMonitorId))
        {
            TSysDutyVo objItems = new TSysDutyVo();
            objItems.MONITOR_TYPE_ID = strMonitorId;
            objItems.DICT_CODE = strType;
            dt = new TSysDutyLogic().SelectTableDutyUser(objItems);
            result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 保存监测预约计划
    /// </summary>
    /// <returns></returns>
    private string SavePlan()
    {
        result = "";
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
            objItems.CONTRACT_ID = task_id;
            dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);
            objItems.CONTRACT_COMPANY_ID = strCompanyId;
            string[] strPlanDate = null;
            strPlanDate = strDate.Split('-');
            if (strPlanDate != null)
            {
                objItems.PLAN_YEAR = strPlanDate[0].ToString();
                objItems.PLAN_MONTH = strPlanDate[1].ToString();
                objItems.PLAN_DAY = strPlanDate[2].ToString();
            }
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && IsNumeric(dt.Rows[0]["NUM"].ToString()))
            {
                objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
            }
            else
            {
                objItems.PLAN_NUM = "1";
            }
            if (new TMisContractPlanLogic().Create(objItems))
            {
                result = objItems.ID;

                strMessage = LogInfo.UserInfo.USER_NAME + "新增采样预约计划" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanInfo, "", strMessage);
            }
        }
        return result;
    }

    private string UpdateTask()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = task_id;
        objTask.TICKET_NUM = strTaskNum;

        if (new TMisMonitorTaskLogic().Edit(objTask))
            return "true";
        else
            return "false";
    }

    /// <summary>
    /// 更新监测计划预约日期 胡方扬 2013-04-02
    /// </summary>
    /// <returns></returns>
    private string UpdatePlan()
    {
        result = "";
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            string[] strPlanDate = null;
            strPlanDate = strDate.Split('-');
            if (strPlanDate != null)
            {
                objItems.PLAN_YEAR = strPlanDate[0].ToString();
                objItems.PLAN_MONTH = strPlanDate[1].ToString();
                objItems.PLAN_DAY = strPlanDate[2].ToString();
            }
            if (new TMisContractPlanLogic().Edit(objItems))
            {
                TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
                objTask.QC_STATUS = "0";
                objTask.TICKET_NUM = strTaskNum;
                //潘德军 2013-12-23  任务单号可改，且初始不生成
                TMisMonitorTaskVo objtaskWhere = new TMisMonitorTaskVo();
                objtaskWhere.PLAN_ID = strPlanId;
                objtaskWhere.ID = strWorkTask_Id;

                //潘德军 2013-12-23  任务单号可改，且初始不生成
                //new TMisMonitorTaskLogic().Edit(objTask);
                new TMisMonitorTaskLogic().Edit(objTask, objtaskWhere);

                //潘德军 2013-12-23  任务单号可改，且初始不生成
                TMisMonitorTaskVo objTaskTmp = new TMisMonitorTaskLogic().SelectByObject(objtaskWhere);

                //潘德军 2013-12-23  任务单号可改，且初始不生成
                TMisMonitorReportVo objReportVoWhere = new TMisMonitorReportVo();
                objReportVoWhere.TASK_ID = objTaskTmp.ID;
                TMisMonitorReportVo objReportVoEdit = new TMisMonitorReportVo();
                objReportVoEdit.REPORT_CODE = objTask.TICKET_NUM;
                new TMisMonitorReportLogic().Edit(objReportVoEdit, objReportVoWhere);

                //潘德军2014-01-07 任务下达时，填写的联系人、联系电话，保存到受检企业信息
                TMisMonitorTaskCompanyVo tTaskTestCompanyVo = new TMisMonitorTaskCompanyVo();
                tTaskTestCompanyVo.ID = objTaskTmp.TESTED_COMPANY_ID;
                tTaskTestCompanyVo.CONTACT_NAME = strLinkMan;
                tTaskTestCompanyVo.PHONE = strLINK_PHONE;
                new TMisMonitorTaskCompanyLogic().Edit(tTaskTestCompanyVo);

                #region//委托任务统计表插数据
                TMisReportVo vo = new TMisReportVo();
                vo.ID = GetSerialNumber("TMisReport");
                vo.YEAR = DateTime.Now.Year.ToString();
                vo.MONTH = DateTime.Now.Month.ToString();
                vo.TICKET_NUM = strTaskNum;
                if (CONTRACT_TYPE == "01")//委托监测 WT
                {
                    vo.TYPE = "WT";
                }
                else if (CONTRACT_TYPE == "02")//YJ 应急
                {
                    vo.TYPE = "YJ";
                }
                else if (CONTRACT_TYPE == "03")//GL 管理
                {
                    vo.TYPE = "GL";
                }
                else if (CONTRACT_TYPE == "04")//自送样监测 CG
                {
                    vo.TYPE = "CG";
                }
                else if (CONTRACT_TYPE == "05")//验收监测 YS
                {
                    vo.TYPE = "YS";
                }
                new TMisMonitorTaskLogic().Creates(vo);
                #endregion
                result = "true";
            }
        }
        return result;
    }
    /// <summary>
    /// 插入监测任务预约点位表信息  胡方扬 2012-12-18
    /// </summary>
    /// <returns></returns>
    private string SavePlanPoint()
    {
        result = "";
        string[] strFreqArr = null, strPointArr = null;
        if (!String.IsNullOrEmpty(strFreqId))
        {
            strFreqArr = strFreqId.Split(';');
        }
        if (!String.IsNullOrEmpty(strPointId))
        {
            strPointArr = strPointId.Split(';');
        }

        if (!String.IsNullOrEmpty(task_id))
        {
            //有委托书的
            if (new TMisContractPlanPointLogic().SavePlanPoint(strFreqArr, strPointArr, strPlanId))
            {
                result = "true";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractPlanPointInfo, "", strMessage);
            }
        }
        else
        {
            //无委托书的
            if (new TMisContractPlanPointLogic().SaveSelectPlanPoint(strFreqArr, strPointArr, strPlanId, false))
            {
                result = "true";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractPlanPointInfo, "", strMessage);
            }

        }
        return result;
    }
    /// <summary>
    /// 保存监测计划岗位
    /// </summary>
    /// <returns></returns>
    private string SavePlanPeople()
    {
        result = "";
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objItems.CONTRACT_ID = task_id;
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            strMonitArr = strMonitorId.Split(';');
            strUserArr = strUserId.Split(';');
            if (strMonitArr != null)
                if (new TMisContractUserdutyLogic().SaveContractPlanDuty(objItems, strMonitArr, strUserArr))
                {
                    result = "true";

                    strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书" + objItems.CONTRACT_ID + "采样预约计划" + objItems.CONTRACT_PLAN_ID + "采样负责人成功";
                    WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanUserDutyInfo, "", strMessage);
                }
        }
        return result;
    }

    /// <summary>
    /// 获取监测计划月度详细
    /// </summary>
    /// <returns></returns>
    private string GetMonthPlan()
    {
        result = "";
        dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }

        dt = new TMisContractPlanLogic().SelectTableContractByMonth(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取监测计划月度详细
    /// </summary>
    /// <returns></returns>
    private string GetWeekPlan()
    {
        result = "";
        dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }

        dt = new TMisContractPlanLogic().SelectTableContractByWeek(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    //监测计划预约删除
    private string DelContractPlan()
    {
        result = "";
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objItems.ID = strPlanId;
            if (new TMisContractPlanLogic().Delete(objItems))
            {
                result = "true";
                TMisMonitorTaskVo TaskVo = new TMisMonitorTaskVo();
                TaskVo.PLAN_ID = strPlanId;
                new TMisMonitorTaskLogic().Delete(TaskVo);
                strMessage = LogInfo.UserInfo.USER_NAME + "删除采样预约计划" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractPlanInfo, "", strMessage);
            }
        }
        return result;
    }
    /// <summary>
    /// 删除已下达的监测任务
    /// </summary>
    /// <returns></returns>
    private string DelTaskPlan()
    {
        result = "";
        if (!String.IsNullOrEmpty(strPlanId))
        {
            if (new TMisMonitorTaskLogic().DelTaskTrans(strPlanId))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "删除已下达的监测任务" + strPlanId + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractPlanInfo, "", strMessage);
            }
        }
        return result;
    }

    /// <summary>
    /// 获取委托书监测点位信息
    /// </summary>
    /// <returns></returns>
    private string GetPointInfors()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.IF_PLAN = strIfPlan;
        objItems.CONTRACT_ID = task_id;

        dt = new TMisContractPointFreqLogic().GetPointInfor(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 按照监测频次获取委托书当前需要监测点位的信息  清远需求 胡方扬 2013-03-13
    /// </summary>
    /// <returns></returns>
    private string GetPointInforsForFreq()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.IF_PLAN = strIfPlan;
        objItems.CONTRACT_ID = task_id;

        dt = new TMisContractPointFreqLogic().GetPointInforForFreq(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取委托书已完成预约的监测点位频次信息
    /// </summary>
    /// <returns></returns>
    private string GetPlanFinishedPointInfor()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.IF_PLAN = strIfPlan;
        objItems.CONTRACT_ID = task_id;

        dt = new TMisContractPointFreqLogic().GetPlanFinishedPointInfor(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取点位排口下所有的监测项目信息
    /// </summary>
    /// <param name="tMisContractPointitem"></param>
    /// <returns></returns>
    private string GetItemsForPoint()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointitemVo objItems = new TMisContractPointitemVo();
        objItems.CONTRACT_POINT_ID = strPointId;

        dt = new TMisContractPointitemLogic().GetItemsForPoint(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取具体监测预约计划
    /// </summary>
    /// <returns></returns>
    private string GetPlanPointSetted()
    {
        result = "";
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.ID = strPlanId;
        dt = new TMisContractPlanLogic().GetPlanPointSetted(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取指定监测预约计划的委托书信息
    /// </summary>
    /// <returns></returns>
    private string GetContractInfor()
    {
        result = "";
        dt = new DataTable();
        TMisContractPlanVo objitems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objitems.ID = strPlanId;
            dt = new TMisContractPlanLogic().GetContractInfor(objitems);
            result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 获取监测计划监测类型负责人信息
    /// </summary>
    /// <returns></returns>
    private string GetContractDutyUser()
    {
        result = "";
        dt = new DataTable();
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        objItems.CONTRACT_ID = task_id;
        objItems.MONITOR_TYPE_ID = strMonitorId;
        objItems.CONTRACT_PLAN_ID = strPlanId;
        dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;

    }
    /// <summary>
    /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2012-12-21
    /// </summary>
    /// <returns></returns>
    private string GetPointMonitorInfor()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = task_id;
        //objItems.IF_PLAN = strIfPlan;
        dt = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, strPlanId);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    private string GetPointMonitorInfoEx()
    {
        result = "";
        dt = new DataTable();
        dt = new TMisContractPointFreqLogic().GetMonitorInfoForPlan(task_id, strPlanId, "01");
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    //获取点位信息
    public string GetPointList()
    {
        result = "";
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.IS_DEL = "0";
        objPoint.COMPANY_ID = strCompanyId;
        objPoint.POINT_TYPE = strContractType;
        DataTable dt = new TBaseCompanyPointLogic().SelectByTable(objPoint, intPageIndex, intPageSize);
        int intTotalCount = new TBaseCompanyPointLogic().GetSelectResultCount(objPoint);
        result = LigerGridDataToJson(dt, intTotalCount);
        return result;
    }

    /// <summary>
    /// 获取断面点位信息
    /// </summary>
    /// <returns></returns>
    public string GetPoint()
    {
        result = "";
        DataTable dt = new TMisContractPointLogic().GetPoint(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strPoint_Area, strRiverArea, strValleyArea, strTableName, strConditionAndValue, intPageIndex, intPageSize);
        int intTotalCount = new TMisContractPointLogic().GetPointCount(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strPoint_Area, strRiverArea, strValleyArea, strTableName, strConditionAndValue);
        result = LigerGridDataToJson(dt, intTotalCount);
        return result;
    }

    /// <summary>
    /// 获取断面点位信息方法二 只有名称和年份
    /// </summary>
    /// <returns></returns>
    public string GetPointForSome()
    {
        result = "";
        DataTable dt = new TMisContractPointLogic().GetPointForSome(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strRiverArea, strValleyArea, strTableName, strConditionAndValue, intPageIndex, intPageSize);
        int intTotalCount = new TMisContractPointLogic().GetPointForSomeCount(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strRiverArea, strValleyArea, strTableName, strConditionAndValue);
        result = LigerGridDataToJson(dt, intTotalCount);
        return result;
    }
    /// <summary>
    /// 动态获取环境质量类点位 2013-04-03
    /// </summary>
    /// <param name="strEvnPointId"></param>
    /// <returns></returns>
    private string GetPlanPointForEvn()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (strFatherKeyValue == "undefined")
            strFatherKeyValue = "";
        dt = new TMisContractPointLogic().GetEnvInforForDefineTable(strEnvTypeId, strEnvTypeName, strFatherKeyColumn, strFatherKeyValue, SubKeyColumn, strTableName, strEnvYear, strEnvMonth);
        result = LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 动态获取环境质量类点位 2013-04-03
    /// </summary>
    /// <param name="strEvnPointId"></param>
    /// <returns></returns>
    private string GetPlanPointForEvnForCX()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (strFatherKeyValue == "undefined")
            strFatherKeyValue = "";
        dt = new TMisContractPointLogic().GetEnvInforForDefineTable(strEnvTypeId, strEnvTypeName, strFatherKeyColumn, strFatherKeyValue, SubKeyColumn, strTableName, strGridFatherTable, strGridFatherKeyColumn, strEnvYear, strEnvMonth);
        result = LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取常规监测任务的点位信息 Create By : weilin 2014-11-15
    /// </summary>
    /// <param name="PlanId"></param>
    /// <param name="TypeId"></param>
    /// <returns></returns>
    private string GetPlanPoints()
    {
        string result = "";
        DataTable dt = new DataTable();

        dt = new TMisContractPointLogic().GetPlanPoints(strPlanId, strEnvTypeId);

        result = LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 生成环境质量的监测计划 2013-05-03
    /// </summary>
    /// <returns></returns>
    public string InsertEnvPlan()
    {
        result = "";
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.ID = GetSerialNumber("t_mis_contract_planId");
        objItems.PLAN_TYPE = strEvnTypeId;
        if (!string.IsNullOrEmpty(strDate))
        {
            string[] DateArr = strDate.Split('-');
            if (DateArr.Length > 0)
            {
                objItems.PLAN_YEAR = DateArr[0];
                objItems.PLAN_MONTH = DateArr[1];
                objItems.PLAN_DAY = DateArr[2];
            }
        }

        if (new TMisContractPlanLogic().Create(objItems))
        {
            if (InsertEnvPlanPoint(objItems.ID))
            {
                result = objItems.ID;
            }
        }
        return result;
    }

    /// <summary>
    /// 生成环境质量的监测计划 2013-05-03
    /// </summary>
    /// <returns>返回监测计划ID</returns>
    public string InsertEnvPlanByCreate()
    {

        result = "";
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.CCFLOW_ID1 = strCCFLOW_WORKID;
        new TMisContractPlanLogic().Delete(objItems);//删除之前同一CCFLOWID的数据
        objItems.ID = GetSerialNumber("t_mis_contract_planId");
        objItems.PLAN_TYPE = strEnvTypeId;
        objItems.REAMRK1 = strSY;//是否送样
        objItems.REAMRK2 = strRemarks;

        if (!string.IsNullOrEmpty(strDate))
        {
            string[] DateArr = strDate.Split('-');
            if (DateArr.Length > 0)
            {
                objItems.PLAN_YEAR = DateArr[0];
                objItems.PLAN_MONTH = DateArr[1];
                objItems.PLAN_DAY = DateArr[2];
            }
        }
        //地表饮用水源地：冀承环测字-YDB15-01
        if (new TMisContractPlanLogic().Create(objItems))
        {
            //string id = objItems.ID;
            ////生成任务编号 黄进军 添加 20150416
            //TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            //objSerialTask.SERIAL_TYPE = "4";
            //string num_code = PageBase.CreateBaseDefineCode(objSerialTask);
            //string num = num_code + "号";
            //result = id + "," + num;

            //地表饮用水源地：冀承环测字-YDB15-01 黄飞 添加 20151211  冀承环测字-YH15-09
            string id = objItems.ID;
            TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            objSerialTask.SERIAL_TYPE = "4";
            string num_code = "";
            if (strEnvTypes.Equals("土壤"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-土壤";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
            }
            else if (strEnvTypes.Equals("废气重点污染源企业"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-废气";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
            }
            else if (strEnvTypes.Equals("废水重点污染源企业") || strEnvTypes.Equals("污水厂污染源企业") || strEnvTypes.Equals("重金属污染源企业"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-废水";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
            }
            else if (strEnvTypes.Equals("降水"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-降水";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
            }
            else if (strEnvTypes.Equals("区域噪声环境") || strEnvTypes.Equals("道路交通噪声"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-交区噪声";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
                string strRegion = Transferred(strEnvTypes);
                num_code = num_code.Replace("H", strRegion);
            }

            else if (strEnvTypes.Equals("功能区噪声"))
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号-功能噪声";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
                int intLast = num_code.LastIndexOf("-");
                int a = int.Parse(num_code.Substring(intLast + 1, 2));
                num_code = num_code.Substring(0, intLast);
                if (a <= 3)
                {
                    num_code = num_code + "-01";
                }
                else if (3 < a && a <= 6)
                {
                    num_code = num_code + "-02";
                }
                else if (6 < a && a <= 9)
                {
                    num_code = num_code + "-03";
                }
                else
                {
                    num_code = num_code + "-04";
                }


                //num_code = num_code.Replace("H-", strFunctionalOne);
            }
            else
            {
                objSerialTask.SERIAL_NAME = "常规监测任务单号";
                num_code = PageBase.CreateBaseDefineCode(objSerialTask);
                string strNum = Transferred(strEnvTypes);
                num_code = num_code.Replace("H", strNum);
            }
            result = id + "," + num_code;


        }
        return result;
    }

    /// <summary>
    /// 插入环境质量计划点位信息
    /// </summary>
    /// <param name="strPlanId"></param>
    /// <returns></returns>
    public bool InsertEnvPlanPoint(string strPlanId)
    {
        bool flag = false;
        TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
        //objItems.ID = GetSerialNumber("t_mis_contract_planpointId");
        objItems.PLAN_ID = strPlanId;
        objItems.CONTRACT_POINT_ID = strPointId;
        if (new TMisContractPlanPointLogic().CreateDefine(objItems))
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 插入环境质量类点位信息 点位信息点位+垂线名称
    /// </summary>
    /// <returns>true Or false</returns>
    public bool InsertEnvContractPoint()
    {
        bool flag = false;
        TMisContractPointVo objItems = new TMisContractPointVo();
        objItems.MONITOR_ID = strEvnTypeId;
        objItems.POINT_ID = strPointItem;
        objItems.POINT_NAME = strPointItemName;
        if (new TMisContractPointLogic().InsertEnvPoints(objItems, strPlanId, strKeyColumns, strTableName))
        {
            SavePlanPeopleForEnv(strPlanId);
            flag = true;
            //if (doPlanTask())
            //{
            //    flag = true;
            //}
        }
        return flag;
    }
    /// <summary>
    /// 更新保存环境质量类的监测项目 Create By：weilin 2014-11-16
    /// </summary>
    /// <returns></returns>
    public string SaveDivEnvItemData()
    {
        TMisContractPointitemVo objPointItemVo = new TMisContractPointitemVo();
        objPointItemVo.CONTRACT_POINT_ID = strPointId;
        objPointItemVo.ITEM_ID = strPointItem;
        if (new TMisContractPointitemLogic().InsertEnvPointItemsEx(objPointItemVo))
        {
            return "true";
        }
        return "";
    }

    /// <summary>
    /// 根据环境类获取相对应的字符串
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public string Transferred(string a)
    {
        string num = "";
        switch (a)
        {
            case "地表水":
                num = "CDB";
                break;
            case "道路交通噪声":
                num = "JZ";
                break;
            case "区域噪声环境":
                num = "QZ";
                break;
            case "地表水饮用水源地":
                num = "YDB";
                break;
            case "生态补偿":
                num = "DM";
                break;
            case "湖库":
                num = "HQ";
                break;
            case "地下水":
                num = "CDX";
                break;
            case "地下水饮用水源地":
                num = "YDX";
                break;
            case "环境空气":
                num = "HJ";
                break;
            default:
                num = "WZ";
                break;
        }

        return num;
    }

    public string TransferredCode(string a)
    {
        string num = "";
        switch (a)
        {
            case "承德市":
                num = "130800";
                break;
            case "市辖区":
                num = "130801";
                break;
            case "双桥区":
                num = "130802";
                break;
            case "双滦区":
                num = "130803";
                break;
            case "鹰手营子矿区":
                num = "130804";
                break;
            case "承德县":
                num = "130821";
                break;
            case "兴隆县":
                num = "130822";
                break;
            case "平泉县":
                num = "130823";
                break;
            case "滦平县":
                num = "130824";
                break;
            case "隆化县":
                num = "130825";
                break;
            case "丰宁满族自治县":
                num = "130826";
                break;
            case "宽城满族自治县":
                num = "130827";
                break;
            case "围场满族蒙古族自治县":
                num = "130828";
                break;
            case "0类功能区":
                num = "30";
                break;
            case "1类功能区":
                num = "31";
                break;
            case "2类功能区":
                num = "32";
                break;
            case "3类功能区":
                num = "33";
                break;
            case "4a类功能区":
                num = "34";
                break;
            case "4b类功能区":
                num = "35";
                break;
        }

        return num;
    }

    /// <summary>
    /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetPointMonitorInfor(string strPlanId)
    {

        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.IF_PLAN = "0";
        dt = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, strPlanId);
        return dt;
    }

    /// <summary>
    /// 获取指定监测类别的岗位职责信息 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetMonitorDutyInforTable()
    {
        DataTable dt = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        objItems.DICT_CODE = "duty_sampling";
        dt = new TSysDutyLogic().SelectTableDutyUser(objItems);
        return dt;
    }

    /// <summary>
    /// 保存默认监测计划岗位负责人 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public void SavePlanPeopleForEnv(string strPlanId)
    {
        DataTable dtMonitor = new DataTable();
        TMisContractPointFreqVo objItemspan = new TMisContractPointFreqVo();
        objItemspan.IF_PLAN = "0";
        dtMonitor = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItemspan, strPlanId);

        //DataTable dtMonitor = GetPointMonitorInfor(strPlanId);

        DataTable dtTemple = new DataTable();
        TSysDutyVo objItemspan1 = new TSysDutyVo();
        objItemspan1.DICT_CODE = "duty_sampling";
        dtTemple = new TSysDutyLogic().SelectTableDutyUser(objItemspan1);
        //DataTable dtTemple = GetMonitorDutyInforTable();
        DataTable dtMonitorDutyUser = new DataTable();
        dtMonitorDutyUser = dtTemple.Copy();

        dtMonitorDutyUser.Clear();
        //获取默认负责人
        for (int n = 0; n < dtMonitor.Rows.Count; n++)
        {
            DataRow[] drowArr = dtTemple.Select(" IF_DEFAULT='0' AND MONITOR_TYPE_ID='" + dtMonitor.Rows[0]["ID"].ToString() + "'");
            if (drowArr.Length > 0)
            {
                foreach (DataRow drow in drowArr)
                {
                    dtMonitorDutyUser.ImportRow(drow);
                }
                dtMonitorDutyUser.AcceptChanges();
            }
            else
            {
                drowArr = dtTemple.Select(" MONITOR_TYPE_ID='" + dtMonitor.Rows[0]["ID"].ToString() + "'");
                if (drowArr.Length > 0)
                {
                    dtMonitorDutyUser.ImportRow(drowArr[0]);
                }
                dtMonitorDutyUser.AcceptChanges();
            }
        }

        //if (drowArr.Length > 0)
        //{
        //    foreach (DataRow drow in drowArr)
        //    {
        //        dtMonitorDutyUser.ImportRow(drow);
        //    }
        //    dtMonitorDutyUser.AcceptChanges();
        //}
        string strMonitorId = "", strUserId = "";
        foreach (DataRow drr in dtMonitor.Rows)
        {
            for (int i = 0; i < dtMonitorDutyUser.Rows.Count; i++)
            {
                if (drr["ID"].ToString() == dtMonitorDutyUser.Rows[i]["MONITOR_TYPE_ID"].ToString())
                {
                    strMonitorId += drr["ID"].ToString() + ";";
                    strUserId += dtMonitorDutyUser.Rows[i]["USERID"].ToString() + ";";
                }
            }
        }
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            if (!String.IsNullOrEmpty(strMonitorId) && !String.IsNullOrEmpty(strUserId))
            {
                strMonitArr = strMonitorId.Substring(0, strMonitorId.Length - 1).Split(';');
                strUserArr = strUserId.Substring(0, strUserId.Length - 1).Split(';');
                if (strMonitArr != null && strUserArr != null)
                    new TMisContractUserdutyLogic().SaveContractPlanDutyForEnv(objItems, strMonitArr, strUserArr);

            }
        }
    }
    /// <summary>
    /// 获取环境监测类任务计划
    /// </summary>
    /// <returns></returns>
    public string GetEnvPlanTaskAbList()
    {
        result = "";
        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.PROJECT_NAME = strProjectName;
        objItems.TICKET_NUM = strTickeNum;
        if (!String.IsNullOrEmpty(strSendStatus))
        {
            objItems.SEND_STATUS = strSendStatus;
        }
        if (!String.IsNullOrEmpty(strQcStatus))
        {
            objItems.QC_STATUS = strQcStatus;
        }
        objItems.ID = strWorkTask_Id;
        objItems.PLAN_ID = strPlanId;
        TMisContractPlanVo objPlan = new TMisContractPlanVo();
        objPlan.PLAN_TYPE = strContractType;
        if (!String.IsNullOrEmpty(strHasDone))
        {
            objPlan.HAS_DONE = strHasDone;
        }

        if (!String.IsNullOrEmpty(strDate))
        {
            string[] strPlanDate = null;
            strPlanDate = strDate.Split('-');
            if (strPlanDate != null)
            {
                objPlan.PLAN_YEAR = strPlanDate[0].ToString();
                objPlan.PLAN_MONTH = strPlanDate[1].ToString();
                objPlan.PLAN_DAY = strPlanDate[2].ToString();
            }
        }
        DataTable dt = new TMisMonitorTaskLogic().SelectEnvPlanTaskAbList(objItems, objPlan, intPageIndex, intPageSize);
        int CountNum = new TMisMonitorTaskLogic().SelectEnvPlanTaskAbListCount(objItems, objPlan);

        result = LigerGridDataToJson(dt, CountNum);
        return result;
    }

    /// <summary>
    /// 获取环境监测类任务计划详细
    /// </summary>
    /// <returns></returns>
    public string GetEnvPlanTaskListDetail()
    {
        result = "";
        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.ID = strWorkTask_Id;
        objItems.PLAN_ID = strPlanId;
        DataTable dt = new TMisMonitorTaskLogic().SelectEnvPlanTaskList(objItems, intPageIndex, intPageSize);
        int CountNum = new TMisMonitorTaskLogic().SelectEnvPlanTaskListCount(objItems);

        result = LigerGridDataToJson(dt, CountNum);
        return result;
    }
    /// <summary>
    /// 插入环境质量类监测点位及其垂线/测点
    /// </summary>
    /// <returns></returns>
    public bool InsertEnvPlanPointItem()
    {
        bool flag = false;
        TMisContractPointitemVo objItems = new TMisContractPointitemVo();
        objItems.CONTRACT_POINT_ID = strPointId;
        objItems.ITEM_ID = strPointItem;
        if (new TMisContractPointitemLogic().InsertEnvPointItems(objItems))
        {
            flag = true;
        }
        return flag;
    }
    /// <summary>
    /// 修改任务信息
    /// </summary>
    /// <returns></returns>
    public bool SaveFinishData()
    {
        bool flag = false;
        if (!String.IsNullOrEmpty(strWorkTask_Id))
        {
            TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
            objItems.ID = strWorkTask_Id;
            objItems.QC_STATUS = strQcStatus;
            objItems.STATE = strSendDept;
            objItems.ALLQC_STATUS = strAllQcStatus;
            objItems.ASKING_DATE = strAskFinishDate;
            objItems.CREATOR_ID = LogInfo.UserInfo.ID;
            objItems.CREATE_DATE = DateTime.Now.ToString();
            if (new TMisMonitorTaskLogic().Edit(objItems))
            {
                flag = true;

            }

        }
        return flag;
    }

    /// <summary>
    /// 生成环境质量类任务 胡方扬 2013-05-06
    /// </summary>
    /// <returns></returns>
    public bool doPlanTask()
    {
        bool strReturn = false;
        string strCodeRule = "";
        //预约表对象
        TMisContractPlanVo objContractPlan = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objContractPlan = new TMisContractPlanLogic().Details(strPlanId);
        }
        if (objContractPlan != null)
        {
            //获取委托书信息

            #region 构造监测任务对象
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.ID = GetSerialNumber("t_mis_monitor_taskId");
            objTask.PLAN_ID = strPlanId;
            objTask.PROJECT_NAME = strProjectName;
            objTask.CREATOR_ID = LogInfo.UserInfo.ID;
            objTask.CREATE_DATE = DateTime.Now.ToString();
            objTask.TASK_STATUS = "01";
            objTask.STATE = strSendDept;
            objTask.TASK_TYPE = "1";
            objTask.CONTRACT_TYPE = "07";//07表示环境质量类
            objTask.COMFIRM_STATUS = "0";
            objTask.ASKING_DATE = strAskingDate;
            objTask.QC_STATUS = strQcStatus;
            objTask.REMARK2 = strRemarks;//黄进军 add 20150917
            if (objContractPlan.REAMRK1 == "1")
                objTask.SAMPLE_SOURCE = "送样";
            else
                objTask.SAMPLE_SOURCE = "抽样";
            objTask.SAMPLE_SEND_MAN = strState;
            objTask.REMARK3 = strFXS;
            objTask.CCFLOW_ID1 = objContractPlan.CCFLOW_ID1;
            objTask.REMARK1 = objContractPlan.REAMRK1;
            TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            objSerialTask.SERIAL_TYPE = "4";

            //潘德军 2013-12-23 任务单号可改，且不自动生成
            objTask.TICKET_NUM = strTaskNum;
            //objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask);
            //生成委托书单号
            TMisContractVo objtCv = new TMisContractVo();
            objtCv.CONTRACT_TYPE = "07";
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SERIAL_TYPE = "1";
            strCodeRule = CreateBaseDefineCode(objSerial, objtCv);
            objTask.CONTRACT_CODE = strCodeRule;

            #endregion
            #region 监测子任务信息 根据委托书监测类别进行构造
            //监测子任务信息 根据委托书监测类别进行构造
            ArrayList arrSubTask = new ArrayList();//监测子任务集合
            ArrayList arrSubTaskApp = new ArrayList();
            ArrayList arrTaskPoint = new ArrayList();//监测点位集合
            ArrayList arrPointItem = new ArrayList();//监测点位明细集合
            ArrayList arrSample = new ArrayList();//样品集合
            ArrayList arrSampleResult = new ArrayList();//样品结果集合 
            ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表
            #endregion

            //预约点位
            DataTable dtPoint = new TMisContractPointLogic().SelectPointTable(strPlanId);
            //获取预约点位明细信息
            DataTable dtContractPoint = new TMisContractPointLogic().GetPlanEnvListTable(strPlanId);
            //监测子任务

            #region 监测子任务
            //监测子任务
            string[] strEnvArry = strEnvTypeId.Split(';');
            if (strEnvArry.Length > 0)
            {
                foreach (string str in strEnvArry)
                {
                    TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
                    string strSampleManagerID = "";//采样负责人ID
                    string strSampleID = "";//采样协同人ID串
                    objSubtask.ID = GetSerialNumber("t_mis_monitor_subtaskId");
                    objSubtask.TASK_ID = objTask.ID;
                    objSubtask.MONITOR_ID = str;
                    objSubtask.SAMPLING_MANAGER_ID = strSampleManagerID;
                    objSubtask.SAMPLING_ID = strSampleID;
                    objSubtask.TASK_TYPE = "发送";
                    objSubtask.TASK_STATUS = "01";
                    arrSubTask.Add(objSubtask);

                    TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
                    objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                    objSubApp.SUBTASK_ID = objSubtask.ID;
                    arrSubTaskApp.Add(objSubApp);
            #endregion

                    #region 按类别分点位
                    //按类别分点位
                    DataRow[] dtTypePoint = dtPoint.Select("MONITOR_ID='" + str + "'");
                    if (dtTypePoint.Length > 0)
                    {
                        string strTaskPointIDs = GetSerialNumberList("t_mis_monitor_taskpointId", dtTypePoint.Length);
                        string[] arrTaskPointIDs = strTaskPointIDs.Split(',');
                        string strSampleIDs = GetSerialNumberList("MonitorSampleId", dtTypePoint.Length);
                        string[] arrSampleIDs = strSampleIDs.Split(',');

                        for (int j = 0; j < dtTypePoint.Length; j++)
                        {
                            DataRow drPoint = dtTypePoint[j];
                            #region 监测点位
                            // 监测点位 
                            TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
                            objTaskPoint.ID = arrTaskPointIDs[j];
                            objTaskPoint.TASK_ID = objTask.ID;
                            objTaskPoint.SUBTASK_ID = objSubtask.ID;
                            objTaskPoint.POINT_ID = drPoint["POINT_ID"].ToString();
                            objTaskPoint.CONTRACT_POINT_ID = drPoint["ID"].ToString();
                            objTaskPoint.POINT_NAME = drPoint["POINT_NAME"].ToString();
                            objTaskPoint.DYNAMIC_ATTRIBUTE_ID = drPoint["DYNAMIC_ATTRIBUTE_ID"].ToString();
                            objTaskPoint.ADDRESS = drPoint["ADDRESS"].ToString();
                            objTaskPoint.LONGITUDE = drPoint["LONGITUDE"].ToString();
                            objTaskPoint.LATITUDE = drPoint["LATITUDE"].ToString();
                            objTaskPoint.FREQ = drPoint["FREQ"].ToString();
                            objTaskPoint.DESCRIPTION = drPoint["DESCRIPTION"].ToString();
                            objTaskPoint.NATIONAL_ST_CONDITION_ID = drPoint["NATIONAL_ST_CONDITION_ID"].ToString();
                            objTaskPoint.INDUSTRY_ST_CONDITION_ID = drPoint["INDUSTRY_ST_CONDITION_ID"].ToString();
                            objTaskPoint.LOCAL_ST_CONDITION_ID = drPoint["LOCAL_ST_CONDITION_ID"].ToString();
                            objTaskPoint.IS_DEL = "0";
                            objTaskPoint.NUM = drPoint["NUM"].ToString();
                            objTaskPoint.CREATE_DATE = DateTime.Now.ToString();
                            arrTaskPoint.Add(objTaskPoint);
                            #endregion

                            #region 样品
                            //样品 与点位对应
                            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                            objSampleInfo.ID = arrSampleIDs[j];
                            objSampleInfo.SUBTASK_ID = objSubtask.ID;
                            objSampleInfo.POINT_ID = objTaskPoint.ID;
                            objSampleInfo.SAMPLE_CODE = drPoint["REMARK5"].ToString();
                            objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                            objSampleInfo.ENV_MONTH = drPoint["REMARK4"].ToString();
                            objSampleInfo.QC_TYPE = "0";//默认原始样
                            objSampleInfo.NOSAMPLE = "0";//默认未采样
                            arrSample.Add(objSampleInfo);
                            #endregion

                            //点位采用的标准条件项ID
                            string strConditionID = "";
                            if (!string.IsNullOrEmpty(objTaskPoint.NATIONAL_ST_CONDITION_ID))
                            {
                                strConditionID = objTaskPoint.NATIONAL_ST_CONDITION_ID;
                            }
                            if (!string.IsNullOrEmpty(objTaskPoint.LOCAL_ST_CONDITION_ID))
                            {
                                strConditionID = objTaskPoint.LOCAL_ST_CONDITION_ID;
                            }
                            if (!string.IsNullOrEmpty(objTaskPoint.INDUSTRY_ST_CONDITION_ID))
                            {
                                strConditionID = objTaskPoint.INDUSTRY_ST_CONDITION_ID;
                            }

                            //预约项目明细
                            DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
                            if (dtPointItem.Length > 0)
                            {
                                string strTaskItemIDs = GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
                                string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
                                string strSampleResultIDs = GetSerialNumberList("MonitorResultId", dtPointItem.Length);
                                string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                string strResultAppIDs = GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
                                string[] arrResultAppIDs = strResultAppIDs.Split(',');

                                for (int k = 0; k < dtPointItem.Length; k++)
                                {
                                    DataRow drPointItem = dtPointItem[k];
                                    //项目采用的标准上限、下限
                                    string strUp = "";
                                    string strLow = "";
                                    string strConditionType = "";
                                    getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                                    #region 构造监测任务点位明细表
                                    //构造监测任务点位明细表
                                    TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
                                    objMonitorTaskItem.ID = arrTaskItemIDs[k];
                                    objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
                                    objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                    objMonitorTaskItem.CONDITION_ID = strConditionID;
                                    objMonitorTaskItem.CONDITION_TYPE = strConditionType;
                                    objMonitorTaskItem.ST_UPPER = strUp;
                                    objMonitorTaskItem.ST_LOWER = strLow;
                                    objMonitorTaskItem.IS_DEL = "0";
                                    arrPointItem.Add(objMonitorTaskItem);
                                    #endregion

                                    #region 构造样品结果表
                                    //构造样品结果表
                                    string strAnalysisID = "";//分析方法ID
                                    string strStandardID = "";//方法依据ID
                                    string strCheckOut = ""; //检出限
                                    getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                    TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                    objSampleResult.ID = arrSampleResultIDs[k];
                                    objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                    objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                    objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                    objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(drPointItem["ITEM_ID"].ToString());
                                    objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                    objSampleResult.STANDARD_ID = strStandardID;
                                    objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                    objSampleResult.QC = "0";// 默认原始样手段
                                    objSampleResult.TASK_TYPE = "发送";
                                    objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                    objSampleResult.PRINTED = "0";//默认未打印
                                    arrSampleResult.Add(objSampleResult);
                                    #endregion

                                    #region 构造样品执行表
                                    //构造样品执行表
                                    TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                    objResultApp.ID = arrResultAppIDs[k];
                                    objResultApp.RESULT_ID = objSampleResult.ID;
                                    objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
                                    objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
                                    arrSampleResultApp.Add(objResultApp);
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
                    #endregion

            if (new TMisMonitorTaskLogic().SaveTrans(objTask, arrTaskPoint, arrSubTask, arrSubTaskApp, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
            {
                strReturn = true;
            }
        }
        return strReturn;
    }


    #region 采样、分析默认人员
    /// <summary>
    /// 获得采样人员相关信息
    /// </summary>
    /// <param name="strMonitorID">监测类别</param>
    /// <param name="strItemID">监测项目</param>
    /// <param name="strSampleManager">采样负责人ID</param>
    /// <param name="strSampleID">采样协同人ID</param>
    protected void GetSamplingMan(string strMonitorID, string strContractID, ref string strSampleManager, ref string strSampleID)
    {
        if (!String.IsNullOrEmpty(strMonitorID) && !string.IsNullOrEmpty(strContractID))
        {
            TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
            objItems.CONTRACT_ID = strContractID;
            objItems.MONITOR_TYPE_ID = strMonitorID;
            DataTable dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i] as DataRow;
                    strSampleManager = dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString() : (dr["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dr["SAMPLING_MANAGER_ID"].ToString() : strSampleManager);
                    strSampleID += strSampleID.Contains(dr["SAMPLING_ID"].ToString()) ? "" : (dr["SAMPLING_ID"].ToString().Length > 0 ? dr["SAMPLING_ID"].ToString() + "," : "");
                }
            }
        }
        strSampleID = strSampleID.Length > 0 ? strSampleID.Remove(strSampleID.LastIndexOf(",")) : "";
    }
    #endregion

    #region 分析方法、依据获取
    /// <summary>
    /// 获取指定监测项目的默认分析方法、依据
    /// </summary>
    /// <param name="strItemID">监测项目ID</param>
    /// <param name="strAnalysisID">分析方法ID</param>
    /// <param name="strStandardID">方法依据ID</param>
    /// <param name="strCheckOut">最底检出限</param>
    protected void getMethod(string strItemID, ref string strAnalysisID, ref string strStandardID, ref string strCheckOut)
    {
        TBaseItemAnalysisVo objAnalysis = new TBaseItemAnalysisVo();
        objAnalysis.ITEM_ID = strItemID;
        objAnalysis.IS_DEL = "0";
        DataTable dtItemAnalysis = new TBaseItemAnalysisLogic().SelectByTable(objAnalysis);
        if (dtItemAnalysis.Rows.Count > 0)
        {
            for (int i = 0; i < dtItemAnalysis.Rows.Count; i++)
            {
                if (dtItemAnalysis.Rows[i]["IS_DEFAULT"].ToString() == "是")//默认负责人，否则取第一条方法
                {
                    strAnalysisID = dtItemAnalysis.Rows[i]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[i]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[i]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }

                    break;//默认方法 唯一
                }
                else
                {
                    strAnalysisID = dtItemAnalysis.Rows[0]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[0]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[0]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }
                }
            }

        }
    }
    #endregion

    /// <summary>
    /// 获取采用的标准项的上下限
    /// </summary>
    /// <param name="strItemID">项目ID</param>
    /// <param name="strConditionID">条件项ID</param>
    /// <param name="strUp">上限</param>
    /// <param name="strLow">下限</param>
    protected void getStandardValue(string strItemID, string strConditionID, ref string strUp, ref string strLow, ref string strConditionType)
    {
        TBaseEvaluationConItemVo objConItemVo = new TBaseEvaluationConItemVo();
        objConItemVo.ITEM_ID = strItemID;
        objConItemVo.CONDITION_ID = strConditionID;
        objConItemVo.IS_DEL = "0";
        objConItemVo = new TBaseEvaluationConItemLogic().Details(objConItemVo);
        //上限处理
        if (objConItemVo.DISCHARGE_UPPER.Length > 0)
        {
            //上限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.UPPER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_UPPER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_UPPER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strUp += (strUnit + str) + ",";
                    }
                }
                if (strUp.Length > 0)
                {
                    strUp = strUp.Remove(strUp.LastIndexOf(","));
                }
            }
            else
            {
                strUp = strUnit + objConItemVo.DISCHARGE_UPPER;
            }
        }
        //下限处理
        if (objConItemVo.DISCHARGE_LOWER.Length > 0)
        {
            //下限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.LOWER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_LOWER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_LOWER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strLow += (strUnit + str) + ",";
                    }
                }
                if (strLow.Length > 0)
                {
                    strLow = strLow.Remove(strLow.LastIndexOf(","));
                }
            }
            else
            {
                strLow = strUnit + objConItemVo.DISCHARGE_LOWER;
            }
        }
        strConditionType = new TBaseEvaluationInfoLogic().Details(new TBaseEvaluationConInfoLogic().Details(strConditionID).STANDARD_ID).STANDARD_TYPE;
    }


    /// <summary>
    /// 删除环境质量监测计划
    /// </summary>
    /// <returns>true或false</returns>
    private bool DelEnvTaskPlan()
    {
        bool flag = false;
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            if (new TMisContractPlanLogic().Delete(strPlanId))
            {
                TMisMonitorTaskVo TaskVo = new TMisMonitorTaskVo();
                TaskVo.PLAN_ID = strPlanId;
                new TMisMonitorTaskLogic().Delete(TaskVo);
                flag = true;
            }
        }
        return flag;
    }

    /// <summary>
    /// 根据监测计划ID获取计划点位信息
    /// 创建时间：2013-06-06
    /// 创建人：胡方扬
    /// 修改时间：
    /// 修改人：
    /// 修改内容：
    /// </summary>
    private string GetPointInforsForPlan()
    {
        result = "";
        dt = new DataTable();

        dt = new TMisContractPlanLogic().GetPointInforsForPlan(strPlanId, strIfPlan);
        int CountNum = new TMisContractPlanLogic().GetPointInforsForPlanCount(strPlanId, strIfPlan);
        result = PageBase.LigerGridDataToJson(dt, CountNum);
        return result;
    }

    /// <summary>
    /// 在指令性（常规）任务下，如果没有设置质控，则直接进行采样任务发布进入采样环节
    /// </summary>
    /// <returns></returns>
    public string SendTask()
    {
        bool IsSuccess = true;
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.PLAN_ID = strPlanId;
            objTask = new TMisMonitorTaskLogic().Details(objTask);


            TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
            objSubTask.TASK_ID = objTask.ID;
            DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objSubTask.ID = dt.Rows[i]["ID"].ToString();
                objSubTask.TASK_STATUS = "02";
                objSubTask.SAMPLE_ASK_DATE = objTask.ASKING_DATE;
                objSubTask.SAMPLE_FINISH_DATE = objTask.FINISH_DATE;
                objSubTask.SAMPLING_MANAGER_ID = getContractPlanDutyUserId(strPlanId, dt.Rows[i]["MONITOR_ID"].ToString());
                if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                    IsSuccess = false;
            }

            if (IsSuccess)
            {
                TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
                objTaskEdit.ID = objTask.ID;
                objTaskEdit.QC_STATUS = "9";//表示已经完成质控设置
                new TMisMonitorTaskLogic().Edit(objTaskEdit);

                TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanId);
                objPlan.HAS_DONE = "1";
                new TMisContractPlanLogic().Edit(objPlan);
            }
        }
        else
        {
            IsSuccess = false;
        }
        return IsSuccess ? "1" : "";
    }

    public string getContractPlanDutyUserId(string strPlan_Id, string strMonitor_Id)
    {
        result = "";
        TMisContractUserdutyVo objContractUserDuty = new TMisContractUserdutyVo();
        objContractUserDuty.CONTRACT_PLAN_ID = strPlan_Id;
        objContractUserDuty.MONITOR_TYPE_ID = strMonitor_Id;
        DataTable dt = new DataTable();
        dt = new TMisContractUserdutyLogic().SelectByTable(objContractUserDuty);

        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString();
        }

        return result;
    }
    /// <summary>
    ///创建原因：插入选择的环境质量点位到质控点位表中
    ///创建人： 胡方扬
    ///创建时间： 2013-06-25
    /// </summary>
    /// <returns></returns>
    public bool InsertQcSettingPoint()
    {
        bool blFlag = false;
        TMisPointQcsettingVo objPointQcSetting = new TMisPointQcsettingVo();
        objPointQcSetting.MONITOR_ID = strMonitorId;
        objPointQcSetting.PROJECT_NAME = strProjectName;
        objPointQcSetting.YEAR = strYear;
        objPointQcSetting.MONTH = strMonth;
        if (!String.IsNullOrEmpty(strPointId) && !String.IsNullOrEmpty(strPoint_Name))
        {
            string[] objPoinIdtstrArr = strPointId.Split(',');
            string[] objPoinNametstrArr = strPoint_Name.Split(',');
            if (new TMisPointQcsettingLogic().InsertPointInforForArry(objPointQcSetting, objPoinIdtstrArr, objPoinNametstrArr))
            {
                blFlag = true;
            }
        }
        return blFlag;
    }

    /// <summary>
    ///创建原因：插入选择的环境质量点位到质控点位表中
    ///创建人： 胡方扬
    ///创建时间： 2013-06-25
    /// </summary>
    /// <returns></returns>
    public bool InsertQcSettingPointItems()
    {
        bool blFlag = false;
        TMisPointitemQcsettingVo objPointItemsQcSetting = new TMisPointitemQcsettingVo();
        objPointItemsQcSetting.POINT_QCSETTING_ID = strPointQcSetting_Id;
        objPointItemsQcSetting.QC_TYPE = strQcStatus;
        if (!String.IsNullOrEmpty(strPointQcSetting_Id))
        {
            if (new TMisPointitemQcsettingLogic().InsertQcSettingPointItems(objPointItemsQcSetting, strPointItem, strPointItemName))
            {
                blFlag = true;
            }
        }
        return blFlag;
    }
    /// <summary>
    ///创建原因：返回已经设置了质控的环境质量类点位列表
    ///创建人： 胡方扬
    ///创建时间： 2013-06-25
    /// </summary>
    /// <returns></returns>
    public string GetEnvQCSettingPointList()
    {
        result = "";
        TMisPointQcsettingVo objPointQcSetting = new TMisPointQcsettingVo();

        objPointQcSetting.POINT_NAME = strPoint_Name;
        objPointQcSetting.MONITOR_ID = strMonitorId;
        objPointQcSetting.YEAR = strYear;
        objPointQcSetting.MONTH = strMonth;

        DataTable objDtPointQcSetting = new TMisPointQcsettingLogic().SelectByTable(objPointQcSetting, intPageIndex, intPageSize);
        result = LigerGridDataToJson(objDtPointQcSetting, objDtPointQcSetting.Rows.Count);
        return result;
    }

    /// <summary>
    ///创建原因：返回符合条件的环境质控质控监测项目
    ///创建人： 胡方扬
    ///创建时间： 2013-06-25
    /// </summary>
    /// <returns></returns>
    public string GetEnvQCSettingPointItemList()
    {
        result = "";
        TMisPointitemQcsettingVo objPointItemQcSetting = new TMisPointitemQcsettingVo();

        objPointItemQcSetting.POINT_QCSETTING_ID = strPointQcSetting_Id;

        DataTable objDtPointItemSetting = new TMisPointitemQcsettingLogic().SelectByTable(objPointItemQcSetting);
        result = LigerGridDataToJson(objDtPointItemSetting, objDtPointItemSetting.Rows.Count);
        return result;
    }
    /// <summary>
    /// 创建原因：动态获取环境质量监测点位的监测项目信息
    /// 创建人：胡方扬
    /// 创建日期：2013-06-25
    /// </summary>
    /// <returns></returns>
    public string GetEnvPointItems()
    {
        result = "";
        if (!String.IsNullOrEmpty(strPointId) && !String.IsNullOrEmpty(strPointQcSetting_Id))
        {
            TMisPointitemQcsettingVo objPointItems = new TMisPointitemQcsettingVo();
            objPointItems.POINT_QCSETTING_ID = strPointQcSetting_Id;
            objPointItems.QC_TYPE = strQcStatus;
            DataTable objDtExit = new TMisPointitemQcsettingLogic().SelectByTable(objPointItems);
            DataTable objDtPointItems = new DataTable();
            if (!String.IsNullOrEmpty(strFatherKeyColumn) && !String.IsNullOrEmpty(strFatherTableName))
            {
                objDtPointItems = new TMisPointitemQcsettingLogic().GetEnvPointItemsForFather(strTableName, strFatherTableName, strFatherKeyColumn, strKeyColumns, strPointId);
            }
            else
            {
                objDtPointItems = new TMisPointitemQcsettingLogic().GetEnvPointItems(strTableName, strKeyColumns, strPointId);
            }
            if (objDtExit.Rows.Count > 0)
            {
                foreach (DataRow dr in objDtExit.Rows)
                {
                    DataRow[] objRow = objDtPointItems.Select("ID='" + dr["ITEM_ID"].ToString() + "'");
                    if (objRow.Length > 0)
                    {
                        foreach (DataRow drr in objRow)
                        {
                            objDtPointItems.Rows.Remove(drr);
                            objDtPointItems.AcceptChanges();
                        }
                    }
                }
            }
            result = LigerGridDataToJson(objDtPointItems, objDtPointItems.Rows.Count);
        }

        return result;
    }

    /// <summary>
    /// 创建原因:获取指定质控点位下的监测项目信息
    /// 创建人：胡方扬
    /// 创建日期：2013-06-25
    /// </summary>
    /// <returns></returns>
    public string GetEnvPointItemsForQcSetting()
    {
        result = "";
        if (!String.IsNullOrEmpty(strPointQcSetting_Id))
        {
            TMisPointitemQcsettingVo objPointItems = new TMisPointitemQcsettingVo();
            objPointItems.POINT_QCSETTING_ID = strPointQcSetting_Id;
            objPointItems.QC_TYPE = strQcStatus;
            DataTable objDtPointItems = new TMisPointitemQcsettingLogic().GetEnvPointItemsForQcSetting(objPointItems);
            result = LigerGridDataToJson(objDtPointItems, objDtPointItems.Rows.Count);
        }

        return result;
    }
    /// <summary>
    /// 创建原因：删除环境质量点位质控计划
    /// 创建人：胡方扬
    /// 创建日期：2013-06-26
    /// </summary>
    /// <returns></returns>
    public bool DelPointQcSetting()
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strPointQcSetting_Id))
        {
            TMisPointQcsettingVo objPointSetting = new TMisPointQcsettingVo();
            objPointSetting.ID = strPointQcSetting_Id;

            if (new TMisPointQcsettingLogic().Delete(strPointQcSetting_Id))
            {
                TMisPointitemQcsettingVo objPointItemSetting = new TMisPointitemQcsettingVo();
                objPointItemSetting.POINT_QCSETTING_ID = strPointQcSetting_Id;
                blFlag = true;
                if (blFlag)
                {
                    new TMisPointitemQcsettingLogic().Delete(objPointItemSetting);
                }
            }
        }
        return blFlag;
    }

    /// <summary>
    /// 创建原因：获取指定监测项目ID的采样仪器ID`
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <param name="strItemID"></param>
    /// <returns></returns>
    public string GetItemSamplingInstrumentID(string strItemID)
    {
        string result = "";
        DataTable dt = new TBaseItemSamplingInstrumentLogic().GetItemSamplingInstrument(strItemID);
        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["ID"].ToString();
        }
        return result;
    }

    /// <summary>
    /// 创建原因：取指定委托书尚未预约的点位
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <returns></returns>
    public string GetContractPointList()
    {
        string result = "";
        TMisContractPointVo objPoint = new TMisContractPointVo();
        objPoint.CONTRACT_ID = task_id;
        objPoint.ID = strPointId;
        DataTable dt = new TMisContractPointLogic().GetContractPointCondtion(objPoint, intPageIndex, intPageSize);
        int CountNum = new TMisContractPointLogic().GetContractPointCondtionCount(objPoint);

        result = LigerGridDataToJson(dt, CountNum);


        return result;
    }

    /// <summary>
    /// 创建原因：取指定监测预约计划尚未预约的点位
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <returns></returns>
    public string GetPlanPointList()
    {
        string result = "";
        if (!String.IsNullOrEmpty(strPlanId))
        {
            DataTable dt = new TMisContractPointLogic().GetContractPointCondtionForPlanId(strPlanId, strPointId, intPageIndex, intPageSize);
            int CountNum = new TMisContractPointLogic().GetContractPointCondtionForPlanIdCount(strPlanId, strPointId);

            result = LigerGridDataToJson(dt, CountNum);
        }


        return result;
    }

    /// <summary>
    /// 创建原因：取指定送样监测预约计划尚未预约的点位
    /// 创建人：魏林
    /// 创建日期：2014-08-7
    /// </summary>
    /// <returns></returns>
    public string GetSamplePlanPointList()
    {
        string result = "";
        TBaseCompanyPointVo CompanyPointVo = new TBaseCompanyPointVo();
        CompanyPointVo.MONITOR_ID = strMonitorId;
        CompanyPointVo.COMPANY_ID = strCompanyId;
        CompanyPointVo.POINT_TYPE = strContractType;
        CompanyPointVo.IS_DEL = "0";

        DataTable dt = new TBaseCompanyPointLogic().SelectByTableForPlan(CompanyPointVo, strPointId, intPageIndex, intPageSize);
        int CountNum = new TBaseCompanyPointLogic().SelectByTableForPlanCount(CompanyPointVo, strPointId);

        result = LigerGridDataToJson(dt, CountNum);

        return result;
    }

    /// <summary>
    /// 创建原因：取指定指令性任务尚未预约的点位
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <returns></returns>
    public string GetUnContractPointList()
    {
        string result = "";
        if (!String.IsNullOrEmpty(strPlanId))
        {
            DataTable dt = new TMisContractPointLogic().GetPointListForPlan(strPlanId, strPointId, intPageIndex, intPageSize);
            int CountNum = new TMisContractPointLogic().GetPointListForPlanCount(strPlanId, strPointId);

            result = LigerGridDataToJson(dt, CountNum);
        }

        return result;
    }

    /// <summary>
    /// 创建原因：插入指定监测点位到监测点位频次表
    /// 创建人:胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <returns></returns>
    public bool InsertPointFreq()
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(task_id) || !String.IsNullOrEmpty(strPlanId))
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            string[] strPoint = strPointId.Split(';');
            foreach (string str in strPoint)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = str;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();

            if (new TMisContractPointFreqLogic().CreatePlanPointQuck(dt, task_id))
            {
                blFlag = true;
            }
        }
        return blFlag;
    }


    /// <summary>
    /// 创建原因：插入指令性任务指定监测点位到监测点位频次表
    /// 创建人:胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <returns></returns>
    public bool InsertUnContractPointFreq()
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strPlanId))
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            string[] strPoint = strPointId.Split(';');
            foreach (string str in strPoint)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = str;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();

            if (new TMisContractPointFreqLogic().CreateUnContractPlanPoint(dt, strPlanId))
            {
                blFlag = true;
            }
        }
        return blFlag;
    }

    /// <summary>
    /// 创建原因：设置任务的各状态信息
    /// 创建人：胡方扬
    /// 创建日期：2013-07-19
    /// </summary>
    /// <returns>true or false</returns>
    public bool SetTaskQcStatus()
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strWorkTask_Id))
        {
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.ID = strWorkTask_Id;
            objTask.QC_STATUS = strQcStatus;
            objTask.ALLQC_STATUS = strAllQcStatus;
            objTask.ASKING_DATE = strAskingDate;
            objTask.SEND_STATUS = strSendStatus;
            //潘德军 2013-12-23  任务单号可改，且初始不生成
            objTask.TICKET_NUM = strTaskNum;

            if (new TMisMonitorTaskLogic().Edit(objTask))
            {
                //潘德军 2013-12-23  任务单号可改，且初始不生成
                TMisMonitorReportVo objReportVoWhere = new TMisMonitorReportVo();
                objReportVoWhere.TASK_ID = strWorkTask_Id;
                TMisMonitorReportVo objReportVoEdit = new TMisMonitorReportVo();
                objReportVoEdit.REPORT_CODE = objTask.TICKET_NUM;
                new TMisMonitorReportLogic().Edit(objReportVoEdit, objReportVoWhere);

                if (!String.IsNullOrEmpty(strPlanId))
                {
                    TMisContractPlanVo objPlan = new TMisContractPlanVo();
                    objPlan.ID = strPlanId;
                    objPlan.HAS_DONE = strHasDone;
                    if (!String.IsNullOrEmpty(strDate))
                    {
                        string[] strPlanDate = null;
                        strPlanDate = strDate.Split('-');
                        if (strPlanDate != null)
                        {
                            objPlan.PLAN_YEAR = strPlanDate[0].ToString();
                            objPlan.PLAN_MONTH = strPlanDate[1].ToString();
                            objPlan.PLAN_DAY = strPlanDate[2].ToString();
                        }
                    }
                    new TMisContractPlanLogic().Edit(objPlan);
                }
                blFlag = true;
            }
        }

        return blFlag;
    }
    private void GetRequest(HttpContext context)
    {
        if (!String.IsNullOrEmpty(context.Request.Params["sortnamer"]))
        {
            strSortname = context.Request.Params["sortnamer"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["sortorder"]))
        {
            strSortorder = context.Request.Params["sortorder"].Trim();
        }
        //当前页面
        if (!String.IsNullOrEmpty(context.Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(context.Request.Params["page"].Trim());
        }
        //每页记录数
        if (!String.IsNullOrEmpty(context.Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(context.Request.Params["pagesize"].Trim());
        }
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        //监测计划ID
        if (!String.IsNullOrEmpty(context.Request.Params["strPlanId"]))
        {
            strPlanId = context.Request.Params["strPlanId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDate"]))
        {
            strDate = context.Request.Params["strDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitorId"]))
        {
            strMonitorId = context.Request.Params["strMonitorId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["task_id"]))
        {
            task_id = context.Request.Params["task_id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strType"]))
        {
            strType = context.Request.Params["strType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserId"]))
        {
            strUserId = context.Request.Params["strUserId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strIfPlan"]))
        {
            strIfPlan = context.Request.Params["strIfPlan"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractCode"]))
        {
            strContractCode = context.Request.Params["strContractCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyNameFrim"]))
        {
            strCompanyNameFrim = context.Request.Params["strCompanyNameFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyId"]))
        {
            strCompanyId = context.Request.Params["strCompanyId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAreaIdFrim"]))
        {
            strAreaIdFrim = context.Request.Params["strAreaIdFrim"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strFreqId"]))
        {
            strFreqId = context.Request.Params["strFreqId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointId"]))
        {
            strPointId = context.Request.Params["strPointId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractId"]))
        {
            strContractId = context.Request.Params["strContractId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractType"]))
        {
            strContractType = context.Request.Params["strContractType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strProjectName"]))
        {
            strProjectName = context.Request.Params["strProjectName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContratYear"]))
        {
            strContratYear = context.Request.Params["strContratYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strQulickly"]))
        {
            strQulickly = context.Request.Params["strQulickly"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strConfigKey"]))
        {
            strConfigKey = context.Request.Params["strConfigKey"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strWorkTask_Id"]))
        {
            strWorkTask_Id = context.Request.Params["strWorkTask_Id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAskFinishDate"]))
        {
            strAskFinishDate = context.Request.Params["strAskFinishDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strQcStatus"]))
        {
            strQcStatus = context.Request.Params["strQcStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAllQcStatus"]))
        {
            strAllQcStatus = context.Request.Params["strAllQcStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEvnTypeId"]))
        {
            strEvnTypeId = context.Request.Params["strEvnTypeId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointItem"]))
        {
            strPointItem = context.Request.Params["strPointItem"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointItemName"]))
        {
            strPointItemName = context.Request.Params["strPointItemName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strKeyColumns"]))
        {
            strKeyColumns = context.Request.Params["strKeyColumns"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFatherKeyColumn"]))
        {
            strFatherKeyColumn = context.Request.Params["strFatherKeyColumn"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strFatherKeyValue"]))
        {
            strFatherKeyValue = context.Request.Params["strFatherKeyValue"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["SubKeyColumn"]))
        {
            SubKeyColumn = context.Request.Params["SubKeyColumn"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTableName"]))
        {
            strTableName = context.Request.Params["strTableName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFatherTableName"]))
        {
            strFatherTableName = context.Request.Params["strFatherTableName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPoint_Code"]))
        {
            strPoint_Code = context.Request.Params["strPoint_Code"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPoint_Name"]))
        {
            strPoint_Name = context.Request.Params["strPoint_Name"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPoint_Area"]))
        {
            strPoint_Area = context.Request.Params["strPoint_Area"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strYear"]))
        {
            strYear = context.Request.Params["strYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strConditionAndValue"]))
        {
            strConditionAndValue = context.Request.Params["strConditionAndValue"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFlag"]))
        {
            strFlag = context.Request.Params["strFlag"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strHasDone"]))
        {
            strHasDone = context.Request.Params["strHasDone"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTickeNum"]))
        {
            strTickeNum = context.Request.Params["strTickeNum"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGridFatherKeyColumn"]))
        {
            strGridFatherKeyColumn = context.Request.Params["strGridFatherKeyColumn"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGridFatherTable"]))
        {
            strGridFatherTable = context.Request.Params["strGridFatherTable"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGridItemKeyColumn"]))
        {
            strGridItemKeyColumn = context.Request.Params["strGridItemKeyColumn"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGridItemTable"]))
        {
            strGridItemTable = context.Request.Params["strGridItemTable"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strConKeyFatherColumn"]))
        {
            strConKeyFatherColumn = context.Request.Params["strConKeyFatherColumn"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strHaveContract"]))
        {
            strHaveContract = context.Request.Params["strHaveContract"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTaskStatus"]))
        {
            strTaskStatus = context.Request.Params["strTaskStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTaskType"]))
        {
            strTaskType = context.Request.Params["strTaskType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleManagerId"]))
        {
            strSampleManagerId = context.Request.Params["strSampleManagerId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEnvTypeId"]))
        {
            strEnvTypeId = context.Request.Params["strEnvTypeId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEnvTypeName"]))
        {
            strEnvTypeName = context.Request.Params["strEnvTypeName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonth"]))
        {
            strMonth = context.Request.Params["strMonth"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointQcSetting_Id"]))
        {
            strPointQcSetting_Id = context.Request.Params["strPointQcSetting_Id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAskingDate"]))
        {
            strAskingDate = context.Request.Params["strAskingDate"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strSendStatus"]))
        {
            strSendStatus = context.Request.Params["strSendStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strRiverArea"]))
        {
            strRiverArea = context.Request.Params["strRiverArea"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strValleyArea"]))
        {
            strValleyArea = context.Request.Params["strValleyArea"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDictCode"]))
        {
            strDictCode = context.Request.Params["strDictCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDictType"]))
        {
            strDictType = context.Request.Params["strDictType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDictID"]))
        {
            strDictID = context.Request.Params["strDictID"].Trim();
        }

        //潘德军2013-12-23  任务单号可改，且初始不生成
        if (!String.IsNullOrEmpty(context.Request.Params["strTaskNum"]))
        {
            strTaskNum = context.Request.Params["strTaskNum"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["CONTRACT_TYPE"]))
        {
            CONTRACT_TYPE = context.Request.Params["CONTRACT_TYPE"].Trim();
        }//strLinkMan = "", strLINK_PHONE = "";
        //潘德军2014-01-07 任务下达时，填写的联系人、联系电话，保存到受检企业信息
        if (!String.IsNullOrEmpty(context.Request.Params["strLinkMan"]))
        {
            strLinkMan = context.Request.Params["strLinkMan"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strLINK_PHONE"]))
        {
            strLINK_PHONE = context.Request.Params["strLINK_PHONE"].Trim();
        }
        //add by weilin 环境质量任务下达时传过来的年和月
        if (!String.IsNullOrEmpty(context.Request.Params["strEnvYear"]))
        {
            strEnvYear = context.Request.Params["strEnvYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEnvMonth"]))
        {
            strEnvMonth = context.Request.Params["strEnvMonth"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSendDept"]))
        {
            strSendDept = context.Request.Params["strSendDept"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strState"]))
        {
            strState = context.Request.Params["strState"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strFXS"]))
        {
            strFXS = context.Request.Params["strFXS"].Trim();
        }

        //CCFLOW工作流ID
        if (!String.IsNullOrEmpty(context.Request.Params["strCCFLOW_WORKID"]))
        {
            strCCFLOW_WORKID = context.Request.Params["strCCFLOW_WORKID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSY"]))
        {
            strSY = context.Request.Params["strSY"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strRemarks"]))
        {
            strRemarks = context.Request.Params["strRemarks"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strEnvTypes"]))
        {
            strEnvTypes = context.Request.Params["strEnvTypes"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strRegionCode"]))
        {
            strRegionCode = context.Request.Params["strRegionCode"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strFunctional"]))
        {
            strFunctional = context.Request.Params["strFunctional"].Trim();
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}