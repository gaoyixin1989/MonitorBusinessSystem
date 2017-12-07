using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Data;
using System.Configuration;
///  委托书--委托书审核
/// 创建时间：2012-12-20
/// 创建人：胡方扬
public partial class Channels_Mis_Contract_ProgramAudit_ContractProgramAudit : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strBtnType = "", strCompanyId = "";
    private string strConfigFreqSetting = ConfigurationManager.AppSettings["FreqSetting"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
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
    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        wfControl.DoContractTaskWF(task_id, strCompanyId, strBtnType, strConfigFreqSetting);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion
}