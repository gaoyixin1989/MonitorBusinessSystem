using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.Env.Fill.PolluteWater;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.PolluteAir;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.ValueObject.Channels.Env.Fill.PolluteAir;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Fill_PolluteAir_PolluteAirFill : PageBase
{
    /// <summary>
    /// 污染源常规废气，刘静楠 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["type"]))
            {
                switch (Request["type"])
                {
                    case "GetYear":
                        GetYear();
                        break;
                    case "GetMonth":
                        GetMonth();
                        break;
                    case "GetPoint":
                        GetPoint();
                        break;
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
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

    #region 获取月份
    private void GetMonth()
    {
        string json = getMonthInfo();
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }
    #endregion

    #region 获取监测点
    private void GetPoint()
    {
        string year = Request["year"];
        string month = Request["month"];
        DataTable dt = new TEnvFillPolluteAirLogic().PointByTable(year, month);
        //加入全部
        DataRow dr = dt.NewRow();
        dr["POINT_NAME"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);
        string json = DataTableToJson(dt);
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
        //拼写条件语句(注：条件中的列名要与点位上的列名一致)
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";

        //填报表需要显示信息(注：这Table里面的第一列数据要与填报表的列名一致)
        DataTable dtShow = new DataTable();
        dtShow = new TEnvFillPolluteAirLogic().CreateShowDT();
        DataTable dt = new TEnvFillPolluteAirLogic().GetFillData(strWhere, dtShow,
                                       TEnvPEnterinfoVo.T_ENV_P_ENTERINFO_TABLE,
                                       TEnvPPolluteTypeVo.T_ENV_P_POLLUTE_TYPE_TABLE,
                                       TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE,
                                       TEnvPPolluteItemVo.T_ENV_P_POLLUTE_ITEM_TABLE,
                                       TEnvFillPolluteAirVo.T_ENV_FILL_POLLUTE_AIR_TABLE,
                                       TEnvFillPolluteAirItemVo.T_ENV_FILL_POLLUTE_AIR_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_PolluteAir,
                                       SerialType.T_ENV_FILL_PolluteAir_ITEM);
        string json = DataTableToJsonUnsureCol(dt);

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 保存数据
    private void SaveData()
    {
        string ID = Request["id"];
        string updateName = Request["updateName"];
        string value = Request["value"];
        CommonLogic com = new CommonLogic();
        bool result = com.UpdateCommonFill(updateName, value, ID, "");
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion


    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE, "POINT_NAME", "ID", strV);
    }
}