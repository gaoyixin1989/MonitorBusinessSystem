using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using System.IO;
using System.Text;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：监测数据查询
/// 创建时间：魏林 2014-02-18
/// </summary>
public partial class Channels_Base_Search_MonitorDataSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["type"]))
            {
                switch (Request["type"])
                {
                    case "GetEnvData":
                        GetEnvData();
                        break;
                    case "GetPollData":
                        GetPollData();
                        break;
                }
            }
        }
    }

    #region 获取环境质量监测数据
    private void GetEnvData()
    {
        string monitortype = Request["monitortype"];
        string pointname = Request["pointname"];
        string datestart = Request["datestart"];
        string dateend = Request["dateend"];
        string itemid = Request["itemid"];

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetEnvMoniotrData(monitortype, pointname, datestart, dateend, itemid);
        
        string json = DataTableToJsonUnsureCol(dt);

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }
    #endregion

    #region 获取污染源企业监测数据
    private void GetPollData()
    {
        string contracttype = Request["contracttype"];
        string companyname = Request["companyname"];
        string monitortype = Request["monitortype"];
        string pointname = Request["pointname"];
        string datestart = Request["datestart"];
        string dateend = Request["dateend"];
        string itemid = Request["itemid"];

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetPollMoniotrData(contracttype, companyname, monitortype, pointname, datestart, dateend, itemid);

        string json = DataTableToJsonUnsureCol(dt);
        Alert("");
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }
    #endregion
}