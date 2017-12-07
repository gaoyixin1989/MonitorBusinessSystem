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

public partial class Channels_Mis_ReportForms_CompanyFreeDetail :PageBase
{
    DataTable dt = new DataTable();
    public string action = "", strYear = "", strContractCode = "", strMonth = "", strQuarter = "", strContractType = "",
strCompanyName = "", strCompanyId = "";
    public int PageIndex = 0, PageSize = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!String.IsNullOrEmpty(action)) {
            switch (action) {
                case "GetContractFreeDetial":
                    Response.Write(GetContractFreeDetial());
                    Response.End();
                    break;
                case "GetContractFreeDetialList":
                    Response.Write(GetContractFreeDetialList());
                    Response.End();
                    break;
                default: 
                    break;
            }
        }
    }
    /// <summary>
    /// 获取企业收费记录
    /// </summary>
    public string GetContractFreeDetial()
    {
        string result = "";
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.TESTED_COMPANY_ID = strCompanyName;
        dt = new TMisContractFeeLogic().SelectGetCompayFreeDetailTable(objItems,PageIndex,PageSize);
        int CountNum = new TMisContractFeeLogic().SelectGetCompayFreeDetailTableCount(objItems);
        result = LigerGridDataToJson(dt, CountNum);

        return result;
    }

    /// <summary>
    /// 获取企业收费记录明细记录列表
    /// </summary>
    public string GetContractFreeDetialList()
    {
        string result = "";
        dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        objItems.REMARK3 = strYear;
        objItems.REMARK4 = strMonth;
        objItems.REMARK5 = strQuarter;
        objItems.TESTED_COMPANY_ID = strCompanyId;
        dt = new TMisContractFeeLogic().SelectGetCompanyDetailListInfor(objItems, PageIndex, PageSize);
        int CountNum = new TMisContractFeeLogic().SelectGetCompanyDetailListInforCount(objItems);
        result = LigerGridDataToJson(dt, CountNum);
        return result;
    }
    public void GetRequestParam()
    {
        if (!String.IsNullOrEmpty(Request.Params["action"]))
        {
            action = Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageIndex = int.Parse(Request.Params["page"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageSize = int.Parse(Request.Params["pagesize"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["strCompanyName"]))
        {
            strCompanyName = Request.Params["strCompanyName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractCode"]))
        {
            strContractCode = Request.Params["strContractCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMonth"]))
        {
            strMonth = Request.Params["strMonth"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strYear"]))
        {
            strYear = Request.Params["strYear"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strCompanyId"]))
        {
            strCompanyId = Request.Params["strCompanyId"].Trim().ToString();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}