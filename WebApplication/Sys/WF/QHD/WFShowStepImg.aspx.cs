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

public partial class Sys_WF_QHD_WFShowStepImg : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
        {
            this.TASK_ID.Value = Request.QueryString["task_id"].ToString();
        }
        if (!IsPostBack)
        {
            BindMonitor();
        }
    }

    protected void dllMonitorChange(object sender, EventArgs e)
    {
        ReturnAllTaskStep();
    }

    protected void ReturnAllTaskStep()
    {
        //任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        //子任务信息
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(new TMisMonitorSubtaskVo()
        {
            TASK_ID = this.TASK_ID.Value,
            MONITOR_ID = this.dllMonitor.SelectedValue
        });
        //样品信息
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(new TMisMonitorSampleInfoVo()
        {
            SUBTASK_ID = objSubTask.ID,
            QC_TYPE = "0"
        });
        //样品结果信息
        DataTable dtResult = new TMisMonitorResultLogic().SelectResultAndAppWithSubtaskID(objSubTask.ID);
        //校阅
        //委托书对象
        TMisContractVo objContract = new TMisContractLogic().Details(objTask.CONTRACT_ID);
        //预约表
        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(new TMisContractPlanVo()
        {
            CONTRACT_ID = objContract.ID
        });
        //预约频次表(已预约未办理数)
        TMisContractPointFreqVo objPointFreq = new TMisContractPointFreqLogic().Details(new TMisContractPointFreqVo()
        {
            CONTRACT_ID = objContract.ID
        });
        //自送样预约数(已预约未办理数)
        TMisContractSamplePlanVo objSamplePlan = new TMisContractSamplePlanLogic().Details(new TMisContractSamplePlanVo()
        {
            CONTRACT_ID = objContract.ID,
        });

        #region 暂行的定制生成任务追踪方法 页面HTML构建
        //定义已处理DIV样式
        string strDivHas = "<div style='width:150px;height:auto;background-color:#5a8f5a;text-align: center;vertical-align: middle;'>{0}</div>";
        //定义待处理DIV样式
        string strDivWait = "<div style='width:150px;height:auto;background-color:#de9a1d;text-align: center;vertical-align: middle;'>{0}</div>";
        //定义特殊处理DIV样式
        //string strDivSepecial = "<div style='width:100px;height:50px;background-color:#e34323;text-align: center;vertical-align: middle;'>{0}</div";
        //定义未处理DIV样式
        //string strDivNo = "<div style='width:100px;height:50px;background-color:#a9a9a9;text-align: center;vertical-align: middle;'>{0}</div>";

        #region 办理人
        //采样负责人
        string strSampleManager = GetSampleManager(objSubTask.SAMPLING_MANAGER_ID);
        //交接人
        string strSampleAccept = GetDefaultOrFirstDutyUser("sample_allocation_sheet", this.dllMonitor.SelectedValue);
        //采样任务分配人
        string strSampleSender = GetDefaultOrFirstDutyUser("duty_other_sampling", this.dllMonitor.SelectedValue);
        //采样后质控人
        string strQcEnd = GetDefaultOrFirstDutyUser("sample_qc_end", this.dllMonitor.SelectedValue);
        //分析任务分配人
        string strAnalyseSender = GetDefaultOrFirstDutyUser("duty_other_analyse", this.dllMonitor.SelectedValue);
        //校核人
        string strAnalyseChecker = GetDefaultOrFirstDutyUser("duty_other_audit ", this.dllMonitor.SelectedValue);
        //复核人
        string strAnalyseAgainChecker = GetDefaultOrFirstDutyUser("duty_other_analyse_result", this.dllMonitor.SelectedValue);
        //审核人
        string strQcChecker = GetDefaultOrFirstDutyUser("duty_other_analyse_control", this.dllMonitor.SelectedValue);
        #endregion

        Table table1 = new Table();
        table1.CssClass = "tMain";
        TableRow row1 = new TableRow();//委托流程连接
        TableCell cell1 = new TableCell();
        cell1.Text = string.Format(strDivHas, "委托流程");
        row1.Cells.Add(cell1);
        table1.Rows.Add(row1);
        //是否已预约
        if (!string.IsNullOrEmpty(objPointFreq.ID))
        {
            if (objPointFreq.IF_PLAN == null)//未预约
            {
                table1 = GetCommonRow(table1, strDivWait, "采样预约", "");

                this.divSample.Controls.Add(table1);
                return;
            }
            else if (objPointFreq.IF_PLAN == "0")//已预约未办理
            {
                table1 = GetCommonRow(table1, strDivHas, "采样预约", "");
            }
        }
        //自送样 预约
        if (objSamplePlan.IF_PLAN == "0")//未预约
        {
            table1 = GetCommonRow(table1, strDivWait, "送样预约", "");
            this.divSample.Controls.Add(table1);
            return;
        }
        else if (objSamplePlan.IF_PLAN == "1")//已预约
        {
            table1 = GetCommonRow(table1, strDivWait, "送样预约", "");
            this.divSample.Controls.Add(table1);
            return;
        }
        else if (objSamplePlan.IF_PLAN == "2")//预约办理
        {
            table1 = GetCommonRow(table1, strDivHas, "送样预约", "");
        }
        //是否预约办理
        if (objPlan.ID != "")
        {
            if (objPlan.HAS_DONE == "1")//已办理
            {
                table1 = GetCommonRow(table1, strDivHas, "预约办理", strSampleSender);
            }
            else if (objPlan.HAS_DONE == "0" || string.IsNullOrEmpty(objPlan.HAS_DONE))
            {
                table1 = GetCommonRow(table1, strDivWait, "预约办理", strSampleSender);

                this.divSample.Controls.Add(table1);
                return;
            }
        }
        //是否采样
        if (objSubTask.TASK_STATUS == "02")
        {
            table1 = GetCommonRow(table1, strDivWait, "采样", strSampleManager);
        }
        //样品交接
        if (objSubTask.TASK_STATUS == "021")
        {
            if (!string.IsNullOrEmpty(objPlan.ID))
            {
                table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
            }
            table1 = GetCommonRow(table1, strDivWait, "样品交接", strSampleAccept);
        }
        //分析任务分配
        DataRow[] dr1 = dtResult.Select(" RESULT_STATUS='01'");//过滤过分析分配环节任务
        //分析结果录入
        DataRow[] dr2 = dtResult.Select(" RESULT_STATUS='20'");
        //数据审核
        DataRow[] dr3 = dtResult.Select(" RESULT_STATUS='30'");
        //分析室主任审核
        DataRow[] dr4 = dtResult.Select(" RESULT_STATUS='40'");
        //技术室主任审核
        DataRow[] dr5 = dtResult.Select(" RESULT_STATUS='50'");
        if (objSubTask.TASK_STATUS == "03")
        {
            if (dr1.Length > 0)//环节在分析任务分配
            {
                if (!string.IsNullOrEmpty(objPlan.ID))
                {
                    table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
                }
                table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
                table1 = GetCommonRow(table1, strDivWait, "分析任务分配", strAnalyseSender);
            }
            else
            {
                if (!string.IsNullOrEmpty(objPlan.ID))
                {
                    table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
                }
                table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
                table1 = GetCommonRow(table1, strDivHas, "分析任务分配", strAnalyseSender);
                table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);

                if (dr2.Length > 0)//结果提交不完全时
                {
                    if (dr5.Length > 0)//流程已到技术室审核环节（最后环节）
                    {
                        table1 = GetCommonRow(table1, strDivWait, "数据审核", strAnalyseChecker);
                        table1 = GetCommonRow(table1, strDivWait, "分析室主任审核", strAnalyseAgainChecker);
                        table1 = GetCommonRow(table1, strDivWait, "技术室主任审核", strQcChecker);
                    }
                    else
                    {
                        if (dr4.Length > 0)//流程仅到分析室审核
                        {
                            table1 = GetCommonRow(table1, strDivWait, "数据审核", strAnalyseChecker);
                            table1 = GetCommonRow(table1, strDivWait, "分析室主任审核", strAnalyseAgainChecker);
                        }
                        else
                        {
                            if (dr3.Length > 0)//流程仅到数据审核
                            {
                                table1 = GetCommonRow(table1, strDivWait, "数据审核", strAnalyseChecker);
                            }
                        }
                    }
                }
                else//结果提交完全时
                {
                    if (dr3.Length > 0)//数据审核不完全
                    {
                        table1 = GetCommonRow(table1, strDivWait, "数据审核", strAnalyseChecker);
                    }
                    else
                    {
                        table1 = GetCommonRow(table1, strDivHas, "数据审核", strAnalyseChecker);
                    }
                    if (dr4.Length > 0)//分析审核不完全
                    {
                        table1 = GetCommonRow(table1, strDivWait, "分析室主任审核", strAnalyseAgainChecker);
                    }
                    else if (dr3.Length == 0)//已经全部发送
                    {
                        table1 = GetCommonRow(table1, strDivHas, "分析室主任审核", strAnalyseAgainChecker);
                    }
                    if (dr5.Length > 0)//流程已到技术室审核环节（最后环节）
                    {
                        if (objSubTask.TASK_STATUS == "09" && objTask.TASK_STATUS == "09")//分析流程完成
                        {
                            table1 = GetCommonRow(table1, strDivHas, "技术室主任审核", strQcChecker);
                        }
                        else
                        {
                            table1 = GetCommonRow(table1, strDivWait, "技术室主任审核", strQcChecker);
                        }
                    }
                }
            }
        }
        else if (objTask.TASK_STATUS == "09" && objSubTask.TASK_STATUS == "09")//报告
        {
            if (!string.IsNullOrEmpty(objPlan.ID))
            {
                table1 = GetCommonRow(table1, strDivHas, "采样", strSampleManager);
            }
            table1 = GetCommonRow(table1, strDivHas, "样品交接", strSampleAccept);
            table1 = GetCommonRow(table1, strDivHas, "分析任务分配", strAnalyseSender);
            table1 = GetResultRow(table1, strDivHas, strDivWait, "分析结果录入", dtResult);

            table1 = GetCommonRow(table1, strDivHas, "数据审核", strAnalyseChecker);
            table1 = GetCommonRow(table1, strDivHas, "分析室主任审核", strAnalyseAgainChecker);
            table1 = GetCommonRow(table1, strDivHas, "技术室主任审核", strQcChecker);

            table1.Rows.Add(GetFollowRow());

            TableRow row = new TableRow();//报告流程连接
            TableCell cell = new TableCell();
            // cell.Text = string.Format(strDivHas, "<a href='#' onclick='ShowReport(" + this.TASK_ID.Value + ");'>报告流程</a>");
            cell.Text = string.Format(strDivHas, "报告流程");
            row.Cells.Add(cell);
            table1.Rows.Add(row);
        }
        this.divSample.Controls.Add(table1);
        #endregion
    }

    /// <summary>
    /// 公共的行构造
    /// </summary>
    /// <param name="table">容器</param>
    /// <param name="strDivStyle">环节状态样式</param>
    /// <param name="strStepName">环节名</param>
    /// <param name="strStepInfo">环节信息</param>
    /// <returns></returns>
    private Table GetCommonRow(Table table, string strDivStyle, string strStepName, string strStepInfo)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        cell.Text = string.Format(strDivStyle, "环节名：" + strStepName + "<br/>处理人：" + strStepInfo + "");
        row.Cells.Add(cell);

        table.Rows.Add(GetFollowRow());
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
        foreach (DataRow dr in dtStepInfo.Rows)
        {
            string strHeadUser = GetSampleManager(dr["HEAD_USERID"].ToString());
            if (dr["RESULT_STATUS"].ToString() == "20")//结果未提交
            {
                strNoSumit += !strNoSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
            }
            else//结果已提交
            {
                strYesSumit += !strYesSumit.Contains(strHeadUser) ? (strHeadUser + "、") : "";
            }
        }
        if (strNoSumit.Length > 0)
        {
            TableCell cellNo = new TableCell();
            cellNo.Text = string.Format(strWaitDivStyle, "环节名：分析结果提交<br/>（未提交）<br/>处理人：" + (strNoSumit.IndexOf("、") > 0 ? strNoSumit.Remove(strNoSumit.LastIndexOf("、")) : strNoSumit));
            row.Cells.Add(cellNo);
        }
        if (strYesSumit.Length > 0)
        {
            TableCell cellYes = new TableCell();
            cellYes.Text = string.Format(strYesDivStyle, "环节名：分析结果提交<br/>（已提交）<br/>处理人：" + (strYesSumit.IndexOf("、") > 0 ? strYesSumit.Remove(strYesSumit.LastIndexOf("、")) : strYesSumit));
            row.Cells.Add(cellYes);
        }
        table.Rows.Add(GetFollowRow());
        table.Rows.Add(row);

        return table;
    }

    /// <summary>
    /// 获取箭头行
    /// </summary>
    /// <returns></returns>
    private TableRow GetFollowRow()
    {
        //定义 箭头行
        TableRow rowFollow = new TableRow();
        TableCell cellFollow = new TableCell();
        cellFollow.Text = "<img src='../img/down2012.gif' />";
        rowFollow.Cells.Add(cellFollow);

        return rowFollow;
    }

    /// <summary>
    ///  绑定任务的监测类别
    /// </summary>
    protected void BindMonitor()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        BindObjectToControls(objTask);

        dllMonitor.DataSource = new TMisMonitorSubtaskLogic().getMonitorByTask(this.TASK_ID.Value);
        dllMonitor.DataTextField = "MONITOR_TYPE_NAME";
        dllMonitor.DataValueField = "MONITOR_ID";
        dllMonitor.DataBind();
        dllMonitor.Items.Insert(0, new ListItem("请选择", ""));
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
            return GetSampleManager(drDuty[0]["USERID"].ToString());
        }
        else
        {
            if (dtDuty.Rows.Count > 0)
            {
                return GetSampleManager(dtDuty.Rows[0]["USERID"].ToString());
            }
        }
        return "";
    }

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
}