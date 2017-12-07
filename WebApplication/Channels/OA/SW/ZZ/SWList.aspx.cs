using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
using System.Web.Services;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

/// <summary>
/// "收文管理功能"功能
/// 创建人：魏林
/// 创建时间：2013-07-16
/// </summary>
public partial class Channels_OA_SW_ZZ_SWList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strJson = "";

        if (Request["action"] != null)
        {
            switch (Request["action"].ToString())
            {
                case "getGridInfo":
                    strJson = getGridInfo(Request["strStatus"].ToString());
                    break;
                default:
                    break;
            }
            Response.Write(strJson);
            Response.End();
        }

    }

    //获取列表信息
    private string getGridInfo(string strStatus)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        TOaSwInfoVo.SW_STATUS = strStatus;
        TOaSwInfoVo.SW_REG_ID = LogInfo.UserInfo.ID;

        if (Request["FROMCODE"] != null)
        {
            if (Request["FROMCODE"].ToString().Trim() != "")
            { TOaSwInfoVo.FROM_CODE = Request["FROMCODE"].ToString().Trim(); }
        }
        if (Request["SWCODE"] != null)
        {
            if (Request["SWCODE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_CODE = Request["SWCODE"].ToString().Trim(); }
        }
        if (Request["SWFROM"] != null)
        {
            if (Request["SWFROM"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_FROM = Request["SWFROM"].ToString().Trim(); }
        }
        if (Request["SWTITLE"] != null)
        {
            if (Request["SWTITLE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_TITLE = Request["SWTITLE"].ToString().Trim(); }
        }
        if (Request["SIGNDATE"] != null)
        {
            if (Request["SIGNDATE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_SIGN_DATE = Request["SIGNDATE"].ToString().Trim(); }
        }
        if (Request["SUBJECTWORD"] != null)
        {
            if (Request["SUBJECTWORD"].ToString().Trim() != "")
            { TOaSwInfoVo.SUBJECT_WORD = Request["SUBJECTWORD"].ToString().Trim(); }
        }

        DataTable dt = new TOaSwInfoLogic().SelectByTable(TOaSwInfoVo, intPageIndex, intPageSize);
        int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount(TOaSwInfoVo);

        string Json = CreateToJson(dt, intTotalCount);
        return Json;
    }

    /// <summary>
    /// 删除监测点信息
    /// </summary>
    /// <param name="strValue">监测点ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteGridInfo(string strValue)
    {
        //TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        //TOaSwInfoVo.ID = strValue;
        bool isSuccess = new TOaSwInfoLogic().Delete(strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除收文信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除收文信息" + strValue + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }

    protected void btnPrintSW_Click(object sender, EventArgs e)
    {
        string swID = this.txtSWID.Text.Trim();

        if (swID == "")
            return;

        new ZZFWBase().SWPrint(swID);
    }
}