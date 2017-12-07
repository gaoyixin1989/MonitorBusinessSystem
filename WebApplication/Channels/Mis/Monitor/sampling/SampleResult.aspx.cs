using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

/// <summary>
/// 功能描述：采样-现场项目结果填报
/// 创建日期：2012-12-13
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_SampleResult : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
            }

            //监测点位信息获取
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getOneGridInfo")
            {
                strResult = GetPointInfo();
                Response.Write(strResult);
                Response.End();
            }

            //监测项目信息获取
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTwoGridInfo")
            {
                strResult = GetItemInfo(Request.QueryString["OneGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获得包含现场项目的点位信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetPointInfo()
    {
        DataTable dt = new TMisMonitorTaskPointLogic().SelectSampleDeptPoint(this.SUBTASK_ID.Value);
        int intTotalCount = dt.Rows.Count;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
            objSample.POINT_ID = dt.Rows[i]["ID"].ToString();
            objSample.SUBTASK_ID = this.SUBTASK_ID.Value;
            objSample = new TMisMonitorSampleInfoLogic().Details(objSample);

            dt.Rows[i]["REMARK1"] = objSample.SAMPLE_CODE;
            dt.Rows[i]["REMARK2"] = objSample.SAMPLE_NAME;
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获得现场项目信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetItemInfo(string strPointID)
    {
        DataTable dt = new DataTable();
        int intTotalCount = 0;
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        dt = new TMisMonitorResultLogic().SelectSampleDeptResult(this.SUBTASK_ID.Value, strPointID);
        intTotalCount = dt.Rows.Count;
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取监测项目
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemID)
    {
        TBaseItemInfoVo objItem = new TBaseItemInfoLogic().Details(strItemID);

        return objItem.ITEM_NAME;
    }

    /// <summary>
    /// 获取监测项目单位
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemUnit(string strItemID)
    {
        TBaseItemAnalysisVo objItemAna = new TBaseItemAnalysisVo();
        objItemAna.ITEM_ID = strItemID;
        objItemAna = new TBaseItemAnalysisLogic().Details(objItemAna);
        return getDictName(objItemAna.UNIT, "item_unit");
    }

    /// <summary>
    /// 样品信息保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSample(string id, string strSubtaskID, string strSampleCode, string strSampleName)
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointLogic().Details(id);
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.SUBTASK_ID = strSubtaskID;
        objSample.POINT_ID = id;
        objSample = new TMisMonitorSampleInfoLogic().Details(objSample);
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.SAMPLE_NAME = strSampleName;
        bool isSuccess = new TMisMonitorSampleInfoLogic().Edit(objSample);

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 现场项目结果保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveResult(string id, string strItemResult)
    {
        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        objResult.ID = id;
        objResult.ITEM_RESULT = strItemResult;
        bool isSuccess = new TMisMonitorResultLogic().Edit(objResult);

        return isSuccess == true ? "1" : "0";
    }
}