using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using System.Web.UI.HtmlControls;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using System.Configuration;

public partial class ccflow_TaskHandler : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request["type"];
        UserLogInfo userLog = (UserLogInfo)Session[KEY_CACHEOPERATOR];
        TSysUserVo userInfoLogin = userLog.UserInfo;
        Response.Redirect(ConfigurationManager.AppSettings["ccflow"]
            + "/AppServices/DoType.aspx?userNo=" + userInfoLogin.USER_NAME+"&type="+type);
    }
}