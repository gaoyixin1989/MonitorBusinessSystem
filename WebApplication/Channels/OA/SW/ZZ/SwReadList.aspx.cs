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
/// "收文传阅功能"功能(查看本人的传阅收文)
/// 创建人：潘德军
/// 创建时间：2013-08-07
/// </summary>
public partial class Channels_OA_SW_ZZ_SwReadList : PageBase
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
        //string strSortname = "SW_PLAN_DATE";
        //string strSortorder = "desc";
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        TOaSwInfoVo.SW_STATUS = strStatus;
        TOaSwInfoVo.SORT_FIELD = "READ_DATE";
        TOaSwInfoVo.SORT_TYPE = "DESC";
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
        if (Request["SIGNDATE"] != null)//传阅日期
        {
            if (Request["SIGNDATE"].ToString().Trim() != "")
            { TOaSwInfoVo.REMARK1 = Request["SIGNDATE"].ToString().Trim(); }
        }
        if (Request["SUBJECTWORD"] != null)
        {
            if (Request["SUBJECTWORD"].ToString().Trim() != "")
            { TOaSwInfoVo.SUBJECT_WORD = Request["SUBJECTWORD"].ToString().Trim(); }
        }

        DataTable dt = new TOaSwInfoLogic().SelectByTable_ForRead(TOaSwInfoVo,base.LogInfo.UserInfo.ID, intPageIndex, intPageSize);
        int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount_ForRead(TOaSwInfoVo, base.LogInfo.UserInfo.ID);

        string Json = CreateToJson(dt, intTotalCount);
        return Json;
    }
}