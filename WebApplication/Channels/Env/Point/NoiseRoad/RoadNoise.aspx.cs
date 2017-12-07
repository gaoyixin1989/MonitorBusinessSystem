using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.NoiseRoad;
using i3.BusinessLogic.Channels.Env.Point.NoiseRoad;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：道路交通噪声监测点管理
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_NoiseRoad_RoadNoise : PageBase
{
    public string srhYear = "", srhMonth = "", srhName = "", srhRoadName = "";
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
            if (!String.IsNullOrEmpty(Request.Params["srhRoadName"]))
            {
                srhRoadName = Request.Params["srhArea"].Trim();
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

        TEnvPNoiseRoadVo TEnvPNoiseRoad = new TEnvPNoiseRoadVo();
        TEnvPNoiseRoad.IS_DEL = "0";
        TEnvPNoiseRoad.SORT_FIELD = strSortname;
        TEnvPNoiseRoad.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhRoadName))
        {
            TEnvPNoiseRoad.YEAR = srhYear;
            TEnvPNoiseRoad.MONTH = srhMonth;
            TEnvPNoiseRoad.POINT_NAME = srhName;
            TEnvPNoiseRoad.ROAD_NAME = srhRoadName;
            dt = new TEnvPNoiseRoadLogic().SelectByTable(TEnvPNoiseRoad, intPageIndex, intPageSize);
            intTotalCount = new TEnvPNoiseRoadLogic().GetSelectResultCount(TEnvPNoiseRoad);
        }
        else
        {
            dt = new TEnvPNoiseRoadLogic().SelectByTable(TEnvPNoiseRoad, intPageIndex, intPageSize);
            intTotalCount = new TEnvPNoiseRoadLogic().GetSelectResultCount(TEnvPNoiseRoad);
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
        TEnvPNoiseRoadVo TEnvPNoiseRoad = new TEnvPNoiseRoadVo();
        TEnvPNoiseRoad.ID = strValue;
        TEnvPNoiseRoad.IS_DEL = "1";
        bool isSuccess = new TEnvPNoiseRoadLogic().Edit(TEnvPNoiseRoad);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除道路交通噪声监测点", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除道路交通噪声监测点" + TEnvPNoiseRoad.ID + "成功");
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

        TEnvPNoiseRoadItemVo TEnvPNoiseRoadItem = new TEnvPNoiseRoadItemVo();
        TEnvPNoiseRoadItem.POINT_ID = oneGridId;
        TEnvPNoiseRoadItem.SORT_FIELD = strSortname;
        TEnvPNoiseRoadItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPNoiseRoadItemLogic().SelectByTable(TEnvPNoiseRoadItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPNoiseRoadItemLogic().GetSelectResultCount(TEnvPNoiseRoadItem);
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
        bool isSuccess = com.SaveItemInfo(TEnvPNoiseRoadItemVo.T_ENV_P_NOISE_ROAD_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_NOISE_ROAD_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存道路交通噪声监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存道路交通噪声：" + strPointId + "监测项目信息");
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