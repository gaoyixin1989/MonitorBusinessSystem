using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.ValueObject.Sys.WF;
using i3.View;

/// <summary>
/// 功能描述：验收方案审核
/// 创建时间：2012-12-29
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Contract_EnvAcceptance_AcceptanceCheck : PageBaseForWF, IWFStepRules
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
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("验收委托", "task_id", this.hdnContracID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

}