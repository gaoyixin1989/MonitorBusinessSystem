using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;

/// <summary>
/// 功能描述：采样-现状信息
/// 创建日期：2012-12-12
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_MisII_sampling_SampleLocale : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();

                this.PlanID.Value = getPlanID();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getWeatherInfo")
            {
                strResult = GetWeatherInfo();
                Response.Write(strResult);
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getWeatherValue")
            {
                strResult = GetWeatherValue();
                Response.Write(strResult);
                Response.End();
            }
            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
            //获取字典信息
            if (Request["type"] != null && Request["type"].ToString() == "GetDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo()
    {
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(this.SUBTASK_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

        TMisMonitorTaskCompanyVo objConCompany = new TMisMonitorTaskCompanyVo();
        objConCompany.TASK_ID = objTask.ID;
        objConCompany = new TMisMonitorTaskCompanyLogic().Details(objConCompany);
        objSubtask.MONITOR_ID = getMonitorTypeName(objSubtask.MONITOR_ID);
        objSubtask.SAMPLING_MANAGER_ID = new TSysUserLogic().Details(LogInfo.UserInfo.ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.CONTRACT_CODE;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.LINK_PHONE;
        //objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        //objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");

        return ToJson(objSubtask);
    }

    protected string getPlanID()
    {
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(this.SUBTASK_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

        return objTask.PLAN_ID;
    }


    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
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
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 根据监测类别获取天气项目
    /// </summary>
    /// <returns>Json</returns>
    protected string GetWeatherInfo()
    {
        string strDictType = "";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(this.SUBTASK_ID.Value);
        if (objSubtask.MONITOR_ID == "000000004")
            strDictType = "noise_weather";
        else
            strDictType = "gerenal_weather";

        TSysDictVo objDict = new TSysDictVo();
        objDict.DICT_TYPE = strDictType;
        objDict.SORT_FIELD = TSysDictVo.ORDER_ID_FIELD;
        DataTable dt = new TSysDictLogic().SelectByTable(objDict, 0, 0);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 根据任务ID获取天气信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetWeatherValue()
    {
        TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();
        objSampleSky.SUBTASK_ID = this.SUBTASK_ID.Value;
        DataTable dt = new TMisMonitorSampleSkyLogic().SelectByTable(objSampleSky);
        return DataTableToJson(dt);
    }

    #region 获取下拉字典项
    private string getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);

        return strJson;
    }
    #endregion

    /// <summary>
    /// 采样时修改采样日期与要求完成日期
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string updateDate(string strSubTaskID, string strAskDate, string strFinishDate)
    {
        TMisMonitorSubtaskVo SubTaskVo = new TMisMonitorSubtaskVo();
        SubTaskVo.ID = strSubTaskID;
        SubTaskVo.SAMPLE_ASK_DATE = strAskDate;
        SubTaskVo.SAMPLE_FINISH_DATE = strFinishDate;
        new TMisMonitorSubtaskLogic().Edit(SubTaskVo);
        return "1";
    }
}