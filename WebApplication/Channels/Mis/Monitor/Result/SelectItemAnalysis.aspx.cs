using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;

/// <summary>
/// 功能描述：获取项目中的分析方法、仪器，仅为下拉框 弹出grid使用
/// 创建日期：2014-8-26
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Base_Item_SelectItemAnalysis : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItemAnalysis")
        {
            GetItemAnalysis(Request.Params["ItemID"].ToString());
            Response.End();
        }
    }

    //获取项目中的分析方法、仪器，仅为下拉框 弹出grid使用
    private void GetItemAnalysis(string strItemID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
        objItemAnalysis.IS_DEL = "0";
        objItemAnalysis.ITEM_ID = strItemID;
        objItemAnalysis.SORT_FIELD = strSortname;
        objItemAnalysis.SORT_TYPE = strSortorder;

        TBaseItemAnalysisLogic logicItemAnalysis = new TBaseItemAnalysisLogic();
        //int intTotalCount = logicItemAnalysis.GetSelectResultCount(objItemAnalysis); ;//总计的数据条数
        DataTable dt = logicItemAnalysis.SelectByTable_ByJoin(objItemAnalysis, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
    }
}