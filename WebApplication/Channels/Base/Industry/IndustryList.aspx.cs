using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.Industry;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：行业信息管理
/// 创建日期：2012-11-21
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Industry_IndustryList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取点位信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            getData();
        }

        //获取指定点位的监测项目信息
        if (Request["type"] != null && Request["type"].ToString() == "GetSubData")
        {
            strResult = GetSubData();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取行业信息
    private void getData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseIndustryInfoVo.ID_FIELD;

        TBaseIndustryInfoLogic logicMain = new TBaseIndustryInfoLogic();
        TBaseIndustryInfoVo objMainVo = new TBaseIndustryInfoVo();
        objMainVo.IS_DEL = "0";
        objMainVo.SORT_FIELD = strSortname;
        objMainVo.SORT_TYPE = strSortorder;
        DataTable dt = logicMain.SelectByTable(objMainVo, intPageIndex, intPageSize);
        int intTotalCount = logicMain.GetSelectResultCount(objMainVo);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    //编辑点位数据
    [WebMethod]
    public static string SaveData(string strID, string strINDUSTRY_CODE, string strINDUSTRY_NAME)
    {
        bool isSuccess = true;
        string strSaveID = strID;
        if (strSaveID == "0")
            strSaveID = "";

        TBaseIndustryInfoVo objVo = new TBaseIndustryInfoVo();
        objVo.ID = strSaveID.Length > 0 ? strID : GetSerialNumber("t_base_industry_info_id");
        objVo.IS_DEL = "0";
        objVo.INDUSTRY_CODE = strINDUSTRY_CODE;
        objVo.INDUSTRY_NAME = strINDUSTRY_NAME;

        if (strSaveID.Length > 0)
        {
            isSuccess = new TBaseIndustryInfoLogic().Edit(objVo);
            if (isSuccess)
            {
                new PageBase().WriteLog("修改行业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改行业信息" + objVo.ID + "成功");
            }
        }
        else
        {
            isSuccess = new TBaseIndustryInfoLogic().Create(objVo);
            if (isSuccess)
            {
                new PageBase().WriteLog("新增行业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增行业信息" + objVo.ID + "成功");
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

    // 删除行业信息
    [WebMethod]
    public static string deleteData(string strValue)
    {
        TBaseIndustryInfoVo objMainVo = new TBaseIndustryInfoVo();
        objMainVo.ID = strValue;
        objMainVo.IS_DEL = "1";
        bool isSuccess = new TBaseIndustryInfoLogic().Edit(objMainVo);

        TBaseIndustryItemVo objSubVoWhere = new TBaseIndustryItemVo();
        objSubVoWhere.INDUSTRY_ID = strValue;
        if (new TBaseIndustryItemLogic().Delete(objSubVoWhere))
        {
            new PageBase().WriteLog("删除行业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除行业信息" + objSubVoWhere.ID + "成功");
        }

        return isSuccess == true ? "1" : "0";
    }

    // 切换显示设置
    [WebMethod]
    public static string isShowData(string strValue)
    {
        TBaseIndustryInfoVo objMainVo = new TBaseIndustryInfoLogic().Details(strValue);
        
        if (objMainVo.IS_SHOW=="1")
            objMainVo.IS_SHOW = "0";
        else
            objMainVo.IS_SHOW = "1";
        bool isSuccess = new TBaseIndustryInfoLogic().Edit(objMainVo);

        return isSuccess == true ? "1" : "0";
    }

    //获取指定行业的监测项目信息
    private string GetSubData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string selIndustryID = (Request.Params["selIndustryID"] != null) ? Request.Params["selIndustryID"] : "";
        if (selIndustryID.Length <= 0)
        {
            return "";
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseIndustryItemVo.ID_FIELD;

        TBaseIndustryItemVo objItem = new TBaseIndustryItemVo();
        objItem.INDUSTRY_ID = selIndustryID;
        objItem.SORT_FIELD = strSortname;
        objItem.SORT_TYPE = strSortorder;
        TBaseIndustryItemLogic logicItem = new TBaseIndustryItemLogic();

        int intTotalCount = logicItem.GetSelectResultCount(objItem); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable(objItem, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    //复制监测项目
    [WebMethod]
    public static string CopyItem(string strCopyID, string strPastID)
    {
        bool isSuccess = true;

        TBaseIndustryItemVo objItemCopy = new TBaseIndustryItemVo();
        objItemCopy.INDUSTRY_ID = strCopyID;
        DataTable dtCopy = new TBaseIndustryItemLogic().SelectByTable(objItemCopy);

        TBaseIndustryItemVo objItemPast = new TBaseIndustryItemVo();
        objItemPast.INDUSTRY_ID = strPastID;
        DataTable dtPast = new TBaseIndustryItemLogic().SelectByTable(objItemPast);

        string strIsExistItem = "";
        for (int i = 0; i < dtPast.Rows.Count; i++)
        {
            strIsExistItem += "," + dtPast.Rows[i]["ITEM_ID"].ToString();
        }
        strIsExistItem += strIsExistItem.Length > 0 ? "," : "";

        for (int i = 0; i < dtCopy.Rows.Count; i++)
        {
            DataRow dr = dtCopy.Rows[i];
            string strCopyItemID = dr["ITEM_ID"].ToString();
            if (!strIsExistItem.Contains(strCopyItemID))
            {
                TBaseIndustryItemVo objItem = new TBaseIndustryItemVo();
                objItem.ID = GetSerialNumber("t_base_industry_item_id");
                objItem.INDUSTRY_ID = strPastID;
                objItem.ITEM_ID = strCopyItemID;

                isSuccess = new TBaseIndustryItemLogic().Create(objItem);
                if (isSuccess)
                {
                    new PageBase().WriteLog("新增行业项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增行业项目" + objItem.ID + "成功");
                }
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

    //设置指定行业的监测项目数据
    [WebMethod]
    public static string SaveDataItem(string strIndustryID, string strSelItem_IDs)
    {
        bool isSuccess = true;

        string[] arrSelItemId = strSelItem_IDs.Split(',');

        TBaseIndustryItemVo objItemWhere = new TBaseIndustryItemVo();
        objItemWhere.INDUSTRY_ID = strIndustryID;
        new TBaseIndustryItemLogic().Delete(objItemWhere);

        for (int i = 0; i < arrSelItemId.Length; i++)
        {
            TBaseIndustryItemVo objItem = new TBaseIndustryItemVo();
            objItem.ID = GetSerialNumber("t_base_industry_item_id");
            objItem.INDUSTRY_ID = strIndustryID;
            objItem.ITEM_ID = arrSelItemId[i];

            isSuccess = new TBaseIndustryItemLogic().Create(objItem);
            if (isSuccess)
            {
                new PageBase().WriteLog("新增行业项目", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增行业项目" + objItem.ID + "成功");
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
    /// 获取点位信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getIndustryName(string strValue)
    {
        return new TBaseIndustryInfoLogic().Details(strValue).INDUSTRY_NAME;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemName(string strValue)
    {
        TBaseItemInfoVo objItem = new TBaseItemInfoLogic().Details(strValue);
        return new TBaseMonitorTypeInfoLogic().Details(objItem.MONITOR_ID).MONITOR_TYPE_NAME + "—" + objItem.ITEM_NAME;
    }
}