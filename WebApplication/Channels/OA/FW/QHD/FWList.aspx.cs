using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

using i3.View;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;

/// <summary>
/// 功能描述：发文列表
/// 创建日期：2013-2-2
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_FW_QHD_FWList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!Page.IsPostBack)
        {
            //获取列表信息
            if (Request["type"] != null && Request["type"].ToString() == "GetFWViewList")
            {
                strResult = getFWList(Request.QueryString["strFWStatus"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    //获取点位信息
    private string getFWList(string strFWStatus)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaFwInfoVo objFWInfo = new TOaFwInfoVo();
        objFWInfo.FW_STATUS = strFWStatus;
        objFWInfo.DRAFT_ID = LogInfo.UserInfo.ID;
        int intTotalCount = new TOaFwInfoLogic().GetSelectResultCount(objFWInfo);
        DataTable dt = new TOaFwInfoLogic().SelectByTable(objFWInfo, intPageIndex, intPageSize);
        string strJson = LigerGridDataToJson(dt, intTotalCount);

        return strJson;
    }

    // 删除发文信息
    [WebMethod]
    public static string deleteFWInfo(string strValue)
    {
        bool isSuccess = new TOaFwInfoLogic().Delete(strValue);

        return isSuccess == true ? "1" : "0";
    }
}