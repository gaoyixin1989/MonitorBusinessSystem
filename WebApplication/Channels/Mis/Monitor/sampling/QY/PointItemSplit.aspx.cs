using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Web.Services;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using System.Data;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.Point;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Base.CodeRule;

public partial class Channels_Mis_Monitor_sampling_QY_PointItemSplit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        if (!Page.IsPostBack)
        {
            //获取点位信息
            if (Request["type"] != null && Request["type"].ToString() == "getItems")
            {
                strResult =  getItemList();
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    //获取指定点位的监测项目信息
    private string getItemList()
    {
        string strSampleID = Request.Params["strSampleID"];

        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult);

        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemName(string strValue)
    {
        return new TBaseItemInfoLogic().Details(strValue).ITEM_NAME;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strPointName"></param>
    /// <param name="strResultItems"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveItemSplit(string strSampleID, string strPointName, string strResultItems, string strItems, string strMonitorID, string strSubTaskID)
    {
        bool isSuccess = true;

        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        objPoint.ID = GetSerialNumber("t_mis_monitor_taskpointId");
        objPoint.IS_DEL = "0";
        objPoint.SUBTASK_ID = strSubTaskID;
        objPoint.POINT_NAME = strPointName;
        objPoint.MONITOR_ID = strMonitorID;
        objPoint.FREQ = "1";
        objPoint.CREATE_DATE = DateTime.Now.ToString();

        TMisMonitorSampleInfoVo objSampleVo = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        objPoint.TASK_ID = objTask.ID;

        //监测任务出现新增排口时，基础资料企业表也要新增
        TBaseCompanyPointVo objnewPoint = new TBaseCompanyPointVo();
        objnewPoint.ID = GetSerialNumber("t_base_company_point_id");
        objnewPoint.IS_DEL = "0";
        objnewPoint.POINT_NAME = strPointName;
        objnewPoint.MONITOR_ID = strMonitorID;
        objnewPoint.FREQ = "1";
        objnewPoint.CREATE_DATE = DateTime.Now.ToString();

        TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
        objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);

        TMisContractCompanyVo objContractCompany = new TMisContractCompanyLogic().Details(objTaskCompany.COMPANY_ID);
        objnewPoint.COMPANY_ID = objContractCompany.COMPANY_ID;

        isSuccess = new TBaseCompanyPointLogic().Create(objnewPoint);

        objPoint.POINT_ID = objnewPoint.ID;
        isSuccess = new TMisMonitorTaskPointLogic().Create(objPoint);

        //增加点位样品信息
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.SUBTASK_ID = strSubTaskID;
        objSample.QC_TYPE = "0";
        objSample.NOSAMPLE = "0";
        objSample.POINT_ID = objPoint.ID;
        objSample.SAMPLE_NAME = objPoint.POINT_NAME;
        //新增点位时候，自动生成该点位的样品编码
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";
        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
        objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);
        isSuccess = new TMisMonitorSampleInfoLogic().Create(objSample);

        isSuccess = new TMisMonitorSampleInfoLogic().UpdateSetWhere("T_Mis_MONITOR_RESULT", "SAMPLE_ID='" + objSample.ID + "'", "ID in(" + strResultItems.TrimEnd(',') + ")");
        isSuccess = new TMisMonitorSampleInfoLogic().UpdateSetWhere("T_Mis_MONITOR_TASK_ITEM", "TASK_POINT_ID='" + objPoint.ID + "'", "TASK_POINT_ID='" + objSampleVo.POINT_ID + "' AND ITEM_ID in(" + strItems.TrimEnd(',') + ")");

        return isSuccess ? "true" : "false";
    }
}