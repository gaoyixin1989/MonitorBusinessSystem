<%@ WebService Language="C#" Class="WfQcApp" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Base.MonitorType;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WfQcApp  : System.Web.Services.WebService {

    [WebMethod]
    public string GetDataLst(int intPageIndex, int intPageSize)
    {
        //DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo("", "qc_manager_audit", "03", "50", intPageIndex, intPageSize);
        //int intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount("", "qc_manager_audit", "03", "50");
        DataTable dt = new TMisMonitorResultLogic().getTaskQcCheckInfo_ZZ("03", "50", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_ZZ("03", "50");
        delCol_fromDataTable(ref dt);

        string strJson = i3.View.PageBase.CreateToJson(dt, intTotalCount);

        return strJson;
    }

    [WebMethod]
    public string SendTask(string strID,string strUserId)
    {
        string strTaskId = strID;
        
        //记录当前分析环节的发送人
        DataTable objTable = new TMisMonitorResultLogic().geResultChecktItemTypeInfo_QHD(strUserId, "qc_manager_audit", strTaskId, "03", "0", "50");
        foreach (DataRow row in objTable.Rows)
        {
            string strId = row["ID"].ToString();
            //根据子任务ＩＤ获取监测子任务审核表ID
            TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVoTemp = new TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVoTemp.SUBTASK_ID = strId;
            string strSubTaskAppId = new TMisMonitorSubtaskAppLogic().Details(TMisMonitorSubtaskAppVoTemp).ID;
            TMisMonitorSubtaskAppVo TMisMonitorSubtaskAppVo = new TMisMonitorSubtaskAppVo();
            TMisMonitorSubtaskAppVo.ID = strSubTaskAppId;
            TMisMonitorSubtaskAppVo.RESULT_QC_CHECK = strUserId;
            new TMisMonitorSubtaskAppLogic().Edit(TMisMonitorSubtaskAppVo);
        }

        //分析环节任务完结状态改变
        bool IsSuccess = false;
        IsSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlow(strTaskId, strUserId, "09", "qc_manager_audit");
        if (IsSuccess == true)//郑州增加技术负责人审核，质量负责人审核不再是最后一环节，以下代码不执行
        {
            //如果是环境质量将自动对数据进行填报
            foreach (DataRow row in objTable.Rows)
            {
                string strSubTaskId = row["ID"].ToString();
                string strMonitorId = row["MONITOR_ID"].ToString();
                TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
                TMisMonitorSubtaskVo.ID = strSubTaskId;
                TMisMonitorSubtaskVo.MONITOR_ID = strMonitorId;
                if (strMonitorId == "EnvRiver" || strMonitorId == "EnvReservoir" || strMonitorId == "EnvDrinking" || strMonitorId == "EnvStbc" || strMonitorId == "EnvMudRiver" || strMonitorId == "EnvPSoild" || strMonitorId == "EnvSoil" || strMonitorId == "EnvAir" || strMonitorId == "EnvSpeed" || strMonitorId == "EnvDust" || strMonitorId == "EnvRain")
                {
                    new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, false, "");
                }
                if (strMonitorId == "AreaNoise" || strMonitorId == "EnvRoadNoise" || strMonitorId == "FunctionNoise")
                {
                    new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, true, "");
                }
            }
            if (new TMisMonitorResultLogic().allTaskIsFinish_ZZ(strTaskId, "24", "60"))
            {
                TMisMonitorTaskVo TMisMonitorTaskVo = new TMisMonitorTaskVo();
                TMisMonitorTaskVo.ID = strTaskId;
                TMisMonitorTaskVo.TASK_STATUS = "09";
                new TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);

                TMisMonitorTaskVo TMisMonitorTaskVoTemp = new TMisMonitorTaskLogic().Details(strTaskId);
                if (TMisMonitorTaskVoTemp.CONTRACT_TYPE == "05")
                {
                    //根据任务ID获取项目负责人
                    string strProjectId = TMisMonitorTaskVoTemp.PROJECT_ID;
                    TMisMonitorReportVo objMrUpdate = new TMisMonitorReportVo();
                    objMrUpdate.IF_SEND = "1";
                    objMrUpdate.REPORT_SCHEDULER = strProjectId;
                    TMisMonitorReportVo objMrWhere = new TMisMonitorReportVo();
                    objMrWhere.TASK_ID = strTaskId;
                    new TMisMonitorReportLogic().Edit(objMrUpdate, objMrWhere);
                }
            }
        }
        return IsSuccess == true ? "1" : "0";
    }

    [WebMethod]
    public string BackTask(string strID, string strUserId)
    {
        bool IsSuccess = new TMisMonitorResultLogic().TaskGoBackInQcCheck_QHD(strID, strUserId, "qc_manager_audit", "0", "50", "40");
        return IsSuccess == true ? "1" : "0";
    }
    
    [WebMethod]
    public string GetDataInfo(string strID)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strID);
        DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo { TASK_ID = strID });
        string strReturn = "";
        
        strReturn = "{";
        strReturn += "'任务单号':'" + objTask.TICKET_NUM + "',";
        strReturn += "'委托类别':'" + new TSysDictLogic().GetDictNameByDictCodeAndType(objTask.CONTRACT_TYPE, "Contract_Type") + "',";
        strReturn += "'项目名称':'" + objTask.PROJECT_NAME + "',";
        strReturn += "'受检企业':'" + new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID).COMPANY_NAME + "',";
        strReturn += "'报告要求完成时间':'" + objTask.ASKING_DATE + "',";
        strReturn += "'Attach':[";

        string strAttStr = "";
        foreach (DataRow dr in dtSub.Rows)
        {
            strAttStr += (strAttStr.Length > 0 ? "," : "") + getAttStr(dr["ID"].ToString());
        }
        strReturn += strAttStr;
        strReturn += "]";
        
        strReturn += "}";

        return strReturn;
    }

    private string getAttStr(string strSubID)
    {
        TMisMonitorSubtaskVo objSub = new TMisMonitorSubtaskLogic().Details(strSubID);
        string strMonitorType = new TBaseMonitorTypeInfoLogic().Details(objSub.MONITOR_ID).MONITOR_TYPE_NAME;

        string strHasAtt_Sample = "";
        string fileName_Sample = "";
        string strMobileFileName_Sample = "";
        getAttFile(strSubID, "SampleSumFile", ref strHasAtt_Sample, ref fileName_Sample, ref strMobileFileName_Sample);
        string strHasAtt_Result = "";
        string fileName_Result = "";
        string strMobileFileName_Result = "";
        getAttFile(strSubID, "ResultSumFile", ref strHasAtt_Result, ref fileName_Result, ref strMobileFileName_Result);

        string strReturn = "{";
        strReturn += "'监测类别':'" + strMonitorType + "',";
        strReturn += "'HasAtt1':'" + strHasAtt_Sample + "',";
        strReturn += "'AttFileName1':'" + fileName_Sample + "',";
        strReturn += "'AttFilePath1':'TempFile/" + strMobileFileName_Sample + "',";
        strReturn += "'HasAtt2':'" + strHasAtt_Result + "',";
        strReturn += "'AttFileName2':'" + fileName_Result + "',";
        strReturn += "'AttFilePath2':'TempFile/" + strMobileFileName_Result + "'";
        strReturn += "}";

        return strReturn;
    }

    private void getAttFile(string strSubID, string strAttFileType, ref string strHasAtt, ref string fileName, ref string strMobileFileName)
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = strSubID;
        TOaAttVo.BUSINESS_TYPE = strAttFileType;
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        fileName = TOaAttVoTemp.ATTACH_NAME + TOaAttVoTemp.ATTACH_TYPE;//客户端保存的文件名 
        string filePath = mastPath + '\\' + TOaAttVoTemp.UPLOAD_PATH;
        strHasAtt = "1";
        strMobileFileName = "";
        if (File.Exists(filePath) == false)
        {
            strHasAtt = "0";
        }
        else
        {
            string serverPath = HttpRuntime.AppDomainAppPath + "TempFile";
            DateTime dtNow = System.DateTime.Now;
            string strDatetimeStr = dtNow.Year.ToString() + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');
            strMobileFileName = strAttFileType + strSubID + strDatetimeStr + TOaAttVoTemp.ATTACH_TYPE;
            try
            {
                File.Copy(filePath, serverPath + "\\" + strMobileFileName, true);
            }
            catch (Exception ex) { }
        }
        
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
        //dt.Columns.Remove("ASKING_DATE");
        dt.Columns.Remove("FINISH_DATE");
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
}