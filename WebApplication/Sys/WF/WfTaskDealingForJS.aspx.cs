using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：任务追踪
/// 创建日期：2012-11-08
/// 创建人  ：石磊
/// 修改日期：2013-01-07
/// 修改人  ：潘德军
/// 修改内容：重构界面
/// </summary>
public partial class Sys_WF_WfTaskDealingForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            getData();
        }
    }

    //获取信息
    private void getData()
    {
        string strUserID = base.LogInfo.UserInfo.ID;

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = "WF_STARTTIME";

        TWfInstTaskDetailLogic logic = new TWfInstTaskDetailLogic();
        TWfInstTaskDetailVo detail = new TWfInstTaskDetailVo();
        detail.SORT_FIELD = strSortname;
        detail.SORT_TYPE = strSortorder;
        DataTable dt = logic.SelectByTableForUserDealing_OA( "2A", intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCountForUserDealing_OA( "2A");
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    public string GetStepHeight(object strStepID)
    {
        int iHeader = 200;
        int iStep = 100;
        int iHeight = 560;

        if (null == strStepID || string.IsNullOrEmpty(strStepID.ToString()))
            return iHeight.ToString();

        DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strStepID.ToString() });
        if (dt.Rows.Count == 0)
            iHeight = iHeader + iStep * 4;
        else
            iHeight = iHeader + iStep * dt.Rows.Count;
        return iHeight.ToString();
    }

    /// <summary>
    /// 获取业务流程信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getWFName(string strValue)
    {
        string strWfName = "";
        DataTable dtWF = new TWfSettingFlowLogic().SelectByTable(new TWfSettingFlowVo());

        foreach (DataRow dr in dtWF.Rows)
            if (dr[TWfSettingFlowVo.WF_ID_FIELD].ToString() == strValue)
                strWfName= dr[TWfSettingFlowVo.WF_CAPTION_FIELD].ToString();

        return strWfName;
    }

    /// <summary>
    /// 获取当前环节信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getTaskName(string strValue)
    {
        string strTaskName = "";
        DataTable dtTask = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo());

        foreach (DataRow dr in dtTask.Rows)
            if (dr[TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strValue)
                strTaskName = dr[TWfSettingTaskVo.TASK_CAPTION_FIELD].ToString();

        return strTaskName;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getUserName(string strValue)
    {
        return new TSysUserLogic().Details(strValue).REAL_NAME;
    }
}