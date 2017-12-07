using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

/// <summary>
/// 功能描述：获取分析完成时间
/// 创建日期：2012-12-03
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_AskingDateSelected : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["strSampleID"] != null)
            this.strSampleID.Value = Request["strSampleID"].ToString();
        if (Request["strResultId"] != null)
            this.strResultId.Value = Request["strResultId"].ToString();

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "getAskingDate")
        {
            strResult = getAskingDate();
            Response.Write(strResult);
            Response.End();
        }
        //保存数据
        if (Request["Status"] != null && Request["Status"].ToString() == "save")
        {
            strResult = SaveAskingDate();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 根据监测类型获取敢为职责用户信息
    /// </summary>
    /// <param name="strMonitorType">监测类型Id</param>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getAskingDate()
    {
        string strAskingDate = "";

        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(Request["strResultId"]);

        if (objTable.Rows.Count > 0)
            strAskingDate = objTable.Rows[0]["ASKING_DATE"].ToString();
        if (strAskingDate != "")
            strAskingDate = DateTime.Parse(strAskingDate).ToString("yyyy-MM-dd");
        return strAskingDate;
    }
    public string SaveAskingDate()
    {
        string strAskingData = Request["ASKING_DATE"] + " " + "23:59:59";

        bool isSuccess = false;

        if (this.strSampleID.Value.Trim() == "")
        {
            isSuccess = new TMisMonitorResultLogic().SaveItemExInfo(Request["strResultId"], Request["ASKING_DATE"], "ASKING_DATE");
        }
        else
        {
            //isSuccess = new TMisMonitorResultLogic().SaveItemExInfo_QY(this.strSampleID.Value.Trim(), "01','00", Request["ASKING_DATE"], "ASKING_DATE");
            isSuccess = new TMisMonitorResultLogic().SaveItemExInfo_MAS(this.strSampleID.Value.Trim(), Request["ASKING_DATE"], "ASKING_DATE");
        }

        return isSuccess == true ? "1" : "0";
    }
}