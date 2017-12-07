using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// 功能描述："纯粹"信息录入类页面母版页（没有导航栏），用于以模态对话框弹出数据录入页面，比如添加数据字典等页面
/// 创建日期：2011-4-13 20:27:53
/// 创建人  ：陈国迎
/// </summary>
public partial class Masters_InputOnly : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl jsJquery = new HtmlGenericControl("script");
        jsJquery.Attributes["type"] = "text/javascript";
        jsJquery.Attributes["id"] = "jsJquery";
        jsJquery.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-1.8.2.min.js");
        Page.Header.Controls.AddAt(0, jsJquery);

        

        HtmlGenericControl jsCookie = new HtmlGenericControl("script");
        jsCookie.Attributes["type"] = "text/javascript";
        jsCookie.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-cookie/jquery.cookie.js");
        Page.Header.Controls.AddAt(3, jsCookie);


        HtmlGenericControl jsDateTime = new HtmlGenericControl("script");
        jsDateTime.Attributes["type"] = "text/javascript";
        jsDateTime.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/dateTimepicker/dateTimePicker.js");
        Page.Header.Controls.AddAt(4, jsDateTime);

        HtmlGenericControl jsFixWidth = new HtmlGenericControl("script");
        jsFixWidth.Attributes["type"] = "text/javascript";
        jsFixWidth.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/other/fixWidth.js");
        Page.Header.Controls.AddAt(5, jsFixWidth);

        HtmlGenericControl jsValidate = new HtmlGenericControl("script");
        jsValidate.Attributes["type"] = "text/javascript";
        jsValidate.Attributes["src"] = ResolveClientUrl("~/Scripts/Controls/validate/validate.js");
        Page.Header.Controls.AddAt(6, jsValidate);
    }

}
