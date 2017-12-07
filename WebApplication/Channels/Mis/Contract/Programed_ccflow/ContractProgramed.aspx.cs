using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;
using System.Data;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Configuration;
using WebApplication;

/// 委托书--采样任务编制
/// 创建时间：2012-12-18
/// 创建人：胡方扬
public partial class Channels_Mis_Contract_Programed_ContractProgramed : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strBtnType = "", strCompanyId = "";
    private string strConfigFreqSetting = ConfigurationManager.AppSettings["FreqSetting"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
        if (!IsPostBack)
        {
            //wfControl.InitWFDict();

            var workID = Request.QueryString["WorkID"];
            workID = workID ?? "-9999999";
            //workID = "355";
            var contract = new TMisContractLogic().SelectByObject(new TMisContractVo { REMARK5 = workID });
            this.hidTaskId.Value = contract.ID;
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strBtnType = this.hidBtnType.Value.ToString();
        strCompanyId = this.hidCompanyId.Value.ToString();
    }

    /// <summary>
    /// 插入监测计划信息  暂时不用
    /// </summary>
    /// <returns></returns>
    //private bool CreateContractPlan()
    // {
    //     bool flag = false;
    //     DataTable dt = new DataTable();
    //     DataTable dtDict = new DataTable();
    //     //获取监测频次字典项
    //     dtDict = getDictList("Freq");
    //     TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
    //     objItems.CONTRACT_ID = task_id;
    //     //获取当前委托书监测点位
    //     dt = GetMonitorPoint();
    //     if (dt != null && dtDict != null)
    //     {
    //         if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, dtDict, task_id))
    //         {
    //             flag = true;
    //             string strMessage = LogInfo.UserInfo.USER_NAME + "采样预约计划点位频次生成成功";
    //             WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanFreqInfo, "", strMessage);
    //         }
    //     }
    //     return flag;
    // }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        //List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //wfControl.DoContractTaskWF(task_id, strCompanyId, strBtnType, strConfigFreqSetting);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
         
        var workID = Convert.ToInt32(Request.QueryString["WorkID"]);
        var flowId = Request.QueryString["FK_Flow"];
        var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
        var fid = Convert.ToInt32(Request.QueryString["FID"]);
        var emps = Request.QueryString["WorkID"];
        var ff = CCFlowFacade.SetNextWork(flowId, workID, nodeId, "administrator,llw", fid);

        var fddf = CCFlowFacade.SetNextCC(workID, nodeId, "administrator,llw@22222,llw@uuuu");
    }
}