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
/// 创建时间：2012-12-16
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_ReportZZ_ReportList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //输入结果
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getReportInfo")
        {
            string strTabType = "";
            if (!Request.Params.AllKeys.Contains("tabType"))
                strTabType = "0";
            else
                strTabType = Request.QueryString["tabType"].ToString();
            Response.Write(GetReportInfo(strTabType));
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractType")
        {
            Response.Write(getDictJsonString("Contract_Type"));
            Response.End();
        }
        if (!IsPostBack)
        {
            string strFirstFlowName = getFirFlowName("RPT");
            this.FirstFlowName.Value = !string.IsNullOrEmpty(strFirstFlowName) ? strFirstFlowName : "任务办理";
        }
    }

    /// <summary>
    /// 获取 未确认的监测报告
    /// </summary>
    /// <returns></returns>
    protected string GetReportInfo(string strTabType)
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
        objTask.COMFIRM_STATUS = strTabType;
        dtEval = new TMisMonitorTaskLogic().SelectByTable(objTask, pageIndex, pageSize);

        intTotalCount = new TMisMonitorTaskLogic().GetSelectResultCount(objTask);
        //处理现场项目任务 现场项目任务未审核完成时将报告任务移除
        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }

    /// <summary>
    /// 任务确认
    /// </summary>
    [WebMethod]
    public static void ComfirmTask(string strValue)
    {
        //任务ID
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.COMFIRM_STATUS = "1";
        objTaskVo.ID = !string.IsNullOrEmpty(strValue) ? strValue : "";
        if (new TMisMonitorTaskLogic().Edit(objTaskVo))
        {
            new PageBase().WriteLog("监测任务确认", "", new PageBase().LogInfo.UserInfo.ID + "对监测任务" + objTaskVo.ID + "确认成功！");
        }
    }


    /// <summary>
    /// 任务办理
    /// </summary>
    [WebMethod]
    public static void ComleteTask(string strValue)
    {
        //任务ID
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.COMFIRM_STATUS = "2";
        objTaskVo.ID = !string.IsNullOrEmpty(strValue) ? strValue : "";
        if (new TMisMonitorTaskLogic().Edit(objTaskVo))
        {
            new PageBase().WriteLog("监测任务办理", "", new PageBase().LogInfo.UserInfo.ID + "对监测任务" + objTaskVo.ID + "办理成功！");
        }
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
    /// 获取工作流第一环节简称
    /// </summary>
    /// <param name="strWfID">工作流ID</param>
    /// <returns></returns>
    protected string getFirFlowName(string strWfID)
    {
        List<i3.ValueObject.Sys.WF.TWfSettingTaskVo> tempList = new i3.BusinessLogic.Sys.WF.TWfSettingTaskLogic().SelectByObjectListForSetp(new i3.ValueObject.Sys.WF.TWfSettingTaskVo()
        {
            WF_ID = strWfID
        });
        if (tempList.Count > 0)
        {
            return tempList[0].TASK_CAPTION;
        }
        return "";
    }
}