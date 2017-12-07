using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：饮用水源地断面管理
/// 创建日期：2013-06-07
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_DrinkSource_DrinkingSource : PageBase
{
    //定义结果
    string strResult = "";
    public string srhYear = "", srhMonth="", srhName = "", srhArea = "",count="";

    protected void Page_Load(object sender, EventArgs e)
    {
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
            if (!string.IsNullOrEmpty(Request.Params["flag"]))
            {
                count = Request.Params["flag"].Trim();
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

        TEnvPDrinkSrcVo TEnvPDrinkSrc = new TEnvPDrinkSrcVo();
        TEnvPDrinkSrc.IS_DEL = "0";
        TEnvPDrinkSrc.SORT_FIELD = strSortname;
        TEnvPDrinkSrc.SORT_TYPE = strSortorder;
        TEnvPDrinkSrc.NUM = count;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPDrinkSrc.YEAR = srhYear;
            TEnvPDrinkSrc.MONTH = srhMonth;
            TEnvPDrinkSrc.SECTION_NAME = srhName;
            TEnvPDrinkSrc.AREA_ID = srhArea;
            dt = new TEnvPDrinkSrcLogic().SelectByTable(TEnvPDrinkSrc, intPageIndex, intPageSize);
            intTotalCount = new TEnvPDrinkSrcLogic().GetSelectResultCount(TEnvPDrinkSrc);
        }
        else
        {
            dt = new TEnvPDrinkSrcLogic().SelectByTable(TEnvPDrinkSrc, intPageIndex, intPageSize);
            intTotalCount = new TEnvPDrinkSrcLogic().GetSelectResultCount(TEnvPDrinkSrc);
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
        TEnvPDrinkSrcVo TEnvPDrinkSrc = new TEnvPDrinkSrcVo();
        TEnvPDrinkSrc.ID = strValue;
        TEnvPDrinkSrc.IS_DEL = "1";
        bool isSuccess = new TEnvPDrinkSrcLogic().Edit(TEnvPDrinkSrc);
        if (isSuccess)
            new PageBase().WriteLog("删除饮用水源地断面信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除饮用水源地断面信息" + TEnvPDrinkSrc.ID);
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

        TEnvPDrinkSrcVVo TEnvPDrinkSrcV = new TEnvPDrinkSrcVVo();
        TEnvPDrinkSrcV.SECTION_ID = oneGridId;
        TEnvPDrinkSrcV.SORT_FIELD = strSortname;
        TEnvPDrinkSrcV.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPDrinkSrcVLogic().SelectByTable(TEnvPDrinkSrcV, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPDrinkSrcVLogic().GetSelectResultCount(TEnvPDrinkSrcV);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除垂线信息
    /// </summary>
    /// <param name="strValue">垂线ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteTwoGridInfo(string strValue)
    {
        bool isSuccess = new TEnvPDrinkSrcVLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除饮用水源地垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除饮用水源地垂线信息" + strValue);
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

        TEnvPDrinkSrcVItemVo TEnvPDrinkSrcVItem = new TEnvPDrinkSrcVItemVo();
        TEnvPDrinkSrcVItem.POINT_ID = twoGridId;
        TEnvPDrinkSrcVItem.SORT_FIELD = strSortname;
        TEnvPDrinkSrcVItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPDrinkSrcVItemLogic().SelectByTable(TEnvPDrinkSrcVItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPDrinkSrcVItemLogic().GetSelectResultCount(TEnvPDrinkSrcVItem);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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
        bool isSuccess = com.SaveItemInfo(TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_DRINK_SRC_V_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存饮用水源地监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存饮用水源地垂线：" + strVerticalCode + "监测项目信息");
        return isSuccess == true ? "1" : "0";
    }
}