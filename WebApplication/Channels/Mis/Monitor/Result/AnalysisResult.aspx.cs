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

/// <summary>
/// 功能描述：分析结果录入
/// 创建日期：2012-12-05
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_AnalysisResult : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo();
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getAnalysisByItemId")
            {
                strResult = getAnalysisByItemId(Request["strItemId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strResultId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //样品发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strSimpleId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        DataTable dt = new TMisMonitorResultLogic().getSimpleCodeInResult(LogInfo.UserInfo.ID, "02", "0");
        int intTotalCount = new TMisMonitorResultLogic().getSimpleCodeInResultCount(LogInfo.UserInfo.ID, "02", "0");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        DataTable dt = new TMisMonitorResultLogic().getResultInResultFlow(strOneGridId, LogInfo.UserInfo.ID, "02");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室质控信息
    /// </summary>
    /// <param name="strTwoGridId"></param>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo(strTwoGridId, "1");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 根据监测项目获取分析方法
    /// </summary>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getAnalysisByItemId(string strItemId)
    {
        DataTable dt = new TMisMonitorResultLogic().getAnalysisByItemId(strItemId);
        return DataTableToJson(dt);
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
            spit = ",";
        }
        return strQcName;
    }
    /// <summary>
    /// 保存监测项目信息
    /// </summary>
    /// <param name="id">分析结果ID</param>
    /// <param name="strItemResult">分析结果</param>
    /// <param name="strAnalysisMethod">分析方法</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemInfo(string id, string strItemResult, string strAnalysisMethod)
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = id;
        TMisMonitorResultVo.ITEM_RESULT = strItemResult == "" ? "###" : strItemResult;
        TMisMonitorResultVo.ANALYSIS_METHOD_ID = strAnalysisMethod;
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);

        TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
        TMisMonitorResultAppVo.RESULT_ID = id;
        string strResultAppId = new TMisMonitorResultAppLogic().Details(TMisMonitorResultAppVo).ID;

        TMisMonitorResultAppVo TMisMonitorResultAppVoTemp = new TMisMonitorResultAppVo();
        TMisMonitorResultAppVoTemp.ID = strResultAppId;
        TMisMonitorResultAppVoTemp.FINISH_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        new TMisMonitorResultAppLogic().Edit(TMisMonitorResultAppVoTemp);

        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 退回监测项目
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    public string GoToBack(string strResultId)
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = strResultId;
        TMisMonitorResultVo.TASK_TYPE = "退回";
        TMisMonitorResultVo.RESULT_STATUS = "01";
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 样品发送
    /// </summary>
    /// <param name="strSimpleId">样品ID</param>
    /// <returns></returns>
    public string SendToNext(string strSimpleId)
    {
        bool isSuccess = new TMisMonitorResultLogic().ResultSendToNext(strSimpleId, LogInfo.UserInfo.ID, "02", "03");
        return isSuccess == true ? "1" : "0";
    }
}