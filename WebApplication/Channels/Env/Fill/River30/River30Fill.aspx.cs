using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.River30;
using i3.ValueObject.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Fill.River30;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.River30;

/// <summary>
/// 功能描述：双三十水质数据填报
/// 创建人：潘德军
/// 创建日期：2013-5-9
/// </summary>
public partial class Channels_Env_Fill_River30_River30Fill : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "GetYear":
                        GetYear();
                        break;
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "getDict":
                        Response.Write(getDictJsonString(Request["dictType"].ToString()));
                        Response.End();
                        break;
                    case "GetPoint":
                        GetPoint();
                        break;
                    case "GetFillID":
                        GetFillID();
                        break;
                }
            }
        }
    }

    #region 获取年份

    private void GetYear()
    {
        string json = getYearInfo();

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region// 获取监测点(ljn,2013/6/19)
    private void GetPoint()
    {
        string year = Request["year"];
        string month = Request["month"]; 
        TEnvPRiver30Vo Dustpoint = new TEnvPRiver30Vo();//双30废水
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.River30.TEnvPRiver30Logic().SelectByTable(Dustpoint);
        //加入全部
        DataRow dr = dt.NewRow();
        dr["SECTION_NAME"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);
        string json = DataTableToJson(dt);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }
    #endregion

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillRiver30Vo envFillRiver30Vo = new TEnvFillRiver30Vo();
        envFillRiver30Vo.YEAR = year;
        envFillRiver30Vo.MONTH = month;

        envFillRiver30Vo = new TEnvFillRiver30Logic().Details(envFillRiver30Vo);

        string json = "{\"ID\":\"" + envFillRiver30Vo.ID + "\",\"STATUS\":\"" + envFillRiver30Vo.STATUS + "\"}";

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion


    #region 获取数据

    private void GetData()
    {
        string strWhere = "1=1";
        string year = Request["year"];
        string month = Request["month"];
        string pointId = Request["pointId"];
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";
        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillRiver30Logic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE,
                                       TEnvPRiver30VVo.T_ENV_P_RIVER30_V_TABLE,
                                        TEnvPRiver30VItemVo.T_ENV_P_RIVER30_V_ITEM_TABLE,
                                       TEnvFillRiver30Vo.T_ENV_FILL_RIVER30_TABLE,
                                       TEnvFillRiver30ItemVo.T_ENV_FILL_RIVER30_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_RIVER30,
                                       SerialType.T_ENV_FILL_RIVER30_ITEM,
                                       "1");
        string json = DataTableToJsonUnsureCol(dt);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 保存数据

    private void SaveData()
    {
        //string data = Request["data"];
        //DataTable dtData = JSONToDataTable2(data);
        //bool result = new TEnvFillRiver30Logic().SaveRiverFillData(dtData);
        bool result = false;
        string data = "[" + Request["data"] + "]";
        string ID = Request["id"];
        string updateName = Request["updateName"];
        string value = Request["value"];
        string ConditionID = "";
        CommonLogic com = new CommonLogic();
        result = com.UpdateCommonFill(updateName, value, ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 获取下拉字典项
    private void getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);
        Response.ContentType = "application/json";
        Response.Write(strJson);
        Response.End();
    }
    #endregion

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
    /// 获取断面名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getSectionName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE, "SECTION_NAME", "ID", strV);
    }

    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPRiver30VVo.T_ENV_P_RIVER30_V_TABLE, "VERTICAL_NAME", "ID", strV);
    }
}