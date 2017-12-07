using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using Microsoft.Reporting.WebForms;
public partial class Channels_Mis_ReportForms_ContractPolSourceReport : PageBase
{
    DataTable dt = new DataTable();
    public string action = "", strContractCode = "", strSampleDate = "", strArea = "", strContractType = "", strCompanyName = "", strReportCode = "", strPolSource = "",strTask_id="";
    public int PageIndex = 0, PageSize = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!String.IsNullOrEmpty(action)) {
            switch (action) { 
                case "GetPolSourceList":
                    Response.Write(GetPolSourceList());
                    Response.End();
                    break;
                case "GetPolSourceDetail":
                    Response.Write(GetPolSourceDetail());
                    Response.End();
                    break;
                default: 
                    break;
            }
        }
    }
    /// <summary>
    /// 污染源列表
    /// </summary>
    public string GetPolSourceList()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt = new TMisMonitorResultLogic().SelectPolSourceListTable(PageIndex,PageSize);
        int Count = new TMisMonitorResultLogic().SelectPolSourceListTableCount();
        DataTable dtTemp=new DataTable();
     
        if(dt.Rows.Count>0){
            string strItemsList="";
               dt.Columns.Add(new DataColumn("ITEM_NAME",typeof(string)));
            for(int i=0;i<dt.Rows.Count;i++){
                    dtTemp=new TMisMonitorResultLogic().GetPolSourceDetail(dt.Rows[i]["ID"].ToString(),0,0);
                if(dtTemp.Rows.Count>0){
                for(int n=0;n<dtTemp.Rows.Count;n++){
                strItemsList+=dtTemp.Rows[n]["ITEM_NAME"].ToString()+",";
                }
                }
                dt.Rows[i]["ITEM_NAME"]=strItemsList;
                dt.AcceptChanges();
            }
        }
        result=LigerGridDataToJson(dt,Count);
        return result;
    }

        /// <summary>
    /// 污染源详细列表
    /// </summary>
    public string GetPolSourceDetail()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt = new TMisMonitorResultLogic().GetPolSourceDetail(strTask_id,PageIndex,PageSize);
        int Count = new TMisMonitorResultLogic().GetPolSourceDetailCount(strTask_id);
        result=LigerGridDataToJson(dt,Count);
        return result;
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
        if (!String.IsNullOrEmpty(Request.Params["strContractCode"]))
        {
            strContractCode = Request.Params["strContractCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strReportCode"]))
        {
            strReportCode = Request.Params["strReportCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPolSource"]))
        {
            strPolSource = Request.Params["strPolSource"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strArea"]))
        {
            strArea = Request.Params["strArea"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSampleDate"]))
        {
            strSampleDate = Request.Params["strSampleDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractType"]))
        {
            strContractType = Request.Params["strContractType"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageIndex = int.Parse(Request.Params["page"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageSize = int.Parse(Request.Params["pagesize"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["strTask_id"]))
        {
            strTask_id = Request.Params["strTask_id"].Trim().ToString();
        }
    }
}