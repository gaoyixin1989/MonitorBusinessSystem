using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using System.Data;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Base.Item;
using System.Web.Services;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能描述：报告编制
/// 创建时间：2013-9-12
/// 创建人：潘德军
/// </summary>
public partial class Channels_Mis_Report_ReportQHD_ReportSchedule : PageBaseForWF, IWFStepRules
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //结果
        string strResult = "";
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            //监测任务ID
            this.ID.Value = Request.QueryString["id"].ToString();
            //委托书ID
            this.ContractID.Value = new TMisMonitorTaskLogic().Details(this.ID.Value).CONTRACT_ID;
            //报告ID
            this.reportId.Value = new TRptFileLogic().getNewReportByContractID(this.ID.Value).ID;
        }
        //获取委托书信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
        {
            strResult = GetContractInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取委托书信息 非编制
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfoForView")
        {
            strResult = GetContractInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取委托单位信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getClientInfo")
        {
            strResult = GetClientInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取受检单位信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getTestedInfo")
        {
            strResult = GetTestedInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取样品
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getSampleInfo")
        {
            strResult = GetSampleInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取项目
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getItemInfo")
        {
            strResult = GetItemInfoBySampleID();
            Response.Write(strResult);
            Response.End();
        }
        
        //监测类别
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getMonitorType")
        {
            strResult = getMonitorType();
            Response.Write(strResult);
            Response.End();
        }
        //委托类别
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDictName")
        {
            strResult = new TSysDictLogic().GetDictNameByDictCodeAndType(Request.QueryString["strCode"], Request.QueryString["strType"]);
            Response.Write(strResult);
            Response.End();
        }
        //监测任务监测类别
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getItemType")
        {
            strResult = getTaskMonitorType(this.ID.Value);
            Response.Write(strResult);
            Response.End();
        }
        //报告生成同时保存业务数据
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "saveinfo")
        {
            strResult = SaveData();
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
    }

    #region 委托书信息
    /// <summary>
    /// 获得委托书基本信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorReportVo objReport = new TMisMonitorReportVo();
        if (!string.IsNullOrEmpty(this.ID.Value))
        {
            objTask = new TMisMonitorTaskLogic().GetContractTaskInfo(this.ID.Value);
            objReport = new TMisMonitorReportLogic().Details(new TMisMonitorReportVo() { TASK_ID = this.ID.Value });
        }
        if (objTask != null)
        {
            try
            {
                objTask.CONSIGN_DATE = DateTime.Parse(objTask.CONSIGN_DATE.ToString()).ToString("yyyy-MM-dd");
            }
            catch { }

        }
        //定制数据
        objTask.REMARK1 = objReport.REPORT_CODE;//报告编号
        objTask.REMARK2 = objTask.REMARK5;//总监测任务的监测结论
        objTask.REMARK3 = GetMonitorType(this.ID.Value);//报告类别 废水，废气...
        objTask.REMARK4 = GetRemark(this.ID.Value);//委托书备注信息 四个同意
        return ToJson(objTask);
    }

    #region 获取委托书备注
    /// <summary>
    /// 获取备注
    /// </summary>
    /// <param name="strTaskID">监测任务</param>
    /// <returns></returns>
    protected string GetRemark(string strTaskID)
    {
        string strRemark = "";
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strTaskID);
        TMisContractVo objContract = new TMisContractLogic().Details(objTask.CONTRACT_ID);
        //备注信息 同意分包
        if (!string.IsNullOrEmpty(objContract.AGREE_OUTSOURCING))
        {
            strRemark += new TSysDictLogic().GetDictNameByDictCodeAndType("accept_subpackage", "Contract_Remarks") + "；";
        }
        //备注信息 是否同意使用的监测方法
        if (!string.IsNullOrEmpty(objContract.AGREE_METHOD))
        {
            strRemark += new TSysDictLogic().GetDictNameByDictCodeAndType("accept_useMonitorMethod", "Contract_Remarks") + "；";
        }
        //是否同意使用非标准方法
        if (!string.IsNullOrEmpty(objContract.AGREE_NONSTANDARD))
        {
            strRemark += new TSysDictLogic().GetDictNameByDictCodeAndType("accept_usenonstandard", "Contract_Remarks") + "；";
        }
        //是否同意其他
        if (!string.IsNullOrEmpty(objContract.AGREE_OTHER))
        {
            strRemark += new TSysDictLogic().GetDictNameByDictCodeAndType("accept_other", "Contract_Remarks") + "；";
        }
        return strRemark;
    }
    #endregion

    #region 自动生成监测结论
    /// <summary>
    /// 构造监测结论 
    /// </summary>
    /// <param name="strTaskID">监测任务ID</param>
    protected string AutoSaveConclusion(string strTaskID)
    {
        string strConclusion = "";// 监测结论
        TMisMonitorSubtaskVo objMonitorSubTask = new TMisMonitorSubtaskVo();
        objMonitorSubTask.TASK_ID = strTaskID;
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(objMonitorSubTask);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //如果子任务中不存在监测结论则自动生成新的监测结论
                string strCon = dr["PROJECT_CONCLUSION"].ToString().Length > 0 ? dr["PROJECT_CONCLUSION"].ToString() : GetConclusion(strTaskID, dr["ID"].ToString());
                strConclusion += strCon.Length > 0 ? strCon + "；" : "";
            }
            strConclusion += "对执行标准如有异议，以环保管理部门核定为准。";
        }
        return strConclusion;
    }

    /// <summary>
    /// 生成并保存监测结论
    /// </summary>
    /// <param name="strTaskID">监测任务ID</param>
    /// <param name="strSubTaskID">监测子任务ID</param>
    /// <returns>监测子任务结论</returns>
    protected string GetConclusion(string strTaskID, string strSubTaskID)
    {
        string strConclusion = "";
        //监测点位
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
        objTaskPoint.TASK_ID = strTaskID;
        objTaskPoint.SUBTASK_ID = strSubTaskID;
        objTaskPoint.IS_DEL = "0";
        DataTable dtTaskItem = new TMisMonitorTaskPointLogic().SelectTaskItemByPoint(objTaskPoint);
        if (dtTaskItem.Rows.Count > 0)
        {
            string strErrItem = "";//超标项目
            string strErrItemID = "";//超标项目ID
            for (int i = 0; i < dtTaskItem.Rows.Count; i++)
            {
                DataRow dr = dtTaskItem.Rows[i] as DataRow;
                //生成监测结论
                if (dr["CONDITION_ID"].ToString().Length > 0)
                {
                    //获得标准、依据名称
                    strConclusion = getStandardName(dr["CONDITION_ID"].ToString());
                    //记录超标项目信息
                    strErrItemID += !strErrItemID.Contains(dr["ITEM_ID"].ToString()) ? (IsOver(dr["ITEM_ID"].ToString(), dr["TASK_POINT_ID"].ToString()) ? dr["ITEM_ID"].ToString() : "") + "、" : "";
                    if (strErrItemID.Contains(dr["ITEM_ID"].ToString()))
                    {
                        string strItemName = new TBaseItemInfoLogic().Details(dr["ITEM_ID"].ToString()).ITEM_NAME;
                        if (!strErrItem.Contains(strItemName))
                            strErrItem += strItemName + "、";
                    }
                    //构造监测结论语句
                    if (strErrItem.Length > 0)
                    {
                        strConclusion += "本次监测该单位" + dr["POINT_NAME"].ToString() + "除" + strErrItem.Remove(strErrItem.LastIndexOf("、")) + "外，其它项目达标";
                    }
                    else
                    {
                        strConclusion += "本次监测该单位" + dr["POINT_NAME"].ToString() + "达标排放";
                    }
                }
            }
        }
        return strConclusion;
    }

    /// <summary>
    /// 获取标准、依据名称
    /// </summary>
    /// <param name="strEvaluationID">条件项ID</param>
    /// <returns></returns>
    protected string getStandardName(string strEvaluationID)
    {
        string strStId = "";//标准ID
        string strConText = "";//所有节点名称 
        //获得条件项信息
        TBaseEvaluationConInfoVo objEvaluationCon = new TBaseEvaluationConInfoVo();
        objEvaluationCon.IS_DEL = "0";
        objEvaluationCon.ID = strEvaluationID;
        objEvaluationCon = new TBaseEvaluationConInfoLogic().Details(objEvaluationCon);
        //遍历条件项，构造所有条件项名称
        while (objEvaluationCon.PARENT_ID.Length > 0 && objEvaluationCon.PARENT_ID != "0")
        {
            strConText = objEvaluationCon.CONDITION_NAME + strConText;
            objEvaluationCon = new TBaseEvaluationConInfoLogic().Details(objEvaluationCon.PARENT_ID);
            strStId = objEvaluationCon.STANDARD_ID;
        }
        //获得评价标准名称和编码
        TBaseEvaluationInfoVo objStandard = new TBaseEvaluationInfoLogic().Details(strStId);
        strConText = "根据《" + objStandard.STANDARD_NAME + "》（" + objStandard.STANDARD_CODE + "）中" + strConText + "，";

        return strConText;
    }

    #endregion

    #region 判断项目是否超标
    /// <summary>
    /// 判断项目是否超标
    /// </summary>
    /// <param name="strItemID">项目ID</param>
    /// <param name="strTaskPointID">监测任务点位ID</param>
    /// <returns>返回超标的项目名称</returns>
    protected bool IsOver(string strItemID, string strTaskPointID)
    {
        TMisMonitorTaskItemVo objTaskItem = new TMisMonitorTaskItemVo();
        objTaskItem.TASK_POINT_ID = strTaskPointID;
        objTaskItem.ITEM_ID = strItemID;
        objTaskItem = new TMisMonitorTaskItemLogic().Details(objTaskItem);

        //样品信息
        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoLogic().GetSampleInfoByPointID(strTaskPointID);
        // 样品结果信息
        TMisMonitorResultVo objResult = new TMisMonitorResultLogic().Details(new TMisMonitorResultVo() { ITEM_ID = strItemID, SAMPLE_ID = objSampleInfo.ID, QC_TYPE = "0" });
        //样品结果
        string strValue = objResult.ITEM_RESULT.Replace("l", "").Replace("L", "").Replace("nd", "").Replace("ND", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "").Replace("/", "").Replace("-", "").Replace("—", "").Replace("。", "."); ;
        if (strValue.Length > 0)
        {
            float ftResult = float.Parse(strValue);//结果值
            string strItemName = new TBaseItemInfoLogic().Details(objTaskItem.ITEM_ID).ITEM_NAME;//项目名称
            string strUpValue = objTaskItem.ST_UPPER;//项目上限
            string strLowValue = objTaskItem.ST_LOWER;//项目下限
            if (strUpValue.IndexOf(",") >= 0)//噪声上限值
            {
                string strItemValue = "";// 项目对应结果值
                string[] listUpValue = strUpValue.Split(',');
                if (strItemName.IndexOf("昼间") >= 0)
                {
                    strItemValue = listUpValue[0].ToString();//昼间上限值
                }
                else if (strItemName.IndexOf("夜间") >= 0)
                {
                    strItemValue = listUpValue[1].ToString();//夜间上限值
                }
                else
                {
                    strItemValue = listUpValue[0].ToString();
                }
                if (strItemValue.Length > 0)
                {
                    if (strItemValue.IndexOf("<") >= 0)//上限
                    {
                        if (strItemValue.IndexOf("=") >= 0)//小于等于
                        {
                            if (ftResult > float.Parse(strItemValue.Replace("<", "").Replace("=", "")))//大于上限
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (ftResult >= float.Parse(strItemValue.Replace("<", "").Replace("=", "")))//大于等于上限
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (strLowValue.IndexOf(",") > 0)//噪声下限值
            {
                string strItemValue = "";// 项目对应结果值
                string[] listLowValue = strLowValue.Split(',');
                if (strItemName.IndexOf("昼间") >= 0)
                {
                    strItemValue = listLowValue[0].ToString();
                }
                else if (strItemName.IndexOf("夜间") >= 0)
                {
                    strItemValue = listLowValue[1].ToString();
                }
                else
                {
                    strItemValue = listLowValue[0].ToString();
                }
                if (strItemValue.Length > 0)
                {
                    //下限
                    if (strItemValue.IndexOf(">") >= 0)
                    {
                        if (strItemValue.IndexOf("=") >= 0)//小于等于
                        {
                            if (ftResult < float.Parse(strItemValue.Replace(">", "").Replace("=", "")))//小于下限
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (ftResult <= float.Parse(strItemValue.Replace(">", "").Replace("=", "")))//小于等于下限
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                //上限
                if (strUpValue.IndexOf("<") >= 0)
                {
                    if (strUpValue.IndexOf("=") >= 0)//大于等于
                    {
                        if (ftResult > float.Parse(strUpValue.Replace("<", "").Replace("=", "")))//大于上限
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (ftResult >= float.Parse(strUpValue.Replace("<", "").Replace("=", "")))//大于等于上限
                        {
                            return true;
                        }
                    }
                }
                //下限
                if (strLowValue.IndexOf(">") >= 0)
                {
                    if (strLowValue.IndexOf("=") >= 0)
                    {
                        if (ftResult < float.Parse(strLowValue.Replace(">", "").Replace("=", "")))//小于下限
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (ftResult <= float.Parse(strLowValue.Replace(">", "").Replace("=", "")))//小于等于下限
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    #endregion

    #region 获取合同类型
    protected string GetMonitorType(string strTaskID)
    {
        string strMonitorType = "";//合同类型字符串
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        objSubTask.TASK_ID = strTaskID;
        List<TMisMonitorSubtaskVo> listSubTask = new TMisMonitorSubtaskLogic().SelectByObject(objSubTask, 0, 0);
        if (listSubTask.Count > 0)
        {
            foreach (TMisMonitorSubtaskVo obj in listSubTask)
            {
                strMonitorType += new TBaseMonitorTypeInfoLogic().Details(obj.MONITOR_ID).MONITOR_TYPE_NAME + "，";
            }
        }
        return strMonitorType.Length > 0 ? strMonitorType.Remove(strMonitorType.LastIndexOf("，")) : "";
    }
    #endregion
    #endregion

    #region 基础信息生成
    /// <summary>
    /// 获得委托单位信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetClientInfo()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.ID.Value);
        TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
        if (!string.IsNullOrEmpty(objTask.CLIENT_COMPANY_ID))
        {
            objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(new TMisMonitorTaskCompanyVo() { ID = objTask.CLIENT_COMPANY_ID, IS_DEL = "0" });
        }
        return ToJson(objTaskCompany);
    }

    /// <summary>
    /// 获得受检单位信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetTestedInfo()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.ID.Value);
        TMisMonitorTaskCompanyVo objTaskCompany = new TMisMonitorTaskCompanyVo();
        if (!string.IsNullOrEmpty(objTask.TESTED_COMPANY_ID))
        {
            objTaskCompany = new TMisMonitorTaskCompanyLogic().Details(new TMisMonitorTaskCompanyVo() { ID = objTask.TESTED_COMPANY_ID, IS_DEL = "0" });
        }
        return ToJson(objTaskCompany);
    }

    /// <summary>
    /// 获得点位信息
    /// </summary>
    /// <returns></returns>
    protected string GetSampleInfo()
    {
        int intTotalCount = 0;
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        string strContractType = Request.QueryString["strContractType"];//委托类型
        DataTable dt = new DataTable();//结果集
        //所有样品信息
        //所有样品信息
        //if (!string.IsNullOrEmpty(Request.QueryString["QC"]) && Request.QueryString["QC"] == "true")
        //{
        intTotalCount = new TMisMonitorSampleInfoLogic().GetAllSampleInfoCountByTask(this.ID.Value, Request.QueryString["item_type"]);
        dt = new TMisMonitorSampleInfoLogic().GetAllSampleInfoSourceByTask(this.ID.Value, Request.QueryString["item_type"]);
        //}
        //else
        //{
        //intTotalCount = new TMisMonitorSampleInfoLogic().GetSampleInfoCountByTask(this.ID.Value, Request.QueryString["item_type"]);
        //dt = new TMisMonitorSampleInfoLogic().GetSampleInfoSourceByTask(this.ID.Value, Request.QueryString["item_type"], intPageIndex, intPageSize);
        //}
        return CreateToJson(dt, intTotalCount);
    }

    /// <summary>
    /// 通过点位ID获得项目信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetItemInfoBySampleID()
    {
        int intTotalCount = 0;
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        string strSampleID = "";//样品ID
        string strQcType = "0";// 质控类型
        if (!string.IsNullOrEmpty(Request.QueryString["sample_id"]))
        {
            strSampleID = Request.QueryString["sample_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["qc_type"]))
        {
            strQcType = Request.QueryString["qc_type"].ToString();
        }
        strQcType = "";
        //结果集
        DataTable dt = new DataTable();
        //根据样品查询
        if (strSampleID.Length > 0)
        {
            intTotalCount = new TMisMonitorResultLogic().GetSelectResultCount(new TMisMonitorResultVo() { SAMPLE_ID = strSampleID, QC_TYPE = strQcType });
            dt = new TMisMonitorResultLogic().SelectByTableForReport(strSampleID, strQcType, intPageIndex, intPageSize);
            //超标项目结果标记
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (IsOver(dr["ITEM_ID"].ToString(), dr["POINT_ID"].ToString()))//超标
                    {
                        dr["ITEM_RESULT"] = "red" + dr["ITEM_RESULT"].ToString();
                    }
                }
            }
        }

        return LigerGridDataToJson(dt, intTotalCount);
    }

    /// <summary>
    /// 获得监测类别
    /// </summary>
    /// <returns>Json</returns>
    protected string getMonitorType()
    {
        TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
        objMonitorType.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(objMonitorType);
        DataRow dr = dt.NewRow();
        dr["MONITOR_TYPE_NAME"] = "所有监测类别";
        dt.Rows.InsertAt(dr, 0);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 获得质控手段
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcType(string strValue)
    {
        string strReturn = "";
        if (strValue.Contains(","))
        {
            string[] arrQcTYpe = strValue.Split(',');//质控手段数组
            foreach (string str in arrQcTYpe)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    //质控手段编码转换成名称
                    switch (str)
                    {
                        case "0":
                            strReturn += "原始样，";
                            break;
                        case "1":
                            strReturn += "现场空白，";
                            break;
                        case "2":
                            strReturn += "现场加标，";
                            break;
                        case "3":
                            strReturn += "现场平行，";
                            break;
                        case "4":
                            strReturn += "实验室密码平行，";
                            break;
                        case "5":
                            strReturn += "实验室空白，";
                            break;
                        case "6":
                            strReturn += "实验室加标，";
                            break;
                        case "7":
                            strReturn += "实验室明码平行，";
                            break;
                        case "8":
                            strReturn += "标准样，";
                            break;
                    }
                }
            }
        }
        return strReturn.Length > 0 ? strReturn.Remove(strReturn.LastIndexOf("，")) : "";
    }
    #endregion

    #region 报告
    /// <summary>
    /// 监测任务监测类别数
    /// </summary>
    /// <param name="strTaskID">监测任务ID</param>
    /// <returns>String</returns>
    protected string getTaskMonitorTypeCount(string strTaskID, ref string strType)
    {
        int intCount = 0;
        List<TMisMonitorSubtaskVo> listSubTask = new TMisMonitorSubtaskLogic().SelectByObject(new TMisMonitorSubtaskVo() { TASK_ID = strTaskID }, 0, 0);
        if (listSubTask.Count > 0)
        {
            foreach (TMisMonitorSubtaskVo obj in listSubTask)
            {
                if (!strType.Contains(obj.MONITOR_ID))
                {
                    strType += obj.MONITOR_ID + "|";
                    intCount++;
                }
            }
        }
        if (intCount > 0 && strType.Length > 0)
        {
            strType = strType.Remove(strType.LastIndexOf("|"));
        }
        return intCount.ToString();
    }

    /// <summary>
    /// 获得监测任务下所有的监测类别数据
    /// </summary>
    /// <param name="strTaskID">监测任务ID</param>
    /// <returns>Json</returns>
    protected string getTaskMonitorType(string strTaskID)
    {
        DataTable dt = new TBaseMonitorTypeInfoLogic().getItemTypeByTask(strTaskID);
        return DataTableToJson(dt);
    }

    #endregion

    #region 保存
    protected string SaveData()
    {
        if (!string.IsNullOrEmpty(this.ID.Value))
        {
            //监测结论
            string strConclusion = !string.IsNullOrEmpty(Request.QueryString["conclusion"]) ? Request.QueryString["conclusion"].ToString() : "";
            //报告单号
            string strReportCode = !string.IsNullOrEmpty(Request.QueryString["report_code"]) ? Request.QueryString["report_code"].ToString() : "";
            //保存报告单号
            TMisMonitorReportVo objReport = new TMisMonitorReportLogic().Details(new TMisMonitorReportVo() { TASK_ID = this.ID.Value });
            objReport.REPORT_CODE = strReportCode;
            objReport.REPORT_SCHEDULER = LogInfo.UserInfo.ID;
            new TMisMonitorReportLogic().Edit(objReport);
            //保存监测结论 寄存到监测任务备注1
            //监测任务对象
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            objTask.ID = this.ID.Value;
            objTask.TASK_STATUS = "11";
            objTask.FINISH_DATE = DateTime.Now.ToString();
            objTask.REMARK5 = strConclusion;
            if (new TMisMonitorTaskLogic().Edit(objTask))
            {
                WriteLog("修改监测任务", "", LogInfo.UserInfo.USER_NAME + "修改监测任务" + objTask.ID + "成功");
                return "1";
            }
        }
        return "";
    }
    #endregion

    /// <summary>
    /// 获取噪声类的子任务ID
    /// </summary>
    /// <param name="strValue">任务ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getNoiseSubtaskID(string strValue)
    {
        string strReturn = "";
        DataTable dt = new TMisMonitorSubtaskLogic().SelectByTable(new TMisMonitorSubtaskVo() { TASK_ID = strValue });
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MONITOR_ID"].ToString().Contains("000000004"))
                {
                    strReturn = dt.Rows[i]["ID"].ToString();
                    break;
                }
            }
        }
        return strReturn;
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList == null)
            return;
        //传递参数
        string strTaskId = myServiceList.Count > 0 ? myServiceList[0].SERVICE_KEY_VALUE : "";
        //监测任务ID
        this.ID.Value = strTaskId;
        //委托书ID
        this.ContractID.Value = new TMisMonitorTaskLogic().Details(this.ID.Value).CONTRACT_ID;
        //报告ID
        this.reportId.Value = new TRptFileLogic().getNewReportByContractID(strTaskId).ID;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        #region 初始化环节信息
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.ID.Value);
        if (objTask.CONTRACT_CODE == "")
            wfControl.ServiceCode = objTask.TICKET_NUM;
        else
            wfControl.ServiceCode = objTask.CONTRACT_CODE;
        wfControl.ServiceName = objTask.PROJECT_NAME;
        #endregion
        //更改监测任务状态
        objTask.TASK_STATUS = "10";
        new TMisMonitorTaskLogic().Edit(objTask);
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("报告流程", "task_id", this.ID.Value);
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
        //SaveBusinessData();
    }
    #endregion
}