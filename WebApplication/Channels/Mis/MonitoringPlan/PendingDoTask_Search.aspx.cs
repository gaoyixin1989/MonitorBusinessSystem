using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

public partial class Channels_Mis_MonitoringPlan_PendingDoTask_Search : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                    GetDocNo();
            }
        }
    }
    private void GetDocNo()
    {
         DataTable dt =null;
         //当前页面
         int intPageIndex = Convert.ToInt32(Request.Params["page"]);
         //每页记录数
         int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        //if(!string.IsNullOrEmpty(Request["CONTRACT_TYPE"]))
        //{
            dt = new TMisMonitorTaskLogic().GetDocNo(Request["CONTRACT_TYPE"].ToString(), intPageIndex, intPageSize);
           
        //}
        int intTotalCount = new TMisMonitorTaskLogic().GetSelectResultCounts(Request["CONTRACT_TYPE"].ToString());
        string strJson = CreateToJson(dt, intTotalCount);
        Response.Write(strJson);
        Response.End();
    }
}