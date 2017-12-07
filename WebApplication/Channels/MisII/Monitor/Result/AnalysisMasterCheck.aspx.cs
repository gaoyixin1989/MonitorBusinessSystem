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
/// <summary>
/// 功能描述：质控审核、质量负责人审核
/// 创建日期：2015-01-29
/// 创建人  ：魏林
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisMasterCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["type"] != null && Request["type"].ToString() == "UpadteEnvData")
            {
                var workID = Request.QueryString["WorkIDs"];

                TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
                objTaskVo.CCFLOW_ID1 = workID;
                objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);

                if (objTaskVo.TASK_TYPE == "1")
                {
                    DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = objTaskVo.ID });
                    //如果是环境质量将自动对数据进行填报
                    foreach (DataRow row in objTable.Rows)
                    {
                        string strSubTaskId = row["ID"].ToString();
                        string strMonitorId = row["MONITOR_ID"].ToString();
                        string strAskDate = row["SAMPLE_ASK_DATE"].ToString();
                        string strSampleDate = row["SAMPLE_FINISH_DATE"].ToString();
                        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
                        TMisMonitorSubtaskVo.ID = strSubTaskId;
                        TMisMonitorSubtaskVo.MONITOR_ID = strMonitorId;
                        TMisMonitorSubtaskVo.SAMPLE_ASK_DATE = strAskDate;
                        TMisMonitorSubtaskVo.SAMPLE_FINISH_DATE = strSampleDate;
                        if (strMonitorId == "EnvRiver" || strMonitorId == "EnvReservoir" || strMonitorId == "EnvDrinking" || strMonitorId == "EnvDrinkingSource" || strMonitorId == "EnvStbc" || strMonitorId == "EnvMudRiver" || strMonitorId == "EnvPSoild" || strMonitorId == "EnvSoil" || strMonitorId == "EnvAir" || strMonitorId == "EnvSpeed" || strMonitorId == "EnvDust" || strMonitorId == "EnvRain")
                        {
                            new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, false, objTaskVo.SAMPLE_SEND_MAN);
                        }
                        if (strMonitorId == "AreaNoise" || strMonitorId == "EnvRoadNoise" || strMonitorId == "FunctionNoise")
                        {
                            new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, true, objTaskVo.SAMPLE_SEND_MAN);
                        }
                    }

                }
                Response.Write("true");
                Response.ContentType = "text/plain";
                Response.End();
            }
            if (Request.QueryString["WorkID"] != null)
            {
                var workID = Convert.ToInt64(Request.QueryString["WorkID"]);

                TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
                objTaskVo.CCFLOW_ID1 = workID.ToString();
                objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);

                this.TASK_ID.Value = objTaskVo.ID;
                this.PLAN_ID.Value = objTaskVo.PLAN_ID;
            }
            if (Request.QueryString["WorkIDs"] != null)
            {
                string strWorkIDs = Request.QueryString["WorkIDs"];
                string[] strarr = strWorkIDs.Split(',');
                for (int i = 0; i < strarr.Length; i++)
                {
                    if (strarr[i] != "" || !string.IsNullOrEmpty(strarr[i]))
                    {

                        Int64 workID = Convert.ToInt64(strarr[i]);

                        DataTable  dt= new TMisMonitorTaskLogic().Details_One(workID);
                        this.TASK_ID.Value = this.TASK_ID.Value + "," + dt.Rows[0]["ID"].ToString();
                        this.PLAN_ID.Value = this.PLAN_ID.Value + "," + dt.Rows[0]["PLAN_ID"].ToString();
                    }
                }
            }

            //定义结果
            string strResult = "";
            
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
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getSampleGridInfo(string strTaskID)
    {
        //string strSortname = Request.Params["sortname"];
        //string strSortorder = Request.Params["sortorder"];
        ////当前页面
        //int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        ////每页记录数
        //int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new DataTable();
        //int intTotalCount = 0;

        //dt = new TMisMonitorResultLogic().getTaskSampleInfo(strTaskID);

        //string strJson = CreateToJson(dt, dt.Rows.Count);
        //return strJson;

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
            //DataTable dt1 = new TMisMonitorResultLogic().Details1(strTaskID);
            //string strResultID1 = "";
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    strResultID1 = strResultID1 + "," + dt1.Rows[i]["SAMPLE_ID"].ToString();
            //}
            //strResultID1 = strResultID1.Substring(strResultID1.IndexOf(",") + 1);
            //strResultID1 = "'" + strResultID1 + "'";
            //strResultID1 = strResultID1.Replace(",", "','");
            dt = new TMisMonitorResultLogic().getTaskSampleInfo_One(strTaskID);
        }
        else
        {
            //objResultVo.ID = strResultID;
            //objResultVo = new TMisMonitorResultLogic().Details(objResultVo);
            //dt = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo() { ID = objResultVo.SAMPLE_ID });
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
        //string strSortname = Request.Params["sortname"];
        //string strSortorder = Request.Params["sortorder"];
        ////当前页面
        //int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        ////每页记录数
        //int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo(strSampleId);

        //string strJson = CreateToJson(dt, dt.Rows.Count);
        //return strJson;

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
            //dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_MAS(strResultID);
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
        //DataTable dtInner = new TMisMonitorResultLogic().getQcDetailInfo(strThreeGridId, "1");
        DataTable objtTable = dtOuter;
        //objtTable.Merge(dtInner);
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