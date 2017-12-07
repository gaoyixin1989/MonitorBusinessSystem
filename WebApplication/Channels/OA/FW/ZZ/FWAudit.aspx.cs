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
namespace n8
{
    /// <summary>
    /// 功能描述：发文核稿
    /// 创建日期：2013-6-26
    /// 创建人  ：李焕明
    /// </summary>
    public partial class Channels_OA_FW_QHD_FWAudit : ZZFWBase, IWFStepRules
    {
        private string strBtnType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = this.Request.Url.ToString();
            if (!string.IsNullOrEmpty(this.Request["fw_id"]))
            {
                this.hidTaskId.Value = this.Request["fw_id"].ToString();
            }

            if (!string.IsNullOrEmpty(this.Request["task_tatus"]))
            {
                this.hidTask_Tatus.Value = this.Request["task_tatus"].ToString();
            }

            if (!string.IsNullOrEmpty(this.Request["op"]))
            {
                this.hidBtnOp.Value = this.Request["op"].ToString();
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
            this.FW_TITLE.Text = objFW.FW_TITLE;
            this.ZB_DEPT.Text = objFW.ZB_DEPT;
            this.DRAFT_NAME.Text = new TSysUserLogic().Details(objFW.DRAFT_ID).REAL_NAME;
            this.MJ.Text = getDictName(objFW.MJ, "FW_MJ");
            this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToShortDateString();
            this.SUBJECT_WORD.Text = objFW.SUBJECT_WORD;
            this.Remark1.Text = objFW.REMARK1;
            this.CB_DEPT.Text = objFW.CB_DEPT;
            this.CS_DEPT.Text = objFW.CS_DEPT;
            this.ZS_DEPT.Text = objFW.ZS_DEPT;

            if (this.hidBtnOp.Value == "zrhg")
            {
                this.ISSUE_INFO.Disabled = true;
                this.CTS_INFO.Disabled = true;
                this.YWNO.Enabled = false;
            }
            else if (this.hidBtnOp.Value == "bgssh")
            {
                SetAPPInfo(objFW);
                this.APP_INFO.Disabled = true;
                this.ISSUE_INFO.Disabled = true;
                this.YWNO.Enabled = false;
            }
            else if (this.hidBtnOp.Value == "fgldqf")
            {
                SetAPPInfo(objFW);
                SetCTSInfo(objFW);
                this.APP_INFO.Disabled = true;
                this.CTS_INFO.Disabled = true;
                this.YWNO.Enabled = false;
            }
            else if (this.hidBtnOp.Value == "fwgd")
            {
                SetAPPInfo(objFW);
                SetCTSInfo(objFW);
                SetISSUEInfo(objFW);
                this.APP_INFO.Disabled = true;
                this.CTS_INFO.Disabled = true;
                this.ISSUE_INFO.Disabled = true;
                this.YWNO.Enabled = true;
            }
            else
            {
                SetAPPInfo(objFW);
                SetCTSInfo(objFW);
                SetISSUEInfo(objFW);

                this.APP_INFO.Disabled = true;
                this.CTS_INFO.Disabled = true;
                this.ISSUE_INFO.Disabled = true;
            }
        }

        private void SetISSUEInfo(TOaFwInfoVo objFW)
        {
            this.ISSUE_INFO.Value = objFW.ISSUE_INFO;
            this.ISSUE_ID.Text = new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME;
            this.ISSUE_DATE.Text = string.IsNullOrEmpty(objFW.ISSUE_DATE) ? "" : DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString();
        }

        private void SetCTSInfo(TOaFwInfoVo objFW)
        {
            this.CTS_INFO.Value = objFW.CTS_INFO;
            this.CTS_ID.Text = new TSysUserLogic().Details(objFW.CTS_ID).REAL_NAME;
            this.CTS_DATE.Text = string.IsNullOrEmpty(objFW.CTS_DATE) ? "" : DateTime.Parse(objFW.CTS_DATE).ToShortDateString();
        }

        private void SetAPPInfo(TOaFwInfoVo objFW)
        {
            this.APP_INFO.Value = objFW.APP_INFO;
            this.APP_ID.Text = new TSysUserLogic().Details(objFW.APP_ID).REAL_NAME;
            this.APP_DATE.Text = string.IsNullOrEmpty(objFW.APP_DATE) ? "" : DateTime.Parse(objFW.APP_DATE).ToShortDateString();
        }

        #region 工作流
        void IWFStepRules.LoadAndViewBusinessData()
        {
            //这里是载入和显示业务数据的地方，InitWFDict由启动
            List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
            this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;

            this.hidTask_Tatus.Value = new TOaFwInfoLogic().Details(this.hidTaskId.Value).FW_STATUS;
        }

        bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
        {
            //这里是验证组件和业务数据的地方，由发送启动

            strMsg = "";
            return true;

        }

        private string ValidateBusinessData()
        {
            string validateMsg = "";

            if (string.IsNullOrEmpty(YWNO.Text))
            {
                validateMsg += "请填写编号！<br />";
            }
            return validateMsg;
        }

        void IWFStepRules.CreatAndRegisterBusinessData()
        {
            string validateMsg = ValidateBusinessData();
            if (this.hidTaskId.Value.Length > 0 && String.IsNullOrEmpty(strBtnType))
            {
                //这里是产生和注册业务数据的地方，由发送启动，在BuildAndValidateBusinessData之后
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
                objFW.ID = this.hidTaskId.Value;

                if (this.hidBtnOp.Value == "zrhg")//办公室审核
                {
                    objFW.APP_INFO = this.APP_INFO.Value;
                    objFW.APP_ID = LogInfo.UserInfo.ID;
                    objFW.APP_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "2";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "2");
                }
                else if (this.hidBtnOp.Value == "bgssh")//分管领导审核
                {
                    objFW.CTS_INFO = this.CTS_INFO.Value;
                    objFW.CTS_ID = LogInfo.UserInfo.ID;
                    objFW.CTS_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "3";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "3");
                }
                else if (this.hidBtnOp.Value == "fgldqf")//书记审核
                {
                    objFW.ISSUE_INFO = this.ISSUE_INFO.Value;
                    objFW.ISSUE_ID = LogInfo.UserInfo.ID;
                    objFW.ISSUE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "4";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "4");
                }
                else if (this.hidBtnOp.Value == "fwdj")//del
                {
                    objFW.FW_STATUS = "8";

                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "8");
                }
                else if (this.hidBtnOp.Value == "fwgd")//发布、归档
                {
                    if (!string.IsNullOrEmpty(validateMsg))//发文编号的校验
                    {
                        LigerDialogAlert(validateMsg, "warn");
                    }
                    else
                    {
                        objFW.PIGONHOLE_ID = LogInfo.UserInfo.ID;
                        objFW.PIGONHOLE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        objFW.YWNO = YWNO.Text;
                        objFW.FWNO = YWNO.Text;
                        objFW.FW_STATUS = "9";
                        //wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "9");
                    }
                }

                new TOaFwInfoLogic().Edit(objFW);
            }
            else if (this.hidTaskId.Value.Length > 0 && strBtnType == "back")
            {
                //这里是产生和注册业务数据的地方
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
                objFW.ID = this.hidTaskId.Value;

                if (this.hidBtnOp.Value == "zrhg")//办公室审核
                {
                    objFW.APP_INFO = this.APP_INFO.Value;
                    objFW.APP_ID = LogInfo.UserInfo.ID;
                    objFW.APP_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "0";//不能设回0，否则再发送就会产生两个工作流
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "0");
                }
                else if (this.hidBtnOp.Value == "bgssh")//分管领导审核
                {
                    objFW.CTS_INFO = this.CTS_INFO.Value;
                    objFW.CTS_ID = LogInfo.UserInfo.ID;
                    objFW.CTS_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "1";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");
                }
                else if (this.hidBtnOp.Value == "fgldqf")//书记审核
                {
                    objFW.ISSUE_INFO = this.ISSUE_INFO.Value;
                    objFW.ISSUE_ID = LogInfo.UserInfo.ID;
                    objFW.ISSUE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "2";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "2");
                }
                else if (this.hidBtnOp.Value == "fwdj")//del
                {
                    objFW.FW_STATUS = "1";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "6");
                }
                else if (this.hidBtnOp.Value == "fwgd")//发布、归档
                {
                    if (!string.IsNullOrEmpty(validateMsg))//发文编号的校验
                    {
                        LigerDialogAlert(validateMsg, "warn");
                    }
                    else
                    {
                        objFW.FW_STATUS = "3";
                        wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "3");
                    }
                }
                new TOaFwInfoLogic().Edit(objFW);
            }
        }

        void IWFStepRules.SaveBusinessDataFromPageControl()
        {
            //这里是执行业务数据保存的地方，此处由工作流控件间接调用，由保存启动
        }

        #endregion

        /// <summary>
        /// 导出、打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            FWExport(this.hidTaskId.Value);
        }
        protected void btn_Print_Click(object sender, EventArgs e)
        {
            string fwID = this.hidTaskId.Value.Trim();
            if (fwID == "")
                return;
            new ZZFWBase().FWExport(fwID);
        }
    }
}