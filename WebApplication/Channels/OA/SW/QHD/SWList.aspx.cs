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
/// 创建日期：2013-03-15
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_SW_QHD_SWList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //根据办理状态获取列表信息
        if (Request["type"] != null && Request["type"].ToString() == "GetSWViewList")
        {
            strResult = getSWList(Request.QueryString["strSWStatus"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取断面信息
    /// </summary>
    /// <returns></returns>
    private string getSWList(string strSWStatus)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaSwInfoVo objSWInfo = new TOaSwInfoVo();
        objSWInfo.SW_STATUS = strSWStatus;
        //objSWInfo.DRAFT_ID = LogInfo.UserInfo.ID;
        int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount(objSWInfo);
        DataTable dt = new TOaSwInfoLogic().SelectByTable(objSWInfo, intPageIndex, intPageSize);
        string strJson = LigerGridDataToJson(dt, intTotalCount);

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
        bool isSuccess = new TOaSwInfoLogic().Delete(strValue);
        return isSuccess == true ? "1" : "0";
    }
}