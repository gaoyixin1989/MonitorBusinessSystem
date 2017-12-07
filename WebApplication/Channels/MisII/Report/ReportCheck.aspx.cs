using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Channels.Mis.Monitor.Task;

/// <summary>
/// 功能描述：报告复核
/// 创建时间：2012-12-10
/// 创建人：邵世卓
/// </summary>
public partial class Channels_MisII_Report_ReportCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ReportStatus.Value = "ReportCheck";//报告环节
        if (!IsPostBack)
        {
            var workID = Request.QueryString["WorkID"];     //当前流程ID
            var fid = Request.QueryString["FID"];           //父流程ID
            workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空
            //结果
            if (!string.IsNullOrEmpty(workID))
            {
                TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
                objTaskVo.CCFLOW_ID1 = workID;
                objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
                //监测任务ID
                this.ID.Value = objTaskVo.ID;
                //委托书ID
                this.ContractID.Value = new TMisMonitorTaskLogic().Details(this.ID.Value).CONTRACT_ID;
                //报告ID
                this.reportId.Value = new TRptFileLogic().getNewReportByContractID(this.ID.Value).ID;
            }
        }
    }

    
}