using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.AirKS;
using i3.BusinessLogic.Channels.Env.Fill.AirKS;

public partial class Channels_Env_Fill_AirKS_AirKsFill : PageBase
{
    /// <summary>
    /// 功能描述：环境空气（科室）数据填报
    /// 修改人：刘静楠
    /// 修改日期：2013-07-03
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
        string month=Request["month"];
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

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillAirksVo envFillAirKsVo = new TEnvFillAirksVo();
        envFillAirKsVo.YEAR = year;
        envFillAirKsVo.MONTH = month;

        envFillAirKsVo = new TEnvFillAirksLogic().Details(envFillAirKsVo);

        string json = "{\"ID\":\"" + envFillAirKsVo.ID + "\",\"STATUS\":\"" + envFillAirKsVo.STATUS + "\"}";

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
        dtShow = new i3.BusinessLogic.Channels.Env.Fill.AirKS.TEnvFillAirksLogic().CreateShowDT();
         
        //CommonLogic com = new CommonLogic();
        //DataTable dt = com.GetFillData(strWhere,
        //                               dtShow,
        //                               "",
        //                               TEnvPAirVo.T_ENV_P_AIR_TABLE,
        //                               TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE,
        //                               i3.ValueObject.Channels.Env.Fill.AirKS.TEnvFillAirksVo.T_ENV_FILL_AIRKS_TABLE,
        //                                TEnvFillAirksItemVo.T_ENV_FILL_AIRKS_ITEM_TABLE,
        //                               SerialType.T_ENV_FILL_AIRKS,
        //                               SerialType.T_ENV_FILL_AIRKS_ITEM,
        //                               "0");
        DataTable dt = new TEnvFillAirksLogic().GetFillData(strWhere, dtShow, TEnvPAirVo.T_ENV_P_AIR_TABLE, TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE, TEnvFillAirksVo.T_ENV_FILL_AIRKS_TABLE,
                                                                               TEnvFillAirksItemVo.T_ENV_FILL_AIRKS_ITEM_TABLE, SerialType.T_ENV_FILL_AIRKS, SerialType.T_ENV_FILL_AIRKS_ITEM);
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
        string itemName = Request["itemname"];//列名
        string value = Request["value"];
        string Fill_ID = Request["fill_id"];
        string ConditionID = "";
        CommonLogic com = new CommonLogic();
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
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

}