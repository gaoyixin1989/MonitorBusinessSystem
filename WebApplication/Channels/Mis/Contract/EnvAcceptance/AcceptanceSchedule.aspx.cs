using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.WF;
using System.Collections;

/// <summary>
/// 功能描述：验收方案编制
/// 创建时间：2012-12-19
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Contract_EnvAcceptance_AcceptanceSchedule : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        //获取委托书ID
        if (!string.IsNullOrEmpty(Request.QueryString["contract_id"]))
        {
            this.hdnContracID.Value = Request.QueryString["contract_id"].ToString();
        }
        //获取委托书信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
        {
            strResult = GetContractInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取委托企业信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getClientCompanyInfo")
        {
            strResult = GetClientCompanyInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取受检企业信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTestedCompanyInfo")
        {
            strResult = GetTestedCompanyInfo();
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 获取基础信息
    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns></returns>
    protected string GetContractInfo()
    {
        TMisContractVo objContractInfo = new TMisContractVo();
        objContractInfo.ID = this.hdnContracID.Value;
        objContractInfo.CONTRACT_STATUS = "9";
        objContractInfo = new TMisContractLogic().Details(objContractInfo);
        //签订日期 格式
        try
        {
            objContractInfo.ASKING_DATE = DateTime.Parse(objContractInfo.ASKING_DATE).ToString("yyyy-MM-dd");
        }
        catch { }
        //备注信息组装
        string strRemark = "";
        //同意分包
        if (objContractInfo.AGREE_OUTSOURCING == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_subpackage", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意监测方法
        if (objContractInfo.AGREE_METHOD == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_useMonitorMethod", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意使用非标准方法
        if (objContractInfo.AGREE_NONSTANDARD == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_usenonstandard", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        //同意其他
        if (objContractInfo.AGREE_OTHER == "1")
        {
            string strDictText = new TSysDictLogic().GetDictNameByDictCodeAndType("accept_other", "Contract_Remarks");
            strRemark += strDictText.Length > 0 ? strDictText + ";" : "";
        }
        if (strRemark.Length > 0)
        {
            strRemark = strRemark.Remove(strRemark.LastIndexOf(";"));
        }
        objContractInfo.REMARK1 = strRemark;
        //添加监测类型
        string strMonitorType = "";//合同类型字符串
        if (objContractInfo.TEST_TYPES.Length > 0)//所有监测类型
        {
            string[] strTestType = objContractInfo.TEST_TYPES.Split(';');
            foreach (string str in strTestType)
            {
                if (str.Length > 0)
                {
                    //监测类别名称
                    string strTypeName = new TBaseMonitorTypeInfoLogic().Details(str).MONITOR_TYPE_NAME;
                    strMonitorType += strTypeName.Length > 0 ? strTypeName + "," : "";
                }
            }
        }
        strMonitorType = strMonitorType.Length > 0 ? strMonitorType.Remove(strMonitorType.LastIndexOf(",")) : "";
        objContractInfo.TEST_TYPES = strMonitorType;
        return ToJson(objContractInfo);
    }
    /// <summary>
    /// 获得委托企业信息
    /// </summary>
    /// <returns></returns>
    protected string GetClientCompanyInfo()
    {
        //委托书信息
        TMisContractVo objContractInfo = new TMisContractVo();
        objContractInfo.ID = this.hdnContracID.Value;
        objContractInfo.CONTRACT_STATUS = "9";
        objContractInfo = new TMisContractLogic().Details(objContractInfo);
        //委托企业
        TMisContractCompanyVo objClientInfo = new TMisContractCompanyVo();
        objClientInfo.ID = objContractInfo.CLIENT_COMPANY_ID;
        objClientInfo.IS_DEL = "0";
        objClientInfo = new TMisContractCompanyLogic().Details(objClientInfo);
        return ToJson(objClientInfo);
    }
    /// <summary>
    /// 获得受检企业信息
    /// </summary>
    /// <returns></returns>
    protected string GetTestedCompanyInfo()
    {
        //委托书信息
        TMisContractVo objContractInfo = new TMisContractVo();
        objContractInfo.ID = this.hdnContracID.Value;
        objContractInfo.CONTRACT_STATUS = "9";
        objContractInfo = new TMisContractLogic().Details(objContractInfo);
        //受检企业
        TMisContractCompanyVo objTestedInfo = new TMisContractCompanyVo();
        objTestedInfo.ID = objContractInfo.TESTED_COMPANY_ID;
        objTestedInfo.IS_DEL = "0";
        objTestedInfo = new TMisContractCompanyLogic().Details(objTestedInfo);
        return ToJson(objTestedInfo);
    }

    #endregion

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        //传递参数
        this.hdnContracID.Value = myServiceList.Count > 0 ? myServiceList[0].SERVICE_KEY_VALUE : "";
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
        string task_id = this.hdnContracID.Value.ToString();
        if (!String.IsNullOrEmpty(task_id)) {
            if (new TMisContractLogic().Edit(new TMisContractVo { ID = task_id, PROJECT_ID = LogInfo.UserInfo.REAL_NAME }))
            {
                wfControl.SaveInstStepServiceData("验收委托", "task_id", this.hdnContracID.Value);
            } 

        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

}