using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;


/// 功能描述：采样前质控
/// 创建日期：2013-07-31
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplingBeginQc : PageBase
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
            //监测项目类别
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //样品号信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString());
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

        DataTable dt = new TMisMonitorResultLogic().getSamplingBeginQcTaskInfo("8", "01", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getSamplingBeginQcTaskCount("8", "01");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getSamplingBeginQcItemTypeInfo(strOneGridId, "01");
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }

    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getSamplingBeginQcSampleInfo(strTwoGridId);
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getSamplingBeginQcItemInfo(strThreeGridId);
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }
    /// <summary>
    /// 发送到下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strTaskId)
    {
        bool IsSuccess = new TMisMonitorResultLogic().sendSamplingBeginQcTaskToNext(strTaskId, "M11");
        return IsSuccess == true ? "1" : "0";
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
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    [WebMethod]
    public static string getPointName(string strPointId)
    {
        string strReturnValue = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskPointLogic().Details(strPointId).POINT_NAME;
        return strReturnValue;
    }
    [WebMethod]
    public static string saveSample(string strSampleId, string strSampleName)
    {
        TMisMonitorSampleInfoVo TMisMonitorSampleInfoVo = new TMisMonitorSampleInfoVo();
        TMisMonitorSampleInfoVo.ID=strSampleId;
        TMisMonitorSampleInfoVo.SAMPLE_NAME = strSampleName==""?"###":strSampleName;
        bool IsSuccess = new TMisMonitorSampleInfoLogic().Edit(TMisMonitorSampleInfoVo);
        return IsSuccess ? "1" : "0";
    }
    /// <summary>
    /// 获取质控手段名称
    /// </summary>
    /// <param name="strQcId">质控手段编码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcName(string strQcId)
    {
        string strQcName = "";

        if (strQcId == "") return "";
        List<string> list = strQcId.Split(',').ToList();
        string spit = "";
        foreach (string strQc in list)
        {
            if (strQc == "0")
                strQcName = strQcName + spit + "原始样";
            if (strQc == "1")
                strQcName = strQcName + spit + "现场空白";
            if (strQc == "2")
                strQcName = strQcName + spit + "现场加标";
            if (strQc == "3")
                strQcName = strQcName + spit + "现场平行";
            if (strQc == "4")
                strQcName = strQcName + spit + "实验室密码平行";
            if (strQc == "5")
                strQcName = strQcName + spit + "实验室空白";
            if (strQc == "6")
                strQcName = strQcName + spit + "实验室加标";
            if (strQc == "7")
                strQcName = strQcName + spit + "实验室明码平行";
            if (strQc == "8")
                strQcName = strQcName + spit + "标准样";
            spit = ",";
        }
        return strQcName;
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
        bool IsSuccess = new TMisMonitorResultLogic().deleteQcInfo(strSampleID, "");
        if (IsSuccess)
            new TMisMonitorResultLogic().deleteSampleInfo(strSampleID);
        return IsSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 增加样品
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddSample(string strSampleID)
    {
        bool IsSuccess = false;

        //获取原始样信息
        TMisMonitorSampleInfoVo TMisMonitorSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        //根据原始样获取监测结果ID
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.SAMPLE_ID = TMisMonitorSampleInfoVo.ID;
        List<TMisMonitorResultVo> resultList = new TMisMonitorResultLogic().SelectByObject(TMisMonitorResultVo, 0, 1000000);

        string strNewSampleId = GetSerialNumber("MonitorSampleId");
        TMisMonitorSampleInfoVo.ID = strNewSampleId;
        new TMisMonitorSampleInfoLogic().Create(TMisMonitorSampleInfoVo);

        foreach (TMisMonitorResultVo TMisMonitorResultTemp in resultList)
        {
            string strOldResultId = TMisMonitorResultTemp.ID;
            string strNewResultId = GetSerialNumber("MonitorResultId");
            TMisMonitorResultTemp.ID = strNewResultId;
            TMisMonitorResultTemp.SAMPLE_ID = strNewSampleId;
            new TMisMonitorResultLogic().Create(TMisMonitorResultTemp);

            TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
            TMisMonitorResultAppVo.RESULT_ID = strOldResultId;
            TMisMonitorResultAppVo TMisMonitorResultAppVoTemp = new TMisMonitorResultAppLogic().SelectByObject(TMisMonitorResultAppVo);
            TMisMonitorResultAppVoTemp.ID = GetSerialNumber("MonitorResultAppId");
            TMisMonitorResultAppVoTemp.RESULT_ID = strNewResultId;
            new TMisMonitorResultAppLogic().Create(TMisMonitorResultAppVoTemp);
        }
        return IsSuccess == true ? "1" : "0";
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (this.hidPlanId.Value.Length == 0)
            return;

        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.hidPlanId.Value);
        string strContractID = objPlan.CONTRACT_ID;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.PLAN_ID = hidPlanId.Value.Trim();
        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
        TMisMonitorTaskCompanyVo objTaskCompanyVo = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.TESTED_COMPANY_ID);

        string strContractType = getDictName(objTaskVo.CONTRACT_TYPE, "Contract_Type");

        string strMonitorNames = "";
        string strPointNames = "";
        string strItemS = "";
        string strFREQ = "";
        GetInfoForPrint(ref strMonitorNames, ref strPointNames, ref strFREQ, ref strItemS);

        string strSAMPLE_ASK_DATE = DateTime.Now.ToString("yyyy-MM-dd");
        string strSAMPLE_FINISH_DATE = DateTime.Parse(objTaskVo.ASKING_DATE).ToString("yyyy-MM-dd");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/MoniterTaskNotify.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        sheet.GetRow(3).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(4).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(5).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(3).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(4).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(5).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(7).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(7).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(7).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(7).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(8).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(8).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);
        //复制一份
        sheet.GetRow(16).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(17).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(18).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(16).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(17).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(18).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(20).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(20).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(20).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(20).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(21).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(21).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("任务通知单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    private void GetInfoForPrint(ref string strMonitorNames, ref string strPointNames, ref string strFREQ, ref string strItemS)
    {
        if (this.hidPlanId.Value.Length == 0)
            return;

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.hidPlanId.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseMonitorTypeInfoVo objMonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(dt.Rows[i]["MONITOR_ID"].ToString());
            strMonitorNames += objMonitorTypeInfoVo.MONITOR_TYPE_NAME + "\n";
            GetPoint_UnderTask(objTask.CONTACT_ID, dt.Rows[i]["ID"].ToString(), ref strPointNames, ref strFREQ, ref strItemS);
        }
    }

    //点位
    private void GetPoint_UnderTask(string strContractID, string strSubTaskID, ref string strPointNames, ref string strFREQ, ref string strItemS)
    {
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);

        string strtmpPointNameS = "";
        string strtmpItems = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(dt.Rows[i]["POINT_ID"].ToString());
            if (!strtmpPointNameS.Contains(objTaskPoint.POINT_NAME))
            {
                strtmpPointNameS += (strtmpPointNameS.Length > 0 ? "、" : "") + objTaskPoint.POINT_NAME;

                GetItem_UnderSample(dt.Rows[i]["ID"].ToString(), objTaskPoint.POINT_NAME, ref strtmpItems);

                TMisContractPointVo objContractPointVo = new TMisContractPointVo();
                objContractPointVo = new TMisContractPointLogic().Details(objTaskPoint.CONTRACT_POINT_ID);
                strFREQ += (strFREQ.Length > 0 ? "\n" : "") + objTaskPoint.POINT_NAME + "：连续" + (objContractPointVo.SAMPLE_DAY == "" ? "1" : objContractPointVo.SAMPLE_DAY) + "天，每天" + (objContractPointVo.SAMPLE_FREQ == "" ? "1" : objContractPointVo.SAMPLE_FREQ) + "次";
            }

        }

        strItemS += (strItemS.Length > 0 ? "\n" : "") + strtmpItems;
        strPointNames += (strPointNames.Length > 0 ? "\r\n" : "") + strtmpPointNameS;

    }

    //项目
    private void GetItem_UnderSample(string strSampleID, string strPointName, ref string strItems)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, 0, 0);

        strItems += (strItems.Length > 0 ? "\n" : "") + strPointName + "：";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(dt.Rows[i]["ITEM_ID"].ToString());

            strItems += objItemInfo.ITEM_NAME + "、";

        }
        strItems = strItems.TrimEnd('、');
    }
}