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
/// <summary>
/// 功能描述：分析室主任审核
/// 创建日期：2015-01-21
/// 创建人  ：魏林
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisMasterQcCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["DirectionType"]))
            {
                var type = Request.QueryString["DirectionType"];
                var direction = Request.QueryString["Direction"];
                
                var workID = Request.QueryString["WorkId"];

                TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
                objTaskVo.CCFLOW_ID1 = workID;
                objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
                switch (type)
                {
                    case "type1":

                        bool isHasOuterQcSample = new TMisMonitorResultLogic().CheckTaskHasOuterQcSample(objTaskVo.ID, "");

                        if (direction == "d1")
                        {
                            if (isHasOuterQcSample)
                            {
                                Response.Write("1");
                            }
                            else
                            {
                                Response.Write("0");
                            }
                        }
                        else if (direction == "d2")
                        {
                            if (!isHasOuterQcSample)
                            {
                                Response.Write("1");
                            }
                            else
                            {
                                Response.Write("0");
                            }
                        }

                        Response.End();
                        break;
                    default:
                        Response.Write("0");
                        Response.End();
                        break;

                }
            }
            if (Request.QueryString["WorkIDs"] != null || Request.QueryString["WorkID"] != null)
            {
                string strWorkIDs = Request.QueryString["WorkIDs"];
                if (string.IsNullOrEmpty(strWorkIDs))
                    strWorkIDs = Request.QueryString["WorkID"];

                string[] strarr = strWorkIDs.Split(',');
                for (int i = 0; i < strarr.Length; i++)
                {
                    if (strarr[i] != "" || !string.IsNullOrEmpty(strarr[i]))
                    {

                        var workID = Convert.ToInt64(strarr[i]);

                        var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID);

                        this.RESULT_ID.Value = this.RESULT_ID.Value + "," + identification;
                    }
                }
            }
            //定义结果
            string strResult = "";
            
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getSampleGridInfo")
            {
                strResult = getSampleGridInfo(Request["strResultID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getItemGridInfo")
            {
                strResult = getItemGridInfo(Request["SampleGridId"].ToString(), Request["strResultID"].ToString());
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
    private string getSampleGridInfo(string strResultID)
    {
        //string strSortname = Request.Params["sortname"];
        //string strSortorder = Request.Params["sortorder"];
        ////当前页面
        //int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        ////每页记录数
        //int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new DataTable();
        //int intTotalCount = 0;

        //TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        //objResultVo.ID = strResultID;
        //objResultVo = new TMisMonitorResultLogic().Details(objResultVo);
        //dt = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo() { ID = objResultVo.SAMPLE_ID });
        
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
        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        if (strResultID.IndexOf(",") >= 0)
        {
            strResultID = strResultID.Substring(strResultID.IndexOf(",") + 1);
            strResultID = "'" + strResultID + "'";
            strResultID = strResultID.Replace(",", "','");
            DataTable dt1 = new TMisMonitorResultLogic().Details1(strResultID);
            string strResultID1 = "";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                strResultID1 = strResultID1 + "," + dt1.Rows[i]["SAMPLE_ID"].ToString();
            }
            strResultID1 = strResultID1.Substring(strResultID1.IndexOf(",") + 1);
            strResultID1 = "'" + strResultID1 + "'";
            strResultID1 = strResultID1.Replace(",", "','");
            dt = new TMisMonitorSampleInfoLogic().SelectByTable1(strResultID1);
        }
        else
        {
            objResultVo.ID = strResultID;
            objResultVo = new TMisMonitorResultLogic().Details(objResultVo);
            dt = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo() { ID = objResultVo.SAMPLE_ID });
        }
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getItemGridInfo(string strSampleId, string strResultID)
    {
        //string strSortname = Request.Params["sortname"];
        //string strSortorder = Request.Params["sortorder"];
        ////当前页面
        //int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        ////每页记录数
        //int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_MAS(strResultID);
        
        //string strJson = CreateToJson(dt, dt.Rows.Count);
        //return strJson;

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new DataTable();

        if (strResultID.IndexOf(",") >= 0)
        {
            strResultID = strResultID.Substring(strResultID.IndexOf(",") + 1);
            strResultID = "'" + strResultID + "'";
            strResultID = strResultID.Replace(",", "','");
            dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_MAS_ONE(strResultID, strSampleId);
        }
        else
        {
            dt = new TMisMonitorResultLogic().getTaskItemQcCheckInfo_MAS(strResultID);

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

    /// <summary>
    /// 根据样品获取任务ID
    /// </summary>
    /// <param name="strQcId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getTaskID(string strSubtask)
    {
        string strTaskID = "";
        DataTable dt = new TMisMonitorResultLogic().getTaskID(strSubtask);
        if(dt.Rows.Count>0){
            strTaskID=dt.Rows[0]["TASK_ID"].ToString();
        }
        return strTaskID;
    }
    /// <summary>
    /// 获取监测项目
    /// </summary>
    /// <param name="strSubtask"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemId(string strSubtask)
    {
        string strTaskID = "";
        DataTable dt = new TMisMonitorResultLogic().getItemId(strSubtask);
        if (dt.Rows.Count > 0)
        {
            strTaskID = dt.Rows[0]["ITEM_ID"].ToString();
        }
        return strTaskID;
    }


}