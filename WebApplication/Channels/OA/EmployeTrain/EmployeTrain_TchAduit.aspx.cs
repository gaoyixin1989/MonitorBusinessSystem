using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Channels.OA.TRAIN;
using i3.BusinessLogic.Channels.OA.TRAIN;

public partial class Channels_OA_EmployeTrain_EmployeTrain_TchAduit : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strLength = "", strTaskProjectName = "", strBtnType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
        //获取隐藏域参数
        GetHiddenParme();
        if (!String.IsNullOrEmpty(task_id))
        {
            //注册编号
            //wfControl.ServiceCode = "BP" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //注册名称
            //wfControl.ServiceName = strTaskProjectName + DateTime.Now.ToString("yyyy") + "年员工培训登记表";
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
        strBtnType = this.hidBtnType.Value.ToString();
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
        if (!String.IsNullOrEmpty(task_id)&&String.IsNullOrEmpty(strBtnType))
        {
            TOaTrainPlanVo objItems = new TOaTrainPlanVo();
            objItems.ID = task_id;
            objItems.FLOW_STATUS = "1";
            //根据用户登录ID获取其档案ID
            TOaEmployeInfoVo objEmploye = new TOaEmployeInfoVo();
            objEmploye.USER_ID = LogInfo.UserInfo.ID;
            DataTable dt = new DataTable();
            dt = new TOaEmployeInfoLogic().SelectByTable(objEmploye);
            if (dt.Rows.Count > 0)
            {
                objItems.TECH_APP_ID = dt.Rows[0]["ID"].ToString();
            }
            objItems.TECH_APP = this.hidTchOption.Value.ToString();
            objItems.TECH_APP_DATE = this.hidDate.Value.ToString();
            objItems.APP_FLOW = "站长审核";
            if (new TOaTrainPlanLogic().Edit(objItems))
            {
                wfControl.SaveInstStepServiceData("员工培训ID", "task_id", task_id, "2");
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
        }
        else
        {
            if (!String.IsNullOrEmpty(task_id) && strBtnType == "back")
            {
                TOaTrainPlanVo objItems = new TOaTrainPlanVo();
                objItems.ID = task_id;
                objItems.FLOW_STATUS = "-1";
                if (new TOaTrainPlanLogic().Edit(objItems))
                {
                    wfControl.SaveInstStepServiceData("员工培训ID(退回)", "task_id", task_id, "B");
                }
                else
                {
                    LigerDialogAlert("数据发送失败！", "error");
                    return;
                }
            }

        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion
}