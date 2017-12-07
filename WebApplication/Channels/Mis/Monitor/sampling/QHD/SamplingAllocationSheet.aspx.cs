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

/// <summary>
/// 功能描述：样品交接
/// 创建日期：2013-1-15
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplingAllocationSheet : PageBase
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

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo_QHD("021,02", "01", "1", "", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount_QHD("021,02", "01", "1", "");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo_QHD(strOneGridId, "021,02", "01", "1", true);
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

        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheet_QHD(strTaskID, strTwoGridId, "021,02");
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
        //把样品从交接发送到分配环节
        DataTable dtSample = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheet_QHD(strTaskId, "", "021,02");
        for (int i = 0; i < dtSample.Rows.Count; i++)
        {
            TMisMonitorSampleInfoVo objSampleVo = new TMisMonitorSampleInfoVo();
            objSampleVo.ID = dtSample.Rows[i]["ID"].ToString();
            objSampleVo.NOSAMPLE = "2";
            IsSuccess = new TMisMonitorSampleInfoLogic().Edit(objSampleVo);
        }
        //如果子任务的样品都采样完，刚更新子任务的状态
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo_QHD(strTaskId, "021", "01", "2", true);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strMonitorID = dt.Rows[i]["MONITOR_ID"].ToString();
            TMisMonitorSubtaskVo objSsubtask = new TMisMonitorSubtaskVo();
            objSsubtask.TASK_ID = strTaskId;
            objSsubtask.MONITOR_ID = strMonitorID;
            objSsubtask = new TMisMonitorSubtaskLogic().Details(objSsubtask);
            objSsubtask.SAMPLE_ACCESS_ID = LogInfo.UserInfo.ID;
            objSsubtask.TASK_STATUS = "03";
            if (!new TMisMonitorSubtaskLogic().Edit(objSsubtask))
                IsSuccess = false;
        }

        return IsSuccess == true ? "1" : "0";
    }

    #region 打印
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strPrintId);
        //objSubTask.TASK_ID = strPrintId;
        //DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        string strSampleIDs = "";
        //for (int i = 0; i < dtSub.Rows.Count; i++)
        //{
        //    GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        //}
        GetPoint_UnderTask(strPrintId, ref strSampleIDs);
        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "02,021","1");

        int iPageCount = dt.Rows.Count / 17;
        if (dt.Rows.Count % 17 != 0)
            iPageCount += 1;
        string strWaterType = "000000001,000000018,000000024,000000027,EnvDrinking,EnvDrinkingSource,EnvDWWater,EnvEstuaries,EnvMudRiver,EnvMudSea,EnvRain,EnvReservoir,EnvRiver,EnvSeaBath";
        string strGasType = "000000002,000000021,000000023,EnvAir,EnvDWAir,EnvSpeed";
        string strSoilType = "EnvSoil";
        string strTemplate = "";
        if (strWaterType.Contains(objSubTask.MONITOR_ID))
        {
            strTemplate = "template/QHDSamplingCode_Water.xls";
        }
        else if (strSoilType.Contains(objSubTask.MONITOR_ID))
        {
            strTemplate = "template/QHDSamplingCode_Soil.xls";
        }
        else
        {
            strTemplate = "template/QHDSamplingCode_Gas.xls";
        }
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath(strTemplate), FileMode.Open, FileAccess.Read);
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
            sheet.GetRow(23).GetCell(0).SetCellValue("样品交接数量： " + dt.Rows.Count.ToString());
            sheet.GetRow(2).GetCell(0).SetCellValue(string.Format("  采 样 日 期：  {0}   年  {1}   月  {2}   日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            sheet.GetRow(3).GetCell(0).SetCellValue(string.Format(" 样品交接日期：   {0}   年  {1}   月  {2}   日   {3}  点   {4}  分", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString()));

            sheet.GetRow(1).GetCell(6).SetCellValue(string.Format("第 {0} 页 共 {1} 页", m.ToString(), iPageCount.ToString()));

            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.Clear();
            for (int n = (m - 1) * 17; n < m * 17; n++)
            {
                if (n >= dt.Rows.Count)
                    break;
                dtNew.ImportRow(dt.Rows[n]);
            }
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                string strItmeNum = "";
                TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                objResult.RESULT_STATUS = "01";
                objResult.SAMPLE_ID = dtNew.Rows[i]["ID"].ToString();
                DataTable dtResult = new TMisMonitorResultLogic().SelectByTable(objResult);
                for (int j = 0; j < dtResult.Rows.Count; j++)
                {
                    TBaseItemInfoVo objItem = new TBaseItemInfoVo();
                    objItem.ID = dtResult.Rows[j]["ITEM_ID"].ToString();
                    //objItem.IS_SAMPLEDEPT = "否";
                    DataTable dtItem = new TBaseItemInfoLogic().SelectByTable(objItem);
                    if (dtItem.Rows.Count > 0 && dtItem.Rows[0]["ITEM_NUM"].ToString().Length > 0)
                        strItmeNum += (strItmeNum.Length > 0) ? "," + dtItem.Rows[0]["ITEM_NUM"].ToString() : dtItem.Rows[0]["ITEM_NUM"].ToString();
                }
                sheet.GetRow(i + 6).GetCell(0).SetCellValue((i + 1).ToString());
                sheet.GetRow(i + 6).GetCell(1).SetCellValue(dtNew.Rows[i]["SAMPLE_CODE"].ToString());
                sheet.GetRow(i + 6).GetCell(6).SetCellValue(strItmeNum);
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
                "attachment;filename=" + HttpUtility.UrlEncode("样品交接记录表.xls", Encoding.UTF8));
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
        string strPrintId = this.strPrintId_code.Value;

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strPrintId);
        string strCompanyName = GetCompany_UnderTask(objSubTask.TASK_ID);
        //objSubTask.TASK_ID = strPrintId;
        //DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubTask.TASK_ID);
        string strSampleIDs = "";
        //for (int i = 0; i < dtSub.Rows.Count; i++)
        //{
        //    GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        //}
        GetPoint_UnderTask(strPrintId, ref strSampleIDs);

        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "02,021","1");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SamplingCodingSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(1).SetCellValue(DateTime.Now.ToString("yyyy年MM月dd日"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int j = i + 1;
            sheet.GetRow(i + 6).GetCell(0).SetCellValue(j.ToString());
            if (objTaskVo.TASK_TYPE == "1")  //常规任务
                sheet.GetRow(i + 6).GetCell(1).SetCellValue(dt.Rows[i]["SAMPLE_NAME"].ToString());
            else
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
    public static string saveSampleCode(string id, string strSampleCode, string strSampleCheckOk)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = id;
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.REMARK1 = strSampleCheckOk;
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
}