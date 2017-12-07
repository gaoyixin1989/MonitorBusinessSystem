using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.BusinessLogic.Channels.OA.PART;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Sys.General;
using System.Web.Services;
/// <summary>
/// 呈报单流程
/// 创建人：魏林 2013-09-11
/// </summary>
public partial class Channels_OA_Part_ZZ_WorkSubmitNew : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strTaskProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["strtaskID"] != null && Request["strtaskID"].ToString().Length > 0)
            {
                this.hidId.Value = Request["strtaskID"].ToString();
            }
            if (Request["view"] != null && Request["view"].ToString().Length > 0)
            {
                this.hidView.Value = Request["view"].ToString();
            }
            wfControl.InitWFDict();
            InitPage();
        }
    }

    #region//页面初始化
    private void InitPage()
    {
        BindDeptDataDictToControl("dept", this.PlanDept, base.LogInfo.UserInfo.ID); 
        this.AgentDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        this.AgentPerson.Value = LogInfo.UserInfo.REAL_NAME;
        this.ManageDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        if (!string.IsNullOrEmpty(this.hidId.Value))
        {
            TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstLogic().Details(this.hidId.Value);

            this.PlanDept.SelectedValue = objPartBuyRequst.APPLY_DEPT_ID;
            this.AgentPerson.Value = !string.IsNullOrEmpty(objPartBuyRequst.APPLY_USER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APPLY_USER_ID).REAL_NAME : "";
            this.AgentDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APPLY_DATE) ? DateTime.Parse(objPartBuyRequst.APPLY_DATE).ToShortDateString() : "";
            this.ManageDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APPLY_DATE) ? DateTime.Parse(objPartBuyRequst.APPLY_DATE).ToShortDateString() : "";
            this.PlanContent.Value = objPartBuyRequst.APPLY_CONTENT;
            
            this.TestOption.Value = objPartBuyRequst.APP_DEPT_INFO;
            this.ChiefPerson.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_DEPT_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_DEPT_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.ChiefDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_DEPT_DATE) ? DateTime.Parse(objPartBuyRequst.APP_DEPT_DATE).ToShortDateString() : DateTime.Now.ToShortDateString();
            
            this.TechOption.Value = objPartBuyRequst.APP_MANAGER_INFO;
            this.LeaderName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_MANAGER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_MANAGER_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.LeaderDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_MANAGER_DATE) ? DateTime.Parse(objPartBuyRequst.APP_MANAGER_DATE).ToShortDateString() : DateTime.Now.ToShortDateString();

            this.OfferOption.Value = objPartBuyRequst.APP_OFFER_INFO;
            this.OfferName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_OFFER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_OFFER_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.OfferDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_OFFER_TIME) ? DateTime.Parse(objPartBuyRequst.APP_OFFER_TIME).ToShortDateString() : DateTime.Now.ToShortDateString();

            this.SerialOption.Value = objPartBuyRequst.APP_LEADER_INFO;
            this.SerialName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_LEADER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_LEADER_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.SerialDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_LEADER_DATE) ? DateTime.Parse(objPartBuyRequst.APP_LEADER_DATE).ToShortDateString() : DateTime.Now.ToShortDateString();

            this.hidStatus.Value = objPartBuyRequst.STATUS;
        }
        //根据当前状态设置控件可不可用
        DisableControls();
    }
    #endregion

    #region//状态
    private void DisableControls()
    {
        if (this.hidView.Value.Trim() == "true")   //浏览状态
        {
            this.PlanDept.Enabled = false;
            this.ManageDate.Disabled = true;
            this.AgentPerson.Disabled = true;
            this.AgentDate.Disabled = true;
            this.PlanContent.Disabled = true;

            this.TestOption.Disabled = true;
            this.ChiefPerson.Disabled = true;
            this.ChiefDate.Disabled = true;
            this.TechOption.Disabled = true;
            this.LeaderName.Disabled = true;
            this.LeaderDate.Disabled = true;
            this.OfferOption.Disabled = true;
            this.OfferName.Disabled = true;
            this.OfferDate.Disabled = true;
            this.SerialOption.Disabled = true;
            this.SerialName.Disabled = true;
            this.SerialDate.Disabled = true;
        }
        else
        {
            switch (this.hidStatus.Value.Trim())
            {
                case "0":    //申请状态
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    break;
                case "1":    //科室审核
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    break;
                case "2":   //主管领导审核
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    this.LeaderName.Disabled = true;
                    this.LeaderDate.Disabled = true;
                    break;
                case "3":  //办公室审核
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    this.TechOption.Disabled = true;
                    this.LeaderName.Disabled = true;
                    this.LeaderDate.Disabled = true;
                    this.OfferName.Disabled = true;
                    this.OfferDate.Disabled = true;
                    break;
                case "4":  //站长审核
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    this.TechOption.Disabled = true;
                    this.LeaderName.Disabled = true;
                    this.LeaderDate.Disabled = true;
                    this.OfferOption.Disabled = true;
                    this.OfferName.Disabled = true;
                    this.OfferDate.Disabled = true;
                    this.SerialName.Disabled = true;
                    this.SerialDate.Disabled = true;
                    break;
                case "5":
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    this.TechOption.Disabled = true;
                    this.LeaderName.Disabled = true;
                    this.LeaderDate.Disabled = true;
                    this.OfferOption.Disabled = true;
                    this.OfferName.Disabled = true;
                    this.OfferDate.Disabled = true;
                    this.SerialOption.Disabled = true;
                    this.SerialName.Disabled = true;
                    this.SerialDate.Disabled = true;
                    break;
                case "9":
                    this.PlanDept.Enabled = false;
                    this.ManageDate.Disabled = true;
                    this.AgentPerson.Disabled = true;
                    this.AgentDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.ChiefPerson.Disabled = true;
                    this.ChiefDate.Disabled = true;
                    this.TechOption.Disabled = true;
                    this.LeaderName.Disabled = true;
                    this.LeaderDate.Disabled = true;
                    this.OfferOption.Disabled = true;
                    this.OfferName.Disabled = true;
                    this.OfferDate.Disabled = true;
                    this.SerialOption.Disabled = true;
                    this.SerialName.Disabled = true;
                    this.SerialDate.Disabled = true;
                    break;
            }
        }
    }
    #endregion

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList != null)
        {
            this.hidId.Value = myServiceList[0].SERVICE_KEY_VALUE;
            this.hidStatus.Value = myServiceList[0].SERVICE_ROW_SIGN;
        }
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方

        strMsg = "";
        return true;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        string strStatus = "";
        //这里是产生和注册业务数据的地方
        TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstVo();
        if (string.IsNullOrEmpty(this.hidId.Value))
        {
            objPartBuyRequst.ID = GetSerialNumber("t_oa_PartBuyID");
            objPartBuyRequst.REMARK1 = "1";
            objPartBuyRequst.STATUS = "1";
            objPartBuyRequst.APPLY_USER_ID = LogInfo.UserInfo.ID;
            objPartBuyRequst.APPLY_DATE = DateTime.Now.ToShortDateString();
            objPartBuyRequst.APPLY_TITLE = "呈报单";
            objPartBuyRequst.APPLY_DEPT_ID = this.PlanDept.SelectedValue;
            objPartBuyRequst.APPLY_CONTENT = this.PlanContent.Value.Trim();
            objPartBuyRequst.APPLY_TYPE = "03";

            if (new TOaPartBuyRequstLogic().Create(objPartBuyRequst))
            {
                wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, "1");

                //注册编号
                wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "呈报单申请:" + this.PlanDept.SelectedItem.Text;
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
        }
        else
        {
            objPartBuyRequst.ID = this.hidId.Value.Trim();

            switch (this.hidStatus.Value.Trim())
            {
                //申请状态
                case "":
                case "0":
                    strStatus = "1";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APPLY_TITLE = "呈报单";
                    objPartBuyRequst.APPLY_DEPT_ID = this.PlanDept.SelectedValue;
                    objPartBuyRequst.APPLY_CONTENT = this.PlanContent.Value.Trim();
                    objPartBuyRequst.APPLY_TYPE = "03";

                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);

                    //注册编号
                    wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //注册名称
                    wfControl.ServiceName = "呈报单申请:" + this.PlanDept.SelectedItem.Text;
                    break;
                //科室审核
                case "1":
                    strStatus = "2";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_DEPT_INFO = this.TestOption.Value.Trim();
                    objPartBuyRequst.APP_DEPT_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_DEPT_DATE = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //主管领导审核
                case "2":
                    strStatus = "3";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_MANAGER_INFO = this.TechOption.Value.Trim();
                    objPartBuyRequst.APP_MANAGER_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_MANAGER_DATE = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //办公室主任审核
                case "3":
                    strStatus = "4";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_OFFER_INFO = this.OfferOption.Value.Trim();
                    objPartBuyRequst.APP_OFFER_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_OFFER_TIME = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //站长审核
                case "4":
                    strStatus = "5";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_LEADER_INFO = this.SerialOption.Value.Trim();
                    objPartBuyRequst.APP_LEADER_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_LEADER_DATE = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //回馈
                case "5":
                    strStatus = "9";
                    objPartBuyRequst.STATUS = strStatus;


                    wfControl.SaveInstStepServiceData("呈报单计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
            }

            if (new TOaPartBuyRequstLogic().Edit(objPartBuyRequst))
            {
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion

    /// <summary>
    /// 附件上传前先做保存
    /// </summary>
    /// <returns></returns>
    /// <remarks>ok</remarks>
    [WebMethod]
    public static string savePARTPLANData(string PlanDept)
    {
        string strResult = "";

        TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstVo();
        objPartBuyRequst.REMARK1 = "0";
        objPartBuyRequst.STATUS = "0";
        objPartBuyRequst.APPLY_DATE = DateTime.Now.ToShortDateString();
        objPartBuyRequst.APPLY_DEPT_ID = PlanDept;
        objPartBuyRequst.ID = GetSerialNumber("t_oa_PartBuyID");
        if (new TOaPartBuyRequstLogic().Create(objPartBuyRequst))
        {
            strResult = objPartBuyRequst.ID;
        }
        else
        {
            strResult = "";
        }
        return strResult;
    }
}