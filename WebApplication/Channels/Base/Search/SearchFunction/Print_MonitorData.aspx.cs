using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Collections;

using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Base_Search_SearchFunction_Print_MonitorData : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string contracttype = "";
        string companyname = "";
        string monitortype = "";
        string pointname = "";
        string datestart = "";
        string dateend = "";
        string itemid = "";
        DataTable dt = new DataTable();
        CommonLogic com = new CommonLogic();

        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            switch (Request.QueryString["type"].ToString())
            {
                case "Env":
                    monitortype = Request.QueryString["monitortype"].ToString();
                    pointname = Request.QueryString["pointname"].ToString();
                    datestart = Request.QueryString["datestart"].ToString();
                    dateend = Request.QueryString["dateend"].ToString();
                    itemid = Request.QueryString["itemid"].ToString();
                    dt = com.GetEnvMoniotrData(monitortype, pointname, datestart, dateend, itemid);
                    break;
                case "Poll":
                    contracttype = Request.QueryString["contracttype"].ToString();
                    companyname = Request.QueryString["companyname"].ToString();
                    monitortype = Request.QueryString["monitortype"].ToString();
                    pointname = Request.QueryString["pointname"].ToString();
                    datestart = Request.QueryString["datestart"].ToString();
                    dateend = Request.QueryString["dateend"].ToString();
                    itemid = Request.QueryString["itemid"].ToString();
                    dt = com.GetPollMoniotrData(contracttype, companyname, monitortype, pointname, datestart, dateend, itemid);
                    break;
                default:
                    break;
            }
        }

        dt.Columns.Remove("ID");

        //标题数组
        ArrayList arrTitle = new ArrayList();

        //列名集合
        ArrayList arrData = new ArrayList();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            arrData.Add(new string[2] { (i + 1).ToString(), dt.Columns[i].ColumnName.ToString().Split('@')[2] });
            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToString().Split('@')[2];
        }
        //结尾数组
        ArrayList arrEnd = new ArrayList();

        new ExcelHelper().RenderDataTableToExcel(dt, "数据汇总表.xls", "../../../../TempFile/DataTotal.xls", "数据汇总表", 1, true, arrTitle, arrData, arrEnd);
    }

}