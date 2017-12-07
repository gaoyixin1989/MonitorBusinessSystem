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
/// 功能描述：郑州标准盲样功能
/// 创建日期：2013-07-01
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_QcBlindSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["strSubTaskId"] != null && Request["strSubTaskId"].ToString() != "")
            this.txtSubTaskId.Value = Request["strSubTaskId"].ToString();
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
        string strSubTaskId = Request["strSubTaskId"];

        List<object> listResult = new List<object>();
        DataTable objTable = new TMisMonitorResultLogic().getQcItemInfo_QHD(strSubTaskId, "0");
        objTable.Columns.Add("STANDARD_VALUE", System.Type.GetType("System.String"));
        objTable.Columns.Add("UNCETAINTY", System.Type.GetType("System.String"));

        int intTotalCount = objTable.Rows.Count;
        string strJson = CreateToJson(objTable, intTotalCount);
        return strJson;
    }
    /// <summary>
    /// 新增空白加标数据保存功能
    /// </summary>
    /// <param name="strSubTaskId">子任务ID</param>
    /// <param name="strQcType">监测类别</param>
    /// <param name="strItemId">监测项目</param>
    /// <returns></returns>
    [WebMethod]
    public static bool QcSave(string strSubTaskId, string strQcType, string strItemId, string strSumQcStandValue, string strSumQcUncetainty)
    {
        bool isSuccess = createQcInfo(strSubTaskId, strQcType, strItemId, strSumQcStandValue, strSumQcUncetainty);
        return isSuccess;
    }
    private static bool createQcInfo(string strSubTaskId, string strQcType, string strItemId, string strSumQcStandValue, string strSumQcUncetainty)
    {
        bool isSuccess = true;

        if (strItemId == "")
            return isSuccess;

        //TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        //string strQcSampleId = GetSerialNumber("MonitorSampleId");
        //objSample.ID = strItemId;
        //objSample.SUBTASK_ID = strSubTaskId;
        //objSample.QC_TYPE = strQcType;

        //string strSourceResultId = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSubTaskId);
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.QC_TYPE = strQcType;
        objSample.QC_SOURCE_ID = strSubTaskId;
        //objSample.SAMPLE_NAME += "现场加密";


        //新增点位时候，自动生成该点位的样品编码
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
        objSerial.SERIAL_TYPE = "2";

        objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

        //objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

        objSample.NOSAMPLE = "0";

        if (strQcType == "11")
            objSample.SAMPLE_NAME += "现场加密";

        //在样品表中添加样品数据
        if (!new TMisMonitorSampleInfoLogic().Create(objSample))
            isSuccess = false;

        //遍历监测项目信息，将监测项目信息添加到结果表、结果分析执行表、标准盲样表中
        for (int i = 0; i < strItemId.Split(',').Length; i++)
        {
            //将数据写入结果表中
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.ID = GetSerialNumber("MonitorResultId");
            objResult.SAMPLE_ID = objSample.ID;
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
            //objResult.ANALYSIS_METHOD_ID = "";
            //objResult.STANDARD_ID = "";
            objResult.TASK_TYPE = "发送";
            objResult.RESULT_STATUS = "01";

            if (!new TMisMonitorResultLogic().Create(objResult))
                isSuccess = false;

            //将数据写入结果分析执行表中
            InsertResultAPP(objResult.ID);

            //将结果写入盲样结果表中
            TMisMonitorQcBlindZzVo TMisMonitorQcBlindZzVo = new TMisMonitorQcBlindZzVo();
            TMisMonitorQcBlindZzVo.ID = GetSerialNumber("QcBlindId_ZZ");
            TMisMonitorQcBlindZzVo.RESULT_ID = objResult.ID;
            TMisMonitorQcBlindZzVo.QC_TYPE = strQcType;
            TMisMonitorQcBlindZzVo.STANDARD_VALUE = strSumQcStandValue.Split(',')[i];
            TMisMonitorQcBlindZzVo.UNCETAINTY = strSumQcUncetainty.Split(',')[i];
            if (!new TMisMonitorQcBlindZzLogic().Create(TMisMonitorQcBlindZzVo))
                isSuccess = false;
        }
        return isSuccess;
    }
}