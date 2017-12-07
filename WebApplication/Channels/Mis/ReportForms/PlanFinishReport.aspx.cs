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
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using Microsoft.Reporting.WebForms;
/// <summary>
/// 功能描述：预约完成情况统计
/// 创建时间：2013-01-02
/// 创建人：胡方扬
/// </summary>

public partial class Channels_Mis_ReportForms_PlanFinishReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractCode = "", strMonth = "", strQuarter = "", strArea = "", strDept = "", strTestCompany = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();

        if (!IsPostBack)
        {
            GetTaskListData();
        }

    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public void GetTaskListData()
    {
        GetFinishedPlan();
        GetFinishingPlan();
        FillChart();
    }
    /// <summary>
    /// 获取已完成的预约任务
    /// </summary>
    public void GetFinishedPlan() {
        dt = new DataTable();
        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.TASK_STATUS = "11";
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_CODE = strContractCode;
        TMisMonitorTaskCompanyVo objComItems = new TMisMonitorTaskCompanyVo();
        objComItems.AREA = strArea;
        objComItems.COMPANY_NAME = strTestCompany;

        dt = new TMisMonitorTaskLogic().GetTaskFinishedChart(objItems,objComItems,strDept,true);

        ReportDataSource rds = new ReportDataSource("DataSet1",dt);
        this.reportViewer1.LocalReport.DataSources.Clear();
        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
    }

    /// <summary>
    /// 饼图绑定
    /// </summary>
    public void FillChart() {

        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_CODE = strContractCode;
        TMisMonitorTaskCompanyVo objComItems = new TMisMonitorTaskCompanyVo();
        objComItems.AREA = strArea;
        objComItems.COMPANY_NAME = strTestCompany;

        dt = new TMisMonitorTaskLogic().GetTaskChartCountWithStatus(objItems, objComItems,strDept);
        if (dt.Rows.Count == 0) {
            DataRow dr = dt.NewRow();
            dr["FINISHSUM"]= "0";
            dr["FINISHTYPE"] = "无数据集";

            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        ReportDataSource rds = new ReportDataSource("DataSet2", dt);

        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();

        this.reportViewer2.LocalReport.DataSources.Add(rds);
        this.reportViewer2.LocalReport.Refresh();
    }
    /// <summary>
    /// 获取进行中的预约任务
    /// </summary>
    public void GetFinishingPlan()
    {
        dt = new DataTable();
        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.TASK_STATUS = "11";
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_CODE = strContractCode;
        TMisMonitorTaskCompanyVo objComItems = new TMisMonitorTaskCompanyVo();
        objComItems.AREA = strArea;
        objComItems.COMPANY_NAME = strTestCompany;

        dt = new TMisMonitorTaskLogic().GetTaskFinishedChart(objItems, objComItems, strDept, false);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer2.LocalReport.DataSources.Clear();
        this.reportViewer2.LocalReport.DataSources.Add(rds);
        this.reportViewer2.LocalReport.Refresh();
    }

    public void GetRequestParam()
    {
        if (!String.IsNullOrEmpty(Request.Params["action"]))
        {
            action = Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractCode"]))
        {
            strContractCode = Request.Params["strContractCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strDept"]))
        {
            strDept = Request.Params["strDept"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strArea"]))
        {
            strArea = Request.Params["strArea"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strTestCompany"]))
        {
            strTestCompany = Request.Params["strTestCompany"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMonth"]))
        {
            strMonth = Request.Params["strMonth"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strYear"]))
        {
            strYear = Request.Params["strYear"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strQuarter"]))
        {
            strQuarter = Request.Params["strQuarter"].Trim().ToString();

            if (strQuarter == "01") {
                strQuarter = "01";
            }
            if (strQuarter == "02")
            {
                strQuarter = "04";
            }
            if (strQuarter == "03")
            {
                strQuarter = "07";
            }
            if (strQuarter == "04")
            {
                strQuarter = "10";
            }
        }
    }
}