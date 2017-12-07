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
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Sys.Duty;
using System.Text;
using WebApplication;
using i3.BusinessLogic.Channels.Mis.ProcessMgm;
using i3.BusinessLogic.Channels.Base.Item;

/// <summary>
/// 功能描述：采样任务
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_MisII_sampling_Sampling : PageBase
{
    public static string strId = "";
    public string strSubTaskID = "", strMonitorID = "", strSourceID = "";

    public string ccflowWorkId = "";
    public string ccflowFid = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["type"] == "check")
            {
                var UserNo = Request.QueryString["UserNo"];//有两个UserNo???等解决
                UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;
                UserNo = UserNo.Trim(',');

                var workID = Convert.ToInt32(Request.QueryString["OID"]);//OID为流程ID

                var strSubTaskID = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split('|')[0];

                var sampleIdList = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (sampleIdList.Count > 0)
                {
                    sampleIdList.RemoveAt(0);
                }

                //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
                TMisMonitorTaskVo tm = new TMisMonitorTaskVo();
                TMisMonitorSubtaskVo tmsub = new TMisMonitorSubtaskVo();
                tmsub = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
                tm.ID = tmsub.TASK_ID;
                DataTable dt = new TMisMonitorTaskLogic().SelectByTable(tm);

                if (dt.Rows[0]["REMARK3"].ToString() == "true")
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
                }
                else
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
                }



                //获取现场项目
                DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID, sampleIdList: sampleIdList);

                if (dtSampleItem.Rows.Count > 0)
                {
                    TMisMonitorSubtaskAppVo objSubtaskAppVo = new TMisMonitorSubtaskAppVo();
                    objSubtaskAppVo.SUBTASK_ID = strSubTaskID;
                    objSubtaskAppVo = new TMisMonitorSubtaskAppLogic().Details(objSubtaskAppVo);

                    //objSubtaskAppVo.SAMPLING_CHECK = "administrator";//临时测试 

                    if (!string.IsNullOrEmpty(objSubtaskAppVo.SAMPLING_CHECK))
                    {
                        //TSysUserVo objUserVo = new TSysUserLogic().Details(objSubtaskAppVo.SAMPLING_CHECK);

                        //var childFlowID = System.Configuration.ConfigurationManager.AppSettings["XCSSH"].ToString().Trim(',');
                        //var childNodeID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["XCSSHJNode"]);

                        //CCFlowFacade.Node_CreateBlankWork(UserNo, childFlowID, UserNo, null, workID, fid, flowId, nodeId, UserNo, childNodeID, objUserVo.USER_NAME.Trim(','), "@GroupMark=" + strSubTaskID);

                    }
                    else
                    {
                        Response.Write("false没有指定现场项目复核人，不能发送");
                        Response.ContentType = "text/plain";
                        Response.ContentEncoding = Encoding.UTF8;
                        Response.End();
                    }

                    //yinchengyi 2015-4-24 打开页面时 记录ccflow流程ID信息到业务系统数据库
                    //TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskVo();
                    //objSubtaskVo.ID = strSubTaskID;
                    //objSubtaskVo.CCFLOW_ID1 = workID.ToString();
                    //objSubtaskVo.CCFLOW_ID2 = fid.ToString();
                    //if (!new TMisMonitorSubtaskLogic().Edit(objSubtaskVo))
                    //{
                    //    Response.Write("false流程ID更新失败，不能发送");
                    //    Response.ContentType = "text/plain";
                    //    Response.ContentEncoding = Encoding.UTF8;
                    //    Response.End();
                    //}

                }

                //分析类现场项目判断
                var strTaskID = "";
                var FID = Convert.ToInt64(Request.QueryString["FID"]);

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

                DataTable dtInfo = new TMisMonitorResultLogic().getItemInfoBySubTaskID_MAS(strTaskID, strSubTaskID, true);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {

                    if (dtInfo.Rows[i]["IS_ANYSCENE_ITEM"].ToString() == "1")
                    {
                        if (dtInfo.Rows[i]["USER_NAME"].ToString() == "")
                        {
                            Response.Write("false分析负责人信息没有选择完整，请检查");

                            Response.ContentType = "text/plain";
                            Response.End();
                        }
                    }
                }

                Response.Write("true");
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "text/plain";
                Response.End();
            }

            if (Request.QueryString["type"] == "AfterSuccessSend")
            {
                Response.ContentEncoding = Encoding.GetEncoding("gb2312");

                var UserNo = Request.QueryString["UserNo"];//有两个UserNo???等解决
                UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;


                var workID = Convert.ToInt32(Request.QueryString["OID"]);//OID为流程ID

                var flowId = Request.QueryString["FK_Flow"];
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);

                var strSubTaskID = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split('|')[0];

                var sampleIdList = CCFlowFacade.GetFlowIdentification(UserNo, workID).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (sampleIdList.Count > 0)
                {
                    sampleIdList.RemoveAt(0);
                }

                //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
                TMisMonitorTaskVo tm = new TMisMonitorTaskVo();
                TMisMonitorSubtaskVo tmsub = new TMisMonitorSubtaskVo();
                tmsub = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
                tm.ID = tmsub.TASK_ID;
                DataTable dt =  new TMisMonitorTaskLogic().SelectByTable(tm);
                
                if (dt.Rows[0]["REMARK3"].ToString()== "true")
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
                }
                else
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
                }

                //获取现场项目
                DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID, sampleIdList: sampleIdList);

                if (dtSampleItem.Rows.Count > 0)
                {
                    TMisMonitorSubtaskAppVo objSubtaskAppVo = new TMisMonitorSubtaskAppVo();
                    objSubtaskAppVo.SUBTASK_ID = strSubTaskID;
                    objSubtaskAppVo = new TMisMonitorSubtaskAppLogic().Details(objSubtaskAppVo);



                    if (!string.IsNullOrEmpty(objSubtaskAppVo.SAMPLING_CHECK))
                    {
                        TSysUserVo objUserVo = new TSysUserLogic().Details(objSubtaskAppVo.SAMPLING_CHECK);

                        var childFlowID = System.Configuration.ConfigurationManager.AppSettings["XCSSH"].ToString();
                        var childNodeID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["XCSSHJNode"]);

                        var subTaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);

                        var taskVo = new TMisMonitorTaskLogic().Details(subTaskVo.TASK_ID);

                        var tempID = CCFlowFacade.Node_CreateBlankWork(UserNo, childFlowID, UserNo, null, workID, fid, flowId, nodeId, UserNo, childNodeID, objUserVo.USER_NAME, "@GroupMark=" + strSubTaskID);
                        CCFlowFacade.SetFlowTitle(UserNo, childFlowID, tempID, taskVo.PROJECT_NAME);
                    }

                }

                //创建分析类现场项目
                var strTaskID = "";
                var FID = Convert.ToInt64(Request.QueryString["FID"]);

                UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;
                UserNo = UserNo.Trim(',');

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

                var childFlowID3 = System.Configuration.ConfigurationManager.AppSettings["FXLXCXM"].ToString().Trim(',');
                var childNodeID3 = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FXLXCXMJNode"]);//配置为跳转到分析室审核流程的第三个节点by lhm

                DataTable dtInfo = new TMisMonitorResultLogic().getItemInfoBySubTaskID_MAS(strTaskID, strSubTaskID, true);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
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
                        workid = CCFlowFacade.Node_CreateBlankWork(UserNo, childFlowID3, UserNo, null, workID, fid, flowId, nodeId, UserNo, childNodeID3, dtInfo.Rows[i]["USER_NAME"].ToString().Trim(','), "@GroupMark=" + dtInfo.Rows[i]["RESULTID"].ToString().Trim(','));

                        CCFlowFacade.SetFlowTitle(Request["UserNo"].ToString(), childFlowID3, workid, title);
                    }


                }

                Response.Write("发送成功");
                Response.ContentType = "text/plain";
                Response.End();
            }





            if (!string.IsNullOrEmpty(Request.QueryString["DirectionType"]))
            {
                var type = Request.QueryString["DirectionType"];
                var direction = Request.QueryString["Direction"];
                var UserNo = Request.QueryString["UserNo"];
                var workID = Request.QueryString["WorkId"];

                var strSubTaskID = CCFlowFacade.GetFlowIdentification(UserNo, Convert.ToInt64(workID)).Split('|')[0];

                //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
                TMisMonitorTaskVo tm = new TMisMonitorTaskVo();
                TMisMonitorSubtaskVo tmsub = new TMisMonitorSubtaskVo();
                tmsub = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
                tm.ID = tmsub.TASK_ID;
                DataTable dt = new TMisMonitorTaskLogic().SelectByTable(tm);

                if (dt.Rows[0]["REMARK3"].ToString() == "true")
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
                }
                else
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
                }

                var sampleIdList = CCFlowFacade.GetFlowIdentification(UserNo, Convert.ToInt64(workID)).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (sampleIdList.Count > 0)
                {
                    sampleIdList.RemoveAt(0);
                }

                switch (type)
                {
                    case "type1":

                        //分析类现场项目在该环节创建时，该逻辑需要更改为是否存在分析类项目 by lhm
                        //DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID(strSubTaskID);//存在分析类项目和分析类现场项目
                        DataTable dtSampleDept = new TMisMonitorResultLogic().SelectSampleDeptWithSubtaskID2(strSubTaskID,sampleIdList);//存在分析类项目

                        if (direction == "d1")
                        {
                            if (dtSampleDept.Rows.Count == 0)
                            {
                                Response.Write("1");
                            }
                            else
                            {
                                Response.Write("0");
                            }
                        }
                        else if (direction == "d2")
                        {
                            if (dtSampleDept.Rows.Count > 0)
                            {
                                Response.Write("1");
                            }
                            else
                            {
                                Response.Write("0");
                            }
                        }
                        Response.End();
                        break;
                    default:
                        Response.Write("0");
                        Response.End();
                        break;

                }
            }

            //数据加载
            if (Request.QueryString["WorkID"] != null)
            {
                var workID = Convert.ToInt64(Request.QueryString["WorkID"]);

                ccflowWorkId = workID.ToString();
                ccflowFid = Request.QueryString["FID"];

                var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID).Split('|')[0];

                this.SUBTASK_ID.Value = identification;
                strSubTaskID = identification;
                strSourceID = identification;

                TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
                strMonitorID = objSubtaskVo.MONITOR_ID;
                this.MONITOR_ID.Value = strMonitorID;

                TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtaskVo.TASK_ID);

                //huangjinjun add 2016.1.26 如果REMARK3等于true，将ph值、电导率、溶解氧设为分析项目
                if (objTask.REMARK3 == "true")
                {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeFX();
                }
                else {
                    bool bl = new TBaseItemInfoLogic().EditItemTypeXC();
                }

                this.PLAN_ID.Value = objTask.PLAN_ID;

                //yinchengyi 2015-4-24 打开页面时 记录ccflow流程ID信息到业务系统数据库
                //TMisMonitorSubtaskVo objSubtaskVo = new TMisMonitorSubtaskVo();
                //objSubtaskVo.ID = strSubTaskID;
                objSubtaskVo.CCFLOW_ID1 = workID.ToString();
                objSubtaskVo.CCFLOW_ID2 = ccflowFid.ToString();
                if (!new TMisMonitorSubtaskLogic().Edit(objSubtaskVo))
                {
                    //todo_yinchengyi:
                }

            }

            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
            }

            //点位动态属性信息拷贝
            AttributeValueCopy();
            //样品编号
            //SetSampleCode();

            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
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
            if (Request["type"] != null && Request["type"].ToString() == "isSendToCheck2")
            {
                strResult = isSendToCheck2();
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
        DataTable strSampleCheckSender = GetDefaultOrFirstDutyUser("duty_other_sample", strMonitorID);
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
    /// 样品编号设置
    /// </summary>
    /// <returns></returns>
    protected void SetSampleCode()
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.SUBTASK_ID = this.SUBTASK_ID.Value;
        objSample.QC_TYPE = "0";
        objSample.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            objSample.QC_TYPE = "";
            objSample.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSample.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                dtSample.ImportRow(dr);
            }
        }

        for (int j = 0; j < dtSample.Rows.Count; j++)
        {
            if (dtSample.Rows[j]["SAMPLE_CODE"].ToString().Length > 0)
                continue;
            string[] strSampleCode = new string[2] { "S" + DateTime.Now.Year + DateTime.Now.Month, i3.View.PageBase.GetSerialNumber("monitor_samplecode") };
            objSample = new TMisMonitorSampleInfoVo();
            objSample.ID = dtSample.Rows[j]["ID"].ToString();
            objSample.SAMPLE_CODE = i3.View.PageBase.CreateSerialNumber(strSampleCode);

            new TMisMonitorSampleInfoLogic().Edit(objSample);
        }

    }

    /// <summary>
    /// 点位描述信息拷贝
    /// </summary>
    /// <returns></returns>
    protected void AttributeValueCopy()
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
        objTaskPoint.SUBTASK_ID = this.SUBTASK_ID.Value;
        DataTable dt = new TMisMonitorTaskPointLogic().SelectByTable(objTaskPoint);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseAttrbuteValueVo objAttrbuteValue = new TBaseAttrbuteValueVo();
            objAttrbuteValue.OBJECT_ID = dt.Rows[i]["POINT_ID"].ToString();

            TBaseAttrbuteValue3Vo objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
            objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();

            DataTable dtAtt3 = new TBaseAttrbuteValue3Logic().SelectByTable(objAttrbuteValue3);
            if (dtAtt3.Rows.Count > 0)
                return;

            DataTable dtAtt = new TBaseAttrbuteValueLogic().SelectByTable(objAttrbuteValue);
            for (int j = 0; j < dtAtt.Rows.Count; j++)
            {
                objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
                objAttrbuteValue3.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrbuteValue3.OBJECT_TYPE = dtAtt.Rows[j]["OBJECT_TYPE"].ToString();
                objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();
                objAttrbuteValue3.ATTRBUTE_CODE = dtAtt.Rows[j]["ATTRBUTE_CODE"].ToString();
                objAttrbuteValue3.ATTRBUTE_VALUE = dtAtt.Rows[j]["ATTRBUTE_VALUE"].ToString();
                objAttrbuteValue3.IS_DEL = dtAtt.Rows[j]["IS_DEL"].ToString();

                new TBaseAttrbuteValue3Logic().Create(objAttrbuteValue3);
            }
        }
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
        objSubtask.SAMPLING_MANAGER_ID = base.LogInfo.UserInfo.REAL_NAME;// new TSysUserLogic().Details(objSubtask.SAMPLING_MANAGER_ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.TICKET_NUM;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.PHONE;


        TMisMonitorSampleInfoVo sampleVo = new TMisMonitorSampleInfoVo { SUBTASK_ID = objSubtask.ID };
        sampleVo = new TMisMonitorSampleInfoLogic().Details(sampleVo);

        IList<string> userList = new List<string>();

        var userIdList = sampleVo.REMARK4.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var userId in userIdList)
        {
            userList.Add( new TSysUserLogic().Details(userId).REAL_NAME);
        }

        objSubtask.SAMPLING_MAN = string.Join(",",userList);


        //if (String.IsNullOrEmpty(objSubtask.SAMPLE_ASK_DATE))
        //{
        //    objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        //}
        //if (String.IsNullOrEmpty(objSubtask.SAMPLE_FINISH_DATE))
        //{
        //    objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");
        //}
        //获取退回意见信息
        TMisReturnInfoVo ReturnInfoVo = new TMisReturnInfoVo();
        ReturnInfoVo.TASK_ID = objSubtask.TASK_ID;
        if (this.SUBTASK_ID.Value == Request.QueryString["strSourceID"].ToString())
        {
            ReturnInfoVo.SUBTASK_ID = this.SUBTASK_ID.Value;
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_005;
        }
        else
        {
            ReturnInfoVo.SUBTASK_ID = Request.QueryString["strSourceID"].ToString();
            ReturnInfoVo.CURRENT_STATUS = SerialType.Monitor_003;
        }
        ReturnInfoVo.BACKTO_STATUS = SerialType.Monitor_002;
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
    /// <summary>
    /// 保存现状信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SaveLocaleInfo(string strSubtaskID, string strAttribute)
    {
        bool isSuccess = true;
        string strMsg = "";
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

        return isSuccess == true ? "[{result:'1',msg:'" + strMsg + "'}]" : "[{result:'0',msg:'" + strMsg + "'}]";
    }
    /// <summary>
    /// 判断任务中是否存在现场项目
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string isSendToCheck(string strSubTaskID, string strCCFlowID = "", string strUserNo = "")
    {
        var sampleIdList = CCFlowFacade.GetFlowIdentification(strUserNo, Convert.ToInt64(strCCFlowID)).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {
            sampleIdList.RemoveAt(0);
        }

        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID, sampleIdList: sampleIdList);

        return dtSampleItem.Rows.Count > 0 ? "1" : "0";
    }

    private  string isSendToCheck2()
    {
        string strSubTaskID = Request.QueryString["strSubTaskID"];
        string strCCFlowID = Request.QueryString["strCCflowWorkId"];

        var sampleIdList = CCFlowFacade.GetFlowIdentification(base.LogInfo.UserInfo.USER_NAME, Convert.ToInt64(strCCFlowID)).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (sampleIdList.Count > 0)
        {
            sampleIdList.RemoveAt(0);
        }

        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID, sampleIdList: sampleIdList);

        return dtSampleItem.Rows.Count > 0 ? "1" : "0";
    }

    [WebMethod]
    public static string UpdateSampleCheck(string strSubTaskID, string strCheckUser)
    {
        DataTable dtSampleItem = new TMisMonitorResultLogic().SelectSampleItemWithSubtaskID(strSubTaskID);
        TMisMonitorSubtaskAppVo objWhere = new TMisMonitorSubtaskAppVo();
        objWhere.SUBTASK_ID = strSubTaskID;
        TMisMonitorSubtaskAppVo objSet = new TMisMonitorSubtaskAppVo();
        objSet.SAMPLING_CHECK = strCheckUser;

        bool bResult = new TMisMonitorSubtaskAppLogic().Edit(objSet, objWhere);

        return bResult ? "1" : "0";
    }

}