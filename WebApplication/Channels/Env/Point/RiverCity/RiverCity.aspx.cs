using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverCity;
using i3.BusinessLogic.Channels.Env.Point.RiverCity;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：城考断面信息管理
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverCity_RiverCity : PageBase
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

        TEnvPRiverCityVo TEnvPRiverCity = new TEnvPRiverCityVo();
        TEnvPRiverCity.IS_DEL = "0";

        TEnvPRiverCity.SORT_FIELD = strSortname;
        TEnvPRiverCity.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPRiverCity.YEAR = srhYear;
            TEnvPRiverCity.MONTH = srhMonth;
            TEnvPRiverCity.SECTION_NAME = srhName;
            TEnvPRiverCity.AREA_ID = srhArea;
            dt = new TEnvPRiverCityLogic().SelectByTable(TEnvPRiverCity, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiverCityLogic().GetSelectResultCount(TEnvPRiverCity);
        }
        else
        {
            dt = new TEnvPRiverCityLogic().SelectByTable(TEnvPRiverCity, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiverCityLogic().GetSelectResultCount(TEnvPRiverCity);
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
        TEnvPRiverCityVo TEnvPRiverCity = new TEnvPRiverCityVo();
        TEnvPRiverCity.ID = strValue;
        TEnvPRiverCity.IS_DEL = "1";
        bool isSuccess = new TEnvPRiverCityLogic().Edit(TEnvPRiverCity);
        if (isSuccess)
            new PageBase().WriteLog("删除城考断面监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除城考断面监测点" + TEnvPRiverCity.ID);
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

        TEnvPRiverCityVVo TEnvPRiverCityV = new TEnvPRiverCityVVo();
        TEnvPRiverCityV.SECTION_ID = oneGridId;
        TEnvPRiverCityV.SORT_FIELD = strSortname;
        TEnvPRiverCityV.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiverCityVLogic().SelectByTable(TEnvPRiverCityV, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiverCityVLogic().GetSelectResultCount(TEnvPRiverCityV);
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
        bool isSuccess = new TEnvPRiverCityVLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除城考垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除城考垂线信息" + strValue);
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

        TEnvPRiverCityVItemVo TEnvPRiverCityVItem = new TEnvPRiverCityVItemVo();
        TEnvPRiverCityVItem.POINT_ID = twoGridId;
        TEnvPRiverCityVItem.SORT_FIELD = strSortname;
        TEnvPRiverCityVItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiverCityVItemLogic().SelectByTable(TEnvPRiverCityVItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiverCityVItemLogic().GetSelectResultCount(TEnvPRiverCityVItem);
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
        bool isSuccess = com.SaveItemInfo(TEnvPRiverCityVItemVo.T_ENV_P_RIVER_CITY_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_RIVER_CITY_V_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存城考监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存城考垂线：" + strVerticalCode + "监测项目信息");
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