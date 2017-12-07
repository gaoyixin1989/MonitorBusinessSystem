using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor;
using i3.View;
using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能： "综合查询--委托书查询--验收项目负责人"功能
/// 创建人：潘德军
/// 创建时间： 2013.7.1
/// </summary>
public partial class Channels_Base_Search_SearchZZ_ContractSrh_YS : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "SelectContract")
        {
            SelectContract();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectTask")
        {
            selectTask();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectsubTask")
        {
            selectsubTask();
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    protected void SelectContract()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //委托年度
        string strYear = !string.IsNullOrEmpty(Request.QueryString["SrhYear"]) ? Request.QueryString["SrhYear"].ToString() : "";
        //委托类型
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["SrhContractType"]) ? Request.QueryString["SrhContractType"].ToString() : "";
        //合同号
        string strContractCode = !string.IsNullOrEmpty(Request.QueryString["SrhContractCode"]) ? Request.QueryString["SrhContractCode"].ToString() : "";
        //任务单号
        string strDutyCode = !string.IsNullOrEmpty(Request.QueryString["DutyCode"]) ? Request.QueryString["DutyCode"].ToString() : "";
        //报告号
        string strReportCode = !string.IsNullOrEmpty(Request.QueryString["ReportCode"]) ? Request.QueryString["ReportCode"].ToString() : "";
        //委托客户
        string strClientName = !string.IsNullOrEmpty(Request.QueryString["ClientName"]) ? Request.QueryString["ClientName"].ToString() : "";
        //合同类别
        string strItemType = !string.IsNullOrEmpty(Request.QueryString["ItemType"]) ? Request.QueryString["ItemType"].ToString() : "";
        //项目名称
        string strProjectName = !string.IsNullOrEmpty(Request.QueryString["SrhProjectName"]) ? Request.QueryString["SrhProjectName"].ToString() : "";

        //构造查询对象
        TMisContractVo objContract = new TMisContractVo();
        TMisContractLogic objContractLogic = new TMisContractLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisContractVo.CONTRACT_CODE_FIELD;

        //objContract.SORT_FIELD = strSortname;
        //objContract.SORT_TYPE = strSortorder;
        objContract.SORT_FIELD = "ID";
        objContract.SORT_TYPE = "desc";
        objContract.CONTRACT_YEAR = strYear;
        objContract.CONTRACT_TYPE = "05";
        objContract.PROJECT_ID = base.LogInfo.UserInfo.ID;
        objContract.CONTRACT_CODE = strContractCode;
        objContract.PROJECT_NAME = strProjectName;
        objContract.CLIENT_COMPANY_ID = strClientName;
        objContract.TEST_TYPE = strItemType;
        

        int intTotalCount = objContractLogic.GetSelectResultCount(objContract);//总计的数据条数
        DataTable dt = objContractLogic.SelectByTable(objContract, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取监测任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        objTask.SORT_FIELD = strSortname;
        objTask.SORT_TYPE = strSortorder;
        objTask.CONTRACT_ID = !string.IsNullOrEmpty(Request.QueryString["contract_id"]) ? Request.QueryString["contract_id"].ToString() : "";

        string strJson = "";
        //int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable(objTask, 0, 0);

        strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取监测子任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectsubTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskLogic objSubTaskLogic = new TMisMonitorSubtaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorSubtaskVo.ID_FIELD;

        objSubTask.SORT_FIELD = strSortname;
        objSubTask.SORT_TYPE = strSortorder;
        objSubTask.TASK_ID = !string.IsNullOrEmpty(Request.QueryString["task_id"]) ? Request.QueryString["task_id"].ToString() : "";

        string strJson = "";
        //int intTotalCount = objSubTaskLogic.GetSelectResultCount(objSubTask);//总计的数据条数
        DataTable dt = objSubTaskLogic.SelectByTable(objSubTask, 0, 0);
        DataTable dtRe = doWithSubtaskData(dt);

        strJson = CreateToJson(dtRe, dtRe.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    private DataTable doWithSubtaskData(DataTable dt)
    {
        DataTable dtRe = new DataTable();
        string strMonitorIDs = "";
        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dtRe.Columns.Add(dt.Columns[j].ColumnName);
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strMonitorIDs.Contains(dt.Rows[i]["MONITOR_ID"].ToString()))
            {
                strMonitorIDs += "," + dt.Rows[i]["MONITOR_ID"].ToString();
                DataRow dr = dtRe.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[dt.Columns[j].ColumnName] = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                }
                dtRe.Rows.Add(dr);
            }
        }
        for (int i = 0; i < dtRe.Rows.Count; i++)
        {
            if (dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
            {
                string strUsername = new TSysUserLogic().Details(dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
                if (!dtRe.Rows[i]["SAMPLING_MAN"].ToString().Contains(strUsername))
                    dtRe.Rows[i]["SAMPLING_MAN"] = strUsername + (dtRe.Rows[i]["SAMPLING_MAN"].ToString().Length > 0 ? "，" : "") + dtRe.Rows[i]["SAMPLING_MAN"].ToString();
            }
        }
        dtRe.AcceptChanges();

        return dtRe;
    }
}