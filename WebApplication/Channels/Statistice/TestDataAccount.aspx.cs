using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;

/// <summary>
/// 功能描述：监测数据统计表(清远)
/// 创建时间：2013-6-29
/// 创建人：潘德军
/// </summary>
public partial class Channels_Statistice_TestDataAccount : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetDataList")
        {
            GetDataList();
            Response.End();
        }
    }

    //获取监测项目
    private void GetDataList()
    {
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhCompanyName = (Request.Params["SrhCOMPANY_NAME"] != null) ? Request.Params["SrhCOMPANY_NAME"] : "";
        string strSrhContractType = (Request.Params["SrhCONTRACT_TYPE"] != null) ? Request.Params["SrhCONTRACT_TYPE"] : "";
        string strSrhSampleDate_Begin = (Request.Params["SrhSampleDate_Begin"] != null) ? Request.Params["SrhSampleDate_Begin"] : "";
        string strSrhSampleDate_End = (Request.Params["SrhSampleDate_End"] != null) ? Request.Params["SrhSampleDate_End"] : "";

        string[] strWhereArr = (strSrhCompanyName + "," + strSrhContractType + "," + strSrhSampleDate_Begin + "," + strSrhSampleDate_End).Split(',');

        DataTable dt = new TMisMonitorResultLogic().SelectTestDataLst(strWhereArr, intPageIdx, intPagesize);
        int intDataCount = new TMisMonitorResultLogic().GetSelectTestDataLstCount(strWhereArr);

        string strJson = CreateToJson(dt, intDataCount);

        Response.Write(strJson);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strHdSrh = this.hdSrh.Value.Trim();
        strHdSrh = strHdSrh.Length == 0 ? ",,," : strHdSrh;
        string[] strWhereArr = strHdSrh.Split(',');
        DataTable dt = new TMisMonitorResultLogic().SelectTestDataLst(strWhereArr, 0, 0);

        FullExcel(dt);
    }

    private void FullExcel(DataTable dt)
    {
        System.DateTime dtNow = System.DateTime.Now;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/TestDataAccount.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        infullExcelValue(hssfworkbook, dt);

        using (MemoryStream stream = new MemoryStream())
        {
            string strExcelTime = dtNow.Year.ToString() + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');

            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("监测数据统计表" + strExcelTime + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void infullExcelValue(HSSFWorkbook hssfworkbook, DataTable dtResult)
    {
        string strSheetName = "监测数据统计表";

        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 2, i + 2, 9);

            IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(i + 2);

            ICell changingCell = null;

            changingCell = changingRow.GetCell(0);
            changingCell.SetCellValue((i+1).ToString());//序号
            changingCell = changingRow.GetCell(1);
            changingCell.SetCellValue(dtResult.Rows[i]["COMPANY_NAME"].ToString());//污染源企业
            changingCell = changingRow.GetCell(2);
            changingCell.SetCellValue(dtResult.Rows[i]["CONTRACT_TYPE"].ToString());//委托类别
            changingCell = changingRow.GetCell(3);
            changingCell.SetCellValue(dtResult.Rows[i]["MONITOR_NAME"].ToString());//监测类别
            changingCell = changingRow.GetCell(4);
            changingCell.SetCellValue(dtResult.Rows[i]["POINT_NAME"].ToString());//监测点位
            changingCell = changingRow.GetCell(5);
            changingCell.SetCellValue(dtResult.Rows[i]["QC_TYPE"].ToString());//样品
            changingCell = changingRow.GetCell(6);
            changingCell.SetCellValue(dtResult.Rows[i]["SAMPLE_FINISH_DATE"].ToString().Replace("0:00:00", ""));//采样日期
            changingCell = changingRow.GetCell(7);
            changingCell.SetCellValue(dtResult.Rows[i]["ITEM_NAME"].ToString());//监测项目
            changingCell = changingRow.GetCell(8);
            changingCell.SetCellValue(dtResult.Rows[i]["ITEM_RESULT"].ToString());//监测结果
        }
    }

    private void CopyRange(HSSFWorkbook myHSSFWorkBook, string strSheetName, int intFromRowIndex, int intToRowIndex, int intCellCount)
    {
        IRow sourceRow = myHSSFWorkBook.GetSheet(strSheetName).GetRow(intFromRowIndex);
        if (sourceRow != null)
        {
            IRow toRow = null;
            toRow = myHSSFWorkBook.GetSheet(strSheetName).GetRow(intToRowIndex);
            if (toRow == null)
                toRow = myHSSFWorkBook.GetSheet(strSheetName).CreateRow(intToRowIndex);
            for (int i = 0; i < intCellCount; i++)
            {
                ICell sourceCell = sourceRow.GetCell(i);
                ICell toCell = null;
                toCell = toRow.GetCell(i);
                if (toCell == null)
                    toCell = toRow.CreateCell(i);
                toCell.CellStyle = sourceCell.CellStyle;
            }
        }
    }
}