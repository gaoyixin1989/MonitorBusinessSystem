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
using i3.ValueObject.Sys.General;


/// <summary>
/// 功能描述：采样任务列表
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplingList : PageBase
{
    private string strTestedCompanyID = "";
    private string strContractCode = "";
    private string strMonitorID = "";
    public string task_id = "", strPlanId = "";
    private void GetHidParam()
    {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
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

    //点位
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

    //项目
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
    #endregion
    protected void btnExport_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/WorkTaskSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "", strMonitorList = "", strWorkContent = "";
        DataTable dt = GetPendingPlanDataTable();
        //插入委托书单号
        if (dt.Rows.Count > 0)
        {
            sheet.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["TICKET_NUM"].ToString());
            sheet.GetRow(4).GetCell(6).SetCellValue(dt.Rows[0]["REPORT_CODE"].ToString());
            strTaskType = getDictName(dt.Rows[0]["CONTRACT_TYPE"].ToString(), "Contract_Type");
            sheet.GetRow(5).GetCell(1).SetCellValue(strTaskType + "任务");
            sheet.GetRow(5).GetCell(7).SetCellValue(dt.Rows[0]["CONTRACT_CODE"].ToString());

            #region 获取计划的监测类型 Add By:weilin
            TMisMonitorTaskVo MonitorTaskVo=new TMisMonitorTaskVo();
            MonitorTaskVo.PLAN_ID=strPlanId;
            //TMisMonitorSubtaskVo MonitorSubtaskVo = new TMisMonitorSubtaskVo();
            //MonitorSubtaskVo.TASK_ID = (new TMisMonitorTaskLogic().SelectByObject(MonitorTaskVo)).ID;
            DataTable dtMonitorType = new TMisMonitorSubtaskLogic().getMonitorByTask((new TMisMonitorTaskLogic().SelectByObject(MonitorTaskVo)).ID);
            string strMonitorTypes = string.Empty;
            for (int i = 0; i < dtMonitorType.Rows.Count; i++)
            {
                strMonitorTypes += dtMonitorType.Rows[i][0].ToString() + ";";
            }
            #endregion
            //string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPES"].ToString().Split(';');
            //string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPE"].ToString().Split(';');
            string[] strMonitroTypeArr = strMonitorTypes.TrimEnd(';').Split(';');
            string strExportMonitorId = this.hidMonitorId.Value.ToString();
            foreach (string str in strMonitroTypeArr)
            {
                if (!String.IsNullOrEmpty(strExportMonitorId))
                {
                    //如果监测子任务的监测类别不符合当前循环的监测类别，则跳出当前循环，继续下一循环，即：导出具体的监测子任务
                    if (strExportMonitorId != str)
                    {
                        //跳出当前循环，继续下一循环
                        continue;
                    }
                }
                strMonitorList += GetMonitorName(str) + "/";
            }
            strMonitorList = strMonitorList.Substring(0, strMonitorList.Length - 1);
            sheet.GetRow(7).GetCell(1).SetCellValue(strMonitorList);
            if (dt.Rows[0]["SAMPLE_SOURCE"].ToString() == "送样")
            {
                //strWorkContent += "地表水、地下水(送样)\n";
                int intLen = strMonitroTypeArr.Length;
                int INTSHOWLEN = 0;
                foreach (string strMonitor in strMonitroTypeArr)
                {
                    INTSHOWLEN++;
                    strWorkContent += GetMonitorName(strMonitor) + "、";
                    if (INTSHOWLEN == intLen - 1)
                    {
                        strWorkContent += "(送样)\n";
                    }
                }
            }
            //strWorkContent += "环境空气：\n";
            //获取当前监测类别信息
            DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable();
            DataTable dtPoint = GetPendingPlanPointDataTable();
            string strOutValuePoint = "", strOutValuePointItems = "";
            if (dtMonitor.Rows.Count > 0)
            {
                foreach (DataRow drr in dtMonitor.Rows)
                {
                    if (!String.IsNullOrEmpty(strExportMonitorId))
                    {
                        //如果监测子任务的监测类别不符合当前循环的监测类别，则跳出当前循环，继续下一循环，即：导出具体的监测子任务
                        if (strExportMonitorId != drr["MONITOR_ID"].ToString())
                        {
                            //跳出当前循环，继续下一循环
                            continue;
                        }
                    }
                    string strMonitorName = "", strPointName = "";
                    DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                    if (drPoint.Length > 0)
                    {

                        foreach (DataRow drrPoint in drPoint)
                        {
                            string strPointNameForItems = "", strPointItems = "";
                            strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString() + "：";
                            strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                            //获取当前点位的监测项目
                            DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                            if (dtPointItems.Rows.Count > 0)
                            {
                                foreach (DataRow drItems in dtPointItems.Rows)
                                {
                                    strPointNameForItems = strMonitorName.Substring(0, strMonitorName.Length - 1) + drrPoint["POINT_NAME"] + "(" + (drrPoint["SAMPLE_DAY"].ToString() == "" ? "1" : drrPoint["SAMPLE_DAY"].ToString()) + "天" + (drrPoint["SAMPLE_FREQ"].ToString() == "" ? "1" : drrPoint["SAMPLE_FREQ"].ToString()) + "次):";
                                    strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                                }
                                strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                            }
                        }
                        //获取输出监测类型监测点位信息
                        strOutValuePoint += strMonitorName + strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                    }
                }
            }
            strWorkContent += "监测点位：\n" + strOutValuePoint;
            strWorkContent += "监测因子与频次：\n" + strOutValuePointItems;
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);

            string strLinkPerple = "";
            strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + "\n";
            strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
            if (!String.IsNullOrEmpty(dt.Rows[0]["PROJECT_ID"].ToString()))
            {
                strLinkPerple += "项目负责人:" + GetUserName(dt.Rows[0]["PROJECT_ID"].ToString());
            }
            else
            {
                strLinkPerple += "报告编写：";
            }
            sheet.GetRow(10).GetCell(1).SetCellValue(strLinkPerple);

            sheet.GetRow(6).GetCell(1).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());

            if (!String.IsNullOrEmpty(dt.Rows[0]["ASKING_DATE"].ToString()))
            {
                sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            }
            //sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            sheet.GetRow(11).GetCell(3).SetCellValue(LogInfo.UserInfo.REAL_NAME);
            sheet.GetRow(11).GetCell(6).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
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
                "attachment;filename=" + HttpUtility.UrlEncode("工作任务通知单-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    /// <summary>
    /// 获取指定监测计划的监测类别信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDistinctMonitorDataTable()
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
    /// <summary>
    /// 获取指定委托书和监测计划的任务信息  胡方扬 2013-04-24
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDataTable()
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems);
        }
        //if ( !String.IsNullOrEmpty(strPlanId))
        //{
        //    TMisContractPlanVo objItems = new TMisContractPlanVo();
        //    objItems.ID = strPlanId;
        //    TMisContractVo objItemContract = new TMisContractVo();
        //    objItemContract.ID = task_id;
        //    dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems, objItemContract);
        //}
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanPointDataTable()
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
    /// 获取指定监测类别的类别名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetMonitorName(string strId)
    {
        TBaseMonitorTypeInfoVo objItems = new TBaseMonitorTypeInfoLogic().Details(new TBaseMonitorTypeInfoVo { ID = strId, IS_DEL = "0" });
        return objItems.MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetUserName(string strId)
    {
        TSysUserVo objItems = new TSysUserLogic().Details(new TSysUserVo { ID = strId, IS_DEL = "0" });
        return objItems.REAL_NAME;
    }
}