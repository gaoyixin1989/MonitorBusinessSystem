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
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Sample;

/// <summary>
/// 功能描述：选择默认协同人
/// 创建日期：2012-12-7
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_DefaultUserExaspx2 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (!IsPostBack)
        {
            if (Request["strSubTaskId"].ToString() != null)
                this.strSubTaskId.Value = Request["strSubTaskId"].ToString();
            if (Request["strTaskId"] != null)
                this.strTaskId.Value = Request["strTaskId"].ToString();
            if (Request["strMonitorType"].ToString() != null)
                this.strMonitorType.Value = Request["strMonitorType"].ToString();
            if (Request["strSampleIds"].ToString() != null)
                this.strSampleIds.Value = Request["strSampleIds"].ToString();
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
    public string SaveUserInfo()
    {
        var sampleIds = this.strSampleIds.Value;

        bool isSuccess = true;

        var logic = new TMisMonitorSampleInfoLogic();

        var sampleIdList = sampleIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var sampleId in sampleIdList)
        {

            TMisMonitorSampleInfoVo vo = logic.Details(sampleId);

            vo.REMARK4 = Request["txtDefaultUserId"];
            logic.Edit(vo);

        }

        return isSuccess == true ? "1" : "0";
    }
}