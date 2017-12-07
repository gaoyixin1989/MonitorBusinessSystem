using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.View;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Channels.Base.Company;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Sys.WF;

/// <summary>
/// 功能描述：技术负责人审核
/// 创建时间：2013-05-07
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_ReportZZ_ReportTechCheck : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //输入结果
        if (Request.Params.AllKeys.Contains("task_id"))
        {
            //任务ID
            this.TASK_ID.Value = Request.QueryString["task_id"].ToString();
        }
        //获取任务信息集
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTaskInfo")
        {
            Response.Write(getTaskInfo());
            Response.End();
        }
        //获取委托类型
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractType")
        {
            Response.Write(getDictJsonString("Contract_Type"));
            Response.End();
        }
        //获取监测类型
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getMonitorType")
        {
            Response.Write(getMonitorType());
            Response.End();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "GetData")
        {
            Response.Write(CreateResultTotalTable());
            Response.End();
        }
        if (!IsPostBack)
        {
            ViewState["BeforePageUrl"] = this.Page.Request.UrlReferrer;
            wfControl.InitWFDict();
        }
    }

    /// <summary>
    /// 获取任务信息Json
    /// </summary>
    /// <returns></returns>
    protected string getTaskInfo()
    {
        //任务ID
        string strTaskID = Request.Params.AllKeys.Contains("task_id") ? Request.QueryString["task_id"].ToString() : this.TASK_ID.Value;
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        TMisMonitorReportVo objReport = new TMisMonitorReportVo();
        if (!string.IsNullOrEmpty(strTaskID))
        {
            objTaskVo = new TMisMonitorTaskLogic().GetContractTaskInfo(strTaskID);
            objReport = new TMisMonitorReportLogic().Details(new TMisMonitorReportVo() { TASK_ID = strTaskID });
        }
        if (objTaskVo != null)
        {
            try
            {
                objTaskVo.CONSIGN_DATE = DateTime.Parse(objTaskVo.CONSIGN_DATE.ToString()).ToString("yyyy-MM-dd");
            }
            catch { }

        }
        //定制数据
        objTaskVo.REMARK1 = objReport.REPORT_CODE;//报告编号
        return ToJson(objTaskVo);
    }

    /// <summary>
    /// 获取委托单位名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetClientName(string strValue)
    {
        return new TMisMonitorTaskCompanyLogic().Details(strValue).COMPANY_NAME;
    }

    /// <summary>
    /// 获取数据字典名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetDataDictName(string strValue, string strType)
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(strValue, strType);
    }

    /// <summary>
    /// 回退任务
    /// </summary>
    /// <param name="strValue">任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string BackTask(string strValue)
    {
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskVo();
        objTaskVo.ID = strValue;
        objTaskVo.COMFIRM_STATUS = "0";
        if (new TMisMonitorTaskLogic().Edit(objTaskVo))
        {
            new PageBase().WriteLog("回退监测数据汇总表", "", new PageBase().LogInfo.UserInfo.ID + "回退监测数据汇总表" + strValue + "成功！");
        }
        return "";
    }

    /// <summary>
    /// 获取项目负责人
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetProjectID(string strValue)
    {
        DataTable dtProjecter = new TMisMonitorTaskLogic().GetProjectID(strValue);
        return DataTableToJson(dtProjecter);
    }


    /// <summary>
    /// 获取下拉控件字典项JSON样式
    /// </summary>
    /// <returns></returns>
    protected string getMonitorType()
    {
        TBaseMonitorTypeInfoVo objMonitorTypeVo = new TBaseMonitorTypeInfoVo();
        objMonitorTypeVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(objMonitorTypeVo);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 构建监测结果汇总表
    /// </summary>
    protected string CreateResultTotalTable()
    {
        //任务ID
        string strTaskId = !string.IsNullOrEmpty(Request.QueryString["strTaskId"].ToString()) ? Request.QueryString["strTaskId"].ToString() : "";
        //样品编号
        string strSampleCode = string.Empty;
        if (Request.Params.AllKeys.Contains("strSampleCode") && !string.IsNullOrEmpty(Request.QueryString["strSampleCode"].ToString()))
        {
            strSampleCode = Request.QueryString["strSampleCode"].ToString();
        }
        //监测类型
        string strMonitorId = string.Empty;
        if (Request.Params.AllKeys.Contains("strMonitorId") && !string.IsNullOrEmpty(Request.QueryString["strMonitorId"].ToString()))
        {
            strMonitorId = Request.QueryString["strMonitorId"].ToString();
        }
        //动态生成列名
        string strPreColumnName = "样品编号,地点,时间,样品种类";
        //动态生成列名编码
        string strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_FINISH_DATE,MONITOR_TYPE_NAME";
        if (!string.IsNullOrEmpty(strTaskId))
        {
            //行转列 前数据表
            DataTable dtResultTotal = new TMisMonitorResultLogic().getTotalItemInfoByTaskID(strTaskId, strSampleCode, strMonitorId);
            //过滤过质控样
            DataRow[] drResultTotal = dtResultTotal.Select(" QC_TYPE='0'");
            //行转列 后数据表
            DataTable dtNew = getDatatable("", strPreColumnName, strPreColumnName_Src, drResultTotal.CopyToDataTable());
            return DataTableToJsonUnsureCol(dtNew, "");
        }
        return "";
    }

    #region 结果汇总表 行转列
    //获取数据DataTable
    private DataTable getDatatable(string strMonitorType, string strPreColumnName, string strPreColumnName_Src, DataTable dtResult)
    {
        //得到指定监测类别下的所有项目
        string strItemIDs = "";
        string strItemName = "";
        GetItems_UnderMonitor(strMonitorType, dtResult, ref  strItemIDs, ref  strItemName);

        DataTable dt_Result_Return = new DataTable();
        //设置Datatable的列
        AddDatatable_column(strPreColumnName, strItemName, ref  dt_Result_Return);
        //填充Datatable的值
        InsertDataTable_Value(strMonitorType, dtResult, strPreColumnName, strPreColumnName_Src, strItemIDs, ref  dt_Result_Return);

        return dt_Result_Return;
    }

    //得到指定监测类别下的所有项目
    private void GetItems_UnderMonitor(string strMonitorType, DataTable dtResult, ref string strItemIDs, ref string strItemName)
    {
        DataRow[] drs = dtResult.Select();
        if (!string.IsNullOrEmpty(strMonitorType))
        {
            drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
        }

        for (int i = 0; i < drs.Length; i++)
        {
            if (!strItemIDs.Contains(drs[i]["ITEM_ID"].ToString()))
            {
                strItemIDs += (strItemIDs.Length > 0 ? "," : "") + drs[i]["ITEM_ID"].ToString();
                strItemName += (strItemIDs.Length > 0 ? "," : "") + drs[i]["ITEM_NAME"].ToString();
                strItemName += drs[i]["ITEM_UNIT"].ToString().Length > 0 ? ("（" + drs[i]["ITEM_UNIT"].ToString() + "）") : "";
            }
        }
    }

    //设置Datatable的列
    private void AddDatatable_column(string strPreColumnName, string strItemNames, ref DataTable dt)
    {
        if (strPreColumnName.Length > 0)
        {
            string[] arrPreColumnName = strPreColumnName.Split(',');
            for (int i = 0; i < arrPreColumnName.Length; i++)
            {
                dt.Columns.Add(arrPreColumnName[i], System.Type.GetType("System.String"));
            }
        }

        if (strItemNames.Length > 0)
        {
            string[] arrItemNames = strItemNames.Split(',');
            for (int i = 0; i < arrItemNames.Length; i++)
            {
                if (arrItemNames[i].Length > 0)
                    dt.Columns.Add(arrItemNames[i], System.Type.GetType("System.String"));
            }
        }
    }

    //填充Datatable的值
    private void InsertDataTable_Value(string strMonitorType, DataTable dtResult, string strPreColumnName, string strPreColumnName_Src, string strItemIDs, ref DataTable dt_Result_Return)
    {
        //过滤出指定监测类别的Datatable
        DataTable dtTmp = FilterDataTable_byMonitorType(strMonitorType, dtResult);

        string strTempSampleID = "";
        for (int i = 0; i < dtTmp.Rows.Count; i++)
        {
            if (strTempSampleID == dtTmp.Rows[i]["SAMPLE_ID"].ToString())
                continue;
            strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();

            DataRow dr = dt_Result_Return.NewRow();
            DataRow drSrc = dtTmp.Rows[i];

            infullPreColumn_Value(drSrc, strPreColumnName, strPreColumnName_Src, ref dr);
            infullResultColumn_Value(dtTmp, strPreColumnName, strTempSampleID, strItemIDs, ref dr);

            dt_Result_Return.Rows.Add(dr);
        }
    }

    //给前几个固定列赋值，比如“监测日期,监测点位,样品编号,样品描述”
    private void infullPreColumn_Value(DataRow drSrc, string strPreColumnName, string strPreColumnName_Src, ref DataRow dr)
    {
        string[] arrPre = strPreColumnName.Split(',');
        string[] arrPreSrc = strPreColumnName_Src.Split(',');
        for (int i = 0; i < arrPre.Length; i++)
        {
            dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString().Replace("0:00:00", "");
        }
    }

    //给结果列赋值
    private void infullResultColumn_Value(DataTable dtTmp, string strPreColumnName, string strTempSampleID, string strItemIDs, ref DataRow dr)
    {
        string[] arrPre = strPreColumnName.Split(',');
        string[] arrItemIds = strItemIDs.Split(',');
        int iPreColumnCount = arrPre.Length;

        DataRow[] drSrc = dtTmp.Select("SAMPLE_ID='" + strTempSampleID + "'");

        for (int i = 0; i < drSrc.Length; i++)
        {
            string strTmpItemId = drSrc[i]["ITEM_ID"].ToString();
            //从column中找到对应该item的列，在该列该行填值
            for (int j = 0; j < arrItemIds.Length; j++)
            {
                if (arrItemIds[j] == strTmpItemId)
                {
                    int iColumnIdx = iPreColumnCount + j;//加上前面的固定列
                    dr[iColumnIdx] = drSrc[i]["ITEM_RESULT"].ToString();
                }
            }
        }
    }

    //过滤出指定监测类别的Datatable
    private DataTable FilterDataTable_byMonitorType(string strMonitorType, DataTable dtResult)
    {
        DataRow[] drs = dtResult.Select();
        if (!string.IsNullOrEmpty(strMonitorType))
        {
            drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
        }
        DataTable dtTmp = new DataTable();

        for (int i = 0; i < dtResult.Columns.Count; i++)
        {
            dtTmp.Columns.Add(dtResult.Columns[i].ColumnName, System.Type.GetType("System.String"));
        }

        for (int i = 0; i < drs.Length; i++)
        {
            DataRow drtmp = dtTmp.NewRow();
            for (int j = 0; j < dtResult.Columns.Count; j++)
            {
                drtmp[j] = drs[i][j].ToString();
            }

            dtTmp.Rows.Add(drtmp);
        }

        return dtTmp;
    }
    #endregion

    #region 判断是否环境质量类别任务
    [WebMethod]
    public static string GetValidateEnvMonitor(string strValue)
    {
        if (new Channels_Mis_Report_ReportZZ_ReportTechCheck().ValidateEnvMonitor(strValue))
        {
            return "true";
        }
        return "false";
    }

    /// <summary>
    ///功能描述：判断是否环境质量类别任务
    ///创建时间：2013-5-16
    ///创建人：邵世卓
    /// </summary>
    /// <param name="strTaskID">任务ID</param>
    /// <returns>bool</returns>
    protected bool ValidateEnvMonitor(string strTaskID)
    {
        //任务的监测类别
        DataTable dtSubtask = new TMisMonitorSubtaskLogic().getMonitorByTask(strTaskID);
        foreach (DataRow dr in dtSubtask.Rows)
        {
            if (!string.IsNullOrEmpty(new TSysDictLogic().GetDictNameByDictCodeAndType(dr[TMisMonitorSubtaskVo.MONITOR_ID_FIELD].ToString(), "EnvTypes")))
            {
                return true;//是环境质量类别
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    #endregion

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList == null || myServiceList.Count <= 0)
            return;
        this.TASK_ID.Value = myServiceList[0].SERVICE_KEY_VALUE;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        string strAcceptance = new TSysDictLogic().GetDictNameByDictCodeAndType("acceptance_code", "dict_system_base");//默认的固定验收监测类别
        if (objTask.CONTRACT_TYPE == strAcceptance)
        {
            wfControl.SetMoreDealUserFlag = true;//自定义任务接收人

            if (!string.IsNullOrEmpty(this.PROJECT_ID.Value))
            {
                wfControl.MoreDealUserForAdd(this.PROJECT_ID.Value);
                return true;
            }
            else
            {
                strMsg = "该项目无项目负责人，请联系管理员！";
                return false;
            }
        }
        // 环境质量类别特殊处理 直接完成流程
        if (ValidateEnvMonitor(this.TASK_ID.Value))
        {
            //更改监测任务状态
            objTask.TASK_STATUS = "12";
            objTask.FINISH_DATE = DateTime.Now.ToString();
            if (new TMisMonitorTaskLogic().Edit(objTask))
            {
                WriteLog("完成监测任务", "", LogInfo.UserInfo.ID + "完成监测任务" + objTask.ID);
            }
            //销毁的处理方法
            //调用页面基类的处理方法即可
            string strWfInstID = new i3.BusinessLogic.Sys.WF.TWfInstControlLogic().Details(new TWfInstControlVo()
            {
                WF_SERVICE_CODE = objTask.CONTRACT_CODE,
                WF_SERVICE_NAME = objTask.PROJECT_NAME
            }).ID;
            if (new PageBaseForWF().WFOperateForKillBySSZ(strWfInstID))//终止整个任务流程
            {
                LigerDialogAlert("任务已结束！", "success");
            }
            //Response.Redirect(ViewState["BeforePageUrl"].ToString());
            return true;
        }
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        #region 初始化环节信息
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        wfControl.ServiceCode = objTask.CONTRACT_CODE;
        wfControl.ServiceName = objTask.PROJECT_NAME;
        #endregion

        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("报告流程", "task_id", this.TASK_ID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
        //SaveBusinessData();
    }
    #endregion
}