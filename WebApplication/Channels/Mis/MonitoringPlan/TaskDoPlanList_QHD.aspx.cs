using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;

public partial class Channels_Mis_MonitoringPlan_TaskDoPlanList_QHD : PageBase
{
    public string task_id = "", strPlanId = "", strWorkTask_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
    }

    private void GetHidParam()
    {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
        strWorkTask_id = this.hidWorkTaskId.Value.ToString();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (strPlanId.Length == 0)
            return;

        TMisContractPlanVo objPlan = new TMisContractPlanLogic().Details(strPlanId);
        string strContractID = objPlan.CONTRACT_ID;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.PLAN_ID = hidPlanId.Value.Trim();
        objTaskVo = new TMisMonitorTaskLogic().Details(objTaskVo);
        TMisMonitorTaskCompanyVo objTaskCompanyVo = new TMisMonitorTaskCompanyLogic().Details(objTaskVo.TESTED_COMPANY_ID);

        string strContractType = getDictName(objTaskVo.CONTRACT_TYPE, "Contract_Type");

        string strMonitorNames = "";
        string strPointNames = "";
        string strItemS = "";
        string strFREQ = "";
        GetInfoForPrint(ref strMonitorNames, ref strPointNames, ref strFREQ, ref strItemS);

        string strSAMPLE_ASK_DATE = DateTime.Now.ToString("yyyy-MM-dd");
        string strSAMPLE_FINISH_DATE = DateTime.Parse(objTaskVo.ASKING_DATE).ToString("yyyy-MM-dd");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../Monitor/sampling/QHD/template/MoniterTaskNotify.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");

        sheet.GetRow(3).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(4).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(5).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(3).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(4).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(5).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(7).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(7).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(7).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(7).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(8).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(8).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);
        //复制一份
        sheet.GetRow(16).GetCell(3).SetCellValue("编号：" + objTaskVo.TICKET_NUM);
        sheet.GetRow(17).GetCell(1).SetCellValue(strContractType);
        sheet.GetRow(18).GetCell(1).SetCellValue(objTaskVo.PROJECT_NAME);
        sheet.GetRow(16).GetCell(0).SetCellValue(objTaskVo.STATE == "01" ? "现场室：" : "分析室：");
        sheet.GetRow(17).GetCell(3).SetCellValue(objTaskCompanyVo.COMPANY_NAME);
        sheet.GetRow(18).GetCell(3).SetCellValue(objTaskCompanyVo.CONTACT_NAME + " " + objTaskCompanyVo.PHONE + " " + objTaskCompanyVo.CONTACT_ADDRESS);
        sheet.GetRow(20).GetCell(0).SetCellValue(strMonitorNames);
        sheet.GetRow(20).GetCell(1).SetCellValue(strPointNames);
        sheet.GetRow(20).GetCell(2).SetCellValue(strFREQ);
        sheet.GetRow(20).GetCell(3).SetCellValue(strItemS);
        sheet.GetRow(21).GetCell(1).SetCellValue(strSAMPLE_ASK_DATE);
        sheet.GetRow(21).GetCell(3).SetCellValue(strSAMPLE_FINISH_DATE);

        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("任务通知单.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void GetInfoForPrint(ref string strMonitorNames, ref string strPointNames, ref string strFREQ, ref string strItemS)
    {
        if (this.hidPlanId.Value.Length == 0)
            return;

        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.PLAN_ID = this.hidPlanId.Value;
        objTask = new TMisMonitorTaskLogic().Details(objTask);

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        objSubtask.TASK_ID = objTask.ID;

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objSubtask, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseMonitorTypeInfoVo objMonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(dt.Rows[i]["MONITOR_ID"].ToString());
            strMonitorNames += objMonitorTypeInfoVo.MONITOR_TYPE_NAME + "\n";
            GetPoint_UnderTask(objTask.CONTACT_ID, dt.Rows[i]["ID"].ToString(), ref strPointNames, ref strFREQ, ref strItemS);
        }
    }

    //点位
    private void GetPoint_UnderTask(string strContractID, string strSubTaskID, ref string strPointNames, ref string strFREQ, ref string strItemS)
    {
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);

        string strtmpPointNameS = "";
        string strtmpItems = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(dt.Rows[i]["POINT_ID"].ToString());
            if (!strtmpPointNameS.Contains(objTaskPoint.POINT_NAME))
            {
                strtmpPointNameS += (strtmpPointNameS.Length > 0 ? "、" : "") + objTaskPoint.POINT_NAME;

                GetItem_UnderSample(dt.Rows[i]["ID"].ToString(), objTaskPoint.POINT_NAME, ref strtmpItems);

                TMisContractPointVo objContractPointVo = new TMisContractPointVo();
                objContractPointVo = new TMisContractPointLogic().Details(objTaskPoint.CONTRACT_POINT_ID);
                strFREQ += (strFREQ.Length > 0 ? "\n" : "") + objTaskPoint.POINT_NAME + "：连续" + (objContractPointVo.SAMPLE_DAY == "" ? "1" : objContractPointVo.SAMPLE_DAY) + "天，每天" + (objContractPointVo.SAMPLE_FREQ == "" ? "1" : objContractPointVo.SAMPLE_FREQ) + "次";
            }

        }

        strItemS += (strItemS.Length > 0 ? "\n" : "") + strtmpItems;
        strPointNames += (strPointNames.Length > 0 ? "\r\n" : "") + strtmpPointNameS;

    }

    //项目
    private void GetItem_UnderSample(string strSampleID, string strPointName, ref string strItems)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.SAMPLE_ID = strSampleID;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, 0, 0);

        strItems += (strItems.Length > 0 ? "\n" : "") + strPointName + "：";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseItemInfoVo objItemInfo = new TBaseItemInfoLogic().Details(dt.Rows[i]["ITEM_ID"].ToString());

            strItems += objItemInfo.ITEM_NAME + "、";

        }
        strItems = strItems.TrimEnd('、');
    }

}