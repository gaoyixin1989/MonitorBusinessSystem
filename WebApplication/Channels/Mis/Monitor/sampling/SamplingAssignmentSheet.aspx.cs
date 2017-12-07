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
/// 功能描述：采样任务分配表查询导出功能
/// 创建日期：2012-12-15
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_SamplingAssignmentSheet : PageBase
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
            //获取下拉列表信息
            if (Request["type"] != null && Request["type"].ToString() == "getDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getMonitorType")
            {
                strResult = getMonitorType();
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <returns></returns>
    public string getMonitorType()
    {
        TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(TBaseMonitorTypeInfoVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strContractType = Request["strContractType"] == null ? "" : Request["strContractType"].ToString();
        string strAnalyseAssignDate = Request["strAnalyseAssignDate"] == null ? "" : Request["strAnalyseAssignDate"].ToString();
        string strMonitorType = "";
        string spit = "";
        if (Request["strMonitorType"] != null && Request["strMonitorType"].ToString() != "")
        {
            foreach (string strMonitorTypeTemp in Request["strMonitorType"].ToString().Split(',').ToList())
            {
                strMonitorType += spit + strMonitorTypeTemp;
                spit = "','";
            }
        }
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingSheetInfo(strContractType, strMonitorType, strAnalyseAssignDate, "duty_sampling", LogInfo.UserInfo.ID, "02");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strSubTaskId = this.strSubTaskId.Value;
        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingSheetInfoBySubTaskId(strSubTaskId, "02");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/TaskSamplingSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(0).SetCellValue("采样日期：" + DateTime.Now.ToString("yyyy-MM-dd"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sheet.GetRow(i + 4).GetCell(0).SetCellValue((i + 1).ToString());
            sheet.GetRow(i + 4).GetCell(1).SetCellValue(dt.Rows[i]["COMPANY_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(2).SetCellValue(dt.Rows[i]["SAMPLE_ASK_DATE"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[i]["SAMPLE_ASK_DATE"].ToString()).ToString("yyyy-MM-dd"));
            sheet.GetRow(i + 4).GetCell(3).SetCellValue(dt.Rows[i]["CONTRACT_TYPE_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(4).SetCellValue(dt.Rows[i]["MONITOR_TYPE_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(5).SetCellValue(dt.Rows[i]["SAMPLING_MANAGER_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(6).SetCellValue(dt.Rows[i]["SAMPLING_MAN"].ToString());
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
                "attachment;filename=" + HttpUtility.UrlEncode("采样任务分配表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
}