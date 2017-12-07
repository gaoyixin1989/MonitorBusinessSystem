using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.NoiseFun;
using i3.ValueObject.Channels.Env.Point.NoiseFun;
using i3.BusinessLogic.Channels.Env.Fill.NoiseFun;
using i3.ValueObject.Channels.Env.Fill.NoiseFun;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;

/// <summary>
/// 功能描述：功能区噪声数据填报
/// 创建人：魏林
/// 创建日期：2013-06-26
public partial class Channels_Env_Fill_NoiseFun_FunctionNoiseFill : PageBase
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
                    case "GetSeason":
                        GetSeason();
                        break;
                    case "GetFunctionArea":
                        GetFunctionArea();
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
                    case "GetSummary":
                        GetSummary();
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

    #region 获取季度

    private void GetSeason()
    {
        string json = getSeasonInfo();

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取功能区

    private void GetFunctionArea()
    {
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(getDictJsonString("Function_Area"));
        Response.End();
    }

    #endregion

    #region 获取监测点

    private void GetPoint()
    {
        string year = Request["year"];
        string month = Request["month"];
        string functionAreaCode = Request["functionAreaCode"];

        DataTable dt = new TEnvPNoiseFunctionLogic().PointByTable(year, month, functionAreaCode);

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
        string quter = Request["quter"];

        TEnvFillNoiseFunctionSummaryVo envFillNoiseFunctionSummaryVo = new TEnvFillNoiseFunctionSummaryVo();
        envFillNoiseFunctionSummaryVo.YEAR = year;
        envFillNoiseFunctionSummaryVo.QUTER = quter;

        envFillNoiseFunctionSummaryVo = new TEnvFillNoiseFunctionSummaryLogic().Details(envFillNoiseFunctionSummaryVo);

        string json = "{\"ID\":\"" + envFillNoiseFunctionSummaryVo.ID + "\",\"STATUS\":\"" + envFillNoiseFunctionSummaryVo.STATUS + "\"}";

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
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

    #region 获取数据

    private void GetData()
    {
        string strWhere = "1=1";
        string pointId = Request["pointId"];

        //拼写条件语句(注：条件中的列名要与点位上的列名一致)
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";

        //填报表需要显示信息(注：这Table里面的第一列数据要与填报表的列名一致)
        DataTable dtShow = new DataTable();
        dtShow = new TEnvFillNoiseFunctionLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvFillNoiseFunctionSummaryVo.T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE,
                                       TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE,
                                       TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE,
                                       TEnvFillNoiseFunctionVo.T_ENV_FILL_NOISE_FUNCTION_TABLE,
                                       TEnvFillNoiseFunctionItemVo.T_ENV_FILL_NOISE_FUNCTION_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_NOISE_FUNCTION,
                                       SerialType.T_ENV_FILL_NOISE_FUNCTION_ITEM,
                                       SerialType.T_ENV_FILL_NOISE_FUNCTION_SUMMARY);
        
        string json = DataTableToJsonUnsureCol(dt);

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取统计数据

    private void GetSummary()
    {
        string pointId = Request["pointId"];

        DataTable dt = new TEnvFillNoiseFunctionSummaryLogic().SelectByTable(new TEnvFillNoiseFunctionSummaryVo() { POINT_CODE = pointId });

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
        string PointID = Request["pointId"];

        //判断列名是不包含'@',如果包含表示编辑填报表数据时的保存数据，否则表示编辑汇总表时的保存
        if (updateName.Contains("@"))
        {
            CommonLogic com = new CommonLogic();

            result = com.UpdateCommonFill(updateName, value, ID, "");

            if (result)
            {
                string[] str = updateName.Split('@');
                //如果更新保存的值是LEQ时，则更新功能区噪声汇总表数据
                if (str[2].ToUpper().TrimEnd().TrimStart()=="LEQ")
                {
                    
                    int n = new TEnvFillNoiseFunctionSummaryLogic().UpdateFunctionSummary(PointID, str[1].ToString());
                }
            }
        }
        else
        {
            if (new TEnvFillNoiseFunctionSummaryLogic().UpdateSummary(ID, updateName, value) > 0)
                result = true;
            else
                result = false;
        }

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
        return com.getNameByID(TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE, "POINT_NAME", "ID", strV);
    }
}