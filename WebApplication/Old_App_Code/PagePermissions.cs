using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
/// <summary>
///PagePermissions 的摘要说明
/// </summary>
public class PagePermissions
{
    public PagePermissions()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 允许所有用户访问的页面
    /// </summary>
    public static string[] _listAllowPagePath = new string[] { 
                                            "Portal/Login.aspx".ToLower(),
                                            "Portal/Error.aspx".ToLower(),
                                            "Portal/Index.aspx".ToLower(),
                                            "Portal/Left_1.aspx".ToLower(),
                                            "Portal/Left.aspx".ToLower()
                                            };

    /// <summary>
    /// 是否开始页面权限(系统变量中的系统开关量的"页面权限开关")
    /// </summary>
    /// <returns></returns>
    protected static bool IsOpenPagePermissions()
    {
        TSysConfigVo objSysConfigVo = new TSysConfigVo();
        objSysConfigVo.CONFIG_CODE = "PagePermissions";
        objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
        if (objSysConfigVo.CONFIG_VALUE != "1")
            return false;
        return true;
    }
    /// <summary>
    /// 判断用户对页面是否有权限
    /// </summary>
    /// <param name="pathfile">文件路径</param>
    /// <param name="userName">用户名</param>
    /// <returns></returns>
    public static bool IsAllowVisitPage(string pathfile, string userName)
    {
        if (!IsOpenPagePermissions())//如果页面权限开关为关闭(0),直接返回有权限
            return true;
        pathfile = pathfile.ToLower();
        if (pathfile == "")
            return true;
        if (_listAllowPagePath.Contains(pathfile))
            return true;
        DataTable dtUserMenu = null;
        TSysMenuVo objSysMenuVo = new TSysMenuVo();
        objSysMenuVo.IS_DEL = "0";
        objSysMenuVo.IS_USE = "1";
        objSysMenuVo.MENU_TYPE = "Menu";
        dtUserMenu = new TSysMenuLogic().GetMenuByUserName(objSysMenuVo, userName);
        for (int i = 0; i < dtUserMenu.Rows.Count; i++)
        {
            if (pathfile == (dtUserMenu.Rows[i][TSysMenuVo.MENU_URL_FIELD].ToString()).ToLower())
            {
                return true;
            }
        }
        return false;
    }
}