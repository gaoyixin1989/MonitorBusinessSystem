using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.Env.Fill.FillQry;
using System.Data;

/// <summary>
/// 统计填报数据
/// 创建人：魏林
/// 创建时间：2013-07-05
/// </summary>
public partial class Channels_Env_Fill_FillQry_FillQry : PageBase
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
                    case "GetPoint":
                        json = GetPoint();
                        break;
                    case "GetData":
                        json = GetData();
                        break;
                    case "GetDict":
                        json = getDict();
                        break;
                    case "GetYear":
                        json = getYearInfo(5,5); 
                        break;
                }
                
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }

        }
    }

    #region 获取监测点信息

    private string GetPoint()
    {
        string type = Request["type"];      //监测类别
        string year = Request["year"];      //年度
        string searchType = Request["searchType"];//查询类型
        string half = Request["half"];//半年
        string quarter = Request["quarter"];//季度
        string month = Request["month"];   //月度
        string Select = string.Empty;
        string TableName = string.Empty;
        string Where = string.Empty;
        string MonthWhere = string.Empty;
        DataTable dt = new DataTable();
        string strDes = string.Empty;

        string months = getMonths(searchType, half, quarter, month, ref strDes);

        switch (type)
        {
            case "EnvRiver": //河流
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_RIVER a inner join T_ENV_P_RIVER_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "AreaNoise": //区域环境噪声
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_NOISE_AREA";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvRoadNoise": //道路交通噪声
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_NOISE_ROAD";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "FunctionNoise": //功能区噪声
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_NOISE_FUNCTION";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvAir":   //环境空气
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_AIR";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvDrinking": //地下饮用水
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_DRINK_UNDER";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvDrinkingSource": //饮用水源地
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_DRINK_SRC a inner join T_ENV_P_DRINK_SRC_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvDust": //降尘
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_DUST";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvDWAir": //双三十废气
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_AIR30";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvDWWater": //双三十废水
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_RIVER30 a inner join T_ENV_P_RIVER30_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvEstuaries": //入海河口
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_ESTUARIES a inner join T_ENV_P_ESTUARIES_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvMudRiver": //沉积物（河流）
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_MUD_RIVER a inner join T_ENV_P_MUD_RIVER_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvMudSea": //沉积物（海水）
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_MUD_SEA a inner join T_ENV_P_MUD_SEA_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvSoil": //土壤
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_SOIL";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvPSoild": //固废
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_SOLID";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvRain": //降水
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_RAIN";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvReservoir": //湖库
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_LAKE a inner join T_ENV_P_LAKE_V b on(a.ID=b.SECTION_ID)";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvSeaBath": //海水浴场
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_SEABATH";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvSear": //近岸海域
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_SEA";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvSource": //近岸直排
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_OFFSHORE";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvSpeed": //盐酸盐化速率
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_ALKALI";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvStbc": //生态补偿
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_PAYFOR";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            default:
                dt.Columns.Add("CODE", typeof(String));
                dt.Columns.Add("NAME", typeof(String));
                break;
        }

        //加入全部
        DataRow dr = dt.NewRow();
        dr["NAME"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);
        string json = DataTableToJson(dt);
        return json;
    }

    #endregion

    #region 获取数据信息

    private string GetData()
    {
        string type = Request["type"];      //监测类别
        string year = Request["year"];      //年度
        string searchType = Request["searchType"];//查询类型
        string half = Request["half"];//半年
        string quarter = Request["quarter"];//季度
        string month = Request["month"];   //月度
        string point = Request["point"];    //监测点
        DataTable dt = new DataTable();
        string strDes = string.Empty;

        string months = getMonths(searchType, half, quarter, month, ref strDes);

        dt = new FillQryLogic().GetDataInfo(type, year, months, point, strDes);

        string json = CreateToJson(dt, dt.Rows.Count);

        return json;
    }

    #endregion

    #region//获取月度
    private string getMonths(string type, string half, string quarter, string month, ref string des)
    {
        string months = string.Empty;
        switch (type)
        {
            case "3":  //半度
                switch (half)
                {
                    case "1":
                        months = "1,2,3,4,5,6";
                        des = "上半年";
                        break;
                    case "2":
                        months = "7,8,9,10,11,12";
                        des = "下半年";
                        break;
                    default:
                        months = "";
                        des = "";
                        break;
                }
                break;
            case "2":  //季度 
                switch (quarter)
                {
                    case "1":
                        months = "1,2,3";
                        des = "一季度";
                        break;
                    case "2":
                        months = "4,5,6";
                        des = "二季度";
                        break;
                    case "3":
                        months = "7,8,9";
                        des = "三季度";
                        break;
                    case "4":
                        months = "10,11,12";
                        des = "四季度";
                        break;
                    default:
                        months = "";
                        des = "";
                        break;
                }
                break;
            case "1":  //月度
                months = month.Replace(';', ',');
                des = months;
                break;
        }
        return months;
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
}