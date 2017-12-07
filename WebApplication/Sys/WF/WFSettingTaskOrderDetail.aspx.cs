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

public partial class Sys_WF_WFSettingTaskOrderDetail : PageBaseForWF
{
    private DataTable GetStepListTable
    {
        get { return (DataTable)ViewState["StepList"]; }
        set { ViewState["StepList"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WF_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
            InitUserData();
        }
    }



    private void InitUserData()
    {
        TWfSettingFlowVo twf = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = WF_ID.Value });
        lblCurFlowName.Text = twf.WF_CAPTION;
        lblCurFlowName.ForeColor = System.Drawing.Color.Red;
        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
        TWfSettingTaskVo tv = new TWfSettingTaskVo();
        tv.WF_ID = WF_ID.Value;
        tv.SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD;
        tv.SORT_TYPE = " ASC ";
        DataTable dt = logic.SelectByTable(tv);
        grdList.DataSource = dt.DefaultView;
        grdList.DataBind();
        GetStepListTable = dt;
    }

    public void btnAddTaskDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("WFSettingTaskInputDetail.aspx?WF_ID=" + WF_ID.Value);
    }

    public void grdList_Command(object sender, GridViewCommandEventArgs e)
    {
        string strID = e.CommandArgument.ToString();
        if (e.CommandName == "iUp")
        {
            for (int i = 0; i < GetStepListTable.Rows.Count; i++)
            {
                if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
                {
                    //开始排序
                    if (i == 0)
                        return;
                    string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                    string strOrderFlag2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                    string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                    string strID2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.ID_FIELD].ToString();
                    TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                    TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                    TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                    tempLogic.Edit(temp1);
                    tempLogic.Edit(temp2);

                    string strMessage = LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                    (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
                }
            }
        }
        if (e.CommandName == "iDown")
        {
            for (int i = 0; i < GetStepListTable.Rows.Count; i++)
            {
                if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
                {
                    //开始排序
                    if (i == GetStepListTable.Rows.Count - 1)
                        return;
                    string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                    string strOrderFlag2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                    string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                    string strID2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.ID_FIELD].ToString();
                    TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                    TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                    TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                    tempLogic.Edit(temp1);
                    tempLogic.Edit(temp2);

                    string strMessage = LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                    (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
                }
            }
        }
        InitUserData();
    }
    protected void btnGoto_Click(object sender, EventArgs e)
    {
        Response.Redirect("WFSettingTaskInput.aspx?WF_ID=" + WF_ID.Value);
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
    protected void btnUpTo_Click(object sender, EventArgs e)
    {
        string strID = GetSelectItemID();
        for (int i = 0; i < GetStepListTable.Rows.Count; i++)
        {
            if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
            {
                //开始排序
                if (i == 0)
                    return;
                string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strOrderFlag2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                string strID2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.ID_FIELD].ToString();
                TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                tempLogic.Edit(temp1);
                tempLogic.Edit(temp2);

                string strMessage = LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
            }
        }
        InitUserData();
    }
    protected void btnDownTo_Click(object sender, EventArgs e)
    {
        string strID = GetSelectItemID();
        for (int i = 0; i < GetStepListTable.Rows.Count; i++)
        {
            if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
            {
                //开始排序
                if (i == GetStepListTable.Rows.Count - 1)
                    return;
                string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strOrderFlag2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                string strID2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.ID_FIELD].ToString();
                TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                tempLogic.Edit(temp1);
                tempLogic.Edit(temp2);

                string strMessage = LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
            }
        }
        InitUserData();
    }
}