using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：代理信息管理
/// 创建日期：2012-11-15
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_General_UserProxyList : PageBase
{
    string strIsAdmin = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["IsAdmin"] != null)
        {
            strIsAdmin = Request["IsAdmin"].ToString();
        }
        else
        {
            hidUserID.Value = LogInfo.UserInfo.ID;
        }

        //定义结果
        string strResult = "";
        //获取代理信息
        if (Request["type"] != null && Request["type"].ToString() == "getProxy")
        {
            strResult = getProxyList();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取代理信息
    private string getProxyList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TSysUserProxyVo.ID_FIELD;

        TSysUserProxyVo objProxy = new TSysUserProxyVo();
        if (strIsAdmin.Length == 0)
        {
            objProxy.USER_ID = LogInfo.UserInfo.ID;
        }
        objProxy.SORT_FIELD = strSortname;
        objProxy.SORT_TYPE = strSortorder;

        int intTotalCount = new TSysUserProxyLogic().GetSelectResultCount(objProxy);
        DataTable dt = new TSysUserProxyLogic().SelectByTable(objProxy, intPageIndex, intPageSize);

        string strJson = CreateToJson(dt, intTotalCount);

        return strJson;
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteData(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            isSuccess = new TSysUserProxyLogic().Delete(arrDelIDs[i]);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除代理", "", new UserLogInfo().UserInfo.USER_NAME + "删除代理" + strDelIDs);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddData(string strUSER_ID, string strProxyID)
    {
        bool isSuccess = true;

        TSysUserProxyVo objProxyDel = new TSysUserProxyVo();
        objProxyDel.USER_ID = strUSER_ID;
        new TSysUserProxyLogic().Delete(objProxyDel);

        TSysUserProxyVo objProxy = new TSysUserProxyVo();
        objProxy.ID = GetSerialNumber("t_sys_user_proxy_id");
        objProxy.USER_ID = strUSER_ID;
        objProxy.PROXY_USER_ID = strProxyID;

        isSuccess = new TSysUserProxyLogic().Create(objProxy);

        if (isSuccess)
        {
            new PageBase().WriteLog("添加代理", "", new UserLogInfo().UserInfo.USER_NAME + "添加代理" + objProxy.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑数据
    /// </summary>
    /// <param name="strID">id</param>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditData(string strID, string strUSER_ID, string strProxyID)
    {
        bool isSuccess = true;

        new TSysUserProxyLogic().Delete(strID);

        TSysUserProxyVo objProxyDel = new TSysUserProxyVo();
        objProxyDel.USER_ID = strUSER_ID;
        new TSysUserProxyLogic().Delete(objProxyDel);

        TSysUserProxyVo objProxy = new TSysUserProxyVo();
        objProxy.ID = GetSerialNumber("t_sys_user_proxy_id");
        objProxy.USER_ID = strUSER_ID;
        objProxy.PROXY_USER_ID = strProxyID;

        isSuccess = new TSysUserProxyLogic().Create(objProxy);

        if (isSuccess)
        {
            new PageBase().WriteLog("编辑代理", "", new UserLogInfo().UserInfo.USER_NAME + "编辑代理" + objProxy.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }
}