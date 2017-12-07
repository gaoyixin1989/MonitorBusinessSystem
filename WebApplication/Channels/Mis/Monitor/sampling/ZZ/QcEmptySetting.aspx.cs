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
/// 功能描述：现场空白添加
/// 创建日期：2013-4-27
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_QcEmptySetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["strSampleId"] != null && Request["strSampleId"].ToString() != "")
            this.txtSampleId.Value = Request["strSampleId"].ToString();
        if (Request["strQcType"] != null && Request["strQcType"].ToString() != "")
            this.txtQcType.Value = Request["strQcType"].ToString();
    }
    /// <summary>
    /// 获取样品-监测项目
    /// </summary>
    /// <param name="strMonitorTypeId">样品Id</param>
    /// <param name="strQcType">质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSampleItem(string strSampleID, string strQcType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        dt = new TBaseItemInfoLogic().SelectItemForQC(strSampleID, strQcType);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }
    /// <summary>
    /// 新增现场空白质控信息
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    /// <param name="strQcType">质控类别</param>
    /// <param name="strItemId">监测项目</param>
    /// <returns></returns>
    [WebMethod]
    public static bool QcSave(string strSampleID, string strQcType, string strItemId)
    {
        deleteSampleInfo(strSampleID, strQcType);
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        string strSourceResultId = "";
        //添加现场空白样
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        objSample.SAMPLE_CODE = GetSampleCode(strSampleID);
        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
        objSample.SAMPLE_NAME += "现场空白";
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            //根据监测项目查询原始样结果表信息
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strSampleID;
            objResult.QC_TYPE = "0";
            objResult.ITEM_ID = strItemId.Split(',')[i];
            objResult = new TMisMonitorResultLogic().Details(objResult);

            //根据监测项目将空白样信息添加到结果表中
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

            //将空白信息添加到现场空白结果表中
            TMisMonitorQcEmptyOutVo objQcEmpty = new TMisMonitorQcEmptyOutVo();
            objQcEmpty.ID = GetSerialNumber("QC_EMPTY_OUT");
            objQcEmpty.RESULT_ID_SRC = strSourceResultId;
            objQcEmpty.RESULT_ID_EMPTY = objResult.ID;
            if (!new TMisMonitorQcEmptyOutLogic().Create(objQcEmpty))
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
    /// <summary>
    /// 功能描述：样品生成规则 Create By Castle(胡方扬) 2014-04-19
    /// </summary>
    /// <param name="strSampleID"></param>
    /// <returns></returns>
    protected static string GetSampleCode(string strSampleID)
    {
        string strSampleCode = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        strSampleCode = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);
        return strSampleCode;
    }
}