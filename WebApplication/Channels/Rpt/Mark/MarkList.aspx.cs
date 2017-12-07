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
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：书签管理
/// 创建时间2012-12-3
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_Mark_MarkList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "getMarkInfo")
            {
                getMarkInfo();
            }
        }
    }

    /// <summary>
    /// 获取标签信息
    /// </summary>
    protected void getMarkInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TRptMarkVo objMark = new TRptMarkVo();
        objMark.SORT_FIELD = !string.IsNullOrEmpty(strSortname) ? strSortname : TRptMarkVo.ID_FIELD;
        objMark.SORT_TYPE = SortDirection.Descending.ToString();

        int intCount = new TRptMarkLogic().GetSelectResultCount(objMark);
        DataTable dt = new TRptMarkLogic().SelectByTable(objMark, intPageIndex, intPageSize);
        Response.Write(CreateToJson(dt, intCount));
        Response.End();
    }

    /// <summary>
    /// 删除标签
    /// </summary>
    /// <param name="strValue">标签ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteMark(string strValue)
    {
        if (!string.IsNullOrEmpty(strValue))
        {
            if (new TRptMarkLogic().Delete(strValue))
            {
                new PageBase().WriteLog("删除标签", "", new UserLogInfo().UserInfo.USER_NAME + "删除标签" + strValue);
                return "1";
            }
        }
        return "0";
    }
}