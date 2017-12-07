using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.FillQry;
using i3.ValueObject.Channels.Env.Fill;
using i3.BusinessLogic.Channels.Env.Fill;
using System.Web.Services;

/// <summary>
/// 环境质量附件上传下载管理
/// 创建人：魏林
/// 创建时间：2014-08-04
/// </summary>
public partial class Channels_Env_Fill_EnvFillAttInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "GetList":
                        json = GetList();
                        break;
                    case "GetYear":
                        json = getYearInfo(5, 5);
                        break;
                    case "GetDict":
                        json = getDict();
                        break;
                }

                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }

        }
    }

    #region 获取数据信息

    private string GetList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strType = (Request.Params["SrhType"] != null) ? Request.Params["SrhType"] : "";
        string strYear = (Request.Params["SrhYear"] != null) ? Request.Params["SrhYear"] : "";
        string strSeason = (Request.Params["SrhSeason"] != null) ? Request.Params["SrhSeason"] : "";
        string strMonth = (Request.Params["SrhMonth"] != null) ? Request.Params["SrhMonth"] : "";
        string strDay = (Request.Params["SrhDay"] != null) ? Request.Params["SrhDay"] : "";

        DataTable dt = new DataTable();
        TEnvFillAttVo objFillAttVo = new TEnvFillAttVo();
        objFillAttVo.ENVTYPE = strType;
        objFillAttVo.YEAR = strYear;
        objFillAttVo.SEASON = strSeason;
        objFillAttVo.MONTH = strMonth;
        objFillAttVo.DAY = strDay;

        objFillAttVo.SORT_FIELD = "ENVTYPE,YEAR";
        objFillAttVo.SORT_TYPE = "desc";

        int intTotalCount = new TEnvFillAttLogic().GetSelectResultCount(objFillAttVo); ;//总计的数据条数
        dt = new TEnvFillAttLogic().SelectByTable(objFillAttVo, intPageIdx, intPagesize);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            i3.ValueObject.Channels.OA.ATT.TOaAttVo objAttVo = new i3.ValueObject.Channels.OA.ATT.TOaAttVo();
            objAttVo.BUSINESS_ID = dt.Rows[i]["ID"].ToString();
            objAttVo.BUSINESS_TYPE = "EnvData";
            objAttVo = new i3.BusinessLogic.Channels.OA.ATT.TOaAttLogic().SelectByObject(objAttVo);
            dt.Rows[i]["REMARK1"] = objAttVo.ATTACH_NAME;
        }
        string strJson = CreateToJson(dt, intTotalCount);

        return strJson;
    }

    #endregion

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict()
    {
        string strDictType = Request["dictType"].ToString();
        
        return getDictJsonString(strDictType);
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteData(string strDelID)
    {
        if (strDelID.Length == 0)
            return "0";

        bool isSuccess = true;
        isSuccess = new TEnvFillAttLogic().Delete(strDelID);

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
    /// 添加数据
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string EditData(string strID, string strEnvType, string strYear, string strSeason, string strMonth, string strDay, string strRemark)
    {
        bool isSuccess = true;
        TEnvFillAttVo objFillAttVo = new TEnvFillAttVo();
        objFillAttVo.ENVTYPE = strEnvType;
        objFillAttVo.YEAR = strYear;
        objFillAttVo.SEASON = strSeason;
        objFillAttVo.MONTH = strMonth;
        objFillAttVo.DAY = strDay;
        objFillAttVo.REMARK = strRemark;
        if (strID.Length > 0)
        {
            objFillAttVo.ID = strID;
            isSuccess = new TEnvFillAttLogic().Edit(objFillAttVo);
        }
        else
        {
            objFillAttVo.ID = GetSerialNumber("t_env_fill_att_id");
            isSuccess = new TEnvFillAttLogic().Create(objFillAttVo);
        }

        return isSuccess ? "1" : "0";
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
}