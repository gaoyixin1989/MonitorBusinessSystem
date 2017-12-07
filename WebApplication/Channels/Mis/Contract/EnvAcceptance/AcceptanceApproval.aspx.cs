using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

/// <summary>
/// 功能描述：验收委托书审批
/// 创建时间：2012-12-29
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Contract_EnvAcceptance_AcceptanceApproval : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 生成报告

    protected void SetReport(string strContractID)
    {
        //获取委托书信息
        TMisContractVo objContract = new TMisContractLogic().Details(strContractID);

        #region 构造监测任务对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        CopyObject(objContract, objTask);
        objTask.ID = GetSerialNumber("t_mis_monitor_taskId");
        objTask.CONTRACT_ID = objContract.ID;
        objTask.PLAN_ID = "";
        objTask.CONSIGN_DATE = objContract.ASKING_DATE;
        objTask.CREATOR_ID = LogInfo.UserInfo.ID;
        objTask.CREATE_DATE = DateTime.Now.ToString();
        objTask.TASK_STATUS = "09";
        objTask.COMFIRM_STATUS = "0";
        #endregion

        #region 构造监测任务委托企业信息
        //委托企业信息
        TMisContractCompanyVo objContractClient = new TMisContractCompanyLogic().Details(objContract.CLIENT_COMPANY_ID);
        //受检企业信息
        TMisContractCompanyVo objContractTested = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);
        //构造监测任务委托企业信息
        TMisMonitorTaskCompanyVo objTaskClient = new TMisMonitorTaskCompanyVo();
        CopyObject(objContractClient, objTaskClient);//复制对象
        objTaskClient.ID = GetSerialNumber("t_mis_monitor_taskcompanyId");
        objTaskClient.TASK_ID = objTask.ID;
        objTaskClient.COMPANY_ID = objContract.CLIENT_COMPANY_ID;
        //构造监测任务受检企业信息
        TMisMonitorTaskCompanyVo objTaskTested = new TMisMonitorTaskCompanyVo();
        CopyObject(objContractTested, objTaskTested);//复制对象
        objTaskTested.ID = GetSerialNumber("t_mis_monitor_taskcompanyId");
        objTaskTested.TASK_ID = objTask.ID;
        objTaskTested.COMPANY_ID = objContract.TESTED_COMPANY_ID;

        //重新赋值监测任务企业ID
        objTask.CLIENT_COMPANY_ID = objTaskClient.ID;
        objTask.TESTED_COMPANY_ID = objTaskTested.ID;
        #endregion


        #region 构造监测报告
        TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
        objReportVo.ID = GetSerialNumber("t_mis_monitor_report_id");
        objReportVo.TASK_ID = objTask.ID;
        objReportVo.REPORT_CODE = objTask.CONTRACT_CODE;
        objReportVo.REPORT_EX_ATTACHE_ID = GetAttID(objTask.CONTRACT_ID, "AcceptanceContract");
        objReportVo.IF_GET = "0";

        if (new TMisMonitorTaskLogic().SaveTrans(objTask, objTaskClient, objTaskTested, objReportVo))
        {
            WriteLog("生成验收委托监测任务", "", LogInfo.UserInfo.USER_NAME + "生成验收委托监测任务" + objTask.ID + "成功");
        }
        #endregion
    }

    /// <summary>
    /// 获取附件ID
    /// </summary>
    /// <param name="strBusinessID">业务ID</param>
    /// <param name="strAttType">业务类型</param>
    /// <returns></returns>
    protected string GetAttID(string strBusinessID, string strAttType)
    {
        TOaAttVo objAtt = new TOaAttVo();
        objAtt.BUSINESS_ID = strBusinessID;
        objAtt.BUSINESS_TYPE = strAttType;
        return new TOaAttLogic().Details(objAtt).ID;
    }

    #endregion

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        //传递参数
        this.hdnContracID.Value = myServiceList.Count > 0 ? myServiceList[0].SERVICE_KEY_VALUE : "";
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //完成验收委托书流程 直接生成监测任务 进行报告流程
        SetReport(this.hdnContracID.Value);
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("验收委托", "task_id", this.hdnContracID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

}