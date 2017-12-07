using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;

/// <summary>
/// 功能描述：属性信息管理
/// 创建日期：2012-11-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeInfo : PageBase
{
    public string srhNmae = "", srhControlId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取属性信息
        if (Request["type"] != null && Request["type"].ToString() == "getAttributeInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srhName"]))
            {
                srhNmae = Request.Params["srhName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhControlId"]))
            {
                srhControlId = Request.Params["srhControlId"].Trim();
            }
            strResult = getAttributeInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取属性信息
    /// </summary>
    /// <returns></returns>
    private string getAttributeInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        TBaseAttributeInfoVo TBaseAttributeInfoVo = new TBaseAttributeInfoVo();
        TBaseAttributeInfoVo.IS_DEL = "0";
        TBaseAttributeInfoVo.SORT_FIELD = strSortname;
        TBaseAttributeInfoVo.SORT_TYPE = strSortorder;
        if (!String.IsNullOrEmpty(srhNmae) || !String.IsNullOrEmpty(srhControlId))
        {
            TBaseAttributeInfoVo.ATTRIBUTE_NAME = srhNmae;
            TBaseAttributeInfoVo.CONTROL_NAME = srhControlId;
            dt = new TBaseAttributeInfoLogic().SelectDefinedTadble(TBaseAttributeInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseAttributeInfoLogic().GetSelecDefinedtResultCount(TBaseAttributeInfoVo);
        }
        else
        {
            dt = new TBaseAttributeInfoLogic().SelectByTable(TBaseAttributeInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseAttributeInfoLogic().GetSelectResultCount(TBaseAttributeInfoVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除属性信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteAttributeInfo(string strValue)
    {
        TBaseAttributeInfoVo TBaseAttributeInfoVo = new TBaseAttributeInfoVo();
        TBaseAttributeInfoVo.ID = strValue;
        TBaseAttributeInfoVo.IS_DEL = "1";
        bool isSuccess = new TBaseAttributeInfoLogic().Edit(TBaseAttributeInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除属性信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除属性信息" + TBaseAttributeInfoVo.ID + "成功");
        }
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
}