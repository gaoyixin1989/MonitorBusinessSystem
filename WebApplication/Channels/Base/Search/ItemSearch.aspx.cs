using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：项目查询
/// 创建日期：2012-11-28
/// 创建人  ：邵世卓
/// </summary>
public partial class Channels_Base_Search_ItemSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItems")
        {
            GetItems();
            Response.End();
        }
        if (Request.Params["Action"] == "GetMethods")
        {
            GetMethods();
            Response.End();
        }
    }

    /// <summary>
    /// 获取监测项目
    /// </summary>
    private void GetItems()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //监测类别参数
        string strSrhMONITOR_ID = (Request.Params["SrhMONITOR_ID"] != null) ? Request.Params["SrhMONITOR_ID"] : "";
        //监测项目参数
        string strSrhITEM_NAME = (Request.Params["SrhITEM_NAME"] != null) ? Request.Params["SrhITEM_NAME"] : "";
        //排序字段
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TBaseItemInfoVo.ORDER_NUM_FIELD;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "1";

        objItem.MONITOR_ID = strSrhMONITOR_ID;
        objItem.ITEM_NAME = strSrhITEM_NAME;

        objItem.SORT_FIELD = strSortname;
        objItem.SORT_TYPE = strSortorder;
        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount(objItem); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ByJoinMonitorType(objItem, intPageIdx, intPagesize);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD] = (dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString().Length > 0) ? (int.Parse(dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString()).ToString()) : "";
        }


        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 获取指定监测项目的分析方法
    /// </summary>
    private void GetMethods()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelItemID = (Request.Params["selItemID"] != null) ? Request.Params["selItemID"] : "";
        if (strSelItemID.Length <= 0)
        {
            Response.Write("");
            return;
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseItemAnalysisVo.ID_FIELD;

        TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
        objItemAnalysis.IS_DEL = "0";
        objItemAnalysis.ITEM_ID = strSelItemID;
        objItemAnalysis.SORT_FIELD = strSortname;
        objItemAnalysis.SORT_TYPE = strSortorder;
        TBaseItemAnalysisLogic logicItemAnalysis = new TBaseItemAnalysisLogic();

        int intTotalCount = logicItemAnalysis.GetSelectResultCount(objItemAnalysis); ;//总计的数据条数
        DataTable dt = logicItemAnalysis.SelectByTable_ByJoin(objItemAnalysis, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }
}
