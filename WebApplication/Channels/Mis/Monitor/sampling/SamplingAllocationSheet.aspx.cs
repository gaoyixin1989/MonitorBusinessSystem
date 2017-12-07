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
/// 功能描述：样品信息交接表查询与导出功能
/// 创建日期：2012-12-16
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_SamplingAllocationSheet : PageBase
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
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfo(strContractType, strMonitorType, strAnalyseAssignDate, "03");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strSampleId = this.strSampleId.Value;
        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleId, "03","0");
        //修改打印状态
        new TMisMonitorSampleInfoLogic().updateSamplingAllocationSheetInfoStatus(strSampleId, "03", "1");
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SamplingAllocationSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(0).SetCellValue("交接日期：" + DateTime.Now.ToString("yyyy-MM-dd"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sheet.GetRow(i + 4).GetCell(0).SetCellValue((dt.Rows[i]["SAMPLE_CODE"].ToString()));
            sheet.GetRow(i + 4).GetCell(2).SetCellValue(dt.Rows[i]["SAMPLE_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(3).SetCellValue(dt.Rows[i]["SAMPLE_COUNT"].ToString());
            sheet.GetRow(i + 4).GetCell(5).SetCellValue(dt.Rows[i]["ITEM_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(6).SetCellValue(dt.Rows[i]["IS_OK"].ToString());
            sheet.GetRow(i + 4).GetCell(7).SetCellValue(dt.Rows[i]["SAMPLING_MANAGER_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(8).SetCellValue(dt.Rows[i]["SAMPLE_ACCESS_NAME"].ToString());
            sheet.GetRow(i + 4).GetCell(9).SetCellValue(dt.Rows[i]["SAMPLE_ACCESS_DATE"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[i]["SAMPLE_ACCESS_DATE"].ToString()).ToString("yyyy-MM-dd"));
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
                "attachment;filename=" + HttpUtility.UrlEncode("样品交接记录表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
}