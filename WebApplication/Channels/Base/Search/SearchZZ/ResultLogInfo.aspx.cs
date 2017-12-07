using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能： "结果数据可追溯性--历史数据"功能
/// 创建人：潘德军
/// 创建时间： 2013.7.6
/// </summary>
public partial class Channels_Base_Search_SearchZZ_ResultLogInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "GetLogInfo")
        {
            GetLogInfo();
        }
    }

    /// <summary>
    /// 历史数据
    /// </summary>
    protected void GetLogInfo()
    {
        string strResultID = !string.IsNullOrEmpty(Request.QueryString["resultid"]) ? Request.QueryString["resultid"].ToString() : "";

        TMisMonitorResultLogVo objVo = new TMisMonitorResultLogVo();
        objVo.RESULT_ID = strResultID;
        objVo.SORT_FIELD = TMisMonitorResultLogVo.ID_FIELD;
        DataTable dt = new TMisMonitorResultLogLogic().SelectByTable(objVo);

        string strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取用户姓名
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }
}