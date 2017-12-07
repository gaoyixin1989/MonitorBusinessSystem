<%@ WebHandler Language="C#" Class="FillFlowHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 创建人：魏林
/// 创建时间：2013-08-26
/// 原因：数据填报流程后台处理方法
/// </summary>
public class FillFlowHandler : PageBase, IHttpHandler, IRequiresSessionState {

    public string json = "";
    public string action = "";
    public string pf_id = "";
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        GetRequestParme(context);

        switch (action)
        {
            case "GetData":
                if (pf_id.Length > 0)
                {
                    DataTable dt = new DataTable();
                    string[] str = pf_id.Split('^');
                    string type = str[0].ToString();
                    string year = str[1].ToString();
                    string month = str[2].ToString();
                    string strTable = string.Empty;
                    string strItemTable = string.Empty;
                    string strPointTable = string.Empty;
                    string m = "";
                    switch (type)
                    {
                        case "HL":             //河流
                            strTable = "T_ENV_FILL_RIVER";
                            strItemTable = "T_ENV_FILL_RIVER_ITEM";
                            strPointTable = "T_ENV_P_RIVER";
                            m = "1";
                            break;
                        case "DX":             //地下饮用水
                            strTable = "T_ENV_FILL_DRINK_UNDER";
                            strItemTable = "T_ENV_FILL_DRINK_UNDER_ITEM";
                            strPointTable = "T_ENV_P_DRINK_UNDER";
                            m = "2";
                            break;
                        case "HK":             //湖库
                            strTable = "T_ENV_FILL_LAKE";
                            strItemTable = "T_ENV_FILL_LAKE_ITEM";
                            strPointTable = "T_ENV_P_LAKE";
                            m = "1";
                            break;
                        case "DS":            //饮用水源地
                            strTable = "T_ENV_FILL_DRINK_SRC";
                            strItemTable = "T_ENV_FILL_DRINK_SRC_ITEM";
                            strPointTable = "T_ENV_P_DRINK_SRC";
                            m = "1";
                            break;
                        case "AZ":             //空气（自动室）
                            strTable = "T_ENV_FILL_AIR";
                            strItemTable = "T_ENV_FILL_AIR_ITEM";
                            strPointTable = "T_ENV_P_AIR";
                            m = "2";
                            break;
                        case "AK":            //空气（科室）
                            strTable = "T_ENV_FILL_AIRKS";
                            strItemTable = "T_ENV_FILL_AIRKS_ITEM";
                            strPointTable = "T_ENV_P_AIR";
                            m = "2";
                            break;
                        case "AH":             //空气（小时）
                            strTable = "T_ENV_FILL_AIRHOUR";
                            strItemTable = "T_ENV_FILL_AIRHOUR_ITEM";
                            strPointTable = "T_ENV_P_AIR";
                            m = "2";
                            break;
                        case "AL":            //硫酸盐化速率
                            strTable = "T_ENV_FILL_ALKALI";
                            strItemTable = "T_ENV_FILL_ALKALI_ITEM";
                            strPointTable = "T_ENV_P_ALKALI";
                            m = "2";
                            break;
                        case "NA":             //区域环境噪声
                            strTable = "T_ENV_FILL_NOISE_AREA";
                            strItemTable = "T_ENV_FILL_NOISE_AREA_ITEM";
                            strPointTable = "T_ENV_P_NOISE_AREA";
                            m = "2";
                            break;
                        case "NR":            //道路交通噪声
                            strTable = "T_ENV_FILL_NOISE_ROAD";
                            strItemTable = "T_ENV_FILL_NOISE_ROAD_ITEM";
                            strPointTable = "T_ENV_P_NOISE_ROAD";
                            m = "2";
                            break;
                        case "NF":             //功能区噪声
                            strTable = "T_ENV_FILL_NOISE_FUNCTION";
                            strItemTable = "T_ENV_FILL_NOISE_FUNCTION_ITEM";
                            strPointTable = "T_ENV_P_NOISE_FUNCTION";
                            m = "2";
                            break;
                        case "SE":            //近岸海域
                            strTable = "T_ENV_FILL_SEA";
                            strItemTable = "T_ENV_FILL_SEA_ITEM";
                            strPointTable = "T_ENV_P_SEA";
                            m = "2";
                            break;
                        case "ET":             //入海河口
                            strTable = "T_ENV_FILL_ESTUARIES";
                            strItemTable = "T_ENV_FILL_ESTUARIES_ITEM";
                            strPointTable = "T_ENV_P_ESTUARIES";
                            m = "1";
                            break;
                        case "OF":            //近岸直排
                            strTable = "T_ENV_FILL_OFFSHORE";
                            strItemTable = "T_ENV_FILL_OFFSHORE_ITEM";
                            strPointTable = "T_ENV_P_OFFSHORE";
                            m = "2";
                            break;
                        case "SB":             //海水浴场
                            strTable = "T_ENV_FILL_SEABATH";
                            strItemTable = "T_ENV_FILL_SEABATH_ITEM";
                            strPointTable = "T_ENV_P_SEABATH";
                            m = "2";
                            break;
                        case "RA":             //降水
                            strTable = "T_ENV_FILL_RAIN";
                            strItemTable = "T_ENV_FILL_RAIN_ITEM";
                            strPointTable = "T_ENV_P_RAIN";
                            m = "2";
                            break;
                        case "DU":             //降尘
                            strTable = "T_ENV_FILL_DUST";
                            strItemTable = "T_ENV_FILL_DUST_ITEM";
                            strPointTable = "T_ENV_P_DUST";
                            m = "2";
                            break;
                        case "SI":             //土壤
                            strTable = "T_ENV_FILL_SOIL";
                            strItemTable = "T_ENV_FILL_SOIL_ITEM";
                            strPointTable = "T_ENV_P_SOIL";
                            m = "2";
                            break;
                        case "SL":             //固废
                            strTable = "T_ENV_FILL_SOLID";
                            strItemTable = "T_ENV_FILL_SOLID_ITEM";
                            strPointTable = "T_ENV_P_SOLID";
                            m = "2";
                            break;
                        case "PF":             //生态补偿
                            strTable = "T_ENV_FILL_PAYFOR";
                            strItemTable = "T_ENV_FILL_PAYFOR_ITEM";
                            strPointTable = "T_ENV_P_PAYFOR";
                            m = "2";
                            break;
                        case "RR":             //双三十水质
                            strTable = "T_ENV_FILL_RIVER30";
                            strItemTable = "T_ENV_FILL_RIVER30_ITEM";
                            strPointTable = "T_ENV_P_RIVER30";
                            m = "1";
                            break;
                        case "AA":             //双三十空气
                            strTable = "T_ENV_FILL_AIR30";
                            strItemTable = "T_ENV_FILL_AIR30_ITEM";
                            strPointTable = "T_ENV_P_AIR30";
                            m = "2";
                            break;
                        case "MR":             //沉积物（河流）
                            strTable = "T_ENV_FILL_MUD_RIVER";
                            strItemTable = "T_ENV_FILL_MUD_RIVER_ITEM";
                            strPointTable = "T_ENV_P_MUD_RIVER";
                            m = "1";
                            break;
                        case "MS":             //沉积物（海水）
                            strTable = "T_ENV_FILL_MUD_SEA";
                            strItemTable = "T_ENV_FILL_MUD_SEA_ITEM";
                            strPointTable = "T_ENV_P_MUD_SEA";
                            m = "1";
                            break;
                    }
                    dt = new CommonLogic().GetFillValue(year, month, strTable, strItemTable, strPointTable, m);

                    json = DataTableToJsonUnsureCol(dt);
                }
                break;
            default:
                break;
        }
        
        context.Response.Write(json);
        context.Response.End();
    }

    /// <summary>
    /// 获取URL参数
    /// </summary>
    /// <param name="context"></param>
    private void GetRequestParme(HttpContext context)
    {
        //业务参数相关
        if (!String.IsNullOrEmpty(context.Request.Params["pf_id"]))
        {
            pf_id = context.Request.Params["pf_id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            action = context.Request.Params["action"].Trim();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}