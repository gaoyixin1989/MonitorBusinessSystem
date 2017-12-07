using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;

/// <summary>
/// 功能描述：获取分析方法，仅为下拉框 弹出grid使用
/// 创建日期：2012-11-02
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_SelectMethod : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetMthods")
        {
            GetMthods();
            Response.End();
        }
    }

    //获取分析方法，仅为下拉框 弹出grid使用
    private void GetMthods()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhMONITOR_ID = (Request.Params["SrhMONITOR_ID"] != null) ? Request.Params["SrhMONITOR_ID"] : "";
        string strSrhMethod_Code = (Request.Params["strSrhMethod_Code"] != null) ? Request.Params["strSrhMethod_Code"] : "";
        string strSrhMethodSel_ItemID = Request.Params["MethodSel_ItemID"].ToString();

        TBaseMethodAnalysisLogic logicMethod = new TBaseMethodAnalysisLogic();

        int intTotalCount = logicMethod.GetSelectResultCount_ForSelectMethod_inItem(strSrhMethod_Code, strSrhMethodSel_ItemID); ;//总计的数据条数
        DataTable dt = logicMethod.SelectByTable_ForSelectMethod_inItem(strSrhMethod_Code, strSrhMethodSel_ItemID, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }
}