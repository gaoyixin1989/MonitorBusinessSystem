using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;
using System.Drawing;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor;
using i3.View;
using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

/// <summary>
/// 功能描述：任务追踪流程图
/// 创建时间：2012-12-1
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_TaskFlowImg : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getTaskInfoForJson();
        }
    }

    /// <summary>
    /// 获取监测任务列表信息
    /// </summary>
    /// <returns></returns>
    protected string getTaskInfoForJson()
    {
        //任务ID
        string strTaskId = Request.QueryString["task_id"].ToString();
        //获取委托书、任务信息
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(strTaskId);
        //构造任务基础信息
        CreateStyleForBaseInfo(objTaskVo);
        //监测类别
        DataTable dtMonitor = new TMisMonitorSubtaskLogic().getMonitorByTask(strTaskId);
        //获取任务的所有环节信息
        DataTable dtTask = new TMisMonitorTaskLogic().SelectByTableForTaskTraking(strTaskId);
        //获取流程环节配置表信息
        DataTable dtFlowConfig = new TSysConfigFlownumLogic().SelectByTable(new TSysConfigFlownumVo() { IS_DEL = "0" });
        CreateFlowImg(dtMonitor, dtTask, dtFlowConfig);
        return "";
    }

    #region 创建任务基本信息
    /// <summary>
    /// 创建任务基本信息
    /// </summary>
    /// <param name="objTaskVo">任务实体对象</param>
    protected void CreateStyleForBaseInfo(TMisMonitorTaskVo objTaskVo)
    {
        Table tb = new Table();

        TableRow row1 = new TableRow();//第一行
        TableCell cell1_1 = new TableCell();//第一行的第一格
        TableCell cell1_2 = new TableCell();//第一行的第二格
        cell1_1.Text = "项目名称：" + objTaskVo.PROJECT_NAME;
        cell1_1.CssClass = "td1";
        cell1_1.ColumnSpan = 2;
        //cell1_2.Text = "委托类型：" + new TSysDictLogic().GetDictNameByDictCodeAndType(objTaskVo.CONTRACT_TYPE, "Contract_Type");
        //cell1_2.CssClass = "td1";
        row1.Cells.AddAt(0, cell1_1);
        //row1.Cells.AddAt(1, cell1_2);

        TableRow row2 = new TableRow();//第二行
        TableCell cell2_1 = new TableCell();//第二行的第一格
        TableCell cell2_2 = new TableCell();//第二行的第二格
        cell2_1.Text = "任务单号：" + objTaskVo.TICKET_NUM;
        cell2_1.CssClass = "td1";
        cell2_2.Text = "委托单号：" + objTaskVo.CONTRACT_CODE;
        cell2_2.CssClass = "td1";
        row2.Cells.AddAt(0, cell2_1);
        row2.Cells.AddAt(1, cell2_2);

        tb.Rows.AddAt(0, row1);
        tb.Rows.AddAt(1, row2);

        this.divTaskInfo.Controls.Add(tb);
    }
    #endregion

    #region 创建任务流程图
    /// <summary>
    /// 创建任务流程图
    /// </summary>
    /// <param name="intMonitorCount">监测类别</param>
    /// <param name="dtTask">任务数据</param>
    /// <param name="dtFlowConfig">流程环节配置信息</param>
    protected void CreateFlowImg(DataTable dtMonitor, DataTable dtTask, DataTable dtFlowConfig)
    {
        //已办理的方框
        string strHas = "<div style='width:100px;height:50px; background-color:#5a8f5a'>{0}</div>";
        //未办理的方框
        string strHasNot = "<div style='width:100px;height:50px; background-color:#de9a1d'>{0}</div>";
        //总Table包括多类别的流程图
        Table tableTotal = new Table();
        TableRow rowTotal = new TableRow();
        foreach (DataRow drMonitor in dtMonitor.Rows)
        {
            //创建监测类别标签
            string strMonitor = drMonitor["MONITOR_TYPE_NAME"].ToString() + "：";
            TableCell cellTotal = new TableCell();//每个类别创建一单元格
            Table table1 = new Table();//每类别中新建Table布局流程图
            //第一行 监测类别
            TableRow row1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = strMonitor;
            row1.Cells.Add(cell1);
            table1.Rows.Add(row1);
            //分解任务流程
            //过滤当前类别的任务
            DataRow[] drTaskList = dtTask.Select(" MONITOR_ID=" + drMonitor["MONITOR_ID"].ToString());
            //排序
            DataRow[] drFlow = dtFlowConfig.Select();
            if (drTaskList.Length == 1)//正常情况只有一条，二条以上为分支
            {
                string strTheEndCode = "no";
                foreach (DataRow drFlow1 in drFlow)
                {
                    if (strTheEndCode == "yes")//流程结束 跳出循环
                        break;
                    TableRow rowX = new TableRow();
                    TableCell cellX = new TableCell();
                    //判断是否最后环节
                    if (drFlow1["FIRST_FLOW_CODE"].ToString() == drTaskList[0]["taskStatus"].ToString() && drFlow1["SECOND_FLOW_CODE"].ToString() == drTaskList[0]["subTaskStatus"].ToString() && drFlow1["THIRD_FLOW_CODE"].ToString() == drTaskList[0]["RESULT_STATUS"].ToString())
                    {
                        //最后环节标记
                        strTheEndCode = "yes";
                        cellX.Text = string.Format(strHasNot, drFlow1["FLOW_NAME"].ToString());
                    }
                    else
                    {
                        cellX.Text = string.Format(strHas, drFlow1["FLOW_NAME"].ToString());
                    }
                    rowX.Cells.Add(cellX);
                    table1.Rows.Add(rowX);
                }
            }
            else
            {
                TableRow row = new TableRow();
                string strTheEndCode = "no";

                foreach (DataRow drTask in drTaskList)
                {
                    TableCell cell = new TableCell();
                    DataRow[] drFlowA = dtFlowConfig.Select(" FLOW_NUM not like 'B%'");//主线+A分支
                    DataRow[] drFlowB = dtFlowConfig.Select(" FLOW_NUM like 'B%'");//分支B
                    //A分支处理
                    foreach (DataRow dr1 in drFlowA)
                    {
                        Table tableA = new Table();
                        if (strTheEndCode == "yes")//流程结束 跳出循环
                            break;
                        TableRow rowA = new TableRow();
                        TableCell cellA = new TableCell();
                        //A分支
                        //判断是否最后环节
                        if (dr1["FIRST_FLOW_CODE"].ToString() == drTaskList[0]["taskStatus"].ToString() && dr1["SECOND_FLOW_CODE"].ToString() == drTaskList[0]["subTaskStatus"].ToString() && dr1["THIRD_FLOW_CODE"].ToString() == drTaskList[0]["RESULT_STATUS"].ToString())
                        {
                            //最后环节标记
                            strTheEndCode = "yes";
                            cellA.Text = string.Format(strHasNot, dr1["FLOW_NAME"].ToString());
                        }
                        else
                        {
                            cellA.Text = string.Format(strHas, dr1["FLOW_NAME"].ToString());
                        }
                        rowA.Cells.Add(cellA);
                        tableA.Rows.Add(rowA);
                        cell.Controls.Add(tableA);
                    }
                    //B分支处理
                    foreach (DataRow dr2 in drFlowB)
                    {
                        Table tableB = new Table();
                        if (strTheEndCode == "yes")//流程结束 跳出循环
                            break;
                        TableRow rowB = new TableRow();
                        TableCell cellB = new TableCell();
                        //判断是否最后环节
                        if (dr2["FIRST_FLOW_CODE"].ToString() == drTaskList[0]["taskStatus"].ToString() && dr2["SECOND_FLOW_CODE"].ToString() == drTaskList[0]["subTaskStatus"].ToString() && dr2["THIRD_FLOW_CODE"].ToString() == drTaskList[0]["RESULT_STATUS"].ToString())
                        {
                            //最后环节标记
                            strTheEndCode = "yes";
                            cellB.Text = string.Format(strHasNot, dr2["FLOW_NAME"].ToString());
                        }
                        else
                        {
                            cellB.Text = string.Format(strHas, dr2["FLOW_NAME"].ToString());
                        }
                        rowB.Cells.Add(cellB);
                        tableB.Rows.Add(rowB);
                        cell.Controls.Add(tableB);
                    }
                    row.Cells.Add(cell);
                }
                table1.Rows.Add(row);
            }

            cellTotal.Controls.Add(table1);
            rowTotal.Cells.Add(cellTotal);//按顺序插入行中
        }
        tableTotal.Rows.Add(rowTotal);
        divFlowInfo.Controls.Add(tableTotal);
    }
    #endregion
}