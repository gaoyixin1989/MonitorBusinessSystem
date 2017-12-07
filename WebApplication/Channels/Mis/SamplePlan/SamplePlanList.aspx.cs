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
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;

public partial class Channels_Mis_SamplePlan_SamplePlanList :PageBase
{
    public string task_id = "", strPlanId = "", strWorkTask_Id = "", strSubTask_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHidParam();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../Monitor/sampling/template/WorkTaskSheetQY.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        string strTaskType = "", strMonitorList = "", strWorkContent = "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        objTask.ID = strWorkTask_Id;
        TMisContractSamplePlanVo objPlan = new TMisContractSamplePlanVo();
        objPlan.ID = strPlanId;
        DataTable dt = new TMisMonitorTaskLogic().GetContractInforUnionSamplePlan(objTask, objPlan);
        //插入委托书单号
        if (dt.Rows.Count > 0)
        {
            sheet.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["TICKET_NUM"].ToString());
            sheet.GetRow(4).GetCell(6).SetCellValue(dt.Rows[0]["REPORT_CODE"].ToString());
            strTaskType = getDictName(dt.Rows[0]["CONTRACT_TYPE"].ToString(), "Contract_Type");
            sheet.GetRow(5).GetCell(1).SetCellValue(strTaskType + "任务");
            sheet.GetRow(5).GetCell(7).SetCellValue(dt.Rows[0]["CONTRACT_CODE"].ToString());

            string[] strMonitroTypeArr = dt.Rows[0]["TEST_TYPE"].ToString().Split(';');
            //foreach (string str in strMonitroTypeArr)
            //{
            //    string strMonitor = "";
            //    strMonitor = GetMonitorName(str);
            //    strMonitorList += (strMonitor == "水和废水" ? "废水" : (strMonitor == "气和废气" ? "废气" : strMonitor)) + "、";
            //}
            //strMonitorList = strMonitorList.Substring(0, strMonitorList.Length - 1);
            sheet.GetRow(6).GetCell(1).SetCellValue(dt.Rows[0]["CLIENT_COMPANY_NAME"].ToString());
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
                    if (INTSHOWLEN == intLen)
                    {
                        strWorkContent = strWorkContent.Substring(0, strWorkContent.Length - 1);
                        strWorkContent += "(送样)\n";
                    }
                }
            }
            DataTable dtPoint = GetSampleDateTable();
            string strPoint = "", strPoint_Name = "", strItems = "", strPointItems = "";
            int IS_SAMPLEDEPT = 0;    //是否存在现场监测项目
            int IS_CERTIFICATE = 0;   //是否存在实验室监测项目
            if (dtPoint.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPoint.Rows)
                {
                    strPoint += dr["SAMPLE_NAME"].ToString() + "、";
                    DataTable dtPointItems = GetSampleItemsDateTable(dr["ID"].ToString());
                    if (dtPointItems.Rows.Count > 0)
                    {
                        strPoint_Name = dr["SAMPLE_NAME"].ToString() + "：";

                        strItems = "";
                        foreach (DataRow drr in dtPointItems.Rows)
                        {
                            strItems += drr["ITEM_NAME"].ToString() + "、";

                            if (drr["IS_SAMPLEDEPT"].ToString() == "是")
                                IS_SAMPLEDEPT = 1;
                            if (drr["LAB_CERTIFICATE"].ToString() == "是")
                                IS_CERTIFICATE = 1;
                        }
                    }
                    if (!String.IsNullOrEmpty(strItems))
                    {
                        strItems = strItems.Substring(0, strItems.Length - 1);
                    }
                    strPointItems += strPoint_Name + strItems + "；";
                }
                if (!String.IsNullOrEmpty(strPoint))
                {
                    strPoint = strPoint.Substring(0, strPoint.Length - 1);
                }
            }
            strWorkContent += "监测样品：" + strPoint + "\n";
            strWorkContent += "监测因子与频次：\n" + strPointItems;
            sheet.GetRow(9).GetCell(1).SetCellValue(strWorkContent);

            //接收科室
            string strJSKS = "";
            if (IS_SAMPLEDEPT == 1)
            {
                strJSKS += "现场室、";
            }
            if (IS_CERTIFICATE == 1)
            {
                strJSKS += "实验室、";
            }
            sheet.GetRow(8).GetCell(1).SetCellValue(strJSKS.TrimEnd('、'));
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
            sheet.GetRow(10).GetCell(1).SetCellValue(strLinkPerple);

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

    private void GetHidParam()
    {
        task_id = this.hidTaskId.Value.ToString();
        strPlanId = this.hidPlanId.Value.ToString();
        strWorkTask_Id = this.hidWorkTaskId.Value.ToString();
        strSubTask_Id = this.hidSubTaskId.Value.ToString();
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

    /// <summary>
    /// 获取样品
    /// </summary>
    /// <returns></returns>
    private DataTable GetSampleDateTable() {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            dt = new TMisContractSampleLogic().SelectByTable(new TMisContractSampleVo { CONTRACT_ID = task_id ,SAMPLE_PLAN_ID=strPlanId});
        }
        return dt;
    }

    /// <summary>
    /// 获取样品下的监测项目
    /// </summary>
    /// <returns></returns>
    private DataTable GetSampleItemsDateTable(string strSamplId)
    {
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strSamplId))
        {
            dt = new TMisContractSampleitemLogic().GetSampleItemsInfor(new TMisContractSampleitemVo { CONTRACT_SAMPLE_ID = strSamplId });
        }
        return dt;
    }
}