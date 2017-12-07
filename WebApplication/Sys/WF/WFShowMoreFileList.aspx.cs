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
public partial class Sys_WF_WFShowMoreFileList : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strInstWFID = Request.QueryString[TWfInstTaskDetailVo.WF_INST_ID_FIELD];
            string strInstStepID = Request.QueryString[TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD];
            WF_INST_ID.Value = strInstWFID;
            WF_INST_TASK_ID.Value = strInstStepID;
            InitTitle();
            InitListData(strInstWFID, strInstStepID);
        }
    }


    public void InitTitle()
    {
        TWfInstTaskDetailVo detail = new TWfInstTaskDetailLogic().Details(WF_INST_TASK_ID.Value);
        TWfInstControlVo control = new TWfInstControlLogic().Details(WF_INST_ID.Value);
        lblWFName.Text = control.WF_CAPTION;
        lblStepName.Text = detail.INST_TASK_CAPTION;
        lblServiceCode.Text = control.WF_SERVICE_CODE;
        lblCreateTime.Text = control.WF_STARTTIME;

    }

    public void InitListData(string strInstWFID, string strInstStepID)
    {
        if (string.IsNullOrEmpty(strInstWFID) || string.IsNullOrEmpty(strInstStepID))
            return;
        DataTable dt = new TWfInstFileListLogic().SelectByTable(new TWfInstFileListVo() { WF_INST_ID = WF_INST_ID.Value, SORT_FIELD = TWfInstFileListVo.UPLOAD_TIME_FIELD, SORT_TYPE = "DESC" });
        DataList1.DataSource = dt.DefaultView;
        DataList1.DataBind();
    }

    public string GetFileName(object objFileName)
    {
        if (null == objFileName)
            return "";
        if (objFileName.ToString().Length < 18)
            return "";
        return objFileName.ToString().Substring(17);
    }

    public string GetUserName(object objUserID)
    {
        if (null == objUserID)
            return "";
        return GetUserNameFromID(objUserID.ToString(), true);
    }

    protected void btnAddMoreFile_Click(object sender, EventArgs e)
    {



    }
}