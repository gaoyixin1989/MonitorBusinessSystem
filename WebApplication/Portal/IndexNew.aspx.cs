using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.SessionState;
using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：系统主页面
/// 创建日期：2011-04-12 15:30
/// 创建人  ：欧耀翔
/// 描述：将直接打开页面改为选项卡打开
/// 修改日期：2012-11-14
/// 修改人：潘德军
/// 描述：屏幕自适应修改，页面重构
/// 修改日期：2012-12-29
/// 修改人：潘德军
/// </summary>
public partial class Portal_IndexNew : PageBase
{
    protected string copyright = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        localUserID.Value = base.LogInfo.UserInfo.ID;
        lbWecomeUserName.Text = base.LogInfo.UserInfo.REAL_NAME;

        TSysConfigVo objSysConfigVo = new TSysConfigVo();
        objSysConfigVo.CONFIG_CODE = "CopyRight";
        objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
        copyright = objSysConfigVo.REMARK;

        //定义结果
        string strResult = "";

        //获取属性类别
        if (Request["type"] != null && Request["type"].ToString() == "getMenu")
        {
            if (Request["localUserID"] == null)
                return;

            strResult = getMenu(Request["localUserID"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取属性类别信息
    /// </summary>
    /// <returns></returns>
    private string getMenu(string strlocalUserID)
    {
        string json = "[]";

        string strDataJson = new TSysMenuLogic().GetMenuByUserIDForLogin(strlocalUserID);
        if (strDataJson.Length > 0)
            json = "[" + strDataJson + "]";

        return json;
    }
}