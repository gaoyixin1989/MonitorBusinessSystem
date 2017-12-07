using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using System.Text;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

using i3.View;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Sys.General;

using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;

using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Company;
using i3.ValueObject.Channels.Base.MonitorType;


/// <summary>
/// 功能描述：采样任务列表
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplingList : PageBase
{
    private string strTestedCompanyID = "";
    private string strContractCode = "";
    private string strMonitorID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (this.Request["TestedCompanyID"] != null)
        {
            strTestedCompanyID = this.Request["TestedCompanyID"].ToString();
        }
        if (this.Request["ContractCode"] != null)
        {
            strContractCode = this.Request["ContractCode"].ToString();
        }
        if (this.Request["MonitorID"] != null)
        {
            strMonitorID = this.Request["MonitorID"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getSampleTask")
        {
            strResult = getSampleTask();
            Response.Write(strResult);
            Response.End();
        }
        //回退任务
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getBackSampleTask")
        {
            strResult = getBackSampleTask();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 采样任务列表信息
    /// </summary>
    /// <returns>Json</returns>
    protected string getSampleTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        //objSubtask.SORT_FIELD = "SAMPLE_ASK_DATE";
        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithTask(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithAllTask(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        //int intTotalCount = dt.Rows.Count;

        //string strJson = CreateToJson(dt, intTotalCount);
        objSubtask.SORT_FIELD = "TASK_ID";
        DataTable dtTreeRoot = new TMisMonitorSubtaskLogic().SelectByTableWithAllTaskForFatherTree(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithAllTask(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        int RecordCount=dtTreeRoot.Rows.Count+dt.Rows.Count;
        string strJson = LigerGridTreeDataToJson(dtTreeRoot, dt, "TASK_ID", RecordCount);
        return strJson;
    }

    /// <summary>
    /// 采样任务列表信息
    /// </summary>
    /// <returns>Json</returns>
    protected string getBackSampleTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        //objSubtask.SORT_FIELD = "SAMPLE_ASK_DATE";
        ////DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithTask(objSubtask, strMonitorID, "122", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithAllTask(objSubtask, strMonitorID, "122", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        //int intTotalCount = dt.Rows.Count;

        //string strJson = CreateToJson(dt, intTotalCount);
        //return strJson;

        objSubtask.SORT_FIELD = "TASK_ID";
        DataTable dtTreeRoot = new TMisMonitorSubtaskLogic().SelectByTableWithAllTaskForFatherTree(objSubtask, strMonitorID, "122", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTableWithAllTask(objSubtask, strMonitorID, "122", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        int RecordCount = dtTreeRoot.Rows.Count + dt.Rows.Count;
        string strJson = LigerGridTreeDataToJson(dtTreeRoot, dt, "TASK_ID", RecordCount);
        return strJson;
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
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strCompanyId)
    {
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }

    /// <summary>
    /// 获取字典项信息
    /// </summary>
    /// <param name="strDictCode"></param>
    /// <param name="strDictType"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strUserID)
    {
        return new TSysUserLogic().Details(strUserID).REAL_NAME;
    }

    #region 打印任务单 Create By 胡方扬 2013-06-03
    private void GetInfoForPrint(ref string strMonitorNames, ref string strPointNames, ref string strFREQ, ref string strItemS)
    {
        if (this.hidPlanId.Value.Length == 0)
            return;

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.hidPlanId.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;
        string strMonitorId=this.hidMonitorId.Value.ToString();

        //实现按具体监测子任务进行导出  Create By 胡方扬 2013-06-04
        if(!String.IsNullOrEmpty(strMonitorId)){
            objSubtask.MONITOR_ID = strMonitorId;
        }
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
                //objContractPointVo.CONTRACT_ID = strContractID;
                //objContractPointVo.POINT_ID = objTaskPoint.POINT_ID;
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

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanPointDataTable(string strPlanId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanPointDataTable(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <param name="strPointId"></param>
    /// <returns></returns>
    private DataTable GetPendingPlanPointItemsDataTable(string strPointId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPointId))
        {
            TMisContractPointitemVo objItems = new TMisContractPointitemVo();
            objItems.CONTRACT_POINT_ID = strPointId;
            dt = new TMisContractPointitemLogic().GetItemsForPoint(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测类别信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDistinctMonitorDataTable(string strPlanId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanDistinctMonitorDataTable(objItems);
        }
        return dt;
    }
    #endregion
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
}