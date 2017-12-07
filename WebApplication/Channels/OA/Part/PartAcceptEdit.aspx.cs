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
using i3.BusinessLogic.Sys.Resource;

public partial class Channels_OA_Part_PartAcceptEdit : PageBase
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
        TOaPartAcceptedVo objT = new TOaPartAcceptedLogic().Details(strid);
        objT.REMARK2 = new TSysUserLogic().Details(objT.CHECK_USERID).REAL_NAME;
        objT.REMARK3 = new TSysDictLogic().GetDictNameByDictCodeAndType(objT.CHECK_RESULT, "CheckResult");

        return ToJson(objT);
    }
}