using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.BusinessLogic.Sys.Duty;

/// <summary>
/// 功能描述：噪声采样任务
/// 创建日期：2014-03-25
/// 创建人  ：魏林
/// </summary>

public partial class Channels_Mis_Monitor_sampling_QY_Sampling_Noise : PageBase
{
    public static string strId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["strMonitorID"]))
            {
                //监测类型ID
                this.MONITOR_ID.Value = Request.QueryString["strMonitorID"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["IS_BACK"]))
            {
                //是否退回的采样任务
                this.IS_BACK.Value = Request.QueryString["IS_BACK"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Link"]))
            {
                //环节标志：Sample-采样环节 Check-现场结果复核环节 QcCheck-现场结果审核环节
                this.Link.Value = Request.QueryString["Link"].ToString();
            }

            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getWeatherInfo")
            {
                strResult = GetWeatherInfo();
                Response.Write(strResult);
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getWeatherValue")
            {
                strResult = GetWeatherValue();
                Response.Write(strResult);
                Response.End();
            }
            //获取字典信息
            if (Request["type"] != null && Request["type"].ToString() == "GetDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "GetItemData")
            {
                strResult = GetItemData();
                Response.Write(strResult);
                Response.End();
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
            //获取现场复核人信息
            if (Request["type"] != null && Request["type"].ToString() == "GetCheckUser")
            {
                strResult = GetCheckUser(Request.QueryString["MonitorID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    private string GetCheckUser(string strMonitorID)
    {
        string strUserIDs = "";
        DataTable dt = new DataTable();
        //TSysUserPostVo UserPostVo = new TSysUserPostVo();
        //UserPostVo.POST_ID = "000000030";
        //dt = new TSysUserPostLogic().SelectByTable(UserPostVo);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    strUserIDs += dt.Rows[i]["USER_ID"].ToString() + ",";
        //}

        string strLink = Request["Link"].ToString();
        string strDuty = "";
        if (strLink == "Sample")
            strDuty = "duty_other_sample";
        else
            strDuty = "sample_result_check";
        DataTable strSampleCheckSender = GetDefaultOrFirstDutyUser(strDuty, strMonitorID);
        for (int i = 0; i < strSampleCheckSender.Rows.Count; i++)
        {
            strUserIDs += strSampleCheckSender.Rows[i]["USERID"].ToString() + ",";
        }

        TSysUserVo SysUserVo = new TSysUserVo();
        SysUserVo.ID = strUserIDs.TrimEnd(',');
        SysUserVo.IS_DEL = "0";
        SysUserVo.IS_USE = "1";
        dt = new TSysUserLogic().SelectByTableEx(SysUserVo, 0, 0);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获得指定职责的默认负责人，如果不存在则取第一个
    /// </summary>
    /// <param name="strDutyType">职责编码</param>
    /// <param name="strMonitorType">监测类别</param>
    /// <returns></returns>
    private DataTable GetDefaultOrFirstDutyUser(string strDutyType, string strMonitorType)
    {
        DataTable dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorType);
        return dtDuty;
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo()
    {
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(this.SUBTASK_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

        TMisMonitorTaskCompanyVo objConCompany = new TMisMonitorTaskCompanyVo();
        //objConCompany.TASK_ID = objTask.ID;
        objConCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);
        objSubtask.MONITOR_ID = getMonitorTypeName(objSubtask.MONITOR_ID);
        objSubtask.SAMPLING_MANAGER_ID = new TSysUserLogic().Details(objSubtask.SAMPLING_MANAGER_ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.CONTRACT_CODE;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.PHONE;
        objSubtask.SAMPLE_APPROVE_INFO = objTask.TEST_PURPOSE;
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_ASK_DATE))
        {
            objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        }
        if (!String.IsNullOrEmpty(objSubtask.SAMPLE_FINISH_DATE))
        {
            objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");
        }
        //获取退回意见信息
        TMisReturnInfoVo ReturnInfoVo = new TMisReturnInfoVo();
        ReturnInfoVo.TASK_ID = objSubtask.TASK_ID;
        ReturnInfoVo.SUBTASK_ID = objSubtask.ID;
        if (Request.QueryString["strLink"].ToString() == "Sample")
        {
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
            ReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_002;
        }
        if (Request.QueryString["strLink"].ToString() == "Check")
        {
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_004;
            ReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_003;
        }
        if (Request.QueryString["strLink"].ToString() == "QcCheck")
        {
            ReturnInfoVo.CURRENT_STATUS = "111";
            ReturnInfoVo.BACKTO_STATUS = "222";
        }
        ReturnInfoVo = new TMisReturnInfoLogic().Details(ReturnInfoVo);
        if (ReturnInfoVo.ID.Length > 0)
        {
            objSubtask.SAMPLING_METHOD = "退回意见：" + ReturnInfoVo.SUGGESTION;
        }

        return ToJson(objSubtask);
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
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

        if (this.SUBTASK_ID.Value.Length == 0)
            return "";

        if (this.MONITOR_ID.Value.Trim() == "FunctionNoise")
        {
            //如果是功能区噪声需要按一天24小时拆分样品
            SplitFunctionNoiseByHour(this.SUBTASK_ID.Value.Trim());
        }

        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
        objSampleInfo.SUBTASK_ID = this.SUBTASK_ID.Value;
        objSampleInfo.QC_TYPE = "0";
        objSampleInfo.SORT_FIELD = "POINT_ID";
        objSampleInfo.SORT_TYPE = "asc";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTable(objSampleInfo, intPageIndex, intPageSize);

        string strUnit = "";
        if (this.MONITOR_ID.Value.Trim() == "000000004")
            strUnit = "dB(A)";

        dtSample = new TMisMonitorSubtaskLogic().getItemBySubTaskID(this.SUBTASK_ID.Value.Trim());
        for (int i = 0; i < dtSample.Rows.Count; i++)
        {
            dt.Columns.Add(dtSample.Rows[i]["ID"].ToString() + "@@" + dtSample.Rows[i]["ITEM_NAME"].ToString() + strUnit, typeof(string));
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample = new TMisMonitorSubtaskLogic().getItemValueBySampleID(dt.Rows[i]["ID"].ToString());
            for (int j = 0; j < dtSample.Rows.Count; j++)
            {
                dt.Rows[i][dtSample.Rows[j]["ID"].ToString() + "@@" + dtSample.Rows[j]["ITEM_NAME"].ToString() + strUnit] = dtSample.Rows[j]["ITEM_RESULT"].ToString();
            }
        }

        int intTotalCount = new TMisMonitorSampleInfoLogic().GetSelectResultCount(objSampleInfo);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 采样时修改采样日期与要求完成日期
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string updateDate(string strSubTaskID, string strAskDate, string strFinishDate)
    {
        TMisMonitorSubtaskVo SubTaskVo = new TMisMonitorSubtaskVo();
        SubTaskVo.ID = strSubTaskID;
        SubTaskVo.SAMPLE_ASK_DATE = strAskDate;
        SubTaskVo.SAMPLE_FINISH_DATE = strFinishDate;
        new TMisMonitorSubtaskLogic().Edit(SubTaskVo);
        return "1";
    }

    /// <summary>
    /// 样品信息保存
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string updateSample(string id, string strCellName, string strCellValue)
    {
        bool isSuccess = false;
        if (!strCellName.Contains("@"))
        {
            isSuccess = new TMisMonitorSampleInfoLogic().UpdateSampleCell(id, strCellName, strCellValue);
        }
        else
        {
            string[] strInfo = strCellName.Split('@');
            TMisMonitorResultVo objValue = new TMisMonitorResultVo();
            objValue.ITEM_RESULT = strCellValue;
            TMisMonitorResultVo objWhere = new TMisMonitorResultVo();
            objWhere.SAMPLE_ID = id;
            objWhere.ITEM_ID = strInfo[0].ToString();
            isSuccess = new TMisMonitorResultLogic().Edit(objValue, objWhere);
        }

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 根据监测类别获取天气项目
    /// </summary>
    /// <returns>Json</returns>
    protected string GetWeatherInfo()
    {
        string strDictType = "";
        if (this.MONITOR_ID.Value.Trim() == "000000004")
            strDictType = "noise_weather";
        else
            strDictType = "gerenal_weather";

        TSysDictVo objDict = new TSysDictVo();
        objDict.DICT_TYPE = strDictType;
        objDict.SORT_FIELD = TSysDictVo.ORDER_ID_FIELD;
        DataTable dt = new TSysDictLogic().SelectByTable(objDict, 0, 0);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 根据任务ID获取天气信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetWeatherValue()
    {
        TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();
        objSampleSky.SUBTASK_ID = this.SUBTASK_ID.Value;
        DataTable dt = new TMisMonitorSampleSkyLogic().SelectByTable(objSampleSky);
        return DataTableToJson(dt);
    }

    #region 获取下拉字典项
    private string getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);

        return strJson;
    }
    #endregion

    //获取动态监测项目数据生成JSON串
    public string GetItemData()
    {
        string strJson = "";
        string strUnit = "";
        
        DataTable dtMain = new DataTable();
        DataTable dt = new TMisMonitorSubtaskLogic().getItemBySubTaskID(this.SUBTASK_ID.Value.Trim());
        if (this.MONITOR_ID.Value.Trim() == "000000004")
            strUnit = "dB(A)";
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtMain.Columns.Add(dt.Rows[i]["ID"].ToString() + "@@" + dt.Rows[i]["ITEM_NAME"].ToString() + strUnit, typeof(string));
        }

        strJson = DataTableToJsonUnsureColEx(dtMain);
        return strJson;
    }

    /// <summary>
    /// 新增监测点保存方法
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SaveDataPoint(string strSubTaskID, string strPointName, string strMonitorID)
    {
        bool isSuccess = true;

        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointVo();
        objPoint.ID = GetSerialNumber("t_mis_monitor_taskpointId");
        objPoint.IS_DEL = "0";
        objPoint.SUBTASK_ID = strSubTaskID;
        objPoint.POINT_NAME = strPointName;
        objPoint.MONITOR_ID = strMonitorID;
        objPoint.FREQ = "1";
        objPoint.CREATE_DATE = DateTime.Now.ToString();

        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
        objPoint.TASK_ID = objTask.ID;

        //监测任务出现新增排口时，基础资料企业表也要新增
        TBaseCompanyPointVo objnewPoint = new TBaseCompanyPointVo();
        objnewPoint.ID = GetSerialNumber("t_base_company_point_id");
        objnewPoint.IS_DEL = "0";
        objnewPoint.POINT_NAME = strPointName;
        objnewPoint.MONITOR_ID = strMonitorID;
        objnewPoint.FREQ = "1";
        objnewPoint.CREATE_DATE = DateTime.Now.ToString();

        TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
        //objTaskCompany.TASK_ID = objTask.ID; ;
        objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(objTask.TESTED_COMPANY_ID);

        TMisContractCompanyVo objContractCompany = new TMisContractCompanyLogic().Details(objTaskCompany.COMPANY_ID);
        objnewPoint.COMPANY_ID = objContractCompany.COMPANY_ID;

        isSuccess = new TBaseCompanyPointLogic().Create(objnewPoint);

        objPoint.POINT_ID = objnewPoint.ID;
        isSuccess = new TMisMonitorTaskPointLogic().Create(objPoint);

        //增加点位样品信息
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.ID = GetSerialNumber("MonitorSampleId");
        objSample.SUBTASK_ID = strSubTaskID;
        objSample.QC_TYPE = "0";
        objSample.NOSAMPLE = "0";
        objSample.POINT_ID = objPoint.ID;
        objSample.SAMPLE_NAME = objPoint.POINT_NAME;
        isSuccess = new TMisMonitorSampleInfoLogic().Create(objSample);

        //为新增的测点添加监测项目
        DataTable dt = new TMisMonitorSubtaskLogic().getItemBySubTaskID(strSubTaskID);
        string strItemIDs = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strItemIDs += dt.Rows[i]["ID"].ToString() + ",";
        }
        isSuccess = SaveDataItem(strSubTaskID, objSample.ID, strItemIDs.TrimEnd(','), true);

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
    public static string SaveDataItem(string strSubTaskID, string strPointIDs, string strSample, string strSelItem_IDs)
    {
        bool isSuccess = true;
        isSuccess = SaveDataItem(strSubTaskID, strPointIDs, strSelItem_IDs, false);

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
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

        isSuccess = new TMisMonitorSampleInfoLogic().Delete(objSample);

        return isSuccess == true ? "1" : "0";
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

    /// <summary>
    /// 创建原因：获取指定子任务的基础资料企业ID
    /// 创建人：胡方扬
    /// 创建日期：2013-07-03
    /// </summary>
    /// <param name="strSubTaskID"></param>
    /// <returns></returns>
    public string getCompanyID(string strSubTaskID)
    {
        string result = "";
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);

        DataTable dt = new TMisMonitorSubtaskLogic().GetCompanyIDForSubTask(objSubTask);
        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["COMPANY_ID"].ToString();
        }

        return result;
    }

    public void SplitFunctionNoiseByHour(string strSubTaskID)
    {
        TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        if (objSubtaskVo.SAMPLING_METHOD != "1")  //SAMPLE_METHOD='1'时表示该任务已进行了拆分
        {
            objSubtaskVo.SAMPLING_METHOD = "1";
            List<TMisMonitorSampleInfoVo> listSample = new List<TMisMonitorSampleInfoVo>();
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
            objSample.SUBTASK_ID = strSubTaskID;
            TMisMonitorSampleInfoVo objSampleSet = new TMisMonitorSampleInfoVo();
            objSampleSet.ENV_HOUR = "0";
            new TMisMonitorSampleInfoLogic().Edit(objSampleSet, objSample);

            listSample = new TMisMonitorSampleInfoLogic().SelectByObject(objSample, 0, 0);
            for (int i = 0; i < listSample.Count; i++)
            {
                objSample = listSample[i];

                TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                objResultVo.SAMPLE_ID = objSample.ID;
                List<TMisMonitorResultVo> listResult = new List<TMisMonitorResultVo>();
                listResult = new TMisMonitorResultLogic().SelectByObject(objResultVo, 0, 0);
                for (int j = 1; j < 24; j++)
                {
                    objSampleSet = new TMisMonitorSampleInfoVo();
                    CopyObject(objSample, objSampleSet);
                    objSampleSet.ID = GetSerialNumber("MonitorSampleId");
                    objSampleSet.ENV_HOUR = j.ToString();

                    for (int k = 0; k < listResult.Count; k++)
                    {
                        objResultVo = listResult[k];
                        TMisMonitorResultVo objResultAdd = new TMisMonitorResultVo();
                        CopyObject(objResultVo, objResultAdd);
                        objResultAdd.ID = GetSerialNumber("MonitorResultId");
                        objResultAdd.SAMPLE_ID = objSampleSet.ID;

                        TMisMonitorResultAppVo objResultAppVo = new TMisMonitorResultAppVo();
                        objResultAppVo.RESULT_ID = objResultVo.ID;
                        objResultAppVo = new TMisMonitorResultAppLogic().Details(objResultAppVo);
                        TMisMonitorResultAppVo objResultAppAdd = new TMisMonitorResultAppVo();
                        CopyObject(objResultAppVo, objResultAppAdd);
                        objResultAppAdd.ID = GetSerialNumber("MonitorResultAppId");
                        objResultAppAdd.RESULT_ID = objResultAdd.ID;

                        new TMisMonitorResultLogic().Create(objResultAdd);
                        new TMisMonitorResultAppLogic().Create(objResultAppAdd);
                    }

                    new TMisMonitorSampleInfoLogic().Create(objSampleSet);
                }
            }

            new TMisMonitorSubtaskLogic().Edit(objSubtaskVo);
        }
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strLink, string strSubtaskID, string strUserID, string strAttribute)
    {
        bool isSuccess = true;
        string strMsg = "";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        if (strLink == "Sample")
        {
            #region 采样环节发送事件
            objSubtask.TASK_STATUS = "021";//其它进行样品交接环节
            strMsg = "样品交接";
            //判断是否存在样品没有设置项目的情况
            TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoVo();
            SampleInfoVo.SUBTASK_ID = strSubtaskID;
            List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();
            list = new TMisMonitorSampleInfoLogic().SelectByObject(SampleInfoVo, 0, 0);
            for (int i = 0; i < list.Count; i++)
            {
                TMisMonitorResultVo ResultVo = new TMisMonitorResultVo();
                ResultVo.SAMPLE_ID = list[i].ID;
                ResultVo = new TMisMonitorResultLogic().SelectByObject(ResultVo);
                if (ResultVo.ID.Length == 0)
                {
                    isSuccess = false;
                    strMsg = "样品[" + list[i].SAMPLE_NAME + "]还没设置监测项目";
                    break;
                }
            }
            if (isSuccess == false)
            {
                return "[{result:'0',msg:'" + strMsg + "'}]";
            }

            //子任务所有项目都属于现场项目，跳过分析环节
            DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubtaskID);
            if (dtSampleDept.Rows.Count == 0)//全是现场项目
            {
                objSubtask.TASK_STATUS = "022";
                objSubtask.REMARK1 = objSubtask.ID;
                strMsg = "现场监测结果复核";
                //设置现场复核人
                TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
                objSubAppSet.SAMPLING_CHECK = strUserID;
                objSubAppSet.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
                TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
                objSubAppWhere.SUBTASK_ID = strSubtaskID;
                new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
            }

            //update by ssz QY 现场项目信息需要另外发送任务
            DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubtaskID);
            if (dtSampleItem.Rows.Count > 0 && dtSampleDept.Rows.Count > 0)//存在 非现场项目和现场项目
            {
                TMisMonitorSubtaskVo objSampleSubtask = new TMisMonitorSubtaskVo();
                CopyObject(objSubtask, objSampleSubtask);
                objSampleSubtask.ID = GetSerialNumber("t_mis_monitor_subtaskId");
                objSampleSubtask.REMARK1 = objSubtask.ID;
                objSampleSubtask.TASK_STATUS = "022";
                //创建一个新的任务 对现场项目流程
                new TMisMonitorSubtaskLogic().Create(objSampleSubtask);

                //设置现场复核人
                TMisMonitorSubtaskAppVo objSubApp = new TMisMonitorSubtaskAppVo();
                objSubApp.ID = GetSerialNumber("TMisMonitorSubtaskAppID");
                objSubApp.SUBTASK_ID = objSampleSubtask.ID;
                objSubApp.SAMPLING_CHECK = strUserID;
                objSubApp.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
                new TMisMonitorSubtaskAppLogic().Create(objSubApp);

                strMsg = "样品交接、现场监测结果复核";
            }
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);

            //现场信息
            TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();

            for (int i = 0; i < strAttribute.Split('-').Length; i++)
            {
                if (strAttribute.Split('-')[i].Contains("|"))
                {
                    objSampleSky.SUBTASK_ID = strSubtaskID;
                    objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                    objSampleSky = new TMisMonitorSampleSkyLogic().Details(objSampleSky);
                    objSampleSky.WEATHER_INFO = strAttribute.Split('-')[i].Split('|')[1];
                    objSampleSky.SUBTASK_ID = strSubtaskID;
                    objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                    if (objSampleSky.ID.Length > 0)
                    {
                        isSuccess = new TMisMonitorSampleSkyLogic().Edit(objSampleSky);
                    }
                    else
                    {
                        objSampleSky.ID = GetSerialNumber("TMisMonitorSampleSky");
                        isSuccess = new TMisMonitorSampleSkyLogic().Create(objSampleSky);

                    }
                }
            }
            #endregion
        }
        if (strLink == "Check")
        {
            #region 现场监测结果复核环节发送事件
            objSubtask.TASK_STATUS = "023";
            objSubtask.TASK_TYPE = "发送";
            strMsg = "现场监测结果审核";
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
            //记录现场复核时间
            TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
            objSubAppSet.REMARK2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            objSubAppSet.SAMPLING_QC_CHECK = strUserID;
            TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
            objSubAppWhere.SUBTASK_ID = strSubtaskID;
            new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
            #endregion
        }
        if (strLink == "QcCheck")
        {
            #region 现场监测结果审核环节发送事件
            objSubtask.TASK_STATUS = "24";
            objSubtask.TASK_TYPE = "发送";
            strMsg = "发送成功";
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
            //更新现场审核人
            TMisMonitorSubtaskAppVo SubtaskAppVo = new TMisMonitorSubtaskAppVo();
            SubtaskAppVo.SUBTASK_ID = strSubtaskID;
            SubtaskAppVo = new TMisMonitorSubtaskAppLogic().SelectByObject(SubtaskAppVo);
            SubtaskAppVo.SAMPLING_QC_CHECK = new i3.View.PageBase().LogInfo.UserInfo.ID;
            SubtaskAppVo.REMARK3 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //现场审核时间
            new TMisMonitorSubtaskAppLogic().Edit(SubtaskAppVo);

            if (isSuccess == true)
            {
                bool IsFinish = false;
                IsFinish = new TMisMonitorSubtaskLogic().isFinishSubTask(objSubtask.TASK_ID, true);
                if (IsFinish == true)
                {
                    TMisMonitorTaskVo TMisMonitorTaskVo = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);
                    TMisMonitorTaskVo.TASK_STATUS = "09";

                    if (TMisMonitorTaskVo.TASK_TYPE == "1")
                    {
                        DataTable objTable = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = objSubtask.TASK_ID });
                        //如果是环境质量将自动对数据进行填报
                        foreach (DataRow row in objTable.Rows)
                        {
                            string strSubTaskId = row["ID"].ToString();
                            string strMonitorId = row["MONITOR_ID"].ToString();
                            string strAskDate = row["SAMPLE_ASK_DATE"].ToString();
                            string strSampleDate = row["SAMPLE_FINISH_DATE"].ToString();
                            TMisMonitorSubtaskVo TMisMonitorSubtaskVo = new TMisMonitorSubtaskVo();
                            TMisMonitorSubtaskVo.ID = strSubTaskId;
                            TMisMonitorSubtaskVo.MONITOR_ID = strMonitorId;
                            TMisMonitorSubtaskVo.SAMPLE_ASK_DATE = strAskDate;
                            TMisMonitorSubtaskVo.SAMPLE_FINISH_DATE = strSampleDate;
                            if (strMonitorId == "EnvRiver" || strMonitorId == "EnvReservoir" || strMonitorId == "EnvDrinking" || strMonitorId == "EnvDrinkingSource" || strMonitorId == "EnvStbc" || strMonitorId == "EnvMudRiver" || strMonitorId == "EnvPSoild" || strMonitorId == "EnvSoil" || strMonitorId == "EnvAir" || strMonitorId == "EnvSpeed" || strMonitorId == "EnvDust" || strMonitorId == "EnvRain")
                            {
                                new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, false, TMisMonitorTaskVo.SAMPLE_SEND_MAN);
                            }
                            if (strMonitorId == "AreaNoise" || strMonitorId == "EnvRoadNoise" || strMonitorId == "FunctionNoise")
                            {
                                new TMisMonitorSubtaskLogic().SetEnvFillData(TMisMonitorSubtaskVo, true, TMisMonitorTaskVo.SAMPLE_SEND_MAN);
                            }
                        }

                        strMsg = "数据填报";
                    }
                    else
                    {
                        if (TMisMonitorTaskVo.REPORT_HANDLE == "")
                            TMisMonitorTaskVo.REPORT_HANDLE = getNextReportUserID("Report_UserID");
                        strMsg = "报告办理";
                    }

                    new TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);
                    
                }

            }
            #endregion
        }
        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }

    /// <summary>
    /// 退回的采样任务发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnBackSendClick(string strLink, string strSubtaskID, string strUserID, string strAttribute)
    {
        bool isSuccess = true;
        string strMsg = "";
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);

        if (strLink == "Sample")   //采样环节发送事件
        {
            //判断是否存在样品没有设置项目的情况
            TMisMonitorSampleInfoVo SampleInfoVo = new TMisMonitorSampleInfoVo();
            SampleInfoVo.SUBTASK_ID = strSubtaskID;
            List<TMisMonitorSampleInfoVo> list = new List<TMisMonitorSampleInfoVo>();
            list = new TMisMonitorSampleInfoLogic().SelectByObject(SampleInfoVo, 0, 0);
            for (int i = 0; i < list.Count; i++)
            {
                TMisMonitorResultVo ResultVo = new TMisMonitorResultVo();
                ResultVo.SAMPLE_ID = list[i].ID;
                ResultVo = new TMisMonitorResultLogic().SelectByObject(ResultVo);
                if (ResultVo.ID.Length == 0)
                {
                    isSuccess = false;
                    strMsg = "样品[" + list[i].SAMPLE_NAME + "]还没设置监测项目";
                    break;
                }
            }
            if (isSuccess == false)
            {
                return "[{result:'0',msg:'" + strMsg + "'}]";
            }

            objSubtask.TASK_STATUS = "022";
            objSubtask.TASK_TYPE = "发送";
            strMsg = "样品交接";
            //子任务所有项目都属于现场项目，跳过分析环节
            DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubtaskID);
            if (dtSampleDept.Rows.Count == 0)//全是现场项目
            {
                strMsg = "现场监测结果复核";

                //设置现场复核人
                TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
                objSubAppSet.SAMPLING_CHECK = strUserID;
                objSubAppSet.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
                TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
                objSubAppWhere.SUBTASK_ID = strSubtaskID;
                new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
            }

            DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubtaskID);
            if (dtSampleItem.Rows.Count > 0 && dtSampleDept.Rows.Count > 0)//存在 非现场项目和现场项目
            {
                strMsg = "样品交接、现场监测结果复核";

                //设置现场复核人
                TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
                objSubAppSet.REMARK1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //采样时间
                objSubAppSet.SAMPLING_CHECK = strUserID;
                TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
                objSubAppWhere.SUBTASK_ID = strSubtaskID;
                new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
            }
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);

            //现场信息
            TMisMonitorSampleSkyVo objSampleSky = new TMisMonitorSampleSkyVo();

            for (int i = 0; i < strAttribute.Split('-').Length; i++)
            {
                if (strAttribute.Split('-')[i].Contains("|"))
                {
                    objSampleSky.SUBTASK_ID = strSubtaskID;
                    objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                    objSampleSky = new TMisMonitorSampleSkyLogic().Details(objSampleSky);
                    objSampleSky.WEATHER_INFO = strAttribute.Split('-')[i].Split('|')[1];
                    objSampleSky.SUBTASK_ID = strSubtaskID;
                    objSampleSky.WEATHER_ITEM = strAttribute.Split('-')[i].Split('|')[0];
                    if (objSampleSky.ID.Length > 0)
                    {
                        isSuccess = new TMisMonitorSampleSkyLogic().Edit(objSampleSky);
                    }
                    else
                    {
                        objSampleSky.ID = GetSerialNumber("TMisMonitorSampleSky");
                        isSuccess = new TMisMonitorSampleSkyLogic().Create(objSampleSky);

                    }
                }
            }
        }
        if (strLink == "Check") //现场结果复核发送事件
        {
            objSubtask.TASK_STATUS = "023";
            objSubtask.TASK_TYPE = "发送";
            strMsg = "现场监测结果审核";
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
            //记录现场复核时间
            TMisMonitorSubtaskAppVo objSubAppSet = new TMisMonitorSubtaskAppVo();
            objSubAppSet.REMARK2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            objSubAppSet.SAMPLING_QC_CHECK = strUserID;
            TMisMonitorSubtaskAppVo objSubAppWhere = new TMisMonitorSubtaskAppVo();
            objSubAppWhere.SUBTASK_ID = strSubtaskID;
            new TMisMonitorSubtaskAppLogic().Edit(objSubAppSet, objSubAppWhere);
        }
        if (strLink == "QcCheck") //现场结果审核发送事件
        {
            objSubtask.TASK_STATUS = "24";
            objSubtask.TASK_TYPE = "发送";
            strMsg = "发送成功";
            isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
            //更新现场审核人
            TMisMonitorSubtaskAppVo SubtaskAppVo = new TMisMonitorSubtaskAppVo();
            SubtaskAppVo.SUBTASK_ID = strSubtaskID;
            SubtaskAppVo = new TMisMonitorSubtaskAppLogic().SelectByObject(SubtaskAppVo);
            SubtaskAppVo.SAMPLING_QC_CHECK = new i3.View.PageBase().LogInfo.UserInfo.ID;
            SubtaskAppVo.REMARK3 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //现场审核时间
            new TMisMonitorSubtaskAppLogic().Edit(SubtaskAppVo);

            if (isSuccess == true)
            {
                bool IsFinish = false;
                IsFinish = new TMisMonitorSubtaskLogic().isFinishSubTask(objSubtask.TASK_ID, true);
                if (IsFinish == true)
                {
                    TMisMonitorTaskVo TMisMonitorTaskVo = new TMisMonitorTaskVo();
                    TMisMonitorTaskVo.ID = objSubtask.TASK_ID;
                    TMisMonitorTaskVo.TASK_STATUS = "09";
                    TMisMonitorTaskVo.REPORT_HANDLE = getNextReportUserID("Report_UserID");
                    new TMisMonitorTaskLogic().Edit(TMisMonitorTaskVo);
                    strMsg = "报告办理";
                }

            }
        }
        
        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }

    /// <summary>
    /// 退回事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnBackClick(string strLink, string strSubtaskID, string strSuggestion)
    {
        bool isSuccess = true;
        string strMsg = "";
        string strCurrentStatus = "";
        string strBackStatus = "";
        TMisMonitorSubtaskVo objSubTaskVo = new TMisMonitorSubtaskLogic().Details(strSubtaskID);

        if (strLink == "Sample")  //采样环节退回事件
        {
            strCurrentStatus = SerialType.Monitor_002;
            strBackStatus = SerialType.Monitor_001;

            objSubTaskVo.TASK_STATUS = "01";
            objSubTaskVo.TASK_TYPE = "退回";
            new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);

            TMisMonitorTaskVo objTaskVo = new TMisMonitorTaskLogic().Details(objSubTaskVo.TASK_ID);
            objTaskVo.QC_STATUS = "3";
            new TMisMonitorTaskLogic().Edit(objTaskVo);

            TMisContractPlanVo objContractPlanVo = new TMisContractPlanLogic().Details(objTaskVo.PLAN_ID);
            objContractPlanVo.HAS_DONE = "0";
            new TMisContractPlanLogic().Edit(objContractPlanVo);

            strMsg = "采样任务分配";
        }
        if (strLink == "Check")   //现场结果复核退回事件
        {
            strCurrentStatus = SerialType.Monitor_003;
            strBackStatus = SerialType.Monitor_002;

            objSubTaskVo.TASK_STATUS = "02";
            objSubTaskVo.TASK_TYPE = "退回";
            new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);

            strMsg = "采样";
        }
        if (strLink == "QcCheck") //现场结果审核退回事件
        {
            strCurrentStatus = SerialType.Monitor_004;
            strBackStatus = SerialType.Monitor_003;

            objSubTaskVo.TASK_STATUS = "022";
            objSubTaskVo.TASK_TYPE = "退回";
            new TMisMonitorSubtaskLogic().Edit(objSubTaskVo);

            strMsg = "现场结果复核";
        }

        TMisReturnInfoVo objReturnInfoVo = new TMisReturnInfoVo();
        objReturnInfoVo.TASK_ID = objSubTaskVo.TASK_ID;
        objReturnInfoVo.SUBTASK_ID = objSubTaskVo.ID;
        objReturnInfoVo.CURRENT_STATUS = strCurrentStatus;
        objReturnInfoVo.BACKTO_STATUS = strBackStatus;
        TMisReturnInfoVo obj = new TMisReturnInfoLogic().Details(objReturnInfoVo);
        if (obj.ID.Length > 0)
        {
            objReturnInfoVo.ID = obj.ID;
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Edit(objReturnInfoVo);
        }
        else
        {
            objReturnInfoVo.ID = GetSerialNumber("t_mis_return_id");
            objReturnInfoVo.SUGGESTION = strSuggestion;
            isSuccess = new TMisReturnInfoLogic().Create(objReturnInfoVo);
        }
        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }
}