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

public partial class Sys_WF_WFTaskDealing : PageBaseForWF
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
            InitListData2(LogInfo.UserInfo.ID);
        }
    }



    protected string GetUserName(object objUserName)
    {
        return GetUserNameFromID(objUserName.ToString(), true);
    }


    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager2_PageChanged(object sender, EventArgs e)
    {
        InitListData2(base.LogInfo.UserInfo.ID);
    }

    private void InitListData2(string strUserID)
    {
        pager2.PageSize = 10;
        TWfInstTaskDetailVo detail = new TWfInstTaskDetailVo();

        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        pager2.RecordCount = logic.GetSelectResultCountForUserDealing(strUserID, "2A");
        DataTable dtControl = logic.SelectByTableForUserDealing(strUserID, "2A", pager2.CurrentPageIndex, pager2.PageSize);

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

        grdList2.DataSource = dtControl.DefaultView;
        grdList2.DataBind();
    }


    public string GetStepHeight(object strStepID)
    {

        int iHeader = 200;
        int iStep = 100;
        int iHeight = 560;

        if (null == strStepID || string.IsNullOrEmpty(strStepID.ToString()))
            return iHeight.ToString();


        DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strStepID.ToString() });
        if (dt.Rows.Count == 0)
            iHeight = iHeader + iStep * 4;
        else
            iHeight = iHeader + iStep * dt.Rows.Count;
        return iHeight.ToString();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        //
        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        bool bIsSuccess = logic.CreateInstWFAndFirstStep("WF_TEST", "000000001", "000000001", "QJ_LIMS", "请假启动流程");
    }
}