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
using Microsoft.Reporting.WebForms;
public partial class Channels_Mis_ReportForms_ContractFreeReport_Sum : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractCode = "", strMonth = "", strQuarter = "", strContractType = "", strCompanyName = "";

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
    /// 获取已缴费的
    /// </summary>
    public void GetAnalyseFinished()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.CONTRACT_CODE = strContractCode;
        objItems.TESTED_COMPANY_ID = strCompanyName;
        TMisContractFeeVo objItemsFree = new TMisContractFeeVo();
        objItemsFree.IF_PAY = "0";
        dt = new TMisContractFeeLogic().SelectTableForContractFree(objItems,objItemsFree,true);

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
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.CONTRACT_CODE = strContractCode;
        objItems.TESTED_COMPANY_ID = strCompanyName;
        TMisContractFeeVo objItemsFree = new TMisContractFeeVo();
        dt = new TMisContractFeeLogic().SelectTableForContractFreeSum(objItems, objItemsFree);
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
    /// <summary>
    /// 获取正常完成的列表
    /// </summary>
    public void GetAnalyseFinishing()
    {
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.CONTRACT_TYPE = strContractType;
        objItems.CONTRACT_CODE = strContractCode;
        objItems.TESTED_COMPANY_ID = strCompanyName;
        TMisContractFeeVo objItemsFree = new TMisContractFeeVo();
        dt = new TMisContractFeeLogic().SelectTableForContractFree(objItems, objItemsFree, true);

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
        if (!String.IsNullOrEmpty(Request.Params["strCompanyName"]))
        {
            strCompanyName = Request.Params["strCompanyName"].Trim().ToString();
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