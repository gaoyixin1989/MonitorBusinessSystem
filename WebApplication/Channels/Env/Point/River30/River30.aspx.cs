using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Point.River30;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;
/// <summary>
/// 双三十废水监测点管理
/// 创建人：魏林 2013-06-17
/// </summary>
public partial class Channels_Env_Point_River30_River30 : PageBase
{
    public string srhYear = "", srhMonth = "", srhName = "", srhArea = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        //获取断面信息
        if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srhYear"]))
            {
                srhYear = Request.Params["srhYear"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhMonth"]))
            {
                srhMonth = Request.Params["srhMonth"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhName"]))
            {
                srhName = Request.Params["srhName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhArea"]))
            {
                srhArea = Request.Params["srhArea"].Trim();
            }
            strResult = getOneGridInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取垂线信息
        if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
        {
            strResult = getTwoGridInfo(Request["oneGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取垂线监测项目信息
        if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
        {
            strResult = getThreeGridInfo(Request["twoGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取断面信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TEnvPRiver30Vo TEnvPRiver30 = new TEnvPRiver30Vo();
        TEnvPRiver30.IS_DEL = "0";

        TEnvPRiver30.SORT_FIELD = "YEAR DESC,MONTH";
        TEnvPRiver30.SORT_TYPE = "DESC";
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPRiver30.YEAR = srhYear;
            TEnvPRiver30.MONTH = srhMonth;
            TEnvPRiver30.SECTION_NAME = srhName;
            TEnvPRiver30.AREA_ID = srhArea;
            dt = new TEnvPRiver30Logic().SelectByTable(TEnvPRiver30, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiver30Logic().GetSelectResultCount(TEnvPRiver30);
        }
        else
        {
            dt = new TEnvPRiver30Logic().SelectByTable(TEnvPRiver30, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiver30Logic().GetSelectResultCount(TEnvPRiver30);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除断面信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteOneGridInfo(string strValue)
    {
        TEnvPRiver30Vo TEnvPRiver30 = new TEnvPRiver30Vo();
        TEnvPRiver30.ID = strValue;
        TEnvPRiver30.IS_DEL = "1";
        bool isSuccess = new TEnvPRiver30Logic().Edit(TEnvPRiver30);
        if (isSuccess)
            new PageBase().WriteLog("删除双三十废水断面监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除双三十废水断面监测点" + TEnvPRiver30.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取垂线信息
    /// </summary>
    /// <param name="oneGridId">断面ID</param>
    /// <returns></returns>
    public string getTwoGridInfo(string oneGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TEnvPRiver30VVo TEnvPRiver30V = new TEnvPRiver30VVo();
        TEnvPRiver30V.SECTION_ID = oneGridId;
        TEnvPRiver30V.SORT_FIELD = strSortname;
        TEnvPRiver30V.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiver30VLogic().SelectByTable(TEnvPRiver30V, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiver30VLogic().GetSelectResultCount(TEnvPRiver30V);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除垂线信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteTwoGridInfo(string strValue)
    {
        bool isSuccess = new TEnvPRiver30VLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除双三十废水垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除双三十废水垂线信息" + strValue);
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取垂线监测项目信息
    /// </summary>
    /// <param name="twoGridId">垂线ID</param>
    /// <returns></returns>
    public string getThreeGridInfo(string twoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TEnvPRiver30VItemVo TEnvPRiver30VItem = new TEnvPRiver30VItemVo();
        TEnvPRiver30VItem.POINT_ID = twoGridId;
        TEnvPRiver30VItem.SORT_FIELD = strSortname;
        TEnvPRiver30VItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiver30VItemLogic().SelectByTable(TEnvPRiver30VItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiver30VItemLogic().GetSelectResultCount(TEnvPRiver30VItem);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 保存监测项目信息
    /// </summary>
    /// <param name="strVerticalCode">垂线ID</param>
    /// <param name="strValue">监测项目值</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemData(string strVerticalCode, string strValue)
    {
        CommonLogic com = new CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPRiver30VItemVo.T_ENV_P_RIVER30_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_RIVER30_V_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存双三十废水监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存双三十废水垂线：" + strVerticalCode + "监测项目信息");
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        DataTable dt = getItemInfo(strItemCode);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
}