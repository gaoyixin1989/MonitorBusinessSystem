using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.PayFor;
using i3.BusinessLogic.Channels.Env.Point.PayFor;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：生态补偿水质监测点管理
/// 创建日期：2013.06.14 
/// 创建人  ：魏林 
/// </summary>
public partial class Channels_Env_Point_Payfor_Payfor : PageBase
{
    public string srhYear = "", srhMonth="", srhName = "", srhCONTRAL_LEVEL = "";
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
            if (!String.IsNullOrEmpty(Request.Params["srhCONTRAL_LEVEL"]))
            {
                srhCONTRAL_LEVEL = Request.Params["srhCONTRAL_LEVEL"].Trim();
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

        TEnvPPayforVo TEnvPPayfor = new TEnvPPayforVo();
        TEnvPPayfor.IS_DEL = "0";
        TEnvPPayfor.SORT_FIELD = "YEAR DESC,MONTH";
        TEnvPPayfor.SORT_TYPE = "DESC";
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhCONTRAL_LEVEL))
        {
            TEnvPPayfor.YEAR = srhYear;
            TEnvPPayfor.POINT_NAME = srhName;
            TEnvPPayfor.CONTRAL_LEVEL = srhCONTRAL_LEVEL;
            dt = new TEnvPPayforLogic().SelectByTable(TEnvPPayfor, intPageIndex, intPageSize);
            intTotalCount = new TEnvPPayforLogic().GetSelectResultCount(TEnvPPayfor);
        }
        else
        {
            dt = new TEnvPPayforLogic().SelectByTable(TEnvPPayfor, intPageIndex, intPageSize);
            intTotalCount = new TEnvPPayforLogic().GetSelectResultCount(TEnvPPayfor);
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
        TEnvPPayforVo TEnvPPayfor = new TEnvPPayforVo();
        TEnvPPayfor.ID = strValue;
        TEnvPPayfor.IS_DEL = "1";
        bool isSuccess = new TEnvPPayforLogic().Edit(TEnvPPayfor);
        if (isSuccess)
            new PageBase().WriteLog("删除生态补偿水质断面监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除生态补偿水质断面监测点" + TEnvPPayfor.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="twoGridId">垂线ID</param>
    /// <returns></returns>
    public string getTwoGridInfo(string twoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TEnvPPayforItemVo TEnvPPayforItem = new TEnvPPayforItemVo();
        TEnvPPayforItem.POINT_ID = twoGridId;
        TEnvPPayforItem.SORT_FIELD = strSortname;
        TEnvPPayforItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPPayforItemLogic().SelectByTable(TEnvPPayforItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPPayforItemLogic().GetSelectResultCount(TEnvPPayforItem);
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
    public static string saveItemData(string strPointId, string strValue)
    {
        CommonLogic com = new CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_PAYFOR_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存生态补偿水质监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存生态补偿水质点位：" + strPointId + "监测项目信息");
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
    /// <summary>
    /// 保存监测项目的考核标准值
    /// </summary>
    /// <param name="strData">JSON</param>
    /// <returns></returns>
    [WebMethod]
    public static string ItemSaveSt(string strData)
    {
        DataTable dtData = JsonToDataTable(strData);
        bool isSuccess = true;

        if (dtData == null)
            isSuccess = true;
        else
            isSuccess = new TEnvPPayforLogic().SaveItemStData(dtData);

        return isSuccess == true ? "1" : "0";

        //string json = "{\"result\":\"" + result + "\"}";

        //Response.ContentType = "application/json;charset=utf-8";
        //Response.Write(json);
        //Response.End();
    }
}