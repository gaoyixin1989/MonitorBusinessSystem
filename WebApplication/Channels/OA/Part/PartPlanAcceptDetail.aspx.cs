using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
/// <summary>
/// 功能描述：物料采购明及验收明细导出
/// 创建日期：2013-02-02
/// 创建人  ：胡方扬
/// </summary>

public partial class Channels_OA_Part_PartPlanAcceptDetail : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 导出、打印发文
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string fwID = this.hidFwId.Value;
        DataTable dt = new TOaPartBuyRequstLstLogic().SelectREQUSTID(fwID);
        if (dt.Rows.Count > 0&&!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
        {
            SWPrint.WSExport(dt.Rows[0][0].ToString());
        }
    }   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        string strType = this.hidGrid.Value.ToString();
        string strPartBuyLstId = this.hidExportDate.Value.ToString();
        if (!String.IsNullOrEmpty(strType)) {
            //导出采购计划明细
            if (strType == "1") {
                TOaPartBuyRequstLstVo objItems = new TOaPartBuyRequstLstVo();
                TOaPartInfoVo objItemPart = new TOaPartInfoVo();
                TOaPartBuyRequstVo objItemBy = new TOaPartBuyRequstVo();
                objItemBy.STATUS = "9";
                if (!String.IsNullOrEmpty(strPartBuyLstId)) {
                    objItems.ID = strPartBuyLstId.Replace(",","','");
                }
                objItems.STATUS = "0";
                dt = new TOaPartBuyRequstLstLogic().SelectUnionPartByTable(objItems, objItemPart, objItemBy, 0, 0);
                DataView dv = new DataView();
                dv = dt.DefaultView;
                if (dt.Rows.Count > 0)
                {
                    dv.Sort = "DELIVERY_DATE";
                }
                FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/PartPlanDetailSheet.xls"), FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                ISheet sheet = hssfworkbook.GetSheet("Sheet1");
                sheet.GetRow(2).GetCell(0).SetCellValue("生成日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                DataTable dtTemp = new DataTable();
                dtTemp = dv.ToTable().Copy(); ;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sheet.GetRow(i + 4).GetCell(0).SetCellValue(dtTemp.Rows[i]["PART_NAME"].ToString());
                    sheet.GetRow(i + 4).GetCell(1).SetCellValue(dtTemp.Rows[i]["PART_CODE"].ToString());
                    sheet.GetRow(i + 4).GetCell(2).SetCellValue(dtTemp.Rows[i]["APPLY_TITLE"].ToString());
                    sheet.GetRow(i + 4).GetCell(3).SetCellValue(dtTemp.Rows[i]["DEPT_NAME"].ToString());
                    sheet.GetRow(i + 4).GetCell(4).SetCellValue(dtTemp.Rows[i]["REAL_NAME"].ToString());
                    sheet.GetRow(i + 4).GetCell(5).SetCellValue(dtTemp.Rows[i]["MODELS"].ToString());
                    sheet.GetRow(i + 4).GetCell(6).SetCellValue(dtTemp.Rows[i]["NEED_QUANTITY"].ToString());
                    sheet.GetRow(i + 4).GetCell(7).SetCellValue(dtTemp.Rows[i]["DELIVERY_DATE"].ToString());
                    sheet.GetRow(i + 4).GetCell(8).SetCellValue("￥"+dtTemp.Rows[i]["BUDGET_MONEY"].ToString());
                    sheet.GetRow(i + 4).GetCell(9).SetCellValue(dtTemp.Rows[i]["REQUEST"].ToString());
                    sheet.GetRow(i + 4).GetCell(10).SetCellValue(dtTemp.Rows[i]["USEING"].ToString());
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    hssfworkbook.Write(stream);
                    HttpContext curContext = HttpContext.Current;
                    // 设置编码和附件格式   
                    curContext.Response.ContentType = "application/vnd.ms-excel";
                    curContext.Response.ContentEncoding = Encoding.UTF8;
                    curContext.Response.Charset = "";
                    curContext.Response.AppendHeader("Content-Disposition",
                        "attachment;filename=" + HttpUtility.UrlEncode("物料采购计划明细表-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
                    curContext.Response.BinaryWrite(stream.GetBuffer());
                    curContext.Response.End();
                }
            }
            //导出验收明细
            if (strType == "2") {
                TOaPartAcceptedlistVo objItems = new TOaPartAcceptedlistVo();
                if (!String.IsNullOrEmpty(strPartBuyLstId)) {
                    objItems.ID = strPartBuyLstId.Replace(",", "','");
                }
                TOaPartInfoVo objItemPart = new TOaPartInfoVo();
                dt = new TOaPartAcceptedlistLogic().SelectUnionByTable(objItems, objItemPart, 0, 0);
                DataView dv = new DataView();
                dv = dt.DefaultView;
                if (dt.Rows.Count > 0)
                {
                    dv.Sort = "CHECK_DATE  DESC";
                }

                FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/PartAcceptDetailSheet.xls"), FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                ISheet sheet = hssfworkbook.GetSheet("Sheet1");
                DataTable dtTemp = new DataTable();
                dtTemp = dv.ToTable().Copy(); ;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sheet.GetRow(i + 3).GetCell(0).SetCellValue(dtTemp.Rows[i]["PARTTYPE"].ToString());
                    sheet.GetRow(i + 3).GetCell(1).SetCellValue(dtTemp.Rows[i]["PART_NAME"].ToString() + "(" + dtTemp.Rows[i]["PART_CODE"].ToString() + ")");
                    sheet.GetRow(i + 3).GetCell(2).SetCellValue(dtTemp.Rows[i]["ENTERPRISE_NAME"].ToString());
                    sheet.GetRow(i + 3).GetCell(3).SetCellValue(dtTemp.Rows[i]["NEED_QUANTITY"].ToString());
                    sheet.GetRow(i + 3).GetCell(4).SetCellValue("￥" + dtTemp.Rows[i]["PRICE"].ToString());
                    sheet.GetRow(i + 3).GetCell(5).SetCellValue("￥" + dtTemp.Rows[i]["AMOUNT"].ToString());
                    sheet.GetRow(i + 3).GetCell(6).SetCellValue(dtTemp.Rows[i]["CHECK_DATE"].ToString());
                    sheet.GetRow(i + 3).GetCell(7).SetCellValue(dtTemp.Rows[i]["RECIVEPART_DATE"].ToString());
                    sheet.GetRow(i + 3).GetCell(8).SetCellValue(dtTemp.Rows[i]["CHECKRESULT"].ToString());
                    sheet.GetRow(i + 3).GetCell(9).SetCellValue(dtTemp.Rows[i]["REAL_NAME"].ToString());
                    sheet.GetRow(i + 3).GetCell(10).SetCellValue(dtTemp.Rows[i]["REMARK1"].ToString());
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    hssfworkbook.Write(stream);
                    HttpContext curContext = HttpContext.Current;
                    // 设置编码和附件格式   
                    curContext.Response.ContentType = "application/vnd.ms-excel";
                    curContext.Response.ContentEncoding = Encoding.UTF8;
                    curContext.Response.Charset = "";
                    curContext.Response.AppendHeader("Content-Disposition",
                        "attachment;filename=" + HttpUtility.UrlEncode("物料验收明细表-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
                    curContext.Response.BinaryWrite(stream.GetBuffer());
                    curContext.Response.End();
                }
            }
        }
    }
}