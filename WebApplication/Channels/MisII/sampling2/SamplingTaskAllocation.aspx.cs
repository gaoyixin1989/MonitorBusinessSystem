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
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Mis.ProcessMgm;

public partial class Channels_MisII_sampling2_SamplingTaskAllocation : PageBase
{
    private static string strQC = "";

    ////任务ID 黄进军 添加 20150422
    private static string strTicketNum = "";

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

            //黄进军 添加 20150422
            if (!string.IsNullOrEmpty(Request.QueryString["strTicketNum"]))
            {
                //任务表ID
                strTicketNum = Request.QueryString["strTicketNum"].ToString();
            }

            //修改项目负责人信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "ModifTaskSampleDutyUser" && !String.IsNullOrEmpty(Request.QueryString["planid"]))
            {
                ModifTaskSampleDutyUser(Request.QueryString["planid"].Trim());
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


            if (Request.QueryString["type"] == "check")
            {
                //发送前事件（产生子流程）
                workID = Request.QueryString["OID"];
                var flowId = Request.QueryString["FK_Flow"].Trim(',');
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);

                string strSendInfo = GetSendInfo(workID);

                var result = CCFlowFacade.SetNextNodeFH(flowId, Convert.ToInt64(workID), nodeId, strSendInfo, fid);

                Response.Write("true");
                Response.ContentType = "text/plain";
                Response.End();
            }
            if (Request["type"] != null && Request["type"].ToString() == "BarCodePrint")
            {
                strResult = BarCodePrint(Request["strProjectName"].ToString(), Request["strSampleCode"].ToString(), Request["strBarCode"].ToString());
                Response.Write(strResult);
                Response.End();
            }

        }
    }

    private string GetSendInfo22(string strWorkID)
    {
        string strUser = string.Empty;
        string strReturn = string.Empty;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.CCFLOW_ID1 = strWorkID;
        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);

        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskVo();
        objSubTaskVo.TASK_ID = objTaskVo.ID;
        List<TMisMonitorSubtaskVo> list = new List<TMisMonitorSubtaskVo>();
        list = new TMisMonitorSubtaskLogic().SelectByObject(objSubTaskVo, 0, 0);

        for (int i = 0; i < list.Count; i++)
        {
            TSysUserVo objUserVo = new TSysUserLogic().Details(list[i].SAMPLING_MANAGER_ID);
            strUser = objUserVo.USER_NAME;
            strReturn += strUser + "@" + list[i].ID + ",";
        }

        return strReturn.TrimEnd(',');
    }

    private string GetSendInfo(string strWorkID)
    {
        string strUser = string.Empty;
        string strReturn = string.Empty;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.CCFLOW_ID1 = strWorkID;
        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);

        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskVo();
        objSubTaskVo.TASK_ID = objTaskVo.ID;
        List<TMisMonitorSubtaskVo> list = new List<TMisMonitorSubtaskVo>();
        list = new TMisMonitorSubtaskLogic().SelectByObject(objSubTaskVo, 0, 0);

        var smapleLogic=new TMisMonitorSampleInfoLogic();

        var strResultList = new List<string>();

        foreach (var subTask in list)
        {

            var sample = new TMisMonitorSampleInfoVo { SUBTASK_ID = subTask.ID };
            var sampleList = smapleLogic.SelectByObject(sample, 0, 0).ToList();

            if (sampleList.Where(t=>string.IsNullOrEmpty(t.REMARK5)).Count() > 0)
            {
                return "";
            }

            var group = sampleList.GroupBy(t => t.REMARK5);

            foreach (var groupItem in group)
            {
                TSysUserVo objUserVo = new TSysUserLogic().Details(groupItem.Key);

                var str=string.Format("{0}@{1}|{2}",objUserVo.USER_NAME,subTask.ID,string.Join("|", groupItem.Select(t=>t.ID)));

                strResultList.Add(str);

            }
        }

        return string.Join(",", strResultList);
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
        TMisMonitorTaskVo objTask1 = new TMisMonitorTaskLogic().Details(strTaskID);
        TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
        DataTable dt = objLogic.SelectByTable_One(objTask1.PLAN_ID);
        TMisMonitorSubtaskVo tMisMonitorSubtask = new TMisMonitorSubtaskVo();
        tMisMonitorSubtask.TASK_ID = objTask1.ID;
        tMisMonitorSubtask.SAMPLE_FINISH_DATE = dt.Rows[0]["MONITOR_TIME_FINISH"].ToString();
        bool objSubtask1 = new TMisMonitorSubtaskLogic().Edit_One(tMisMonitorSubtask);

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
            if (dt.Rows[i]["REMARK5"].ToString().Length > 0)
                dt.Rows[i]["REMARK5"] = new TSysUserLogic().Details(dt.Rows[i]["REMARK5"].ToString()).REAL_NAME;
            else
                dt.Rows[i]["REMARK5"] = "请选择";

            if (dt.Rows[i]["REMARK4"] != null && dt.Rows[i]["REMARK4"].ToString().Length > 0)
            {
                var nameList = new List<string>();

                var idList = dt.Rows[i]["REMARK4"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach(var id in idList)
                {
                    TSysUserVo objUser = new TSysUserLogic().Details(id);
                    nameList.Add(objUser.REAL_NAME);
                }

                dt.Rows[i]["REMARK4"] = string.Join(",", nameList);

            }

            TMisMonitorSampleInfoVo sampleinfo = new TMisMonitorSampleInfoVo();
            sampleinfo = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());

            if (sampleinfo.SAMPLE_CODE == "" || sampleinfo.SAMPLE_CODE == null)
            {
                //自动生成样品编号 黄进军 添加 20150914
                string num = GetSerialNumber("monitor_samplecode");
                sampleinfo.ID = dt.Rows[i]["ID"].ToString();
                sampleinfo.SAMPLE_CODE = num;
                new TMisMonitorSampleInfoLogic().Edit(sampleinfo);
            }

            if (sampleinfo.SAMPLE_BARCODE == "" || sampleinfo.SAMPLE_BARCODE == null)
            {
                //自动生成条码编号 黄进军 添加 20150417
                string num = GetSerialNumber("Barcoding_num");
                sampleinfo.ID = dt.Rows[i]["ID"].ToString();
                sampleinfo.SAMPLE_BARCODE = strTicketNum + num;
                new TMisMonitorSampleInfoLogic().Edit(sampleinfo);
            }
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
    /// 修改事件
    /// </summary>
    /// 黄进军 添加 20140417
    /// <returns></returns>
    [WebMethod]
    public static string saveDataInfo(string id, string SAMPLE_CODE, string SAMPLE_BARCODE)
    {
        TMisMonitorSampleInfoVo sampleinfo = new TMisMonitorSampleInfoVo();
        sampleinfo.ID = id;
        sampleinfo.SAMPLE_CODE = SAMPLE_CODE;
        sampleinfo.SAMPLE_BARCODE = SAMPLE_BARCODE;
        new TMisMonitorSampleInfoLogic().Edit(sampleinfo);

        return "1";
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strWorkTask_Id, string strPlanID, string strSampleAskDate, string strSampleFinishDate, string strTicketNum, string strQCStatus)
    {
        bool IsSuccess = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTask_Id;
        objTask.PLAN_ID = strPlanID;
        objTask = new TMisMonitorTaskLogic().Details(objTask);


        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = objTask.ID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        //判断是否经过质控，如果经过质控，则将数据发送到质控环节
        if (!String.IsNullOrEmpty(strQCStatus) && strQCStatus == "1")
        {
            TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
            objTaskEdit.ID = objTask.ID;
            objTaskEdit.QC_STATUS = "2";
            objTaskEdit.TICKET_NUM = strTicketNum;
            objTaskEdit.ASKING_DATE = strSampleFinishDate;//黄进军添加20140901
            //如果是环境质量类 将 SEND_STATUS 设置为1
            if (objTask.TASK_TYPE == "1")
            {
                objTaskEdit.SEND_STATUS = "1";
            }
            else
            {
                //分配任务报告编制人
                if (objTask.REPORT_HANDLE == "")
                    objTaskEdit.REPORT_HANDLE = getNextReportUserID("Report_UserID");
            }
            if (new TMisMonitorTaskLogic().Edit(objTaskEdit))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSubTask.ID = dt.Rows[i]["ID"].ToString();
                    //objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
                    objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
                    if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                        IsSuccess = false;
                }
            }

        }
        //如果不需要质控，则直接发送到采样环节
        else
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objSubTask.ID = dt.Rows[i]["ID"].ToString();
                objSubTask.TASK_STATUS = "02";
                objSubTask.TASK_TYPE = "发送";
                //objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
                objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
                if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                    IsSuccess = false;
            }

            if (IsSuccess)
            {
                TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
                objTaskEdit.ID = objTask.ID;
                objTaskEdit.TICKET_NUM = strTicketNum;
                objTaskEdit.ASKING_DATE = strSampleFinishDate;//黄进军添加20140901
                objTaskEdit.QC_STATUS = "8";//表示已经完成质控设置
                //如果是环境质量类 将 SEND_STATUS 设置为1
                if (objTask.TASK_TYPE == "1")
                {
                    objTaskEdit.SEND_STATUS = "1";
                }
                else
                {
                    //分配任务报告编制人
                    if (objTask.REPORT_HANDLE == "")
                        objTaskEdit.REPORT_HANDLE = getNextReportUserID("Report_UserID");
                }
                new TMisMonitorTaskLogic().Edit(objTaskEdit);

                TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
                objPlan.HAS_DONE = "1";
                new TMisContractPlanLogic().Edit(objPlan);
            }

        }

        TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";
        List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
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
            //噪声除外的所有样品设置样品编号 Create By weilin 2014-03-19
            if (dt.Rows[i]["MONITOR_ID"].ToString() != "000000004" && dt.Rows[i]["MONITOR_ID"].ToString() != "FunctionNoise" && dt.Rows[i]["MONITOR_ID"].ToString() != "AreaNoise" && dt.Rows[i]["MONITOR_ID"].ToString() != "EnvRoadNoise")
            {
                objSubTask = new TMisMonitorSubtaskLogic().Details(dt.Rows[i]["ID"].ToString());
                objSample = new TMisMonitorSampleInfoVo();
                objSample.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
                list = new TMisMonitorSampleInfoLogic().SelectByObject(objSample, 0, 0);
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].SAMPLE_CODE.Length == 0)
                    {
                        objSample.ID = list[j].ID;

                        if (objTask.TASK_TYPE == "1")
                            objSample.SAMPLE_CODE = list[j].SAMPLE_NAME;
                        else
                            objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubTask);

                        new TMisMonitorSampleInfoLogic().Edit(objSample);
                    }
                }
            }
        }
        SplitAcceptTask(strWorkTask_Id, objSerial);
        return IsSuccess == true ? "1" : "0";
        //bool IsSuccess = true;
        //TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        //objTask.PLAN_ID = strPlanID;
        //objTask = new TMisMonitorTaskLogic().Details(objTask);

        //TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        //objSubTask.TASK_ID = objTask.ID;

        //DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    objSubTask.ID = dt.Rows[i]["ID"].ToString();
        //    objSubTask.TASK_STATUS = "02";
        //    objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
        //    objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
        //    if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
        //        IsSuccess = false;

        //    //TMisMonitorSubtaskAppVo objSubtaskApp = new TMisMonitorSubtaskAppVo();

        //    //objSubtaskApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
        //    //objSubtaskApp.SUBTASK_ID = objSubTask.ID;
        //    //objSubtaskApp.SAMPLE_ASSIGN_ID = LogInfo.UserInfo.ID;
        //    //objSubtaskApp.SAMPLE_ASSIGN_DATE = DateTime.Now.ToString();

        //    //new TMisMonitorSubtaskAppLogic().Create(objSubtaskApp);
        //}

        //if (IsSuccess)
        //{
        //    TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
        //    objPlan.HAS_DONE = "1";
        //    new TMisContractPlanLogic().Edit(objPlan);
        //}
        //return IsSuccess == true ? "1" : "0";
    }
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
    #region 打印任务单
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return;

        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(this.PLAN_ID.Value);
        string strContractID = objPlan.CONTRACT_ID;
        TMisContractVo objContract = new TMisContractLogic().Details(strContractID);
        TMisContractCompanyVo objCompany = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);

        string strPointNames = "";
        string strItemS = "";
        int intPointCount = 0;
        GetInfoForPrint(ref strPointNames, ref intPointCount, ref strItemS);

        //string strSAMPLE_ASK_DATE = this.strSAMPLE_ASK_DATE.Value;
        //string strSAMPLE_FINISH_DATE = this.strSAMPLE_FINISH_DATE.Value;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/例行监测任务单.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        sheet.GetRow(3).GetCell(1).SetCellValue(objContract.PROJECT_NAME);
        sheet.GetRow(3).GetCell(3).SetCellValue(objCompany.COMPANY_NAME);
        sheet.GetRow(4).GetCell(1).SetCellValue(objCompany.MONITOR_ADDRESS);
        sheet.GetRow(4).GetCell(3).SetCellValue(objCompany.CONTACT_NAME + "、" + objCompany.PHONE);
        //sheet.GetRow(5).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        //sheet.GetRow(5).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);

        //获取当前监测类别信息
        DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable(objPlan.ID);
        DataTable dtPoint = GetPendingPlanPointDataTable(objPlan.ID);
        if (dtMonitor.Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow drr in dtMonitor.Rows)
            {
                string strMonitorName = "", strPointName = "", strOutValuePoint = "", strOutValuePointItems = "";
                DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                if (drPoint.Length > 0)
                {

                    foreach (DataRow drrPoint in drPoint)
                    {
                        string strPointNameForItems = "", strPointItems = "";
                        strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString();
                        strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                        //获取当前点位的监测项目
                        DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                        if (dtPointItems.Rows.Count > 0)
                        {
                            foreach (DataRow drItems in dtPointItems.Rows)
                            {
                                strPointNameForItems = drrPoint["POINT_NAME"] + ":";
                                strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                            }
                            strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                        }
                    }
                    //获取输出监测类型监测点位信息
                    strOutValuePoint += strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                }

                sheet.GetRow(i + 7).GetCell(0).SetCellValue(strMonitorName);
                sheet.GetRow(i + 7).GetCell(1).SetCellValue(strOutValuePoint);
                sheet.GetRow(i + 7).GetCell(2).SetCellValue(strOutValuePointItems);

                i++;
            }
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
                "attachment;filename=" + HttpUtility.UrlEncode("采样任务分配表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void GetInfoForPrint(ref string strPointNames, ref int intPointCount, ref string strItemS)
    {
        if (this.PLAN_ID.Value.Length == 0)
            return;

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.PLAN_ID.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GetPoint_UnderTask(dt.Rows[i]["ID"].ToString(), ref strPointNames, ref intPointCount, ref strItemS);
        }
    }

    //点位
    private void GetPoint_UnderTask(string strSubTaskID, ref string strPointNames, ref int intPointCount, ref string strItemS)
    {
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);

        string strtmpPointNameS = "";
        string strtmpItems = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(dt.Rows[i]["POINT_ID"].ToString());
            if (!strtmpPointNameS.Contains(objTaskPoint.POINT_NAME))
            {
                strtmpPointNameS += (strtmpPointNameS.Length > 0 ? "\r\n" : "") + objTaskPoint.POINT_NAME;
            }
            GetItem_UnderSample(dt.Rows[i]["ID"].ToString(), ref strtmpItems);
        }

        strItemS += (strItemS.Length > 0 ? "、" : "") + strtmpItems;
        strPointNames += (strPointNames.Length > 0 ? "\r\n" : "") + strtmpPointNameS;
        intPointCount += dt.Rows.Count;
    }

    //项目
    private void GetItem_UnderSample(string strSampleID, ref string strItems)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(dt.Rows[i]["ITEM_ID"].ToString());
            if (!strItems.Contains(objItemInfo.ITEM_NAME))
            {
                strItems += (strItems.Length > 0 ? "、" : "") + objItemInfo.ITEM_NAME;
            }
        }
    }
    #endregion

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
            objSubTask.TASK_STATUS = "01";
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
                            objUpSubTask.SAMPLING_MANAGER_ID = drr["USERID"].ToString();
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


    //设置点位的监测项目数据
    [WebMethod]
    public static string SaveDataItem(string strSubtaskID, string strSample, string strSelItem_IDs)
    {
        bool isSuccess = true;

        string[] arrSelItemId = strSelItem_IDs.Split(',');

        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSample);
        TMisMonitorTaskItemVo objPointItemSet = new TMisMonitorTaskItemVo();
        objPointItemSet.IS_DEL = "1";
        TMisMonitorTaskItemVo objPointItemWhere = new TMisMonitorTaskItemVo();
        objPointItemWhere.IS_DEL = "0";
        objPointItemWhere.TASK_POINT_ID = objSample.POINT_ID;
        new TMisMonitorTaskItemLogic().Edit(objPointItemSet, objPointItemWhere);


        TMisMonitorResultVo objResult = new TMisMonitorResultVo();

        objResult = new TMisMonitorResultVo();
        if (strSample.Length > 0)
        {
            objResult.SAMPLE_ID = strSample;
            new TMisMonitorResultLogic().Delete(objResult);
        }


        if (strSelItem_IDs.Length > 0)
        {
            for (int i = 0; i < arrSelItemId.Length; i++)
            {
                TMisMonitorTaskItemVo objPointItem = new TMisMonitorTaskItemVo();
                objPointItem.ID = GetSerialNumber("t_mis_monitor_task_item_id");
                objPointItem.IS_DEL = "0";
                objPointItem.TASK_POINT_ID = objSample.POINT_ID;
                objPointItem.ITEM_ID = arrSelItemId[i];

                isSuccess = new TMisMonitorTaskItemLogic().Create(objPointItem);

                objResult = new TMisMonitorResultVo();
                objResult.ID = GetSerialNumber("MonitorResultId");
                objResult.SAMPLE_ID = objSample.ID;
                objResult.ITEM_ID = arrSelItemId[i];
                objResult.QC_TYPE = objSample.QC_TYPE;
                objResult.RESULT_STATUS = "01";

                //填充默认分析方法和方法依据
                TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
                objItemAnalysis.ITEM_ID = arrSelItemId[i];
                objItemAnalysis.IS_DEFAULT = "是";
                objItemAnalysis.IS_DEL = "0";
                objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);

                if (objItemAnalysis.ID.Length == 0)
                {
                    objItemAnalysis.ITEM_ID = arrSelItemId[i];
                    objItemAnalysis.IS_DEL = "0";
                    objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                }
                //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                objResult.RESULT_CHECKOUT = objItemAnalysis.LOWER_CHECKOUT;
                //objMethod.METHOD_ID = objMethod.METHOD_ID;

                isSuccess = new TMisMonitorResultLogic().Create(objResult);

                string strAnalysisManagerID = "";
                string strAnalysisManID = "";
                TMisMonitorResultVo objResultTemp = new TMisMonitorResultVo();
                objResultTemp.ID = objResult.ID;
                DataTable dtManager = new TMisMonitorResultLogic().SelectManagerByTable(objResultTemp);
                if (dtManager.Rows.Count > 0)
                {
                    strAnalysisManagerID = dtManager.Rows[0]["ANALYSIS_MANAGER"].ToString();
                    strAnalysisManID = dtManager.Rows[0]["ANALYSIS_ID"].ToString();
                }
                TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                objResultApp.ID = GetSerialNumber("MonitorResultAppId");
                objResultApp.RESULT_ID = objResult.ID;
                objResultApp.HEAD_USERID = strAnalysisManagerID;
                objResultApp.ASSISTANT_USERID = strAnalysisManID;

                isSuccess = new TMisMonitorResultAppLogic().Create(objResultApp);
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    ///  //黄飞  20150820  采样任务分配中  点位增加修改   保存
    /// </summary>
    /// <param name="strPointID"></param>
    /// <param name="strSubtaskID"></param>
    /// <param name="strPOINT_NAME"></param>
    /// <param name="strMONITOR_ID"></param>
    /// <param name="strPOINT_TYPE"></param>
    /// <param name="strDYNAMIC_ATTRIBUTE_ID"></param>
    /// <param name="strSAMPLEFREQ"></param>
    /// <param name="strCREATE_DATE"></param>
    /// <param name="strADDRESS"></param>
    /// <param name="strLONGITUDE"></param>
    /// <param name="strLATITUDE"></param>
    /// <param name="strNUM"></param>
    /// <param name="strAttribute"></param>
    /// <param name="strNATIONAL_ST_CONDITION_ID"></param>
    /// <param name="strLOCAL_ST_CONDITION_ID"></param>
    /// <param name="strINDUSTRY_ST_CONDITION_ID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveData(string strPointID, string strSubtaskID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID, string strSAMPLEFREQ,
        string strCREATE_DATE, string strADDRESS, string strLONGITUDE, string strLATITUDE, string strNUM, string strAttribute,
        string strNATIONAL_ST_CONDITION_ID, string strLOCAL_ST_CONDITION_ID, string strINDUSTRY_ST_CONDITION_ID)
    {
        bool isSuccess = true;
        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        //if (strPointID.Length < 0)
        //{
        objPoint.ID = strPointID.Length > 0 ? strPointID : GetSerialNumber("t_mis_monitor_taskpointId");
        //}
        objPoint.IS_DEL = "0";
        objPoint.SUBTASK_ID = strSubtaskID;
        objPoint.POINT_NAME = strPOINT_NAME;
        objPoint.MONITOR_ID = strMONITOR_ID;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.FREQ = "1";
        objPoint.SAMPLE_FREQ = strSAMPLEFREQ;
        objPoint.CREATE_DATE = strCREATE_DATE;
        objPoint.ADDRESS = strADDRESS;
        objPoint.LONGITUDE = strLONGITUDE;
        objPoint.LATITUDE = strLATITUDE;
        objPoint.NUM = strNUM;

        objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
        objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
        objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        objPoint.TASK_ID = objTask.ID;

        //监测任务出现新增排口时，基础资料企业表也要新增
        TBaseCompanyPointVo objnewPoint = new TBaseCompanyPointVo();
        if (strPointID.Length == 0)
        {
            objnewPoint.ID = GetSerialNumber("t_base_company_point_id");
            objnewPoint.IS_DEL = "0";
            objnewPoint.POINT_NAME = strPOINT_NAME;
            objnewPoint.MONITOR_ID = strMONITOR_ID;
            objnewPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
            objnewPoint.FREQ = "1";
            objnewPoint.SAMPLE_FREQ = strSAMPLEFREQ;
            objnewPoint.CREATE_DATE = strCREATE_DATE;
            objnewPoint.ADDRESS = strADDRESS;
            objnewPoint.LONGITUDE = strLONGITUDE;
            objnewPoint.LATITUDE = strLATITUDE;
            objnewPoint.NUM = strNUM;

            objnewPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
            objnewPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
            objnewPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

            TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
            objTaskCompany.TASK_ID = objTask.ID; ;
            objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(objTaskCompany);

            TMisContractCompanyVo objContractCompany = new TMisContractCompanyLogic().Details(objTaskCompany.COMPANY_ID);
            objnewPoint.COMPANY_ID = objContractCompany.COMPANY_ID;

            new TBaseCompanyPointLogic().Create(objnewPoint);

            objPoint.POINT_ID = objnewPoint.ID;
        }

        if (strPointID.Length > 0)
        {
            //objPoint.ID = strPointID;
            isSuccess = new TMisMonitorTaskPointLogic().Edit(objPoint);
        }
        else
        {
            isSuccess = new TMisMonitorTaskPointLogic().Create(objPoint);

            //增加点位样品信息
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
            objSample.ID = GetSerialNumber("MonitorSampleId");
            objSample.SUBTASK_ID = strSubtaskID;
            objSample.QC_TYPE = "0";
            objSample.NOSAMPLE = "0";
            //黄飞 20150821 "录入样品交接状态：已接收/未接收。
            objSample.REMARK5 = "未交接";
            objSample.POINT_ID = objPoint.ID;
            objSample.SAMPLE_NAME = objPoint.POINT_NAME;
            //新增点位时候，自动生成该点位的样品编码
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
            objSerial.SERIAL_TYPE = "2";

            objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

            objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

            new TMisMonitorSampleInfoLogic().Create(objSample);
        }

        TBaseAttrbuteValue3Logic logicAttrValue = new TBaseAttrbuteValue3Logic();

        //清掉原有动态属性值
        TBaseAttrbuteValue3Vo objAttrValueDelWhere = new TBaseAttrbuteValue3Vo();
        objAttrValueDelWhere.OBJECT_ID = objPoint.ID;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValue3Vo objAttrValueDelSet = new TBaseAttrbuteValue3Vo();
        objAttrValueDelSet.IS_DEL = "1";
        logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere);

        //新增动态属性值
        if (strAttribute.Length > 0)
        {
            string[] arrAttribute = strAttribute.Split('-');
            for (int i = 0; i < arrAttribute.Length; i++)
            {
                string[] arrAttrValue = arrAttribute[i].Split('|');

                TBaseAttrbuteValue3Vo objAttrValueAdd = new TBaseAttrbuteValue3Vo();
                objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrValueAdd.IS_DEL = "0";
                objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                objAttrValueAdd.OBJECT_ID = objPoint.ID;
                objAttrValueAdd.ATTRBUTE_CODE = arrAttrValue[1];
                objAttrValueAdd.ATTRBUTE_VALUE = arrAttrValue[2];
                isSuccess = logicAttrValue.Create(objAttrValueAdd);
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
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
    /// <summary>
    /// 根据监测天数拆分任务（验收监测） Create By:weilin 2014-12-04
    /// </summary>
    /// <param name="strTaskID"></param>
    /// <returns></returns>
    private static bool SplitAcceptTask(string strTaskID, TBaseSerialruleVo objSerial)
    {
        bool bResult = true;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(strTaskID);
        TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
        if (objTaskVo.CONTRACT_TYPE == "05")  //验收监测
        {
            TMisContractVo objContractVo = new TMisContractLogic().Details(objTaskVo.CONTRACT_ID);
            TMisContractPointVo objContractPointVo = new TMisContractPointVo();
            objContractPointVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
            objContractPointVo.IS_DEL = "0";
            objContractPointVo = new TMisContractPointLogic().Details(objContractPointVo);
            if (objContractPointVo.SAMPLE_DAY != "" && IsNumeric(objContractPointVo.SAMPLE_DAY) && objContractVo.REMARK5 == "")
            {
                int iSampleDay = int.Parse(objContractPointVo.SAMPLE_DAY);
                TMisMonitorTaskCompanyVo objClient = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.CLIENT_COMPANY_ID);
                TMisMonitorTaskCompanyVo objTested = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.TESTED_COMPANY_ID);
                for (int i = 1; i < iSampleDay; i++)
                {
                    TMisMonitorTaskVo objTaskAddVo = new TMisMonitorTaskVo();
                    objTaskAddVo.ID = GetSerialNumber("t_mis_monitor_taskId");
                    objTaskAddVo.CONTRACT_ID = objTaskVo.CONTRACT_ID;
                    objTaskAddVo.PLAN_ID = objTaskVo.PLAN_ID;
                    objTaskAddVo.CONTRACT_CODE = objTaskVo.CONTRACT_CODE;
                    objTaskAddVo.TICKET_NUM = objTaskVo.TICKET_NUM + "-" + (i + 1).ToString();
                    objTaskAddVo.PROJECT_NAME = objTaskVo.PROJECT_NAME;
                    objTaskAddVo.CONTRACT_YEAR = objTaskVo.CONTRACT_YEAR;
                    objTaskAddVo.CONTRACT_TYPE = objTaskVo.CONTRACT_TYPE;
                    objTaskAddVo.TEST_TYPE = objTaskVo.TEST_TYPE;
                    objTaskAddVo.TEST_PURPOSE = objTaskVo.TEST_PURPOSE;
                    objTaskAddVo.ASKING_DATE = objTaskVo.ASKING_DATE;
                    objTaskAddVo.SAMPLE_SOURCE = objTaskVo.SAMPLE_SOURCE;
                    objTaskAddVo.CREATOR_ID = objTaskVo.CREATOR_ID;
                    objTaskAddVo.CREATE_DATE = objTaskVo.CREATE_DATE;
                    objTaskAddVo.TASK_STATUS = objTaskVo.TASK_STATUS;
                    objTaskAddVo.REMARK1 = objTaskVo.REMARK1;
                    objTaskAddVo.REMARK2 = objTaskVo.REMARK2;
                    objTaskAddVo.REMARK3 = objTaskVo.REMARK3;
                    objTaskAddVo.QC_STATUS = objTaskVo.QC_STATUS;
                    objTaskAddVo.COMFIRM_STATUS = objTaskVo.COMFIRM_STATUS;
                    objTaskAddVo.ALLQC_STATUS = objTaskVo.ALLQC_STATUS;
                    objTaskAddVo.REPORT_HANDLE = objTaskVo.REPORT_HANDLE;

                    //构造监测任务委托企业信息
                    TMisMonitorTaskCompanyVo objClientAdd = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objClient, objClientAdd);//复制对象
                    objClientAdd.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objClientAdd.TASK_ID = objTaskAddVo.ID;
                    //构造监测任务受检企业信息
                    TMisMonitorTaskCompanyVo objTestedAdd = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objTested, objTestedAdd);//复制对象
                    objTestedAdd.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objTestedAdd.TASK_ID = objTaskAddVo.ID;

                    objTaskAddVo.CLIENT_COMPANY_ID = objClientAdd.ID;
                    objTaskAddVo.TESTED_COMPANY_ID = objTestedAdd.ID;

                    TMisMonitorReportVo objReportAdd = new TMisMonitorReportVo();
                    objReportVo.ID = GetSerialNumber("t_mis_monitor_report_id");
                    objReportVo.REPORT_CODE = objReportVo.REPORT_CODE + "-" + (i + 1).ToString();
                    objReportVo.TASK_ID = objTaskAddVo.ID;
                    objReportVo.IF_GET = "0";

                    //监测子任务信息 根据委托书监测类别进行构造
                    ArrayList arrSubTask = new ArrayList();//监测子任务集合
                    ArrayList arrSubTaskApp = new ArrayList();
                    ArrayList arrTaskPoint = new ArrayList();//监测点位集合
                    ArrayList arrPointItem = new ArrayList();//监测点位明细集合
                    ArrayList arrSample = new ArrayList();//样品集合
                    ArrayList arrSampleResult = new ArrayList();//样品结果集合 
                    ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表

                    List<TMisMonitorSubtaskVo> listSubTask = new TMisMonitorSubtaskLogic().SelectByObject(new TMisMonitorSubtaskVo() { TASK_ID = objTaskVo.ID }, 0, 0);
                    for (int j = 0; j < listSubTask.Count; j++)
                    {
                        TMisMonitorSubtaskVo objSubTaskAdd = new TMisMonitorSubtaskVo();
                        CopyObject(listSubTask[j], objSubTaskAdd);
                        objSubTaskAdd.ID = GetSerialNumber("t_mis_monitor_subtaskId");
                        objSubTaskAdd.TASK_ID = objTaskAddVo.ID;
                        arrSubTask.Add(objSubTaskAdd);

                        TMisMonitorSubtaskAppVo objSubTaskApp = new TMisMonitorSubtaskAppVo();
                        objSubTaskApp.SUBTASK_ID = listSubTask[j].ID;
                        objSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(objSubTaskApp);
                        TMisMonitorSubtaskAppVo objSubTaskAppAdd = new TMisMonitorSubtaskAppVo();
                        CopyObject(objSubTaskApp, objSubTaskAppAdd);
                        objSubTaskAppAdd.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                        objSubTaskAppAdd.SUBTASK_ID = objSubTaskAdd.ID;
                        arrSubTaskApp.Add(objSubTaskAppAdd);

                        List<TMisMonitorTaskPointVo> listTaskPoint = new TMisMonitorTaskPointLogic().SelectByObject(new TMisMonitorTaskPointVo() { TASK_ID = objTaskVo.ID, SUBTASK_ID = listSubTask[j].ID, IS_DEL = "0" }, 0, 0);
                        for (int k = 0; k < listTaskPoint.Count; k++)
                        {
                            TMisMonitorTaskPointVo objTaskPointAdd = new TMisMonitorTaskPointVo();
                            CopyObject(listTaskPoint[k], objTaskPointAdd);
                            objTaskPointAdd.ID = GetSerialNumber("t_mis_monitor_taskpointId");
                            objTaskPointAdd.TASK_ID = objTaskAddVo.ID;
                            arrTaskPoint.Add(objTaskPointAdd);

                            TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoVo();
                            objSampleInfoVo.SUBTASK_ID = listSubTask[j].ID;
                            objSampleInfoVo.POINT_ID = listTaskPoint[k].ID;
                            objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(objSampleInfoVo);
                            TMisMonitorSampleInfoVo objSampleInfoAdd = new TMisMonitorSampleInfoVo();
                            CopyObject(objSampleInfoVo, objSampleInfoAdd);
                            objSampleInfoAdd.ID = GetSerialNumber("MonitorSampleId");
                            objSampleInfoAdd.SUBTASK_ID = objSubTaskAdd.ID;
                            objSampleInfoAdd.POINT_ID = objTaskPointAdd.ID;
                            objSampleInfoAdd.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTaskAddVo, objSubTaskAdd);
                            arrSample.Add(objSampleInfoAdd);

                            List<TMisMonitorTaskItemVo> listTaskItem = new TMisMonitorTaskItemLogic().SelectByObject(new TMisMonitorTaskItemVo() { TASK_POINT_ID = listTaskPoint[k].ID, IS_DEL = "0" }, 0, 0);
                            for (int l = 0; l < listTaskItem.Count; l++)
                            {
                                TMisMonitorTaskItemVo objTaskItemAdd = new TMisMonitorTaskItemVo();
                                CopyObject(listTaskItem[l], objTaskItemAdd);
                                objTaskItemAdd.ID = GetSerialNumber("t_mis_monitor_task_item_id");
                                objTaskItemAdd.TASK_POINT_ID = objTaskPointAdd.ID;
                                arrPointItem.Add(objTaskItemAdd);
                            }

                            List<TMisMonitorResultVo> listResult = new TMisMonitorResultLogic().SelectByObject(new TMisMonitorResultVo() { SAMPLE_ID = objSampleInfoVo.ID }, 0, 0);
                            for (int l = 0; l < listResult.Count; l++)
                            {
                                TMisMonitorResultVo objResultAdd = new TMisMonitorResultVo();
                                CopyObject(listResult[l], objResultAdd);
                                objResultAdd.ID = GetSerialNumber("MonitorResultId");
                                objResultAdd.SAMPLE_ID = objSampleInfoAdd.ID;
                                arrSampleResult.Add(objResultAdd);

                                TMisMonitorResultAppVo objResultAppVo = new TMisMonitorResultAppVo();
                                objResultAppVo.RESULT_ID = listResult[l].ID;
                                objResultAppVo = new TMisMonitorResultAppLogic().Details(objResultAppVo);
                                TMisMonitorResultAppVo objResultAppAdd = new TMisMonitorResultAppVo();
                                CopyObject(objResultAppVo, objResultAppAdd);
                                objResultAppAdd.ID = GetSerialNumber("MonitorResultAppId");
                                objResultAppAdd.RESULT_ID = objResultAdd.ID;
                                arrSampleResultApp.Add(objResultAppAdd);
                            }
                        }

                    }
                    if (new TMisMonitorTaskLogic().SaveTrans(objTaskAddVo, objClientAdd, objTestedAdd, objReportVo, arrTaskPoint, arrSubTask, arrSubTaskApp, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
                    {
                        bResult = true;
                    }
                }
                //更新任务单号
                objTaskVo.TICKET_NUM = objTaskVo.TICKET_NUM + "-1";
                new TMisMonitorTaskLogic().Edit(objTaskVo);
                //更新报告单号
                objReportVo.REPORT_CODE = objReportVo.REPORT_CODE + "-1";
                new TMisMonitorReportLogic().Edit(objReportVo);
                //更新验收委托书任务已经拆分任务的标志
                objContractVo.REMARK5 = "1";
                new TMisContractLogic().Edit(objContractVo);
            }
        }

        return bResult;
    }

    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        int nRet;
        nRet = PrintLab.OpenPort("POSTEK C168/200s");//打开打印机端口
        nRet = PrintLab.PTK_ClearBuffer();           //清空缓冲区
        nRet = PrintLab.PTK_SetPrintSpeed(4);        //设置打印速度
        nRet = PrintLab.PTK_SetDarkness(10);         //设置打印黑度
        nRet = PrintLab.PTK_SetLabelHeight(360, 16); //设置标签的高度和定位间隙\黑线\穿孔的高度
        nRet = PrintLab.PTK_SetLabelWidth(600);      //设置标签的宽度

        for (int i = 1; i <= 1; i++)
        {

            // 画矩形
            nRet = PrintLab.PTK_DrawRectangle(42, 30, 5, 558, 260);

            // 画表格分割线
            nRet = PrintLab.PTK_DrawLineOr(42, 107, 516, 5);
            nRet = PrintLab.PTK_DrawLineOr(42, 184, 516, 5);
            //PrintLab.PTK_DrawLineOr(42, 261, 516, 5);

            // 打印一行TrueTypeFont文字;123456789

            //string Name = "AA"; //Request.Form["Name"].ToString();
            //string std = "BB"; //Request.Form["std"].ToString();
            //string Time = "CC"; //Request.Form["Time"].ToString();
            nRet = PrintLab.PTK_DrawTextTrueTypeW(70, 50, 40, 0, "Arial", 1, 600, false, false, false, "A1", "任务编号:");
            nRet = PrintLab.PTK_DrawTextTrueTypeW(70, 130, 40, 0, "Arial", 1, 600, false, false, false, "A2", "样品编号:");
            nRet = PrintLab.PTK_DrawTextTrueTypeW(70, 200, 40, 0, "Arial", 1, 600, false, false, false, "A3", "采样日期:");

            // 打印一个条码;

            nRet = PrintLab.PTK_DrawBarcode(240, 280, 0, "1", 2, 4, 50, 'B', "afdsfds1123");


            //// 打印PCX图片 方式一
            //PrintLab.PTK_PcxGraphicsDel("PCX");
            //PrintLab.PTK_PcxGraphicsDownload("PCX", "logo.pcx");
            //PrintLab.PTK_DrawPcxGraphics(80, 20, "PCX");

            //// 打印PCX图片 方式二
            //// PTK_PrintPCX(80,30,pchar('logo.pcx'));           

            //// 打印一行文本文字(内置字体或软字体);
            //PrintLab.PTK_DrawText(80, 168, 0, 3, 1, 1, 'N', "Internal Soft Font");

            //// 打印PDF417码
            //PrintLab.PTK_DrawBar2D_Pdf417(80, 210, 400, 300, 0, 0, 3, 7, 10, 2, 0, 0, "123456789");//PDF417码

            //// 打印QR码
            //PrintLab.PTK_DrawBar2D_QR(420, 120, 180, 180, 0, 3, 2, 0, 0, "Postek Electronics Co., Ltd.");


            //// 打印一行TrueTypeFont文字旋转;
            //PrintLab.PTK_DrawTextTrueTypeW(520, 102, 22, 0, "Arial", 2, 400, false, false, false, "A2", "www.postek.com.cn");
            //PrintLab.PTK_DrawTextTrueTypeW(80, 260, 19, 0, "Arial", 1, 700, false, false, false, "A3", "Use different ID_NAME for different Truetype font objects");


            // 命令打印机执行打印工作
            nRet = PrintLab.PTK_PrintLabel(10, 1);
            nRet = PrintLab.ClosePort();//关闭打印机端口
        }
    }

    private string BarCodePrint(string strProjectName, string strSampleCode, string strBarCode)
    {
        PrintLab.OpenPort("POSTEK C168/200s");//打开打印机端口
        PrintLab.PTK_ClearBuffer();           //清空缓冲区
        PrintLab.PTK_SetPrintSpeed(4);        //设置打印速度
        PrintLab.PTK_SetDarkness(10);         //设置打印黑度
        PrintLab.PTK_SetLabelHeight(360, 16); //设置标签的高度和定位间隙\黑线\穿孔的高度
        PrintLab.PTK_SetLabelWidth(600);      //设置标签的宽度

        for (int i = 1; i <= 1; i++)
        {

            // 画矩形
            PrintLab.PTK_DrawRectangle(42, 30, 5, 558, 260);

            // 画表格分割线
            PrintLab.PTK_DrawLineOr(42, 107, 516, 5);
            PrintLab.PTK_DrawLineOr(42, 184, 516, 5);
            //PrintLab.PTK_DrawLineOr(42, 261, 516, 5);

            // 打印一行TrueTypeFont文字;123456789

            //string Name = "AA"; //Request.Form["Name"].ToString();
            //string std = "BB"; //Request.Form["std"].ToString();
            //string Time = "CC"; //Request.Form["Time"].ToString();
            PrintLab.PTK_DrawTextTrueTypeW(70, 50, 40, 0, "Arial", 1, 600, false, false, false, "A1", "任务编号:" + strProjectName);
            PrintLab.PTK_DrawTextTrueTypeW(70, 130, 40, 0, "Arial", 1, 600, false, false, false, "A2", "样品编号:" + strSampleCode);
            PrintLab.PTK_DrawTextTrueTypeW(70, 200, 40, 0, "Arial", 1, 600, false, false, false, "A3", "采样日期:");

            // 打印一个条码;

            PrintLab.PTK_DrawBarcode(240, 280, 0, "1", 2, 4, 50, 'B', strBarCode);


            //// 打印PCX图片 方式一
            //PrintLab.PTK_PcxGraphicsDel("PCX");
            //PrintLab.PTK_PcxGraphicsDownload("PCX", "logo.pcx");
            //PrintLab.PTK_DrawPcxGraphics(80, 20, "PCX");

            //// 打印PCX图片 方式二
            //// PTK_PrintPCX(80,30,pchar('logo.pcx'));           

            //// 打印一行文本文字(内置字体或软字体);
            //PrintLab.PTK_DrawText(80, 168, 0, 3, 1, 1, 'N', "Internal Soft Font");

            //// 打印PDF417码
            //PrintLab.PTK_DrawBar2D_Pdf417(80, 210, 400, 300, 0, 0, 3, 7, 10, 2, 0, 0, "123456789");//PDF417码

            //// 打印QR码
            //PrintLab.PTK_DrawBar2D_QR(420, 120, 180, 180, 0, 3, 2, 0, 0, "Postek Electronics Co., Ltd.");


            //// 打印一行TrueTypeFont文字旋转;
            //PrintLab.PTK_DrawTextTrueTypeW(520, 102, 22, 0, "Arial", 2, 400, false, false, false, "A2", "www.postek.com.cn");
            //PrintLab.PTK_DrawTextTrueTypeW(80, 260, 19, 0, "Arial", 1, 700, false, false, false, "A3", "Use different ID_NAME for different Truetype font objects");


            // 命令打印机执行打印工作
            PrintLab.PTK_PrintLabel(1, 1);
            PrintLab.ClosePort();//关闭打印机端口
        }
        return "true";
    }

    [WebMethod]
    public static string btnPrintClick(string strProjectName, string strSampleName, string strSampleCode, string strBarCode)
    {
        PrintLab.OpenPort("POSTEK C168/200s");//打开打印机端口
        PrintLab.PTK_ClearBuffer();           //清空缓冲区
        PrintLab.PTK_SetPrintSpeed(4);        //设置打印速度
        PrintLab.PTK_SetDarkness(10);         //设置打印黑度
        PrintLab.PTK_SetLabelHeight(360, 16); //设置标签的高度和定位间隙\黑线\穿孔的高度
        PrintLab.PTK_SetLabelWidth(600);      //设置标签的宽度

        for (int i = 1; i <= 1; i++)
        {

            // 画矩形
            PrintLab.PTK_DrawRectangle(42, 30, 5, 558, 260);

            // 画表格分割线
            PrintLab.PTK_DrawLineOr(42, 107, 516, 5);
            PrintLab.PTK_DrawLineOr(42, 184, 516, 5);
            //PrintLab.PTK_DrawLineOr(42, 261, 516, 5);

            // 打印一行TrueTypeFont文字;123456789

            //string Name = "AA"; //Request.Form["Name"].ToString();
            //string std = "BB"; //Request.Form["std"].ToString();
            //string Time = "CC"; //Request.Form["Time"].ToString();
            PrintLab.PTK_DrawTextTrueTypeW(70, 50, 40, 0, "Arial", 1, 600, false, false, false, "A1", "任务编号:" + strProjectName);
            PrintLab.PTK_DrawTextTrueTypeW(70, 130, 40, 0, "Arial", 1, 600, false, false, false, "A2", "样品编号:" + strSampleCode);
            PrintLab.PTK_DrawTextTrueTypeW(70, 200, 40, 0, "Arial", 1, 600, false, false, false, "A3", "采样日期:");

            // 打印一个条码;

            PrintLab.PTK_DrawBarcode(240, 280, 0, "1", 2, 4, 50, 'B', strBarCode);


            //// 打印PCX图片 方式一
            //PrintLab.PTK_PcxGraphicsDel("PCX");
            //PrintLab.PTK_PcxGraphicsDownload("PCX", "logo.pcx");
            //PrintLab.PTK_DrawPcxGraphics(80, 20, "PCX");

            //// 打印PCX图片 方式二
            //// PTK_PrintPCX(80,30,pchar('logo.pcx'));           

            //// 打印一行文本文字(内置字体或软字体);
            //PrintLab.PTK_DrawText(80, 168, 0, 3, 1, 1, 'N', "Internal Soft Font");

            //// 打印PDF417码
            //PrintLab.PTK_DrawBar2D_Pdf417(80, 210, 400, 300, 0, 0, 3, 7, 10, 2, 0, 0, "123456789");//PDF417码

            //// 打印QR码
            //PrintLab.PTK_DrawBar2D_QR(420, 120, 180, 180, 0, 3, 2, 0, 0, "Postek Electronics Co., Ltd.");


            //// 打印一行TrueTypeFont文字旋转;
            //PrintLab.PTK_DrawTextTrueTypeW(520, 102, 22, 0, "Arial", 2, 400, false, false, false, "A2", "www.postek.com.cn");
            //PrintLab.PTK_DrawTextTrueTypeW(80, 260, 19, 0, "Arial", 1, 700, false, false, false, "A3", "Use different ID_NAME for different Truetype font objects");


            // 命令打印机执行打印工作
            PrintLab.PTK_PrintLabel(1, 1);
            PrintLab.ClosePort();//关闭打印机端口
        }
        return "true";
    }


    public class PrintLab
    {
        [DllImport("WINPSK.dll")]
        public static extern int OpenPort(string printname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetPrintSpeed(uint px);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetDarkness(uint id);
        [DllImport("WINPSK.dll")]
        public static extern int ClosePort();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PrintLabel(uint number, uint cpnumber);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawTextTrueTypeW
                                            (int x, int y, int FHeight,
                                            int FWidth, string FType,
                                            int Fspin, int FWeight,
                                            bool FItalic, bool FUnline,
                                            bool FStrikeOut,
                                            string id_name,
                                            string data);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBarcode(uint px,
                                        uint py,
                                        uint pdirec,
                                        string pCode,
                                        uint pHorizontal,
                                        uint pVertical,
                                        uint pbright,
                                        char ptext,
                                        string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelHeight(uint lheight, uint gapH);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelWidth(uint lwidth);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_ClearBuffer();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawRectangle(uint px, uint py, uint thickness, uint pEx, uint pEy);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawLineOr(uint px, uint py, uint pLength, uint pH);
        //[DllImport("WINPSK.dll")]
        //public static extern int PTK_DrawBar2D_QR( uint x,uint y, uint w,  uint v,uint o,  uint r,uint m,  uint g,uint s,string pstr);
        //[DllImport("WINPSK.dll")]
        //public static extern int PTK_DrawBar2D_Pdf417(uint x, uint  y,uint w, uint v,uint s, uint c,uint px, uint  py,uint r, uint l,uint t, uint o,string pstr);
        //[DllImport("WINPSK.dll")]
        //public static extern int PTK_PcxGraphicsDel(string pid);
        //[DllImport("WINPSK.dll")]
        //public static extern int PTK_PcxGraphicsDownload(string pcxname, string pcxpath);
        //[DllImport("WINPSK.dll")]
        //public static extern int PTK_DrawPcxGraphics(uint px, uint py, string gname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawText(uint px, uint py, uint pdirec, uint pFont, uint pHorizontal, uint pVertical, char ptext, string pstr);


    }
}