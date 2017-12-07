<%@ WebService Language="C#" Class="WfApp" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Sys.Duty;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WfApp  : System.Web.Services.WebService {

    [WebMethod]
    public string GetDataLst(string strYear, string strContractTypeID, string strTICKET_NUM, string strProjectName, int intPageIndex, int intPageSize)
    {
        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();

        objTask.SORT_FIELD = "ID";
        objTask.SORT_TYPE = "desc";
        objTask.CONTRACT_YEAR = strYear;
        objTask.CONTRACT_TYPE = strContractTypeID;
        objTask.TICKET_NUM = strTICKET_NUM;
        objTask.PROJECT_NAME = strProjectName;

        string strJson = "";
        int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable(objTask, intPageIndex, intPageSize);
        delCol_fromDataTable(ref dt);

        strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string GetContractType()
    {
        DataTable dt = new TSysDictLogic().SelectByTable(new TSysDictVo { DICT_TYPE = "Contract_Type" });

        string strJson = "[";
        foreach (DataRow dr in dt.Rows)
        {
            strJson += "{";
            strJson += "'ContractTypeID':'" + dr["DICT_CODE"].ToString() + "',";
            strJson += "'ContractTypeName':'" + dr["DICT_TEXT"].ToString() + "'";
            strJson += "},";
        }
        strJson = strJson.TrimEnd(',');
        strJson += "]";

        return strJson;
    }

    [WebMethod]
    public string GetDataInfo(string strID)
    {
        string strReturn = "";
        strReturn += "{'type':'委托书执行情况','info':";
        strReturn += GetConStep(strID);
        strReturn += "},";
        strReturn += "{'type':'报告执行情况','info':";
        strReturn += GetRptStep(strID);
        strReturn += "}";

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo { TASK_ID = strID });
        foreach (DataRow dr in dt.Rows)
        {
            string strMonitorID = dr["MONITOR_ID"].ToString();
            string strResultJson = DrawResultStep(strID, strMonitorID);
            string strMName = new TBaseMonitorTypeInfoLogic().Details(strMonitorID).MONITOR_TYPE_NAME;
            if (strResultJson.Length > 0)
            {
                strReturn += ",";
                strReturn += "{'type':'监测执行情况','subType':'" + strMName + "','info':";
                
                strResultJson = "[" + strResultJson + "]";
                strReturn += strResultJson;
                strReturn += "}";
            }
        }

        strReturn = "[" + strReturn + "]";

        return strReturn;
    }

    private string GetConStep(string strID)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strID);
        string strContractId = objTask.CONTRACT_ID;

        string strWf_ID = "WT_FLOW";
        //流程类别
        switch (objTask.CONTRACT_TYPE)
        {
            //自送样流程
            case "04":
                strWf_ID = "SAMPLE_WT";
                break;
            //验收流程
            case "05":
                strWf_ID = "WF_A";
                break;
            //常规委托书流程
            default:
                strWf_ID = "WT_FLOW";
                break;
        }

        return GetWfStep(strContractId, strWf_ID);
    }

    private string GetRptStep(string strID)
    {
        string strWf_ID = "RPT";

        return GetWfStep(strID, strWf_ID);
    }

    private string GetWfStep(string strID, string strWf_ID)
    {
       //构造实例明细对象
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailVo();
        objTaskDetail.WF_ID = strWf_ID;

        //获取业务流程所有环节信息
        DataTable dt = new TWfInstTaskDetailLogic().GetWFDetailByBusinessInfo(objTaskDetail, "task_id", strID);
        string strControlID = "";
        if (dt.Rows.Count > 0)
        {
            strControlID = dt.Rows[0]["CONTROL_ID"].ToString();
        }
        else
            return "[]";

        TWfInstControlVo control = new TWfInstControlLogic().Details(strControlID);
        List<TWfSettingTaskVo> taskList = new TWfSettingTaskLogic().SelectByObjectListForSetp(new TWfSettingTaskVo() { WF_ID = control.WF_ID });
        List<TWfInstTaskDetailVo> InstTaskList = new TWfInstTaskDetailLogic().SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strControlID, SORT_FIELD = TWfInstTaskDetailVo.INST_TASK_STARTTIME_FIELD, SORT_TYPE = i3.ValueObject.ConstValues.SortType.ASC }, 0, 200);

        string strReturn = CreatStepJson(taskList, InstTaskList);

        return strReturn;
    }

   

    private string CreatStepJson(List<TWfSettingTaskVo> taskList, List<TWfInstTaskDetailVo> InstTaskList)
    {
        string strModel = "'环节名称':'{0}','环节状态':'{1}','处理者':'{2}'";

        //当任务未进入工作流时直接返回 
        if (InstTaskList.Count <= 0)
        {
            return "[]";
        }
        
        //获取最后一在用的环节 
        TWfInstTaskDetailVo instTaskLast = InstTaskList[InstTaskList.Count - 1];
        List<TWfInstTaskDetailVo> instHaveStep = GetTrueInstStep(InstTaskList);
        int iStepOrder = 0;
        string strHtmlString = "";
        foreach (TWfSettingTaskVo tsdv in taskList)
        {
            string strHeader = strModel;

            if (instTaskLast.WF_TASK_ID == tsdv.WF_TASK_ID && instTaskLast.INST_TASK_STATE == "2A")
            {
                iStepOrder = int.Parse(tsdv.TASK_ORDER);
            }

            //增加信号量，如果一直没有匹配上，则说明需要现实配置信息，环节配置信息也需要显示
            bool bIsHaveInstTask = false;

            foreach (TWfInstTaskDetailVo ttdv in instHaveStep)
            {
                if (ttdv.WF_TASK_ID == tsdv.WF_TASK_ID)
                {
                    bIsHaveInstTask = true;

                    strHeader = string.Format(strHeader,
                            ttdv.INST_TASK_CAPTION,
                           new i3.View.PageBaseForWF().GetStepStateName(ttdv.INST_TASK_STATE, ttdv.INST_TASK_DEAL_STATE),
                           GetUserRealName(ttdv.REAL_USER)
                           );
                    strHeader = "{" + strHeader + "}";

                    break;
                }
            }
            if (!bIsHaveInstTask)
            {
                //只显示配置本身信息，
                strHeader = "";
            }

            if (strHeader.Length > 0)
                strHtmlString += (strHtmlString.Length > 0 ? "," : "") + strHeader;

        }
        strHtmlString = "[" + strHtmlString + "]";
        
        return strHtmlString;
    }

    /// <summary>
    /// 排序，取出时间最靠后的所有环节，组成单一数据
    /// </summary>
    /// <param name="InstTaskList"></param>
    /// <returns></returns>
    private List<TWfInstTaskDetailVo> GetTrueInstStep(List<TWfInstTaskDetailVo> InstTaskList)
    {
        List<TWfInstTaskDetailVo> list = new List<TWfInstTaskDetailVo>();
        foreach (TWfInstTaskDetailVo temp in InstTaskList)
        {
            if (list.Contains(temp))
                continue;
            //如果没有，则开始比对
            TWfInstTaskDetailVo baidu = new TWfInstTaskDetailVo();
            bool bIsHave = false;
            bool bIsExsit = false;
            foreach (TWfInstTaskDetailVo google in list)
            {
                if (google.WF_TASK_ID == temp.WF_TASK_ID)
                {
                    bIsExsit = true;
                    if (DateTime.Parse(google.INST_TASK_STARTTIME) < DateTime.Parse(temp.INST_TASK_STARTTIME))
                    {
                        bIsHave = true;
                        baidu = temp;
                    }
                }
            }
            if (!bIsExsit)
                list.Add(temp);
            else if (bIsHave)
            {
                foreach (TWfInstTaskDetailVo tt in list)
                {
                    if (tt.WF_TASK_ID == temp.WF_TASK_ID && tt.ID != temp.ID)
                    {
                        list.Remove(tt);
                        list.Add(baidu);
                        break;
                    }
                }
            }
        }

        return list;
    }

    private void delCol_fromDataTable(ref DataTable dt)
    {
        dt.Columns.Remove("CONTRACT_ID");
        dt.Columns.Remove("PLAN_ID");
        dt.Columns.Remove("CONTRACT_CODE");
        dt.Columns.Remove("CONTRACT_YEAR");
        dt.Columns.Remove("CONTRACT_TYPE");
        dt.Columns.Remove("TEST_TYPE");
        dt.Columns.Remove("TEST_PURPOSE");
        dt.Columns.Remove("CLIENT_COMPANY_ID");
        dt.Columns.Remove("TESTED_COMPANY_ID");
        dt.Columns.Remove("CONSIGN_DATE");
        dt.Columns.Remove("ASKING_DATE");
        //dt.Columns.Remove("FINISH_DATE");
        dt.Columns.Remove("SAMPLE_SOURCE");
        dt.Columns.Remove("CONTACT_ID");
        dt.Columns.Remove("MANAGER_ID");
        dt.Columns.Remove("CREATOR_ID");
        dt.Columns.Remove("PROJECT_ID");
        dt.Columns.Remove("CREATE_DATE");
        dt.Columns.Remove("STATE");
        dt.Columns.Remove("TASK_STATUS");
        dt.Columns.Remove("QC_STATUS");
        dt.Columns.Remove("ALLQC_STATUS");
        dt.Columns.Remove("TASK_TYPE");
        dt.Columns.Remove("SEND_STATUS");
        dt.Columns.Remove("COMFIRM_STATUS");
        dt.Columns.Remove("REMARK1");
        dt.Columns.Remove("REMARK2");
        dt.Columns.Remove("REMARK3");
        dt.Columns.Remove("REMARK4");
        dt.Columns.Remove("REMARK5");
    }


    protected string DrawResultStep(string strId,string strMonitorId)
    {
       //定义已处理 
        string strDivHas = "'环节名称':'{0}','环节状态':'已处理','处理者':'{1}'";
        //定义待处理 
        string strDivWait = "'环节名称':'{0}','环节状态':'待处理','处理者':'{1}'";

        //任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strId);
        //无现场监测子任务信息
        TMisMonitorSubtaskVo objNoSampleSubTask = new TMisMonitorSubtaskLogic().GetNoSampleSubTaskInfo(strId, strMonitorId);
        //现场子任务
        TMisMonitorSubtaskVo objSampleSubTask = new TMisMonitorSubtaskLogic().GetSampleSubTaskInfo(strId, strMonitorId);
        //监测子任务审核表 T_MIS_MONITOR_SUBTASK_APP
        TMisMonitorSubtaskAppVo objSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(new TMisMonitorSubtaskAppVo()
        {
            SUBTASK_ID = objNoSampleSubTask.ID,
        }
        );
        //监测子任务审核表 T_MIS_MONITOR_SUBTASK_APP
        TMisMonitorSubtaskAppVo objSampleSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(new TMisMonitorSubtaskAppVo()
        {
            SUBTASK_ID = objSampleSubTask.ID,
        }
        );
        //样品信息
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(new TMisMonitorSampleInfoVo()
        {
            SUBTASK_ID = objNoSampleSubTask.ID,
            QC_TYPE = "0"
        });
        //样品结果信息
        DataTable dtResult = new TMisMonitorResultLogic().SelectResultAndAppWithSubtaskID(objNoSampleSubTask.ID);
        //委托书对象
        TMisContractVo objContract = new TMisContractLogic().Details(objTask.CONTRACT_ID);

        string strStepJson = "";
        if (objTask.SAMPLE_SOURCE != "送样")//是否送样
        {
            string strShow01Name = "采样任务下达";
            string strShow02Name = "采样任务分配";

            //现场复核人
            string strSampleChecker = GetDefaultOrFirstDutyUser("sample_result_check", strMonitorId);
            //现场审核人
            string strSampleQcChecker = GetDefaultOrFirstDutyUser("sample_result_qccheck", strMonitorId);

            string strSteps = "";
            string strEndSteps = "";

            GetStepStr(objTask, objNoSampleSubTask, objSampleSubTask, ref  strSteps, ref  strEndSteps);

            if (strSteps.Contains(",采样预约,"))
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, strShow01Name, GetUserRealName(objTask.CREATOR_ID)) + "}";
            }
            if (strSteps.Contains(",预约办理,"))
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, strShow02Name, GetUserRealName(objSubTaskApp.SAMPLE_ASSIGN_ID)) + "}";
            }
            if (strSteps.Contains(",采样前质控,"))//有 采样前质控
            {
                if (objTask.QC_STATUS == "9")
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivHas, "采样前质控", GetUserRealName(objSubTaskApp.QC_USER_ID)) + "}";
                }
            }
            if (strSteps.Contains(",采样,"))//采样
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, "采样", GetUserRealName(objNoSampleSubTask.SAMPLING_MANAGER_ID)) + "}";
            }
            if (strSteps.Contains(",现场审核,"))//现场审核
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, "现场审核", GetUserRealName(objSampleSubTaskApp.SAMPLING_QC_CHECK)) + "}";
            }
            if (strSteps.Contains(",采样后质控,"))//采样后质控,郑州特有 
            {
                if (objTask.QC_STATUS == "9")//
                {
                    //strStepJson += strStepJson.Length > 0 ? "," : "";
                    //strStepJson += "{" + string.Format(strDivHas, "采样后质控", GetUserRealName(objSubTaskApp.SAMPLING_END_QC)) + "}";
                }
            }
            if (strSteps.Contains(",样品交接,"))
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, "样品交接", GetUserRealName(objNoSampleSubTask.SAMPLE_ACCESS_ID)) + "}";
            }

            switch (strEndSteps)
            {
                case ",采样预约,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShow01Name, "") + "}";
                    break;
                case ",预约办理,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShow02Name, "") + "}";
                    break;
                case ",采样前质控,"://郑州特有 
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "采样前质控", "") + "}";
                    break;
                case ",采样,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "采样", GetUserRealName(objNoSampleSubTask.SAMPLING_MANAGER_ID)) + "}";
                    break;
                case ",现场复核,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "现场复核", "") + "}";
                    break;
                case ",现场审核,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "现场审核", "") + "}";
                    break;
                case ",采样后质控,":
                    //strStepJson += strStepJson.Length > 0 ? "," : "";
                    //strStepJson += "{" + string.Format(strDivWait, "采样后质控", "") + "}";
                    break;
                case ",样品交接,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "样品交接", "") + "}";
                    break;
                case ",实验室环节,":
                    AddResultJson(ref strStepJson,strMonitorId, dtResult, objSubTaskApp, strDivWait, strDivHas, false);
                    break;
                case ",报告,":
                    AddResultJson(ref strStepJson, strMonitorId, dtResult, objSubTaskApp, strDivWait, strDivHas, true);
                    break;
            }
        }
        else
        {
            string strShow01Name = "自送样任务下达";

            string strSteps = "";
            string strEndSteps = "";

            GetSendSampleStepStr(objTask, objNoSampleSubTask, objSampleSubTask, ref  strSteps, ref  strEndSteps);

            if (strSteps.Contains(",自送样预约,"))
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, strShow01Name, GetUserRealName(objTask.CREATOR_ID)) + "}";
            }
            if (strSteps.Contains(",采样后质控,"))//采样后质控
            {
                if (objTask.QC_STATUS == "9")//郑州特有 
                {
                    //strStepJson += strStepJson.Length > 0 ? "," : "";
                    //strStepJson += "{" + string.Format(strDivHas, "采样后质控", GetUserRealName(objSubTaskApp.SAMPLING_END_QC)) + "}";
                }
            }
            if (strSteps.Contains(",样品交接,"))
            {
                strStepJson += strStepJson.Length > 0 ? "," : "";
                strStepJson += "{" + string.Format(strDivHas, "样品交接", GetUserRealName(objNoSampleSubTask.SAMPLE_ACCESS_ID)) + "}";
            }

            switch (strEndSteps)
            {
                case ",自送样预约,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShow01Name,"") + "}";
                    break;
                case ",采样后质控,":
                    //strStepJson += strStepJson.Length > 0 ? "," : "";
                    //strStepJson += "{" + string.Format(strDivWait, "采样后质控", "") + "}";
                    break;
                case ",样品交接,":
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, "样品交接", "") + "}";
                    break;
                case ",实验室环节,":
                    AddResultJson(ref strStepJson, strMonitorId, dtResult, objSubTaskApp, strDivWait, strDivHas, false);
                    break;
                case ",报告,":
                    AddResultJson(ref strStepJson, strMonitorId, dtResult, objSubTaskApp, strDivWait, strDivHas, true);
                    break;
            }
        }

        return strStepJson;
    }

    private void AddResultJson(ref string strStepJson,string strMonitorID, DataTable dtResult, TMisMonitorSubtaskAppVo objSubTaskApp, string strDivWait, string strDivHas, bool isfinish)
    {
        //分析任务分配人
        string strAnalyseSender = GetDefaultOrFirstDutyUser("duty_other_analyse", strMonitorID);
        //校核人
        string strAnalyseChecker = GetUserRealName(objSubTaskApp.RESULT_AUDIT);
        //复核人
        string strAnalyseAgainChecker = GetUserRealName(objSubTaskApp.RESULT_CHECK);
        //质量负责人审核 审核人
        string strQcChecker = GetUserRealName(objSubTaskApp.RESULT_QC_CHECK);
        //技术负责人审核 审核人
        string strTechChecker = GetUserRealName(objSubTaskApp.RESULT_QC_CHECK);

        //分析任务分配
        DataRow[] dr1 = dtResult.Select(" RESULT_STATUS='01'");//过滤过分析分配环节任务
        //分析结果录入
        DataRow[] dr2 = dtResult.Select(" RESULT_STATUS='20'");
        //实验室主任复核
        DataRow[] dr3 = dtResult.Select(" RESULT_STATUS='30'");
        //质量科审核
        DataRow[] dr4 = dtResult.Select(" RESULT_STATUS='40'");
        //质量负责人审核
        DataRow[] dr5 = dtResult.Select(" RESULT_STATUS='50'");
        //技术负责人审核
        DataRow[] dr6 = dtResult.Select(" RESULT_STATUS='60'");

        string strShowNameAnalyseChecker = "实验室主任复核";
        string strShowNameAgainChecker = "质量科审核";
        string strShowNameQcChecker = "质量负责人审核";
        string strShowNameTechChecker = "技术负责人审核";

        if (dr1.Length > 0)//环节在分析任务安排
        {
            strStepJson += strStepJson.Length > 0 ? "," : "";
            strStepJson += "{" + string.Format(strDivWait, "分析任务分配", strAnalyseSender) + "}";
        }
        else
        {
            strStepJson += strStepJson.Length > 0 ? "," : "";
            strStepJson += "{" + string.Format(strDivHas, "分析任务分配", GetUserRealName(objSubTaskApp.ANALYSE_ASSIGN_ID)) + "}";


            GetResultJson(ref strStepJson,strDivHas, strDivWait, "分析结果录入", dtResult);

            if (dr2.Length > 0)//结果提交不完全时
            {
                #region 结果提交不完全时
                if (dr6.Length > 0)//流程已到技术负责人审核（最后环节）
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker) + "}";
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameQcChecker, strQcChecker) + "}";
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameTechChecker, strTechChecker) + "}";
                }
                else if (dr5.Length > 0)//流程仅到质量负责人审核
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker) + "}";
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameQcChecker, strQcChecker) + "}";
                }
                else
                {
                    if (dr4.Length > 0)//流程仅到分析室审核
                    {
                        strStepJson += strStepJson.Length > 0 ? "," : "";
                        strStepJson += "{" + string.Format(strDivWait, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                        strStepJson += strStepJson.Length > 0 ? "," : "";
                        strStepJson += "{" + string.Format(strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker) + "}";
                    }
                    else
                    {
                        if (dr3.Length > 0)//流程仅到实验室主任复核
                        {
                            strStepJson += strStepJson.Length > 0 ? "," : "";
                            strStepJson += "{" + string.Format(strDivWait, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                        }
                    }
                }
                #endregion
            }
            else//结果提交完全时
            {
                #region 结果提交完全时
                if (dr3.Length > 0)//实验室主任复核不完全
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                }
                else
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivHas, strShowNameAnalyseChecker, strAnalyseChecker) + "}";
                }
                if (dr4.Length > 0)//分析审核不完全
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker) + "}";
                }
                else if (dr3.Length == 0)//已经全部发送
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivHas, strShowNameAgainChecker, strAnalyseAgainChecker) + "}";
                }
                if (dr5.Length > 0)//流程已到质量负责人审核环节
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivWait, strShowNameQcChecker, strQcChecker) + "}";
                }
                else
                {
                    strStepJson += strStepJson.Length > 0 ? "," : "";
                    strStepJson += "{" + string.Format(strDivHas, strShowNameQcChecker, strQcChecker) + "}";
                }
                if (dr6.Length > 0)//技术负责人审核（最后环节）
                {
                    if (isfinish)
                    {
                        strStepJson += strStepJson.Length > 0 ? "," : "";
                        strStepJson += "{" + string.Format(strDivHas, strShowNameTechChecker, strTechChecker) + "}";
                    }
                    else
                    {
                        strStepJson += strStepJson.Length > 0 ? "," : "";
                        strStepJson += "{" + string.Format(strDivWait, strShowNameTechChecker, strTechChecker) + "}";
                    }
                }
                #endregion
            }
        }
    }

    /// <summary>
    /// 结果录入 分支处理
    /// </summary>
    /// <param name="strStepJson">容器</param>
    /// <param name="strYesDivStyle">已办环节状态样式</param>
    /// <param name="strWaitDivStyle">未办环节状态</param>
    /// <param name="strStepName">环节名</param>
    /// <param name="dtStepInfo">结果执行表</param>
    /// <returns></returns>
    private void GetResultJson(ref string strStepJson, string strYesDivStyle, string strWaitDivStyle, string strStepName, DataTable dtStepInfo)
    {
        string strNoSumit = "";//未完成
        string strYesSumit = "";//已完成
        foreach (DataRow dr in dtStepInfo.Rows)
        {
            string strHeadUser = GetUserRealName(dr["HEAD_USERID"].ToString());
            if (dr["RESULT_STATUS"].ToString() == "20")//结果未提交
            {
                strNoSumit += !strNoSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
            }
            else//结果已提交
            {
                strYesSumit += !strYesSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
            }
        }
        if (strNoSumit.Length > 0)
        {
            strStepJson += strStepJson.Length > 0 ? "," : "";
            strStepJson += "{" + string.Format(strWaitDivStyle, "分析结果提交", (strNoSumit.IndexOf("、") > 0 ? strNoSumit.Remove(strNoSumit.LastIndexOf("、")) : strNoSumit)) + "}";
        }
        if (strYesSumit.Length > 0)
        {
            strStepJson += strStepJson.Length > 0 ? "," : "";
            strStepJson += "{" + string.Format(strYesDivStyle, "分析结果提交", (strYesSumit.IndexOf("、") > 0 ? strYesSumit.Remove(strYesSumit.LastIndexOf("、")) : strYesSumit)) + "}";
        }
    }

    private void GetStepStr(TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objNoSampleSubTask, TMisMonitorSubtaskVo objSampleSubTask,
        ref string strSteps, ref string strEndSteps)
    {
        //当前环节在预约
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "")
        {
            strEndSteps = ",采样预约,";
        }
        //当前环节在预约办理
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && (objTask.QC_STATUS == "1" || objTask.QC_STATUS == "3"))
        {
            strSteps = ",采样预约,";
            strEndSteps = ",预约办理,";
        }
        //当前环节在采样前质控 有采样前质控
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "2")
        {
            strSteps = ",采样预约,预约办理,";
            strEndSteps = ",采样前质控,";
        }

        //当前环节在采样
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "02")
        {
            strSteps = ",采样预约,预约办理,采样前质控,";
            strEndSteps = ",采样,";
        }

        if (objNoSampleSubTask.ID == "" && objSampleSubTask.ID != "")//全是现场分析项目
        {
            //当前环节在现场复核 清远特有 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "022")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",现场复核,";
            }
            //当前环节在现场审核 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "023")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,";
                strEndSteps = ",现场审核,";
            }
            //当前环节在现场审核完毕 
            if (objTask.TASK_STATUS == "01" && (objSampleSubTask.TASK_STATUS == "024" || objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09"))
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = "";
            }
            if (objTask.TASK_STATUS == "09")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "11")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = ",报告,";
            }
        }
        else if (objNoSampleSubTask.ID != "" && objSampleSubTask.ID == "")//无现场分析项目
        {
            //当前环节在采样后质控 郑州特有 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",采样后质控,";
            }
            //当前环节在样品交接 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,";
                strEndSteps = ",样品交接,";
            }
            //当前环节在分析任务分配
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",实验室环节,";
            }
            if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "10" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "11")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
        }
        else if (objNoSampleSubTask.ID != "" && objSampleSubTask.ID != "")//都有
        {
            //当前环节在现场复核 清远特有 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "022")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",现场复核,";
            }
            //当前环节在现场审核 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "023")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,";
                strEndSteps = ",现场审核,";
            }
            //当前环节在现场审核完毕 
            if (objTask.TASK_STATUS == "01" && (objSampleSubTask.TASK_STATUS == "024" || objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09"))
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = "";
            }

            //当前环节在采样后质控 郑州特有 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = ",采样后质控,";
            }
            //当前环节在样品交接 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,";
                strEndSteps = ",样品交接,";
            }
            //当前环节在分析任务分配
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",实验室环节,";
            }
            if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "10")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "11")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
        }
    }

    private void GetSendSampleStepStr(TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objNoSampleSubTask, TMisMonitorSubtaskVo objSampleSubTask,
        ref string strSteps, ref string strEndSteps)
    {
        //当前环节在自送样预约
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "" && objTask.QC_STATUS == "")
        {
            strEndSteps = ",自送样预约,";
        }

        //当前环节在采样后质控 郑州特有 
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
        {
            strSteps = ",自送样预约,";
            strEndSteps = ",采样后质控,";
        }
        //当前环节在样品交接 
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
        {
            strSteps = ",自送样预约,采样后质控,";
            strEndSteps = ",样品交接,";
        }
        //当前环节在分析任务分配
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
        {
            strSteps = ",自送样预约,采样后质控,样品交接,";
            strEndSteps = ",实验室环节,";
        }
        if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
        {
            strSteps = ",自送样预约,采样后质控,样品交接,";
            strEndSteps = ",报告,";
        }
    }

    /// <summary>
    /// 获得指定职责的默认负责人，如果不存在则取第一个
    /// </summary>
    /// <param name="strDutyType">职责编码</param>
    /// <param name="strMonitorType">监测类别</param>
    /// <returns></returns>
    private string GetDefaultOrFirstDutyUser(string strDutyType, string strMonitorType)
    {
        DataTable dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorType);
        //过滤默认负责人
        DataRow[] drDuty = dtDuty.Select(" IF_DEFAULT");
        if (drDuty.Length > 0)
        {
            return GetUserRealName(drDuty[0]["USERID"].ToString());
        }
        else
        {
            if (dtDuty.Rows.Count > 0)
            {
                return GetUserRealName(dtDuty.Rows[0]["USERID"].ToString());
            }
        }
        return "";
    }

    /// <summary>
    /// 获取人员姓名
    /// </summary>
    /// <param name="strUserID">用户ID</param>
    /// <returns></returns>
    private string GetUserRealName(string strUserID)
    {
        return new TSysUserLogic().Details(strUserID).REAL_NAME;
    }
}