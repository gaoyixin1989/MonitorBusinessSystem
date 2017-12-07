using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using System.Web.Services;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 物料采购流程
/// 创建人：魏林 2013-09-10
/// </summary>
public partial class Channels_OA_Part_ZZ_FillMaterialsNew : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["strtaskID"] != null && Request["strtaskID"].ToString().Length > 0)
            {
                this.hidId.Value = Request["strtaskID"].ToString();
            }
            if (Request["WF_ID"] != null && Request["WF_ID"].ToString().Length > 0)
            {
                this.hidWF_ID.Value = Request["WF_ID"].ToString();
            }
            if (Request["view"] != null && Request["view"].ToString().Length > 0)
            {
                this.hidView.Value = Request["view"].ToString();
            }
            wfControl.InitWFDict();
            InitPage();
        }
    }
    
    //页面初始化
    private void InitPage()
    {
        BindDataDictToControl("dept", this.PlanDept);

        TSysUserPostVo UserPostVo = new TSysUserPostVo();
        UserPostVo.USER_ID = LogInfo.UserInfo.ID;
        TSysPostVo PostVo = new TSysPostVo(); 
        PostVo = new TSysPostLogic().Details(((TSysUserPostVo)new TSysUserPostLogic().SelectByObject(UserPostVo, 0, 0)[0]).POST_ID);
        this.PlanDept.SelectedValue = PostVo.POST_DEPT_ID;
        
        this.hidUserID.Value = LogInfo.UserInfo.ID;
        this.PlanDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        this.PlanPerson.Value = LogInfo.UserInfo.REAL_NAME;

        if (!string.IsNullOrEmpty(this.hidId.Value))
        {
            TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstLogic().Details(this.hidId.Value);

            this.PlanBt.Value = objPartBuyRequst.APPLY_TITLE;
            this.PlanDept.SelectedValue = objPartBuyRequst.APPLY_DEPT_ID;
            this.PlanPerson.Value = !string.IsNullOrEmpty(objPartBuyRequst.APPLY_USER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APPLY_USER_ID).REAL_NAME : "";
            this.PlanDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APPLY_DATE) ? DateTime.Parse(objPartBuyRequst.APPLY_DATE).ToShortDateString() : "";
            this.PlanContent.Value = objPartBuyRequst.APPLY_CONTENT;
            this.TestOption.Value = objPartBuyRequst.APP_DEPT_INFO;
            this.TestName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_DEPT_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_DEPT_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.TestDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_DEPT_DATE) ? DateTime.Parse(objPartBuyRequst.APP_DEPT_DATE).ToShortDateString() : DateTime.Now.ToShortDateString();
            this.OfficeOption.Value = objPartBuyRequst.APP_OFFER_INFO;
            this.OfficeName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_OFFER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_OFFER_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.OfficeDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_OFFER_TIME) ? DateTime.Parse(objPartBuyRequst.APP_OFFER_TIME).ToShortDateString() : DateTime.Now.ToShortDateString();
            this.TechOption.Value = objPartBuyRequst.APP_MANAGER_INFO;
            this.TechName.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_MANAGER_ID) ? new TSysUserLogic().Details(objPartBuyRequst.APP_MANAGER_ID).REAL_NAME : LogInfo.UserInfo.REAL_NAME;
            this.TechDate.Value = !string.IsNullOrEmpty(objPartBuyRequst.APP_MANAGER_DATE) ? DateTime.Parse(objPartBuyRequst.APP_MANAGER_DATE).ToShortDateString() : DateTime.Now.ToShortDateString();
            this.hidStatus.Value = objPartBuyRequst.STATUS;
        }
        //根据当前状态设置控件可不可用
        DisableControls();
    }

    private void DisableControls()
    {
        if (this.hidView.Value.Trim() == "true")   //浏览状态
        {
            this.PlanBt.Disabled = true;
            this.PlanDept.Enabled = false;
            this.PlanPerson.Disabled = true;
            this.PlanDate.Disabled = true;
            this.PlanContent.Disabled = true;
            this.TestOption.Disabled = true;
            this.TestName.Disabled = true;
            this.TestDate.Disabled = true;
            this.OfficeOption.Disabled = true;
            this.OfficeName.Disabled = true;
            this.OfficeDate.Disabled = true;
            this.TechOption.Disabled = true;
            this.TechName.Disabled = true;
            this.TechDate.Disabled = true;
        }
        else
        {
            switch (this.hidStatus.Value.Trim())
            {
                case "0":    //申请状态
                    this.PlanPerson.Disabled = true;
                    this.PlanDate.Disabled = true;
                    break;
                case "1":    //科室主任审核
                    this.PlanBt.Disabled = true;
                    this.PlanDept.Enabled = false;
                    this.PlanPerson.Disabled = true;
                    this.PlanDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestName.Disabled = true;
                    this.TestDate.Disabled = true;
                    break;
                case "2":   //办公室主任审核
                    this.PlanBt.Disabled = true;
                    this.PlanDept.Enabled = false;
                    this.PlanPerson.Disabled = true;
                    this.PlanDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.TestName.Disabled = true;
                    this.TestDate.Disabled = true;
                    this.OfficeName.Disabled = true;
                    this.OfficeDate.Disabled = true;
                    break;
                case "3":  //仓库管理员审核
                    this.PlanBt.Disabled = true;
                    this.PlanDept.Enabled = false;
                    this.PlanPerson.Disabled = true;
                    this.PlanDate.Disabled = true;
                    this.PlanContent.Disabled = true;

                    this.TestOption.Disabled = true;
                    this.TestName.Disabled = true;
                    this.TestDate.Disabled = true;
                    this.OfficeOption.Disabled = true;
                    this.OfficeName.Disabled = true;
                    this.OfficeDate.Disabled = true;
                    this.TechName.Disabled = true;
                    this.TechDate.Disabled = true;
                    break;
                case "9":
                    this.PlanBt.Disabled = true;
                    this.PlanDept.Enabled = false;
                    this.PlanPerson.Disabled = true;
                    this.PlanDate.Disabled = true;
                    this.PlanContent.Disabled = true;
                    this.TestOption.Disabled = true;
                    this.TestName.Disabled = true;
                    this.TestDate.Disabled = true;
                    this.OfficeOption.Disabled = true;
                    this.OfficeName.Disabled = true;
                    this.OfficeDate.Disabled = true;
                    this.TechOption.Disabled = true;
                    this.TechName.Disabled = true;
                    this.TechDate.Disabled = true;
                    break;
            }
        }
    }

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

        if (this.PlanBt.Value.Trim() == "")
            strMsg = "申请主题不能为空";
        if (this.PlanContent.Value.Trim() == "")
            strMsg = "申购内容不能为空";

        return strMsg == "" ? true : false;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        string strStatus = "";
        //这里是产生和注册业务数据的地方
        TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstVo();
        if (string.IsNullOrEmpty(this.hidId.Value))
        {
            objPartBuyRequst.ID = GetSerialNumber("t_oa_PartBuyID");
            objPartBuyRequst.REMARK1 = "0";
            objPartBuyRequst.STATUS = "1";
            objPartBuyRequst.APPLY_USER_ID = LogInfo.UserInfo.ID;
            objPartBuyRequst.APPLY_DATE = DateTime.Now.ToShortDateString();
            objPartBuyRequst.APPLY_TITLE = this.PlanBt.Value.Trim();
            objPartBuyRequst.APPLY_DEPT_ID = this.PlanDept.SelectedValue;
            objPartBuyRequst.APPLY_CONTENT = this.PlanContent.Value.Trim();
            objPartBuyRequst.APPLY_TYPE = this.hidWF_ID.Value.Trim() == "OW_PARTPLAN" ? "01" : "02";

            if (new TOaPartBuyRequstLogic().Create(objPartBuyRequst))
            {
                wfControl.SaveInstStepServiceData("采购计划ID", "task_id", objPartBuyRequst.ID, "1");

                //注册编号
                wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "物料采购申请:" + this.PlanBt.Value.Trim();
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
                    objPartBuyRequst.APPLY_TITLE = this.PlanBt.Value.Trim();
                    objPartBuyRequst.APPLY_DEPT_ID = this.PlanDept.SelectedValue;
                    objPartBuyRequst.APPLY_CONTENT = this.PlanContent.Value.Trim();
                    objPartBuyRequst.APPLY_TYPE = this.hidWF_ID.Value.Trim() == "OW_PARTPLAN" ? "01" : "02";

                    wfControl.SaveInstStepServiceData("采购计划ID", "task_id", objPartBuyRequst.ID, strStatus);

                    //注册编号
                    wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //注册名称
                    wfControl.ServiceName = "物料采购申请:" + this.PlanBt.Value.Trim();
                    break;
                //科室主任审核
                case "1":
                    if (this.hidWF_ID.Value.Trim() == "OW_PARTPLAN")   //办公物料
                        strStatus = "2";
                    else
                        strStatus = "3";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_DEPT_INFO = this.TestOption.Value.Trim();
                    objPartBuyRequst.APP_DEPT_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_DEPT_DATE = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("采购计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //办公室主任审核
                case "2":
                    strStatus = "3";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_OFFER_INFO = this.OfficeOption.Value.Trim();
                    objPartBuyRequst.APP_OFFER_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_OFFER_TIME = DateTime.Now.ToShortDateString();

                    wfControl.SaveInstStepServiceData("采购计划ID", "task_id", objPartBuyRequst.ID, strStatus);
                    break;
                //仓管审核
                case "3":
                    strStatus = "9";
                    objPartBuyRequst.STATUS = strStatus;
                    objPartBuyRequst.APP_MANAGER_INFO = this.TechOption.Value.Trim();
                    objPartBuyRequst.APP_MANAGER_ID = LogInfo.UserInfo.ID;
                    objPartBuyRequst.APP_MANAGER_DATE = DateTime.Now.ToShortDateString();
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
    public static string savePARTPLANData(string PlanBt, string PlanDept, string PlanContent, string PlanType, string UserID)   
    {
        string strResult = "";

        TOaPartBuyRequstVo objPartBuyRequst = new TOaPartBuyRequstVo();
        objPartBuyRequst.REMARK1 = "0";
        objPartBuyRequst.STATUS = "0";
        objPartBuyRequst.APPLY_USER_ID = UserID;
        objPartBuyRequst.APPLY_DATE = DateTime.Now.ToShortDateString();
        objPartBuyRequst.APPLY_TITLE = PlanBt;
        objPartBuyRequst.APPLY_DEPT_ID = PlanDept;
        objPartBuyRequst.APPLY_CONTENT = PlanContent;
        objPartBuyRequst.APPLY_TYPE = PlanType;
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