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
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：常规性监测类任务预约   
/// 创建时间：2013-06-07
/// 创建人 ： 胡方扬
/// 修改时间：
/// 修改人：
/// 修改内容：
/// </summary>
public partial class Channels_Mis_MonitoringPlan_ConvenTaskPlanList : PageBase
{
    public string task_id = "", strPlanId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
    }

    private void GetHidParam()
    {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../Monitor/sampling/template/WorkTaskSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "", strMonitorList = "", strWorkContent = "";
        DataTable dt = GetPendingPlanDataTable();
        //插入委托书单号
        if (dt.Rows.Count > 0)
        {
            sheet.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["TICKET_NUM"].ToString());
            sheet.GetRow(4).GetCell(6).SetCellValue(dt.Rows[0]["REPORT_CODE"].ToString());
            strTaskType = getDictName(dt.Rows[0]["CONTRACT_TYPE"].ToString(), "Contract_Type");
            sheet.GetRow(5).GetCell(1).SetCellValue(strTaskType + "任务");
            sheet.GetRow(5).GetCell(7).SetCellValue(dt.Rows[0]["CONTRACT_CODE"].ToString());

            string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPE"].ToString().Split(';');
            //string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPES"].ToString().Split(';');
            foreach (string str in strMonitroTypeArr)
            {
                strMonitorList += GetMonitorName(str) + "/";
            }
            strMonitorList = strMonitorList.Substring(0, strMonitorList.Length - 1);
            sheet.GetRow(7).GetCell(1).SetCellValue(strMonitorList);
            if (dt.Rows[0]["SAMPLE_SOURCE"].ToString() == "送样")
            {
                //strWorkContent += "地表水、地下水(送样)\n";
                int intLen = strMonitroTypeArr.Length;
                int INTSHOWLEN = 0;
                foreach (string strMonitor in strMonitroTypeArr)
                {
                    INTSHOWLEN++;
                    strWorkContent += GetMonitorName(strMonitor) + "、";
                    if (INTSHOWLEN == intLen)
                    {
                        strWorkContent = strWorkContent.Substring(0, strWorkContent.Length - 1);
                        strWorkContent += "(送样)\n";
                    }
                }
            }
            //获取当前监测类别信息
            DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable();
            DataTable dtPoint = GetPendingPlanPointDataTable();
            string strOutValuePoint = "", strOutValuePointItems = "";
            if (dtMonitor.Rows.Count > 0)
            {
                foreach (DataRow drr in dtMonitor.Rows)
                {
                    string strMonitorName = "", strPointName = "";
                    DataRow[] drPoint = dtPoint.Select("MONITOR_ID='" + drr["MONITOR_ID"].ToString() + "'");
                    if (drPoint.Length > 0)
                    {

                        foreach (DataRow drrPoint in drPoint)
                        {
                            string strPointNameForItems = "", strPointItems = "";
                            strMonitorName = drrPoint["MONITOR_TYPE_NAME"].ToString() + "：";
                            strPointName += drrPoint["POINT_NAME"].ToString() + "、";

                            //获取当前点位的监测项目
                            DataTable dtPointItems = GetPendingPlanPointItemsDataTable(drrPoint["CONTRACT_POINT_ID"].ToString());
                            if (dtPointItems.Rows.Count > 0)
                            {
                                foreach (DataRow drItems in dtPointItems.Rows)
                                {
                                    strPointNameForItems = strMonitorName.Substring(0, strMonitorName.Length - 1) + drrPoint["POINT_NAME"] + ":";
                                    strPointItems += drItems["ITEM_NAME"].ToString() + "、";
                                }
                                strOutValuePointItems += strPointNameForItems + strPointItems.Substring(0, strPointItems.Length - 1) + "；\n";
                            }
                        }
                        //获取输出监测类型监测点位信息
                        strOutValuePoint += strMonitorName + strPointName.Substring(0, strPointName.Length - 1) + "；\n";
                    }
                }
            }
            strWorkContent += "监测点位：\n" + strOutValuePoint;
            strWorkContent += "监测因子与频次：\n" + strOutValuePointItems;
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);

            string strLinkPerple = "";
            strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + "\n";
            strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
            if (!String.IsNullOrEmpty(dt.Rows[0]["PROJECT_ID"].ToString()))
            {
                strLinkPerple += "项目负责人:" + GetUserName(dt.Rows[0]["PROJECT_ID"].ToString());
            }
            else
            {
                strLinkPerple += "报告编写：";
            }
            sheet.GetRow(10).GetCell(1).SetCellValue(strLinkPerple);

            if (!String.IsNullOrEmpty(dt.Rows[0]["ASKING_DATE"].ToString()))
            {
                sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            }
            sheet.GetRow(11).GetCell(3).SetCellValue(LogInfo.UserInfo.REAL_NAME);
            sheet.GetRow(11).GetCell(6).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("工作任务通知单-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// 获取指定监测计划的监测类别信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDistinctMonitorDataTable()
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanDistinctMonitorDataTable(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定委托书和监测计划的任务信息  胡方扬 2013-04-24
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanDataTable()
    {
        DataTable dt = new DataTable();

        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanVo objItems = new TMisContractPlanVo();
            objItems.ID = strPlanId;
            dt = new TMisContractPlanLogic().SelectByTableContractPlanForPending(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetPendingPlanPointDataTable()
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID = strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanPointDataTable(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <param name="strPointId"></param>
    /// <returns></returns>
    private DataTable GetPendingPlanPointItemsDataTable(string strPointId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPointId))
        {
            TMisContractPointitemVo objItems = new TMisContractPointitemVo();
            objItems.CONTRACT_POINT_ID = strPointId;
            dt = new TMisContractPointitemLogic().GetItemsForPoint(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测类别的类别名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetMonitorName(string strId)
    {
        TBaseMonitorTypeInfoVo objItems = new TBaseMonitorTypeInfoLogic().Details(new TBaseMonitorTypeInfoVo { ID = strId, IS_DEL = "0" });
        return objItems.MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <param name="strId"></param>
    /// <returns></returns>
    private string GetUserName(string strId)
    {
        TSysUserVo objItems = new TSysUserLogic().Details(new TSysUserVo { ID = strId, IS_DEL = "0" });
        return objItems.REAL_NAME;
    }
}