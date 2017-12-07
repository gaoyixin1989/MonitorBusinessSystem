using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
using System.Configuration;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：验收监测委托书审批
/// 创建时间：2014-10-16
/// 创建人：weilin
/// </summary>
public partial class Channels_Mis_Contract_AcceptanceEx_AcceptanceAudit : PageBaseForWF, IWFStepRules
{
    private string task_id = "";
    private string strConfigFreqSetting = ConfigurationManager.AppSettings["FreqSetting"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
        string strReturn = "";
        //监测年度
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetContratYear")
        {
            strReturn = getContractYear();
            Response.Write(strReturn);
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetDict")
        {
            strReturn = getReportType(Request.QueryString["dict_type"].ToString());
            Response.Write(strReturn);
            Response.End();
        }
        //监测类型
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetMonitorType")
        {
            strReturn = getMonitorType();
            Response.Write(strReturn);
            Response.End();
        }
        //验收委托类型
        this.Contract_Type.Value = new TSysDictLogic().Details(new TSysDictVo() { DICT_TYPE = "Contract_Type", DICT_TEXT = "验收监测" }).DICT_CODE;
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 信息初始化
    /// <summary>
    /// 获取监测年度
    /// </summary>
    /// <returns></returns>
    protected string getContractYear()
    {
        string strResult = "";
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("YEAR", typeof(string)));
        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            if (i == 0)
            {
                dr["ID"] = DateTime.Now.ToString("yyyy");
                dr["YEAR"] = DateTime.Now.ToString("yyyy");
            }
            else
            {
                dr["ID"] = DateTime.Now.AddYears(+1).ToString("yyyy");
                dr["YEAR"] = DateTime.Now.AddYears(+1).ToString("yyyy");
            }
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        strResult = DataTableToJson(dt);
        return strResult;
    }
    protected string getReportType(string strDictType)
    {
        return i3.View.PageBase.getDictJsonString(strDictType);
    }
    /// <summary>
    /// 获取监测类型
    /// </summary>
    /// <returns></returns>
    protected string getMonitorType()
    {
        string strReturn = "";
        DataTable dt = new DataTable();
        TBaseMonitorTypeInfoVo objMonitor = new TBaseMonitorTypeInfoVo();
        objMonitor.IS_DEL = "0";
        dt = new TBaseMonitorTypeInfoLogic().SelectByTable(objMonitor);
        strReturn = DataTableToJson(dt);
        return strReturn;
    }
    #endregion


    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList != null)
            this.CONTRACT_ID.Value = myServiceList[0].SERVICE_KEY_VALUE;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        TMisContractVo objContractVo = new TMisContractLogic().Details(this.CONTRACT_ID.Value.Trim());
        //这里是产生和注册业务数据的地方
        if (this.CONTRACT_ID.Value.Length > 0 && this.hidBtnType.Value.Trim() == "send")
        {
            if (objContractVo.CONTRACT_STATUS == "3")
            { 
                //方案编制
                objContractVo.CONTRACT_STATUS = "4";
            }
            else if (objContractVo.CONTRACT_STATUS == "4")
            {
                //方案审核
                objContractVo.CONTRACT_STATUS = "5";
            }
            else if (objContractVo.CONTRACT_STATUS == "5")
            {
                //费用审核
                objContractVo.CONTRACT_STATUS = "9";
            }
            
            wfControl.DoContractTaskWF(this.CONTRACT_ID.Value, this.hidCompanyId.Value, this.hidBtnType.Value, strConfigFreqSetting);
            new TMisContractLogic().Edit(objContractVo);
        }
        else if (this.CONTRACT_ID.Value.Length > 0 && this.hidBtnType.Value.Trim() == "back")
        {
            if (objContractVo.CONTRACT_STATUS == "5")
            {
                //费用审核
                objContractVo.CONTRACT_STATUS = "4";
                
            }
            else if (objContractVo.CONTRACT_STATUS == "4")
            {
                //方案审核
                objContractVo.CONTRACT_STATUS = "3";
            }
            else if (objContractVo.CONTRACT_STATUS == "3")
            {
                //方案编制
                objContractVo.CONTRACT_STATUS = "2";
            }
            
            wfControl.DoContractTaskWF(this.CONTRACT_ID.Value, this.hidCompanyId.Value, this.hidBtnType.Value, strConfigFreqSetting);
            new TMisContractLogic().Edit(objContractVo);
        }

    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
    }

}