using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
using System.Web.Services;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

/// <summary>
/// "收文管理功能"功能(查看所有人的收文)
/// 创建人：魏林
/// 创建时间：2013-07-20
/// </summary>
public partial class Channels_OA_SW_ZZ_SWAllList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strJson = "";

        if (Request["action"] != null)
        {
            switch (Request["action"].ToString())
            {
                case "getGridInfo":
                    strJson = getGridInfo(Request["strStatus"].ToString());
                    break;
                default:
                    break;
            }
            Response.Write(strJson);
            Response.End();
        }
    }

    //获取列表信息
    private string getGridInfo(string strStatus)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaSwInfoVo TOaSwInfoVo = new TOaSwInfoVo();
        TOaSwInfoVo.SW_STATUS = strStatus;
        TOaSwInfoVo.SORT_FIELD = "ID";
        TOaSwInfoVo.SORT_TYPE = "DESC";
        if (Request["FROMCODE"] != null)
        {
            if (Request["FROMCODE"].ToString().Trim() != "")
            { TOaSwInfoVo.FROM_CODE = Request["FROMCODE"].ToString().Trim(); }
        }
        if (Request["SWCODE"] != null)
        {
            if (Request["SWCODE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_CODE = Request["SWCODE"].ToString().Trim(); }
        }
        if (Request["SWFROM"] != null)
        {
            if (Request["SWFROM"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_FROM = Request["SWFROM"].ToString().Trim(); }
        }
        if (Request["SWTITLE"] != null)
        {
            if (Request["SWTITLE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_TITLE = Request["SWTITLE"].ToString().Trim(); }
        }
        if (Request["SIGNDATE"] != null)
        {
            if (Request["SIGNDATE"].ToString().Trim() != "")
            { TOaSwInfoVo.SW_SIGN_DATE = Request["SIGNDATE"].ToString().Trim(); }
        }
        if (Request["SUBJECTWORD"] != null)
        {
            if (Request["SUBJECTWORD"].ToString().Trim() != "")
            { TOaSwInfoVo.SUBJECT_WORD = Request["SUBJECTWORD"].ToString().Trim(); }
        }

        DataTable dt = new TOaSwInfoLogic().SelectByTable(TOaSwInfoVo, intPageIndex, intPageSize);
        int intTotalCount = new TOaSwInfoLogic().GetSelectResultCount(TOaSwInfoVo);

        string Json = CreateToJson(dt, intTotalCount);
        return Json;
    }

    protected void btnPrintSW_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SW.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
        cellStyle.WrapText = true;
        cellStyle.VerticalAlignment = VerticalAlignment.CENTER;
        ICell cell;

        DataTable dt = new TOaSwInfoLogic().GetSWDetails(txtSWID.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            sheet.GetRow(1).GetCell(2).SetCellValue(dt.Rows[0]["FROM_CODE"].ToString());           //原文编号
            sheet.GetRow(1).GetCell(4).SetCellValue(dt.Rows[0]["SW_FROM"].ToString());           //来文机关
            if (dt.Rows[0]["SW_DATE"].ToString() != "")
                sheet.GetRow(1).GetCell(6).SetCellValue(DateTime.Parse(dt.Rows[0]["SW_DATE"].ToString()).ToShortDateString());           //收到日期
            sheet.GetRow(2).GetCell(2).SetCellValue(dt.Rows[0]["SW_CODE"].ToString());           //收文编号
            if (dt.Rows[0]["PIGONHOLE_DATE"].ToString() != "")
                sheet.GetRow(2).GetCell(4).SetCellValue(DateTime.Parse(dt.Rows[0]["PIGONHOLE_DATE"].ToString()).ToShortDateString());           //办结日期
            sheet.GetRow(3).GetCell(2).SetCellValue(dt.Rows[0]["SW_TITLE"].ToString());           //标题
            cell = sheet.GetRow(4).GetCell(2);
            cell.CellStyle = cellStyle;
            cell.SetCellValue(dt.Rows[0]["SW_PLAN3"].ToString() + "\n" + dt.Rows[0]["SW_PLAN4"].ToString() + "\n" + dt.Rows[0]["SW_PLAN5"].ToString());  //领导批示
            cell = sheet.GetRow(5).GetCell(2);
            cell.CellStyle = cellStyle;
            cell.SetCellValue(dt.Rows[0]["SW_PLAN2"].ToString());           //办公室意见
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
                "attachment;filename=" + HttpUtility.UrlEncode("收文.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();

        }
    }
}