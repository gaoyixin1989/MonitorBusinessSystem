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

public partial class Sys_WF_WFTaskDetailView : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData(Request.QueryString["ID"]);
        }
    }

    public void InitData(string strInstStepID)
    {
        string strDataModel = "[{0}] 由 [{1}] {2}";

        if (null == strInstStepID || strInstStepID.Trim() == "")
            return;
        TWfInstTaskDetailVo titdv = new TWfInstTaskDetailLogic().Details(strInstStepID);
        TWfInstControlVo tcv = new TWfInstControlLogic().Details(titdv.WF_INST_ID);
        TWfSettingTaskVo ttv = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = titdv.WF_ID, WF_TASK_ID = titdv.WF_TASK_ID });
        TWfInstTaskDetailVo titdv2 = new TWfInstTaskDetailLogic().Details(new TWfInstTaskDetailVo() { WF_TASK_ID = tcv.WF_TASK_ID });
        TWfInstTaskDetailVo titdvPre = new TWfInstTaskDetailLogic().Details(titdv.PRE_INST_TASK_ID);
        Label1.Text = tcv.WF_SERVICE_NAME;
        Label2.Text = tcv.WF_STARTTIME;
        Label3.Text = GetWFStateName(tcv.WF_STATE);
        Label5.Text = (titdv2.ID == "" ? "该流程已处理完毕" : titdv2.INST_TASK_CAPTION);
        Label6.Text = string.Format(strDataModel, titdv.INST_TASK_STARTTIME, GetUserNameFromID(titdv.SRC_USER, true), "创建任务");
        Label7.Text = string.Format(strDataModel, titdv.CFM_TIME, GetUserNameFromID(titdv.CFM_USER, true), "认领任务");
        Label8.Text = string.Format(strDataModel, titdv.INST_TASK_ENDTIME, GetUserNameFromID(titdv.REAL_USER, true), "完成任务");
        Label9.Text = (titdvPre.ID==""?"无上一环节":(titdvPre.INST_TASK_CAPTION + ":" + string.Format(strDataModel, titdvPre.INST_TASK_ENDTIME, GetUserNameFromID(titdvPre.REAL_USER, true), "完成")));


    }

}