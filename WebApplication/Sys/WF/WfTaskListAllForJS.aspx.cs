using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public partial class Sys_WF_WfTaskListAllForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取任务列表
        if (Request["Action"] != null && Request["Action"].ToString() == "GetTask")
        {
            //传参数TaskState进来，2A,2B,2C,2D和 ""五个参数，其中""表示"非2A和2D"的
            string strTaskState = (null == Request["TaskState"] ? "XX" : Request["TaskState"].Trim());
            string strIsDept = "";
            if (Request["isDept"] == null || Request["isDept"].ToString().Trim().Length == 0 || Request["isDept"].ToString().ToLower().Trim() == "undefined")
            {
                strIsDept = "";
            }
            else
            {
                strIsDept =  Request["isDept"].Trim();
            }
            string strIsAllOrDeptOrSelf = "";
            if (Request["isAllOrDeptOrSelf"] == null || Request["isAllOrDeptOrSelf"].ToString().Trim().Length == 0 || Request["isAllOrDeptOrSelf"].ToString().ToLower().Trim() == "undefined")
            {
                strIsAllOrDeptOrSelf = "";
            }
            else
            {
                strIsAllOrDeptOrSelf = Request["isAllOrDeptOrSelf"].Trim();
            }

            //getTaskLst(LogInfo.UserInfo.ID, strTaskState);
            //0,个人；1，科室；2，全站；
            if (strIsAllOrDeptOrSelf.Length == 0)
                getTaskLst(LogInfo.UserInfo.ID, strTaskState);
            else
            {
                if (strIsAllOrDeptOrSelf == "0")
                    getTaskLst(LogInfo.UserInfo.ID, strTaskState);
                else if (strIsAllOrDeptOrSelf == "1")
                    getTaskLst(GetUserId_InDept(), strTaskState);
                else if (strIsAllOrDeptOrSelf == "2")
                    getTaskLst("", strTaskState);
            }
        }

    }

    //获取任务列表
    private void getTaskLst(string strUserID, string strTaskState)
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        string strWF_SERVICE_NAME = (Request.Params["SrhWF_SERVICE_NAME"] != null) ? Request.Params["SrhWF_SERVICE_NAME"] : "";
        string strSRC_USER = (Request.Params["SrhSRC_USER"] != null) ? Request.Params["SrhSRC_USER"] : "";
        string strINST_TASK_STARTTIME_from = (Request.Params["SrhINST_TASK_STARTTIME_from"] != null) ? Request.Params["SrhINST_TASK_STARTTIME_from"] : "";
        string strINST_TASK_STARTTIME_to = (Request.Params["SrhINST_TASK_STARTTIME_to"] != null) ? Request.Params["SrhINST_TASK_STARTTIME_to"] : "";

        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        DataTable dt = logic.SelectByTableForUserTaskListEx(strUserID, strTaskState, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to, intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCountForUserTaskListEx(strUserID, strTaskState, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 当前环节
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetTaskName(string strValue)
    {
        DataTable dtTask = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo());
        foreach (DataRow dr in dtTask.Rows)
            if (dr[TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strValue)
                return dr[TWfSettingTaskVo.TASK_CAPTION_FIELD].ToString();
        return "";
    }

    /// <summary>
    /// 发送人
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }

    /// <summary>
    /// 任务确认和撤销，确认后，该任务才可以进入办理环节，撤销后，任务可直接变为非确认状态
    /// </summary>
    /// <param name="strValue">环节ID</param>
    /// <param name="strUserID">用户ID</param>
    /// <param name="bIsUnConfirm">确认的用户</param>
    /// <returns></returns>
    [WebMethod]
    public static bool SetTaskToConfirm(string strValue, bool bIsUnConfirm)
    {
        TWfInstTaskDetailVo ttdv = new TWfInstTaskDetailVo();
        ttdv.ID = strValue;
        if (!bIsUnConfirm)
        {
            ttdv.CFM_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ttdv.INST_TASK_STATE = TWfCommDict.StepState.StateConfirm;
            ttdv.CFM_USER = new PageBase().LogInfo.UserInfo.ID;
        }
        else
        {
            ttdv.CFM_UNTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ttdv.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        }
        return new TWfInstTaskDetailLogic().Edit(ttdv);
    }

    [WebMethod]
    public static string GetStepStateName(string strStepState)
    {
        switch (strStepState)
        {
            case "2A":
                return "待确认";
            case "2B":
                return "已处理";
            case "2C":
                return "已保存";
            case "2D":
                return "已确认";
            default:
                return "";
        }

    }

    private string GetUserId_InDept()
    {
        TSysUserVo tUserV = new TSysUserVo();
        tUserV.IS_DEL = "0";
        tUserV.IS_HIDE="0";
        tUserV.IS_USE="1";
        DataTable dtUser = new TSysUserLogic().SelectByTable(tUserV);

        TSysPostVo tPostV = new TSysPostVo();
        tPostV.IS_DEL = "0";
        tPostV.IS_HIDE = "0";
        DataTable dtPost = new TSysPostLogic().SelectByTable(tPostV);

        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());

        string strLocalUserId = base.LogInfo.UserInfo.ID;
        string strReturnUserId = "'" + strLocalUserId + "'";

        //用户的所有职位
        DataRow[] drLocalUserPost = dtUserPost.Select("USER_ID='" + strLocalUserId + "'");
        string strLocalPostIDs = "";
        for (int i = 0; i < drLocalUserPost.Length; i++)
        {
            strLocalPostIDs += (strLocalPostIDs.Length > 0 ? "," : "") + "'" + drLocalUserPost[i]["POST_ID"].ToString() + "'";
        }
        if (strLocalPostIDs.Length == 0)
            return strReturnUserId;

        //用户的所有职位为部门主任或副主任的部门id
        DataRow[] drLocalPost = dtPost.Select("ID in (" + strLocalPostIDs + ") and POST_LEVEL_ID in ('Director','DirectorEx')");
        string strLocalDeptIDs = "";
        for (int i = 0; i < drLocalPost.Length; i++)
        {
            strLocalDeptIDs += (strLocalDeptIDs.Length > 0 ? "," : "") + "'" + drLocalPost[i]["POST_DEPT_ID"].ToString() + "'";
        }
        if (strLocalDeptIDs.Length == 0)
            return strReturnUserId;

        //用户的职位为部门主任或副主任，的部门的所有职位
        DataRow[] drSrhPost = dtPost.Select("POST_DEPT_ID in (" + strLocalDeptIDs + ")");
        string strSrhPostIDs = "";
        for (int i = 0; i < drSrhPost.Length; i++)
        {
            strSrhPostIDs += (strSrhPostIDs.Length > 0 ? "," : "") + "'" + drSrhPost[i]["ID"].ToString() + "'";
        }
        if (strSrhPostIDs.Length == 0)
            return strReturnUserId;

        //用户的职位为部门主任或副主任，的部门的所有职位对应的用户
        DataRow[] drSrhUserPost = dtUserPost.Select("POST_ID in (" + strSrhPostIDs + ")");
        string strSrhUSerIDs = "";
        for (int i = 0; i < drSrhUserPost.Length; i++)
        {
            strSrhUSerIDs += (strSrhUSerIDs.Length > 0 ? "," : "") + "'" + drSrhUserPost[i]["USER_ID"].ToString() + "'";
        }
        if (strSrhUSerIDs.Length == 0)
            return strReturnUserId;

        return strSrhUSerIDs;
    }
}