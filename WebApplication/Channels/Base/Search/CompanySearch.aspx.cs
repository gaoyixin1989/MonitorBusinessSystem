using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;

/// <summary>
/// 功能描述：企业信息查询
/// 创建时间：2012-11-30
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_CompanySearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //定义结果
            string strResult = "";
            string strID = "";
            if (Request["strid"] != null)
            {
                strID = this.Request["strid"].ToString();
            }
            //获取企业信息
            if (Request["type"] != null && Request["type"].ToString() == "getCompanyInfo")
            {
                strResult = getCompanyInfo();
                Response.Write(strResult);
                Response.End();
            }
            //获得企业点位信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getCompanyPointInfo")
            {
                strResult = getPointList();
                Response.Write(strResult);
                Response.End();
            }
            //获得点位项目信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getCompanyPointItemInfo")
            {
                strResult = getItemList();
                Response.Write(strResult);
                Response.End();
            }
            //获取下拉列表信息
            if (Request["type"] != null && Request["type"].ToString() == "getDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取行业信息
            if (Request["type"] != null && Request["type"].ToString() == "getIndustry")
            {
                strResult = getIndustry();
                Response.Write(strResult);
                Response.End();
            }
            //获取指定动态属性类别的动态属性数据
            if (Request["type"] != null && Request["type"].ToString() == "GetAttrrbute")
            {
                GetAttrrbute();
            }
            //获取指定动态属性类别的动态属性数据
            if (Request["type"] != null && Request["type"].ToString() == "GetAttrValue")
            {
                GetAttrValue(strID);
            }
            //加载数据
            if (Request["type"] != null && Request["type"].ToString() == "loadData")
            {
                GetData(strID);
            }
        }
    }

    #region 基础信息数据获取
    /// <summary>
    /// 获取企业信息
    /// </summary>
    /// <returns></returns>
    private string getCompanyInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.IS_DEL = "0";
        TBaseCompanyInfoVo.SORT_FIELD = strSortname;
        TBaseCompanyInfoVo.SORT_TYPE = strSortorder;
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        //自定义查询使用
        TBaseCompanyInfoVo.COMPANY_NAME = !string.IsNullOrEmpty(Request.QueryString["srhCompayName"]) ? Request.QueryString["srhCompayName"].ToString().Trim() : "";
        TBaseCompanyInfoVo.AREA = !string.IsNullOrEmpty(Request.QueryString["srh_Area"]) ? Request.QueryString["srh_Area"].ToString().Trim() : "";
        TBaseCompanyInfoVo.INDUSTRY = !string.IsNullOrEmpty(Request.QueryString["srh_Industry"]) ? Request.QueryString["srh_Industry"].ToString().Trim() : "";
        intTotalCount = new TBaseCompanyInfoLogic().GetSelecDefinedtResultCount(TBaseCompanyInfoVo);
        dt = new TBaseCompanyInfoLogic().SelectDefinedTadble(TBaseCompanyInfoVo, intPageIndex, intPageSize);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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

        string strCompanyID = Request.Params["comId"];
        if (strCompanyID == null || strCompanyID.Length == 0)
            return "";

        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.IS_DEL = "0";
        objPoint.COMPANY_ID = strCompanyID;
        objPoint.SORT_FIELD = strSortname;
        objPoint.SORT_TYPE = strSortorder;
        DataTable dt = new TBaseCompanyPointLogic().SelectByTable(objPoint, intPageIndex, intPageSize);
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

        string strSelPointID = (Request.Params["pId"] != null) ? Request.Params["pId"] : "";
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

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
    /// <summary>
    /// 获取行业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    public static string getIndustry()
    {
        DataTable dt = new i3.BusinessLogic.Channels.Base.Industry.TBaseIndustryInfoLogic().SelectByTable(new i3.ValueObject.Channels.Base.Industry.TBaseIndustryInfoVo());
        return DataTableToJson(dt);
    }
    #endregion

    #region 动态属性
    //获取指定动态属性类别的动态属性数据
    private void GetAttrrbute()
    {
        DataTable dt = new TBaseAttributeInfoLogic().SelectByTableByJoin();

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
        Response.End();
    }

    //获取点位对应的动态属性值
    private void GetAttrValue(string strID)
    {
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointLogic().Details(strID);

        TBaseAttrbuteValueVo objAttrValue = new TBaseAttrbuteValueVo();
        objAttrValue.IS_DEL = "0";
        objAttrValue.OBJECT_ID = strID;

        DataTable dt = new TBaseAttrbuteValueLogic().SelectByTable(objAttrValue);

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
        Response.End();
    }

    //获取数据
    private void GetData(string strID)
    {
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointLogic().Details(strID);

        TBaseEvaluationConInfoLogic logicCon = new TBaseEvaluationConInfoLogic();
        TBaseEvaluationInfoLogic logicSt = new TBaseEvaluationInfoLogic();

        //按行、地、国标优先顺序进行标准的选定
        //国标条件项
        if (objPoint.NATIONAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.NATIONAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.NATIONAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidNATIONAL_ST_CON = objCon.ID;
        }
        //地标条件项
        if (objPoint.LOCAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.LOCAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.LOCAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidLOCAL_ST_CON = objCon.ID;
        }
        //行标条件项
        if (objPoint.INDUSTRY_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.INDUSTRY_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.INDUSTRY_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidINDUSTRY_ST_CON = objCon.ID;
        }

        string strJson = ToJson(objPoint);

        Response.Write(strJson);
        Response.End();
    }
    #endregion

    #region 远程客户端调用方法
    /// <summary>
    /// 获取行业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getIndustryCode(string strValue)
    {
        return new i3.BusinessLogic.Channels.Base.Industry.TBaseIndustryInfoLogic().Details(strValue).INDUSTRY_NAME;
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strType, string strCode)
    {
        return PageBase.getDictName(strCode, strType);
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

    /// <summary>
    /// 获取点位属性类别
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAttName(string strValue)
    {
        return new TBaseAttributeTypeLogic().Details(new TBaseAttributeTypeVo() { ID = strValue, IS_DEL = "0" }).SORT_NAME;
    }
    #endregion
}