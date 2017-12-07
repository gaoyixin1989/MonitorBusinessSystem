using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;

/// <summary>
/// 点位编辑：附件费用设置
/// 创建日期：2012-11-16
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_TestPrice_AttachPriceEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["strid"] != null)
        {
            strID = this.Request["strid"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strID);
        }
    }

    //获取数据
    private void GetData(string strID)
    {
        TMisContractAttfeeitemVo objFee = new TMisContractAttfeeitemLogic().Details(strID);

        string strJson = ToJson(objFee);

        Response.Write(strJson);
        Response.End();
    }
}