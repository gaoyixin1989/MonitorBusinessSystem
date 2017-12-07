using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Sys.Resource;
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplingQCStep : PageBase
{
    private string strResult = "";
    private string strPlanId = "", strTask_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!IsPostBack) {
            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo()
    {
        //if (!String.IsNullOrEmpty(strTask_Id))
        //{
        //    TMisContractVo objContracDetial = new TMisContractLogic().Details(strTask_Id);
        //    return ToJson(objContracDetial);
        //}
        //else {
        //    return "";
        //}

        if (!String.IsNullOrEmpty(strPlanId))
        {
            //TMisContractVo objContracDetial = new TMisContractLogic().Details(strTask_Id);
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.PLAN_ID = strPlanId;
            objTask = new TMisMonitorTaskLogic().Details(objTask);
            //TMisMonitorTaskVo objContracDetial = new TMisMonitorTaskLogic().Details(strTask_Id);
            return ToJson(objTask);
        }
        else
        {
            return "";
        }
    }

    private void GetRequestParam()
    {
        if (!String.IsNullOrEmpty(Request.Params["strTask_Id"])) {
            strTask_Id = Request.Params["strTask_Id"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPlanId"]))
        {
            strPlanId = Request.Params["strPlanId"].Trim();
        }
    }
}