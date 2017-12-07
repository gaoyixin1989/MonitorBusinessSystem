using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.River;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Fill.River;
using i3.ValueObject.Channels.Env.Fill.River;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;

/// <summary>
/// 功能描述：河流数据填报
/// 创建人：魏林
/// 创建日期：2013-06-19
public partial class Channels_Env_Fill_River_RiverFill : PageBase
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
                    case "GetDict":
                        getDict(Request["dictType"].ToString());
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
                    case "ModifyJUDGE":
                        ModifyJUDGE();
                        break;
                    case "GetFillID":
                        GetFillID();
                        break;
                    case "GetBcData":
                        GetBcData();
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
        DataTable dt = new TEnvPRiverLogic().PointByTable(year, month);

        //加入全部
        DataRow dr = dt.NewRow();
        dr["SECTION_NAME"] = "--全部--";
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

        TEnvFillRiverVo envFillRiverVo = new TEnvFillRiverVo();
        envFillRiverVo.YEAR = year;
        envFillRiverVo.MONTH = month;

        envFillRiverVo = new TEnvFillRiverLogic().Details(envFillRiverVo);

        string json = "{\"ID\":\"" + envFillRiverVo.ID + "\",\"STATUS\":\"" + envFillRiverVo.STATUS + "\"}";

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取填报数据

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
        dtShow = new TEnvFillRiverLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvPRiverVo.T_ENV_P_RIVER_TABLE,
                                       TEnvPRiverVVo.T_ENV_P_RIVER_V_TABLE,
                                       TEnvPRiverVItemVo.T_ENV_P_RIVER_V_ITEM_TABLE,
                                       TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE,
                                       TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_RIVER,
                                       SerialType.T_ENV_FILL_RIVER_ITEM,
                                       "1");
        
        //DataTable dt = new TEnvFillRiverLogic().GetRiverFillData(year, month, pointId, ref dtAllItem, unSureMark);
        string json = DataTableToJsonUnsureCol(dt);
 
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取补测数据

    private void GetBcData()
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
        dtShow = new TEnvFillRiverLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillBcData(strWhere,
                                       dtShow,
                                       TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE,
                                       TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE,
                                       "1");

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
        string ConditionID = "";
        
        CommonLogic com = new CommonLogic();
        //获取测点的评价标准ID
        ConditionID = com.getNameByID(TEnvPRiverVo.T_ENV_P_RIVER_TABLE + " a left join " + TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE + " b on(a.ID=b.SECTION_ID)", "CONDITION_ID", "b.ID", ID);
        //特殊处理采样日期
        if (updateName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + ID + "'");
        }
        //bool result = com.SaveFillData(dtData, TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE, TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE, unSureMark, ref strFillID, SerialType.T_ENV_FILL_RIVER, SerialType.T_ENV_FILL_RIVER_ITEM);
        bool result = com.UpdateCommonFill(updateName, value, ID, ConditionID);

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

    
    #region 获取下拉字典项
    private void getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);
        Response.ContentType = "application/json";
        Response.Write(strJson);
        Response.End();
    }
    #endregion

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 获取断面名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getSectionName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPRiverVo.T_ENV_P_RIVER_TABLE, "SECTION_NAME", "ID", strV);
    }

    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPRiverVVo.T_ENV_P_RIVER_V_TABLE, "VERTICAL_NAME", "ID", strV);
    }

    /// <summary>
    /// 更新当前数据监测项的评价值
    /// </summary>
    private void ModifyJUDGE()
    {
        CommonLogic com = new CommonLogic();
        string strFillIDs = Request["ids"].ToString().TrimEnd(',');

        bool result = com.ModifyJUDGE(strFillIDs, TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE, TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE, TEnvPRiverVo.T_ENV_P_RIVER_TABLE, TEnvFillRiverVo.SECTION_ID_FIELD);

        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }
}