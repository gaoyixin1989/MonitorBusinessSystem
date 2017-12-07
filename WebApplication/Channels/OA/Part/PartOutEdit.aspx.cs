using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using i3.BusinessLogic.Sys.General;

public partial class Channels_OA_Part_PartOutEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        string strid = "";
        //获取企业信息
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            if (!String.IsNullOrEmpty(Request.Params["strid"]))
            {
                strid = Request.Params["strid"].Trim();
            }

            strResult = getInfo(strid);
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取信息
    /// </summary>
    /// <returns></returns>
    private string getInfo(string strid)
    {
        TOaPartCollarVo objT = new TOaPartCollarLogic().Details(strid);
        objT.REMARK2 = new TSysUserLogic().Details(objT.USER_ID).REAL_NAME;

        return ToJson(objT);
    }
}