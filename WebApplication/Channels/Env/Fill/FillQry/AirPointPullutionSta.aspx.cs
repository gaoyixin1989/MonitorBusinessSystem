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
/// 空气监测点首要污染物统计报表
/// 创建人：魏林
/// 创建时间：2013-09-03
/// </summary>
public partial class Channels_Env_Fill_FillQry_AirPointPullutionSta : PageBase
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
                    case "GetPoint":
                        json = GetPoint();
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
        string PointCode = Request["pointcode"];

        DataTable dt = new DataTable();
        
        dt = new FillQryLogic().GetAirPointPullutionData(StartDate, EndDate, PointCode);

        string json = DataTableToJsonUnsureCol(dt);

        return json;
    }

    #endregion

    #region 获取监测点信息

    private string GetPoint()
    {
        string StartDate = Request["startdate"];
        string EndDate = Request["enddate"];

        string Select = string.Empty;
        string TableName = string.Empty;
        string Where = "1=1";
        DataTable dt = new DataTable();

        Select = "distinct POINT_CODE CODE, POINT_NAME NAME";
        TableName = "T_ENV_P_AIR";

        if (StartDate.Length > 0 && EndDate.Length > 0)
        {
            StartDate = DateTime.Parse(StartDate).ToString("yyyy-MM-01");
            EndDate = DateTime.Parse(EndDate).ToString("yyyy-MM-01");
            Where += " and CAST(YEAR+'-'+MONTH+'-01' as DATETIME) between '" + StartDate + "' and '" + EndDate + "' and IS_DEL='0'";
        }
        else if (StartDate.Length > 0 && EndDate.Length == 0)
        {
            StartDate = DateTime.Parse(StartDate).ToString("yyyy-MM-01");
            Where += " and CAST(YEAR+'-'+MONTH+'-01' as DATETIME) >='" + StartDate + "' and IS_DEL='0'";
        }
        else if (StartDate.Length == 0 && EndDate.Length > 0)
        {
            EndDate = DateTime.Parse(EndDate).ToString("yyyy-MM-01");
            Where += " and CAST(YEAR+'-'+MONTH+'-01' as DATETIME) <='" + EndDate + "' and IS_DEL='0'";
        }

        dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                
        //加入全部
        DataRow dr = dt.NewRow();
        dr["NAME"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);
        string json = DataTableToJson(dt);
        return json;
    }

    #endregion
}