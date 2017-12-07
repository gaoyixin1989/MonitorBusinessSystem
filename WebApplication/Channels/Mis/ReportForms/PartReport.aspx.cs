using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using Microsoft.Reporting.WebForms;
public partial class Channels_Mis_ReportForms_PartReport : PageBase
{
    private string strPartCollarId = "", strReal_Name = "", strBeginDate = "", strEndDate = "", strPartCode = "", strPartName = "", strPartId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!IsPostBack) {
            PartCollarInfor();
        }
    }

    public void PartCollarInfor() 
    {
        DataTable dt = new DataTable();
        TOaPartCollarVo objItems = new TOaPartCollarVo();
        TOaPartInfoVo objItemParts = new TOaPartInfoVo();
        if (!String.IsNullOrEmpty(strPartCollarId))
        {
            objItems.ID = strPartCollarId;
        }
        objItems.USER_ID = strReal_Name;
        if (!String.IsNullOrEmpty(strBeginDate) || !String.IsNullOrEmpty(strEndDate))
        {
            objItems.REMARK4 = strBeginDate;
            objItems.REMARK5 = strEndDate;
        }

        if (!String.IsNullOrEmpty(strPartCode) || !String.IsNullOrEmpty(strPartName) || !String.IsNullOrEmpty(strPartId))
        {
            objItemParts.ID = strPartId;
            objItemParts.PART_NAME = strPartName;
            objItemParts.PART_CODE = strPartCode;
        }

        dt = new TOaPartCollarLogic().SelectUnionPartByTable(objItems, objItemParts, 0, 0);

        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        this.reportViewer1.LocalReport.DataSources.Clear();
        this.reportViewer1.LocalReport.DataSources.Add(rds);
        this.reportViewer1.LocalReport.Refresh();
    }

    public void GetRequestParam()
    {
        if (!String.IsNullOrEmpty(Request.Params["strPartCollarId"]))
        {
            strPartCollarId = Request.Params["strPartCollarId"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strReal_Name"]))
        {
            strReal_Name = Request.Params["strReal_Name"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strBeginDate"]))
        {
            strBeginDate = Request.Params["strBeginDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strEndDate"]))
        {
            strEndDate = Request.Params["strEndDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPartCode"]))
        {
            strPartCode = Request.Params["strPartCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPartName"]))
        {
            strPartName = Request.Params["strPartName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPartId"]))
        {
            strPartId = Request.Params["strPartId"].Trim().ToString();
        }
    }
}