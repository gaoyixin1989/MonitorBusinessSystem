using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Web.Services;
using System.Data;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

public partial class Channels_Base_Search_SearchQHD_TotalSearchForSample : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        //获取样品
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getSampleInfo")
        {
            strResult = GetSampleInfo();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取质控类型名称
    /// </summary>
    /// <param name="strValue">质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcType(string strValue)
    {
        string strResult = "";
        switch (strValue)
        {
            case "0":
                strResult = "原始样";
                break;
            case "1":
                strResult = "现场空白";
                break;
            case "2":
                strResult = "现场加标";
                break;
            case "3":
                strResult = "现场平行";
                break;
            case "4":
                strResult = "实验室密码平行";
                break;
            case "5":
                strResult = "实验室空白";
                break;
            case "6":
                strResult = "实验室加标";
                break;
            case "7":
                strResult = "实验室明码平行";
                break;
            case "8":
                strResult = "标准样";
                break;
        }
        return strResult;
    }

    /// <summary>
    /// 获得点位信息
    /// </summary>
    /// <returns></returns>
    protected string GetSampleInfo()
    {
        int intTotalCount = 0;
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();//结果集
        string strTaskID = !string.IsNullOrEmpty(Request.QueryString["id"]) ? Request.QueryString["id"].ToString() : "";
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["strContractType"]) ? Request.QueryString["strContractType"].ToString() : "";
        string strMonitorID = !string.IsNullOrEmpty(Request.QueryString["strMonitorId"]) ? Request.QueryString["strMonitorId"].ToString() : "";
        //所有样品信息
        if (!string.IsNullOrEmpty(Request.QueryString["QC"]) && Request.QueryString["QC"] == "true")
        {
           // intTotalCount = new TMisMonitorSampleInfoLogic().GetAllSampleInfoCountByTask(strTaskID, strMonitorID);
            dt = new TMisMonitorSampleInfoLogic().GetAllSampleInfoSourceByTask(strTaskID, strMonitorID);
        }
        else
        {
            intTotalCount = new TMisMonitorSampleInfoLogic().GetSampleInfoCountByTask(strTaskID, strMonitorID);
            dt = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTask(strTaskID, strMonitorID, intPageIndex, intPageSize);
        }
        return CreateToJson(dt, intTotalCount);
    }
}