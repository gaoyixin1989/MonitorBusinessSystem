using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.FillQry;

/// <summary>
/// 综合评价统计报表
/// 创建人：魏林
/// 创建时间：2013-08-28
/// </summary>
public partial class Channels_Env_Fill_FillQry_CompreEvalSta : PageBase
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
                    case "GetEvalDict":
                        json = getEvalDict();
                        break;
                    case "GetYear":
                        json = getYearInfo(5, 5);
                        break;
                    case "GetDict":
                        json = getDict();
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
            case "EnvDrinking": //地下饮用水
                Select = "distinct POINT_CODE CODE,POINT_NAME NAME";
                TableName = "T_ENV_P_DRINK_UNDER";
                Where = "YEAR='" + year + "' " + (months == "" ? "" : "and MONTH in(" + months + ")") + " and IS_DEL='0'";
                dt = new FillQryLogic().GetPointInfo(Select, TableName, Where);
                break;
            case "EnvReservoir": //湖库
                Select = "distinct SECTION_CODE CODE,SECTION_NAME NAME";
                TableName = "T_ENV_P_LAKE a inner join T_ENV_P_LAKE_V b on(a.ID=b.SECTION_ID)";
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
        string river_id = Request["river_id"]; //河流ID
        DataTable dt = new DataTable();
        string strDes = string.Empty;

        string months = getMonths(searchType, half, quarter, month, ref strDes);

        dt = new FillQryLogic().GetFillEvalData(type, year, months, point, river_id);

        string json = DataTableToJsonUnsureCol(dt);

        return json;
    }

    #endregion

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict()
    {
        string strDictType = Request["dictType"].ToString();
        
        //return getDictJsonString(strDictType);
        DataTable dt = getDictList(strDictType);
        //加入全部
        DataRow dr = dt.NewRow();
        dr["DICT_TEXT"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);
        string json = DataTableToJson(dt);
        return json;
    }

    private string getEvalDict()
    {
        return "[{\"DICT_CODE\":\"EnvRiver\", \"DICT_TEXT\":\"河流\"},{\"DICT_CODE\":\"EnvDrinking\", \"DICT_TEXT\":\"地下饮用水\"},{\"DICT_CODE\":\"EnvReservoir\", \"DICT_TEXT\":\"湖库\"}]";
    }
}