using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.BusinessLogic.Channels.Env.Point.Estuaries;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：入海河口断面列表
/// 创建日期：2012-11-19
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Estuaries_EstuariesList : PageBase
{
    public string srhYear = "", srhName = "", srhArea = "";
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

        TEnvPointEstuariesVo TEnvPointEstuariesVo = new TEnvPointEstuariesVo();
        TEnvPointEstuariesVo.IS_DEL = "0";
        TEnvPointEstuariesVo.SORT_FIELD = "YEAR DESC,MONTH";
        TEnvPointEstuariesVo.SORT_TYPE = "DESC";
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPointEstuariesVo.YEAR = srhYear;
            TEnvPointEstuariesVo.SECTION_NAME = srhName;
            TEnvPointEstuariesVo.AREA_ID = srhArea;
            dt = new TEnvPointEstuariesLogic().SelectDefinedTadble(TEnvPointEstuariesVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointEstuariesLogic().GetSelecDefinedtResultCount(TEnvPointEstuariesVo);
        }
        else
        {
            dt = new TEnvPointEstuariesLogic().SelectByTable(TEnvPointEstuariesVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointEstuariesLogic().GetSelectResultCount(TEnvPointEstuariesVo);
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
        TEnvPointEstuariesVo TEnvPointEstuariesVo = new TEnvPointEstuariesVo();
        TEnvPointEstuariesVo.ID = strValue;
        TEnvPointEstuariesVo.IS_DEL = "1";
        bool isSuccess = new TEnvPointEstuariesLogic().Edit(TEnvPointEstuariesVo);
        if (isSuccess)
            new PageBase().WriteLog("删除入海河口监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除入海河口监测点" + TEnvPointEstuariesVo.ID);
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

        TEnvPointEstuariesVerticalVo TEnvPointEstuariesVerticalVo = new TEnvPointEstuariesVerticalVo();
        TEnvPointEstuariesVerticalVo.SECTION_ID = oneGridId;
        TEnvPointEstuariesVerticalVo.SORT_FIELD = strSortname;
        TEnvPointEstuariesVerticalVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointEstuariesVerticalLogic().SelectByTable(TEnvPointEstuariesVerticalVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointEstuariesVerticalLogic().GetSelectResultCount(TEnvPointEstuariesVerticalVo);
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
        bool isSuccess = new TEnvPointEstuariesVerticalLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除入海河口垂线信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除入海河口垂线信息" + strValue);
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

        TEnvPointEstuariesVItemVo TEnvPointEstuariesVItemVo = new TEnvPointEstuariesVItemVo();
        TEnvPointEstuariesVItemVo.POINT_ID = twoGridId;
        TEnvPointEstuariesVItemVo.SORT_FIELD = strSortname;
        TEnvPointEstuariesVItemVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointEstuariesVItemLogic().SelectByTable(TEnvPointEstuariesVItemVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointEstuariesVItemLogic().GetSelectResultCount(TEnvPointEstuariesVItemVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 删除垂线监测项目信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteThreeGridInfo(string strValue)
    {
        bool isSuccess = new TEnvPointEstuariesVItemLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除入海河口垂线监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除入海河口垂线监测项目信息" + strValue);
        return isSuccess == true ? "1" : "0";
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
        bool isSuccess = com.SaveItemInfo(TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_P_ESTUARIES_V_ITEM);
  //      bool isSuccess = new i3.BusinessLogic.Channels.Env.Point.River.TEnvPointRiverLogic().SaveItemByTransaction("T_ENV_P_ESTUARIES_V_ITEM", "estuariespointverticalitem_id", strVerticalCode, strValue);
        if (isSuccess)
            new PageBase().WriteLog("批量保存入海河口垂线监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存入海河口垂线" + strVerticalCode + "监测项目信息");
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