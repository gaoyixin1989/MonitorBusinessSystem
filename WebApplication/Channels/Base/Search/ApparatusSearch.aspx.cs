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
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：仪器查询
/// 创建时间：2012-11-29
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_ApparatusSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //定义结果
            string strResult = "";
            //获取仪器信息
            if (Request["type"] != null && Request["type"].ToString() == "getApparatusInfo")
            {
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

        TBaseApparatusInfoVo.APPARATUS_CODE = !String.IsNullOrEmpty(Request.Params["srhApparatus_Code"]) ? Request.Params["srhApparatus_Code"].ToString() : "";
        TBaseApparatusInfoVo.NAME = !String.IsNullOrEmpty(Request.Params["srh_Name"]) ? Request.Params["srh_Name"].ToString() : ""; ;
        TBaseApparatusInfoVo.MODEL = !String.IsNullOrEmpty(Request.Params["srh_Model"]) ? Request.Params["srh_Model"].ToString() : ""; ;
        TBaseApparatusInfoVo.FITTINGS_PROVIDER = !String.IsNullOrEmpty(Request.Params["srhProvider"]) ? Request.Params["srhProvider"].ToString() : ""; ;
        intTotalCount = new TBaseApparatusInfoLogic().GetSelecDefinedtResultCount(TBaseApparatusInfoVo);
        dt = new TBaseApparatusInfoLogic().SelectDefinedTadble(TBaseApparatusInfoVo, intPageIndex, intPageSize);

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
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetDict(string strType, string strCode)
    {
        if (!string.IsNullOrEmpty(strCode))
        {
            TSysDictVo objtd = new TSysDictVo();
            objtd.DICT_TYPE = strType;
            objtd.DICT_CODE = strCode;
            objtd = new TSysDictLogic().Details(objtd);
            return objtd.DICT_TEXT;
        }
        return "";
    }
}