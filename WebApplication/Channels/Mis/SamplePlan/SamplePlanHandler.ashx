<%@ WebHandler Language="C#" Class="SamplePlanHandler" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Web.SessionState;
using System.Reflection;
using System.Collections;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;

public class SamplePlanHandler : PageBase, IHttpHandler, IRequiresSessionState
{
    public string strMessage = "",strAction = "", strType = "";//执行方法，字典类别
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    public string strContratId = "", strWorkTask_Id = "", strSubTask_Id = "", strSamplePlanId = "", strContractTypeId = "", strCompanyId = "",
        strCompanyIdFrim = "", strProjectName = "", strYear = "", strDate = "", strMonitorId = "", strRequest = "",strTaskNum="";
    public string strQcStatus = "", strAllQcStatus = "", strPlanDate = "", strNew_id = "", strCompany_Name = "";
    public string strContactName = "", strContactPhone = "";
    public string strMan = "";
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID))
        {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
          //获取参数值
        GetRequestParme(context);

        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                    //获取自送样预约计划（树形表结构TreeGrid）
                case "GetContractInforUnionSamplePlan":
                    context.Response.Write(GetContractInforUnionSamplePlan());
                    context.Response.End();
                    break;
                case "DelSamplePlan":
                    context.Response.Write(DelSamplePlan());
                    context.Response.End();
                    break;
                case "doSubTask":
                    context.Response.Write(doSubTask());
                    context.Response.End();
                    break;
                case "doSubTask_QHD":
                    context.Response.Write(doSubTask_QHD());
                    context.Response.End();
                    break;
                case "CreateSamplePlan":
                    context.Response.Write(CreateSamplePlan());
                    context.Response.End();
                    break;
                case "doPlanTaskForSample":
                    context.Response.Write(doPlanTaskForSample());
                    context.Response.End();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 创建原因：插入自送样监测计划
    /// 创建人：胡方扬
    /// 创建日期：2013-07-01
    /// </summary>
    /// <returns></returns>
    public string CreateSamplePlan() {
        string result = "";
        TMisContractSamplePlanVo objItems = new TMisContractSamplePlanVo();
        objItems.ID = GetSerialNumber("t_mis_contract_SamplePlanId");
        objItems.FREQ = "1";
        objItems.IF_PLAN = "0";
        objItems.CONTRACT_ID = strContratId;
        if (new TMisContractSamplePlanLogic().CreateSamplePlan(objItems, strCompany_Name)) 
        {
            result = objItems.ID;
        }
        return result;
    }
    //获取自送样预约计划（树形表结构TreeGrid）
    public string GetContractInforUnionSamplePlan() {
        string result = "";
        DataTable dt = new DataTable();
        DataTable dtSample = new DataTable();
        DataTable dtTemple = new DataTable();
        TMisMonitorTaskVo objItems = new TMisMonitorTaskVo();
        objItems.SAMPLE_SOURCE = "送样";
        objItems.CONTRACT_TYPE = "04";
        dt = new TMisMonitorTaskLogic().SelectByUnionTaskTable(objItems, intPageIndex, intPageSize);
        int intCountNum = new TMisMonitorTaskLogic().SelectByUnionTaskTableResult(objItems);
        //获取一个空集合 复制表结构
        TMisContractSamplePlanVo objItemTem = new TMisContractSamplePlanVo();
        objItemTem.IF_PLAN="3";
        dtTemple = new TMisMonitorTaskLogic().GetContractInforUnionSamplePlan(objItems, objItemTem);
        if (dtTemple.Rows.Count == 0) {
            dtSample = dtTemple.Copy();
            dtSample.Clear();
        }
        if (dt.Rows.Count > 0) {
            foreach (DataRow dr in dt.Rows) {
                objItems.ID = dr["ID"].ToString();
                TMisContractSamplePlanVo objItemSample = new TMisContractSamplePlanVo();
                objItemSample.IF_PLAN = "1";
                dtTemple = new DataTable();
                dtTemple = new TMisMonitorTaskLogic().GetContractInforUnionSamplePlan(objItems, objItemSample);
                if (dtTemple.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtTemple.Rows)
                    {
                        dtSample.ImportRow(drr);
                        dtSample.AcceptChanges();
                    }
                }
                else {
                    dr.Delete();
                }
            }
            dt.AcceptChanges();
            //intCountNum = dt.Rows.Count + dtSample.Rows.Count;
            intCountNum = dt.Rows.Count;
            result = LigerGridTreeDataToJson(dt, dtSample, "ID", intCountNum);
        }
        return result;
    }
    /// <summary>
    /// 删除自送样预约计划
    /// </summary>
    /// <returns></returns>
    public string DelSamplePlan() {
        string result = "";
        TMisContractSamplePlanVo objItems = new TMisContractSamplePlanVo();
        objItems.ID = strSamplePlanId;
        //if (SamplePlan() > 1)
        //{
            if (new TMisContractSamplePlanLogic().Delete(objItems))
            {
                result = "true";
            }
        //}
        return result;
    }

    /// <summary>
    /// 获取该委托书是否还有监测计划
    /// </summary>
    /// <returns></returns>
    public int SamplePlan() {
        int Count = 0;
        DataTable dt = new DataTable();
        TMisContractSamplePlanVo objItems = new TMisContractSamplePlanVo();
        objItems.CONTRACT_ID = strContratId;
        dt = new TMisContractSamplePlanLogic().SelectByTable(objItems);

        Count = dt.Rows.Count;
        return Count;
    }

    /// <summary>
    /// 预约办理
    /// </summary>
    /// <param name="strPlanID">预约表ID</param>
    protected string doSubTask()
    {
        string strReturn = "";
        string strTaskStatus = "021";
        if (!String.IsNullOrEmpty(strRequest) && strRequest == "true") {
            strTaskStatus = "024";
        }
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTask_Id;
        objTask.ASKING_DATE =strDate;
        objTask.TICKET_NUM = strTaskNum;
        //objTask.QC_STATUS = strQcStatus;
        //objTask.ALLQC_STATUS = strAllQcStatus;
        new TMisMonitorTaskLogic().Edit(objTask);

        TMisMonitorReportVo objReportVoWhere = new TMisMonitorReportVo();
        objReportVoWhere.TASK_ID = strWorkTask_Id;
        TMisMonitorReportVo objReportVoEdit = new TMisMonitorReportVo();
        objReportVoEdit.REPORT_CODE = objTask.TICKET_NUM;
        new TMisMonitorReportLogic().Edit(objReportVoEdit, objReportVoWhere);

        TMisMonitorTaskVo objTaskTmp = new TMisMonitorTaskLogic().Details(strWorkTask_Id);
        TMisMonitorTaskCompanyVo tTaskCompany = new TMisMonitorTaskCompanyVo();
        tTaskCompany.ID = objTaskTmp.TESTED_COMPANY_ID;
        tTaskCompany.CONTACT_NAME = strContactName;
        tTaskCompany.PHONE = strContactPhone;
        new TMisMonitorTaskCompanyLogic().Edit(tTaskCompany);
        
        TMisContractPlanVo objPlan = new TMisContractPlanVo();
        objPlan.ID = objTaskTmp.PLAN_ID;

        if (!String.IsNullOrEmpty(objPlan.ID))
        {
            if (!String.IsNullOrEmpty(strPlanDate))
            {
                string[] strPlanDateArr = null;
                strPlanDateArr = strPlanDate.Split('-');
                if (strPlanDateArr != null)
                {
                    objPlan.PLAN_YEAR = strPlanDateArr[0].ToString();
                    objPlan.PLAN_MONTH = strPlanDateArr[1].ToString();
                    objPlan.PLAN_DAY = strPlanDateArr[2].ToString();
                    new TMisContractPlanLogic().Create(objPlan);
                }
            }
        }
        DataTable objDt = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo { TASK_ID=strWorkTask_Id});
        if (objDt.Rows.Count > 0) {
            for (int i = 0; i < objDt.Rows.Count; i++) {
                TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();


                DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = objDt.Rows[i]["ID"].ToString() });
                if (dtApp.Rows.Count > 0)
                {
                    objSubApp.ID = dtApp.Rows[0]["ID"].ToString();
                    objSubApp.SUBTASK_ID = objDt.Rows[i]["ID"].ToString();
                    objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                    objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();
                    new TMisMonitorSubtaskAppLogic().Edit(objSubApp);
                }
                else
                {
                    objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                    objSubApp.SUBTASK_ID = objDt.Rows[i]["ID"].ToString(); 
                    objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                    objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

                    new TMisMonitorSubtaskAppLogic().Create(objSubApp);
                }

            }
        }
        bool flag=  new TMisMonitorSubtaskLogic().Edit(new TMisMonitorSubtaskVo {ID=strSubTask_Id, TASK_ID = strWorkTask_Id, TASK_STATUS = strTaskStatus, TASK_TYPE = "发送",SAMPLE_FINISH_DATE=DateTime.Now.ToString(),SAMPLING_MANAGER_ID=LogInfo.UserInfo.ID.ToString() });
        if (flag) {
            if (new TMisContractSamplePlanLogic().Edit(new TMisContractSamplePlanVo { ID = strSamplePlanId, IF_PLAN="2" }))
            {
                strReturn = "true";
            }
        }
        return strReturn;
    }
    /// <summary>
    /// 预约办理(秦皇岛\清远)
    /// </summary>
    /// <param name="strPlanID">预约表ID</param>
    protected string doSubTask_QHD()
    {
        string strReturn = "";
        string strTaskStatus = "021";
        if (!String.IsNullOrEmpty(strRequest) && strRequest == "true")
        {
            strTaskStatus = "024";
        }
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTask_Id;
        objTask.ASKING_DATE = strDate;
        objTask.TICKET_NUM = strTaskNum;
        objTask.REPORT_HANDLE = getNextReportUserID("Report_UserID");  //报告编制人
        //objTask.QC_STATUS = strQcStatus;
        //objTask.ALLQC_STATUS = strAllQcStatus;
        new TMisMonitorTaskLogic().Edit(objTask);

        TMisMonitorReportVo objReportVoWhere = new TMisMonitorReportVo();
        objReportVoWhere.TASK_ID = strWorkTask_Id;
        TMisMonitorReportVo objReportVoEdit = new TMisMonitorReportVo();
        objReportVoEdit.REPORT_CODE = objTask.TICKET_NUM;
        new TMisMonitorReportLogic().Edit(objReportVoEdit, objReportVoWhere);

        TMisMonitorTaskVo objTaskTmp = new TMisMonitorTaskLogic().Details(strWorkTask_Id);
        TMisMonitorTaskCompanyVo tTaskCompany = new TMisMonitorTaskCompanyVo();
        tTaskCompany.ID = objTaskTmp.TESTED_COMPANY_ID;
        tTaskCompany.CONTACT_NAME = strContactName;
        tTaskCompany.PHONE = strContactPhone;
        new TMisMonitorTaskCompanyLogic().Edit(tTaskCompany);

        TMisContractPlanVo objPlan = new TMisContractPlanVo();
        objPlan.ID = objTaskTmp.PLAN_ID;

        if (!String.IsNullOrEmpty(objPlan.ID))
        {
            if (!String.IsNullOrEmpty(strPlanDate))
            {
                string[] strPlanDateArr = null;
                strPlanDateArr = strPlanDate.Split('-');
                if (strPlanDateArr != null)
                {
                    objPlan.PLAN_YEAR = strPlanDateArr[0].ToString();
                    objPlan.PLAN_MONTH = strPlanDateArr[1].ToString();
                    objPlan.PLAN_DAY = strPlanDateArr[2].ToString();
                    new TMisContractPlanLogic().Create(objPlan);
                }
            }
        }
        bool flag = false;
        DataTable objDt = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo { TASK_ID = strWorkTask_Id });
        if (objDt.Rows.Count > 0)
        {
            for (int i = 0; i < objDt.Rows.Count; i++)
            {
                TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();

                DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = objDt.Rows[i]["ID"].ToString() });
                if (dtApp.Rows.Count > 0)
                {
                    objSubApp.ID = dtApp.Rows[0]["ID"].ToString();
                    objSubApp.SUBTASK_ID = objDt.Rows[i]["ID"].ToString();
                    objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                    objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();
                    new TMisMonitorSubtaskAppLogic().Edit(objSubApp);
                }
                else
                {
                    objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                    objSubApp.SUBTASK_ID = objDt.Rows[i]["ID"].ToString();
                    objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                    objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

                    new TMisMonitorSubtaskAppLogic().Create(objSubApp);
                }

                //判断任务中是否存在在现场项目，如果有把任务分流到现场室 Add By weilin 2014-7-25
                TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoVo();
                SampleInfoVo.SUBTASK_ID = objDt.Rows[i]["ID"].ToString();
                DataTable dtSample = new i3.BusinessLogic.Channels.Mis.Monitor.Sample.TMisMonitorSampleInfoLogic().SelectByTable(SampleInfoVo);
                string strSampleIDs = "";
                for (int j = 0; j < dtSample.Rows.Count; j++)
                {
                    strSampleIDs += dtSample.Rows[j]["ID"].ToString() + ",";
                    SampleInfoVo = new TMisMonitorSampleInfoVo();
                    SampleInfoVo.ID = dtSample.Rows[j]["ID"].ToString();
                    if (!String.IsNullOrEmpty(strRequest) && strRequest == "QY")
                    {
                        SampleInfoVo.NOSAMPLE = "0";
                    }
                    else
                    {
                        if (new i3.BusinessLogic.Channels.Mis.Monitor.Result.TMisMonitorResultLogic().SelectSampleDeptWithSampleID(dtSample.Rows[j]["ID"].ToString()).Rows.Count > 0)
                            SampleInfoVo.NOSAMPLE = "1";
                        else
                            SampleInfoVo.NOSAMPLE = "2";
                    }
                    new i3.BusinessLogic.Channels.Mis.Monitor.Sample.TMisMonitorSampleInfoLogic().Edit(SampleInfoVo);
                }
                if (String.IsNullOrEmpty(strRequest) || strRequest != "QY")
                    new i3.BusinessLogic.Channels.Mis.Monitor.Result.TMisMonitorResultLogic().updateResultBySample(strSampleIDs.TrimEnd(','), "022", true);

                flag = new TMisMonitorSubtaskLogic().Edit(new TMisMonitorSubtaskVo { ID = objDt.Rows[i]["ID"].ToString(), TASK_ID = strWorkTask_Id, TASK_STATUS = strTaskStatus, TASK_TYPE = "发送", SAMPLE_FINISH_DATE = DateTime.Now.ToString(), SAMPLING_MANAGER_ID = LogInfo.UserInfo.ID.ToString() });
            }
            
        }
        
        if (flag)
        {
            if (new TMisContractSamplePlanLogic().Edit(new TMisContractSamplePlanVo { ID = strSamplePlanId, IF_PLAN = "2" }))
            {
                strReturn = "true";
            }
        }
        return strReturn;
    }

    /// <summary>
    /// 创建原因：无委托书的预约任务生成
    /// 创建人：胡方扬
    /// 创建日期：2013-07-01
    /// </summary>
    /// <param name="strPlanID">预约表ID</param>
    protected bool doPlanTaskForSample()
    {
        bool strReturn = false;
        string strCodeRule = "";
        if (!String.IsNullOrEmpty(strSamplePlanId))
        {
            DataTable dtSamplePlan = new TMisContractSamplePlanLogic().SelectByTable(new TMisContractSamplePlanVo { ID = strSamplePlanId });
            if (dtSamplePlan.Rows.Count > 0)
            {
                for (int m = 0; m < dtSamplePlan.Rows.Count; m++)
                {
                    //预约表对象
                    TMisContractSamplePlanVo objContractPlan = new TMisContractSamplePlanLogic().Details(strSamplePlanId);
                    if (strSamplePlanId != "")
                    {
                        //获取委托书信息
                        #region 构造监测任务对象
                        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
                        objTask.ID = PageBase.GetSerialNumber("t_mis_monitor_taskId");
                        objTask.PLAN_ID = strSamplePlanId;
                        objTask.CREATOR_ID = new PageBase().LogInfo.UserInfo.ID;
                        objTask.CREATE_DATE = DateTime.Now.ToString();
                        objTask.TASK_STATUS = "01";
                        objTask.SAMPLE_SOURCE = "送样";
                        objTask.COMFIRM_STATUS = "0";
                        objTask.PROJECT_NAME = strProjectName;
                        objTask.CONTRACT_TYPE = strContractTypeId;
                        objTask.TEST_TYPE = strMonitorId;
                        objTask.SAMPLE_SEND_MAN = strMan;
                        //生成委托书单号
                        TMisContractVo objtCv = new TMisContractVo();
                        objtCv.CONTRACT_TYPE = strContractTypeId;
                        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                        objSerial.SERIAL_TYPE = "1";
                        strCodeRule = CreateBaseDefineCode(objSerial, objtCv);
                        objTask.CONTRACT_CODE = strCodeRule;
                        
                        if (strMonitorId == "000000027") //暑期河流作为常规监测
                        {
                            objTask.TASK_TYPE = "1";
                        }
                        //objTask.CLIENT_COMPANY_ID = InsertContractCompanyInfo(strCompanyId);
                        //objTask.TESTED_COMPANY_ID = InsertContractCompanyInfo(strCompanyIdFrim);
                        objTask.CREATE_DATE = DateTime.Now.ToString();
                        objTask.CONTRACT_YEAR = strYear;
                        objTask.ASKING_DATE = strDate;
                        //生成任务编号
                        TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
                        objSerialTask.SERIAL_TYPE = "4";
                        TMisContractVo objContract = new TMisContractVo();
                        objContract.CONTRACT_TYPE = strContractTypeId;
                        objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
                        //objTask.TICKET_NUM = "未编号";
                        #endregion

                        #region 构造监测任务委托企业信息
                        //委托企业信息
                        TBaseCompanyInfoVo objContractClient = new TBaseCompanyInfoLogic().Details(strCompanyId);
                        //受检企业信息
                        TBaseCompanyInfoVo objContractTested = new TBaseCompanyInfoLogic().Details(strCompanyIdFrim);
                        //构造监测任务委托企业信息
                        TMisMonitorTaskCompanyVo objTaskClient = new TMisMonitorTaskCompanyVo();
                        PageBase.CopyObject(objContractClient, objTaskClient);//复制对象
                        objTaskClient.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                        objTaskClient.TASK_ID = objTask.ID;
                        objTaskClient.COMPANY_ID = strCompanyId;
                        //构造监测任务受检企业信息
                        TMisMonitorTaskCompanyVo objTaskTested = new TMisMonitorTaskCompanyVo();
                        PageBase.CopyObject(objContractTested, objTaskTested);//复制对象
                        objTaskTested.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                        objTaskTested.TASK_ID = objTask.ID;
                        objTaskTested.COMPANY_ID = strCompanyIdFrim;

                        //重新赋值监测任务企业ID
                        objTask.CLIENT_COMPANY_ID = objTaskClient.ID;
                        objTask.TESTED_COMPANY_ID = objTaskTested.ID;
                        #endregion

                        #region 监测报告
                        TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
                        objReportVo.ID = PageBase.GetSerialNumber("t_mis_monitor_report_id");
                        //objReportVo.REPORT_CODE = objContract.CONTRACT_CODE;
                        //生成报告编号  胡方扬 2013-04-24
                        //TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                        //objSerial.SERIAL_TYPE = "3";
                        //objReportVo.REPORT_CODE = PageBase.CreateBaseDefineCode(objSerial, objContract);
                        DataTable objDt = PageBase.getDictList("RptISWT_Code");
                        if (objDt.Rows.Count > 0)
                        {
                            if (objDt.Rows[0]["DICT_CODE"].ToString() == "1")
                            {
                                objReportVo.REPORT_CODE = objTask.TICKET_NUM;
                            }
                        }
                        objReportVo.TASK_ID = objTask.ID;
                        objReportVo.IF_GET = "0";
                        if (strMonitorId == "000000027") //暑期河流作为常规监测
                        {
                            objReportVo.IF_SEND = "5";
                        }
                        #endregion

                        #region 监测子任务信息 根据委托书监测类别进行构造
                        //监测子任务信息 根据委托书监测类别进行构造
                        ArrayList arrSubTask = new ArrayList();//监测子任务集合
                        ArrayList arrSample = new ArrayList();//样品集合--取自送样
                        ArrayList arrSampleResult = new ArrayList();//样品结果集合 
                        ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表
                        #endregion

                        //样品类别集合
                        TMisContractSamplePlanVo objSamplePlan = new TMisContractSamplePlanVo();
                        objSamplePlan.ID = strSamplePlanId;
                        DataTable dtTestType = new TMisContractSamplePlanLogic().GetSamplePlanMonitor(objSamplePlan);

                        //获取样品信息
                        TMisContractSampleVo objSampleInfor = new TMisContractSampleVo();
                        objSampleInfor.SAMPLE_PLAN_ID = strSamplePlanId;
                        DataTable objDtSampleInfor = new TMisContractSampleLogic().SelectByTable(objSampleInfor);

                        //获取预约点位明细信息
                        DataTable dtContractSamplePlanItems = new TMisContractSamplePlanLogic().GetSamplePlanItems(objSamplePlan);
                        //监测子任务
                        #region 监测子任务
                        if (dtTestType.Rows.Count > 0)
                        {
                            string strSubTaskIDs = new PageBase().GetSerialNumberList("t_mis_monitor_subtaskId", dtTestType.Rows.Count);
                            string[] arrSubTaskIDs = strSubTaskIDs.Split(',');
                            for (int i = 0; i < dtTestType.Rows.Count; i++)
                            {
                                string str = dtTestType.Rows[i]["MONITOR_ID"].ToString();//监测类别
                                if (str.Length > 0)
                                {
                                    #region 监测子任务
                                    //监测子任务
                                    TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
                                    string strSampleManagerID = "";//采样负责人ID
                                    string strSampleID = "";//采样协同人ID串
                                    GetSamplingMan(str, objContract.ID, ref strSampleManagerID, ref strSampleID);
                                    objSubtask.ID = arrSubTaskIDs[i];
                                    objSubtask.TASK_ID = objTask.ID;
                                    objSubtask.MONITOR_ID = str;
                                    objSubtask.SAMPLING_MANAGER_ID = objContract.SAMPLE_ACCEPTER_ID;
                                    objSubtask.SAMPLING_ID = strSampleID;
                                    //objSubtask.TASK_TYPE = "发送";
                                    //objSubtask.TASK_STATUS = "024";
                                    arrSubTask.Add(objSubtask);
                                    #endregion

                                    #region 按类别分样品
                                    //监测按类别分样品
                                    DataRow[] dtTypePoint = objDtSampleInfor.Select(" MONITOR_ID='" + str + "'");
                                    if (dtTypePoint.Length > 0)
                                    {
                                        for (int n = 0; n < dtTypePoint.Length; n++)
                                        {
                                            string strSampleIDs = PageBase.GetSerialNumber("MonitorSampleId");

                                            #region 构造样品表数据
                                            //样品 
                                            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                            objSampleInfo.ID = strSampleIDs;
                                            objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                            //objSampleInfo.POINT_ID = dtTypePoint[n]["ID"].ToString();
                                            objSampleInfo.SAMPLE_NAME = dtTypePoint[n]["SAMPLE_NAME"].ToString();
                                            objSampleInfo.SAMPLE_TYPE = dtTypePoint[n]["SAMPLE_TYPE"].ToString();
                                            objSampleInfo.SAMPLE_STATUS = dtTypePoint[n]["SAMPLE_STATUS"].ToString();
                                            objSampleInfo.SRC_CODEORNAME = dtTypePoint[n]["SRC_CODEORNAME"].ToString();
                                            objSampleInfo.SAMPLE_ACCEPT_DATEORACC = dtTypePoint[n]["SAMPLE_ACCEPT_DATEORACC"].ToString();
                                            objSampleInfo.REMARK1 = dtTypePoint[n]["REMARK1"].ToString();
                                            objSampleInfo.SAMPLE_COUNT = dtTypePoint[n]["SAMPLE_COUNT"].ToString();
                                            objSampleInfo.QC_TYPE = "0";//默认原始样
                                            objSampleInfo.NOSAMPLE = "0";//默认未采样
                                            arrSample.Add(objSampleInfo);
                                            #endregion

                                            DataRow[] dtTypeItem = dtContractSamplePlanItems.Select("SAMPLE_ID=" + dtTypePoint[n]["ID"].ToString() + "");
                                            string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtTypeItem.Length);
                                            string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                            string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtTypeItem.Length);
                                            string[] arrResultAppIDs = strResultAppIDs.Split(',');
                                            for (int k = 0; k < dtTypeItem.Length; k++)
                                            {

                                                #region 构造样品分析结果表数据
                                                //构造样品结果表
                                                string strAnalysisID = "";//分析方法ID
                                                string strStandardID = "";//方法依据ID
                                                string strCheckOut = ""; //检出限
                                                getMethod(dtTypeItem[k]["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                                TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                                objSampleResult.ID = arrSampleResultIDs[k];
                                                objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                                objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                                objSampleResult.ITEM_ID = dtTypeItem[k]["ITEM_ID"].ToString();
                                                objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(dtTypeItem[k]["ITEM_ID"].ToString());
                                                objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                                objSampleResult.STANDARD_ID = strStandardID;
                                                objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                                objSampleResult.QC = "0";// 默认原始样手段
                                                objSampleResult.TASK_TYPE = "发送";
                                                objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                                objSampleResult.PRINTED = "0";//默认未打印
                                                objSampleResult.REMARK_4 = "1";
                                                arrSampleResult.Add(objSampleResult);
                                                #endregion

                                                #region 构造样品分析结果执行表数据
                                                //构造样品执行表
                                                TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                                objResultApp.ID = arrResultAppIDs[k];
                                                objResultApp.RESULT_ID = objSampleResult.ID;
                                                objResultApp.HEAD_USERID = GetAnayUser(str, dtTypeItem[k]["ITEM_ID"].ToString(), "duty_analyse", true);
                                                objResultApp.ASSISTANT_USERID = GetAnayUser(str, dtTypeItem[k]["ITEM_ID"].ToString(), "duty_analyse", false);
                                                arrSampleResultApp.Add(objResultApp);
                                                #endregion
                                            }

                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        if (new TMisMonitorTaskLogic().SaveSampleTrans(objTask, objTaskClient, objTaskTested, objReportVo, arrSubTask, arrSample, arrSampleResult, arrSampleResultApp))
                        {
                            TMisContractSamplePlanVo objItemSap = new TMisContractSamplePlanVo();
                            objItemSap.ID = strSamplePlanId;
                            objItemSap.IF_PLAN = "1";
                            if (new TMisContractSamplePlanLogic().Edit(objItemSap))
                            {
                                strReturn = true;
                            }
                        }
                    }
                }
            }
        }
        return strReturn;
    }
    #region 获取指定监测类别 监测项目 分析负责人 分析协同人 Create By 胡方扬  2013-07-17
    public string GetAnayUser(string strMonitorId, string strItemsId, string strDictCode, bool isHead)
    {
        string result = "";

        //潘德军修改2013-7-19 环境质量的监测类别取对应的污染源类别的岗位职责
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorType = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strMonitorId);
        if (objMonitorType.REMARK1.Trim().Length > 0)
        {
            strMonitorId = objMonitorType.REMARK1.Trim();
        }
        
        TSysDutyVo objDuty = new TSysDutyVo();
        objDuty.DICT_CODE = "duty_analyse";
        objDuty.MONITOR_TYPE_ID = strMonitorId;
        objDuty.MONITOR_ITEM_ID = strItemsId;
        DataTable dt = new TSysDutyLogic().GetDutyUser(objDuty);
        if (dt.Rows.Count > 0)
        {
            if (isHead)
            {
                DataRow[] drArr = dt.Select(" IF_DEFAULT='0'");
                if (drArr.Length > 0)
                {
                    result = drArr[0]["USERID"].ToString();
                }
                else
                {
                    result = dt.Rows[0]["USERID"].ToString();
                }
            }
            else
            {
                DataRow[] drArr = dt.Select(" IF_DEFAULT_EX='0'");
                if (drArr.Length > 0)
                {
                    foreach (DataRow drr in drArr)
                    {
                        result += drr["USERID"].ToString() + ",";
                    }
                    result = result.Substring(0, result.Length - 1);
                }
            }
        }

        return result;
    }
    #endregion
    #region 采样、分析默认人员
    /// <summary>
    /// 获得采样人员相关信息
    /// </summary>
    /// <param name="strMonitorID">监测类别</param>
    /// <param name="strItemID">监测项目</param>
    /// <param name="strSampleManager">采样负责人ID</param>
    /// <param name="strSampleID">采样协同人ID</param>
    protected void GetSamplingMan(string strMonitorID, string strContractID, ref string strSampleManager, ref string strSampleID)
    {
        if (!String.IsNullOrEmpty(strMonitorID) && !string.IsNullOrEmpty(strContractID))
        {
            TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
            objItems.CONTRACT_ID = strContractID;
            objItems.MONITOR_TYPE_ID = strMonitorID;
            DataTable dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i] as DataRow;
                    strSampleManager = dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString() : (dr["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dr["SAMPLING_MANAGER_ID"].ToString() : strSampleManager);
                    strSampleID += strSampleID.Contains(dr["SAMPLING_ID"].ToString()) ? "" : (dr["SAMPLING_ID"].ToString().Length > 0 ? dr["SAMPLING_ID"].ToString() + "," : "");
                }
            }
        }
        strSampleID = strSampleID.Length > 0 ? strSampleID.Remove(strSampleID.LastIndexOf(",")) : "";
    }
    #endregion

    #region 分析方法、依据获取
    /// <summary>
    /// 获取指定监测项目的默认分析方法、依据
    /// </summary>
    /// <param name="strItemID">监测项目ID</param>
    /// <param name="strAnalysisID">分析方法ID</param>
    /// <param name="strStandardID">方法依据ID</param>
    /// <param name="strCheckOut">最底检出限</param>
    protected void getMethod(string strItemID, ref string strAnalysisID, ref string strStandardID, ref string strCheckOut)
    {
        TBaseItemAnalysisVo objAnalysis = new TBaseItemAnalysisVo();
        objAnalysis.ITEM_ID = strItemID;
        objAnalysis.IS_DEL = "0";
        DataTable dtItemAnalysis = new TBaseItemAnalysisLogic().SelectByTable(objAnalysis);
        if (dtItemAnalysis.Rows.Count > 0)
        {
            for (int i = 0; i < dtItemAnalysis.Rows.Count; i++)
            {
                if (dtItemAnalysis.Rows[i]["IS_DEFAULT"].ToString() == "是")//默认负责人，否则取第一条方法
                {
                    strAnalysisID = dtItemAnalysis.Rows[i]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[i]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[i]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }
                    
                    break;//默认方法 唯一
                }
                else
                {
                    strAnalysisID = dtItemAnalysis.Rows[0]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[0]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[0]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }
                }
            }
            
        }
    }
    #endregion

    /// <summary>
    /// 获取采用的标准项的上下限
    /// </summary>
    /// <param name="strItemID">项目ID</param>
    /// <param name="strConditionID">条件项ID</param>
    /// <param name="strUp">上限</param>
    /// <param name="strLow">下限</param>
    protected void getStandardValue(string strItemID, string strConditionID, ref string strUp, ref string strLow, ref string strConditionType)
    {
        TBaseEvaluationConItemVo objConItemVo = new TBaseEvaluationConItemVo();
        objConItemVo.ITEM_ID = strItemID;
        objConItemVo.CONDITION_ID = strConditionID;
        objConItemVo.IS_DEL = "0";
        objConItemVo = new TBaseEvaluationConItemLogic().Details(objConItemVo);
        //上限处理
        if (objConItemVo.DISCHARGE_UPPER.Length > 0)
        {
            //上限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.UPPER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_UPPER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_UPPER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strUp += (strUnit + str) + ",";
                    }
                }
                if (strUp.Length > 0)
                {
                    strUp = strUp.Remove(strUp.LastIndexOf(","));
                }
            }
            else
            {
                strUp = strUnit + objConItemVo.DISCHARGE_UPPER;
            }
        }
        //下限处理
        if (objConItemVo.DISCHARGE_LOWER.Length > 0)
        {
            //下限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.LOWER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_LOWER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_LOWER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strLow += (strUnit + str) + ",";
                    }
                }
                if (strLow.Length > 0)
                {
                    strLow = strLow.Remove(strLow.LastIndexOf(","));
                }
            }
            else
            {
                strLow = strUnit + objConItemVo.DISCHARGE_LOWER;
            }
        }
        strConditionType = new TBaseEvaluationInfoLogic().Details(new TBaseEvaluationConInfoLogic().Details(strConditionID).STANDARD_ID).STANDARD_TYPE;
    }

    private string InsertContractCompanyInfo(string strCompanyID)
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisMonitorTaskCompanyVo objTmc = new TMisMonitorTaskCompanyVo();
        //Update By SSZ 将基础资料企业信息复制到委托书企业信息
        //基础企业资料信息
        TBaseCompanyInfoVo objCompanyInfo = new TBaseCompanyInfoLogic().Details(strCompanyID);
        //将相同字段的数据进行复制
        CopyObject(objCompanyInfo, objTmc);
        //Update end 
        objTmc.COMPANY_ID = strCompanyID;

        objTmc.IS_DEL = "0";

        objTmc.ID = i3.View.PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
        if (new TMisMonitorTaskCompanyLogic().Create(objTmc))
        {
            result = objTmc.ID.ToString();

        }
        return result;
    }

    /// <summary>
    /// 创建原因：获取指定监测项目ID的采样仪器ID
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <param name="strItemID"></param>
    /// <returns></returns>
    public string GetItemSamplingInstrumentID(string strItemID)
    {
        string result = "";
        DataTable dt = new TBaseItemSamplingInstrumentLogic().GetItemSamplingInstrument(strItemID);
        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["ID"].ToString();
        }
        return result;
    }
    /// <summary>
    /// 获取URL参数
    /// </summary>
    /// <param name="context"></param>
    public void GetRequestParme(HttpContext context) {
        //排序信息
        if (!String.IsNullOrEmpty(context.Request.Params["sortname"]))
        {
            strSortname = context.Request["sortname"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["sortorder"]))
        {
            strSortorder = context.Request.Params["sortorder"].Trim();
        }
        //当前页面
        if (!String.IsNullOrEmpty(context.Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(context.Request.Params["page"].Trim());
        }
        //每页记录数
        if (!String.IsNullOrEmpty(context.Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(context.Request.Params["pagesize"].Trim());
        }
        //方法
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["type"]))
        {
            strType = context.Request.Params["type"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strContratId"]))
        {
            strContratId = context.Request.Params["strContratId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strWorkTask_Id"]))
        {
            strWorkTask_Id = context.Request.Params["strWorkTask_Id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSubTask_Id"]))
        {
            strSubTask_Id = context.Request.Params["strSubTask_Id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSamplePlanId"]))
        {
            strSamplePlanId = context.Request.Params["strSamplePlanId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractTypeId"]))
        {
            strContractTypeId = context.Request.Params["strContractTypeId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strProjectName"]))
        {
            strProjectName = context.Request.Params["strProjectName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyIdFrim"]))
        {
            strCompanyIdFrim = context.Request.Params["strCompanyIdFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyId"]))
        {
            strCompanyId = context.Request.Params["strCompanyId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitorId"]))
        {
            strMonitorId = context.Request.Params["strMonitorId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strYear"]))
        {
            strYear = context.Request.Params["strYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDate"]))
        {
            strDate = context.Request.Params["strDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPlanDate"]))
        {
            strPlanDate = context.Request.Params["strPlanDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAllQcStatus"]))
        {
            strAllQcStatus = context.Request.Params["strAllQcStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strQcStatus"]))
        {
            strQcStatus = context.Request.Params["strQcStatus"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["Company_Name"]))
        {
            strCompany_Name = context.Request.Params["Company_Name"].Trim();
        }
        //使用初始化URL参数标识是 郑州 还是其他地方的
        if (!String.IsNullOrEmpty(context.Request.Params["strRequest"]))
        {
            strRequest = context.Request.Params["strRequest"].Trim();
        }
        //潘德军2013-12-23
        if (!String.IsNullOrEmpty(context.Request.Params["strTaskNum"]))
        {
            strTaskNum = context.Request.Params["strTaskNum"].Trim();
        }
        //潘德军2014-01-07
        if (!String.IsNullOrEmpty(context.Request.Params["strContactName"]))
        {
            strContactName = context.Request.Params["strContactName"].Trim();
        }
        //潘德军2014-01-07
        if (!String.IsNullOrEmpty(context.Request.Params["strContactPhone"]))
        {
            strContactPhone = context.Request.Params["strContactPhone"].Trim();
        }
        //weilin 2014-10-11
        if (!String.IsNullOrEmpty(context.Request.Params["strMan"]))
        {
            strMan = context.Request.Params["strMan"].Trim();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}