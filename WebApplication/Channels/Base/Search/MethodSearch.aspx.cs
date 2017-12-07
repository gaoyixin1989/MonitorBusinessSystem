using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能：方法依据查询
/// 创建时间：2012-11-29
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_MethodSearch : PageBase
{
    public string srhCode = "", srhName = "", srhMonitorId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取方法依据信息
        if (Request["type"] != null && Request["type"].ToString() == "getMethodInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srhCode"]))
            {
                srhCode = Request.Params["srhCode"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhName"]))
            {
                srhName = Request.Params["srhName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhMonitorId"]))
            {
                srhMonitorId = Request.Params["srhMonitorId"].Trim();
            }
            strResult = getMethodInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取分析方法信息
        if (Request["type"] != null && Request["type"].ToString() == "getAnalysisInfo")
        {
            strResult = getAnalysisInfo(Request["appId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取监测类别信息
        if (Request["type"] != null && Request["type"].ToString() == "getMonitorType")
        {
            strResult = getMonitorType();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取方法依据信息
    /// </summary>
    /// <returns></returns>
    private string getMethodInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseMethodInfoVo TBaseMethodInfoVo = new TBaseMethodInfoVo();
        TBaseMethodInfoVo.IS_DEL = "0";
        TBaseMethodInfoVo.SORT_FIELD = strSortname;
        TBaseMethodInfoVo.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (!String.IsNullOrEmpty(srhCode) || !String.IsNullOrEmpty(srhName) || !String.IsNullOrEmpty(srhMonitorId))
        {
            TBaseMethodInfoVo.METHOD_CODE = srhCode;
            TBaseMethodInfoVo.METHOD_NAME = srhName;
            TBaseMethodInfoVo.MONITOR_ID = srhMonitorId;
            dt = new TBaseMethodInfoLogic().SelectDefinedTadble(TBaseMethodInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseMethodInfoLogic().GetSelecDefinedtResultCount(TBaseMethodInfoVo);
        }
        else
        {
            dt = new TBaseMethodInfoLogic().SelectByTable(TBaseMethodInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseMethodInfoLogic().GetSelectResultCount(TBaseMethodInfoVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取分析方法信息
    /// </summary>
    /// <param name="appId">仪器ID</param>
    /// <returns></returns>
    public string getAnalysisInfo(string appId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseMethodAnalysisVo TBaseMethodAnalysisVo = new TBaseMethodAnalysisVo();
        TBaseMethodAnalysisVo.METHOD_ID = appId;
        TBaseMethodAnalysisVo.IS_DEL = "0";
        TBaseMethodAnalysisVo.SORT_FIELD = strSortname;
        TBaseMethodAnalysisVo.SORT_TYPE = strSortorder;
        DataTable dt = new TBaseMethodAnalysisLogic().SelectByTable(TBaseMethodAnalysisVo, intPageIndex, intPageSize);
        int intTotalCount = new TBaseMethodAnalysisLogic().GetSelectResultCount(TBaseMethodAnalysisVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获得监测类别
    /// </summary>
    /// <returns></returns>
    public string getMonitorType()
    {
        TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(TBaseMonitorTypeInfoVo);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strValue">监测类别ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strValue)
    {
        return new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取方法依据代码
    /// </summary>
    /// <param name="strValue">依据ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorCode(string strValue)
    {
        return new TBaseMethodInfoLogic().Details(strValue).METHOD_CODE;
    }
    /// <summary>
    /// 获取方法依据名称
    /// </summary>
    /// <param name="strValue">依据ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorName(string strValue)
    {
        return new TBaseMethodInfoLogic().Details(strValue).METHOD_NAME;
    }
}