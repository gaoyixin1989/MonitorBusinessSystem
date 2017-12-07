using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using System.Data;
/// <summary>
/// 为样品添加子样品数据记录 
/// 胡方扬 2013-04-08 （清远提出需求 现场改）
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_SubSample : PageBase
{
    private string strAction = "", strSampleId = "", strActionDate = "", strSubSampleId = "", strSubSampleNum = "", strSampleCode = "";
    private int intPageIndex =0, intPageSize = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParm();

        if (!String.IsNullOrEmpty(strAction)) {
            switch (strAction) { 
                case "GetSubSampleList":
                    Response.Write(GetSubSampleList());
                    Response.End();
                    break;
                case "UpdateSubSample":
                    Response.Write(UpdateSubSample());
                    Response.End();
                    break;
                case "InsertSubSample":
                    Response.Write(InsertSubSample());
                    Response.End();
                    break;
                case "SaveData":
                    Response.Write(SaveData());
                    Response.End();
                    break;
                case "DeleteData":
                    Response.Write(DeleteData());
                    Response.End();
                    break;
                case "UpdateSample":
                    Response.Write(UpdateSample());
                    Response.End();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 保存表格按钮新增的数据
    /// </summary>
    /// <returns></returns>
    public string SaveData() {
        string result = "";
        TMisMonitorSubsampleInfoVo objItems = new TMisMonitorSubsampleInfoVo();
        objItems.SAMPLEID = strSampleId;
        objItems.SUBSAMPLE_NAME = strSampleCode;
        objItems.ACTIONDATE = strActionDate;
        if (!String.IsNullOrEmpty(strSubSampleId))
        {
            objItems.ID = strSubSampleId;
            if (new TMisMonitorSubsampleInfoLogic().Edit(objItems))
            {
                result = "true";
            }
        }
        else
        {
            objItems.ID = GetSerialNumber("t_mis_monitor_SubSampleID");
            if (new TMisMonitorSubsampleInfoLogic().Create(objItems))
            {
                result = "true";
            }
        }
        return result;
    }

    /// <summary>
    /// 删除子样品
    /// </summary>
    /// <returns></returns>
    public string DeleteData() {
        string result = "";
        TMisMonitorSubsampleInfoVo objItems = new TMisMonitorSubsampleInfoVo();
        objItems.ID = strSubSampleId;
        if (new TMisMonitorSubsampleInfoLogic().Delete(objItems)) {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 插入子样品记录
    /// </summary>
    /// <returns></returns>
    public string InsertSubSample() {
        string result = "";
        int intNumber = 0;
        TMisMonitorSubsampleInfoVo objItems = new TMisMonitorSubsampleInfoVo();
        objItems.SAMPLEID = strSampleId;
        objItems.ACTIONDATE = strActionDate;
        if (!String.IsNullOrEmpty(strSubSampleNum)) {
            intNumber = Convert.ToInt32(strSubSampleNum);
        }
        if (new TMisMonitorSubsampleInfoLogic().InsertSubSample(objItems, strSampleCode, intNumber))
        {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 更新样品子样数据
    /// </summary>
    /// <returns></returns>
    public string UpdateSample() {
        string result = "";
        TMisMonitorSampleInfoVo objItems = new TMisMonitorSampleInfoVo();
        objItems.ID = strSampleId;
        objItems.SUBSAMPLE_NUM = strSubSampleNum;
        if (new TMisMonitorSampleInfoLogic().Edit(objItems))
        {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 获取样品的子样品信息
    /// </summary>
    /// <returns></returns>
    public string GetSubSampleList() {
        string result = "";
        DataTable dt = new DataTable();

        TMisMonitorSubsampleInfoVo objItems = new TMisMonitorSubsampleInfoVo();
        objItems.SAMPLEID = strSampleId;
        dt = new TMisMonitorSubsampleInfoLogic().SelectByTable(objItems, intPageIndex,intPageSize);
        int CountNum=new TMisMonitorSubsampleInfoLogic().GetSelectResultCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }
    /// <summary>
    /// 更新子样数据
    /// </summary>
    /// <returns></returns>
    public string UpdateSubSample()
    {
        string result = "";
        TMisMonitorSubsampleInfoVo objItems = new TMisMonitorSubsampleInfoVo();
        objItems.ID = strSubSampleId;
        objItems.ACTIONDATE = strActionDate;
        if (new TMisMonitorSubsampleInfoLogic().Edit(objItems))  {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 获取参数
    /// </summary>
    public void GetRequestParm() {
        if (!String.IsNullOrEmpty(Request.Params["action"])) {
            strAction = Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSampleId"]))
        {
            strSampleId = Request.Params["strSampleId"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strActionDate"]))
        {
            strActionDate = Request.Params["strActionDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSubSampleId"]))
        {
            strSubSampleId = Request.Params["strSubSampleId"].Trim().ToString();
        }

        if (!String.IsNullOrEmpty(Request.Params["strSubSampleNum"]))
        {
            strSubSampleNum = Request.Params["strSubSampleNum"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSampleCode"]))
        {
            strSampleCode = Request.Params["strSampleCode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(Request.Params["page"].Trim().ToString());
        }
        if (!String.IsNullOrEmpty(Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(Request.Params["pagesize"].Trim().ToString());
        }
    }
}