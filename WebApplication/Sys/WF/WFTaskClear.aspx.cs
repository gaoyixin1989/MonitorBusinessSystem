using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 此页面可销毁工作流程，管理员可以销毁任何流程操作
/// </summary>
public partial class Sys_WF_WFTaskClear : PageBaseForWF
{

    public DataTable StepTable
    {
        get { return (DataTable)ViewState["StepTable"]; }
        set { ViewState["StepTable"] = value; }
    }
    public DataTable WFTable
    {
        get { return (DataTable)ViewState["WFTable"]; }
        set { ViewState["WFTable"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetUserData();
        }
    }

    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager_PageChanged(object sender, EventArgs e)
    {
        //
        GetUserData();
    }

    private void GetUserData()
    {
        if (base.LogInfo.UserInfo.USER_TYPE == "admin")
            InitListData("");
        else
            InitListData(LogInfo.UserInfo.ID);
    }

    public void InitListData(string strUserID)
    {
        TWfInstTaskDetailVo detail = new TWfInstTaskDetailVo();

        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        pager.RecordCount = logic.GetSelectResultCountForUserDealing(strUserID, "2A");
        DataTable dtControl = logic.SelectByTableForUserDealing(strUserID, "2A", pager.CurrentPageIndex, pager.PageSize);

        if (null == StepTable)
        {
            DataTable dtTask = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo());
            StepTable = dtTask;
        }
        if (null == WFTable)
        {
            DataTable dtWF = new TWfSettingFlowLogic().SelectByTable(new TWfSettingFlowVo());
            WFTable = dtWF;
        }

        grdList.DataSource = dtControl.DefaultView;
        grdList.DataBind();
    }


    public void grdList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cHold")
        {
            //处理挂起的相关问题
            string strID = e.CommandArgument.ToString();
            WFOperateForHold(strID);
            GetUserData();
        }
        else if (e.CommandName == "cReNormal")
        {
            //处理挂起的相关问题
            string strID = e.CommandArgument.ToString();
            WFOperateForReNormal(strID);
            GetUserData();
        }
        else if (e.CommandName == "cKillWF")
        {
            //处理销毁的相关问题，挂起状态为2的情况 是销毁的处理结果
            string strID = e.CommandArgument.ToString();
            WFOperateForKill(strID);

            GetUserData();
        }
        else if (e.CommandName == "cReStartWF")
        {
            string strID = e.CommandArgument.ToString();
            WFOperateGoStart(strID);
            GetUserData();
        }
    }


    public string GetWFName(object objName)
    {
        foreach (DataRow dr in WFTable.Rows)
            if (dr[TWfSettingFlowVo.WF_ID_FIELD].ToString() == objName.ToString().Trim())
                return dr[TWfSettingFlowVo.WF_CAPTION_FIELD].ToString();
        return "";
    }

    public string GetTaskName(object objName)
    {
        foreach (DataRow dr in StepTable.Rows)
            if (dr[TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == objName.ToString().Trim())
                return dr[TWfSettingTaskVo.TASK_CAPTION_FIELD].ToString();
        return "";
    }
    public bool GetHoldVisble(object objHoldState)
    {
        if (objHoldState.ToString() == TWfCommDict.WfState.StateHold)
            return false;
        else
            return true;
    }
    public bool GetNormalVisble(object objHoldState)
    {
        if (objHoldState.ToString() == TWfCommDict.WfState.StateHold)
            return true;
        else
            return false;
    }



}