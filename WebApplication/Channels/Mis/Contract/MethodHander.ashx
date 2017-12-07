<%@ WebHandler Language="C#" Class="MethodHander" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Web.SessionState;
using System.Configuration;
using i3.ValueObject.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.Industry;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;

using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;

using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.River;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
/// <summary>
/// Create By 胡方扬 委托书录入 2012-11-30
/// </summary>
public class MethodHander : PageBase, IHttpHandler, IRequiresSessionState
{
    public string strMessage = "";
    public string strAction = "", strType = "";//执行方法，字典类别
    //委托单位信息
    public string strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "", strWorkTask_id = "", strTestCompanyId = "";
    //委托书核定信息
    public string strContratType = "", strContratYear = "", strMonitroType = "", strContratId = "", strContractFee = "", Company_Names = "", strAskingDate = "";
    //受检企业信息
    public string strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";
    //监测点位信息
    public string strPointId = "", strPointName = "", strDYNAMIC_ATTRIBUTE_ID = "", strSampleFreq = "", strFREQ = "", strCREATE_DATE = "", strPointAddress = "", strLONGITUDE = "", strLATITUDE = "", strNUM = "", strNATIONAL_ST_CONDITION_ID = "", strLOCAL_ST_CONDITION_ID = "", strINDUSTRY_ST_CONDITION_ID = "", strStanardItemId = "";
    public string strPointItemId = "", strPointAddItemsId = "", strPointItemsMoveId = "", strSampleDay = "";
    //核对委托书信息
    public string strStatus = "", strProjectName = "", strContractCode = "", strRpt_Way = "", strContract_Date = "", strMonitor_Purpose = "", strAGREE_OUTSOURCING = "", strAGREE_METHOD = "", strAGREE_NONSTANDARD = "", strAGREE_OTHER = "", strSampleSource = "", strBookType = "", strQuck = "";
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    public string strContractPointId = "";
    ////监测项目附加项目ID、费用ID
    public string strAtt_item_Id = "", strAttFeeId = "", strAttFee = "", strAttAddItemsId = "", strAttMoveItemsId = "", strAttItemName = "", strAttItemInfor = "";
    public string strFeeId = "", strFeeTest_FeeSum = "", strFeeAtt_FeeSum = "", strBudGet = "", strIncome = "", strTestNum = "", strTestPointNum = "", strTestAnsyFee = "";
    //此处为自送样变量
    public string strSampleAccept = "", strSampleMan = "", strSampleStatus = "", strSrcCodeOrName = "", strSampleDateOrAcc = "", strSampleId = "", strSampleName = "", strSampleType = "", strSampleCount = "";
    public string strProData = "", strOtherAsk = "", strAccording = "", strtxtRemarks = "", SAMPLENUM = "1", FREQ = "1";//其他要求说明信息
    //流程ID
    public string strWF_ID = "";
    public string strKey = "";
    //验收委托
    public string strRadioInfo = "";     //验收委托的Radio信息
    public string strPFYQ = "";          //设计产量
    public string strSZCL = "";          //实际产量
    public string strBL = "";            //比例

    public string strMonitorName = "";
    public string strMonitorID = "";

    public string strCCFLOW_WORKID = "";  //流程ID

    //监测计划信息
    public string strPlanId = "", strFlag = "";

    public string filenameA = "", strContratID = "";
    public string strTask_code = "";//任务单号

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID))
        {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
        //获取参数值
        GetRequestParme(context);

        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                //获取行业类别
                case "GetIndustryInfo":
                    context.Response.Write(GetIndustryInfo());
                    context.Response.End();
                    break;
                //获取字典项
                case "GetDict":
                    context.Response.Write(GetDict(strType));
                    context.Response.End();
                    break;
                //获取企业基本信息
                case "GetCompanyInfo":
                    context.Response.Write(GetCompanyInfo());
                    context.Response.End();
                    break;
                //获取年度
                case "GetContratYear":
                    context.Response.Write(GetContratYear());
                    context.Response.End();
                    break;
                //获取监测类别
                case "GetMonitorType":
                    context.Response.Write(GetMonitorType());
                    context.Response.End();
                    break;
                //检查企业是否存在
                case "checkCompany":
                    context.Response.Write(checkCompany());
                    context.Response.End();
                    break;
                //根据用户需求插入企业信息
                case "InsertCompany":
                    context.Response.Write(InsertCompany());
                    context.Response.End();
                    break;
                //根据用户需求插入企业信息返回企业信息
                case "InsertBaseCompany":
                    context.Response.Write(InsertBaseCompany());
                    context.Response.End();
                    break;
                //插入委托书信息
                case "InsertInfo":
                    context.Response.Write(InsertInfo());
                    context.Response.End();
                    break;
                case "InsertContratPointForID":
                    context.Response.Write(InsertContratPointForID());
                    context.Response.End();
                    break;
                case "DelContratPointForID":
                    context.Response.Write(DelContratPointForID());
                    context.Response.End();
                    break;
                //修改委托书信息
                case "EditInfo":
                    context.Response.Write(EditInfo());
                    context.Response.End();
                    break;
                //获取指定委托书监测类别
                case "GetContractMonitorType":
                    context.Response.Write(GetContractMonitorType());
                    context.Response.End();
                    break;
                //获取委托书点位
                case "GetContractPoint":
                    context.Response.Write(GetContractPoint(strContratId, strMonitroType));
                    context.Response.End();
                    break;
                //获取委托书点位监测项目
                case "GetContractPointItem":
                    context.Response.Write(GetContractPointItem());
                    context.Response.End();
                    break;
                //获取监测项目
                case "GetMonitorItems":
                    context.Response.Write(GetMonitorItems());
                    context.Response.End();
                    break;
                //检查是否存在该点位
                case "IsExistContractPoint":
                    context.Response.Write(IsExistContractPoint());
                    context.Response.End();
                    break;
                //保存新增点位信息
                case "SaveContractPoint":
                    context.Response.Write(SaveContractPoint());
                    context.Response.End();
                    break;
                //获取委托书点位信息
                case "GetContractPointInfor":
                    context.Response.Write(GetContractPointInfor());
                    context.Response.End();
                    break;
                //获取委托书评价标准条件项信息
                case "GetStanardInfor":
                    context.Response.Write(GetStanardInfor());
                    context.Response.End();
                    break;
                //删除委托书点位信息
                case "DelContractPoint":
                    context.Response.Write(DelContractPoint());
                    context.Response.End();
                    break;
                //初始化监测项目左侧ListBox（未选）
                case "GetMonitorSubItems":
                    context.Response.Write(GetMonitorSubItems());
                    context.Response.End();
                    break;
                //初始化监测项目右侧ListBox（已选）
                case "GetSelectedMonitorItems":
                    context.Response.Write(GetSelectedMonitorItems());
                    context.Response.End();
                    break;
                //保存监测项目数据
                case "SaveDivItemData":
                    context.Response.Write(SaveDivItemData());
                    context.Response.End();
                    break;
                //发送统一保存修改委托书信息（动态生成委托书编号）
                case "SaveCheckContractInfor":
                    context.Response.Write(SaveCheckContractInfor());
                    context.Response.End();
                    break;
                //获取委托书列表数据（未提交和已提交的）
                case "GetContractListData":
                    context.Response.Write(GetContractListData());
                    context.Response.End();
                    break;
                //获取指定工作流第一个节点
                case "GetWFfirstNode":
                    context.Response.Write(GetWFfirstNode());
                    context.Response.End();
                    break;
                //获取委托书企业信息
                case "GetContractCompanyInfor":
                    context.Response.Write(GetContractCompanyInfor());
                    context.Response.End();
                    break;
                //获取委托书企业信息
                case "GetContractCompany":
                    context.Response.Write(GetContractCompany());
                    context.Response.End();
                    break;
                ///删除委托书
                case "DeleteContractInfor":
                    context.Response.Write(DeleteContractInfor());
                    context.Response.End();
                    break;
                //获取指定监测类型的监测项目
                case "GetItemList":
                    context.Response.Write(GetItemList());
                    context.Response.End();
                    break;
                //获取指定委托书监测费用明细表
                case "GetContractConstFeeDetail":
                    context.Response.Write(GetContractConstFeeDetail());
                    context.Response.End();
                    break;
                //获取附件项目列表
                case "GetAttItemList":
                    context.Response.Write(GetAttItemList());
                    context.Response.End();
                    break;
                //获取指定委托书设置的附件项目费用明细表
                case "GetAttFeeDetail":
                    context.Response.Write(GetAttFeeDetail());
                    context.Response.End();
                    break;
                //获取指定委托书的费用总额表（含监测项目费用、附加项目费用、实际费用） 
                case "GetConstractFeeCount":
                    context.Response.Write(GetConstractFeeCount());
                    context.Response.End();
                    break;
                //更新当前委托书附加费用
                case "UpdateAttFeeInfor":
                    context.Response.Write(UpdateAttFeeInfor());
                    context.Response.End();
                    break;
                //获取未当前委托书未选择的附加项目列表
                case "GetSubAttItems":
                    context.Response.Write(GetSubAttItems());
                    context.Response.End();
                    break;
                //获取当前委托书已选的附加项目列表
                case "GetSelectedAttItems":
                    context.Response.Write(GetSelectedAttItems());
                    context.Response.End();
                    break;
                //添加移除附加项目
                case "SaveDivAttItemData":
                    context.Response.Write(SaveDivAttItemData());
                    context.Response.End();
                    break;
                //新增监测点位 附加项目
                case "SaveDivAttItems":
                    context.Response.Write(SaveDivAttItems());
                    context.Response.End();
                    break;
                //删除监测点位 附加项目费用信息
                case "DelAttFeeItems":
                    context.Response.Write(DelAttFeeItems());
                    context.Response.End();
                    break;
                //用户自定义更新信息
                case "UpdateConstractFeeCount":
                    context.Response.Write(UpdateConstractFeeCount());
                    context.Response.End();
                    break;
                //获取委托书列表并合并获取受检企业信息
                case "GetContractInfor":
                    context.Response.Write(GetContractInfor());
                    context.Response.End();
                    break;
                case "GetEnvPlanInfo":
                    context.Response.Write(GetEnvPlanInfo());
                    context.Response.End();
                    break;
                //获取委托书列表关联获取委托、受检企业信息
                case "GetAcceptContractInfor":
                    context.Response.Write(GetAcceptContractInfor());
                    context.Response.End();
                    break;
                //获取可用用户列表
                case "GetUserList":
                    context.Response.Write(GetUserList());
                    context.Response.End();
                    break;
                //获取自送样委托书样品信息
                case "GetContractSample":
                    context.Response.Write(GetContractSample());
                    context.Response.End();
                    break;
                //插入自送样委托书样品信息
                case "SaveContractSample":
                    context.Response.Write(SaveContractSample());
                    context.Response.End();
                    break;
                //插入自送样委托书样品信息(企业信息设置的测点和项目)
                case "SaveSamplePoint":
                    context.Response.Write(SaveSamplePoint());
                    context.Response.End();
                    break;
                //删除委托书样品信息
                case "DelContractSample":
                    context.Response.Write(DelContractSample());
                    context.Response.End();
                    break;
                //获取委托书自送样样品未选监测项目
                case "GetMonitorSubSampleItems":
                    context.Response.Write(GetMonitorSubSampleItems());
                    context.Response.End();
                    break;
                //获取委托书自送样样品已选监测项目
                case "GetSelectedMonitorSampleItems":
                    context.Response.Write(GetSelectedMonitorSampleItems());
                    context.Response.End();
                    break;
                //保存委托书自送样样品已选监测项目
                case "SaveDivSampleItemData":
                    context.Response.Write(SaveDivSampleItemData());
                    context.Response.End();
                    break;
                //获取自送样委托书选择样品的监测项目
                case "GetContractSampleItem":
                    context.Response.Write(GetContractSampleItem());
                    context.Response.End();
                    break;
                //生成快捷委托书监测点位计划信息
                case "CreateQuckPointPlan":
                    context.Response.Write(CreateQuckPointPlan());
                    context.Response.End();
                    break;
                //根据系统时间，自动生成委托年度（当前年度之前5年）历史数据使用
                case "GetContratYearHistory":
                    context.Response.Write(GetContratYearHistory());
                    context.Response.End();
                    break;
                // 获取是否显示委托书费用信息配置
                case "GetHidConstStatus":
                    context.Response.Write(GetHidConstStatus());
                    context.Response.End();
                    break;
                // 获取指定监测类别的点位
                case "GetPointInfor":
                    context.Response.Write(GetPointInfor());
                    context.Response.End();
                    break;
                // 获取指定点位的监测类别
                case "GetItemInfor":
                    context.Response.Write(GetItemInfor());
                    context.Response.End();
                    break;
                /// 获取指定行业类别的监测项目 胡方扬 2013-03-14
                case "GetIndurstyAllItems":
                    context.Response.Write(GetIndurstyAllItems());
                    context.Response.End();
                    break;
                //根据点位，行业类别，确定企业已选的监测项目 胡方扬 2013-03-14
                case "GetIndurstySelectedItems":
                    context.Response.Write(GetIndurstySelectedItems());
                    context.Response.End();
                    break;
                //生成用户自定义虚拟委托信息
                case "CreateDefinePointPlan":
                    context.Response.Write(CreateDefinePointPlan());
                    context.Response.End();
                    break;
                // 插入环境质量类委托书（作为虚拟数据使用） 
                case "InsertContractInforForPendingEvn":
                    context.Response.Write(InsertContractInforForPendingEvn());
                    context.Response.End();
                    break;
                // 插入环境质量类委托书监测计划
                case "SavePlanInforEnv":
                    context.Response.Write(SavePlanInforEnv());
                    context.Response.End();
                    break;
                //case "GetPlanPointForEvn":
                //    context.Response.Write(GetPlanPointForEvn());
                //    context.Response.End();
                //    break;
                // 获取指定KeyName的web.config配置值
                case "GetWebConfigValue":
                    context.Response.Write(GetWebConfigValue());
                    context.Response.End();
                    break;
                case "UpdateTestFree":
                    context.Response.Write(UpdateTestFree());
                    context.Response.End();
                    break;
                // 保存无委托书临时监测监测计划 
                case "SavePlanInforForUnContract":
                    context.Response.Write(SavePlanInforForUnContract());
                    context.Response.End();
                    break;

                // 生成计划点位 
                case "InsertContratPointForPlan":
                    context.Response.Write(InsertContratPointForPlan());
                    context.Response.End();
                    break;
                //获取电话和联系人
                case "GetPhoneInfo":
                    context.Response.Write(GetPhoneInfo(strWorkTask_id, strCompanyIdFrim));
                    context.Response.End();
                    break;
                //插入验收委托书信息
                case "InsertAcceptInfo":
                    context.Response.Write(InsertAcceptInfo());
                    context.Response.End();
                    break;
                //修改验收委托书信息
                case "EditAcceptInfo":
                    context.Response.Write(EditAcceptInfo());
                    context.Response.End();
                    break;
                //修改委托书的监测类型信息
                case "updateMonitorType":
                    context.Response.Write(updateMonitorType());
                    context.Response.End();
                    break;
                //更新验收委托的勘查结果
                case "updateProspecting":
                    context.Response.Write(updateProspecting());
                    context.Response.End();
                    break;
                //通过workID获取业务数据信息
                case "GetTaskInfor":
                    context.Response.Write(GetTaskInfo());
                    context.Response.End();
                    break;
                //通过workID获取常规业务数据信息
                case "GetEnvInfor":
                    context.Response.Write(GetEnvInfor());
                    context.Response.End();
                    break;
                //黄飞 20150924 更新附件保存数据
                case "UpdatAtt":
                    context.Response.Write(UpdatAtt(filenameA, strContratID));
                    context.Response.End();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 通过workID获取常规业务数据信息
    /// 黄进军 添加 20150917
    /// </summary>
    /// <returns></returns>
    private string GetEnvInfor()
    {
        string result = "";

        //i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo objTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
        //objTaskVo.CCFLOW_ID1 = strCCFLOW_WORKID;
        DataTable dt = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().GetEnvInfo(strCCFLOW_WORKID);

        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// //黄飞 20150924 更新附件保存数据
    /// </summary>
    /// <param name="filenameA"></param>
    /// <param name="strContratID"></param>
    /// <returns></returns>
    private string UpdatAtt(string filenameA, string strContratID)
    {

        string result = "";
        DataTable dt = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().UpdatAtt(filenameA, strContratID);


        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    //获取电话和联系人
    public string GetPhoneInfo(string strWorkTask_id, string strCompanyIdFrim)
    {
        string result = "";
        DataTable dt = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().GetPhone(strWorkTask_id, strCompanyIdFrim);
        if (dt.Rows.Count > 0)
        {
            result = i3.View.PageBase.CreateMIMUJson(dt);
        }
        return result;
    }

    /// <summary>
    /// 获取是否显示委托书费用信息配置
    /// </summary>
    /// <returns></returns>
    private string GetHidConstStatus()
    {
        string result = "";
        string strConfigApp = ConfigurationManager.AppSettings["HidConst"].ToString();
        if (!String.IsNullOrEmpty(strConfigApp))
        {
            result = strConfigApp;
        }
        return result;
    }

    /// <summary>
    /// 获取指定KeyName的web.config配置值
    /// </summary>
    /// <returns></returns>
    private string GetWebConfigValue()
    {
        string result = "";
        string strConfigApp = "";
        if (!String.IsNullOrEmpty(strKey))
        {
            strConfigApp = ConfigurationManager.AppSettings[strKey].ToString();
            result = strConfigApp;
        }
        return result;
    }

    /// <summary>
    /// 修改监测费用样品数
    /// </summary>
    /// <returns></returns>
    private string UpdateTestFree()
    {
        string result = "";
        TMisContractTestfeeVo objItems = new TMisContractTestfeeVo();
        objItems.ID = strFeeId;
        if (!String.IsNullOrEmpty(strTestNum))
        {
            objItems.PLAN_ID = strTestNum;
        }
        if (!String.IsNullOrEmpty(strTestPointNum))
        {
            objItems.PROJECT_NAME = strTestPointNum;
        }
        if (!String.IsNullOrEmpty(strTestAnsyFee))
        {
            objItems.CONTRACT_TYPE = strTestAnsyFee;
        }
        if (new TMisContractTestfeeLogic().Edit(objItems))
        {
            result = "true";
        }
        return result;
    }

    /// <summary>
    /// 修改委托书监测类型信息
    /// </summary>
    /// <returns></returns>
    private string updateMonitorType()
    {
        string result = "";
        TMisContractVo objC = new TMisContractVo();
        objC.ID = strContratId;
        objC.TEST_TYPES = strMonitroType;
        result = new TMisContractLogic().Edit(objC).ToString();

        return result;
    }

    /// <summary>
    /// 获取行业类别
    /// </summary>
    /// <returns></returns>
    private string GetIndustryInfo()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objMainVo = new TBaseIndustryInfoVo();
        objMainVo.IS_DEL = "0";
        objMainVo.IS_SHOW = "1";
        dt = new TBaseIndustryInfoLogic().SelectByTable(objMainVo);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }


    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string GetDict(string strDictType)
    {
        return i3.View.PageBase.getDictJsonString(strDictType);
    }

    /// <summary>
    /// 填充委托书列表企业信息
    /// </summary>
    /// <returns></returns>
    private string GetContractCompany()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractCompanyVo TCompanyInfoVo = new TMisContractCompanyVo();
        TCompanyInfoVo.IS_DEL = "0";
        dt = new TMisContractCompanyLogic().SelectByTable(TCompanyInfoVo);
        result = i3.View.PageBase.CreateMIMUJson(dt);
        return result;
    }


    /// <summary>
    /// 自动获取完成企业信息填充
    /// </summary>
    /// <returns></returns>
    private string GetCompanyInfo()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.IS_DEL = "0";
        dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo);
        result = i3.View.PageBase.CreateMIMUJson(dt);
        return result;
    }

    /// <summary>
    /// 根据系统时间，自动生成委托年度（当前年度和下一年度）
    /// </summary>
    /// <returns></returns>
    public string GetContratYear()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("YEAR", typeof(string)));
        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = i.ToString();
            if (i == 0)
            {
                dr["YEAR"] = DateTime.Now.ToString("yyyy");
            }
            else
            {
                dr["YEAR"] = DateTime.Now.AddYears(+1).ToString("yyyy");
            }
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 根据系统时间，自动生成委托年度（当前年度之前5年）历史数据使用
    /// </summary>
    /// <returns></returns>
    public string GetContratYearHistory()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("YEAR", typeof(string)));
        for (int i = 0; i < 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = i.ToString();
            if (i == 0)
            {
                dr["YEAR"] = DateTime.Now.ToString("yyyy");
            }
            else
            {
                dr["YEAR"] = DateTime.Now.AddYears(-i).ToString("yyyy");
            }
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取监测类别
    /// </summary>
    /// <returns></returns>
    public string GetMonitorType()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseMonitorTypeInfoVo objMonitor = new TBaseMonitorTypeInfoVo();
        objMonitor.IS_DEL = "0";
        objMonitor.REMARK5 = "0";
        objMonitor.SORT_FIELD = "CONVERT (INT,SORT_NUM)";
        objMonitor.SORT_TYPE = "asc";
        dt = new TBaseMonitorTypeInfoLogic().SelectByTable(objMonitor, 0, 0);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 保存委托书信息
    /// </summary>
    /// <returns>返回委托书ID</returns>
    private string InsertInfo()
    {
        string result = "", strIContratComId = "", strITestComId = "";
        bool pointFlag = false;

        object objResult = new object();
        //插入委托书信息
        //strContratId = InsertContractInfo();
        string strMsg = "";
        if (InsertContractInfo(out strMsg))
        //if (!string.IsNullOrEmpty(strContratId))
        {
            strContratId = strMsg;

            //插入委托企业信息
            strIContratComId = InsertContractCompanyInfo(false);
            //插入受检企业信息
            strITestComId = InsertContractCompanyInfo(true);

            if (strBookType == "0")
            {
                //插入受检企业委托监测点位信息
                if (String.IsNullOrEmpty(strQuck))
                {
                    pointFlag = InsertContratPoint();
                }
                else
                {
                    pointFlag = InsertContratPointForContractType();
                }
                if (!String.IsNullOrEmpty(strIContratComId) && !String.IsNullOrEmpty(strITestComId) && pointFlag)
                {
                    if (EditContratInfo(strIContratComId, strITestComId))
                    {
                        //result = strContratId.ToString();
                        objResult = new { result = true, Msg = strContratId.ToString() };
                    }
                }
            }
            if (strBookType != "0")
            {
                //插入验收监测费用 Create by SSZ 
                if (strBookType == "1")
                {
                    SaveContractFee();
                }
                if (!String.IsNullOrEmpty(strIContratComId) && !String.IsNullOrEmpty(strITestComId))
                {
                    if (EditContratInfo(strIContratComId, strITestComId))
                    {
                        //result = strContratId.ToString();
                        objResult = new { result = true, Msg = strContratId.ToString() };
                    }
                }
            }
        }
        else
        {
            objResult = new { result = false, Msg = strMsg };
        }
        return ToJson(objResult);
    }

    /// <summary>
    /// 保存验收委托书信息 Create By weilin 2014-10-15
    /// </summary>
    /// <returns>返回委托书ID</returns>
    private string InsertAcceptInfo()
    {
        string result = "", strIContratComId = "", strITestComId = "";
        string strCodeRule = "";

        //插入委托书信息
        TMisContractVo objtCv = new TMisContractVo();
        objtCv.ID = i3.View.PageBase.GetSerialNumber("t_mis_contrat_Id");
        objtCv.CONTRACT_TYPE = strContratType;
        objtCv.CONTRACT_YEAR = strContratYear;
        objtCv.PROJECT_NAME = strProjectName;
        objtCv.CONTRACT_STATUS = "0";
        objtCv.TEST_TYPE = "0";
        objtCv.BOOKTYPE = "1";
        objtCv.SAMPLE_SOURCE = "抽样";
        objtCv.TEST_PURPOSE = "验收监测";
        //生成委托书单号
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SERIAL_TYPE = "1";
        strCodeRule = CreateBaseDefineCode(objSerial, objtCv);
        objtCv.CONTRACT_CODE = strCodeRule;
        objtCv.PROVIDE_DATA = strPFYQ;   //批复要求
        objtCv.OTHER_ASKING = strSZCL;   //实际产量
        objtCv.MONITOR_ACCORDING = strBL;//比例
        objtCv.QC_STEP = strNUM;         //废气烟道条数
        objtCv.REMARK1 = strtxtRemarks;
        objtCv.REMARK2 = strRadioInfo;   //验收委托Radio集合

        if (new TMisContractLogic().Create(objtCv))
        {
            strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书" + objtCv.ID + "成功";
            //插入委托企业信息
            strCompanyId = getBaseCompanyID(strCompanyName, strAreaId, strContactName, strAddress, strTelPhone);
            strIContratComId = InsertCCompanyInfo(objtCv.ID, strCompanyId, strCompanyName, strAreaId, strContactName, strTelPhone, strAddress);
            //插入受检企业信息
            strCompanyIdFrim = getBaseCompanyID(strCompanyNameFrim, strAreaIdFrim, strContactNameFrim, strAddressFrim, strTelPhoneFrim);
            strITestComId = InsertCCompanyInfo(objtCv.ID, strCompanyIdFrim, strCompanyNameFrim, strAreaIdFrim, strContactNameFrim, strTelPhoneFrim, strAddressFrim);

            //更改委托书的企业信息
            TMisContractVo obj = new TMisContractVo();
            obj.ID = objtCv.ID;
            obj.CLIENT_COMPANY_ID = strIContratComId;
            obj.TESTED_COMPANY_ID = strITestComId;
            new TMisContractLogic().Edit(obj);
        }
        result = objtCv.ID + "|" + objtCv.CONTRACT_CODE;
        return result;
    }
    private string getBaseCompanyID(string strName, string strArea, string strContractMan, string strAdd, string strPhone)
    {
        TBaseCompanyInfoVo CompanyInfoVo = new TBaseCompanyInfoVo();
        CompanyInfoVo.COMPANY_NAME = strName;
        CompanyInfoVo = new TBaseCompanyInfoLogic().SelectByObject(CompanyInfoVo);
        if (CompanyInfoVo.ID.Length == 0)
        {
            CompanyInfoVo.ID = i3.View.PageBase.GetSerialNumber("Company_Id");
            CompanyInfoVo.AREA = strArea;
            CompanyInfoVo.CONTACT_NAME = strContractMan;
            CompanyInfoVo.CONTACT_ADDRESS = strAdd;
            CompanyInfoVo.MOBAIL_PHONE = strPhone;
            new TBaseCompanyInfoLogic().Create(CompanyInfoVo);
        }
        return CompanyInfoVo.ID;
    }
    private string InsertCCompanyInfo(string strContractID, string strCompanyID, string strCName, string strArea, string strContractMan, string strPhoe, string strAdd)
    {
        TMisContractCompanyVo CCompanyVo = new TMisContractCompanyVo();
        CCompanyVo.ID = i3.View.PageBase.GetSerialNumber("t_mis_contratcompany_Id");
        CCompanyVo.CONTRACT_ID = strContractID;
        CCompanyVo.COMPANY_ID = strCompanyID;
        CCompanyVo.COMPANY_NAME = strCName;
        CCompanyVo.AREA = strArea;
        CCompanyVo.CONTACT_NAME = strContractMan;
        CCompanyVo.CONTACT_ADDRESS = strAdd;
        CCompanyVo.PHONE = strPhoe;
        CCompanyVo.IS_DEL = "0";
        new TMisContractCompanyLogic().Create(CCompanyVo);
        return CCompanyVo.ID;
    }
    /// <summary>
    /// 编辑修改验收委托单信息  Create By weilin 2014-10-16
    /// </summary>
    /// <returns>返回true Or false</returns>
    private bool EditAcceptInfo()
    {
        bool result = false;
        DataTable dtCont = new DataTable();
        TMisContractVo objContratV = new TMisContractVo();
        objContratV.ID = strContratId;

        objContratV.CONTRACT_YEAR = strContratYear;
        objContratV.PROJECT_NAME = strProjectName;
        objContratV.PROVIDE_DATA = strPFYQ;   //批复要求
        objContratV.OTHER_ASKING = strSZCL;   //实际产量
        objContratV.MONITOR_ACCORDING = strBL;//比例
        objContratV.QC_STEP = strNUM;         //废气烟道条数
        objContratV.REMARK1 = strtxtRemarks;
        objContratV.REMARK2 = strRadioInfo;   //验收委托Radio集合

        result = new TMisContractLogic().Edit(objContratV);

        return result;

    }
    /// <summary>
    /// 更新验收委托勘查结果信息  Create By weilin 2014-11-19
    /// </summary>
    /// <returns>返回true Or false</returns>
    private bool updateProspecting()
    {
        bool result = false;
        TMisContractVo objContratV = new TMisContractVo();
        objContratV.ID = strContratId;

        objContratV.REMARK3 = strRadioInfo;   //验收委托Radio集合

        result = new TMisContractLogic().Edit(objContratV);

        return result;
    }

    /// <summary>
    /// 插入环境质量类委托书（作为虚拟数据使用） 2013-04-03
    /// </summary>
    /// <returns></returns>
    private string InsertContractInforForPendingEvn()
    {
        string result = "", strCodeRule = "";
        TMisContractVo objtCv = new TMisContractVo();
        objtCv.ID = i3.View.PageBase.GetSerialNumber("t_mis_contrat_Id");
        objtCv.CONTRACT_TYPE = strContratType;
        objtCv.CONTRACT_YEAR = strContratYear;
        objtCv.PROJECT_NAME = strProjectName;
        objtCv.CONTRACT_STATUS = "9";
        objtCv.SAMPLE_SOURCE = "抽样";
        objtCv.BOOKTYPE = "0";
        objtCv.ISQUICKLY = "3";
        //string[] strCodeRule = new string[4] { "G" + strContratYear.ToString(), "", strContratType.ToString(), i3.View.PageBase.GetSerialNumber("contract_serialnumber") };
        //objtCv.CONTRACT_CODE = i3.View.PageBase.CreateSerialNumber(strCodeRule);
        TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
        objSerial.SERIAL_TYPE = "1";
        strCodeRule += CreateBaseDefineCode(objSerial, objtCv);
        objtCv.CONTRACT_CODE = strCodeRule;
        if (new TMisContractLogic().Create(objtCv))
        {
            result = objtCv.ID;
            strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书" + objtCv.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractInfo, "", strMessage);
        }
        return result;
    }

    /// <summary>
    /// 获取江河水点位 2013-04-03
    /// </summary>
    /// <param name="strEvnPointId"></param>
    /// <returns></returns>
    //private string GetPlanPointForEvn()
    //{
    //    string result = "";
    //    DataTable dt = new DataTable();
    //    TEnvPointRiverVVo objItem = new TEnvPointRiverVVo();
    //    objItem.RIVER_POINT_ID = strPointId;
    //    dt = new TEnvPointRiverVLogic().SelectByTable(objItem, intPageIndex, intPageSize);
    //    result = LigerGridDataToJson(dt, dt.Rows.Count);

    //    return result;
    //}

    /// 插入委托书
    /// </summary>
    /// <returns></returns>
    private bool InsertContractInfo(out string strMsg)
    {
        #region 当任务单号有值时，先判定任务单号是否可用
        if (!string.IsNullOrEmpty(strTask_code))
        {
            int tempCount = new TMisMonitorTaskLogic().GetSelectResultCountByTicketNum(strTask_code);
            if (tempCount > 0)
            {
                strMsg = "任务单号已经存在";
                return false;
            }
        }
        #endregion

        bool result = false;
        TMisContractVo objtCv = new TMisContractVo();
        objtCv.ID = i3.View.PageBase.GetSerialNumber("t_mis_contrat_Id");
        objtCv.CONTRACT_TYPE = strContratType;
        objtCv.CONTRACT_YEAR = strContratYear;
        objtCv.TEST_TYPES = strMonitroType;
        objtCv.PROJECT_NAME = strProjectName;
        objtCv.CONTRACT_STATUS = "0";
        objtCv.PROVIDE_DATA = strProData;
        objtCv.OTHER_ASKING = strOtherAsk;
        objtCv.MONITOR_ACCORDING = strAccording;
        objtCv.REMARK2 = strtxtRemarks;
        objtCv.REMARK1 = strSampleSource;
        objtCv.CCFLOW_ID1 = strCCFLOW_WORKID;

        if (!string.IsNullOrEmpty(strTask_code))
        {
            objtCv.REMARK5 = strTask_code;//REMARK5暂时用来存放任务单号，在发送或保存任务时，任务单号会作为Task表的TICKET_NUM被使用，之后REMARK5无意义
        }

        string[] strArr = strMonitroType.Split(';');
        if (strArr.Length > 1)
        {
            objtCv.TEST_TYPE = "0";
        }
        else
        {
            objtCv.TEST_TYPE = strMonitroType;
        }
        objtCv.BOOKTYPE = strBookType;
        if (strBookType == "0" || strBookType == "1")
        {
            objtCv.SAMPLE_SOURCE = "抽样";
        }
        if (strBookType == "2")
        {
            objtCv.SAMPLE_SOURCE = "送样";
            objtCv.SAMPLE_SEND_MAN = strSampleMan;
            objtCv.SAMPLE_ACCEPTER_ID = strSampleAccept;
            objtCv.SAMPLE_FREQ = strFREQ;
        }
        if (new TMisContractLogic().Create(objtCv))
        {
            result = true;
            strMsg = objtCv.ID;
            strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书" + objtCv.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractInfo, "", strMessage);
        }
        else
        {
            result = false;
            strMsg = "保存失败";
        }

        return result;
    }


    /// <summary>
    /// 生成委托书企业信息
    /// </summary>
    /// <param name="ContractId">委托书ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <param name="strIndustryId">行业ID</param>
    /// <param name="strAreaId">区域ID</param>
    /// <param name="strContactName">联系人姓名</param>
    /// <param name="strTelPhone">联系电话</param>
    /// <param name="strAddress">地址</param>
    /// <param name="isNew">是否新增</param>
    /// <returns></returns>
    private string InsertContractCompanyInfo(bool isFrim)
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractCompanyVo objTmc = new TMisContractCompanyVo();
        //Update By SSZ 将基础资料企业信息复制到委托书企业信息
        //基础企业资料信息
        TBaseCompanyInfoVo objCompanyInfo = new TBaseCompanyInfoLogic().Details(strCompanyId);
        //将相同字段的数据进行复制
        CopyObject(objCompanyInfo, objTmc);
        //Update end 
        objTmc.CONTRACT_ID = strContratId;
        objTmc.COMPANY_ID = strCompanyId;

        objTmc.INDUSTRY = strIndustryId;
        objTmc.AREA = strAreaId;
        objTmc.CONTACT_NAME = strContactName;
        objTmc.PHONE = strTelPhone;

        objTmc.IS_DEL = "0";
        if (isFrim)
        {
            objTmc.COMPANY_NAME = strCompanyNameFrim;
            objTmc.MONITOR_ADDRESS = strAddress;
        }
        else
        {
            objTmc.COMPANY_NAME = strCompanyName;
            objTmc.CONTACT_ADDRESS = strAddress;
        }

        objTmc.ID = i3.View.PageBase.GetSerialNumber("t_mis_contratcompany_Id");
        if (new TMisContractCompanyLogic().Create(objTmc))
        {
            result = objTmc.ID.ToString();
            strMessage = LogInfo.UserInfo.USER_NAME + "添加委托企业" + objTmc.ID + "成功";
        }

        return result;
    }

    /// <summary>
    /// 复制受检企业点位信息到委托企业点位信息表中
    /// </summary>
    /// <param name="strContractId">委托书ID</param>
    /// <param name="strCompanyId">受检企业ID</param>
    /// <param name="strContratType">委托类型</param>
    /// <param name="strMonitroType">监测类型</param>
    /// <returns></returns>
    private bool InsertContratPoint()
    {
        //委托类别，监测类型，所属公司  获取监测点基础数据 插入委托书监测点表中（复制）

        bool result = false;
        DataTable dt = new DataTable();

        string[] strMonitorId = strMonitroType.Split(';');
        foreach (string monitorId in strMonitorId)
        {
            string strCompanyPoinId = "";
            TBaseCompanyPointVo tbV = new TBaseCompanyPointVo();

            tbV.COMPANY_ID = strCompanyId;
            tbV.MONITOR_ID = monitorId;
            tbV.POINT_TYPE = strContratType;
            tbV.IS_DEL = "0";
            dt = new TBaseCompanyPointLogic().SelectByTable(tbV);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drr in dt.Rows)
                {
                    strCompanyPoinId += drr["ID"].ToString() + ";";
                }

                if (new TMisContractPointLogic().InsertContratPoint(strContratId, strCompanyPoinId))
                {
                    result = true;
                    strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位成功";
                    //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
                }
            }
            else
            {
                result = true;
            }
        }
        return result;
    }

    /// <summary>
    /// 删除指定委托书的指定点位
    /// </summary>
    /// <returns></returns>
    private bool DelContratPointForID()
    {
        bool result = false;
        TMisContractPointVo objPointVo = new TMisContractPointVo();
        objPointVo.ID = strPointId;
        if (new TMisContractPointLogic().Delete(objPointVo))
        {
            result = true;
        }
        return result;
    }
    /// <summary>
    ///增加点位，并将增加的点位复制到指定委托书的点位信息表中
    /// </summary>
    /// <returns></returns>
    private bool InsertContratPointForID()
    {
        //委托类别，监测类型，所属公司  获取监测点基础数据 插入委托书监测点表中（复制）

        bool result = false;
        DataTable dt = new DataTable();

        TBaseCompanyPointVo tbV = new TBaseCompanyPointVo();
        tbV.ID = strPointId;
        dt = new TBaseCompanyPointLogic().SelectByTable(tbV);

        if (dt.Rows.Count > 0)
        {
            if (new TMisContractPointLogic().InsertContratPoint(strContratId, tbV.ID.ToString()))
            {
                TMisContractPointVo objPointVo = new TMisContractPointVo();
                objPointVo.CONTRACT_ID = strContratId;
                objPointVo.POINT_ID = tbV.ID.ToString();

                DataTable dtPoint = new TMisContractPointLogic().SelectByTable(objPointVo);
                if (dtPoint.Rows.Count > 0)
                {
                    TMisContractPointFreqVo objFreqVo = new TMisContractPointFreqVo();
                    objFreqVo.ID = GetSerialNumber("t_mis_contractplanfreqID");
                    objFreqVo.CONTRACT_ID = strContratId;
                    objFreqVo.CONTRACT_POINT_ID = dtPoint.Rows[0]["ID"].ToString();
                    objFreqVo.IF_PLAN = "0";
                    objFreqVo.FREQ = "1";
                    objFreqVo.NUM = "1";
                    objFreqVo.SAMPLE_FREQ = "1";
                    if (new TMisContractPointFreqLogic().Create(objFreqVo))
                    {
                        result = true;
                    }
                }
                strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
            }
        }
        else
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    ///增加点位，并将增加的点位复制到指定委托书的点位信息表中
    /// </summary>
    /// <returns></returns>
    private bool InsertContratPointForPlan()
    {
        //委托类别，监测类型，所属公司  获取监测点基础数据 插入委托书监测点表中（复制）

        bool result = false;
        DataTable dt = new DataTable();
        string[] strPointIds;
        strPointIds = strPointId.Split('、');

        for (int i = 0; i < strPointIds.Length; i++)
        {
            TBaseCompanyPointVo tbV = new TBaseCompanyPointVo();
            tbV.ID = strPointIds[i].ToString();
            dt = new TBaseCompanyPointLogic().SelectByTable(tbV);

            if (dt.Rows.Count > 0)
            {
                string strID = GetSerialNumber("t_mis_contract_PointID");
                if (new TMisContractPointLogic().InsertContratPointForPlan(strID, tbV.ID.ToString()))
                {
                    TMisContractPointVo objPointVo = new TMisContractPointVo();
                    objPointVo.ID = strContratId;
                    objPointVo.POINT_ID = tbV.ID.ToString();

                    DataTable dtPoint = new TMisContractPointLogic().SelectByTable(objPointVo);
                    if (dtPoint.Rows.Count > 0)
                    {
                        TMisContractPointFreqVo objFreqVo = new TMisContractPointFreqVo();
                        objFreqVo.ID = GetSerialNumber("t_mis_contractplanfreqID");
                        objFreqVo.CONTRACT_ID = strContratId;
                        objFreqVo.CONTRACT_POINT_ID = dtPoint.Rows[0]["ID"].ToString();
                        objFreqVo.IF_PLAN = "0";
                        objFreqVo.FREQ = "1";
                        objFreqVo.NUM = "1";
                        objFreqVo.SAMPLE_FREQ = "1";
                        if (new TMisContractPointFreqLogic().Create(objFreqVo))
                        {
                            TMisContractPlanPointVo objPlanPoint = new TMisContractPlanPointVo();
                            objPlanPoint.ID = GetSerialNumber("t_mis_contract_planpointId");
                            objPlanPoint.PLAN_ID = strPlanId;
                            objPlanPoint.POINT_FREQ_ID = objFreqVo.ID;
                            objPlanPoint.CONTRACT_POINT_ID = strID;
                            if (new TMisContractPlanPointLogic().Create(objPlanPoint))
                            {
                                result = true;
                            }
                        }
                    }
                    strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位成功";
                    //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
                }
            }
            else
            {
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// 根据企业与委托书类别复制受检企业点位信息到委托企业点位信息表中
    /// </summary>
    /// <param name="strContractId">委托书ID</param>
    /// <param name="strCompanyId">受检企业ID</param>
    /// <param name="strContratType">委托类型</param>
    /// <param name="strMonitroType">监测类型</param>
    /// <returns></returns>
    private bool InsertContratPointForContractType()
    {
        //委托类别，监测类型，所属公司  获取监测点基础数据 插入委托书监测点表中（复制）

        bool result = false;
        DataTable dt = new DataTable();


        string strCompanyPoinId = "";
        TBaseCompanyPointVo tbV = new TBaseCompanyPointVo();

        tbV.COMPANY_ID = strCompanyIdFrim;
        tbV.POINT_TYPE = strContratType;
        tbV.IS_DEL = "0";
        dt = new TBaseCompanyPointLogic().SelectByTable(tbV);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drr in dt.Rows)
            {
                strCompanyPoinId += drr["ID"].ToString() + ";";
            }

            if (new TMisContractPointLogic().InsertContratPoint(strContratId, strCompanyPoinId))
            {
                result = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
            }
        }
        else
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 生成快捷委托书监测点位频次信息
    /// </summary>
    /// <returns></returns>
    public string CreateQuckPointPlan()
    {
        string result = "", strCodeRule = "";
        if (!String.IsNullOrEmpty(strContratId))
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strContratId;
            //0表示未提交 1表示已提交 流转中，9表示已审核，中间其他数字待议，留做备用
            objItems.CONTRACT_STATUS = "9";
            if (!String.IsNullOrEmpty(strQuck))
            {
                objItems.ISQUICKLY = "5";
            }
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SERIAL_TYPE = "1";

            objItems.CONTRACT_TYPE = strContratType;
            strCodeRule += CreateBaseDefineCode(objSerial, objItems);
            objItems.CONTRACT_CODE = strCodeRule;
            if (new TMisContractLogic().Edit(objItems))
            {
                //if (CreateContractPlan())
                //{
                result = "true";
                //}
                //最后一步进行委托书状态更新 表示走完流程
            }
        }

        return result;
    }

    /// <summary>
    /// 生成快捷委托书监测点位频次信息
    /// </summary>
    /// <returns></returns>
    public string CreateDefinePointPlan()
    {

        string result = "", strCodeRule = "";
        if (!String.IsNullOrEmpty(strContratId))
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strContratId;
            //0表示未提交 1表示已提交 流转中，9表示已审核，中间其他数字待议，留做备用
            objItems.CONTRACT_STATUS = "9";
            objItems.ISQUICKLY = "2";//表示用户自定义的虚拟委托书
            //string[] strCodeRule = new string[4] { "D" + strContratYear.ToString(), strCompanyIdFrim.ToString(), strContratType.ToString(), i3.View.PageBase.GetSerialNumber("contract_serialnumber") };
            //objItems.CONTRACT_CODE = i3.View.PageBase.CreateSerialNumber(strCodeRule);
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SERIAL_TYPE = "1";

            objItems.CONTRACT_TYPE = strContratType;
            if (!String.IsNullOrEmpty(strQuck))
            {
                strCodeRule = "G";
            }
            strCodeRule += CreateBaseDefineCode(objSerial, objItems);
            objItems.CONTRACT_CODE = strCodeRule;
            if (new TMisContractLogic().Edit(objItems))
            {
                if (CreateContractPlanForDefine())
                {
                    result = "true";
                }
                //最后一步进行委托书状态更新 表示走完流程
            }
        }

        return result;
    }

    /// <summary>
    /// 根据用户选择委托类别（环境质量类除外）添加点位采样频次信息
    /// </summary>
    /// <returns></returns>
    private bool CreateContractPlanForDefine()
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strContratId;
        //获取当前委托书监测点位
        dt = GetMonitorPoint();
        if (dt != null)
        {
            if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, strContratId))
            {
                string strMessage = LogInfo.UserInfo.USER_NAME + "采样预约计划点位频次生成成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanFreqInfo, "", strMessage);
                if (SavePlanInfor(strContratId, strCompanyIdFrim))
                {
                    flag = true;
                }
            }
        }
        return flag;
    }
    /// <summary>
    /// 插入监测计划信息
    /// </summary>
    /// <returns></returns>
    private bool CreateContractPlan()
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strContratId;
        //获取当前委托书监测点位
        dt = GetMonitorPoint();
        if (dt != null)
        {
            if (new TMisContractPointFreqLogic().CreatePlanPointQuck(dt, strContratId))
            {
                flag = true;
                string strMessage = LogInfo.UserInfo.USER_NAME + "采样预约计划点位频次生成成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPlanFreqInfo, "", strMessage);
            }
        }
        return flag;
    }

    /// <summary>
    /// 保存(环境质量类)监测预约计划 胡方扬 2013-04-19
    /// </summary>
    /// <returns></returns>
    public bool SavePlanInforEnv()
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strContratId))
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
            objItems.CONTRACT_ID = strContratId;
            dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);

            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && PageBase.IsNumeric(dt.Rows[0]["NUM"].ToString()))
            {
                objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
            }
            else
            {
                objItems.PLAN_NUM = "1";
            }
            if (new TMisContractPlanLogic().Create(objItems))
            {
                if (SavePlanPoint(strContratId, objItems.ID))
                {
                    flag = true;
                }
            }
        }
        return flag;
    }

    /// <summary>
    /// 保存监测预约计划 胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanInfor(string strTaskId, string strCompanyId)
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
            objItems.CONTRACT_ID = strTaskId;
            dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);
            objItems.CONTRACT_COMPANY_ID = strCompanyId;

            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && PageBase.IsNumeric(dt.Rows[0]["NUM"].ToString()))
            {
                objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
            }
            else
            {
                objItems.PLAN_NUM = "1";
            }
            if (new TMisContractPlanLogic().Create(objItems))
            {
                if (SavePlanPoint(strTaskId, objItems.ID))
                {
                    if (SavePlanPeople(strTaskId, objItems.ID))
                    {
                        flag = true;
                    }
                }
            }
        }
        return flag;
    }

    /// <summary>
    /// 插入监测任务预约点位表信息  胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanPoint(string strTaskId, string strPlanId)
    {
        bool flag = false;
        if (new TMisContractPlanPointLogic().SavePlanPoint(strTaskId, strPlanId))
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetPointMonitorInfor(string strTaskId, string strPlanId)
    {

        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strTaskId;
        objItems.IF_PLAN = "0";
        dt = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, strPlanId);
        return dt;
    }

    /// <summary>
    /// 获取指定监测类别的岗位职责信息 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetMonitorDutyInfor()
    {
        DataTable dt = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        objItems.DICT_CODE = "duty_sampling";
        dt = new TSysDutyLogic().SelectTableDutyUser(objItems);
        return dt;
    }

    /// <summary>
    /// 保存默认监测计划岗位负责人 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    private bool SavePlanPeople(string strTaskId, string strPlanId)
    {
        bool flag = false;
        DataTable dtMonitor = GetPointMonitorInfor(strTaskId, strPlanId);
        DataTable dtTemple = GetMonitorDutyInfor();
        DataTable dtMonitorDutyUser = new DataTable();
        dtMonitorDutyUser = dtTemple.Copy();
        dtMonitorDutyUser.Clear();
        //获取默认负责人
        DataRow[] drowArr = dtTemple.Select(" IF_DEFAULT='0'");

        if (drowArr.Length > 0)
        {
            foreach (DataRow drow in drowArr)
            {
                dtMonitorDutyUser.ImportRow(drow);
            }
            dtMonitorDutyUser.AcceptChanges();
        }
        string strMonitorId = "", strUserId = "";
        foreach (DataRow drr in dtMonitor.Rows)
        {
            for (int i = 0; i < dtMonitorDutyUser.Rows.Count; i++)
            {
                if (drr["ID"].ToString() == dtMonitorDutyUser.Rows[i]["MONITOR_TYPE_ID"].ToString())
                {
                    strMonitorId += drr["ID"].ToString() + ";";
                    strUserId += dtMonitorDutyUser.Rows[i]["USERID"].ToString() + ";";
                }
            }
        }
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.CONTRACT_ID = strTaskId;
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            if (!String.IsNullOrEmpty(strMonitorId) && !String.IsNullOrEmpty(strUserId))
            {
                strMonitArr = strMonitorId.Substring(0, strMonitorId.Length - 1).Split(';');
                strUserArr = strUserId.Substring(0, strUserId.Length - 1).Split(';');
                if (strMonitArr != null && strUserArr != null)
                    if (new TMisContractUserdutyLogic().SaveContractPlanDuty(objItems, strMonitArr, strUserArr))
                    {
                        flag = true;
                    }
            }
        }
        return flag;
    }
    /// <summary>
    /// 获取委托书监测点位
    /// </summary>
    /// <returns></returns>
    private DataTable GetMonitorPoint()
    {
        DataTable dt = new DataTable();
        TMisContractPointVo objItems = new TMisContractPointVo();
        objItems.CONTRACT_ID = strContratId;

        dt = new TMisContractPointLogic().SelectByTable(objItems);

        return dt;
    }
    /// <summary>
    /// 编辑修改委托单信息
    /// </summary>
    /// <returns>返回true Or false</returns>
    private string EditInfo()
    {
        object objResult = new object();

        bool result = false;
        DataTable dtCont = new DataTable();
        string strContractCompanyId = "", strTestCompanyId = "";
        TMisContractVo objContratV = new TMisContractVo();
        objContratV.ID = strContratId;
        dtCont = new TMisContractLogic().SelectByTable(objContratV);

        if (dtCont.Rows.Count > 0)
        {
            //获取要修改委托书的 委托单位ID 和受检单位ID
            strContractCompanyId = dtCont.Rows[0]["CLIENT_COMPANY_ID"].ToString();
            strTestCompanyId = dtCont.Rows[0]["TESTED_COMPANY_ID"].ToString();

            if (EditContratInfo(strContractCompanyId, strTestCompanyId))
            {

                if (EditContractCompanyInfo(strContractCompanyId, false))
                {
                    if (EditContractCompanyInfo(strTestCompanyId, true))
                    {
                        result = true;
                    }
                }
            }
        }

        objResult = new { result = result, Msg = "" };
        return ToJson(objResult);

    }

    /// <summary>
    /// 修改委托书企业信息
    /// </summary>
    /// <param name="ContractId">委托书ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <param name="strIndustryId">行业ID</param>
    /// <param name="strAreaId">区域ID</param>
    /// <param name="strContactName">联系人姓名</param>
    /// <param name="strTelPhone">联系电话</param>
    /// <param name="strAddress">地址</param>
    /// <param name="isFrim">委托企业 true,受检企业 false</param>
    /// <returns></returns>
    private bool EditContractCompanyInfo(string strRquestContractCompanyId, bool isFrim)
    {
        bool result = false;

        DataTable dt = new DataTable();


        TMisContractCompanyVo objTmc = new TMisContractCompanyVo();

        if (!isFrim)
        {
            objTmc.ID = strRquestContractCompanyId;
            objTmc.CONTRACT_ID = strContratId;
            objTmc.INDUSTRY = strIndustryId;
            objTmc.AREA = strAreaId;
            objTmc.CONTACT_NAME = strContactName;
            objTmc.PHONE = strTelPhone;
            objTmc.CONTACT_ADDRESS = strAddress;
            objTmc.IS_DEL = "0";
        }
        else
        {

            objTmc.ID = strRquestContractCompanyId;
            objTmc.CONTRACT_ID = strContratId;
            objTmc.INDUSTRY = strIndustryIdFrim;
            objTmc.AREA = strAreaIdFrim;
            objTmc.CONTACT_NAME = strContactNameFrim;
            objTmc.PHONE = strTelPhoneFrim;
            objTmc.CONTACT_ADDRESS = strAddressFrim;
            objTmc.IS_DEL = "0";

        }
        if (new TMisContractCompanyLogic().Edit(objTmc))
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// 修改企业信息
    /// </summary>
    /// <param name="strCompanyId"></param>
    /// <param name="strCompanyName"></param>
    /// <param name="strIndustryId"></param>
    /// <param name="strAreaId"></param>
    /// <param name="strContactName"></param>
    /// <param name="strTelPhone"></param>
    /// <param name="strAddress"></param>
    /// <returns></returns>
    private bool EditCompanyInfo()
    {
        bool result = false;
        DataTable dt = new DataTable();
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();

        TBaseCompanyInfoVo.INDUSTRY = strIndustryId;
        TBaseCompanyInfoVo.AREA = strAreaId;
        TBaseCompanyInfoVo.CONTACT_NAME = strContactName;
        TBaseCompanyInfoVo.PHONE = strTelPhone;
        TBaseCompanyInfoVo.CONTACT_ADDRESS = strAddress;

        TBaseCompanyInfoVo.ID = strCompanyId;
        if (new TBaseCompanyInfoLogic().Edit(TBaseCompanyInfoVo))
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 修改委托书信息
    /// </summary>
    /// <param name="strContratId"></param>
    /// <param name="strContratType"></param>
    /// <param name="strContratYear"></param>
    /// <param name="strMonitroType"></param>
    /// <returns></returns>
    private bool EditContratInfo(string strIContratComId, string strITestComId)
    {
        bool result = false;
        DataTable dt = new DataTable();
        TMisContractVo objCvo = new TMisContractVo();
        objCvo.ID = strContratId;
        objCvo.CONTRACT_TYPE = strContratType;
        objCvo.CONTRACT_YEAR = strContratYear;
        objCvo.TEST_TYPES = strMonitroType;
        objCvo.REMARK1 = strSampleSource;
        string[] strArr = strMonitroType.Split(';');
        if (strArr.Length > 1)
        {
            objCvo.TEST_TYPE = "0";
        }
        else
        {
            objCvo.TEST_TYPE = strMonitroType;
        }
        if (!String.IsNullOrEmpty(strBookType))
        {
            //如果是自送样，则修改送样人等信息
            if (strBookType == "2")
            {
                objCvo.SAMPLE_SEND_MAN = strSampleMan;
                objCvo.SAMPLE_ACCEPTER_ID = strSampleAccept;
                objCvo.SAMPLE_FREQ = strFREQ;
            }
        }

        objCvo.CLIENT_COMPANY_ID = strIContratComId;
        objCvo.TESTED_COMPANY_ID = strITestComId;
        if (new TMisContractLogic().Edit(objCvo))
        {
            result = true;
            strMessage = LogInfo.UserInfo.USER_NAME + "编辑委托书" + objCvo.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractInfo, "", strMessage);
        }

        return result;
    }

    /// <summary>
    /// 检查是否已存在该企业基础信息
    /// </summary>
    /// <param name="strCompanyName"></param>
    /// <returns></returns>
    public string checkCompany()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.COMPANY_NAME = strCompanyName;
        TBaseCompanyInfoVo.IS_DEL = "0";
        dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo);

        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["ID"].ToString();
        }

        return result;
    }
    /// <summary>
    /// 如果不存在企业基本信息，且用户确定要加入，则加入
    /// </summary>
    /// <returns>返回企业ID</returns>
    private string InsertCompany()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.COMPANY_NAME = strCompanyName;
        TBaseCompanyInfoVo.IS_DEL = "0";
        dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo);

        if (dt.Rows.Count <= 0)
        {
            TBaseCompanyInfoVo.INDUSTRY = strIndustryId;
            TBaseCompanyInfoVo.AREA = strAreaId;
            TBaseCompanyInfoVo.CONTACT_NAME = strContactName;
            TBaseCompanyInfoVo.PHONE = strTelPhone;
            TBaseCompanyInfoVo.CONTACT_ADDRESS = strAddress;
            TBaseCompanyInfoVo.IS_DEL = "0";
            TBaseCompanyInfoVo.ID = i3.View.PageBase.GetSerialNumber("Company_Id");
            if (new TBaseCompanyInfoLogic().Create(TBaseCompanyInfoVo))
            {
                result = TBaseCompanyInfoVo.ID.ToString();
                strMessage = LogInfo.UserInfo.USER_NAME + "新增企业" + TBaseCompanyInfoVo.ID + "成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddCompanyInfo, "", strMessage);
            }
        }
        else
        {
            result = dt.Rows[0]["ID"].ToString();
        }

        return result;
    }
    /// <summary>
    /// 如果不存在企业基本信息，且用户确定要加入，则加入
    /// </summary>
    /// <returns>返回企业ID</returns>
    private string InsertBaseCompany()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.COMPANY_NAME = strCompanyName;
        TBaseCompanyInfoVo.IS_DEL = "0";
        dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo);

        if (dt.Rows.Count <= 0)
        {
            TBaseCompanyInfoVo.INDUSTRY = strIndustryId;
            TBaseCompanyInfoVo.AREA = strAreaId;
            TBaseCompanyInfoVo.CONTACT_NAME = strContactName;
            TBaseCompanyInfoVo.PHONE = strTelPhone;
            TBaseCompanyInfoVo.CONTACT_ADDRESS = strAddress;
            TBaseCompanyInfoVo.IS_DEL = "0";
            TBaseCompanyInfoVo.ID = i3.View.PageBase.GetSerialNumber("Company_Id");
            if (new TBaseCompanyInfoLogic().Create(TBaseCompanyInfoVo))
            {
                dt = new TBaseCompanyInfoLogic().SelectByTable(TBaseCompanyInfoVo);
                result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
                strMessage = LogInfo.UserInfo.USER_NAME + "新增企业" + TBaseCompanyInfoVo.ID + "成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddCompanyInfo, "", strMessage);
            }
        }
        else
        {
            result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        }

        return result;
    }

    /// <summary>
    /// 获取当前委托书的下的监测类别
    /// </summary>
    /// <param name="strContratId"></param>
    /// <returns></returns>
    private string GetContractMonitorType()
    {
        string result = "";
        DataTable dt = new DataTable();
        DataTable dtMonitor = new DataTable();
        if (!String.IsNullOrEmpty(strContratId))
        {
            TMisContractVo objTm = new TMisContractVo();
            objTm.ID = strContratId;
            dt = new TMisContractLogic().SelectByTable(objTm);

            if (dt.Rows.Count > 0)
            {
                //获取监测类别列表
                TBaseMonitorTypeInfoVo objTb = new TBaseMonitorTypeInfoVo();
                objTb.IS_DEL = "0";
                dtMonitor = new TBaseMonitorTypeInfoLogic().SelectByTable(objTb);

                string strMonitorId = dt.Rows[0]["TEST_TYPES"].ToString().Replace(";", "','").ToString();
                DataRow[] drr = dtMonitor.Select(" ID NOT IN ('" + strMonitorId + "')");

                foreach (DataRow dr in drr)
                {
                    dr.Delete();
                    dtMonitor.AcceptChanges();
                }

                result = i3.View.PageBase.LigerGridDataToJson(dtMonitor, dtMonitor.Rows.Count);
            }
        }
        return result;
    }

    /// <summary>
    /// 获取当前委托书的所有监测点位
    /// </summary>
    /// <param name="urlContractId"></param>
    /// <param name="urlMonitorTypeId"></param>
    /// <returns></returns>
    private string GetContractPoint(string urlContractId, string urlMonitorTypeId)
    {
        //string result = "";
        //DataTable dt = new DataTable();
        //TMisContractPointVo objCpoint = new TMisContractPointVo();
        //objCpoint.IS_DEL = "0";
        //objCpoint.CONTRACT_ID = urlContractId;
        //objCpoint.MONITOR_ID = urlMonitorTypeId;

        //dt = new TMisContractPointLogic().SelectByTable(objCpoint, intPageIndex, intPageSize);
        //int CountNum = new TMisContractPointLogic().GetSelectResultCount(objCpoint);
        //result = i3.View.PageBase.LigerGridDataToJson(dt, CountNum);
        //return result;

        DataTable dtDict = PageBase.getDictList("is_Zhengzhou");//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
        string strIsZhengzhou = "";
        if (dtDict.Rows.Count > 0)
        {
            strIsZhengzhou = dtDict.Rows[0]["DICT_CODE"].ToString();
        }

        string result = "";
        DataTable dt = new DataTable();
        TMisContractPointVo objCpoint = new TMisContractPointVo();
        objCpoint.IS_DEL = "0";
        objCpoint.CONTRACT_ID = urlContractId;
        objCpoint.MONITOR_ID = urlMonitorTypeId;

        dt = new TMisContractPointLogic().SelectByTable(objCpoint, intPageIndex, intPageSize);
        int CountNum = new TMisContractPointLogic().GetSelectResultCount(objCpoint);

        if (strIsZhengzhou == "1")//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
        {
            foreach (DataRow dr in dt.Rows)
            {
                string strSAMPLE_DAY = dr["SAMPLE_DAY"].ToString().Trim().TrimStart('0');
                string strSAMPLE_FREQ = dr["SAMPLE_FREQ"].ToString().Trim().TrimStart('0');

                if (strSAMPLE_DAY == "1" && strSAMPLE_FREQ == "1")
                {
                }
                else
                {
                    string strPOINT_NAME = dr["POINT_NAME"].ToString().Trim();
                    dr["POINT_NAME"] = strPOINT_NAME + "(" + strSAMPLE_DAY + "天" + strSAMPLE_FREQ + "次)";
                }
            }
        }
        dt.AcceptChanges();

        result = i3.View.PageBase.LigerGridDataToJson(dt, CountNum);
        return result;
    }

    /// <summary>
    /// 获取选择监测点位下的所有监测项目
    /// </summary>
    /// <param name="urlstrContractPointId"></param>
    /// <returns></returns>
    private string GetContractPointItem()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strContractPointId))
        {
            TMisContractPointitemVo objCpoint = new TMisContractPointitemVo();
            objCpoint.CONTRACT_POINT_ID = strContractPointId;
            dt = new TMisContractPointitemLogic().SelectByTable(objCpoint, intPageIndex, intPageSize);
            int ContNum = new TMisContractPointitemLogic().GetSelectResultCount(objCpoint);
            result = i3.View.PageBase.LigerGridDataToJson(dt, ContNum);
        }
        return result;
    }

    /// <summary>
    /// 获取选择样品下的所有监测项目
    /// </summary>
    /// <param name="urlstrContractPointId"></param>
    /// <returns></returns>
    private string GetContractSampleItem()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strSampleId))
        {
            TMisContractSampleitemVo objCpoint = new TMisContractSampleitemVo();
            objCpoint.CONTRACT_SAMPLE_ID = strSampleId;

            dt = new TMisContractSampleitemLogic().SelectByTable(objCpoint, intPageIndex, intPageSize);
            int ContNum = new TMisContractSampleitemLogic().GetSelectResultCount(objCpoint);
            result = i3.View.PageBase.LigerGridDataToJson(dt, ContNum);
        }
        return result;
    }

    private string GetMonitorItems()
    {
        TBaseMonitorTypeInfoVo objMTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(strMonitroType);

        string result = "";
        DataTable dt = new DataTable();
        TBaseItemInfoVo objCpoint = new TBaseItemInfoVo();
        objCpoint.MONITOR_ID = objMTypeInfoVo.REMARK1 == "" ? strMonitroType : objMTypeInfoVo.REMARK1;
        objCpoint.IS_DEL = "0";

        dt = new TBaseItemInfoLogic().SelectByTable(objCpoint);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        result = result.TrimEnd('}') + ",\"MonitorTypeID\":\"" + objMTypeInfoVo.REMARK1 + "\"}";
        return result;
    }

    /// <summary>
    /// 检查是否存在相关监测点位信息
    /// </summary>
    /// <param name="strContratType">委托类型</param>
    /// <param name="strCompanyIdFrim">受检单位</param>
    /// <param name="strMonitroType">监测类别</param>
    /// <param name="strPointName">点位名称</param>
    /// <returns></returns>
    private string IsExistContractPoint()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.MONITOR_ID = strMonitroType;
        objPoint.COMPANY_ID = strCompanyIdFrim;
        objPoint.POINT_NAME = strPointName;
        objPoint.POINT_TYPE = strContratType;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.IS_DEL = "0";
        dt = new TBaseCompanyPointLogic().SelectByTable(objPoint);
        if (dt.Rows.Count > 0)
        {
            result = "true";
        }
        return result;
    }

    /// <summary>
    /// 保存委托书企业点位信息
    /// </summary>
    /// <returns></returns>
    public string SaveContractPoint()
    {
        string result = ""; bool isNew = false;
        if (String.IsNullOrEmpty(strPointId))
        {
            //如果传入的strPointId为空，则为新增,否则为修改
            isNew = true;
        }

        TMisContractPointVo objPoint = new TMisContractPointVo();

        // 黄进军20141029
        if (strPointName.Contains(","))
        {
            string[] nameItems = strPointName.Split(',');
            string[] idItems = new string[nameItems.Length];
            for (int i = 0; i < nameItems.Length; i++)
            {
                objPoint.ID = i3.View.PageBase.GetSerialNumber("t_mis_contract_pointID");
                objPoint.POINT_ID = SaveCompanyPoint(nameItems[i]);
                objPoint.CONTRACT_ID = strContratId;
                objPoint.POINT_NAME = nameItems[i];
                objPoint.MONITOR_ID = strMonitroType;
                objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
                objPoint.SAMPLE_FREQ = strSampleFreq;
                objPoint.SAMPLE_DAY = strSampleDay;
                DataTable objDtF = getDictList("FreqTask");
                if (objDtF.Rows.Count > 0)
                {
                    if (objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
                    {
                        objPoint.FREQ = FREQ;
                    }
                }
                else
                {
                    objPoint.FREQ = "1";
                }
                objPoint.SAMPLENUM = SAMPLENUM;
                objPoint.CREATE_DATE = strCREATE_DATE;
                objPoint.ADDRESS = strPointAddress;
                objPoint.LONGITUDE = strLONGITUDE;
                objPoint.LATITUDE = strLATITUDE;
                objPoint.NUM = strNUM;
                objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
                objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
                objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;
                objPoint.IS_DEL = "0";
                if (new TMisContractPointLogic().Create(objPoint))
                {
                    result = objPoint.ID.ToString();
                    idItems[i] = objPoint.ID;
                    //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
                }
            }
            string IDs = null;
            foreach (string id in idItems)
            {
                IDs = id + ',';
            }
            strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位" + IDs + "成功";
        }
        else
        {
            if (isNew)
            {
                //如果为新增的
                objPoint.ID = i3.View.PageBase.GetSerialNumber("t_mis_contract_pointID");
                objPoint.POINT_ID = SaveCompanyPoint(strPointName);
            }
            else
            {
                objPoint.ID = strPointId;
            }
            objPoint.CONTRACT_ID = strContratId;
            objPoint.POINT_NAME = strPointName;
            objPoint.MONITOR_ID = strMonitroType;
            objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
            objPoint.SAMPLE_FREQ = strSampleFreq;
            objPoint.SAMPLE_DAY = strSampleDay;

            DataTable objDtF = getDictList("FreqTask");
            if (objDtF.Rows.Count > 0)
            {
                if (objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
                {
                    objPoint.FREQ = FREQ;
                }
            }
            else
            {
                objPoint.FREQ = "1";
            }
            objPoint.SAMPLENUM = SAMPLENUM;
            objPoint.CREATE_DATE = strCREATE_DATE;
            objPoint.ADDRESS = strPointAddress;
            objPoint.LONGITUDE = strLONGITUDE;
            objPoint.LATITUDE = strLATITUDE;
            objPoint.NUM = strNUM;
            objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
            objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
            objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;
            objPoint.IS_DEL = "0";

            if (isNew)
            {
                if (new TMisContractPointLogic().Create(objPoint))
                {
                    result = objPoint.ID.ToString();
                    strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书点位" + objPoint.ID + "成功";
                    //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointInfo, "", strMessage);
                }
            }
            else
            {
                if (new TMisContractPointLogic().Edit(objPoint))
                {
                    result = objPoint.ID.ToString();
                    strMessage = LogInfo.UserInfo.USER_NAME + "编辑委托书点位" + objPoint.ID + "成功";
                    //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractPointInfo, "", strMessage);
                }
            }
        }

        return result;
    }

    //删除监测项目
    private string DelContractPoint()
    {
        string result = "";
        TMisContractPointVo objTcp = new TMisContractPointVo();
        objTcp.ID = strPointId;
        if (new TMisContractPointLogic().Delete(objTcp))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除委托书点位" + objTcp.ID + "成功";
            // WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractPointInfo, "", strMessage);
        }
        return result;
    }
    /// <summary>
    /// 保存新的监测点位信息
    /// </summary>
    /// <returns></returns>
    public string SaveCompanyPoint(string name)
    {
        string result = "";
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointVo();
        objPoint.POINT_NAME = strPointName;
        objPoint.COMPANY_ID = strCompanyIdFrim;
        objPoint.MONITOR_ID = strMonitroType;
        objPoint.POINT_TYPE = strContratType;
        objPoint.DYNAMIC_ATTRIBUTE_ID = strDYNAMIC_ATTRIBUTE_ID;
        objPoint.SAMPLE_FREQ = strSampleFreq;
        objPoint.SAMPLE_DAY = strSampleDay;
        objPoint.FREQ = "1";
        objPoint.CREATE_DATE = strCREATE_DATE;
        objPoint.ADDRESS = strPointAddress;
        objPoint.LONGITUDE = strLONGITUDE;
        objPoint.LATITUDE = strLATITUDE;
        objPoint.NUM = strNUM;
        objPoint.NATIONAL_ST_CONDITION_ID = strNATIONAL_ST_CONDITION_ID;
        objPoint.LOCAL_ST_CONDITION_ID = strLOCAL_ST_CONDITION_ID;
        objPoint.INDUSTRY_ST_CONDITION_ID = strINDUSTRY_ST_CONDITION_ID;

        objPoint.ID = i3.View.PageBase.GetSerialNumber("t_base_company_point_id");
        if (new TBaseCompanyPointLogic().Create(objPoint))
        {
            result = objPoint.ID.ToString();
            strMessage = LogInfo.UserInfo.USER_NAME + "新增企业点位" + objPoint.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.AddCompanyPointInfo, "", strMessage);
        }
        return result;
    }



    /// <summary>
    /// 获取监测点位基本信息 初始化编辑控件
    /// </summary>
    /// <returns></returns>
    private string GetContractPointInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractPointVo objPoint = new TMisContractPointVo();
        objPoint.ID = strPointId;
        objPoint.IS_DEL = "0";
        dt = new TMisContractPointLogic().SelectByTable(objPoint);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 根据条件项目ID 获取标准信息
    /// </summary>
    /// <returns></returns>
    private string GetStanardInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseEvaluationConInfoVo objConItem = new TBaseEvaluationConInfoVo();
        DataTable dtStandart = new DataTable();
        if (!String.IsNullOrEmpty(strStanardItemId))
        {
            objConItem.ID = strStanardItemId;
            objConItem.IS_DEL = "0";
            dt = new TBaseEvaluationConInfoLogic().SelectByTable(objConItem);

            if (dt.Rows.Count > 0)
            {
                TBaseEvaluationInfoVo objEv = new TBaseEvaluationInfoVo();
                objEv.ID = dt.Rows[0]["STANDARD_ID"].ToString();

                dtStandart = new TBaseEvaluationInfoLogic().SelectByTable(objEv);

            }
        }
        result = i3.View.PageBase.LigerGridDataToJson(dtStandart, dtStandart.Rows.Count);
        return result;
    }

    /// <summary>
    /// 获取指定监测类别的监测项目
    /// </summary>
    /// <returns></returns>
    private string GetItemList()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitroType;
        objitem.IS_DEL = "0";
        dt = new TBaseItemInfoLogic().SelectByTable(objitem);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 初始化监测点位监测项目列表ListBox
    /// </summary>
    /// <returns></returns>
    public string GetMonitorSubItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractPointitemVo objtd = new TMisContractPointitemVo();
        objtd.CONTRACT_POINT_ID = strPointId;
        dtSt = new TMisContractPointitemLogic().SelectByTable(objtd);

        //环境质量的监测项目 add by :weilin
        TBaseMonitorTypeInfoVo MonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        MonitorTypeInfoVo = new TBaseMonitorTypeInfoLogic().Details(strMonitroType);
        if (MonitorTypeInfoVo.REMARK1 != null && MonitorTypeInfoVo.REMARK1 != "")
        {
            strMonitroType = MonitorTypeInfoVo.REMARK1;
        }

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitroType;
        objitem.IS_DEL = "0";
        objitem.IS_SUB = "1";
        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();
        if (dtSt.Rows.Count > 0)
        {
            for (int i = 0; i < dtSt.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
                    if (dr != null)
                    {
                        foreach (DataRow Temrow in dr)
                        {
                            Temrow.Delete();
                            dt.AcceptChanges();
                        }
                    }
                }
            }
        }

        dtItem = dt.Copy();
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 初始化已选监测点位监测项目列表
    /// </summary>
    /// <returns></returns>
    private string GetSelectedMonitorItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractPointitemVo objtd = new TMisContractPointitemVo();
        objtd.CONTRACT_POINT_ID = strPointId;

        dtSt = new TMisContractPointitemLogic().SelectByTable(objtd);

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitroType;
        objitem.IS_DEL = "0";
        objitem.IS_SUB = "1";
        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();

        for (int i = 0; i < dtSt.Rows.Count; i++)
        {
            if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
            {
                DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItem.ImportRow(Temrow);
                    }
                }
            }
        }
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 初始化样品监测项目列表ListBox
    /// </summary>
    /// <returns></returns>
    public string GetMonitorSubSampleItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractSampleitemVo objtd = new TMisContractSampleitemVo();
        TMisContractSampleVo objtd2 = new TMisContractSampleVo();
        objtd.CONTRACT_SAMPLE_ID = strSampleId;
        objtd2.MONITOR_ID = strMonitroType;
        if (!String.IsNullOrEmpty(strContratId))
        {
            objtd2.CONTRACT_ID = strContratId;
        }

        if (!String.IsNullOrEmpty(strPlanId))
        {
            objtd2.SAMPLE_PLAN_ID = strPlanId;
        }
        dtSt = new TMisContractSampleitemLogic().SelectMonitorForSample(objtd, objtd2);

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitroType;
        objitem.IS_DEL = "0";
        objitem.IS_SUB = "1";
        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();
        if (dtSt.Rows.Count > 0)
        {
            for (int i = 0; i < dtSt.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
                    if (dr != null)
                    {
                        foreach (DataRow Temrow in dr)
                        {
                            Temrow.Delete();
                            dt.AcceptChanges();
                        }
                    }
                }
            }
        }

        dtItem = dt.Copy();
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 初始化已选样品监测项目列表
    /// </summary>
    /// <returns></returns>
    private string GetSelectedMonitorSampleItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractSampleitemVo objtd = new TMisContractSampleitemVo();
        TMisContractSampleVo objtd2 = new TMisContractSampleVo();
        objtd.CONTRACT_SAMPLE_ID = strSampleId;
        dtSt = new TMisContractSampleitemLogic().SelectMonitorForSample(objtd, objtd2);

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitroType;
        objitem.IS_DEL = "0";
        objitem.IS_SUB = "1";
        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();

        for (int i = 0; i < dtSt.Rows.Count; i++)
        {
            if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
            {
                DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItem.ImportRow(Temrow);
                    }
                }
            }
        }
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 初始化附加项目列表ListBox
    /// </summary>
    /// <returns></returns>
    public string GetSubAttItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractAttfeeVo objtd = new TMisContractAttfeeVo();
        objtd.CONTRACT_ID = strContratId;
        dtSt = new TMisContractAttfeeLogic().SelectByTable(objtd);

        DataTable dt = new DataTable();
        TMisContractAttfeeitemVo objitem = new TMisContractAttfeeitemVo();

        dt = new TMisContractAttfeeitemLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();
        if (dtSt.Rows.Count > 0)
        {
            for (int i = 0; i < dtSt.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtSt.Rows[i]["ATT_FEE_ITEM_ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ATT_FEE_ITEM_ID"].ToString() + "'");
                    if (dr != null)
                    {
                        foreach (DataRow Temrow in dr)
                        {
                            Temrow.Delete();
                            dt.AcceptChanges();
                        }
                    }
                }
            }
        }

        dtItem = dt.Copy();
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 初始化已选附加项目列表
    /// </summary>
    /// <returns></returns>
    private string GetSelectedAttItems()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TMisContractAttfeeVo objtd = new TMisContractAttfeeVo();
        objtd.CONTRACT_ID = strContratId;
        dtSt = new TMisContractAttfeeLogic().SelectByTable(objtd);

        DataTable dt = new DataTable();
        TMisContractAttfeeitemVo objitem = new TMisContractAttfeeitemVo();

        dt = new TMisContractAttfeeitemLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();

        for (int i = 0; i < dtSt.Rows.Count; i++)
        {
            if (!String.IsNullOrEmpty(dtSt.Rows[i]["ATT_FEE_ITEM_ID"].ToString()))
            {
                DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ATT_FEE_ITEM_ID"].ToString() + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItem.ImportRow(Temrow);
                    }
                }
            }
        }
        reslut = i3.View.PageBase.LigerGridDataToJson(dtItem, dtItem.Rows.Count);
        return reslut;
    }
    /// <summary>
    /// 保存委托书监测点位监测项目信息
    /// </summary>
    /// <returns></returns>
    private string SaveDivItemData()
    {
        string result = "";
        string[] strItems = strPointAddItemsId.Split(';');
        if (!String.IsNullOrEmpty(strPointItemsMoveId.ToString()))
        {
            string[] strMove = strPointItemsMoveId.Split(';');
            if (DelMoveItems(strMove))
            {
                if (EditItems(strItems))
                {
                    result = "true";
                }
            }
        }
        else
        {
            if (EditItems(strItems))
            {
                result = "true";
            }
        }

        return result;
    }

    /// <summary>
    /// 保存自送样委托书监测样品监测项目信息
    /// </summary>
    /// <returns></returns>
    private string SaveDivSampleItemData()
    {
        string result = "";
        string[] strItems = strPointAddItemsId.Split(';');
        if (!String.IsNullOrEmpty(strPointItemsMoveId.ToString()))
        {
            string[] strMove = strPointItemsMoveId.Split(';');
            if (DelMoveSampleItems(strMove))
            {
                if (EditSampleItems(strItems))
                {
                    result = "true";
                }
            }
        }
        else
        {
            if (EditSampleItems(strItems))
            {
                result = "true";
            }
        }

        return result;
    }

    /// <summary>
    /// 删除已经移除的样品的监测项目
    /// </summary>
    /// <param name="strMove"></param>
    /// <returns></returns>
    private bool DelMoveSampleItems(string[] strMove)
    {
        bool flag = false;
        TMisContractSampleitemVo objPointItems = new TMisContractSampleitemVo();
        objPointItems.CONTRACT_SAMPLE_ID = strSampleId;
        if (new TMisContractSampleitemLogic().DelMoveSampleItems(objPointItems, strMove))
        {
            flag = true;
            strMessage = LogInfo.UserInfo.USER_NAME + "删除样品" + objPointItems.ID + "监测项目成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractSampleItemsInfo, "", strMessage);
        }
        return flag;
    }

    /// <summary>
    /// 添加新增加的样品监测项目
    /// </summary>
    /// <param name="strItems"></param>
    /// <returns></returns>
    private bool EditSampleItems(string[] strItems)
    {
        bool flag = false;
        string AddItems = "";
        TMisContractSampleitemVo objPointItems = new TMisContractSampleitemVo();
        objPointItems.CONTRACT_SAMPLE_ID = strSampleId;
        DataTable dt = new DataTable();
        dt = new TMisContractSampleitemLogic().SelectByTable(objPointItems);
        if (dt.Rows.Count > 0)
        {
            //判断传入的符合条件的当前委托书的监测项目ID是否存在
            foreach (string strItemsId in strItems)
            {
                DataRow[] dr = dt.Select("CONTRACT_SAMPLE_ID='" + strSampleId + "' AND ITEM_ID='" + strItemsId + "'");
                //如果不存在，则拼凑出需要添加的当前条件下的监测项目ID
                if (dr.Length <= 0)
                {
                    AddItems += strItemsId + ";";
                }
            }
            if (!String.IsNullOrEmpty(AddItems))
            {
                AddItems = AddItems.Substring(0, AddItems.Length - 1);
            }
            string[] ArrAddItems = AddItems.Split(';');
            //新增监测项目到委托书监测项目表
            if (new TMisContractSampleitemLogic().EditSampleItems(objPointItems, ArrAddItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑样品" + objPointItems.ID + "监测项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractSampleItemsInfo, "", strMessage);
            }
        }
        //如果该委托书监测点位的监测项目还未添加，则直接进行添加
        else
        {
            //新增监测项目到委托书监测项目表
            if (new TMisContractSampleitemLogic().EditSampleItems(objPointItems, strItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑样品" + objPointItems.ID + "监测项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractSampleItemsInfo, "", strMessage);
            }
        }
        return flag;
    }

    /// <summary>
    /// 删除已经移除的监测项目
    /// </summary>
    /// <param name="strMove"></param>
    /// <returns></returns>
    private bool DelMoveItems(string[] strMove)
    {
        bool flag = false;
        TMisContractPointitemVo objPointItems = new TMisContractPointitemVo();
        objPointItems.CONTRACT_POINT_ID = strPointId;
        if (new TMisContractPointitemLogic().DelMoveItems(objPointItems, strMove))
        {
            flag = true;
            strMessage = LogInfo.UserInfo.USER_NAME + "删除点位" + objPointItems.CONTRACT_POINT_ID + "监测项目成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractPointItemsInfo, "", strMessage);
        }
        return flag;
    }

    /// <summary>
    /// 添加新增加的监测项目
    /// </summary>
    /// <param name="strItems"></param>
    /// <returns></returns>
    private bool EditItems(string[] strItems)
    {
        bool flag = false;
        string AddItems = "";
        TMisContractPointitemVo objPointItems = new TMisContractPointitemVo();
        objPointItems.CONTRACT_POINT_ID = strPointId;
        DataTable dt = new DataTable();
        dt = new TMisContractPointitemLogic().SelectByTable(objPointItems);
        if (dt.Rows.Count > 0)
        {
            //判断传入的符合条件的当前委托书的监测项目ID是否存在
            foreach (string strItemsId in strItems)
            {
                DataRow[] dr = dt.Select("CONTRACT_POINT_ID='" + strPointId + "' AND ITEM_ID='" + strItemsId + "'");
                //如果不存在，则拼凑出需要添加的当前条件下的监测项目ID
                if (dr.Length <= 0)
                {
                    AddItems += strItemsId + ";";
                }
            }
            if (!String.IsNullOrEmpty(AddItems))
            {
                AddItems = AddItems.Substring(0, AddItems.Length - 1);
            }
            string[] ArrAddItems = AddItems.Split(';');
            //新增监测项目到委托书监测项目表
            if (new TMisContractPointitemLogic().EditItems(objPointItems, ArrAddItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书监测项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointItemsInfo, "", strMessage);
            }
        }
        //如果该委托书监测点位的监测项目还未添加，则直接进行添加
        else
        {
            //新增监测项目到委托书监测项目表
            if (new TMisContractPointitemLogic().EditItems(objPointItems, strItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "新增委托书监测项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractPointItemsInfo, "", strMessage);
            }
        }
        return flag;
    }


    /// <summary>
    /// 保存委托书监测点位附加项目信息
    /// </summary>
    /// <returns></returns>
    private string SaveDivAttItemData()
    {
        string result = "";
        string[] strItems = strAttAddItemsId.Split(';');
        if (!String.IsNullOrEmpty(strAttMoveItemsId.ToString()))
        {
            string[] strMove = strAttMoveItemsId.Split(';');
            if (DelMoveAttItems(strMove))
            {
                if (EditAttItems(strItems))
                {
                    result = "true";
                }
            }
        }
        else
        {
            if (EditAttItems(strItems))
            {
                result = "true";
            }
        }

        return result;
    }

    /// <summary>
    /// 删除已经移除的监测项目
    /// </summary>
    /// <param name="strMove"></param>
    /// <returns></returns>
    private bool DelMoveAttItems(string[] strMove)
    {
        bool flag = false;
        TMisContractAttfeeVo objAttItems = new TMisContractAttfeeVo();
        objAttItems.CONTRACT_ID = strContratId;
        if (new TMisContractAttfeeLogic().DelMoveAttItems(objAttItems, strMove))
        {
            flag = true;
            strMessage = LogInfo.UserInfo.USER_NAME + "删除附加项目成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractAttItemsInfo, "", strMessage);
        }
        return flag;
    }

    /// <summary>
    /// 添加新增加的监测项目
    /// </summary>
    /// <param name="strItems"></param>
    /// <returns></returns>
    private bool EditAttItems(string[] strItems)
    {
        bool flag = false;
        string AddItems = "";
        TMisContractAttfeeVo objAttItems = new TMisContractAttfeeVo();
        objAttItems.CONTRACT_ID = strContratId;
        DataTable dt = new DataTable();
        dt = new TMisContractAttfeeLogic().SelectByTable(objAttItems);
        if (dt.Rows.Count > 0)
        {
            //判断传入的符合条件的当前委托书的监测项目ID是否存在
            foreach (string strItemsId in strItems)
            {
                DataRow[] dr = dt.Select("CONTRACT_ID='" + strContratId + "' AND ATT_FEE_ITEM_ID='" + strItemsId + "'");
                //如果不存在，则拼凑出需要添加的当前条件下的监测项目ID
                if (dr.Length <= 0)
                {
                    AddItems += strItemsId + ";";
                }
            }
            if (!String.IsNullOrEmpty(AddItems))
            {
                AddItems = AddItems.Substring(0, AddItems.Length - 1);
            }
            string[] ArrAddItems = AddItems.Split(';');
            //新增监测项目到委托书监测项目表
            if (new TMisContractAttfeeLogic().EditAttItems(objAttItems, ArrAddItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "新增附加项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractAttItemsInfo, "", strMessage);
            }
        }
        //如果该委托书监测点位的监测项目还未添加，则直接进行添加
        else
        {
            //新增监测项目到委托书监测项目表
            if (new TMisContractAttfeeLogic().EditAttItems(objAttItems, strItems))
            {
                flag = true;
                strMessage = LogInfo.UserInfo.USER_NAME + "新增附加项目成功";
                //WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractAttItemsInfo, "", strMessage);
            }
        }
        return flag;
    }
    ////-------------------

    /// <summary>
    /// 更新委托书核对信息，生成委托书编号
    /// </summary>
    /// <returns></returns>
    private string SaveCheckContractInfor()
    {
        string result = "";
        string strContractCompanyId = "", strTestCompanyId = "";
        //string[] strCodeRule = null;
        string strCodeRule = "";
        TMisContractVo objTmis = new TMisContractVo();

        DataTable dt = new DataTable();
        objTmis.ID = strContratId;
        dt = new TMisContractLogic().SelectByTable(objTmis);

        if (dt.Rows.Count > 0)
        {
            //获取要修改委托书的 委托单位ID 和受检单位ID
            strContractCompanyId = dt.Rows[0]["CLIENT_COMPANY_ID"].ToString();
            strTestCompanyId = dt.Rows[0]["TESTED_COMPANY_ID"].ToString();

            TMisContractCompanyVo objTmc = new TMisContractCompanyVo();

            objTmc.ID = strContractCompanyId;
            objTmc.AREA = strAreaId;
            objTmc.CONTACT_NAME = strContactName;
            objTmc.PHONE = strTelPhone;
            objTmc.CONTACT_ADDRESS = strAddress;

            if (new TMisContractCompanyLogic().Edit(objTmc))
            {
                objTmc.ID = strTestCompanyId;
                objTmc.AREA = strAreaIdFrim;
                objTmc.CONTACT_NAME = strContactNameFrim;
                objTmc.PHONE = strTelPhoneFrim;
                objTmc.CONTACT_ADDRESS = strAddressFrim;

                new TMisContractCompanyLogic().Edit(objTmc);
            }
        }

        objTmis.PROJECT_NAME = strProjectName;
        objTmis.RPT_WAY = strRpt_Way;
        objTmis.ASKING_DATE = strContract_Date.ToString();
        objTmis.TEST_PURPOSE = strMonitor_Purpose;
        objTmis.PROVIDE_DATA = strProData;
        objTmis.OTHER_ASKING = strOtherAsk;
        objTmis.MONITOR_ACCORDING = strAccording;
        objTmis.REMARK2 = strtxtRemarks;
        if (!String.IsNullOrEmpty(strQuck))
        {
            objTmis.ISQUICKLY = strQuck;
        }
        if (!String.IsNullOrEmpty(strAGREE_NONSTANDARD))
        {
            objTmis.AGREE_NONSTANDARD = strAGREE_NONSTANDARD;
        }
        else
        {
            objTmis.AGREE_NONSTANDARD = "0";
        }
        if (!String.IsNullOrEmpty(strAGREE_METHOD))
        {
            objTmis.AGREE_METHOD = strAGREE_METHOD;
        }
        else
        {
            objTmis.AGREE_METHOD = "0";
        }
        if (!String.IsNullOrEmpty(strAGREE_OUTSOURCING))
        {
            objTmis.AGREE_OUTSOURCING = strAGREE_OUTSOURCING;
        }
        else
        {
            objTmis.AGREE_OUTSOURCING = "0";
        }
        if (!String.IsNullOrEmpty(strAGREE_OTHER))
        {
            objTmis.AGREE_OTHER = strAGREE_OTHER;
        }
        else
        {
            objTmis.AGREE_OTHER = "0";
        }
        objTmis.CONTRACT_STATUS = strStatus;
        if (String.IsNullOrEmpty(dt.Rows[0]["CONTRACT_CODE"].ToString()))
        {
            //strCodeRule = new string[4] { strContratYear.ToString(), strCompanyIdFrim.ToString(), strContratType.ToString(), i3.View.PageBase.GetSerialNumber("contract_serialnumber") };
            //objTmis.CONTRACT_CODE = i3.View.PageBase.CreateSerialNumber(strCodeRule);
            //生成委托书单号
            TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            objSerial.SERIAL_TYPE = "1";
            TMisContractVo objvo = new TMisContractVo();
            objvo.CONTRACT_TYPE = dt.Rows[0]["CONTRACT_TYPE"].ToString();
            if (!String.IsNullOrEmpty(strQuck))
            {
                strCodeRule = "G";
            }
            strCodeRule += CreateBaseDefineCode(objSerial, objvo);
            objTmis.CONTRACT_CODE = strCodeRule;
        }
        if (new TMisContractLogic().Edit(objTmis))
        {
            //生成费用明细

            if (String.IsNullOrEmpty(dt.Rows[0]["CONTRACT_CODE"].ToString()))
            {
                result = objTmis.CONTRACT_CODE + "|" + objTmis.ID;
            }
            else
            {
                result = dt.Rows[0]["CONTRACT_CODE"].ToString() + "|" + dt.Rows[0]["ID"].ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// 获取符合条件的委托书列表
    /// </summary>
    /// <returns></returns>
    private string GetContractListData()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        TMisContractCompanyVo objComItems = new TMisContractCompanyVo();

        objItems.CONTRACT_CODE = strContractCode;
        objItems.CONTRACT_TYPE = strContratType;
        objItems.PROJECT_NAME = strProjectName;
        objItems.CONTRACT_YEAR = strContratYear;
        objItems.CLIENT_COMPANY_ID = strCompanyName;
        objItems.TESTED_COMPANY_ID = strCompanyNameFrim;
        objItems.BOOKTYPE = strBookType;
        objItems.CONTRACT_STATUS = "'" + strStatus.Replace(",", "','") + "'";
        objItems.ISQUICKLY = strQuck;
        int CountNum = new TMisContractLogic().GetSelectResultCountForSearchList(objItems);
        dt = new TMisContractLogic().SelectByTableForSearchList(objItems, intPageIndex, intPageSize);

        result = i3.View.PageBase.LigerGridDataToJson(dt, CountNum);

        return result;
    }
    /// <summary>
    /// 获取可用用户列表
    /// </summary>
    /// <returns></returns>
    private string GetUserList()
    {
        string result = "";
        DataTable dt = new DataTable();
        TSysUserVo objitems = new TSysUserVo();
        objitems.IS_DEL = "0";
        dt = new TSysUserLogic().SelectByTable(objitems);
        foreach (DataRow drr in dt.Rows)
        {
            if (drr["ID"].ToString() == "000000001")
            {
                drr.Delete();
            }
        }
        dt.AcceptChanges();
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取流程第一个环节
    /// </summary>
    /// <returns></returns>
    private string GetWFfirstNode()
    {
        string result = "";
        if (!String.IsNullOrEmpty(strWF_ID))
        {
            result = i3.View.PageBaseForWF.GetFirstStepIDFromWFID(strWF_ID);
        }
        return result;

    }
    /// <summary>
    /// 修改状态下 获取委托企业信息
    /// </summary>
    /// <returns></returns>
    private string GetContractCompanyInfor()
    {
        string result = "";
        if (!String.IsNullOrEmpty(strContratId))
        {
            DataTable dt = new DataTable();
            TMisContractCompanyVo objItems = new TMisContractCompanyVo();
            objItems.CONTRACT_ID = strContratId;
            if (!String.IsNullOrEmpty(strCompanyId))
            {
                objItems.ID = strCompanyId;
            }
            if (!String.IsNullOrEmpty(strCompanyIdFrim))
            {
                objItems.ID = strCompanyIdFrim;
            }

            dt = new TMisContractCompanyLogic().SelectByTable(objItems);
            result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 删除委托书信息，同时通过触发器删除其他级联信息
    /// </summary>
    /// <returns></returns>
    private string DeleteContractInfor()
    {
        string result = "";
        TMisContractVo objItems = new TMisContractVo();
        objItems.ID = strContratId;
        if (new TMisContractLogic().Delete(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除委托书" + objItems.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.DelContractInfo, "", strMessage);
        }
        return result;
    }

    /// <summary>
    /// 获取监测费用明细列表
    /// </summary>
    /// <returns></returns>
    private string GetContractConstFeeDetail()
    {
        string result = "";
        DataTable dt = new DataTable();

        TMisContractTestfeeVo objItems = new TMisContractTestfeeVo();
        objItems.CONTRACT_ID = strContratId;
        //int CountNum = new TMisContractTestfeeLogic().GetSelectResultCount(objItems);
        //dt = new TMisContractTestfeeLogic().SelectByTable(objItems, intPageIndex, intPageSize);
        int CountNum = new TMisContractTestfeeLogic().GetContractConstFeeDetailCount(objItems);
        dt = new TMisContractTestfeeLogic().GetContractConstFeeDetail(objItems, intPageIndex, intPageSize);
        result = i3.View.PageBase.LigerGridDataToJson(dt, CountNum);

        return result;
    }

    /// <summary>
    /// 获取附加费用列表
    /// </summary>
    /// <returns></returns>
    private string GetAttItemList()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractAttfeeitemVo objItems = new TMisContractAttfeeitemVo();
        dt = new TMisContractAttfeeitemLogic().SelectByTable(objItems);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 获取指定委托书的附加费用列表
    /// </summary>
    /// <returns></returns>
    private string GetAttFeeDetail()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractAttfeeVo objItems = new TMisContractAttfeeVo();
        objItems.CONTRACT_ID = strContratId;
        int CountNum = new TMisContractAttfeeLogic().GetSelectResultCount(objItems);
        dt = new TMisContractAttfeeLogic().SelectByTable(objItems);
        result = i3.View.PageBase.LigerGridDataToJson(dt, CountNum);
        return result;
    }
    /// <summary>
    /// 获取指定委托书的监测费用总额（监测项目费用、附件项目费用以及实际费用）
    /// </summary>
    /// <returns></returns>
    private string GetConstractFeeCount()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractFeeVo objItems = new TMisContractFeeVo();
        objItems.CONTRACT_ID = strContratId;
        dt = new TMisContractFeeLogic().SelectByTable(objItems);
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 更新当前委托书的附加费用
    /// </summary>
    /// <returns></returns>
    private string UpdateAttFeeInfor()
    {
        string result = "";
        TMisContractAttfeeVo objItems = new TMisContractAttfeeVo();
        objItems.ID = strAttFeeId;
        objItems.ATT_FEE_ITEM_ID = strAtt_item_Id;
        objItems.FEE = strAttFee;
        if (new TMisContractAttfeeLogic().Edit(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "编辑附加项目" + objItems.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractAttItemsInfo, "", strMessage);
        }
        return result;
    }

    /// <summary>
    /// 新增附加项目信息
    /// </summary>
    /// <returns></returns>
    private string SaveDivAttItems()
    {
        string result = "";
        TMisContractAttfeeitemVo objItems = new TMisContractAttfeeitemVo();
        objItems.ID = i3.View.PageBase.GetSerialNumber("t_mis_contract_AttfeeId");
        objItems.ATT_FEE_ITEM = strAttItemName;
        objItems.PRICE = strAttFee;
        objItems.INFO = strAttItemInfor;
        objItems.IS_DEL = "0";
        if (new TMisContractAttfeeitemLogic().Create(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "新增附加项目" + objItems.ID + "成功";
            //WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractAttItemsInfo, "", strMessage);
        }
        return result;
    }
    /// <summary>
    /// 功能描述：创建实际费用（验收监测）
    /// 创建时间：2012-12-19
    /// 创建人：邵世卓
    /// </summary>
    private void SaveContractFee()
    {
        TMisContractFeeVo objContractFee = new TMisContractFeeVo();
        objContractFee.ID = i3.View.PageBase.GetSerialNumber("t_mis_contract_feeId");
        objContractFee.CONTRACT_ID = strContratId;
        objContractFee.IF_PAY = "0";
        objContractFee.INCOME = strContractFee;
        new TMisContractFeeLogic().Create(objContractFee);
    }

    /// <summary>
    /// 删除一条监测点位附加费用信息
    /// </summary>
    /// <returns></returns>
    private string DelAttFeeItems()
    {
        string result = "";
        TMisContractAttfeeVo objItems = new TMisContractAttfeeVo();
        objItems.ID = strAttFeeId;
        if (new TMisContractAttfeeLogic().Delete(objItems))
        {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 用户自定义修改更新统计的费用总计信息
    /// </summary>
    /// <returns></returns>
    private string UpdateConstractFeeCount()
    {
        string result = "";
        DataTable dt = new DataTable();

        TMisContractFeeVo objItems = new TMisContractFeeVo();
        objItems.CONTRACT_ID = strContratId;
        dt = new TMisContractFeeLogic().SelectByTable(objItems);
        if (dt.Rows.Count > 0)
        {
            objItems.ID = dt.Rows[0]["ID"].ToString();
            objItems.TEST_FEE = strFeeTest_FeeSum;
            objItems.ATT_FEE = strFeeAtt_FeeSum;
            objItems.BUDGET = strBudGet;
            objItems.INCOME = strIncome;
            if (new TMisContractFeeLogic().Edit(objItems))
            {
                result = "true";
            }
        }
        else
        {
            objItems.ID = GetSerialNumber("t_mis_contract_feeId");
            objItems.IF_PAY = "0";
            objItems.TEST_FEE = strFeeTest_FeeSum;
            objItems.ATT_FEE = strFeeAtt_FeeSum;
            objItems.BUDGET = strBudGet;
            objItems.INCOME = strIncome;
            if (new TMisContractFeeLogic().Create(objItems))
            {
                result = "true";
            }
        }
        return result;
    }

    /// <summary>
    /// 获取委托书关联的受检企业列表
    /// </summary>
    /// <returns></returns>
    private string GetContractInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractVo objItems = new TMisContractVo();
        if (!String.IsNullOrEmpty(strContratId))
        {
            objItems.ID = strContratId;
        }
        if (!String.IsNullOrEmpty(strCCFLOW_WORKID))
        {
            objItems.CCFLOW_ID1 = strCCFLOW_WORKID;
        }
        if (!String.IsNullOrEmpty(strStatus))
        {
            objItems.CONTRACT_STATUS = strStatus;
        }
        if (!String.IsNullOrEmpty(strMonitroType))
        {
            objItems.TEST_TYPES = strMonitroType;
        }
        if (!String.IsNullOrEmpty(strContractCode))
        {
            objItems.CONTRACT_CODE = strContractCode;
        }
        int CountNum = new TMisContractLogic().SelectDefineTableContractResult(objItems, strCompanyNameFrim, strAreaIdFrim).Rows.Count;

        dt = new TMisContractLogic().SelectDefineTableContract(objItems, strCompanyNameFrim, strAreaIdFrim, intPageIndex, intPageSize);
        DataView dv = dt.DefaultView;
        dv.Sort = "ASKING_DATE DESC";
        DataTable dtCopy = new DataTable();
        dtCopy = dt.Copy();
        dtCopy.Clear();
        dtCopy = dv.ToTable();
        result = PageBase.LigerGridDataToJson(dt, CountNum);

        return result;
    }

    /// <summary>
    /// 获取环境质量监测计划信息
    /// </summary>
    /// <returns></returns>
    private string GetEnvPlanInfo()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractPlanVo objPlan = new TMisContractPlanVo();
        objPlan.CCFLOW_ID1 = strCCFLOW_WORKID;

        dt = new TMisContractPlanLogic().SelectByTable(objPlan);
        string[] vPLAN_TYPE;
        string strPlan_Type_Name = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strPlan_Type_Name = "";
            vPLAN_TYPE = dt.Rows[i]["PLAN_TYPE"].ToString().Split(';');
            for (int j = 0; j < vPLAN_TYPE.Length; j++)
            {
                strPlan_Type_Name += new TBaseMonitorTypeInfoLogic().Details(vPLAN_TYPE[j]).MONITOR_TYPE_NAME + ";";
            }
            dt.Rows[i]["REAMRK1"] = strPlan_Type_Name.TrimEnd(';');
        }

        //result = PageBase.DataTableToJson(dt);
        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 通过workID获取业务数据信息
    /// </summary>
    /// <returns></returns>
    private string GetTaskInfo()
    {
        string result = "";

        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo objTaskVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskVo();
        objTaskVo.CCFLOW_ID1 = strCCFLOW_WORKID;
        DataTable dt = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskLogic().SelectByTable(objTaskVo);

        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 获取委托书列表关联获取委托、受检企业信息 Create By weilin 2014-10-16
    /// </summary>
    /// <returns></returns>
    private string GetAcceptContractInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt = new TMisContractLogic().SelectAcceptContract(strContratId);

        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取委托书自送样样品
    /// </summary>
    /// <returns></returns>
    private string GetContractSample()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractSampleVo objItems = new TMisContractSampleVo();
        if (!String.IsNullOrEmpty(strSampleId))
        {
            objItems.ID = strSampleId;
        }

        objItems.CONTRACT_ID = strContratId;
        objItems.MONITOR_ID = strMonitroType;
        objItems.SAMPLE_PLAN_ID = strPlanId;
        dt = new TMisContractSampleLogic().SelectByTable(objItems, intPageIndex, intPageSize);
        int NUM = new TMisContractSampleLogic().GetSelectResultCount(objItems);
        result = PageBase.LigerGridDataToJson(dt, NUM);
        return result;
    }

    /// <summary>
    /// 插入委托书自送样样品
    /// </summary>
    /// <returns></returns>
    private string SaveContractSample()
    {
        string result = "";
        TMisContractSampleVo objItems = new TMisContractSampleVo();
        objItems.SAMPLE_NAME = strSampleName;
        objItems.SAMPLE_TYPE = strSampleType;
        objItems.SAMPLE_COUNT = strSampleCount;
        objItems.SAMPLE_ACCEPT_DATEORACC = strSampleDateOrAcc;
        objItems.SRC_CODEORNAME = strSrcCodeOrName;
        objItems.SAMPLE_STATUS = strSampleStatus;

        objItems.MONITOR_ID = strMonitroType;
        objItems.CONTRACT_ID = strContratId;
        objItems.SAMPLE_PLAN_ID = strPlanId;

        objItems.REMARK1 = strtxtRemarks;
        if (!String.IsNullOrEmpty(strSampleId))
        {
            objItems.ID = strSampleId;
            if (new TMisContractSampleLogic().Edit(objItems))
            {
                result = "true";
            }
        }
        else
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_sampleId");

            if (new TMisContractSampleLogic().Create(objItems))
            {
                result = "true";
            }
        }
        return result;
    }
    /// <summary>
    /// 插入委托书自送样样品(企业信息设置的测点和项目) Create By weilin 2014-8-7
    /// </summary>
    /// <returns></returns>
    private string SaveSamplePoint()
    {
        string result = "";
        DataTable dt = new DataTable();
        string strItemIds = "";
        string[] strCompanyPointIDs = strPointId.Split(';');
        for (int i = 0; i < strCompanyPointIDs.Length; i++)
        {
            TBaseCompanyPointVo CompanyPointVo = new TBaseCompanyPointLogic().Details(strCompanyPointIDs[i].ToString());
            strItemIds = "";
            TBaseCompanyPointItemVo CompanyPointItemVo = new TBaseCompanyPointItemVo();
            CompanyPointItemVo.POINT_ID = strCompanyPointIDs[i].ToString();
            dt = new TBaseCompanyPointItemLogic().SelectByTable(CompanyPointItemVo);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                strItemIds += dt.Rows[j]["ITEM_ID"].ToString() + ";";
            }
            string[] strItems = strItemIds.TrimEnd(';').Split(';');

            TMisContractSampleVo objItems = new TMisContractSampleVo();
            objItems.SAMPLE_NAME = CompanyPointVo.POINT_NAME;
            objItems.SAMPLE_TYPE = strMonitorName;
            objItems.SAMPLE_COUNT = "1";
            objItems.REMARK1 = strCompanyPointIDs[i].ToString();

            objItems.MONITOR_ID = strMonitorID;
            objItems.SAMPLE_PLAN_ID = strPlanId;

            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_sampleId");
            if (new TMisContractSampleLogic().Create(objItems))
            {
                TMisContractSampleitemVo objPointItems = new TMisContractSampleitemVo();
                objPointItems.CONTRACT_SAMPLE_ID = objItems.ID;
                //新增监测项目到委托书监测项目表
                if (new TMisContractSampleitemLogic().EditSampleItems(objPointItems, strItems))
                {
                    result = "true";
                    strMessage = LogInfo.UserInfo.USER_NAME + "编辑样品" + objPointItems.ID + "监测项目成功";
                }
            }


        }

        return result;
    }

    /// <summary>
    /// 删除委托书样品
    /// </summary>
    /// <returns></returns>
    private string DelContractSample()
    {
        string result = "";
        TMisContractSampleVo objItems = new TMisContractSampleVo();
        objItems.ID = strSampleId;
        if (new TMisContractSampleLogic().Delete(strSampleId))
        {
            result = "true";
        }

        return result;
    }

    /// <summary>
    /// 获取指定监测类别下的所有点位
    /// </summary>
    /// <returns></returns>
    private string GetPointInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strMonitroType))
        {
            TBaseCompanyPointVo objItems = new TBaseCompanyPointVo();
            objItems.MONITOR_ID = strMonitroType;
            objItems.IS_DEL = "0";
            dt = new TBaseCompanyPointLogic().SelectByTable(objItems);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 获取指定监测点位下的所有监测项目
    /// </summary>
    /// <returns></returns>
    private string GetItemInfor()
    {
        string result = "";
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strPointId))
        {
            TBaseCompanyPointItemVo objItems = new TBaseCompanyPointItemVo();
            objItems.POINT_ID = strPointId;
            dt = new TBaseCompanyPointItemLogic().SelectItemsForPoint(objItems);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 获取指定行业类别的监测项目 胡方扬 2013-03-14
    /// </summary>
    /// <returns></returns>
    public string GetIndurstyAllItems()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objItem = new TBaseIndustryInfoVo();
        objItem.IS_DEL = "0";
        objItem.ID = strIndustryId;

        dt = new TBaseIndustryInfoLogic().SelectByObjectForIndustry(objItem, strMonitroType, 0, 0);
        result = LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 根据点位，行业类别，确定企业已选的监测项目 胡方扬 2013-03-14
    /// </summary>
    /// <returns></returns>
    public string GetIndurstySelectedItems()
    {
        string result = "";
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objItem = new TBaseIndustryInfoVo();
        objItem.IS_DEL = "0";
        objItem.ID = strIndustryId;

        dt = new TBaseIndustryInfoLogic().SelectByObjectForFinishedIndustry(objItem, strMonitroType, strPointId, 0, 0);
        result = LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 非环境质量类通用插入点位相关信息 Create By 胡方扬 2013-06-06
    /// </summary>
    /// <returns></returns>
    public bool InsertPointsInfor(string strBaseCompanyId, string strTaskId, string strReqContractType, string strReqPlanId, string Company_Names, ref string New_ID)
    {
        bool flag = false;
        TBaseCompanyPointVo objCompany = new TBaseCompanyPointVo();

        objCompany.COMPANY_ID = strBaseCompanyId;
        objCompany.POINT_TYPE = strReqContractType;
        objCompany.IS_DEL = "0";
        DataTable objTable = new TBaseCompanyPointLogic().SelectByTable(objCompany);

        if (new TMisContractPointLogic().InsertPointsInfor(objTable, strTaskId, strReqPlanId, Company_Names, strReqContractType, ref New_ID))
        {
            flag = true;
        }

        return flag;
    }
    /// <summary>
    /// 保存无委托书临时监测监测计划 
    /// 创建时间：2013-06-06
    /// 创建人：胡方扬
    /// 修改时间：
    /// 修改人：
    /// 修改内容：
    /// </summary>
    public string SavePlanInforForUnContract()
    {
        string result = "";
        string New_ID = "";
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
        objItems.PLAN_NUM = "1";
        if (new TMisContractPlanLogic().Create(objItems))
        {
            /*插入无委托书监测点位*/
            string strTaskId = null;
            if (!String.IsNullOrEmpty(strContratId))
            {
                strTaskId = strContratId;
            }
            if (InsertPointsInfor(strCompanyIdFrim, strTaskId, strContratType, objItems.ID, Company_Names, ref New_ID))
            {
                result = objItems.ID + "," + New_ID;
            }
        }
        return result;
    }
    /// <summary>
    /// 获取Url传参
    /// </summary>
    /// <param name="context"></param>
    private void GetRequestParme(HttpContext context)
    {
        //排序信息
        if (!String.IsNullOrEmpty(context.Request.Params["sortname"]))
        {
            strSortname = context.Request["sortname"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["sortorder"]))
        {
            strSortorder = context.Request.Params["sortorder"].Trim();
        }
        //当前页面
        if (!String.IsNullOrEmpty(context.Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(context.Request.Params["page"].Trim());
        }
        //每页记录数
        if (!String.IsNullOrEmpty(context.Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(context.Request.Params["pagesize"].Trim());
        }
        //方法
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["type"]))
        {
            strType = context.Request.Params["type"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strContratId"]))
        {
            strContratId = context.Request.Params["strContratId"].Trim();
        }
        //委托单位信息
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyId"]))
        {
            strCompanyId = context.Request.Params["strCompanyId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyName"]))
        {
            strCompanyName = context.Request.Params["strCompanyName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strIndustryId"]))
        {
            strIndustryId = context.Request.Params["strIndustryId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAreaId"]))
        {
            strAreaId = context.Request.Params["strAreaId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContactName"]))
        {
            strContactName = context.Request.Params["strContactName"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strTelPhone"]))
        {
            strTelPhone = context.Request.Params["strTelPhone"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAddress"]))
        {
            strAddress = context.Request.Params["strAddress"].Trim();
        }
        //受检单位信息
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyIdFrim"]))
        {
            strCompanyIdFrim = context.Request.Params["strCompanyIdFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyNameFrim"]))
        {
            strCompanyNameFrim = context.Request.Params["strCompanyNameFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strIndustryIdFrim"]))
        {
            strIndustryIdFrim = context.Request.Params["strIndustryIdFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAreaIdFrim"]))
        {
            strAreaIdFrim = context.Request.Params["strAreaIdFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContactNameFrim"]))
        {
            strContactNameFrim = context.Request.Params["strContactNameFrim"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strTelPhoneFrim"]))
        {
            strTelPhoneFrim = context.Request.Params["strTelPhoneFrim"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strAddressFrim"]))
        {
            strAddressFrim = context.Request.Params["strAddressFrim"].Trim();
        }
        //委托类型信息
        if (!String.IsNullOrEmpty(context.Request.Params["strContratType"]))
        {
            strContratType = context.Request.Params["strContratType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBookType"]))
        {
            strBookType = context.Request.Params["strBookType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContratYear"]))
        {
            strContratYear = context.Request.Params["strContratYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitroType"]))
        {
            strMonitroType = context.Request.Params["strMonitroType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractPointId"]))
        {
            strContractPointId = context.Request.Params["strContractPointId"].Trim();
        }
        //其他要求说明信息
        if (!String.IsNullOrEmpty(context.Request.Params["strProData"]))
        {
            strProData = context.Request.Params["strProData"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAccording"]))
        {
            strAccording = context.Request.Params["strAccording"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strOtherAsk"]))
        {
            strOtherAsk = context.Request.Params["strOtherAsk"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strtxtRemarks"]))
        {
            strtxtRemarks = context.Request.Params["strtxtRemarks"].Trim();
        }
        //监测点位信息
        if (!String.IsNullOrEmpty(context.Request.Params["strPointId"]))
        {
            strPointId = context.Request.Params["strPointId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointName"]))
        {
            strPointName = context.Request.Params["strPointName"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strDYNAMIC_ATTRIBUTE_ID"]))
        {
            strDYNAMIC_ATTRIBUTE_ID = context.Request.Params["strDYNAMIC_ATTRIBUTE_ID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleFreq"]))
        {
            strSampleFreq = context.Request.Params["strSampleFreq"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["FREQ"]))
        {
            FREQ = context.Request.Params["FREQ"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["SAMPLENUM"]))
        {
            SAMPLENUM = context.Request.Params["SAMPLENUM"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleDay"]))
        {
            strSampleDay = context.Request.Params["strSampleDay"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCREATE_DATE"]))
        {
            strCREATE_DATE = context.Request.Params["strCREATE_DATE"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointAddress"]))
        {
            strPointAddress = context.Request.Params["strPointAddress"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strLONGITUDE"]))
        {
            strLONGITUDE = context.Request.Params["strLONGITUDE"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strLATITUDE"]))
        {
            strLATITUDE = context.Request.Params["strLATITUDE"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strNUM"]))
        {
            strNUM = context.Request.Params["strNUM"].Trim();
        }
        //标准信息
        if (!String.IsNullOrEmpty(context.Request.Params["strNATIONAL_ST_CONDITION_ID"]))
        {
            strNATIONAL_ST_CONDITION_ID = context.Request.Params["strNATIONAL_ST_CONDITION_ID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strLOCAL_ST_CONDITION_ID"]))
        {
            strLOCAL_ST_CONDITION_ID = context.Request.Params["strLOCAL_ST_CONDITION_ID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strINDUSTRY_ST_CONDITION_ID"]))
        {
            strINDUSTRY_ST_CONDITION_ID = context.Request.Params["strINDUSTRY_ST_CONDITION_ID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strStanardItemId"]))
        {
            strStanardItemId = context.Request.Params["strStanardItemId"].Trim();
        }

        //监测项目信息
        if (!String.IsNullOrEmpty(context.Request.Params["strPointItemId"]))
        {
            strPointItemId = context.Request.Params["strPointItemId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointAddItemsId"]))
        {
            strPointAddItemsId = context.Request.Params["strPointAddItemsId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointItemsMoveId"]))
        {
            strPointItemsMoveId = context.Request.Params["strPointItemsMoveId"].Trim();
        }

        //核对委托书信息
        if (!String.IsNullOrEmpty(context.Request.Params["strStatus"]))
        {
            strStatus = context.Request.Params["strStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strProjectName"]))
        {
            strProjectName = context.Request.Params["strProjectName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContract_Date"]))
        {
            strContract_Date = context.Request.Params["strContract_Date"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strRpt_Way"]))
        {
            strRpt_Way = context.Request.Params["strRpt_Way"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleSource"]))
        {
            strSampleSource = context.Request.Params["strSampleSource"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitor_Purpose"]))
        {
            strMonitor_Purpose = context.Request.Params["strMonitor_Purpose"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAGREE_OUTSOURCING"]))
        {
            strAGREE_OUTSOURCING = context.Request.Params["strAGREE_OUTSOURCING"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAGREE_METHOD"]))
        {
            strAGREE_METHOD = context.Request.Params["strAGREE_METHOD"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAGREE_NONSTANDARD"]))
        {
            strAGREE_NONSTANDARD = context.Request.Params["strAGREE_NONSTANDARD"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAGREE_OTHER"]))
        {
            strAGREE_OTHER = context.Request.Params["strAGREE_OTHER"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractCode"]))
        {
            strContractCode = context.Request.Params["strContractCode"].Trim();
        }
        //附加项目ID
        if (!String.IsNullOrEmpty(context.Request.Params["strAtt_item_Id"]))
        {
            strAtt_item_Id = context.Request.Params["strAtt_item_Id"].Trim();
        }
        //附加项目费用ID 
        if (!String.IsNullOrEmpty(context.Request.Params["strAttFeeId"]))
        {
            strAttFeeId = context.Request.Params["strAttFeeId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttFee"]))
        {
            strAttFee = context.Request.Params["strAttFee"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttAddItemsId"]))
        {
            strAttAddItemsId = context.Request.Params["strAttAddItemsId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttMoveItemsId"]))
        {
            strAttMoveItemsId = context.Request.Params["strAttMoveItemsId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttItemName"]))
        {
            strAttItemName = context.Request.Params["strAttItemName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttItemInfor"]))
        {
            strAttItemInfor = context.Request.Params["strAttItemInfor"].Trim();
        }
        //委托书费用信息
        if (!String.IsNullOrEmpty(context.Request.Params["strFeeId"]))
        {
            strFeeId = context.Request.Params["strFeeId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTestNum"]))
        {
            strTestNum = context.Request.Params["strTestNum"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFeeTest_Fee"]))
        {
            strFeeTest_FeeSum = context.Request.Params["strFeeTest_Fee"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFeeAtt_FeeSum"]))
        {
            strFeeAtt_FeeSum = context.Request.Params["strFeeAtt_FeeSum"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTestPointNum"]))
        {
            strTestPointNum = context.Request.Params["strTestPointNum"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBudGet"]))
        {
            strBudGet = context.Request.Params["strBudGet"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strIncome"]))
        {
            strIncome = context.Request.Params["strIncome"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strContractFee"]))
        {
            strContractFee = context.Request.Params["strContractFee"].Trim();
        }
        //自送样需要信息
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleAccept"]))
        {
            strSampleAccept = context.Request.Params["strSampleAccept"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleMan"]))
        {
            strSampleMan = context.Request.Params["strSampleMan"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSrcCodeOrName"]))
        {
            strSrcCodeOrName = context.Request.Params["strSrcCodeOrName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleStatus"]))
        {
            strSampleStatus = context.Request.Params["strSampleStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleDateOrAcc"]))
        {
            strSampleDateOrAcc = context.Request.Params["strSampleDateOrAcc"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleName"]))
        {
            strSampleName = context.Request.Params["strSampleName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleType"]))
        {
            strSampleType = context.Request.Params["strSampleType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleCount"]))
        {
            strSampleCount = context.Request.Params["strSampleCount"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSampleId"]))
        {
            strSampleId = context.Request.Params["strSampleId"].Trim();
        }
        ///流程ID
        if (!String.IsNullOrEmpty(context.Request.Params["strWF_ID"]))
        {
            strWF_ID = context.Request.Params["strWF_ID"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strQuck"]))
        {
            strQuck = context.Request.Params["strQuck"].Trim();
        }
        //获取指定web.config的值
        if (!String.IsNullOrEmpty(context.Request.Params["strKey"]))
        {
            strKey = context.Request.Params["strKey"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPlanId"]))
        {
            strPlanId = context.Request.Params["strPlanId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFlag"]))
        {
            strFlag = context.Request.Params["strFlag"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strWorkTask_id"]))
        {
            strWorkTask_id = context.Request.Params["strWorkTask_id"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["Company_Names"]))
        {
            Company_Names = context.Request.Params["Company_Names"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTestAnsyFee"]))
        {
            strTestAnsyFee = context.Request.Params["strTestAnsyFee"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitorID"]))
        {
            strMonitorID = context.Request.Params["strMonitorID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMonitorName"]))
        {
            strMonitorName = context.Request.Params["strMonitorName"].Trim();
        }
        //验收委托
        if (!string.IsNullOrEmpty(context.Request.Params["strRadioInfo"]))
        {
            strRadioInfo = context.Request.Params["strRadioInfo"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strPFYQ"]))
        {
            strPFYQ = context.Request.Params["strPFYQ"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSZCL"]))
        {
            strSZCL = context.Request.Params["strSZCL"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBL"]))
        {
            strBL = context.Request.Params["strBL"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCCFLOW_WORKID"]))
        {
            strCCFLOW_WORKID = context.Request.Params["strCCFLOW_WORKID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["filenameA"]))
        {
            filenameA = context.Request.Params["filenameA"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strContratID"]))
        {
            strContratID = context.Request.Params["strContratID"].Trim();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["strTask_code"]))
        {
            strTask_code = context.Request.Params["strTask_code"].Trim();
        }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}