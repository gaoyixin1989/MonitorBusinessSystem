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
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：监测费用设置
/// 创建日期：2012-11-16
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_TestPrice_ItemPrice : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItem")
        {
            GetItem();
        }
    }

    //获取监测项目
    private void GetItem()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhMONITOR_ID = (Request.Params["SrhMONITOR_ID"] != null) ? Request.Params["SrhMONITOR_ID"] : "";
        string strSrhITEM_NAME = (Request.Params["SrhITEM_NAME"] != null) ? Request.Params["SrhITEM_NAME"] : "";

        if (strSortname == null || strSortname.Length == 0)
            strSortname = TBaseItemInfoVo.ORDER_NUM_FIELD;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "1";

        objItem.MONITOR_ID = strSrhMONITOR_ID;
        objItem.ITEM_NAME = strSrhITEM_NAME;

        objItem.SORT_FIELD = strSortname;
        objItem.SORT_TYPE = strSortorder;
        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount(objItem); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ByJoinMonitorType(objItem, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 编辑项目数据
    /// </summary>
    /// <param name="strID">项目id</param>
    /// <param name="strITEM_NAME">监测项目名称</param>
    /// <param name="strLAB_CERTIFICATE">实验室认可</param>
    /// <param name="strMEASURE_CERTIFICATE">计量认可</param>
    /// <param name="strORDER_NUM">序号</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditItem(string strItemID, string strCharge, string strPowerFee,string strPreFree,string strTestAnsyFree,string strTestPointNum,string strAnsyNum)
    {
        bool isSuccess = true;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.ID = strItemID;
        objItem.CHARGE = strCharge;
        objItem.TEST_POWER_FEE = strPowerFee;
        objItem.PRETREATMENT_FEE = strPreFree;
        objItem.TEST_ANSY_FEE = strTestAnsyFree;
        objItem.TEST_POINT_NUM = strTestPointNum;
        objItem.ANALYSE_NUM = strAnsyNum;

        isSuccess = new TBaseItemInfoLogic().Edit(objItem);

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