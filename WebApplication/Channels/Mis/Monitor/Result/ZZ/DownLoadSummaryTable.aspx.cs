using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

/// <summary>
/// 功能描述：汇总表下载列表
/// 创建日期：2013-09-26
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_ZZ_DownLoadSummaryTable : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["id"] != null && Request["id"].ToString() != "")
            this.formId.Value = Request["id"].ToString();

        if (!Page.IsPostBack)
        {
            //定义结果
            string strResult = "";
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo();
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskVo.TASK_ID = Request["formId"].ToString();
        DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(TMisMonitorSubtaskVo);
        DataTable dt = objTable.Clone();

        foreach (DataRow row in objTable.Rows)
        {
            if (row["REMARK1"].ToString() == "")
                dt.ImportRow(row);
        }
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }
}