using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：根据监测类型获取点位信息，仅为下拉框 弹出grid使用
/// 创建日期：2014-02-18
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Base_Item_SelectPoint : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetPoints")
        {
            GetPoints();
            Response.End();
        }
    }

    //获取点位信息，仅为下拉框 弹出grid使用
    private void GetPoints()
    {
        string strSrhPointName = (Request.Params["strSrhPointName"] != null) ? Request.Params["strSrhPointName"] : "";
        string strMONITOR_TYPE = Request.Params["MONITOR_TYPE"].ToString();
        string strTableName = "";
        string strCode = "";
        string strName = "";

        switch (strMONITOR_TYPE)
        {
            case "EnvStbc":              //生态补偿
                strTableName = "T_ENV_P_PAYFOR";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvReservoir":         //湖库
                strTableName = "T_ENV_P_LAKE";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvDrinkingSource":    //饮用水源地(湖库、河流)
                strTableName = "T_ENV_P_DRINK_SRC";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvMudRiver":         //沉积物（河流）
                strTableName = "T_ENV_P_MUD_RIVER";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvSoil":             //土壤
                strTableName = "T_ENV_P_SOIL";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvPSoil":            //固废
                strTableName = "T_ENV_P_SOLID";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvRiverCity":        //城考
                strTableName = "T_ENV_P_RIVER_CITY";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvRiverTarget":      //责任目标
                strTableName = "T_ENV_P_RIVER_TARGET";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvRiverPlan":        //规划断面
                strTableName = "T_ENV_P_RIVER_PLAN";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvRoadNoise":        //道路交通噪声
                strTableName = "T_ENV_P_NOISE_ROAD";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "FunctionNoise":       //功能区噪声
                strTableName = "T_ENV_P_NOISE_FUNCTION";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "AreaNoise":           //区域噪声环境
                strTableName = "T_ENV_P_NOISE_AREA";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvDust":             //降尘
                strTableName = "T_ENV_P_DUST";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvRiver":            //河流
                strTableName = "T_ENV_P_RIVER";
                strCode = "SECTION_CODE";
                strName = "SECTION_NAME";
                break;
            case "EnvDrinking":         //地下饮用水
                strTableName = "T_ENV_P_DRINK_UNDER";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvRain":             //降水
                strTableName = "T_ENV_P_RAIN";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvAir":              //环境空气
                strTableName = "T_ENV_P_AIR";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            case "EnvSpeed":            //盐酸盐化速率
                strTableName = "T_ENV_P_ALKALI";
                strCode = "POINT_CODE";
                strName = "POINT_NAME";
                break;
            default:
                break;
        }

        CommonLogic common = new CommonLogic();

        DataTable dt = common.getPointInfo(strTableName, strCode, strName, strSrhPointName);

        string strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
    }
}