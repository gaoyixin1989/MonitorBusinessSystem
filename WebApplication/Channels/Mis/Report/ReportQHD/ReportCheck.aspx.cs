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
public partial class Channels_Mis_Report_ReportQHD_ReportCheck : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ReportStatus.Value = "ReportCheck";//报告环节
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 工作流
    /// <summary>
    /// 载入和显示业务数据
    /// </summary>
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        //传递参数
        string strTaskId = myServiceList.Count > 0 ? myServiceList[0].SERVICE_KEY_VALUE : "";
        //监测任务ID
        this.ID.Value = strTaskId;
        //委托书ID
        this.ContractID.Value = new TMisMonitorTaskLogic().Details(this.ID.Value).CONTRACT_ID;
        //报告ID
        this.reportId.Value = new TRptFileLogic().getNewReportByContractID(strTaskId).ID;
    }
    /// <summary>
    /// 验证组件和业务数据
    /// </summary>
    /// <param name="strMsg">提示信息</param>
    /// <returns></returns>
    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }
    /// <summary>
    /// 产生和注册业务数据
    /// </summary>
    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.ID.Value);
        wfControl.ServiceCode = objTask.CONTRACT_CODE;
        wfControl.ServiceName = objTask.PROJECT_NAME;
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("报告流程", "task_id", this.ID.Value);
    }
    /// <summary>
    /// 执行业务数据保存
    /// </summary>
    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
        //SaveBusinessData();
    }
    #endregion
}