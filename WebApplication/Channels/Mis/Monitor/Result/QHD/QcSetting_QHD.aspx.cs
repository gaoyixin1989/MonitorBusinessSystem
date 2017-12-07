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
/// 功能描述：秦皇岛实验室质控设置
/// 创建日期：2013-4-10
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QHD_QcSetting_QHD : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["resultid"] != null)
        {
            this.resultid.Value = Request["resultid"].ToString();
            this.result.Value = Request["result"].ToString();
        }
        //保存数据信息
        if (Request["status"] != null && Request["status"].ToString() == "save")
        {
            strResult = saveQcValue();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 保存质控数据
    /// </summary>
    /// <returns></returns>
    public string saveQcValue()
    {
        string strResultId = Request["resultid"].ToString();
        //空白数据
        string chkQcEmpty = Request["chkQcEmpty"] == null ? "" : Request["chkQcEmpty"].ToString();
        //标准样数据
        string chkQcSt = Request["chkQcSt"] == null ? "" : Request["chkQcSt"].ToString();
        //实验室加标数据
        string chkQcAdd = Request["chkQcAdd"] == null ? "" : Request["chkQcAdd"].ToString();
        //实验室明码平行
        string chkQcTwin = Request["chkQcTwin"] == null ? "" : Request["chkQcTwin"].ToString();

        bool isSuccess = new TMisMonitorResultLogic().saveQcValue(strResultId, chkQcEmpty, "", "", chkQcSt,
                                             "", "", chkQcAdd, "", "", "",
                                             chkQcTwin, "", "", "", "");
        return isSuccess == true ? "1" : "0";
    }
}