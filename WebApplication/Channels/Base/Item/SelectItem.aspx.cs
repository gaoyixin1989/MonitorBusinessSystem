using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;

/// <summary>
/// 功能描述：获取监测项目，仅为下拉框 弹出grid使用
/// 创建日期：2012-11-02
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_SelectItem : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItems")
        {
            GetItems();
            Response.End();
        }
    }

    //获取监测项目，仅为下拉框 弹出grid使用
    private void GetItems()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhMONITOR_ID = (Request.Params["monitorId"] != null) ? Request.Params["monitorId"] : "";
        string strSrhItemName = (Request.Params["strSrhItemName"] != null) ? Request.Params["strSrhItemName"] : "";

        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount_ForSelectItem_inBag(strSrhMONITOR_ID, strSrhItemName); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ForSelectItem_inBag(strSrhMONITOR_ID, strSrhItemName, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }
}