using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using System.Configuration;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using System.Collections;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.ProcessMgm;
using WebApplication;

/// <summary>
/// 功能描述：委托监测委托书录入
/// 创建时间：2015-01-24
/// </summary>
public partial class Channels_MisII_Contract_ProgramCreate_ContractInfor : PageBase
{
    private string task_id = "", strTaskCode = "", strTaskProjectName = "", strBtnType = "", strCompanyId = "";
    private string strConfigFreqSetting = ConfigurationManager.AppSettings["FreqSetting"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取隐藏域参数
            GetHiddenParme();

            //如果是发送前事件
            if (Request["type"] != null && Request["type"].ToString() == "CreateTaskInfo")
            {
                var workID = Request.QueryString["OID"];     //当前流程ID

                TMisContractVo objContractVo = new TMisContractVo();
                objContractVo.CCFLOW_ID1 = workID;
                objContractVo = new TMisContractLogic().Details(objContractVo);
                task_id = objContractVo.ID;
                strCompanyId = objContractVo.TESTED_COMPANY_ID;

                if (task_id == "")
                {
                    Response.Write(HttpUtility.UrlEncode("false委托书没有保存，不能发送"));
                }
                else
                {
                    string strFK_Event = Request["FK_Event"];
                    if (strFK_Event.IndexOf("SendWhen") == -1)
                    {
                        #region 判断是否已经存在业务数据，如果存在则更新相关数据表（场景：委托书录入环节退回的发送）
                        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
                        objTaskVo.CCFLOW_ID1 = workID;
                        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
                        if (objTaskVo.ID.Length > 0)
                        {
                            TMisContractPointFreqVo objFreqVo = new TMisContractPointFreqVo();
                            objFreqVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
                            new TMisContractPointFreqLogic().Delete(objFreqVo);

                            TMisContractPlanPointVo objPlanPointVo = new TMisContractPlanPointVo();
                            objPlanPointVo.PLAN_ID = objTaskVo.PLAN_ID;
                            new TMisContractPlanPointLogic().Delete(objPlanPointVo);

                            TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                            objPlanVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
                            new TMisContractPlanLogic().Delete(objPlanVo);

                            objTaskVo.CCFLOW_ID1 = "delete";
                            new TMisMonitorTaskLogic().Edit(objTaskVo);
                        }
                        #endregion
                    }

                    TMisContractPlanVo objTemp = new TMisContractPlanLogic().Details(new TMisContractPlanVo() { CCFLOW_ID1 = workID.ToString() });
                    bool b = true;
                    if (objTemp == null || string.IsNullOrEmpty(objTemp.ID))
                    {
                        b = CreateContractPlan(task_id, strCompanyId, workID);
                    }

                    if (!b)
                    {
                        Response.Write(HttpUtility.UrlEncode("false插入业务数据失败请重试"));
                    }
                    else
                    {
                        Response.Write("true");
                    }
                }
                Response.ContentType = "text/plain";
                Response.End();
            }

            //如果是发送成功事件
            if (Request["type"] != null && Request["type"].ToString() == "AfterSuccessSend")
            {

                Response.ContentEncoding = Encoding.GetEncoding("gb2312");

                var workID = Request.QueryString["OID"];     //当前流程ID

                var contract = new TMisContractLogic().Details(new TMisContractVo { CCFLOW_ID1 = workID });

                var title = string.Format("{0}-{1}", contract.PROJECT_NAME, contract.CONTRACT_CODE);

                CCFlowFacade.SetFlowTitle(Request["UserNo"].ToString(), Request["FK_Flow"].ToString(), Convert.ToInt64(workID), title);

                Response.Write("发送成功");

                Response.ContentType = "text/plain";
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strTaskCode = this.hidTaskCode.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
        strBtnType = this.hidBtnType.Value.ToString();
        strCompanyId = this.hidCompanyId.Value.ToString();
    }
    #region 生成业务数据

    /// <summary>
    /// 插入监测计划信息
    /// </summary>
    /// <returns></returns>
    public bool CreateContractPlan(string strTaskId, string strCompanyId, string ccflowID = "")
    {
        string strSampleDay = "1";
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strTaskId;
        //获取当前委托书监测点位
        dt = GetMonitorPoint(strTaskId);
        #region 清远点位频次处理方式
        DataTable objDtF = PageBase.getDictList("FreqTask");
        if (objDtF.Rows.Count > 0 && objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
        {
            //if (objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
            //{
            DataTable dtDict = PageBase.getDictList("Freq");
            DataTable objDtContractType = PageBase.getDictList("FreqMonitor");
            string strContractTypeId = "";
            if (objDtContractType.Rows.Count > 0)
            {
                strContractTypeId = objDtContractType.Rows[0]["DICT_CODE"].ToString();
            }
            TMisContractVo objTmisContract = new TMisContractLogic().Details(strTaskId);

            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SERIAL_TYPE = "1";
            TMisContractVo objvo = new TMisContractVo();
            objvo.CONTRACT_TYPE = objTmisContract.CONTRACT_TYPE;
            objTmisContract.CONTRACT_CODE = CreateBaseDefineCode(objSerial, objvo);
            new TMisContractLogic().Edit(objTmisContract);

            if (objDtF.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(strContractTypeId) && strContractTypeId != "0" && (strContractTypeId.IndexOf(objTmisContract.CONTRACT_TYPE) >= 0))
                {
                    if (dt != null && dtDict != null)
                    {
                        if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, dtDict, strTaskId))
                        {
                            flag = true;
                        }
                    }
                }
                else
                {
                    if (dt != null)
                    {
                        if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, strTaskId))
                        {
                            if (dt.Rows.Count > 0)
                                strSampleDay = dt.Rows[0]["SAMPLE_DAY"].ToString();
                            if (SavePlanInfor(strTaskId, strCompanyId, ccflowID))
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            //}
        }

        #endregion

        #region 其他地方点位处理方式
        else
        {
            if (dt != null)
            {
                if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, strTaskId))
                {
                    if (SavePlanInfor(strTaskId, strCompanyId))
                    {
                        flag = true;
                    }
                }
            }
        }
        #endregion
        return flag;
    }
    /// <summary>
    /// 获取委托书监测点位
    /// </summary>
    /// <param name="strTaskId">委托书业务ID</param>
    /// <returns></returns>
    public DataTable GetMonitorPoint(string strTaskId)
    {
        DataTable dt = new DataTable();
        TMisContractPointVo objItems = new TMisContractPointVo();
        objItems.CONTRACT_ID = strTaskId;

        dt = new TMisContractPointLogic().SelectByTable(objItems);

        return dt;
    }

    /// <summary>
    /// 保存监测预约计划 胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanInfor(string strTaskId, string strCompanyId, string ccflowID = "")
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
            objItems.CONTRACT_ID = strTaskId;
            dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);
            objItems.CONTRACT_COMPANY_ID = strCompanyId;
            objItems.CCFLOW_ID1 = ccflowID;

            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && PageBase.IsNumeric(dt.Rows[0]["NUM"].ToString()))
            {
                objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
            }
            else
            {
                objItems.PLAN_NUM = "1";
            }
            if (new TMisContractPlanLogic().Create(objItems))
            {
                if (SavePlanPoint(strTaskId, objItems.ID))
                {
                    if (doPlanTask(objItems.ID))
                    {
                        // by yinchengyi 2015-9-22 任务正式下达后 才能生成任务的PLAN_ID 更新ProcessMgm中的PLAN_ID
                        TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
                        objLogic.UpdatePlanIDOfContractTask(objItems.CONTRACT_ID + "tempPlanIdOfConract", objItems.ID);

                        flag = true;
                    }
                }
            }
        }
        return flag;
    }
    /// <summary>
    /// 插入监测任务预约点位表信息  胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanPoint(string strTaskId, string strPlanId)
    {
        bool flag = false;
        if (new TMisContractPlanPointLogic().SavePlanPoint(strTaskId, strPlanId))
        {
            flag = true;
        }
        return flag;
    }
    /// <summary>
    /// 常规预约办理（任务、报告、样品等生成）
    /// </summary>
    /// <param name="strPlanID">预约ID</param>
    /// <returns>返回true Or false</returns>
    protected bool doPlanTask(string strPlanID)
    {
        bool strReturn = false;
        string strTaskFreqType = "0";
        strTaskFreqType = System.Configuration.ConfigurationManager.AppSettings["TaskFreqType"].ToString();
        //预约表对象
        TMisContractPlanVo objContractPlan = new TMisContractPlanLogic().Details(strPlanID);
        if (objContractPlan != null)
        {
            //获取委托书信息
            TMisContractVo objContract = new TMisContractLogic().Details(objContractPlan.CONTRACT_ID);

            #region 构造监测任务对象
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            PageBase.CopyObject(objContract, objTask);
            objTask.ID = PageBase.GetSerialNumber("t_mis_monitor_taskId");
            //生成任务编号
            TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            objSerialTask.SERIAL_TYPE = "4";

            ////潘德军 2013-12-23 任务单号可改，且初始不生成
            //objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
            string flag = ConfigurationManager.AppSettings["DeptInfor"];
            if (flag == "郑州市环境保护监测站")
            {
                objTask.TICKET_NUM = "未编号";
            }
            else
            {
                if (!string.IsNullOrEmpty(objContract.REMARK5))
                {
                    objTask.TICKET_NUM = objContract.REMARK5;//REMARK5暂时用来存放任务单号，在发送或保存任务时，任务单号会作为Task表的TICKET_NUM被使用，之后REMARK5无意义
                }
                else
                {
                    objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
                }
                //如果objTask.TICKET_NUM已经被占用，需要再次生成
                while (new TMisMonitorTaskLogic().GetSelectResultCountByTicketNum(objTask.TICKET_NUM) > 0)
                {
                    objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
                }
            }

            objTask.CONTRACT_ID = objContract.ID;
            objTask.PLAN_ID = strPlanID;
            objTask.CONSIGN_DATE = objContract.ASKING_DATE;
            objTask.CREATOR_ID = new PageBase().LogInfo.UserInfo.ID;
            objTask.CREATE_DATE = DateTime.Now.ToString();
            objTask.TASK_STATUS = "01";
            objTask.CCFLOW_ID1 = objContract.CCFLOW_ID1;

            //update by ssz 增加默认的确认状态
            objTask.COMFIRM_STATUS = "0";
            #endregion

            #region 构造监测任务委托企业信息
            //委托企业信息
            TMisContractCompanyVo objContractClient = new TMisContractCompanyLogic().Details(objContract.CLIENT_COMPANY_ID);
            //受检企业信息
            TMisContractCompanyVo objContractTested = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);
            //构造监测任务委托企业信息
            TMisMonitorTaskCompanyVo objTaskClient = new TMisMonitorTaskCompanyVo();
            PageBase.CopyObject(objContractClient, objTaskClient);//复制对象
            objTaskClient.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
            objTaskClient.TASK_ID = objTask.ID;
            objTaskClient.COMPANY_ID = objContract.CLIENT_COMPANY_ID;
            //构造监测任务受检企业信息
            TMisMonitorTaskCompanyVo objTaskTested = new TMisMonitorTaskCompanyVo();
            PageBase.CopyObject(objContractTested, objTaskTested);//复制对象
            objTaskTested.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
            objTaskTested.TASK_ID = objTask.ID;
            objTaskTested.COMPANY_ID = objContract.TESTED_COMPANY_ID;

            //重新赋值监测任务企业ID
            objTask.CLIENT_COMPANY_ID = objTaskClient.ID;
            objTask.TESTED_COMPANY_ID = objTaskTested.ID;
            #endregion

            #region 监测报告 胡方扬 2013-04-23 Modify  将报告记录初始化生成数据移到委托书办理完毕后就生成
            TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
            objReportVo.ID = PageBase.GetSerialNumber("t_mis_monitor_report_id");

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
            #endregion

            #region 监测子任务信息 根据委托书监测类别进行构造
            //监测子任务信息 根据委托书监测类别进行构造
            ArrayList arrSubTask = new ArrayList();//监测子任务集合
            ArrayList arrTaskPoint = new ArrayList();//监测点位集合
            ArrayList arrPointItem = new ArrayList();//监测点位明细集合
            ArrayList arrSample = new ArrayList();//样品集合
            ArrayList arrSampleResult = new ArrayList();//样品结果集合 
            ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表
            #endregion

            //监测类别集合
            DataTable dtTestType = new TMisContractPointLogic().GetTestType(strPlanID);
            //预约点位
            DataTable dtPoint = new TMisContractPointLogic().SelectPointTable(strPlanID);
            //获取预约点位明细信息
            DataTable dtContractPoint = new TMisContractPointLogic().SelectByTableForPlan(strPlanID);
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
                        if (objContract.PROJECT_ID != "")
                        {
                            objSubtask.SAMPLING_MANAGER_ID = objContract.PROJECT_ID;
                        }
                        else
                        {
                            objSubtask.SAMPLING_MANAGER_ID = strSampleManagerID;
                        }
                        objSubtask.SAMPLING_ID = strSampleID;
                        objSubtask.TASK_TYPE = "发送";
                        objSubtask.TASK_STATUS = "01";
                        arrSubTask.Add(objSubtask);

                        TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
                        objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                        objSubApp.SUBTASK_ID = objSubtask.ID;
                        new TMisMonitorSubtaskAppLogic().Create(objSubApp);

                        #endregion

                        #region 按类别分点位
                        //按类别分点位
                        DataRow[] dtTypePoint = dtPoint.Select("MONITOR_ID='" + str + "'");
                        if (dtTypePoint.Length > 0)
                        {
                            string strTaskPointIDs = new PageBase().GetSerialNumberList("t_mis_monitor_taskpointId", dtTypePoint.Length);
                            string[] arrTaskPointIDs = strTaskPointIDs.Split(',');
                            string strSampleIDs = new PageBase().GetSerialNumberList("MonitorSampleId", dtTypePoint.Length);
                            string[] arrSampleIDs = strSampleIDs.Split(',');

                            for (int j = 0; j < dtTypePoint.Length; j++)
                            {
                                DataRow drPoint = dtTypePoint[j];
                                #region 监测点位
                                // 监测点位 
                                TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
                                objTaskPoint.ID = arrTaskPointIDs[j];
                                objTaskPoint.TASK_ID = objTask.ID;
                                objTaskPoint.SUBTASK_ID = objSubtask.ID;
                                objTaskPoint.POINT_ID = drPoint["POINT_ID"].ToString();
                                objTaskPoint.MONITOR_ID = str;
                                objTaskPoint.COMPANY_ID = objTaskTested.ID;
                                objTaskPoint.CONTRACT_POINT_ID = drPoint["ID"].ToString();
                                objTaskPoint.POINT_NAME = drPoint["POINT_NAME"].ToString();
                                objTaskPoint.DYNAMIC_ATTRIBUTE_ID = drPoint["DYNAMIC_ATTRIBUTE_ID"].ToString();
                                objTaskPoint.ADDRESS = drPoint["ADDRESS"].ToString();
                                objTaskPoint.LONGITUDE = drPoint["LONGITUDE"].ToString();
                                objTaskPoint.LATITUDE = drPoint["LATITUDE"].ToString();
                                objTaskPoint.FREQ = drPoint["FREQ"].ToString();
                                objTaskPoint.DESCRIPTION = drPoint["DESCRIPTION"].ToString();
                                objTaskPoint.NATIONAL_ST_CONDITION_ID = drPoint["NATIONAL_ST_CONDITION_ID"].ToString();
                                objTaskPoint.INDUSTRY_ST_CONDITION_ID = drPoint["INDUSTRY_ST_CONDITION_ID"].ToString();
                                objTaskPoint.LOCAL_ST_CONDITION_ID = drPoint["LOCAL_ST_CONDITION_ID"].ToString();
                                objTaskPoint.IS_DEL = "0";
                                objTaskPoint.NUM = drPoint["NUM"].ToString();
                                objTaskPoint.CREATE_DATE = DateTime.Now.ToString();
                                arrTaskPoint.Add(objTaskPoint);
                                #endregion

                                //点位采用的标准条件项ID
                                string strConditionID = "";
                                if (!string.IsNullOrEmpty(objTaskPoint.NATIONAL_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.NATIONAL_ST_CONDITION_ID;
                                }
                                if (!string.IsNullOrEmpty(objTaskPoint.LOCAL_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.LOCAL_ST_CONDITION_ID;
                                }
                                if (!string.IsNullOrEmpty(objTaskPoint.INDUSTRY_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.INDUSTRY_ST_CONDITION_ID;
                                }

                                //计算采样频次

                                DataTable dtDict = PageBase.getDictList("is_Zhengzhou");//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                string strIsZhengzhou = "";
                                if (dtDict.Rows.Count > 0)
                                {
                                    strIsZhengzhou = dtDict.Rows[0]["DICT_CODE"].ToString();
                                }

                                int intFreq = 1, intSampleDay = 1;
                                if (drPoint["FREQ"].ToString().Length > 0)
                                {
                                    intFreq = int.Parse(drPoint["FREQ"].ToString());
                                }
                                if (strIsZhengzhou == "1")//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                {
                                    if (drPoint["SAMPLE_FREQ"].ToString().Length > 0)
                                    {
                                        intFreq = int.Parse(drPoint["SAMPLE_FREQ"].ToString());
                                    }
                                }
                                //计算周期
                                if (drPoint["SAMPLE_DAY"].ToString().Length > 0)
                                {
                                    intSampleDay = int.Parse(drPoint["SAMPLE_DAY"].ToString());
                                }
                                #region 样品信息、结果、结果执行
                                #region 如果 strTaskFreqType 判断为0
                                if (!String.IsNullOrEmpty(strTaskFreqType) && strTaskFreqType == "0")
                                {
                                    #region 样品
                                    //样品 与点位对应
                                    TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                    objSampleInfo.ID = arrSampleIDs[j];
                                    objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                    objSampleInfo.POINT_ID = objTaskPoint.ID;
                                    objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                    objSampleInfo.QC_TYPE = "0";//默认原始样
                                    objSampleInfo.NOSAMPLE = "0";//默认未采样
                                    arrSample.Add(objSampleInfo);
                                    #endregion

                                    //预约项目明细
                                    DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
                                    if (dtPointItem.Length > 0)
                                    {
                                        string strTaskItemIDs = new PageBase().GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
                                        string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
                                        string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtPointItem.Length);
                                        string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                        string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
                                        string[] arrResultAppIDs = strResultAppIDs.Split(',');

                                        for (int k = 0; k < dtPointItem.Length; k++)
                                        {
                                            DataRow drPointItem = dtPointItem[k];
                                            //项目采用的标准上限、下限
                                            string strUp = "";
                                            string strLow = "";
                                            string strConditionType = "";
                                            getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                                            #region 构造监测任务点位明细表
                                            //构造监测任务点位明细表
                                            TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
                                            objMonitorTaskItem.ID = arrTaskItemIDs[k];
                                            objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
                                            objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                            objMonitorTaskItem.CONDITION_ID = strConditionID;
                                            objMonitorTaskItem.CONDITION_TYPE = strConditionType;
                                            objMonitorTaskItem.ST_UPPER = strUp;
                                            objMonitorTaskItem.ST_LOWER = strLow;
                                            objMonitorTaskItem.IS_DEL = "0";
                                            arrPointItem.Add(objMonitorTaskItem);
                                            #endregion

                                            #region 构造样品结果表
                                            //构造样品结果表
                                            string strAnalysisID = "";//分析方法ID
                                            string strStandardID = "";//方法依据ID
                                            string strCheckOut = ""; //检出限
                                            getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                            TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                            objSampleResult.ID = arrSampleResultIDs[k];
                                            objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                            objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                            objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                            objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(drPointItem["ITEM_ID"].ToString());
                                            objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                            objSampleResult.STANDARD_ID = strStandardID;
                                            objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                            objSampleResult.QC = "0";// 默认原始样手段
                                            objSampleResult.TASK_TYPE = "发送";
                                            objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                            objSampleResult.PRINTED = "0";//默认未打印
                                            arrSampleResult.Add(objSampleResult);
                                            #endregion

                                            #region 构造样品执行表
                                            //构造样品执行表
                                            TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                            objResultApp.ID = arrResultAppIDs[k];
                                            objResultApp.RESULT_ID = objSampleResult.ID;
                                            objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
                                            objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
                                            arrSampleResultApp.Add(objResultApp);
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                #region 如果 strTaskFreqType 判断为1
                                if (!String.IsNullOrEmpty(strTaskFreqType) && strTaskFreqType == "1")
                                {
                                    bool bNeedDayAndFreq = true;//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                    if (intSampleDay == 1 && intFreq == 1)//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                    {
                                        bNeedDayAndFreq = false;
                                    }

                                    for (int r = 0; r < intSampleDay; r++)
                                    {
                                        for (int s = 0; s < intFreq; s++)
                                        {
                                            #region 样品
                                            //样品 与点位对应
                                            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                            objSampleInfo.ID = new PageBase().GetSerialNumberList("MonitorSampleId", 1);
                                            objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                            objSampleInfo.POINT_ID = objTaskPoint.ID;
                                            //objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                            if (bNeedDayAndFreq)//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                                objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME + " 第" + (r + 1).ToString() + "天" + " 第" + (s + 1).ToString() + "次";
                                            else
                                                objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                            objSampleInfo.QC_TYPE = "0";//默认原始样
                                            objSampleInfo.NOSAMPLE = "0";//默认未采样
                                            arrSample.Add(objSampleInfo);
                                            #endregion

                                            //预约项目明细
                                            DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
                                            if (dtPointItem.Length > 0)
                                            {
                                                string strTaskItemIDs = new PageBase().GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
                                                string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
                                                string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtPointItem.Length);
                                                string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                                string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
                                                string[] arrResultAppIDs = strResultAppIDs.Split(',');

                                                for (int k = 0; k < dtPointItem.Length; k++)
                                                {
                                                    DataRow drPointItem = dtPointItem[k];
                                                    //项目采用的标准上限、下限
                                                    string strUp = "";
                                                    string strLow = "";
                                                    string strConditionType = "";
                                                    getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                                                    #region 构造监测任务点位明细表
                                                    //构造监测任务点位明细表
                                                    TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
                                                    objMonitorTaskItem.ID = arrTaskItemIDs[k];
                                                    objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
                                                    objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                                    objMonitorTaskItem.CONDITION_ID = strConditionID;
                                                    objMonitorTaskItem.CONDITION_TYPE = strConditionType;
                                                    objMonitorTaskItem.ST_UPPER = strUp;
                                                    objMonitorTaskItem.ST_LOWER = strLow;
                                                    objMonitorTaskItem.IS_DEL = "0";
                                                    arrPointItem.Add(objMonitorTaskItem);
                                                    #endregion

                                                    #region 构造样品结果表
                                                    //构造样品结果表
                                                    string strAnalysisID = "";//分析方法ID
                                                    string strStandardID = "";//方法依据ID
                                                    string strCheckOut = ""; //检出限
                                                    getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                                    TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                                    objSampleResult.ID = arrSampleResultIDs[k];
                                                    objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                                    objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                                    objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                                    objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(drPointItem["ITEM_ID"].ToString());
                                                    objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                                    objSampleResult.STANDARD_ID = strStandardID;
                                                    objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                                    objSampleResult.QC = "0";// 默认原始样手段
                                                    objSampleResult.TASK_TYPE = "发送";
                                                    objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                                    objSampleResult.PRINTED = "0";//默认未打印
                                                    arrSampleResult.Add(objSampleResult);
                                                    #endregion

                                                    #region 构造样品执行表
                                                    //构造样品执行表
                                                    TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                                    objResultApp.ID = arrResultAppIDs[k];
                                                    objResultApp.RESULT_ID = objSampleResult.ID;
                                                    objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
                                                    objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
                                                    arrSampleResultApp.Add(objResultApp);
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion

            if (new TMisMonitorTaskLogic().SaveTrans(objTask, objTaskClient, objTaskTested, objReportVo, arrTaskPoint, arrSubTask, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
            {
                strReturn = true;
            }
        }
        return strReturn;
    }
    /// <summary>
    /// 创建原因：获取指定监测项目ID的采样仪器ID
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <param name="strItemID">监测项目ID</param>
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

    #region 委托书导出 胡方扬 2013-04-23
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../../Mis/Contract/TempFile/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        //插入委托书单号
        sheet.GetRow(2).GetCell(6).SetCellValue("No:" + dt.Rows[0]["CONTRACT_CODE"].ToString());

        sheet.GetRow(4).GetCell(2).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());
        sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["CONTACT_NAME"].ToString());
        sheet.GetRow(4).GetCell(8).SetCellValue(dt.Rows[0]["PHONE"].ToString());
        sheet.GetRow(5).GetCell(2).SetCellValue(dt.Rows[0]["CONTACT_ADDRESS"].ToString());
        sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["POST"].ToString());
        DataTable dtDict = PageBase.getDictList("RPT_WAY");
        DataTable dtSampleSource = PageBase.getDictList("SAMPLE_SOURCE");
        string strWay = "", strSampleWay = ""; ;
        if (dtDict != null)
        {
            foreach (DataRow dr in dtDict.Rows)
            {
                strWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_CODE"].ToString() == dt.Rows[0]["RPT_WAY"].ToString())
                {
                    strWay += "■ ";
                }
                else
                {
                    strWay += "□ ";
                }
            }
        }
        if (dtSampleSource != null)
        {
            foreach (DataRow dr in dtSampleSource.Rows)
            {
                strSampleWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_TEXT"].ToString() == dt.Rows[0]["SAMPLE_SOURCE"].ToString())
                {
                    strSampleWay += "■ ";
                }
                else
                {
                    strSampleWay += "□ ";
                }
            }
        }
        sheet.GetRow(5).GetCell(8).SetCellValue(strWay);
        sheet.GetRow(7).GetCell(2).SetCellValue(strSampleWay);
        sheet.GetRow(8).GetCell(2).SetCellValue(dt.Rows[0]["TEST_PURPOSE"].ToString());
        sheet.GetRow(9).GetCell(2).SetCellValue(dt.Rows[0]["PROVIDE_DATA"].ToString());
        sheet.GetRow(11).GetCell(2).SetCellValue(dt.Rows[0]["OTHER_ASKING"].ToString());
        sheet.GetRow(16).GetCell(1).SetCellValue(dt.Rows[0]["MONITOR_ACCORDING"].ToString());
        sheet.GetRow(20).GetCell(1).SetCellValue(dt.Rows[0]["REMARK2"].ToString());
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测协议书-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion

    #region 清远委托书导出 魏林 2014-02-16
    protected void btnExport_QY_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        string strWorkContent = "";

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../../Mis/Contract/TempFile/QY/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        //插入委托书单号
        sheet.GetRow(2).GetCell(6).SetCellValue("No:" + dt.Rows[0]["CONTRACT_CODE"].ToString());

        sheet.GetRow(4).GetCell(2).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());   //委托单位
        sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["CONTACT_NAME"].ToString());   //联系人  
        sheet.GetRow(4).GetCell(8).SetCellValue(dt.Rows[0]["PHONE"].ToString());          //联系电话
        sheet.GetRow(5).GetCell(2).SetCellValue(dt.Rows[0]["CONTACT_ADDRESS"].ToString()); //地址
        sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["POST"].ToString());            //邮编

        sheet.GetRow(9).GetCell(2).SetCellValue(dt.Rows[0]["TESTED_COMPANY_NAME"].ToString());   //受检单位
        sheet.GetRow(9).GetCell(4).SetCellValue(dt.Rows[0]["TESTED_PHONE"].ToString());          //联系电话
        sheet.GetRow(9).GetCell(8).SetCellValue(dt.Rows[0]["TESTED_CONTACT_ADDRESS"].ToString()); //地址
        sheet.GetRow(9).GetCell(6).SetCellValue(dt.Rows[0]["TESTED_POST"].ToString());            //邮编

        DataTable dtDict = PageBase.getDictList("RPT_WAY");
        DataTable dtSampleSource = PageBase.getDictList("SAMPLE_SOURCE");
        string strWay = "", strSampleWay = ""; ;
        if (dtDict != null)
        {
            foreach (DataRow dr in dtDict.Rows)
            {
                strWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_CODE"].ToString() == dt.Rows[0]["RPT_WAY"].ToString())
                {
                    strWay += "■ ";
                }
                else
                {
                    strWay += "□ ";
                }
            }
        }
        if (dtSampleSource != null)
        {
            foreach (DataRow dr in dtSampleSource.Rows)
            {
                strSampleWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_TEXT"].ToString() == dt.Rows[0]["SAMPLE_SOURCE"].ToString())
                {
                    strSampleWay += "■ ";
                }
                else
                {
                    strSampleWay += "□ ";
                }
            }
        }
        sheet.GetRow(5).GetCell(8).SetCellValue(strWay);               //领取方式
        sheet.GetRow(6).GetCell(2).SetCellValue(strSampleWay);         //监测类型
        sheet.GetRow(7).GetCell(2).SetCellValue(dt.Rows[0]["TEST_PURPOSE"].ToString());   //监测目的
        sheet.GetRow(8).GetCell(2).SetCellValue(dt.Rows[0]["PROVIDE_DATA"].ToString());   //提供资料
        sheet.GetRow(10).GetCell(2).SetCellValue(dt.Rows[0]["OTHER_ASKING"].ToString());  //其他要求
        sheet.GetRow(15).GetCell(1).SetCellValue(dt.Rows[0]["MONITOR_ACCORDING"].ToString());//监测依据
        sheet.GetRow(20).GetCell(1).SetCellValue(dt.Rows[0]["REMARK2"].ToString());        //备注

        string strExplain = @"1.是否有分包：□是[□电话确认；□其它：         ] □否 
  是否使用非标准方法：  □是  □否
2.监测收费参照广东省物价局粤价函[1996]64号文规定执行。委托单位到本站办公室（603室）领取《清远市非税收入缴款通知书》限期到通知书列明的银行所属任何一个网点缴监测费{0}元（{1}），到颁行缴款后应将盖有银行收讫章的广东省非税收入（电子）票据执收单位联送回给本站综合室。
3.本站在确认已缴监测费用和委托方提供了必要的监测条件后60个工作日内完成监测。";
        string strBUDGET = dt.Rows[0]["INCOME"].ToString() == "" ? "0" : dt.Rows[0]["INCOME"].ToString();
        strExplain = string.Format(strExplain, strBUDGET, DaXie(strBUDGET));
        sheet.GetRow(18).GetCell(1).SetCellValue(strExplain);

        //监测内容
        string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPES"].ToString().Split(';');
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
        //获取当前监测点信息
        TMisContractPointVo ContractPointVo = new TMisContractPointVo();
        ContractPointVo.CONTRACT_ID = task_id;
        ContractPointVo.IS_DEL = "0";
        DataTable dtPoint = new TMisContractPointLogic().SelectByTable(ContractPointVo);
        string strOutValuePoint = "", strOutValuePointItems = "";
        if (strMonitroTypeArr.Length > 0)
        {
            foreach (string strMonitor in strMonitroTypeArr)
            {
                string strMonitorName = "", strPointName = "";
                DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + strMonitor + "'");
                if (drPoint.Length > 0)
                {

                    foreach (DataRow drrPoint in drPoint)
                    {
                        string strPointNameForItems = "", strPointItems = "";
                        strMonitorName = GetMonitorName(strMonitor) + "：";
                        strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                        //获取当前点位的监测项目
                        TMisContractPointitemVo ContractPointitemVo = new TMisContractPointitemVo();
                        ContractPointitemVo.CONTRACT_POINT_ID = drrPoint["ID"].ToString();
                        DataTable dtPointItems = new TMisContractPointitemLogic().GetItemsForPoint(ContractPointitemVo);
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
        sheet.GetRow(12).GetCell(1).SetCellValue(strWorkContent);

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测协议书-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    /// <summary>
    /// 小写数字金额转换成大写人民币金额(正则表达式)
    /// Add by: weilin
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    private string DaXie(string money)
    {
        string s = double.Parse(money).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
        string d = System.Text.RegularExpressions.Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
        return System.Text.RegularExpressions.Regex.Replace(d, ".", delegate(System.Text.RegularExpressions.Match m)
        {
            return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万億兆京垓秭穰"[m.Value[0] - '-'].ToString();
        });
    }
    /// <summary>
    /// 小写数字金额转换成大写人民币金额（数组）
    /// Add by: weilin
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public string chang(string money)
    {
        //将小写金额转换成大写金额           
        double MyNumber = Convert.ToDouble(money);
        String[] MyScale = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
        String[] MyBase = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        String M = "";
        bool isPoint = false;
        if (money.IndexOf(".") != -1)
        {
            money = money.Remove(money.IndexOf("."), 1);
            isPoint = true;
        }
        for (int i = money.Length; i > 0; i--)
        {
            int MyData = Convert.ToInt16(money[money.Length - i].ToString());
            M += MyBase[MyData];
            if (isPoint == true)
            {
                M += MyScale[i - 1];
            }
            else
            {
                M += MyScale[i + 1];
            }
        }
        return M;
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
    #endregion

    #region 清远委托书费用明细导出 魏林 2014-02-25
    protected void btnExport_QY_FEE_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtAtt = new DataTable();
        DataRow[] dr;
        TMisContractVo objItems = new TMisContractVo();
        int r = 1;
        string strMonitorTypes = "";
        string[] strMonitroType;
        string strTEST_FEE = "";
        string strATT_FEE = "";
        string strBUDGET = "";
        double dFeeTemp = 0;

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetContractFreeData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../../Mis/Contract/TempFile/QY/ContractInforFee.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        IRow iRow;
        ICell iCell;

        if (dt.Rows.Count > 0)
        {
            strTEST_FEE = dt.Rows[0]["TEST_FEE"].ToString() == "" ? "0" : dt.Rows[0]["TEST_FEE"].ToString();
            strATT_FEE = dt.Rows[0]["ATT_FEE"].ToString() == "" ? "0" : dt.Rows[0]["ATT_FEE"].ToString();
            strBUDGET = dt.Rows[0]["BUDGET"].ToString() == "" ? "0" : dt.Rows[0]["BUDGET"].ToString();
            strMonitorTypes = dt.Rows[0]["TEST_TYPES"].ToString();
            sheet.GetRow(0).GetCell(0).SetCellValue(dt.Rows[0]["PROJECT_NAME"].ToString() + "经费计算表");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r++;
                iRow = sheet.CreateRow(r);
                iRow.CreateCell(0).SetCellValue(dt.Rows[i]["MONITOR_TYPE_NAME"].ToString());
                iRow.CreateCell(1).SetCellValue(dt.Rows[i]["ITEM_NAME"].ToString());
                iRow.CreateCell(2).SetCellValue(dt.Rows[i]["PRETREATMENT_FEE"].ToString());
                iRow.CreateCell(3).SetCellValue(dt.Rows[i]["TEST_ANSY_FEE"].ToString());
                iRow.CreateCell(4).SetCellValue(dt.Rows[i]["TEST_NUM"].ToString());
                iRow.CreateCell(5).SetCellValue(dt.Rows[i]["FREQ"].ToString());
                iRow.CreateCell(6).SetCellValue(dt.Rows[i]["TEST_POWER_PRICE"].ToString());
                iRow.CreateCell(7).SetCellValue(dt.Rows[i]["TEST_PRICE"].ToString());
                iRow.CreateCell(8).SetCellValue(dt.Rows[i]["TEST_POINT_NUM"].ToString());
                iRow.CreateCell(9).SetCellValue(dt.Rows[i]["FEE_COUNT"].ToString());
            }
            r += 2;
            iRow = sheet.CreateRow(r);
            iRow.CreateCell(0).SetCellValue(dt.Rows[0]["PROJECT_NAME"].ToString() + "经费汇总");
            iRow.CreateCell(9).SetCellValue(strTEST_FEE);

            strMonitroType = strMonitorTypes.Split(';');
            for (int j = 0; j < strMonitroType.Length; j++)
            {
                dr = dt.Select("MONITOR_ID='" + strMonitroType[j].ToString() + "'");
                if (dr.Length > 0)
                {
                    r++;
                    dFeeTemp = 0;
                    for (int k = 0; k < dr.Length; k++)
                    {
                        dFeeTemp += double.Parse(dr[k]["FEE_COUNT"].ToString() == "" ? "0" : dr[k]["FEE_COUNT"].ToString());
                    }
                    iRow = sheet.CreateRow(r);
                    iRow.CreateCell(0).SetCellValue(dr[0]["MONITOR_TYPE_NAME"].ToString() + "监测分析费");
                    iRow.CreateCell(9).SetCellValue(dFeeTemp.ToString());
                }
            }
            dtAtt = new TMisContractLogic().GetContractAttFeeData(task_id);
            for (int i = 0; i < dtAtt.Rows.Count; i++)
            {
                r++;
                iRow = sheet.CreateRow(r);
                iRow.CreateCell(0).SetCellValue(dtAtt.Rows[i]["ATT_FEE_ITEM"].ToString());
                iRow.CreateCell(9).SetCellValue(dtAtt.Rows[i]["FEE"].ToString());
            }
            r++;
            iRow = sheet.CreateRow(r);
            iRow.CreateCell(0).SetCellValue("合计：");
            iRow.CreateCell(9).SetCellValue(strBUDGET);
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
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测费用明细-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion
}
