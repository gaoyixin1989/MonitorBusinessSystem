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
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

/// <summary>
/// 功能描述：综合查询
/// 创建时间：2012-12-1
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_TaskTraking : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDict")
        {
            Response.Write(getDictJsonString(Request.QueryString["typeid"]));
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
        //获取工作流程信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getFlowInfo")
        {
            strResult = getFlowInfo(Request.QueryString["business_id"]);
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetTaskInfo")
            {
                strResult = getTaskInfoForJson();
                Response.Write(strResult);
                Response.End();
            }
        }
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
        //按监测类别动态返回流程图宽度
        //基数
        int intWidth = 500;
        //监测类别数
        int intSubtaskCount = new TMisMonitorSubtaskLogic().getMonitorCountByTask(strBusinessID);
        //限制最大为800
        intSubtaskCount = intSubtaskCount > 2 ? (intSubtaskCount = 2) : intSubtaskCount;
        return (intWidth * intSubtaskCount).ToString();
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