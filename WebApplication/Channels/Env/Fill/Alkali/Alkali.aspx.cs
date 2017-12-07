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
using i3.BusinessLogic.Channels.Env.Fill.Alkali;
using i3.ValueObject.Channels.Env.Fill.Alkali;
using i3.ValueObject.Channels.Env.Point.Sulfate;
using i3.ValueObject.Channels.Env.Point.Seabath;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;
using NPOI.SS.UserModel;
using System.Text;

/// <summary>
/// 功能描述：硫酸盐化速率数据填报 
/// 修改人：刘静楠
/// 修改日期：2013-06-26
/// </summary>
public partial class Channels_Env_Fill_Alkali_Alkali : PageBase
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
                    case "GetEnvConfig":
                        //GetEnvConfig();
                        break;
                    case "DataImport":
                           DataImport();
                        break;
                    case "DataExport":
                        //DataExport();
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

    #region// 获取监测点(ljn,2013/6/19)
    private void GetPoint()
    {
        string year = Request["year"]; 
        string month=Request["month"];
        TEnvPAlkaliVo Dustpoint = new TEnvPAlkaliVo();//碳酸盐化速率点位
        Dustpoint.YEAR= year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Sulfate.TEnvPAlkaliLogic().SelectByTable(Dustpoint);
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

        TEnvFillAlkaliVo envFillAlkaliVo = new TEnvFillAlkaliVo();
        envFillAlkaliVo.YEAR = year;
        envFillAlkaliVo.MONTH = month;

        envFillAlkaliVo = new TEnvFillAlkaliLogic().Details(envFillAlkaliVo);

        string json = "{\"ID\":\"" + envFillAlkaliVo.ID + "\",\"STATUS\":\"" + envFillAlkaliVo.STATUS + "\"}";

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
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";
        DataTable dtShow = new DataTable();
        dtShow = new TEnvFillAlkaliLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE,
                                       TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE,
                                       TEnvFillAlkaliVo.T_ENV_FILL_ALKALI_TABLE,
                                       TEnvFillAlkaliItemVo.T_ENV_FILL_ALKALI_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_ALKALI,
                                       SerialType.T_ENV_FILL_ALKALI_ITEM,
                                       "0");
        string json = DataTableToJsonUnsureCol(dt);
        //dt = BuildRowNum(dt);
        //dtStatic = dt;
        //string json = LigerGridDataToJson(dtStatic, dtStatic.Rows.Count);

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

    #region 保存数据

    private void SaveData()
    {
        //string data = Request["data"];
        //DataTable dtData = JSONToDataTable2(data);
        //string result = new TEnvFillAlkaliLogic().SaveAlkaliFillData(dtData);
        //string json = "{\"result\":\"" + result + "\"}";
        //Response.ContentType = "application/json;charset=utf-8";
        //Response.Write(json);

        bool result = false;
        string data = "[" + Request["data"] + "]";
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

    #region 新增行

    private void AddRow()
    {
        DataRow dr = dtStatic.NewRow();
        dr["year"] = dtStatic.Rows[dtStatic.Rows.Count - 1]["year"].ToString();
        dr["SMONTH"] = dtStatic.Rows[dtStatic.Rows.Count - 1]["SMONTH"].ToString();
        dr["EMONTH"] = dtStatic.Rows[dtStatic.Rows.Count - 1]["EMONTH"].ToString();
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

    #region//Excel导出(暂不用)
    //protected void btnExcelOut_Click(object sender, EventArgs e)
    //{
    //    string strXmlPath = Server.MapPath("../../xmlTemp/Export/AlkaliTemple.xml");
    //    string strExcelPath = Server.MapPath("../../excelTemp/硫酸盐化速率导出.xls"); 
    //    ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheetSpecial(strXmlPath, strExcelPath, "Sheet1", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        sheet.Workbook.Write(stream);
    //        HttpContext curContext = HttpContext.Current;
    //        // 设置编码和附件格式   
    //        curContext.Response.ContentType = "application/vnd.ms-excel";
    //        curContext.Response.ContentEncoding = Encoding.UTF8;
    //        curContext.Response.Charset = "";
    //        curContext.Response.AppendHeader("Content-Disposition",
    //            "attachment;filename=" + HttpUtility.UrlEncode("硫酸盐化速率.xls", Encoding.UTF8));
    //        curContext.Response.BinaryWrite(stream.GetBuffer());
    //        curContext.Response.End();
    //    }
    //}
     
    #endregion

    #region//获取测点名称
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPAlkaliVo.T_ENV_P_ALKALI_TABLE, "POINT_NAME", "ID", strV);
    }
    #endregion

    private void DataImport()
    {
        string Excel = Request["ExcelImport"];
    }

    

}