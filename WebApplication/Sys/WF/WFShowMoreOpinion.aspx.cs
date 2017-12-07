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

public partial class Sys_WF_WFShowMoreOpinion : PageBaseForWF
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
        DataTable dt = new TWfInstTaskOpinionsLogic().SelectByTable(new TWfInstTaskOpinionsVo() { WF_INST_ID = WF_INST_ID.Value });
        DataList1.DataSource = dt.DefaultView;
        DataList1.DataBind();
    }


    public string GetUserName(object objUserID)
    {
        if (null == objUserID)
            return "";
        return GetUserNameFromID(objUserID.ToString(), true);
    }

    protected void btnAddOpinion_Click(object sender, EventArgs e)
    {
        if (txtOpinionText.Text.Trim() == "")
            return;

        TWfInstTaskOpinionsVo opinion = new TWfInstTaskOpinionsVo();
        opinion.ID = this.GetGUID();
        opinion.WF_INST_ID = WF_INST_ID.Value;
        opinion.WF_INST_TASK_ID = WF_INST_TASK_ID.Value;
        opinion.WF_IT_OPINION = txtOpinionText.Text;
        opinion.WF_IT_OPINION_TYPE = "";//评论类型，后续开发
        opinion.WF_IT_OPINION_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;
        opinion.WF_IT_SHOW_TYPE = "";//评论显示问题，后续开发，主要分自己可见，还是所有人可见。
        opinion.WF_IT_OPINION_TIME = GetDateTimeToStanString();
        //写入数据库
        bool bisSuccess = new TWfInstTaskOpinionsLogic().Create(opinion);
        //string strMsg = "";
        if (bisSuccess)
        {
            //strMsg = "增加评论成功";
            InitListData(WF_INST_ID.Value, WF_INST_TASK_ID.Value);
            txtOpinionText.Text = "";
            string strMessage = LogInfo.UserInfo.USER_NAME + "为环节:" + WF_INST_TASK_ID.Value + "添加评论成功";
            (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFEidtSettingInfo, "", strMessage); 
        }
        //else
            //strMsg = "增加评论失败";
        //LigerDialogAlert(strMsg, "warn");
    }
}