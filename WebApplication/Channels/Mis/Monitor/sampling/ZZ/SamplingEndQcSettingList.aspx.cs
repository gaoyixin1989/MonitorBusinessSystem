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

using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

/// <summary>
/// 功能描述：采样后质控设置
/// 创建日期：2013-05-03
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplingEndQcSettingList : PageBase
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

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo(LogInfo.UserInfo.ID, "sample_qc_end", "024", "01", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "sample_qc_end", "024", "01");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo(LogInfo.UserInfo.ID, "sample_qc_end", strOneGridId, "024", "01");
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

        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheet(strTaskID, strTwoGridId, "024");
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
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo(LogInfo.UserInfo.ID, "sample_qc_end", strTaskId, "024", "01");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strMonitorID = dt.Rows[i]["MONITOR_ID"].ToString();
            TMisMonitorSubtaskVo objSsubtask = new TMisMonitorSubtaskVo();
            objSsubtask.TASK_ID = strTaskId;
            objSsubtask.MONITOR_ID = strMonitorID;
            objSsubtask = new TMisMonitorSubtaskLogic().Details(objSsubtask);
            objSsubtask.TASK_STATUS = "021";
            if (!new TMisMonitorSubtaskLogic().Edit(objSsubtask))
                IsSuccess = false;
        }
        return IsSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 现场项目结果保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSampleCode(string id, string strSampleCode)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = id;
        objSample.SAMPLE_CODE = strSampleCode;
        bool isSuccess = new TMisMonitorSampleInfoLogic().Edit(objSample);

        return isSuccess == true ? "1" : "0";
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

    protected void btnImport_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../template/QcSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskCompanyVo objCompany = new TMisMonitorTaskCompanyVo();
        int isOk = 0;
        getPrintQcInfo_pan(ref objTask, ref objCompany, ref isOk);
        if (isOk == 1)
        {
            sheet.GetRow(2).GetCell(5).SetCellValue(objTask.TICKET_NUM);
            sheet.GetRow(4).GetCell(1).SetCellValue(objCompany.COMPANY_NAME);
            if (objTask.TICKET_NUM.ToUpper().Contains("GL"))
                strTaskType = "管理类监测";
            else
                strTaskType = "应急类监测";
            //strTaskType = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
            sheet.GetRow(4).GetCell(5).SetCellValue(strTaskType);
            sheet.GetRow(5).GetCell(1).SetCellValue(objTask.PROJECT_NAME);
            if (objTask.CONTRACT_TYPE == "02" || objTask.CONTRACT_TYPE == "03")
            {
                sheet.GetRow(6).GetCell(1).SetCellValue("监测表");
            }
            else
            {
                sheet.GetRow(6).GetCell(1).SetCellValue("监测报告");
            }
            sheet.GetRow(6).GetCell(5).SetCellValue(objCompany.COMPANY_NAME);
            sheet.GetRow(8).GetCell(0).SetCellValue(objTask.REMARK1);
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
                "attachment;filename=" + HttpUtility.UrlEncode("监测质控通知单-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void getPrintQcInfo_pan(ref TMisMonitorTaskVo objTask, ref TMisMonitorTaskCompanyVo objCompany, ref int isOk)
    {
        string strTaskId = hidTaskId.Value;
        if (!String.IsNullOrEmpty(strTaskId) && strTaskId.Length > 0)
        {
            objTask = new TMisMonitorTaskLogic().Details(strTaskId);
            objCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);
            isOk = 1;
        }
    }
}