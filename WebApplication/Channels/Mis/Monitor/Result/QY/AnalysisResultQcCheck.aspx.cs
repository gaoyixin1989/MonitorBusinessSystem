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
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
/// <summary>
/// 功能描述：分析结果质控审核
/// 创建日期：2012-12-10
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_AnalysisResultQcCheck : PageBase
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
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取现场质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["strSubTaskID"].ToString(), Request["strItemID"].ToString());
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
                strResult = GoToBack(Request["strTaskId"].ToString(), Request["strSuggestion"].ToString());
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
                strResult = SendToNext(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目退回
            if (Request["type"] != null && Request["type"].ToString() == "ResultGoToBack")
            {
                strResult = ResultGoToBack(Request["strResultId"].ToString(), Request["strSuggestion"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //判断现场项目是否已审核完成
            if (Request["type"] != null && Request["type"].ToString() == "IsCanSendTaskQcToNextFlowWithSence")
            {
                strResult = IsCanSendTaskQcToNextFlowWithSence(Request["strTaskId"].ToString());
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

        DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo_QY(LogInfo.UserInfo.ID, "analysis_result_qc_check", "03", "40", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_QY(LogInfo.UserInfo.ID, "analysis_result_qc_check", "03", "40");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfo(strOneGridId, LogInfo.UserInfo.ID, "analysis_result_qc_check", "03", "40", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfoCount(strOneGridId, LogInfo.UserInfo.ID, "analysis_result_qc_check", "03", "40");
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

        DataTable dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo(strTwoGridId, "40");
        dt.Columns.Add("isQC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (new TMisMonitorResultLogic().getQcDetailInfo_QY(dt.Rows[i]["ID"].ToString(), "0", strSubTaskID, dt.Rows[i]["ITEM_ID"].ToString()).Rows.Count > 0)
            {
                dt.Rows[i]["isQC"] = "1";
            }
            else
            {
                dt.Rows[i]["isQC"] = "0";
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
    private string getFourGridInfo(string strThreeGridId, string strSubTaskID, string strItemID)
    {
        DataTable dtOuter = new TMisMonitorResultLogic().getQcDetailInfo_QY(strThreeGridId, "0", strSubTaskID, strItemID);
        //DataTable dtInner = new TMisMonitorResultLogic().getQcDetailInfo(strThreeGridId, "1");
        DataTable objtTable = dtOuter;
        //objtTable.Merge(dtInner);
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
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo_QY(strThreeGridId, "1", "", "");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 任务退回
    /// </summary>
    /// <param name="strTaskId">任务Id</param>
    /// <returns></returns>
    private string GoToBack(string strTaskId, string strSuggestion)
    {
        bool isSuccess = new TMisMonitorResultLogic().sendTaskQcCheckInfoToNext_QY(strTaskId, "03", "40", "50", "退回", "2");

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = strTaskId;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_009;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_011;
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
    //判断任务是否可以发送
    public string IsCanSendTaskQcToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().TaskCanSendInQcCheck_QHD(strTaskId, LogInfo.UserInfo.ID, "40", true);
        return IsCanGoToBack == true ? "1" : "0";
    }
    //判断现场项目任务是否已审核
    public string IsCanSendTaskQcToNextFlowWithSence(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().TaskCanSendInQcCheck_QY(strTaskId, LogInfo.UserInfo.ID, "analysis_result_qccheck", "03", "24");
        return IsCanGoToBack == true ? "1" : "0";
    }
    private string SendToNext(string strTaskId)
    {
        DataTable objTable = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QHD(LogInfo.UserInfo.ID, "analysis_result_qccheck", strTaskId, "03", "0", "40");
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
        string strMsg = "";

        bool isSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlowForQy(strTaskId, LogInfo.UserInfo.ID, "09", "analysis_result_check");

        if (isSuccess == true)
        {
            bool IsFinish = false;
            IsFinish = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().isFinishSubTask(strTaskId, true);
            if (IsFinish == true)
            {
                i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVo = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Details(strTaskId);
                TMisMonitorTaskVo.ID = strTaskId;
                TMisMonitorTaskVo.TASK_STATUS = "09";
                if (TMisMonitorTaskVo.TASK_TYPE == "1")
                {
                    objTable = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = strTaskId });
                    //如果是环境质量将自动对数据进行填报
                    foreach (DataRow row in objTable.Rows)
                    {
                        string strSubTaskId = row["ID"].ToString();
                        string strMonitorId = row["MONITOR_ID"].ToString();
                        string strAskDate = row["SAMPLE_ASK_DATE"].ToString();
                        string strSampleDate = row["SAMPLE_FINISH_DATE"].ToString();
                        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
                        TMisMonitorSubtaskVo.ID = strSubTaskId;
                        TMisMonitorSubtaskVo.MONITOR_ID = strMonitorId;
                        TMisMonitorSubtaskVo.SAMPLE_ASK_DATE = strAskDate;
                        TMisMonitorSubtaskVo.SAMPLE_FINISH_DATE = strSampleDate;
                        if (strMonitorId == "EnvRiver" || strMonitorId == "EnvReservoir" || strMonitorId == "EnvDrinking" || strMonitorId == "EnvDrinkingSource" || strMonitorId == "EnvStbc" || strMonitorId == "EnvMudRiver" || strMonitorId == "EnvPSoild" || strMonitorId == "EnvSoil" || strMonitorId == "EnvAir" || strMonitorId == "EnvSpeed" || strMonitorId == "EnvDust" || strMonitorId == "EnvRain")
                        {
                            new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, false, TMisMonitorTaskVo.SAMPLE_SEND_MAN);
                        }
                        if (strMonitorId == "AreaNoise" || strMonitorId == "EnvRoadNoise" || strMonitorId == "FunctionNoise")
                        {
                            new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, true, TMisMonitorTaskVo.SAMPLE_SEND_MAN);
                        }
                    }

                    strMsg = "数据填报";
                }
                else
                {
                    if (TMisMonitorTaskVo.REPORT_HANDLE == "")
                        TMisMonitorTaskVo.REPORT_HANDLE = getNextReportUserID("Report_UserID");
                    strMsg = "报告办理";
                }
                new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);
            }
        }

        return isSuccess == true ? "{\"result\":\"1\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"0\",\"msg\":\"" + strMsg + "\"}";
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
            if (strQc == "11")
                strQcName = strQcName + spit + "现场密码";
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
    /// 监测项目退回 黄进军20141103
    /// </summary>
    /// <param name="strResultId">监测结果Id</param>
    /// <returns></returns>
    public string ResultGoToBack(string strResultId, string strSuggestion)
    {
        //bool isSuccess = new TMisMonitorResultLogic().SendResultToNext_QHD(strResultId, "30");
        //return isSuccess == true ? "1" : "0";

        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = strResultId;
        TMisMonitorResultVo.TASK_TYPE = "退回";
        TMisMonitorResultVo.RESULT_STATUS = "50";
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);

        TMisMonitorResultVo = new TMisMonitorResultLogic().Details(strResultId);
        TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(TMisMonitorResultVo.SAMPLE_ID);
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(objSampleInfoVo.SUBTASK_ID);

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = objSubtaskVo.TASK_ID;
        objReturnInfoVo.SUBTASK_ID = objSubtaskVo.ID;
        objReturnInfoVo.RESULT_ID = strResultId;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_009;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_011;
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
}