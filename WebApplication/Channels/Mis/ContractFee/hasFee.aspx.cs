using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：委托书缴费
/// 创建日期：2012-12-19
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Mis_ContractFee_hasFee : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取待缴费信息
        if (Request["type"] != null && Request["type"].ToString() == "GetFee")
        {
            GetFees();
        }
    }

    //获取已缴费信息
    private void GetFees()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TMisContractFeeVo.ID_FIELD;

        TMisContractFeeVo objFee = new TMisContractFeeVo();
        objFee.IF_PAY = "1";
        DataTable dt = new TMisContractFeeLogic().SelectByTable(objFee, intPageIndex, intPageSize);
        int intTotalCount = new TMisContractFeeLogic().GetSelectResultCount(objFee);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    //未缴费
    [WebMethod]
    public static string setNotPay(string strValue)
    {
        TMisContractFeeVo objFee = new TMisContractFeeVo();
        objFee.ID = strValue;
        objFee.IF_PAY = "0";
        bool isSuccess = new TMisContractFeeLogic().Edit(objFee);

        return isSuccess == true ? "1" : "0";
    }

    #region 列表获取信息
    /// <summary>
    /// 获取项目名称
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getProjectName(string strValue)
    {
        return new TMisContractLogic().Details(strValue).PROJECT_NAME;
    }

    /// <summary>
    /// 获取委托单号
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractCode(string strValue)
    {
        return new TMisContractLogic().Details(strValue).CONTRACT_CODE;
    }

    /// <summary>
    /// 获取委托年度
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractYear(string strValue)
    {
        return new TMisContractLogic().Details(strValue).CONTRACT_YEAR;
    }

    /// <summary>
    /// 获取委托单位
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strValue)
    {
        string strID = new TMisContractLogic().Details(strValue).CLIENT_COMPANY_ID;
        return new TMisContractCompanyLogic().Details(strID).COMPANY_NAME;
    }

    /// <summary>
    /// 获取委托类型
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strValue)
    {
        string strID = new TMisContractLogic().Details(strValue).CONTRACT_TYPE;
        return getDictName(strID, "CONTRACT_TYPE");
    }

    /// <summary>
    /// 获取监测类型
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getTestTypes(string strValue)
    {
        TMisContractPointVo objContractPoint = new TMisContractPointVo();
        objContractPoint.CONTRACT_ID = strValue;
        objContractPoint.IS_DEL = "0";
        DataTable dt = new TMisContractPointLogic().SelectByTable(objContractPoint, 0, 0);

        string strTypeIDs = "";
        string strTypeNames = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strTypeID = dt.Rows[i]["MONITOR_ID"].ToString();
            if (!strTypeIDs.Contains(strTypeID))
                strTypeIDs += (strTypeIDs.Length == 0 ? "" : ",") + strTypeID;
        }

        if (strTypeIDs.Length > 0)
        {
            string[] arrTypeID = strTypeIDs.Split(',');
            for (int i = 0; i < arrTypeID.Length; i++)
            {
                string strTypeName = new TBaseMonitorTypeInfoLogic().Details(arrTypeID[i]).MONITOR_TYPE_NAME;
                strTypeNames += (strTypeNames.Length > 0 ? "," : "") + strTypeName;
            }
        }

        return strTypeNames;
    }

    #endregion
}