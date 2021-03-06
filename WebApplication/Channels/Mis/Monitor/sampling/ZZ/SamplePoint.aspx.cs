﻿using System;
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
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
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
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：采样-监测点位信息
/// 创建日期：2012-12-14
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_ZZ_SamplePoint : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "", strSubTask_Id = "";
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
                strSubTask_Id = this.SUBTASK_ID.Value.ToString();
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
        objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, intPageIndex, intPageSize);
        dtSample = dt.Clone();

        //定义样品计数器
        int c = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            ////清远 采样时自动生成样品编号（原始样）
            //TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());
            //if (objSample.SAMPLE_CODE.Length == 0)
            //{
            //    objSample.SAMPLE_CODE = GetSampleCode(dt.Rows[i]["ID"].ToString());
            //    new TMisMonitorSampleInfoLogic().Edit(objSample);
            //    objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
            //    dtSample.Rows[i + c]["SAMPLE_CODE"] = objSample.SAMPLE_CODE;
            //}
            objSampleInfo.QC_TYPE = "";
            objSampleInfo.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSampleInfo.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                c++;//质控样+1
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                ////清远 生成样品编号（质控）
                //TMisMonitorSampleInfoVo objSampleQc = new TMisMonitorSampleInfoLogic().Details(dr["ID"].ToString());
                //if (objSampleQc.SAMPLE_CODE.Length == 0)
                //{
                //    objSampleQc.SAMPLE_CODE = GetSampleCode(dr["ID"].ToString());
                //    objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
                //    new TMisMonitorSampleInfoLogic().Edit(objSampleQc);
                //    dr["SAMPLE_CODE"] = objSampleQc.SAMPLE_CODE;
                //}
                dtSample.ImportRow(dr);
            }

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

    /// <summary>
    /// 更新样品的特殊说明
    /// </summary>
    /// <param name="strValue"></param>
    /// <param name="strRemark"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveRemark(string strValue, string strRemark)
    {
        string result = "";
        TMisMonitorSampleInfoVo objItems = new TMisMonitorSampleInfoVo();
        objItems.ID = strValue;
        objItems.SPECIALREMARK = strRemark;
        if (new TMisMonitorSampleInfoLogic().Edit(objItems))
        {
            result = "true";
        }
        return result;
    }

    //编辑点位数据
    [WebMethod]
    public static string SaveData(string strPointID, string strSubtaskID, string strPOINT_NAME, string strMONITOR_ID, string strPOINT_TYPE, string strDYNAMIC_ATTRIBUTE_ID,string strSAMPLEFREQ,
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

            //新增点位时候，自动生成该点位的样品编码
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SAMPLE_SOURCE = objTask.SAMPLE_SOURCE;
            objSerial.SERIAL_TYPE = "2";

            objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");

            objSample.SAMPLE_CODE = CreateBaseDefineCodeForSample(objSerial, objTask, objSubtask);

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

                if (objItemAnalysis.ID.Length > 0)
                {
                    //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                    objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                    //objMethod.METHOD_ID = objMethod.METHOD_ID;
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
                    objMethod.METHOD_ID = objMethod.METHOD_ID;
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

    #region 样品生成规则
    /// <summary>
    /// 功能描述：样品生成规则
    /// </summary>
    /// <param name="strSampleID"></param>
    /// <returns></returns>
    //protected static string GetSampleCode(string strSampleID)
    //{
    //    string strSampleCode = "";
    //    TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(strSampleID);
    //    TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
    //    TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

    //    string strSerialName = "", strSampleCodeNum = "";
    //    if (objTask.SAMPLE_SOURCE == "2")
    //    {
    //        strSerialName = "G";
    //    }
    //    else
    //        strSerialName = "X";
    //    if (objSubtask.MONITOR_ID == "000000001")
    //    {
    //        strSerialName += "W";
    //    }
    //    else if (objSubtask.MONITOR_ID == "000000002")
    //    {
    //        strSerialName += "Q";
    //    }
    //    else if (objSubtask.MONITOR_ID == "000000003")
    //    {
    //        strSerialName += "F";
    //    }
    //    else
    //    {
    //        strSerialName += "W";
    //    }
    //    strSampleCodeNum = i3.View.PageBase.GetSerialNumber(strSerialName + DateTime.Now.Year.ToString());
    //    if (strSampleCodeNum.Length == 0)
    //    {
    //        TSysSerialVo sv = new TSysSerialVo();
    //        sv.SERIAL_CODE = strSerialName + DateTime.Now.Year.ToString();
    //        sv.SERIAL_NAME = strSerialName + DateTime.Now.Year.ToString();
    //        sv.SERIAL_NUMBER = "1";
    //        sv.LENGTH = "4";
    //        sv.GRANULARITY = "1";
    //        sv.MIN = "1";
    //        sv.MAX = new string('9', 4);
    //        sv.CREATE_TIME = System.DateTime.Now.ToString();
    //        new TSysSerialLogic().Create(sv);
    //        strSampleCodeNum = i3.View.PageBase.GetSerialNumber(strSerialName + DateTime.Now.Year.ToString());
    //    }

    //    strSampleCode = strSerialName + strSampleCodeNum;

    //    return strSampleCode;
    //}

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
    #endregion

    #region 质控类型
    /// <summary>
    /// 质控类型
    /// </summary>
    /// <param name="strQcType"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetQcType(string strValue)
    {
        string strReturn = "";
        switch (strValue)
        {
            case "0":
                strReturn = "原始样";
                break;
            case "1":
                strReturn = "现场空白";
                break;
            case "2":
                strReturn = "现场加标";
                break;
            case "3":
                strReturn = "现场平行";
                break;
            case "4":
                strReturn = "实验室密码平行";
                break;
            case "5":
                strReturn = "实验室空白";
                break;
            case "6":
                strReturn = "实验室加标";
                break;
            case "7":
                strReturn = "实验室明码平行";
                break;
            case "8":
                strReturn = "标准样";
                break;
        }
        return strReturn;
    }
    #endregion

    /// <summary>
    /// 样品信息保存
    /// </summary>
    /// <param name="id"></param>
    /// <param name="strSubtaskID">子任务ID</param>
    /// <param name="strSampleCode">样品编号</param>
    /// <param name="strSampleName">样品名称</param>
    /// <param name="strOriginalNum">原始单号</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSample(string id, string strSubtaskID, string strSampleCode, string strSampleName, string strOriginalNum, string strSampleDes)
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample = new TMisMonitorSampleInfoLogic().Details(id);
        objSample.SAMPLE_CODE = strSampleCode;
        objSample.SAMPLE_NAME = strSampleName;
        objSample.REMARK2 = strOriginalNum == "" ? "###" : strOriginalNum;
        objSample.REMARK3 = strSampleDes == "" ? "###" : strSampleDes;
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
    /// 获取质控原样编号
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcSourceCode(string strValue)
    {
        TMisMonitorSampleInfoVo objSampleInfoVo = new TMisMonitorSampleInfoLogic().Details(strValue);
        return objSampleInfoVo.SAMPLE_CODE;
    }
}