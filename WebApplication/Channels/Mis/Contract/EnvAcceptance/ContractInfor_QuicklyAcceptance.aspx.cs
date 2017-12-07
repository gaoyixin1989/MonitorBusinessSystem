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

/// <summary>
/// 功能描述：验收监测委托书快速录入
/// 创建时间：2012-12-18
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Contract_EnvAcceptance_ContractInfor_QuicklyAcceptance : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strReturn = "";
        //委托书ID
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "changeStatus")
        {
            this.CONTRACT_ID.Value = Request.QueryString["strContratId"].ToString();
            strReturn = ChangeStatusBySend();
            Response.Write(strReturn);
            Response.End();
        }
        //监测年度
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetContratYear")
        {
            strReturn = getContractYear();
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
        //报告领取方式
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetDict")
        {
            strReturn = getReportType(Request.QueryString["dict_type"].ToString());
            Response.Write(strReturn);
            Response.End();
        }
        //获取监测实际费用
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractFee")
        {
            strReturn = getContractFee(Request.QueryString["contract_id"].ToString());
            Response.Write(strReturn);
            Response.End();
        }
        //验收委托类型
        this.Contract_Type.Value = new TSysDictLogic().Details(new TSysDictVo() { DICT_TYPE = "Contract_Type", DICT_TEXT = "验收监测" }).DICT_CODE;
        if (!IsPostBack)
        {
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
    /// <summary>
    /// 获取报告领取方式
    /// </summary>
    /// <returns></returns>
    protected string getReportType(string strDictType)
    {
        return i3.View.PageBase.getDictJsonString(strDictType);
    }
    /// <summary>
    /// 获取监测实际费用
    /// </summary>
    /// <param name="strContractFee">委托ID</param>
    /// <returns></returns>
    protected string getContractFee(string strContractFee)
    {
        return new TMisContractFeeLogic().Details(new TMisContractFeeVo() { CONTRACT_ID = strContractFee }).INCOME;
    }

    #endregion

    #region 发送时更改状态
    /// <summary>
    /// 发送时更改委托书提交状态
    /// </summary>
    protected string ChangeStatusBySend()
    {
        TMisContractVo objContractVo = new TMisContractVo();
        objContractVo.ID = this.CONTRACT_ID.Value;
        objContractVo.CONTRACT_STATUS = "9";
        new TMisContractLogic().Edit(objContractVo);

        return objContractVo.ID;
    }
    #endregion
}