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
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
/// <summary>
/// 功能描述：分析任务分配
/// 创建日期：2012-11-29
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QHD_AnalysisTaskAllocation : PageBase
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
            //监测项目类别
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //样品号信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //分配情况
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["oneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //判断是否能退回
            if (Request["type"] != null && Request["type"].ToString() == "IsCanGoToBack")
            {
                strResult = IsCanGoToBack(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strTaskId"].ToString(), Request["strSuggestion"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strTaskId"].ToString());
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
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getTaskInfo(LogInfo.UserInfo.ID, "duty_other_analyse", "03", "'01','00'", intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "duty_other_analyse", "03", "'01','00'");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo(LogInfo.UserInfo.ID, "duty_other_analyse", strOneGridId, "03", "'01','00'");
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }

    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getSimpleCodeInAlloction_QHD(strTwoGridId, LogInfo.UserInfo.ID, "'01','00'", "0", "", "");
        string strJson = CreateToJson(dt, 0);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getItemInfoInAlloction_QHD(strThreeGridId, "'01','00'", "0", "", "", intPageIndex, intPageSize);

        //退回意见
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorResultVo objResutlVo = new TMisMonitorResultLogic().Details(dt.Rows[i]["ID"].ToString());
            TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(objResutlVo.SAMPLE_ID);
            TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(objSampleInfoVo.SUBTASK_ID);

            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = objSubtaskVo.TASK_ID;
            objReturnInfoVo.SUBTASK_ID = objSubtaskVo.ID;
            objReturnInfoVo.RESULT_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_007;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_006;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK_1"] = objReturnInfoVo.SUGGESTION;

            //获取原始记录表的采样编号
            DataTable dtInfo = new DataTable();
            DataRow[] drInfo = dtInfo.Select("1=2");
            string strFiter_Code = "";
            TMisMonitorDustinforVo objDustinforVo = new TMisMonitorDustinforVo();
            objDustinforVo.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
            objDustinforVo = new TMisMonitorDustinforLogic().Details(objDustinforVo);
            if (objDustinforVo.ID.Length > 0)
            {
                TMisMonitorDustattributeVo objDustattributeVo = new TMisMonitorDustattributeVo();
                objDustattributeVo.BASEINFOR_ID = objDustinforVo.ID;
                dtInfo = new TMisMonitorDustattributeLogic().SelectByTable(objDustattributeVo);
                if (dtInfo.Rows.Count > 0)
                {
                    drInfo = dtInfo.Select("FITER_CODE<>'平均'");
                }
                else
                {
                    TMisMonitorDustattributeSo2ornoxVo objDustattributeSo2ornoxVo = new TMisMonitorDustattributeSo2ornoxVo();
                    objDustattributeSo2ornoxVo.BASEINFOR_ID = objDustinforVo.ID;
                    dtInfo = new TMisMonitorDustattributeSo2ornoxLogic().SelectByTable(objDustattributeSo2ornoxVo);
                    if (dtInfo.Rows.Count > 0)
                    {
                        drInfo = dtInfo.Select("FITER_CODE<>'平均'");
                    }
                    else {
                        TMisMonitorDustattributePmVo DustattributePmVo = new TMisMonitorDustattributePmVo();
                        DustattributePmVo.BASEINFOR_ID = objDustinforVo.ID;
                        dtInfo = new TMisMonitorDustattributePmLogic().SelectByTable(DustattributePmVo);
                        if (dtInfo.Rows.Count > 0)
                        {
                            drInfo = dtInfo.Select("FITER_CODE<>'平均'");
                        }
                    }
                }
                for (int j = 0; j < drInfo.Length; j++)
                {
                    strFiter_Code += drInfo[j]["FITER_CODE"].ToString() + ",";
                }
                dt.Rows[i]["REMARK_2"] = strFiter_Code.TrimEnd(',');
            }
        }

        int intTotalCount = new TMisMonitorResultLogic().getItemInfoCountInAlloction_QHD(strThreeGridId, "'01','00'", "0", "", "");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 退回之前判断任务是否可以回退
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string IsCanGoToBack(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().IsCanGoToBack(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse", "'01','00'");
        return IsCanGoToBack == true ? "1" : "0";
    }
    /// <summary>
    /// 退回到上一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string GoToBack(string strTaskId, string strSuggestion)
    {
        bool IsSuccess = new TMisMonitorResultLogic().subTaskGoToBackEx(strTaskId, "03", LogInfo.UserInfo.ID, "021", "duty_other_analyse");

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = strTaskId;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_006;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_005;
        TMisReturnInfoVo obj = new TMisReturnInfoLogic().Details(objReturnInfoVo);
        if (obj.ID.Length > 0)
        {
            objReturnInfoVo.ID = obj.ID;
            objReturnInfoVo.SUGGESTION = strSuggestion;
            IsSuccess = new TMisReturnInfoLogic().Edit(objReturnInfoVo);
        }
        else
        {
            objReturnInfoVo.ID = GetSerialNumber("t_mis_return_id");
            objReturnInfoVo.SUGGESTION = strSuggestion;
            IsSuccess = new TMisReturnInfoLogic().Create(objReturnInfoVo);
        }

        return IsSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 发送到下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strTaskId)
    {
        bool IsSuccess = new TMisMonitorResultLogic().sendToNextFlow(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse", "03", "'01','00'", "20");
        return IsSuccess == true ? "1" : "0";
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
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }
    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <param name="strMonitorTypeId">用户Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
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
    /// 获取获取分析负责人信息
    /// </summary>
    /// <param name="strResultId">结果ＩＤ</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strResultId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(string));
        dt.Columns.Add("UserName", typeof(string));

        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return DataTableToJson(dt);

        string strUserId = objTable.Rows[0]["HEAD_USERID"].ToString();
        if (strUserId == "") return DataTableToJson(dt);

        //将获取用户ID信息转换成用户名称进行返回
        string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
        DataRow row = dt.NewRow();
        row["UserId"] = strUserId;
        row["UserName"] = strUserName;
        dt.Rows.Add(row);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取获取分析协人信息
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExName(string strResultId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(string));
        dt.Columns.Add("UserName", typeof(string));

        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return DataTableToJson(dt);

        string strUserExIds = objTable.Rows[0]["ASSISTANT_USERID"].ToString();
        if (strUserExIds == "") return DataTableToJson(dt);

        List<string> list = strUserExIds.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        DataRow row = dt.NewRow();
        row["UserId"] = strUserExIds;
        row["UserName"] = strSumUserExName;
        dt.Rows.Add(row);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取分析完成时间
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAskingDate(string strResultId)
    {
        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return "";
        string strAskingDate = objTable.Rows[0]["ASKING_DATE"].ToString();
        if (strAskingDate != "")
            strAskingDate = DateTime.Parse(strAskingDate).ToString("yyyy-MM-dd");
        return strAskingDate;
    }

    /// <summary>
    /// 根据默认负责人获取已经分配的项目信息
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedDefaultItemName(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItem(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumItemName = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
            spit = ",";
        }
        return strSumItemName;
    }
    /// <summary>
    /// 根据默认协同人获取已经分配的项目信息
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedDefaultItemNameEx(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItemEx(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumItemName = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
            spit = ",";
        }
        return strSumItemName;
    }

    /// <summary>
    /// 根据默认协同人获取已经分配的样品号
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedSampleCode(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedSampleCode(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumSampleCode = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumSampleCode = strSumSampleCode + spit + row["SAMPLE_CODE"].ToString();
            spit = ",";
        }
        return strSumSampleCode;
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