using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using i3.ValueObject.Channels.OA.FW;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.BusinessLogic.Channels.OA.FW;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using System.Data;
using i3.BusinessLogic.Channels.OA.SW;

namespace i3.View
{
    /// <summary>
    ///ZZFWBase 的摘要说明
    /// </summary>
    public class ZZFWBase : PageBaseForWF
    {
        public ZZFWBase()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 导出、打印发文
        /// </summary>
        /// <param name="fwID">发文ID</param>
        public void FWExport(string fwID)
        {
            //获取基本信息
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(fwID);
            
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/FW.xls"), FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");

            //sheet.GetRow(3).GetCell(2).SetCellValue("签发");//签发
            //sheet.GetRow(3).GetCell(4).SetCellValue("会签");//会签
            //sheet.GetRow(5).GetCell(2).SetCellValue("文件标题");//文件标题
            //sheet.GetRow(6).GetCell(2).SetCellValue("附件");//附件
            //sheet.GetRow(7).GetCell(2).SetCellValue("主送单位");//主送单位
            //sheet.GetRow(8).GetCell(2).SetCellValue("抄报单位");//抄报单位
            //sheet.GetRow(8).GetCell(4).SetCellValue("抄送单位");//抄送单位
            //sheet.GetRow(11).GetCell(1).SetCellValue("科室审核");//科室审核
            //sheet.GetRow(11).GetCell(3).SetCellValue("拟稿人");//拟稿人
            //sheet.GetRow(12).GetCell(2).SetCellValue("编号");//编号
            //sheet.GetRow(12).GetCell(3).SetCellValue("2013年印发");//印发日期
            //sheet.GetRow(13).GetCell(2).SetCellValue("说明");//说明
            //sheet.GetRow(14).GetCell(2).SetCellValue("主题词");//主题词

            string fwDate = "";

            if (!string.IsNullOrEmpty(objFW.FW_DATE))
            {
                DateTime date = Convert.ToDateTime(objFW.FW_DATE);
                fwDate = string.Format("{0} 年 {1} 月 {2} 日  印发", date.Year, date.Month, date.Day);
            }
            else
            {
                fwDate = string.Format("年   月   日  印发");
            }

            string fwNo = "";

            if (string.IsNullOrEmpty(objFW.YWNO))
            {
                fwNo = string.Format("﹝     ﹞    号");
            }
            else
            {
                fwNo = string.Format("﹝{0}﹞    号", objFW.FWNO);
            }

            TOaAttVo tOaAttVo = new TOaAttVo();

            tOaAttVo.BUSINESS_ID = objFW.ID;
            tOaAttVo.BUSINESS_TYPE = "FWFile";

            tOaAttVo = new TOaAttLogic().Details(tOaAttVo);

            sheet.GetRow(3).GetCell(2).SetCellValue(objFW.ISSUE_INFO);//签发
            sheet.GetRow(3).GetCell(4).SetCellValue(objFW.CTS_INFO);//会签
            sheet.GetRow(5).GetCell(2).SetCellValue(objFW.FW_TITLE);//文件标题
            sheet.GetRow(6).GetCell(2).SetCellValue(tOaAttVo.ATTACH_NAME);//附件
            sheet.GetRow(7).GetCell(2).SetCellValue(objFW.ZS_DEPT);//主送单位
            sheet.GetRow(8).GetCell(2).SetCellValue(objFW.CB_DEPT);//抄报单位
            sheet.GetRow(8).GetCell(4).SetCellValue(objFW.CS_DEPT);//抄送单位
            sheet.GetRow(11).GetCell(1).SetCellValue(objFW.APP_INFO);//科室审核
            sheet.GetRow(11).GetCell(3).SetCellValue(!string.IsNullOrEmpty(objFW.DRAFT_ID) ? new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME : "");//拟稿人
            sheet.GetRow(12).GetCell(2).SetCellValue(fwNo);//编号
            sheet.GetRow(12).GetCell(3).SetCellValue(fwDate);//印发日期
            sheet.GetRow(13).GetCell(2).SetCellValue(objFW.REMARK1);//说明
            sheet.GetRow(14).GetCell(2).SetCellValue(objFW.SUBJECT_WORD);//主题词

            using (MemoryStream stream = new MemoryStream())
            {
                hssfworkbook.Write(stream);
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式   
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition",
                    "attachment;filename=" + HttpUtility.UrlEncode("发文.xls", Encoding.UTF8));
                curContext.Response.BinaryWrite(stream.GetBuffer());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 导出、打印收文
        /// </summary>
        /// <param name="fwID">收文ID</param>
        public void SWPrint(string swID)
        {
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SW.xls"), FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");

            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.WrapText = true;
            cellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            ICell cell;

            DataTable dt = new TOaSwInfoLogic().GetSWDetails(swID);

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
                //cell.SetCellValue(dt.Rows[0]["SW_PLAN3"].ToString() + "\n" + dt.Rows[0]["SW_PLAN5"].ToString());  //领导批示
                cell.SetCellValue(dt.Rows[0]["SW_PLAN3"].ToString());  //领导批示
                cell = sheet.GetRow(5).GetCell(2);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(dt.Rows[0]["SW_PLAN2"].ToString());           //办公室意见
                cell = sheet.GetRow(6).GetCell(2);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(dt.Rows[0]["SW_PLAN4"].ToString());           //分管阅办
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
}