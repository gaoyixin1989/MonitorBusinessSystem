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
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using WebApplication;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;

/// <summary>
/// 功能描述：样品交接
/// 创建日期：2013-1-15
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_MisII_sampling_SamplingAllocationSheet : PageBase
{
    private static string strQC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            if (Request.QueryString["WorkID"] != null)
            {
                var FID = Convert.ToInt64(Request.QueryString["FID"]); //获取主流程ID

                TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                objPlanVo.CCFLOW_ID1 = FID.ToString();
                objPlanVo = new TMisContractPlanLogic().Details(objPlanVo);

                if (objPlanVo.ID.Length > 0 && objPlanVo.REAMRK1 == "1")
                {
                    //当前流程属于送样的
                    this.TASK_ID.Value = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    this.SUBTASK_ID.Value = "";
                }
                else
                {
                    //当前流程属于采样的
                    var workID = Convert.ToInt64(Request.QueryString["WorkID"]);  //获取子流程ID

                    var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID).Split('|')[0];

                    this.SUBTASK_ID.Value = identification;
                    this.TASK_ID.Value = "";
                }

            }
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo(Request["strTaskID"].ToString(), Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目类别
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString(), Request["strSubTaskID"].ToString());
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
        }
    }
    /// <summary>
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strTaskID, string strSubTaskID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo_MAS(strTaskID, strSubTaskID, intPageIndex, intPageSize);

        //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
        if (dt.Rows[0]["REMARK3"].ToString() == "true")
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
        }
        else
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
        }

        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount_MAS(strTaskID, strSubTaskID);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId, string strSubTaskID)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo_MAS(strOneGridId, strSubTaskID);
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


        var strCCflowWorkId = Request.QueryString["strCCflowWorkId"];

        var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, Convert.ToInt64(strCCflowWorkId));

        var sampleIdList = identification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {

            sampleIdList.RemoveAt(0);
        }

        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheet_MAS(strTaskID, strTwoGridId, "1,2");


        var newDT = new DataTable();

        foreach (DataColumn column in dt.Columns)
        {
            newDT.Columns.Add(column.ColumnName);
        }



        //样品编号生成
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (sampleIdList.Count > 0 && !sampleIdList.Contains(dt.Rows[i]["ID"].ToString()))
            {

                continue;
            }

            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());
            TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
            TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

            if (objSample.SAMPLE_CODE.Length == 0)
            {
                //objSample.SAMPLE_CODE = GetSampleCode_QHD(dt.Rows[i]["ID"].ToString());
                TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
                objSerial.SERIAL_TYPE = "2";

                objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

                if (objTask.TASK_TYPE == "1")
                    objSample.SAMPLE_CODE = objSample.SAMPLE_NAME;
                else
                    objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

                new TMisMonitorSampleInfoLogic().Edit(objSample);

                dt.Rows[i]["SAMPLE_CODE"] = objSample.SAMPLE_CODE;
            }
            //样品状态,外委除外(废水、地表水)
            if (objTask.CONTRACT_TYPE != "04")
            {
                DataTable dtAtt = new DataTable();
                string strAttValue = string.Empty;
                string strAttID = "''";
                if (objSubtask.MONITOR_ID == "000000001" || objSubtask.MONITOR_ID == "EnvDrinkingSource")
                {
                    strAttID = "'000000017'";
                }
                if (objSubtask.MONITOR_ID == "EnvRiver")
                {
                    strAttID = "'000000211'";
                }
                dtAtt = new i3.BusinessLogic.Channels.Base.DynamicAttribute.TBaseAttributeInfoLogic().GetAttValue(strAttID, dt.Rows[i]["POINT_ID"].ToString());
                for (int j = 0; j < dtAtt.Rows.Count; j++)
                {
                    strAttValue += dtAtt.Rows[j]["ATTRBUTE_TEXT"].ToString() + "、";
                }
                dt.Rows[i]["SAMPLE_STATUS"] = strAttValue.TrimEnd('、');
            }

            newDT.Rows.Add(dt.Rows[i].ItemArray);
        }

        int intTotalCount = newDT.Rows.Count;
        string strJson = CreateToJson(newDT, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 功能描述：样品生成规则 Create By Castle(胡方扬) 2014-04-19
    /// </summary>
    /// <param name="strSampleID"></param>
    /// <returns></returns>
    protected static string GetSampleCode(string strSampleID)
    {
        string strSampleCode = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        strSampleCode = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);
        return strSampleCode;
    }

    #region 打印
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = strPrintId;
        DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        string strSampleIDs = "";
        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        }

        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "021", "0");

        int iPageCount = dt.Rows.Count / 17;
        if (dt.Rows.Count % 17 != 0)
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
                objResult.SAMPLE_ID = dtNew.Rows[i]["ID"].ToString();
                DataTable dtResult = new TMisMonitorResultLogic().SelectByTable(objResult);
                for (int j = 0; j < dtResult.Rows.Count; j++)
                {
                    TBaseItemInfoVo objItem = new TBaseItemInfoVo();
                    objItem.ID = dtResult.Rows[j]["ITEM_ID"].ToString();
                    objItem.IS_SAMPLEDEPT = "否";
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
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "021", "0");

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
    public static string saveSampleCode(string id, string strSampleCode, string strSampleCount)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = id;
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.SAMPLE_COUNT = strSampleCount;
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
    /// 获取样品-监测项目
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <param name="strQcType">质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSampleItem(string strSampleID, string strQcType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();

        dt = new TBaseItemInfoLogic().SelectItemForQC(strSampleID, strQcType);

        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 质控样品添加（外控）
    /// </summary>
    /// <param name="strSampleID">样品Id</param>
    /// <param name="strListQc1">现场空白项目</param>
    /// <param name="strListQc2">现场加标项目</param>
    /// <param name="strListQc3">现场平行项目</param>
    /// <param name="strListQc4">实验室密码平行项目</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveQc(string strSampleID, string strListQc1, string strListQc2, string strListQc3, string strListQc4, string strQc3Count, string strQc4Count)
    {
        bool IsSuccess = true;

        //deleteQcInfo(strSampleID);
        strQC = "";
        strQC += (strListQc1.Trim().Length > 0) ? "1," : "";
        strQC += (strListQc2.Trim().Length > 0) ? "2," : "";
        strQC += (strListQc3.Trim().Length > 0) ? "3," : "";
        strQC += (strListQc4.Trim().Length > 0) ? "4," : "";
        strQC = strQC.TrimEnd(',');

        //现场空白
        if (strListQc1.Trim().Length > 0)
        {
            IsSuccess = AddQcEmptyInfo(strSampleID, strListQc1.Replace(" ", "").TrimEnd(','));
        }

        //现场加标
        if (strListQc2.Trim().Length > 0)
        {
            IsSuccess = AddQcAddInfo(strSampleID, strListQc2.Replace(" ", "").TrimEnd(','));
        }

        //现场平行
        if (strListQc3.Trim().Length > 0)
        {
            IsSuccess = AddQcTwinInfo(strSampleID, "3", strListQc3.Replace(" ", "").TrimEnd(','), strQc3Count);
        }

        //密码平行
        if (strListQc4.Trim().Length > 0)
        {
            IsSuccess = AddQcTwinInfo(strSampleID, "4", strListQc4.Replace(" ", "").TrimEnd(','), strQc4Count);
        }

        return IsSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 清除原有质控信息:样品表、结果表、质控表(3个表)
    /// </summary>
    /// <returns></returns>
    private static void deleteQcInfo(string strSampleID)
    {
        //根据原始样品ID，删除下属的质控表、结果表、结果执行表数据
        //new TMisMonitorQcEmptyOutLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorQcAddLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorQcTwinLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorResultAppLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorResultLogic().DeleteQcWithSampleID(strSampleID);

        //删除样品表
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.QC_SOURCE_ID = strSampleID;
        new TMisMonitorSampleInfoLogic().Delete(objSample);
    }

    /// <summary>
    /// 新增现场空白质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcEmptyInfo(string strSampleID, string strListQc1)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = "1";
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_NAME += "现场空白";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc1.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc1.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = "1";
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcEmptyOutVo objQcEmpty = new TMisMonitorQcEmptyOutVo();
            objQcEmpty.ID = GetSerialNumber("QC_EMPTY_OUT");
            objQcEmpty.RESULT_ID_SRC = strOldResultID;
            objQcEmpty.RESULT_ID_EMPTY = objResult.ID;
            if (!new TMisMonitorQcEmptyOutLogic().Create(objQcEmpty))
                IsSuccess = false;
        }

        return IsSuccess;
    }

    /// <summary>
    /// 新增现场加标质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcAddInfo(string strSampleID, string strListQc2)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = "2";
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_NAME += "现场加标";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc2.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc2.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = "2";
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcAddVo objQcAdd = new TMisMonitorQcAddVo();
            objQcAdd.ID = GetSerialNumber("QcAddId");
            objQcAdd.RESULT_ID_SRC = strOldResultID;
            objQcAdd.RESULT_ID_ADD = objResult.ID;
            objQcAdd.QC_TYPE = "2";
            if (!new TMisMonitorQcAddLogic().Create(objQcAdd))
                IsSuccess = false;
        }
        return IsSuccess;
    }

    /// <summary>
    /// 新增现场平行质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcTwinInfo(string strSampleID, string strQcType, string strListQc3, string strQc3Count)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_CODE = GetSampleCode(strSampleID);
        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
        if (strQcType == "4")
            objSample.SAMPLE_NAME += "密码平行";
        else
            objSample.SAMPLE_NAME += "现场平行";

        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc3.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc3.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = strQcType;
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
            objQcTwin.ID = GetSerialNumber("QcTwinId");
            objQcTwin.RESULT_ID_SRC = strOldResultID;
            objQcTwin.RESULT_ID_TWIN1 = objResult.ID;
            objQcTwin.QC_TYPE = strQcType;
            if (!new TMisMonitorQcTwinLogic().Create(objQcTwin))
                IsSuccess = false;
        }
        if (strQc3Count == "2")
        {
            objSample.ID = GetSerialNumber("MonitorSampleId");
            if (strQcType == "4")
                objSample.SAMPLE_NAME += "密码平行2";
            else
                objSample.SAMPLE_NAME += "现场平行2";
            objSample.SAMPLE_CODE = GetSampleCode(strSampleID);
            objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
            if (!new TMisMonitorSampleInfoLogic().Create(objSample))
                IsSuccess = false;

            for (int i = 0; i < strListQc3.Split(',').Length; i++)
            {
                TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                objResult.SAMPLE_ID = strSampleID;
                objResult.QC_TYPE = "0";
                objResult.ITEM_ID = strListQc3.Split(',')[i];
                objResult = new TMisMonitorResultLogic().Details(objResult);

                strOldResultID = objResult.ID;
                objResult.ID = GetSerialNumber("MonitorResultId");
                objResult.SAMPLE_ID = objSample.ID;
                objResult.QC_TYPE = strQcType;
                objResult.QC_SOURCE_ID = strOldResultID;
                objResult.SOURCE_ID = strOldResultID;
                objResult.QC = strQC;
                if (!new TMisMonitorResultLogic().Create(objResult))
                    IsSuccess = false;
                InsertResultAPP(objResult.ID);

                TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
                objQcTwin.RESULT_ID_SRC = strOldResultID;
                objQcTwin.QC_TYPE = strQcType;
                objQcTwin = new TMisMonitorQcTwinLogic().Details(objQcTwin);
                objQcTwin.TWIN_RESULT2 = objResult.ID;
                if (!new TMisMonitorQcTwinLogic().Edit(objQcTwin))
                    IsSuccess = false;
            }
        }

        return IsSuccess;
    }

    /// <summary>
    /// 新增结果分析执行表信息
    /// </summary>
    /// <returns></returns>
    private static void InsertResultAPP(string strResultID)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.ID = strResultID;
        DataTable dt = new TMisMonitorResultLogic().SelectManagerByTable(objResult);

        TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
        objResultApp.ID = GetSerialNumber("MonitorResultAppId");
        objResultApp.RESULT_ID = strResultID;
        if (dt.Rows.Count > 0)
        {
            objResultApp.HEAD_USERID = dt.Rows[0]["ANALYSIS_MANAGER"].ToString();
            objResultApp.ASSISTANT_USERID = dt.Rows[0]["ANALYSIS_ID"].ToString();
        }
        new TMisMonitorResultAppLogic().Create(objResultApp);
    }

    /// <summary>
    /// 删除样品
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteSample(string strSampleID)
    {
        bool IsSuccess = new TMisMonitorSampleInfoLogic().Delete(strSampleID);
        return IsSuccess == true ? "1" : "0";
    }
}