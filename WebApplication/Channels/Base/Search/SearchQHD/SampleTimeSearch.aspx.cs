using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using System.Data;
using i3.View;

public partial class Channels_Base_Search_SearchQHD_SampleTimeSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(Request["action"]) && Request["action"].ToString() == "GetData")
            {
                json = GetData();
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
    #region 获取数据信息
    private string GetData()
    {
        //页数
        int pageIndex = Int32.Parse(Request.Params["page"].ToString());
        //分页数
        int pageSize = Int32.Parse(Request.Params["pagesize"].ToString());
        string StartTime = string.Empty;
        string EndTime = string.Empty;
        string HEAD_USERID = string.Empty;
        string TICKET_NUM = string.Empty;
        string OverTime = string.Empty;
        if (Request["StartTime"] != null)
        {
            StartTime = Request["StartTime"].ToString();//实际开始时间
        }
        if (Request["EndTime"] != null)
        {
            EndTime = Request["EndTime"].ToString();//实际结束时间
        }
        if (Request["HEAD_USERID"] != null)
        {
            HEAD_USERID = Request["HEAD_USERID"].ToString();//分析负责人
        }
        if (Request["TICKET_NUM"] != null)
        {
            TICKET_NUM = Request["TICKET_NUM"].ToString();//任务单号
        }
        if (Request["OverTime"] != null)
        {
            OverTime = Request["OverTime"].ToString();//是否超期完成{0：全部；1：是；2：否}
        }
        int intTotalCount = new TMisMonitorSubtaskLogic().GetResultCount(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime);
        DataTable result = new TMisMonitorSubtaskLogic().SearchDataEx(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime, pageIndex, pageSize);
        string json = LigerGridDataToJson(result, intTotalCount);

        return json;
    }
    #endregion
}