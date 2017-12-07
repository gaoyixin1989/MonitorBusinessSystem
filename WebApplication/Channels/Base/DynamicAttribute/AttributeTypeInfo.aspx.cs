using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;

/// <summary>
/// 功能描述：属性类别管理
/// 创建日期：2012-11-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeTypeInfo : PageBase
{
    public string srhSortName = "", srhMonitorId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取属性类别信息
        if (Request["type"] != null && Request["type"].ToString() == "getAttributeTypeInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srhSortName"]))
            {
                srhSortName = Request.Params["srhSortName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_MonitorId"]))
            {
                srhMonitorId = Request.Params["srh_MonitorId"].Trim();
            }
            strResult = getAttributeTypeInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strValue">监测类别信息代码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorType(string strValue)
    {
        return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
    }
    /// <summary>
    /// 获取属性类别信息
    /// </summary>
    /// <returns></returns>
    private string getAttributeTypeInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseAttributeTypeVo TBaseAttributeTypeVo = new TBaseAttributeTypeVo();
        TBaseAttributeTypeVo.IS_DEL = "0";
        TBaseAttributeTypeVo.SORT_FIELD = strSortname;
        TBaseAttributeTypeVo.SORT_TYPE = strSortorder;
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(srhSortName) || !String.IsNullOrEmpty(srhMonitorId))
        {
            TBaseAttributeTypeVo.SORT_NAME = srhSortName;
            TBaseAttributeTypeVo.MONITOR_ID = srhMonitorId;
            intTotalCount = new TBaseAttributeTypeLogic().GetSelecDefinedtResultCount(TBaseAttributeTypeVo);
            dt = new TBaseAttributeTypeLogic().SelectDefinedTadble(TBaseAttributeTypeVo, intPageIndex, intPageSize);
        }
        else
        {
            dt = new TBaseAttributeTypeLogic().SelectByTable(TBaseAttributeTypeVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseAttributeTypeLogic().GetSelectResultCount(TBaseAttributeTypeVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除属性类别信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteAttributeTypeInfo(string strValue)
    {
        TBaseAttributeTypeVo TBaseAttributeTypeVo = new TBaseAttributeTypeVo();
        TBaseAttributeTypeVo.ID = strValue;
        TBaseAttributeTypeVo.IS_DEL = "1";
        bool isSuccess = new TBaseAttributeTypeLogic().Edit(TBaseAttributeTypeVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除属性类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除属性类别" + TBaseAttributeTypeVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }

}