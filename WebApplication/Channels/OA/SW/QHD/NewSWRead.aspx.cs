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
public partial class Channels_OA_SW_QHD_NewSWRead : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //根据办理状态获取列表信息
        if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
        {
            strResult = getOneGridInfo();
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
        
        TOaSwReadVo tr = new TOaSwReadVo();
        tr.REMARK1 = "0";
        tr.SW_PLAN_ID = LogInfo.UserInfo.ID;
        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();

        DataTable dt = new TOaSwInfoLogic().SelectByTable_ForRead(TOaSwInfoVo, LogInfo.UserInfo.ID, intPageIndex, intPageSize);
        //int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount(TOaSwInfoVo);
        int intTotalCount = new TOaSwReadLogic().GetSelectResultCount(tr);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 修改阅办状态
    /// </summary>
    /// <param name="strValue">收文ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string updateStatus(string strID)
    {
        TOaSwReadVo sr = new TOaSwReadVo();
        sr.SW_ID = strID;
        sr.SW_PLAN_ID = new i3.View.PageBase().LogInfo.UserInfo.ID;
        TOaSwReadVo srv = new TOaSwReadLogic().SelectByObject(sr);
        srv.IS_OK = "1";
        new TOaSwReadLogic().Edit(srv);
      
        return "1";
    }
}