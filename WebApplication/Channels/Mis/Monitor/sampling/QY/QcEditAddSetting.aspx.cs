using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;

/// <summary>
/// 功能描述：获取样品的质控信息
/// 创建日期：2014-03-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_QcEditAddSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["strSampleId"] != null && Request["strSampleId"].ToString() != "")
            this.txtSampleId.Value = Request["strSampleId"].ToString();
        if (Request["strQcType"] != null && Request["strQcType"].ToString() != "")
            this.txtQcType.Value = Request["strQcType"].ToString();

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
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        DataTable objTable = new TBaseItemInfoLogic().SelectQcAddItem(this.txtSampleId.Value.Trim(), this.txtQcType.Value.Trim());
        
        int intTotalCount = objTable.Rows.Count;
        string strJson = CreateToJson(objTable, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 修改现场加标质控信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static bool QcUpdate(string strID, string strUpdateCell, string strUpdateCellValue)
    {
        bool isSuccess = true;

        isSuccess = new TBaseItemInfoLogic().UpdateQcInfo("T_MIS_MONITOR_QC_ADD", strUpdateCell, strUpdateCellValue, strID);

        return isSuccess;
    }
}