using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Sys.General;
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
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using System.IO;
using System.Text;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using System.Collections;

/// <summary>
/// 功能描述：采样任务分配
/// 创建日期：2012-12-6
/// 创建人  ：苏成斌
/// 修改内容：增加任务单打印
/// 修改日期：2013-01-18
/// 修改人  :  潘德军
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_SamplingTaskAllocation : PageBase
{
    private static string strQC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                //预约表ID
                this.PLAN_ID.Value = Request.QueryString["planid"].ToString();
            }

            //修改项目负责人信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "ModifTaskSampleDutyUser" && !String.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                ModifTaskSampleDutyUser(Request.QueryString["planid"].Trim());
            }
            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo(Request.QueryString["strWorkTask_Id"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //采样任务分配信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["strWorkTask_Id"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测点位信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }

            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["QcType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strSampleAskDate"].ToString(), Request["strSampleFinishDate"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 任务单号设置
    /// </summary>
    /// <returns></returns>
    protected string SetTaskCode()
    {
        string strTaskCode = i3.View.PageBase.GetSerialNumber("ticket_num");

        return strTaskCode;
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo(string strTaskID)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return "";
        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskID;
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);
        if (objTask.TICKET_NUM.Length == 0)
        {
            objTask.TICKET_NUM = SetTaskCode();
            new TMisMonitorTaskLogic().Edit(objTask);
        }
        objTask.TESTED_COMPANY_ID = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID).COMPANY_NAME;
        objTask.CONTRACT_TYPE = new TSysDictLogic().GetDictNameByDictCodeAndType(objTask.CONTRACT_TYPE, "Contract_Type");

        //如果是验收监测
        if (objTask.CONTRACT_TYPE == "05")
        {
            //获取方案编制人信息
            string strProjectId = objTask.PROJECT_ID;
            //将方案编制人信息写入监测子任务表中
            TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
            TMisMonitorSubtaskVo.TASK_ID = objTask.ID;
            TMisMonitorSubtaskVo.TASK_STATUS = "01";
            DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(TMisMonitorSubtaskVo);
            foreach (DataRow row in objTable.Rows)
            {
                string strSubTaskId = row["ID"].ToString();
                TMisMonitorSubtaskVo TMisMonitorSubtaskVoTemp = new TMisMonitorSubtaskVo();
                TMisMonitorSubtaskVoTemp.ID = strSubTaskId;
                TMisMonitorSubtaskVoTemp.SAMPLING_MANAGER_ID = strProjectId;
                new TMisMonitorSubtaskLogic().Edit(TMisMonitorSubtaskVoTemp);
            }
        }
        return ToJson(objTask);
    }

    /// <summary>
    /// 采样任务分配信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strTaskID)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskID;
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;
        objSubtask.TASK_STATUS = "01";
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, intPageIndex, intPageSize);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = new TSysUserLogic().Details(dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
            else
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = "请选择";
            
            //退回意见
            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["TASK_ID"].ToString();
            objReturnInfoVo.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_002;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_001;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] = objReturnInfoVo.SUGGESTION;
        }
        dt.DefaultView.Sort = "MONITOR_ID ASC";
        DataTable dtTemp = dt.DefaultView.ToTable();
        int intTotalCount = new TMisMonitorSubtaskLogic().GetSelectResultCount(objSubtask);
        string strJson = CreateToJson(dtTemp, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测点位信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strTwoGridId;
        objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, intPageIndex, intPageSize);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            objSampleInfo.QC_TYPE = "";
            objSampleInfo.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSampleInfo.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                dtSample.ImportRow(dr);
            }
        }
        objSampleInfo.QC_TYPE = "11";
        objSampleInfo.QC_SOURCE_ID = "";
        dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
        }

        int intTotalCount = dtSample.Rows.Count;
        string strJson = CreateToJson(dtSample, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId, string strQcType)
    {
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strThreeGridId;
        dt = new TMisMonitorResultLogic().SelectByTable(objResult, intPageIndex, intPageSize);
        DataColumn dcP;
        dcP = new DataColumn("TASK_POINT_ID", Type.GetType("System.String"));
        dt.Columns.Add(dcP);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["TASK_POINT_ID"] = new TMisMonitorSampleInfoLogic().Details(strThreeGridId).POINT_ID;
        }
        intTotalCount = new TMisMonitorResultLogic().GetSelectResultCount(objResult);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strWorkTask_Id, string strPlanID, string strSampleAskDate, string strSampleFinishDate, string strTicketNum, string strQCStatus)
    {
        bool IsSuccess = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTask_Id;
        objTask.PLAN_ID = strPlanID;
        objTask = new TMisMonitorTaskLogic().Details(objTask);


        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = objTask.ID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        //判断是否经过质控，如果经过质控，则将数据发送到质控环节
        if (!String.IsNullOrEmpty(strQCStatus) && strQCStatus == "1")
        {
            TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
            objTaskEdit.ID = objTask.ID;
            objTaskEdit.QC_STATUS = "2";
            objTaskEdit.TICKET_NUM = strTicketNum;
            objTaskEdit.ASKING_DATE = strSampleFinishDate;//黄进军添加20140901
            //如果是环境质量类 将 SEND_STATUS 设置为1
            if (objTask.TASK_TYPE == "1")
            {
                objTaskEdit.SEND_STATUS = "1";
            }
            else
            {
                //分配任务报告编制人
                if (objTask.REPORT_HANDLE == "")
                    objTaskEdit.REPORT_HANDLE = getNextReportUserID("Report_UserID");
            }
            if (new TMisMonitorTaskLogic().Edit(objTaskEdit))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSubTask.ID = dt.Rows[i]["ID"].ToString();
                    //objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
                    objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
                    if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                        IsSuccess = false;
                }
            }

        }
        //如果不需要质控，则直接发送到采样环节
        else
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objSubTask.ID = dt.Rows[i]["ID"].ToString();
                objSubTask.TASK_STATUS = "02";
                objSubTask.TASK_TYPE = "发送";
                //objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
                objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
                if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                    IsSuccess = false;
            }

            if (IsSuccess)
            {
                TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
                objTaskEdit.ID = objTask.ID;
                objTaskEdit.TICKET_NUM = strTicketNum;
                objTaskEdit.ASKING_DATE = strSampleFinishDate;//黄进军添加20140901
                objTaskEdit.QC_STATUS = "8";//表示已经完成质控设置
                //如果是环境质量类 将 SEND_STATUS 设置为1
                if (objTask.TASK_TYPE == "1")
                {
                    objTaskEdit.SEND_STATUS = "1";
                }
                else {
                    //分配任务报告编制人
                    if (objTask.REPORT_HANDLE == "")
                        objTaskEdit.REPORT_HANDLE = getNextReportUserID("Report_UserID");
                }
                new TMisMonitorTaskLogic().Edit(objTaskEdit);

                TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
                objPlan.HAS_DONE = "1";
                new TMisContractPlanLogic().Edit(objPlan);
            }

        }

        TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";
        List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = dt.Rows[i]["ID"].ToString() });
            if (dtApp.Rows.Count > 0)
            {
                objSubApp.ID = dtApp.Rows[0]["ID"].ToString();
                objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();
                new TMisMonitorSubtaskAppLogic().Edit(objSubApp);
            }
            else
            {
                objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

                new TMisMonitorSubtaskAppLogic().Create(objSubApp);
            }
            //噪声除外的所有样品设置样品编号 Create By weilin 2014-03-19
            if (dt.Rows[i]["MONITOR_ID"].ToString() != "000000004" && dt.Rows[i]["MONITOR_ID"].ToString() != "FunctionNoise" && dt.Rows[i]["MONITOR_ID"].ToString() != "AreaNoise" && dt.Rows[i]["MONITOR_ID"].ToString() != "EnvRoadNoise")
            {
                objSubTask = new TMisMonitorSubtaskLogic().Details(dt.Rows[i]["ID"].ToString());
                objSample = new TMisMonitorSampleInfoVo();
                objSample.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                list = new TMisMonitorSampleInfoLogic().SelectByObject(objSample, 0, 0);
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].SAMPLE_CODE.Length == 0)
                    {
                        objSample.ID = list[j].ID;

                        if (objTask.TASK_TYPE == "1")
                            objSample.SAMPLE_CODE = list[j].SAMPLE_NAME;
                        else
                            objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubTask);

                        new TMisMonitorSampleInfoLogic().Edit(objSample);
                    }
                }
            }
        }
        SplitAcceptTask(strWorkTask_Id, objSerial);
        return IsSuccess == true ? "1" : "0";
        //bool IsSuccess = true;
        //TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        //objTask.PLAN_ID = strPlanID;
        //objTask = new TMisMonitorTaskLogic().Details(objTask);

        //TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        //objSubTask.TASK_ID = objTask.ID;

        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    objSubTask.ID = dt.Rows[i]["ID"].ToString();
        //    objSubTask.TASK_STATUS = "02";
        //    objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
        //    objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
        //    if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
        //        IsSuccess = false;

        //    //TMisMonitorSubtaskAppVo objSubtaskApp = new TMisMonitorSubtaskAppVo();

        //    //objSubtaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
        //    //objSubtaskApp.SUBTASK_ID = objSubTask.ID;
        //    //objSubtaskApp.SAMPLE_ASSIGN_ID = LogInfo.UserInfo.ID;
        //    //objSubtaskApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

        //    //new TMisMonitorSubtaskAppLogic().Create(objSubtaskApp);
        //}

        //if (IsSuccess)
        //{
        //    TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
        //    objPlan.HAS_DONE = "1";
        //    new TMisContractPlanLogic().Edit(objPlan);
        //}
        //return IsSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 发送到下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strSampleAskDate, string strSampleFinishDate)
    {
        bool IsSuccess = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = objTask.ID;

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            objSubTask.ID = dt.Rows[i]["ID"].ToString();
            objSubTask.TASK_STATUS = "02";
            objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
            objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
            if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                IsSuccess = false;

            TMisMonitorSubtaskAppVo objSubtaskApp = new TMisMonitorSubtaskAppVo();

            objSubtaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
            objSubtaskApp.SUBTASK_ID = objSubTask.ID;
            objSubtaskApp.SAMPLE_ASSIGN_ID = LogInfo.UserInfo.ID;
            objSubtaskApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

            new TMisMonitorSubtaskAppLogic().Create(objSubtaskApp);
        }

        if (IsSuccess)
        {
            TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
            objPlan.HAS_DONE = "1";
            new TMisContractPlanLogic().Edit(objPlan);
        }

        return IsSuccess == true ? "1" : "0";
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
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorPointName(string strTaskPointId)
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(strTaskPointId);

        return objTaskPoint.POINT_NAME;
    }

    /// <summary>
    /// 获取监测项目名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorItemName(string strTaskItemId)
    {
        TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(strTaskItemId);

        return objItemInfo.ITEM_NAME;
    }
    #region 打印任务单
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return ;

        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
        string strContractID = objPlan.CONTRACT_ID;
        TMisContractVo objContract = new TMisContractLogic().Details(strContractID);
        TMisContractCompanyVo objCompany = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);

        string strPointNames = "";
        string strItemS = "";
        int intPointCount = 0;
        GetInfoForPrint(ref strPointNames, ref intPointCount, ref strItemS);

        string strSAMPLE_ASK_DATE = this.strSAMPLE_ASK_DATE.Value;
        string strSAMPLE_FINISH_DATE = this.strSAMPLE_FINISH_DATE.Value;

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
        DataTable dtPoint = GetPendingPlanPointDataTable(objPlan.ID);
        if (dtMonitor.Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow drr in dtMonitor.Rows)
            {
                string strMonitorName = "", strPointName = "", strOutValuePoint = "", strOutValuePointItems = "";
                DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                if (drPoint.Length > 0)
                {

                    foreach (DataRow drrPoint in drPoint)
                    {
                        string strPointNameForItems = "", strPointItems = "";
                        strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString();
                        strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                        //获取当前点位的监测项目
                        DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                        if (dtPointItems.Rows.Count > 0)
                        {
                            foreach (DataRow drItems in dtPointItems.Rows)
                            {
                                strPointNameForItems =  drrPoint["POINT_NAME"] + ":";
                                strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                            }
                            strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                        }
                    }
                    //获取输出监测类型监测点位信息
                    strOutValuePoint +=strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                }

                sheet.GetRow(i+7).GetCell(0).SetCellValue(strMonitorName);
                sheet.GetRow(i+7).GetCell(1).SetCellValue(strOutValuePoint);
                sheet.GetRow(i+7).GetCell(2).SetCellValue(strOutValuePointItems);
      
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

    private void GetInfoForPrint(ref string strPointNames, ref int intPointCount, ref string strItemS)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return;

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GetPoint_UnderTask(dt.Rows[i]["ID"].ToString(),ref strPointNames,ref intPointCount,ref strItemS);
        }
    }

    //点位
    private void GetPoint_UnderTask(string strSubTaskID,ref string strPointNames,ref int intPointCount,ref string strItemS)
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
    private void GetItem_UnderSample(string strSampleID,ref string strItems)
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
    #endregion

    /// <summary>
    /// 修改项目复制人
    /// </summary>
    /// <param name="strPlanId"></param>
    private void ModifTaskSampleDutyUser(string strPlanId)
    {
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisMonitorTaskVo objTaskDetail = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = strPlanId });
            TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
            objSubTask.TASK_ID = objTaskDetail.ID;
            objSubTask.TASK_STATUS = "01";
            DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask, 0, 0);

            TMisContractUserdutyVo objUserDuty = new TMisContractUserdutyVo();
            objUserDuty.CONTRACT_PLAN_ID = strPlanId;
            DataTable dtDuty = new TMisContractUserdutyLogic().SelectByTable(objUserDuty, 0, 0);
            //如果没有获取到委托书的默认采样人 则取对应监测类别的 岗位职责数据
            if (dt.Rows.Count > 0 && dtDuty.Rows.Count < 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strMonitorType_ID = dt.Rows[i]["MONITOR_ID"].ToString();
                    //潘德军修改2013-7-19 环境质量的监测类别取对应的污染源类别的岗位职责
                    i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorType = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strMonitorType_ID);
                    if (objMonitorType.REMARK1.Trim().Length > 0)
                    {
                        strMonitorType_ID = objMonitorType.REMARK1.Trim();
                    }

                    TSysDutyVo objDuty = new TSysDutyVo();
                    objDuty.MONITOR_TYPE_ID = strMonitorType_ID;
                    objDuty.DICT_CODE = "duty_sampling";
                    DataTable objDutyDt = new TSysDutyLogic().GetDutyUser(objDuty);
                    DataRow drr = null;

                    if (objDutyDt.Rows.Count > 0)
                    {
                        //如果设置了默认负责人，则取默认负责人
                        DataRow[] drArr = objDutyDt.Select(" IF_DEFAULT='0'");
                        if (drArr.Length > 0)
                        {
                            drr = drArr[0];
                        }
                        else
                        {
                            //如果未设置默认负责人，则取第一行数据
                            drr = objDutyDt.Rows[0];
                        }

                        if (drr != null)
                        {
                            TMisMonitorSubtaskVo objUpSubTask = new TMisMonitorSubtaskVo();
                            objUpSubTask.ID = dt.Rows[i]["ID"].ToString();
                            objUpSubTask.SAMPLING_MANAGER_ID = drr["USERID"].ToString();
                            new TMisMonitorSubtaskLogic().Edit(objUpSubTask);
                        }
                    }
                }
            }

            if (dt.Rows.Count > 0 && dtDuty.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataRow drr in dtDuty.Rows)
                    {
                        if (dr["MONITOR_ID"].ToString() == drr["MONITOR_TYPE_ID"].ToString())
                        {
                            TMisMonitorSubtaskVo objUpSubTask = new TMisMonitorSubtaskVo();
                            objUpSubTask.ID = dr["ID"].ToString();
                            objUpSubTask.SAMPLING_MANAGER_ID = drr["SAMPLING_MANAGER_ID"].ToString();
                            new TMisMonitorSubtaskLogic().Edit(objUpSubTask);
                        }
                    }
                }
            }
        }
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

    /// <summary>
    /// 根据任务ID获取委托类别
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strTaskId)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskId;
        objTask = new TMisMonitorTaskLogic().Details(objTask);
        return objTask.CONTRACT_TYPE;
    }
    /// <summary>
    /// 根据监测天数拆分任务（验收监测） Create By:weilin 2014-12-04
    /// </summary>
    /// <param name="strTaskID"></param>
    /// <returns></returns>
    private static bool SplitAcceptTask(string strTaskID, TBaseSerialruleVo objSerial)
    {
        bool bResult = true;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(strTaskID);
        TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
        if (objTaskVo.CONTRACT_TYPE == "05")  //验收监测
        {
            TMisContractVo objContractVo = new TMisContractLogic().Details(objTaskVo.CONTRACT_ID);
            TMisContractPointVo objContractPointVo = new TMisContractPointVo();
            objContractPointVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
            objContractPointVo.IS_DEL = "0";
            objContractPointVo = new TMisContractPointLogic().Details(objContractPointVo);
            if (objContractPointVo.SAMPLE_DAY != "" && IsNumeric(objContractPointVo.SAMPLE_DAY) && objContractVo.REMARK5 == "")
            {
                int iSampleDay = int.Parse(objContractPointVo.SAMPLE_DAY);
                TMisMonitorTaskCompanyVo objClient = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.CLIENT_COMPANY_ID);
                TMisMonitorTaskCompanyVo objTested = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.TESTED_COMPANY_ID);
                for (int i = 1; i < iSampleDay; i++)
                {
                    TMisMonitorTaskVo objTaskAddVo = new TMisMonitorTaskVo();
                    objTaskAddVo.ID = GetSerialNumber("t_mis_monitor_taskId");
                    objTaskAddVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
                    objTaskAddVo.PLAN_ID = objTaskVo.PLAN_ID;
                    objTaskAddVo.CONTRACT_CODE = objTaskVo.CONTRACT_CODE;
                    objTaskAddVo.TICKET_NUM = objTaskVo.TICKET_NUM + "-" + (i + 1).ToString();
                    objTaskAddVo.PROJECT_NAME = objTaskVo.PROJECT_NAME;
                    objTaskAddVo.CONTRACT_YEAR = objTaskVo.CONTRACT_YEAR;
                    objTaskAddVo.CONTRACT_TYPE = objTaskVo.CONTRACT_TYPE;
                    objTaskAddVo.TEST_TYPE = objTaskVo.TEST_TYPE;
                    objTaskAddVo.TEST_PURPOSE = objTaskVo.TEST_PURPOSE;
                    objTaskAddVo.ASKING_DATE = objTaskVo.ASKING_DATE;
                    objTaskAddVo.SAMPLE_SOURCE = objTaskVo.SAMPLE_SOURCE;
                    objTaskAddVo.CREATOR_ID = objTaskVo.CREATOR_ID;
                    objTaskAddVo.CREATE_DATE = objTaskVo.CREATE_DATE;
                    objTaskAddVo.TASK_STATUS = objTaskVo.TASK_STATUS;
                    objTaskAddVo.REMARK1 = objTaskVo.REMARK1;
                    objTaskAddVo.REMARK2 = objTaskVo.REMARK2;
                    objTaskAddVo.REMARK3 = objTaskVo.REMARK3;
                    objTaskAddVo.QC_STATUS = objTaskVo.QC_STATUS;
                    objTaskAddVo.COMFIRM_STATUS = objTaskVo.COMFIRM_STATUS;
                    objTaskAddVo.ALLQC_STATUS = objTaskVo.ALLQC_STATUS;
                    objTaskAddVo.REPORT_HANDLE = objTaskVo.REPORT_HANDLE;

                    //构造监测任务委托企业信息
                    TMisMonitorTaskCompanyVo objClientAdd = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objClient, objClientAdd);//复制对象
                    objClientAdd.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objClientAdd.TASK_ID = objTaskAddVo.ID;
                    //构造监测任务受检企业信息
                    TMisMonitorTaskCompanyVo objTestedAdd = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objTested, objTestedAdd);//复制对象
                    objTestedAdd.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objTestedAdd.TASK_ID = objTaskAddVo.ID;

                    objTaskAddVo.CLIENT_COMPANY_ID = objClientAdd.ID;
                    objTaskAddVo.TESTED_COMPANY_ID = objTestedAdd.ID;

                    TMisMonitorReportVo objReportAdd = new TMisMonitorReportVo();
                    objReportVo.ID = GetSerialNumber("t_mis_monitor_report_id");
                    objReportVo.REPORT_CODE = objReportVo.REPORT_CODE + "-" + (i + 1).ToString();
                    objReportVo.TASK_ID = objTaskAddVo.ID;
                    objReportVo.IF_GET = "0";

                    //监测子任务信息 根据委托书监测类别进行构造
                    ArrayList arrSubTask = new ArrayList();//监测子任务集合
                    ArrayList arrSubTaskApp = new ArrayList();
                    ArrayList arrTaskPoint = new ArrayList();//监测点位集合
                    ArrayList arrPointItem = new ArrayList();//监测点位明细集合
                    ArrayList arrSample = new ArrayList();//样品集合
                    ArrayList arrSampleResult = new ArrayList();//样品结果集合 
                    ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表

                    List<TMisMonitorSubtaskVo> listSubTask = new TMisMonitorSubtaskLogic().SelectByObject(new TMisMonitorSubtaskVo() { TASK_ID = objTaskVo.ID }, 0, 0);
                    for (int j = 0; j < listSubTask.Count; j++)
                    {
                        TMisMonitorSubtaskVo objSubTaskAdd = new TMisMonitorSubtaskVo();
                        CopyObject(listSubTask[j], objSubTaskAdd);
                        objSubTaskAdd.ID = GetSerialNumber("t_mis_monitor_subtaskId");
                        objSubTaskAdd.TASK_ID = objTaskAddVo.ID;
                        arrSubTask.Add(objSubTaskAdd);

                        TMisMonitorSubtaskAppVo objSubTaskApp = new TMisMonitorSubtaskAppVo();
                        objSubTaskApp.SUBTASK_ID = listSubTask[j].ID;
                        objSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(objSubTaskApp);
                        TMisMonitorSubtaskAppVo objSubTaskAppAdd = new TMisMonitorSubtaskAppVo();
                        CopyObject(objSubTaskApp, objSubTaskAppAdd);
                        objSubTaskAppAdd.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                        objSubTaskAppAdd.SUBTASK_ID = objSubTaskAdd.ID;
                        arrSubTaskApp.Add(objSubTaskAppAdd);

                        List<TMisMonitorTaskPointVo> listTaskPoint = new TMisMonitorTaskPointLogic().SelectByObject(new TMisMonitorTaskPointVo() { TASK_ID = objTaskVo.ID, SUBTASK_ID = listSubTask[j].ID, IS_DEL = "0" }, 0, 0);
                        for (int k = 0; k < listTaskPoint.Count; k++)
                        {
                            TMisMonitorTaskPointVo objTaskPointAdd = new TMisMonitorTaskPointVo();
                            CopyObject(listTaskPoint[k], objTaskPointAdd);
                            objTaskPointAdd.ID = GetSerialNumber("t_mis_monitor_taskpointId");
                            objTaskPointAdd.TASK_ID = objTaskAddVo.ID;
                            arrTaskPoint.Add(objTaskPointAdd);

                            TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoVo();
                            objSampleInfoVo.SUBTASK_ID = listSubTask[j].ID;
                            objSampleInfoVo.POINT_ID = listTaskPoint[k].ID;
                            objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(objSampleInfoVo);
                            TMisMonitorSampleInfoVo objSampleInfoAdd = new TMisMonitorSampleInfoVo();
                            CopyObject(objSampleInfoVo, objSampleInfoAdd);
                            objSampleInfoAdd.ID = GetSerialNumber("MonitorSampleId");
                            objSampleInfoAdd.SUBTASK_ID = objSubTaskAdd.ID;
                            objSampleInfoAdd.POINT_ID = objTaskPointAdd.ID;
                            objSampleInfoAdd.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTaskAddVo, objSubTaskAdd);
                            arrSample.Add(objSampleInfoAdd);

                            List<TMisMonitorTaskItemVo> listTaskItem = new TMisMonitorTaskItemLogic().SelectByObject(new TMisMonitorTaskItemVo() { TASK_POINT_ID = listTaskPoint[k].ID, IS_DEL = "0" }, 0, 0);
                            for (int l = 0; l < listTaskItem.Count; l++)
                            {
                                TMisMonitorTaskItemVo objTaskItemAdd = new TMisMonitorTaskItemVo();
                                CopyObject(listTaskItem[l], objTaskItemAdd);
                                objTaskItemAdd.ID = GetSerialNumber("t_mis_monitor_task_item_id");
                                objTaskItemAdd.TASK_POINT_ID = objTaskPointAdd.ID;
                                arrPointItem.Add(objTaskItemAdd);
                            }

                            List<TMisMonitorResultVo> listResult = new TMisMonitorResultLogic().SelectByObject(new TMisMonitorResultVo() { SAMPLE_ID = objSampleInfoVo.ID }, 0, 0);
                            for (int l = 0; l < listResult.Count; l++)
                            {
                                TMisMonitorResultVo objResultAdd = new TMisMonitorResultVo();
                                CopyObject(listResult[l], objResultAdd);
                                objResultAdd.ID = GetSerialNumber("MonitorResultId");
                                objResultAdd.SAMPLE_ID = objSampleInfoAdd.ID;
                                arrSampleResult.Add(objResultAdd);

                                TMisMonitorResultAppVo objResultAppVo = new TMisMonitorResultAppVo();
                                objResultAppVo.RESULT_ID = listResult[l].ID;
                                objResultAppVo = new TMisMonitorResultAppLogic().Details(objResultAppVo);
                                TMisMonitorResultAppVo objResultAppAdd = new TMisMonitorResultAppVo();
                                CopyObject(objResultAppVo, objResultAppAdd);
                                objResultAppAdd.ID = GetSerialNumber("MonitorResultAppId");
                                objResultAppAdd.RESULT_ID = objResultAdd.ID;
                                arrSampleResultApp.Add(objResultAppAdd);
                            }
                        }
                        
                    }
                    if (new TMisMonitorTaskLogic().SaveTrans(objTaskAddVo, objClientAdd, objTestedAdd, objReportVo, arrTaskPoint, arrSubTask, arrSubTaskApp, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
                    {
                        bResult = true;
                    }
                }
                //更新任务单号
                objTaskVo.TICKET_NUM = objTaskVo.TICKET_NUM + "-1";
                new TMisMonitorTaskLogic().Edit(objTaskVo);
                //更新报告单号
                objReportVo.REPORT_CODE = objReportVo.REPORT_CODE + "-1";
                new TMisMonitorReportLogic().Edit(objReportVo);
                //更新验收委托书任务已经拆分任务的标志
                objContractVo.REMARK5 = "1";
                new TMisContractLogic().Edit(objContractVo);
            }
        }

        return bResult;
    }
}