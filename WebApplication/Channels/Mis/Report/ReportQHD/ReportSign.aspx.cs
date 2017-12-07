using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using System.Data;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Base.Item;
using System.Web.Services;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能描述：报告审核、签发
/// 创建时间：2013-9-12
/// 创建人：潘德军
/// </summary>
public partial class Channels_Mis_Report_ReportQHD_ReportSign : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 工作流
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

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        #region 初始化环节信息
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.ID.Value);
        if (objTask.CONTRACT_CODE == "")
            wfControl.ServiceCode = objTask.TICKET_NUM;
        else
            wfControl.ServiceCode = objTask.CONTRACT_CODE;
        wfControl.ServiceName = objTask.PROJECT_NAME;
        #endregion
        //更改监测任务状态
        objTask.TASK_STATUS = "11";
        new TMisMonitorTaskLogic().Edit(objTask);
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("报告流程", "task_id", this.ID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
        //SaveBusinessData();
    }
    #endregion
}