using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.NoiseRoad;
using i3.ValueObject.Channels.Env.Point.NoiseRoad;
using i3.BusinessLogic.Channels.Env.Fill.NoiseRoad;
using i3.ValueObject.Channels.Env.Fill.NoiseRoad;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;

/// <summary>
/// 功能描述：道路交通噪声数据填报
/// 创建人：魏林
/// 创建日期：2013-06-27
public partial class Channels_Env_Fill_NoiseRoad_RoadNoiseFill : PageBase
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
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "GetSummary":
                        GetSummary();
                        break;
                    case "operSummary":
                        OperSummary();
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

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillNoiseRoadSummaryVo envFillNoiseRoadSummaryVo = new TEnvFillNoiseRoadSummaryVo();
        envFillNoiseRoadSummaryVo.YEAR = year;
        //envFillNoiseRoadSummaryVo.MONTH = month;

        envFillNoiseRoadSummaryVo = new TEnvFillNoiseRoadSummaryLogic().Details(envFillNoiseRoadSummaryVo);

        string json = "{\"ID\":\"" + envFillNoiseRoadSummaryVo.ID + "\",\"STATUS\":\"" + envFillNoiseRoadSummaryVo.STATUS + "\"}";

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

        //拼写条件语句(注：条件中的列名要与点位上的列名一致)
        if (year != "")
            strWhere += " and YEAR='" + year + "'";

        //填报表需要显示信息(注：这Table里面的第一列数据要与填报表的列名一致)
        DataTable dtShow = new DataTable();
        dtShow = new TEnvFillNoiseRoadLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvFillNoiseRoadSummaryVo.T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE,
                                       TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE,
                                       TEnvPNoiseRoadItemVo.T_ENV_P_NOISE_ROAD_ITEM_TABLE,
                                       TEnvFillNoiseRoadVo.T_ENV_FILL_NOISE_ROAD_TABLE,
                                       TEnvFillNoiseRoadItemVo.T_ENV_FILL_NOISE_ROAD_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_NOISE_ROAD,
                                       SerialType.T_ENV_FILL_NOISE_ROAD_ITEM,
                                       SerialType.T_ENV_FILL_NOISE_ROAD_SUMMARY);

        string json = DataTableToJsonUnsureCol(dt);

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取统计数据

    private void GetSummary()
    {
        string year = Request["year"];

        DataTable dt = new TEnvFillNoiseRoadSummaryLogic().SelectByTable(new TEnvFillNoiseRoadSummaryVo() { YEAR = year });

        string json = LigerGridDataToJson(dt, dt.Rows.Count);

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 保存数据

    private void SaveData()
    {
        bool result = true;
        string ID = Request["id"];
        string updateName = Request["updateName"];
        string value = Request["value"];

        //判断列名是不包含'@',如果包含表示编辑填报表数据时的保存数据，否则表示编辑汇总表时的保存
        if (updateName.Contains("@"))
        {
            CommonLogic com = new CommonLogic();

            result = com.UpdateCommonFill(updateName, value, ID, "");

        }
        else
        {
            if (new TEnvFillNoiseRoadSummaryLogic().UpdateSummary(ID, updateName, value) > 0)
                result = true;
            else
                result = false;
        }

        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 计算汇总信息
    private void OperSummary()
    {
        string year = Request["year"];
        bool result = true;

        if (new TEnvFillNoiseRoadSummaryLogic().OperSummary(year) > 0)
            result = true;
        else
            result = false;

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
        return com.getNameByID(TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE, "POINT_NAME", "ID", strV);
    }

    /// <summary>
    /// 获取道路名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getRoadName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPNoiseRoadVo.T_ENV_P_NOISE_ROAD_TABLE, "ROAD_NAME", "ID", strV);
    }
}

