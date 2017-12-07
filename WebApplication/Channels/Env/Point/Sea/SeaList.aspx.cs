using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Sea;
using i3.BusinessLogic.Channels.Env.Point.Sea;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：近岸海域监测点管理
/// 创建日期：2012-11-20
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Sea_SeaList : PageBase
{
    public string srhYear = "", srhName = "", srhFunctionCode = "", srhPointType = "";
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
            if (!String.IsNullOrEmpty(Request.Params["srhName"]))
            {
                srhName = Request.Params["srhName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhFunctionCode"]))
            {
                srhFunctionCode = Request.Params["srhFunctionCode"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhPointType"]))
            {
                srhPointType = Request.Params["srhPointType"].Trim();
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

        TEnvPointSeaVo TEnvPointSeaVo = new TEnvPointSeaVo();
        TEnvPointSeaVo.IS_DEL = "0";
        TEnvPointSeaVo.SORT_FIELD = "YEAR DESC,MONTH";
        TEnvPointSeaVo.SORT_TYPE = "DESC";
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhFunctionCode) || !String.IsNullOrEmpty(srhPointType))
        {
            TEnvPointSeaVo.YEAR = srhYear;
            TEnvPointSeaVo.POINT_NAME  = srhName;
            TEnvPointSeaVo.FUNCTION_CODE = srhFunctionCode;
            TEnvPointSeaVo.POINT_TYPE = srhPointType;

            dt = new TEnvPointSeaLogic().SelectDefinedTadble(TEnvPointSeaVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointSeaLogic().GetSelecDefinedtResultCount(TEnvPointSeaVo);
        }
        else
        {
            dt = new TEnvPointSeaLogic().SelectByTable(TEnvPointSeaVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointSeaLogic().GetSelectResultCount(TEnvPointSeaVo);
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
        TEnvPointSeaVo TEnvPointSeaVo = new TEnvPointSeaVo();
        TEnvPointSeaVo.ID = strValue;
        TEnvPointSeaVo.IS_DEL = "1";
        bool isSuccess = new TEnvPointSeaLogic().Edit(TEnvPointSeaVo);
        if (isSuccess)
            new PageBase().WriteLog("删除近岸海域监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除近岸海域监测点" + TEnvPointSeaVo.ID);
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

        TEnvPointSeaItemVo TEnvPointSeaItemVo = new TEnvPointSeaItemVo();
        TEnvPointSeaItemVo.POINT_ID = oneGridId;
        TEnvPointSeaItemVo.SORT_FIELD = strSortname;
        TEnvPointSeaItemVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointSeaItemLogic().SelectByTable(TEnvPointSeaItemVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointSeaItemLogic().GetSelectResultCount(TEnvPointSeaItemVo);
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
        bool isSuccess = new TEnvPointSeaItemLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除近岸海域监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除近岸海域" + strValue + "监测项目信息");
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
        i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic com = new   i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_SEA_ITEM);
      //  bool isSuccess = new i3.BusinessLogic.Channels.Env.Point.River.TEnvPointRiverLogic().SaveItemByTransaction("T_ENV_P_SEA_ITEM", "POINT_ID", "seaitem_id", strPointId, strValue);
        if (isSuccess)
            new PageBase().WriteLog("批量保存近岸海域监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存近岸海域点位：" + strPointId + "监测项目信息");
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