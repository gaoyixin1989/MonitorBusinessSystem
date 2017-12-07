using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
/// 功能描述： 样品分析通知单打印功能
/// 创建日期：2013.07.02
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_ZZ_SamplesOrder : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo();
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        DataTable dt = new TMisMonitorResultLogic().getSamplesOrderInfo_ZZ("021", "03");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strSampleIds = this.strSampleId.Value;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/样品分析通知单.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        //获取基本信息
        DataTable dt = new TMisMonitorResultLogic().getSamplesOrderInfo_ZZ("021", "03");

        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            string strId = row["ID"] == null ? "" : row["ID"].ToString();
            foreach (string id in strSampleIds.Split(','))
            {
                if (id == strId)
                {
                    //将打印状态保存到数据库中
                    TMisMonitorSampleInfoVo TMisMonitorSampleInfoVo = new TMisMonitorSampleInfoVo();
                    TMisMonitorSampleInfoVo.ID = id;
                    TMisMonitorSampleInfoVo.SAMPLES_ORDER_ISPRINTED = "1";
                    new TMisMonitorSampleInfoLogic().Edit(TMisMonitorSampleInfoVo);

                    string strTaskDate = row["TASK_DATE"] == null ? "" : DateTime.Parse(row["TASK_DATE"].ToString()).ToString("yyyy.MM.dd");
                    string strSampleCode = row["SAMPLE_CODE"] == null ? "" : row["SAMPLE_CODE"].ToString();
                    string strCompanyName = row["COMPANY_NAME"] == null ? "" : row["COMPANY_NAME"].ToString();
                    string strSampleName = row["SAMPLE_NAME"] == null ? "" : row["SAMPLE_NAME"].ToString();
                    string strSampleAddr = strCompanyName + strSampleName;
                    string strSampleType = row["SAMPLE_TYPE"] == null ? "" : row["SAMPLE_TYPE"].ToString();
                    string strItemName = row["ITEM_NAME"] == null ? "" : row["ITEM_NAME"].ToString();
                    string strAskingDate = row["ASKING_DATE"] == null ? "" : row["ASKING_DATE"].ToString();
                    string strProjectName = row["PROJECT_NAME"] == null ? "" : row["PROJECT_NAME"].ToString();

                    sheet.GetRow(i + 4).GetCell(0).SetCellValue(strTaskDate);
                    sheet.GetRow(i + 4).GetCell(1).SetCellValue(strSampleCode);
                    if (this.hidden.Value == "1")
                        sheet.GetRow(i + 4).GetCell(2).SetCellValue(strSampleAddr);
                    sheet.GetRow(i + 4).GetCell(3).SetCellValue(strSampleType);
                    sheet.GetRow(i + 4).GetCell(4).SetCellValue(strItemName);
                    sheet.GetRow(i + 4).GetCell(5).SetCellValue(strAskingDate);
                    sheet.GetRow(i + 4).GetCell(6).SetCellValue(strProjectName);
                    i++;
                }
            }
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
                "attachment;filename=" + HttpUtility.UrlEncode("样品分析通知单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
}