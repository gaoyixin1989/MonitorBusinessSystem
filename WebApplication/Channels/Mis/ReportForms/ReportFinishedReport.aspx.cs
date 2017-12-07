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
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using Microsoft.Reporting.WebForms;

public partial class Channels_Mis_ReportForms_ReportFinishedReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractCode = "", strMonth = "", strQuarter = "", strArea = "", strDept = "", strUserName = "", strTestCompany = "", strContractType = "";

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

        //获取正常完成的的
        GetAnalyseFinished();
        //获取超时完成的
        GetAnalyseFinishing();
        FillChart();
    }
    /// <summary>
    /// 获取正常任务
    /// </summary>
    public void GetAnalyseFinished()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.CONTRACT_CODE = strContractCode;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.TESTED_COMPANY_ID = strTestCompany;
        objItems.REMARK5 = strDept;
        TMisMonitorTaskVo objMvo = new TMisMonitorTaskVo();
        objMvo.REMARK3 = strYear;
        objMvo.REMARK4 = strMonth;
        objMvo.REMARK5 = strQuarter;
        dt = new TMisMonitorTaskLogic().SeletByTableReportFinished(objMvo,objItems ,true);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer1.LocalReport.DataSources.Clear();
        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
    }
    /// <summary>
    /// 获取超时完成的列表
    /// </summary>
    public void GetAnalyseFinishing()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.CONTRACT_CODE = strContractCode;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.TESTED_COMPANY_ID = strTestCompany;
        objItems.REMARK5 = strDept;
        TMisMonitorTaskVo objMvo = new TMisMonitorTaskVo();
        objMvo.REMARK3 = strYear;
        objMvo.REMARK4 = strMonth;
        objMvo.REMARK5 = strQuarter;
        dt = new TMisMonitorTaskLogic().SeletByTableReportFinished(objMvo, objItems, false);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer2.LocalReport.DataSources.Clear();
        this.reportViewer2.LocalReport.DataSources.Add(rds);
        this.reportViewer2.LocalReport.Refresh();
    }

    /// <summary>
    /// 饼图绑定
    /// </summary>
    public void FillChart()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.CONTRACT_CODE = strContractCode;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.TESTED_COMPANY_ID = strTestCompany;
        objItems.REMARK5 = strDept;
        TMisMonitorTaskVo objMvo = new TMisMonitorTaskVo();
        objMvo.REMARK3 = strYear;
        objMvo.REMARK4 = strMonth;
        objMvo.REMARK5 = strQuarter;
        dt = new TMisMonitorTaskLogic().SeletByTableReportFinishedCount(objMvo,objItems);
        if (dt.Rows.Count == 0)
        {
            DataRow dr = dt.NewRow();
            dr["FINISHSUM"] = "0";
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
        if (!String.IsNullOrEmpty(Request.Params["strUserName"]))
        {
            strUserName = Request.Params["strUserName"].Trim().ToString();
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
        if (!String.IsNullOrEmpty(Request.Params["strContractType"]))
        {
            strContractType = Request.Params["strContractType"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strQuarter"]))
        {
            strQuarter = Request.Params["strQuarter"].Trim().ToString();

            if (strQuarter == "01")
            {
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