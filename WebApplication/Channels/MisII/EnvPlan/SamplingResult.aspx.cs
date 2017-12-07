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
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;

using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Company;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using System.IO;
using System.Text;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using System.Collections;
using System.Runtime.InteropServices;
using WebApplication;


public partial class Channels_MisII_sampling_SamplingResult : PageBase
{
    private static string strQC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            var workID = Request.QueryString["WorkID"];     //当前流程ID
            workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空

            if (!string.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                //预约表ID
                this.PLAN_ID.Value = Request.QueryString["planid"].ToString();
            }

            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo(Request.QueryString["strWorkTask_Id"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //采样任务分配信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["strWorkTask_Id"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测点位信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }

            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
            {
                strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["QcType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            
        }       
    }

    /// <summary>
    /// 任务单号设置
    /// </summary>
    /// <returns></returns>
    protected string SetTaskCode()
    {
        string strTaskCode = i3.View.PageBase.GetSerialNumber("ticket_num");

        return strTaskCode;
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo(string strTaskID)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return "";
        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskID;
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);
        if (objTask.TICKET_NUM.Length == 0)
        {
            objTask.TICKET_NUM = SetTaskCode();
            new TMisMonitorTaskLogic().Edit(objTask);
        }
        objTask.TESTED_COMPANY_ID = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID).COMPANY_NAME;
        objTask.CONTRACT_TYPE = new TSysDictLogic().GetDictNameByDictCodeAndType(objTask.CONTRACT_TYPE, "Contract_Type");

        //如果是验收监测
        if (objTask.CONTRACT_TYPE == "05")
        {
            //获取方案编制人信息
            string strProjectId = objTask.PROJECT_ID;
            //将方案编制人信息写入监测子任务表中
            TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
            TMisMonitorSubtaskVo.TASK_ID = objTask.ID;
            TMisMonitorSubtaskVo.TASK_STATUS = "01";
            DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(TMisMonitorSubtaskVo);
            foreach (DataRow row in objTable.Rows)
            {
                string strSubTaskId = row["ID"].ToString();
                TMisMonitorSubtaskVo TMisMonitorSubtaskVoTemp = new TMisMonitorSubtaskVo();
                TMisMonitorSubtaskVoTemp.ID = strSubTaskId;
                TMisMonitorSubtaskVoTemp.SAMPLING_MANAGER_ID = strProjectId;
                new TMisMonitorSubtaskLogic().Edit(TMisMonitorSubtaskVoTemp);
            }
        }
        return ToJson(objTask);
    }

    /// <summary>
    /// 采样任务分配信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strTaskID)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskID;
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;
        objSubtask.TASK_STATUS = "01";
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, intPageIndex, intPageSize);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = new TSysUserLogic().Details(dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
            else
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = "请选择";

            //退回意见
            TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
            objReturnInfoVo.TASK_ID = dt.Rows[i]["TASK_ID"].ToString();
            objReturnInfoVo.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
            objReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_002;
            objReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_001;
            objReturnInfoVo = new TMisReturnInfoLogic().Details(objReturnInfoVo);
            dt.Rows[i]["REMARK1"] = objReturnInfoVo.SUGGESTION;
        }
        dt.DefaultView.Sort = "MONITOR_ID ASC";
        DataTable dtTemp = dt.DefaultView.ToTable();
        int intTotalCount = new TMisMonitorSubtaskLogic().GetSelectResultCount(objSubtask);
        string strJson = CreateToJson(dtTemp, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测点位信息
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

        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strTwoGridId;
        objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, intPageIndex, intPageSize);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            objSampleInfo.QC_TYPE = "";
            objSampleInfo.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSampleInfo.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                dtSample.ImportRow(dr);
            }
        }
        objSampleInfo.QC_TYPE = "11";
        objSampleInfo.QC_SOURCE_ID = "";
        dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
        }

        int intTotalCount = dtSample.Rows.Count;
        string strJson = CreateToJson(dtSample, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getFourGridInfo(string strThreeGridId, string strQcType)
    {
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strThreeGridId;
        dt = new TMisMonitorResultLogic().SelectByTable(objResult, intPageIndex, intPageSize);
        DataColumn dcP;
        dcP = new DataColumn("TASK_POINT_ID", Type.GetType("System.String"));
        dt.Columns.Add(dcP);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["TASK_POINT_ID"] = new TMisMonitorSampleInfoLogic().Details(strThreeGridId).POINT_ID;
        }
        intTotalCount = new TMisMonitorResultLogic().GetSelectResultCount(objResult);

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
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
    /// 保存结果表信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="strColumnName"></param>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemInfo(string id, string strColumnName, string strValue)
    {
        bool isSuccess = true;
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = id;
        switch (strColumnName)
        {
            case "ITEM_RESULT":
                TMisMonitorResultVo.ITEM_RESULT = strValue == "" ? "###" : strValue;
                break;
            default:
                break;
        }

        isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorPointName(string strTaskPointId)
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(strTaskPointId);

        return objTaskPoint.POINT_NAME;
    }

    /// <summary>
    /// 获取监测项目名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorItemName(string strTaskItemId)
    {
        TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(strTaskItemId);

        return objItemInfo.ITEM_NAME;
    }

    /// <summary>
    /// 删除样品
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteSample(string strSampleID)
    {
        bool IsSuccess = new TMisMonitorResultLogic().deleteSampleInfo(strSampleID);
        return IsSuccess == true ? "1" : "0";
    }



    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanPointDataTable(string strPlanId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanPointDataTable(objItems);
        }
        return dt;
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
    /// 获取指定监测计划的监测类别信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDistinctMonitorDataTable(string strPlanId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanDistinctMonitorDataTable(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 根据任务ID获取委托类别
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getContractType(string strTaskId)
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strTaskId;
        objTask = new TMisMonitorTaskLogic().Details(objTask);
        return objTask.CONTRACT_TYPE;
    }
    

}