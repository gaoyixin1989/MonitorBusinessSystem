using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：报告份数统计表(清远)
/// 创建时间：2014-11-21
/// 创建人：魏林
/// </summary>
public partial class Channels_Statistice_ReportDataAccount : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetDataList")
        {
            GetDataList();
            Response.End();
        }
        if (Request.Params["Action"] == "getMonitorType")
        {
            getMonitorType();
            Response.End();
        }
    }

    //获取监测项目
    private void GetDataList()
    {
        //int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        //int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhContractType = (Request.Params["SrhCONTRACT_TYPE"] != null) ? Request.Params["SrhCONTRACT_TYPE"] : "";
        string strSrhMonitorType = (Request.Params["SrhMONITOR_TYPE"] != null) ? Request.Params["SrhMONITOR_TYPE"] : "";
        string strSrhSampleDate_Begin = (Request.Params["SrhSampleDate_Begin"] != null) ? Request.Params["SrhSampleDate_Begin"] : "";
        string strSrhSampleDate_End = (Request.Params["SrhSampleDate_End"] != null) ? Request.Params["SrhSampleDate_End"] : "";

        DataTable dt = new TMisMonitorResultLogic().SelectReportDataLst(strSrhContractType, strSrhMonitorType, strSrhSampleDate_Begin, strSrhSampleDate_End);
        
        string strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
    }

    private void getMonitorType()
    {
        DataTable dt = new TBaseMonitorTypeInfoLogic().getMonitorType();

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
    }
}