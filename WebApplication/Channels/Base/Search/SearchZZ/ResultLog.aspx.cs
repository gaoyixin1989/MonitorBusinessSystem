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
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能： "结果数据可追溯性"功能
/// 创建人：潘德军
/// 创建时间： 2013.7.6
/// </summary>
public partial class Channels_Base_Search_SearchZZ_ResultLog : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectTask")
        {
            selectTask();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectSample")
        {
            selectSample();
        }


        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectResult")
        {
            selectResult();
        }
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

        //委托年度
        string strYear = !string.IsNullOrEmpty(Request.QueryString["SrhYear"]) ? Request.QueryString["SrhYear"].ToString() : "";
        //委托类型
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["SrhContractType"]) ? Request.QueryString["SrhContractType"].ToString() : "";
        //任务单号
        string strTICKET_NUM = !string.IsNullOrEmpty(Request.QueryString["SrhTICKET_NUM"]) ? Request.QueryString["SrhTICKET_NUM"].ToString() : "";
        //项目名称
        string strProjectName = !string.IsNullOrEmpty(Request.QueryString["SrhProjectName"]) ? Request.QueryString["SrhProjectName"].ToString() : "";

        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        objTask.SORT_FIELD = strSortname;
        objTask.SORT_TYPE = strSortorder;
        objTask.CONTRACT_YEAR = strYear;
        objTask.CONTRACT_TYPE = strContractType;
        objTask.TICKET_NUM = strTICKET_NUM;
        objTask.PROJECT_NAME = strProjectName;

        string strJson = "";
        int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable(objTask, intPageIdx, intPagesize);

        strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取样品列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectSample()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        string strTaskID = !string.IsNullOrEmpty(Request.QueryString["task_id"]) ? Request.QueryString["task_id"].ToString() : "";
        string strTypeID = !string.IsNullOrEmpty(Request.QueryString["type_id"]) ? Request.QueryString["type_id"].ToString() : "";

        string strJson = "";
        int intTotalCount = new TMisMonitorSampleInfoLogic().GetSampleInfoCountByTask_Ex(strTaskID, strTypeID);//总计的数据条数
        DataTable dt = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTask_Ex(strTaskID, strTypeID, intPageIdx, intPagesize);

        strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取结果列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectResult()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        string sample_id = !string.IsNullOrEmpty(Request.QueryString["sample_id"]) ? Request.QueryString["sample_id"].ToString() : "";

        string strJson = "";
        int intTotalCount = new TMisMonitorResultLogic().getTaskItemCheckForSampleCount(sample_id);//总计的数据条数
        DataTable dt = new TMisMonitorResultLogic().getTaskItemForSample(sample_id, intPageIdx, intPagesize);

        strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
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
}