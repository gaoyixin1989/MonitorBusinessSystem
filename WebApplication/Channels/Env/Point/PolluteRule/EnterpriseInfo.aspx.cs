using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.PolluteRule;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_PolluteRule_EnterpriseInfo : PageBase
{
    /// <summary>
    /// //污染源常规 , 刘静楠
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
          //获取企业信息
        if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
        {
            strResult = getOneGridInfo();
            Response.Write(strResult);
            Response.End();
        }
          //获取类别信息
        if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
        {
            strResult = getTwoGridInfo(Request["oneGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取监测点信息
        if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
        {
            strResult = getThreeGridInfo(Request["twoGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取监测项目信息
        if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
        {
            strResult = getFourGridInfo(Request["threeGridId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    #region//获取企业信息
    /// <summary>
    /// 获取企业信息
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
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        TEnvPEnterinfoVo TEnvPEnterInfo = new TEnvPEnterinfoVo();
        TEnvPEnterInfo.IS_DEL = "0";
        dt = new TEnvPEnterinfoLogic().SelectByTable(TEnvPEnterInfo, intPageIndex, intPageSize);
        intTotalCount = new TEnvPEnterinfoLogic().GetSelectResultCount(TEnvPEnterInfo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    #endregion

    #region//获取类别信息
    public string getTwoGridInfo(string oneGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TEnvPPolluteTypeVo TEnvPPolluteType = new TEnvPPolluteTypeVo();
        TEnvPPolluteType.SATAIONS_ID = oneGridId;
        TEnvPPolluteType.SORT_FIELD = strSortname;
        TEnvPPolluteType.SORT_TYPE = strSortorder;
        TEnvPPolluteType.IS_DEL = "0";
        DataTable dt = new TEnvPPolluteTypeLogic().SelectByTable(TEnvPPolluteType, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPPolluteTypeLogic().GetSelectResultCount(TEnvPPolluteType);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    #endregion

    #region//获取监测点信息
    public string getThreeGridInfo(string twoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TEnvPPolluteVo TEnvPPolluteType = new TEnvPPolluteVo();
        TEnvPPolluteType.TYPE_ID = twoGridId;
        TEnvPPolluteType.IS_DEL = "0";
        TEnvPPolluteType.SORT_FIELD = strSortname;
        TEnvPPolluteType.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPPolluteLogic().SelectByTable(TEnvPPolluteType, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPPolluteLogic().GetSelectResultCount(TEnvPPolluteType);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    #endregion

    #region//获取监测项目信息
    public string getFourGridInfo(string threeGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TEnvPPolluteItemVo TEnvPPolluteType = new TEnvPPolluteItemVo();
        TEnvPPolluteType.POINT_ID = threeGridId;
        TEnvPPolluteType.SORT_FIELD = strSortname;
        TEnvPPolluteType.SORT_TYPE = strSortorder;
        DataTable dt = new TEnvPPolluteItemLogic().SelectByTable(TEnvPPolluteType, intPageIndex, intPageSize);
        int intTotalCount = new TEnvPPolluteItemLogic().GetSelectResultCount(TEnvPPolluteType);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    #endregion
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
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictNames(string strDictCode)
    {
        return PageBase.getDictNames(strDictCode);
    }
    /// <summary>
    /// 删除企业信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteOneGridInfo(string strValue)
    {
        TEnvPEnterinfoVo TEnvPEnterInfo = new TEnvPEnterinfoVo();
        TEnvPEnterInfo.ID = strValue;
        TEnvPEnterInfo.IS_DEL = "1";
        bool isSuccess = new TEnvPEnterinfoLogic().Edit(TEnvPEnterInfo);
        if (isSuccess)
            new PageBase().WriteLog("删除企业信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除企业信息" + strValue);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 删除类别信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteTwoGridInfo(string strValue)
    {
        TEnvPPolluteTypeVo TEnvPEnterInfo = new TEnvPPolluteTypeVo();
        TEnvPEnterInfo.ID = strValue;
        TEnvPEnterInfo.IS_DEL = "1";
        bool isSuccess = new TEnvPPolluteTypeLogic().Edit(TEnvPEnterInfo); 
        if (isSuccess)
            new PageBase().WriteLog("删除类别信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除类别信息" + strValue);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 删除类别信息
    /// </summary>
    /// <param name="strValue">断面ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteThreeGridInfo(string strValue)
    {
        TEnvPPolluteVo TEnvPEnterInfo = new TEnvPPolluteVo();
        TEnvPEnterInfo.ID = strValue;
        TEnvPEnterInfo.IS_DEL = "1";
        bool isSuccess = new TEnvPPolluteLogic().Edit(TEnvPEnterInfo);
        if (isSuccess)
            new PageBase().WriteLog("删除监测点信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除监测点信息" + strValue);
        return isSuccess == true ? "1" : "0";
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
    /// 保存监测项目信息
    /// </summary>
    /// <param name="strVerticalCode">垂线ID</param>
    /// <param name="strValue">监测项目值</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemData(string strVerticalCode, string strValue)
    {
        CommonLogic com = new CommonLogic();
        bool isSuccess = com.SaveItemInfo(TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE, strVerticalCode, strValue, SerialType.T_ENV_POINT_POLLUTE_ITEM);
        if (isSuccess)
            new PageBase().WriteLog("批量保存常规污染源监测项目信息", "", new UserLogInfo().UserInfo.USER_NAME + "批量保存常规污染源：" + strVerticalCode + "监测项目信息");
        return isSuccess == true ? "1" : "0";
    }
}