using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Channels.OA.SW;
using System.Configuration;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Contract;
using WebApplication;
using System.Xml.Linq;
using System.Web.Script.Serialization;

public partial class Portal_Welcome_ZZ_New : PageBase
{
    #region//变量定义
    protected string strTaskHasCount = "0";       //待办任务
    //protected string strTodayTaskHasCount = "0";        //今日待办
    //protected string strTaskFinishCount = "0";        //已办总数
    protected string strTaskBackCount = "0";       //退回任务
    protected string ReportCount = "0";//报告办理
    //protected string strFwTaskAllCount = "0";//待办发文
    //protected string strFwTaskBackCount = "0";//退回发文
    //protected string strSwTaskAllCount = "0";//收文办理
    //protected string strSwTaskBackCount = "0";//退回收文
    protected string TaskPlanCount = "0";//采样预约
    protected string TaskPlan10Count = "0"; //监督性(国控)预约
    protected string TaskPlan11Count = "0"; //监督性(省控)预约
    protected string TaskPlan12Count = "0"; //监督性(重金属)预约
    protected string TaskPlan13Count = "0"; //监督性预约
    protected string TaskPlan14Count = "0"; //年度委托预约
    protected string TaskDoPlanCount = "0";//预约办理、采样任务分配
    protected string SamplingCount = "0"; //采样任务
    protected string AllocationSheetCount = "0"; //样品交接
    protected string TaskAllocationCount = "0"; //样品分发
    protected string AnalysisCount = "0";//监测分析
    protected string ResultCheckCount = "0"; //分析结果复核
    protected string MasterQcCheckCount = "0"; //分析主任审核
    protected string ResultQcCheckCount = "0"; //质控审核
    protected string SampleAnalysisCheckCount = "0"; //分析现场项目结果核录
    protected string SampleCheckCount = "0"; //现场监测结果复核
    protected string SampleQcCheckCount = "0"; //现场室主任审核
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Hidden1.Value = ConfigurationManager.AppSettings["Welcome"];
        ShowData();


        if (Request["GetCCFlow"] == "GetCCFlow")
        {
            var works = CCFlowFacade.GetEmpWorks(this.LogInfo.UserInfo.USER_NAME, new string[][] { }, null, null);

            var xe = XElement.Parse(works).Element("record").Elements();

            var group = from p in xe
                        where !Server.UrlDecode(p.Element("AtPara").Value).Contains("IsCC=1")
                        group p by p.Element("FK_Node").Value into grps
                        select new
                        {
                            FK_Node = grps.Key,

                            Total = grps.Count(),
                            NodeName = Server.UrlDecode(grps.First().Element("NodeName").Value)
                        };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(group);

            Response.Write(result);
            Response.End();
        }

        if (Request["GetCCFlow"] == "GetCCFlowBatch")
        {
            var works = CCFlowFacade.GetBatchWorks(this.LogInfo.UserInfo.USER_NAME);

            var xe = XElement.Parse(works).Element("record").Elements();

            var group = from p in xe

                        select new
                        {
                            FK_Node = p.Element("NodeID").Value,

                            NUM = p.Element("NUM").Value,
                            NodeName = Server.UrlDecode(p.Element("Name").Value)
                        };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(group);

            Response.Write(result);
            Response.End();
        }

        if (Request["GetCCFlow"] == "GetCCFlowCC")
        {
            //待办中的抄送
            var works = CCFlowFacade.GetEmpWorks(this.LogInfo.UserInfo.USER_NAME, new string[][] { }, null, null);

            var xe = XElement.Parse(works).Element("record").Elements().Where(t => Server.UrlDecode(t.Element("AtPara").Value).Contains("IsCC=1"));



            //抄送
            var queryParams = new List<string[]>();

            queryParams.Add(new string[3] { "Sta", "0", "eq" });

            var ccWorks = CCFlowFacade.GetCC(this.LogInfo.UserInfo.USER_NAME, queryParams.ToArray(), null, null);

            var inWorks = xe.Select(t => t.Element("WorkID").Value + t.Element("FK_Node").Value + t.Element("FK_Emp").Value);

            var ccXE = XElement.Parse(ccWorks).Element("record").Elements()
                .Where(t => !inWorks.Contains(t.Element("WorkID").Value + t.Element("FK_Node").Value + t.Element("CCTo").Value));

            var group = (from p in xe
                         group p by p.Element("FK_Node").Value into grps
                         select new
                         {
                             FK_Node = grps.Key,
                             Total = grps.Count(),
                             NodeName = Server.UrlDecode(grps.First().Element("NodeName").Value),
                             IsToDo = true
                         }).Union
                        (
                        from p in ccXE
                        group p by p.Element("FK_Node").Value into grps
                        select new
                        {
                            FK_Node = grps.Key,
                            Total = grps.Count(),
                            NodeName = Server.UrlDecode(grps.First().Element("NodeName").Value),
                            IsToDo = false
                        }
                        );

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(group);

            Response.Write(result);
            Response.End();
        }
    }
    protected void ShowData()
    {
        #region//任务数量
        //待办任务
        strTaskHasCount = new TWfInstTaskDetailLogic().GetSelectResultCountForUserTaskList(LogInfo.UserInfo.ID, TWfCommDict.StepState.StateNormal, "", "", "", "").ToString();
        //今日待办
        //strTodayTaskHasCount = new TWfInstTaskDetailLogic().GetSelectResultCountForDayTaskList(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepState.StateNormal }, DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.ToShortDateString()).ToString();
        //已办总数
        //strTaskFinishCount = new TWfInstTaskDetailLogic().GetSelectResultCount(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepState.StateDown }).ToString();
        //退回任务
        //strTaskBackCount = new TWfInstTaskDetailLogic().GetSelectResultCount(new TWfInstTaskDetailVo() { OBJECT_USER = LogInfo.UserInfo.ID, INST_TASK_DEAL_STATE = i3.ValueObject.Sys.WF.TWfCommDict.StepDealState.ForBack }).ToString();
        //报告办理   
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();  //监测任务对象
        objTask.TASK_STATUS = "09";
        if (!base.LogInfo.UserInfo.REAL_NAME.Contains("管理员"))
        {
            objTask.REPORT_HANDLE = LogInfo.UserInfo.ID;
        }
        objTask.COMFIRM_STATUS = "0";
        ReportCount = new TMisMonitorTaskLogic().GetSelectResultCount(objTask).ToString();
        ////收文办理
        //strSwTaskAllCount = new TOaSwInfoLogic().GetSwResultCount(LogInfo.UserInfo.ID);
        ////发文待办
        //strFwTaskAllCount = new TWfInstTaskDetailLogic().Get_FW_Count(LogInfo.UserInfo.ID, TWfCommDict.StepState.StateNormal);
        ////发文退回
        //strFwTaskBackCount = new TWfInstTaskDetailLogic().Get_FW_Count(LogInfo.UserInfo.ID, TWfCommDict.StepDealState.ForBack);
        //采样预约
        objTask = new TMisMonitorTaskVo();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        TMisContractVo objItemContract = new TMisContractVo();
        objItemContract.CONTRACT_STATUS = "9";
        TaskPlanCount = new TMisContractPlanLogic().GetSelectByTablePlanForTaskCount(objItems, objItemContract, objTask, false).ToString();
        //监督性(国控)预约
        TMisContractPointFreqVo objFreq = new TMisContractPointFreqVo();
        TMisContractVo objCv = new TMisContractVo();
        TMisContractCompanyVo objCom = new TMisContractCompanyVo();
        objCv.CONTRACT_STATUS = "9";
        objFreq.IF_PLAN = "0";
        objCv.CONTRACT_TYPE = "10";
        TaskPlan10Count = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objFreq, objCv, objCom).ToString();
        //监督性(省控)预约
        objCv.CONTRACT_TYPE = "11";
        TaskPlan11Count = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objFreq, objCv, objCom).ToString();
        //监督性(重金属)预约
        objCv.CONTRACT_TYPE = "12";
        TaskPlan12Count = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objFreq, objCv, objCom).ToString();
        //监督性预约
        objCv.CONTRACT_TYPE = "13";
        TaskPlan13Count = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objFreq, objCv, objCom).ToString();
        //年度委托预约
        objCv.CONTRACT_TYPE = "14";
        TaskPlan14Count = new TMisContractPointFreqLogic().SelectContractPlanInforCount(objFreq, objCv, objCom).ToString();
        //预约办理
        objTask.QC_STATUS = "2|3";
        TaskDoPlanCount = new TMisContractPlanLogic().GetSelectByTablePlanForDoTaskCount(objItems, objItemContract, objTask, true).ToString();
        //采样任务
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        SamplingCount = new TMisMonitorSubtaskLogic().SelectByTableWithAllTaskForFatherTree(objSubtask, "", "02", "", "", LogInfo.UserInfo.ID, 0, 0).Rows.Count.ToString();
        //样品交接
        AllocationSheetCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "sample_allocation_sheet", "021", "'01','00'").ToString();
        //样品分发
        TaskAllocationCount = new TMisMonitorResultLogic().getTaskInfoCount(LogInfo.UserInfo.ID, "duty_other_analyse", "03", "'01','00'").ToString();
        //监测分析
        AnalysisCount = new TMisMonitorResultLogic().getResultInResultFlowCount_QY(LogInfo.UserInfo.ID, "20").ToString();
        //分析结果复核
        ResultCheckCount = new TMisMonitorResultLogic().getTaskInfoCheckCount_QY(LogInfo.UserInfo.ID, "03", "30").ToString();
        //分析主任审核
        MasterQcCheckCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_QY(LogInfo.UserInfo.ID, "analysis_result_check", "03", "50").ToString();
        //质控审核
        ResultQcCheckCount = new TMisMonitorResultLogic().getTaskInfoQcCheckCount_QY(LogInfo.UserInfo.ID, "analysis_result_qc_check", "03", "40").ToString();
        //分析现场项目结果核录
        SampleAnalysisCheckCount = new TMisMonitorTaskLogic().SelectSampleTaskForWithSampleAnalysisCountQY(LogInfo.UserInfo.ID, "sample_result_check", "'02','022'", "0", "50").ToString();
        //现场监测结果复核
        SampleCheckCount = new TMisMonitorSubtaskLogic().SelectSamplingCheckListCount(LogInfo.UserInfo.ID, "022", true).ToString();
        //现场室主任审核
        SampleQcCheckCount = new TMisMonitorSubtaskLogic().SelectSamplingCheckListCount(LogInfo.UserInfo.ID, "023", false).ToString();
        #endregion

        #region//任务项数判断
        if (strTaskHasCount == "0")
        {
            this.strTask.Attributes.CssStyle.Value = "display:none";
        }
        if (strTaskBackCount == "0")
        {
            this.strTaskBack.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlanCount == "0")
        {
            this.TaskPlan.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlan10Count == "0")
        {
            this.TaskPlan_10.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlan11Count == "0")
        {
            this.TaskPlan_11.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlan12Count == "0")
        {
            this.TaskPlan_12.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlan13Count == "0")
        {
            this.TaskPlan_13.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskPlan14Count == "0")
        {
            this.TaskPlan_14.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskDoPlanCount == "0")
        {
            this.TaskDoPlan.Attributes.CssStyle.Value = "display:none";
        }
        if (SamplingCount == "0")
        {
            this.Sampling.Attributes.CssStyle.Value = "display:none";
        }
        if (AllocationSheetCount == "0")
        {
            this.AllocationSheet.Attributes.CssStyle.Value = "display:none";
        }
        if (TaskAllocationCount == "0")
        {
            this.TaskAllocation.Attributes.CssStyle.Value = "display:none";
        }
        if (AnalysisCount == "0")
        {
            this.Analysis.Attributes.CssStyle.Value = "display:none";
        }
        if (ReportCount == "0")
        {
            this.ReportManage.Attributes.CssStyle.Value = "display:none";
        }
        if (ResultCheckCount == "0")
        {
            this.ResultCheck.Attributes.CssStyle.Value = "display:none";
        }
        if (MasterQcCheckCount == "0")
        {
            this.MasterQcCheck.Attributes.CssStyle.Value = "display:none";
        }
        if (ResultQcCheckCount == "0")
        {
            this.ResultQcCheck.Attributes.CssStyle.Value = "display:none";
        }
        if (SampleAnalysisCheckCount == "0")
        {
            this.SampleAnalysisCheck.Attributes.CssStyle.Value = "display:none";
        }
        if (SampleCheckCount == "0")
        {
            this.SampleCheck.Attributes.CssStyle.Value = "display:none";
        }
        if (SampleQcCheckCount == "0")
        {
            this.SampleQcCheck.Attributes.CssStyle.Value = "display:none";
        }
        #endregion
    }
    /// <summary>
    /// 获取当前系统登录用户有权限的所有菜单信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getMenuInfo()
    {
        string strUserID = new PageBase().LogInfo.UserInfo.ID;
        DataTable dtUserMenu = null;
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.IS_SHORTCUT = "0";
        menuvo.MENU_TYPE = "Menu";
        if (strUserID == "000000001")
        {
            dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
        }
        else
        {
            menuvo.IS_HIDE = "0";
            dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, strUserID);
        }
        return DataTableToJson(dtUserMenu);
    }
}