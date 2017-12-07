using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using System.Data;
using i3.BusinessLogic.Channels.OA.PART.SAMPLE;
using System.Web.Services;

/// <summary>
/// 标准样品管理 
/// 创建人：魏林  2013-09-13
/// </summary>
public partial class Channels_OA_Part_Sample_PartSampleList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string json = string.Empty;
        if (!IsPostBack)
        {
            if (Request["type"] != null)
            {
                switch (Request["type"].ToString())
                {
                    case "getOneGridInfo":
                        json = getOneGridInfo();
                        break;
                    case "getTwoGridInfo":
                        json = getTwoGridInfo();
                        break;
                    case "SaveCollarDate":
                        json = SaveCollarDate();
                        break;
                }

                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
            }

        }
    }

    private string getOneGridInfo()
    {
        string strJson = "";

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaPartstandInfoVo PartstandInfoVo = new TOaPartstandInfoVo();
        PartstandInfoVo.SORT_FIELD = strSortname;
        PartstandInfoVo.SORT_TYPE = strSortorder;

        if (Request["SAMPLE_CODE"] != null)
            PartstandInfoVo.SAMPLE_CODE = Request["SAMPLE_CODE"].ToString();
        if (Request["SAMPLE_NAME"] != null)
            PartstandInfoVo.SAMPLE_NAME = Request["SAMPLE_NAME"].ToString();
        if (Request["SAMPLE_TYPE"] != null)
            PartstandInfoVo.SAMPLE_TYPE = Request["SAMPLE_TYPE"].ToString();
        if (Request["CLASS_TYPE"] != null)
            PartstandInfoVo.CLASS_TYPE = Request["CLASS_TYPE"].ToString();

        DataTable dt = new DataTable();
        int intTotalCount = 0;

        dt = new TOaPartstandInfoLogic().SelectByTableNew(PartstandInfoVo, intPageIndex, intPageSize, false);
        intTotalCount = new TOaPartstandInfoLogic().GetSelectResultCount(PartstandInfoVo);

        strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    private string getTwoGridInfo()
    {
        string strJson = "";

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaPartstandInfoVo PartstandInfoVo = new TOaPartstandInfoVo();

        PartstandInfoVo.SORT_FIELD = strSortname;
        PartstandInfoVo.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;

        dt = new TOaPartstandInfoLogic().SelectByTableNew(PartstandInfoVo, intPageIndex, intPageSize, true);
        intTotalCount = new TOaPartstandInfoLogic().GetSelectResultCount(PartstandInfoVo);

        strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 增加领用信息
    /// </summary>
    /// <returns></returns>
    private string SaveCollarDate()
    {
        string SampleID = Request["strPartId"].ToString();
        string NeedQuatity = Request["strNeedQuatity"].ToString() == "" ? "0" : Request["strNeedQuatity"].ToString();
        string UserID = Request["strUserID"].ToString();
        string Reason = Request["strRemark"].ToString();

        TOaPartstandCollarVo PartstandCollarVo = new TOaPartstandCollarVo();
        PartstandCollarVo.ID = GetSerialNumber("t_oa_partstand_collar_id");
        PartstandCollarVo.SAMPLE_ID = SampleID;
        PartstandCollarVo.LASTIN_DATE = DateTime.Now.ToString();
        PartstandCollarVo.REASON = Reason;
        PartstandCollarVo.USED_QUANTITY = NeedQuatity;
        PartstandCollarVo.USER_ID = UserID;

        if (new TOaPartstandCollarLogic().Create(PartstandCollarVo, true))
            return PartstandCollarVo.ID;
        else
            return "";
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }
}