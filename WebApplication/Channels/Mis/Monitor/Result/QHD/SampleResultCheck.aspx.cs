﻿using System;
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
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
/// <summary>
/// 功能描述：现场审核、现场主任审核
/// 创建日期：2014-05-08
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Mis_Monitor_Result_QHD_SampleResultCheck : PageBase
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
                strResult = getOneGridInfo(Request["status"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString(), Request["status"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["status"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["status"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            
            //任务发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strTaskId"].ToString(), Request["status"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string status)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (status == "Check")
            status = "022";
        else if (status == "QcCheck")
            status = "023";
        else
            status = "022";
        DataTable dt = new TMisMonitorResultLogic().getSampleTaskCheck_QHD("03,021,02", status, intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getSampleTaskCheckCount_QHD("03,021,02", status);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strOneGridId"></param>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId, string status)
    {
        if (status == "Check")
            status = "022";
        else if (status == "QcCheck")
            status = "023";
        else
            status = "022";
        DataTable dt = new TMisMonitorResultLogic().getSampleItemCheckMonitorType_QHD(strOneGridId, "03,021,02", status);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId, string status)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (status == "Check")
            status = "022";
        else if (status == "QcCheck")
            status = "023";
        else
            status = "022";
        DataTable dt = new TMisMonitorResultLogic().getSampleCheckInfo_QHD(strTwoGridId, status, intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorResultLogic().getSampleCheckInfoCount_QHD(strTwoGridId, status);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId, string status)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (status == "Check")
            status = "022";
        else if (status == "QcCheck")
            status = "023";
        else
            status = "022";
        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        objResultVo.SAMPLE_ID = strThreeGridId;
        //objResultVo.QC_TYPE = "0";
        objResultVo.RESULT_STATUS = status;
        DataTable dt = new TMisMonitorResultLogic().SelectSceneItemInfo(objResultVo);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    
    /// <summary>
    /// 将任务发送至下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strTaskId, string status)
    {
        string strMsg = "";
        bool isSuccess = true;
        string strNextStatus = "";
        if (status == "Check")
        {
            status = "022";
            strNextStatus = "023";
        }
        else if (status == "QcCheck")
        {
            status = "023";
            strNextStatus = "50";
        }
        else
        {
            status = "022";
            strNextStatus = "023";
        }
        isSuccess = new TMisMonitorResultLogic().SendTaskSampleCheckToNext_QHD(strTaskId, status, strNextStatus);
        
        return isSuccess == true ? "{\"result\":\"1\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"0\",\"msg\":\"" + strMsg + "\"}";
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