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
/// 功能描述：验收监测委托书录入
/// 创建时间：2012-12-18
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Contract_EnvAcceptance_AcceptanceCreate : PageBaseForWF, IWFStepRules
{
    private string task_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
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
        //环评监测类型
        this.Contract_Type.Value = new TSysDictLogic().Details(new TSysDictVo() { DICT_TYPE = "Contract_Type", DICT_CODE = "20" }).DICT_CODE;
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

        //委托书信息
        objContractVo = new TMisContractLogic().Details(this.CONTRACT_ID.Value);
        wfControl.ServiceCode = objContractVo.CONTRACT_CODE;
        wfControl.ServiceName = objContractVo.PROJECT_NAME;

        return objContractVo.ID;
    }
    #endregion

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
        //ChangeStatusBySend();
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("验收委托", "task_id", this.CONTRACT_ID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }

    #endregion

    #region 委托书导出 胡方扬 2013-04-23
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();

        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.ID = task_id;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../TempFile/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        //插入委托书单号
        sheet.GetRow(2).GetCell(6).SetCellValue("No:" + dt.Rows[0]["CONTRACT_CODE"].ToString());

        sheet.GetRow(4).GetCell(2).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());
        sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["CONTACT_NAME"].ToString());
        sheet.GetRow(4).GetCell(8).SetCellValue(dt.Rows[0]["PHONE"].ToString());
        sheet.GetRow(5).GetCell(2).SetCellValue(dt.Rows[0]["CONTACT_ADDRESS"].ToString());
        sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["POST"].ToString());
        DataTable dtDict = PageBase.getDictList("RPT_WAY");
        DataTable dtSampleSource = PageBase.getDictList("SAMPLE_SOURCE");
        string strWay = "", strSampleWay = ""; ;
        if (dtDict != null)
        {
            foreach (DataRow dr in dtDict.Rows)
            {
                strWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_CODE"].ToString() == dt.Rows[0]["RPT_WAY"].ToString())
                {
                    strWay += "■ ";
                }
                else
                {
                    strWay += "□ ";
                }
            }
        }
        if (dtSampleSource != null)
        {
            foreach (DataRow dr in dtSampleSource.Rows)
            {
                strSampleWay += dr["DICT_TEXT"].ToString();
                if (dr["DICT_TEXT"].ToString() == dt.Rows[0]["SAMPLE_SOURCE"].ToString())
                {
                    strSampleWay += "■ ";
                }
                else
                {
                    strSampleWay += "□ ";
                }
            }
        }
        sheet.GetRow(5).GetCell(8).SetCellValue(strWay);
        sheet.GetRow(7).GetCell(2).SetCellValue(strSampleWay);
        sheet.GetRow(8).GetCell(2).SetCellValue(dt.Rows[0]["TEST_PURPOSE"].ToString());
        sheet.GetRow(9).GetCell(2).SetCellValue(dt.Rows[0]["PROVIDE_DATA"].ToString());
        sheet.GetRow(11).GetCell(2).SetCellValue(dt.Rows[0]["OTHER_ASKING"].ToString());
        sheet.GetRow(16).GetCell(1).SetCellValue(dt.Rows[0]["MONITOR_ACCORDING"].ToString());
        sheet.GetRow(20).GetCell(1).SetCellValue(dt.Rows[0]["REMARK2"].ToString());
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("委托监测协议书-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
    #endregion
}