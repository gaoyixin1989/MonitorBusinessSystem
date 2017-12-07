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
/// 功能描述：现场平行、密码平行添加
/// 创建日期：2013-4-27
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_QcTwinSetting : PageBase
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
    /// 新增现场平行、密码平行质控信息
    /// </summary>
    /// <param name="strSampleID">样品ID</param>
    /// <param name="strQcType">监测类型</param>
    /// <param name="strItemId">监测项目</param>
    /// <param name="strQcCount">质控数量</param>
    /// <returns></returns>
    [WebMethod]
    public static bool QcSave(string strSampleID, string strQcType, string strItemId, string strQcCount)
    {
        bool isSuccess = true;
        deleteSampleInfo(strSampleID, strQcType);
        if (strQcCount == "1")
            createQcInfo(strSampleID, strQcType, strItemId);
        if (strQcCount == "2")
        {
            createQcInfo(strSampleID, strQcType, strItemId);
            createQcInfo(strSampleID, strQcType, strItemId);
        }
        return isSuccess;
    }
    private static bool createQcInfo(string strSampleID, string strQcType, string strItemId)
    {
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        string strQcSampleId = GetSerialNumber("MonitorSampleId");
        objSample.ID = strQcSampleId;
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSampleID;
        //objSample.SAMPLE_CODE = GetSampleCode_QHD(strSampleID);
        //新增点位时候，自动生成该点位的样品编码
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

        objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

        if (strQcType == "3")
            objSample.SAMPLE_NAME += "现场平行";
        if (strQcType == "4")
            objSample.SAMPLE_NAME += "密码平行";

        //在样品表中添加样品数据
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        //遍历监测项目信息，将监测项目信息添加到结果表、结果分析执行表、平行样结果表中
        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            //根据需要做质控的监测项目获取原始样的结果表ID
            TMisMonitorResultVo objSourceResult = new TMisMonitorResultVo();
            objSourceResult.SAMPLE_ID = strSampleID;
            objSourceResult.QC_TYPE = "0";
            objSourceResult.ITEM_ID = strItemId.Split(',')[i];
            TMisMonitorResultVo objResult = new TMisMonitorResultLogic().Details(objSourceResult);
            string strSourceId = objResult.ID;

            //将数据写入结果表中
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = strQcSampleId;
            objResult.QC_TYPE = strQcType;
            objResult.SOURCE_ID = strSourceId;
            objResult.QC_SOURCE_ID = strSourceId;
            if (!new TMisMonitorResultLogic().Create(objResult))
                isSuccess = false;

            //将数据写入结果分析执行表中
            InsertResultAPP(objResult.ID);

            //将结果写入分析样结果表中
            TMisMonitorQcTwinVo objQcTwin = new TMisMonitorQcTwinVo();
            objQcTwin.ID = GetSerialNumber("QcTwinId");
            objQcTwin.RESULT_ID_SRC = strSourceId;
            objQcTwin.RESULT_ID_TWIN1 = objResult.ID;
            objQcTwin.QC_TYPE = strQcType;
            if (!new TMisMonitorQcTwinLogic().Create(objQcTwin))
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