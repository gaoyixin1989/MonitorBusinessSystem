using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using i3.View;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Data;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;

/// <summary>
/// 功能描述：综合查询点位信息
/// 创建时间:2013-1-3
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_TotalSearchForPoint : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        //委托项目
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetPointItem")
        {
            strResult = getContractItem(Request.QueryString["point_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        {
            //委托点位
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetContractPoint")
            {
                getContractPoint(Request.QueryString["id"]);
            }
        }
    }

    /// <summary>
    /// 获取监测类别
    /// </summary>
    /// <param name="strValue">监测类别ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strValue)
    {
        return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
    }
    /// <summary>
    /// 获得委托点位信息
    /// </summary>
    /// <param name="strContractID">委托书ID</param>
    /// <returns></returns>
    protected void getContractPoint(string strContractID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisContractPointVo objPoint = new TMisContractPointVo();
        TMisContractPointLogic objPointLogic = new TMisContractPointLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisContractPointVo.NUM_FIELD;

        objPoint.SORT_FIELD = strSortname;
        objPoint.SORT_TYPE = strSortorder;
        objPoint.CONTRACT_ID = strContractID;
        objPoint.IS_DEL = "0";

        int intTotalCount = objPointLogic.GetSelectResultCount(objPoint);//总计的数据条数
        DataTable dt = objPointLogic.SelectByTable(objPoint, intPageIdx, intPagesize);
        //插入委托书编号
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr["REMARK1"] = new TMisContractLogic().Details(strContractID).CONTRACT_CODE;
            }
        }

        string strJson = CreateToJson(dt, intTotalCount);
        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获得委托项目
    /// </summary>
    /// <param name="strPointID">委托点位</param>
    /// <returns></returns>
    protected string getContractItem(string strPointID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisContractPointitemVo objPointitem = new TMisContractPointitemVo();
        TMisContractPointitemLogic objPointitemLogic = new TMisContractPointitemLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisContractPointitemVo.ITEM_ID_FIELD;

        objPointitem.SORT_FIELD = strSortname;
        objPointitem.SORT_TYPE = strSortorder;
        objPointitem.CONTRACT_POINT_ID = strPointID;

        int intTotalCount = objPointitemLogic.GetSelectResultCount(objPointitem);//总计的数据条数
        DataTable dt = objPointitemLogic.SelectByTable(objPointitem, intPageIdx, intPagesize);

        return CreateToJson(dt, intTotalCount);
    }
    /// <summary>
    ///  获取监测项目名称
    /// </summary>
    /// <param name="strItemID">项目ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItmeName(string strValue)
    {
        return new TBaseItemInfoLogic().Details(new TBaseItemInfoVo() { ID = strValue, IS_DEL = "0" }).ITEM_NAME;
    }
}