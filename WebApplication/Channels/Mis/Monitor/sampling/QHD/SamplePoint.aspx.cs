using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

/// <summary>
/// 功能描述：采样-监测点位信息
/// 创建日期：2012-12-14
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QHD_SamplePoint : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
            }

            //获取点位信息
            if (Request["type"] != null && Request["type"].ToString() == "getPoint")
            {
                strResult = getPointList();
                Response.Write(strResult);
                Response.End();
            }

            //获取指定点位的监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "GetItems")
            {
                strResult = getItemList();
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目现场采样仪器信息
            if (Request["type"] != null && Request["type"].ToString() == "getSamplingInstrument")
            {
                strResult = getSamplingInstrument();
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    //获取点位信息
    private string getPointList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TMisMonitorTaskPointVo.NUM_FIELD;

        string strSubtaskID = this.SUBTASK_ID.Value;
        if (strSubtaskID.Length == 0)
            return "";

        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubtaskID;
        //objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "ISNULL(SAMPLE_COUNT,'9999'),POINT_ID,ID";
        objSampleInfo.NOSAMPLE = "0";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, intPageIndex, intPageSize);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            //objSampleInfo.QC_TYPE = "";
            //objSampleInfo.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            //objSampleInfo.SORT_FIELD = "QC_TYPE";
            //objSampleInfo.NOSAMPLE = "0";
            //DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            //for (int j = 0; j < dtQcSample.Rows.Count; j++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr = dtQcSample.Rows[j];
            //    dtSample.ImportRow(dr);
            //}
        }
        int intTotalCount = dtSample.Rows.Count;
        string strJson = CreateToJson(dtSample, intTotalCount);
        return strJson;
    }

    //获取指定点位的监测项目信息
    private string getItemList()
    {
        string strSampleID = Request.Params["strSampleID"];
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSelPointID = (Request.Params["selPointID"] != null) ? Request.Params["selPointID"] : "";
        if (strSelPointID.Length <= 0)
        {
            return "";
        }

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TMisMonitorTaskItemVo.ID_FIELD;

        TMisMonitorResultVo objResult = new TMisMonitorResultVo();
        if (strSampleID == null)
            strSampleID = strSelPointID;
        objResult.SAMPLE_ID = strSampleID;
        objResult.SORT_FIELD = strSortname;
        objResult.SORT_TYPE = strSortorder;
        DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResult, intPageIdx, intPagesize);
        DataColumn dcP;
        dcP = new DataColumn("TASK_POINT_ID", Type.GetType("System.String"));
        dt.Columns.Add(dcP);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["TASK_POINT_ID"] = strSelPointID;
        }
        int intTotalCount = new TMisMonitorResultLogic().GetSelectResultCount(objResult);

        //TMisMonitorTaskItemVo objPointItem = new TMisMonitorTaskItemVo();
        //objPointItem.IS_DEL = "0";
        //objPointItem.TASK_POINT_ID = strSelPointID;
        //objPointItem.SORT_FIELD = strSortname;
        //objPointItem.SORT_TYPE = strSortorder;
        //TMisMonitorTaskItemLogic logicPointItem = new TMisMonitorTaskItemLogic();

        //int intTotalCount = logicPointItem.GetSelectResultCount(objPointItem); ;//总计的数据条数
        //DataTable dt = logicPointItem.SelectByTable(objPointItem, intPageIdx, intPagesize);
        foreach (DataRow row in dt.Rows)
        {
            string strSamplingInstrumentId = row["SAMPLING_INSTRUMENT"] == null ? "" : row["SAMPLING_INSTRUMENT"].ToString();
            if (strSamplingInstrumentId != "")
            {
                string strSamplingInstrumentName = new TBaseItemSamplingInstrumentLogic().Details(strSamplingInstrumentId).INSTRUMENT_NAME;
                row["REMARK_1"] = strSamplingInstrumentName;
            }
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除点位信息
    [WebMethod]
    public static string deletePoint(string strValue)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strValue);

        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        objPoint.ID = objSample.POINT_ID;
        objPoint.IS_DEL = "1";
        bool isSuccess = new TMisMonitorTaskPointLogic().Edit(objPoint);

        TBaseAttrbuteValue3Vo objAttrValueDelWhere = new TBaseAttrbuteValue3Vo();
        objAttrValueDelWhere.OBJECT_ID = strValue;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValue3Vo objAttrValueDelSet = new TBaseAttrbuteValue3Vo();
        objAttrValueDelSet.IS_DEL = "1";
        new TBaseAttrbuteValue3Logic().Edit(objAttrValueDelSet, objAttrValueDelWhere);

        new TMisMonitorSampleInfoLogic().Delete(objSample);

        return isSuccess == true ? "1" : "0";
    }

    // 删除点位信息
    [WebMethod]
    public static string discontinued(string strValue)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strValue);
        string strSubTaskID = objSample.SUBTASK_ID;
        bool isSuccess = new TMisContractPlanPointLogic().DelPlanPointForSampleDistr(objSample.POINT_ID);
        if (isSuccess)
        {
            isSuccess = new TMisMonitorTaskPointLogic().Delete(objSample.POINT_ID);
            isSuccess = new TMisMonitorSampleInfoLogic().Delete(strValue);
            TMisMonitorResultVo objResult = new TMisMonitorResultVo();
            objResult.SAMPLE_ID = strValue;
            isSuccess = new TMisMonitorResultLogic().Delete(objResult);
        }
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
        objTaskPoint.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorTaskPointLogic().SelectByTable(objTaskPoint);
        if (dt.Rows.Count == 0)
        {
            new TMisMonitorSubtaskLogic().Delete(strSubTaskID);
            return "2";
        }

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 保存停产原因 胡方扬2013-03-13
    /// </summary>
    /// <param name="strValue"></param>
    /// <param name="strReason"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveStopReason(string strValue, string strReason)
    {
        string result = "";
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strValue);
        TMisContractPlanPointstopVo objItems = new TMisContractPlanPointstopVo();
        objItems.STOPRESON = strReason;
        objItems.ACTION_USERID = new PageBase().LogInfo.UserInfo.ID.ToString();
        objItems.ACTIONDATE = DateTime.Now.ToString();
        if (new TMisContractPlanPointstopLogic().InsertStopPointForSampleItems(objSample.POINT_ID, objItems))
        {
            result = "true";
        }
        return result;
    }

    //编辑点位数据
    [WebMethod]
    public static string SaveData(string strPointID, string strSubtaskID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID, string strSAMPLEFREQ,
        string strCREATE_DATE, string strADDRESS, string strLONGITUDE, string strLATITUDE, string strNUM, string strAttribute,
        string strNATIONAL_ST_CONDITION_ID, string strLOCAL_ST_CONDITION_ID, string strINDUSTRY_ST_CONDITION_ID)
    {
        bool isSuccess = true;

        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        objPoint.ID = strPointID.Length > 0 ? strPointID : GetSerialNumber("t_mis_monitor_taskpointId");
        objPoint.IS_DEL = "0";
        objPoint.SUBTASK_ID = strSubtaskID;
        objPoint.POINT_NAME = strPOINT_NAME;
        objPoint.MONITOR_ID = strMONITOR_ID;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.FREQ = "1";
        objPoint.SAMPLE_FREQ = strSAMPLEFREQ;
        objPoint.CREATE_DATE = strCREATE_DATE;
        objPoint.ADDRESS = strADDRESS;
        objPoint.LONGITUDE = strLONGITUDE;
        objPoint.LATITUDE = strLATITUDE;
        objPoint.NUM = strNUM;

        objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
        objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
        objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        objPoint.TASK_ID = objTask.ID;

        //监测任务出现新增排口时，基础资料企业表也要新增
        TBaseCompanyPointVo objnewPoint = new TBaseCompanyPointVo();
        if (strPointID.Length == 0)
        {
            objnewPoint.ID = GetSerialNumber("t_base_company_point_id");
            objnewPoint.IS_DEL = "0";
            objnewPoint.POINT_NAME = strPOINT_NAME;
            objnewPoint.MONITOR_ID = strMONITOR_ID;
            objnewPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
            objnewPoint.FREQ = "1";
            objnewPoint.SAMPLE_FREQ = strSAMPLEFREQ;
            objnewPoint.CREATE_DATE = strCREATE_DATE;
            objnewPoint.ADDRESS = strADDRESS;
            objnewPoint.LONGITUDE = strLONGITUDE;
            objnewPoint.LATITUDE = strLATITUDE;
            objnewPoint.NUM = strNUM;

            objnewPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
            objnewPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
            objnewPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

            TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
            objTaskCompany.TASK_ID = objTask.ID; ;
            objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(objTaskCompany);

            TMisContractCompanyVo objContractCompany = new TMisContractCompanyLogic().Details(objTaskCompany.COMPANY_ID);
            objnewPoint.COMPANY_ID = objContractCompany.COMPANY_ID;

            new TBaseCompanyPointLogic().Create(objnewPoint);

            objPoint.POINT_ID = objnewPoint.ID;
        }

        if (strPointID.Length > 0)
            isSuccess = new TMisMonitorTaskPointLogic().Edit(objPoint);
        else
        {
            isSuccess = new TMisMonitorTaskPointLogic().Create(objPoint);

            //增加点位样品信息
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
            objSample.ID = GetSerialNumber("MonitorSampleId");
            objSample.SUBTASK_ID = strSubtaskID;
            objSample.QC_TYPE = "0";
            objSample.NOSAMPLE = "0";
            objSample.POINT_ID = objPoint.ID;
            objSample.SAMPLE_NAME = objPoint.POINT_NAME;

            new TMisMonitorSampleInfoLogic().Create(objSample);
        }

        TBaseAttrbuteValue3Logic logicAttrValue = new TBaseAttrbuteValue3Logic();

        //清掉原有动态属性值
        TBaseAttrbuteValue3Vo objAttrValueDelWhere = new TBaseAttrbuteValue3Vo();
        objAttrValueDelWhere.OBJECT_ID = objPoint.ID;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValue3Vo objAttrValueDelSet = new TBaseAttrbuteValue3Vo();
        objAttrValueDelSet.IS_DEL = "1";
        logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere);

        //新增动态属性值
        if (strAttribute.Length > 0)
        {
            string[] arrAttribute = strAttribute.Split('-');
            for (int i = 0; i < arrAttribute.Length; i++)
            {
                string[] arrAttrValue = arrAttribute[i].Split('|');

                TBaseAttrbuteValue3Vo objAttrValueAdd = new TBaseAttrbuteValue3Vo();
                objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrValueAdd.IS_DEL = "0";
                objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                objAttrValueAdd.OBJECT_ID = objPoint.ID;
                objAttrValueAdd.ATTRBUTE_CODE = arrAttrValue[1];
                objAttrValueAdd.ATTRBUTE_VALUE = arrAttrValue[2];
                isSuccess = logicAttrValue.Create(objAttrValueAdd);
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    //设置点位的监测项目数据
    [WebMethod]
    public static string SaveDataItem(string strSubtaskID, string strSample, string strSelItem_IDs)
    {
        bool isSuccess = true;

        string[] arrSelItemId = strSelItem_IDs.Split(',');

        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSample);
        TMisMonitorTaskItemVo objPointItemSet = new TMisMonitorTaskItemVo();
        objPointItemSet.IS_DEL = "1";
        TMisMonitorTaskItemVo objPointItemWhere = new TMisMonitorTaskItemVo();
        objPointItemWhere.IS_DEL = "0";
        objPointItemWhere.TASK_POINT_ID = objSample.POINT_ID;
        new TMisMonitorTaskItemLogic().Edit(objPointItemSet, objPointItemWhere);


        TMisMonitorResultVo objResult = new TMisMonitorResultVo();

        objResult = new TMisMonitorResultVo();
        if (strSample.Length > 0)
        {
            objResult.SAMPLE_ID = strSample;
            new TMisMonitorResultLogic().Delete(objResult);
        }


        if (strSelItem_IDs.Length > 0)
        {
            for (int i = 0; i < arrSelItemId.Length; i++)
            {
                TMisMonitorTaskItemVo objPointItem = new TMisMonitorTaskItemVo();
                objPointItem.ID = GetSerialNumber("t_mis_monitor_task_item_id");
                objPointItem.IS_DEL = "0";
                objPointItem.TASK_POINT_ID = objSample.POINT_ID;
                objPointItem.ITEM_ID = arrSelItemId[i];

                isSuccess = new TMisMonitorTaskItemLogic().Create(objPointItem);

                objResult = new TMisMonitorResultVo();
                objResult.ID = GetSerialNumber("MonitorResultId");
                objResult.SAMPLE_ID = objSample.ID;
                objResult.ITEM_ID = arrSelItemId[i];
                objResult.QC_TYPE = objSample.QC_TYPE;
                objResult.RESULT_STATUS = "01";

                //填充默认分析方法和方法依据
                TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
                objItemAnalysis.ITEM_ID = arrSelItemId[i];
                objItemAnalysis.IS_DEFAULT = "是";
                objItemAnalysis.IS_DEL = "0";
                objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);

                if (objItemAnalysis.ID.Length == 0)
                {
                    objItemAnalysis = new TBaseItemAnalysisVo();
                    objItemAnalysis.ITEM_ID = arrSelItemId[i];
                    objItemAnalysis.IS_DEL = "0";
                    objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                }

                if (objItemAnalysis.ID.Length > 0)
                {
                    //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                    objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                    //objResult.STANDARD_ID = objMethod.METHOD_ID;
                }

                isSuccess = new TMisMonitorResultLogic().Create(objResult);

                string strAnalysisManagerID = "";
                string strAnalysisManID = "";
                TMisMonitorResultVo objResultTemp = new TMisMonitorResultVo();
                objResultTemp.ID = objResult.ID;
                DataTable dtManager = new TMisMonitorResultLogic().SelectManagerByTable(objResultTemp);
                if (dtManager.Rows.Count > 0)
                {
                    strAnalysisManagerID = dtManager.Rows[0]["ANALYSIS_MANAGER"].ToString();
                    strAnalysisManID = dtManager.Rows[0]["ANALYSIS_ID"].ToString();
                }
                TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                objResultApp.ID = GetSerialNumber("MonitorResultAppId");
                objResultApp.RESULT_ID = objResult.ID;
                objResultApp.HEAD_USERID = strAnalysisManagerID;
                objResultApp.ASSISTANT_USERID = strAnalysisManID;

                isSuccess = new TMisMonitorResultAppLogic().Create(objResultApp);
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    //复制监测项目
    [WebMethod]
    public static string CopyPointItem(string strCopyID, string strPastID, string strSubtaskID)
    {
        bool isSuccess = true;
        string strCopyPointID = new TMisMonitorSampleInfoLogic().Details(strCopyID).POINT_ID;
        string strPastPointID = new TMisMonitorSampleInfoLogic().Details(strPastID).POINT_ID;

        TMisMonitorTaskItemVo objPointItemCopy = new TMisMonitorTaskItemVo();
        objPointItemCopy.IS_DEL = "0";
        objPointItemCopy.TASK_POINT_ID = strCopyPointID;
        DataTable dtCopy = new TMisMonitorTaskItemLogic().SelectByTable(objPointItemCopy);

        TMisMonitorTaskItemVo objPointItemPast = new TMisMonitorTaskItemVo();
        objPointItemPast.IS_DEL = "0";
        objPointItemPast.TASK_POINT_ID = strPastPointID;
        DataTable dtPast = new TMisMonitorTaskItemLogic().SelectByTable(objPointItemPast);

        string strIsExistItem = "";
        for (int i = 0; i < dtPast.Rows.Count; i++)
        {
            strIsExistItem += "," + dtPast.Rows[i]["ITEM_ID"].ToString();
        }
        strIsExistItem += strIsExistItem.Length > 0 ? "," : "";

        //获取粘贴样品ID，填充结果表数据
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strPastID);

        for (int i = 0; i < dtCopy.Rows.Count; i++)
        {
            DataRow dr = dtCopy.Rows[i];
            string strCopyItemID = dr["ITEM_ID"].ToString();
            if (!strIsExistItem.Contains(strCopyItemID))
            {
                TMisMonitorTaskItemVo objPointItem = new TMisMonitorTaskItemVo();
                objPointItem.ID = GetSerialNumber("t_base_company_point_item_id");
                objPointItem.IS_DEL = "0";
                objPointItem.TASK_POINT_ID = strPastPointID;
                objPointItem.ITEM_ID = strCopyItemID;

                isSuccess = new TMisMonitorTaskItemLogic().Create(objPointItem);

                TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                objResult.ID = GetSerialNumber("MonitorResultId");
                objResult.SAMPLE_ID = objSample.ID;
                objResult.ITEM_ID = strCopyItemID;
                objResult.QC_TYPE = objSample.QC_TYPE;
                objResult.RESULT_STATUS = "01";

                //填充默认分析方法和方法依据
                TBaseItemAnalysisVo objItemAnalysis = new TBaseItemAnalysisVo();
                objItemAnalysis.ITEM_ID = strCopyItemID;
                objItemAnalysis.IS_DEFAULT = "是";
                objItemAnalysis.IS_DEL = "0";
                objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);

                if (objItemAnalysis.ID.Length > 0)
                {
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                    objResult.ANALYSIS_METHOD_ID = objMethod.ID;

                    //updated by xwh 2013.04.18 解决复制监测项目的时候不能设置方法依据的错误
                    //objMethod.METHOD_ID = objMethod.METHOD_ID;
                    objResult.STANDARD_ID = objMethod.METHOD_ID;
                }

                isSuccess = new TMisMonitorResultLogic().Create(objResult);

                string strAnalysisManagerID = "";
                string strAnalysisManID = "";
                TMisMonitorResultVo objResultTemp = new TMisMonitorResultVo();
                objResultTemp.ID = objResult.ID;
                DataTable dtManager = new TMisMonitorResultLogic().SelectManagerByTable(objResultTemp);
                if (dtManager.Rows.Count > 0)
                {
                    strAnalysisManagerID = dtManager.Rows[0]["ANALYSIS_MANAGER"].ToString();
                    strAnalysisManID = dtManager.Rows[0]["ANALYSIS_ID"].ToString();
                }
                TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                objResultApp.ID = GetSerialNumber("MonitorResultAppId");
                objResultApp.RESULT_ID = objResult.ID;
                objResultApp.HEAD_USERID = strAnalysisManagerID;
                objResultApp.ASSISTANT_USERID = strAnalysisManID;

                isSuccess = new TMisMonitorResultAppLogic().Create(objResultApp);
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 样品信息保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSample(string id, string strSubtaskID, string strSampleCode, string strSampleName, string strSampleRemark)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample = new TMisMonitorSampleInfoLogic().Details(id);
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.SAMPLE_NAME = strSampleName;
        objSample.SAMPLE_REMARK = strSampleRemark;
        bool isSuccess = new TMisMonitorSampleInfoLogic().Edit(objSample);

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取企业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strValue)
    {
        return new TMisMonitorTaskCompanyLogic().Details(strValue).COMPANY_NAME;
    }

    /// <summary>
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorName(string strValue)
    {
        return new TBaseMonitorTypeInfoLogic().Details(strValue).MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 获取点位信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strValue)
    {
        return new TMisMonitorTaskPointLogic().Details(strValue).POINT_NAME;
    }

    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemName(string strValue)
    {
        return new TBaseItemInfoLogic().Details(strValue).ITEM_NAME;
    }
    /// <summary>
    /// 获取监测项目是否是现场项目
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemDept(string strValue)
    {
        return new TBaseItemInfoLogic().Details(strValue).IS_SAMPLEDEPT;
    }
    public string getSamplingInstrument()
    {
        string strItemId = Request["strItemId"].ToString();
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVo = new TBaseItemSamplingInstrumentVo();
        TBaseItemSamplingInstrumentVo.ITEM_ID = strItemId;
        TBaseItemSamplingInstrumentVo.IS_DEL = "0";
        DataTable objTable = new TBaseItemSamplingInstrumentLogic().SelectByTable(TBaseItemSamplingInstrumentVo);
        return DataTableToJson(objTable);
    }
    /// <summary>
    /// 保存现场分析仪器信息
    /// </summary>
    /// <param name="id">监测结果ID</param>
    /// <param name="strSamplingInstrumentId">现场分析仪器ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSamplingInstrument(string id, string strSamplingInstrumentId)
    {
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = id;
        TMisMonitorResultVo.SAMPLING_INSTRUMENT = strSamplingInstrumentId == "" ? "###" : strSamplingInstrumentId;
        bool isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);
        return isSuccess == true ? "1" : "0";
    }

    #region 打印
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strPrintId = this.strPrintId.Value;
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = strPrintId;
        DataTable dtSub = new TMisMonitorSubtaskLogic().SelectByTable(objSubTask);
        string strSampleIDs = "";
        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            GetPoint_UnderTask(dtSub.Rows[i]["ID"].ToString(), ref strSampleIDs);
        }
        //获取基本信息
        DataTable dt = new TMisMonitorSampleInfoLogic().getSamplingAllocationSheetInfoBySampleId(strSampleIDs, "02,021", "0");

        int iPageCount = dt.Rows.Count / 10;
        if (dt.Rows.Count % 10 != 0)
            iPageCount += 1;

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/SampleHandover.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        //sheet复制
        for (int k = 1; k < iPageCount; k++)
        {
            hssfworkbook.CloneSheet(0);
            hssfworkbook.SetSheetName(k, "Sheet" + (k + 1).ToString());
        }
        for (int m = 1; m <= iPageCount; m++)
        {
            ISheet sheet = hssfworkbook.GetSheet("Sheet" + m.ToString());
            sheet.GetRow(1).GetCell(10).SetCellValue(dt.Rows[0]["TICKET_NUM"].ToString());
            //sheet.GetRow(2).GetCell(0).SetCellValue(string.Format("  采 样 日 期：  {0}   年  {1}   月  {2}   日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            //sheet.GetRow(3).GetCell(0).SetCellValue(string.Format(" 样品交接日期：   {0}   年  {1}   月  {2}   日   {3}  点   {4}  分", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString()));
            //sheet.GetRow(1).GetCell(6).SetCellValue(string.Format("第 {0} 页 共 {1} 页", m.ToString(), iPageCount.ToString()));

            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.Clear();
            for (int n = (m - 1) * 10; n < m * 10; n++)
            {
                if (n >= dt.Rows.Count)
                    break;
                dtNew.ImportRow(dt.Rows[n]);
            }
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                //string strItmeNum = "";
                //TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                //objResult.RESULT_STATUS = "01";
                //objResult.SAMPLE_ID = dtNew.Rows[i]["ID"].ToString();
                //DataTable dtResult = new TMisMonitorResultLogic().SelectByTable(objResult);
                //for (int j = 0; j < dtResult.Rows.Count; j++)
                //{
                //    TBaseItemInfoVo objItem = new TBaseItemInfoVo();
                //    objItem.ID = dtResult.Rows[j]["ITEM_ID"].ToString();
                //    DataTable dtItem = new TBaseItemInfoLogic().SelectByTable(objItem);
                //    if (dtItem.Rows.Count > 0 && dtItem.Rows[0]["ITEM_NUM"].ToString().Length > 0)
                //        strItmeNum += (strItmeNum.Length > 0) ? "," + dtItem.Rows[0]["ITEM_NUM"].ToString() : dtItem.Rows[0]["ITEM_NUM"].ToString();
                //}
                sheet.GetRow(i + 4).GetCell(0).SetCellValue(dtNew.Rows[i]["SAMPLE_NAME"].ToString());
                //sheet.GetRow(i + 6).GetCell(1).SetCellValue(dtNew.Rows[i]["SAMPLE_CODE"].ToString());
                //sheet.GetRow(i + 6).GetCell(6).SetCellValue(strItmeNum);
            }
        }
        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("样品交接记录表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    //点位
    private void GetPoint_UnderTask(string strSubTaskID, ref string strSampleIDs)
    {
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubTaskID;
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strSampleIDs.Contains(dt.Rows[i]["ID"].ToString()))
            {
                strSampleIDs += (strSampleIDs.Length > 0 ? "','" : "") + dt.Rows[i]["ID"].ToString();
            }
        }
    }
    #endregion

}