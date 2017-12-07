using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.OA.Message;
using i3.BusinessLogic.Channels.OA.Message;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

using i3.DataAccess;

/// <summary>
/// 功能描述：短消息列表（收件箱）
/// 创建日期：2012-11-29
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_Message_MessageList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getMessage")
        {
            TOaMessageInfoVo megs = new TOaMessageInfoVo();
            megs.REMARK2 = "1";
            megs.SEND_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            new TOaMessageInfoLogic().Edit(megs);

            strResult = getMessage();
            Response.Write(strResult);
            Response.End();
        }

        if (Request["type"] != null && Request["type"].ToString() == "UpdateStatus")
        {
            if (Request["strValue"] != null)
            {
                strResult = UpdateStatus(Request["strValue"].ToString());
            }

            Response.Write(strResult);
            Response.End();
        }

        if (Request["type"] != null && Request["type"].ToString() == "deleteData")
        {
            if (Request["strValue"] != null)
            {
                strResult = deleteData(Request["strValue"].ToString());
            }

            Response.Write(strResult);
            Response.End();
        }
    }

    //获取信息
    private string getMessage()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        string strDept = "";
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TOaMessageInfoVo.ID_FIELD;

        DataTable dtDept = new TSysPostLogic().SelectByTable_byUser(LogInfo.UserInfo.ID);
        for (int i = 0; i < dtDept.Rows.Count; i++)
        {
            string strDeptCode = dtDept.Rows[i]["POST_DEPT_ID"].ToString();
            if (strDeptCode.Length > 0)
            {
                strDept += (strDept.Length > 0) ? "," + strDeptCode : strDeptCode;
            }
        }

        TOaMessageInfoVo objMessage = new TOaMessageInfoVo();
        objMessage.SORT_FIELD = strSortname;
        objMessage.SORT_TYPE = strSortorder;
        DataTable dt = new TOaMessageInfoLogic().SelectByUserIdAndDept(LogInfo.UserInfo.ID, intPageIndex, intPageSize);
        int intTotalCount = new TOaMessageInfoLogic().GetSelectByUserIdAndDeptCount(LogInfo.UserInfo.ID, strDept);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["REMARK1"].ToString() == "0")
                dt.Rows[i]["REMARK1"] = "未读";
            if (dt.Rows[i]["REMARK1"].ToString() == "1")
                dt.Rows[i]["REMARK1"] = "已读";
        }

        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除信息
    public string deleteData(string strValue)
    {
        //TOaMessageInfoVo objMessage = new TOaMessageInfoVo();
        //objMessage.ID = strValue;
        //bool isSuccess = new TOaMessageInfoLogic().Delete(objMessage);

        TOaMessageInfoReceiveVo objMessageRecv = new TOaMessageInfoReceiveVo();

        objMessageRecv.MESSAGE_ID = strValue;

        objMessageRecv.RECEIVER = LogInfo.UserInfo.ID;

        bool isSuccess = new TOaMessageInfoReceiveLogic().Delete(objMessageRecv);

        if (isSuccess)
        {
            new PageBase().WriteLog("删除接收短消息", "", new UserLogInfo().UserInfo.USER_NAME + "删除接收短消息" + objMessageRecv.ID);
        }

        return isSuccess == true ? "1" : "0";
    }

    // 更新消息状态（0：未读，1：已读）
    public string UpdateStatus(string strValue)
    {
        TOaMessageInfoReceiveVo objMessage_where = new TOaMessageInfoReceiveVo();
        TOaMessageInfoReceiveVo objMessage_set = new TOaMessageInfoReceiveVo();

        objMessage_where.MESSAGE_ID = strValue;

        objMessage_where.RECEIVER = LogInfo.UserInfo.ID;

        objMessage_set.IS_READ = "1";

        bool isSuccess = new TOaMessageInfoReceiveLogic().Edit(objMessage_set, objMessage_where);

        if (isSuccess)
        {
            new PageBase().WriteLog("更新消息状态为：1（已读）", "", new UserLogInfo().UserInfo.USER_NAME + "更新消息状态" + objMessage_where.ID);
        }

        return "1";
    }
}