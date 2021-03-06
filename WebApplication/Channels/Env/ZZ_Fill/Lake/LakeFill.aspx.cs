﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.Lake;
using i3.BusinessLogic.Channels.Env.Fill.Lake;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Point.Lake;
using i3.ValueObject.Channels.Env.Fill.Lake;
using System.Web.Services;

public partial class Channels_Env_ZZ_Fill_Lake_LakeFill :PageBase
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
                    case "GetPoint":
                        GetPoint();
                        break;
                    case "GetData":
                        GetData();
                        break;
                    case "GetDay":
                        GetDay();
                        break;
                    case "SaveData":
                        SaveData();
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
        DataTable dt = new TEnvPLakeLogic().PointByTable(year, month);
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
        dtShow = new TEnvFillLakeLogic().CreateShowDT_ZZ();
         
        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvPLakeVo.T_ENV_P_LAKE_TABLE,
                                       TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE,
                                       TEnvPLakeVItemVo.T_ENV_P_LAKE_V_ITEM_TABLE,
                                       TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE,
                                       TEnvFillLakeItemVo.T_ENV_FILL_LAKE_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_LAKE,
                                       SerialType.T_ENV_FILL_LAKE_ITEM,
                                       "1");

        string json = DataTableToJsonUnsureCol(dt);
        Response.ContentType = "application/json";
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

    #region 保存数据

    private void SaveData()
    {
        string ID = Request["id"];
        string updateName = Request["updateName"];
        string value = Request["value"];
        string ConditionID = "";

        CommonLogic com = new CommonLogic();
        //获取测点的评价标准ID
        ConditionID = com.getNameByID(TEnvPLakeVo.T_ENV_P_LAKE_TABLE + " a left join " + TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE + " b on(a.ID=b.SECTION_ID)", "CONDITION_ID", "b.ID", ID);
        //特殊处理采样日期
        if (updateName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + ID + "'");
        }

        bool result = com.UpdateCommonFill(updateName, value, ID, ConditionID);

        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
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
        return com.getNameByID(TEnvPLakeVo.T_ENV_P_LAKE_TABLE, "SECTION_NAME", "ID", strV);
    }
    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE, "VERTICAL_NAME", "ID", strV);
    }
}