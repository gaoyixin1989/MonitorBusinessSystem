﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.NoiseArea;
using i3.BusinessLogic.Channels.Env.Point.NoiseArea;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：区域环境噪声监测点管理
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_NoiseArea_AreaNoise : PageBase
{
    public string srhYear = "", srhMonth = "", srhName = "", srhArea = "", srhFun = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取监测点信息
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
            if (!String.IsNullOrEmpty(Request.Params["srhFun"]))
            {
                srhFun = Request.Params["srhFun"].Trim();
            }
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

        TEnvPNoiseAreaVo TEnvPNoiseArea = new TEnvPNoiseAreaVo();
        TEnvPNoiseArea.IS_DEL = "0";
        TEnvPNoiseArea.SORT_FIELD = strSortname;
        TEnvPNoiseArea.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea) || !String.IsNullOrEmpty(srhFun))
        {
            TEnvPNoiseArea.YEAR = srhYear;
            TEnvPNoiseArea.MONTH = srhMonth;
            TEnvPNoiseArea.POINT_NAME = srhName;
            TEnvPNoiseArea.AREA_ID = srhArea;
            TEnvPNoiseArea.FUNCTION_AREA_ID = srhFun;
            dt = new TEnvPNoiseAreaLogic().SelectByTable(TEnvPNoiseArea, intPageIndex, intPageSize);
            intTotalCount = new TEnvPNoiseAreaLogic().GetSelectResultCount(TEnvPNoiseArea);
        }
        else
        {
            dt = new TEnvPNoiseAreaLogic().SelectByTable(TEnvPNoiseArea, intPageIndex, intPageSize);
            intTotalCount = new TEnvPNoiseAreaLogic().GetSelectResultCount(TEnvPNoiseArea);
        }
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
        TEnvPNoiseAreaVo TEnvPNoiseArea = new TEnvPNoiseAreaVo();
        TEnvPNoiseArea.ID = strValue;
        TEnvPNoiseArea.IS_DEL = "1";
        bool isSuccess = new TEnvPNoiseAreaLogic().Edit(TEnvPNoiseArea);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除区域环境噪声监测点", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除区域环境噪声监测点" + TEnvPNoiseArea.ID + "成功");
        }
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

        TEnvPNoiseAreaItemVo TEnvPNoiseAreaItem = new TEnvPNoiseAreaItemVo();
        TEnvPNoiseAreaItem.POINT_ID = oneGridId;
        TEnvPNoiseAreaItem.SORT_FIELD = strSortname;
        TEnvPNoiseAreaItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPNoiseAreaItemLogic().SelectByTable(TEnvPNoiseAreaItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPNoiseAreaItemLogic().GetSelectResultCount(TEnvPNoiseAreaItem);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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
        CommonLogic com = new CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_NOISE_AREA_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存区域环境噪声监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存区域环境噪声：" + strPointId + "监测项目信息");
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