using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;


using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using System.Text;
using WebApplication;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Base.Item;
/// <summary>
/// 功能描述：分析任务分配
/// 创建日期：2015-01-20
/// 创建人  ：魏林
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisTaskAllocation : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["type"] == "check")
            {
                Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                //Response.Write("false分析负责人信息没有选择完整，请检查");
                ////Response.ContentEncoding =  Encoding.UTF8  ;
                //Response.ContentEncoding = Encoding.GetEncoding("GB2312");

                //Response.ContentType = "text/plain";
                //Response.End();


                var strTaskID = "";
                var strSubTaskID = "";
                var FID = Convert.ToInt64(Request.QueryString["FID"]);

                var UserNo = Request.QueryString["UserNo"];
                UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;

                var workID = Convert.ToInt32(Request.QueryString["OID"]);//OID为流程ID

                TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                objPlanVo.CCFLOW_ID1 = FID.ToString();
                objPlanVo = new TMisContractPlanLogic().Details(objPlanVo);
                if (objPlanVo.ID.Length > 0 && objPlanVo.REAMRK1 == "1")
                {
                    //当前流程属于送样的
                    strTaskID = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    strSubTaskID = "";
                }
                else
                {
                    strTaskID = "";
                    strSubTaskID = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split('|')[0];
                }

                var flowId = Request.QueryString["FK_Flow"];
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);

                var identification = CCFlowFacade.GetFlowIdentification(UserNo, Convert.ToInt64(workID));

                var sampleIdList = identification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (sampleIdList.Count > 0)
                {
                    sampleIdList.RemoveAt(0);
                }

                DataTable dtInfo = new TMisMonitorResultLogic().getItemInfoBySubTaskID_MAS(strTaskID, strSubTaskID, true);

                var newDT = new DataTable();

                foreach (DataColumn column in dtInfo.Columns)
                {
                    newDT.Columns.Add(column.ColumnName);
                }

                foreach (DataRow row in dtInfo.Rows)
                {
                    if (sampleIdList.Count == 0)
                    {
                        newDT.Rows.Add(row.ItemArray);
                    }
                    else if (sampleIdList.Contains(row["SampleID"].ToString()))
                    {
                        newDT.Rows.Add(row.ItemArray);
                    }                   
                }

                for (int i = 0; i < newDT.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["USER_NAME"].ToString() == "")
                    {
                        Response.Write("false分析负责人信息没有选择完整，请检查");

                        Response.ContentType = "text/plain";
                        Response.End();
                    }
                }

                Response.Write("true");

                Response.ContentType = "text/plain";
                Response.End();
            }

            if (Request.QueryString["type"] == "checkAfter")
            {
                Response.ContentEncoding = Encoding.GetEncoding("gb2312");


                var strTaskID = "";
                var strSubTaskID = "";
                var FID = Convert.ToInt64(Request.QueryString["FID"]);

                var UserNo = Request.QueryString["UserNo"];
                UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;
                UserNo = UserNo.Trim(',');

                var workID = Convert.ToInt32(Request.QueryString["OID"]);//OID为流程ID

                TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                objPlanVo.CCFLOW_ID1 = FID.ToString();
                objPlanVo = new TMisContractPlanLogic().Details(objPlanVo);
                if (objPlanVo.ID.Length > 0 && objPlanVo.REAMRK1 == "1")
                {
                    //当前流程属于送样的
                    strTaskID = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    strSubTaskID = "";
                }
                else
                {
                    strTaskID = "";
                    strSubTaskID = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split('|')[0];
                }

                var flowId = Request.QueryString["FK_Flow"].Trim(',');
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);

                var childFlowID = System.Configuration.ConfigurationManager.AppSettings["FXSSH"].ToString().Trim(',');
                var childNodeID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FXSSHJNode"]);

                var childFlowID2 = System.Configuration.ConfigurationManager.AppSettings["FXLXCXM"].ToString().Trim(',');
                var childNodeID2 = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FXLXCXMJNode"]);

                var identification = CCFlowFacade.GetFlowIdentification(UserNo, Convert.ToInt64(workID));

                var sampleIdList = identification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (sampleIdList.Count > 0)
                {
                    sampleIdList.RemoveAt(0);
                }

                DataTable dtInfo = new TMisMonitorResultLogic().getItemInfoBySubTaskID_MAS(strTaskID, strSubTaskID, true);

                var newDT = new DataTable();

                foreach (DataColumn column in dtInfo.Columns)
                {
                    newDT.Columns.Add(column.ColumnName);
                }

                foreach (DataRow row in dtInfo.Rows)
                {
                    if (sampleIdList.Count == 0)
                    {
                        newDT.Rows.Add(row.ItemArray);
                    }
                    else if (sampleIdList.Contains(row["SampleID"].ToString()))
                    {
                        newDT.Rows.Add(row.ItemArray);
                    }
                }


                for (int i = 0; i < newDT.Rows.Count; i++)
                {
                    if (dtInfo.Rows[i]["USER_NAME"].ToString() == "")
                    {
                        Response.Write("false分析负责人信息没有选择完整，请检查");

                        Response.ContentType = "text/plain";
                        Response.End();
                    }

                    var itemVo = new TBaseItemInfoLogic().Details(dtInfo.Rows[i]["ITEM_ID"].ToString());
                    var sampleVo = new TMisMonitorSampleInfoLogic().Details(dtInfo.Rows[i]["SAMPLE_ID"].ToString());
                    var subtaskVO = new TMisMonitorSubtaskLogic().Details(sampleVo.SUBTASK_ID);
                    var taskVo = new TMisMonitorTaskLogic().Details(subtaskVO.TASK_ID);

                    long workid = 0;
                    var title = string.Format("{0} {1}", taskVo.PROJECT_NAME, itemVo.ITEM_NAME);

                    if (dtInfo.Rows[i]["IS_ANYSCENE_ITEM"].ToString() == "1")  //分析类现场项目 
                    {

                        //在采样实施环节创建
                        //workid = CCFlowFacade.Node_CreateBlankWork(UserNo, childFlowID2, UserNo, null, workID, fid, flowId, nodeId, UserNo, childNodeID2, dtInfo.Rows[i]["USER_NAME"].ToString().Trim(','), "@GroupMark=" + dtInfo.Rows[i]["RESULTID"].ToString().Trim(','));

                        //CCFlowFacade.SetFlowTitle(Request["UserNo"].ToString(), childFlowID2, workid, title);
                    }
                    else
                    {
                        workid = CCFlowFacade.Node_CreateBlankWork(UserNo, childFlowID, UserNo, null, workID, fid, flowId, nodeId, UserNo, childNodeID, dtInfo.Rows[i]["USER_NAME"].ToString().Trim(','), "@GroupMark=" + dtInfo.Rows[i]["RESULTID"].ToString().Trim(','));
                        CCFlowFacade.SetFlowTitle(Request["UserNo"].ToString(), childFlowID, workid, title);
                    }

                }


                Response.Write("发送成功");

                Response.ContentType = "text/plain";
                Response.End();
            }


            //定义结果
            string strResult = "";
            if (Request.QueryString["WorkID"] != null)
            {
                var FID = Convert.ToInt64(Request.QueryString["FID"]);

                TMisContractPlanVo objPlanVo = new TMisContractPlanVo();
                objPlanVo.CCFLOW_ID1 = FID.ToString();
                objPlanVo = new TMisContractPlanLogic().Details(objPlanVo);

                this.PLAN_ID.Value = objPlanVo.ID;

                if (objPlanVo.ID.Length > 0 && objPlanVo.REAMRK1 == "1")
                {
                    //当前流程属于送样的
                    this.TASK_ID.Value = new TMisMonitorTaskLogic().Details(new TMisMonitorTaskVo { PLAN_ID = objPlanVo.ID }).ID;
                    this.SUBTASK_ID.Value = "";

                }
                else
                {
                    //当前流程属于采样的
                    var workID = Convert.ToInt64(Request.QueryString["WorkID"]);

                    var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID).Split('|')[0];

                    this.SUBTASK_ID.Value = identification;
                    this.TASK_ID.Value = "";
                }


            }
            //样品号信息
            if (Request["type"] != null && Request["type"].ToString() == "getSampleGridInfo")
            {
                strResult = getSampleGridInfo(Request["strTaskID"].ToString(), Request["strSubTaskID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getItemGridInfo")
            {
                strResult = getItemGridInfo(Request["SampleID"].ToString());
                Response.Write(strResult);
                Response.End();
            }

        }
    }

    /// <summary>
    /// 获取样品信息
    /// </summary>
    /// <returns></returns>
    private string getSampleGridInfo(string strTaskID, string strSubTaskID)
    {
        //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
        TMisMonitorTaskVo tm = new TMisMonitorTaskVo();
        TMisMonitorSubtaskVo tmsub = new TMisMonitorSubtaskVo();
        tmsub = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        tm.ID = tmsub.TASK_ID;
        DataTable dtRemark = new TMisMonitorTaskLogic().SelectByTable(tm);
        if (dtRemark.Rows[0]["REMARK3"].ToString() == "true")
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
        }
        else
        {
            bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
        }

        var strCCflowWorkId = Request.QueryString["strCCflowWorkId"];
        var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, Convert.ToInt64(strCCflowWorkId));

        var sampleIdList = identification.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {
            sampleIdList.RemoveAt(0);
        }

        if (sampleIdList.Count == 0)
        {
            DataTable dt = new TMisMonitorResultLogic().getSampleCodeInAlloction_MAS(strTaskID, strSubTaskID);
            string strJson = CreateToJson(dt, 0);
            return strJson;
        }
        else
        {
            DataTable dt = new TMisMonitorResultLogic().getSampleCodeInAlloction_MAS(strTaskID, strSubTaskID);

            var newDT = new DataTable();

            foreach (DataColumn column in dt.Columns)
            {
                newDT.Columns.Add(column.ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!sampleIdList.Contains(dt.Rows[i]["ID"].ToString()))
                {

                    continue;
                }

                newDT.Rows.Add(dt.Rows[i].ItemArray);

            }

            string strJson = CreateToJson(newDT, 0);
            return strJson;
        }
    }
    /// <summary>
    /// 获取监测项目信息
    /// </summary>
    /// <returns></returns>
    private string getItemGridInfo(string strSampleId)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TMisMonitorResultLogic().getItemInfoInAlloction_MAS(strSampleId, intPageIndex, intPageSize, "1,2");

        int intTotalCount = new TMisMonitorResultLogic().getItemInfoCountInAlloction_MAS(strSampleId, "1,2");
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    /// <summary>
    /// 获取企业名称信息
    /// </summary>
    /// <param name="strTaskId">任务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getCompanyName(string strTaskId, string strCompanyId)
    {
        if (strCompanyId == "") return "";
        i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
        TMisMonitorTaskCompanyVo.ID = strCompanyId;
        string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
        return strCompanyName;
    }
    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }
    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <param name="strMonitorTypeId">用户Id</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    /// <summary>
    /// 获取获取分析负责人信息
    /// </summary>
    /// <param name="strResultId">结果ＩＤ</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strResultId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(string));
        dt.Columns.Add("UserName", typeof(string));

        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return DataTableToJson(dt);

        string strUserId = objTable.Rows[0]["HEAD_USERID"].ToString();
        if (strUserId == "") return DataTableToJson(dt);

        //将获取用户ID信息转换成用户名称进行返回
        string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
        DataRow row = dt.NewRow();
        row["UserId"] = strUserId;
        row["UserName"] = strUserName;
        dt.Rows.Add(row);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取获取分析协人信息
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExName(string strResultId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(string));
        dt.Columns.Add("UserName", typeof(string));

        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return DataTableToJson(dt);

        string strUserExIds = objTable.Rows[0]["ASSISTANT_USERID"].ToString();
        if (strUserExIds == "") return DataTableToJson(dt);

        List<string> list = strUserExIds.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        DataRow row = dt.NewRow();
        row["UserId"] = strUserExIds;
        row["UserName"] = strSumUserExName;
        dt.Rows.Add(row);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取分析完成时间
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAskingDate(string strResultId)
    {
        DataTable objTable = new TMisMonitorResultLogic().getItemExInfo(strResultId);
        if (objTable.Rows.Count == 0) return "";
        string strAskingDate = objTable.Rows[0]["ASKING_DATE"].ToString();
        if (strAskingDate != "")
            strAskingDate = DateTime.Parse(strAskingDate).ToString("yyyy-MM-dd");
        return strAskingDate;
    }

    /// <summary>
    /// 根据默认负责人获取已经分配的项目信息
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedDefaultItemName(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItem(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumItemName = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
            spit = ",";
        }
        return strSumItemName;
    }
    /// <summary>
    /// 根据默认协同人获取已经分配的项目信息
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedDefaultItemNameEx(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItemEx(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumItemName = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
            spit = ",";
        }
        return strSumItemName;
    }

    /// <summary>
    /// 根据默认协同人获取已经分配的样品号
    /// </summary>
    /// <param name="strTaskId">总任务ID</param>
    /// <param name="strDefaultUser">默认负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getAssignedSampleCode(string strTaskId, string strDefaultUser)
    {
        DataTable objTable = new TMisMonitorResultLogic().getAssignedSampleCode(strTaskId, strDefaultUser);
        if (objTable.Rows.Count == 0) return "";
        string strSumSampleCode = "";
        string spit = "";
        foreach (DataRow row in objTable.Rows)
        {
            strSumSampleCode = strSumSampleCode + spit + row["SAMPLE_CODE"].ToString();
            spit = ",";
        }
        return strSumSampleCode;
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
}