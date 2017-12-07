using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Data;

public partial class Channels_Mis_MonitoringPlan_MonitorAssigRpt : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["type"]))
            {
                switch (Request["type"])
                {
                    case "GetYear":
                        GetYear();
                        break;
                    case "GetMonth":
                        GetMonth();
                        break;
                    case "getGridInfo":
                        getGridInfo();
                        break;
                }
            }
        }
    }
    #region 获取年份

    private void GetYear()
    {
        string json = getYearInfo();
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取月份
    private void GetMonth()
    {
        string json = getMonthInfo();
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    private void getGridInfo()
    {
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        string year = string.Empty;
        string month = string.Empty;
        if (Request["year"] != null)
        {
            year = Request["year"].ToString();
        }
        if (Request["month"] != null)
        {
            month = Request["month"].ToString();
        }
        DataTable dt = new TMisMonitorTaskLogic().GetInfo(year, month,intPageIndex,intPageSize);
        int Totalcount = new TMisMonitorTaskLogic().GetInfoCount(year, month);
        string strJson = CreateToJson(dt, Totalcount);
        Response.Write(strJson);
        Response.End();
    }
}