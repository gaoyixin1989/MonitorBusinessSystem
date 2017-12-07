using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Duty;

public partial class Channels_Base_Search_TaskFlow_Sample : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
        {
            this.TASK_ID.Value = Request.QueryString["task_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Monitor_ID"]))
        {
            this.Monitor_ID.Value = Request.QueryString["Monitor_ID"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["step_type"]))
        {
            this.step_type.Value = Request.QueryString["step_type"].ToString();
        }
        if (!IsPostBack)
        {
            //ReturnAllTaskStep();
            DrawStep();
        }
    }

    protected void DrawStep()
    {
        //定义已处理DIV样式
        //string strDivHas = "<div style='width:150px;height:auto;background-color:#5a8f5a;text-align: center;vertical-align: middle;'>{0}</div>";
        string strDivHas = "<div class='listgreen'><h2>{0}</h2><p><span>环节状态：</span><strong>已处理</strong><br /><span>处理者：</span>{1}&nbsp;&nbsp;<span>办理时间：</span>{2}</p><img src='../../../sys/wf/img/down2012.gif' /></div>";
        //定义待处理DIV样式
        //string strDivWait = "<div style='width:150px;height:auto;background-color:#de9a1d;text-align: center;vertical-align: middle;'>{0}</div>";
        string strDivWait = "<div class='listyellow'><h2>{0}</h2><p><span>环节状态：</span><strong>待处理</strong><br /><span>处理者：</span>{1}&nbsp;&nbsp;<span>办理时间：</span>{2}</p><img src='../../../sys/wf/img/down2012.gif' /></div>";

        //任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        //无现场监测子任务信息
        TMisMonitorSubtaskVo objNoSampleSubTask = new TMisMonitorSubtaskLogic().GetNoSampleSubTaskInfo(this.TASK_ID.Value, this.Monitor_ID.Value);
        //现场子任务
        TMisMonitorSubtaskVo objSampleSubTask = new TMisMonitorSubtaskLogic().GetSampleSubTaskInfo(this.TASK_ID.Value, this.Monitor_ID.Value);
        //监测子任务审核表 T_MIS_MONITOR_SUBTASK_APP
        TMisMonitorSubtaskAppVo objSubTaskApp = new TMisMonitorSubtaskAppVo();
        if (objNoSampleSubTask.ID.Length > 0)
        {
            objSubTaskApp.SUBTASK_ID = objNoSampleSubTask.ID;
            objSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(objSubTaskApp);
        }
        //监测子任务审核表 T_MIS_MONITOR_SUBTASK_APP
        TMisMonitorSubtaskAppVo objSampleSubTaskApp = new TMisMonitorSubtaskAppVo();
        if (objSampleSubTask.ID.Length > 0)
        {
            objSampleSubTaskApp.SUBTASK_ID = objSampleSubTask.ID;
            objSampleSubTaskApp = new TMisMonitorSubtaskAppLogic().Details(objSampleSubTaskApp);
        }
        //样品信息
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(new TMisMonitorSampleInfoVo()
        {
            SUBTASK_ID = objNoSampleSubTask.ID,
            QC_TYPE = "0"
        });
        DataTable dtSampleInfo = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo()
        {
            SUBTASK_ID = objNoSampleSubTask.ID,
            QC_TYPE = "0"
        });
        DataTable dtResult = new DataTable();
        //样品结果信息
        if (this.step_type.Value == "QHD")
            dtResult = new TMisMonitorResultLogic().SelectResultAndAppWithSubtaskID_QHD(objNoSampleSubTask.ID);
        else
            dtResult = new TMisMonitorResultLogic().SelectResultAndAppWithSubtaskID(objNoSampleSubTask.ID);
        //委托书对象
        TMisContractVo objContract = new TMisContractLogic().Details(objTask.CONTRACT_ID);

        Table table1 = new Table();
        table1.CssClass = "tMain";
        //TableRow row1 = new TableRow();//委托流程连接
        //TableCell cell1 = new TableCell();
        //cell1.Text = string.Format(strDivHas, "开始");
        //row1.Cells.Add(cell1);
        //table1.Rows.Add(row1);

        //this.step_type.Value//ZZ 郑州，QY 清远，QHD 秦皇岛 
        if (objTask.SAMPLE_SOURCE != "送样")//是否送样
        {
            string strShow01Name = this.step_type.Value == "ZZ" ? "采样任务下达" : "采样任务下达";
            string strShow02Name = this.step_type.Value == "ZZ" ? "采样任务分配" : "采样任务分配";
            

            //现场复核人
            string strSampleChecker = GetDefaultOrFirstDutyUser("sample_result_check", this.Monitor_ID.Value);
            //现场审核人
            string strSampleQcChecker = GetDefaultOrFirstDutyUser("sample_result_qccheck", this.Monitor_ID.Value);

            string strSteps = "";
            string strEndSteps = "";

            GetStepStr(objTask, objNoSampleSubTask, objSampleSubTask, ref  strSteps, ref  strEndSteps);

            if (strSteps.Contains(",采样预约,"))
            {
                table1 = GetCommonRow(table1, strDivHas, strShow01Name, GetUserRealName(objTask.CREATOR_ID), objTask.CREATE_DATE, false);
            }
            if (strSteps.Contains(",采样前质控,"))//有 采样前质控
            {
                //if (objTask.QC_STATUS == "9")
                //{
                    table1 = GetCommonRow(table1, strDivHas, "采样前质控", GetUserRealName(objSubTaskApp.QC_USER_ID), objSubTaskApp.QC_DATE, false);
                //}
            }
            if (strSteps.Contains(",技术室主管审核,"))//QHD
            {
                table1 = GetCommonRow(table1, strDivHas, "技术室主管审核", "冯建社", "", false);
            }
            if (strSteps.Contains(",现场室主管审核,"))//QHD
            {
                table1 = GetCommonRow(table1, strDivHas, "现场室主管审核", "吕萍", "", false);
            }
            if (strSteps.Contains(",分析室主管审核,"))//QHD
            {
                table1 = GetCommonRow(table1, strDivHas, "分析室主管审核", "李中秋", "", false);
            }
            if (strSteps.Contains(",预约办理,"))
            {
                table1 = GetCommonRow(table1, strDivHas, strShow02Name, GetUserRealName(objSubTaskApp.ID.Length > 0 ? objSubTaskApp.SAMPLE_ASSIGN_ID : objSampleSubTaskApp.SAMPLE_ASSIGN_ID), objSubTaskApp.ID.Length > 0 ? objSubTaskApp.SAMPLE_ASSIGN_DATE : objSampleSubTaskApp.SAMPLE_ASSIGN_DATE, false);
            }
            if (strSteps.Contains(",采样,"))//采样
            {
                if (objNoSampleSubTask.SAMPLING_MANAGER_ID != "")
                    table1 = GetCommonRow(table1, strDivHas, "采样", GetUserRealName(objNoSampleSubTask.SAMPLING_MANAGER_ID), objSubTaskApp.REMARK1, false);//objNoSampleSubTask.SAMPLE_FINISH_DATE 黄进军修改
                else
                    table1 = GetCommonRow(table1, strDivHas, "采样", GetUserRealName(objSampleSubTask.SAMPLING_MANAGER_ID), objSampleSubTaskApp.REMARK1, false);// 黄进军修改 这里根据吴主任的意思是显示采样人员登录系统处理任务时间
            }
            if (strSteps.Contains(",现场复核,"))//现场复核 清远特有
            {
                if (this.step_type.Value == "QY")//清远特有 
                {
                    table1 = GetCommonRow(table1, strDivHas, "现场复核", GetUserRealName(objSampleSubTaskApp.SAMPLING_CHECK), objSampleSubTaskApp.REMARK2, false);
                }
            }
            if (strSteps.Contains(",现场审核,"))//现场审核
            {
                if (this.step_type.Value != "QHD")//秦皇岛没有 
                {
                    table1 = GetCommonRow(table1, strDivHas, "现场审核", GetUserRealName(objSampleSubTaskApp.SAMPLING_QC_CHECK), objSampleSubTaskApp.REMARK3, false);
                }
            }
            if (strSteps.Contains(",采样后质控,"))//采样后质控
            {
                if (objTask.QC_STATUS == "9")//郑州特有 
                {
                    table1 = GetCommonRow(table1, strDivHas, "采样后质控", GetUserRealName(objSubTaskApp.SAMPLING_END_QC), "", false);
                }
            }
            if (strSteps.Contains(",样品交接,"))
            {
                table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objNoSampleSubTask.SAMPLE_ACCESS_ID), objNoSampleSubTask.SAMPLE_ACCESS_DATE, false);
            }

            if (strEndSteps.Contains(",采样预约,"))
            {
                table1 = GetCommonRow(table1, strDivWait, strShow01Name, "", "", false);
            }
            if (strEndSteps.Contains(",预约办理,"))
            {
                table1 = GetCommonRow(table1, strDivWait, strShow02Name, "", "", false);
            }
            if (strEndSteps.Contains(",采样前质控,"))//郑州特有 
            {
                table1 = GetCommonRow(table1, strDivWait, "采样前质控", "", "", false);
            }
            if (strEndSteps.Contains(",技术室主管审核,"))//QHD 
            {
                table1 = GetCommonRow(table1, strDivWait, "技术室主管审核", "冯建社", "", false);
            }
            if (strEndSteps.Contains(",现场室主管审核,"))//QHD 
            {
                table1 = GetCommonRow(table1, strDivWait, "现场室主管审核", "吕萍", "", false);
            }
            if (strEndSteps.Contains(",分析室主管审核,"))//QHD 
            {
                table1 = GetCommonRow(table1, strDivWait, "分析室主管审核", "李中秋", "", false);
            }
            if (strEndSteps.Contains(",采样,"))
            {
                table1 = GetCommonRow(table1, strDivWait, "采样", GetUserRealName(objNoSampleSubTask.SAMPLING_MANAGER_ID), "", false);
            }
            if (strEndSteps.Contains(",现场复核,"))
            {
                table1 = GetCommonRow(table1, strDivWait, "现场复核", GetUserRealName(objSampleSubTaskApp.SAMPLING_CHECK), "", false);
            }
            if (strEndSteps.Contains(",现场审核,"))
            {
                table1 = GetCommonRow(table1, strDivWait, "现场审核", "", "", false);
            }
            if (strEndSteps.Contains(",采样后质控,"))
            {
                table1 = GetCommonRow(table1, strDivWait, "采样后质控", "", "", false);
            }
            if (strEndSteps.Contains(",样品交接,"))
            {
                table1 = GetCommonRow(table1, strDivWait, "样品交接", "", "", false);
            }
            if (strEndSteps.Contains(",实验室环节,"))
            {
                AddResultTable(ref table1, objTask, objNoSampleSubTask, dtSampleInfo, dtResult, objSubTaskApp, strDivWait, strDivHas, false);
            }
            if (strEndSteps.Contains(",报告,"))
            {
                AddResultTable(ref table1, objTask, objNoSampleSubTask, dtSampleInfo, dtResult, objSubTaskApp, strDivWait, strDivHas, true);
            }
            
        }
        else
        {
            string strShow01Name = this.step_type.Value == "ZZ" ? "自送样任务下达" : "自送样预约";

            string strSteps = "";
            string strEndSteps = "";

            GetSendSampleStepStr(objTask, objNoSampleSubTask, objSampleSubTask, ref  strSteps, ref  strEndSteps);

            if (strSteps.Contains(",自送样预约,"))
            {
                table1 = GetCommonRow(table1, strDivHas, strShow01Name, GetUserRealName(objTask.CREATOR_ID), objTask.CREATE_DATE, false);
            }
            if (strSteps.Contains(",采样后质控,"))//采样后质控
            {
                if (objTask.QC_STATUS == "9")//郑州特有 
                {
                    table1 = GetCommonRow(table1, strDivHas, "采样后质控", GetUserRealName(objSubTaskApp.SAMPLING_END_QC), "", false);
                }
            }
            if (strSteps.Contains(",样品交接,"))
            {
                table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objNoSampleSubTask.SAMPLE_ACCESS_ID), objNoSampleSubTask.SAMPLE_ACCESS_DATE, false);
            }

            switch (strEndSteps)
            {
                case ",自送样预约,":
                    table1 = GetCommonRow(table1, strDivWait, strShow01Name, "", "", false);
                    break;
                case ",采样后质控,":
                    table1 = GetCommonRow(table1, strDivWait, "采样后质控", "", "", false);
                    break;
                case ",样品交接,":
                    table1 = GetCommonRow(table1, strDivWait, "样品交接", "", "", false);
                    break;
                case ",实验室环节,":
                    AddResultTable(ref table1, objTask, objNoSampleSubTask, dtSampleInfo, dtResult, objSubTaskApp, strDivWait, strDivHas, false);
                    break;
                case ",报告,":
                    AddResultTable(ref table1, objTask, objNoSampleSubTask, dtSampleInfo, dtResult, objSubTaskApp, strDivWait, strDivHas, true);
                    break;
            }
        }

        this.divSample.Controls.Add(table1);
    }

    private void GetStepStr(TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objNoSampleSubTask, TMisMonitorSubtaskVo objSampleSubTask,
        ref string strSteps, ref string strEndSteps)
    {
        //当前环节在预约
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "")
        {
            strEndSteps = ",采样预约,";
        }
        //当前环节在预约办理
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && (objTask.QC_STATUS == "1" || objTask.QC_STATUS == "3"))
        {
            if (this.step_type.Value == "QHD")
            {
                if (objTask.STATE == "01")
                    strSteps = ",采样预约,采样前质控,技术室主管审核,现场室主管审核,";
                else
                    strSteps = ",采样预约,采样前质控,技术室主管审核,分析室主管审核,";
            }
            else
            {
                strSteps = ",采样预约,";
            }
            strEndSteps = ",预约办理,";
        }
        //当前环节在采样前质控 有采样前质控
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "2")
        {
            strSteps = ",采样预约,预约办理,";
            strEndSteps = ",采样前质控,";
        }

        //当前环节在采样
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "02")
        {
            if (this.step_type.Value == "QY")
                strSteps = ",采样预约,预约办理,";
            else
                strSteps = ",采样预约,预约办理,采样前质控,";
            strEndSteps = ",采样,";
        }
        //当前环节在采样前质控（QHD）
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "8")
        {
            strSteps = ",采样预约,";
            strEndSteps = ",采样前质控,";
        }
        //当前环节在技术室主管审核（QHD）
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "M11")
        {
            strSteps = ",采样预约,采样前质控,";
            strEndSteps = ",技术室主管审核,";
        }
        //当前环节在现场室主管审核（QHD）
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "M12")
        {
            strSteps = ",采样预约,采样前质控,技术室主管审核,";
            strEndSteps = ",现场室主管审核,";
        }
        //当前环节在现场室主管审核（QHD）
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "01" && objTask.QC_STATUS == "M13")
        {
            strSteps = ",采样预约,采样前质控,技术室主管审核,";
            strEndSteps = ",分析室主管审核,";
        }


        if (objNoSampleSubTask.ID == "" && objSampleSubTask.ID != "")//全是现场分析项目
        {
            //当前环节在现场复核 清远特有 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "022")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",现场复核,";
            }
            //当前环节在现场审核 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "023")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,现场复核,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,";
                strEndSteps = ",现场审核,";
            }
            //当前环节在现场审核完毕 
            if (objTask.TASK_STATUS == "01" && (objSampleSubTask.TASK_STATUS == "024" || objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09"))
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = "";
            }
            if (objTask.TASK_STATUS == "09" || objTask.TASK_STATUS == "10" || objTask.TASK_STATUS == "10.1" || objTask.TASK_STATUS == "10.2" || objTask.TASK_STATUS == "10.3" || objTask.TASK_STATUS == "11")
            {
                if (this.step_type.Value == "QY")
                {
                    strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,";
                    strEndSteps = "";
                }
                else
                {
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                    strEndSteps = ",报告,";
                }
            }
        }
        else if (objNoSampleSubTask.ID != "" && objSampleSubTask.ID == "")//无现场分析项目
        {
            //当前环节在采样后质控 郑州特有 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",采样后质控,";
            }
            //当前环节在样品交接 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,";
                strEndSteps = ",样品交接,";
            }
            //当前环节在分析任务分配
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",实验室环节,";
            }
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if ((objTask.TASK_STATUS == "10" || objTask.TASK_STATUS == "10.1" || objTask.TASK_STATUS == "10.2" || objTask.TASK_STATUS == "10.3") && objNoSampleSubTask.TASK_STATUS == "09")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "11" )
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            //秦皇岛流程
            if (this.step_type.Value == "QHD")
            {
                if (objTask.TASK_STATUS == "01" && (objNoSampleSubTask.TASK_STATUS == "02" || objNoSampleSubTask.TASK_STATUS == "021" || objNoSampleSubTask.TASK_STATUS == "03"))
                {
                    if (objTask.STATE == "01")
                        strSteps = ",采样预约,采样前质控,技术室主管审核,现场室主管审核,预约办理,";
                    else
                        strSteps = ",采样预约,采样前质控,技术室主管审核,分析室主管审核,预约办理,";
                    strEndSteps = ",实验室环节,";
                }
                if (objNoSampleSubTask.TASK_STATUS == "09")
                {
                    if (objTask.STATE == "01")
                        strSteps = ",采样预约,采样前质控,技术室主管审核,现场室主管审核,预约办理,";
                    else
                        strSteps = ",采样预约,采样前质控,技术室主管审核,分析室主管审核,预约办理,";
                    strEndSteps = ",报告,";
                }
            }
        }
        else if (objNoSampleSubTask.ID != "" && objSampleSubTask.ID != "")//都有
        {
            //当前环节在现场复核 清远特有 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "022")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,";
                strEndSteps = ",现场复核,";
            }
            //当前环节在现场审核 
            if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "023")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,";
                strEndSteps = ",现场审核,";
            }
            //当前环节在现场审核完毕 
            if (objTask.TASK_STATUS == "01" && (objSampleSubTask.TASK_STATUS == "024" || objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09"))
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = "";
            }
            //当前环节在采样后质控 郑州特有 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,";
                strEndSteps = ",采样后质控,";
            }
            //当前环节在样品交接 
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,";
                strEndSteps = ",样品交接,";
            }
            //当前环节在分析任务分配
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
            {
                strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",实验室环节,";
            }

            if (this.step_type.Value == "QY")
            {
                //当前环节在采样
                if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "02")
                {
                    switch (objNoSampleSubTask.TASK_STATUS)
                    {
                        case "021":
                            strSteps = ",采样预约,预约办理,";
                            strEndSteps = ",采样,样品交接,";
                            break;
                        case "03":
                            strSteps = ",采样预约,预约办理,";
                            strEndSteps = ",采样,实验室环节,";
                            break;
                        case "09":
                            strSteps = ",采样预约,预约办理,";
                            strEndSteps = ",采样,报告,";
                            break;
                    }
                }
                //当前环节在现场审复核
                if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "022")
                {
                    switch (objNoSampleSubTask.TASK_STATUS)
                    {
                        case "021":
                            strSteps = ",采样预约,预约办理,采样,";
                            strEndSteps = ",现场复核,样品交接,";
                            break;
                        case "03":
                            strSteps = ",采样预约,预约办理,采样,样品交接,";
                            strEndSteps = ",现场复核,实验室环节,";
                            break;
                        case "09":
                            strSteps = ",采样预约,预约办理,采样,样品交接,";
                            strEndSteps = ",现场复核,报告,";
                            break;
                    }
                }
                //当前环节在现场审核 
                if (objTask.TASK_STATUS == "01" && objSampleSubTask.TASK_STATUS == "023")
                {
                    switch (objNoSampleSubTask.TASK_STATUS)
                    {
                        case "021":
                            strSteps = ",采样预约,预约办理,采样,现场复核,";
                            strEndSteps = ",现场审核,样品交接,";
                            break;
                        case "03":
                            strSteps = ",采样预约,预约办理,采样,现场复核,样品交接,";
                            strEndSteps = ",现场审核,实验室环节,";
                            break;
                        case "09":
                            strSteps = ",采样预约,预约办理,采样,现场复核,样品交接,";
                            strEndSteps = ",现场审核,报告,";
                            break;
                    }
                }
                //当前环节在现场审核完毕 
                if (objTask.TASK_STATUS == "01" && (objSampleSubTask.TASK_STATUS == "024" || objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09"))
                {
                    switch (objNoSampleSubTask.TASK_STATUS)
                    {
                        case "021":
                            strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,";
                            strEndSteps = ",样品交接,";
                            break;
                        case "03":
                            strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,样品交接,";
                            strEndSteps = ",实验室环节,";
                            break;
                        case "09":
                            strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,样品交接,";
                            strEndSteps = ",报告,";
                            break;
                    }
                }
            }

            if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "10" || objTask.TASK_STATUS == "10.1" || objTask.TASK_STATUS == "10.2" || objTask.TASK_STATUS == "10.3")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
            if (objTask.TASK_STATUS == "11")
            {
                if (this.step_type.Value == "QY")
                    strSteps = ",采样预约,预约办理,采样,现场复核,现场审核,样品交接,";
                else
                    strSteps = ",采样预约,预约办理,采样前质控,采样,现场复核,现场审核,采样后质控,样品交接,";
                strEndSteps = ",报告,";
            }
        }
    }

    private void GetSendSampleStepStr(TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objNoSampleSubTask, TMisMonitorSubtaskVo objSampleSubTask,
        ref string strSteps, ref string strEndSteps)
    {
        //当前环节在自送样预约
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "" && objTask.QC_STATUS == "")
        {
            strEndSteps = ",自送样预约,";
        }

        //当前环节在采样后质控 郑州特有 
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "024")
        {
            strSteps = ",自送样预约,";
            strEndSteps = ",采样后质控,";
        }
        //当前环节在样品交接 
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "021")
        {
            strSteps = ",自送样预约,采样后质控,";
            strEndSteps = ",样品交接,";
        }
        //当前环节在分析任务分配
        if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "03")
        {
            strSteps = ",自送样预约,采样后质控,样品交接,";
            strEndSteps = ",实验室环节,";
        }
        if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")
        {
            strSteps = ",自送样预约,采样后质控,样品交接,";
            strEndSteps = ",报告,";
        }
        if (this.step_type.Value == "QHD")
        {
            //当前环节在自送样预约
            if (objTask.TASK_STATUS == "01" && objNoSampleSubTask.TASK_STATUS == "" && objTask.QC_STATUS == "")
            {
                strEndSteps = ",自送样预约,";
            }
            else if (objNoSampleSubTask.TASK_STATUS == "09")
            {
                strSteps = ",自送样预约,";
                strEndSteps = ",报告,";
            }
            else
            {
                strSteps = ",自送样预约,";
                strEndSteps = ",实验室环节,";
            }
        }
    }

    private void AddResultTable(ref Table table1, TMisMonitorTaskVo objTask, TMisMonitorSubtaskVo objSubTask, DataTable dtSample, DataTable dtResult, TMisMonitorSubtaskAppVo objSubTaskApp, string strDivWait, string strDivHas, bool isfinish)
    {
        //分析任务分配人
        string strAnalyseSender = GetDefaultOrFirstDutyUser("duty_other_analyse", this.Monitor_ID.Value);
        //校核人
        //string strAnalyseChecker = GetDefaultOrFirstDutyUser("duty_other_analyse_result", this.Monitor_ID.Value);
        string strAnalyseChecker =  GetUserRealName(objSubTaskApp.RESULT_AUDIT);
        //复核人
        string strAnalyseAgainChecker =  GetUserRealName(objSubTaskApp.RESULT_CHECK);
        //质量负责人审核 审核人
        string strQcChecker = GetUserRealName(objSubTaskApp.RESULT_QC_CHECK);
        //技术负责人审核 审核人
        string strTechChecker = GetUserRealName(objSubTaskApp.RESULT_QC_CHECK);

        //分析任务分配
        DataRow[] dr1 = dtResult.Select(" RESULT_STATUS in('01','00')");//过滤过分析分配环节任务
        //分析结果录入
        DataRow[] dr2 = dtResult.Select(" RESULT_STATUS='20'");
        //实验室主任复核
        DataRow[] dr3 = dtResult.Select(" RESULT_STATUS='30'");
        //质量科审核 清远：质控审核
        DataRow[] dr4 = dtResult.Select(" RESULT_STATUS='40'");
        //质量负责人审核
        DataRow[] dr5 = dtResult.Select(" RESULT_STATUS='50'");
        //技术负责人审核
        DataRow[] dr6 = dtResult.Select(" RESULT_STATUS='60'");
        DataRow[] dr7 = dtResult.Select("1=2"); ;
        DataRow[] dr8 = dtResult.Select("1=2"); ;  //有数据走现场
        DataRow[] dr9 = dtResult.Select("1=2");  //有数据走分析
        DataRow[] drSample1 = dtSample.Select("1=2"); ; //采样
        DataRow[] drSample2 = dtSample.Select("1=2"); ; //交接
        DataRow[] drSample3 = dtSample.Select("1=2"); ; //其他环节
        if (this.step_type.Value == "QY")
        {
            //现场核录
            dr5 = dtResult.Select(" RESULT_STATUS='50' and IS_ANYSCENE_ITEM='1'");
            //分析主任审核
            dr6 = dtResult.Select(" RESULT_STATUS='50' and IS_ANYSCENE_ITEM='0'");
        }
        if (this.step_type.Value == "QHD")
        {
            if (objTask.STATE == "01")
            {
                dr8 = dtResult.Select("IS_SAMPLEDEPT='是'");
                dr9 = dtResult.Select("IS_SAMPLEDEPT='否'");
            }
            else
            {
                dr8 = dtResult.Select("1=2");
                dr9 = dtResult.Select("1=1");
            }
            drSample1 = dtSample.Select("NOSAMPLE='0'");  //采样环节
            drSample2 = dtSample.Select("NOSAMPLE='1'");  //样品交接
            drSample3 = dtSample.Select("NOSAMPLE='2'");  //样品交接之后的环节
            dr1 = dtResult.Select("RESULT_STATUS='01'");  //采样、交接、分析任务分配
            dr2 = dtResult.Select("RESULT_STATUS='20'");  //监测分析
            dr3 = dtResult.Select("RESULT_STATUS='30'");  //数据审核
            dr4 = dtResult.Select("RESULT_STATUS='40'");  //主任审核
            dr5 = dtResult.Select("RESULT_STATUS='022'"); //现场复核
            dr6 = dtResult.Select("RESULT_STATUS='023'"); //现场审核
            dr7 = dtResult.Select("RESULT_STATUS='50'");  //技术室审核
        }

        string strShowNameAnalyseChecker = "";
        string strShowNameAgainChecker = "";
        string strShowNameQcChecker = "";
        string strShowNameAnyscener = "";
        string strShowNameTechChecker = "技术负责人审核";
        if (this.step_type.Value == "ZZ")
        {
            strShowNameAnalyseChecker = "实验室主任复核";
            strShowNameAgainChecker = "质量科审核";
            strShowNameQcChecker = "质量负责人审核";
        }
        else if (this.step_type.Value == "QY")
        {
            strShowNameAnalyseChecker = "分析结果复核";
            strShowNameAgainChecker = "分析审核";
            strShowNameAnyscener = "现场结果核录";
            strShowNameQcChecker = "质控审核";
        }

        if (this.step_type.Value == "QHD")
        {
            if (!isfinish)
            {
                if (objSubTask.TASK_STATUS == "02")
                {
                    if (objTask.SAMPLE_SOURCE != "送样")
                        table1 = GetCommonRow(table1, strDivWait, "采样", GetUserRealName(objSubTask.SAMPLING_MANAGER_ID), objSubTask.SAMPLE_FINISH_DATE, false);
                    
                    if (drSample2.Length > 0)
                    {
                        table1 = GetCommonRow(table1, strDivWait, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                    }
                    else if (drSample3.Length > 0)
                    {
                        if (dr1.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                            table1 = GetCommonRow(table1, strDivWait, "分析任务分配", "", "", false);
                        }
                        else if (dr2.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", strAnalyseSender, "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                        }
                        else if (dr3.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivWait, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                        }
                        else if (dr4.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                            table1 = GetCommonRow(table1, strDivWait, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                        }
                        else if (dr5.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            table1 = GetCommonRow(table1, strDivWait, "现场复核", "", "", false);
                        }
                        else if (dr6.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                            table1 = GetCommonRow(table1, strDivWait, "现场审核", "", "", false);
                        }
                        else if (dr7.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            if (dr8.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                                table1 = GetCommonRow(table1, strDivHas, "现场审核", "", "", false);
                            }
                            table1 = GetCommonRow(table1, strDivWait, "技术室审核", GetUserRealName(objSubTaskApp.RESULT_QC_CHECK), objSubTaskApp.RESULT_QC_CHECK_DATE, false);
                        }
                    }
                }
                else if (objSubTask.TASK_STATUS == "021")
                {
                    if (objTask.SAMPLE_SOURCE != "送样")
                        table1 = GetCommonRow(table1, strDivHas, "采样", GetUserRealName(objSubTask.SAMPLING_MANAGER_ID), objSubTask.SAMPLE_FINISH_DATE, false);
                    table1 = GetCommonRow(table1, strDivWait, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                    if (drSample3.Length > 0)
                    {
                        if (dr1.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivWait, "分析任务分配", "", "", false);
                        }
                        else if (dr2.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                        }
                        else if (dr3.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivWait, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                        }
                        else if (dr4.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                            table1 = GetCommonRow(table1, strDivWait, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                        }
                        else if (dr5.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            table1 = GetCommonRow(table1, strDivWait, "现场复核", "", "", false);
                        }
                        else if (dr6.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                            table1 = GetCommonRow(table1, strDivWait, "现场审核", "", "", false);
                        }
                        else if (dr7.Length > 0)
                        {
                            if (dr9.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                                table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                                table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                            }
                            if (dr8.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                                table1 = GetCommonRow(table1, strDivHas, "现场审核", "", "", false);
                            }
                            table1 = GetCommonRow(table1, strDivWait, "技术室审核", GetUserRealName(objSubTaskApp.RESULT_QC_CHECK), objSubTaskApp.RESULT_QC_CHECK_DATE, false);
                        }
                    }
                }
                else if (objSubTask.TASK_STATUS == "03")
                {
                    if (objTask.SAMPLE_SOURCE != "送样")
                        table1 = GetCommonRow(table1, strDivHas, "采样", GetUserRealName(objSubTask.SAMPLING_MANAGER_ID), objSubTask.SAMPLE_FINISH_DATE, false);
                    table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                    if (dr1.Length > 0)
                    {
                        table1 = GetCommonRow(table1, strDivWait, "分析任务分配", "", "", false);
                    }
                    else if (dr2.Length > 0)
                    {
                        table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                        table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                    }
                    else if (dr3.Length > 0)
                    {
                        table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                        table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                        table1 = GetCommonRow(table1, strDivWait, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                    }
                    else if (dr4.Length > 0)
                    {
                        table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                        table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                        table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                        table1 = GetCommonRow(table1, strDivWait, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                    }
                    else if (dr5.Length > 0)
                    {
                        if (dr9.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                            table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                        }
                        table1 = GetCommonRow(table1, strDivWait, "现场复核", "", "", false);
                    }
                    else if (dr6.Length > 0)
                    {
                        if (dr9.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                            table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                        }
                        table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                        table1 = GetCommonRow(table1, strDivWait, "现场审核", "", "", false);
                    }
                    else if (dr7.Length > 0)
                    {
                        if (dr9.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                            table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                            table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                        }
                        if (dr8.Length > 0)
                        {
                            table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                            table1 = GetCommonRow(table1, strDivHas, "现场审核", "", "", false);
                        }
                        table1 = GetCommonRow(table1, strDivWait, "技术室审核", GetUserRealName(objSubTaskApp.RESULT_QC_CHECK), objSubTaskApp.RESULT_QC_CHECK_DATE, false);
                    }
                }
            }
            else
            {
                if (objTask.SAMPLE_SOURCE != "送样")
                    table1 = GetCommonRow(table1, strDivHas, "采样", GetUserRealName(objSubTask.SAMPLING_MANAGER_ID), objSubTask.SAMPLE_FINISH_DATE, false);
                if (dr9.Length > 0)
                {
                    table1 = GetCommonRow(table1, strDivHas, "样品交接", GetUserRealName(objSubTask.SAMPLE_ACCESS_ID), objSubTask.SAMPLE_ACCESS_DATE, false);
                    table1 = GetCommonRow(table1, strDivHas, "分析任务分配", "", "", false);
                    table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                    table1 = GetCommonRow(table1, strDivHas, "数据审核", GetUserRealName(objSubTaskApp.RESULT_AUDIT), "", false);
                    table1 = GetCommonRow(table1, strDivHas, "主任审核", GetUserRealName(objSubTaskApp.RESULT_CHECK), objSubTaskApp.RESULT_CHECK_DATE, false);
                }
                if (dr8.Length > 0)
                {
                    table1 = GetCommonRow(table1, strDivHas, "现场复核", "", "", false);
                    table1 = GetCommonRow(table1, strDivHas, "现场审核", "", "", false);
                }
                table1 = GetCommonRow(table1, strDivHas, "技术室审核", GetUserRealName(objSubTaskApp.RESULT_QC_CHECK), objSubTaskApp.RESULT_QC_CHECK_DATE, false);
            }
        }
        else
        {
            if (dr1.Length > 0)//环节在分析任务安排
            {
                //if (this.step_type.Value != "QY")//清远无分析任务分配,在交接环节直接分配
                table1 = GetCommonRow(table1, strDivWait, "分析任务分配", strAnalyseSender, "", false);
            }
            else
            {
                //if (this.step_type.Value != "QY")//清远无分析任务分配,在交接环节直接分配
                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", GetUserRealName(objSubTaskApp.ANALYSE_ASSIGN_ID), objSubTaskApp.ANALYSE_ASSIGN_DATE, false);
                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
                if (this.step_type.Value == "QY")
                {
                    string strNoSumit = "";
                    string strYesSumit = "";
                    string strYesTime = "";
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        string strHeadUser = GetUserRealName(dr["REMARK_1"].ToString());
                        string strTime = dr["APPARTUS_TIME_USED"].ToString();
                        if (dr["RESULT_STATUS"].ToString() == "30")//结果未提交
                        {
                            strNoSumit += !strNoSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
                        }
                        else//结果已提交
                        {
                            if (!strYesSumit.Contains(strHeadUser))
                            {
                                strYesSumit += strHeadUser + "、";
                                strYesTime += strTime + "、";
                            }
                        }
                    }
                    strNoSumit = strNoSumit.TrimEnd('、');
                    strYesSumit = strYesSumit.TrimEnd('、');
                    //strYesTime = strYesTime.TrimEnd('、');
                    TMisMonitorSubtaskVo SubtaskVo = new TMisMonitorSubtaskLogic().Details(objSubTaskApp.SUBTASK_ID);
                    bool IsAnyscene = new TMisMonitorSubtaskLogic().isExistAnyscene("", objSubTaskApp.SUBTASK_ID);
                    bool IsAnysceneDept = new TMisMonitorSubtaskLogic().isExistAnysceneDept("", objSubTaskApp.SUBTASK_ID);
                    bool isHasOuterQcSample = new TMisMonitorResultLogic().CheckTaskHasOuterQcSample("", objSubTaskApp.SUBTASK_ID);
                    if (isfinish)
                    {
                        table1 = GetCommonRow(table1, strDivHas, strShowNameAnalyseChecker, strYesSumit, strYesTime, true);   //分析复核
                        if (IsAnysceneDept)
                        {
                            if (dr5.Length > 0)//现场结果核录不完全
                            {
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAnyscener, GetUserRealName(SubtaskVo.SAMPLING_MANAGER_ID), "", false);
                            }
                            else
                            {
                                table1 = GetCommonRow(table1, strDivHas, strShowNameAnyscener, GetUserRealName(SubtaskVo.SAMPLING_MANAGER_ID), "", false); //现场核录
                            }
                        }
                        if (IsAnyscene)
                        {
                            table1 = GetCommonRow(table1, strDivHas, strShowNameAgainChecker, strAnalyseAgainChecker, objSubTaskApp.RESULT_CHECK_DATE, false);   //分析审核
                        }
                        if (isHasOuterQcSample)
                        {
                            table1 = GetCommonRow(table1, strDivHas, strShowNameQcChecker, strQcChecker, objSubTaskApp.RESULT_QC_CHECK_DATE, false);  //质控审核
                        }
                    }
                    else
                    {
                        if (dr3.Length > 0)//分析复核不完全
                        {
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strNoSumit, strYesTime, false);
                            if (dr5.Length > 0)//现场结果核录不完全
                            {
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAnyscener, GetUserRealName(SubtaskVo.SAMPLING_MANAGER_ID), "", false);
                            }
                            if (dr6.Length > 0)//分析审核不完全
                            {
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                            }
                            if (dr4.Length > 0)//质控审核不完全
                            {
                                if (isHasOuterQcSample)
                                {
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                                }
                            }
                        }
                        else
                        {
                            if (dr5.Length > 0)
                            {
                                table1 = GetCommonRow(table1, strDivHas, strShowNameAnalyseChecker, strYesSumit, strYesTime, true);
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAnyscener, GetUserRealName(SubtaskVo.SAMPLING_MANAGER_ID), "", false);
                                if (dr6.Length > 0)//分析审核不完全
                                {
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                                }
                                else
                                {
                                    if (IsAnyscene)
                                    {
                                        table1 = GetCommonRow(table1, strDivHas, strShowNameAgainChecker, strAnalyseAgainChecker, objSubTaskApp.RESULT_CHECK_DATE, false);   //分析审核
                                    }
                                }
                                if (dr4.Length > 0)//质控审核不完全
                                {
                                    if (isHasOuterQcSample)
                                    {
                                        table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                                    }
                                }
                                else
                                {
                                    if (isHasOuterQcSample)
                                    {
                                        table1 = GetCommonRow(table1, strDivHas, strShowNameQcChecker, strQcChecker, objSubTaskApp.RESULT_QC_CHECK_DATE, false);  //质控审核
                                    }
                                }
                            }
                            else
                            {
                                if (IsAnysceneDept)
                                {
                                    table1 = GetCommonRow(table1, strDivHas, strShowNameAnyscener, GetUserRealName(SubtaskVo.SAMPLING_MANAGER_ID), "", false);
                                }
                                if (dr6.Length > 0)
                                {
                                    table1 = GetCommonRow(table1, strDivHas, strShowNameAnalyseChecker, strYesSumit, strYesTime, true);
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                                    if (dr4.Length > 0)//质控审核不完全
                                    {
                                        if (isHasOuterQcSample)
                                        {
                                            table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                                        }
                                    }
                                }
                                else
                                {
                                    if (dr4.Length > 0)//质控审核不完全
                                    {
                                        table1 = GetCommonRow(table1, strDivHas, strShowNameAnalyseChecker, strYesSumit, strYesTime, true);
                                        table1 = GetCommonRow(table1, strDivHas, strShowNameAgainChecker, strAnalyseAgainChecker, objSubTaskApp.RESULT_CHECK_DATE, false);
                                        if (isHasOuterQcSample)
                                        {
                                            table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (dr2.Length > 0)//结果提交不完全时
                    {
                        #region 结果提交不完全时
                        if (dr6.Length > 0)//流程已到技术负责人审核（最后环节）
                        {
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                            table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                            table1 = GetCommonRow(table1, strDivWait, strShowNameTechChecker, strTechChecker, "", false);
                        }
                        else if (dr5.Length > 0)//流程已到技术室审核环节（最后环节）
                        {
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                            table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                        }
                        else
                        {
                            if (dr4.Length > 0)//流程仅到分析室审核
                            {
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                                table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                            }
                            else
                            {
                                if (dr3.Length > 0)//流程仅到实验室主任复核
                                {
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                                }
                            }
                        }
                        #endregion
                    }
                    else//结果提交完全时
                    {
                        #region 结果提交完全时
                        if (dr3.Length > 0)//实验室主任复核不完全
                        {
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                        }
                        else
                        {
                            table1 = GetCommonRow(table1, strDivHas, strShowNameAnalyseChecker, strAnalyseChecker, "", false);
                        }
                        if (dr4.Length > 0)//分析审核不完全
                        {
                            table1 = GetCommonRow(table1, strDivWait, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                        }
                        else if (dr3.Length == 0)//已经全部发送
                        {
                            table1 = GetCommonRow(table1, strDivHas, strShowNameAgainChecker, strAnalyseAgainChecker, "", false);
                        }

                        if (dr5.Length > 0)//流程已到质量负责人或者技术室审核环节（秦皇岛最后环节）
                        {
                            if (this.step_type.Value == "ZZ")
                            {
                                table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                            }
                            else
                            {
                                if (isfinish)
                                {
                                    table1 = GetCommonRow(table1, strDivHas, strShowNameQcChecker, strQcChecker, "", false);
                                }
                                else
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameQcChecker, strQcChecker, "", false);
                            }
                        }
                        else
                        {
                            if (this.step_type.Value == "ZZ")
                            {
                                table1 = GetCommonRow(table1, strDivHas, strShowNameQcChecker, strQcChecker, "", false);
                            }
                        }

                        if (this.step_type.Value == "ZZ")//60，郑州专有
                        {
                            if (dr6.Length > 0)//流程已到技术负责人审核环节（最后环节）
                            {
                                if (isfinish)
                                {
                                    table1 = GetCommonRow(table1, strDivHas, strShowNameTechChecker, strTechChecker, "", false);
                                }
                                else
                                    table1 = GetCommonRow(table1, strDivWait, strShowNameTechChecker, strTechChecker, "", false);
                            }
                        }
                        #endregion
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取人员姓名
    /// </summary>
    /// <param name="strUserID">用户ID</param>
    /// <returns></returns>
    private string GetUserRealName(string strUserID)
    {
        return new TSysUserLogic().Details(strUserID).REAL_NAME;
    }

    /// <summary>
    /// 公共的行构造
    /// </summary>
    /// <param name="table">容器</param>
    /// <param name="strDivStyle">环节状态样式</param>
    /// <param name="strStepName">环节名</param>
    /// <param name="strStepInfo">环节信息</param>
    /// <param name="b">true:办理人和时间拆行显示，false:办理人和时间同一行显示</param>
    /// <returns></returns>
    private Table GetCommonRow(Table table, string strDivStyle, string strStepName, string strStepInfo, string strStepTime, bool b)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        //cell.Text = string.Format(strDivStyle, "环节名：" + strStepName + "<br/>处理人：" + strStepInfo + "");
        if (b)
        {
            strDivStyle = strDivStyle.Replace("<br /><span>处理者：</span>{1}&nbsp;&nbsp;<span>办理时间：</span>{2}", "{1}");
            string[] strName = strStepInfo.Split('、');
            string[] strTime = strStepTime.Split('、');
            string strNameTime = "";
            for (int i = 0; i < strName.Length; i++)
            {
                strNameTime += "<br /><span>处理者：</span>" + strName[i] + "&nbsp;&nbsp;<span>办理时间：</span>" + strTime[i];
            }

            cell.Text = string.Format(strDivStyle, strStepName, strNameTime);
        }
        else
        {
            cell.Text = string.Format(strDivStyle, strStepName, strStepInfo, strStepTime);
        }
        
        row.Cells.Add(cell);

        //邵的注释
        //table.Rows.Add(GetFollowRow());
        table.Rows.Add(row);

        return table;
    }

    /// <summary>
    /// 结果录入 分支处理
    /// </summary>
    /// <param name="table">容器</param>
    /// <param name="strYesDivStyle">已办环节状态样式</param>
    /// <param name="strWaitDivStyle">未办环节状态</param>
    /// <param name="strStepName">环节名</param>
    /// <param name="dtStepInfo">结果执行表</param>
    /// <returns></returns>
    private Table GetResultRow(Table table, string strYesDivStyle, string strWaitDivStyle, string strStepName, DataTable dtStepInfo)
    {
        string strNoSumit = "";//未完成
        string strYesSumit = "";//已完成
        TableRow row = new TableRow();
        DateTime minAnalysisDate = new DateTime(2990, 1, 1);//最早分析时间
        DateTime maxAnalysisDate = new DateTime(1990, 1, 1);//最迟分析时间
        foreach (DataRow dr in dtStepInfo.Rows)
        {
            string strHeadUser = GetUserRealName(dr["HEAD_USERID"].ToString());
            string strFinishDate = dr["FINISH_DATE"].ToString();
            
            if (dr["RESULT_STATUS"].ToString() == "20")//结果未提交
            {
                strNoSumit += !strNoSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
            }
            else//结果已提交
            {
                strYesSumit += !strYesSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
                if (strFinishDate.Length > 0)
                {
                    if (minAnalysisDate > DateTime.Parse(strFinishDate))
                        minAnalysisDate = DateTime.Parse(strFinishDate);
                    if (maxAnalysisDate < DateTime.Parse(strFinishDate))
                        maxAnalysisDate = DateTime.Parse(strFinishDate);
                }
            }
        }
        if (strNoSumit.Length > 0)
        {
            TableCell cellNo = new TableCell();
            //cellNo.Text = string.Format(strWaitDivStyle, "环节名：分析结果提交<br/>（未提交）<br/>处理人：" + (strNoSumit.IndexOf("、") > 0 ? strNoSumit.Remove(strNoSumit.LastIndexOf("、")) : strNoSumit));
            cellNo.Text = string.Format(strWaitDivStyle, "分析结果提交", (strNoSumit.IndexOf("、") > 0 ? strNoSumit.Remove(strNoSumit.LastIndexOf("、")) : strNoSumit), "");
            row.Cells.Add(cellNo);
        }
        if (strYesSumit.Length > 0)
        {
            TableCell cellYes = new TableCell();
            //cellYes.Text = string.Format(strYesDivStyle, "环节名：分析结果提交<br/>（已提交）<br/>处理人：" + (strYesSumit.IndexOf("、") > 0 ? strYesSumit.Remove(strYesSumit.LastIndexOf("、")) : strYesSumit));
            cellYes.Text = string.Format(strYesDivStyle, "分析结果提交", (strYesSumit.IndexOf("、") > 0 ? strYesSumit.Remove(strYesSumit.LastIndexOf("、")) : strYesSumit), minAnalysisDate.ToString() + "~" + maxAnalysisDate.ToString());
            row.Cells.Add(cellYes);
        }
        //邵的注释
        //table.Rows.Add(GetFollowRow());
        table.Rows.Add(row);

        return table;
    }

    /// <summary>
    /// 获得指定职责的默认负责人，如果不存在则取第一个
    /// </summary>
    /// <param name="strDutyType">职责编码</param>
    /// <param name="strMonitorType">监测类别</param>
    /// <returns></returns>
    private string GetDefaultOrFirstDutyUser(string strDutyType, string strMonitorType)
    {
        DataTable dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorType);
        //过滤默认负责人
        DataRow[] drDuty = dtDuty.Select(" IF_DEFAULT");
        if (drDuty.Length > 0)
        {
            return GetUserRealName(drDuty[0]["USERID"].ToString());
        }
        else
        {
            if (dtDuty.Rows.Count > 0)
            {
                return GetUserRealName(dtDuty.Rows[0]["USERID"].ToString());
            }
        }
        return "";
    }

    #region  邵写的 注释
    #region ReturnAllTaskStep
    //protected void ReturnAllTaskStep()
    //{
    //    #region get data
    //    //任务对象
    //    TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
    //    //无现场监测子任务信息
    //    TMisMonitorSubtaskVo objNoSampleSubTask = new TMisMonitorSubtaskLogic().GetNoSampleSubTaskInfo(this.TASK_ID.Value, this.Monitor_ID.Value);
    //    //现场子任务
    //    TMisMonitorSubtaskVo objSampleSubTask = new TMisMonitorSubtaskLogic().GetSampleSubTaskInfo(this.TASK_ID.Value, this.Monitor_ID.Value);
    //    //样品信息
    //    TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(new TMisMonitorSampleInfoVo()
    //    {
    //        SUBTASK_ID = objNoSampleSubTask.ID,
    //        QC_TYPE = "0"
    //    });
    //    //样品结果信息
    //    DataTable dtResult = new TMisMonitorResultLogic().SelectResultAndAppWithSubtaskID(objNoSampleSubTask.ID);
    //    //校阅
    //    //委托书对象
    //    TMisContractVo objContract = new TMisContractLogic().Details(objTask.CONTRACT_ID);
    //    //预约表
    //    TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(new TMisContractPlanVo()
    //    {
    //        CONTRACT_ID = objContract.ID
    //    });
    //    //预约频次表
    //    TMisContractPointFreqVo objPointFreq = new TMisContractPointFreqLogic().Details(new TMisContractPointFreqVo()
    //    {
    //        CONTRACT_ID = objContract.ID
    //    });
    //    //自送样预约数(已预约未办理数)
    //    TMisContractSamplePlanVo objSamplePlan = new TMisContractSamplePlanLogic().Details(new TMisContractSamplePlanVo()
    //    {
    //        CONTRACT_ID = objContract.ID,
    //    });
    //    #endregion

    //    #region 暂行的定制生成任务追踪方法 页面HTML构建
    //    //定义已处理DIV样式
    //    string strDivHas = "<div style='width:150px;height:auto;background-color:#5a8f5a;text-align: center;vertical-align: middle;'>{0}</div>";
    //    //string strDivHas = "<div class='listgreen'><h2>{0}</h2><p><span>环节状态：</span><strong>已处理</strong><br /><span>处理者：</span>{1}</p><img src='img/down2012.gif' /></div>";
    //    //定义待处理DIV样式
    //    string strDivWait = "<div style='width:150px;height:auto;background-color:#de9a1d;text-align: center;vertical-align: middle;'>{0}</div>";
    //    //string strDivWait = "<div class='listyellow'><h2>{0}</h2><p><span>环节状态：</span><strong>待处理</strong><br /><span>处理者：</span>{1}</p><img src='img/down2012.gif' /></div>";

    //    #region 办理人
    //    //采样负责人
    //    string strSampleManager = GetSampleManager(objNoSampleSubTask.SAMPLING_MANAGER_ID);
    //    //交接人
    //    string strSampleAccept = GetDefaultOrFirstDutyUser("sample_delivery", this.Monitor_ID.Value);
    //    //采样任务分配人
    //    string strSampleSender = GetDefaultOrFirstDutyUser("duty_other_sampling", this.Monitor_ID.Value);
    //    //采样后质控人
    //    string strQcEnd = GetDefaultOrFirstDutyUser("sample_qc_end", this.Monitor_ID.Value);
    //    //分析任务分配人
    //    string strAnalyseSender = GetDefaultOrFirstDutyUser("duty_other_analyse", this.Monitor_ID.Value);
    //    //校核人
    //    string strAnalyseChecker = GetDefaultOrFirstDutyUser("duty_other_analyse_result", this.Monitor_ID.Value);
    //    //复核人
    //    string strAnalyseAgainChecker = GetDefaultOrFirstDutyUser("duty_other_analyse_control", this.Monitor_ID.Value);
    //    //审核人
    //    string strQcChecker = GetDefaultOrFirstDutyUser("qc_manager_audit", this.Monitor_ID.Value);
    //    #endregion
    //    Table table1 = new Table();
    //    table1.CssClass = "tMain";
    //    TableRow row1 = new TableRow();//委托流程连接
    //    TableCell cell1 = new TableCell();
    //    cell1.Text = string.Format(strDivHas, "开始");
    //    row1.Cells.Add(cell1);
    //    table1.Rows.Add(row1);

    //    #region 邵
    //    //是否已预约 常规的
    //    #region 是否已预约 常规的
    //    if (!string.IsNullOrEmpty(objPointFreq.ID))
    //    {
    //        if (objPointFreq.IF_PLAN == "0")//未下达
    //        {
    //            table1 = GetCommonRow(table1, strDivHas, "采样任务下达", "");
    //        }
    //        else
    //        {
    //            table1 = GetCommonRow(table1, strDivWait, "采样任务下达", "");
    //            this.divSample.Controls.Add(table1);
    //            return;
    //        }
    //    }
    //    #endregion
    //    //自送样 预约
    //    #region 自送样 预约
    //    if (objSamplePlan.IF_PLAN == "0")//未预约
    //    {
    //        table1 = GetCommonRow(table1, strDivWait, "任务未下达", "");
    //        this.divSample.Controls.Add(table1);
    //        return;
    //    }
    //    else if (objSamplePlan.IF_PLAN == "1")//已预约
    //    {
    //        table1 = GetCommonRow(table1, strDivWait, "任务未办理", "");
    //        this.divSample.Controls.Add(table1);
    //        return;
    //    }
    //    else if (objSamplePlan.IF_PLAN == "2")//预约办理
    //    {
    //        table1 = GetCommonRow(table1, strDivHas, "任务已下达", "");
    //    }
    //    #endregion
    //    //采样前质控
    //    #region 采样前质控
    //    if (!string.IsNullOrEmpty(objTask.QC_STATUS))
    //    {
    //        if (objTask.QC_STATUS == "1")//前质控
    //        {
    //            table1 = GetCommonRow(table1, strDivWait, "采样前质控", "");
    //            this.divSample.Controls.Add(table1);
    //            return;
    //        }
    //        else
    //        {
    //            table1 = GetCommonRow(table1, strDivHas, "采样前质控", "");
    //        }
    //    }
    //    #endregion
    //    //是否采样任务分配
    //    #region 是否采样任务分配
    //    if (objPlan.HAS_DONE == "1")//已办理
    //    {
    //        table1 = GetCommonRow(table1, strDivHas, "采样任务分配", strSampleSender);
    //    }
    //    else if (objPlan.HAS_DONE == "0" || objPlan.HAS_DONE == "")
    //    {
    //        table1 = GetCommonRow(table1, strDivWait, "采样任务分配", strSampleSender);

    //        this.divSample.Controls.Add(table1);
    //        return;
    //    }
    //    #endregion
    //    //是否采样 特殊处理
    //    #region 是否采样 特殊处理
    //    if (objNoSampleSubTask.TASK_STATUS == "02")
    //    {
    //        table1 = CreateSampleRow(table1, strDivWait, strDivHas, true, objSampleSubTask, strSampleManager);
    //    }
    //    #endregion
    //    //全是现场项目时
    //    #region 全是现场项目时
    //    if (objNoSampleSubTask.ID == "" && objSampleSubTask.ID != "")
    //    {
    //        if (objSampleSubTask.TASK_STATUS == "02")
    //        {
    //            table1 = CreateSampleRow(table1, strDivWait, strDivHas, true, objSampleSubTask, strSampleManager);
    //        }
    //        else
    //        {
    //            table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //        }
    //        this.divSample.Controls.Add(table1);
    //        return;
    //    }
    //    #endregion
    //    //采样后质控
    //    #region 采样后质控
    //    if (objNoSampleSubTask.TASK_STATUS == "024")
    //    {
    //        table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //        table1 = GetCommonRow(table1, strDivWait, "采样后质控", strQcEnd);
    //    }
    //    #endregion
    //    //样品交接
    //    #region 样品交接
    //    if (objNoSampleSubTask.TASK_STATUS == "021")
    //    {
    //        //table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
    //        table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //        table1 = GetCommonRow(table1, strDivHas, "采样后质控", strQcEnd);
    //        table1 = GetCommonRow(table1, strDivWait, "样品交接", strSampleAccept);
    //    }
    //    #endregion
    //    //分析任务安排
    //    DataRow[] dr1 = dtResult.Select(" RESULT_STATUS='01'");//过滤过分析分配环节任务
    //    //分析结果录入
    //    DataRow[] dr2 = dtResult.Select(" RESULT_STATUS='20'");
    //    //实验室主任复核
    //    DataRow[] dr3 = dtResult.Select(" RESULT_STATUS='30'");
    //    //质量科审核
    //    DataRow[] dr4 = dtResult.Select(" RESULT_STATUS='40'");
    //    //质量负责人审核
    //    DataRow[] dr5 = dtResult.Select(" RESULT_STATUS='50'");
    //    if (objNoSampleSubTask.TASK_STATUS == "03")
    //    {
    //        #region 质量负责人审核
    //        if (dr1.Length > 0)//环节在分析任务安排
    //        {
    //            // table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
    //            table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //            table1 = GetCommonRow(table1, strDivHas, "采样后质控", strQcEnd);
    //            table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
    //            table1 = GetCommonRow(table1, strDivWait, "分析任务安排", strAnalyseSender);
    //        }
    //        else
    //        {
    //            //table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
    //            table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //            table1 = GetCommonRow(table1, strDivHas, "采样后质控", strQcEnd);
    //            table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
    //            table1 = GetCommonRow(table1, strDivHas, "分析任务安排", strAnalyseSender);
    //            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);

    //            if (dr2.Length > 0)//结果提交不完全时
    //            {
    //                #region 结果提交不完全时
    //                if (dr5.Length > 0)//流程已到技术室审核环节（最后环节）
    //                {
    //                    table1 = GetCommonRow(table1, strDivWait, "实验室主任复核", strAnalyseChecker);
    //                    table1 = GetCommonRow(table1, strDivWait, "质量科审核", strAnalyseAgainChecker);
    //                    table1 = GetCommonRow(table1, strDivWait, "质量负责人审核", strQcChecker);
    //                }
    //                else
    //                {
    //                    if (dr4.Length > 0)//流程仅到分析室审核
    //                    {
    //                        table1 = GetCommonRow(table1, strDivWait, "实验室主任复核", strAnalyseChecker);
    //                        table1 = GetCommonRow(table1, strDivWait, "质量科审核", strAnalyseAgainChecker);
    //                    }
    //                    else
    //                    {
    //                        if (dr3.Length > 0)//流程仅到实验室主任复核
    //                        {
    //                            table1 = GetCommonRow(table1, strDivWait, "实验室主任复核", strAnalyseChecker);
    //                        }
    //                    }
    //                }
    //                #endregion
    //            }
    //            else//结果提交完全时
    //            {
    //                #region 结果提交完全时
    //                if (dr3.Length > 0)//实验室主任复核不完全
    //                {
    //                    table1 = GetCommonRow(table1, strDivWait, "实验室主任复核", strAnalyseChecker);
    //                }
    //                else
    //                {
    //                    table1 = GetCommonRow(table1, strDivHas, "实验室主任复核", strAnalyseChecker);
    //                }
    //                if (dr4.Length > 0)//分析审核不完全
    //                {
    //                    table1 = GetCommonRow(table1, strDivWait, "质量科审核", strAnalyseAgainChecker);
    //                }
    //                else if (dr3.Length == 0)//已经全部发送
    //                {
    //                    table1 = GetCommonRow(table1, strDivHas, "质量科审核", strAnalyseAgainChecker);
    //                }
    //                if (dr5.Length > 0)//流程已到技术室审核环节（最后环节）
    //                {
    //                    if (objNoSampleSubTask.TASK_STATUS == "09" && objTask.TASK_STATUS == "09")//分析流程完成
    //                    {
    //                        table1 = GetCommonRow(table1, strDivHas, "质量负责人审核", strQcChecker);
    //                    }
    //                    else
    //                    {
    //                        table1 = GetCommonRow(table1, strDivWait, "质量负责人审核", strQcChecker);
    //                    }
    //                }
    //                #endregion
    //            }
    //        }
    //        #endregion
    //    }
    //    else if (objTask.TASK_STATUS == "09" && objNoSampleSubTask.TASK_STATUS == "09")//报告
    //    {
    //        #region 报告
    //        table1 = CreateSampleRow(table1, strDivWait, strDivHas, false, objSampleSubTask, strSampleManager);
    //        table1 = GetCommonRow(table1, strDivHas, "采样后质控", strQcEnd);
    //        table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
    //        table1 = GetCommonRow(table1, strDivHas, "分析任务安排", strAnalyseSender);
    //        table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);
    //        table1 = GetCommonRow(table1, strDivHas, "实验室主任复核", strAnalyseChecker);
    //        table1 = GetCommonRow(table1, strDivHas, "质量科审核", strAnalyseAgainChecker);
    //        table1 = GetCommonRow(table1, strDivHas, "质量负责人审核", strQcChecker);
    //        table1.Rows.Add(GetFollowRow());

    //        TableRow row = new TableRow();//报告流程连接
    //        TableCell cell = new TableCell();
    //        cell.Text = string.Format(strDivHas, "结束");
    //        row.Cells.Add(cell);
    //        table1.Rows.Add(row);
    //        #endregion
    //    }
    //    #endregion
    //    this.divSample.Controls.Add(table1);
    //    #endregion
    //}
    #endregion

    #region CreateSampleRow
    ///// <summary>
    ///// 采样行的特殊 创建
    ///// </summary>
    ///// <param name="table">容器</param>
    ///// <param name="strDivWaitStyle">未办环节状态</param>
    ///// <param name="strDivDoStyle">已办环节状态样式</param>
    ///// <param name="strStepInfo">采样办理人</param>
    ///// <returns></returns>
    //private Table CreateSampleRow(Table table, string strDivWaitStyle, string strDivDoStyle, bool Has, TMisMonitorSubtaskVo objSampleSubTask, string strStepInfo)
    //{
    //    TableRow row = new TableRow();
    //    TableCell cell = new TableCell();
    //    table.Rows.Add(GetFollowRow());
    //    if (Has)//当前环节是否采样
    //    {
    //        cell.Text = string.Format(strDivWaitStyle, "环节名：采样<br/>处理人：" + strStepInfo + "");
    //    }
    //    else
    //    {
    //        cell.Text = string.Format(strDivDoStyle, "环节名：采样<br/>处理人：" + strStepInfo + "");
    //    }
    //    row.Cells.Add(cell);
    //    if (!string.IsNullOrEmpty(objSampleSubTask.TASK_STATUS))
    //    {
    //        row = GetSampleSubtask(row, strDivWaitStyle, strDivDoStyle, objSampleSubTask);
    //    }
    //    table.Rows.Add(row);
    //    return table;
    //}
    #endregion

    #region  GetSampleSubtask
    /// <summary>
    /// 构造现场审核的信息
    /// </summary>
    /// <param name="strDivWaitStyle">未办环节状态</param>
    /// <param name="strDivDoStyle">已办环节状态样式</param>
    /// <param name="objSampleSubTask">现场审核项目信息</param>
    /// <returns></returns>
    //private TableRow GetSampleSubtask(TableRow row, string strDivWaitStyle, string strDivDoStyle, TMisMonitorSubtaskVo objSampleSubTask)
    //{
    //    string strSampleAudier = GetDefaultOrFirstDutyUser("sample_result_qccheck", this.Monitor_ID.Value);//默认的现场主任审核负责人
    //    //现场审核
    //    TableCell cell1 = new TableCell();
    //    TableCell cell2 = new TableCell();
    //    if (objSampleSubTask.TASK_STATUS == "023")
    //    {
    //        row.Cells.Add(GetRigthFollowRow());

    //        cell2.Text = string.Format(strDivWaitStyle, "环节名：现场室主任审核<br/>办理人：" + strSampleAudier);
    //        row.Cells.Add(cell2);
    //    }
    //    else if (objSampleSubTask.TASK_STATUS == "24" || objSampleSubTask.TASK_STATUS == "09")
    //    {
    //        row.Cells.Add(GetRigthFollowRow());
    //        cell2.Text = string.Format(strDivDoStyle, "环节名：现场室主任审核<br/>办理人：" + strSampleAudier);
    //        row.Cells.Add(cell2);

    //        row.Cells.Add(GetRigthFollowRow());
    //        TableCell cell = new TableCell();//报告流程连接
    //        //cell.Text = string.Format(strDivDoStyle, "<a href='#' onclick='ShowReport('" + this.TASK_ID.Value + "');'>报告流程</a>");
    //        cell.Text = string.Format(strDivDoStyle, "报告流程");
    //        row.Cells.Add(cell);
    //    }
    //    return row;
    //}
    #endregion

    #region  GetSampleManager
    /// <summary>
    /// 获取人员姓名
    /// </summary>
    /// <param name="strUserID">用户ID</param>
    /// <returns></returns>
    private string GetSampleManager(string strUserID)
    {
        TSysUserVo objUser = new TSysUserLogic().Details(strUserID);
        return objUser.REAL_NAME;
    }
    #endregion

    #region  GetFollowRow
    /// <summary>
    /// 获取向下箭头行
    /// </summary>
    /// <returns></returns>
    //private TableRow GetFollowRow()
    //{
    //    //定义 箭头行
    //    TableRow rowFollow = new TableRow();
    //    TableCell cellFollow = new TableCell();
    //    cellFollow.Text = "<img src='../../../sys/wf/img/down2012.gif' />";
    //    rowFollow.Cells.Add(cellFollow);

    //    return rowFollow;
    //}
    #endregion

    #region  GetRigthFollowRow
    ///// <summary>
    ///// 获取向右箭头
    ///// </summary>
    ///// <returns></returns>
    //private TableCell GetRigthFollowRow()
    //{
    //    //定义 箭头行
    //    TableCell cellFollow = new TableCell();
    //    cellFollow.Text = "<img src='../../../sys/wf/img/turnLeft.gif' />";

    //    return cellFollow;
    //}
    #endregion
    #endregion
}