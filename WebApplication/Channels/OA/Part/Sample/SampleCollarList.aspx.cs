using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using i3.BusinessLogic.Channels.OA.PART.SAMPLE;
/// <summary>
/// 功能描述：样品领用历史记录明细
/// 创建日期：2013-09-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_OA_Part_Sample_SampleCollarList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string json = "";
            if (Request["type"] != null)
            {
                switch (Request["type"].ToString())
                {
                    case "GetSampleCollarInfor":
                        json = GetSampleCollarInfor();
                        break;
                }

                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
            }
        }
    }

    private string GetSampleCollarInfor()
    {
        string strJson = "";
        DataTable dt = new DataTable();
        TOaPartstandCollarVo objItems = new TOaPartstandCollarVo();
        TOaPartstandInfoVo objItemParts = new TOaPartstandInfoVo();

        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (!String.IsNullOrEmpty(Request["strPartId"]))
        {
            objItemParts.ID = Request["strPartId"].ToString();
        }
        else
        {
            objItemParts.SAMPLE_CODE = Request["strPartCode"].ToString();
            objItemParts.SAMPLE_NAME = Request["strPartName"].ToString();
            objItems.REMARK4 = Request["strBeginDate"].ToString();
            objItems.REMARK5 = Request["strEndDate"].ToString();
            objItems.USER_ID = Request["strReal_Name"].ToString();
        }

        dt = new TOaPartstandCollarLogic().SelectUnionPartByTable(objItems, objItemParts, intPageIndex, intPageSize);
        int CountNum = new TOaPartstandCollarLogic().GetUnionPartByTableCount(objItems, objItemParts);
        strJson = CreateToJson(dt, CountNum);
        return strJson;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        
    }
}