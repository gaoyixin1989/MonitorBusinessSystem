using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// 功能描述：一栏查询、一栏OpenFalshChart、一栏数据表格类页面母版页，比如专题分析类报告等
/// 创建日期：2011-4-13 20:27:53
/// 创建人  ：陈国迎
/// </summary>
public partial class Masters_Report : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl jsJquery = new HtmlGenericControl("script");
        jsJquery.Attributes["type"] = "text/javascript";
        jsJquery.Attributes["id"] = "jsJquery";
        jsJquery.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-1.8.2.min.js");
        Page.Header.Controls.AddAt(0, jsJquery);

        HtmlGenericControl jsAjaxError = new HtmlGenericControl("script");
        jsAjaxError.Attributes["type"] = "text/javascript";
        jsAjaxError.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/ajaxError/ajaxError.js");
        Page.Header.Controls.AddAt(3, jsAjaxError);

        HtmlGenericControl jsCookie = new HtmlGenericControl("script");
        jsCookie.Attributes["type"] = "text/javascript";
        jsCookie.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-cookie/jquery.cookie.js");
        Page.Header.Controls.AddAt(4, jsCookie);

        HtmlGenericControl jsDateTime = new HtmlGenericControl("script");
        jsDateTime.Attributes["type"] = "text/javascript";
        jsDateTime.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/dateTimepicker/dateTimePicker.js");
        Page.Header.Controls.AddAt(5, jsDateTime);

        HtmlGenericControl jsSwfObject = new HtmlGenericControl("script");
        jsSwfObject.Attributes["type"] = "text/javascript";
        jsSwfObject.Attributes["src"] = ResolveClientUrl("~/Controls/OpenFlashChart/swfobject.js");
        Page.Header.Controls.AddAt(6, jsSwfObject);

        HtmlGenericControl jsFixWidth = new HtmlGenericControl("script");
        jsFixWidth.Attributes["type"] = "text/javascript";
        jsFixWidth.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/other/fixWidth.js");
        Page.Header.Controls.AddAt(7, jsFixWidth);

    }
}
