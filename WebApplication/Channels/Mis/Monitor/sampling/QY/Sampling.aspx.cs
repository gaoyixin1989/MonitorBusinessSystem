using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Sys.Duty;

/// <summary>
/// 功能描述：采样任务
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_Mis_Monitor_sampling_QY_Sampling : PageBase
{
    public static string strId = "";
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
            }
            if (!string.IsNullOrEmpty(Request.QueryString["IS_BACK"]))
            {
                //是否退回的采样任务
                this.IS_BACK.Value = Request.QueryString["IS_BACK"].ToString();
            }

            //点位动态属性信息拷贝
            AttributeValueCopy();
            //样品编号
            //SetSampleCode();


            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
            //获取现场复核人信息
            if (Request["type"] != null && Request["type"].ToString() == "GetCheckUser")
            {
                strResult = GetCheckUser(Request.QueryString["MonitorID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    private string GetCheckUser(string strMonitorID)
    {
        string strUserIDs = "";
        DataTable dt = new DataTable();
        //TSysUserPostVo UserPostVo = new TSysUserPostVo();
        //UserPostVo.POST_ID = "000000030";
        //dt = new TSysUserPostLogic().SelectByTable(UserPostVo);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    strUserIDs += dt.Rows[i]["USER_ID"].ToString() + ",";
        //}
        DataTable strSampleCheckSender = GetDefaultOrFirstDutyUser("duty_other_sample", strMonitorID);
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
    /// 样品编号设置
    /// </summary>
    /// <returns></returns>
    protected void SetSampleCode()
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.SUBTASK_ID = this.SUBTASK_ID.Value;
        objSample.QC_TYPE = "0";
        objSample.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            objSample.QC_TYPE = "";
            objSample.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSample.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                dtSample.ImportRow(dr);
            }
        }

        for (int j = 0; j < dtSample.Rows.Count; j++)
        {
            if (dtSample.Rows[j]["SAMPLE_CODE"].ToString().Length > 0)
                continue;
            string[] strSampleCode = new string[2] { "S" + DateTime.Now.Year + DateTime.Now.Month, i3.View.PageBase.GetSerialNumber("monitor_samplecode") };
            objSample = new TMisMonitorSampleInfoVo();
            objSample.ID = dtSample.Rows[j]["ID"].ToString();
            objSample.SAMPLE_CODE = i3.View.PageBase.CreateSerialNumber(strSampleCode);

            new TMisMonitorSampleInfoLogic().Edit(objSample);
        }

    }

    /// <summary>
    /// 点位描述信息拷贝
    /// </summary>
    /// <returns></returns>
    protected void AttributeValueCopy()
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
        objTaskPoint.SUBTASK_ID = this.SUBTASK_ID.Value;
        DataTable dt = new TMisMonitorTaskPointLogic().SelectByTable(objTaskPoint);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseAttrbuteValueVo objAttrbuteValue = new TBaseAttrbuteValueVo();
            objAttrbuteValue.OBJECT_ID = dt.Rows[i]["POINT_ID"].ToString();

            TBaseAttrbuteValue3Vo objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
            objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();

            DataTable dtAtt3 = new TBaseAttrbuteValue3Logic().SelectByTable(objAttrbuteValue3);
            if (dtAtt3.Rows.Count > 0)
                return;

            DataTable dtAtt = new TBaseAttrbuteValueLogic().SelectByTable(objAttrbuteValue);
            for (int j = 0; j < dtAtt.Rows.Count; j++)
            {
                objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
                objAttrbuteValue3.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrbuteValue3.OBJECT_TYPE = dtAtt.Rows[j]["OBJECT_TYPE"].ToString();
                objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();
                objAttrbuteValue3.ATTRBUTE_CODE = dtAtt.Rows[j]["ATTRBUTE_CODE"].ToString();
                objAttrbuteValue3.ATTRBUTE_VALUE = dtAtt.Rows[j]["ATTRBUTE_VALUE"].ToString();
                objAttrbuteValue3.IS_DEL = dtAtt.Rows[j]["IS_DEL"].ToString();

                new TBaseAttrbuteValue3Logic().Create(objAttrbuteValue3);
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
        //objConCompany.TASK_ID = objTask.ID;
        objConCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);
        objSubtask.MONITOR_ID = getMonitorTypeName(objSubtask.MONITOR_ID);
        objSubtask.SAMPLING_MANAGER_ID = new TSysUserLogic().Details(objSubtask.SAMPLING_MANAGER_ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.CONTRACT_CODE;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.PHONE;
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_ASK_DATE))
        {
            objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        }
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_FINISH_DATE))
        {
            objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");
        }
        //获取退回意见信息
        TMisReturnInfoVo ReturnInfoVo = new TMisReturnInfoVo();
        ReturnInfoVo.TASK_ID = objSubtask.TASK_ID;
        if (this.SUBTASK_ID.Value == Request.QueryString["strSourceID"].ToString())
        {
            ReturnInfoVo.SUBTASK_ID = this.SUBTASK_ID.Value;
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_005;
        }
        else
        {
            ReturnInfoVo.SUBTASK_ID = Request.QueryString["strSourceID"].ToString();
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
        }
        ReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_002;
        ReturnInfoVo = new TMisReturnInfoLogic().Details(ReturnInfoVo);
        if (ReturnInfoVo.ID.Length > 0)
        {
            objSubtask.SAMPLING_METHOD = "退回意见：" + ReturnInfoVo.SUGGESTION;
        }

        return ToJson(objSubtask);
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
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strSubtaskID, string strSourceID, string strUserID, string strAttribute)
    {
        bool isSuccess = true;
        string strMsg = "";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        string strTaskID = objSubtask.TASK_ID;
        #region//判断是否存在样品没有设置项目的情况
        TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoVo();
        SampleInfoVo.SUBTASK_ID = strSubtaskID;
        List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();
        list = new TMisMonitorSampleInfoLogic().SelectByObject(SampleInfoVo, 0, 0);
        for (int i = 0; i < list.Count; i++)
        {
            TMisMonitorResultVo ResultVo = new TMisMonitorResultVo();
            ResultVo.SAMPLE_ID = list[i].ID;
            ResultVo = new TMisMonitorResultLogic().SelectByObject(ResultVo);
            if (ResultVo.ID.Length == 0)
            {
                isSuccess = false;
                strMsg = "样品[" + list[i].SAMPLE_NAME + "]还没设置监测项目";
                break;
            }
        }
        if (isSuccess == false)
        {
            return "[{result:'0',msg:'" + strMsg + "'}]";
        }
        #endregion
        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubtaskID);
        #region//判断现场监测项目是否填写结果值
        DataRow[] drSampleItem = dtSampleItem.Select("IS_ANYSCENE_ITEM='0'");
        for (int i = 0; i < drSampleItem.Length; i++)
        {
            if (drSampleItem[i]["ITEM_RESULT"].ToString().Length == 0)
            {
                isSuccess = false;
                strMsg = "现场项目[" + drSampleItem[i]["ITEM_NAME"].ToString() + "]还没填写结果值，请检查！";
                break;
            }
        }
        if (isSuccess == false)
        {
            return "[{result:'0',msg:'" + strMsg + "'}]";
        }
        #endregion
        TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
        //if (objSubtask.MONITOR_ID == "000000004" || objSubtask.MONITOR_ID == "000000005")
        if (objSubtask.MONITOR_ID == "000000005")
        {
            objSubtask.TASK_STATUS = "022";//噪声、辐射现场监测项目进行 现场项目审核流程
            objSubtask.REMARK1 = objSubtask.ID;
        }
        else
        {
            objSubtask.TASK_STATUS = "021";//其它进行样品交接环节
            strMsg = "样品交接";
        }
        //子任务所有项目都属于现场项目，跳过分析环节
        DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubtaskID);
        if (dtSampleDept.Rows.Count == 0)//全是现场项目
        {
            objSubtask.TASK_STATUS = "022";
            objSubtask.REMARK1 = objSubtask.ID;
            strMsg = "现场监测结果复核";
            //设置现场复核人
            objSubAppSet.SAMPLING_CHECK = strUserID;
        }
        objSubAppSet.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
        TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
        objSubAppWhere.SUBTASK_ID = strSubtaskID;
        new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);

        //update by ssz QY 现场项目信息需要另外发送任务
        if (dtSampleItem.Rows.Count > 0 && dtSampleDept.Rows.Count > 0)//存在 非现场项目和现场项目
        {
            TMisMonitorSubtaskVo objSampleSubtask = new TMisMonitorSubtaskVo();
            CopyObject(objSubtask, objSampleSubtask);
            objSampleSubtask.ID = GetSerialNumber("t_mis_monitor_subtaskId");
            objSampleSubtask.REMARK1 = objSubtask.ID;
            objSampleSubtask.TASK_STATUS = "022";
            //创建一个新的任务 对现场项目流程
            new TMisMonitorSubtaskLogic().Create(objSampleSubtask);

            //设置现场复核人
            TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
            objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
            objSubApp.SUBTASK_ID = objSampleSubtask.ID;
            objSubApp.SAMPLING_CHECK = strUserID;
            objSubApp.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
            new TMisMonitorSubtaskAppLogic().Create(objSubApp);

            strMsg = "样品交接、现场监测结果复核";
        }
        isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);

        //根据子任务ID把该任务下所有分析类现场监测项目的结果状态改为：00
        new TMisMonitorResultLogic().setSampleItemWithSubtaskID(strSubtaskID);

        #region 任务全部完成，修改任务表状态(注释)
        int iStatus = 0;
        objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = strTaskID;
        DataTable dtTask = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask);
        for (int j = 0; j < dtTask.Rows.Count; j++)
        {
            if (dtTask.Rows[j]["TASK_STATUS"].ToString() != "022")
                iStatus += 1;
        }
        if (iStatus == 0)
        {
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.ID = strTaskID;
            objTask.TASK_STATUS = "09";
            new TMisMonitorTaskLogic().Edit(objTask);
        }
        #endregion

        //现场信息
        TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();

        for (int i = 0; i < strAttribute.Split('-').Length; i++)
        {
            if (strAttribute.Split('-')[i].Contains("|"))
            {
                objSampleSky.SUBTASK_ID = strSubtaskID;
                objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                objSampleSky = new TMisMonitorSampleSkyLogic().Details(objSampleSky);
                objSampleSky.WEATHER_INFO = strAttribute.Split('-')[i].Split('|')[1];
                objSampleSky.SUBTASK_ID = strSubtaskID;
                objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                if (objSampleSky.ID.Length > 0)
                {
                    isSuccess = new TMisMonitorSampleSkyLogic().Edit(objSampleSky);
                }
                else
                {
                    objSampleSky.ID = GetSerialNumber("TMisMonitorSampleSky");
                    isSuccess = new TMisMonitorSampleSkyLogic().Create(objSampleSky);

                }
            }
        }
        
        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }
    /// <summary>
    /// 判断任务中是否存在现场项目
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string isSendToCheck(string strSubTaskID)
    {
        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID);

        return dtSampleItem.Rows.Count > 0 ? "1" : "0";
    }

    /// <summary>
    /// 退回的采样任务发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnBackSendClick(string strSubtaskID, string strSourceID, string strUserID, string strAttribute)
    {
        bool isSuccess = true;
        string strMsg = "";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSourceID);
        if (strSubtaskID == strSourceID)
        {
            objSubtask.TASK_STATUS = "021";
            strMsg = "样品交接";
        }
        else
        {
            objSubtask.TASK_STATUS = "022";
            strMsg = "现场监测结果复核";
        }
        objSubtask.TASK_TYPE = "发送";
        //判断是否存在样品没有设置项目的情况
        TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoVo();
        SampleInfoVo.SUBTASK_ID = strSubtaskID;
        List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();
        list = new TMisMonitorSampleInfoLogic().SelectByObject(SampleInfoVo, 0, 0);
        for (int i = 0; i < list.Count; i++)
        {
            TMisMonitorResultVo ResultVo = new TMisMonitorResultVo();
            ResultVo.SAMPLE_ID = list[i].ID;
            ResultVo = new TMisMonitorResultLogic().SelectByObject(ResultVo);
            if (ResultVo.ID.Length == 0)
            {
                isSuccess = false;
                strMsg = "样品[" + list[i].SAMPLE_NAME + "]还没设置监测项目";
                break;
            }
        }
        if (isSuccess == false)
        {
            return "[{result:'0',msg:'" + strMsg + "'}]";
        }

        //设置现场复核人
        TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
        objSubAppSet.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
        objSubAppSet.SAMPLING_CHECK = strUserID;
        TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
        objSubAppWhere.SUBTASK_ID = strSourceID;
        new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);

        isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);

        //现场信息
        TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();

        for (int i = 0; i < strAttribute.Split('-').Length; i++)
        {
            if (strAttribute.Split('-')[i].Contains("|"))
            {
                objSampleSky.SUBTASK_ID = strSubtaskID;
                objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                objSampleSky = new TMisMonitorSampleSkyLogic().Details(objSampleSky);
                objSampleSky.WEATHER_INFO = strAttribute.Split('-')[i].Split('|')[1];
                objSampleSky.SUBTASK_ID = strSubtaskID;
                objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                if (objSampleSky.ID.Length > 0)
                {
                    isSuccess = new TMisMonitorSampleSkyLogic().Edit(objSampleSky);
                }
                else
                {
                    objSampleSky.ID = GetSerialNumber("TMisMonitorSampleSky");
                    isSuccess = new TMisMonitorSampleSkyLogic().Create(objSampleSky);

                }
            }
        }
        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }

    /// <summary>
    /// 退回事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnBackClick(string strSubtaskID, string strSuggestion)
    {
        bool isSuccess = true;
        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        objSubTaskVo.TASK_STATUS = "01";
        objSubTaskVo.TASK_TYPE = "退回";
        new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);

        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubTaskVo.TASK_ID);
        objTaskVo.QC_STATUS = "3";
        new TMisMonitorTaskLogic().Edit(objTaskVo);

        TMisContractPlanVo objContractPlanVo = new TMisContractPlanLogic().Details(objTaskVo.PLAN_ID);
        objContractPlanVo.HAS_DONE = "0";
        new TMisContractPlanLogic().Edit(objContractPlanVo);

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = objSubTaskVo.TASK_ID;
        objReturnInfoVo.SUBTASK_ID = objSubTaskVo.ID;
        objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_002;
        objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_001;
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
}