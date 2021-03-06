﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;

public partial class Channels_OA_Part_QHD_PartOfficeAdd : PageBaseForWF, IWFStepRules
{
    private string task_id = "",  strTaskProjectName = "";
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
            wfControl.ServiceCode = "BG" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //注册名称
            wfControl.ServiceName = strTaskProjectName+"办公用品申请单";
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
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

        if (task_id == "")
            strMsg = "请添加申请的用品";

        return strMsg == "" ? true : false;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //这里是产生和注册业务数据的地方
        if (!String.IsNullOrEmpty(task_id))
        {
            TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
            objItems.ID = task_id;
            objItems.STATUS = "1";
            objItems.APPLY_TYPE = "03";
            if (new TOaPartBuyRequstLogic().Edit(objItems))
            {
                wfControl.SaveInstStepServiceData("申请计划ID", "task_id", task_id, "1");
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