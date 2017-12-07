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
/// 功能描述：报告领取
/// 创建时间：2012-12-10
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_ReportQHD_ReportManager : PageBase
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
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "EditStatus")
        {
            EditReportStatus();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractType")
        {
            strReturn = getDictJsonString("Contract_Type");
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
        //监测报告对象
        TMisMonitorReportVo objReport = new TMisMonitorReportVo();
        //监测任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        //创建标准JSON数据
        objReport.SORT_FIELD = Request.Params["sortname"];
        objReport.SORT_TYPE = Request.Params["sortorder"];
        //过滤条件
        //委托类型
        objTask.CONTRACT_TYPE = !String.IsNullOrEmpty(Request.Params["srhContractType"]) ? Request.Params["srhContractType"].ToString() : "";
        //委托书编号
        objTask.CONTRACT_CODE = !String.IsNullOrEmpty(Request.Params["srhContractCode"]) ? Request.Params["srhContractCode"].ToString() : "";
        //项目名称
        objTask.PROJECT_NAME = !String.IsNullOrEmpty(Request.Params["srhProjectName"]) ? Request.Params["srhProjectName"].ToString() : "";
        //委托单位
        objTask.CLIENT_COMPANY_ID = !String.IsNullOrEmpty(Request.Params["srhClientName"]) ? Request.Params["srhClientName"].ToString() : "";
        //监测任务状态
        objTask.TASK_STATUS = "11";
        //签订日期
        if (!string.IsNullOrEmpty(Request.QueryString["srhBeginDate"]) || !string.IsNullOrEmpty(Request.QueryString["srhEndDate"]))
        {
            objTask.CONSIGN_DATE = Request.QueryString["srhBeginDate"].ToString() + "|" + Request.QueryString["srhEndDate"].ToString();
        }
        //报告单号
        objReport.REPORT_CODE = !String.IsNullOrEmpty(Request.Params["srhReportCode"]) ? Request.Params["srhReportCode"].ToString() : "";


        intTotalCount = new TMisMonitorReportLogic().GetSelectResultCount(objReport, objTask);
        dtEval = new TMisMonitorReportLogic().SelectByTableForManager(objReport, objTask, pageIndex, pageSize);

        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }

    /// <summary>
    /// 报告领取状态更新
    /// </summary>
    protected void EditReportStatus()
    {
        TMisMonitorReportVo objReport = new TMisMonitorReportVo();
        objReport.IF_GET = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Request.QueryString["status"].ToString() : "";
        objReport.ID = !string.IsNullOrEmpty(Request.QueryString["id"]) ? Request.QueryString["id"].ToString() : "";
        //管理员特殊权限，可以对已领取的报告 取回到未收卷状态
        if (objReport.IF_GET == "0")
        {
            if (LogInfo.UserInfo.REAL_NAME != "超级管理员")
            {
                objReport.IF_GET = "";
            }
        }
        new TMisMonitorReportLogic().Edit(objReport);
    }

    /// <summary>
    /// 获取委托单位名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetClientName(string strValue)
    {
        return new TMisMonitorTaskCompanyLogic().Details(strValue).COMPANY_NAME;
    }

    /// <summary>
    /// 获取数据字典名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetDataDictName(string strValue, string strType)
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(strValue, strType);
    }

    /// <summary>
    ///  获取报告领取方式
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetReportWay(string strValue)
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(new TMisContractLogic().Details(strValue).RPT_WAY, "RPT_WAY");
    }

}