using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.Outcompany;
using i3.BusinessLogic.Channels.Base.Outcompany;

/// <summary>
/// 功能描述：外包单位管理
/// 创建日期：2012-11-07
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_OutCompany_OutCompanyList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetOutCompany")
        {
            GetOutCompany();
            Response.End();
        }
        if (Request.Params["Action"] == "GetAllow")
        {
            GetAllow();
            Response.End();
        }
    }

    //获取外包单位
    private void GetOutCompany()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhSrhCOMPANY_NAME = (Request.Params["SrhCOMPANY_NAME"] != null) ? Request.Params["SrhCOMPANY_NAME"] : "";
        string strSrhCOMPANY_CODE = (Request.Params["SrhCOMPANY_CODE"] != null) ? Request.Params["SrhCOMPANY_CODE"] : "";

        if (strSortname == null || strSortname.Length == 0)
            strSortname = TBaseOutcompanyInfoVo.ID_FIELD;

        TBaseOutcompanyInfoVo objOutCompany = new TBaseOutcompanyInfoVo();
        objOutCompany.IS_DEL = "0";

        objOutCompany.COMPANY_NAME = strSrhSrhCOMPANY_NAME;
        objOutCompany.COMPANY_CODE = strSrhCOMPANY_CODE;

        objOutCompany.SORT_FIELD = strSortname;
        objOutCompany.SORT_TYPE = strSortorder;
        TBaseOutcompanyInfoLogic logicOutCompany = new TBaseOutcompanyInfoLogic();

        int intTotalCount = logicOutCompany.GetSelectResultCount(objOutCompany); ;//总计的数据条数
        DataTable dt = logicOutCompany.SelectByTable(objOutCompany, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    //获取指定外包单位的资质差新信息
    private void GetAllow()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelCompanyID = (Request.Params["selCompanyID"] != null) ? Request.Params["selCompanyID"] : "";
        if (strSelCompanyID.Length <= 0)
        {
            Response.Write("");
            return;
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseOutcompanyAllowVo.ID_FIELD;

        TBaseOutcompanyAllowVo objAllow = new TBaseOutcompanyAllowVo();
        objAllow.OUTCOMPANY_ID = strSelCompanyID;
        objAllow.SORT_FIELD = strSortname;
        objAllow.SORT_TYPE = strSortorder;
        TBaseOutcompanyAllowLogic logicAllow = new TBaseOutcompanyAllowLogic();

        int intTotalCount = logicAllow.GetSelectResultCount(objAllow); ;//总计的数据条数
        DataTable dt = logicAllow.SelectByTable_ByJoin(objAllow, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 删除外包单位
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteOutCompany(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            TBaseOutcompanyInfoVo objOutCompany = new TBaseOutcompanyInfoVo();
            objOutCompany.ID = arrDelIDs[i];
            objOutCompany.IS_DEL = "1";
            isSuccess = new TBaseOutcompanyInfoLogic().Edit(objOutCompany);
            if (isSuccess)
            {
                new PageBase().WriteLog("删除外包单位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除外包单位" + objOutCompany.ID + "成功");
            }
            TBaseOutcompanyAllowVo objAllowWhere = new TBaseOutcompanyAllowVo();
            objAllowWhere.OUTCOMPANY_ID = arrDelIDs[i];
            if (new TBaseOutcompanyAllowLogic().Delete(objAllowWhere))
            {
                new PageBase().WriteLog("删除外包单位资质", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除外包单位" + objAllowWhere.OUTCOMPANY_ID + "的资质成功");
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加外包单位
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string AddOutCompany(string strCOMPANY_NAME, string strCOMPANY_CODE, string strLINK_MAN, string strPHONE, string strPOST, string strADDRESS, string strAPTITUDE)
    {
        bool isSuccess = true;

        TBaseOutcompanyInfoVo objOutCompany = new TBaseOutcompanyInfoVo();
        objOutCompany.ID = GetSerialNumber("t_base_out_company_info_id");
        objOutCompany.IS_DEL = "0";
        objOutCompany.COMPANY_NAME = strCOMPANY_NAME;
        objOutCompany.COMPANY_CODE = strCOMPANY_CODE;
        objOutCompany.LINK_MAN = strLINK_MAN;
        objOutCompany.PHONE = strPHONE;
        objOutCompany.POST = strPOST;
        objOutCompany.ADDRESS = strADDRESS;
        objOutCompany.APTITUDE = strAPTITUDE;

        isSuccess = new TBaseOutcompanyInfoLogic().Create(objOutCompany);

        if (isSuccess)
        {
            new PageBase().WriteLog("新增外包单位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增外包单位" + objOutCompany.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑外包单位
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string EditOutCompany(string strID, string strCOMPANY_NAME, string strCOMPANY_CODE, string strLINK_MAN, string strPHONE, string strPOST, string strADDRESS, string strAPTITUDE)
    {
        bool isSuccess = true;

        TBaseOutcompanyInfoVo objOutCompany = new TBaseOutcompanyInfoVo();
        objOutCompany.ID = strID;
        objOutCompany.IS_DEL = "0";
        objOutCompany.COMPANY_NAME = strCOMPANY_NAME;
        objOutCompany.COMPANY_CODE = strCOMPANY_CODE;
        objOutCompany.LINK_MAN = strLINK_MAN;
        objOutCompany.PHONE = strPHONE;
        objOutCompany.POST = strPOST;
        objOutCompany.ADDRESS = strADDRESS;
        objOutCompany.APTITUDE = strAPTITUDE;

        isSuccess = new TBaseOutcompanyInfoLogic().Edit(objOutCompany);

        if (isSuccess)
        {
            new PageBase().WriteLog("修改外包单位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改外包单位" + objOutCompany.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 删除外包单位的资质查新信息
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string delAllow(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            isSuccess = new TBaseOutcompanyAllowLogic().Delete(arrDelIDs[i]);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除外包单位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除外包单位" + strDelIDs[0].ToString() + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加外包单位的资质查新信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string AddAllow(string strOutCompany_ID, string strQUALIFICATIONS_INFO, string strPROJECT_INFO, string strQC_INFO, string strCOMPLETE_INFO, string strCHECK_USER_ID,
        string strCHECK_DATE, string strIS_OK, string strAPP_INFO, string strAPP_USER_ID, string strAPP_DATE)
    {
        bool isSuccess = true;

        TBaseOutcompanyAllowVo objAllow = new TBaseOutcompanyAllowVo();
        objAllow.ID = GetSerialNumber("t_base_out_company_allow_id");
        objAllow.OUTCOMPANY_ID = strOutCompany_ID;
        objAllow.QUALIFICATIONS_INFO = strQUALIFICATIONS_INFO;
        objAllow.PROJECT_INFO = strPROJECT_INFO;
        objAllow.QC_INFO = strQC_INFO;
        objAllow.COMPLETE_INFO = strCOMPLETE_INFO;
        objAllow.CHECK_USER_ID = strCHECK_USER_ID;
        objAllow.CHECK_DATE = strCHECK_DATE;
        objAllow.IS_OK = strIS_OK;
        objAllow.APP_INFO = strAPP_INFO;
        objAllow.APP_USER_ID = strAPP_USER_ID;
        objAllow.APP_DATE = strAPP_DATE;

        isSuccess = new TBaseOutcompanyAllowLogic().Create(objAllow);

        if (isSuccess)
        {
            new PageBase().WriteLog("新增外包单位的资质查新信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增外包单位的资质查新信息" + objAllow.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑外包单位的资质查新信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string EditAllow(string strID, string strQUALIFICATIONS_INFO, string strPROJECT_INFO, string strQC_INFO, string strCOMPLETE_INFO, string strCHECK_USER_ID,
        string strCHECK_DATE, string strIS_OK, string strAPP_INFO, string strAPP_USER_ID, string strAPP_DATE)
    {
        bool isSuccess = true;

        TBaseOutcompanyAllowVo objAllow = new TBaseOutcompanyAllowVo();
        objAllow.ID = strID;
        objAllow.QUALIFICATIONS_INFO = strQUALIFICATIONS_INFO;
        objAllow.PROJECT_INFO = strPROJECT_INFO;
        objAllow.QC_INFO = strQC_INFO;
        objAllow.COMPLETE_INFO = strCOMPLETE_INFO;
        objAllow.CHECK_USER_ID = strCHECK_USER_ID;
        objAllow.CHECK_DATE = strCHECK_DATE;
        objAllow.IS_OK = strIS_OK;
        objAllow.APP_INFO = strAPP_INFO;
        objAllow.APP_USER_ID = strAPP_USER_ID;
        objAllow.APP_DATE = strAPP_DATE;

        isSuccess = new TBaseOutcompanyAllowLogic().Edit(objAllow);

        if (isSuccess)
        {
            new PageBase().WriteLog("编辑外包单位的资质查新信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑外包单位的资质查新信息" + objAllow.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }
}