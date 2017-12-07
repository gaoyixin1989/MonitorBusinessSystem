using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///SerialType 设置各表的序列号类型 Create By 魏林
/// </summary>
public class SerialType
{
    public SerialType()
    {
    }
    #region 点位序列号
    /// <summary>
    /// 饮用水源地断面序列号
    /// </summary>
    public static string T_ENV_P_DRINK_SRC = "drinkingpoint_id";
    /// <summary>
    /// 饮用水源地断面垂线序列号
    /// </summary>
    public static string T_ENV_P_DRINK_SRC_V = "drinkingpointvertical_id";
    /// <summary>
    /// 饮用水源地垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_DRINK_SRC_V_ITEM = "drinkingpointverticalitem_id";

    /// <summary>
    /// 河流断面序列号
    /// </summary>
    public static string T_ENV_P_RIVER = "riverpoint_id";
    /// <summary>
    /// 河流断面垂线序列号
    /// </summary>
    public static string T_ENV_P_RIVER_V = "riverpointvertical_id";
    /// <summary>
    /// 河流垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_RIVER_V_ITEM = "riverpointverticalitem_id";

    /// <summary>
    /// 城考断面序列号
    /// </summary>
    public static string T_ENV_P_RIVER_CITY = "rivercitypoint_id";
    /// <summary>
    /// 城考断面垂线序列号
    /// </summary>
    public static string T_ENV_P_RIVER_CITY_V = "rivercitypointvertical_id";
    /// <summary>
    /// 城考垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_RIVER_CITY_V_ITEM = "rivercitypointverticalitem_id";

    /// <summary>
    /// 责任目标断面序列号
    /// </summary>
    public static string T_ENV_P_RIVER_TARGET = "rivertargetpoint_id";
    /// <summary>
    /// 责任目标断面垂线序列号
    /// </summary>
    public static string T_ENV_P_RIVER_TARGET_V = "rivertargetpointvertical_id";
    /// <summary>
    /// 责任目标垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_RIVER_TARGET_V_ITEM = "rivertargetpointverticalitem_id";

    /// <summary>
    /// 规划断面序列号
    /// </summary>
    public static string T_ENV_P_RIVER_PLAN = "riverplanpoint_id";
    /// <summary>
    /// 规划断面垂线序列号
    /// </summary>
    public static string T_ENV_P_RIVER_PLAN_V = "riverplanpointvertical_id";
    /// <summary>
    /// 规划垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_RIVER_PLAN_V_ITEM = "riverplanpointverticalitem_id";

    /// <summary>
    /// 湖库断面序列号
    /// </summary>
    public static string T_ENV_P_LAKE = "lakepoint_id";
    /// <summary>
    /// 湖库断面垂线序列号
    /// </summary>
    public static string T_ENV_P_LAKE_V = "lakepointvertical_id";
    /// <summary>
    /// 湖库垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_LAKE_V_ITEM = "lakepointverticalitem_id";

    /// <summary>
    /// 地下饮用水序列号
    /// </summary>
    public static string T_ENV_P_DRINK_UNDER = "drinkunderpoint_id";
    /// <summary>
    /// 地下饮用水监测项目序列号
    /// </summary>
    public static string T_ENV_P_DRINK_UNDER_ITEM = "drinkunderpointverticalitem_id";

    /// <summary>
    /// 生态补偿测点序列号
    /// </summary>
    public static string T_ENV_P_PAYFOR = "payforpoint_id";
    /// <summary>
    /// 生态补偿测点监测项目序列号
    /// </summary>
    public static string T_ENV_P_PAYFOR_ITEM = "payforpointitem_id";

    /// <summary>
    /// 沉积物（河流）断面序列号
    /// </summary>
    public static string T_ENV_P_MUD_RIVER = "mudriverpoint_id";
    /// <summary>
    /// 沉积物（河流）断面垂线序列号
    /// </summary>
    public static string T_ENV_P_MUD_RIVER_V = "mudriverpointvertical_id";
    /// <summary>
    /// 沉积物（河流）垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_MUD_RIVER_V_ITEM = "mudriverpointverticalitem_id";

    /// <summary>
    /// 沉积物（海水）断面序列号
    /// </summary>
    public static string T_ENV_P_MUD_SEA = "mudseapoint_id";
    /// <summary>
    /// 沉积物（海水）断面垂线序列号
    /// </summary>
    public static string T_ENV_P_MUD_SEA_V = "mudseapointvertical_id";
    /// <summary>
    /// 沉积物（海水）垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_MUD_SEA_V_ITEM = "mudseapointverticalitem_id";

    /// <summary>
    /// 土壤序列号
    /// </summary>
    public static string T_ENV_P_SOIL = "soilpoint_id";
    /// <summary>
    /// 土壤监测项目序列号
    /// </summary>
    public static string T_ENV_P_SOIL_ITEM = "soilpointverticalitem_id";

    /// <summary>
    /// 固废序列号
    /// </summary>
    public static string T_ENV_P_SOLID = "solidpoint_id";
    /// <summary>
    /// 固废监测项目序列号
    /// </summary>
    public static string T_ENV_P_SOLID_ITEM = "solidpointverticalitem_id";

    /// <summary>
    /// 功能区噪声序列号
    /// </summary>
    public static string T_ENV_P_NOISE_FUNCTION = "FunctionNoisePoint_Id";
    /// <summary>
    /// 功能区噪声监测项目序列号
    /// </summary>
    public static string T_ENV_P_NOISE_FUNCTION_ITEM = "FunctionNoisePointItem_Id";

    /// <summary>
    /// 区域环境噪声序列号
    /// </summary>
    public static string T_ENV_P_NOISE_AREA = "AreaNoisePoint_Id";
    /// <summary>
    /// 区域环境噪声监测项目序列号
    /// </summary>
    public static string T_ENV_P_NOISE_AREA_ITEM = "AreaNoisePointItem_Id";

    /// <summary>
    /// 道路交通噪声序列号
    /// </summary>
    public static string T_ENV_P_NOISE_ROAD = "RoadNoisePoint_Id";
    /// <summary>
    /// 道路交通噪声监测项目序列号
    /// </summary>
    public static string T_ENV_P_NOISE_ROAD_ITEM = "RoadNoisePointItem_Id";

    /// <summary>
    /// 双三十废水断面序列号
    /// </summary>
    public static string T_ENV_P_RIVER30 = "river30point_id";
    /// <summary>
    /// 双三十废水断面垂线序列号
    /// </summary>
    public static string T_ENV_P_RIVER30_V = "river30pointvertical_id";
    /// <summary>
    /// 双三十废水垂线监测项目序列号
    /// </summary>
    public static string T_ENV_P_RIVER30_V_ITEM = "river30pointverticalitem_id";

    /// <summary>
    /// 双三十废气序列号
    /// </summary>
    public static string T_ENV_P_AIR30 = "air30point_id";
    /// <summary>
    /// 双三十废气监测项目序列号
    /// </summary>
    public static string T_ENV_P_AIR30_ITEM = "air30pointitem_id";

    /// <summary>
    /// 环境空气污染序列号
    /// </summary>
    public static string T_ENV_P_AIR = "airpoint_id";
    /// <summary>
    /// 环境空气污染监测项目序列号
    /// </summary>
    public static string T_ENV_P_AIR_ITEM = "airpointitem_id";

    /// <summary>
    ///  硫酸盐化速率序列号
    /// </summary>
    public static string T_ENV_P_ALKALI = "alkalipoint_id";
    /// <summary>
    ///  硫酸盐化速率监测序列号
    /// </summary>
    public static string T_ENV_P_ALKALI_ITEM = "alkalipointitem_id";

    /// <summary>
    ///  入海河口序列号
    /// </summary>
    public static string T_ENV_P_ESTUARIES = "estuariespoint_id";
    /// <summary>
    ///  入海河口垂线序列号
    /// </summary>
    public static string T_ENV_P_ESTUARIES_V = "estuariespointvertical_id";
    /// <summary>
    ///  入海河口断面垂线监测序列号
    /// </summary>
    public static string T_ENV_P_ESTUARIES_V_ITEM = "estuariespointverticalitem_id";

    /// <summary>
    ///  降尘序列号
    /// </summary>
    public static string T_ENV_P_DUST = "dustpoint_id";
    /// <summary>
    ///  降尘监测序列号
    /// </summary>
    public static string T_ENV_P_DUST_ITEM = "dustpointitem_id";

    /// <summary>
    ///  降水序列号
    /// </summary>
    public static string T_ENV_P_RAIN = "rainpoint_Id";
    /// <summary>
    ///  降水监测序列号
    /// </summary>
    public static string T_ENV_P_RAIN_ITEM = "rainpointitem_Id";

    /// <summary>
    ///  近岸海域序列号
    /// </summary>
    public static string T_ENV_P_SEA = "seapoint_id";
    /// <summary>
    ///  近岸海域监测序列号
    /// </summary>
    public static string T_ENV_P_SEA_ITEM = "seapointitme_id";

    /// <summary>
    ///  海水浴场序列号
    /// </summary>
    public static string T_ENV_P_SEABATH = "seabathpoint_id";
    /// <summary>
    /// 海水浴场监测序列号
    /// </summary>
    public static string T_ENV_P_SEABATH_ITEM = "seabathpointitem_id ";

    /// <summary>
    /// 近岸直排序列号
    /// </summary>
    public static string T_ENV_POINT_OFFSHORE_TABLE = "offshorepoint_id ";
    /// <summary>
    /// 近岸直排监测序列号
    /// </summary>
    public static string T_ENV_POINT_OFFSHORE_TABLE_ITEM = "offshorepointitem_id ";
    /// <summary>
    /// 污染源常规企业序列号
    /// </summary>
    public static string T_ENV_POINT_ENTERINFO = "pollute_enterinfo_id ";
    /// <summary>
    /// 污染源常规类别序列号
    /// </summary>
    public static string T_ENV_POINT_POLLUTETYPE = "pollute_type_id ";
    /// <summary>
    /// 污染源常规监测点序列号
    /// </summary>
    public static string T_ENV_POINT_POLLUTE = "pollute_id ";
    /// <summary>
    /// 污染源常规监测项目序列号
    /// </summary>
    public static string T_ENV_POINT_POLLUTE_ITEM = "pollute_item_id ";

    /// <summary>
    /// 底泥重金属监测点序列号
    /// </summary>
    public static string T_ENV_P_SEDIMENT = "sediment_id";
    /// <summary>
    /// 底泥重金属监测项目序列号
    /// </summary>
    public static string T_ENV_P_SEDIMENT_ITEM = "sediment_item_id";
    
    #endregion

    #region 填报序列号
    /// <summary>
    /// 河流数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER = "river_fill_id";
    /// <summary>
    /// 河流数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_ITEM = "river_fill_item_id";

    /// <summary>
    /// 城考数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_CITY = "river_city_fill_id";
    /// <summary>
    /// 城考数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_CITY_ITEM = "river_city_fill_item_id";

    /// <summary>
    /// 责任目标数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_TARGET = "river_target_fill_id";
    /// <summary>
    /// 责任目标数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_TARGET_ITEM = "river_target_fill_item_id";

    /// <summary>
    /// 规划数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_PLAN = "river_plan_fill_id";
    /// <summary>
    /// 规划数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER_PLAN_ITEM = "river_plan_fill_item_id";

    /// <summary>
    /// 湖库数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_LAKE = "lake_fill_id";
    /// <summary>
    /// 湖库数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_LAKE_ITEM = "lake_fill_item_id";

    /// <summary>
    /// 饮用水源地数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_DRINK_SRC = "drink_src_fill_id";
    /// <summary>
    /// 饮用水源地数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_DRINK_SRC_ITEM = "drink_src_fill_item_id";

    /// <summary>
    /// 沉积物（河流）数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_MUD_RIVER = "mud_river_fill_id";
    /// <summary>
    /// 沉积物（河流）数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_MUD_RIVER_ITEM = "mud_river_fill_item_id";

    /// <summary>
    /// 沉积物（海水）数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_MUD_SEA = "mud_sea_fill_id";
    /// <summary>
    /// 沉积物（海水）数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_MUD_SEA_ITEM = "mud_sea_fill_item_id";

    /// <summary>
    /// 生态补偿数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_PAYFOR = "payfor_fill_id";
    /// <summary>
    /// 生态补偿数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_PAYFOR_ITEM = "payfor_fill_item_id";

    /// <summary>
    /// 地下饮用水数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_DRINK_UNDER = "drink_under_fill_id";
    /// <summary>
    /// 地下饮用水数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_DRINK_UNDER_ITEM = "drink_under_fill_item_id";

    /// <summary>
    /// 土壤数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_SOIL = "soil_fill_id";
    /// <summary>
    /// 土壤数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_SOIL_ITEM = "soil_fill_item_id";

    /// <summary>
    /// 固废数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_SOILD = "soild_fill_id";
    /// <summary>
    /// 固废数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_SOILD_ITEM = "soild_fill_item_id";

    /// <summary>
    /// 入海河口数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_ESTUARIES = "estuaries_fill_id";
    /// <summary>
    /// 入海河口数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_ESTUARIES_ITEM = "estuaries_fill_item_id";

    /// <summary>
    /// 近岸海域数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_SEA = "sea_fill_id";
    /// <summary>
    /// 近岸海域数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_SEA_ITEM = "sea_fill_item_id";

    /// <summary>
    /// 近岸直排数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_OFFSHORE = "offshore_fill_id";
    /// <summary>
    /// 近岸直排数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_OFFSHORE_ITEM = "offshore_fill_item_id";

    /// <summary>
    /// 海水浴场数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_SEABATH = "seabath_fill_id";
    /// <summary>
    /// 海水浴场数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_SEABATH_ITEM = "seabath_fill_item_id";

    /// <summary>
    /// 降尘数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_DUST = "dust_fill_id";
    /// <summary>
    /// 降尘数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_DUST_ITEM = "dust_fill_item_id";

    /// <summary>
    /// 降水数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RAIN = "rain_fill_id";
    /// <summary>
    /// 降水数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RAIN_ITEM = "rain_fill_item_id";

    /// <summary>
    /// 噪声数据填报序列号-道路交通噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_ROAD = "noise_road_fill_id";
    /// <summary>
    /// 噪声数据年度汇总填报序列号-道路交通噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_ROAD_SUMMARY = "noise_road_summary_fill_id";
    /// <summary>
    /// 噪声数据填报监测项目序列号-道路交通噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_ROAD_ITEM = "noise_road_fill_item_id";

    /// <summary>
    /// 噪声数据填报序列号-功能区噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_FUNCTION = "noise_function_fill_id";
    /// <summary>
    /// 噪声数据年度汇总填报序列号-功能区噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_FUNCTION_SUMMARY = "noise_function_summary_fill_id";
    /// <summary>
    /// 噪声数据填报监测项目序列号-功能区噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_FUNCTION_ITEM = "noise_function_fill_item_id";

    /// <summary>
    /// 噪声数据填报序列号-区域环境噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_AREA = "noise_area_fill_id";
    /// <summary>
    /// 噪声数据年度汇总填报序列号-区域环境噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_AREA_SUMMARY = "noise_area_summary_fill_id";
    /// <summary>
    /// 噪声数据填报监测项目序列号-区域环境噪声
    /// </summary>
    public static string T_ENV_FILL_NOISE_AREA_ITEM = "noise_area_fill_item_id";

    /// <summary>
    /// 环境空气(天)数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_AIR = "air_fill_id";
    /// <summary>
    /// 环境空气(天)数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_AIR_ITEM = "air_fill_item_id";

    /// <summary>
    /// 环境空气(小时)数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_AIRHOUR = "airhour_fill_id";
    /// <summary>
    /// 环境空气(小时)数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_AIRHOUR_ITEM = "airhour_fill_item_id";

    /// <summary>
    /// 环境空气(科室)数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_AIRKS = "airks_fill_id";
    /// <summary>
    /// 环境空气(科室)数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_AIRKS_ITEM = "airks_fill_item_id";


    /// <summary>
    /// 硫酸盐化速率数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_ALKALI = "alkali_fill_id";
    /// <summary>
    /// 硫酸盐化速率数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_ALKALI_ITEM = "alkali_fill_item_id";

    /// <summary>
    /// 双三十水数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER30 = "ariver30_fill_id";
    /// <summary>
    /// 双三十水数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_RIVER30_ITEM = "ariver30_fill_item_id";

    /// <summary>
    /// 双三十废气数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_AIR30 = "air30_fill_id";
    /// <summary>
    /// 双三十废气数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_AIR30_ITEM = "air30_fill_item_id";

    /// <summary>
    /// 污染源常规(废水)数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_PolluteWater = "PolluteWater_fill_id";
    /// <summary>
    /// 污染源常规(废水)数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_PolluteWater_ITEM = "PolluteWater_fill_item_id";

    /// <summary>
    /// 污染源常规(废气)数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_PolluteAir = "PolluteAir_fill_id";
    /// <summary>
    /// 污染源常规(废气)数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_PolluteAir_ITEM = "PolluteAir_fill_item_id";

    /// <summary>
    /// 底泥重金属数据填报序列号
    /// </summary>
    public static string T_ENV_FILL_SEDIMENT = "sediment_fill_id";
    /// <summary>
    /// 底泥重金属数据填报监测项目序列号
    /// </summary>
    public static string T_ENV_FILL_SEDIMENT_ITEM = "sediment_fill_item_id";
    #endregion

    #region  监测分析环节的状态号(清远)
    /// <summary>
    /// 采样任务下达
    /// </summary>
    public static string Monitor_000 = "000";
    /// <summary>
    /// 采样任务分配环节
    /// </summary>
    public static string Monitor_001 = "001";
    /// <summary>
    /// 采样环节
    /// </summary>
    public static string Monitor_002 = "002";
    /// <summary>
    /// 现场结果复核环节
    /// </summary>
    public static string Monitor_003 = "003";
    /// <summary>
    /// 现场结果审核环节
    /// </summary>
    public static string Monitor_004 = "004";
    /// <summary>
    /// 样品交接环节
    /// </summary>
    public static string Monitor_005 = "005";
    /// <summary>
    /// 样品分发环节
    /// </summary>
    public static string Monitor_006 = "006";
    /// <summary>
    /// 监测分析环节
    /// </summary>
    public static string Monitor_007 = "007";
    /// <summary>
    /// 分析复核环节
    /// </summary>
    public static string Monitor_008 = "008";
    /// <summary>
    /// 质控审核环节
    /// </summary>
    public static string Monitor_009 = "009";
    /// <summary>
    /// 采样核录环节
    /// </summary>
    public static string Monitor_010 = "010";
    /// <summary>
    /// 分析审核环节
    /// </summary>
    public static string Monitor_011 = "011";
    #endregion

    #region  监测分析环节的状态号(郑州)
    /// <summary>
    /// 采样任务下达
    /// </summary>
    public static string Monitor_ZZ_000 = "000";
    /// <summary>
    /// 采样前质控
    /// </summary>
    public static string Monitor_ZZ_001 = "001";
    /// <summary>
    /// 采样前质控审核
    /// </summary>
    public static string Monitor_ZZ_002 = "002";
    /// <summary>
    /// 采样任务分配
    /// </summary>
    public static string Monitor_ZZ_003 = "003";
    /// <summary>
    /// 采样
    /// </summary>
    public static string Monitor_ZZ_004 = "004";
    /// <summary>
    /// 现场主任审核
    /// </summary>
    public static string Monitor_ZZ_005 = "005";
    /// <summary>
    /// 样品交接
    /// </summary>
    public static string Monitor_ZZ_006 = "006";
    /// <summary>
    /// 分析任务分配
    /// </summary>
    public static string Monitor_ZZ_007 = "007";
    /// <summary>
    /// 监测分析
    /// </summary>
    public static string Monitor_ZZ_008 = "008";
    /// <summary>
    /// 主任复核
    /// </summary>
    public static string Monitor_ZZ_009 = "009";
    /// <summary>
    /// 质量科审核
    /// </summary>
    public static string Monitor_ZZ_010 = "010";
    /// <summary>
    /// 质量负责人审核
    /// </summary>
    public static string Monitor_ZZ_011 = "011";
    /// <summary>
    /// 技术负责人审核
    /// </summary>
    public static string Monitor_ZZ_012 = "012";
    #endregion
}