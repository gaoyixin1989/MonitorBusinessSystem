using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using System.IO;
using System.Data.OleDb;
using i3.BusinessLogic.Channels.Env.Fill.AIR30;
using i3.ValueObject.Channels.Env.Point.Air30;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.AIR30;
using i3.ValueObject.Channels.Env.Fill.Air30;

/// <summary>
/// 功能描述：双三十空气数据填报
/// 创建人：潘德军
/// 创建日期：2013-05-08
/// 修改人：刘静楠
/// 时间：2013-6-25
/// </summary>
public partial class Channels_Env_Fill_Air30_Air30Fill : PageBase
{
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
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "GetEnvConfig":
                        //GetEnvConfig();
                        break;
                    case "DataImport":
                        //DataImport();
                        break;
                    case "DataExport":
                        //DataExport();
                        break;
                    case "GetPoint":
                        GetPoint();
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
        string yearResult = getYearInfo(5, 5);

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(yearResult);
        Response.End();
    }

    #endregion

    #region// 获取监测点(ljn,2013/6/19)
    private void GetPoint()
    {
        string year = Request["year"];
         string month = Request["month"]; 
        TEnvPAir30Vo Dustpoint = new TEnvPAir30Vo();//降尘点位
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Air30.TEnvPAir30Logic().SelectByTable(Dustpoint);
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

        TEnvFillAir30Vo envFillAir30Vo = new TEnvFillAir30Vo();
        envFillAir30Vo.YEAR = year;
        envFillAir30Vo.MONTH = month;

        envFillAir30Vo = new TEnvFillAir30Logic().Details(envFillAir30Vo);

        string json = "{\"ID\":\"" + envFillAir30Vo.ID + "\",\"STATUS\":\"" + envFillAir30Vo.STATUS + "\"}";

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
       // string areacode = Request["areacode"];
        //DataTable dt = new TEnvFillAir30Logic().GetAir30FillData(year, month, areacode);
        //string json = LigerGridDataToJson(dt, dt.Rows.Count);
        //Response.ContentType = "application/json;charset=utf-8";
        string pointId = Request["pointId"];
        //拼写条件语句(注：条件中的列名要与点位上的列名一致)
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";
         
        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillAir30Logic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPAir30Vo.T_ENV_P_AIR30_TABLE,
                                       TEnvPAir30ItemVo.T_ENV_P_AIR30_ITEM_TABLE,
                                       TEnvFillAir30Vo.T_ENV_FILL_AIR30_TABLE,
                                        TEnvFillAir30ItemVo.T_ENV_FILL_AIR30_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_AIR30,
                                       SerialType.T_ENV_FILL_AIR30_ITEM,
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
        string data = Request["data"];
        //string areacode = Request["areacode"];
        string itemName = Request["itemname"];//列名
        string value = Request["value"];
        string Fill_ID = Request["fill_id"];
        string ConditionID = "";
        //DataTable dtData = JSONToDataTable2(data);
        //string result = new TEnvFillAir30Logic().SaveAir30FillData(dtData, areacode);
        //string json = "{\"result\":\"" + result + "\"}";
        CommonLogic com = new CommonLogic();
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        //Response.ContentType = "application/json;charset=utf-8";
        //Response.Write(json);
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
        return com.getNameByID(TEnvPAir30Vo.T_ENV_P_AIR30_TABLE, "POINT_NAME", "ID", strV);
    }
}