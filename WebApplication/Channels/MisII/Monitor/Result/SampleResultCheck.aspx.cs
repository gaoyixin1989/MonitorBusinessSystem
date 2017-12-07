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
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.DataAccess.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Sys.Duty;
using WebApplication;
/// <summary>
/// 功能描述：现场监测结果复核
/// 创建日期：2013-3-14
/// 创建人  ：邵世卓
/// </summary>
public partial class Channels_MisII_Monitor_Result_SampleResultCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            if (Request.QueryString["WorkID"] != null)
            {
                var workID = Convert.ToInt64(Request.QueryString["WorkID"]);

                var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID);

                this.SUBTASK_ID.Value = identification;
            }
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo(Request["strSample"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString(), Request["strSample"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["strSample"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getFiveGridInfo")
            {
                strResult = getFiveGridInfo(Request["fourGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取现场复核人信息
            if (Request["type"] != null && Request["type"].ToString() == "GetCheckUser")
            {
                strResult = GetCheckUser(Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    private string GetCheckUser(string strSubTaskID)
    {
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        string strUserIDs = "";
        DataTable dt = new DataTable();
        DataTable strSampleCheckSender = GetDefaultOrFirstDutyUser("sample_result_check", objSubtaskVo.MONITOR_ID);
        for (int i = 0; i < strSampleCheckSender.Rows.Count; i++)
        {
            strUserIDs += strSampleCheckSender.Rows[i]["USERID"].ToString() + ",";
        }

        TSysUserVo SysUserVo = new TSysUserVo();
        SysUserVo.ID = strUserIDs.TrimEnd(',');
        SysUserVo.IS_DEL = "0";
        SysUserVo.IS_USE = "1";
        dt = new TSysUserLogic().SelectByTableEx(SysUserVo, 0, 0);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获得指定职责的默认负责人，如果不存在则取第一个
    /// </summary>
    /// <param name="strDutyType">职责编码</param>
    /// <param name="strMonitorType">监测类别</param>
    /// <returns></returns>
    private DataTable GetDefaultOrFirstDutyUser(string strDutyType, string strMonitorType)
    {
        DataTable dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorType);
        return dtDuty;
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strSample)
    {
        string strSubTaskID = Request["strSubTaskID"].ToString();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorTaskLogic().SelectSampleTaskForMAS(strSubTaskID, strSample == "1" ? true : false, intPageIndex, intPageSize);

        //huangjinjun add 2016.1.26
        TMisMonitorTaskVo tm =  new TMisMonitorTaskLogic().Details(dt.Rows[0]["ID"].ToString());
        if (tm.REMARK3 == "true")
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
        }
        else
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
        }

        int intTotalCount = new TMisMonitorTaskLogic().SelectSampleTaskCountForMAS(strSubTaskID, strSample == "1" ? true : false);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId, string strSample)
    {
        string strSubTaskID = Request["strSubTaskID"].ToString();
        DataTable dt = new TMisMonitorSubtaskLogic().SelectSampleSubTaskForMAS(strSubTaskID, strOneGridId, strSample == "1" ? true : false);

        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        var strCCflowWorkId = Request.QueryString["strCCflowWorkId"];
        var subFlowId = CCFlowFacade.GetFatherThreadIDOfSubFlow(LogInfo.UserInfo.USER_NAME, Convert.ToInt64(strCCflowWorkId));
        var subFlowIdentification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, subFlowId);

        var sampleIdList = subFlowIdentification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {

            sampleIdList.RemoveAt(0);
        }

        DataTable dt = new DataTable();
        int intTotalCount = 0;

        dt = new TMisMonitorSampleInfoLogic().SelectSampleSitePoint(strTwoGridId);
        intTotalCount = new TMisMonitorSampleInfoLogic().SelectSampleSitePointCount(strTwoGridId);

        var newDT = new DataTable();

        foreach (DataColumn column in dt.Columns)
        {
            newDT.Columns.Add(column.ColumnName);
        }

        foreach (DataRow row in dt.Rows)
        {

            if (sampleIdList.Count > 0 && !sampleIdList.Contains(row["ID"].ToString()))
            {
                intTotalCount--;
                continue;
            }

            newDT.Rows.Add(row.ItemArray);
        }

        //dt = new TMisMonitorSampleInfoLogic().getSamplingForSampleItemOne_MAS(strTwoGridId, intPageIndex, intPageSize);
        //intTotalCount = new TMisMonitorSampleInfoLogic().getSamplingCountForSampleItemOne_MAS(strTwoGridId);

        //dt = new TMisMonitorTaskPointLogic().SelectSampleDeptPoint(strTwoGridId);
        //intTotalCount = dt.Rows.Count;

        string strJson = CreateToJson(newDT, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId, string strSample)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        objResultVo.SAMPLE_ID = strThreeGridId;
        objResultVo.QC_TYPE = "0";
        //objResultVo.RESULT_STATUS = "01,02";
        DataTable dt = new TMisMonitorResultLogic().SelectSceneItemInfo_MAS(objResultVo, strSample);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取实验室内控信息
    /// </summary>
    /// <returns></returns>
    private string getFiveGridInfo(string strFourGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo(strFourGridId, "1");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }

    /// <summary>
    /// 根据监测类型获取岗位职责用户信息
    /// </summary>
    /// <param name="strMonitorType">监测类型Id</param>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getUserInfo(string strMonitorType)
    {
        //获取采样分配环节用户信息
        DataTable objTable = new TMisMonitorResultLogic().getUsersInfo("duty_sampling", strMonitorType, "");
        return DataTableToJson(objTable);
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
    /// 获取默认采样负责人名称
    /// </summary>
    /// <param name="strUserId">采样负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取默认采样协同人信息
    /// </summary>
    /// <param name="strUserId">采样负责人ID</param>
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
    /// 获取监测项目单位
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemUnit(string strItemID)
    {
        TBaseItemAnalysisVo objItemAna = new TBaseItemAnalysisVo();
        objItemAna.ITEM_ID = strItemID;
        objItemAna = new TBaseItemAnalysisLogic().Details(objItemAna);
        return getDictName(objItemAna.UNIT, "item_unit");
    }
}