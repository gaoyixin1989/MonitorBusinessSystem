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
/// 功能描述：项目包管理
/// 创建日期：2012-11-06
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_ItemBag : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItemBag")
        {
            GetItemBag();
            Response.End();
        }
        if (Request.Params["Action"] == "GetItems")
        {
            GetItems();
            Response.End();
        }
    }

    //获取项目包
    private void GetItemBag()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhMONITOR_ID = (Request.Params["SrhMONITOR_ID"] != null) ? Request.Params["SrhMONITOR_ID"] : "";
        string strSrhBag_NAME = (Request.Params["SrhBag_NAME"] != null) ? Request.Params["SrhBag_NAME"] : "";

        if (strSortname == null || strSortname.Length == 0)
            strSortname = TBaseItemInfoVo.ORDER_NUM_FIELD;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "1";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "0";

        objItem.MONITOR_ID = strSrhMONITOR_ID;
        objItem.ITEM_NAME = strSrhBag_NAME;

        objItem.SORT_FIELD = strSortname;
        objItem.SORT_TYPE = strSortorder;
        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount(objItem); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ByJoinMonitorType(objItem, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    //获取指定项目包的监测项目，即子项目
    private void GetItems()
    {
        string strSelItemBagID = (Request.Params["selBagID"] != null) ? Request.Params["selBagID"] : "";
        if (strSelItemBagID.Length <= 0)
        {
            Response.Write("");
            return;
        }

        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount_ItemOfBag(strSelItemBagID); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ItemOfBag(strSelItemBagID, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 删除项目包数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteItemBag(string strDelIDs)
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

            TBaseItemSubItemVo objSubItemSet = new TBaseItemSubItemVo();
            objSubItemSet.IS_DEL = "1";
            TBaseItemSubItemVo objSubItemWhere = new TBaseItemSubItemVo();
            objSubItemWhere.PARENT_ITEM_ID = arrDelIDs[i];
            new TBaseItemSubItemLogic().Edit(objSubItemSet, objSubItemWhere);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除项目包", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除项目包" + arrDelIDs[0].ToString() + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加项目包数据
    /// </summary>
    /// <param name="strMONITOR_ID">监测类别id</param>
    /// <param name="strITEM_NAME">监测项目名称</param>
    /// <param name="strLAB_CERTIFICATE">实验室认可</param>
    /// <param name="strMEASURE_CERTIFICATE">计量认可</param>
    /// <param name="strORDER_NUM">序号</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddItemBag(string strMONITOR_ID, string strITEM_NAME)
    {
        bool isSuccess = true;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.ID = GetSerialNumber("t_base_item_info_id");
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "1";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "0";
        objItem.MONITOR_ID = strMONITOR_ID;
        objItem.ITEM_NAME = strITEM_NAME;

        isSuccess = new TBaseItemInfoLogic().Create(objItem);

        if (isSuccess)
        {
            new PageBase().WriteLog("新增项目包", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增项目包" + objItem.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑项目包数据
    /// </summary>
    /// <param name="strID">项目id</param>
    /// <param name="strITEM_NAME">监测项目名称</param>
    /// <param name="strLAB_CERTIFICATE">实验室认可</param>
    /// <param name="strMEASURE_CERTIFICATE">计量认可</param>
    /// <param name="strORDER_NUM">序号</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditItemBag(string strID, string strITEM_NAME)
    {
        bool isSuccess = true;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.ID = strID;
        objItem.ITEM_NAME = strITEM_NAME;

        isSuccess = new TBaseItemInfoLogic().Edit(objItem);

        if (isSuccess)
        {
            new PageBase().WriteLog("修改项目包", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改项目包" + objItem.ID + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 设置项目包的监测项目数据
    /// </summary>
    /// <param name="strID">id</param>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveDataItem(string strBagID, string strSelItem_IDs)
    {
        bool isSuccess = true;

        string[] arrSelItemId = strSelItem_IDs.Split(',');

        TBaseItemSubItemVo objSubItemSet = new TBaseItemSubItemVo();
        objSubItemSet.IS_DEL = "1";
        TBaseItemSubItemVo objSubItemWhere = new TBaseItemSubItemVo();
        objSubItemWhere.IS_DEL = "0";
        objSubItemWhere.PARENT_ITEM_ID = strBagID;
        if (new TBaseItemSubItemLogic().Edit(objSubItemSet, objSubItemWhere))
        {
            new PageBase().WriteLog("删除项目包中监测项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除项目包父项目" + strBagID + "成功");
        }

        for (int i = 0; i < arrSelItemId.Length; i++)
        {
            TBaseItemSubItemVo objSubItem = new TBaseItemSubItemVo();
            objSubItem.ID = GetSerialNumber("t_base_item_sub_item_id");
            objSubItem.IS_DEL = "0";
            objSubItem.ITEM_ID = arrSelItemId[i];
            objSubItem.PARENT_ITEM_ID = strBagID;

            isSuccess = new TBaseItemSubItemLogic().Create(objSubItem);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("新增项目包监测项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增项目包监测项目" + arrSelItemId[0].ToString() + "成功");
            return "1";
        }
        else
        {
            return "0";
        }
    }
}