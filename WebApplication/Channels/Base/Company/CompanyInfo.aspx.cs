using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;


/// <summary>
/// 功能描述：企业信息管理
/// 创建日期：2012-11-05
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Company_CompanyInfo : PageBase
{
    public string srhCompayName = "", srh_Area = "", srh_Industry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取企业信息
        if (Request["type"] != null && Request["type"].ToString() == "getCompanyInfo")
        {
            if (!String.IsNullOrEmpty(Request.Params["srhCompayName"]))
            {
                srhCompayName = Request.Params["srhCompayName"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_Area"]))
            {
                srh_Area = Request.Params["srh_Area"];
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_Industry"]))
            {
                srh_Industry = Request.Params["srh_Industry"];
            }

            strResult = getCompanyInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取企业信息
    /// </summary>
    /// <returns></returns>
    private string getCompanyInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.IS_DEL = "0";
        TBaseCompanyInfoVo.SORT_FIELD = strSortname;
        TBaseCompanyInfoVo.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        //自定义查询使用
        if (!String.IsNullOrEmpty(srhCompayName) || !String.IsNullOrEmpty(srh_Area) || !String.IsNullOrEmpty(srh_Industry))
        {
            TBaseCompanyInfoVo.COMPANY_NAME = srhCompayName;
            TBaseCompanyInfoVo.AREA = srh_Area;
            TBaseCompanyInfoVo.INDUSTRY = srh_Industry;
            intTotalCount = new TBaseCompanyInfoLogic().GetSelecDefinedtResultCount(TBaseCompanyInfoVo);
            dt = new TBaseCompanyInfoLogic().SelectDefinedTadble(TBaseCompanyInfoVo, intPageIndex, intPageSize);
        }
        else
        {
            dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseCompanyInfoLogic().GetSelectResultCount(TBaseCompanyInfoVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除企业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteCompanyInfo(string strValue)
    {
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.ID = strValue;
        TBaseCompanyInfoVo.IS_DEL = "1";
        bool isSuccess = new TBaseCompanyInfoLogic().Edit(TBaseCompanyInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除企业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除企业信息" + TBaseCompanyInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取行业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getIndustryCode(string strValue)
    {
        return new i3.BusinessLogic.Channels.Base.Industry.TBaseIndustryInfoLogic().Details(strValue).INDUSTRY_NAME;
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