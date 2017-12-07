using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.General;
using System.Data;
using i3.BusinessLogic.Sys.General;
using System.Web.UI.HtmlControls;

public partial class Masters_TopBottom : System.Web.UI.MasterPage
{
    protected PageBase _PageBase;
    protected void Page_Load(object sender, EventArgs e)
    {
        _PageBase = new PageBase();
        HtmlGenericControl jsJquery = new HtmlGenericControl("script");
        jsJquery.Attributes["type"] = "text/javascript";
        jsJquery.Attributes["id"] = "jsJquery";
        jsJquery.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-1.5.2.min.js");
        Page.Header.Controls.AddAt(0, jsJquery);

        HtmlGenericControl jsLayout = new HtmlGenericControl("script");
        jsLayout.Attributes["type"] = "text/javascript";
        jsLayout.Attributes["src"] = ResolveClientUrl("~/ Scripts/jquery.layout/jquery.layout-latest.js");
        Page.Header.Controls.AddAt(1, jsLayout);

        HtmlGenericControl jsCookie = new HtmlGenericControl("script");
        jsCookie.Attributes["type"] = "text/javascript";
        jsCookie.Attributes["src"] = ResolveClientUrl("~/Scripts/jquery-cookie/jquery.cookie.js");
        Page.Header.Controls.AddAt(2, jsCookie);
    }
  
}
