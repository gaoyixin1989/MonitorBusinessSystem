using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

using WebApplication;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI;

/// <summary>
/// 功能描述：质控审核
/// 创建日期：2015-09-24
/// 创建人  ：黄进军
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisDataCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["WorkID"] != null)
            {
                var workid = Convert.ToInt64(Request.QueryString["WorkID"]); //获取主流程ID
                //var workid = CCFlowFacade.GetFatherIDOfSubFlow(LogInfo.UserInfo.USER_NAME, FID);

                TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                objPlanVo.CCFLOW_ID1 = workid.ToString();
                objPlanVo = new TMisContractPlanLogic().Details(objPlanVo);

                if (objPlanVo.ID.Length > 0 && objPlanVo.REAMRK1 == "1")
                {
                    //当前流程属于送样的
                    this.TASK_ID.Value = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    this.SUBTASK_ID.Value = "";
                }
                else
                {
                    //当前流程属于采样的
                    this.TASK_ID.Value = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    TMisMonitorSubtaskVo sub = new TMisMonitorSubtaskVo();
                    sub.TASK_ID = this.TASK_ID.Value;
                    DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(sub);
                    this.SUBTASK_ID.Value = dt.Rows[0]["ID"].ToString();
                    
                }
            }

            //定义结果
            string strResult = "";

            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo(Request["strTaskID"].ToString(), Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getSampleGridInfo")
            {
                strResult = getSampleGridInfo(Request["strTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getItemGridInfo")
            {
                strResult = getItemGridInfo(Request["SampleGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取现场质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getQCGrid1Info")
            {
                strResult = getQCGrid1Info(Request["ResultId"].ToString(), Request["strItemID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getQCGrid2Info")
            {
                strResult = getQCGrid2Info(Request["ResultId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            
        }
    }

    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strTaskID, string strSubTaskID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo_MAS(strTaskID, strSubTaskID, intPageIndex, intPageSize);

        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount_MAS(strTaskID, strSubTaskID);
        //string strJson = CreateToJson(dt, intTotalCount);
        //return strJson;
        //DataTable dt = new DataTable();
        //int intTotalCount = 0;
        //dt = new TMisMonitorResultLogic().getTaskQcCheckInfo_QY(LogInfo.UserInfo.ID, "analysis_result_check", "03", "50", intPageIndex, intPageSize);
        //intTotalCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_QY(LogInfo.UserInfo.ID, "analysis_result_check", "03", "50");

        //退回意见
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //分析审核退回到复核的意见
            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_009;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_011;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] = objReturnInfoVo.SUGGESTION;
        }

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getSampleGridInfo(string strTaskID)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new DataTable();
        int intTotalCount = 0;
        if (strTaskID.IndexOf(",") >= 0)
        {
            strTaskID = strTaskID.Substring(strTaskID.IndexOf(",") + 1);
            strTaskID = "'" + strTaskID + "'";
            strTaskID = strTaskID.Replace(",", "','");
            dt = new TMisMonitorResultLogic().getTaskSampleInfo_One(strTaskID);
        }
        else
        {
            dt = new TMisMonitorResultLogic().getTaskSampleInfo(strTaskID);
        }
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;

    }
    /// <summary>2
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getItemGridInfo(string strSampleId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();

        if (strSampleId.IndexOf(",") >= 0)
        {
            strSampleId = strSampleId.Substring(strSampleId.IndexOf(",") + 1);
            strSampleId = "'" + strSampleId + "'";
            strSampleId = strSampleId.Replace(",", "','");
            dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_One(strSampleId);
        }
        else
        {
            dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo(strSampleId);

        }
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;

    }
    /// <summary>
    /// 获取现场质控信息
    /// </summary>
    /// <param name="strThreeGridId"></param>
    /// <returns></returns>
    private string getQCGrid1Info(string strResultId, string strItemID)
    {
        TMisMonitorResultVo objResultVo = new TMisMonitorResultLogic().Details(strResultId);
        TMisMonitorSampleInfoVo objSampleVo = new TMisMonitorSampleInfoLogic().Details(objResultVo.SAMPLE_ID);

        DataTable dtOuter = new TMisMonitorResultLogic().getQcDetailInfo_QY(strResultId, "0", objSampleVo.SUBTASK_ID, strItemID);
        DataTable objtTable = dtOuter;
        string strJson = CreateToJson(objtTable, objtTable.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室质控信息
    /// </summary>
    /// <param name="strThreeGridId"></param>
    /// <returns></returns>
    private string getQCGrid2Info(string strResultId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo_QY(strResultId, "1", "", "");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    

    /// <summary>
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strTaskId, string strCompanyId)
    {
        if (strCompanyId == "") return "";
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    /// <summary>
    /// 获取默认分析负责人名称
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取默认分析协同人信息
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExName(string strUserId)
    {
        if (strUserId.Trim() == "") return "";
        List<string> list = strUserId.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        return strSumUserExName;
    }
    /// <summary>
    /// 获取质控手段名称
    /// </summary>
    /// <param name="strQcId">质控手段编码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcName(string strQcId)
    {
        string strQcName = "";

        if (strQcId == "") return "";
        List<string> list = strQcId.Split(',').ToList();
        string spit = "";
        foreach (string strQc in list)
        {
            if (strQc == "0")
                strQcName = strQcName + spit + "原始样";
            if (strQc == "1")
                strQcName = strQcName + spit + "现场空白";
            if (strQc == "2")
                strQcName = strQcName + spit + "现场加标";
            if (strQc == "3")
                strQcName = strQcName + spit + "现场平行";
            if (strQc == "4")
                strQcName = strQcName + spit + "实验室密码平行";
            if (strQc == "5")
                strQcName = strQcName + spit + "实验室空白";
            if (strQc == "6")
                strQcName = strQcName + spit + "实验室加标";
            if (strQc == "7")
                strQcName = strQcName + spit + "实验室明码平行";
            if (strQc == "8")
                strQcName = strQcName + spit + "标准样";
            if (strQc == "11")
                strQcName = strQcName + spit + "现场密码";
            spit = ",";
        }
        return strQcName;
    }
    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }
    
}