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
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Mis.Monitor.Report;

using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Duty;

public partial class Channels_Base_Search_SearchQHD_TaskFlow_Rpt : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            this.TASK_ID.Value = Request.QueryString["ID"].ToString();
        }

        if (!IsPostBack)
        {
            DrawStep();
        }
    }

    protected void DrawStep()
    {
        //定义已处理DIV样式
        //string strDivHas = "<div style='width:150px;height:auto;background-color:#5a8f5a;text-align: center;vertical-align: middle;'>{0}</div>";
        string strDivHas = "<div class='listgreen'><h2>{0}</h2><p><span>环节状态：</span><strong>已处理</strong><br /><span>处理者：</span>{1}</p></div>";
        //定义待处理DIV样式
        //string strDivWait = "<div style='width:150px;height:auto;background-color:#de9a1d;text-align: center;vertical-align: middle;'>{0}</div>";
        string strDivWait = "<div class='listyellow'><h2>{0}</h2><p><span>环节状态：</span><strong>待处理</strong><br /><span>处理者：</span>{1}</p></div>";

        //任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        TMisMonitorReportVo objTaskRpt = new TMisMonitorReportLogic().Details(new TMisMonitorReportVo { TASK_ID = this.TASK_ID.Value });

        Table table1 = new Table();
        table1.CssClass = "tMain";
        string strShowName = "报告编制";
        if (objTask.TASK_STATUS == "09")
        {
            if (objTaskRpt.IF_SEND == "1")//已分配
            {
                table1 = GetCommonRow(table1, strDivWait, strShowName, GetUserRealName(objTaskRpt.REPORT_SCHEDULER));
            }
            else//未分配
            {
                table1 = GetCommonRow(table1, strDivWait, strShowName, "未分配");
            }
        }
        else if (objTask.TASK_STATUS == "11")//已完成
        {
            table1 = GetCommonRow(table1, strDivHas, strShowName, GetUserRealName(objTaskRpt.REPORT_SCHEDULER));
        }
        this.divRpt.Controls.Add(table1);
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
        cell.Text = string.Format(strDivStyle, strStepName, strStepInfo);
        row.Cells.Add(cell);

        table.Rows.Add(row);

        return table;
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
}