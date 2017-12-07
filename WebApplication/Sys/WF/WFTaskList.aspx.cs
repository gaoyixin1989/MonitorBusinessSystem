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

public partial class Sys_WF_WFTaskList : PageBaseForWF
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

    public void InitListData(string strUserID)
    {
        pager1.PageSize = 10;
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        pager1.RecordCount = logic.GetSelectResultCountForUserTaskList(strUserID, TWfCommDict.StepState.StateNormal);
        DataTable dtTaskDetail = logic.SelectByTableForUserTaskList(strUserID, TWfCommDict.StepState.StateNormal, pager1.CurrentPageIndex, pager1.PageSize);
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
        grdList.DataSource = dtTaskDetail.DefaultView;
        grdList.DataBind();


    }

    protected string GetUserName(object objUserName)
    {
        return GetUserNameFromID(objUserName.ToString(), true);
    }

    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager1_PageChanged(object sender, EventArgs e)
    {
        InitListData(base.LogInfo.UserInfo.ID);
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