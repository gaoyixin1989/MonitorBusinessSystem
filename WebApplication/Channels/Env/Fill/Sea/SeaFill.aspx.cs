﻿using System;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.Sea;
using i3.BusinessLogic.Channels.Env.Point.Sea;
using i3.ValueObject.Channels.Env.Point.Sea;
using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.Sea;
using System.Web.Services;
using NPOI.SS.UserModel;
using System.IO;
using System.Web;
using System.Text;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

/// <summary>
/// 功能描述：近岸海域数据填报
/// 创建人：钟杰华
/// 创建日期：2013-02-22
/// modify：刘静楠
/// time:2013-6-25
/// </summary>
public partial class Channels_Env_Fill_Sea_SeaFill : PageBase
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
        string json = getYearInfo();

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取监测点

    private void GetPoint()
    {
        string year = Request["year"];
         string month=Request["month"];
        TEnvPointSeaVo model = new TEnvPointSeaVo();
        model.YEAR = year;
        model.MONTH = month;
        model.IS_DEL = "0";
        DataTable dt = new TEnvPointSeaLogic().SelectByTable(model);

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

        TEnvFillSeaVo envFillSeaVo = new TEnvFillSeaVo();
        envFillSeaVo.YEAR = year;
        envFillSeaVo.MONTH = month;

        envFillSeaVo = new TEnvFillSeaLogic().Details(envFillSeaVo);

        string json = "{\"ID\":\"" + envFillSeaVo.ID + "\",\"STATUS\":\"" + envFillSeaVo.STATUS + "\"}";

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
        //DataTable dt = new TEnvFillSeaLogic().GetSeaFillData(year, month, pointId);
        //string json = DataTableToJsonUnsureCol(dt, "_unSure");
        //Response.ContentType = "application/json";
        //拼写条件语句(注：条件中的列名要与点位上的列名一致)
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";


        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillSeaLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE,
                                       TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE,
                                       TEnvFillSeaVo.T_ENV_FILL_SEA_TABLE,
                                       TEnvFillSeaItemVo.T_ENV_FILL_SEA_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_SEA,
                                       SerialType.T_ENV_FILL_SEA_ITEM,
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
        //string data = Request["data"];
        //DataTable dtData = JSONToDataTable2(data);
        //bool result = new TEnvFillSeaLogic().SaveSeaFillData(dtData);
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
            com.UpdateTableByWhere(TEnvFillSeaVo.T_ENV_FILL_SEA_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + Fill_ID + "'");
        }
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 获取日期

    private void GetDay()
    {
        string month = Request["month"];
        string year = Request["year"];

        string json = GetDayByMonth(year, Convert.ToInt32(month));

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
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
        return com.getNameByID(TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE, "POINT_NAME", "ID", strV);
    }
    #endregion

    #region//Excel导出 (暂不用)
    //protected void btnExcelOut_Click(object sender, EventArgs e)
    //{
 
        //string strXmlPath = Server.MapPath("../../xmlTemp/Export/SeaTemple.xml");
        //string strExcelPath = Server.MapPath("../../excelTemp/近岸海域导出.xls");
        //ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheetSpecial(strXmlPath, strExcelPath, "Sheet1", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
        //using (MemoryStream stream = new MemoryStream())
        //{
        //    sheet.Workbook.Write(stream);
        //    HttpContext curContext = HttpContext.Current;
        //    // 设置编码和附件格式   
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode("近岸海域.xls", Encoding.UTF8));
        //    curContext.Response.BinaryWrite(stream.GetBuffer());
        //    curContext.Response.End();
        //}
    //}

    #endregion
}