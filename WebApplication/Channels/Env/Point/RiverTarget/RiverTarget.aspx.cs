using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverTarget;
using i3.BusinessLogic.Channels.Env.Point.RiverTarget;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：责任目标断面信息管理
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverTarget_RiverTarget : PageBase
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

        TEnvPRiverTargetVo TEnvPRiverTarget = new TEnvPRiverTargetVo();
        TEnvPRiverTarget.IS_DEL = "0";

        TEnvPRiverTarget.SORT_FIELD = strSortname;
        TEnvPRiverTarget.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPRiverTarget.YEAR = srhYear;
            TEnvPRiverTarget.MONTH = srhMonth;
            TEnvPRiverTarget.SECTION_NAME = srhName;
            TEnvPRiverTarget.AREA_ID = srhArea;
            dt = new TEnvPRiverTargetLogic().SelectByTable(TEnvPRiverTarget, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiverTargetLogic().GetSelectResultCount(TEnvPRiverTarget);
        }
        else
        {
            dt = new TEnvPRiverTargetLogic().SelectByTable(TEnvPRiverTarget, intPageIndex, intPageSize);
            intTotalCount = new TEnvPRiverTargetLogic().GetSelectResultCount(TEnvPRiverTarget);
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
        TEnvPRiverTargetVo TEnvPRiverTarget = new TEnvPRiverTargetVo();
        TEnvPRiverTarget.ID = strValue;
        TEnvPRiverTarget.IS_DEL = "1";
        bool isSuccess = new TEnvPRiverTargetLogic().Edit(TEnvPRiverTarget);
        if (isSuccess)
            new PageBase().WriteLog("删除责任目标断面监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除责任目标断面监测点" + TEnvPRiverTarget.ID);
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

        TEnvPRiverTargetVVo TEnvPRiverTargetV = new TEnvPRiverTargetVVo();
        TEnvPRiverTargetV.SECTION_ID = oneGridId;
        TEnvPRiverTargetV.SORT_FIELD = strSortname;
        TEnvPRiverTargetV.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiverTargetVLogic().SelectByTable(TEnvPRiverTargetV, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiverTargetVLogic().GetSelectResultCount(TEnvPRiverTargetV);
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
        bool isSuccess = new TEnvPRiverTargetVLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除责任目标垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除责任目标垂线信息" + strValue);
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

        TEnvPRiverTargetVItemVo TEnvPRiverTargetVItem = new TEnvPRiverTargetVItemVo();
        TEnvPRiverTargetVItem.POINT_ID = twoGridId;
        TEnvPRiverTargetVItem.SORT_FIELD = strSortname;
        TEnvPRiverTargetVItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPRiverTargetVItemLogic().SelectByTable(TEnvPRiverTargetVItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPRiverTargetVItemLogic().GetSelectResultCount(TEnvPRiverTargetVItem);
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
        bool isSuccess = com.SaveItemInfo(TEnvPRiverTargetVItemVo.T_ENV_P_RIVER_TARGET_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_RIVER_TARGET_V_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存责任目标监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存责任目标垂线：" + strVerticalCode + "监测项目信息");
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