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
using i3.BusinessLogic.Channels.Env.Fill.Air;
using i3.ValueObject.Channels.Env.Fill.Air;
using i3.ValueObject.Channels.Env.Point.Environment;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using System.Configuration;

/// <summary>
/// 功能描述：环境空气数据填报
/// 修改人：刘静楠
/// 修改日期：2013-06-26
/// </summary>
public partial class Channels_Env_Fill_Air_AirFill : PageBase
{
    private static DataTable dtStatic = new DataTable(); //全局DT
  
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
                    case  "EnvImport":
                        EnvImport();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "AddRow":
                        AddRow();
                        break;
                    case "DeleteRow":
                        DeleteRow();
                        break;
                    case "SaveTemp":
                        SaveTemp();
                        break;
                    case "GetFillID":
                        GetFillID();
                        break;
                    case "ComputeAQI":
                        ComputeAQI();
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
        TEnvPAirVo Dustpoint = new TEnvPAirVo();//降尘点位
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Environment.T_ENV_P_AIR().SelectByTable(Dustpoint);
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

    private void ComputeAQI()
    {
        string year = Request["year"];
        string month = Request["month"];
        int Rtnresult = new TEnvFillAirLogic().ExcelOutCal(year, month, TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE);
         
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"success\"}");
        Response.End();
    }

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillAirVo envFillAirVo = new TEnvFillAirVo();
        envFillAirVo.YEAR = year;
        envFillAirVo.MONTH = month;

        envFillAirVo = new TEnvFillAirLogic().Details(envFillAirVo);

        string json = "{\"ID\":\"" + envFillAirVo.ID + "\",\"STATUS\":\"" + envFillAirVo.STATUS + "\"}";

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
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month.ToString() != "")
            strWhere += " and MONTH='" + month+ "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";

        DataTable dtShow = new DataTable();
        dtShow = new TEnvFillAirLogic().CreateShowDT();
        DataTable dt = new TEnvFillAirLogic().GetFillData(strWhere, dtShow, TEnvPAirVo.T_ENV_P_AIR_TABLE, TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE, TEnvFillAirVo.T_ENV_FILL_AIR_TABLE,
                                                                                       TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE, SerialType.T_ENV_FILL_AIR, SerialType.T_ENV_FILL_AIR_ITEM);
        string json = DataTableToJsonUnsureColAirHour(dt); 
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
        result = new TEnvFillAirLogic().UpdateAirValue(Fill_ID, value, itemName);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 新增行

    private void AddRow()
    {
        DataRow dr = dtStatic.NewRow();
        dr["year"] = dtStatic.Rows[dtStatic.Rows.Count - 1]["year"].ToString();
        dr["month"] = dtStatic.Rows[dtStatic.Rows.Count - 1]["month"].ToString();
        dtStatic.Rows.InsertAt(dr, 0);
        dtStatic = BuildRowNum(dtStatic);

        string json = LigerGridDataToJson(dtStatic, dtStatic.Rows.Count);

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 删除行

    private void DeleteRow()
    {
        string rowNum = Request["rowNum"];

        foreach (string rn in rowNum.Split(','))
        {
            var removeRow = dtStatic.AsEnumerable().Where(c => c["row_no"].ToString().Equals(rn)).ToList();
            dtStatic.Rows.Remove(removeRow[0]);
        }

        dtStatic = BuildRowNum(dtStatic);

        string json = LigerGridDataToJson(dtStatic, dtStatic.Rows.Count);

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 构建行号

    private DataTable BuildRowNum(DataTable dt)
    {
        if (!dt.Columns.Contains("row_no"))
            dt.Columns.Add("row_no", typeof(string));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["row_no"] = (i + 1).ToString();
        }

        return dt;
    }

    #endregion

    #region 临时保存

    private void SaveTemp()
    {
        string data = Request["data"];
        dtStatic = JSONToDataTable2(data);
        dtStatic = BuildRowNum(dtStatic);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write("{\"result\":\"success\"}");
        Response.End();
    }

    #endregion

    #region// 获取测点名称
    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPAirVo.T_ENV_P_AIR_TABLE, "POINT_NAME", "ID", strV);

    }
    #endregion

    #region//业务系统导入
    private void  EnvImport()
    {
       // string json = string.Empty;
       // string strMsg = string.Empty;
       // string year = Request["year"];
       // string month = Request["month"];
       // WebReference.WebServiceEMBMS www; 
       // www = new WebReference.WebServiceEMBMS();
       // www.Url = ConfigurationManager.AppSettings["localhost.WebService"];
       // DataSet ds = www.GetDayData(year,month);
       //strMsg = new TEnvFillAirLogic().EnvInsertData(ds, TEnvFillAirVo.T_ENV_FILL_AIR_TABLE, TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE, SerialType.T_ENV_FILL_AIR, SerialType.T_ENV_FILL_AIR_ITEM);
       //if (string.IsNullOrEmpty(strMsg))
       //{
       //     json = "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}";
       //}
       //else
       //{
       //     json = "{\"result\":\"false\",\"msg\":\"" + strMsg + "\"}";
       //}
       // Response.ContentType = "application/json;charset=utf-8";
       // Response.Write(json);
       // Response.End();
    }
    #endregion
}