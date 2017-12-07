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

using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Company;

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
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：采样任务分配
/// 创建日期：2012-12-6
/// 创建人  ：苏成斌
/// 修改内容：增加任务单打印
/// 修改日期：2013-01-18
/// 修改人  :  潘德军
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplingTaskAllocation : PageBase
{
    private static string strQC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
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
    /// 任务单号设置
    /// </summary>
    /// <param name="CONTRACT_TYPE">委托类型</param>
    /// <returns></returns>
    protected string SetTaskCode(string CONTRACT_TYPE)
    {
        string strTaskCode = i3.View.PageBase.GetSerialNumber("ticket_num");
        //委托监测：01；监督性监测：02；临时委托监测：03；自送样监测：04；验收监测：05；排污许可证监测：06；信访监测：07；
        if (CONTRACT_TYPE == "05")
            strTaskCode = "验" + strTaskCode;
        if (CONTRACT_TYPE == "06")
            strTaskCode = "证" + strTaskCode;
        if (CONTRACT_TYPE == "01" || CONTRACT_TYPE == "02" || CONTRACT_TYPE == "03" || CONTRACT_TYPE == "04" || CONTRACT_TYPE == "07")
            strTaskCode = "数" + strTaskCode;

        return strTaskCode;
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
        if (objTask.TICKET_NUM.Length == 0)
        {
            objTask.TICKET_NUM = SetTaskCode(objTask.CONTRACT_TYPE);
            new TMisMonitorTaskLogic().Edit(objTask);
        }

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
            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
            objSampleInfo.SUBTASK_ID = dt.Rows[i]["ID"].ToString();
            DataTable objDtSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            if (objDtSample.Rows.Count > 0)
            {
                DataRow[] objDtMuMiRow = objDtSample.Select(" QC_TYPE='0'");
                foreach (DataRow dr in objDtMuMiRow)
                {
                    //样品编号
                    if (dr["SAMPLE_CODE"].ToString().Length == 0)
                    {
                        //TMisMonitorSubtaskVo objSubtaskEx = new TMisMonitorSubtaskLogic().Details(dr["SUBTASK_ID"].ToString());
                        //TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                        //objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
                        //objSerial.SERIAL_TYPE = "2";

                        //TMisMonitorSampleInfoVo objSampleInfoEx = new TMisMonitorSampleInfoVo();
                        //objSampleInfoEx.ID = dr["ID"].ToString();
                        //objSampleInfoEx.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

                        //objSampleInfoEx.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtaskEx);

                        //new TMisMonitorSampleInfoLogic().Edit(objSampleInfoEx);

                    }



                    DataTable objDt = new TMisMonitorSampleInfoLogic().GetSampleInforForEnvQcSettingTable(dr["POINT_ID"].ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
                    if (objDt.Rows.Count > 0)
                    {
                        string strItemArry = "";
                        DataRow[] objExistRow = null;
                        //查找是否有做现场平行的质控计划
                        objExistRow = objDtSample.Select(" POINT_ID='" + dr["POINT_ID"].ToString() + "' AND QC_TYPE='3'");
                        if (objExistRow.Length <= 0)
                        {
                            DataRow[] drRow_PX = objDt.Select("QC_TYPE='3'");
                            if (drRow_PX.Length > 0)
                            {
                                strItemArry = "";
                                deleteSampleInfo(dr["ID"].ToString(), "3");
                                foreach (DataRow drItem in drRow_PX)
                                {
                                    strItemArry += drItem["ITEM_ID"].ToString() + ",";
                                }
                                strItemArry = strItemArry.Substring(0, strItemArry.Length - 1);
                                createQcInfo(dr["ID"].ToString(), "3", strItemArry);
                            }
                        }
                        //查找是否有做现场平行的空白计划
                        objExistRow = objDtSample.Select(" POINT_ID='" + dr["POINT_ID"].ToString() + "' AND QC_TYPE='1'");
                        if (objExistRow.Length <= 0)
                        {
                            DataRow[] drRow_KB = objDt.Select(" QC_TYPE='1'");

                            if (drRow_KB.Length > 0)
                            {
                                strItemArry = "";
                                deleteSampleInfo(dr["ID"].ToString(), "1");
                                foreach (DataRow drItem in drRow_KB)
                                {
                                    strItemArry += drItem["ITEM_ID"].ToString() + ",";
                                }
                                strItemArry = strItemArry.Substring(0, strItemArry.Length - 1);
                                createQcInfo(dr["ID"].ToString(), "1", strItemArry);
                            }
                        }
                    }
                }
            }
            if (dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = new TSysUserLogic().Details(dt.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
            else
                dt.Rows[i]["SAMPLING_MANAGER_ID"] = "请选择";
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
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strPlanID, string strSampleAskDate, string strSampleFinishDate, string strTicketNum, string strQCStatus)
    {
        bool IsSuccess = true;
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
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
            //如果是环境质量类 将 SEND_STATUS 设置为1
            if (objTask.TASK_TYPE == "1")
            {
                objTaskEdit.SEND_STATUS = "1";
            }
            if (new TMisMonitorTaskLogic().Edit(objTaskEdit))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSubTask.ID = dt.Rows[i]["ID"].ToString();
                    objSubTask.SAMPLE_ASK_DATE = strSampleAskDate;
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
                //objSubTask.SAMPLE_ASK_DATE = strSampleAskDate; update by xwh 2013.07.31
                objSubTask.SAMPLE_FINISH_DATE = strSampleFinishDate;
                if (!new TMisMonitorSubtaskLogic().Edit(objSubTask))
                    IsSuccess = false;
            }

            if (IsSuccess)
            {
                TMisMonitorTaskVo objTaskEdit = new TMisMonitorTaskVo();
                objTaskEdit.ID = objTask.ID;
                objTaskEdit.TICKET_NUM = strTicketNum;
                objTaskEdit.QC_STATUS = "8";//表示已经完成质控设置

                //如果是环境质量类 将 SEND_STATUS 设置为1
                if (objTask.TASK_TYPE == "1")
                {
                    objTaskEdit.SEND_STATUS = "1";
                }
                new TMisMonitorTaskLogic().Edit(objTaskEdit);

                TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanID);
                objPlan.HAS_DONE = "1";
                new TMisContractPlanLogic().Edit(objPlan);
            }

        }

        TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable dtApp = new TMisMonitorSubtaskAppLogic().SelectByTable(new TMisMonitorSubtaskAppVo { SUBTASK_ID = dt.Rows[i]["ID"].ToString() });
            if (dtApp.Rows.Count > 0)
            {
                objSubApp.ID = dtApp.Rows[i]["ID"].ToString();
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
        }
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
            objSubTask.TASK_STATUS = "010";
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
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.PLAN_ID = PLAN_ID.Value.Trim();
        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
        TMisMonitorTaskCompanyVo objTaskCompanyVo = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.TESTED_COMPANY_ID);

        string strContractType = getDictName(objTaskVo.CONTRACT_TYPE, "Contract_Type");

        string strMonitorNames = "";
        string strPointNames = "";
        string strItemS = "";
        string strFREQ = "";
        GetInfoForPrint(ref strMonitorNames, ref strPointNames, ref strFREQ, ref strItemS);

        string strSAMPLE_ASK_DATE = DateTime.Now.ToString("yyyy-MM-dd");
        string strSAMPLE_FINISH_DATE = DateTime.Parse(objTaskVo.ASKING_DATE).ToString("yyyy-MM-dd");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/MoniterTaskNotify.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        sheet.GetRow(3).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(4).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(5).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(3).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(4).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(5).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(7).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(7).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(7).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(7).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(8).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(8).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);
        //复制一份
        sheet.GetRow(16).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(17).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(18).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(16).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(17).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(18).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(20).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(20).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(20).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(20).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(21).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(21).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("任务通知单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void GetInfoForPrint(ref string strMonitorNames, ref string strPointNames, ref string strFREQ, ref string strItemS)
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
            TBaseMonitorTypeInfoVo objMonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(dt.Rows[i]["MONITOR_ID"].ToString());
            strMonitorNames += objMonitorTypeInfoVo.MONITOR_TYPE_NAME + "\n";
            GetPoint_UnderTask(objTask.CONTACT_ID, dt.Rows[i]["ID"].ToString(), ref strPointNames, ref strFREQ, ref strItemS);
        }
    }

    //点位
    private void GetPoint_UnderTask(string strContractID, string strSubTaskID, ref string strPointNames, ref string strFREQ, ref string strItemS)
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
                strtmpPointNameS += (strtmpPointNameS.Length > 0 ? "、" : "") + objTaskPoint.POINT_NAME;

                GetItem_UnderSample(dt.Rows[i]["ID"].ToString(), objTaskPoint.POINT_NAME, ref strtmpItems);

                TMisContractPointVo objContractPointVo = new TMisContractPointVo();
                objContractPointVo = new TMisContractPointLogic().Details(objTaskPoint.CONTRACT_POINT_ID);
                strFREQ += (strFREQ.Length > 0 ? "\n" : "") + objTaskPoint.POINT_NAME + "：连续" + (objContractPointVo.SAMPLE_DAY == "" ? "1" : objContractPointVo.SAMPLE_DAY) + "天，每天" + (objContractPointVo.SAMPLE_FREQ == "" ? "1" : objContractPointVo.SAMPLE_FREQ) + "次";
            }

        }

        strItemS += (strItemS.Length > 0 ? "\n" : "") + strtmpItems;
        strPointNames += (strPointNames.Length > 0 ? "\r\n" : "") + strtmpPointNameS;

    }

    //项目
    private void GetItem_UnderSample(string strSampleID, string strPointName, ref string strItems)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, 0, 0);

        strItems += (strItems.Length > 0 ? "\n" : "") + strPointName + "：";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(dt.Rows[i]["ITEM_ID"].ToString());

            strItems += objItemInfo.ITEM_NAME + "、";

        }
        strItems = strItems.TrimEnd('、');
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


    private bool createQcInfo(string strSampleID, string strQcType, string strItemId)
    {
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        string strQcSampleId = GetSerialNumber("MonitorSampleId");
        objSample.ID = strQcSampleId;
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        //objSample.SAMPLE_CODE = GetSampleCode_QHD(strSampleID);
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        //objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
        //objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

        if (strQcType == "3")
            objSample.SAMPLE_NAME += "现场平行";
        if (strQcType == "1")
            objSample.SAMPLE_NAME += "现场空白";
        if (strQcType == "4")
            objSample.SAMPLE_NAME += "密码平行";

        //在样品表中添加样品数据
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        //遍历监测项目信息，将监测项目信息添加到结果表、结果分析执行表、平行样结果表中
        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            //根据需要做质控的监测项目获取原始样的结果表ID
            TMisMonitorResultVo objSourceResult = new TMisMonitorResultVo();
            objSourceResult.SAMPLE_ID = strSampleID;
            objSourceResult.QC_TYPE = "0";
            objSourceResult.ITEM_ID = strItemId.Split(',')[i];
            TMisMonitorResultVo objResult = new TMisMonitorResultLogic().Details(objSourceResult);
            string strSourceId = objResult.ID;

            //将数据写入结果表中
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = strQcSampleId;
            objResult.QC_TYPE = strQcType;
            objResult.SOURCE_ID = strSourceId;
            objResult.QC_SOURCE_ID = strSourceId;
            if (!new TMisMonitorResultLogic().Create(objResult))
                isSuccess = false;

            //将数据写入结果分析执行表中
            InsertResultAPP(objResult.ID);

            //将结果写入分析样结果表中
            TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
            objQcTwin.ID = GetSerialNumber("QcTwinId");
            objQcTwin.RESULT_ID_SRC = strSourceId;
            objQcTwin.RESULT_ID_TWIN1 = objResult.ID;
            objQcTwin.QC_TYPE = strQcType;
            if (!new TMisMonitorQcTwinLogic().Create(objQcTwin))
                isSuccess = false;
        }
        return isSuccess;
    }
    /// <summary>
    /// 删除质控样品信息
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    private static void deleteSampleInfo(string strSampleID, string strQcType)
    {
        new TMisMonitorResultLogic().deleteQcInfo(strSampleID, strQcType);
    }
}