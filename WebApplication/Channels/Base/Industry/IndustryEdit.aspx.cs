using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.Industry;

/// <summary>
/// 点位编辑：行业编辑
/// 创建日期：2012-11-21
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Industry_IndustryEdit : PageBase
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
        TBaseIndustryInfoVo objVo = new TBaseIndustryInfoLogic().Details(strID);

        string strJson = ToJson(objVo);

        Response.Write(strJson);
        Response.End();
    }
}