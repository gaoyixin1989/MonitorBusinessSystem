using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
public partial class Channels_Mis_Monitor_sampling_SamplePointStopList : PageBase
{
    private string strAction="",strRealName = "", strStartDate = "", strEndDate = "", strPointName = "";
    private int PageIndex = 0, PageSize = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParam();
        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                case "GetPointStopList":
                    Response.Write(GetPointStopList());
                    Response.End();
                    break;
                default:
                    break;
            }
        }
    }
    private string GetPointStopList() {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractPlanPointstopVo objItems = new TMisContractPlanPointstopVo();
        objItems.ACTION_USERID = strRealName;
        objItems.CONTRACT_POINT_ID = strPointName;
        objItems.REMARK4 = strStartDate;
        objItems.REMARK5 = strEndDate;
        dt = new TMisContractPlanPointstopLogic().GetStopPointForSampleList(objItems, PageIndex, PageSize);
        int CountNum = new TMisContractPlanPointstopLogic().GetStopPointForSampleListCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }
    private void GetRequestParam() {
        if (!String.IsNullOrEmpty(Request.Params["action"]))
        { 
            strAction=Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageIndex = int.Parse(Request.Params["page"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            PageSize = int.Parse(Request.Params["pagesize"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["strRealName"])) 
        {
            strRealName = Request.Params["strRealName"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strStartDate"])) 
        {
            strStartDate = Request.Params["strStartDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strEndDate"]))
        {
            strEndDate = Request.Params["strEndDate"].Trim().ToString();
        }

        if (!String.IsNullOrEmpty(Request.Params["strPointName"]))
        {
            strPointName = Request.Params["strPointName"].Trim().ToString();
        }
    }
}