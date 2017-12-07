using System;
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
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Sys.Duty;
/// <summary>
/// 功能描述：现场监测结果复核
/// 创建日期：2013-3-14
/// 创建人  ：邵世卓
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_SampleResultCheck : PageBase
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
                strResult = getOneGridInfo();
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString());
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
                strResult = GoToBack(Request["strResultId"].ToString());
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
            //判断是否能够发送(如果分析类现场项目还没走到现场复核不能发送)
            if (Request["type"] != null && Request["type"].ToString() == "IsCanToSend")
            {
                strResult = IsCanToSend(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strOneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务回退
            if (Request["type"] != null && Request["type"].ToString() == "GoToBackTask")
            {
                strResult = GoToBackTask(Request["strTaskId"].ToString(), Request["strSubTaskID"].ToString(), Request["strSuggestion"].ToString());
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
            //获取现场复核人信息
            if (Request["type"] != null && Request["type"].ToString() == "GetCheckUser")
            {
                strResult = GetCheckUser(Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    private string GetCheckUser(string strSubTaskID)
    {
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        string strUserIDs = "";
        DataTable dt = new DataTable();
        DataTable strSampleCheckSender = GetDefaultOrFirstDutyUser("sample_result_check", objSubtaskVo.MONITOR_ID);
        for (int i = 0; i < strSampleCheckSender.Rows.Count; i++)
        {
            strUserIDs += strSampleCheckSender.Rows[i]["USERID"].ToString() + ",";
        }

        TSysUserVo SysUserVo = new TSysUserVo();
        SysUserVo.ID = strUserIDs.TrimEnd(',');
        SysUserVo.IS_DEL = "0";
        SysUserVo.IS_USE = "1";
        dt = new TSysUserLogic().SelectByTableEx(SysUserVo, 0, 0);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获得指定职责的默认负责人，如果不存在则取第一个
    /// </summary>
    /// <param name="strDutyType">职责编码</param>
    /// <param name="strMonitorType">监测类别</param>
    /// <returns></returns>
    private DataTable GetDefaultOrFirstDutyUser(string strDutyType, string strMonitorType)
    {
        DataTable dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorType);
        return dtDuty;
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strSubTaskID = Request["strSubTaskID"].ToString();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorTaskLogic().SelectSampleTaskForQY(strSubTaskID, LogInfo.UserInfo.ID, "sample_result_check", "022", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorTaskLogic().SelectSampleTaskCountForQY(strSubTaskID, LogInfo.UserInfo.ID, "sample_result_check", "022");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        string strSubTaskID = Request["strSubTaskID"].ToString();
        DataTable dt = new TMisMonitorSubtaskLogic().SelectSampleSubTaskForQY(strSubTaskID, strOneGridId, "022", "sample_result_check");

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = strOneGridId;
        objReturnInfoVo.SUBTASK_ID = strSubTaskID;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_004;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_003;
        objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Suggestion"] = objReturnInfoVo.SUGGESTION;
        }

        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        //如果子任务是分支任务
        string strRemark = new TMisMonitorSubtaskLogic().Details(strTwoGridId).REMARK1;
        if (!string.IsNullOrEmpty(strRemark))
        {
            dt = new TMisMonitorSampleInfoLogic().getSamplingForSampleItem(strTwoGridId, "022", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorSampleInfoLogic().getSamplingCountForSampleItem(strTwoGridId, "022");
        }
        else
        {
            dt = new TMisMonitorSampleInfoLogic().getSamplingForSampleItemOne(strTwoGridId, "022", intPageIndex, intPageSize);
            intTotalCount = new TMisMonitorSampleInfoLogic().getSamplingCountForSampleItemOne(strTwoGridId, "022");
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        objResultVo.SAMPLE_ID = strThreeGridId;
        objResultVo.QC_TYPE = "0";
        objResultVo.RESULT_STATUS = "01,02";
        DataTable dt = new TMisMonitorResultLogic().SelectSceneItemInfo(objResultVo);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室内控信息
    /// </summary>
    /// <returns></returns>
    private string getFiveGridInfo(string strFourGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo(strFourGridId, "1");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    public string IsCanSendTaskCheckToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().IsCanSendTaskCheckToNextFlow(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse_result", "03");
        return IsCanGoToBack == true ? "1" : "0";
    }
    /// <summary>
    /// 判断分析类现场项目是否全部走到了该环节（现场结果复核）
    /// </summary>
    /// <param name="strTaskId"></param>
    /// <returns></returns>
    public string IsCanToSend(string strTaskId)
    {
        bool IsCanToSend = new TMisMonitorResultLogic().IsAnalySampleItemFlow(strTaskId, "02");
        return IsCanToSend == true ? "1" : "0";
    }
    /// <summary>
    /// 将任务发送至下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strOneGridId)
    {
        string strSubTaskID = Request["strSubTaskID"].ToString();
        string strUserID = Request["strUserID"].ToString();
        int SuccessCount = 0;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectSampleSubTaskForQY(strSubTaskID, strOneGridId, "022", "sample_result_check");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskVo();
                objSubtaskVo.ID = dt.Rows[i]["ID"].ToString();
                objSubtaskVo.TASK_STATUS = "023";
                objSubtaskVo.TASK_TYPE = "发送";
                if (new TMisMonitorSubtaskLogic().Edit(objSubtaskVo)) {
                    SuccessCount++;
                }

                TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
                objSubAppSet.REMARK2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //复核时间
                objSubAppSet.SAMPLING_QC_CHECK = strUserID;
                TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
                objSubAppWhere.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
            }
        }
        return SuccessCount>0 ? "1" : "0";
    }

    /// <summary>
    /// 将任务回退到上一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string GoToBackTask(string strTaskId, string strSubTaskID, string strSuggestion)
    {
        bool isSuccess = false;
        //TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskVo();
        //if (new TMisMonitorSubtaskLogic().Details(strSubTaskId).REMARK1 == "")
        //{
        //    objSubTaskVo.ID = strSubTaskId;
        //    objSubTaskVo.TASK_STATUS = "122";//退回状态
        //    objSubTaskVo.TASK_TYPE = "退回";
        //    isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);
        //}
        //else
        //{
        isSuccess = new TMisMonitorSubtaskLogic().SampleResultCheckBackTo(strTaskId, strSubTaskID, "02", "50", "退回");
        bool isSampleDept = new TMisMonitorSubtaskLogic().isExistAnysceneDept(strTaskId, strSubTaskID);
        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID);
        //}
        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = strTaskId;
        objReturnInfoVo.SUBTASK_ID = strSubTaskID;
        if (dtSampleItem.Rows.Count > 0) //有现场类项目
        {
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_002;
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
        }
        if (isSampleDept) //有分析类现场项目
        {
            objReturnInfoVo.ID = "";
            objReturnInfoVo.SUGGESTION = "";
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_010;
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
        }
        
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 将结果数据发送至下一环节
    /// </summary>
    /// <param name="strResultId">监测结果Id</param>
    /// <returns></returns>
    public string SendResultToNext(string strResultId)
    {
        bool isSuccess = new TMisMonitorResultLogic().SendResultToNext_QHD(strResultId, "04");
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 根据监测类型获取岗位职责用户信息
    /// </summary>
    /// <param name="strMonitorType">监测类型Id</param>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getUserInfo(string strMonitorType)
    {
        //获取采样分配环节用户信息
        DataTable objTable = new TMisMonitorResultLogic().getUsersInfo("duty_sampling", strMonitorType, "");
        return DataTableToJson(objTable);
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
    public string GoToBack(string strResultId)
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = strResultId;
        TMisMonitorResultVo.TASK_TYPE = "退回";
        TMisMonitorResultVo.RESULT_STATUS = "02";
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取默认采样负责人名称
    /// </summary>
    /// <param name="strUserId">采样负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取默认采样协同人信息
    /// </summary>
    /// <param name="strUserId">采样负责人ID</param>
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

    /// <summary>
    /// 获取监测项目单位
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemUnit(string strItemID)
    {
        TBaseItemAnalysisVo objItemAna = new TBaseItemAnalysisVo();
        objItemAna.ITEM_ID = strItemID;
        objItemAna = new TBaseItemAnalysisLogic().Details(objItemAna);
        return getDictName(objItemAna.UNIT, "item_unit");
    }
}