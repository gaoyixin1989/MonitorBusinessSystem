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

public partial class Channels_Mis_MonitoringPlan_TaskPlanList : PageBase
{
    public string task_id="",strPlanId="",strType="";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
    }

    private void GetHidParam() {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
        strType = this.hidType.Value.ToString();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        FileStream file = null;
        if (strType == "QY")
        {
             file = new FileStream(HttpContext.Current.Server.MapPath("../Monitor/sampling/template/WorkTaskSheetQY.xls"), FileMode.Open, FileAccess.Read);
        }
        else if (strType == "ZZ")
        {
             file = new FileStream(HttpContext.Current.Server.MapPath("../Monitor/sampling/template/WorkTaskSheet.xls"), FileMode.Open, FileAccess.Read);
        }
        
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "", strMonitorList = "", Monitor = "", strWorkContent = "";
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
            //foreach (string str in strMonitroTypeArr) {
            //    string strMonitor = "";
            //    strMonitor = GetMonitorName(str);
            //    strMonitorList += (strMonitor == "水和废水" ? "废水" : (strMonitor == "气和废气" ? "废气" : strMonitor)) + "、";
            //}
            // strMonitorList = strMonitorList.Substring(0, strMonitorList.Length - 1);
            TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
            objItems.CONTRACT_ID = "";
            DataTable dts = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, dt.Rows[0]["ID"].ToString());
            if (dts.Rows.Count > 0)
            {
                strMonitorList = dts.Rows[0][1].ToString();
            }
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
                    if (INTSHOWLEN == intLen - 1)
                    {
                        strWorkContent += "(送样)\n";
                    }
                }
            }
            //获取当前监测类别信息
            DataTable dtMonitor = GetPendingPlanDistinctMonitorDataTable();
            DataTable dtPoint = GetPendingPlanPointDataTable();
            string strOutValuePoint = "", strOutValuePointItems = "";
            int IS_SAMPLEDEPT = 0;    //是否存在现场监测项目
            int IS_CERTIFICATE = 0;   //是否存在实验室监测项目
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
                                    strPointNameForItems = strMonitorName.Substring(0, strMonitorName.Length - 1) + drrPoint["POINT_NAME"] + "(" + (drrPoint["SAMPLE_DAY"].ToString() == "" ? "1" : drrPoint["SAMPLE_DAY"].ToString()) + "天" + (drrPoint["SAMPLE_FREQ"].ToString() == "" ? "1" : drrPoint["SAMPLE_FREQ"].ToString()) + "次):";
                                    strPointItems += drItems["ITEM_NAME"].ToString() + "、";

                                    if (drItems["IS_SAMPLEDEPT"].ToString() == "是")
                                        IS_SAMPLEDEPT = 1;
                                    if (drItems["IS_SAMPLEDEPT"].ToString() == "否")
                                        IS_CERTIFICATE = 1;
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
            //if (dt.Rows[0]["CONTRACT_TYPE"].ToString() == "02" || dt.Rows[0]["CONTRACT_TYPE"].ToString() == "03")//应急或者管理类的  strMonitorList
            if (strMonitorList == "02" || strMonitorList == "03")//应急或者管理类的  strMonitorList
            {
                strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + " \n";
                strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
                strLinkPerple += "报告编写人：韩永博 \n";
            }
            else
            {
                #region//暂不用代码
                //string[] strMonitroType = dt.Rows[0]["TEST_TYPE"].ToString().Split(';');
                //foreach (string strs in strMonitroType)
                //{
                //    string strMonitors = "";
                //    strMonitors = GetMonitorName(strs);
                //    if (strMonitors == "水和废水")
                //    {
                //        Monitor = "水";
                //    }
                //    else if (strMonitors == "气和废气")
                //    {
                //        Monitor = "气";
                //    }
                //    else if (strMonitors == "固废和土壤")
                //    {
                //        Monitor = "固体";
                //    }
                //    else if (strMonitors.Contains("噪声"))
                //    {
                //        Monitor = "噪声";
                //    }
                //    Monitor = Monitor + "、"; 
                //}
                //Monitor = Monitor.Substring(0, Monitor.Length - 1);
                #endregion
                if (strMonitorList == "水" || strMonitorList == "固体")
                {
                    strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + " \n";
                    strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
                    strLinkPerple += "报告编写人：韩永博 \n";
                }
                else if (strMonitorList == "气" || strMonitorList == "噪声")
                {
                    strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + " \n";
                    strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
                    strLinkPerple += "报告编写人：马振芳\n";
                }
                else if (strMonitorList == "水、气、声、固体")
                {
                    strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + " \n";
                    strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
                    strLinkPerple += "报告编写人：王志远\n";
                }
            }
            //strLinkPerple += "联系人：" + dt.Rows[0]["CONTACT_NAME"].ToString() + "\n";
            //strLinkPerple += "联系电话：" + dt.Rows[0]["PHONE"].ToString() + "\n";
            //if (!String.IsNullOrEmpty(dt.Rows[0]["PROJECT_ID"].ToString()))
            //{
            //    strLinkPerple += "项目负责人:" + GetUserName(dt.Rows[0]["PROJECT_ID"].ToString());
            //}
            //else
            //{
            //    strLinkPerple += "报告编写：";
            //}

            sheet.GetRow(10).GetCell(1).SetCellValue(strLinkPerple);

            //任务来源
            sheet.GetRow(6).GetCell(1).SetCellValue(dt.Rows[0]["COMPANY_NAME"].ToString());
            //接收科室
            string strJSKS = "";
            strJSKS += "现场室、";
            //if (IS_SAMPLEDEPT == 1)
            //{
            //    strJSKS += "现场室、";
            //}
            if (IS_CERTIFICATE == 1)
            {
                strJSKS += "实验室、";
            }
            sheet.GetRow(8).GetCell(1).SetCellValue(strJSKS.TrimEnd('、'));

            if (!String.IsNullOrEmpty(dt.Rows[0]["ASKING_DATE"].ToString()))
            {
                sheet.GetRow(11).GetCell(1).SetCellValue(DateTime.Parse(dt.Rows[0]["ASKING_DATE"].ToString()).ToString("yyyy-MM-dd"));
            }
            //sheet.GetRow(11).GetCell(3).SetCellValue(LogInfo.UserInfo.REAL_NAME);
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
    private DataTable GetPendingPlanPointDataTable() {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            TMisContractPlanPointVo objItems = new TMisContractPlanPointVo();
            objItems.PLAN_ID= strPlanId;
            dt = new TMisContractPlanPointLogic().GetPendingPlanPointDataTable(objItems);
        }
        return dt;
    }

    /// <summary>
    /// 获取指定监测计划的监测点位信息
    /// </summary>
    /// <param name="strPointId"></param>
    /// <returns></returns>
    private DataTable GetPendingPlanPointItemsDataTable(string strPointId) {
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