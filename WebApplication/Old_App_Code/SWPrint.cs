using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using i3.View;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using System.Data;

/// <summary>
///SWPrint 的摘要说明
/// </summary>
public class SWPrint : PageBaseForWF
{
	public SWPrint()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static void WSExport(string fwID)
    {
        string remark = string.Empty;
        DataTable dt = new TOaPartBuyRequstLogic().SelectRemarks(fwID);
        if (dt.Rows.Count > 0)
        {
            remark = dt.Rows[0][0].ToString();//获取物料采购申请信息的remark1的值
            if (remark.Equals("1"))
            {
                #region//工作呈报单打印
                WorkSubmitPrint(fwID);
                #endregion
            }
            else if (remark.Equals("0"))
            {
                #region//领料单打印
                ItemApplye(fwID);
                #endregion
            }
        }
    }

    #region//工作呈报单打印
    private static void WorkSubmitPrint(string fwID)
    {
        string fwDate = string.Empty;
        string KSDate = string.Empty;
        string ZGDate = string.Empty;
        string LDDate = string.Empty;
        string BGDate = string.Empty;
        string KSName = string.Empty;
        string ZGName = string.Empty;
        string LDName = string.Empty;
        string BGName = string.Empty;
        string JBName = string.Empty;
        string strJB = string.Empty;
        string strKS = string.Empty;
        string strZG = string.Empty;
        string strLD = string.Empty;
        string strBG = string.Empty;
        string strItem = string.Empty;
        //获取基本信息
        TOaPartBuyRequstVo objRequst = new TOaPartBuyRequstLogic().Details(fwID);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("ZZ/temple/WorkSubmit.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        if (!string.IsNullOrEmpty(objRequst.APPLY_DATE))
        {
            DateTime date = Convert.ToDateTime(objRequst.APPLY_DATE);//办理日期
            fwDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
        }
        #region//获取料品信息
        DataTable dt = new TOaPartBuyRequstLogic().SelectItemInfo(fwID);
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {
                strItem += "料品编码:" + dr["PART_CODE"].ToString() + " 品名:" + dr["PART_NAME"].ToString() + "  规格:" + dr["MODELS"].ToString() + "  单位:" + dr["UNIT"].ToString() + " 需求数量:" + dr["NEED_QUANTITY"].ToString() + " \n";
            }
        }
        #endregion

        #region//经办人
        if (!string.IsNullOrEmpty(objRequst.APPLY_USER_ID))
        {
            DataTable JBDT = new TOaPartBuyRequstLogic().SelectName(objRequst.APPLY_USER_ID);
            if (JBDT.Rows.Count > 0)
            {
                JBName = JBDT.Rows[0][0].ToString();
            }
            strJB = strItem + " 经办人：" + JBName + "  " + fwDate;
        }
        #endregion

        #region//科室
        if (!string.IsNullOrEmpty(objRequst.APP_DEPT_ID))//科室
        {
            if (!string.IsNullOrEmpty(objRequst.APP_DEPT_DATE))
            {
                DateTime date = Convert.ToDateTime(objRequst.APP_DEPT_DATE);//日期
                KSDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
            }
            DataTable KSDT = new TOaPartBuyRequstLogic().SelectName(objRequst.APP_DEPT_ID);
            if (KSDT.Rows.Count > 0)
            {
                KSName = KSDT.Rows[0][0].ToString();
            }
            strKS = "科室意见:" + objRequst.APP_DEPT_INFO + "   \n   室主任：" + KSName + "  " + KSDate;
        }
        #endregion

        #region//主管领导
        if (!string.IsNullOrEmpty(objRequst.APP_MANAGER_ID))//主管领导
        {
            if (!string.IsNullOrEmpty(objRequst.APP_MANAGER_DATE))
            {
                DateTime date = Convert.ToDateTime(objRequst.APP_MANAGER_DATE);//日期
                ZGDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
            }
            DataTable ZGDT = new TOaPartBuyRequstLogic().SelectName(objRequst.APP_MANAGER_DATE);
            if (ZGDT.Rows.Count > 0)
            {
                ZGName = ZGDT.Rows[0][0].ToString();
            }
            strZG = "主管领导意见:" + objRequst.APP_MANAGER_INFO + "   \n   主管领导：" + ZGName + "  " + ZGDate;
        }
        #endregion

        #region//办公室
        if (!string.IsNullOrEmpty(objRequst.APP_OFFER_ID))//办公室
        {
            if (!string.IsNullOrEmpty(objRequst.APP_OFFER_TIME))
            {
                DateTime date = Convert.ToDateTime(objRequst.APP_OFFER_TIME);//日期
                BGDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
            }
            DataTable BGDT = new TOaPartBuyRequstLogic().SelectName(objRequst.APP_OFFER_ID);
            if (BGDT.Rows.Count > 0)
            {
                BGName = BGDT.Rows[0][0].ToString();
            }
            strBG = "办公室意见:" + objRequst.APP_OFFER_INFO + " \n  办公室：" + BGName + "  " + BGDate;
        }
        #endregion

        #region//领导人
        if (!string.IsNullOrEmpty(objRequst.APP_LEADER_ID))//领导人
        {
            if (!string.IsNullOrEmpty(objRequst.APP_LEADER_DATE))
            {
                DateTime date = Convert.ToDateTime(objRequst.APP_LEADER_DATE);//日期
                LDDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
            }
            DataTable LDDT = new TOaPartBuyRequstLogic().SelectName(objRequst.APP_LEADER_ID);
            if (LDDT.Rows.Count > 0)
            {
                LDName = LDDT.Rows[0][0].ToString();
            }
            strLD = "领导意见:" + objRequst.APP_LEADER_INFO + " \n 领导：" + LDName + "  " + LDDate;
        }
        #endregion

        #region//sheet赋值
        ICellStyle style = hssfworkbook.CreateCellStyle();
        style.WrapText = true;//换行
        style.VerticalAlignment = VerticalAlignment.TOP;//格式顶部
        ICell cell;
        if (!string.IsNullOrEmpty(objRequst.APPLY_DEPT_ID))
        {
            DataTable dtdept = new TOaPartBuyRequstLogic().SelectDept(objRequst.APPLY_DEPT_ID);
            if (dtdept.Rows.Count > 0)
                sheet.GetRow(1).GetCell(2).SetCellValue(dtdept.Rows[0][0].ToString());//呈报科室
        }
        sheet.GetRow(1).GetCell(6).SetCellValue(fwDate);//办结日期
        cell = sheet.GetRow(2).GetCell(1);
        cell.CellStyle = style;
        cell.SetCellValue(strJB);//经办日期
        cell = sheet.GetRow(13).GetCell(1);
        cell.CellStyle = style;
        cell.SetCellValue(strKS);//科室意见
        cell = sheet.GetRow(24).GetCell(1);
        cell.CellStyle = style;
        cell.SetCellValue(strZG);//主管领导意见
        cell = sheet.GetRow(34).GetCell(1);
        cell.CellStyle = style;
        cell.SetCellValue(strBG);//办公室意见
        cell = sheet.GetRow(45).GetCell(1);
        cell.CellStyle = style;
        cell.SetCellValue(strLD);//领导意见
        #endregion

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("工作呈报单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion

    #region//领料单打印
    private static void ItemApplye(string fwID)
    {
        string fwDate = string.Empty;
        string strItem = string.Empty;
        TOaPartBuyRequstVo objRequst = new TOaPartBuyRequstLogic().Details(fwID);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("ZZ/temple/FillMasterial.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        if (!string.IsNullOrEmpty(objRequst.APPLY_DATE))
        {
            DateTime date = Convert.ToDateTime(objRequst.APPLY_DATE);//办理日期
            fwDate = string.Format("{0} 年 {1} 月 {2} 日  ", date.Year, date.Month, date.Day);
        }
        if (!string.IsNullOrEmpty(objRequst.APPLY_DEPT_ID))
        {
            DataTable dtdept = new TOaPartBuyRequstLogic().SelectDept(objRequst.APPLY_DEPT_ID);
            if (dtdept.Rows.Count > 0)
                sheet.GetRow(1).GetCell(2).SetCellValue(dtdept.Rows[0][0].ToString());//部门
        }

        sheet.GetRow(1).GetCell(5).SetCellValue(fwDate);//日期

        #region//获取料品信息
        DataTable dt = new TOaPartBuyRequstLogic().SelectItemInfo(fwID);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sheet.GetRow(i + 3).GetCell(1).SetCellValue(dt.Rows[i]["PART_CODE"].ToString());//编码
                sheet.GetRow(i + 3).GetCell(2).SetCellValue(dt.Rows[i]["PART_NAME"].ToString());//名称
                sheet.GetRow(i + 3).GetCell(3).SetCellValue(dt.Rows[i]["MODELS"].ToString());//规格
                sheet.GetRow(i + 3).GetCell(4).SetCellValue(dt.Rows[i]["UNIT"].ToString());//单位
                sheet.GetRow(i + 3).GetCell(5).SetCellValue(dt.Rows[i]["NEED_QUANTITY"].ToString());//数量
            }
        }
        #endregion

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("填报领导单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion

}