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
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
/// <summary>
/// 功能描述：现场室主任审核
/// 创建日期：2013.05.03
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_ZZ_SampleResultQcCheck : PageBase
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
            //任务信息退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strTaskId"].ToString());
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

        DataTable dt = new TMisMonitorResultLogic().getTaskInfoByDeptItem(LogInfo.UserInfo.ID, "sample_result_qccheck", "023", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoByDeptItemCount(LogInfo.UserInfo.ID, "sample_result_qccheck", "023");
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

        DataTable dt = new TMisMonitorResultLogic().getSampleInfoByDeptItem(strOneGridId, "sample_result_qccheck", LogInfo.UserInfo.ID);
        int intTotalCount = dt.Rows.Count;
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

        DataTable dt = new TMisMonitorResultLogic().getItemInfoByDeptItem(strTwoGridId);
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
        bool IsSuccess = new TMisMonitorResultLogic().TaskGoBackInSampleResultQcCheck(strTaskId, "023", "122");
        return IsSuccess == true ? "1" : "0";
    }
    //发送到下一环节
    private string SendToNext(string strTaskId)
    {
        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskVo.TASK_ID = strTaskId;
        TMisMonitorSubtaskVo.TASK_STATUS = "023";
        DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(TMisMonitorSubtaskVo);
        #region 审核人修改
        foreach (DataRow row in objTable.Rows)
        {
            string strId = row["ID"].ToString();
            //根据子任务ＩＤ获取监测子任务审核表ID
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVoTemp = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVoTemp.SUBTASK_ID = strId;
            string strSubTaskAppId = new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Details(TMisMonitorSubtaskAppVoTemp).ID;
            i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVo = new i3.ValueObject.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVo.ID = strSubTaskAppId;
            TMisMonitorSubtaskAppVo.SAMPLING_QC_CHECK = LogInfo.UserInfo.ID;
            new i3.BusinessLogic.Channels.Mis.Monitor.SubTask.TMisMonitorSubtaskAppLogic().Edit(TMisMonitorSubtaskAppVo);
        }
        #endregion

        //bool IsSuccess = new TMisMonitorResultLogic().TaskGoBackInSampleResultQcCheck(strTaskId, "023", "24");

        bool IsSuccess = new TMisMonitorResultLogic().SetSampleResultStatus(strTaskId, "023", "50");

        if (IsSuccess == true)
        {
            new TMisMonitorResultLogic().TaskGoBackInSampleResultQcCheck(strTaskId, "023", "03");
        }
        #region 注释
        //if (IsSuccess == true)
        //{
        //    if (new TMisMonitorResultLogic().allTaskIsFinish_ZZ(strTaskId, "24", "50"))
        //    {
        //        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
        //        TMisMonitorTaskVo.ID = strTaskId;
        //        TMisMonitorTaskVo.TASK_STATUS = "09";
        //        new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);

        //        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo TMisMonitorTaskVoTemp = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().Details(strTaskId);
        //        if (TMisMonitorTaskVoTemp.CONTRACT_TYPE == "05")
        //        {
        //            //根据任务ID获取项目负责人
        //            string strProjectId = TMisMonitorTaskVoTemp.PROJECT_ID;
        //            TMisMonitorReportVo objMrUpdate = new TMisMonitorReportVo();
        //            objMrUpdate.IF_SEND = "1";
        //            objMrUpdate.REPORT_SCHEDULER = strProjectId;
        //            TMisMonitorReportVo objMrWhere = new TMisMonitorReportVo();
        //            objMrWhere.TASK_ID = strTaskId;
        //            new TMisMonitorReportLogic().Edit(objMrUpdate, objMrWhere);
        //        }
        //    }
        //}
        #endregion
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