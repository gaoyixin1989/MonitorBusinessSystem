using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
using i3.BusinessLogic.Sys.General;
/// <summary>
/// 功能描述：收文登记
/// 创建日期：
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_OA_SW_SWEdit : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["strtaskID"] != null && this.Request["strtaskID"].ToString().Length > 0)
        {
            this.hidTaskId.Value = this.Request["strtaskID"].ToString();
        }
        if (this.Request["view"] != null && this.Request["view"].ToString().Length > 0)
        {
            this.hidView.Value = this.Request["view"].ToString();
        }
        string strUrl = this.Page.Request.Url.ToString().ToUpper();
        if (!IsPostBack && !strUrl.Contains("SAVESWDATA"))
        {
            wfControl.InitWFDict();
            InitPage();
        }
        base.ReplaceDisabledControlsToLabels();
    }

    //页面初始化
    private void InitPage()
    {
        BindDataDictToControl("FW_MJ", this.MJ);
        this.SW_REG_DATE.Text = DateTime.Now.ToString("yyyy/MM/dd");

        this.SW_PLAN_INFO.Disabled = true;
        this.SW_PLAN_APP_INFO.Disabled = true;
        this.SW_APP_INFO.Disabled = true;

        if (this.hidView.Value == "true")
        {
            TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            BindObjectToControls(objSW, this.form1);
            this.SW_REG_DATE.Text = DateTime.Parse(objSW.SW_REG_DATE).ToString("yyyy/MM/dd");
            base.DisableAllControls(this.form1);
            this.SW_SIGN_DATE.Text = DateTime.Parse(objSW.SW_SIGN_DATE).ToShortDateString();

            this.SW_PLAN_ID.Text = (objSW.SW_PLAN_ID.Length > 0) ? new TSysUserLogic().Details(objSW.SW_PLAN_ID).REAL_NAME : "";
            this.SW_PLAN_DATE.Text = (objSW.SW_PLAN_DATE.Length > 0) ? DateTime.Parse(objSW.SW_PLAN_DATE).ToShortDateString() : "";

            this.SW_PLAN_APP_ID.Text = (objSW.SW_PLAN_APP_ID.Length > 0) ? new TSysUserLogic().Details(objSW.SW_PLAN_APP_ID).REAL_NAME : "";
            this.SW_PLAN_APP_DATE.Text = (objSW.SW_PLAN_APP_DATE.Length > 0) ? DateTime.Parse(objSW.SW_PLAN_APP_DATE).ToShortDateString() : "";

            this.SW_APP_ID.Text = (objSW.SW_APP_ID.Length > 0) ? new TSysUserLogic().Details(objSW.SW_APP_ID).REAL_NAME : "";
            this.SW_APP_DATE.Text = (objSW.SW_APP_DATE.Length > 0) ? DateTime.Parse(objSW.SW_APP_DATE).ToShortDateString() : "";

            this.divContratSubmit.Visible = false;
            this.divBack.Visible = true;
        }
        else if (this.hidTaskId.Value.Length > 0)
        {
            TOaSwInfoVo objFW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            BindObjectToControls(objFW, this.form1);
            this.SW_REG_DATE.Text = DateTime.Parse(objFW.SW_REG_DATE).ToString("yyyy/MM/dd");
        }
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList != null)
            this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;
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
        TOaSwInfoVo objSW = new TOaSwInfoVo();

        objSW.FROM_CODE = this.FROM_CODE.Text;
        objSW.SW_CODE = this.SW_CODE.Text;
        objSW.SW_FROM = this.SW_FROM.Text;
        objSW.SW_COUNT = this.SW_COUNT.Text;
        objSW.SW_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
        objSW.SW_TITLE = this.SW_TITLE.Text;
        objSW.SW_MJ = this.MJ.SelectedValue;
        objSW.SW_SIGN_ID = this.SW_SIGN_ID.Text;
        objSW.SW_SIGN_DATE = this.SW_SIGN_DATE.Text;
        objSW.SW_STATUS = "1";
        objSW.SW_REG_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        objSW.SW_REG_ID = LogInfo.UserInfo.ID;

        if (this.hidTaskId.Value.Length == 0)
        {
            objSW.ID = GetSerialNumber("t_oa_SWInfoID");

            if (new TOaSwInfoLogic().Create(objSW))
            {
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "1");

                //注册编号
                wfControl.ServiceCode = "SW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "“" + this.SW_TITLE.Text + "”" + "公文收取";
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
        }
        else
        {
            objSW.ID = this.hidTaskId.Value;
            if (new TOaSwInfoLogic().Edit(objSW))
            {
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "1");

                //注册编号
                wfControl.ServiceCode = "SW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "“" + this.SW_TITLE.Text + "”" + "公文收取";
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
    [WebMethod]
    public static string saveSWData(string strFROM_CODE, string strSW_CODE, string strSW_TITLE, string strSW_FROM, string strSW_COUNT, string strMJ, string strSW_SIGN_ID, string strSW_SIGN_DATE)
    {
        string strResult = "";
        TOaSwInfoVo objSW = new TOaSwInfoVo();
        objSW.FROM_CODE = strFROM_CODE;
        objSW.SW_CODE = strSW_CODE;
        objSW.SW_FROM = strSW_FROM;
        objSW.SW_COUNT = strSW_COUNT;
        objSW.SW_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
        objSW.SW_TITLE = strSW_TITLE;
        objSW.SW_MJ = strMJ;
        objSW.SW_SIGN_ID = strSW_SIGN_ID;
        objSW.SW_SIGN_DATE = strSW_SIGN_DATE;
        objSW.SW_STATUS = "0";
        objSW.SW_REG_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        objSW.ID = GetSerialNumber("t_oa_SWInfoID");

        if (new TOaSwInfoLogic().Create(objSW))
            strResult = objSW.ID;
        return strResult;
    }
}