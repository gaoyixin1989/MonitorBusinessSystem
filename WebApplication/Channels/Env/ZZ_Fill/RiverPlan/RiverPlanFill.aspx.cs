using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Point.RiverPlan;
using i3.ValueObject.Channels.Env.Fill.RiverPlan;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.RiverPlan;

public partial class Channels_Env_ZZ_Fill_RiverPlan_RiverPlanFill : PageBase
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
        DataTable dt = new TEnvFillRiverPlanLogic().PointByTable(year, month);
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
        dtShow = new TEnvFillRiverPlanLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE,
                                       TEnvPRiverPlanVVo.T_ENV_P_RIVER_PLAN_V_TABLE,
                                       TEnvPRiverPlanVItemVo.T_ENV_P_RIVER_PLAN_V_ITEM_TABLE,
                                       TEnvFillRiverPlanVo.T_ENV_FILL_RIVER_PLAN_TABLE,
                                       TEnvFillRiverPlanItemVo.T_ENV_FILL_RIVER_PLAN_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_RIVER_PLAN,
                                       SerialType.T_ENV_FILL_RIVER_PLAN_ITEM,
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
        ConditionID = com.getNameByID(TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE + " a left join " + TEnvFillRiverPlanVo.T_ENV_FILL_RIVER_PLAN_TABLE + " b on(a.ID=b.SECTION_ID)", "CONDITION_ID", "b.ID", ID);
        //特殊处理采样日期
        if (updateName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + ID + "'");
        }
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
        return com.getNameByID(TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE, "SECTION_NAME", "ID", strV);
    }

    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPRiverPlanVVo.T_ENV_P_RIVER_PLAN_V_TABLE, "VERTICAL_NAME", "ID", strV); 
    }
}