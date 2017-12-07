using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.View;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Channels.Base.Company;
using i3.BusinessLogic.Channels.Mis.Contract;

/// <summary>
/// 功能描述：报告办理
/// 创建时间：2013-7-9
/// 创建人：潘德军
/// </summary>
public partial class Channels_Mis_Report_ReportZZ_Report_Done_Not : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //输入结果
        string strReturn = "";
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getReportInfo")
        {
            strReturn = GetReportInfo();
            Response.Write(strReturn);
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getLocalUserId")
        {
            strReturn = base.LogInfo.UserInfo.ID;
            Response.Write(strReturn);
            Response.End();
        }
    }

    /// <summary>
    /// 获取监测报告
    /// </summary>
    /// <returns></returns>
    protected string GetReportInfo()
    {
        string result = "";
        int intTotalCount = 0;
        //页数
        int pageIndex = Int32.Parse(Request.Params["page"].ToString());
        //分页数
        int pageSize = Int32.Parse(Request.Params["pagesize"].ToString());
        DataTable dtEval = new DataTable();
        //监测任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        //创建标准JSON数据
        objTask.SORT_FIELD = Request.Params["sortname"];
        objTask.SORT_TYPE = Request.Params["sortorder"];
        //过滤条件
        //委托类型
        objTask.CONTRACT_TYPE = !String.IsNullOrEmpty(Request.Params["srhContractType"]) ? Request.Params["srhContractType"].ToString() : "";
        //委托书编号
        objTask.CONTRACT_CODE = !String.IsNullOrEmpty(Request.Params["srhContractCode"]) ? Request.Params["srhContractCode"].ToString() : "";
        //项目名称
        objTask.PROJECT_NAME = !String.IsNullOrEmpty(Request.Params["srhProjectName"]) ? Request.Params["srhProjectName"].ToString() : "";
        //委托单位
        objTask.CLIENT_COMPANY_ID = !String.IsNullOrEmpty(Request.Params["srhClientName"]) ? Request.Params["srhClientName"].ToString() : "";
        //签订日期
        if (!string.IsNullOrEmpty(Request.QueryString["srhBeginDate"]) || !string.IsNullOrEmpty(Request.QueryString["srhEndDate"]))
        {
            objTask.CONSIGN_DATE = Request.QueryString["srhBeginDate"].ToString() + "|" + Request.QueryString["srhEndDate"].ToString();
        }
        //任务状态
        objTask.TASK_STATUS = "09";
        intTotalCount = new TMisMonitorTaskLogic().GetSelectResultCountForZZ(objTask, LogInfo.UserInfo.ID, false);
        //dtEval = new TMisMonitorTaskLogic().SelectByTableForZZ(objTask, LogInfo.UserInfo.ID, pageIndex, pageSize, false);
        dtEval = new TMisMonitorTaskLogic().SelectByTableForReportAccept(objTask, pageIndex, pageSize);
        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }

    [WebMethod]
    public static string AcceptReport(string strValue,string strLocalUserId)
    {
        TMisMonitorReportVo objvo=new TMisMonitorReportVo();
        objvo.ID=strValue;
        objvo.IF_SEND = "1";
        objvo.IF_ACCEPT="1";
        objvo.REPORT_SCHEDULER = strLocalUserId;
        if (new TMisMonitorReportLogic().Edit(objvo))
            return "1";
        else
            return "0";
    }
}