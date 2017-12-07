using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：根据企业ID获取点位信息，仅为下拉框 弹出grid使用
/// 创建日期：2014-02-19
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Base_Item_SelectCompanyPoint : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetPoints")
        {
            GetPoints();
            Response.End();
        }
    }

    //获取点位信息，仅为下拉框 弹出grid使用
    private void GetPoints()
    {
        string strSrhPointName = (Request.Params["strSrhPointName"] != null) ? Request.Params["strSrhPointName"] : "";
        string strCOMPANY_ID = Request.Params["COMPANY_ID"].ToString();

        CommonLogic common = new CommonLogic();

        DataTable dt = common.getCompanyPointInfo(strCOMPANY_ID, strSrhPointName);

        string strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
    }
}