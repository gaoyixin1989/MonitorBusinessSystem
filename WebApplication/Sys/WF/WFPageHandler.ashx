<%@ WebHandler Language="C#" Class="WFPageHandler" %>

using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.SessionState;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
/// <summary>
/// 创建人:  胡方扬 
/// 创建时间:2013-02-23
/// 创建原因：为跳转工作流提供 选择环节后根据获取的用户ID获取用户信息 重新绑定用户列表控件
/// </summary>
public class WFPageHandler : PageBase, IHttpHandler, IRequiresSessionState
{
    private string result = "", strMessage = "";
    private DataTable dt = new DataTable();
    private string strSortname = "", strSortorder = "", strAction = "";
    private string strUserId = "", strSetpValue = "", strWfId = "";
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID)) {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
        GetRequest(context);
        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                //  获取用户信息
                case "getUserInfor":
                    context.Response.Write(getUserInfor());
                    context.Response.End();
                    break;
                case "SelectedChange":
                    context.Response.Write(SelectedChange());
                    context.Response.End();
                    break;
                default:
                    break;
            }
                  
        }
    }

    /// <summary>
    ///  获取用户信息
    /// </summary>
    /// <returns></returns>
    private string getUserInfor()
    {
        result = "";
        DataTable dt = new DataTable();
        if (!String.IsNullOrEmpty(strUserId))
        {
            TSysUserVo objItems = new TSysUserVo();
            objItems.ID = strUserId;
            dt = new TSysUserLogic().SelectByTable(objItems);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 用户更换环节时触发返回选择环节的用户列表
    /// </summary>
    /// <returns></returns>
    private string SelectedChange()
    {
        result = "";
        string strUserList = "";
        if (!String.IsNullOrEmpty(strSetpValue))
        {
            //InitDivViewAndTaskData(this.WF_ID, this.TASK_ID, this.STEP_NAME.SelectedValue, this.WF_INST_TASK_ID);
            //只刷新跳转环节所对应的 人员范围，不涉及其他资料刷新
            TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strSetpValue, WF_ID = strWfId });

            DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
            DataTable dtUserInfo = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0", IS_USE = "1" });
            //增加限定处理人员信息，先将以职位存储的数据转换为系统用户
            if (task.OPER_TYPE == "02")
            {
                foreach (string strPost in task.OPER_VALUE.Split('|'))
                    foreach (DataRow dr in dtUserPost.Rows)
                        if (dr[TSysUserPostVo.POST_ID_FIELD].ToString() == strPost)
                            if (strUserList.IndexOf(dr[TSysUserPostVo.USER_ID_FIELD].ToString()) < 0)
                            {
                                strUserList += dr[TSysUserPostVo.USER_ID_FIELD].ToString() + "|";
                                continue;
                            }
            }
            else
                strUserList = task.OPER_VALUE;
        }
        result = strUserList;
        return result;
    }
    
    public void GetRequest(HttpContext context)
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
     
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserId"]))
        {
            strUserId = context.Request.Params["strUserId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSetpValue"]))
        {
            strSetpValue = context.Request.Params["strSetpValue"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strWfId"]))
        {
            strWfId = context.Request.Params["strWfId"].Trim();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }
    

}