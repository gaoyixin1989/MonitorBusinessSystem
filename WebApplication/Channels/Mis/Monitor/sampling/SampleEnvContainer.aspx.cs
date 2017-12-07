using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;

using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;

/// <summary>
/// 功能描述：环境质量采样容器模块【针对功能区噪声、区域环境噪声、道路交通噪声、空气等】
/// 创建日期：2013-07-11
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_SampleEnvContainer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取子任务ID
        if (Request["strSubtaskID"] != null)
        {
            this.hiddenSubTaskId.Value = Request["strSubtaskID"].ToString();

            //功能区噪声：functionnoise
            this.hiddenMonitorType.Value = new TMisMonitorSubtaskLogic().Details(this.hiddenSubTaskId.Value).MONITOR_ID.ToLower();

            string strTaskId = new TMisMonitorSubtaskLogic().Details(this.hiddenSubTaskId.Value).TASK_ID.Trim();
            string strPlanId = new TMisMonitorTaskLogic().Details(strTaskId).PLAN_ID.Trim();

            //获取计划信息
            TMisContractPlanVo TMisContractPlanVo = new TMisContractPlanLogic().Details(strPlanId);
            this.hiddenYear.Value = TMisContractPlanVo.PLAN_YEAR;
            this.hiddenMonth.Value = TMisContractPlanVo.PLAN_MONTH;
        }
    }
    /// <summary>
    /// 环境质量采样任务完成
    /// </summary>
    /// <param name="strSubTaskId">子任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string EnvFinish(string strSubTaskId)
    {
        TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskVo.ID = strSubTaskId;
        TMisMonitorSubtaskVo.TASK_STATUS = "09";
        if (new TMisMonitorSubtaskLogic().Edit(TMisMonitorSubtaskVo))
            return "1";
        else
            return "0";
    }
}