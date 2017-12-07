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

/// <summary>
/// 功能描述：获取仪器，仅为下拉框 弹出grid使用
/// 创建日期：2012-11-02
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_SelectApparatus : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetApparatus")
        {
            GetApparatus();
            Response.End();
        }
    }

    //获取仪器，仅为下拉框 弹出grid使用
    private void GetApparatus()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhApparatus_CODE = (Request.Params["strSrhApparatus_CODE"] != null) ? Request.Params["strSrhApparatus_CODE"] : "";
        string strSrhName = (Request.Params["strSrhName"] != null) ? Request.Params["strSrhName"] : "";

        TBaseApparatusInfoLogic logicApparatus = new TBaseApparatusInfoLogic();

        int intTotalCount = logicApparatus.GetSelectResultCount_ForSelectApparatus_inItem(strSrhName, strSrhApparatus_CODE); ;//总计的数据条数
        DataTable dt = logicApparatus.SelectByTable_ForSelectApparatus_inItem(strSrhName, strSrhApparatus_CODE, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }
}