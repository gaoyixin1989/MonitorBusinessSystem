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
/// 功能描述：现场加标
/// 创建日期：2013-4-26
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_QcAddSetting : PageBase
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
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSourceSampleId = Request["strSampleId"];

        List<object> listResult = new List<object>();
        DataTable objTable = new TBaseItemInfoLogic().SelectItemForQC(strSourceSampleId, "0");
        objTable.Columns.Add("QC_ADD", System.Type.GetType("System.String"));
        objTable.Columns.Add("IS_CHECKED", System.Type.GetType("System.String"));

        //遍历数据集
        foreach (DataRow row in objTable.Rows)
        {
            //监测项目
            string strItemId = row["ID"].ToString();
            //获取加标样结果表【T_MIS_MONITOR_QC_ADD】中的家标量数据
            DataTable obj = new TMisMonitorResultLogic().getQcAddValue(strSourceSampleId, Request["strQcType"], strItemId);
            if (obj.Rows.Count != 0)
            {
                row["QC_ADD"] = obj.Rows[0]["QC_ADD"].ToString();
                row["IS_CHECKED"] = "1";
            }
            else
            {
                row["IS_CHECKED"] = "0";
            }
        }
        int intTotalCount = objTable.Rows.Count;
        string strJson = CreateToJson(objTable, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 新增现场加标质控信息
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    /// <param name="strQcType">质控类别</param>
    /// <param name="strItemId">监测项目</param>
    /// <param name="strQcAddValue">加标量</param>
    /// <returns></returns>
    [WebMethod]
    public static bool QcSave(string strSampleID, string strQcType, string strItemId, string strQcAddValue)
    {
        deleteSampleInfo(strSampleID, strQcType);
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        string strSourceResultId = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_NAME += "现场加标";
        //objSample.SAMPLE_CODE = GetSampleCode_QHD(strSampleID);
        //新增点位时候，自动生成该点位的样品编码
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

        //objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strItemId.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            strSourceResultId = objResult.ID;
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
            objResult.QC_TYPE = strQcType;
            objResult.QC_SOURCE_ID = strSourceResultId;
            objResult.SOURCE_ID = strSourceResultId;
            objResult.QC = "###";
            if (!new TMisMonitorResultLogic().Create(objResult))
                isSuccess = false;
            InsertResultAPP(objResult.ID);

            TMisMonitorQcAddVo objQcAdd = new TMisMonitorQcAddVo();
            objQcAdd.ID = GetSerialNumber("QcAddId");
            objQcAdd.RESULT_ID_SRC = strSourceResultId;
            objQcAdd.RESULT_ID_ADD = objResult.ID;
            objQcAdd.QC_TYPE = strQcType;
            objQcAdd.QC_ADD = strQcAddValue.Split(',')[i];
            if (!new TMisMonitorQcAddLogic().Create(objQcAdd))
                isSuccess = false;
        }
        return isSuccess;
    }
    /// <summary>
    /// 删除质控样品信息
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    private static void deleteSampleInfo(string strSampleID, string strQcType)
    {
        new TMisMonitorResultLogic().deleteQcInfo(strSampleID, strQcType);
    }
}