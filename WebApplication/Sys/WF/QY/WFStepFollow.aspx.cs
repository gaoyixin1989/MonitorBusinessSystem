using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 功能描述：采样分析流程任务追踪
/// 创建时间：2013-5-10
/// 创建人：邵世卓
/// </summary>
public partial class Sys_WF_QY_WFStepFollow : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
        {
            this.TASK_ID.Value = Request.QueryString["task_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getData")
        {
            Response.Write(getData());
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractType")
        {
            Response.Write(getDictJsonString("Contract_Type"));
            Response.End();
        }
        if (!IsPostBack)
        { }
    }

    protected string getData()
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
        dtEval = new TMisMonitorTaskLogic().SelectByTable(objTask, pageIndex, pageSize);

        intTotalCount = new TMisMonitorTaskLogic().GetSelectResultCount(objTask);
        //处理现场项目任务 现场项目任务未审核完成时将报告任务移除
        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
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
}