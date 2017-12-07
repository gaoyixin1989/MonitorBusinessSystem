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
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
/// <summary>
/// 功能描述：分析类现场监测结果核录
/// 创建日期：2013-07-11
/// 创建人  ：胡方扬
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_SampleAnalysisResultCheck : PageBase
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
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString());
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
                strResult = getFourGridInfo(Request["threeGridId"].ToString());
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
            //监测项目退回
            if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
            {
                strResult = GoToBack(Request["strTaskId"].ToString(), Request["strSuggestion"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //判断是否能够发送
            if (Request["type"] != null && Request["type"].ToString() == "IsCanSendTaskCheckToNextFlow")
            {
                strResult = IsCanSendTaskCheckToNextFlow(Request["strTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strSubtaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //任务回退
            if (Request["type"] != null && Request["type"].ToString() == "GoToBackTask")
            {
                strResult = GoToBackTask(Request["strSubTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测结果发送
            if (Request["type"] != null && Request["type"].ToString() == "SendResultToNext")
            {
                strResult = SendResultToNext(Request["strResultId"].ToString());
                Response.Write(strResult);
                Response.End();
            }

            //按动态列更新列值
            if (Request["type"] != null && Request["type"].ToString() == "UpdateCellValue")
            {
                string strCellName = Request["strUpdateCell"].ToString();
                string strCellValue = Request["strUpdateCellValue"].ToString();
                string strInforId = Request["strInfor_Id"].ToString();
                Response.Write(UpdateCellValue(strCellName, strCellValue, strInforId));
                Response.End();
            }

            //按动态列更新列值
            if (Request["type"] != null && Request["type"].ToString() == "setResultStatus")
            {
                string strInforId = Request["strInfor_Id"].ToString();
                Response.Write(setResultStatus(strInforId));
                Response.End();
            }

            //任务发送(新)
            if (Request["type"] != null && Request["type"].ToString() == "SendToNextNew")
            {
                Response.Write(SendToNextNew(Request["strTaskId"].ToString()));
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取任务信息
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

        DataTable dt = new TMisMonitorTaskLogic().SelectSampleTaskForWithSampleAnalysisQY(LogInfo.UserInfo.ID, "sample_result_check", "'02','022'","0","50", intPageIndex, intPageSize);

        //退回意见
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //分析审核退回到复核的意见
            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_010;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] = objReturnInfoVo.SUGGESTION;
        }

        int intTotalCount = new TMisMonitorTaskLogic().SelectSampleTaskForWithSampleAnalysisCountQY(LogInfo.UserInfo.ID, "sample_result_check", "'02','022'", "0", "50");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId)
    {
        DataTable dt = new TMisMonitorTaskLogic().SelectSampleSubTaskForWithSampleAnalysisQY(strOneGridId);
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
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        //如果子任务是分支任务
        dt = new TMisMonitorResultLogic().GetSampleAnalysisSample(strTwoGridId, intPageIndex, intPageSize);
        intTotalCount = new TMisMonitorResultLogic().GetSampleAnalysisSampleCount(strTwoGridId);
        //string strRemark = new TMisMonitorSubtaskLogic().Details(strTwoGridId).REMARK1;
        //if (!string.IsNullOrEmpty(strRemark))
        //{
        //    dt = new TMisMonitorSampleInfoLogic().getSamplingForSampleItem(strTwoGridId, "022", intPageIndex, intPageSize);
        //    intTotalCount = new TMisMonitorSampleInfoLogic().getSamplingCountForSampleItem(strTwoGridId, "022");
        //}
        //else
        //{
        //    dt = new TMisMonitorSampleInfoLogic().getSamplingForSampleItemOne(strTwoGridId, "022", intPageIndex, intPageSize);
        //    intTotalCount = new TMisMonitorSampleInfoLogic().getSamplingCountForSampleItemOne(strTwoGridId, "022");
        //}
        string strJson = CreateToJson(dt, intTotalCount);
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
        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        objResultVo.SAMPLE_ID = strThreeGridId;
        objResultVo.QC_TYPE = "0";
        objResultVo.RESULT_STATUS = "50";
        DataTable dt = new TMisMonitorResultLogic().GetSampleAnalysisResult(objResultVo);
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

    public string IsCanSendTaskCheckToNextFlow(string strTaskId)
    {
        bool IsCanGoToBack = new TMisMonitorResultLogic().IsCanSendTaskCheckToNextFlow(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse_result", "03");
        return IsCanGoToBack == true ? "1" : "0";
    }
    /// <summary>
    /// 将任务发送至下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strSubtaskId)
    {
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskVo();
        objSubtaskVo.ID = strSubtaskId;
        objSubtaskVo.TASK_STATUS = "023";
        objSubtaskVo.TASK_TYPE = "发送";
        bool isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtaskVo);
        return isSuccess == true ? "1" : "0";
    }

    public bool SendToNextNew(string strTaskId)
    {
        bool isSuccess = true;
        bool IsAnyscene = new TMisMonitorSubtaskLogic().isExistAnyscene(strTaskId, "");
        //如果废气子任务中不存在分析类项目只有分析现场类项目
        if (!IsAnyscene)
        {
            isSuccess = new TMisMonitorResultLogic().SendQcTaskToNextFlowForQy(strTaskId, LogInfo.UserInfo.ID, "09", "analysis_result_check");
        }
        if (isSuccess)
            isSuccess = new TMisMonitorTaskLogic().SetAnysceneItem(strTaskId, "02");

        return isSuccess;
    }

    /// <summary>
    /// 将任务回退到上一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string GoToBackTask(string strSubTaskId)
    {
        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskVo();
        objSubTaskVo.ID = strSubTaskId;
        objSubTaskVo.TASK_STATUS = "122";//退回状态
        objSubTaskVo.TASK_TYPE = "退回";
        bool isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 将结果数据发送至下一环节
    /// </summary>
    /// <param name="strResultId">监测结果Id</param>
    /// <returns></returns>
    public string SendResultToNext(string strResultId)
    {
        bool isSuccess = new TMisMonitorResultLogic().SendResultToNext_QHD(strResultId, "04");
        return isSuccess == true ? "1" : "0";
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
    /// 退回监测项目
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    public string GoToBack(string strTaskId, string strSuggestion)
    {
        bool isSuccess = false;

        isSuccess = new TMisMonitorResultLogic().sendTaskQcCheckInfoToNext_QY(strTaskId, "02','022','03", "50", "30", "退回", "1");
        if (isSuccess)
        {
            TMisMonitorSubtaskVo objSetVo = new TMisMonitorSubtaskVo();
            objSetVo.TASK_STATUS = "03";
            TMisMonitorSubtaskVo objWhereVo = new TMisMonitorSubtaskVo();
            objWhereVo.TASK_ID = strTaskId;
            objWhereVo.TASK_STATUS = "09";
            new TMisMonitorSubtaskLogic().Edit(objSetVo, objWhereVo);
        }

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = strTaskId;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_010;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_008;
        TMisReturnInfoVo obj = new TMisReturnInfoLogic().Details(objReturnInfoVo);
        if (obj.ID.Length > 0)
        {
            objReturnInfoVo.ID = obj.ID;
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Edit(objReturnInfoVo);
        }
        else
        {
            objReturnInfoVo.ID = GetSerialNumber("t_mis_return_id");
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Create(objReturnInfoVo);
        }

        return isSuccess == true ? "1" : "0";
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

    /// <summary>
    /// 创建原因：动态更新指定列的列值
    /// 创建人：胡方扬
    /// 创建日期：2013-07-11
    /// </summary>
    /// <param name="strCellName"></param>
    /// <param name="strCellValue"></param>
    /// <param name="strInfor_id"></param>
    /// <returns></returns>
    public bool UpdateCellValue(string strCellName,string strCellValue,string strInfor_id) {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strInfor_id) && !String.IsNullOrEmpty(strCellName)) {
            blFlag = new TMisMonitorResultLogic().UpdateCellValue(strCellName,strCellValue,strInfor_id);
        }

        return blFlag;
    }

    /// <summary>
    /// 创建原因：更新结果状态
    /// 创建人：胡方扬
    /// 创建日期：2013-07-11
    /// </summary>
    /// <param name="strInfor_id"></param>
    /// <returns></returns>
    public bool setResultStatus(string strInfor_id)
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strInfor_id))
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.ID = strInfor_id;
            //objResult.RESULT_STATUS = "01";
            objResult.RESULT_STATUS = "02";
            blFlag = new TMisMonitorResultLogic().Edit(objResult);
        }
        return blFlag;
    }
}