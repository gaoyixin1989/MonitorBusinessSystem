using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;

/// <summary>
/// 功能描述：附加费用设置
/// 创建日期：2012-11-16
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_TestPrice_AttachPrice : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getFee")
        {
            strResult = getFee();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取信息
    private string getFee()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TMisContractAttfeeitemVo.ID_FIELD;

        TMisContractAttfeeitemVo objFee = new TMisContractAttfeeitemVo();
        objFee.IS_DEL = "0";
        objFee.SORT_FIELD = strSortname;
        objFee.SORT_TYPE = strSortorder;
        DataTable dt = new TMisContractAttfeeitemLogic().SelectByTable(objFee, intPageIndex, intPageSize);
        int intTotalCount = new TMisContractAttfeeitemLogic().GetSelectResultCount(objFee);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除信息
    [WebMethod]
    public static string deleteData(string strValue)
    {
        TMisContractAttfeeitemVo objFee = new TMisContractAttfeeitemVo();
        objFee.ID = strValue;
        objFee.IS_DEL = "1";
        bool isSuccess = new TMisContractAttfeeitemLogic().Edit(objFee);
        return isSuccess == true ? "1" : "0";
    }

    //编辑数据
    [WebMethod]
    public static string SaveData(string strID, string strAttFeeItem, string strPrice, string strInfo)
    {
        bool isSuccess = true;

        TMisContractAttfeeitemVo objFee = new TMisContractAttfeeitemVo();
        objFee.ID = strID.Length > 0 ? strID : GetSerialNumber("t_mis_contract_attfeeitem_id");
        objFee.IS_DEL = "0";
        objFee.ATT_FEE_ITEM = strAttFeeItem;
        objFee.PRICE = strPrice;
        objFee.INFO = strInfo;

        if (strID.Length > 0)
            isSuccess = new TMisContractAttfeeitemLogic().Edit(objFee);
        else
            isSuccess = new TMisContractAttfeeitemLogic().Create(objFee);

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}