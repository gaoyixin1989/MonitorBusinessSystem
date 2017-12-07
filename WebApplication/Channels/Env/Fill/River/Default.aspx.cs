using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.River;
using i3.View;
using System.Text;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
public partial class Channels_Env_Fill_River_Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.MapPath("");
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        if (this.fileUpload.PostedFile.ContentLength >= 20971520)
        {
            LigerDialogAlert("上传的文件不能大于20M", "error"); return;
        }
        if (this.fileUpload.PostedFile.ContentLength <= 0)
        {
            LigerDialogAlert("请选择文件", "error"); return;
        }
        Stream stream = this.fileUpload.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        bool isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcel(Server.MapPath("../../xmlTemp/Import/RiverTemple.xml"), sheet);
        if (isSuccess)
            Alert("导入成功");
        else
            Alert("导入失败");
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        if (this.fileUpload1.PostedFile.ContentLength >= 20971520)
        {
            LigerDialogAlert("上传的文件不能大于20M", "error"); return;
        }
        if (this.fileUpload1.PostedFile.ContentLength <= 0)
        {
            LigerDialogAlert("请选择文件", "error"); return;
        }
        Stream stream = this.fileUpload1.FileContent;
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(stream);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        bool isSuccess = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().importExcelSpecial(Server.MapPath("../../xmlTemp/Import/FunctionNoiseTemple.xml"), sheet);
        if (isSuccess)
            Alert("导入成功");
        else
            Alert("导入失败");
    }
    protected void btnExport1_Click(object sender, EventArgs e)
    {
        string strXmlPath = Server.MapPath("../../xmlTemp/Export/FunctionNoiseTemple.xml");
        string strExcelPath = Server.MapPath("../../excelTemp/功能区噪声.xls");
        ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheetSpecial(strXmlPath, strExcelPath, "Sheet1", "2012", "11");

        using (MemoryStream stream = new MemoryStream())
        {
            sheet.Workbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("功能区噪声.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strXmlPath = Server.MapPath("../../xmlTemp/Export/RiverTemple.xml");
        string strExcelPath = Server.MapPath("../../excelTemp/河流.xls");
        ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheet(strXmlPath, strExcelPath, "Sheet1", "2013", "1");

        using (MemoryStream stream = new MemoryStream())
        {
            sheet.Workbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("江河水.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
}