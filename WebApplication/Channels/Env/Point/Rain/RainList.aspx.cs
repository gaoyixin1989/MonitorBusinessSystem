﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Rain;
using i3.BusinessLogic.Channels.Env.Point.Rain;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：降水监测点管理
/// 创建日期：2012-11-19
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Rain_RainList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取监测点信息
        if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
        {
            strResult = getOneGridInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取监测项目信息
        if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
        {
            strResult = getTwoGridInfo(Request["oneGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取监测点信息
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

        TEnvPointRainVo TEnvPointRainVo = new TEnvPointRainVo();
        TEnvPointRainVo.IS_DEL = "0";
        TEnvPointRainVo.SORT_FIELD = strSortname;
        TEnvPointRainVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointRainLogic().SelectByTable(TEnvPointRainVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointRainLogic().GetSelectResultCount(TEnvPointRainVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除监测点信息
    /// </summary>
    /// <param name="strValue">监测点ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteOneGridInfo(string strValue)
    {
        TEnvPointRainVo TEnvPointRainVo = new TEnvPointRainVo();
        TEnvPointRainVo.ID = strValue;
        TEnvPointRainVo.IS_DEL = "1";
        bool isSuccess = new TEnvPointRainLogic().Edit(TEnvPointRainVo);
        if (isSuccess)
            new PageBase().WriteLog("删除降水监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除降水监测点" + TEnvPointRainVo.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="oneGridId">监测点ID</param>
    /// <returns></returns>
    public string getTwoGridInfo(string oneGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TEnvPointRainItemVo TEnvPointRainItemVo = new TEnvPointRainItemVo();
        TEnvPointRainItemVo.POINT_ID = oneGridId; 
        TEnvPointRainItemVo.SORT_FIELD = strSortname;
        TEnvPointRainItemVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointRainItemLogic().SelectByTable(TEnvPointRainItemVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointRainItemLogic().GetSelectResultCount(TEnvPointRainItemVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除监测项目信息
    /// </summary>
    /// <param name="strValue">监测点ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteTwoGridInfo(string strValue)
    {
        bool isSuccess = new TEnvPointRainItemLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除降水监测点项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除降水监测点" + strValue + "项目信息");
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 保存监测项目信息
    /// </summary>
    /// <param name="strPointId">监测点ID</param>
    /// <param name="strValue">监测项目数据</param>
    /// <returns></returns> 
    [WebMethod]
    public static string saveItemData(string strPointId, string strValue)
    {
        i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic com = new i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_RAIN_ITEM);
       // bool isSuccess = new i3.BusinessLogic.Channels.Env.Point.River.TEnvPointRiverLogic().SaveItemByTransaction("T_ENV_P_RAIN_ITEM", "RAIN_POINT_ID", "rainitem_id", strPointId, strValue);
        if (isSuccess)
            new PageBase().WriteLog("批量保存降水监测点项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存降水监测点：" + strPointId + "的监测项目信息");
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
        i3.ValueObject.Channels.Base.Item.TBaseItemAnalysisVo TBaseItemAnalysisVo = new i3.ValueObject.Channels.Base.Item.TBaseItemAnalysisVo();
        TBaseItemAnalysisVo.ITEM_ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemAnalysisLogic().SelectByTable_ByJoin1(TBaseItemAnalysisVo.ITEM_ID);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
}