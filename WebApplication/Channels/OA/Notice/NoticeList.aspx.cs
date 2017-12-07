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
/// 功能描述：公告管理列表
/// 创建日期：2013-02-25
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_Notice_NoticeList : PageBase
{
    public string strTitle = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (Request["type"] != null && Request["type"].ToString() == "getDataInfo")
        {
            strResult = getDataInfo();
            Response.Write(strResult);
            Response.End();
        }

        //获取查询公告信息 黄进军 add 20160223
        if (Request["type"] != null && Request["type"].ToString() == "getNoticeList")
        {
            //获取传入的查询条件
            if (!String.IsNullOrEmpty(Request.Params["strTitle"]))
            {
                strTitle = Request.Params["strTitle"].Trim();
            }

            strResult = getNoticeList();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取查询公告信息
    /// huangjinjun add 20160223
    /// </summary>
    /// <returns></returns>
    private string getNoticeList()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaNoticeVo notice = new TOaNoticeVo();
        notice.SORT_FIELD = strSortname;
        notice.SORT_TYPE = strSortorder;

        //自定义查询使用
        if (!String.IsNullOrEmpty(strTitle))
        {
            notice.TITLE = strTitle;
            dt = new TOaNoticeLogic().SelectByTable(notice, intPageIndex, intPageSize);
            intTotalCount = dt.Rows.Count;
        }
        //无条件首次加载用
        else
        {
            dt = new TOaNoticeLogic().SelectByTable(notice, intPageIndex, intPageSize);
            intTotalCount = dt.Rows.Count;
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteDataInfo(string strValue)
    {
        TOaNoticeVo TOaNoticeVo = new TOaNoticeVo();
        TOaNoticeVo.ID = strValue;
        bool isSuccess = new TOaNoticeLogic().Delete(TOaNoticeVo);
        if (isSuccess)
            new PageBase().WriteLog("删除公告信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除公告信息" + TOaNoticeVo.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 主界面公告接口，获取前四条公告完整信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getNoticeInfo()
    {
        string serverPath = "..\\Channels\\OA\\Notice\\Image";
        DataTable dt = new TOaNoticeLogic().getTopTenData();
        dt.Columns.Add("Path", System.Type.GetType("System.String"));
        foreach (DataRow row in dt.Rows)
        {
            if (row["CONTENT"].ToString().Length > 140)
                row["CONTENT"] = row["CONTENT"].ToString().Substring(0, 140) + "...";

            TOaAttVo TOaAttVo = new TOaAttVo();
            TOaAttVo.BUSINESS_ID = row["ID"].ToString();
            TOaAttVo.BUSINESS_TYPE = "OA_NOTICE";
            TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
            string strExtent = TOaAttVoTemp.ATTACH_TYPE;
            string path = serverPath + "\\" + row["ID"].ToString() + strExtent;
            if (strExtent != "")
                row["Path"] = path;
            else
                row["Path"] = "..\\Channels\\OA\\Notice\\default.gif";
        }
        string strJson = DataTableToJson(dt);
        return strJson;
    }

    /// <summary>
    /// 置顶
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string SetTopOne(string id)
    {
        new TOaNoticeLogic().EditAll();
        new TOaNoticeLogic().EditSetTopOne(id);

        return "1";
    }
}