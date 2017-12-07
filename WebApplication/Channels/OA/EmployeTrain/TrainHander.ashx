<%@ WebHandler Language="C#" Class="TrainHander" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Web.SessionState;
using System.Configuration;
using i3.View;
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Channels.OA.TRAIN;
using i3.BusinessLogic.Channels.OA.TRAIN;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public class TrainHander : PageBase, IHttpHandler, IRequiresSessionState
{
    //系统用户信息
    public string strUserID = "";
    public string strMessage = "";
    public string strAction = "", strType = "", result = "";//执行方法，字典类别,返回值
    public DataTable dt = new DataTable();
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    public string strtaskID = "", strTrainBt = "", strTrainType = "", strTrainTo = "", strTrainInfor = "", strTrainTarger = "", strTrainDate = "", strDept = "", strExamType = "", strPlanYear = "", strDarptId = "", strDarptDate = "", strAppId = "", strAppDate = "", strAppInfor = "", strAppResult = "", strFlowStatus = "", strTypes = "", strTrainResult = "", strTchAppId = "", strTchApp = "", strTchAppDate = "", strTrainCompany = "", strFileId = "", strFileType = "";
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
                //获取考核列表
                case "GetTrainViewList":
                    context.Response.Write(GetTrainViewList());
                    context.Response.End();
                    break;
                 //获取指定ID的详细信息
                case "GetTrainInfor":
                    context.Response.Write(GetTrainInfor());
                    context.Response.End();
                    break;
                 //保存数据
                case "SaveTrainDate":
                    context.Response.Write(SaveTrainDate());
                    context.Response.End();
                    break;
                 //数据删除
                case "DeleteTrainInfor":
                    context.Response.Write(DeleteTrainInfor());
                    context.Response.End();
                    break;
                //附件信息生成
                case "CreateTrainFile":
                    context.Response.Write(CreateTrainFile());
                    context.Response.End();
                    break;
                //附件信息删除
                case "DelTrainFile":
                    context.Response.Write(DelTrainFile());
                    context.Response.End();
                    break;
                //获取员工有效性评价附件列表
                case "GetTrainFile":
                    context.Response.Write(GetTrainFile());
                    context.Response.End();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 获取培训计划列表
    /// </summary>
    /// <returns></returns>
    private string GetTrainViewList() {
        result="";
        dt=new DataTable();
        TOaTrainPlanVo objItems = new TOaTrainPlanVo();
        objItems.FLOW_STATUS = strFlowStatus;
        objItems.TYPES=strTypes;
        dt = new TOaTrainPlanLogic().SelectByTable(objItems, intPageIndex, intPageSize);
        int CountNum = new TOaTrainPlanLogic().GetSelectResultCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }


    /// <summary>
    /// 获取指定培训ID的详细信息
    /// </summary>
    /// <returns></returns>
    private string GetTrainInfor()
    {
        result = "";
        dt = new DataTable();
        TOaTrainPlanVo objItems = new TOaTrainPlanVo();
        objItems.ID = strtaskID;
        dt = new TOaTrainPlanLogic().SelectByTable(objItems);

        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 保存员工培训计划信息
    /// </summary>
    /// <returns></returns>
    private string SaveTrainDate() {
        result = "";
        TOaTrainPlanVo objItems = new TOaTrainPlanVo();
        objItems.TRAIN_BT = strTrainBt;
        objItems.TYPES = strTypes;
        objItems.TRAIN_TYPE = strTrainType;
        objItems.TRAIN_DATE = strTrainDate;
        objItems.TRAIN_INFO = strTrainInfor;
        objItems.TRAIN_TARGET = strTrainTarger;
        objItems.TRAIN_TO = strTrainTo;
        objItems.EXAMINE_METHOD = strExamType;
        objItems.DEPT_ID = strDept;
        objItems.PLAN_YEAR = strPlanYear;
        objItems.TRAIN_COMPANY = strTrainCompany;
        objItems.TRAIN_RESULT = strTrainResult;
        objItems.TECH_APP_ID = strTchApp;
        objItems.TECH_APP = strTchApp;
        objItems.TECH_APP_DATE = strTchAppDate;
        
        if (String.IsNullOrEmpty(strtaskID))
        {
            objItems.ID = GetSerialNumber("t_oa_trainPlanID");
            objItems.APP_FLOW = "流程未启动";
            if (new TOaTrainPlanLogic().Create(objItems))
            {
                result = objItems.ID;

                strMessage = LogInfo.UserInfo.USER_NAME + "新增员工培训计划信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddTrainPlan, "", strMessage);
            }
        }
        else {
            objItems.ID = strtaskID;
            if (new TOaTrainPlanLogic().Edit(objItems)) {

                result = objItems.ID;
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑员工培训计划信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditTrainPlan, "", strMessage);
            }
        }

        return result;
    }
    /// <summary>
    /// 删除培训计划
    /// </summary>
    /// <returns></returns>
    private string DeleteTrainInfor() {
        result = "";
        if (!String.IsNullOrEmpty(strtaskID)) {
            TOaTrainPlanVo objitems = new TOaTrainPlanVo();
            objitems.ID = strtaskID;
            if (new TOaTrainPlanLogic().Delete(objitems)) {

                result = "true";
            }
        }
        return result;
    }

    /// <summary>
    /// 获取员工有效性评价附件列表信息
    /// </summary>
    /// <returns></returns>
    public string GetTrainFile() {
        result = "";
        dt = new DataTable();
        TOaTrainFileVo objItems = new TOaTrainFileVo();
        objItems.TRAIN_PLAN_ID = strtaskID;
        objItems.REMARK4 = strFileType;
        dt = new TOaTrainFileLogic().TrainFileByTableList(objItems, intPageIndex, intPageSize);
        int CountNum = new TOaTrainFileLogic().SelectTrainFileByTableCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);

        return result;
    }
    
    /// <summary>
    /// 新增员工培训计划附件信息
    /// </summary>
    /// <returns></returns>
    public string CreateTrainFile() {
        result = "";
        TOaTrainFileVo objItems = new TOaTrainFileVo();
        objItems.TRAIN_PLAN_ID = strtaskID;
        objItems.ID = GetSerialNumber("t_oa_TrainFileID");
        if (new TOaTrainFileLogic().Create(objItems))
        {
            result = objItems.ID;
            strMessage = LogInfo.UserInfo.USER_NAME + "新增员工培训计划附件信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddTrainPlanFile, "", strMessage);
        }
        return result;
    }
   /// <summary>
    /// 删除指定培训计划的文件附件信息
   /// </summary>
   /// <returns></returns>
    public string DelTrainFile() {
        result = "";
        TOaTrainFileVo objItems = new TOaTrainFileVo();
        objItems.ID = strFileId;
        if (new TOaTrainFileLogic().Delete(objItems)) {
            result = "true";

            strMessage = LogInfo.UserInfo.USER_NAME + "删除员工培训计划附件信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelTrainPlanFile, "", strMessage);
        }

        return result;
    }
    /// <summary>
    /// 获取URL参数
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
        
        //业务参数相关
        if (!String.IsNullOrEmpty(context.Request.Params["strtaskID"]))
        {
            strtaskID = context.Request.Params["strtaskID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainBt"]))
        {
            strTrainBt = context.Request.Params["strTrainBt"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainDate"]))
        {
            strTrainDate = context.Request.Params["strTrainDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainInfor"]))
        {
            strTrainInfor = context.Request.Params["strTrainInfor"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainTarger"]))
        {
            strTrainTarger = context.Request.Params["strTrainTarger"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainTo"]))
        {
            strTrainTo = context.Request.Params["strTrainTo"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainType"]))
        {
            strTrainType = context.Request.Params["strTrainType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTypes"]))
        {
            strTypes = context.Request.Params["strTypes"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAppId"]))
        {
            strAppId = context.Request.Params["strAppId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAppInfor"]))
        {
            strAppInfor = context.Request.Params["strAppInfor"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAppResult"]))
        {
            strAppResult = context.Request.Params["strAppResult"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFlowStatus"]))
        {
            strFlowStatus = context.Request.Params["strFlowStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPlanYear"]))
        {
            strPlanYear = context.Request.Params["strPlanYear"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDept"]))
        {
            strDept = context.Request.Params["strDept"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDarptDate"]))
        {
            strDarptDate = context.Request.Params["strDarptDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDarptId"]))
        {
            strDarptId = context.Request.Params["strDarptId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamType"]))
        {
            strExamType = context.Request.Params["strExamType"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strTrainResult"]))
        {
            strTrainResult = context.Request.Params["strTrainResult"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTchApp"]))
        {
            strTchApp = context.Request.Params["strTchApp"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTchAppId"]))
        {
            strTchAppId = context.Request.Params["strTchAppId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTchAppDate"]))
        {
            strTchAppDate = context.Request.Params["strTchAppDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTrainCompany"]))
        {
            strTrainCompany = context.Request.Params["strTrainCompany"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strFileId"]))
        {
            strFileId = context.Request.Params["strFileId"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strFileType"]))
        {
            strFileType = context.Request.Params["strFileType"].Trim();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}