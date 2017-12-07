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
using i3.ValueObject.Channels.Mis.Monitor.Result;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;

/// <summary>
/// 功能描述：采样任务
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_Sampling : PageBase
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

            //获取子任务ID
            if (Request["strSubtaskID"] != null && Request["strSubtaskID"].ToString() != "")
                this.txtSubTaskId.Value = Request["strSubtaskID"].ToString();
        }
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
        objConCompany.TASK_ID = objTask.ID;
        objConCompany = new TMisMonitorTaskCompanyLogic().Details(objConCompany);
        objSubtask.MONITOR_ID = getMonitorTypeName(objSubtask.MONITOR_ID);
        objSubtask.SAMPLING_MANAGER_ID = new TSysUserLogic().Details(LogInfo.UserInfo.ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.CONTRACT_CODE;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.LINK_PHONE;
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_ASK_DATE))
        {
            objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        }
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_FINISH_DATE))
        {
            objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");
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
    public static string btnSendClick(string strSubtaskID, string strAttribute)
    {
        bool isSuccess = true;
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        string strTaskID = objSubtask.TASK_ID;
        if (objSubtask.MONITOR_ID == "000000004" || objSubtask.MONITOR_ID == "000000005")
        {
            objSubtask.TASK_STATUS = "023";//噪声、辐射现场监测项目进行 现场项目审核流程
            objSubtask.REMARK1 = objSubtask.ID;
        }
        else
            objSubtask.TASK_STATUS = "021";//其它进行样品交接环节

        //子任务所有项目都属于现场项目，跳过分析环节 
        //DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubtaskID);
        //update by ssz QY 现场项目信息需要另外发送任务
        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubtaskID);

        //郑州'余氯','透明度','pH值','电导率','溶解氧'为现场项目
        //以上项目，环境质量由现场室监测，污染源由实验室监测(余氯除外)
        //这几个项目需要实验室填写，REMARK_4=1
        //string strEnvSampleItemID = "FS0000005,FS0000006,FS0000009,FS0000012,001000159";
        string strEnvSampleItemID = "FS0000005,FS0000006,FS0000009,FS0000012";
        //string strEnvType = "EnvRiver,EnvRiverCity,EnvRiverTarget,EnvRiverPlan,EnvReservoir,EnvRain,EnvMudRiver,EnvDrinkingSource,EnvDrinking";
        string strEnvType = "EnvRiver,EnvRiverCity,EnvRiverTarget,EnvRiverPlan,EnvReservoir,EnvMudRiver,EnvDrinkingSource,EnvDrinking";//降水除外
        for (int i = 0; i < dtSampleItem.Rows.Count; i++)
        {
            if (strEnvSampleItemID.Contains(dtSampleItem.Rows[i]["ITEM_ID"].ToString()))
            {
                if (!strEnvType.Contains(objSubtask.MONITOR_ID))
                {
                    TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                    objResult.ID = dtSampleItem.Rows[i]["ID"].ToString();
                    objResult.REMARK_4 = "1";
                    new TMisMonitorResultLogic().Edit(objResult);
                }
            }
        }

        //子任务所有项目都属于现场项目，跳过分析环节
        DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubtaskID);
        //update by ssz QY 现场项目信息需要另外发送任务
        dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubtaskID);

        //郑州'余氯','透明度','pH值','电导率','溶解氧'为现场项目
        //以上项目，环境质量由现场室监测，污染源由实验室监测
        bool ifNeedNewSubTask = false;
        if (dtSampleItem.Rows.Count > 0 && dtSampleDept.Rows.Count > 0)//存在 非现场项目和现场项目
        {
            ifNeedNewSubTask = true;
        }

        //判定是否有现场室项目，或者是否有实验室项目
        bool isHasSampleItem = false;
        bool isHasDeptItem = false;
        for (int i = 0; i < dtSampleItem.Rows.Count; i++)
        {
            if (dtSampleItem.Rows[i]["REMARK_4"].ToString() == "1")
            {
                
                isHasDeptItem = true;
            }
            if (dtSampleItem.Rows[i]["REMARK_4"].ToString() != "1")
            {
                isHasSampleItem = true;
            }
        }

        if (dtSampleItem.Rows.Count > 0)
        {
            if (!isHasDeptItem)//现场项目全是现场室填写
            {
                if (dtSampleDept.Rows.Count == 0)//而且无实验室项目
                {
                    objSubtask.TASK_STATUS = "023";
                    objSubtask.REMARK1 = objSubtask.ID;
                }
                else//但有实验室项目
                    ifNeedNewSubTask = true;
            }
            else if (!isHasSampleItem)//现场项目全是实验室填写
                ifNeedNewSubTask = false;
            else //现场项目有现场室和实验室填写内容
                ifNeedNewSubTask = true;
        }
        else
        {
            //全实验室项目，021
        }

        //if (dtSampleDept.Rows.Count == 0)//全是现场项目
        //{
        //    objSubtask.TASK_STATUS = "023";
        //    objSubtask.REMARK1 = objSubtask.ID;
        //}

        if (ifNeedNewSubTask)//存在 非现场项目和现场项目
        {
            TMisMonitorSubtaskVo objSampleSubtask = new TMisMonitorSubtaskVo();
            CopyObject(objSubtask, objSampleSubtask);
            objSampleSubtask.ID = GetSerialNumber("t_mis_monitor_subtaskId");
            objSampleSubtask.REMARK1 = objSubtask.ID;
            objSampleSubtask.TASK_STATUS = "023";
            //创建一个新的任务 对现场项目流程
            new TMisMonitorSubtaskLogic().Create(objSampleSubtask);

            //创建监测子任务审核表 对现场项目流程
            TMisMonitorSubtaskAppVo objSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(new TMisMonitorSubtaskAppVo()
                {
                    SUBTASK_ID = objSubtask.ID,
                }
            );
            TMisMonitorSubtaskAppVo objSampleSubTaskApp = new TMisMonitorSubtaskAppVo();
            CopyObject(objSubTaskApp, objSampleSubTaskApp);
            objSampleSubTaskApp.SUBTASK_ID = objSampleSubtask.ID;
            objSampleSubTaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
            new TMisMonitorSubtaskAppLogic().Create(objSampleSubTaskApp);
        }
        isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);

        #region 任务全部完成，修改任务表状态(注释)
        int iStatus = 0;
        objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = strTaskID;
        DataTable dtTask = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask);
        for (int j = 0; j < dtTask.Rows.Count; j++)
        {
            if (dtTask.Rows[j]["TASK_STATUS"].ToString() != "023")
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

        #region 现场信息
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
        #endregion 
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnBackClick(string strSubtaskID, string strSuggestion)
    {
        //TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
        //TMisMonitorSubtaskVo.ID = strSubtaskID;
        //TMisMonitorSubtaskVo.TASK_STATUS = "04";
        //TMisMonitorSubtaskVo.TASK_TYPE = "退回";
        //bool isSuccess = new TMisMonitorSubtaskLogic().Edit(TMisMonitorSubtaskVo);
        //return isSuccess == true ? "1" : "0";

        bool isSuccess = true;
        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        objSubTaskVo.TASK_STATUS = "01";
        objSubTaskVo.TASK_TYPE = "退回";
        new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);

        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubTaskVo.TASK_ID);
        objTaskVo.QC_STATUS = "2";
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
    /// <summary>
    /// 获取指定监测类别的类别名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetMonitorName(string strId)
    {
        TBaseMonitorTypeInfoVo objItems = new TBaseMonitorTypeInfoLogic().Details(new TBaseMonitorTypeInfoVo { ID = strId, IS_DEL = "0" });
        return objItems.MONITOR_TYPE_NAME;
    }
    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <param name="strPointId"></param>
    /// <returns></returns>
    private DataTable GetPendingPlanPointItemsDataTable(string strPointId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPointId))
        {
            TMisContractPointitemVo objItems = new TMisContractPointitemVo();
            objItems.CONTRACT_POINT_ID = strPointId;
            dt = new TMisContractPointitemLogic().GetItemsForPoint(objItems);
        }
        return dt;
    }
    /// <summary>
    /// 获取全程质控信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getEntire_QC(string strSubTaskId)
    {
        string strEntire_QC = "否";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubTaskId);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        if (objTask.ALLQC_STATUS == "1")
            strEntire_QC = "是";
        return strEntire_QC;
    }
}