using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：报告模板
/// 创建时间：2012-12-3
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_Template_TemplateList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "getTemplateInfo")
            {
                getTemplateInfo();
            }
        }
    }

    /// <summary>
    /// 获取模板信息
    /// </summary>
    protected void getTemplateInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TRptTemplateVo objTemplate = new TRptTemplateVo();
        objTemplate.IS_DEL = "0";
        objTemplate.SORT_FIELD = !string.IsNullOrEmpty(strSortname) ? strSortname : TRptTemplateVo.ID_FIELD;
        objTemplate.SORT_TYPE = SortDirection.Descending.ToString();

        int intCount = new TRptTemplateLogic().GetSelectResultCount(objTemplate);
        DataTable dt = new TRptTemplateLogic().SelectByTable(objTemplate, intPageIndex, intPageSize);
        Response.Write(CreateToJson(dt, intCount));
        Response.End();
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="strValue">标签ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteTemplate(string strValue)
    {
        if (!string.IsNullOrEmpty(strValue))
        {
            if (new TRptTemplateLogic().Edit(new TRptTemplateVo() { ID = strValue, IS_DEL = "1" }))
            {
                new PageBase().WriteLog("删除印章", "", new UserLogInfo().UserInfo.USER_NAME + "删除印章" + strValue);
                return "1";
            }
        }
        return "0";
    }

    /// <summary>
    /// 获得监测类别
    /// </summary>
    /// <param name="strValue">类别ID</param>
    /// <returns>类别名称</returns>
    [WebMethod]
    public static string getItemType(string strValue)
    {
        if (!string.IsNullOrEmpty(strValue))
            return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
        return "";
    }
}