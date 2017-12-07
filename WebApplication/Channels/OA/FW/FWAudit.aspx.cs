using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：发文核稿
/// 创建日期：2013-2-3
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_FW_FWAudit : PageBaseForWF, IWFStepRules
{
    private string strBtnType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["fw_id"] != null && this.Request["fw_id"].ToString().Length > 0)
        {
            this.hidTaskId.Value = this.Request["fw_id"].ToString();
        }
        if (this.Request["task_tatus"] != null && this.Request["task_tatus"].ToString().Length > 0)
        {
            this.hidTask_Tatus.Value = this.Request["task_tatus"].ToString();
        }
        strBtnType = this.hidBtnType.Value.ToString();
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
            InitPage();
        }

    }

    //页面初始化
    private void InitPage()
    {
        TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
        this.YWNO.Text = objFW.YWNO;
        this.FWNO.Text = objFW.FWNO;
        this.FW_TITLE.Text = objFW.FW_TITLE;
        this.ZB_DEPT.Text = objFW.ZB_DEPT;
        this.DRAFT_ID.Text = new TSysUserLogic().Details(objFW.DRAFT_ID).REAL_NAME;
        this.MJ.Text = getDictName(objFW.MJ, "FW_MJ");
        this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToShortDateString();
        this.START_DATE.Text = DateTime.Parse(objFW.START_DATE).ToShortDateString();
        this.END_DATE.Text = DateTime.Parse(objFW.END_DATE).ToShortDateString();

        if (this.hidTask_Tatus.Value == "1")
        {
            this.ISSUE_INFO.Disabled = true;
            this.REG_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "2")
        {
            this.APP_INFO.Value = objFW.APP_INFO;
            this.APP_ID.Text = new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME;
            this.APP_DATE.Text = DateTime.Parse(objFW.APP_DATE).ToShortDateString();

            this.APP_INFO.Disabled = true;
            this.REG_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "3")
        {
            this.APP_INFO.Value = objFW.APP_INFO;
            this.APP_ID.Text = new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME;
            this.APP_DATE.Text = DateTime.Parse(objFW.APP_DATE).ToShortDateString();

            this.ISSUE_INFO.Value = objFW.ISSUE_INFO;
            this.ISSUE_ID.Text = new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME;
            this.ISSUE_DATE.Text = DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString();

            this.APP_INFO.Disabled = true;
            this.ISSUE_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "9")
        {
            this.APP_INFO.Value = objFW.APP_INFO;
            this.APP_ID.Text = new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME;
            this.APP_DATE.Text = DateTime.Parse(objFW.APP_DATE).ToShortDateString();

            this.ISSUE_INFO.Value = objFW.ISSUE_INFO;
            this.ISSUE_ID.Text = new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME;
            this.ISSUE_DATE.Text = DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString();

            this.REG_INFO.Value = objFW.REG_INFO;
            this.REG_ID.Text = new TSysUserLogic().Details(objFW.REG_ID).REAL_NAME;
            this.REG_DATE.Text = DateTime.Parse(objFW.REG_DATE).ToShortDateString();

            this.APP_INFO.Disabled = true;
            this.ISSUE_INFO.Disabled = true;
            this.REG_INFO.Disabled = true;
        }
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;

        this.hidTask_Tatus.Value = new TOaFwInfoLogic().Details(this.hidTaskId.Value).FW_STATUS;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方

        strMsg = "";
        return true;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        if (this.hidTaskId.Value.Length > 0 && String.IsNullOrEmpty(strBtnType))
        {
            //这里是产生和注册业务数据的地方
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            objFW.ID = this.hidTaskId.Value;

            if (this.hidTask_Tatus.Value == "1")
            {
                objFW.APP_INFO = this.APP_INFO.Value;
                objFW.APP_ID = LogInfo.UserInfo.ID;
                objFW.APP_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objFW.FW_STATUS = "2";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "2");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objFW.ISSUE_INFO = this.ISSUE_INFO.Value;
                objFW.ISSUE_ID = LogInfo.UserInfo.ID;
                objFW.ISSUE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objFW.FW_STATUS = "3";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "3");
            }
            else if (this.hidTask_Tatus.Value == "3")
            {
                objFW.REG_INFO = this.REG_INFO.Value;
                objFW.REG_ID = LogInfo.UserInfo.ID;
                objFW.REG_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objFW.FW_STATUS = "8";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "9");
            }
            else if (this.hidTask_Tatus.Value == "8")
            {
                //objFW.REG_INFO = this.REG_INFO.Value;
                objFW.PIGONHOLE_ID = LogInfo.UserInfo.ID;
                objFW.PIGONHOLE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objFW.FW_STATUS = "9";
                //wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "9");
            }
            new TOaFwInfoLogic().Edit(objFW);
        }
        else if (this.hidTaskId.Value.Length > 0 && strBtnType == "back")
        {
            //这里是产生和注册业务数据的地方
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            objFW.ID = this.hidTaskId.Value;

            if (this.hidTask_Tatus.Value == "1")
            {
                objFW.APP_ID = "###";
                objFW.APP_DATE = "###";
                objFW.FW_STATUS = "0";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objFW.ISSUE_ID = "###";
                objFW.ISSUE_DATE = "###";
                objFW.FW_STATUS = "1";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");
            }
            else if (this.hidTask_Tatus.Value == "3")
            {
                objFW.REG_ID = "###";
                objFW.REG_DATE = "###";
                objFW.FW_STATUS = "2";
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");
            }
            new TOaFwInfoLogic().Edit(objFW);
        }

    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion
}