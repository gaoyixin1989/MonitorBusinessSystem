using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
/// <summary>
/// 功能描述：发文拟稿
/// 创建日期：2013-2-3
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_FW_QHD_FWAdmin : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["fw_id"] != null && this.Request["fw_id"].ToString().Length > 0)
        {
            this.hidTaskId.Value = this.Request["fw_id"].ToString();
        }
        if (this.Request["fwread"] != null && this.Request["fwread"].ToString().Length > 0)
        {
            this.fwread.Value = this.Request["fwread"].ToString();
        }
        if (this.Request["view"] != null && this.Request["view"].ToString().Length > 0)
        {
            this.hidView.Value = this.Request["view"].ToString();
        }
        string strUrl = this.Page.Request.Url.ToString().ToUpper();
        if (!IsPostBack && !strUrl.Contains("SAVEFWDATA"))
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
        this.FW_DATE.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.USER_NAME.Text = LogInfo.UserInfo.REAL_NAME;
        this.USER_ID.Value = LogInfo.UserInfo.ID;

        //this.APP_INFO.Disabled = true;
        this.ISSUE_INFO.Disabled = true;

        if (this.hidView.Value == "true")
        {
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            BindObjectToControls(objFW, this.form1);
            base.DisableAllControls(this.form1);
            this.MJ.Text = objFW.MJ;
            this.START_DATE.Text = DateTime.Parse(objFW.START_DATE).ToShortDateString();
            this.END_DATE.Text = DateTime.Parse(objFW.END_DATE).ToShortDateString();
            this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToString("yyyy/MM/dd");

            //this.APP_ID.Text = (objFW.APP_ID.Length > 0) ? new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME : "";
            //this.APP_DATE.Text = (objFW.APP_DATE.Length > 0) ? DateTime.Parse(objFW.APP_DATE).ToShortDateString() : "";

            this.ISSUE_ID.Text = (objFW.ISSUE_ID.Length > 0) ? new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME : "";
            this.ISSUE_DATE.Text = (objFW.ISSUE_DATE.Length > 0) ? DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString() : "";

            this.divContratSubmit.Visible = false;
            this.divBack.Visible = true;
            this.btnFileUp.Visible = false;

            List<TOaSwReadVo> list = new TOaSwReadLogic().SelectByReadUser(objFW.ID);
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                string swid = list[i].SW_PLAN_ID;
                string swname = new TSysUserLogic().Details(swid).REAL_NAME;
                if (i == 0)
                {
                    str = swname;
                }
                else if (i > 0)
                {
                    str = str + "," + swname;
                }

            }

            this.Text1.Value = str;
            this.TReadUserList.Attributes.Add("style", "display:block");
        }
        else if (this.hidTaskId.Value.Length > 0)
        {
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            BindObjectToControls(objFW, this.form1);
            this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToString("yyyy/MM/dd");

            this.START_DATE.Text = DateTime.Parse(objFW.START_DATE).ToShortDateString();
            this.END_DATE.Text = DateTime.Parse(objFW.END_DATE).ToShortDateString();
            this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToString("yyyy/MM/dd");
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
        TOaFwInfoVo objFW = new TOaFwInfoVo();
        objFW.YWNO = this.YWNO.Text;
        objFW.FWNO = this.FWNO.Text;
        objFW.FW_DATE = DateTime.Now.ToString();
        objFW.FW_TITLE = this.FW_TITLE.Text;
        objFW.FW_STATUS = "1";
        objFW.ZB_DEPT = this.ZB_DEPT.Text;
        objFW.DRAFT_ID = LogInfo.UserInfo.ID;
        objFW.MJ = this.MJ.SelectedValue;
        objFW.START_DATE = this.START_DATE.Text;
        objFW.END_DATE = this.END_DATE.Text;
        objFW.DRAFT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (this.hidTaskId.Value.Length == 0)
        {
            objFW.ID = GetSerialNumber("t_oa_FWInfoID");

            if (new TOaFwInfoLogic().Create(objFW))
            {
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                //注册编号
                wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "公文发放";
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
        }
        else
        {
            objFW.ID = this.hidTaskId.Value;
           //TOaFwInfoVo fv =  new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            if (new TOaFwInfoLogic().Edit(objFW))
            {
                wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                //注册编号
                wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "公文发放";
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
    public static string saveFWData(string strYWNO, string strFWNO, string strFW_TITLE, string strZB_DEPT, string strDRAFT_ID, string strMJ, string strSTART_DATE, string strEND_DATE)
    {
        string strResult = "";
        TOaFwInfoVo objFW = new TOaFwInfoVo();
        objFW.YWNO = strYWNO;
        objFW.FWNO = strFWNO;
        objFW.FW_DATE = DateTime.Now.ToString();
        objFW.FW_TITLE = strFW_TITLE;
        //objFW.FW_STATUS = "1";
        objFW.ZB_DEPT = strZB_DEPT;
        objFW.DRAFT_ID = strDRAFT_ID;
        objFW.MJ = strMJ;
        objFW.START_DATE = strSTART_DATE;
        objFW.END_DATE = strEND_DATE;
        objFW.FW_STATUS = "0";
        objFW.DRAFT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        objFW.ID = GetSerialNumber("t_oa_FWInfoID");

        if (new TOaFwInfoLogic().Create(objFW))
            strResult = objFW.ID;
        return strResult;
    }
}