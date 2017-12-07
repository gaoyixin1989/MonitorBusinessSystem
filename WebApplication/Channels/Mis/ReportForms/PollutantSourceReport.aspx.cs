using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;

using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;

using Microsoft.Reporting.WebForms;
public partial class Channels_Mis_ReportForms_PollutantSourceReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractType = "", strCompanyName = "", strPointName = "", strItemName = "", strMonitor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!Page.IsPostBack)
        {
            GetTaskListData();
        }
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public void GetTaskListData()
    {

        //获取数据集
        GetAnalyseFinished();
        FillChart();
    }
    /// <summary>
    /// 获取污染源监测列表
    /// </summary>
    public void GetAnalyseFinished()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strMonitor;
        objItems.REMARK4 = strContractType;
        objItems.REMARK5 = strYear;

        TMisMonitorResultAppVo objItemsFree = new TMisMonitorResultAppVo();
        objItemsFree.REMARK3 = strCompanyName;
        objItemsFree.REMARK4 = strItemName;
        objItemsFree.REMARK5 = strPointName;
        dt = new TMisMonitorResultAppLogic().GetPollutantSourceReport(objItemsFree, objItems);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer1.LocalReport.DataSources.Clear();
        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
    }

    /// <summary>
    /// 饼图绑定
    /// </summary>
    public void FillChart()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strMonitor;
        objItems.REMARK4 = strContractType;
        objItems.REMARK5 = strYear;

        TMisMonitorResultAppVo objItemsFree = new TMisMonitorResultAppVo();
        objItemsFree.REMARK3 = strCompanyName;
        objItemsFree.REMARK4 = strItemName;
        objItemsFree.REMARK5 = strPointName;
        dt = new TMisMonitorResultAppLogic().GetPollutantSourceReport(objItemsFree, objItems);
        if (dt.Rows.Count == 0)
        {
            //return;
        }
        ReportDataSource rds = new ReportDataSource("DataSet2", dt);

        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
    }

    public void GetRequestParam()
    {
        if (!String.IsNullOrEmpty(Request.Params["action"]))
        {
            action = Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strCompanyName"]))
        {
            strCompanyName = Request.Params["strCompanyName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPointName"]))
        {
            strPointName = Request.Params["strPointName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMonitor"]))
        {
            strMonitor = Request.Params["strMonitor"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strItemName"]))
        {
            strItemName = Request.Params["strItemName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractType"]))
        {
            strContractType = Request.Params["strContractType"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strYear"]))
        {
            strYear = Request.Params["strYear"].Trim().ToString();
        }

    }
}