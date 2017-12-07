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
/// 功能描述：项目管理
/// 创建日期：2012-11-02
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_ItemList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItems")
        {
            GetItems();
            Response.End();
        }
        if (Request.Params["Action"] == "GetMethods")
        {
            GetMethods();
            Response.End();
        }
        if (Request.Params["Action"] == "GetItemSamplingInstrumentInfo")
        {
            GetItemSamplingInstrumentInfo();
            Response.End();
        }
        if (Request.Params["Action"] == "GetDefineTable"&!String.IsNullOrEmpty(Request.Params["strWhere"]))
        {
            Response.Write( GetDefineTable(Request.Params["strWhere"].ToString()));
            Response.End();
        }
    }

    //获取监测项目
    private void GetItems()
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

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD] = (dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString().Length > 0) ? (int.Parse(dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString()).ToString()) : "";
        }


        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    //获取指定监测项目的分析方法
    private void GetMethods()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelItemID = (Request.Params["selItemID"] != null) ? Request.Params["selItemID"] : "";
        if (strSelItemID.Length <= 0)
        {
            Response.Write("");
            return;
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseItemAnalysisVo.ID_FIELD;

        TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
        objItemAnalysis.IS_DEL = "0";
        objItemAnalysis.ITEM_ID = strSelItemID;
        objItemAnalysis.SORT_FIELD = strSortname;
        objItemAnalysis.SORT_TYPE = strSortorder;
        TBaseItemAnalysisLogic logicItemAnalysis = new TBaseItemAnalysisLogic();

        int intTotalCount = logicItemAnalysis.GetSelectResultCount(objItemAnalysis); ;//总计的数据条数
        DataTable dt = logicItemAnalysis.SelectByTable_ByJoin(objItemAnalysis, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }
    /// <summary>
    /// 获取现场采样仪器的信息
    /// </summary>
    public void GetItemSamplingInstrumentInfo()
    {
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVo = new TBaseItemSamplingInstrumentVo();
        TBaseItemSamplingInstrumentVo.ITEM_ID = Request["ITEM_ID"].ToString();
        TBaseItemSamplingInstrumentVo.IS_DEL = "0";
        DataTable objTable = new TBaseItemSamplingInstrumentLogic().SelectByTable(TBaseItemSamplingInstrumentVo);
        string strJson = CreateToJson(objTable, objTable.Rows.Count);
        Response.Write(strJson);
    }
    /// <summary>
    /// 删除项目数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteItem(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            TBaseItemInfoVo objItem = new TBaseItemInfoVo();
            objItem.ID = arrDelIDs[i];
            objItem.IS_DEL = "1";
            isSuccess = new TBaseItemInfoLogic().Edit(objItem);

            TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
            objItemAnalysisSet.IS_DEL = "1";
            TBaseItemAnalysisVo objItemAnalysisWhere = new TBaseItemAnalysisVo();
            objItemAnalysisWhere.ITEM_ID = arrDelIDs[i];
            if (new TBaseItemAnalysisLogic().Edit(objItemAnalysisSet, objItemAnalysisWhere))
            {
                new PageBase().WriteLog("删除项目分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除项目ID" + objItemAnalysisWhere.ITEM_ID + "的分析方法成功");
            }
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除项目" + arrDelIDs[0].ToString() + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加项目数据
    /// </summary>
    /// <param name="strMONITOR_ID">监测类别id</param>
    /// <param name="strITEM_NAME">监测项目名称</param>
    /// <param name="strLAB_CERTIFICATE">实验室认可</param>
    /// <param name="strMEASURE_CERTIFICATE">计量认可</param>
    /// <param name="strORDER_NUM">序号</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddItem(string strMONITOR_ID, string strITEM_NAME, string strLAB_CERTIFICATE, string strMEASURE_CERTIFICATE, string strORDER_NUM, string strITEM_NUM, string strIS_SAMPLEDEPT, string strIS_ANYSCENE, string strORI_CATALOG_TABLEID)
    {
        bool isSuccess = true;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.ID = GetSerialNumber("t_base_item_info_id");
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "1";
        objItem.MONITOR_ID = strMONITOR_ID;
        objItem.ITEM_NAME = strITEM_NAME;
        objItem.LAB_CERTIFICATE = strLAB_CERTIFICATE;
        objItem.MEASURE_CERTIFICATE = strMEASURE_CERTIFICATE;
        objItem.ORDER_NUM = strORDER_NUM.PadLeft(8, '0');
        objItem.ITEM_NUM = strITEM_NUM;
        objItem.IS_SAMPLEDEPT = strIS_SAMPLEDEPT;
        objItem.IS_ANYSCENE_ITEM = strIS_ANYSCENE;
        objItem.ORI_CATALOG_TABLEID = strORI_CATALOG_TABLEID;
        isSuccess = new TBaseItemInfoLogic().Create(objItem);

        if (isSuccess)
        {
            new PageBase().WriteLog("新增项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增项目" + objItem.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
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
    public static string EditItem(string strID, string strITEM_NAME, string strLAB_CERTIFICATE, string strMEASURE_CERTIFICATE, string strORDER_NUM, string strITEM_NUM, string strIS_SAMPLEDEPT, string strIS_ANYSCENE,string strORI_CATALOG_TABLEID)
    {
        bool isSuccess = true;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.ID = strID;
        objItem.ITEM_NAME = strITEM_NAME;
        objItem.LAB_CERTIFICATE = strLAB_CERTIFICATE;
        objItem.MEASURE_CERTIFICATE = strMEASURE_CERTIFICATE;
        objItem.ORDER_NUM = strORDER_NUM.PadLeft(8, '0');
        objItem.ITEM_NUM = strITEM_NUM;
        objItem.IS_SAMPLEDEPT = strIS_SAMPLEDEPT;
        objItem.IS_ANYSCENE_ITEM = strIS_ANYSCENE;
        objItem.ORI_CATALOG_TABLEID = strORI_CATALOG_TABLEID;
        isSuccess = new TBaseItemInfoLogic().Edit(objItem);

        if (isSuccess)
        {
            new PageBase().WriteLog("修改项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改项目" + objItem.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 删除项目的分析方法数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string delMethod(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
            objItemAnalysisSet.ID = arrDelIDs[i];
            objItemAnalysisSet.IS_DEL = "1";
            isSuccess = new TBaseItemAnalysisLogic().Edit(objItemAnalysisSet);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除项目分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除项目分析方法" + strDelIDs[0].ToString() + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 设置项目的默认分析方法数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string defMethod(string strItemID, string strMethodID)
    {
        bool isSuccess = true;

        if (strItemID.Length == 0)
            return "0";

        if (strMethodID.Length == 0)
            return "0";

        TBaseItemAnalysisVo objItemAnalysisWhere = new TBaseItemAnalysisVo();
        objItemAnalysisWhere.ITEM_ID = strItemID;
        TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
        objItemAnalysisSet.IS_DEFAULT = "否";
        isSuccess = new TBaseItemAnalysisLogic().Edit(objItemAnalysisSet, objItemAnalysisWhere);

        TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
        objItemAnalysis.ID = strMethodID;
        objItemAnalysis.IS_DEFAULT = "是";
        isSuccess = new TBaseItemAnalysisLogic().Edit(objItemAnalysis);

        if (isSuccess)
        {
            new PageBase().WriteLog("设置项目的默认分析方法数据", "", new PageBase().LogInfo.UserInfo.USER_NAME + "设置项目的默认分析方法数据" + strMethodID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加分析方法数据
    /// </summary>
    /// <param name="strItemID">监测项目id</param>
    /// <param name="strANALYSISID">方法id</param>
    /// <param name="strAPPARATUS_ID">仪器id</param>
    /// <param name="strLOWER_CHECKOUT">最低检测限</param>
    /// <param name="strUnitCode">单位</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddMehod(string strItemID, string strANALYSISID, string strAPPARATUS_ID, string strLOWER_CHECKOUT, string strUnitCode, string strPRECISION)
    {
        bool isSuccess = true;

        TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
        objItemAnalysisSet.ID = GetSerialNumber("t_base_item_analysis_id");
        objItemAnalysisSet.IS_DEL = "0";
        objItemAnalysisSet.ITEM_ID = strItemID;
        objItemAnalysisSet.ANALYSIS_METHOD_ID = strANALYSISID;
        objItemAnalysisSet.INSTRUMENT_ID = strAPPARATUS_ID;
        objItemAnalysisSet.LOWER_CHECKOUT = strLOWER_CHECKOUT;
        objItemAnalysisSet.UNIT = strUnitCode;
        objItemAnalysisSet.IS_DEFAULT = "否";
        objItemAnalysisSet.PRECISION = strPRECISION;

        isSuccess = new TBaseItemAnalysisLogic().Create(objItemAnalysisSet);

        if (isSuccess)
        {
            new PageBase().WriteLog("添加分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "添加分析方法" + objItemAnalysisSet.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑分析方法数据
    /// </summary>
    /// <param name="strID">项目id</param>
    /// <param name="strITEM_NAME">监测项目名称</param>
    /// <param name="strLAB_CERTIFICATE">实验室认可</param>
    /// <param name="strMEASURE_CERTIFICATE">计量认可</param>
    /// <param name="strORDER_NUM">序号</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditMehod(string strID, string strANALYSISID, string strAPPARATUS_ID, string strLOWER_CHECKOUT, string strUnitCode, string strPRECISION)
    {
        bool isSuccess = true;

        TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
        objItemAnalysisSet.ID = strID;
        objItemAnalysisSet.ANALYSIS_METHOD_ID = strANALYSISID;
        objItemAnalysisSet.INSTRUMENT_ID = strAPPARATUS_ID;
        objItemAnalysisSet.LOWER_CHECKOUT = strLOWER_CHECKOUT;
        objItemAnalysisSet.UNIT = strUnitCode;
        objItemAnalysisSet.PRECISION = strPRECISION;

        isSuccess = new TBaseItemAnalysisLogic().Edit(objItemAnalysisSet);

        if (isSuccess)
        {
            new PageBase().WriteLog("编辑分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑分析方法" + objItemAnalysisSet.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    /// <summary>
    /// 删除现场采样仪器信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteSamplingInstrumentInfo(string strValue)
    {
        bool isSuccess = new TBaseItemSamplingInstrumentLogic().Delete(strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除现场采样仪器信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除现场采样仪器信息");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 设置默认采样仪器
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string setDefaultSamplingInstrumentInfo(string ITEM_ID, string strValue)
    {
        bool isSuccess = new TBaseItemInfoLogic().setItemSamplingInstrumentDefault(ITEM_ID, strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("设置默认采样仪器", "", new PageBase().LogInfo.UserInfo.USER_NAME + "设置默认采样仪器");
        }
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 创建原因：获取监测项目要用到的原始记录表的表名称和表ID
    /// 创建人：胡方扬
    /// 创意日期：2013-08-29
    /// </summary>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public string GetDefineTable(string strWhere) {
        string result = "";
        DataTable dt = new TBaseItemInfoLogic().GetSysDutyCataLogTableInfor(strWhere);
        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }
}