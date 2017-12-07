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
using System.Web.Script.Serialization;
namespace n19
{
    /// <summary>
    /// 功能描述：发文拟稿
    /// 创建日期：2013-6-26
    /// 创建人  ：李焕明
    /// </summary>
    public partial class Channels_OA_FW_QHD_FWAdmin : PageBaseForWF, IWFStepRules
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Request["fw_id"]))
            {
                this.hidTaskId.Value = this.Request["fw_id"].ToString();
            }

            if (!string.IsNullOrEmpty(this.Request["view"]))
            {
                this.hidView.Value = this.Request["view"].ToString();
            }

            if (!IsPostBack)
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
            this.DRAFT_NAME.Text = LogInfo.UserInfo.REAL_NAME;

            this.APP_INFO.Disabled = true;
            this.ISSUE_INFO.Disabled = true;
            this.CTS_INFO.Disabled = true;

            if (this.hidView.Value == "true1" || this.hidView.Value == "true2")
            {
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);

                BindObjectToControls(objFW, this.form1);//？？？
                base.DisableAllControls(this.form1);

                this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToString("yyyy/MM/dd");

                this.APP_ID.Text = !string.IsNullOrEmpty(objFW.APP_ID) ? new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME : "";
                this.APP_DATE.Text = !string.IsNullOrEmpty(objFW.APP_DATE) ? DateTime.Parse(objFW.APP_DATE).ToShortDateString() : "";

                this.CTS_ID.Text = !string.IsNullOrEmpty(objFW.CTS_ID) ? new TSysUserLogic().Details(objFW.CTS_ID).REAL_NAME : "";
                this.CTS_DATE.Text = !string.IsNullOrEmpty(objFW.CTS_DATE) ? DateTime.Parse(objFW.CTS_DATE).ToShortDateString() : "";

                this.ISSUE_ID.Text = !string.IsNullOrEmpty(objFW.ISSUE_ID) ? new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME : "";
                this.ISSUE_DATE.Text = !string.IsNullOrEmpty(objFW.ISSUE_DATE) ? DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString() : "";

                this.divContratSubmit.Visible = false;
                this.divBack.Visible = true;
                this.btnFileUp.Visible = false;
            }
            else if (!string.IsNullOrEmpty(this.hidTaskId.Value))
            {
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
                BindObjectToControls(objFW, this.form1);

                if (!string.IsNullOrEmpty(objFW.FW_DATE))
                {
                    this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToString("yyyy/MM/dd");
                }
            }
        }

        /// <summary>
        /// 数据检验
        /// </summary>
        /// <returns></returns>
        /// <remarks>ok</remarks>
        private string ValidateBusinessData()
        {
            string validateMsg = "";

            //if (string.IsNullOrEmpty(YWNO.Text))
            //{
            //    validateMsg += "请填写编号！<br />";
            //}

            if (string.IsNullOrEmpty(FW_TITLE.Text))
            {
                validateMsg += "请填写文件标题！<br />";
            }

            if (validateMsg != "")
            {
                validateMsg = "<br />" + validateMsg;
            }

            return validateMsg;
        }

        #region 工作流

        void IWFStepRules.LoadAndViewBusinessData()
        {
            //这里是载入和显示业务数据的地方
            List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
            if (myServiceList != null)
            {
                //这里应该不需要？？？
                this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;
            }
        }

        bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
        {
            //这里是验证组件和业务数据的地方

            strMsg = "";

            strMsg = ValidateBusinessData();

            return string.IsNullOrEmpty(strMsg) ? true : false;


        }

        void IWFStepRules.CreatAndRegisterBusinessData()
        {
            //这里是产生和注册业务数据的地方
            TOaFwInfoVo objFW = new TOaFwInfoVo();

            objFW.YWNO = this.YWNO.Text;
            objFW.FWNO = this.YWNO.Text;
            objFW.FW_TITLE = this.FW_TITLE.Text;
            objFW.ZB_DEPT = this.ZB_DEPT.Text;
            objFW.MJ = this.MJ.SelectedValue;
            objFW.ZS_DEPT = this.ZS_DEPT.Text;
            objFW.CB_DEPT = this.CB_DEPT.Text;
            objFW.CS_DEPT = this.CS_DEPT.Text;
            objFW.REMARK1 = this.REMARK1.Text;
            objFW.SUBJECT_WORD = this.SUBJECT_WORD.Text;


            objFW.DRAFT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objFW.DRAFT_ID = LogInfo.UserInfo.ID;
            objFW.FW_DATE = DateTime.Now.ToString();

            objFW.FW_STATUS = "1";

            if (string.IsNullOrEmpty(this.hidTaskId.Value))
            {
                objFW.ID = GetSerialNumber("t_oa_FWInfoID");

                if (new TOaFwInfoLogic().Create(objFW))
                {
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                    //注册编号
                    wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //注册名称
                    wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "发文办理";
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

                if (new TOaFwInfoLogic().Edit(objFW))
                {
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                    //注册编号
                    wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //注册名称
                    wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "发文办理";
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

            string validateMsg = ValidateBusinessData();

            if (!string.IsNullOrEmpty(validateMsg))
            {
                LigerDialogAlert(validateMsg, "warn");
            }
            else
            {
                TOaFwInfoVo objFW = new TOaFwInfoVo();

                objFW.YWNO = this.YWNO.Text;
                objFW.FWNO = this.YWNO.Text;
                objFW.FW_TITLE = this.FW_TITLE.Text;
                objFW.ZB_DEPT = this.ZB_DEPT.Text;
                objFW.MJ = this.MJ.SelectedValue;
                objFW.ZS_DEPT = this.ZS_DEPT.Text;
                objFW.CB_DEPT = this.CB_DEPT.Text;
                objFW.CS_DEPT = this.CS_DEPT.Text;
                objFW.REMARK1 = this.REMARK1.Text;
                objFW.SUBJECT_WORD = this.SUBJECT_WORD.Text;

                objFW.FW_DATE = DateTime.Now.ToString();
                objFW.DRAFT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objFW.DRAFT_ID = LogInfo.UserInfo.ID;

                objFW.FW_STATUS = "0";

                if (string.IsNullOrEmpty(hidTaskId.Value))
                {
                    objFW.ID = GetSerialNumber("t_oa_FWInfoID");
                    this.hid_FwId.Value = objFW.ID;//发文ID
                    if (new TOaFwInfoLogic().Create(objFW))
                    {
                        //wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                        ////注册编号
                        //wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        ////注册名称
                        //wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "公文发放";

                        this.hidTaskId.Value = objFW.ID;
                    }
                    else
                    {
                        LigerDialogAlert("数据保存失败！", "error");
                        return;
                    }
                }
                else
                {
                    objFW.ID = this.hidTaskId.Value;
                    this.hid_FwId.Value = this.hidTaskId.Value;
                    if (new TOaFwInfoLogic().Edit(objFW))
                    {
                        //wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");

                        ////注册编号
                        //wfControl.ServiceCode = "FW" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        ////注册名称
                        //wfControl.ServiceName = "“" + this.FW_TITLE.Text + "”" + "公文发放";
                    }
                    else
                    {
                        LigerDialogAlert("数据保存失败！", "error");
                        return;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 附件上传前先做保存
        /// </summary>
        /// <returns></returns>
        /// <remarks>ok</remarks>
        [WebMethod]
        public static string SaveFWData(string strYWNO, string strFW_TITLE, string strZB_DEPT,
            string strDRAFT_ID, string strMJ, string strZS_DEPT, string strCB_DEPT, string strCS_DEPT, string strREMARK1, string strSUBJECT_WORD)
        {
            string strResult = "";

            TOaFwInfoVo objFW = new TOaFwInfoVo();

            objFW.YWNO = strYWNO;
            objFW.FWNO = strYWNO;
            objFW.FW_TITLE = strFW_TITLE;
            objFW.ZB_DEPT = strZB_DEPT;
            objFW.MJ = strMJ;
            objFW.ZS_DEPT = strZS_DEPT;
            objFW.CB_DEPT = strCB_DEPT;
            objFW.CS_DEPT = strCS_DEPT;
            objFW.REMARK1 = strREMARK1;
            objFW.SUBJECT_WORD = strSUBJECT_WORD.Replace("，", ",");


            objFW.DRAFT_ID = strDRAFT_ID;
            objFW.FW_DATE = DateTime.Now.ToString();
            objFW.DRAFT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            objFW.FW_STATUS = "0";

            objFW.ID = GetSerialNumber("t_oa_FWInfoID");
            if (new TOaFwInfoLogic().Create(objFW))
                strResult = objFW.ID;
            else
                strResult = "";

            return strResult;
        }
        //增加页面，打印功能，huangjinjun 20140509
        protected void btn_Print_Click(object sender, EventArgs e)
        {
            string fwID = this.hidTaskId.Value.Trim();
            //if (fwID == "")
            //    return;
            new ZZFWBase().FWExport(fwID);
        }
    }
}