using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：任务列表
/// 创建日期：2012-11-08
/// 创建人  ：石磊
/// 修改说明：改为ligerui
/// 修改时间：2012-12-20
/// 修改人  ：潘德军
/// </summary>
public partial class Sys_WF_WfTaskListForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取任务列表
        if (Request["Action"] != null && Request["Action"].ToString() == "GetTask")
        {
            getTaskLst(LogInfo.UserInfo.ID);
        }
        if (Request["action"] != null && Request["action"] == "WaitingTask")
        {
            WatingTaskList(Request["status"]);
        }
        if (Request["action"] != null && Request["action"] == "BackTask")
        {
            ReturnTaskList(Request["back"]);
        }
        if (Request["action"] != null && Request["action"] == "FW_WaitingTask")
        {
            Get_FW_WaitTaskList(Request["fw_status"]);
        }
        if (Request["action"] != null && Request["action"] == "FW_BackTask")
        {
            Get_FW_BackTaskList(Request["fw_back"]);
        }
    }
    //待办任务
    private void WatingTaskList(string Task_Status)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        DataTable dt = logic.Get_Waiting_TaskList(base.LogInfo.UserInfo.ID, Task_Status);
        string strJson = CreateToJson(dt,15);
        Response.Write(strJson);
        Response.End();
    }
    //退回任务
    private void ReturnTaskList(string Task_Status)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        DataTable dt = logic.Get_Return_TaskList(base.LogInfo.UserInfo.ID, Task_Status);
        string strJson = CreateToJson(dt, 15);
        Response.Write(strJson);
        Response.End();
    }
    //发文待办
    public void Get_FW_WaitTaskList(string Task_Status)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        DataTable dt = logic.Get_FW_WaitTaskList(base.LogInfo.UserInfo.ID, Task_Status);
        string strJson = CreateToJson(dt, 15);
        Response.Write(strJson);
        Response.End();
    }
    //发文退回
    public void Get_FW_BackTaskList(string Task_Status)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        DataTable dt = logic.Get_FW_BackTaskList(base.LogInfo.UserInfo.ID, Task_Status);
        string strJson = CreateToJson(dt, 15);
        Response.Write(strJson);
        Response.End();
    }
    //获取任务列表
    private void getTaskLst(string strUserID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        string strWF_SERVICE_NAME = (Request.Params["SrhWF_SERVICE_NAME"] != null) ? Request.Params["SrhWF_SERVICE_NAME"] : "";
        string strSRC_USER = (Request.Params["SrhSRC_USER"] != null) ? Request.Params["SrhSRC_USER"] : "";
        string strINST_TASK_STARTTIME_from = (Request.Params["SrhINST_TASK_STARTTIME_from"] != null) ? Request.Params["SrhINST_TASK_STARTTIME_from"] : "";
        string strINST_TASK_STARTTIME_to = (Request.Params["SrhINST_TASK_STARTTIME_to"] != null) ? Request.Params["SrhINST_TASK_STARTTIME_to"] : "";

        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        string strState = "";
        if (Request["isQueding"] == null || Request["isQueding"].ToString().Trim().Length==0 || Request["isQueding"].ToString().ToLower().Trim()=="undefined")
            strState = "XX";
        else
            strState = TWfCommDict.StepState.StateConfirm;
        DataTable dt = logic.SelectByTableForUserTaskListEx(strUserID, strState, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to, intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCountForUserTaskListEx(strUserID, strState, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 当前环节
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetTaskName(string strValue)
    {
        DataTable dtTask = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo());
        foreach (DataRow dr in dtTask.Rows)
            if (dr[TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strValue)
                return dr[TWfSettingTaskVo.TASK_CAPTION_FIELD].ToString();
        return "";
    }

    /// <summary>
    /// 发送人
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }
}