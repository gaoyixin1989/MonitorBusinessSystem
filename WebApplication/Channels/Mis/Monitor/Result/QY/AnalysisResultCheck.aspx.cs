﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
/// <summary>
/// 功能描述：分析数据审核
/// 创建日期：2013-03-28
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_AnalysisResultCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo(Request["IsDo"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString(), Request["IsDo"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["IsDo"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["IsDo"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getFiveGridInfo")
            {
                strResult = getFiveGridInfo(Request["fourGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strResultId"].ToString(), Request["strSuggestion"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //判断是否能够发送
            if (Request["type"] != null && Request["type"].ToString() == "IsCanSendTaskCheckToNextFlow")
            {
                strResult = IsCanSendTaskCheckToNextFlow(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测结果发送
            if (Request["type"] != null && Request["type"].ToString() == "SendResultToNext")
            {
                strResult = SendResultToNext(Request["strResultId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strIsDo)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (strIsDo == "0")
        {
            dt = new TMisMonitorResultLogic().getTaskCheckInfo_QY(LogInfo.UserInfo.ID, "03", "30", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorResultLogic().getTaskInfoCheckCount_QY(LogInfo.UserInfo.ID, "03", "30");
        }
        else
        {
            dt = new TMisMonitorResultLogic().getTaskCheckInfo_QY(LogInfo.UserInfo.ID, "03','09", "40','50", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorResultLogic().getTaskInfoCheckCount_QY(LogInfo.UserInfo.ID, "03','09", "40','50");
        }

        //退回意见
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //分析审核退回到复核的意见
            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_011;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_008;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] = objReturnInfoVo.SUGGESTION;
            //现场核录退回到复核的意见
            objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_010;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_008;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] += objReturnInfoVo.SUGGESTION;
        }

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId, string strIsDo)
    {
        DataTable dt = new DataTable();
        if (strIsDo == "0")
            dt = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QY(LogInfo.UserInfo.ID, strOneGridId, "03", "30");
        else
            dt = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QY(LogInfo.UserInfo.ID, strOneGridId, "03','09", "40','50");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId, string strIsDo)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (strIsDo == "0")
        {
            dt = new TMisMonitorResultLogic().getTaskSimpleCheckInfo_QY(strTwoGridId, "30", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorResultLogic().getTaskSimpleCheckInfoCount_QY(strTwoGridId, "30");
        }
        else
        {
            dt = new TMisMonitorResultLogic().getTaskSimpleCheckInfo_QY(strTwoGridId, "40','50", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorResultLogic().getTaskSimpleCheckInfoCount_QY(strTwoGridId, "40','50");
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId, string strIsDo)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();
        if (strIsDo == "0")
            dt = new TMisMonitorResultLogic().getTaskItemCheckInfo_QY(LogInfo.UserInfo.ID, strThreeGridId, "30");
        else
            dt = new TMisMonitorResultLogic().getTaskItemCheckInfo_QY(LogInfo.UserInfo.ID, strThreeGridId, "40','50");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室内控信息
    /// </summary>
    /// <returns></returns>
    private string getFiveGridInfo(string strFourGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo_QY(strFourGridId, "1", "", "");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    public string IsCanSendTaskCheckToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().IsCanSendTaskCheckToNextFlow(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse_result", "30");
        return IsCanGoToBack == true ? "1" : "0";
    }
    /// <summary>
    /// 将任务发送至下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strTaskId)
    {
        DataTable objTable = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QHD(LogInfo.UserInfo.ID, "duty_other_analyse_result", strTaskId, "03", "0", "30");
        foreach (DataRow row in objTable.Rows)
        {
            string strId = row["ID"].ToString();
            //根据子任务ＩＤ获取监测子任务审核表ID
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVoTemp = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVoTemp.SUBTASK_ID = strId;
            TMisMonitorSubtaskAppVoTemp = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Details(TMisMonitorSubtaskAppVoTemp);
            string strSubTaskAppId = TMisMonitorSubtaskAppVoTemp.ID;
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVo = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVo.ID = strSubTaskAppId;
            TMisMonitorSubtaskAppVo.RESULT_AUDIT = LogInfo.UserInfo.ID;
            new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Edit(TMisMonitorSubtaskAppVo);
        }

        bool isSuccess = false;
        string strMsg = "";
        isSuccess = new TMisMonitorResultLogic().SendTaskCheckToNextFlow_QY(strTaskId, LogInfo.UserInfo.ID, "03", "30", "50");
        bool IsAnyscene = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().isExistAnyscene(strTaskId, "");
        bool IsAnysceneDept = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().isExistAnysceneDept(strTaskId, "");
        if (IsAnyscene && IsAnysceneDept)
            strMsg = "现场项目结果核录、分析室主任审核";
        else if (IsAnyscene)
            strMsg = "分析室主任审核";
        else if (IsAnysceneDept)
            strMsg = "现场项目结果核录";

        return isSuccess == true ? "{\"result\":\"1\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"0\",\"msg\":\"" + strMsg + "\"}";
    }
    /// <summary>
    /// 将结果数据发送至下一环节
    /// </summary>
    /// <param name="strResultId">监测结果Id</param>
    /// <returns></returns>
    public string SendResultToNext(string strResultId)
    {
        bool isSuccess = new TMisMonitorResultLogic().SendResultToNext_QHD(strResultId, "40");
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strTaskId, string strCompanyId)
    {
        if (strCompanyId == "") return "";
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    /// <summary>
    /// 退回监测项目
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    public string GoToBack(string strResultId, string strSuggestion)
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = strResultId;
        TMisMonitorResultVo.TASK_TYPE = "退回";
        TMisMonitorResultVo.RESULT_STATUS = "20";
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);

        TMisMonitorResultVo = new TMisMonitorResultLogic().Details(strResultId);
        TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(TMisMonitorResultVo.SAMPLE_ID);
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(objSampleInfoVo.SUBTASK_ID);

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = objSubtaskVo.TASK_ID;
        objReturnInfoVo.SUBTASK_ID = objSubtaskVo.ID;
        objReturnInfoVo.RESULT_ID = strResultId;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_008;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_007;
        TMisReturnInfoVo obj = new TMisReturnInfoLogic().Details(objReturnInfoVo);
        if (obj.ID.Length > 0)
        {
            objReturnInfoVo.ID = obj.ID;
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Edit(objReturnInfoVo);
        }
        else
        {
            objReturnInfoVo.ID = GetSerialNumber("t_mis_return_id");
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Create(objReturnInfoVo);
        }

        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取默认分析负责人名称
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取默认分析协同人信息
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExName(string strUserId)
    {
        if (strUserId.Trim() == "") return "";
        List<string> list = strUserId.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        return strSumUserExName;
    }

    /// <summary>
    /// 获取质控手段名称
    /// </summary>
    /// <param name="strQcId">质控手段编码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcName(string strQcId)
    {
        string strQcName = "";

        if (strQcId == "") return "";
        List<string> list = strQcId.Split(',').ToList();
        string spit = "";
        foreach (string strQc in list)
        {
            if (strQc == "0")
                strQcName = strQcName + spit + "原始样";
            if (strQc == "1")
                strQcName = strQcName + spit + "现场空白";
            if (strQc == "2")
                strQcName = strQcName + spit + "现场加标";
            if (strQc == "3")
                strQcName = strQcName + spit + "现场平行";
            if (strQc == "4")
                strQcName = strQcName + spit + "实验室密码平行";
            if (strQc == "5")
                strQcName = strQcName + spit + "实验室空白";
            if (strQc == "6")
                strQcName = strQcName + spit + "实验室加标";
            if (strQc == "7")
                strQcName = strQcName + spit + "实验室明码平行";
            if (strQc == "8")
                strQcName = strQcName + spit + "标准样";
            spit = ",";
        }
        return strQcName;
    }
    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }
    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }
}