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

using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;

/// <summary>
/// 功能描述：秦皇岛空白加标功能增加
/// 创建日期：2013-4-27
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_QcEmptyAddSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["strSubTaskId"] != null && Request["strSubTaskId"].ToString() != "")
            this.txtSubTaskId.Value = Request["strSubTaskId"].ToString();
        if (Request["strQcType"] != null && Request["strQcType"].ToString() != "")
            this.txtQcType.Value = Request["strQcType"].ToString();
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="strSubTaskId"></param>
    /// <param name="strQcType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSampleItem(string strSubTaskId, string strQcType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        dt = new TMisMonitorResultLogic().getQcItemInfo_QHD(strSubTaskId, strQcType);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }
    /// <summary>
    /// 新增空白加标数据保存功能
    /// </summary>
    /// <param name="strSubTaskId">子任务ＩＤ</param>
    /// <param name="strQcType">监测类别　１０</param>
    /// <param name="strItemId">监测项目</param>
    /// <returns></returns>
    [WebMethod]
    public static bool QcSave(string strSubTaskId, string strQcType, string strItemId)
    {
        bool isSuccess = createQcInfo(strSubTaskId, strQcType, strItemId);
        return isSuccess;
    }
    private static bool createQcInfo(string strSubTaskId, string strQcType, string strItemId)
    {
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        string strQcSampleId = GetSerialNumber("MonitorSampleId");
        objSample.ID = strQcSampleId;
        objSample.SUBTASK_ID = strSubTaskId;
        objSample.QC_TYPE = strQcType;
        //objSample.SAMPLE_CODE = GetSampleCode_QHD(strQcSampleId);
        //新增点位时候，自动生成该点位的样品编码
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

        objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

        objSample.NOSAMPLE = "1";

        if (strQcType == "10")
            objSample.SAMPLE_NAME = "空白加标样";
        objSample.SAMPLE_COUNT = "";
        //在样品表中添加样品数据
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        //遍历监测项目信息，将监测项目信息添加到结果表、结果分析执行表、空白加标表中
        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            //将数据写入结果表中
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = strQcSampleId;
            objResult.QC_TYPE = strQcType;
            objResult.ITEM_ID = strItemId.Split(',')[i];

            //填充默认分析方法和方法依据
            TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
            objItemAnalysis.ITEM_ID = objResult.ITEM_ID;
            objItemAnalysis.IS_DEFAULT = "是";
            objItemAnalysis.IS_DEL = "0";
            objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);

            if (objItemAnalysis.ID.Length == 0)
            {
                objItemAnalysis = new TBaseItemAnalysisVo();
                objItemAnalysis.ITEM_ID = objResult.ITEM_ID;
                objItemAnalysis.IS_DEL = "0";
                objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
            }

            if (objItemAnalysis.ID.Length > 0)
            {
                TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                objResult.ANALYSIS_METHOD_ID = objMethod.ID;
                objResult.STANDARD_ID = objMethod.METHOD_ID;
            }
            objResult.ANALYSIS_METHOD_ID = "";
            objResult.STANDARD_ID = "";
            objResult.TASK_TYPE = "发送";
            objResult.RESULT_STATUS = "01";

            if (!new TMisMonitorResultLogic().Create(objResult))
                isSuccess = false;

            //将数据写入结果分析执行表中
            InsertResultAPP(objResult.ID);

            //将结果写入分析样结果表中
            TMisMonitorQcAddQhdVo objQcAdd = new TMisMonitorQcAddQhdVo();
            objQcAdd.ID = GetSerialNumber("QcAddId_QHD");
            objQcAdd.RESULT_ID_ADD = objResult.ID;
            objQcAdd.QC_TYPE = strQcType;
            if (!new TMisMonitorQcAddQhdLogic().Create(objQcAdd))
                isSuccess = false;
        }
        return isSuccess;
    }
}