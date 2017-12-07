using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using i3.ValueObject.Channels.Env.Fill.Air;
using i3.BusinessLogic.Channels.Env.Fill.Air;
using System.Data;
using i3.BusinessLogic.Channels.Env.Fill.FillQry;

/// <summary>
/// 数据填报导出
/// 创建人：魏林 
/// 创建时间：2013-07-01
/// </summary>
public partial class Channels_Env_Fill_FillExportDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ISheet sheet = null;
        DataTable dt = new DataTable();
        string strPoint_ID = string.Empty;
        string strType = string.Empty;
        string strAction = string.Empty;
        string strYear = string.Empty;
        string strMonth = string.Empty;
        string strXmlPath = string.Empty;
        string strExcelPath = string.Empty;
        string strSheetName = string.Empty;
        string strFileName = string.Empty; 
        if (Request["type"] != null && Request["action"]!=null)
        {
            string strDes = string.Empty;
            
            string action = Request["action"].ToString();
            strType = Request["type"].ToString();
            strYear = Request["year"].ToString();
            strMonth = Request["month"];
            
            if (action.Equals("FillData"))
            {
                #region//填报的导出
                switch (strType)
                {
                    //河流导出
                    case "River":
                        strXmlPath = "../xmlTemp/QY_Export/RiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/河流.xls";
                        strSheetName = "Sheet1";
                        strFileName = "河流.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //排污河流导出 黄进军 添加20141114
                    case "SewageRiver":
                        strXmlPath = "../xmlTemp/QY_Export/SewageRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/产业转移工业园排污河流.xls";
                        strSheetName = "Sheet1";
                        strFileName = "产业转移工业园排污河流.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //湖库水质 河流导出 黄进军 添加20141114
                    case "LakeRiver":
                        strXmlPath = "../xmlTemp/QY_Export/LakeRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/湖库水质.xls";
                        strSheetName = "Sheet1";
                        strFileName = "湖库水质.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //水质月报 河流导出 黄进军 添加20141114
                    case "WQRiver":
                        strXmlPath = "../xmlTemp/QY_Export/WQRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/水质月报.xls";
                        strSheetName = "Sheet1";
                        strFileName = "水质月报.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //高桥 河流导出 黄进军 添加20141114
                    case "GQRiver":
                        strXmlPath = "../xmlTemp/QY_Export/GQRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/高桥.xls";
                        strSheetName = "Sheet1";
                        strFileName = "高桥.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //界牌 河流导出 黄进军 添加20141114
                    case "JPRiver":
                        strXmlPath = "../xmlTemp/QY_Export/JPRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/界牌.xls";
                        strSheetName = "Sheet1";
                        strFileName = "界牌.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //蓝藻水华 河流导出 黄进军 添加20141114
                    case "LZSHRiver":
                        strXmlPath = "../xmlTemp/QY_Export/LZSHRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/蓝藻水华.xls";
                        strSheetName = "Sheet1";
                        strFileName = "蓝藻水华.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //重金属 河流导出 黄进军 添加20141114
                    case "MetalRiver":
                        strXmlPath = "../xmlTemp/QY_Export/MetalRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/重金属.xls";
                        strSheetName = "Sheet1";
                        strFileName = "重金属.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //饮用水源 河流导出 黄进军 添加20141114
                    case "WaterRiver":
                        strXmlPath = "../xmlTemp/QY_Export/WaterRiverTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/饮用水源.xls";
                        strSheetName = "Sheet1";
                        strFileName = "饮用水源.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //底泥重金属导出
                    case "Metal":
                        strXmlPath = "../xmlTemp/QY_Export/Metal.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/底泥重金属.xls";
                        strSheetName = "Sheet1";
                        strFileName = "底泥重金属.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //功能区噪声导出
                    case "NoiseFun":
                        strXmlPath = "../xmlTemp/QY_Export/FunctionNoiseTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/功能区噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "功能区噪声.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //道路交通噪声导出
                    case "NoiseRoad":
                        strXmlPath = "../xmlTemp/QY_Export/RoadNoiseTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/道路交通噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "道路交通噪声.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //区域环境噪声导出
                    case "NoiseArea":
                        strXmlPath = "../xmlTemp/QY_Export/AreaNoiseTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/区域环境噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "区域环境噪声.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //饮用水源地导出
                    case "DrinkSource":
                        strXmlPath = "../xmlTemp/Export/DrinkSourceTemple.xml";
                        strExcelPath = "../excelTemp/饮用水源地.xls";
                        strSheetName = "Sheet1";
                        strFileName = "饮用水源地.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //湖库饮用水导出 黄进军20141105
                    case "DrinkUnder":
                        strXmlPath = "../xmlTemp/QY_Export/DrinkUnderTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/湖库饮用水.xls";
                        strSheetName = "Sheet1";
                        strFileName = "湖库饮用水.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //湖库导出
                    case "Lake":
                        strXmlPath = "../xmlTemp/Export/LakeTemple.xml";
                        strExcelPath = "../excelTemp/湖库.xls";
                        strSheetName = "Sheet1";
                        strFileName = "湖库.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        sheet = ModifySheetTitle(sheet, strYear, strMonth);
                        break;
                    //环境空气（天）
                    case "Air":
                        //int Rtnresult = new TEnvFillAirLogic().ExcelOutCal(strYear, strMonth, TEnvFillAirHourVo.T_ENV_FILL_AIR_ITEM_TABLE);
                        //if (Rtnresult > 0)
                        //{
                            strXmlPath = "../xmlTemp/QY_Export/AirTemple.xml";
                            strExcelPath = "../excelTemp/QY_excelTemp/环境空气(天).xls";
                            strSheetName = "Sheet1";
                            strFileName = "环境空气(天).xls";
                            sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        //}
                        break;
                    //环境空(小时)
                    case "AirHour":
                        strXmlPath = "../xmlTemp/Export/AirHourTemple.xml";
                        strExcelPath = "../excelTemp/环境空气(小时)导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "环境空(小时).xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //环境空科室(天)
                    case "AirKS":
                        strXmlPath = "../xmlTemp/Export/AirKsTemple.xml";
                        strExcelPath = "../excelTemp/环境空气科室(天)导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "环境空气科室(天).xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //降尘
                    case "Dust":
                        strXmlPath = "../xmlTemp/QY_Export/DustTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/降尘导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "降尘.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //硫酸盐化速率
                    case "Alkali":
                        strXmlPath = "../xmlTemp/Export/AlkaliTemple.xml";
                        strExcelPath = "../excelTemp/硫酸盐化速率导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "硫酸盐化速率.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //降水
                    case "Rain":
                        strXmlPath = "../xmlTemp/QY_Export/RainTemple.xml";
                        strExcelPath = "../excelTemp/QY_excelTemp/降水导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "降水.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //近岸直排
                    case "OffShore":
                        strXmlPath = "../xmlTemp/Export/OffShoreTemple.xml";
                        strExcelPath = "../excelTemp/近岸直排导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "近岸直排.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //近岸海域
                    case "Sea":
                        strXmlPath = "../xmlTemp/Export/SeaTemple.xml";
                        strExcelPath = "../excelTemp/近岸海域导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "近岸海域.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //入海河口
                    case "Estuaries":
                        strXmlPath = "../xmlTemp/Export/EstuariesTemple.xml";
                        strExcelPath = "../excelTemp/入海河口导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "入海河口.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //污染源常规（废水）
                    case "PolluteWater":
                            strXmlPath = "../xmlTemp/Export/PolluteWater.xml";
                            strExcelPath = "../excelTemp/污染源常规废水导出.xls";
                            strSheetName = "Sheet1";
                            strFileName = "污染源常规废水导出.xls";
                            sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                            break;
                    //污染源常规（废气）
                    case "PolluteAir":
                            strXmlPath = "../xmlTemp/Export/PolluteAir.xml";
                            strExcelPath = "../excelTemp/污染源常规废气导出.xls";
                            strSheetName = "Sheet1";
                            strFileName = "污染源常规废气导出.xls";
                            sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName); 
                        break;
                }
                #endregion
            }
            else if (action.Equals("FillQry"))
            {
                string searchType = Request["searchType"];//查询类型
                string half = Request["half"];//半年
                string quarter = Request["quarter"];//季度
                strPoint_ID = Request["point_id"]; 

                string months = getMonths(searchType, half, quarter, strMonth, ref strDes);

                #region//填报查询导出
                strFileName = "填报统计导出.xls";
                dt = new FillQryLogic().GetDataInfo(strType, strYear, months, strPoint_ID, strDes);
                sheet = CreatSheet(dt, searchType,half,quarter,strMonth);
                #endregion
            }
            
            #region//输出文件到浏览器
            using (MemoryStream stream = new MemoryStream())
            {
                sheet.Workbook.Write(stream);
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式   
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition",
                    "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
                curContext.Response.BinaryWrite(stream.GetBuffer());
                curContext.Response.End();
            }
            #endregion
        }
    }

    #region//填报统计导出
    public ISheet CreatSheet(DataTable table, string searchType,string half,string quarter,string strMonth)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet();
        ICellStyle cellStyle = workbook.CreateCellStyle();
        IFont f = workbook.CreateFont();
        #region//创建列名
        IRow Row = sheet.CreateRow(0);
        ICell Cell = Row.CreateCell(0);
        Cell.SetCellValue("年度");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(1);
        if (searchType.Equals("1"))
        {
            Cell.SetCellValue("月度");
        }
        else if (searchType.Equals("2"))
        {
            Cell.SetCellValue("季度");
        }
        else if (searchType.Equals("3"))
        {
            Cell.SetCellValue("半年");
        }
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(2);
        Cell.SetCellValue("监测点名称");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(3);
        Cell.SetCellValue("监测项目名称");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(4);
        Cell.SetCellValue("监测项目最小值");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(5);
        Cell.SetCellValue("监测项目最大值");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");
        Cell = Row.CreateCell(6);
        Cell.SetCellValue("监测项目平均值");
        Cell.CellStyle = getCellStyle(cellStyle, f, "H");

        #endregion
        int rowIndex = 1;
        #region//赋值
        for (int i = 0; i < table.Rows.Count; i++)
        {
            Row = sheet.CreateRow(rowIndex);
            Cell = Row.CreateCell(0);
            Cell.SetCellValue(table.Rows[i]["YEAR"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(1);
            Cell.SetCellValue(table.Rows[i]["TEMP"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(2);
            Cell.SetCellValue(table.Rows[i]["POINT_NAME"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(3);
            Cell.SetCellValue(table.Rows[i]["ITEM_NAME"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(4);
            Cell.SetCellValue(table.Rows[i]["MIN_VALUE"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(5);
            Cell.SetCellValue(table.Rows[i]["MAX_VALUE"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(6);
            Cell.SetCellValue(table.Rows[i]["AVG_VALUE"].ToString());
            Cell.CellStyle = getCellStyle(cellStyle, f, "H");
            Cell = Row.CreateCell(7);
            rowIndex++;
        }
        #endregion
        sheet = AutoSizeColumns(sheet, 2);
        return sheet;
    }

    /// <summary>
    /// 设置单元格样式
    /// </summary>
    /// <param name="wb"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    private ICellStyle getCellStyle(ICellStyle cellStyle, IFont f, string Type)
    {
        switch (Type)
        {
            case "T":
                cellStyle.Alignment = HorizontalAlignment.CENTER;
                f.FontHeightInPoints = 16;
                cellStyle.SetFont(f);
                break;
            case "H":
                cellStyle.Alignment = HorizontalAlignment.CENTER;
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                f.FontHeightInPoints = 10;
                cellStyle.SetFont(f);
                break;

        }
        return cellStyle;
    }
    /// <summary>
    /// 自动设置Excel列宽
    /// </summary>
    /// <param name="sheet">Excel表</param>
    private ISheet AutoSizeColumns(ISheet sheet, int r)
    {
        if (sheet.PhysicalNumberOfRows > 0)
        {
            IRow headerRow = sheet.GetRow(r);

            for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }
        return sheet;
    }
    #endregion

    #region//查询月份
    private string getMonths(string type, string half, string quarter, string month, ref string des)
    {
        string months = string.Empty;
        switch (type)
        {
            case "3":  //半度
                switch (half)
                {
                    case "1":
                        months = "1,2,3,4,5,6";
                        des = "上半年";
                        break;
                    case "2":
                        months = "7,8,9,10,11,12";
                        des = "下半年";
                        break;
                    default:
                        months = "";
                        des = "";
                        break;
                }
                break;
            case "2":  //季度 
                switch (quarter)
                {
                    case "1":
                        months = "1,2,3";
                        des = "一季度";
                        break;
                    case "2":
                        months = "4,5,6";
                        des = "二季度";
                        break;
                    case "3":
                        months = "7,8,9";
                        des = "三季度";
                        break;
                    case "4":
                        months = "10,11,12";
                        des = "四季度";
                        break;
                    default:
                        months = "";
                        des = "";
                        break;
                }
                break;
            case "1":  //月度
                months = month.Replace(';', ',');
                des = months;
                break;
        }
        return months;
    }
    #endregion


    /// <summary>
    /// 导出Excel【支持导出河流、湖泊类】
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="XmlPath"></param>
    /// <param name="ExcelPath"></param>
    /// <param name="SheetName"></param>
    /// <returns></returns>
    public ISheet ExcelExport(string year, string month, string XmlPath, string ExcelPath, string SheetName)
    {
        string strXmlPath = Server.MapPath(XmlPath);
        string strExcelPath = Server.MapPath(ExcelPath);
        ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheet(strXmlPath, strExcelPath, SheetName, year, month);

        return sheet;
    }
    /// <summary>
    /// 导出Excel【支持导出噪声、降尘、空气类】
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="XmlPath"></param>
    /// <param name="ExcelPath"></param>
    /// <param name="SheetName"></param>
    /// <returns></returns>
    public ISheet ExcelExportSpecial(string year, string month, string XmlPath, string ExcelPath, string SheetName)
    {
        string strXmlPath = Server.MapPath(XmlPath);
        string strExcelPath = Server.MapPath(ExcelPath);
        ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheetSpecial(strXmlPath, strExcelPath, SheetName, year, month);

        return sheet;
    }

    /// <summary>
    /// 修改导出EXCEL的标题
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    public ISheet ModifySheetTitle(ISheet sheet, string year, string month)
    {
        IWorkbook workbook = sheet.Workbook;
        ICellStyle cellStyle = workbook.CreateCellStyle();
        IFont f = workbook.CreateFont();
        cellStyle.Alignment = HorizontalAlignment.CENTER;
        f.FontHeightInPoints = 23;
        cellStyle.SetFont(f);

        ICell c = sheet.GetRow(0).GetCell(0);
        int num = sheet.LastRowNum;
        ICell I = sheet.GetRow(num).GetCell(0);
        c.CellStyle = cellStyle;
        //I.CellStyle = cellStyle;
        c.SetCellValue(c.StringCellValue.Replace("{YEAR}", year).Replace("{MONTH}", month));
        I.SetCellValue(I.StringCellValue.Replace("{YEAR}", year).Replace("{MONTH}", month).Replace("{DAY}",DateTime.Now.Day.ToString()));

        return sheet;
    }

}