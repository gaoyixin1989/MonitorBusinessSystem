using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Data;
using System.Configuration;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.View;
using System.Reflection;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Collections;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
/// <summary>
/// 功能描述：临时性任务预约新增   
/// 创建时间：2013-06-06
/// 创建人 ： 胡方扬
/// 修改时间：
/// 修改人：
/// 修改内容：
/// </summary>
public partial class Channels_Mis_MonitoringPlan_TempTaskPlanAdd : PageBase
{
    public string strPlanID="",strContractId="",strProjectName = "", strContractYear = "", strContractTypeId = "", strDate = "", strCompanyId = "", strCompanyFrimId = "";
    public string strClientCompanyId = "", strTestCompanyId = "", strTest_Type = "", strTaskStatus = "", strTaskType = "", strAskingDate = "", strQC_Status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParamer();

        if (!String.IsNullOrEmpty(Request.Params["action"]) && Request.Params["action"].ToString() == "doPlanTask")
        {
            Response.Write(doPlanTask());
            Response.End();
        }

    }

    /// <summary>
    /// 预约办理
    /// </summary>
    /// <param name="strPlanID">预约表ID</param>
    protected string doPlanTask()
    {
        string strReturn = "";
        string strTaskFreqType = "0";
        string strCodeRule = "";
        strTaskFreqType = System.Configuration.ConfigurationManager.AppSettings["TaskFreqType"].ToString();
        //预约表对象
        TMisContractPlanVo objContractPlan = new TMisContractPlanLogic().Details(strPlanID);
        TMisContractVo objContract = new TMisContractVo();
        objContract.CONTRACT_TYPE = strContractTypeId;
        if (objContractPlan != null)
        {
            //获取委托书信息

            #region 构造监测任务对象
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.ID = GetSerialNumber("t_mis_monitor_taskId");
            objTask.PLAN_ID = strPlanID;
            objTask.PROJECT_NAME = strProjectName;
            objTask.CONTRACT_TYPE = strContractTypeId;
            objTask.TEST_TYPE = strTest_Type;
            objTask.CREATOR_ID = LogInfo.UserInfo.ID;
            objTask.CREATE_DATE = DateTime.Now.ToString();
            objTask.TASK_STATUS = "01";
            objTask.COMFIRM_STATUS = "0";
            objTask.SAMPLE_SOURCE = "抽样";
            objTask.TASK_TYPE = strTaskType;
            objTask.CONTRACT_YEAR = strContractYear;
            objTask.ASKING_DATE = strAskingDate;
            //string strQcSetting = ConfigurationManager.AppSettings["QCsetting"].ToString();
            //if (!String.IsNullOrEmpty(strQcSetting))
            //{
            //    if (strQcSetting == "1")
            //    {
            //        objTask.QC_STATUS = "1";
            //    }
            //    else
            //    {
            //        objTask.QC_STATUS = "2";
            //    }
            //}

            //生成任务编号
            TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            objSerialTask.SERIAL_TYPE = "4";
            //潘德军 2013-12-23  任务单号可改，且初始不生成
            objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask,objContract);
            //objTask.TICKET_NUM = "未编号";
            //生成委托书单号
            objSerialTask.SERIAL_TYPE = "1";
            strCodeRule = CreateBaseDefineCode(objSerialTask, objContract);
            objTask.CONTRACT_CODE = strCodeRule;

            #region 构造监测任务委托企业信息
            //插入委托企业信息
            if (!String.IsNullOrEmpty(strCompanyId))
            {
                strClientCompanyId = InsertContractCompanyInfo(objTask.ID,strCompanyId);
                objTask.CLIENT_COMPANY_ID = strClientCompanyId;
            }
            //插入受检企业信息
            if (!String.IsNullOrEmpty(strCompanyFrimId))
            {
                strTestCompanyId = InsertContractCompanyInfo(objTask.ID,strCompanyFrimId);
                objTask.TESTED_COMPANY_ID = strTestCompanyId;
            }
            #endregion
            #endregion

            #region 监测报告 胡方扬 2013-04-23 Modify  将报告记录初始化生成数据移到委托书办理完毕后就生成
            TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
            objReportVo.ID = GetSerialNumber("t_mis_monitor_report_id");
            //生成报告编号  胡方扬 2013-04-24
            //TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            //objSerial.SERIAL_TYPE = "3";

            //objReportVo.REPORT_CODE = PageBase.CreateBaseDefineCode(objSerial,objContract);
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
            #region 注释
            //#region 监测子任务
            //if (dtTestType.Rows.Count > 0)
            //{
            //    string strSubTaskIDs = GetSerialNumberList("t_mis_monitor_subtaskId", dtTestType.Rows.Count);
            //    string[] arrSubTaskIDs = strSubTaskIDs.Split(',');
            //    for (int i = 0; i < dtTestType.Rows.Count; i++)
            //    {
            //        string str = dtTestType.Rows[i]["MONITOR_ID"].ToString();//监测类别
            //        if (str.Length > 0)
            //        {
            //            #region 监测子任务
            //            //监测子任务
            //            TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
            //            string strSampleManagerID = "";//采样负责人ID
            //            string strSampleID = "";//采样协同人ID串
            //            //GetSamplingMan(str, objContract.ID, ref strSampleManagerID, ref strSampleID);
            //            objSubtask.ID = arrSubTaskIDs[i];
            //            objSubtask.TASK_ID = objTask.ID;
            //            objSubtask.MONITOR_ID = str;

            //            if (objContract.PROJECT_ID != "")
            //            {
            //                objSubtask.SAMPLING_MANAGER_ID = objContract.PROJECT_ID;
            //            }
            //            else
            //            {
            //                objSubtask.SAMPLING_MANAGER_ID = strSampleManagerID;
            //            }
            //            objSubtask.SAMPLING_ID = strSampleID;
            //            objSubtask.TASK_TYPE = "发送";
            //            objSubtask.TASK_STATUS = "01";
            //            arrSubTask.Add(objSubtask);
            //            #endregion

            //            #region 按类别分点位
            //            //按类别分点位
            //            DataRow[] dtTypePoint = dtPoint.Select("MONITOR_ID=" + str);
            //            if (dtTypePoint.Length > 0)
            //            {
            //                string strTaskPointIDs = GetSerialNumberList("t_mis_monitor_taskpointId", dtTypePoint.Length);
            //                string[] arrTaskPointIDs = strTaskPointIDs.Split(',');
            //                string strSampleIDs = GetSerialNumberList("MonitorSampleId", dtTypePoint.Length);
            //                string[] arrSampleIDs = strSampleIDs.Split(',');

            //                for (int j = 0; j < dtTypePoint.Length; j++)
            //                {
            //                    DataRow drPoint = dtTypePoint[j];
            //                    #region 监测点位
            //                    // 监测点位 
            //                    TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
            //                    objTaskPoint.ID = arrTaskPointIDs[j];
            //                    objTaskPoint.TASK_ID = objTask.ID;
            //                    objTaskPoint.SUBTASK_ID = objSubtask.ID;
            //                    objTaskPoint.POINT_ID = drPoint["POINT_ID"].ToString();
            //                    objTaskPoint.MONITOR_ID = str;
            //                    //objTaskPoint.COMPANY_ID = objTaskTested.ID;
            //                    objTaskPoint.CONTRACT_POINT_ID = drPoint["ID"].ToString();
            //                    objTaskPoint.POINT_NAME = drPoint["POINT_NAME"].ToString();
            //                    objTaskPoint.DYNAMIC_ATTRIBUTE_ID = drPoint["DYNAMIC_ATTRIBUTE_ID"].ToString();
            //                    objTaskPoint.ADDRESS = drPoint["ADDRESS"].ToString();
            //                    objTaskPoint.LONGITUDE = drPoint["LONGITUDE"].ToString();
            //                    objTaskPoint.LATITUDE = drPoint["LATITUDE"].ToString();
            //                    objTaskPoint.FREQ = drPoint["FREQ"].ToString();
            //                    objTaskPoint.DESCRIPTION = drPoint["DESCRIPTION"].ToString();
            //                    objTaskPoint.NATIONAL_ST_CONDITION_ID = drPoint["NATIONAL_ST_CONDITION_ID"].ToString();
            //                    objTaskPoint.INDUSTRY_ST_CONDITION_ID = drPoint["INDUSTRY_ST_CONDITION_ID"].ToString();
            //                    objTaskPoint.LOCAL_ST_CONDITION_ID = drPoint["LOCAL_ST_CONDITION_ID"].ToString();
            //                    objTaskPoint.IS_DEL = "0";
            //                    objTaskPoint.NUM = drPoint["NUM"].ToString();
            //                    objTaskPoint.CREATE_DATE = DateTime.Now.ToString();
            //                    arrTaskPoint.Add(objTaskPoint);
            //                    #endregion

            //                    #region 样品
            //                    //样品 与点位对应
            //                    TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
            //                    objSampleInfo.ID = arrSampleIDs[j];
            //                    objSampleInfo.SUBTASK_ID = objSubtask.ID;
            //                    objSampleInfo.POINT_ID = objTaskPoint.ID;
            //                    objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
            //                    objSampleInfo.QC_TYPE = "0";//默认原始样
            //                    objSampleInfo.NOSAMPLE = "0";//默认未采样
            //                    arrSample.Add(objSampleInfo);
            //                    #endregion

            //                    //点位采用的标准条件项ID
            //                    string strConditionID = "";
            //                    if (!string.IsNullOrEmpty(objTaskPoint.NATIONAL_ST_CONDITION_ID))
            //                    {
            //                        strConditionID = objTaskPoint.NATIONAL_ST_CONDITION_ID;
            //                    }
            //                    if (!string.IsNullOrEmpty(objTaskPoint.LOCAL_ST_CONDITION_ID))
            //                    {
            //                        strConditionID = objTaskPoint.LOCAL_ST_CONDITION_ID;
            //                    }
            //                    if (!string.IsNullOrEmpty(objTaskPoint.INDUSTRY_ST_CONDITION_ID))
            //                    {
            //                        strConditionID = objTaskPoint.INDUSTRY_ST_CONDITION_ID;
            //                    }

            //                    //预约项目明细
            //                    DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
            //                    if (dtPointItem.Length > 0)
            //                    {
            //                        string strTaskItemIDs = GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
            //                        string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
            //                        string strSampleResultIDs = GetSerialNumberList("MonitorResultId", dtPointItem.Length);
            //                        string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
            //                        string strResultAppIDs = GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
            //                        string[] arrResultAppIDs = strResultAppIDs.Split(',');

            //                        for (int k = 0; k < dtPointItem.Length; k++)
            //                        {
            //                            DataRow drPointItem = dtPointItem[k];
            //                            //项目采用的标准上限、下限
            //                            string strUp = "";
            //                            string strLow = "";
            //                            string strConditionType = "";
            //                            getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
            //                            #region 构造监测任务点位明细表
            //                            //构造监测任务点位明细表
            //                            TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
            //                            objMonitorTaskItem.ID = arrTaskItemIDs[k];
            //                            objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
            //                            objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
            //                            objMonitorTaskItem.CONDITION_ID = strConditionID;
            //                            objMonitorTaskItem.CONDITION_TYPE = strConditionType;
            //                            objMonitorTaskItem.ST_UPPER = strUp;
            //                            objMonitorTaskItem.ST_LOWER = strLow;
            //                            objMonitorTaskItem.IS_DEL = "0";
            //                            arrPointItem.Add(objMonitorTaskItem);
            //                            #endregion

            //                            #region 构造样品结果表
            //                            //构造样品结果表
            //                            string strAnalysisID = "";//分析方法ID
            //                            string strStandardID = "";//方法依据ID
            //                            getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID);
            //                            TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
            //                            objSampleResult.ID = arrSampleResultIDs[k];
            //                            objSampleResult.SAMPLE_ID = objSampleInfo.ID;
            //                            objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
            //                            objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
            //                            objSampleResult.SAMPLING_INSTRUMENT =GetItemSamplingInstrumentID( drPointItem["ITEM_ID"].ToString());
            //                            objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
            //                            objSampleResult.STANDARD_ID = strStandardID;
            //                            objSampleResult.QC = "0";// 默认原始样手段
            //                            objSampleResult.TASK_TYPE = "发送";
            //                            objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
            //                            objSampleResult.PRINTED = "0";//默认未打印
            //                            arrSampleResult.Add(objSampleResult);
            //                            #endregion

            //                            #region 构造样品执行表
            //                            //构造样品执行表
            //                            TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
            //                            objResultApp.ID = arrResultAppIDs[k];
            //                            objResultApp.RESULT_ID = objSampleResult.ID;
            //                            objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
            //                            objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
            //                            arrSampleResultApp.Add(objResultApp);
            //                            #endregion
            //                        }
            //                    }
            //                }
            //            }
            //            #endregion
            //        }
            //    }
            //}
            //#endregion
            #endregion
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
                                //objTaskPoint.COMPANY_ID = objTaskTested.ID;
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
                                int intFreq = 1, intSampleDay = 1;
                                if (drPoint["SAMPLE_FREQ"].ToString().Length > 0)
                                {
                                    intFreq = int.Parse(drPoint["SAMPLE_FREQ"].ToString());
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
                                    for (int r = 0; r < intSampleDay; r++)
                                    {
                                        for (int s = 0; s < intFreq; s++)
                                        {
                                            #region 样品
                                            //样品 与点位对应
                                            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                            //objSampleInfo.ID = arrSampleIDs[j];
                                            objSampleInfo.ID = new PageBase().GetSerialNumberList("MonitorSampleId", 1);
                                            objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                            objSampleInfo.POINT_ID = objTaskPoint.ID;
                                            //objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                            objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME + " 第" + (r + 1).ToString() + "天" + " 第" + (s + 1).ToString() + "次";
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

            if (new TMisMonitorTaskLogic().SaveTrans(objTask,objReportVo, arrTaskPoint, arrSubTask, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
            {
                strReturn = "1";
            }
        }
        return strReturn;
    }

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

    private string InsertContractCompanyInfo(string strReqTaskId,string strReqCompanyId)
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisMonitorTaskCompanyVo objTmc = new TMisMonitorTaskCompanyVo();
        //Update By SSZ 将基础资料企业信息复制到委托书企业信息
        //基础企业资料信息
        TBaseCompanyInfoVo objCompanyInfo = new TBaseCompanyInfoLogic().Details(strReqCompanyId);
        //将相同字段的数据进行复制
        CopyObject(objCompanyInfo, objTmc);
        objTmc.TASK_ID = strReqTaskId;
        objTmc.COMPANY_ID = strReqCompanyId;
        objTmc.IS_DEL = "0";

        objTmc.ID = i3.View.PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
        if (new TMisMonitorTaskCompanyLogic().Create(objTmc))
        {
            result = objTmc.ID.ToString();
            string strMessage = LogInfo.UserInfo.USER_NAME + "添加任务企业" + objTmc.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractCompanyInfo, "", strMessage);
        }

        return result;
    }
    public void GetRequestParamer() {
        if (!String.IsNullOrEmpty(Request.Params["strCompanyId"]))
        {
            strCompanyId = Request.Params["strCompanyId"].Trim();
        }

        if (!String.IsNullOrEmpty(Request.Params["strCompanyFrimId"]))
        {
            strCompanyFrimId = Request.Params["strCompanyFrimId"].Trim();
        }

        if (!String.IsNullOrEmpty(Request.Params["strPlanID"]))
        {
            strPlanID = Request.Params["strPlanID"].Trim();
        }

        if (!String.IsNullOrEmpty(Request.Params["strProjectName"]))
        {
            strProjectName = Request.Params["strProjectName"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractTypeId"]))
        {
            strContractTypeId = Request.Params["strContractTypeId"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strContractYear"]))
        {
            strContractYear = Request.Params["strContractYear"].Trim();
        }

        if (!String.IsNullOrEmpty(Request.Params["strDate"]))
        {
            strDate = Request.Params["strDate"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strTest_Type"]))
        {
            strTest_Type = Request.Params["strTest_Type"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strTaskStatus"]))
        {
            strTaskStatus = Request.Params["strTaskStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strTaskType"]))
        {
            strTaskType = Request.Params["strTaskType"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strAskingDate"]))
        {
            strAskingDate = Request.Params["strAskingDate"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["strQC_Status"]))
        {
            strQC_Status = Request.Params["strQC_Status"].Trim();
        }
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
    
}