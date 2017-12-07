using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor;
using i3.View;
using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;

using i3.BusinessLogic.Channels.RPT;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Result;

/// <summary>
/// 功能描述：综合查询
/// 创建时间：2012-12-1
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_TotalSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDict")
        {
            Response.Write(getDictJsonString(Request.QueryString["typeid"]));
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getContract")
        {
            strResult = getContractInfoForJson(Request.QueryString["contract_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getSample")
        {
            strResult = getSampleInfoForJson(Request.QueryString["task_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getTask")
        {
            strResult = getTaskFinder(Request.QueryString["task_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetTaskInfo")
        {
            strResult = getTaskInfoForJson();
            Response.Write(strResult);
            Response.End();
        }
        //获取委托企业信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getClientCompanyInfo")
        {
            strResult = GetClientCompanyInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取受检企业信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTestedCompanyInfo")
        {
            strResult = GetTestedCompanyInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取监测费用
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractFee")
        {
            strResult = getContractFee(Request.QueryString["contract_id"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取监测报告ID
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getReportID")
        {
            strResult = getReportID(Request.QueryString["task_id"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取工作流程信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getFlowInfo")
        {
            strResult = getFlowInfo(Request.QueryString["business_id"]); 
            Response.Write(strResult);
            Response.End();
        }
        //获取工作流程情况列表（委托书流程、监测业务流程、报告流程） Create By weilin 204-5-30
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTotalFlowList")
        {
            strResult = getTotalFlowList(Request.QueryString["ContractID"], Request.QueryString["TaskID"]);
            Response.Write(strResult);
            Response.End();
        }
        //获取采购工作流程信息，黄进军添加20141024
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getPurchaseInfo")
        {
            strResult = getPurchaseInfo(Request.QueryString["business_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "GetContractInfo")
            {
                getContractInfo();
            }
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    protected void getContractInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //委托年度
        string strYear = !string.IsNullOrEmpty(Request.QueryString["SrhYear"]) ? Request.QueryString["SrhYear"].ToString() : "";
        //委托类型
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["SrhContractType"]) ? Request.QueryString["SrhContractType"].ToString() : "";
        //合同号
        string strContractCode = !string.IsNullOrEmpty(Request.QueryString["SrhContractCode"]) ? Request.QueryString["SrhContractCode"].ToString() : "";
        //任务单号
        string strDutyCode = !string.IsNullOrEmpty(Request.QueryString["DutyCode"]) ? Request.QueryString["DutyCode"].ToString() : "";
        //报告号
        string strReportCode = !string.IsNullOrEmpty(Request.QueryString["ReportCode"]) ? Request.QueryString["ReportCode"].ToString() : "";
        //委托客户
        string strClientName = !string.IsNullOrEmpty(Request.QueryString["ClientName"]) ? Request.QueryString["ClientName"].ToString() : "";
        //合同类别
        string strItemType = !string.IsNullOrEmpty(Request.QueryString["ItemType"]) ? Request.QueryString["ItemType"].ToString() : "";
        //项目名称
        string strProjectName = !string.IsNullOrEmpty(Request.QueryString["SrhProjectName"]) ? Request.QueryString["SrhProjectName"].ToString() : "";

        //构造查询对象
        TMisContractVo objContract = new TMisContractVo();
        TMisContractLogic objContractLogic = new TMisContractLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisContractVo.CONTRACT_CODE_FIELD;

        objContract.SORT_FIELD = strSortname;
        objContract.SORT_TYPE = strSortorder;
        objContract.CONTRACT_YEAR = strYear;
        objContract.CONTRACT_TYPE = strContractType;
        objContract.CONTRACT_CODE = strContractCode;
        objContract.CLIENT_COMPANY_ID = strClientName;
        objContract.TEST_TYPE = strItemType;
        objContract.PROJECT_NAME = strProjectName;


        int intTotalCount = objContractLogic.GetSelectResultCount(objContract);//总计的数据条数
        DataTable dt = objContractLogic.SelectByTable(objContract, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取委托书企业名称
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strValue)
    {
        return new TMisContractCompanyLogic().Details(strValue).COMPANY_NAME;
    }
    /// <summary>
    /// 获取任务企业名称
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyNameByTask(string strValue)
    {
        return new TMisMonitorTaskCompanyLogic().Details(strValue).COMPANY_NAME;
    }
    /// <summary>
    /// 获取委托类型名称
    /// </summary>
    /// <param name="strTypeID">委托类型ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strValue)
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(strValue, "Contract_Type");
    }

    /// <summary>
    /// 获取监测类型名称
    /// </summary>
    /// <param name="strTypeID">委托类型ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorName(string strValue)
    {
        return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取用户姓名
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }

    /// <summary>
    /// 获取任务报告编制人用户姓名
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getReportUserName(string strTaskID, string strContractID)
    {
        string strUserName = "";
        string strUserID = "";
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        if (strTaskID.Length > 0)
        {
            objTaskVo = new TMisMonitorTaskLogic().Details(strTaskID);
            if (objTaskVo.REPORT_HANDLE.Length > 0)
                strUserName = new TSysUserLogic().Details(objTaskVo.REPORT_HANDLE).REAL_NAME;
        }
        else
        {
            objTaskVo.CONTRACT_ID = strContractID;
            DataTable dtTask = new TMisMonitorTaskLogic().SelectByTable(objTaskVo);
            for (int i = 0; i < dtTask.Rows.Count; i++)
            {
                if (dtTask.Rows[i]["REPORT_HANDLE"].ToString().Length > 0 && !strUserID.Contains(dtTask.Rows[i]["REPORT_HANDLE"].ToString()))
                {
                    strUserID += dtTask.Rows[i]["REPORT_HANDLE"].ToString() + "、";
                    strUserName += new TSysUserLogic().Details(dtTask.Rows[i]["REPORT_HANDLE"].ToString()).REAL_NAME + "、";
                }
            }
        }
        return strUserName.TrimEnd('、');
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <param name="strContractID">委托书ID</param>
    /// <returns></returns>
    protected string getContractInfoForJson(string strContractID)
    {
        TMisContractVo objContract = new TMisContractLogic().Details(strContractID);
        //签订日期 格式
        try
        {
            objContract.ASKING_DATE = DateTime.Parse(objContract.ASKING_DATE).ToString("yyyy-MM-dd");
        }
        catch { }
        //备注信息组装
        string strRemark = "";
        //同意分包
        if (objContract.AGREE_OUTSOURCING == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_subpackage", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意监测方法
        if (objContract.AGREE_METHOD == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_useMonitorMethod", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意使用非标准方法
        if (objContract.AGREE_NONSTANDARD == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_usenonstandard", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意其他
        if (objContract.AGREE_OTHER == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_other", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        if (strRemark.Length > 0)
        {
            strRemark = strRemark.Remove(strRemark.LastIndexOf(";"));
        }
        objContract.REMARK1 = strRemark;
        //添加监测类型
        string strMonitorType = "";//合同类型字符串
        if (objContract.TEST_TYPES.Length > 0)//所有监测类型
        {
            string[] strTestType = objContract.TEST_TYPES.Split(';');
            foreach (string str in strTestType)
            {
                if (str.Length > 0)
                {
                    //监测类别名称
                    string strTypeName = new TBaseMonitorTypeInfoLogic().Details(str).MONITOR_TYPE_NAME;
                    strMonitorType += strTypeName.Length > 0 ? strTypeName + "," : "";
                }
            }
        }
        strMonitorType = strMonitorType.Length > 0 ? strMonitorType.Remove(strMonitorType.LastIndexOf(",")) : "";
        objContract.TEST_TYPES = strMonitorType;
        return ToJson(objContract);
    }
    /// <summary>
    /// 获取监测任务列表信息
    /// </summary>
    /// <returns></returns>
    protected string getTaskInfoForJson()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        objTask.SORT_FIELD = strSortname;
        objTask.SORT_TYPE = strSortorder;
        objTask.CONTRACT_ID = !string.IsNullOrEmpty(Request.QueryString["contract_id"]) ? Request.QueryString["contract_id"].ToString() : "";

        int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable(objTask, intPageIdx, intPagesize);

        return CreateToJson(dt, intTotalCount);
    }
    /// <summary>
    /// 获得样品信息
    /// </summary>
    /// <param name="strContractID"></param>
    /// <returns></returns>
    protected string getSampleInfoForJson(string strTaskID)
    {
        return "";
    }
    /// <summary>
    /// 获得任务信息（任务追踪）
    /// </summary>
    /// <param name="strContractID"></param>
    /// <returns></returns>
    protected string getTaskFinder(string strTaskID)
    {
        return "";
    }
    /// <summary>
    /// 获得委托企业信息
    /// </summary>
    /// <returns></returns>
    protected string GetClientCompanyInfo()
    {
        //委托企业
        TMisContractCompanyVo objTaskCompany = new TMisContractCompanyLogic().Details(Request.QueryString["id"]);
        return ToJson(objTaskCompany);
    }
    /// <summary>
    /// 获得受检企业信息
    /// </summary>
    /// <returns></returns>
    protected string GetTestedCompanyInfo()
    {
        //受检企业
        TMisContractCompanyVo objTaskCompany = new TMisContractCompanyLogic().Details(Request.QueryString["id"]);
        return ToJson(objTaskCompany);
    }
    /// <summary>
    /// 获取监测实际费用
    /// </summary>
    /// <param name="strContractFee">委托ID</param>
    /// <returns></returns>
    protected string getContractFee(string strContractFee)
    {
        return new TMisContractFeeLogic().Details(new TMisContractFeeVo() { CONTRACT_ID = strContractFee }).INCOME;
    }
    /// <summary>
    /// 获取监测报告ID
    /// </summary>
    /// <param name="strTaskID">监测任务ID</param>
    /// <returns></returns>
    protected string getReportID(string strTaskID)
    {
        return new TRptFileLogic().getNewReportByContractID(strTaskID).ID;
    }

    /// <summary>
    /// 获取物料采购流程信息，黄进军添加20141024
    /// </summary>
    /// <param name="strBusinessID">业务ID</param>
    /// <returns></returns>
    protected string getPurchaseInfo(string strBusinessID)
    {
        string strWf_ID = "PARTPLAN";//物料采购流程

        //构造实例明细对象
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailVo();
        objTaskDetail.WF_ID = strWf_ID;
        //获取业务流程所有环节信息
        DataTable dt = new TWfInstTaskDetailLogic().GetWFDetailByBusinessInfo(objTaskDetail, "task_id", Request.QueryString["business_id"]);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["CONTROL_ID"].ToString() + "|" + GetStepHeight(strWf_ID);
        }
        return "";
    }

    /// <summary>
    /// 获取委托书流程信息
    /// </summary>
    /// <param name="strBusinessID">业务ID</param>
    /// <returns></returns>
    protected string getFlowInfo(string strBusinessID)
    {
        string strWf_ID = "RPT";//默认为报告流程
        //流程类别
        if (!string.IsNullOrEmpty(Request.QueryString["contract_type"]))
        {
            switch (Request.QueryString["contract_type"])
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
        }
        //构造实例明细对象
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailVo();
        objTaskDetail.WF_ID = strWf_ID;
        //获取业务流程所有环节信息
        DataTable dt = new TWfInstTaskDetailLogic().GetWFDetailByBusinessInfo(objTaskDetail, "task_id", Request.QueryString["business_id"]);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["CONTROL_ID"].ToString() + "|" + GetStepHeight(strWf_ID);
        }
        return "";
    }
    /// <summary>
    /// 动态返回任务追踪窗体高度
    /// </summary>
    /// <param name="strStepID">环节ID</param>
    /// <returns></returns>
    public string GetStepHeight(object strStepID)
    {

        int iHeader = 200;
        int iStep = 100;
        int iHeight = 560;

        if (null == strStepID || string.IsNullOrEmpty(strStepID.ToString()))
            return iHeight.ToString();


        DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strStepID.ToString() });
        if (dt.Rows.Count == 0)
            iHeight = iHeader + iStep * 4;
        else
            iHeight = iHeader + iStep * dt.Rows.Count;
        return iHeight.ToString();
    }

    /// <summary>
    /// 获取工作流程情况列表（委托书流程、监测业务流程、报告流程） Create By weilin 204-5-30
    /// </summary>
    /// <param name="strPlan_ID">计划ID</param>
    /// <returns></returns>
    public string getTotalFlowList(string strContractID, string strTaskID)
    {
        string strContractType = "";
        string strWF_ID = "";
        DataTable dt = new DataTable();
        dt.Columns.Add("WF_LINK");               //环节
        dt.Columns.Add("WF_STATUS");             //状态
        dt.Columns.Add("WF_USER");               //办理人
        dt.Columns.Add("WF_TIME");               //办理时间

        TMisMonitorTaskVo MonitorTaskVo = new TMisMonitorTaskVo();
        TMisContractVo ContractVo = new TMisContractVo();
        if (strContractID == "" && strTaskID != "")
        {
            MonitorTaskVo = new TMisMonitorTaskLogic().Details(strTaskID);
            strContractID = MonitorTaskVo.CONTRACT_ID;
            ContractVo = new TMisContractLogic().Details(strContractID);
        }
        if (strContractID != "" && strTaskID == "")
        {
            ContractVo = new TMisContractLogic().Details(strContractID);
            MonitorTaskVo.CONTRACT_ID = strContractID;
            MonitorTaskVo = MonitorTaskVo = new TMisMonitorTaskLogic().SelectByObject(MonitorTaskVo);
            strTaskID = MonitorTaskVo.ID;
        }
        if (strContractID != "" && strTaskID != "")
        {
            MonitorTaskVo = new TMisMonitorTaskLogic().Details(strTaskID);
            ContractVo = new TMisContractLogic().Details(strContractID);
        }
        
        strContractType = ContractVo.CONTRACT_TYPE;

        #region 委托书流程
        //流程类别
        if (strContractID != "")
        {
            switch (strContractType)
            {
                //自送样流程
                case "04":
                    strWF_ID = "SAMPLE_WT";
                    break;
                //验收流程
                case "05":
                    strWF_ID = "WF_A";
                    break;
                //常规委托书流程
                default:
                    strWF_ID = "WT_FLOW";
                    break;
            }
        }
        //构造实例明细对象
        TWfInstTaskDetailVo objTaskDetail = new TWfInstTaskDetailVo();
        objTaskDetail.WF_ID = strWF_ID;
        //获取业务流程所有环节信息
        DataTable dtFlow = new TWfInstTaskDetailLogic().GetWFDetailByBusinessInfo(objTaskDetail, "task_id", (strContractID == "" ? "null" : strContractID));
        if (dtFlow.Rows.Count > 0)
        {
            objTaskDetail.WF_INST_ID = dtFlow.Rows[0]["CONTROL_ID"].ToString();
            dtFlow = new TWfInstTaskDetailLogic().SelectByTable(objTaskDetail);
            for (int i = 0; i < dtFlow.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["WF_LINK"] = dtFlow.Rows[i]["INST_TASK_CAPTION"].ToString();
                dr["WF_STATUS"] = dtFlow.Rows[i]["INST_TASK_STATE"].ToString();
                dr["WF_USER"] = new TSysUserLogic().Details(dtFlow.Rows[i]["OBJECT_USER"].ToString()).REAL_NAME;
                dr["WF_TIME"] = dtFlow.Rows[i]["INST_TASK_ENDTIME"].ToString();
                dt.Rows.Add(dr);
            }
        }
        #endregion

        #region 监测业务流程

        getMonitorFlow(MonitorTaskVo, ref dt);

        #endregion

        #region 报告流程
        //流程类别
        strWF_ID = "RPT";
        //构造实例明细对象
        objTaskDetail = new TWfInstTaskDetailVo();
        objTaskDetail.WF_ID = strWF_ID;
        //获取业务流程所有环节信息
        dtFlow = new TWfInstTaskDetailLogic().GetWFDetailByBusinessInfo(objTaskDetail, "task_id", (strTaskID == "" ? "null" : strTaskID));
        if (dtFlow.Rows.Count > 0)
        {
            objTaskDetail.WF_INST_ID = dtFlow.Rows[0]["CONTROL_ID"].ToString();
            dtFlow = new TWfInstTaskDetailLogic().SelectByTable(objTaskDetail);
            for (int i = 0; i < dtFlow.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["WF_LINK"] = dtFlow.Rows[i]["INST_TASK_CAPTION"].ToString();
                dr["WF_STATUS"] = dtFlow.Rows[i]["INST_TASK_STATE"].ToString();
                dr["WF_USER"] = new TSysUserLogic().Details(dtFlow.Rows[i]["OBJECT_USER"].ToString()).REAL_NAME;
                dr["WF_TIME"] = dtFlow.Rows[i]["INST_TASK_ENDTIME"].ToString();
                dt.Rows.Add(dr);
            }
        }
        #endregion
        return CreateToJson(dt, dt.Rows.Count);
        //return DataTableToJson(dt);
    }

    public void getMonitorFlow(TMisMonitorTaskVo MonitorTaskVo, ref DataTable dt)
    {
        string strTaskStatus = "";        //任务状态
        string strQcStatus = "";       
        string strMark = "";        //监测项目的情况（0：任务中只有分析项目；1：任务中只有现场项目；2：任务中现场、分析项目都有）
        DataTable dtSceneItem = new DataTable();      //现场项目
        DataTable dtAnalyItem = new DataTable();      //分析项目

        dtSceneItem = new TMisMonitorResultLogic().SelectItemInfoWithTaskID(MonitorTaskVo.ID, true);
        dtAnalyItem = new TMisMonitorResultLogic().SelectItemInfoWithTaskID(MonitorTaskVo.ID, false);
        if (dtSceneItem.Rows.Count > 0 && dtAnalyItem.Rows.Count > 0)
        { strMark = "2"; }
        else if (dtSceneItem.Rows.Count > 0 && dtAnalyItem.Rows.Count == 0)
        { strMark = "1"; }
        else if (dtSceneItem.Rows.Count == 0 && dtAnalyItem.Rows.Count > 0)
        { strMark = "0"; }
        else
            return;

        strTaskStatus = MonitorTaskVo.TASK_STATUS;
        strQcStatus = MonitorTaskVo.QC_STATUS;
        
        if (strTaskStatus == "01")        //监测业务流程未走完
        {
            string strSubStatus = "";
            List<TMisMonitorSubtaskVo> objList = new TMisMonitorSubtaskLogic().SelectByObject(new TMisMonitorSubtaskVo { TASK_ID = MonitorTaskVo.ID }, 0, 0);
            for (int i = 0; i < objList.Count; i++)
            {
                strSubStatus += objList[i].TASK_STATUS + ",";
            }
            switch (strQcStatus)
            {
                case "":   //流程状态：任务下达
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2A");
                    break;
                case "1":  //流程状态：采样前质控
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2A");
                    break;
                case "4":  //流程状态：采样前质控审核
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2A");
                    break;
                case "2":  //流程状态：采样任务分配
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2A");
                    break;
                default:
                    if (strSubStatus.Contains("02,")) //流程状态：采样
                    {
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2A");
                    }
                    else if (strSubStatus.Contains("023,"))
                    {
                        if (strSubStatus.Contains("021,"))   //流程状态：现场主任审核、样品交接
                        {
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2A");
                        }
                        else if (strSubStatus.Contains("03,"))
                        {
                            DataTable dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "20");
                            if (dtTemp.Rows.Count > 0)    //流程状态：现场主任审核、监测分析
                            {
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2A");
                            }
                            else
                            {
                                dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "30");
                                if (dtTemp.Rows.Count > 0)      //流程状态：现场主任审核、主任复核
                                {
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2A");
                                }
                                else
                                {
                                    dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "40");
                                    if (dtTemp.Rows.Count > 0)   //流程状态：现场主任审核、质量科审核
                                    {
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2A");
                                    }
                                    else
                                    {
                                        dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "50");
                                        if (dtTemp.Rows.Count > 0)   //流程状态：现场主任审核、质量负责人审核
                                        {
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2A");
                                        }
                                        else
                                        {
                                            dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "60");
                                            if (dtTemp.Rows.Count > 0)   //流程状态：现场主任审核、技术负责人审核
                                            {
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_012, "2A");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else      //流程状态：现场主任审核
                        {
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2A");
                        }
                    }
                    else if (strSubStatus.Contains("021,"))  //流程状态：样品交接   
                    {
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                        if (strMark == "1" || strMark == "2")
                        {
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                        }
                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2A");
                    }
                    else if (strSubStatus.Contains("03,"))
                    {
                        DataTable dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "20");
                        if (dtTemp.Rows.Count > 0)    //流程状态：监测分析
                        {
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                            if (strMark == "1" || strMark == "2")
                            {
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                            }
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2A");
                        }
                        else
                        {
                            dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "30");
                            if (dtTemp.Rows.Count > 0)      //流程状态：主任复核
                            {
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                if (strMark == "1" || strMark == "2")
                                {
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                }
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2A");
                            }
                            else
                            {
                                dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "40");
                                if (dtTemp.Rows.Count > 0)   //流程状态：质量科审核
                                {
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                    if (strMark == "1" || strMark == "2")
                                    {
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                    }
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                    InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2A");
                                }
                                else
                                {
                                    dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "50");
                                    if (dtTemp.Rows.Count > 0)   //流程状态：质量负责人审核
                                    {
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                        if (strMark == "2")
                                        {
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                        }
                                        else if (strMark == "1")
                                        {
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                        }
                                        else if (strMark == "0")
                                        {
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                        }
                                        InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2A");
                                    }
                                    else
                                    {
                                        dtTemp = new TMisMonitorResultLogic().SelectItemStatus(MonitorTaskVo.ID, "60");
                                        if (dtTemp.Rows.Count > 0)   //流程状态：技术负责人审核
                                        {
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
                                            if (strMark == "2")
                                            {
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                                            }
                                            else if (strMark == "1")
                                            {
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                                            }
                                            else if (strMark == "0")
                                            {
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                                                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                                            }
                                            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_012, "2A");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

        }
        else                          //监测业务流程已走完(流程状态：报告编制)
        {
            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_000, "2B");
            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_001, "2B");
            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_002, "2B");
            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_003, "2B");
            InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_004, "2B");
            if (strMark == "0")
            {
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_012, "2B");
            }
            else if (strMark == "1")
            {
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_012, "2B");
            }
            else if (strMark == "2")
            {
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_005, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_006, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_008, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_009, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_010, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_011, "2B");
                InsertDt(MonitorTaskVo, ref dt, SerialType.Monitor_ZZ_012, "2B");
            }
        }
    }

    public void InsertDt(TMisMonitorTaskVo MonitorTaskVo, ref DataTable dt, string strLink, string strStatus)
    {
        string strName = "";
        string strLinkName = "";
        string strUser = "";
        string strTime = "";
        List<TMisMonitorSubtaskVo> listObjSubTaskVo = new TMisMonitorSubtaskLogic().SelectByObject(new TMisMonitorSubtaskVo { TASK_ID = MonitorTaskVo.ID }, 0, 0);
        TMisMonitorSubtaskAppVo objSubtaskAppVo = new TMisMonitorSubtaskAppVo();
        if (listObjSubTaskVo.Count > 0)
        {
            objSubtaskAppVo = new TMisMonitorSubtaskAppLogic().SelectByObject(new TMisMonitorSubtaskAppVo { SUBTASK_ID = listObjSubTaskVo[0].ID });
        }
        switch (strLink)
        {
            case "000":
                strLinkName = "采样任务下达";
                strUser = new TSysUserLogic().Details(MonitorTaskVo.CREATOR_ID).REAL_NAME;
                strTime = MonitorTaskVo.CREATE_DATE;
                break;
            case "001":
                strLinkName = "采样前质控";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.QC_USER_ID).REAL_NAME;
                strTime = objSubtaskAppVo.QC_DATE;
                break;
            case "002":
                strLinkName = "采样前质控审核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.QC_APP_USER_ID).REAL_NAME;
                strTime = objSubtaskAppVo.QC_APP_DATE;
                break;
            case "003":
                strLinkName = "采样任务分配";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.SAMPLE_ASSIGN_ID).REAL_NAME;
                strTime = objSubtaskAppVo.SAMPLE_ASSIGN_DATE;
                break;
            case "004":
                strLinkName = "采样";
                for (int i = 0; i < listObjSubTaskVo.Count; i++)
                {
                    strName = new TSysUserLogic().Details(listObjSubTaskVo[i].SAMPLING_MANAGER_ID).REAL_NAME;
                    if (!strUser.Contains(strName))
                    {
                        strUser += strName + ",";
                    }
                    strTime = listObjSubTaskVo[i].SAMPLE_FINISH_DATE;
                }
                strUser = strUser.TrimEnd(',');
                break;
            case "005":
                strLinkName = "现场主任审核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.SAMPLING_QC_CHECK).REAL_NAME;
                strTime = "";
                break;
            case "006":
                strLinkName = "样品交接";
                for (int i = 0; i < listObjSubTaskVo.Count; i++)
                {
                    strName = new TSysUserLogic().Details(listObjSubTaskVo[i].SAMPLE_ACCESS_ID).REAL_NAME;
                    if (!strUser.Contains(strName))
                    {
                        strUser += strName + ",";
                    }
                    strTime = listObjSubTaskVo[i].SAMPLE_ACCESS_DATE;
                }
                strUser = strUser.TrimEnd(',');
                break;
            case "007":
                strLinkName = "分析任务分配";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.ANALYSE_ASSIGN_ID).REAL_NAME;
                strTime = objSubtaskAppVo.ANALYSE_ASSIGN_ID;
                break;
            case "008":
                strLinkName = "监测分析";
                DataTable dtAnalyItem = new TMisMonitorResultLogic().SelectItemInfoWithTaskID(MonitorTaskVo.ID, false);
                TMisMonitorResultAppVo objResultAppVo = new TMisMonitorResultAppVo();
                for (int i = 0; i < dtAnalyItem.Rows.Count; i++)
                {
                    objResultAppVo.RESULT_ID = dtAnalyItem.Rows[i]["ID"].ToString();
                    objResultAppVo = new TMisMonitorResultAppLogic().SelectByObject(objResultAppVo);

                    strName = new TSysUserLogic().Details(objResultAppVo.HEAD_USERID).REAL_NAME;
                    if (!strUser.Contains(strName))
                    {
                        strUser += strName + ",";
                    }
                }
                strUser = strUser.TrimEnd(',');
                strTime = "";
                break;
            case "009":
                strLinkName = "主任复核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.RESULT_AUDIT).REAL_NAME;
                strTime = "";
                break;
            case "010":
                strLinkName = "质量科审核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.RESULT_CHECK).REAL_NAME;
                strTime = "";
                break;
            case "011":
                strLinkName = "质量负责人审核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.RESULT_QC_CHECK).REAL_NAME;
                strTime = "";
                break;
            case "012":
                strLinkName = "技术负责人审核";
                strUser = new TSysUserLogic().Details(objSubtaskAppVo.RESULT_QC_CHECK).REAL_NAME;
                strTime = "";
                break;
        }
        DataRow dr = dt.NewRow();
        dr["WF_LINK"] = strLinkName;
        dr["WF_STATUS"] = strStatus;
        dr["WF_USER"] = strUser;
        dr["WF_TIME"] = strTime;
        dt.Rows.Add(dr);
    }

}