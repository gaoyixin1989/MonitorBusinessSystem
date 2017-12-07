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
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
/// <summary>
/// 功能描述：分析结果质控审核
/// 创建日期：2012-12-10
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QHD_AnalysisResultQcCheck : PageBase
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
            //监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getMonitorGridInfo")
            {
                strResult = getMonitorGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["MonitorGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["SubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取现场质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getFiveGridInfo")
            {
                strResult = getFiveGridInfo(Request["threeGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务信息退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //判断是否能够发送
            if (Request["type"] != null && Request["type"].ToString() == "IsCanSendTaskQcToNextFlow")
            {
                strResult = IsCanSendTaskQcToNextFlow(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务信息发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strTaskId"].ToString(), Request["strTaskType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目退回
            if (Request["type"] != null && Request["type"].ToString() == "ResultGoToBack")
            {
                strResult = ResultGoToBack(Request["strResultId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo(LogInfo.UserInfo.ID, "duty_other_analyse_control", "03,021,02", "2", "50", intPageIndex, intPageSize);
        //int intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount(LogInfo.UserInfo.ID, "duty_other_analyse_control", "03,021,02", "2", "50");
        DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo_QHD("03,021,02", "50", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskQcCheckInfoCount_QHD("03,021,02", "50");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getMonitorGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getMonitorQcCheckInfo_QHD(strOneGridId, "03,021,02", "50");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strSubTaskId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfo(strOneGridId, LogInfo.UserInfo.ID, "duty_other_analyse_control", "03", "50", intPageIndex, intPageSize);
        //int intTotalCount = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfoCount(strOneGridId, LogInfo.UserInfo.ID, "duty_other_analyse_control", "03", "50");

        DataTable dt = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfo_QHD(strSubTaskId, "50", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfoCount_QHD(strSubTaskId, "50");


        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId, string strSubTaskID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_QHD(strTwoGridId, "50");
        TMisMonitorSubtaskVo SubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        //添加现场监测项目的采样负责人
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["HEAD_USERID"].ToString() == "")
            {
                dt.Rows[i]["HEAD_USERID"] = SubtaskVo.SAMPLING_MANAGER_ID;
            }
        }
        // int intTotalCount = new TMisMonitorResultLogic().getTaskItemCheckQcInfoCount(strTwoGridId);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取现场质控信息
    /// </summary>
    /// <param name="strThreeGridId"></param>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId)
    {
        DataTable dtOuter = new TMisMonitorResultLogic().getQcDetailInfo(strThreeGridId, "0");
        DataTable dtInner = new TMisMonitorResultLogic().getQcDetailInfo(strThreeGridId, "1");
        DataTable objtTable = dtOuter;
        objtTable.Merge(dtInner);
        string strJson = CreateToJson(objtTable, objtTable.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室质控信息
    /// </summary>
    /// <param name="strThreeGridId"></param>
    /// <returns></returns>
    private string getFiveGridInfo(string strThreeGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo(strThreeGridId, "1");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 任务退回
    /// </summary>
    /// <param name="strTaskId">任务Id</param>
    /// <returns></returns>
    private string GoToBack(string strTaskId)
    {
        bool IsSuccess = new TMisMonitorResultLogic().TaskGoBackInQcCheck_QHD(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse_control", "2", "50", "40");
        return IsSuccess == true ? "1" : "0";
    }
    //判断任务是否可以发送
    public string IsCanSendTaskQcToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().TaskCanSendInQcCheck_QHD(strTaskId, LogInfo.UserInfo.ID, "50", false);
        return IsCanGoToBack == true ? "1" : "0";
    }
    private string SendToNext(string strTaskId, string strTaskType)
    {
        DataTable objTable = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QHD(LogInfo.UserInfo.ID, "duty_other_analyse_control", strTaskId, "03,021,02", "2", "50");
        foreach (DataRow row in objTable.Rows)
        {
            string strId = row["ID"].ToString();
            //根据子任务ＩＤ获取监测子任务审核表ID
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVoTemp = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVoTemp.SUBTASK_ID = strId;
            string strSubTaskAppId = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Details(TMisMonitorSubtaskAppVoTemp).ID;
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVo = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVo.ID = strSubTaskAppId;
            TMisMonitorSubtaskAppVo.RESULT_QC_CHECK = LogInfo.UserInfo.ID;
            TMisMonitorSubtaskAppVo.RESULT_QC_CHECK_DATE = DateTime.Now.ToString();
            new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Edit(TMisMonitorSubtaskAppVo);
        }

        bool IsSuccess = true;
        if (strTaskType != "1")     //非环境质量类的任务
        {
            IsSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlow(strTaskId, LogInfo.UserInfo.ID, "09", "duty_other_analyse_control");
            if (IsSuccess == true)
            {
                i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
                TMisMonitorTaskVo.ID = strTaskId;
                TMisMonitorTaskVo.TASK_STATUS = "09";
                new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);
            }
        }
        else //环境质量类
        {
            IsSuccess = new TMisMonitorResultLogic().SendTaskCheckToNextFlow_QHD(strTaskId, LogInfo.UserInfo.ID, "03,021,02", "duty_other_analyse_result", "2", "50", "60", false, "");
            if (IsSuccess == true)
            {
                if (new TMisMonitorResultLogic().isAllSend(strTaskId, "60", false))
                {
                    IsSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlow(strTaskId, LogInfo.UserInfo.ID, "09", "duty_other_analyse_control");
                    if (IsSuccess == true)
                    {
                        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
                        TMisMonitorTaskVo.ID = strTaskId;
                        TMisMonitorTaskVo.TASK_STATUS = "09";
                        new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);
                    }
                }
            }
        }
        return IsSuccess == true ? "1" : "0";
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
    /// 样品发送
    /// </summary>
    /// <param name="strResultId">监测结果Id</param>
    /// <returns></returns>
    public string ResultGoToBack(string strResultId)
    {
        bool isSuccess = new TMisMonitorResultLogic().SendResultToNext_QHD(strResultId, "40");
        return isSuccess == true ? "1" : "0";
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
}