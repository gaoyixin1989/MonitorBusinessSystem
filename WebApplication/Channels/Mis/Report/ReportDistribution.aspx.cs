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
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能描述：报告办理 任务分配功能
/// 创建时间：2014-01-07
/// 创建人：weilin
/// </summary>
public partial class Channels_Mis_Report_ReportDistribution : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //输入结果
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getReportInfo")
        {
            Response.Write(GetReportInfo());
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractType")
        {
            Response.Write(getDictJsonString("Contract_Type"));
            Response.End();
        }
        if (Request["type"] != null && Request["type"].ToString() == "GetReportUsers")
        {
            Response.Write(GetReportUsers());
            Response.End();
        }
        if (!IsPostBack)
        {
        }
    }

    /// <summary>
    /// 获取 未确认的监测报告
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
        //任务单号
        objTask.TICKET_NUM = !String.IsNullOrEmpty(Request.Params["srhTaskCode"]) ? Request.Params["srhTaskCode"].ToString() : "";
        //任务状态
        objTask.TASK_STATUS = "09";
        //确认状态 未确认
        objTask.COMFIRM_STATUS = "0";
        //过滤处理人
        objTask.REPORT_HANDLE = "null";     //"null"表示任务还没分配出去

        dtEval = new TMisMonitorTaskLogic().SelectByTable(objTask, pageIndex, pageSize);

        intTotalCount = new TMisMonitorTaskLogic().GetSelectResultCount(objTask);
        //处理现场项目任务 现场项目任务未审核完成时将报告任务移除
        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }

    //获取报告办理人数据
    private string GetReportUsers()
    {
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = "D2355FBCD1B545A", WF_ID = "RPT" });

        DataTable dt = new DataTable();
        TSysUserVo SysUserVo = new TSysUserVo();
        SysUserVo.ID = task.OPER_VALUE.TrimEnd('|').Replace("|", ",");
        SysUserVo.IS_DEL = "0";
        SysUserVo.IS_USE = "1";
        dt = new TSysUserLogic().SelectByTableEx(SysUserVo, 0, 0);

        return DataTableToJson(dt);
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
    /// 任务分配
    /// </summary>
    /// <param name="strUserID">用户Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string SendToNext(string strTaskID, string strUserID)
    {
        TMisMonitorTaskVo TMisMonitorTaskVo = new TMisMonitorTaskVo();
        TMisMonitorTaskVo.ID = strTaskID;
        TMisMonitorTaskVo.REPORT_HANDLE = strUserID;
        if (new TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo))
            return "true";
        else
            return "false";
    }
}