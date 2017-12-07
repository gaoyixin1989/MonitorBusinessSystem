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

public partial class Sys_WF_WFTaskHistory : PageBaseForWF
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
            InitListData(LogInfo.UserInfo.ID);
        }
    }


    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager_PageChanged(object sender, EventArgs e)
    {
        //
        InitListData(LogInfo.UserInfo.ID);
    }


    public void InitListData(string strUserID)
    {
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        pager.RecordCount = logic.GetSelectResultCountForUserTaskList(strUserID, TWfCommDict.StepState.StateDown);
        DataTable dtTaskDetail2 = logic.SelectByTableForUserTaskList(strUserID, TWfCommDict.StepState.StateDown, pager.CurrentPageIndex, pager.PageSize);
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
        grdDownList.DataSource = dtTaskDetail2.DefaultView;
        grdDownList.DataBind();
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

}