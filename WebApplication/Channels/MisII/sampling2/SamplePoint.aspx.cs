using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
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
using System.IO;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using WebApplication;

/// <summary>
/// 功能描述：采样-监测点位信息
/// 创建日期：2012-12-14
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_MisII_sampling2_SamplePoint : PageBase
{
    public string strItemId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "", strSubTask_ID = "";
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                strSubTask_ID = Request.QueryString["strSubtaskID"].ToString();
                this.SUBTASK_ID.Value =strSubTask_ID;

                if (!GetSubTaskAtt(strSubTask_ID))
                {
                    CopyPointMap(GetPointMap(strSubTask_ID));
                }
            }

            //获取点位信息
            if (Request["type"] != null && Request["type"].ToString() == "getPoint")
            {
                strResult = getPointList();
                Response.Write(strResult);
                Response.End();
            }

            //获取子任务基础资料企业ID信息
            if (Request["type"] != null && Request["strSubTaskId"] != null && Request["type"].ToString() == "getCompanyID")
            {
                strResult = getCompanyID(Request["strSubTaskId"].ToString());
                Response.Write(strResult);
                Response.End();
            }

            if (Request["type"] != null && Request["strSubTaskId"] != null && Request["type"].ToString() == "DelFile")
            {
                strResult = DelFile(Request["strSubTaskId"].ToString());
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

            if (Request["type"] != null && Request["strItemId"] != null && Request["type"].ToString() == "getBaseItemInfor")
            {
                strItemId = Request["strItemId"].ToString();
                strResult = getBaseItemInfor();
                Response.Write(strResult);
                Response.End();
            }

            //获取点位信息
            if (Request["type"] != null && Request["type"].ToString() == "GetAttData")
            {
                strResult = GetAttData();
                Response.Write(strResult);
                Response.End();
            }

            //获取点位信息
            if (Request["type"] != null && Request["type"].ToString() == "GetDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }

            if (Request["type"] != null && Request["type"].ToString() == "getDustInfor")
            {
                strResult = getDustInfor(Request["strSO2"].ToString(), Request["strNOX"].ToString());
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

        var strCCflowWorkId = Request.QueryString["strCCflowWorkId"];
        var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, Convert.ToInt64( strCCflowWorkId));

        var sampleIdList = identification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {

             sampleIdList.RemoveAt(0); 
        }

        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = strSubtaskID;
        objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, intPageIndex, intPageSize);
        int intTotalCount = new TMisMonitorSampleInfoLogic().SelectByTableForPointCount(objSampleInfo); 
        dtSample = dt.Clone();

        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubtaskVo.TASK_ID);

        //定义样品计数器
        int c = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //if(!sampleIdList.Contains(dt.Rows[i]["ID"].ToString()))
                
            //    continue;
            //}

            if (!sampleIdList.Contains(dt.Rows[i]["ID"].ToString()))
            {
                continue;
            }

            dtSample.ImportRow(dt.Rows[i]);
            //清远 采样时自动生成样品编号（原始样）
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(dt.Rows[i]["ID"].ToString());
            if (objSample.SAMPLE_CODE.Length == 0)
            {
                if (objTaskVo.TASK_TYPE == "1")
                    objSample.SAMPLE_CODE = objSample.SAMPLE_NAME;
                else
                    objSample.SAMPLE_CODE = GetSampleCode(dt.Rows[i]["ID"].ToString());
                objSample.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
                new TMisMonitorSampleInfoLogic().Edit(objSample);
                dtSample.Rows[i + c]["SAMPLE_CODE"] = objSample.SAMPLE_CODE;
            }
            objSampleInfo.QC_TYPE = "";
            objSampleInfo.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSampleInfo.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                c++;//质控样+1
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                //清远 生成样品编号（质控）
                TMisMonitorSampleInfoVo objSampleQc = new TMisMonitorSampleInfoLogic().Details(dr["ID"].ToString());
                if (objSampleQc.SAMPLE_CODE.Length == 0)
                {
                    if (objTaskVo.TASK_TYPE == "1")
                        objSample.SAMPLE_CODE = objSample.SAMPLE_NAME;
                    else
                        objSampleQc.SAMPLE_CODE = GetSampleCode(dr["ID"].ToString());
                    objSampleQc.SAMPLECODE_CREATEDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    new TMisMonitorSampleInfoLogic().Edit(objSampleQc);
                    dr["SAMPLE_CODE"] = objSampleQc.SAMPLE_CODE;
                }
                dtSample.ImportRow(dr);
            }

        }
        objSampleInfo.QC_TYPE = "11";
        objSampleInfo.QC_SOURCE_ID = "";
        dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSampleInfo, 0, 0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
        }

        if (objSubtaskVo.MONITOR_ID == "000000001")
        {
            //流量测定情况属性 感官描述属性 
            dt = new TBaseAttributeInfoLogic().GetAttDate("'000000017','000000210'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtSample.Columns.Add(dt.Rows[i]["ID"].ToString() + "@" + dt.Rows[i]["CONTROL_ID"].ToString() + "@" + dt.Rows[i]["ATTRIBUTE_NAME"].ToString() + "@" + dt.Rows[i]["CONTROL_NAME"].ToString() + "@" + dt.Rows[i]["DICTIONARY"].ToString(), typeof(string));
            }
            for (int i = 0; i < dtSample.Rows.Count; i++)
            {
                dt = new TBaseAttributeInfoLogic().GetAttValue("'000000017','000000210'", dtSample.Rows[i]["POINT_ID"].ToString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dtSample.Rows[i][dt.Rows[j]["ID"].ToString() + "@" + dt.Rows[j]["CONTROL_ID"].ToString() + "@" + dt.Rows[j]["ATTRIBUTE_NAME"].ToString() + "@" + dt.Rows[j]["CONTROL_NAME"].ToString() + "@" + dt.Rows[j]["DICTIONARY"].ToString()] = dt.Rows[j]["ATTRBUTE_VALUE"].ToString();
                }
            }
        }
        if (objSubtaskVo.MONITOR_ID == "EnvRiver")
        {
            //流量测定情况属性 感官描述属性 
            dt = new TBaseAttributeInfoLogic().GetAttDate("'000000211'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtSample.Columns.Add(dt.Rows[i]["ID"].ToString() + "@" + dt.Rows[i]["CONTROL_ID"].ToString() + "@" + dt.Rows[i]["ATTRIBUTE_NAME"].ToString() + "@" + dt.Rows[i]["CONTROL_NAME"].ToString() + "@" + dt.Rows[i]["DICTIONARY"].ToString(), typeof(string));
            }
            for (int i = 0; i < dtSample.Rows.Count; i++)
            {
                dt = new TBaseAttributeInfoLogic().GetAttValue("'000000211'", dtSample.Rows[i]["POINT_ID"].ToString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dtSample.Rows[i][dt.Rows[j]["ID"].ToString() + "@" + dt.Rows[j]["CONTROL_ID"].ToString() + "@" + dt.Rows[j]["ATTRIBUTE_NAME"].ToString() + "@" + dt.Rows[j]["CONTROL_NAME"].ToString() + "@" + dt.Rows[j]["DICTIONARY"].ToString()] = dt.Rows[j]["ATTRBUTE_VALUE"].ToString();
                }
            }
        }
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
            //return "";
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

        TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoLogic().Details(strSampleID);
        TMisMonitorSubtaskVo SubtaskVo = new TMisMonitorSubtaskLogic().Details(SampleInfoVo.SUBTASK_ID);
        TMisMonitorTaskVo TaskVo = new TMisMonitorTaskLogic().Details(SubtaskVo.TASK_ID);

        DataColumn dcP;
        dcP = new DataColumn("TASK_POINT_ID", Type.GetType("System.String"));
        dt.Columns.Add(dcP);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["TASK_POINT_ID"] = strSelPointID;
            dt.Rows[i]["REMARK_1"] = TaskVo.TEST_PURPOSE;
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
    public static string SaveRemark(string strValue, string strRemark) {
        string result="";
        TMisMonitorSampleInfoVo objItems = new TMisMonitorSampleInfoVo();
        objItems.ID = strValue;
        objItems.SPECIALREMARK = strRemark;
        if (new TMisMonitorSampleInfoLogic().Edit(objItems)) {
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
    /// <summary>
    /// 保存点位的动态属性方法（如烟气黑度） Create By weilin 2014-11-12
    /// </summary>
    /// <param name="strPointID"></param>
    /// <param name="strAttribute"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveDataAttr(string strPointID, string strAttrID, string strAttribute)
    {
        bool isSuccess = true;
        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        objPoint.ID = strPointID;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strAttrID;
        isSuccess = new TMisMonitorTaskPointLogic().Edit(objPoint);

        TBaseAttrbuteValue3Logic logicAttrValue = new TBaseAttrbuteValue3Logic();
        //清掉原有动态属性值
        TBaseAttrbuteValue3Vo objAttrValueDelWhere = new TBaseAttrbuteValue3Vo();
        objAttrValueDelWhere.OBJECT_ID = strPointID;
        objAttrValueDelWhere.IS_DEL = "0";
        TBaseAttrbuteValue3Vo objAttrValueDelSet = new TBaseAttrbuteValue3Vo();
        objAttrValueDelSet.IS_DEL = "1";
        logicAttrValue.Edit(objAttrValueDelSet, objAttrValueDelWhere);

        //新增动态属性值
        if (strAttribute.Length > 0)
        {
            string[] arrAttribute = strAttribute.Split('=');
            for (int i = 0; i < arrAttribute.Length; i++)
            {
                string[] arrAttrValue = arrAttribute[i].Split('|');

                TBaseAttrbuteValue3Vo objAttrValueAdd = new TBaseAttrbuteValue3Vo();
                objAttrValueAdd.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrValueAdd.IS_DEL = "0";
                objAttrValueAdd.OBJECT_TYPE = arrAttrValue[0];
                objAttrValueAdd.OBJECT_ID = strPointID;
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
                    objItemAnalysis.ITEM_ID = arrSelItemId[i];
                    objItemAnalysis.IS_DEL = "0";
                    objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                }
                //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                objResult.RESULT_CHECKOUT = objItemAnalysis.LOWER_CHECKOUT;
                //objMethod.METHOD_ID = objMethod.METHOD_ID;

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
                objPointItem.ID = GetSerialNumber("t_mis_monitor_task_item_id");
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

                if (objItemAnalysis.ID.Length == 0)
                {
                    objItemAnalysis.ITEM_ID = strCopyItemID;
                    objItemAnalysis.IS_DEL = "0";
                    objItemAnalysis = new TBaseItemAnalysisLogic().Details(objItemAnalysis);
                }
                //TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(objItemAnalysis.ANALYSIS_METHOD_ID);
                objResult.ANALYSIS_METHOD_ID = objItemAnalysis.ID;
                objResult.RESULT_CHECKOUT = objItemAnalysis.LOWER_CHECKOUT;
                //objResult.STANDARD_ID = objMethod.METHOD_ID;

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
            case "11":
                strReturn = "密码盲样";
                break;
        }
        return strReturn;
    }
    #endregion

    /// <summary>
    /// 样品信息保存
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveSample(string id, string strPointID, string strCellName, string strCellValue)
    {
        bool isSuccess = false;
        if (!strCellName.Contains("@"))
        {
            isSuccess = new TMisMonitorSampleInfoLogic().UpdateSampleCell(id, strCellName, strCellValue);
        }
        else
        {
            if (strPointID == "")
            {
                isSuccess = false;
            }
            else
            {
                string[] strInfo = strCellName.Split('@');
                TBaseAttrbuteValue3Vo objAttValue = new TBaseAttrbuteValue3Vo();
                objAttValue.OBJECT_ID = strPointID;
                objAttValue.ATTRBUTE_CODE = strInfo[0].ToString();
                objAttValue.IS_DEL = "0";
                objAttValue = new TBaseAttrbuteValue3Logic().Details(objAttValue);
                if (objAttValue.ID == "")
                {
                    objAttValue.ID = GetSerialNumber("t_base_attribute_value3_id");
                    objAttValue.IS_DEL = "0";
                    objAttValue.OBJECT_ID = strPointID;
                    objAttValue.OBJECT_TYPE = strInfo[3].ToString();
                    objAttValue.ATTRBUTE_CODE = strInfo[0].ToString();
                    objAttValue.ATTRBUTE_VALUE = strCellValue;

                    isSuccess = new TBaseAttrbuteValue3Logic().Create(objAttValue);
                }
                else
                {
                    objAttValue.ATTRBUTE_VALUE = strCellValue;
                    isSuccess = new TBaseAttrbuteValue3Logic().Edit(objAttValue);
                }
            }
        }

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
    public string getBaseItemInfor( )
    {

        DataTable objDt = new TBaseItemInfoLogic().SelectByTable(new TBaseItemInfoVo { ID = strItemId });
        return LigerGridDataToJson(objDt, objDt.Rows.Count);
    }

    public string getDustInfor(string strSO2, string strNOX)
    {

        DataTable  objDt = new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = strSO2 });
        if (objDt.Rows.Count == 0)
        {
            objDt = new TMisMonitorDustinforLogic().SelectByTable(new TMisMonitorDustinforVo { SUBTASK_ID = strNOX });
        }

        return LigerGridDataToJson(objDt, objDt.Rows.Count);
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

    /// <summary>
    /// 创建原因：根据子任务ID获取当前企业噪声点位图
    /// 创建人：胡方扬
    /// 创建日期：2013-07-03
    /// </summary>
    /// <param name="strSubTask_ID"></param>
    /// <returns></returns>
    public DataTable GetPointMap(string strSubTask_ID) {
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strSubTask_ID);

        DataTable dt = new TMisMonitorSubtaskLogic().GetPointMapForSubTask(objSubTask);
        return dt;
    }

    /// <summary>
    /// 创建原因：复制噪声点位图到噪声子任务中
    /// 创建人：胡方扬
    /// 创建原因：2013-07-03
    /// </summary>
    /// <param name="dt"></param>
    public void CopyPointMap(DataTable dt) {
        string strFilePath = ConfigurationManager.AppSettings["AttPath"].ToString();
        if (!String.IsNullOrEmpty(strFilePath))
        {
            if (dt.Rows.Count > 0)
            {
                string strSourceFilePath = strFilePath + "\\" + dt.Rows[0]["UPLOAD_PATH"].ToString();
                if (File.Exists(strSourceFilePath))
                {
                    //新文件夹路径
                    string strfolderPath = "SubTask" + "\\" + DateTime.Now.ToString("yyyyMMdd");
                    if (!Directory.Exists(strFilePath+"\\"+strfolderPath))
                    {
                        //创建目录
                        Directory.CreateDirectory(strFilePath+"\\"+strfolderPath);
                    }
                    string strSerialNumber = GetSerialNumber("attFileId");
                    string strFullName = dt.Rows[0]["ATTACH_TYPE"].ToString();
                    //新命名的文件名称
                    string strNewFileName = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + strSerialNumber + strFullName;
                    //将新的信息写入数据库


                    TOaAttVo objTOaAttVo = new TOaAttVo();
                    objTOaAttVo.ID = strSerialNumber;
                    objTOaAttVo.DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString();
                    objTOaAttVo.BUSINESS_ID = this.SUBTASK_ID.Value.ToString() ;
                    objTOaAttVo.BUSINESS_TYPE = "SubTask";
                    objTOaAttVo.UPLOAD_PATH = strfolderPath + "\\" + strNewFileName;
                    objTOaAttVo.UPLOAD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                    objTOaAttVo.UPLOAD_PERSON = LogInfo.UserInfo.REAL_NAME;
                    objTOaAttVo.ATTACH_NAME = dt.Rows[0]["ATTACH_NAME"].ToString();
                    objTOaAttVo.ATTACH_TYPE = dt.Rows[0]["ATTACH_TYPE"].ToString();
                    //新文件路径
                    string strDestFilePath = strFilePath + "\\" + objTOaAttVo.UPLOAD_PATH;

                    //COPY文件到新文件
                    File.Copy(strSourceFilePath, strDestFilePath, true);
                    new TOaAttLogic().Create(objTOaAttVo);
                    }
                }
            }
    }

    /// <summary>
    /// 创建原因：查找当前子任务是否已经复制了点位图
    /// 创建人：胡方扬
    /// 创建日期：2013-07-03
    /// </summary>
    /// <param name="strSubTaskID"></param>
    /// <returns></returns>
    public bool GetSubTaskAtt(string strSubTaskID) {
        bool blFlag = false;
        TOaAttVo objTOaAttVo = new TOaAttVo();
        objTOaAttVo.BUSINESS_ID = strSubTaskID;
        objTOaAttVo.BUSINESS_TYPE = "SubTask";

        DataTable dt = new TOaAttLogic().SelectByTable(objTOaAttVo);
        if (dt.Rows.Count > 0) {
            blFlag = true;
        }
        return blFlag;
    }

    /// <summary>
    /// 创建原因：获取指定子任务的基础资料企业ID
    /// 创建人：胡方扬
    /// 创建日期：2013-07-03
    /// </summary>
    /// <param name="strSubTaskID"></param>
    /// <returns></returns>
    public string getCompanyID(string strSubTaskID) {
        string result = "";
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);

        DataTable dt = new TMisMonitorSubtaskLogic().GetCompanyIDForSubTask(objSubTask);
        if (dt.Rows.Count > 0) {
            result = dt.Rows[0]["COMPANY_ID"].ToString();
        }

        return result;
    }


    /// <summary>
    /// 创建原因：删除指定子任务附件
    /// 创建人：胡方扬
    /// 创建原因：2013-07-03
    /// </summary>
    /// <param name="strSubTaskID"></param>
    /// <returns></returns>
    public string DelFile(string strSubTaskID) {
        string result = "";
        TOaAttVo objToa = new TOaAttVo();
        objToa.BUSINESS_ID = strSubTaskID;
        objToa.BUSINESS_TYPE = "SubTask";
        if (new TOaAttLogic().Delete(objToa)) {
            result = "True";
        }

        return result;
    }

    /// <summary>
    /// 修改样品编号中的日期部分
    /// </summary>
    /// <param name="strSubTaskId"></param>
    /// <param name="strSampleId"></param>
    /// <param name="strDateCode"></param>
    /// <param name="isAll">true: 更新任务的所有样品编号， false: 更新单个样品编号</param>
    /// <returns></returns>
    [WebMethod]
    public static string UpdateSampleCode(string strSubTaskId, string strSampleId, string strDateCode, string isAll)
    {
        string result = "";
        string strSampleCode = "";
        string strNewSampleCode = "";
        string[] objStr;
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();

        if (isAll == "true")
        {
            List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();
            objSample.SUBTASK_ID = strSubTaskId;
            list = new TMisMonitorSampleInfoLogic().SelectByObject(objSample, 0, 0);
            for (int i = 0; i < list.Count; i++)
            {
                strSampleCode = list[i].SAMPLE_CODE;
                objStr = strSampleCode.Split('-');

                if (objStr.Length == 3)
                {
                    strNewSampleCode = objStr[0].ToString();
                    strNewSampleCode += "-" + objStr[1].ToString().Substring(0, 2) + strDateCode;
                    strNewSampleCode += "-" + objStr[2].ToString();

                    objSample = new TMisMonitorSampleInfoVo();
                    objSample.ID = list[i].ID;
                    objSample.SAMPLE_CODE = strNewSampleCode;
                    if (new TMisMonitorSampleInfoLogic().Edit(objSample))
                    {
                        result = "true";
                    }
                }
            }
        }
        else
        {
            objSample = new TMisMonitorSampleInfoLogic().Details(strSampleId);
            strSampleCode = objSample.SAMPLE_CODE;
            objStr = strSampleCode.Split('-');

            if (objStr.Length == 3)
            {
                strNewSampleCode = objStr[0].ToString();
                strNewSampleCode += "-" + objStr[1].ToString().Substring(0, 2) + strDateCode;
                strNewSampleCode += "-" + objStr[2].ToString();

                objSample = new TMisMonitorSampleInfoVo();
                objSample.ID = strSampleId;
                objSample.SAMPLE_CODE = strNewSampleCode;
                if (new TMisMonitorSampleInfoLogic().Edit(objSample))
                {
                    result = "true";
                }
            }
        }
        return result;
    }
    //获取动态属性数据生成JSON串
    public string GetAttData()
    {
        string strJson = "";
        string strType_ID = Request["Type_ID"].ToString();
        strType_ID = "'" + strType_ID.Replace(",", "','") + "'";
        DataTable dtMain = new DataTable();
        DataTable dt = new TBaseAttributeInfoLogic().GetAttDate(strType_ID);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtMain.Columns.Add(dt.Rows[i]["ID"].ToString() + "@" + dt.Rows[i]["CONTROL_ID"].ToString() + "@" + dt.Rows[i]["ATTRIBUTE_NAME"].ToString() + "@" + dt.Rows[i]["CONTROL_NAME"].ToString() + "@" + dt.Rows[i]["DICTIONARY"].ToString(), typeof(string));
        }

        strJson = DataTableToJsonUnsureColEx(dtMain);
        return strJson;
    }

    #region 获取下拉字典项
    private string getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);

        return strJson;
    }
    #endregion

    // 删除删除原始记录信息
    [WebMethod]
    public static string DeleteDustInfor(string strResultID, string strItemID)
    {
        bool isSuccess = false;
        TMisMonitorDustinforVo objDustinforVo = new TMisMonitorDustinforVo();
        objDustinforVo.SUBTASK_ID = strResultID;
        objDustinforVo.ITEM_ID = strItemID;
        isSuccess = new TMisMonitorDustinforLogic().Delete(objDustinforVo);

        return isSuccess == true ? "1" : "0";
    }
}