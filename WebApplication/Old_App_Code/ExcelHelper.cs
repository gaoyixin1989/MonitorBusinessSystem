using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Drawing;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections;
using System.Text;

/// <summary>
/// 功能：EXCEL操作类
/// 创建人：钟杰华 2013-04-22
/// </summary>
public class ExcelHelper
{
    public ExcelHelper()
    {

    }

    #region 弹出下载框

    /// <summary>
    /// 弹出下载框
    /// </summary>
    /// <param name="excelSavedPath">EXCEL文件服务器上保存的路径（需要先使用Server.MapPath）</param>
    /// <param name="excelFileName">EXCEL文件保存到客户端后的文件名（需要带后缀名）</param>
    public void DownLoadExcel(string excelSavedPath, string excelFileName)
    {
        //把目标EXCEL读取成二进制数据
        //FileStream fs = new FileStream(excelSavedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //byte[] b = new byte[Convert.ToInt32(fs.Length)];
        //fs.Read(b, 0, b.Length);
        //fs.Close();
        //fs.Dispose();
        //File.Delete(excelSavedPath);//删除在服务器临时文件夹上的EXCEL

        FileInfo fi = new FileInfo(excelSavedPath);

        //设置输出属性并输出到客户端
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Buffer = false;
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(excelFileName, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.WriteFile(fi.FullName);
        HttpContext.Current.Response.Flush();
        File.Delete(excelSavedPath);//删除在服务器临时文件夹上的EXCEL
        HttpContext.Current.Response.End();
    }

    #endregion

    #region NPOI导出导入
    /// <summary>
    /// Excel 导出 
    /// </summary>
    /// <param name="SourceTable">来源DataTable数据集</param>
    /// <param name="strFileName">输出文件名.xls</param>
    /// <param name="strTemplateUrl">模板路径../sheet.xls</param>
    /// <param name="strSheetName">填充的工作表名</param>
    /// <param name="intTitleRowCount">标题行数</param>
    /// <param name="HasColumns">是否自动生成列名，针对动态生成列名数据导出</param>
    /// <param name="arrTitle">标题数组string[3]{"存放位置","内容","是否正标题(true/false)"}</param>
    /// <param name="arrRow">列数组string[2]{"列号","列名"}</param>
    /// <param name="arrEnd">列数组string[2]{"排放顺序","内容"}</param>
    /// <returns></returns>
    public void RenderDataTableToExcel(System.Data.DataTable SourceTable, string strFileName, string strTemplateUrl, string strSheetName, int intTitleRowCount, bool HasColumns, ArrayList arrTitle, ArrayList arrRow, ArrayList arrEnd)
    {
        if (!File.Exists(HttpContext.Current.Server.MapPath(strTemplateUrl)))
        {
            FileStream stream = File.Create(HttpContext.Current.Server.MapPath(strTemplateUrl));
            stream.Close();//立即关闭创建文件进程，以便下面读取文件
        }
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath(strTemplateUrl), FileMode.Open, FileAccess.Read);

        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet(strSheetName);
        if (sheet == null)
            sheet = hssfworkbook.GetSheetAt(0);//取第一个工作表
        try
        {
            //列标题数据填充
            for (int i = 0; i < arrTitle.Count; i++)
            {
                string[] arr = arrTitle[i] as string[];
                //获取行
                IRow row = sheet.CreateRow(Int32.Parse(arr[0].ToString()) - 1);
                ICell cell = row.CreateCell(0);
                cell.SetCellValue(arr[1].ToString());
                //合并列
                sheet.AddMergedRegion(new CellRangeAddress(i, i, 0, SourceTable.Columns.Count - 1));
                //格式设置
                ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
                cellStyle.Alignment = HorizontalAlignment.LEFT;
                //字体设置
                NPOI.SS.UserModel.IFont font = hssfworkbook.CreateFont();
                //font.Boldweight = (short)FontBoldWeight.BOLD;
                if (arr[2].ToString() == "true")//正标题 字体放大
                {
                    font.FontHeight = 400;
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                }
                cellStyle.SetFont(font);
                cell.CellStyle = cellStyle;
            }
            
            //构建表头
            if (HasColumns)
            {
                IRow row = sheet.CreateRow(intTitleRowCount - 1);
                //表头所在行号 即intTitleRowCount标题行数
                for (int i = 0; i < arrRow.Count; i++)
                {
                    string[] arr = arrRow[i] as string[];
                    //获取行
                    ICell cell = row.CreateCell(Int32.Parse(arr[0].ToString()) - 1);
                    cell.SetCellValue(arr[1].ToString());
                    //格式设置
                    ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                    //字体设置
                    NPOI.SS.UserModel.IFont font = hssfworkbook.CreateFont();
                    //font.Boldweight = (short)FontBoldWeight.BOLD;
                    cellStyle.SetFont(font);
                    //边框设置
                    cellStyle.BorderBottom = BorderStyle.THIN;
                    cellStyle.BorderTop = BorderStyle.THIN;
                    cellStyle.BorderLeft = BorderStyle.THIN;
                    cellStyle.BorderRight = BorderStyle.THIN;
                    cell.CellStyle = cellStyle;
                    //宽度设置
                    sheet.SetColumnWidth(Int32.Parse(arr[0].ToString()) - 1, (arr[1].ToString().Length) * 420);
                }
            }
            //表尾填充
            for (int i = 0; i < arrEnd.Count; i++)
            {
                string[] arr = arrEnd[i] as string[];
                int intEnd = Int32.Parse(arr[0].ToString()) + SourceTable.Rows.Count + intTitleRowCount - 1;//插入表尾位置
                //获取行
                IRow row = sheet.CreateRow(intEnd);
                ICell cell = row.CreateCell(0);
                cell.SetCellValue(arr[1].ToString());
                //构建一个合并区域
                sheet.AddMergedRegion(new CellRangeAddress(intEnd, intEnd, 0, arrRow.Count - 1));

                //格式设置
                ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
                cellStyle.Alignment = HorizontalAlignment.RIGHT;
                //字体设置
                NPOI.SS.UserModel.IFont font = hssfworkbook.CreateFont();
                //font.Boldweight = (short)FontBoldWeight.BOLD;
                cellStyle.SetFont(font);
                cell.CellStyle = cellStyle;
            }
            //列主体数据填充
            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                int intTopWidth = 0;
                int intMainWidth = 0;
                //创建新行
                IRow row = sheet.CreateRow(i + intTitleRowCount);
                for (int j = 0; j < arrRow.Count; j++)
                {
                    string[] arr = arrRow[j] as string[];
                    //创建新列
                    ICell cell = row.CreateCell(Int32.Parse(arr[0].ToString()) - 1);
                    cell.SetCellValue(SourceTable.Rows[i][arr[1].ToString()].ToString());

                    //列宽度设置
                    intTopWidth = sheet.GetColumnWidth(Int32.Parse(arr[0].ToString()) - 1);//表头宽度
                    intMainWidth = intMainWidth > (SourceTable.Rows[i][arr[1].ToString()].ToString().Length) * 420 ? intMainWidth : (SourceTable.Rows[i][arr[1].ToString()].ToString().Length) * 420;//表体宽度
                    if (intTopWidth < intMainWidth)//如果数据宽度比表头宽度大，则以数据宽度重新设置
                        sheet.SetColumnWidth(Int32.Parse(arr[0].ToString()) - 1, intMainWidth);
                    //边框设置
                    ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
                    cellStyle.Alignment = HorizontalAlignment.LEFT;
                    cellStyle.BorderBottom = BorderStyle.THIN;
                    cellStyle.BorderTop = BorderStyle.THIN;
                    cellStyle.BorderLeft = BorderStyle.THIN;
                    cellStyle.BorderRight = BorderStyle.THIN;
                    cell.CellStyle = cellStyle;
                }
                //固定列
                //sheet.CreateFreezePane(4, 0, 4, 0);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                hssfworkbook.Write(stream);
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式   
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
                curContext.Response.BinaryWrite(stream.GetBuffer());
                curContext.Response.End();
            }
        }
        catch (Exception e)
        {
            //AppendToFile(e.ToString());
        }
    }
    #endregion
}