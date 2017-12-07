using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Common;
using System.Data;
using i3.ValueObject.Sys.WF;

/// <summary>
/// 功能描述：环境质量数据填报流程
/// 创建日期：2013-8-26
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Fill_FillFlow : PageBaseForWF,IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["pf_id"] != null && Request["pf_id"].ToString().Length > 0)
            {
                this.hidID.Value = Request["pf_id"].ToString();
            }
            wfControl.InitWFDict();
            InitPage();
        }
    }

    //页面初始化
    private void InitPage()
    {
        if (this.hidID.Value.Trim().Length > 0)
        {
            DataTable dt = new CommonLogic().GetPointFillInfo(this.hidID.Value.Trim(), '^');

            if (dt.Rows.Count > 0)
            {
                this.PF_TITLE.Text = dt.Rows[0]["TITLE"].ToString();
                this.PF_YEAR.Text = dt.Rows[0]["YEAR"].ToString();
                this.PF_MONTH.Text = dt.Rows[0]["MONTH"].ToString();
                this.hidStatus.Value = dt.Rows[0]["STATUS"].ToString();
                this.hidTableName.Value = dt.Rows[0]["TableName"].ToString();
            }
        }
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList != null)
            this.hidID.Value = myServiceList[0].SERVICE_KEY_VALUE;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方

        strMsg = "";
        return true;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        if (this.hidID.Value.Length > 0)
        {
            string Status = "";
            //这里是产生和注册业务数据的地方
            if (this.hidStatus.Value.Trim() == "" || this.hidStatus.Value.Trim() == "0")   //未提交
            {
                Status = "1";
                wfControl.SaveInstStepServiceData("数据填报ID", "pf_id", this.hidID.Value.Trim(), "1");

                //注册编号
                wfControl.ServiceCode = "PF" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                wfControl.ServiceName = "“" + this.PF_TITLE.Text + "”" + "审批签发";
            }
            else if (this.hidStatus.Value.Trim() == "1")                            //待审核
            {
                Status = "2";
                wfControl.SaveInstStepServiceData("数据填报ID", "pf_id", this.hidID.Value.Trim(), "2");
            }
            else if (this.hidStatus.Value.Trim() == "2")                            //待签发
            {
                Status = "9";
                wfControl.SaveInstStepServiceData("数据填报ID", "pf_id", this.hidID.Value.Trim(), "3");
            }
            else if (this.hidStatus.Value.Trim() == "3")                           //待归档
            {
                Status = "9";
            }
            new CommonLogic().UpdateFillStatus(this.hidID.Value.Trim(), this.hidTableName.Value.Trim(), Status);
        }
        
        
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion
}