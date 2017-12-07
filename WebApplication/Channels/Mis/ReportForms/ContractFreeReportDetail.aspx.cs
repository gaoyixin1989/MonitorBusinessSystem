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
public partial class Channels_Mis_ReportForms_ContractFreeReportDetail : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractCode = "", strMonth = "", strQuarter = "", strContractType = "",

strCompanyName = "";

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
    }
    /// <summary>
    /// 获取收费金额明细记录
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
        dt = new TMisContractFeeLogic().SelectTableForContractFree(objItems, objItemsFree, false);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer1.LocalReport.DataSources.Clear();
        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
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