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
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

/// <summary>
/// 功能描述：综合查询
/// 创建时间：2012-12-1
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_SearchQHD_TotalSearch : PageBase
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
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetContractInfo")
        {
            Response.Write(getContractInfo());
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
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "GetTaskInfo")
            {
                Response.Write(getTaskInfoForJson());
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    protected string getContractInfo()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //合同ID
        if (!string.IsNullOrEmpty(Request.QueryString["contract_id"]))
        {
            string strContractId = Request.QueryString["contract_id"].ToString();
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

            objContract.ID = strContractId;
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

            return CreateToJson(dt, intTotalCount);
        }
        return "";
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
    /// <param name="strTypeID">任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strValue)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strValue);
        //委托类别
        string strContractType = new TSysDictLogic().GetDictNameByDictCodeAndType(objTask.CONTRACT_TYPE, "Contract_Type");
        if (!string.IsNullOrEmpty(strContractType))
        {
            return strContractType;
        }
        else//环境质量类别处理
        {
            DataTable dtSubtask = new TMisMonitorSubtaskLogic().getMonitorByTask(strValue);
            if (dtSubtask.Rows.Count > 0)
            {
                string strEnvType = new TSysDictLogic().GetDictNameByDictCodeAndType(dtSubtask.Rows[0][TMisMonitorSubtaskVo.MONITOR_ID_FIELD].ToString(), "EnvTypes");
                if (!string.IsNullOrEmpty(strEnvType))
                {
                    return strEnvType;
                }
            }
        }
        return "";
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
    /// 获取任务状态
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getTaskStatusName(string strValue)
    {
        string strTaskStatusName = "";
        switch (strValue)
        {
            case "01":
                strTaskStatusName = "采样环节";
                break;
            case "02":
                strTaskStatusName = "采样环节";
                break;
            case "03":
                strTaskStatusName = "分析环节";
                break;
            case "09":
                strTaskStatusName = "报告环节";
                break;
            case "11":
                strTaskStatusName = "已办结";
                break;
        }
        return strTaskStatusName;
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

        //单号
        string strTicketNum = !string.IsNullOrEmpty(Request.QueryString["srhTicketNum"]) ? Request.QueryString["srhTicketNum"].ToString() : "";
        //项目名称
        string strProjectName = !string.IsNullOrEmpty(Request.QueryString["srhProjectName"]) ? Request.QueryString["srhProjectName"].ToString() : "";
        string strYear = !string.IsNullOrEmpty(Request.QueryString["srhYear"]) ? Request.QueryString["srhYear"].ToString() : "";
        //合同号
        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        objTask.SORT_FIELD = strSortname;
        objTask.SORT_TYPE = strSortorder;
        objTask.TICKET_NUM = strTicketNum;
        objTask.PROJECT_NAME = strProjectName;
        objTask.CONTRACT_YEAR = strYear;
        //objTask.CONTRACT_ID = !string.IsNullOrEmpty(Request.QueryString["contract_id"]) ? Request.QueryString["contract_id"].ToString() : "";

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
}