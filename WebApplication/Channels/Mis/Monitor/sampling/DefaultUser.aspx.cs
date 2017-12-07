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

using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
/// <summary>
/// 功能描述：获取默认负责人
/// 创建日期：2012-12-7
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_DefaultUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        if (!IsPostBack)
        {
            if (Request["strSubTaskId"].ToString() != null)
                this.strSubTaskId.Value = Request["strSubTaskId"].ToString();
            if (Request["strTaskId"] != null)
                this.strTaskId.Value = Request["strTaskId"].ToString();
            if (Request["strMonitorType"].ToString() != null)
                this.strMonitorType.Value = Request["strMonitorType"].ToString();
        }
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "getUserInfo")
        {
            strResult = getUserInfo(Request["strMonitorType"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //保存数据
        if (Request["Status"] != null && Request["Status"].ToString() == "save")
        {
            strResult = SaveUserInfo();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 根据监测类型获取岗位职责用户信息
    /// </summary>
    /// <param name="strMonitorType">监测类型Id</param>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getUserInfo(string strMonitorType)
    {
        //获取采样分配环节用户信息
        DataTable objTable = new TMisMonitorResultLogic().getUsersInfo("duty_sampling", strMonitorType, "");
        return DataTableToJson(objTable);
    }

    /// <summary>
    /// 获取监测计划监测类型负责人信息
    /// </summary>
    /// <returns></returns>
    private string GetContractDutyUser(string strMonitorId, string task_id, string strPlanId)
    {
        string result = "";
         DataTable dt = new DataTable();
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        objItems.CONTRACT_ID = task_id;
        objItems.MONITOR_TYPE_ID = strMonitorId;
        objItems.CONTRACT_PLAN_ID = strPlanId;
        dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    public string SaveUserInfo()
    {
        bool isSuccess = true;
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
        if (this.strTaskId.Value.Trim() == "")
        {
            objSubtask.ID = this.strSubTaskId.Value;
            objSubtask.SAMPLING_MANAGER_ID = Request["DEFAULT_USER"];
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
        }
        else
        {   
            //在采样分配环节批量修改采样人 add by weilin
            objSubtask.SAMPLING_MANAGER_ID = Request["DEFAULT_USER"];
            TMisMonitorSubtaskVo objWhere = new TMisMonitorSubtaskVo();
            objWhere.TASK_ID = this.strTaskId.Value.Trim();
            objWhere.TASK_STATUS = "01";
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask, objWhere);
        }
        
        return isSuccess == true ? "1" : "0";
    }

}