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
/// 功能描述：属性信息配置管理
/// 创建日期：2012-11-07
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeInfoConfig : PageBase
{
    public string srh_ItemId = "", srh_AttTypeId = "", srh_AttId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取属性信息
        if (Request["type"] != null && Request["type"].ToString() == "getAttributeConfigInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srh_ItemId"]))
            {
                srh_ItemId = Request.Params["srh_ItemId"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_AttTypeId"]))
            {
                srh_AttTypeId = Request.Params["srh_AttTypeId"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_AttId"]))
            {
                srh_AttId = Request.Params["srh_AttId"].Trim();
            }
            strResult = getAttributeConfigInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取属性配置信息
    /// </summary>
    /// <returns></returns>
    private string getAttributeConfigInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVo = new TBaseAttributeTypeValueVo();
        TBaseAttributeTypeValueVo.IS_DEL = "0";
        TBaseAttributeTypeValueVo.SORT_FIELD = strSortname;
        TBaseAttributeTypeValueVo.SORT_TYPE = strSortorder;
        if (!String.IsNullOrEmpty(srh_ItemId) || !String.IsNullOrEmpty(srh_AttTypeId) || !String.IsNullOrEmpty(srh_AttId))
        {
            TBaseAttributeTypeValueVo.ITEM_TYPE = srh_ItemId;
            TBaseAttributeTypeValueVo.ATTRIBUTE_TYPE_ID = srh_AttTypeId;
            TBaseAttributeTypeValueVo.ATTRIBUTE_ID = srh_AttId;
            dt = new TBaseAttributeTypeValueLogic().SelectDefinedTadble(TBaseAttributeTypeValueVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseAttributeTypeValueLogic().GetSelecDefinedtResultCount(TBaseAttributeTypeValueVo);
        }
        else
        {
            dt = new TBaseAttributeTypeValueLogic().SelectByTable(TBaseAttributeTypeValueVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseAttributeTypeValueLogic().GetSelectResultCount(TBaseAttributeTypeValueVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除属性配置信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteAttributeConfigInfo(string strValue)
    {
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVo = new TBaseAttributeTypeValueVo();
        TBaseAttributeTypeValueVo.ID = strValue;
        TBaseAttributeTypeValueVo.IS_DEL = "1";
        bool isSuccess = new TBaseAttributeTypeValueLogic().Edit(TBaseAttributeTypeValueVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除属性配置", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除属性配置" + strValue + "成功");
        }
        return isSuccess == true ? "1" : "0";
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
    /// 获取属性类型
    /// </summary>
    /// <param name="strValue">属性类型代码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAttributeType(string strValue)
    {
        return new TBaseAttributeTypeLogic().Details(strValue).SORT_NAME;
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    /// <param name="strValue">属性名称代码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAttributeName(string strValue)
    {
        return new TBaseAttributeInfoLogic().Details(strValue).ATTRIBUTE_NAME;
    }
}