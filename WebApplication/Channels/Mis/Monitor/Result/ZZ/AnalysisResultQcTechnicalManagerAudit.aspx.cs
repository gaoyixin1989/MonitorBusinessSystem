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
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;

/// <summary>
/// 功能描述：技术负责人审核
/// 创建日期：2013-09-12
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_ZZ_AnalysisResultQcTechnicalManagerAudit : PageBase
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
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
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
                strResult = GoToBack(Request["strTaskId"].ToString(), Request["Rtn_Content"].ToString()); 
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

        DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo_ZZ("03", "60", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_ZZ("03", "60");
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

        DataTable dt = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfo_ZZ(strOneGridId, "03", "60", intPageIndex, intPageSize);
        dt.Columns.Add("ENTIRE_QC", System.Type.GetType("System.String"));
        foreach (DataRow row in dt.Rows)
        {
            string strEntire_QC = new TMisMonitorResultLogic().getEntire_QC(row["ID"].ToString());
            row["ENTIRE_QC"] = strEntire_QC;
        }
        int intTotalCount = new TMisMonitorResultLogic().getTaskSimpleQcCheckInfoCount_ZZ(strOneGridId, "03", "60");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
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

        DataTable dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_ZZ(strTwoGridId, "60");
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
    /// 任务退回，退回到主任复核环节
    /// </summary>
    /// <param name="strTaskId">任务Id</param>
    /// <returns></returns>
    private string GoToBack(string strTaskId,string Rtn_Content)
    {
        new TMisMonitorResultLogic().Update_Info(strTaskId, Rtn_Content);
        bool IsSuccess = new TMisMonitorResultLogic().TaskGoBackInQcCheck_QHD(strTaskId, LogInfo.UserInfo.ID, "qc_manager_audit", "0", "60", "50");
        return IsSuccess == true ? "1" : "0";
    }
    //判断任务是否可以发送
    public string IsCanSendTaskQcToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().TaskCanSendInQcManagerAudit_ZZ(strTaskId, "60");
        return IsCanGoToBack == true ? "1" : "0";
    }
    private string SendToNext(string strTaskId)
    {
        //记录当前分析环节的发送人
        DataTable objTable = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QHD(LogInfo.UserInfo.ID, "qc_manager_audit", strTaskId, "03", "0", "60");
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
            new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Edit(TMisMonitorSubtaskAppVo);
        }

        //分析环节任务完结状态改变
        bool IsSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlow(strTaskId, LogInfo.UserInfo.ID, "09", "qc_manager_audit");
        if (IsSuccess == true)
        {
            //如果是环境质量将自动对数据进行填报
            foreach (DataRow row in objTable.Rows)
            {
                string strSubTaskId = row["ID"].ToString();
                string strMonitorId = row["MONITOR_ID"].ToString();
                string strSampleDate = row["SAMPLE_FINISH_DATE"].ToString();
                i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskVo();
                TMisMonitorSubtaskVo.ID = strSubTaskId;
                TMisMonitorSubtaskVo.MONITOR_ID = strMonitorId;
                TMisMonitorSubtaskVo.SAMPLE_FINISH_DATE = strSampleDate;
                if (strMonitorId == "EnvRiver" || strMonitorId == "EnvReservoir" || strMonitorId == "EnvDrinking" || strMonitorId == "EnvDrinkingSource" || strMonitorId == "EnvStbc" || strMonitorId == "EnvMudRiver" || strMonitorId == "EnvPSoild" || strMonitorId == "EnvSoil" || strMonitorId == "EnvAir" || strMonitorId == "EnvSpeed" || strMonitorId == "EnvDust" || strMonitorId == "EnvRain")
                {
                    new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, false, "");
                }
                if (strMonitorId == "AreaNoise" || strMonitorId == "EnvRoadNoise" || strMonitorId == "FunctionNoise")
                {
                    new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, true, "");
                }
            }
            if (new TMisMonitorResultLogic().allTaskIsFinish_ZZ(strTaskId, "24", "60"))
            {
                i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
                TMisMonitorTaskVo.ID = strTaskId;
                TMisMonitorTaskVo.TASK_STATUS = "09";
                new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);

                i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVoTemp = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Details(strTaskId);
                if (TMisMonitorTaskVoTemp.CONTRACT_TYPE == "05")
                {
                    //根据任务ID获取项目负责人
                    string strProjectId = TMisMonitorTaskVoTemp.PROJECT_ID;
                    TMisMonitorReportVo objMrUpdate = new TMisMonitorReportVo();
                    objMrUpdate.IF_SEND = "1";
                    objMrUpdate.REPORT_SCHEDULER = strProjectId;
                    TMisMonitorReportVo objMrWhere = new TMisMonitorReportVo();
                    objMrWhere.TASK_ID = strTaskId;
                    new TMisMonitorReportLogic().Edit(objMrUpdate, objMrWhere);
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
}