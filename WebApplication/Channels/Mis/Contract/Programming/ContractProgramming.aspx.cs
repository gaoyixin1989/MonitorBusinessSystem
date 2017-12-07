using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;
using System.Configuration;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Data;
/// 委托书流程---委托编制
/// 创建时间：2012-12-20
/// 创建人：胡方扬
public partial class Channels_Mis_Contract_Programming_ContractProgramming : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strBtnType = "", strCompanyId = "", strQCStep = "",strQcList="";
    private string strConfigFreqSetting = ConfigurationManager.AppSettings["FreqSetting"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        this.hidleader.Value = LogInfo.UserInfo.ID.ToString();
        strQCStep = this.hidQcStep.Value.ToString();
        strBtnType = this.hidBtnType.Value.ToString();
        strCompanyId = this.hidCompanyId.Value.ToString();
        strQcList = this.hidQcList.Value.ToString();
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
        if (!String.IsNullOrEmpty(task_id)) {
            new TMisContractLogic().Edit(new TMisContractVo { ID = task_id, PROJECT_ID = LogInfo.UserInfo.ID, QC_STEP = strQCStep,QCRULE=strQcList });
        }
        wfControl.DoContractTaskWF(task_id, strCompanyId, strBtnType, strConfigFreqSetting);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

    #region 原来的工作流处理模式，现在统一到工作流控件中，不用每个页面都写一堆代码 胡方扬 2013-03-28
    ///// <summary>
    ///// 获取委托书监测点位
    ///// </summary>
    ///// <returns></returns>
    //private DataTable GetMonitorPoint()
    //{
    //    DataTable dt = new DataTable();
    //    TMisContractPointVo objItems = new TMisContractPointVo();
    //    objItems.CONTRACT_ID = task_id;

    //    dt = new TMisContractPointLogic().SelectByTable(objItems);

    //    return dt;
    //}
    ///// <summary>
    ///// 插入监测计划信息
    ///// </summary>
    ///// <returns></returns>
    //private bool CreateContractPlan()
    //{
    //    bool flag = false;
    //    DataTable dt = new DataTable();
    //    TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
    //    objItems.CONTRACT_ID = task_id;
    //    //获取当前委托书监测点位
    //    dt = GetMonitorPoint();
    //    if (dt != null )
    //    {
    //        if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, task_id))
    //        {
    //            string strMessage = LogInfo.UserInfo.USER_NAME + "采样预约计划点位、采样频次生成成功";
    //            WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanFreqInfo, "", strMessage);
    //            //如果为清远，则暂时使用原来方式
    //            if (!String.IsNullOrEmpty(strConfigFreqSetting) && strConfigFreqSetting == "1")
    //            {
    //                flag = true;
    //            }
    //            else
    //            {
    //                if (SavePlanInfor())
    //                {
    //                    flag = true;
    //                }
    //            }
    //        }
    //    }
    //    return flag;
    //}

    ///// <summary>
    ///// 保存监测预约计划 胡方扬 2013-03-27
    ///// </summary>
    ///// <returns></returns>
    //private bool SavePlanInfor()
    //{
    //    bool flag = false;
    //    DataTable dt = new DataTable();
    //    TMisContractPlanVo objItems = new TMisContractPlanVo();
    //    if (!String.IsNullOrEmpty(task_id))
    //    {
    //        objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
    //        objItems.CONTRACT_ID = task_id;
    //        dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);
    //        objItems.CONTRACT_COMPANY_ID = strCompanyId;

    //        if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && IsNumeric(dt.Rows[0]["NUM"].ToString()))
    //        {
    //            objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
    //        }
    //        else
    //        {
    //            objItems.PLAN_NUM = "1";
    //        }
    //        if (new TMisContractPlanLogic().Create(objItems))
    //        {
    //            string strMessage = LogInfo.UserInfo.USER_NAME + "新增采样预约计划" + objItems.ID + "成功";
    //            WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanInfo, "", strMessage);
    //            if (SavePlanPoint(objItems.ID)) 
    //            {
    //                flag = true;
    //            }
    //        }
    //    }
    //    return flag;
    //}

    ///// <summary>
    ///// 插入监测任务预约点位表信息  胡方扬 2013-03-27
    ///// </summary>
    ///// <returns></returns>
    //private bool SavePlanPoint(string strPlanId)
    //{
    //    bool flag = false;
    //    if (new TMisContractPlanPointLogic().SavePlanPoint(task_id,strPlanId))
    //    {
    //        string strMessage = LogInfo.UserInfo.USER_NAME + "新增采样预约计划" + strPlanId + "点位成功";
    //        WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractPlanPointInfo, "", strMessage);
    //        flag = true;
    //    }
    //    return flag;
    //}

    ///// <summary>
    ///// 保存项目负责人
    ///// </summary>
    ///// <returns></returns>
    //private bool SavePlanPeople() {
    //    return true;
    //}
    #endregion
}