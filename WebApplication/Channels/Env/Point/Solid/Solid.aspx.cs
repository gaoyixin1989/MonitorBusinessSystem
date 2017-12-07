using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Solid;
using i3.BusinessLogic.Channels.Env.Point.Solid;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：固废监测点管理
/// 创建日期：2013-06-15
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_Solid_Solid : PageBase
{
    public string srhYear = "", srhMonth = "", srhName = "", srhArea = "";
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

        TEnvPSolidVo TEnvPSolid = new TEnvPSolidVo();
        TEnvPSolid.IS_DEL = "0";
        TEnvPSolid.SORT_FIELD = strSortname;
        TEnvPSolid.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhYear) || !String.IsNullOrEmpty(srhMonth) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhArea))
        {
            TEnvPSolid.YEAR = srhYear;
            TEnvPSolid.MONTH = srhMonth;
            TEnvPSolid.POINT_NAME = srhName;
            TEnvPSolid.AREA_ID = srhArea;
            dt = new TEnvPSolidLogic().SelectByTable(TEnvPSolid, intPageIndex, intPageSize);
            intTotalCount = new TEnvPSolidLogic().GetSelectResultCount(TEnvPSolid);
        }
        else
        {
            dt = new TEnvPSolidLogic().SelectByTable(TEnvPSolid, intPageIndex, intPageSize);
            intTotalCount = new TEnvPSolidLogic().GetSelectResultCount(TEnvPSolid);
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
        TEnvPSolidVo TEnvPSolid = new TEnvPSolidVo();
        TEnvPSolid.ID = strValue;
        TEnvPSolid.IS_DEL = "1";
        bool isSuccess = new TEnvPSolidLogic().Edit(TEnvPSolid);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除固废监测点", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除固废监测点" + TEnvPSolid.ID + "成功");
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

        TEnvPSolidItemVo TEnvPSolidItem = new TEnvPSolidItemVo();
        TEnvPSolidItem.POINT_ID = oneGridId;
        TEnvPSolidItem.SORT_FIELD = strSortname;
        TEnvPSolidItem.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPSolidItemLogic().SelectByTable(TEnvPSolidItem, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPSolidItemLogic().GetSelectResultCount(TEnvPSolidItem);
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
        bool isSuccess = com.SaveItemInfo(TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE, strPointId, strValue, SerialType.T_ENV_P_SOLID_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存固废监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存固废：" + strPointId + "监测项目信息");
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