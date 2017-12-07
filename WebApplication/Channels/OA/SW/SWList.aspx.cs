using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;

/// <summary>
/// 功能描述：收文列表功能
/// 创建日期：2013-02-02
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_SW_SWList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //根据办理状态获取列表信息
        if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
        {
            strResult = getOneGridInfo(Request["strSwStatus"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取断面信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strSwStatus)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        TOaSwInfoVo.SW_STATUS = strSwStatus;

        DataTable dt = new TOaSwInfoLogic().SelectByTable(TOaSwInfoVo, intPageIndex, intPageSize);
        int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount(TOaSwInfoVo);

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
        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        TOaSwInfoVo.ID = strValue;
        bool isSuccess = new TOaSwInfoLogic().Edit(TOaSwInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除收文信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除收文信息" + TOaSwInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}