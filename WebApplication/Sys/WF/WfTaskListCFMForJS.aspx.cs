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
/// 功能描述：任务列表（确认认领界面）
/// 创建日期：2013-05-03
/// 修改人  ：潘德军
/// </summary>
public partial class Sys_WF_WfTaskListCFMForJS : PageBaseForWF
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
        string strState = TWfCommDict.StepState.StateNormal;

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

    /// <summary>
    /// 任务确认和撤销，确认后，该任务才可以进入办理环节，撤销后，任务可直接变为非确认状态
    /// </summary>
    /// <param name="strValue">环节ID</param>
    /// <param name="strUserID">用户ID</param>
    /// <param name="bIsUnConfirm">确认的用户</param>
    /// <returns></returns>
    [WebMethod]
    public static bool SetTaskToConfirm(string strValue, bool bIsUnConfirm)
    {
        TWfInstTaskDetailVo ttdv = new TWfInstTaskDetailVo();
        ttdv.ID = strValue;
        if (!bIsUnConfirm)
        {
            ttdv.CFM_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ttdv.INST_TASK_STATE = TWfCommDict.StepState.StateConfirm;
            ttdv.CFM_USER = new PageBase().LogInfo.UserInfo.ID;
        }
        else
        {
            ttdv.CFM_UNTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ttdv.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        }
        return new TWfInstTaskDetailLogic().Edit(ttdv);
    }
}