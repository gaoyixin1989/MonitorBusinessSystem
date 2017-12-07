using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.Dust;
using i3.ValueObject.Channels.Env.Fill.Dust;
using i3.ValueObject.Channels.Env.Point.Dust;
using System.Text;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Web.Services;
using NPOI.SS.UserModel;
using System.IO;

/// <summary>
/// 功能描述：降尘数据填报
/// 创建人：钟杰华
/// 创建日期：2013-02-03
/// modify by 刘静楠 2013/6/19 
/// </summary>
public partial class Channels_Env_Fill_Dust_DustFill : PageBase
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
        string month = Request["month"];  
        TEnvPointDustVo Dustpoint = new TEnvPointDustVo();//降尘点位
        Dustpoint.YEAR = year;
        Dustpoint.MONTH = month;
        Dustpoint.IS_DEL = "0";//删除标志
        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Dust.TEnvPointDustLogic().SelectByTable(Dustpoint);
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

        TEnvFillDustVo envFillDustVo = new TEnvFillDustVo();
        envFillDustVo.YEAR = year;
        envFillDustVo.MONTH = month;

        envFillDustVo = new TEnvFillDustLogic().Details(envFillDustVo);

        string json = "{\"ID\":\"" + envFillDustVo.ID + "\",\"STATUS\":\"" + envFillDustVo.STATUS + "\"}";

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
        dtShow = new TEnvFillDustLogic().CreateShowDT();

        CommonLogic com = new CommonLogic();
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       "",
                                       TEnvPointDustVo.T_ENV_POINT_DUST_TABLE,
                                       TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE,
                                       TEnvFillDustVo.T_ENV_FILL_DUST_TABLE,
                                       TEnvFillDustItemVo.T_ENV_FILL_DUST_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_DUST,
                                       SerialType.T_ENV_FILL_DUST_ITEM,
                                       "0");
        string json = DataTableToJsonUnsureCol(dt);

        //DataTable dtAllItem = new DataTable();//动态监测项目信息
        //DataTable dt = new TEnvFillDustLogic().GetDustFillData(year, month, ref dtAllItem, pointId,SerialType.T_ENV_FILL_DUST, SerialType.T_ENV_FILL_DUST_ITEM);

        // string json = DataTableToJsonUnsureCol(dt, dtAllItem,"T_ENV_FILL_DUST_ITEM@");
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
        //DataTable dtData = JSONToDataTable2(data);
        //result = new TEnvFillDustLogic().UpdateCommonFill(itemName, value, Fill_ID);
        CommonLogic com = new CommonLogic();
        result = com.UpdateCommonFill(itemName, value, Fill_ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();


        #region//暂不用代码
        //string year = Request["year"];
        //string data = Request["data"];

        //DataTable dtData = JSONToDataTable2(data);

        //string result = new TEnvFillDustLogic().SaveDustFillData(dtData);

        //string json = "{\"result\":\"" + result + "\"}";

        //Response.ContentType = "application/json;charset=utf-8";
        //Response.Write(json);
        //Response.End();
        #endregion
    }

    #endregion

    #region//获取测点名称
    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPointDustVo.T_ENV_POINT_DUST_TABLE, "POINT_NAME", "ID", strV);
    }
    #endregion

    #region//Excel导出(暂不用)
    //protected void btnExcelOut_Click(object sender, EventArgs e)
    //{
    //    string strXmlPath = Server.MapPath("../../xmlTemp/Export/DustTemple.xml");
    //    string strExcelPath = Server.MapPath("../../excelTemp/降尘导出.xls");
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
    //            "attachment;filename=" + HttpUtility.UrlEncode("降尘.xls", Encoding.UTF8));
    //        curContext.Response.BinaryWrite(stream.GetBuffer());
    //        curContext.Response.End();
    //    }
    //}
     
    #endregion
}