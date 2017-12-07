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
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Sys.Duty;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

/// <summary>
/// 功能描述：获取数据分析数据审核人
/// 创建日期：2013-03-17
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_ResultUserSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["strItemId"].ToString() != null)
            this.strItemId.Value = Request["strItemId"].ToString();
        if (Request["strSampleId"].ToString() != null)
            this.strSampleId.Value = Request["strSampleId"].ToString();
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "getUserInfo")
        {
            strResult = getUserInfo(Request.QueryString["strItemId"].ToString(), Request.QueryString["strSampleId"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 根据监测类型获取敢为职责用户信息
    /// </summary>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getUserInfo(string strItemId, string strSampleId)
    {
        //获取监测类别
        string strMonitorType = "";
        DataTable dtSubtask = new TMisMonitorSubtaskLogic().GetSubTaskObjectBySample(strSampleId);
        if (dtSubtask.Rows.Count > 0)
            strMonitorType = dtSubtask.Rows[0]["MONITOR_ID"].ToString();
        //获取分析分配环节用户信息
        DataTable objTable = new TSysDutyLogic().SelectTableDutyUser(new TSysDutyVo() { DICT_CODE = "duty_other_analyse_result", MONITOR_TYPE_ID = strMonitorType, MONITOR_ITEM_ID = strItemId });
        string strCurrentUserId = LogInfo.UserInfo.ID;
        DataRow deleteRow = null;
        foreach (DataRow row in objTable.Rows)
        {
            if (row["USERID"].ToString() == strCurrentUserId)
                deleteRow = row;
        }
        if (deleteRow != null)
            objTable.Rows.Remove(deleteRow);
        return DataTableToJson(objTable);
    }
}