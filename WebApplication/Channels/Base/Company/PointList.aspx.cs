using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：点位信息管理
/// 创建日期：2012-11-08
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Company_PointList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //定义结果
        string strResult = "";
        //获取点位信息
        if (Request["type"] != null && Request["type"].ToString() == "getPoint")
        {
            strResult = getPointList();
            Response.Write(strResult);
            Response.End();
        }

        //获取指定点位的监测项目信息
        if (Request["type"] != null && Request["type"].ToString() == "GetItems")
        {
            strResult = getItemList();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取点位信息
    private string getPointList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseCompanyPointVo.NUM_FIELD;

        string strCompanyID = Request.Params["CompanyID"];
        if (strCompanyID.Length == 0)
            return "";

        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.IS_DEL = "0";
        objPoint.COMPANY_ID = strCompanyID;
        objPoint.SORT_FIELD = strSortname;
        objPoint.SORT_TYPE = strSortorder;
        DataTable dt = new TBaseCompanyPointLogic().SelectByTable(objPoint, intPageIndex, intPageSize);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseMonitorTypeInfoVo MonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
            MonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(dt.Rows[i]["MONITOR_ID"].ToString());
            dt.Rows[i]["REMARK1"] = MonitorTypeInfoVo.REMARK1;
        }
        int intTotalCount = new TBaseCompanyPointLogic().GetSelectResultCount(objPoint);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    //获取指定点位的监测项目信息
    private string getItemList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelPointID = (Request.Params["selPointID"] != null) ? Request.Params["selPointID"] : "";
        if (strSelPointID.Length <= 0)
        {
            return "";
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TBaseCompanyPointItemVo.ID_FIELD;

        TBaseCompanyPointItemVo objPointItem = new TBaseCompanyPointItemVo();
        objPointItem.IS_DEL = "0";
        objPointItem.POINT_ID = strSelPointID;
        objPointItem.SORT_FIELD = strSortname;
        objPointItem.SORT_TYPE = strSortorder;
        TBaseCompanyPointItemLogic logicPointItem = new TBaseCompanyPointItemLogic();

        int intTotalCount = logicPointItem.GetSelectResultCount(objPointItem); ;//总计的数据条数
        DataTable dt = logicPointItem.SelectByTable(objPointItem, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除点位信息
    [WebMethod]
    public static string deletePoint(string strValue)
    {
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.ID = strValue;
        objPoint.IS_DEL = "1";
        bool isSuccess = new TBaseCompanyPointLogic().Edit(objPoint);

        TBaseAttrbuteValueVo objAttrValueDelWhere = new TBaseAttrbuteValueVo();
        objAttrValueDelWhere.OBJECT_ID = strValue;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValueVo objAttrValueDelSet = new TBaseAttrbuteValueVo();
        objAttrValueDelSet.IS_DEL = "1";
        if (new TBaseAttrbuteValueLogic().Edit(objAttrValueDelSet, objAttrValueDelWhere))
        {
            new PageBase().WriteLog("删除点位属性", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除对象ID" + objPoint.ID + "的点位属性成功");
        }
        if (isSuccess)
        {
            new PageBase().WriteLog("删除点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除点位" + objPoint.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }

    //编辑点位数据
    [WebMethod]
    public static string SaveData(string strPointID, string strCompanyID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID, string strSAMPLE_FREQ,string strFREQ,
        string strCREATE_DATE, string strADDRESS, string strLONGITUDE, string strLATITUDE, string strNUM, string strAttribute,
        string strNATIONAL_ST_CONDITION_ID, string strLOCAL_ST_CONDITION_ID, string strINDUSTRY_ST_CONDITION_ID)
    {
        bool isSuccess = true;

        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.ID = strPointID.Length > 0 ? strPointID : GetSerialNumber("t_base_company_point_id");
        objPoint.IS_DEL = "0";
        objPoint.COMPANY_ID = strCompanyID;
        objPoint.POINT_NAME = strPOINT_NAME;
        objPoint.MONITOR_ID = strMONITOR_ID;
        objPoint.POINT_TYPE = strPOINT_TYPE;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.SAMPLE_FREQ = strSAMPLE_FREQ;
        //objPoint.FREQ = strFREQ;
        //监测频次默认写1
        objPoint.FREQ = "1";
        objPoint.CREATE_DATE = strCREATE_DATE;
        objPoint.ADDRESS = strADDRESS;
        objPoint.LONGITUDE = strLONGITUDE;
        objPoint.LATITUDE = strLATITUDE;
        objPoint.NUM = strNUM;

        objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
        objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
        objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

        if (strPointID.Length > 0)
        {
            isSuccess = new TBaseCompanyPointLogic().Edit(objPoint);
            if (isSuccess)
            {
                new PageBase().WriteLog("编辑点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑点位" + objPoint.ID + "成功");
            }
        }
        else
        {
            isSuccess = new TBaseCompanyPointLogic().Create(objPoint);
            if (isSuccess)
            {
                new PageBase().WriteLog("新增点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增点位" + objPoint.ID + "成功");
            }
        }

        TBaseAttrbuteValueLogic logicAttrValue = new TBaseAttrbuteValueLogic();

        //清掉原有动态属性值
        TBaseAttrbuteValueVo objAttrValueDelWhere = new TBaseAttrbuteValueVo();
        objAttrValueDelWhere.OBJECT_ID = objPoint.ID;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValueVo objAttrValueDelSet = new TBaseAttrbuteValueVo();
        objAttrValueDelSet.IS_DEL = "1";
        if (logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere))
        {
            new PageBase().WriteLog("清掉动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "清掉对象ID" + objPoint.ID + "的动态属性值成功");
        }

        //新增动态属性值
        if (strAttribute.Length > 0)
        {
            string[] arrAttribute = strAttribute.Split('-');
            for (int i = 0; i < arrAttribute.Length; i++)
            {
                string[] arrAttrValue = arrAttribute[i].Split('|');

                TBaseAttrbuteValueVo objAttrValueAdd = new TBaseAttrbuteValueVo();
                objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value_id");
                objAttrValueAdd.IS_DEL = "0";
                objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                objAttrValueAdd.OBJECT_ID = objPoint.ID;
                objAttrValueAdd.ATTRBUTE_CODE = arrAttrValue[1];
                objAttrValueAdd.ATTRBUTE_VALUE = arrAttrValue[2];
                if (isSuccess = logicAttrValue.Create(objAttrValueAdd))
                {
                    new PageBase().WriteLog("新增动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增动态属性值" + objAttrValueAdd.ID + "成功");
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

    //编辑点位数据
    [WebMethod]
    public static string SaveCompanyPointData(string strPointID, string strCompanyID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID, string strSAMPLE_FREQ, string strFREQ,
        string strCREATE_DATE, string strADDRESS, string strLONGITUDE, string strLATITUDE, string strNUM, string strAttribute,
        string strNATIONAL_ST_CONDITION_ID, string strLOCAL_ST_CONDITION_ID, string strINDUSTRY_ST_CONDITION_ID)
    {
        bool isSuccess = true;

        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.ID = strPointID.Length > 0 ? strPointID : GetSerialNumber("t_base_company_point_id");
        objPoint.IS_DEL = "0";
        objPoint.COMPANY_ID = strCompanyID;
        objPoint.POINT_NAME = strPOINT_NAME;
        objPoint.MONITOR_ID = strMONITOR_ID;
        objPoint.POINT_TYPE = strPOINT_TYPE;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.SAMPLE_FREQ = strSAMPLE_FREQ;
        objPoint.FREQ = "1";
        objPoint.CREATE_DATE = strCREATE_DATE;
        objPoint.ADDRESS = strADDRESS;
        objPoint.LONGITUDE = strLONGITUDE;
        objPoint.LATITUDE = strLATITUDE;
        objPoint.NUM = strNUM;

        objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
        objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
        objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

        if (strPointID.Length > 0)
        {
            isSuccess = new TBaseCompanyPointLogic().Edit(objPoint);
            if (isSuccess)
            {
                new PageBase().WriteLog("编辑点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑点位" + objPoint.ID + "成功");
            }
        }
        else
        {
            isSuccess = new TBaseCompanyPointLogic().Create(objPoint);
            if (isSuccess)
            {
                new PageBase().WriteLog("新增点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增点位" + objPoint.ID + "成功");
            }
        }

        TBaseAttrbuteValueLogic logicAttrValue = new TBaseAttrbuteValueLogic();

        //清掉原有动态属性值
        TBaseAttrbuteValueVo objAttrValueDelWhere = new TBaseAttrbuteValueVo();
        objAttrValueDelWhere.OBJECT_ID = objPoint.ID;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValueVo objAttrValueDelSet = new TBaseAttrbuteValueVo();
        objAttrValueDelSet.IS_DEL = "1";
        if (logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere))
        {
            new PageBase().WriteLog("清掉动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "清掉对象ID" + objPoint.ID + "的动态属性值成功");
        }

        //新增动态属性值
        if (strAttribute.Length > 0)
        {
            string[] arrAttribute = strAttribute.Split('-');
            for (int i = 0; i < arrAttribute.Length; i++)
            {
                string[] arrAttrValue = arrAttribute[i].Split('|');

                TBaseAttrbuteValueVo objAttrValueAdd = new TBaseAttrbuteValueVo();
                objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value_id");
                objAttrValueAdd.IS_DEL = "0";
                objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                objAttrValueAdd.OBJECT_ID = objPoint.ID;
                objAttrValueAdd.ATTRBUTE_CODE = arrAttrValue[1];
                objAttrValueAdd.ATTRBUTE_VALUE = arrAttrValue[2];
                if (isSuccess = logicAttrValue.Create(objAttrValueAdd))
                {
                    new PageBase().WriteLog("新增动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增动态属性值" + objAttrValueAdd.ID + "成功");
                }
            }
        }

        if (isSuccess)
        {
            return objPoint.ID;
        }
        else
        {
            return "";
        }
    }

    //为了避免产生冲突，新扩展的方法
    //编辑点位数据 Create by weilin 2013-12-12 
    [WebMethod]
    public static string SaveCompanyPointDataEx(string strPointID, string strCompanyID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID, string strSAMPLE_DAY, string strSAMPLE_FREQ, string strFREQ,
        string strCREATE_DATE, string strADDRESS, string strLONGITUDE, string strLATITUDE, string strNUM, string strAttribute,
        string strNATIONAL_ST_CONDITION_ID, string strLOCAL_ST_CONDITION_ID, string strINDUSTRY_ST_CONDITION_ID)
    {
        bool isSuccess = true;
        string strPointIDs = string.Empty;
        string[] strPointNames;
        if (strPointID.Length > 0)
        {
            strPointNames = strPOINT_NAME.Split('|');
        }
        else
        {
            strPointNames = strPOINT_NAME.Split('、');
        }
        for (int j = 0; j < strPointNames.Length; j++)
        {
            TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
            objPoint.ID = strPointID.Length > 0 ? strPointID : GetSerialNumber("t_base_company_point_id");
            objPoint.IS_DEL = "0";
            objPoint.COMPANY_ID = strCompanyID;
            objPoint.POINT_NAME = strPointNames[j].ToString();
            objPoint.MONITOR_ID = strMONITOR_ID;
            objPoint.POINT_TYPE = strPOINT_TYPE;
            objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
            objPoint.SAMPLE_DAY = strSAMPLE_DAY;
            objPoint.SAMPLE_FREQ = strSAMPLE_FREQ;
            objPoint.FREQ = "1";
            objPoint.CREATE_DATE = strCREATE_DATE;
            objPoint.ADDRESS = strADDRESS;
            objPoint.LONGITUDE = strLONGITUDE;
            objPoint.LATITUDE = strLATITUDE;
            objPoint.NUM = strNUM;

            objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
            objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
            objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

            if (strPointID.Length > 0)
            {
                isSuccess = new TBaseCompanyPointLogic().Edit(objPoint);
                if (isSuccess)
                {
                    new PageBase().WriteLog("编辑点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑点位" + objPoint.ID + "成功");
                }
            }
            else
            {
                isSuccess = new TBaseCompanyPointLogic().Create(objPoint);
                if (isSuccess)
                {
                    new PageBase().WriteLog("新增点位", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增点位" + objPoint.ID + "成功");
                }
            }
            strPointIDs += objPoint.ID + "、";

            TBaseAttrbuteValueLogic logicAttrValue = new TBaseAttrbuteValueLogic();

            //清掉原有动态属性值
            TBaseAttrbuteValueVo objAttrValueDelWhere = new TBaseAttrbuteValueVo();
            objAttrValueDelWhere.OBJECT_ID = objPoint.ID;
            objAttrValueDelWhere.IS_DEL = "0";
            TBaseAttrbuteValueVo objAttrValueDelSet = new TBaseAttrbuteValueVo();
            objAttrValueDelSet.IS_DEL = "1";
            if (logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere))
            {
                new PageBase().WriteLog("清掉动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "清掉对象ID" + objPoint.ID + "的动态属性值成功");
            }

            //新增动态属性值
            if (strAttribute.Length > 0)
            {
                string[] arrAttribute = strAttribute.Split('-');
                for (int i = 0; i < arrAttribute.Length; i++)
                {
                    string[] arrAttrValue = arrAttribute[i].Split('|');

                    TBaseAttrbuteValueVo objAttrValueAdd = new TBaseAttrbuteValueVo();
                    objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value_id");
                    objAttrValueAdd.IS_DEL = "0";
                    objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                    objAttrValueAdd.OBJECT_ID = objPoint.ID;
                    objAttrValueAdd.ATTRBUTE_CODE = arrAttrValue[1];
                    objAttrValueAdd.ATTRBUTE_VALUE = arrAttrValue[2];
                    if (isSuccess = logicAttrValue.Create(objAttrValueAdd))
                    {
                        new PageBase().WriteLog("新增动态属性值", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增动态属性值" + objAttrValueAdd.ID + "成功");
                    }
                }
            }
        }

        if (isSuccess)
        {
            return strPointIDs.TrimEnd('、');
        }
        else
        {
            return "";
        }
    }

    //设置点位的监测项目数据
    [WebMethod]
    public static string SaveDataItem(string strPointID, string strSelItem_IDs)
    {
        bool isSuccess = true;

        string[] arrSelItemId = strSelItem_IDs.Split(',');

        TBaseCompanyPointItemVo objPointItemSet = new TBaseCompanyPointItemVo();
        objPointItemSet.IS_DEL = "1";
        TBaseCompanyPointItemVo objPointItemWhere = new TBaseCompanyPointItemVo();
        objPointItemWhere.IS_DEL = "0";
        objPointItemWhere.POINT_ID = strPointID;
        if (new TBaseCompanyPointItemLogic().Edit(objPointItemSet, objPointItemWhere))
        {
            new PageBase().WriteLog("删除监测点位明细表", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除监测点位" + strPointID + "的明细表成功");
        }

        if (strSelItem_IDs.Length > 0)
        {
            for (int i = 0; i < arrSelItemId.Length; i++)
            {
                TBaseCompanyPointItemVo objPointItem = new TBaseCompanyPointItemVo();
                objPointItem.ID = GetSerialNumber("t_base_company_point_item_id");
                objPointItem.IS_DEL = "0";
                objPointItem.POINT_ID = strPointID;
                objPointItem.ITEM_ID = arrSelItemId[i];

                isSuccess = new TBaseCompanyPointItemLogic().Create(objPointItem);
                if (isSuccess)
                {
                    new PageBase().WriteLog("新增监测点位明细表", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增监测点位明细表" + objPointItem.ID + "成功");
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

    //复制监测项目
    [WebMethod]
    public static string CopyPointItem(string strCopyID, string strPastID)
    {
        bool isSuccess = true;

        TBaseCompanyPointItemVo objPointItemCopy = new TBaseCompanyPointItemVo();
        objPointItemCopy.IS_DEL = "0";
        objPointItemCopy.POINT_ID = strCopyID;
        DataTable dtCopy = new TBaseCompanyPointItemLogic().SelectByTable(objPointItemCopy);

        TBaseCompanyPointItemVo objPointItemPast = new TBaseCompanyPointItemVo();
        objPointItemPast.IS_DEL = "0";
        objPointItemPast.POINT_ID = strPastID;
        DataTable dtPast = new TBaseCompanyPointItemLogic().SelectByTable(objPointItemPast);

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
                TBaseCompanyPointItemVo objPointItem = new TBaseCompanyPointItemVo();
                objPointItem.ID = GetSerialNumber("t_base_company_point_item_id");
                objPointItem.IS_DEL = "0";
                objPointItem.POINT_ID = strPastID;
                objPointItem.ITEM_ID = strCopyItemID;

                isSuccess = new TBaseCompanyPointItemLogic().Create(objPointItem);
                if (isSuccess)
                {
                    new PageBase().WriteLog("新增监测点位明细表", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增监测点位明细表" + objPointItem.ID + "成功");
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

    /// <summary>
    /// 获取企业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strValue)
    {
        return new TBaseCompanyInfoLogic().Details(strValue).COMPANY_NAME;
    }

    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorName(string strValue)
    {
        return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
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

    /// <summary>
    /// 获取点位信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strValue)
    {
        return new TBaseCompanyPointLogic().Details(strValue).POINT_NAME;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemName(string strValue)
    {
        return new TBaseItemInfoLogic().Details(strValue).ITEM_NAME;
    }
}