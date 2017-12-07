using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Seabath;
using i3.BusinessLogic.Channels.Env.Fill.Seabath;
using i3.ValueObject.Channels.Env.Fill.Seabath;

public partial class Channels_Env_Fill_Seabath_SeabathFill : PageBase
{
    /// <summary>
    /// 功能描述：海水浴场数据填报
    /// 创建人：刘静楠
    /// 创建日期：2013/6/19 
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "GetYear":
                        GetYear();
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
        string yearResult = getYearInfo();

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(yearResult);
        Response.End();
    }

    #endregion

    #region// 获取监测点(ljn,2013/6/25)
    private void GetPoint()
    {
        string year = Request["year"];
        string month = Request["month"];  
        TEnvPSeabathVo Dustpoint = new TEnvPSeabathVo();//降尘点位
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Seabath.TEnvPSeabathLogic().SelectByTable(Dustpoint);
        //加入全部
        DataRow dr = dt.NewRow();
        dr["POINT_NAME"] = "--全部--";
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

        TEnvFillSeabathVo envFillSeabathVo = new TEnvFillSeabathVo();
        envFillSeabathVo.YEAR = year;
        envFillSeabathVo.MONTH = month;

        envFillSeabathVo = new TEnvFillSeabathLogic().Details(envFillSeabathVo);

        string json = "{\"ID\":\"" + envFillSeabathVo.ID + "\",\"STATUS\":\"" + envFillSeabathVo.STATUS + "\"}";

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

        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillSeabathLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE,
                                       TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE,
                                       TEnvFillSeabathVo.T_ENV_FILL_SEABATH_TABLE,
                                       TEnvFillSeabathItemVo.T_ENV_FILL_SEABATH_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_SEABATH,
                                       SerialType.T_ENV_FILL_SEABATH_ITEM,
                                       "0");
        string json = DataTableToJsonUnsureCol(dt);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }


    #endregion

    #region 保存数据

    private void SaveData()
    {
        bool result = false;
        string data = "[" + Request["data"] + "]";
        string itemName = Request["itemname"];//列名
        string value = Request["value"];
        string Fill_ID = Request["fill_id"];
        string ConditionID = "";
        CommonLogic com = new CommonLogic();
        //特殊处理采样日期
        if (itemName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvFillSeabathVo.T_ENV_FILL_SEABATH_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + Fill_ID + "'");
        }
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
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
        return com.getNameByID(TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE, "POINT_NAME", "ID", strV);
    }
}