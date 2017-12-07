<%@ WebHandler Language="C#" Class="ProgrammingHandler" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Web.SessionState;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
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
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

public class ProgrammingHandler : PageBase, IHttpHandler, IRequiresSessionState
 {
    //系统参数变量
    private string strAction = "", result = "", strMessage = "";
    private DataTable dt = new DataTable();
    //业务关联变量
    private string wf_inst_task_id = "", wf_inst_id="", service_key_name="",task_id = "";
    //委托企业ID，受检企业ID
    private string strCompanyId = "", strCompanyIdFrim = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "", strisFrim = "";
    private string strFileType = "";
    private string strLeader = "", strPointId = "", strQCStep = "";
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID))
        {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
        GetRequestParme(context);
        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                case "GetFlowTaskInfo":
                    context.Response.Write(GetFlowTaskInfo());
                    context.Response.End();
                    break;
                case "GetTaskInfor":
                    context.Response.Write(GetTaskInfor());
                    context.Response.End();
                    break;
                case "GetCompanyInfor":
                    context.Response.Write(GetCompanyInfor());
                    context.Response.End();
                    break;
                case "GetFile":
                    context.Response.Write(GetFile());
                    context.Response.End();
                    break;
                case "EditContractCompanyInfo":
                    context.Response.Write(EditContractCompanyInfo());
                    context.Response.End();
                    break;
                case "UpdateContractLeader":
                    context.Response.Write(UpdateContractLeader());
                    context.Response.End();
                    break;
                case "GetContractPlan":
                    context.Response.Write(GetContractPlan());
                    context.Response.End();
                    break;
                case "GetPointInfors":
                    context.Response.Write(GetPointInfors());
                    context.Response.End();
                    break;
                default:
                    break;
            } 
        }
    }
    /// <summary>
    /// 根据业务ID 获取委托书信息
    /// </summary>
    /// <returns></returns>
    private string GetTaskInfor()
    {
        result = "";
        if (!String.IsNullOrEmpty(task_id))
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = task_id;
            dt = new TMisContractLogic().SelectByTable(objItems);
            if (dt != null)
            {
                result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count); 
            }
        }
        return result;
    }
    /// <summary>
    /// 根据任务ID，获取任务信息（含业务ID，业务流程）
    /// </summary>
    /// <returns></returns>
    private string GetFlowTaskInfo()
    {
        result = "";
        dt = new DataTable();
        TWfInstTaskServiceVo objItems = new TWfInstTaskServiceVo();
        objItems.WF_INST_TASK_ID = wf_inst_task_id;
        objItems.WF_INST_ID = wf_inst_id;
        objItems.SERVICE_KEY_NAME = service_key_name;
        dt = new TWfInstTaskServiceLogic().SelectByTable(objItems);
        if (dt != null)
        {
            result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count); 
        }

        return result;
    }

    //获取委托书企业信息
    private string GetCompanyInfor()
    {
        result = "";
        dt = new DataTable();
        TMisContractCompanyVo objItems = new TMisContractCompanyVo();
        objItems.CONTRACT_ID = task_id;
        if (!String.IsNullOrEmpty(strCompanyId))
        {
            objItems.ID = strCompanyId;
        }
        dt = new TMisContractCompanyLogic().SelectByTable(objItems);

        result = PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    //验证是否存在文件
    private string GetFile()
    {
        result = "";
        dt = new DataTable();
        TOaAttVo objItems = new TOaAttVo();
        objItems.BUSINESS_ID = task_id;
        objItems.BUSINESS_TYPE = strFileType;

        dt = new TOaAttLogic().SelectByTable(objItems);

        if (dt.Rows.Count > 0)
        {
            result = "true"; 
        }
        return result;
    }

    /// <summary>
    /// 修改委托书企业信息
    /// </summary>
    private string EditContractCompanyInfo( )
    {
        string result = "";

        DataTable dt = new DataTable();


        TMisContractCompanyVo objTmc = new TMisContractCompanyVo();

        objTmc.ID = strCompanyId;
        objTmc.CONTRACT_ID = task_id;
        objTmc.INDUSTRY = strIndustryId;
        objTmc.AREA = strAreaId;
        objTmc.CONTACT_NAME = strContactName;
        objTmc.PHONE = strTelPhone;
        if (strisFrim == "false")
        {
            objTmc.CONTACT_ADDRESS = strAddress;
        }
        else
        {
            objTmc.MONITOR_ADDRESS = strAddress;
        }
        objTmc.IS_DEL = "0";
        if (new TMisContractCompanyLogic().Edit(objTmc))
        {
            result = "true";
            strMessage=LogInfo.UserInfo.USER_NAME+"编辑委托书企业"+objTmc.ID+"成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddContractCompanyInfo,"",strMessage);
        }

        return result;
    }

    /// <summary>
    /// 更新项目负责人信息
    /// </summary>
    /// <returns></returns>
    private string UpdateContractLeader()
    {
        result = "";
        if (!String.IsNullOrEmpty(task_id))
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = task_id;
            objItems.PROJECT_ID = LogInfo.UserInfo.ID;
            objItems.QC_STEP = strQCStep;
            if (new TMisContractLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "更新委托书项目负责人信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditContractInfo, "", strMessage);
            } 
        }

        return result;
    }

    /// <summary>
    /// 获取监测计划信息
    /// </summary>
    /// <returns></returns>
    private string GetContractPlan()
    {
        string result = "";
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        
        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.CONTRACT_ID = task_id;

            int CountNum = new TMisContractPlanLogic().SelectByTable(objItems).Rows.Count;
            dt = new TMisContractPlanLogic().SelectByTable(objItems, intPageIndex, intPageSize);
            if (dt != null)
            {
                dt.Columns.Add(new DataColumn("PLAN_DATE", typeof(string)));
                foreach (DataRow drr in dt.Rows)
                {
                    drr["PLAN_DATE"] =drr["PLAN_YEAR"].ToString()+ '-' + drr["PLAN_MONTH"] .ToString()+ '-' + drr["PLAN_DAY"].ToString();
                    dt.AcceptChanges();
                } 
            }
            result = PageBase.LigerGridDataToJson(dt, CountNum);
        }
        return result;
    }

    /// <summary>
    /// 获取排口信息
    /// </summary>
    /// <returns></returns>
    private string GetPointInfors()
    {
        result = "";
        dt = new DataTable();
        TMisContractPointVo objItems = new TMisContractPointVo();
        if (!String.IsNullOrEmpty(task_id))
        {
            objItems.CONTRACT_ID = task_id;
            dt = new TMisContractPointLogic().SelectByTable(objItems);

            result = PageBase.LigerGridDataToJson(dt,dt.Rows.Count);
        }

        return result;
    }
    /// <summary>
    /// 设置Request Url参数
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
        if (!String.IsNullOrEmpty(context.Request.Params["action"].Trim()))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["wf_inst_id"]))
        {
            wf_inst_id = context.Request.Params["wf_inst_id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["wf_inst_task_id"]))
        {
            wf_inst_task_id = context.Request.Params["wf_inst_task_id"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["service_key_name"]))
        {
            service_key_name = context.Request.Params["service_key_name"].Trim();
        }
        //业务ID
        if (!String.IsNullOrEmpty(context.Request.Params["task_id"]))
        {
            task_id = context.Request.Params["task_id"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyId"]))
        {
            strCompanyId = context.Request.Params["strCompanyId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCompanyIdFrim"]))
        {
            strCompanyIdFrim = context.Request.Params["strCompanyIdFrim"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFileType"]))
        {
            strFileType = context.Request.Params["strFileType"].Trim();
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

        if (!String.IsNullOrEmpty(context.Request.Params["strisFrim"]))
        {
            strisFrim = context.Request.Params["strisFrim"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strLeader"]))
        {
            strLeader = context.Request.Params["strLeader"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strQCStep"]))
        {
            strQCStep = context.Request.Params["strQCStep"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPointId"]))
        {
            strPointId = context.Request.Params["strPointId"].Trim();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}