using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.PayFor;
using i3.ValueObject.Channels.Env.Point.PayFor;
using i3.BusinessLogic.Channels.Env.Fill.PayFor;
using i3.ValueObject.Channels.Env.Fill.PayFor;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;

/// <summary>
/// 功能描述：生态补偿数据填报
/// 创建人：魏林
/// 创建日期：2013-06-24
public partial class Channels_Env_Fill_Payfor_PayforFill : PageBase
{
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
                    case "GetDay":
                        GetDay();
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
        DataTable dt = new TEnvPPayforLogic().PointByTable(year, month);

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

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillPayforVo envFillPayforVo = new TEnvFillPayforVo();
        envFillPayforVo.YEAR = year;
        envFillPayforVo.MONTH = month;

        envFillPayforVo = new TEnvFillPayforLogic().Details(envFillPayforVo);

        string json = "{\"ID\":\"" + envFillPayforVo.ID + "\",\"STATUS\":\"" + envFillPayforVo.STATUS + "\"}";

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
        dtShow = new TEnvFillPayforLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE,
                                       TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE,
                                       TEnvFillPayforVo.T_ENV_FILL_PAYFOR_TABLE,
                                       TEnvFillPayforItemVo.T_ENV_FILL_PAYFOR_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_PAYFOR,
                                       SerialType.T_ENV_FILL_PAYFOR_ITEM,
                                       "0");
        
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
        //特殊处理采样日期
        if (updateName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvFillPayforVo.T_ENV_FILL_PAYFOR_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + ID + "'");
        }
        
        bool result = com.UpdateCommonFill(updateName, value, ID, "");

        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 获取日期

    private void GetDay()
    {
        string year = Request["year"];
        string month = Request["month"];

        string json = GetDayByMonth(year, Convert.ToInt32(month));

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
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
        return com.getNameByID(TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE, "POINT_NAME", "ID", strV);
    }
}