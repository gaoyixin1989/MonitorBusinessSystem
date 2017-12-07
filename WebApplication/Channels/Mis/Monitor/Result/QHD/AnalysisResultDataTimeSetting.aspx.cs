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
/// 功能描述：获取分析仪器使用时间
/// 创建日期：2013-04-22
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QHD_AnalysisResultDataTimeSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["strResultId"] != null)
            this.strResultId.Value = Request["strResultId"].ToString();
        if (Request["timeType"] != null)
            this.timeType.Value = Request["timeType"].ToString();

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "getAnalysisResultDataTime")
        {
            strResult = getAnalysisResultDataTime();
            Response.Write(strResult);
            Response.End();
        }
        //保存数据
        if (Request["Status"] != null && Request["Status"].ToString() == "save")
        {
            strResult = SaveAnalysisResultDataTime();
            Response.Write(strResult);
            Response.End();
        }
    }
    public string getAnalysisResultDataTime()
    {
        string strResultTime = "";
        if (this.timeType.Value == "getStartTime")
            strResultTime = new TMisMonitorResultLogic().Details(this.strResultId.Value).APPARTUS_START_TIME;
        else
            strResultTime = new TMisMonitorResultLogic().Details(this.strResultId.Value).APPARTUS_END_TIME;

        return strResultTime;
    }
    public string SaveAnalysisResultDataTime()
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = this.strResultId.Value;
        if (this.timeType.Value == "getStartTime")
            TMisMonitorResultVo.APPARTUS_START_TIME = Request["AnalysisResultDataTime"];
        else
            TMisMonitorResultVo.APPARTUS_END_TIME = Request["AnalysisResultDataTime"];

        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);
        return isSuccess == true ? "1" : "0";
    }
}