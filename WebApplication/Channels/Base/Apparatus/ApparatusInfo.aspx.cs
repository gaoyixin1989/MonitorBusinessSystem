using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;


/// <summary>
/// 功能描述：仪器信息管理
/// 创建日期：2012-10-31
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Apparatus_ApparatusInfo : PageBase
{
    public string srhApparatus_Code = "", srh_Name = "", srh_Model = "", srhProvider = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取仪器信息
        if (Request["type"] != null && Request["type"].ToString() == "getApparatusInfo")
        {
            //获取传入的查询条件
            if (!String.IsNullOrEmpty(Request.Params["srhApparatus_Code"]))
            {
                srhApparatus_Code = Request.Params["srhApparatus_Code"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_Name"]))
            {
                srh_Name = Request.Params["srh_Name"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srh_Model"]))
            {
                srh_Model = Request.Params["srh_Model"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["srhProvider"]))
            {
                srhProvider = Request.Params["srhProvider"].Trim();
            }
            strResult = getApparatusInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取仪器资料信息
        if (Request["type"] != null && Request["type"].ToString() == "getApparatusDocumentInfo")
        {
            strResult = getApparatusDocumentInfo(Request["appId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取仪器检定证书信息
        if (Request["type"] != null && Request["type"].ToString() == "getApparatusCertificInfo")
        {
            strResult = getApparatusCertificInfo(Request["appId"].ToString());
            Response.Write(strResult);
            Response.End();
        }

        if (!Page.IsPostBack)
        {

        }
    }
    /// <summary>
    /// 获取仪器信息
    /// </summary>
    /// <returns></returns>
    private string getApparatusInfo()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseApparatusInfoVo TBaseApparatusInfoVo = new TBaseApparatusInfoVo();
        TBaseApparatusInfoVo.IS_DEL = "0";
        TBaseApparatusInfoVo.SORT_FIELD = strSortname;
        TBaseApparatusInfoVo.SORT_TYPE = strSortorder;

        //自定义查询使用
        if (!String.IsNullOrEmpty(srhApparatus_Code) || !String.IsNullOrEmpty(srh_Name) || !String.IsNullOrEmpty(srh_Model) || !String.IsNullOrEmpty(srhProvider))
        {
            TBaseApparatusInfoVo.APPARATUS_CODE = srhApparatus_Code;
            TBaseApparatusInfoVo.NAME = srh_Name;
            TBaseApparatusInfoVo.MODEL = srh_Model;
            TBaseApparatusInfoVo.FITTINGS_PROVIDER = srhProvider;
            intTotalCount = new TBaseApparatusInfoLogic().GetSelecDefinedtResultCount(TBaseApparatusInfoVo);
            dt = new TBaseApparatusInfoLogic().SelectDefinedTadble(TBaseApparatusInfoVo, intPageIndex, intPageSize);
        }
        //无条件首次加载用
        else
        {
            dt = new TBaseApparatusInfoLogic().SelectByTable(TBaseApparatusInfoVo, intPageIndex, intPageSize);
            intTotalCount = new TBaseApparatusInfoLogic().GetSelectResultCount(TBaseApparatusInfoVo);
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取仪器资料信息
    /// </summary>
    /// <param name="appId">仪器ID</param>
    /// <returns></returns>
    public string getApparatusDocumentInfo(string appId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseApparatusFilebakVo TBaseApparatusFilebakVo = new TBaseApparatusFilebakVo();
        TBaseApparatusFilebakVo.APPARATUS_CODE = appId;
        TBaseApparatusFilebakVo.SORT_FIELD = strSortname;
        TBaseApparatusFilebakVo.SORT_TYPE = strSortorder;
        DataTable dt = new TBaseApparatusFilebakLogic().SelectByTable(TBaseApparatusFilebakVo, intPageIndex, intPageSize);
        int intTotalCount = new TBaseApparatusFilebakLogic().GetSelectResultCount(TBaseApparatusFilebakVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取仪器检定证书信息
    /// </summary>
    /// <param name="appId">仪器ID</param>
    /// <returns></returns>
    public string getApparatusCertificInfo(string appId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseApparatusCertificVo TBaseApparatusCertificVo = new TBaseApparatusCertificVo();
        TBaseApparatusCertificVo.APPARATUS_ID = appId;
        TBaseApparatusCertificVo.SORT_FIELD = strSortname;
        TBaseApparatusCertificVo.SORT_TYPE = strSortorder;
        DataTable dt = new TBaseApparatusCertificLogic().SelectByTable(TBaseApparatusCertificVo, intPageIndex, intPageSize);
        int intTotalCount = new TBaseApparatusCertificLogic().GetSelectResultCount(TBaseApparatusCertificVo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 删除仪器设备数据
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteApparatusInfo(string strValue)
    {
        TBaseApparatusInfoVo TBaseApparatusInfoVo = new TBaseApparatusInfoVo();
        TBaseApparatusInfoVo.ID = strValue;
        TBaseApparatusInfoVo.IS_DEL = "1";
        bool isSuccess = new TBaseApparatusInfoLogic().Edit(TBaseApparatusInfoVo);
        if (isSuccess)
        {
            new i3.View.PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.DelApparatusInfo, "", new i3.View.PageBase().LogInfo.UserInfo.USER_NAME + "删除仪器信息" + TBaseApparatusInfoVo.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 删除仪器资料数据
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteApparatusDocumentInfo(string strValue)
    {
        bool isSuccess = new TBaseApparatusFilebakLogic().Delete(strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除仪器资料", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除仪器资料成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 删除仪器检定资料信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteApparatusCertificInfo(string strValue)
    {
        bool isSuccess = new TBaseApparatusCertificLogic().Delete(strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除仪器检定资料", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除仪器检定资料");
        }
        return isSuccess == true ? "1" : "0";
    }
}