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

/// <summary>
/// 功能描述：选择默认协同人
/// 创建日期：2012-12-03
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_DefaultUserEx : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["strResultId"] != null)
            this.strResultId.Value = Request["strResultId"].ToString();

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "getUserInfo")
        {
            strResult = getUserInfo(Request["strResultId"].ToString());
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
    /// 根据监测类型获取敢为职责用户信息
    /// </summary>
    /// <param name="strResultId">监测结果ID</param>
    /// <returns></returns>
    public string getUserInfo(string strResultId)
    {
        string strMonitorType = "";
        //获取该结果数据所属的监测类别
        DataTable objMonitorType = new TMisMonitorResultLogic().getMonitorTypeByResultId(strResultId);
        if (objMonitorType.Rows.Count > 0)
            strMonitorType = objMonitorType.Rows[0]["MONITOR_ID"].ToString();
        //获取该结果数据所属的监测项目
        string strItemId = new TMisMonitorResultLogic().Details(strResultId).ITEM_ID;

        //获取分析分配环节用户信息
        DataTable objTable = new TMisMonitorResultLogic().getUsersInfo("duty_analyse", strMonitorType, strItemId);
        return DataTableToJson(objTable);
    }
    public string SaveUserInfo()
    {
        bool isSuccess = true;
        if (Request["strItemID"] == null)
        {
            isSuccess = new TMisMonitorResultLogic().SaveItemExInfo(Request["strResultId"], Request["txtDefaultUserId"], "ASSISTANT_USERID");
        }
        else
        {
            isSuccess = new TMisMonitorResultLogic().SaveItemExInfo_QHD(Request["strSampleIDs"].ToString().TrimEnd(',').Replace(",", "','"), Request["strItemID"], Request["txtDefaultUserId"], "ASSISTANT_USERID");
        }
        return isSuccess == true ? "1" : "0";
    }
}