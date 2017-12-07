using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：采样任务列表
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplingList_Old : PageBase
{
    private string strTestedCompanyID = "";
    private string strContractCode = "";
    private string strMonitorID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (this.Request["TestedCompanyID"] != null)
        {
            strTestedCompanyID = this.Request["TestedCompanyID"].ToString();
        }
        if (this.Request["ContractCode"] != null)
        {
            strContractCode = this.Request["ContractCode"].ToString();
        }
        if (this.Request["MonitorID"] != null)
        {
            strMonitorID = this.Request["MonitorID"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getSampleTask")
        {
            strResult = getSampleTask();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 采样任务列表信息
    /// </summary>
    /// <returns>Json</returns>
    protected string getSampleTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.SORT_FIELD = "SAMPLE_ASK_DATE";
        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithTask(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithAllTask(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        int intTotalCount = dt.Rows.Count;

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strCompanyId)
    {
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }

    /// <summary>
    /// 获取字典项信息
    /// </summary>
    /// <param name="strDictCode"></param>
    /// <param name="strDictType"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strUserID)
    {
        return new TSysUserLogic().Details(strUserID).REAL_NAME;
    }
}