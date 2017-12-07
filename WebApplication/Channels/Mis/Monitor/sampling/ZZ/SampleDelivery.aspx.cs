using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Globalization;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using System.Configuration;

/// <summary>
/// 功能描述：样品交接
/// 创建日期：2013-5-8
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SampleDelivery : PageBase
{
    private static string strQC = "";

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
            //监测项目类别
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["TaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo(LogInfo.UserInfo.ID, "sample_delivery", "021", "01", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "sample_delivery", "021", "01");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo(LogInfo.UserInfo.ID, "sample_delivery", strOneGridId, "021", "01");
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId, string strTaskID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheet(strTaskID, strTwoGridId, "021");
        //样品编号生成
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());

            if (objSample.SAMPLE_CODE.Length == 0)
            {
                //objSample.SAMPLE_CODE = GetSampleCode_QHD(dt.Rows[i]["ID"].ToString());

                TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
                TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
                TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
                objSerial.SERIAL_TYPE = "2";

                objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

                objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

                new TMisMonitorSampleInfoLogic().Edit(objSample);

                dt.Rows[i]["SAMPLE_CODE"] = objSample.SAMPLE_CODE;
            }
        }

        int intTotalCount = dt.Rows.Count;
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 发送到下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strTaskId)
    {
        bool IsSuccess = true;
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo(LogInfo.UserInfo.ID, "sample_delivery", strTaskId, "021", "01");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strMonitorID = dt.Rows[i]["MONITOR_ID"].ToString();
            TMisMonitorSubtaskVo objSsubtask = new TMisMonitorSubtaskVo();
            objSsubtask.TASK_ID = strTaskId;
            objSsubtask.MONITOR_ID = strMonitorID;
            objSsubtask = new TMisMonitorSubtaskLogic().Details(objSsubtask);
            objSsubtask.SAMPLE_ACCESS_ID = LogInfo.UserInfo.ID;
            objSsubtask.SAMPLE_ACCESS_DATE = DateTime.Now.ToString();
            objSsubtask.TASK_STATUS = "03";
            if (!new TMisMonitorSubtaskLogic().Edit(objSsubtask))
                IsSuccess = false;
        }
        
        if (IsSuccess)
        {
            //如果配置文件的IsToDelivery=0，则直接跳过分析任务分配环节，到监测分析环节
            if (ConfigurationManager.AppSettings["IsToDelivery"].ToString() != "1")
                new TMisMonitorResultLogic().sendToAnalysis(strTaskId, "03", "01", "20");
        }

        return IsSuccess == true ? "1" : "0";
    }

    #region 打印
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;

        string strCompanyName = "郑州市环保局";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strPrintId);
        if (objTask.CLIENT_COMPANY_ID.Length > 0)
        {
            strCompanyName = new i3.BusinessLogic.Channels.Base.Company.TBaseCompanyInfoLogic().Details(objTask.CLIENT_COMPANY_ID).COMPANY_NAME;
        }

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = strPrintId;
        DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        string strSampleIDs = "";
        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        }
        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "021","0");

        int iPageCount = dt.Rows.Count / 7;
        if (dt.Rows.Count % 7 != 0)
            iPageCount += 1;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/QHDSamplingCode.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        //sheet复制
        for (int k = 1; k < iPageCount; k++)
        {
            hssfworkbook.CloneSheet(0);
            hssfworkbook.SetSheetName(k, "Sheet" + (k + 1).ToString());
        }
        for (int m = 1; m <= iPageCount; m++)
        {
            ISheet sheet = hssfworkbook.GetSheet("Sheet" + m.ToString());

            sheet.GetRow(2).GetCell(0).SetCellValue("委托单位： " + strCompanyName);
            sheet.GetRow(2).GetCell(4).SetCellValue("任务编号： " + objTask.TICKET_NUM);

            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.Clear();
            for (int n = (m - 1) * 7; n < m * 7; n++)
            {
                if (n >= dt.Rows.Count)
                    break;
                dtNew.ImportRow(dt.Rows[n]);
            }
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {

                sheet.GetRow(i + 4).GetCell(0).SetCellValue((i + 1).ToString());
                if (objTask.SAMPLE_SOURCE == "抽样")
                    sheet.GetRow(i + 4).GetCell(1).SetCellValue(dtNew.Rows[i]["SAMPLE_NAME"].ToString());
                else
                    sheet.GetRow(i + 4).GetCell(1).SetCellValue(dtNew.Rows[i]["SAMPLE_NAME"].ToString());
                sheet.GetRow(i + 4).GetCell(3).SetCellValue(dtNew.Rows[i]["MONITOR_TYPE_NAME"].ToString());
                sheet.GetRow(i + 4).GetCell(4).SetCellValue(dtNew.Rows[i]["ITEM_NAME"].ToString());
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
                "attachment;filename=" + HttpUtility.UrlEncode("水质样品交接记录表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    //点位
    private void GetPoint_UnderTask(string strSubTaskID, ref string strSampleIDs)
    {
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strSampleIDs.Contains(dt.Rows[i]["ID"].ToString()))
            {
                strSampleIDs += (strSampleIDs.Length > 0 ? "','" : "") + dt.Rows[i]["ID"].ToString();
            }
        }
    }

    private string GetCompany_UnderTask(string strTaskID)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strTaskID);
        TMisMonitorTaskCompanyVo objCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);

        return objCompany.COMPANY_NAME;
    }

    protected void btnImportCode_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;

        string strCompanyName = GetCompany_UnderTask(strPrintId);

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = strPrintId;
        DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        string strSampleIDs = "";
        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        }

        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "021","0");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SamplingCodingSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(1).SetCellValue(DateTime.Now.ToString("yyyy年MM月dd日"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int j = i + 1;
            sheet.GetRow(i + 6).GetCell(0).SetCellValue(j.ToString());
            sheet.GetRow(i + 6).GetCell(1).SetCellValue(strCompanyName + dt.Rows[i]["SAMPLE_NAME"].ToString());
            //sheet.GetRow(i + 6).GetCell(3).SetCellValue("1");
            sheet.GetRow(i + 6).GetCell(5).SetCellValue(dt.Rows[i]["SAMPLE_CODE"].ToString());
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
                "attachment;filename=" + HttpUtility.UrlEncode("样品编码表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion
    /// <summary>
    /// 现场项目结果保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSampleCode(string id, string strSampleCode, string strIsOk, string strAskingdate)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = id;
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.REMARK4 = strIsOk == "" ? "###" : strIsOk;
        objSample.REMARK5 = strAskingdate == "" ? "###" : UTCConvert(strAskingdate);
        bool isSuccess = new TMisMonitorSampleInfoLogic().Edit(objSample);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>  
    /// GMT时间转成本地时间  
    /// </summary>  
    /// <param name="gmt">字符串形式的GMT时间</param>  
    /// <returns></returns>  
    public static string UTCConvert(string strUtcDateTime)
    {
        if (!strUtcDateTime.Contains("UTC"))
            return strUtcDateTime;

        string fmtDate = "ddd MMM d HH:mm:ss 'UTC'zz'00' yyyy";
        CultureInfo ciDate = CultureInfo.CreateSpecificCulture("en-US");
        //将C#时间转换成JS时间字符串    
        string JSstring = DateTime.Now.ToString(fmtDate, ciDate);
        //将JS时间字符串转换成C#时间    
        DateTime dt = DateTime.ParseExact(strUtcDateTime, fmtDate, ciDate);
        return dt.ToString("yyyy-MM-dd");
    }
    /// <summary>
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strTaskId, string strCompanyId)
    {
        if (strCompanyId == "") return "";
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }
    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }
    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }
    /// <summary>
    /// 删除样品
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteSample(string strSampleID)
    {
        bool IsSuccess = new TMisMonitorResultLogic().deleteSampleInfo(strSampleID);
        return IsSuccess == true ? "1" : "0";
    }

    protected void btnExportSamplesOrder_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;
        DataTable dt = new TMisMonitorResultLogic().getSampleOrderInfo_ZZ(strPrintId);

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../Result/ZZ/template/样品分析通知单.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            string strId = row["ID"] == null ? "" : row["ID"].ToString();

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

        string strTaskNum = new TMisMonitorTaskLogic().Details(strPrintId).TICKET_NUM;
        sheet.GetRow(2).GetCell(6).SetCellValue(strTaskNum);

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
    /// <summary>
    /// 获取标准盲样的信息
    /// </summary>
    /// <param name="strSampleId">样品ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getBlindString(string strSampleId)
    {
        string strBlindString = new TMisMonitorResultLogic().getBlindString(strSampleId);
        return strBlindString;
    }
}