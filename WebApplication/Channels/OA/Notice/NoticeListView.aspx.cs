using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using System.IO;

using i3.ValueObject.Channels.OA.Notice;
using i3.BusinessLogic.Channels.OA.Notice;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：公告管理列表查阅功能
/// 创建日期：2013-02-28
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_Notice_NoticeListView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取降尘监测点信息
        if (Request["type"] != null && Request["type"].ToString() == "getDataInfo")
        {
            strResult = getDataInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    private string getDataInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaNoticeVo TOaNoticeVo = new TOaNoticeVo();
        TOaNoticeVo.SORT_FIELD = strSortname;
        TOaNoticeVo.SORT_TYPE = strSortorder;
        DataTable dt = new TOaNoticeLogic().SelectByTable(TOaNoticeVo, intPageIndex, intPageSize);
        int intTotalCount = new TOaNoticeLogic().GetSelectResultCount(TOaNoticeVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
}