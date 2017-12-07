using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public partial class Sys_General_UserProxySet : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hidUserID1.Value = LogInfo.UserInfo.ID;
            hidUserName.Value = LogInfo.UserInfo.REAL_NAME;
        }
    }

    /// <summary>
    /// 代理设置
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SaveData(string strUserID, string strProxyID)
    {
        bool isSuccess = true;

        TSysUserProxyVo objProxy = new TSysUserProxyVo();

        TSysUserProxyVo objProxyWhere = new TSysUserProxyVo();
        objProxyWhere.USER_ID = strUserID;
        List<TSysUserProxyVo> lst = new TSysUserProxyLogic().SelectByObject(objProxyWhere, 0, 0);
        if (lst.Count > 0)
        {
            objProxy.ID = lst[0].ID;
            objProxy.USER_ID = strUserID;
            objProxy.PROXY_USER_ID = strProxyID;

            isSuccess = new TSysUserProxyLogic().Edit(objProxy);
            if (isSuccess)
                new PageBase().WriteLog("编辑代理设置", "", new UserLogInfo().UserInfo.USER_NAME + "编辑代理设置" + objProxy.ID);
        }
        else
        {
            objProxy.ID = GetSerialNumber("t_sys_user_proxy_id");
            objProxy.USER_ID = strUserID;
            objProxy.PROXY_USER_ID = strProxyID;

            isSuccess = new TSysUserProxyLogic().Create(objProxy);
            if (isSuccess)
                new PageBase().WriteLog("添加代理设置", "", new UserLogInfo().UserInfo.USER_NAME + "添加代理设置" + objProxy.ID);
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 获取指定用户的代理人
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getProxyOfUser(string strUserID)
    {
        TSysUserProxyVo objProxyWhere = new TSysUserProxyVo();
        objProxyWhere.USER_ID = strUserID;
        TSysUserProxyVo objProxy = new TSysUserProxyLogic().Details(objProxyWhere);
        TSysUserVo objUser = new TSysUserLogic().Details(objProxy.PROXY_USER_ID);
        return objProxy.PROXY_USER_ID + "|" + objUser.REAL_NAME;
    }
}