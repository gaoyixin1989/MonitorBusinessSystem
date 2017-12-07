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


/// <summary>
/// 功能描述：采样任务列表
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_SamplingList : PageBase
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
        objSubtask.SORT_FIELD = "CREATE_DATE";
        objSubtask.SORT_TYPE = "desc";
        DataTable dtTreeRoot = new TMisMonitorSubtaskLogic().SelectByTableWithAllTaskForFatherTree(objSubtask, strMonitorID, "02", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        objSubtask.SORT_FIELD = "TASK_ID";
        objSubtask.SORT_TYPE = "asc";
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

        objSubtask.SORT_FIELD = "CREATE_DATE";
        objSubtask.SORT_TYPE = "desc";
        DataTable dtTreeRoot = new TMisMonitorSubtaskLogic().SelectByTableWithAllTaskForFatherTree(objSubtask, strMonitorID, "122", strTestedCompanyID, strContractCode, LogInfo.UserInfo.ID, 0, 0);
        objSubtask.SORT_FIELD = "TASK_ID";
        objSubtask.SORT_TYPE = "asc";
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
    private void GetInfoForPrint(ref string strPointNames, ref int intPointCount, ref string strItemS)
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
            GetPoint_UnderTask(dt.Rows[i]["ID"].ToString(), ref strPointNames, ref intPointCount, ref strItemS);
        }
    }
    
    /// <summary>
    /// 获取点位
    /// </summary>
    /// <param name="strSubTaskID"></param>
    /// <param name="strPointNames"></param>
    /// <param name="intPointCount"></param>
    /// <param name="strItemS"></param>
    private void GetPoint_UnderTask(string strSubTaskID, ref string strPointNames, ref int intPointCount, ref string strItemS)
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
                strtmpPointNameS += (strtmpPointNameS.Length > 0 ? "\r\n" : "") + objTaskPoint.POINT_NAME;
            }
            GetItem_UnderSample(dt.Rows[i]["ID"].ToString(), ref strtmpItems);
        }

        strItemS += (strItemS.Length > 0 ? "、" : "") + strtmpItems;
        strPointNames += (strPointNames.Length > 0 ? "\r\n" : "") + strtmpPointNameS;
        intPointCount += dt.Rows.Count;
    }

    /// <summary>
    /// 获取项目
    /// </summary>
    /// <param name="strSampleID"></param>
    /// <param name="strItems"></param>
    private void GetItem_UnderSample(string strSampleID, ref string strItems)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(dt.Rows[i]["ITEM_ID"].ToString());
            if (!strItems.Contains(objItemInfo.ITEM_NAME))
            {
                strItems += (strItems.Length > 0 ? "、" : "") + objItemInfo.ITEM_NAME;
            }
        }
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (this.hidPlanId.Value.Length == 0)
            return;

        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.hidPlanId.Value);
        string strContractID = objPlan.CONTRACT_ID;
        TMisContractVo objContract = new TMisContractLogic().Details(strContractID);
        TMisContractCompanyVo objCompany = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);

        string strPointNames = "";
        string strItemS = "";
        int intPointCount = 0;
        GetInfoForPrint(ref strPointNames, ref intPointCount, ref strItemS);

        string strSAMPLE_ASK_DATE = this.hidASK_DATE.Value;
        string strSAMPLE_FINISH_DATE = this.hidFINISH_DATE.Value;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/MoniterTaskNotify.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        sheet.GetRow(3).GetCell(1).SetCellValue(objContract.PROJECT_NAME);
        sheet.GetRow(3).GetCell(3).SetCellValue(objCompany.COMPANY_NAME);
        sheet.GetRow(4).GetCell(1).SetCellValue(objCompany.MONITOR_ADDRESS);
        sheet.GetRow(4).GetCell(3).SetCellValue(objCompany.CONTACT_NAME + "、" + objCompany.PHONE);
        sheet.GetRow(5).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(5).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);

        //获取当前监测类别信息
        DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable(objPlan.ID);
        //DataTable dtPoint = GetPendingPlanPointDataTable(objPlan.ID);
        DataTable dtPoint = new TMisMonitorSampleInfoLogic().GetSampleInfoByPlanID(objPlan.ID);
        if (dtMonitor.Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow drr in dtMonitor.Rows)
            {
                string strExportMonitorId = this.hidMonitorId.Value.ToString();
                //如果hidMonitorId值为空 表示选择的父节点，要导出当前任务所有的监测子任务，如果不为空，则表示选择了某条监测子任务
                if (!String.IsNullOrEmpty(strExportMonitorId))
                {
                    //如果监测子任务的监测类别不符合当前循环的监测类别，则跳出当前循环，继续下一循环，即：导出具体的监测子任务
                    if(strExportMonitorId!=drr["MONITOR_ID"].ToString()){
                        //跳出当前循环，继续下一循环
                        continue;
                    }
                }
                string strMonitorName = "", strPointName = "", strOutValuePoint = "", strOutValuePointItems = "";
                DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                if (drPoint.Length > 0)
                {

                    foreach (DataRow drrPoint in drPoint)
                    {
                        string strPointNameForItems = "", strPointItems = "";
                        strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString();
                        strPointName += drrPoint["SAMPLE_CODE"].ToString() + ":" + drrPoint["SAMPLE_NAME"].ToString() + "；\n";

                        //获取当前点位的监测项目
                        //DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                        DataTable dtPointItems = new TMisMonitorSampleInfoLogic().GetItemInfoBySampleID(drrPoint["ID"].ToString());
                        if (dtPointItems.Rows.Count > 0)
                        {
                            foreach (DataRow drItems in dtPointItems.Rows)
                            {
                                strPointNameForItems = drrPoint["SAMPLE_NAME"] + ":";
                                strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                            }
                            strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                        }
                    }
                    //获取输出监测类型监测点位信息
                    strOutValuePoint += strPointName.Substring(0, strPointName.Length - 2) + "；\n";
                }

                sheet.GetRow(i + 7).GetCell(0).SetCellValue(strMonitorName);
                sheet.GetRow(i + 7).GetCell(1).SetCellValue(strOutValuePoint);
                sheet.GetRow(i + 7).GetCell(2).SetCellValue(strOutValuePointItems);

                i++;
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
                "attachment;filename=" + HttpUtility.UrlEncode("采样任务分配表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion
}