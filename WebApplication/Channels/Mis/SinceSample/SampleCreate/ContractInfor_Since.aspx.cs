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
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using System.Configuration;
/// <summary>
/// 功能描述：自送样委托书录入
/// 创建时间：2012-12-18
/// 创建人：胡方扬
/// </summary>
public partial class Channels_Mis_SinceSample_SampleCreate_ContractInfor_Since : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strTaskCode = "", strTaskProjectName = "", strBtnType = "", strCompanyId = "";
    private string strConfigFreqSetting ="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
        //获取隐藏域参数
        GetHiddenParme();
        if (!String.IsNullOrEmpty(task_id) && !String.IsNullOrEmpty(strTaskCode) && !String.IsNullOrEmpty(strTaskProjectName))
        {
            //注册委托书编号
            wfControl.ServiceCode = strTaskCode;
            //注册委托书项目名称
            wfControl.ServiceName = strTaskProjectName;
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strTaskCode = this.hidTaskCode.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
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
        //这里是产生和注册业务数据的地方
        //if (!String.IsNullOrEmpty(task_id))
        //{
        //    TMisContractVo objItems = new TMisContractVo();
        //    objItems.ID = task_id;
        //    objItems.CONTRACT_STATUS = "1";
        //    objItems.PROJECT_ID = LogInfo.UserInfo.ID;
        //    if (new TMisContractLogic().Edit(objItems))
        //    {
        //        wfControl.SaveInstStepServiceData("委托书(自送样)ID", "task_id", task_id, "1");
        //    }
        //    else
        //    {
        //        LigerDialogAlert("数据发送失败！", "error");
        //        return;
        //    }
        //}
        wfControl.DoContractTaskWF(task_id, strCompanyId, strBtnType, strConfigFreqSetting,"1");
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion
}