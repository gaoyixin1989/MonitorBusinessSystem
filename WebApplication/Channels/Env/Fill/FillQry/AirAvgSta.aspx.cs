using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.FillQry;

/// <summary>
/// 全市空气均值统计报表
/// 创建人：魏林
/// 创建时间：2013-08-29
/// </summary>
public partial class Channels_Env_Fill_FillQry_AirAvgSta : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "GetData":
                        json = GetData();
                        break;
                }

                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }

        }
    }

    #region 获取数据信息

    private string GetData()
    {
        string StartDate = Request["startdate"];
        string EndDate = Request["enddate"];

        DataTable dt = new DataTable();
        
        dt = new FillQryLogic().GetAirAvgData(StartDate, EndDate);

        string json = DataTableToJsonUnsureCol(dt);

        return json;
    }

    #endregion
}