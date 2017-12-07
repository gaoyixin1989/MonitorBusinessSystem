using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：在线用户管理
/// 创建日期：2011-04-11 18:00
/// 创建人  ：郑义
/// 修改时间：2011-04-14 17:10
/// 修改人  ：郑义
/// 修改内容：更改所有符合开发规范
/// 修改时间：2012-11-19
/// 修改内容：根据新操作模式，重构代码
/// 修改人：潘德军
/// </summary>
public partial class Sys_General_UserOnline : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取点位信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            strResult = getData();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取点位信息
    private string getData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TSysUserVo.ID_FIELD;

        string strUserName = "";
        if (Request["strUserName"] != null)
            strUserName = Request.Params["strUserName"];

        TSysUserStatusVo objUserStatusVo = new TSysUserStatusVo();
        objUserStatusVo.SORT_FIELD = strSortname;
        objUserStatusVo.SORT_TYPE = strSortorder;
        TSysUserStatusLogic objUserStatusLogic = new TSysUserStatusLogic();

        DataTable dtUserTatus = objUserStatusLogic.SelectByTableEx(objUserStatusVo, intPageIndex, intPageSize, DateTime.Now.Subtract(new TimeSpan(0, 10, 0)), strUserName);
        int intTotalCount = objUserStatusLogic.GetSelectResultCountEx(objUserStatusVo, DateTime.Now.Subtract(new TimeSpan(0, 10, 0)), strUserName);

        string strJson = CreateToJson(dtUserTatus, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserRealName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).USER_NAME;
    }
}