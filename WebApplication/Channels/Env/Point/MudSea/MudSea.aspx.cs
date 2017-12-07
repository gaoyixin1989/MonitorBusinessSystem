using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.MudSea;
using i3.BusinessLogic.Channels.Env.Point.MudSea;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 沉积物（海水）监测点管理 Create By 魏林 2013-06-15
/// </summary>
public partial class Channels_Env_Point_MudSea_MudSea : PageBase
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

        TEnvPMudSeaVo TEnvPMudSea = new TEnvPMudSeaVo();
        TEnvPMudSea.IS_DEL = "0";

        TEnvPMudSea.SORT_FIELD = strSortname;
        TEnvPMudSea.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPMudSea.YEAR = srhYear;
            TEnvPMudSea.MONTH = srhMonth;
            TEnvPMudSea.SECTION_NAME = srhName;
            TEnvPMudSea.AREA_ID = srhArea;
            dt = new TEnvPMudSeaLogic().SelectByTable(TEnvPMudSea, intPageIndex, intPageSize);
            intTotalCount = new TEnvPMudSeaLogic().GetSelectResultCount(TEnvPMudSea);
        }
        else
        {
            dt = new TEnvPMudSeaLogic().SelectByTable(TEnvPMudSea, intPageIndex, intPageSize);
            intTotalCount = new TEnvPMudSeaLogic().GetSelectResultCount(TEnvPMudSea);
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
        TEnvPMudSeaVo TEnvPMudSea = new TEnvPMudSeaVo();
        TEnvPMudSea.ID = strValue;
        TEnvPMudSea.IS_DEL = "1";
        bool isSuccess = new TEnvPMudSeaLogic().Edit(TEnvPMudSea);
        if (isSuccess)
            new PageBase().WriteLog("删除沉积物（海水）断面监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除沉积物（海水）断面监测点" + TEnvPMudSea.ID);
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

        TEnvPMudSeaVVo TEnvPMudSeaV = new TEnvPMudSeaVVo();
        TEnvPMudSeaV.SECTION_ID = oneGridId;
        TEnvPMudSeaV.SORT_FIELD = strSortname;
        TEnvPMudSeaV.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPMudSeaVLogic().SelectByTable(TEnvPMudSeaV, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPMudSeaVLogic().GetSelectResultCount(TEnvPMudSeaV);
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
        bool isSuccess = new TEnvPMudSeaVLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除沉积物（海水）垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除沉积物（海水）垂线信息" + strValue);
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

        TEnvPMudSeaVItemVo TEnvPMudSeaVItem = new TEnvPMudSeaVItemVo();
        TEnvPMudSeaVItem.POINT_ID = twoGridId;
        TEnvPMudSeaVItem.SORT_FIELD = strSortname;
        TEnvPMudSeaVItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPMudSeaVItemLogic().SelectByTable(TEnvPMudSeaVItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPMudSeaVItemLogic().GetSelectResultCount(TEnvPMudSeaVItem);
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
        bool isSuccess = com.SaveItemInfo(TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_MUD_SEA_V_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存沉积物（海水）监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存沉积物（海水）垂线：" + strVerticalCode + "监测项目信息");
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