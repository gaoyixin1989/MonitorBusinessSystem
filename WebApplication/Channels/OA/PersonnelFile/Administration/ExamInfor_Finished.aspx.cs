﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Channels.OA.EXAMINE;
using i3.BusinessLogic.Channels.OA.EXAMINE;

public partial class Channels_OA_PersonnelFile_Administration_ExamInfor_Finished : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strLength = "", strTaskProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
        //获取隐藏域参数
        GetHiddenParme();
        if (!String.IsNullOrEmpty(task_id) && !String.IsNullOrEmpty(strTaskProjectName))
        {
            ////注册编号
            //wfControl.ServiceCode = "";
            ////注册名称
            //wfControl.ServiceName = strTaskProjectName;
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strLength = this.hidLength.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
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
        if (!String.IsNullOrEmpty(task_id))
        {
            TOaExamineInfoVo objItems = new TOaExamineInfoVo();
            objItems.ID = task_id;
            objItems.EXAMINE_STATUS = "9";
            objItems.SUPERIOR_APP = this.hidAduitOption.Value.ToString();
            objItems.OPINION = this.hidPersonOption.Value.ToString();
            objItems.APPEAL = this.hidCheckOption.Value.ToString();
            objItems.SUPERIOR_APP_DATE = DateTime.Now.ToString();
            objItems.OPINION_DATE = DateTime.Now.ToString();
            if (new TOaExamineInfoLogic().Edit(objItems))
            {
                wfControl.SaveInstStepServiceData("事业单位人员人事考核ID(主管单位审核)", "task_id", task_id, "4");
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
}