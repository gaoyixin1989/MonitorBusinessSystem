using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor;
using i3.View;
using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using WebApplication;

/// <summary>
/// 功能： "综合查询--委托书查询"功能
/// 创建人：潘德军
/// 创建时间： 2013.7.1
/// </summary>
public partial class Channels_Base_Search_SearchZZ_ContractSrh : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "SelectContract")
        {
            SelectContract();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectTask")
        {
            selectTask();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectsubTask")
        {
            selectsubTask();
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    protected void SelectContract()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //未办、已办状态 0：未办，1：已办
        string strStatus = !string.IsNullOrEmpty(Request.QueryString["Status"]) ? Request.QueryString["Status"].ToString() : "";
        //委托年度
        string strYear = !string.IsNullOrEmpty(Request.QueryString["SrhYear"]) ? Request.QueryString["SrhYear"].ToString() : "";
        //委托类型
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["SrhContractType"]) ? Request.QueryString["SrhContractType"].ToString() : "";
        //合同号
        string strContractCode = !string.IsNullOrEmpty(Request.QueryString["SrhContractCode"]) ? Request.QueryString["SrhContractCode"].ToString() : "";
        //任务单号
        string strDutyCode = !string.IsNullOrEmpty(Request.QueryString["DutyCode"]) ? Request.QueryString["DutyCode"].ToString() : "";
        //报告号
        string strReportCode = !string.IsNullOrEmpty(Request.QueryString["ReportCode"]) ? Request.QueryString["ReportCode"].ToString() : "";
        //委托客户
        string strClientName = !string.IsNullOrEmpty(Request.QueryString["ClientName"]) ? Request.QueryString["ClientName"].ToString() : "";
        //合同类别
        string strItemType = !string.IsNullOrEmpty(Request.QueryString["ItemType"]) ? Request.QueryString["ItemType"].ToString() : "";
        //项目名称
        string strProjectName = !string.IsNullOrEmpty(Request.QueryString["SrhProjectName"]) ? Request.QueryString["SrhProjectName"].ToString() : "";

        //构造查询对象
        TMisContractVo objContract = new TMisContractVo();
        TMisContractLogic objContractLogic = new TMisContractLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisContractVo.CONTRACT_CODE_FIELD;

        //objContract.SORT_FIELD = strSortname;
        //objContract.SORT_TYPE = strSortorder;
        objContract.SORT_FIELD = "ID";
        objContract.SORT_TYPE = "desc";
        objContract.CONTRACT_YEAR = strYear;
        objContract.CONTRACT_TYPE = strContractType;
        objContract.CONTRACT_CODE = strContractCode;
        objContract.CLIENT_COMPANY_ID = strClientName;
        objContract.TEST_TYPE = strItemType;
        objContract.PROJECT_NAME = strProjectName;

        //int intTotalCount = objContractLogic.GetSelectResultCountEx(objContract, strStatus);//总计的数据条数
        //DataTable dt = objContractLogic.SelectByTableEx(objContract, strStatus, intPageIdx, intPagesize);
        
        //string strJson = CreateToJson(dt, intTotalCount);


        string strJson = "";
        //int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objContractLogic.SelectByTableEx(objContract, strStatus, 0, 0);
        int intTotalCount = dt.Rows.Count;
        var ccflowDt = CCFlowFacade.GetCCFLowStatus(this.LogInfo.UserInfo.USER_NAME, dt, strStatus, intPageIdx, intPagesize, out intTotalCount);

        strJson = CreateToJson(ccflowDt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取监测任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        //任务类型
        string strContractType = !string.IsNullOrEmpty(Request.QueryString["SrhContractType"]) ? Request.QueryString["SrhContractType"].ToString() : "";
        objTask.CONTRACT_TYPE = strContractType;
        objTask.SORT_FIELD = strSortname;
        objTask.SORT_TYPE = strSortorder;
        objTask.CONTRACT_ID = !string.IsNullOrEmpty(Request.QueryString["contract_id"]) ? Request.QueryString["contract_id"].ToString() : "";

        string strJson = "";
        //int intTotalCount = objTaskLogic.GetSelectResultCount(objTask);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable(objTask, 0, 0);

        strJson = CreateToJson(dt, dt.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取监测子任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectsubTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskLogic objSubTaskLogic = new TMisMonitorSubtaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorSubtaskVo.ID_FIELD;

        objSubTask.SORT_FIELD = strSortname;
        objSubTask.SORT_TYPE = strSortorder;
        objSubTask.TASK_ID = !string.IsNullOrEmpty(Request.QueryString["task_id"]) ? Request.QueryString["task_id"].ToString() : "";

        string strJson = "";
        //int intTotalCount = objSubTaskLogic.GetSelectResultCount(objSubTask);//总计的数据条数
        DataTable dt = objSubTaskLogic.SelectByTable(objSubTask, 0, 0);
        DataTable dtRe = doWithSubtaskData(dt);

        strJson = CreateToJson(dtRe, dtRe.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    private DataTable doWithSubtaskData(DataTable dt)
    {
        DataTable dtRe = new DataTable();
        string strMonitorIDs = "";
        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dtRe.Columns.Add(dt.Columns[j].ColumnName);
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strMonitorIDs.Contains(dt.Rows[i]["MONITOR_ID"].ToString()))
            {
                strMonitorIDs += "," + dt.Rows[i]["MONITOR_ID"].ToString();
                DataRow dr = dtRe.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[dt.Columns[j].ColumnName] = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                }
                dtRe.Rows.Add(dr);
            }
        }
        for (int i = 0; i < dtRe.Rows.Count; i++)
        {
            if (dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
            {
                string strUsername = new TSysUserLogic().Details(dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
                if (!dtRe.Rows[i]["SAMPLING_MAN"].ToString().Contains(strUsername))
                    dtRe.Rows[i]["SAMPLING_MAN"] = strUsername + (dtRe.Rows[i]["SAMPLING_MAN"].ToString().Length > 0 ? "，" : "") + dtRe.Rows[i]["SAMPLING_MAN"].ToString();
            }
        }
        dtRe.AcceptChanges();

        return dtRe;
    }

    /// <summary>
    /// 导出、打印委托信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// huangjinjun 20140509
    protected void btnExport_Click(object sender, EventArgs e)
    {
        var hidid = this.hiddid.Value;
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();

        if (!String.IsNullOrEmpty(hidid))
        {
            objItems.ID = hidid;
        }
        dt = new TMisContractLogic().GetExportInforData(objItems);
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../../Mis/Contract/TempFile/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
        //FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/ContractInforSheet.xls"), FileMode.Open, FileAccess.Read);
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

}