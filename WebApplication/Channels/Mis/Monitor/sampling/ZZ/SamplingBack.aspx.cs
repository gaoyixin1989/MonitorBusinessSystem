using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

/// <summary>
/// 功能描述：采样任务退回
/// 创建日期：2013-09-10
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplingBack : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        DataTable dt = new TMisMonitorResultLogic().getSamplingBackInfo_ZZ("04", "9");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GoToBack(string strTaskId)
    {
        TMisMonitorTaskVo TMisMonitorTaskVo = new TMisMonitorTaskVo();
        TMisMonitorTaskVo.ID = strTaskId;
        TMisMonitorTaskVo.QC_STATUS = "NULL";
        bool IsTaskEditSuccess = new TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);

        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskVo.TASK_ID = strTaskId;
        TMisMonitorSubtaskVo.TASK_STATUS = "04";

        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(TMisMonitorSubtaskVo);

        bool IsSubTaskEditSuccess = true;

        foreach (DataRow row in dt.Rows)
        {
            string strSubTaskId = row["ID"].ToString();
            TMisMonitorSubtaskVo TMisMonitorSubtaskVoTemp = new TMisMonitorSubtaskVo();
            TMisMonitorSubtaskVoTemp.ID = strSubTaskId;
            TMisMonitorSubtaskVoTemp.TASK_STATUS = "01";
            IsSubTaskEditSuccess = new TMisMonitorSubtaskLogic().Edit(TMisMonitorSubtaskVoTemp);
        }
        return IsTaskEditSuccess && IsSubTaskEditSuccess == true ? "1" : "0";
    }
}