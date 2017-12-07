using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

using System.Data;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

using System.Web.Services;

public partial class Portal_Welcome_New : PageBase
{
    protected string strTaskHasCount = "0";
    protected string strTodayTaskHasCount = "0";
    protected string strTaskFinishCount = "0";
    protected string strTaskBackCount = "0";
    protected string strSampleCount = "0";
    protected string strAnalysisCount = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowData();
    }

    protected void ShowData()
    {
        //待办任务
        strTaskHasCount = new TWfInstTaskDetailLogic().GetSelectResultCountForUserTaskList(LogInfo.UserInfo.ID, TWfCommDict.StepState.StateNormal, "", "", "", "").ToString();
        //今日待办
        strTodayTaskHasCount = new TWfInstTaskDetailLogic().GetSelectResultCountForDayTaskList(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepState.StateNormal }, DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.ToShortDateString()).ToString();
        //已办总数
        strTaskFinishCount = new TWfInstTaskDetailLogic().GetSelectResultCount(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepState.StateDown }).ToString();
        //退回任务
        strTaskBackCount = new TWfInstTaskDetailLogic().GetSelectResultCount(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_DEAL_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepDealState.ForBack }).ToString();
        //采样任务
        strSampleCount = new TMisMonitorSubtaskLogic().GetCountWithTask(new TMisMonitorSubtaskVo() { TASK_STATUS = "02" }, LogInfo.UserInfo.ID).ToString();
        //分析任务
        strAnalysisCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "duty_other_analyse", "03", "01").ToString();
    }

    /// <summary>
    /// 获取当前系统登录用户有权限的所有菜单信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getMenuInfo()
    {
        string strUserID = new PageBase().LogInfo.UserInfo.ID;
        DataTable dtUserMenu = null;
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.IS_SHORTCUT = "0";
        menuvo.MENU_TYPE = "Menu";
        if (strUserID == "000000001")
        {
            dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
        }
        else
        {
            menuvo.IS_HIDE = "0";
            dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, strUserID);
        }
        return DataTableToJson(dtUserMenu); ;
    }
}