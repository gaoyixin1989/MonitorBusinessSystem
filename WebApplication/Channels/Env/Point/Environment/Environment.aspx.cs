using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using i3.View;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.Environment;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_Environment_Environment : PageBase
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

        TEnvPAirVo TEnvPointRainVo = new TEnvPAirVo();
        TEnvPointRainVo.IS_DEL = "0";
        TEnvPointRainVo.SORT_FIELD = strSortname;
        TEnvPointRainVo.SORT_TYPE = strSortorder;
        DataTable dt = new T_ENV_P_AIR().SelectByTable(TEnvPointRainVo, intPageIndex, intPageSize);
        int intTotalCount = new T_ENV_P_AIR().GetSelectResultCount(TEnvPointRainVo);
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
        TEnvPAirVo TEnvPointRainVo = new TEnvPAirVo();
        TEnvPointRainVo.ID = strValue;
        TEnvPointRainVo.IS_DEL = "1";
        bool isSuccess = new T_ENV_P_AIR().Edit(TEnvPointRainVo);
        if (isSuccess)
            new PageBase().WriteLog("删除环境空气监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除环境空气监测点" + TEnvPointRainVo.ID);
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

        TEnvPNoiseFunctionItemVo TEnvPointRainItemVo = new TEnvPNoiseFunctionItemVo();
        //  TEnvPointRainItemVo.ID = oneGridId;
        TEnvPointRainItemVo.POINT_ID = oneGridId;
        //TEnvPointRainItemVo.ITEM_ID = strSortorder;
        DataTable dt = new TEnvPNoiseFunctionItemLogic().SelectByTable(TEnvPointRainItemVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPNoiseFunctionItemLogic().GetSelectResultCount(TEnvPointRainItemVo);
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
        bool isSuccess = new TEnvPNoiseFunctionItemLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除环境空气监测点项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除环境空气监测点" + strValue + "项目信息");
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
        CommonLogic com = new CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_AIR_ITEM);
        //bool isSuccess = new i3.BusinessLogic.Channels.Env.Point.Environment.TEnvPNoiseFunctionItemLogic().SaveItemByTransaction("T_ENV_P_AIR_ITEM", "POINT_ID", "rainitem_id", strPointId, strValue);
        if (isSuccess)
            new PageBase().WriteLog("批量保存环境空气监测点项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存环境空气监测点：" + strPointId + "的监测项目信息");
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
        // DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemAnalysisLogic().SelectByTable_ByJoin(TBaseItemAnalysisVo);
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemAnalysisLogic().SelectByTable_ByJoin1(TBaseItemAnalysisVo.ITEM_ID);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
}