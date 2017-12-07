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
/// 功能描述：物料领用历史记录明细导出
/// 创建日期：2013-02-02
/// 创建人  ：胡方扬
/// </summary>
public partial class Channels_OA_Part_PartCollarList : PageBase
{
    string strPartId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestUrlParame();

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            string strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strPartCollarId = "";
        strPartCollarId = this.hidExportDate.Value.ToString();
        DataTable dt = new DataTable();
        TOaPartCollarVo objItems = new TOaPartCollarVo();
        TOaPartInfoVo objItemParts = new TOaPartInfoVo();
        objItems.ID = strPartCollarId.Replace(",","','");
        if (!String.IsNullOrEmpty(strPartId))
        {
            objItemParts.ID = strPartId;
        }
        dt = new TOaPartCollarLogic().SelectUnionPartByTable(objItems, objItemParts, 0, 0);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/PartUsedHistorySheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(0).SetCellValue("生成日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sheet.GetRow(i + 4).GetCell(0).SetCellValue(dt.Rows[i]["PART_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(1).SetCellValue(dt.Rows[i]["PART_CODE"].ToString());
            sheet.GetRow(i + 4).GetCell(2).SetCellValue(dt.Rows[i]["USED_QUANTITY"].ToString());
            sheet.GetRow(i + 4).GetCell(3).SetCellValue(dt.Rows[i]["LASTIN_DATE"].ToString());
            sheet.GetRow(i + 4).GetCell(4).SetCellValue(dt.Rows[i]["REAL_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(5).SetCellValue(dt.Rows[i]["REASON"].ToString());
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
                "attachment;filename=" + HttpUtility.UrlEncode("物料领用历史明细表-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// 获取URL参数
    /// </summary>
    public void GetRequestUrlParame() {
        if (!String.IsNullOrEmpty(Request.Params["strPartId"].ToString())) {
            strPartId = Request.Params["strPartId"].Trim();
        }

    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        if (strPartId.Length == 0)
            return "";

        TOaPartInfoVo objPart = new TOaPartInfoLogic().Details(strPartId);

        return objPart.INVENTORY;
    }
}