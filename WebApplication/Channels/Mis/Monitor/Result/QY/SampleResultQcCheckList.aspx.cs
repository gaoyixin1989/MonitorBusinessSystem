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

/// <summary>
/// 功能描述：现场结果审核列表
/// 创建时间：2014-03-27
/// 创建人：weilin
/// </summary>
public partial class Channels_Mis_Monitor_Result_QY_SampleResultQcCheckList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //输入结果
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getListInfo")
        {
            Response.Write(GetReportInfo());
            Response.End();
        }
        if (!IsPostBack)
        {
        }
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    protected string GetReportInfo()
    {
        string result = "";
        int intTotalCount = 0;
        //页数
        int pageIndex = Int32.Parse(Request.Params["page"].ToString());
        //分页数
        int pageSize = Int32.Parse(Request.Params["pagesize"].ToString());
        DataTable dt = new DataTable();

        dt = new TMisMonitorSubtaskLogic().SelectSamplingCheckList(LogInfo.UserInfo.ID, "023", pageIndex, pageSize, false);

        intTotalCount = new TMisMonitorSubtaskLogic().SelectSamplingCheckListCount(LogInfo.UserInfo.ID, "023", false);
        
        result = LigerGridDataToJson(dt, intTotalCount);
        return result;
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