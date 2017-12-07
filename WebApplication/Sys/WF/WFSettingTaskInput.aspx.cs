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

public partial class Sys_WF_WFSettingTaskInput : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strID = Request.QueryString[TWfSettingTaskVo.ID_FIELD];
            string strWFID = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
            //把ID转换为WF_ID
            if (string.IsNullOrEmpty(strWFID))
                strWFID = GetWFIDFromID(strID);
            WF_ID.Value = strWFID;
            InitUserData();
        }
    }

    public string GetWFIDFromID(string strID)
    {
        return new TWfSettingFlowLogic().Details(strID).WF_ID;
    }

    /// <summary>
    /// 翻页控件事件
    /// </summary>
    protected void pager_PageChanged(object sender, EventArgs e)
    {
        InitUserData();
    }

    private void InitUserData()
    {
        TWfSettingFlowVo twf = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = WF_ID.Value });

        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
        TWfSettingTaskVo tv = new TWfSettingTaskVo();
        tv.WF_ID = WF_ID.Value;
        tv.SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD;
        tv.SORT_TYPE = " ASC ";
        pager.RecordCount = logic.GetSelectResultCount(tv);
        DataTable dt = logic.SelectByTable(tv, pager.CurrentPageIndex, pager.PageSize);
        grdList.DataSource = dt.DefaultView;
        grdList.DataBind();
    }

    public void btnAddTaskDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("WFSettingTaskInputDetail.aspx?WF_ID=" + WF_ID.Value);
    }

    public void btnOrderTask_Click(object sender, EventArgs e)
    {
        Response.Redirect("WFSettingTaskOrderDetail.aspx?WF_ID=" + WF_ID.Value);
    }

    public void grdList_Command(object sender, GridViewCommandEventArgs e)
    {
        string strID = e.CommandArgument.ToString();
        if (e.CommandName == "iDelete")
        {
            TWfSettingTaskVo setStep = new TWfSettingTaskLogic().Details(strID);
            TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
            TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
            TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
            bool bIsSucess = taskLogic.Delete(strID);
            if (bIsSucess)
            {
                cmdLogic.Delete(new TWfSettingTaskCmdVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
                formLogic.Delete(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
            }
            InitUserData();
            string strMessage = LogInfo.UserInfo.USER_NAME + "删除环节:" + setStep.WF_TASK_ID + " 成功";
            (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
        }
    }

    protected string GetCMDName(object objStr)
    {
        string[] strList = objStr.ToString().Split('|');
        string strText = "";
        foreach (string strTemp in strList)
        {
            if (string.IsNullOrEmpty(strTemp))
                continue;
            strText += GetCMDNameFromCode(strTemp);
            strText += "|";
        }
        return strText.EndsWith("|") ? strText.Substring(0, strText.Length - 1) : strText;
    }
    protected string GetFUNCName(object objStr)
    {
        string[] strList = objStr.ToString().Split('|');
        string strText = "";
        foreach (string strTemp in strList)
        {
            if (string.IsNullOrEmpty(strTemp))
                continue;
            strText += GetFUNCTIONNameFromCode(strTemp);
            strText += "|";
        }
        return strText.EndsWith("|") ? strText.Substring(0, strText.Length - 1) : strText;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string strID = GetSelectItemID();
        if (!string.IsNullOrEmpty(strID))
        {
            //获取WF_ID 和 WF_TASK_ID
            TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(strID);
            Response.Redirect("WFSettingTaskInputDetail.aspx?WF_ID=" + task.WF_ID + "&WF_TASK_ID=" + task.WF_TASK_ID);
        }
    }
    protected void btnDeleteTask_Click(object sender, EventArgs e)
    {
        string strID = GetSelectItemID();
        TWfSettingTaskVo setStep = new TWfSettingTaskLogic().Details(strID);
        TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
        TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
        TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
        bool bIsSucess = taskLogic.Delete(strID);
        if (bIsSucess)
        {
            cmdLogic.Delete(new TWfSettingTaskCmdVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
            formLogic.Delete(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
        }
        InitUserData();
    }

    protected void myCheckChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (sender as CheckBox).Parent.Parent as GridViewRow;

        foreach (GridViewRow gvr in grdList.Rows)
        {
            if (gvr.RowIndex != currentRow.RowIndex)
            {
                (gvr.FindControl("chkbx") as CheckBox).Checked = false;
            }
        }
    }

    /// <summary>
    /// 获得选择行的ID
    /// </summary>
    /// <returns></returns>
    protected string GetSelectItemID()
    {
        int iIndex = -1;
        foreach (GridViewRow gvr in grdList.Rows)
            if ((gvr.FindControl("chkbx") as CheckBox).Checked)
                iIndex = gvr.RowIndex;
        if (iIndex == -1)
            return "";
        else
            return grdList.DataKeys[iIndex].Value.ToString();
    }
}