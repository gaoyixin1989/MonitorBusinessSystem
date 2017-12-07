using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.Rain;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.Rain;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.Rain;
using System.Web.Services;

public partial class Channels_Env_ZZ_Fill_Rain_RainFill : PageBase
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
                    case "GetPoint":
                        GetPoint();
                        break;
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "GetDay":
                        GetDay();
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

    #region 获取监测点
    private void GetPoint()
    {
        string year = Request["year"];
        string month = Request["month"];
        TEnvPointRainVo Dustpoint = new TEnvPointRainVo();//降尘点位
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Rain.TEnvPointRainLogic().SelectByTable(Dustpoint);
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
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";
        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillRainLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE,
                                       TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE,
                                       TEnvFillRainVo.T_ENV_FILL_RAIN_TABLE,
                                       TEnvFillRainItemVo.T_ENV_FILL_RAIN_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_RAIN,
                                       SerialType.T_ENV_FILL_RAIN_ITEM,
                                       "0");
        string json = DataTableToJsonUnsureCol(dt);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 保存数据
    private void SaveData()
    {
        bool result = false;
        string data = "[" + Request["data"] + "]";
        string itemName = Request["itemname"];//列名
        string value = Request["value"];
        string Fill_ID = Request["fill_id"];
        string ConditionID = "";
        CommonLogic com = new CommonLogic();
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 获取日期
    private void GetDay()
    {
        string month = Request["month"];
        string year = Request["year"];
        string json = GetDayByMonth(year, Convert.ToInt32(month));
        //加入“全部”
        json = json.Insert(1, "{\"VALUE\":\"\",\"DAY\":\"--全部--\"},");
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region//获取测点名称
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE, "POINT_NAME", "ID", strV);
    }
    #endregion
}