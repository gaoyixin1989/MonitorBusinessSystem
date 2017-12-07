using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Text;

public partial class Channels_Env_ZZ_Fill_FilOutport : System.Web.UI.Page
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
        if (Request["type"] != null && Request["action"] != null)
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
                    //湖库导出
                    case "Lake":
                        strXmlPath = "../xmlTemp/ZZ_Export/LakeTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/湖库导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "湖库.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //地下饮用水导出
                    case "DrinkUnder":
                        strXmlPath = "../xmlTemp/ZZ_Export/DrinkUnderTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/地下水导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "地下水导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //河流导出
                    case "River":
                        strXmlPath = "../xmlTemp/ZZ_Export/RiverTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/河流导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "河流导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //城考导出
                    case "RiverCity":
                        strXmlPath = "../xmlTemp/ZZ_Export/RiverCityTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/城考导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "城考导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //规划断面导出
                    case "RiverPlan":
                        strXmlPath = "../xmlTemp/ZZ_Export/RiverPlanTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/规划断面导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "规划断面导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //责任目标导出
                    case "RiverTarget":
                        strXmlPath = "../xmlTemp/ZZ_Export/RiverTargetTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/责任目标导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "责任目标导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //饮用水源地导出（地表）
                    case "DrinkSource":
                        strXmlPath = "../xmlTemp/ZZ_Export/DrinkSourceTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/地表水饮用水导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "地表水饮用水导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //饮用水源地导出（地下）
                    case "UnderDrinkSource":
                        strXmlPath = "../xmlTemp/ZZ_Export/UnderDrinkSourceTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/地下水饮用水导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "地下水饮用水导出.xls";
                        sheet = ExcelExport(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //降水
                    case "Rain":
                        strXmlPath = "../xmlTemp/ZZ_Export/RainTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/降水导出.xls";
                        strSheetName = "Sheet1";
                        strFileName = "降水.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //功能区噪声导出
                    case "NoiseFun":
                        strXmlPath = "../xmlTemp/ZZ_Export/FunctionNoiseTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/功能区噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "功能区噪声.xls"; 
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //道路交通噪声导出
                    case "NoiseRoad":
                        strXmlPath = "../xmlTemp/ZZ_Export/RoadNoiseTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/道路交通噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "道路交通噪声.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                    //区域环境噪声导出
                    case "NoiseArea":
                        strXmlPath = "../xmlTemp/ZZ_Export/AreaNoiseTemple.xml";
                        strExcelPath = "../ZZ_ExcelTemp/区域环境噪声.xls";
                        strSheetName = "Sheet1";
                        strFileName = "区域环境噪声.xls";
                        sheet = ExcelExportSpecial(strYear, strMonth, strXmlPath, strExcelPath, strSheetName);
                        break;
                }
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
}