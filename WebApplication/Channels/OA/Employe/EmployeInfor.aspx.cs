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
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.BusinessLogic.Channels.OA.EMPLOYE;

/// 站务管理--人员档案管理
/// 创建时间：2013-01-07
/// 创建人：胡方扬
public partial class Channels_OA_Employe_EmployeInfor : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Params["action"] == "ShowExcelData")
        //{
        //    Response.End();
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strHdSrh = this.hdSrh.Value.Trim();
        strHdSrh = strHdSrh.Length == 0 ? ",,,," : strHdSrh;
        string[] strWhereArr = strHdSrh.Split(',');
        DataTable dt = new TOaEmployeInfoLogic().SelectTestDataLst(strWhereArr, 0, 0);

        FullExcel(dt);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    private void FullExcel(DataTable dt)
    {
        System.DateTime dtNow = System.DateTime.Now;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("Template/PersonalFile.xls"), FileMode.Open, FileAccess.Read);
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
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("员工信息表" + strExcelTime + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void infullExcelValue(HSSFWorkbook hssfworkbook, DataTable dtResult)
    {
        string strSheetName = "Sheet1";

        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 2, i + 3, 46);

            IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(i + 3);

            ICell changingCell = null;

            changingCell = changingRow.GetCell(0);
            changingCell.SetCellValue((i + 1).ToString());//序号
            changingCell = changingRow.GetCell(1);
            changingCell.SetCellValue(dtResult.Rows[i]["USER_ID"].ToString());
            changingCell = changingRow.GetCell(2);
            changingCell.SetCellValue(dtResult.Rows[i]["EMPLOYE_CODE"].ToString());
            changingCell = changingRow.GetCell(3);
            changingCell.SetCellValue(dtResult.Rows[i]["EMPLOYE_NAME"].ToString());
            changingCell = changingRow.GetCell(4);
            changingCell.SetCellValue(dtResult.Rows[i]["ID_CARD"].ToString());
            changingCell = changingRow.GetCell(5);
            changingCell.SetCellValue(dtResult.Rows[i]["SEX"].ToString());
            changingCell = changingRow.GetCell(6);
            changingCell.SetCellValue(dtResult.Rows[i]["BIRTHDAY"].ToString());
            changingCell = changingRow.GetCell(7);
            changingCell.SetCellValue(dtResult.Rows[i]["AGE"].ToString());
            changingCell = changingRow.GetCell(8);
            changingCell.SetCellValue(dtResult.Rows[i]["NATION"].ToString());
            changingCell = changingRow.GetCell(9);
            changingCell.SetCellValue(dtResult.Rows[i]["POLITICALSTATUS"].ToString());
            changingCell = changingRow.GetCell(10);
            changingCell.SetCellValue(dtResult.Rows[i]["EDUCATIONLEVEL"].ToString());
            changingCell = changingRow.GetCell(11);
            changingCell.SetCellValue(dtResult.Rows[i]["DEPART"].ToString());
            changingCell = changingRow.GetCell(12);
            changingCell.SetCellValue(dtResult.Rows[i]["POST"].ToString());
            changingCell = changingRow.GetCell(13);
            changingCell.SetCellValue(dtResult.Rows[i]["POSITION"].ToString());
            changingCell = changingRow.GetCell(14);
            changingCell.SetCellValue(dtResult.Rows[i]["POST_LEVEL"].ToString());
            changingCell = changingRow.GetCell(15);
            changingCell.SetCellValue(dtResult.Rows[i]["POST_DATE"].ToString());
            changingCell = changingRow.GetCell(16);
            changingCell.SetCellValue(dtResult.Rows[i]["ORGANIZATION_TYPE"].ToString());
            changingCell = changingRow.GetCell(17);
            changingCell.SetCellValue(dtResult.Rows[i]["ORGANIZATION_DATE"].ToString());
            changingCell = changingRow.GetCell(18);
            changingCell.SetCellValue(dtResult.Rows[i]["ENTRYDATE"].ToString());
            changingCell = changingRow.GetCell(19);
            changingCell.SetCellValue(dtResult.Rows[i]["TECHNOLOGY_POST"].ToString());
            changingCell = changingRow.GetCell(20);
            changingCell.SetCellValue(dtResult.Rows[i]["APPLY_DATE"].ToString());
            changingCell = changingRow.GetCell(21);
            changingCell.SetCellValue(dtResult.Rows[i]["GRADUATE"].ToString());
            changingCell = changingRow.GetCell(22);
            changingCell.SetCellValue(dtResult.Rows[i]["SPECIALITY"].ToString());
            changingCell = changingRow.GetCell(23);
            changingCell.SetCellValue(dtResult.Rows[i]["GRADUATE_DATE"].ToString());
            changingCell = changingRow.GetCell(24);
            changingCell.SetCellValue(dtResult.Rows[i]["TECHNOLOGY_LEVEL"].ToString());
            changingCell = changingRow.GetCell(25);
            changingCell.SetCellValue(dtResult.Rows[i]["SKILL_LEVEL"].ToString());
            changingCell = changingRow.GetCell(26);
            changingCell.SetCellValue(dtResult.Rows[i]["POST_STATUS"].ToString());
            changingCell = changingRow.GetCell(27);
            changingCell.SetCellValue(dtResult.Rows[i]["INFO"].ToString());
            changingCell = changingRow.GetCell(28);
            changingCell.SetCellValue(dtResult.Rows[i]["ENTRYDATE"].ToString());

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