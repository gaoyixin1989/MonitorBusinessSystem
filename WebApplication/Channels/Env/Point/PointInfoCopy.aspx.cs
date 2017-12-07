using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.BusinessLogic.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.RiverCity;
using i3.BusinessLogic.Channels.Env.Point.RiverTarget;
using i3.BusinessLogic.Channels.Env.Point.RiverPlan;
using i3.BusinessLogic.Channels.Env.Point.Lake;
using i3.BusinessLogic.Channels.Env.Point.DrinkUnder;
using i3.BusinessLogic.Channels.Env.Point.PayFor;
using i3.BusinessLogic.Channels.Env.Point.MudRiver;
using i3.BusinessLogic.Channels.Env.Point.MudSea;
using i3.BusinessLogic.Channels.Env.Point.Soil;
using i3.BusinessLogic.Channels.Env.Point.Solid;
using i3.BusinessLogic.Channels.Env.Point.NoiseFun;
using i3.BusinessLogic.Channels.Env.Point.NoiseArea;
using i3.BusinessLogic.Channels.Env.Point.NoiseRoad;
using i3.BusinessLogic.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Point.Air30;
using i3.BusinessLogic.Channels.Env.Point.Environment;
using i3.BusinessLogic.Channels.Env.Point.Sulfate;
using i3.BusinessLogic.Channels.Env.Point.Estuaries;
using i3.BusinessLogic.Channels.Env.Point.Dust;
using i3.BusinessLogic.Channels.Env.Point.Rain;
using i3.BusinessLogic.Channels.Env.Point.Sea;
using i3.BusinessLogic.Channels.Env.Point.Seabath;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Sediment;

public partial class Channels_Env_Point_PointInfoCopy : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        string strType = "";

        if (Request["type"] != null)
        {
            string TableName = string.Empty;
            string Year_From = string.Empty;
            string Month_From = string.Empty;
            string Year_To = string.Empty;
            string Month_To = string.Empty;
            string strFPointID = string.Empty;
            string strTPointID = string.Empty;
            strType = Request["type"].ToString();
            List<string> list = new List<string>();
            CommonLogic com = new CommonLogic();
            switch (strType)
            {
                //获取年度下拉列表数据
                case "getYearInfo":
                    strResult = getYearInfo();
                    break;
                //获取年度下拉列表数据
                case "getMonthInfo":
                    strResult = getMonthInfo();
                    break;
                //饮用水源地点位复制
                case "copyDrinkSrc":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_DRINK_SRC";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_DRINK_SRC, SerialType.T_ENV_P_DRINK_SRC_V, SerialType.T_ENV_P_DRINK_SRC_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    //strResult = "({result:false,msg:'weilin'})";
                    break;
                //饮用水源地垂线监测项目复制
                case "copyDrinkSrcV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPDrinkSrcLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_DRINK_SRC_V_ITEM);
                    break;
                //河流点位复制
                case "copyRiver":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RIVER";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RIVER, SerialType.T_ENV_P_RIVER_V, SerialType.T_ENV_P_RIVER_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //河流垂线监测项目复制
                case "copyRiverV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPRiverLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RIVER_V_ITEM);
                    break;
                //城考点位复制
                case "copyRiverCity":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RIVER_CITY";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RIVER_CITY, SerialType.T_ENV_P_RIVER_CITY_V, SerialType.T_ENV_P_RIVER_CITY_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //城考垂线监测项目复制
                case "copyRiverCityV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPRiverCityLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RIVER_CITY_V_ITEM);
                    break;
                //责任目标点位复制
                case "copyRiverTarget":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RIVER_TARGET";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RIVER_TARGET, SerialType.T_ENV_P_RIVER_TARGET_V, SerialType.T_ENV_P_RIVER_TARGET_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //责任目标垂线监测项目复制
                case "copyRiverTargetV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPRiverTargetLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RIVER_TARGET_V_ITEM);
                    break;
                //规划断面点位复制
                case "copyRiverPlan":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RIVER_PLAN";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RIVER_PLAN, SerialType.T_ENV_P_RIVER_PLAN_V, SerialType.T_ENV_P_RIVER_PLAN_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //规划断面垂线监测项目复制
                case "copyRiverPlanV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPRiverPlanLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RIVER_PLAN_V_ITEM);
                    break;
                //湖库点位复制
                case "copyLake":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_LAKE";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_LAKE, SerialType.T_ENV_P_LAKE_V, SerialType.T_ENV_P_LAKE_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //湖库垂线监测项目复制
                case "copyLakeV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPLakeLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_LAKE_V_ITEM);
                    break;
                //地下饮用水点位复制
                case "copyDrinkUnder":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_DRINK_UNDER";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_DRINK_UNDER, "", SerialType.T_ENV_P_DRINK_UNDER_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //地下饮用水监测项目复制
                case "copyDrinkUnderV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPDrinkUnderLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_DRINK_UNDER_ITEM);
                    break;
                //**************************何海亮********************************************//
                //底泥重金属点位复制
                case "copySediment":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_SEDIMENT";
                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_SEDIMENT, "", SerialType.T_ENV_P_SEDIMENT_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //底泥重金属监测项目复制
                case "copySedimentV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPSedimentLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_SEDIMENT_ITEM);
                    break;
                //*****************************************************************************//
                //生态补偿点位复制
                case "copyPayfor":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_PAYFOR";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_PAYFOR, "", SerialType.T_ENV_P_PAYFOR_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //生态补偿监测项目复制
                case "copyPayforV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPPayforLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_PAYFOR_ITEM);
                    break;
                //沉积物（河流）点位复制
                case "copyMudRiver":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_MUD_RIVER";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_MUD_RIVER, SerialType.T_ENV_P_MUD_RIVER_V, SerialType.T_ENV_P_MUD_RIVER_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //沉积物（河流）监测项目复制
                case "copyMudRiverV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPMudRiverLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_MUD_RIVER_V_ITEM);
                    break;
                //沉积物（海水）点位复制
                case "copyMudSea":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_MUD_SEA";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_MUD_SEA, SerialType.T_ENV_P_MUD_SEA_V, SerialType.T_ENV_P_MUD_SEA_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //沉积物（海水）监测项目复制
                case "copyMudSeaV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPMudSeaLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_MUD_SEA_V_ITEM);
                    break;
                //土壤点位复制
                case "copySoil":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_SOIL";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_SOIL, "", SerialType.T_ENV_P_SOIL_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //土壤监测项目复制
                case "copySoilV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPSoilLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_SOIL_ITEM);
                    break;
                //固废点位复制
                case "copySolid":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_SOLID";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_SOLID, "", SerialType.T_ENV_P_SOLID_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //固废监测项目复制
                case "copySolidV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPSolidLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_SOLID_ITEM);
                    break;
                //功能区噪声点位复制
                case "copyFunctionNoise":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_NOISE_FUNCTION";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_NOISE_FUNCTION, "", SerialType.T_ENV_P_NOISE_FUNCTION_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //功能区噪声监测项目复制
                case "copyFunctionNoiseV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPNoiseFunctionLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_NOISE_FUNCTION_ITEM);
                    break;
                //区域环境噪声点位复制
                case "copyAreaNoise":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_NOISE_AREA";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_NOISE_AREA, "", SerialType.T_ENV_P_NOISE_AREA_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //区域环境噪声监测项目复制
                case "copyAreaNoiseV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPNoiseAreaLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_NOISE_AREA_ITEM);
                    break;
                //道路交通噪声点位复制
                case "copyRoadNoise":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_NOISE_ROAD";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_NOISE_ROAD, "", SerialType.T_ENV_P_NOISE_ROAD_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //道路交通噪声监测项目复制
                case "copyRoadNoiseV":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPNoiseRoadLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_NOISE_ROAD_ITEM);
                    break;
                //双三十废水点位复制
                case "copyRiver30":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RIVER30";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RIVER30, SerialType.T_ENV_P_RIVER30_V, SerialType.T_ENV_P_RIVER30_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //双三十废水垂线监测项目复制
                case "copyRiver30V":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPRiver30Logic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RIVER30_V_ITEM);
                    break;
                //双三十废气点位复制
                case "copyAir30":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_AIR30";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_AIR30, "", SerialType.T_ENV_P_AIR30_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //双三十废气监测项目复制
                case "copyAir30V":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPAir30Logic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_AIR30_ITEM);
                    break;
                //环境空气复制
                case "copyEnvironment":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_AIR";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_AIR, "", SerialType.T_ENV_P_AIR_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //环境空气监测复制
                case "EnvironmentEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new T_ENV_P_AIR().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_AIR_ITEM);
                    break;
                //碳酸盐化速率复制
                case "SulFateSpeed":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_ALKALI";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_ALKALI, "", SerialType.T_ENV_P_ALKALI_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //碳酸盐化速率监测复制
                case "copySulFateSpeedEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPAlkaliLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_ALKALI_ITEM);
                    break;
                //入海河口复制
                case "copyEstuariesList":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_ESTUARIES";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_ESTUARIES, SerialType.T_ENV_P_ESTUARIES_V, SerialType.T_ENV_P_ESTUARIES_V_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //入海河口监测复制
                case "copyEstuariesVertEdit":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPointEstuariesLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_ALKALI_ITEM);
                    break;
                //降尘复制
                case "copyDust":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_DUST";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_DUST, "", SerialType.T_ENV_P_DUST_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //降尘监测赋值
                case "CopyDust_Item":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPointDustItemLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_DUST_ITEM);
                    break;
                //降水复制
                case "copyRain":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_RAIN";

                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_RAIN, "", SerialType.T_ENV_P_RAIN_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //降水监测复制
                case "CopyRain_Item":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPointRainLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_RAIN_ITEM);
                    break;
                //近水海域复制
                case "copySealList": 
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_SEA";
                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_SEA, "", SerialType.T_ENV_P_SEA_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //近水海域监测复制
                case "copySeaEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPointSeaLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_SEA_ITEM);
                    break;
                //海水浴场复制
                case "copySeabathList":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_SEABATH";
                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_P_SEABATH, "", SerialType.T_ENV_P_SEABATH_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //海水浴场监测复制
                case "CopySeabathEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new TEnvPSeabathLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_P_SEABATH_ITEM);
                    break;
                //近岸直排复制
                case "copyOffshoreList":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_OFFSHORE";
                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_POINT_OFFSHORE_TABLE, "", SerialType.T_ENV_POINT_OFFSHORE_TABLE_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //近岸直排监测复制
                case "CopyOffshoreEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new i3.BusinessLogic.Channels.Env.Point.Offshore.TEnvPointOffshoreLogic().PasteItem(strFPointID, strTPointID, SerialType.T_ENV_POINT_OFFSHORE_TABLE_ITEM);
                    break;
                //污染源常规监测项目复制
                case "copyPolluteItem":
                    Year_From = Request["Year_From"].ToString();
                    Month_From = Request["Month_From"].ToString();
                    Year_To = Request["Year_To"].ToString();
                    Month_To = Request["Month_To"].ToString();
                    TableName = "T_ENV_P_POLLUTE";
                    strResult = com.CopyPointData(TableName, SerialType.T_ENV_POINT_POLLUTE, "", SerialType.T_ENV_POINT_POLLUTE_ITEM, Year_From, Month_From, Year_To, Month_To);
                    break;
                //污染源常规监测项目复制
                case "copyPolluteItemEditer":
                    strFPointID = Request["fromID"].ToString();
                    strTPointID = Request["toID"].ToString();
                    strResult = new i3.BusinessLogic.Channels.Env.Point.PolluteRule.TEnvPPolluteItemLogic().PasteItem(strFPointID, strTPointID,SerialType.T_ENV_POINT_POLLUTE_ITEM);
                    break;

                default:
                    break;
            }
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 判断复制到的年月是否已经存在点位数据
    /// </summary>
    /// <param name="strTable">点位表名</param>
    /// <param name="strYear">年份</param>
    /// <param name="strMonth">月份</param>
    /// <returns></returns>
    [WebMethod]
    public static string isExistData(string strTable, string strYear, string strMonth)
    {
        CommonLogic com = new CommonLogic();

        bool isSuccess = com.ExistPointData(strTable, strYear, strMonth);

        return isSuccess == true ? "1" : "0";
    }
}