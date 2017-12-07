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
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using System.Configuration;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
/// <summary>
/// 功能描述：采样任务分配
/// 创建日期：2012-12-6
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplingTaskAllocation : PageBase
{
    private static string strQC = "";
    public string task_id = "", strPlanId = "", strQCStep = "";
    private void GetHidParam()
    {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                //预约表ID
                this.PLAN_ID.Value = Request.QueryString["planid"].ToString();
            }

            //修改项目负责人信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "ModifTaskSampleDutyUser" && !String.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                ModifTaskSampleDutyUser(Request.QueryString["planid"].Trim());
            }

            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
            //采样任务分配信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo();
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
            //发送
            if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
            {
                strResult = SendToNext(Request["strSampleAskDate"].ToString(), Request["strSampleFinishDate"].ToString());
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
        if (this.PLAN_ID.Value.Length == 0)
            return "";
        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);
        //如果是验收监测
        if (objTask.CONTRACT_TYPE == "05")
        {
            //获取方案编制人信息
            string strProjectId = objTask.PROJECT_ID;
            //将方案编制人信息写入监测子任务表中
            TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
            TMisMonitorSubtaskVo.TASK_ID = objTask.ID;
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
        objTask.TESTED_COMPANY_ID = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID).COMPANY_NAME;
        objTask.CONTRACT_TYPE = new TSysDictLogic().GetDictNameByDictCodeAndType(objTask.CONTRACT_TYPE, "Contract_Type");
        
       
        return ToJson(objTask);
    }

    /// <summary>
    /// 采样任务分配信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo()
    {
        if (this.PLAN_ID.Value.Length == 0)
            return "";

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
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
    /// 发送到各个项目负责人的采样任务列表环节
    /// </summary>
    /// <param name="strWorkTaskId">任务ID</param>
    /// <returns>1 成功 Or 0 失败</returns>
    [WebMethod]
    public static bool btnSendClick(string strWorkTaskId)
    {
        string strConfigApp = ConfigurationManager.AppSettings["EnvQCsetting"].ToString();
        bool flag = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTaskId;

        objTask = new TMisMonitorTaskLogic().Details(strWorkTaskId);
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = objTask.ID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();


            DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = dt.Rows[i]["ID"].ToString() });
            if (dtApp.Rows.Count > 0)
            {
                objSubApp.ID = dtApp.Rows[0]["ID"].ToString();
                objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();
                new TMisMonitorSubtaskAppLogic().Edit(objSubApp);
            }
            else
            {
                objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
                objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

                new TMisMonitorSubtaskAppLogic().Create(objSubApp);
            }

            objSubTask.ID = dt.Rows[i]["ID"].ToString();
            ////如果是非环境质量类 将 TASK_STATUS 设置为02
            //if (objTask.TASK_TYPE != "1" || strConfigApp != "1")
            //{
            objSubTask.TASK_STATUS = "02";
            //}
            if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                flag = false;
        }

        if (flag)
        {
            TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
            objTaskEdit.ID = objTask.ID;
            objTaskEdit.QC_STATUS = "9";//表示已经完成质控设置
            //如果是环境质量类 将 SEND_STATUS 设置为1
            if (objTask.TASK_TYPE == "1")
            {
                objTaskEdit.SEND_STATUS = "1";
                //if (strConfigApp == "1")
                //    objTaskEdit.QC_STATUS = "1";  //环境质量类时，采样任务分配后到采样前质控环节
            }
            new TMisMonitorTaskLogic().Edit(objTaskEdit);

            TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(objTask.PLAN_ID);
            //if (objTask.TASK_TYPE != "1")
            objPlan.HAS_DONE = "1";
            new TMisContractPlanLogic().Edit(objPlan);
        }

        return flag;
    }
   // public static string btnSendClick(string strPlanID, string strSampleAskDate, string strSampleFinishDate, string strTicketNum, string strQCStatus)
   // {
   //     bool IsSuccess = true;
   //     TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
   //     objTask.PLAN_ID = strPlanID;
   //     objTask = new TMisMonitorTaskLogic().Details(objTask);


   //     TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
   //     objSubTask.TASK_ID = objTask.ID;
   //     DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
   //     //判断是否经过质控，如果经过质控，则将数据发送到质控环节
   //     if (!String.IsNullOrEmpty(strQCStatus) && strQCStatus == "1")
   //     {
   //         TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
   //         objTaskEdit.ID = objTask.ID;
   //         objTaskEdit.QC_STATUS = "2";
   //         objTaskEdit.TICKET_NUM = strTicketNum;
   //         //如果是环境质量类 将 SEND_STATUS 设置为1
   //         if (objTask.TASK_TYPE == "1")
   //         {
   //             objTaskEdit.SEND_STATUS = "1";
   //         }
   //         if (new TMisMonitorTaskLogic().Edit(objTaskEdit))
   //         {
   //             for (int i = 0; i < dt.Rows.Count; i++)
   //             {
   //                 objSubTask.ID = dt.Rows[i]["ID"].ToString();
   //                 objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
   //                 objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
   //                 if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
   //                     IsSuccess = false;
   //             }
   //         }

   //     }
   //     //如果不需要质控，则直接发送到采样环节
   //     else
   //     {
   //         TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
   //         objTaskEdit.ID = objTask.ID;
   //         objTaskEdit.QC_STATUS = "8";
   //         objTaskEdit.TICKET_NUM = strTicketNum;
   //         //如果是环境质量类 将 SEND_STATUS 设置为1
   //         if (objTask.TASK_TYPE == "1") {
   //             objTaskEdit.SEND_STATUS = "1";
   //         }
   //         if (new TMisMonitorTaskLogic().Edit(objTaskEdit))
   //         {
   //             for (int i = 0; i < dt.Rows.Count; i++)
   //             {
   //                 objSubTask.ID = dt.Rows[i]["ID"].ToString();
   //                 objSubTask.TASK_STATUS = "02";
   //                 objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
   //                 objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
   //                 if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
   //                     IsSuccess = false;
   //             }
   //         }


   //         if (IsSuccess)
   //         {
   //             TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
   //             objPlan.HAS_DONE = "1";
   //             new TMisContractPlanLogic().Edit(objPlan);
   //         }

   //     }

   //     TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();

   //     for (int i = 0; i < dt.Rows.Count; i++)
   //     {
   //         DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = dt.Rows[i]["ID"].ToString() });
   //         if (dtApp.Rows.Count > 0)
   //         {
   //             objSubApp.ID = dtApp.Rows[0]["ID"].ToString();
   //             objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
   //             objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
   //             objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();
   //             new TMisMonitorSubtaskAppLogic().Edit(objSubApp);
   //         }
   //         else {
   //             objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
   //             objSubApp.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
   //             objSubApp.SAMPLE_ASSIGN_ID = new PageBase().LogInfo.UserInfo.ID;
   //             objSubApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

   //             new TMisMonitorSubtaskAppLogic().Create(objSubApp);
   //         }
   //     }
   //     return IsSuccess == true ? "1" : "0";
   //     //bool IsSuccess = true;
   //     //TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
   ////DateTime.Now.ToString();= strPlanID;
   ////         if (!//obTMisMonitorSubtaskLogickLogic()objSubTasko)
   ////                     IsSuccess = false;jTask);

   //     //TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
   //     //objSubTask.TASK_ID = objTask.ID;

   //     //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
   //     //for (int i = 0; i < dt.Rows.Count; i++)
   //     //{
   //     //    objSubTask.ID = dt.Rows[i]["ID"].ToString();
   //     //    objSubTask.TASK_STATUS = "02";
   //     //    objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
   //     //    objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
   //     //    if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
   //     //        IsSuccess = false;

   //     //    //TMisMonitorSubtaskAppVo objSubtaskApp = new TMisMonitorSubtaskAppVo();

   //     //    //objSubtaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
   //     //    //objSubtaskApp.SUBTASK_ID = objSubTask.ID;
   //     //    //objSubtaskApp.SAMPLE_ASSIGN_ID = LogInfo.UserInfo.ID;
   //     //    //objSubtaskApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

   //     //    //new TMisMonitorSubtaskAppLogic().Create(objSubtaskApp);
   //     //}

   //     //if (IsSuccess)
   //     //{
   //     //    TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
   //     //    objPlan.HAS_DONE = "1";
   //     //    new TMisContractPlanLogic().Edit(objPlan);
   //     //}
   //     //return IsSuccess == true ? "1" : "0";
   // }
    /// <summary>
    /// 发送到下一环节
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <returns></returns>
    public string SendToNext(string strSampleAskDate, string strSampleFinishDate)
    {
        bool IsSuccess = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = objTask.ID;

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            objSubTask.ID = dt.Rows[i]["ID"].ToString();
            objSubTask.TASK_STATUS = "02";
            objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
            objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
            if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                IsSuccess = false;

            TMisMonitorSubtaskAppVo objSubtaskApp = new TMisMonitorSubtaskAppVo();

            objSubtaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
            objSubtaskApp.SUBTASK_ID = objSubTask.ID;
            objSubtaskApp.SAMPLE_ASSIGN_ID = LogInfo.UserInfo.ID;
            objSubtaskApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

            new TMisMonitorSubtaskAppLogic().Create(objSubtaskApp);
        }

        if (IsSuccess)
        {
            TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
            objPlan.HAS_DONE = "1";
            new TMisContractPlanLogic().Edit(objPlan);
        }

        return IsSuccess == true ? "1" : "0";
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

    #region 质控信息设置
    /// <summary>
    /// 删除质控样品
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteQc(string strSampleID)
    {
        bool IsSuccess = new TMisMonitorSampleInfoLogic().Delete(strSampleID);
        return IsSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取样品-监测项目
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <param name="strQcType">质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSampleItem(string strSampleID, string strQcType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();

        dt = new TBaseItemInfoLogic().SelectItemForQC(strSampleID, strQcType);

        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 质控样品添加（外控）
    /// </summary>
    /// <param name="strSampleID">样品Id</param>
    /// <param name="strListQc1">现场空白项目</param>
    /// <param name="strListQc2">现场加标项目</param>
    /// <param name="strListQc3">现场平行项目</param>
    /// <param name="strListQc4">实验室密码平行项目</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveQc(string strSampleID, string strListQc1, string strListQc2, string strListQc3, string strListQc4, string strQc3Count, string strQc4Count)
    {
        bool IsSuccess = true;

        deleteQcInfo(strSampleID);
        strQC = "";
        strQC += (strListQc1.Trim().Length > 0) ? "1," : "";
        strQC += (strListQc2.Trim().Length > 0) ? "2," : "";
        strQC += (strListQc3.Trim().Length > 0) ? "3," : "";
        strQC += (strListQc4.Trim().Length > 0) ? "4," : "";
        strQC = strQC.TrimEnd(',');

        //现场空白
        if (strListQc1.Trim().Length > 0)
        {
            IsSuccess = AddQcEmptyInfo(strSampleID, strListQc1.Replace(" ", "").TrimEnd(','));
        }

        //现场加标
        if (strListQc2.Trim().Length > 0)
        {
            IsSuccess = AddQcAddInfo(strSampleID, strListQc2.Replace(" ", "").TrimEnd(','));
        }

        //现场平行
        if (strListQc3.Trim().Length > 0)
        {
            IsSuccess = AddQcTwinInfo(strSampleID, "3", strListQc3.Replace(" ", "").TrimEnd(','), strQc3Count);
        }

        //密码平行
        if (strListQc4.Trim().Length > 0)
        {
            IsSuccess = AddQcTwinInfo(strSampleID, "4", strListQc4.Replace(" ", "").TrimEnd(','), strQc4Count);
        }

        return IsSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 清除原有质控信息:样品表、结果表、质控表(3个表)
    /// </summary>
    /// <returns></returns>
    private static void deleteQcInfo(string strSampleID)
    {
        //根据原始样品ID，删除下属的质控表、结果表、结果执行表数据
        //new TMisMonitorQcEmptyOutLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorQcAddLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorQcTwinLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorResultAppLogic().DeleteQcWithSampleID(strSampleID);
        //new TMisMonitorResultLogic().DeleteQcWithSampleID(strSampleID);

        //删除样品表
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.QC_SOURCE_ID = strSampleID;
        new TMisMonitorSampleInfoLogic().Delete(objSample);
    }

    /// <summary>
    /// 新增现场空白质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcEmptyInfo(string strSampleID, string strListQc1)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = "1";
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_NAME += "现场空白";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc1.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc1.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = "1";
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcEmptyOutVo objQcEmpty = new TMisMonitorQcEmptyOutVo();
            objQcEmpty.ID = GetSerialNumber("QC_EMPTY_OUT");
            objQcEmpty.RESULT_ID_SRC = strOldResultID;
            objQcEmpty.RESULT_ID_EMPTY = objResult.ID;
            if (!new TMisMonitorQcEmptyOutLogic().Create(objQcEmpty))
                IsSuccess = false;
        }

        return IsSuccess;
    }

    /// <summary>
    /// 新增现场加标质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcAddInfo(string strSampleID, string strListQc2)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = "2";
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_NAME += "现场加标";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc2.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc2.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = "2";
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcAddVo objQcAdd = new TMisMonitorQcAddVo();
            objQcAdd.ID = GetSerialNumber("QcAddId");
            objQcAdd.RESULT_ID_SRC = strOldResultID;
            objQcAdd.RESULT_ID_ADD = objResult.ID;
            objQcAdd.QC_TYPE = "2";
            if (!new TMisMonitorQcAddLogic().Create(objQcAdd))
                IsSuccess = false;
        }
        return IsSuccess;
    }

    /// <summary>
    /// 新增现场平行质控信息
    /// </summary>
    /// <returns></returns>
    private static bool AddQcTwinInfo(string strSampleID, string strQcType, string strListQc3, string strQc3Count)
    {
        bool IsSuccess = true;
        string strOldResultID = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        if (strQcType == "4")
            objSample.SAMPLE_NAME += "密码平行";
        else
            objSample.SAMPLE_NAME += "现场平行";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            IsSuccess = false;

        for (int i = 0; i < strListQc3.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strListQc3.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strOldResultID = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = strQcType;
            objResult.QC_SOURCE_ID = strOldResultID;
            objResult.SOURCE_ID = strOldResultID;
            objResult.QC = strQC;
            if (!new TMisMonitorResultLogic().Create(objResult))
                IsSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
            objQcTwin.ID = GetSerialNumber("QcTwinId");
            objQcTwin.RESULT_ID_SRC = strOldResultID;
            objQcTwin.RESULT_ID_TWIN1 = objResult.ID;
            objQcTwin.QC_TYPE = strQcType;
            if (!new TMisMonitorQcTwinLogic().Create(objQcTwin))
                IsSuccess = false;
        }
        if (strQc3Count == "2")
        {
            objSample.ID = GetSerialNumber("MonitorSampleId");
            if (strQcType == "4")
                objSample.SAMPLE_NAME += "密码平行2";
            else
                objSample.SAMPLE_NAME += "现场平行2";
            if (!new TMisMonitorSampleInfoLogic().Create(objSample))
                IsSuccess = false;

            for (int i = 0; i < strListQc3.Split(',').Length; i++)
            {
                TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                objResult.SAMPLE_ID = strSampleID;
                objResult.QC_TYPE = "0";
                objResult.ITEM_ID = strListQc3.Split(',')[i];
                objResult = new TMisMonitorResultLogic().Details(objResult);

                strOldResultID = objResult.ID;
                objResult.ID = GetSerialNumber("MonitorResultId");
                objResult.SAMPLE_ID = objSample.ID;
                objResult.QC_TYPE = strQcType;
                objResult.QC_SOURCE_ID = strOldResultID;
                objResult.SOURCE_ID = strOldResultID;
                objResult.QC = strQC;
                if (!new TMisMonitorResultLogic().Create(objResult))
                    IsSuccess = false;
                InsertResultAPP(objResult.ID);

                TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
                objQcTwin.RESULT_ID_SRC = strOldResultID;
                objQcTwin.QC_TYPE = strQcType;
                objQcTwin = new TMisMonitorQcTwinLogic().Details(objQcTwin);
                objQcTwin.TWIN_RESULT2 = objResult.ID;
                if (!new TMisMonitorQcTwinLogic().Edit(objQcTwin))
                    IsSuccess = false;
            }
        }

        return IsSuccess;
    }

    /// <summary>
    /// 新增结果分析执行表信息
    /// </summary>
    /// <returns></returns>
    private static void InsertResultAPP(string strResultID)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.ID = strResultID;
        DataTable dt = new TMisMonitorResultLogic().SelectManagerByTable(objResult);

        TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
        objResultApp.ID = GetSerialNumber("MonitorResultAppId");
        objResultApp.RESULT_ID = strResultID;
        if (dt.Rows.Count > 0)
        {
            objResultApp.HEAD_USERID = dt.Rows[0]["ANALYSIS_MANAGER"].ToString();
            objResultApp.ASSISTANT_USERID = dt.Rows[0]["ANALYSIS_ID"].ToString();
        }
        new TMisMonitorResultAppLogic().Create(objResultApp);
    }
    #endregion
    protected void btnImport_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/WorkTaskSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "", strMonitorList = "", strWorkContent = "";
        DataTable dt = GetPendingPlanDataTable();
        //插入委托书单号
        if (dt.Rows.Count > 0)
        {
            sheet.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["TICKET_NUM"].ToString());
            sheet.GetRow(4).GetCell(6).SetCellValue(dt.Rows[0]["REPORT_CODE"].ToString());
            strTaskType = getDictName(dt.Rows[0]["CONTRACT_TYPE"].ToString(), "Contract_Type");
            sheet.GetRow(5).GetCell(1).SetCellValue(strTaskType + "任务");
            sheet.GetRow(5).GetCell(7).SetCellValue(dt.Rows[0]["CONTRACT_CODE"].ToString());

            //string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPES"].ToString().Split(';');
            string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPE"].ToString().Split(';');
            foreach (string str in strMonitroTypeArr)
            {
                strMonitorList += GetMonitorName(str) + "/";
            }
            strMonitorList = strMonitorList.Substring(0, strMonitorList.Length - 1);
            sheet.GetRow(7).GetCell(1).SetCellValue(strMonitorList);
            if (dt.Rows[0]["SAMPLE_SOURCE"].ToString() == "送样")
            {
                //strWorkContent += "地表水、地下水(送样)\n";
                int intLen = strMonitroTypeArr.Length;
                int INTSHOWLEN = 0;
                foreach (string strMonitor in strMonitroTypeArr)
                {
                    INTSHOWLEN++;
                    strWorkContent += GetMonitorName(strMonitor) + "、";
                    if (INTSHOWLEN == intLen - 1)
                    {
                        strWorkContent += "(送样)\n";
                    }
                }
            }
            //strWorkContent += "环境空气：\n";
            //获取当前监测类别信息
            DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable();
            DataTable dtPoint = GetPendingPlanPointDataTable();
            string strOutValuePoint = "", strOutValuePointItems = "";
            if (dtMonitor.Rows.Count > 0)
            {
                foreach (DataRow drr in dtMonitor.Rows)
                {
                    string strMonitorName = "", strPointName = "";
                    DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                    if (drPoint.Length > 0)
                    {

                        foreach (DataRow drrPoint in drPoint)
                        {
                            string strPointNameForItems = "", strPointItems = "";
                            strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString() + "：";
                            strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                            //获取当前点位的监测项目
                            DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                            if (dtPointItems.Rows.Count > 0)
                            {
                                foreach (DataRow drItems in dtPointItems.Rows)
                                {
                                    strPointNameForItems = strMonitorName.Substring(0, strMonitorName.Length - 1) + drrPoint["POINT_NAME"] + ":";
                                    strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                                }
                                strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                            }
                        }
                        //获取输出监测类型监测点位信息
                        strOutValuePoint += strMonitorName + strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                    }
                }
            }
            strWorkContent += "监测点位：\n" + strOutValuePoint;
            strWorkContent += "监测因子与频次：\n" + strOutValuePointItems;
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);

            string strLinkPerple = "";
            strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + "\n";
            strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
            if (!String.IsNullOrEmpty(dt.Rows[0]["PROJECT_ID"].ToString()))
            {
                strLinkPerple += "项目负责人:" + GetUserName(dt.Rows[0]["PROJECT_ID"].ToString());
            }
            else
            {
                strLinkPerple += "报告编写：";
            }
            sheet.GetRow(10).GetCell(1).SetCellValue(strLinkPerple);

            if (!String.IsNullOrEmpty(dt.Rows[0]["ASKING_DATE"].ToString()))
            {
                sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            }
            //sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            sheet.GetRow(11).GetCell(3).SetCellValue(LogInfo.UserInfo.REAL_NAME);
            sheet.GetRow(11).GetCell(6).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("工作任务通知单-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }


    /// <summary>
    /// 获取指定监测计划的监测类别信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDistinctMonitorDataTable()
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
    /// 获取指定委托书和监测计划的任务信息  胡方扬 2013-04-24
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDataTable()
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems);
        }
        //if ( !String.IsNullOrEmpty(strPlanId))
        //{
        //    TMisContractPlanVo objItems = new TMisContractPlanVo();
        //    objItems.ID = strPlanId;
        //    TMisContractVo objItemContract = new TMisContractVo();
        //    objItemContract.ID = task_id;
        //    dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems, objItemContract);
        //}
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanPointDataTable()
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
    /// 获取用户名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetUserName(string strId)
    {
        TSysUserVo objItems = new TSysUserLogic().Details(new TSysUserVo { ID = strId, IS_DEL = "0" });
        return objItems.REAL_NAME;
    }


    /// <summary>
    /// 修改项目复制人
    /// </summary>
    /// <param name="strPlanId"></param>
    private void ModifTaskSampleDutyUser(string strPlanId)
    {
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisMonitorTaskVo objTaskDetail = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = strPlanId });
            TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
            objSubTask.TASK_ID = objTaskDetail.ID;
            DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask, 0, 0);
            TMisContractUserdutyVo objUserDuty = new TMisContractUserdutyVo();
            objUserDuty.CONTRACT_PLAN_ID = strPlanId;
            DataTable dtDuty = new TMisContractUserdutyLogic().SelectByTable(objUserDuty, 0, 0);

            //如果没有获取到委托书的默认采样人 则取对应监测类别的 岗位职责数据
            if (dt.Rows.Count > 0 && dtDuty.Rows.Count < 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strMonitorType_ID = dt.Rows[i]["MONITOR_ID"].ToString();
                    //潘德军修改2013-7-19 环境质量的监测类别取对应的污染源类别的岗位职责
                    i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorType = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strMonitorType_ID);
                    if (objMonitorType.REMARK1.Trim().Length > 0)
                    {
                        strMonitorType_ID = objMonitorType.REMARK1.Trim();
                    }

                    TSysDutyVo objDuty = new TSysDutyVo();
                    objDuty.MONITOR_TYPE_ID = strMonitorType_ID;
                    objDuty.DICT_CODE = "duty_sampling";
                    DataTable objDutyDt = new TSysDutyLogic().GetDutyUser(objDuty);
                    DataRow drr = null;

                    if (objDutyDt.Rows.Count > 0)
                    {
                        //如果设置了默认负责人，则取默认负责人
                        DataRow[] drArr = objDutyDt.Select(" IF_DEFAULT='0'");
                        if (drArr.Length > 0)
                        {
                            drr = drArr[0];
                        }
                        else
                        {
                            //如果未设置默认负责人，则取第一行数据
                            drr = objDutyDt.Rows[0];
                        }

                        if (drr != null)
                        {
                            TMisMonitorSubtaskVo objUpSubTask = new TMisMonitorSubtaskVo();
                            objUpSubTask.ID = dt.Rows[i]["ID"].ToString();
                            if (dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString() == "")
                            {
                                objUpSubTask.SAMPLING_MANAGER_ID = drr["USERID"].ToString();
                            }
                            new TMisMonitorSubtaskLogic().Edit(objUpSubTask);
                        }
                    }
                }
            }

            if (dt.Rows.Count > 0 && dtDuty.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataRow drr in dtDuty.Rows)
                    {
                        if (dr["MONITOR_ID"].ToString() == drr["MONITOR_TYPE_ID"].ToString())
                        {
                            TMisMonitorSubtaskVo objUpSubTask = new TMisMonitorSubtaskVo();
                            objUpSubTask.ID = dr["ID"].ToString();
                            objUpSubTask.SAMPLING_MANAGER_ID = drr["SAMPLING_MANAGER_ID"].ToString();
                            new TMisMonitorSubtaskLogic().Edit(objUpSubTask);
                        }
                    }
                }
            }
        }
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