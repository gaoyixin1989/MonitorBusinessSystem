<%@ WebHandler Language="C#" Class="ExamHander" %>

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
using i3.ValueObject.Channels.OA.EXAMINE;
using i3.BusinessLogic.Channels.OA.EXAMINE;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public class ExamHander : PageBase, IHttpHandler, IRequiresSessionState
{
    //系统用户信息
    public string strUserID ="";
    public string strMessage = "";
    public string strAction = "", strType = "", result = "";//执行方法，字典类别,返回值
    public DataTable dt = new DataTable();
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    //业务变量
    public string strExamStatus = "", strtaskID = "", strEmployeID = "", strEmployeName = "", strEmployePostName = "", strExamDate = "", strExamContent = "", strExamLeaderAppID = "", strExamLeaderAppContent = "", strExamLeaderAppDate = "", strExamType = "", strExamLevel = "", strExamDeptAppID = "", strExamDeptAppDate = "", strExamDeptApp = "", strSupperID = "";
    public string strExamID = "", strActionType = "";
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
                 case "GetExamViewList":
                     context.Response.Write(GetExamViewList());
                     context.Response.End();
                     break;
                     //获取考核单详细信息
                 case "GetEmployeExamInfor":
                     context.Response.Write(GetEmployeExamInfor());
                     context.Response.End();
                     break;
                 //获取登录用户档案信息
                 case "GetUserEmployInfor":
                     context.Response.Write(GetUserEmployInfor());
                     context.Response.End();
                     break;
                 //获取指定用户档案信息
                 case "GetSubUserEmployInfor":
                     context.Response.Write(GetSubUserEmployInfor());
                     context.Response.End();
                     break;
                     //获取单位全称
                 case "GetDeptFullName":
                     context.Response.Write(GetDeptFullName());
                     context.Response.End();
                     break;
                     //保存(新增或修改)数据
                 case "SaveExamInfor":
                     context.Response.Write(SaveExamInfor());
                     context.Response.End();
                     break;
                 case "DeleteExamInfor":
                     context.Response.Write(DeleteExamInfor());
                     context.Response.End();
                     break;
                 default:
                     break;
             }
         }
    }

    /// <summary>
    /// 获取用户考核列表
    /// </summary>
    /// <returns></returns>
    public string GetExamViewList() { 
    //判断是否为员工，如果为科室领导则显示本科室所有人员的考核记录，如果为单位领导，则显示主管科室所有的
        result = "";
        strUserID = LogInfo.UserInfo.ID;

        TOaEmployeInfoVo objEm = new TOaEmployeInfoVo();
        objEm.USER_ID = strUserID;
        DataTable  dtTemp = new TOaEmployeInfoLogic().SelectByTable(objEm);
        if (dtTemp.Rows.Count > 0)
        {
            //根据用户ID判断是否有领导职务
            //Code
            dt = new DataTable();
            TOaExamineInfoVo objItems = new TOaExamineInfoVo();
            objItems.USERID = dtTemp.Rows[0]["ID"].ToString();
            if (!String.IsNullOrEmpty(strExamStatus))
            {
                objItems.EXAMINE_STATUS = strExamStatus;
            }
            dt = new TOaExamineInfoLogic().SelectByTable(objItems, intPageIndex, intPageSize);
            int CountNum = new TOaExamineInfoLogic().GetSelectResultCount(objItems);
            result = LigerGridDataToJson(dt, CountNum);
        }

        return result;
    }

    /// <summary>
    /// 根据考核单ID获取考核信息
    /// </summary>
    /// <returns></returns>
    public string GetEmployeExamInfor() {
        result = "";
        dt = new DataTable();
        TOaExamineInfoVo objItems = new TOaExamineInfoVo();
        objItems.ID = strtaskID;
        dt = new TOaExamineInfoLogic().SelectByTable(objItems);
        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 获取当前登录用户档案信息信息
    /// </summary>
    /// <returns></returns>
    public string GetUserEmployInfor()
    {
        result = "";
        TOaEmployeInfoVo objitems = new TOaEmployeInfoVo();
        objitems.USER_ID =LogInfo.UserInfo.ID ;
        dt = new TOaEmployeInfoLogic().SelectByTable(objitems);

        result = LigerGridDataToJson(dt,dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 获取指定用户档案信息信息
    /// </summary>
    /// <returns></returns>
    public string GetSubUserEmployInfor()
    {
        result = "";
        TOaEmployeInfoVo objitems = new TOaEmployeInfoVo();
        objitems.ID = strUserID;
        dt = new TOaEmployeInfoLogic().SelectByTable(objitems);

        result = LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }
    /// <summary>
    /// 获取单位名称
    /// </summary>
    /// <returns></returns>
    public string GetDeptFullName() {
        result = "";
        string DeptFullName = ConfigurationManager.AppSettings["DeptInfor"].ToString();
        if (!String.IsNullOrEmpty(DeptFullName)) {
            result = DeptFullName;
        }

        return result;
    }

    /// <summary>
    /// 保存（新增或修改）数据
    /// </summary>
    /// <returns></returns>
    public string SaveExamInfor() {
        result = "";
        TOaExamineInfoVo objItems = new TOaExamineInfoVo();
        objItems.USERID = strEmployeID;
        objItems.EXAMINE_TYPE = strExamType;
        objItems.EXAMINE_YEAR = DateTime.Now.ToString("yyyy").ToString();
        objItems.EXAMINE_LEVEL = strExamLevel;
        objItems.LEADER_APP_ID = strExamLeaderAppID;
        objItems.LEADER_APP_DATE = strExamLeaderAppDate;

        objItems.DEPT_APP_ID = strExamDeptAppID;
        objItems.DEPT_APP_DATE = strExamDeptAppDate;
        objItems.SUPERIOR_APP_ID = strSupperID;
        
        if (String.IsNullOrEmpty(strtaskID))
        {
                objItems.ID = GetSerialNumber("t_oa_ExamInforID");
                if (strExamType == "2")
                {
                    objItems.EXAMINE_DATE = strExamDate;
                }
                else
                {
                    objItems.EXAMINE_DATE = DateTime.Now.ToString();
                }
                if (new TOaExamineInfoLogic().Create(objItems)) {
                string TempType = "事业单位人员";
                if (strExamType == "2") {
                    TempType = "专业技术人员";
                }
                TempType = TempType + "(" + strEmployeName + ")" + objItems.EXAMINE_YEAR + "年度考核登记表";

                result = objItems.ID + "|" + TempType;

                strMessage = LogInfo.UserInfo.USER_NAME + "新增人事考核" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddExamInfor, "", strMessage);
            }
        }
        else {
            objItems.ID = strtaskID;
            if (new TOaExamineInfoLogic().Edit(objItems))
            {
                result = objItems.ID;
            }
            strMessage = LogInfo.UserInfo.USER_NAME + "编辑人事考核" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddExamInfor, "", strMessage);
        }
        return result;
    }

    /// <summary>
    /// 删除员工考核
    /// </summary>
    /// <returns></returns>
    public string DeleteExamInfor() {
        result = "";
        TOaExamineInfoVo objItems = new TOaExamineInfoVo();
        objItems.ID = strtaskID;

        if (new TOaExamineInfoLogic().Delete(objItems)) {
            result = "true";
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
        if (!String.IsNullOrEmpty(context.Request.Params["strUserID"]))
        {
            strUserID = context.Request.Params["strUserID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeID"]))
        {
            strEmployeID = context.Request.Params["strEmployeID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeName"]))
        {
            strEmployeName = context.Request.Params["strEmployeName"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strEmployePostName"]))
        {
            strEmployePostName = context.Request.Params["strEmployePostName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strtaskID"]))
        {
            strtaskID = context.Request.Params["strtaskID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamType"]))
        {
            strExamType = context.Request.Params["strExamType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamContent"]))
        {
            strExamContent = context.Request.Params["strExamContent"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamDate"]))
        {
            strExamDate = context.Request.Params["strExamDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamStatus"]))
        {
            strExamStatus = context.Request.Params["strExamStatus"].Trim();
        }
        //单位领导意见
        if (!String.IsNullOrEmpty(context.Request.Params["strExamLeaderAppDate"]))
        {
            strExamLeaderAppDate = context.Request.Params["strExamLeaderAppDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamLeaderAppID"]))
        {
            strExamLeaderAppID = context.Request.Params["strExamLeaderAppID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strLeaderAppContent"]))
        {
            strExamLeaderAppContent = context.Request.Params["strLeaderAppContent"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strExamLevel"]))
        {
            strExamLevel = context.Request.Params["strExamLevel"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamDeptApp"]))
        {
            strExamDeptApp = context.Request.Params["strExamDeptApp"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamDeptAppID"]))
        {
            strExamDeptAppID = context.Request.Params["strExamDeptAppID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strExamDeptAppDate"]))
        {
            strExamDeptAppDate = context.Request.Params["strExamDeptAppDate"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strSupperID"]))
        {
            strSupperID = context.Request.Params["strSupperID"].Trim();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}