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

public partial class Sys_WF_WFStepDown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowMsg(Request.QueryString[TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD]);
        }
    }
    public void ShowMsg(string strInstStepID)
    {
        TWfInstTaskDetailVo detail = new TWfInstTaskDetailLogic().Details(strInstStepID);
        lblMsg.InnerHtml = "<h1 style='font-size:18px;'>处理结果</h1><br />流程实例编号：" + detail.ID + "|<br />流程编码：" + detail.WF_ID + "|<br /> 环节编号：" + detail.WF_TASK_ID + "| <br /> 起始时间：" + detail.INST_TASK_STARTTIME + "|<br /> 结束时间：" + detail.INST_TASK_ENDTIME + "|<br /> 目标处理人：" + detail.OBJECT_USER;
    }
}