using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.BusinessLogic.Channels.Env.Point.Offshore;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：近岸直排监测点管理
/// 创建日期：2012-11-20
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Offshore_OffshoreList : PageBase
{
    public string srhYear = "", srhName = "", srhLocation = "";
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
            if (!String.IsNullOrEmpty(Request.Params["srhLocation"]))
            {
                srhLocation = Request.Params["srhLocation"].Trim();
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

        TEnvPointOffshoreVo TEnvPointOffshoreVo = new TEnvPointOffshoreVo();
        TEnvPointOffshoreVo.IS_DEL = "0";
        TEnvPointOffshoreVo.SORT_FIELD = strSortname;
        TEnvPointOffshoreVo.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhLocation))
        {
            TEnvPointOffshoreVo.YEAR = srhYear;
            TEnvPointOffshoreVo.COMPANY_NAME = srhName;
            TEnvPointOffshoreVo.LOCATION = srhLocation;
            dt = new TEnvPointOffshoreLogic().SelectDefinedTadble(TEnvPointOffshoreVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointOffshoreLogic().GetSelecDefinedtResultCount(TEnvPointOffshoreVo);
        }
        else
        {
            dt = new TEnvPointOffshoreLogic().SelectByTable(TEnvPointOffshoreVo, intPageIndex, intPageSize);
            intTotalCount = new TEnvPointOffshoreLogic().GetSelectResultCount(TEnvPointOffshoreVo);
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
        TEnvPointOffshoreVo TEnvPointOffshoreVo = new TEnvPointOffshoreVo();
        TEnvPointOffshoreVo.ID = strValue;
        TEnvPointOffshoreVo.IS_DEL = "1";
        bool isSuccess = new TEnvPointOffshoreLogic().Edit(TEnvPointOffshoreVo);
        if (isSuccess)
            new PageBase().WriteLog("删除近岸直排监测点", "", new UserLogInfo().UserInfo.USER_NAME + "删除近岸直排监测点" + TEnvPointOffshoreVo.ID);
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

        TEnvPointOffshoreItemVo TEnvPointOffshoreItemVo = new TEnvPointOffshoreItemVo();
        TEnvPointOffshoreItemVo.POINT_ID = oneGridId;
        TEnvPointOffshoreItemVo.SORT_FIELD = strSortname;
        TEnvPointOffshoreItemVo.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPointOffshoreItemLogic().SelectByTable(TEnvPointOffshoreItemVo, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPointOffshoreItemLogic().GetSelectResultCount(TEnvPointOffshoreItemVo);
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
        bool isSuccess = new TEnvPointOffshoreItemLogic().Delete(strValue);
        if (isSuccess)
            new PageBase().WriteLog("删除近岸直排项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除近岸直排项目信息" + strValue);
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
        bool isSuccess = com.SaveItemInfo(TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_POINT_OFFSHORE_TABLE_ITEM);
        //bool isSuccess = new i3.BusinessLogic.Channels.Env.Point.Offshore.TEnvPointOffshoreLogic().SaveItemByTransaction("T_ENV_P_OFFSHORE_ITEM", "POINT_ID", "offshoreitem_id", strPointId, strValue);
        if (isSuccess)
            new PageBase().WriteLog("批量保存近岸直排监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存近岸直排点位：" + strPointId + "的监测项目信息");
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