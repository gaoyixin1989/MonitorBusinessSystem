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
using i3.ValueObject.Channels.OA.SUPPLIER;
using i3.BusinessLogic.Channels.OA.SUPPLIER;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：服务商管理
/// 创建日期：2012-11-07
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_OA_Supplier_SupplierList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetSupplier")
        {
            GetSupplier();
            Response.End();
        }
        if (Request.Params["Action"] == "GetJudge")
        {
            GetJudge();
            Response.End();
        }
    }

    //获取服务商
    private void GetSupplier()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhSUPPLIER_NAME = (Request.Params["srhSUPPLIER_NAME"] != null) ? Request.Params["srhSUPPLIER_NAME"] : "";
        string strSrhSUPPLIER_TYPE = (Request.Params["srhSUPPLIER_TYPE"] != null) ? Request.Params["srhSUPPLIER_TYPE"] : "";

        if (strSortname == null || strSortname.Length == 0)
            strSortname = TOaSupplierInfoVo.ID_FIELD;

        TOaSupplierInfoVo objSupplier = new TOaSupplierInfoVo();

        objSupplier.SUPPLIER_NAME = strSrhSUPPLIER_NAME;
        objSupplier.SUPPLIER_TYPE = strSrhSUPPLIER_TYPE;

        objSupplier.SORT_FIELD = strSortname;
        objSupplier.SORT_TYPE = strSortorder;
        TOaSupplierInfoLogic logicSupplier = new TOaSupplierInfoLogic();

        int intTotalCount = logicSupplier.GetSelectResultCount(objSupplier); ;//总计的数据条数
        DataTable dt = logicSupplier.SelectByTable(objSupplier, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    //获取指定外包单位的资质差新信息
    private void GetJudge()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelSuppierID = (Request.Params["selSuppierID"] != null) ? Request.Params["selSuppierID"] : "";
        if (strSelSuppierID.Length <= 0)
        {
            Response.Write("");
            return;
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TOaSupplierJudgeVo.ID_FIELD;

        TOaSupplierJudgeVo objJudge = new TOaSupplierJudgeVo();
        objJudge.SUPPLIER_ID = strSelSuppierID;
        objJudge.SORT_FIELD = strSortname;
        objJudge.SORT_TYPE = strSortorder;
        TOaSupplierJudgeLogic logicJudge = new TOaSupplierJudgeLogic();

        int intTotalCount = logicJudge.GetSelectResultCount(objJudge); ;//总计的数据条数
        DataTable dt = logicJudge.SelectByTable_ByJoin(objJudge, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 删除服务商
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteSupplier(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            isSuccess = new TOaSupplierInfoLogic().Delete(arrDelIDs[i]);
            if (isSuccess)
                new PageBase().WriteLog("删除服务商", "", new UserLogInfo().UserInfo.USER_NAME + "删除服务商" + strDelIDs);
            TOaSupplierJudgeVo objJudge = new TOaSupplierJudgeVo();
            objJudge.SUPPLIER_ID = arrDelIDs[i];
            if (new TOaSupplierJudgeLogic().Delete(objJudge))
                new PageBase().WriteLog("删除服务商产品评价", "", new UserLogInfo().UserInfo.USER_NAME + "删除服务商产品评价" + objJudge.ID);
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
    /// 添加服务商
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string AddSupplier(string strSUPPLIER_NAME, string strSUPPLIER_TYPE, string strPRODUCTS, string strLINK_MAN, string strTEL, string strFAX, string strEMAIL,
        string strPOST_CODE, string strADDRESS, string strBANK, string strACCOUNT_FOR)
    {
        bool isSuccess = true;

        TOaSupplierInfoVo objSupplier = new TOaSupplierInfoVo();
        objSupplier.ID = GetSerialNumber("t_oa_supplier_info_id");
        objSupplier.SUPPLIER_NAME = strSUPPLIER_NAME;
        objSupplier.SUPPLIER_TYPE = strSUPPLIER_TYPE;
        objSupplier.PRODUCTS = strPRODUCTS;
        objSupplier.LINK_MAN = strLINK_MAN;
        objSupplier.TEL = strTEL;
        objSupplier.FAX = strFAX;
        objSupplier.EMAIL = strEMAIL;
        objSupplier.POST_CODE = strPOST_CODE;
        objSupplier.ADDRESS = strADDRESS;
        objSupplier.BANK = strBANK;
        objSupplier.ACCOUNT_FOR = strACCOUNT_FOR;

        isSuccess = new TOaSupplierInfoLogic().Create(objSupplier);

        if (isSuccess)
        {
            new PageBase().WriteLog("添加服务商", "", new UserLogInfo().UserInfo.USER_NAME + "添加服务商" + objSupplier.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑服务商
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string EditSupplier(string strID, string strSUPPLIER_NAME, string strSUPPLIER_TYPE, string strPRODUCTS, string strLINK_MAN, string strTEL, string strFAX, string strEMAIL,
        string strPOST_CODE, string strADDRESS, string strBANK, string strACCOUNT_FOR)
    {
        bool isSuccess = true;

        TOaSupplierInfoVo objSupplier = new TOaSupplierInfoVo();
        objSupplier.ID = strID;
        objSupplier.SUPPLIER_NAME = strSUPPLIER_NAME;
        objSupplier.SUPPLIER_TYPE = strSUPPLIER_TYPE;
        objSupplier.PRODUCTS = strPRODUCTS;
        objSupplier.LINK_MAN = strLINK_MAN;
        objSupplier.TEL = strTEL;
        objSupplier.FAX = strFAX;
        objSupplier.EMAIL = strEMAIL;
        objSupplier.POST_CODE = strPOST_CODE;
        objSupplier.ADDRESS = strADDRESS;
        objSupplier.BANK = strBANK;
        objSupplier.ACCOUNT_FOR = strACCOUNT_FOR;

        isSuccess = new TOaSupplierInfoLogic().Edit(objSupplier);

        if (isSuccess)
        {
            new PageBase().WriteLog("编辑服务商", "", new UserLogInfo().UserInfo.USER_NAME + "编辑服务商" + objSupplier.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 删除服务商的评价信息
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string delJudge(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            isSuccess = new TOaSupplierJudgeLogic().Delete(arrDelIDs[i]);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除服务商的评价信息", "", new UserLogInfo().UserInfo.USER_NAME + "删除服务商的评价信息" + strDelIDs);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加服务商的评价信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string AddJudge(string strSUPPLIER_ID, string strPARTNAME, string strMODEL, string strREFERENCEPRICE, string strPRODUCTSTANDARD, string strDELIVERYPERIOD,
        string strQUANTITY, string strENTERPRISECODE, string strQUATITYSYSTEM, string strSINCERITY)
    {
        bool isSuccess = true;

        TOaSupplierJudgeVo objJudge = new TOaSupplierJudgeVo();
        objJudge.ID = GetSerialNumber("t_oa_supplier_judge_id");
        objJudge.SUPPLIER_ID = strSUPPLIER_ID;
        objJudge.PARTNAME = strPARTNAME;
        objJudge.MODEL = strMODEL;
        objJudge.REFERENCEPRICE = strREFERENCEPRICE;
        objJudge.PRODUCTSTANDARD = strPRODUCTSTANDARD;
        objJudge.DELIVERYPERIOD = strDELIVERYPERIOD;
        objJudge.QUANTITY = strQUANTITY;
        objJudge.ENTERPRISECODE = strENTERPRISECODE;
        objJudge.QUATITYSYSTEM = strQUATITYSYSTEM;
        objJudge.SINCERITY = strSINCERITY;

        isSuccess = new TOaSupplierJudgeLogic().Create(objJudge);

        if (isSuccess)
        {
            new PageBase().WriteLog("添加服务商的评价信息", "", new UserLogInfo().UserInfo.USER_NAME + "添加服务商的评价信息" + objJudge.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑服务商的评价信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string EditJudge(string strID, string strPARTNAME, string strMODEL, string strREFERENCEPRICE, string strPRODUCTSTANDARD, string strDELIVERYPERIOD,
        string strQUANTITY, string strENTERPRISECODE, string strQUATITYSYSTEM, string strSINCERITY)
    {
        bool isSuccess = true;

        TOaSupplierJudgeVo objJudge = new TOaSupplierJudgeVo();
        objJudge.ID = strID;
        objJudge.PARTNAME = strPARTNAME;
        objJudge.MODEL = strMODEL;
        objJudge.REFERENCEPRICE = strREFERENCEPRICE;
        objJudge.PRODUCTSTANDARD = strPRODUCTSTANDARD;
        objJudge.DELIVERYPERIOD = strDELIVERYPERIOD;
        objJudge.QUANTITY = strQUANTITY;
        objJudge.ENTERPRISECODE = strENTERPRISECODE;
        objJudge.QUATITYSYSTEM = strQUATITYSYSTEM;
        objJudge.SINCERITY = strSINCERITY;

        isSuccess = new TOaSupplierJudgeLogic().Edit(objJudge);

        if (isSuccess)
        {
            new PageBase().WriteLog("编辑服务商的评价信息", "", new UserLogInfo().UserInfo.USER_NAME + "编辑服务商的评价信息" + objJudge.ID);
            return "1";
        }
        else
        {
            return "0";
        }
    }

}

