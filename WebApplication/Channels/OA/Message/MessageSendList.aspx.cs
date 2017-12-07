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
/// 功能描述：短消息列表（发件箱）
/// 创建日期：2012-12-1
/// 创建人  ：苏成斌
/// </summary>
/// 


public partial class Channels_OA_Message_MessageSendList : PageBase
{
    private static SendMobileMsgV2 cInstanceMsgHandle = new SendMobileMsgV2();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getMessage")
        {
            strResult = getMessage();
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

        //DataTable dtDept = new TSysPostLogic().SelectByTable_byUser(LogInfo.UserInfo.ID);
        //for (int i = 0; i < dtDept.Rows.Count; i++)
        //{
        //    string strDeptCode = dtDept.Rows[i]["POST_DEPT_ID"].ToString();
        //    if (strDeptCode.Length > 0)
        //    {
        //        strDept += (strDept.Length > 0) ? "," + strDeptCode : strDeptCode;
        //    }
        //}

        TOaMessageInfoVo objMessage = new TOaMessageInfoVo();
        objMessage.SEND_BY = LogInfo.UserInfo.ID;
        objMessage.SORT_FIELD = strSortname;
        objMessage.SORT_TYPE = strSortorder;
        DataTable dt = new TOaMessageInfoLogic().SelectByTable(objMessage);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["ACCEPT_TYPE"].ToString() == "1")
                dt.Rows[i]["ACCEPT_TYPE"] = "全站";
            if (dt.Rows[i]["ACCEPT_TYPE"].ToString() == "2")
                dt.Rows[i]["ACCEPT_TYPE"] = "个人";
        }
        int intTotalCount = new TOaMessageInfoLogic().GetSelectResultCount(objMessage);
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除信息
    [WebMethod]
    public static string deleteData(string strValue)
    {
        TOaMessageInfoVo objMessage = new TOaMessageInfoVo();
        objMessage.ID = strValue;
        bool isSuccess = new TOaMessageInfoLogic().Delete(objMessage);

        if (isSuccess)
            new PageBase().WriteLog("删除短发送消息", "", new UserLogInfo().UserInfo.USER_NAME + "删除发送短消息" + objMessage.ID);
        return isSuccess == true ? "1" : "0";
    }

    //发送短信
    [WebMethod]
    public static string SendMsg(string strID, string strMessageTitle, string strSendBy, string strSendDate, string strAcceptType,
        string strAcceptRealNames, string strAcceptUserIDs, string strAcceptDeptNames, string strAcceptDeptIDs, string strMessageContent, string strUserID)
    {
        string strRet = SaveData(strID, strMessageTitle, strSendBy, strSendDate, strAcceptType, strAcceptRealNames, strAcceptUserIDs, strAcceptDeptNames, strAcceptDeptIDs, strMessageContent, strUserID);

        return strRet;
    }

    //保存数据
    private static string SaveData(string strID, string strMessageTitle, string strSendBy, string strSendDate, string strAcceptType,
        string strAcceptRealNames, string strAcceptUserIDs, string strAcceptDeptNames, string strAcceptDeptIDs, string strMessageContent, string strUserID)
    {
        string strSendByName = strSendBy;

        TSysUserVo objUserFrom = new TSysUserVo();
        objUserFrom.ID = strSendBy;
        DataTable dtUserSender = new TSysUserLogic().SelectByTable_ByDept(objUserFrom, 0, 0);

        if (dtUserSender.Rows.Count > 0)
        {
            strSendByName = dtUserSender.Rows[0]["REAL_NAME"].ToString();
        }


        bool isSuccess = true;
        strAcceptRealNames = strAcceptRealNames.Trim();
        strAcceptUserIDs = strAcceptUserIDs.Trim();
        strAcceptDeptNames = strAcceptDeptNames.Trim();
        strAcceptDeptIDs = strAcceptDeptIDs.Trim();

        #region 消息查看--编辑消息阅读状态
        if (strID.Length > 0)
        {
            TOaMessageInfoReceiveVo objReceive = new TOaMessageInfoReceiveVo();
            objReceive.IS_READ = "0";
            objReceive.MESSAGE_ID = strID;
            objReceive.RECEIVER = strUserID;
            objReceive = new TOaMessageInfoReceiveLogic().Details(objReceive);
            if (objReceive.ID.Length > 0)
            {
                objReceive.IS_READ = "1";
                if (new TOaMessageInfoReceiveLogic().Edit(objReceive))
                {
                    new PageBase().WriteLog("编辑消息阅读状态", "", new UserLogInfo().UserInfo.USER_NAME + "编辑消息阅读状态" + objReceive.ID);
                }

                return "1";
            }
        }
        #endregion

        DataTable dtDept = new TSysDictLogic().SelectByTable(new TSysDictVo() { DICT_TYPE = "dept" });
        strAcceptDeptNames = strAcceptDeptNames.Replace(";", "，");
        string[] arrAccDeptName = strAcceptDeptNames.Split('，');
        for (int i = 0; i < arrAccDeptName.Length; i++)
        {
            if (arrAccDeptName[i].Length > 0)
            {
                DataRow[] drDept = dtDept.Select("DICT_TEXT='" + arrAccDeptName[i] + "'");
                if (drDept.Length > 0)
                {
                    strAcceptDeptIDs += (strAcceptDeptIDs.Length > 0 ? "," : "") + drDept[0]["DICT_CODE"];
                }
            }
        }

        #region 清空对应的几个接收信息表，冗余操作，但可以防止错误
        if (strID.Length > 0)
        {
            //消息接收人表
            TOaMessageInfoManVo objMessageMan = new TOaMessageInfoManVo();
            objMessageMan.MESSAGE_ID = strID;
            if (new TOaMessageInfoManLogic().Delete(objMessageMan))
                new PageBase().WriteLog("删除消息接收人", "", new UserLogInfo().UserInfo.USER_NAME + "删除消息接收人" + objMessageMan.ID);


            //消息阅读状态表
            TOaMessageInfoReceiveVo objMessageReceive = new TOaMessageInfoReceiveVo();
            objMessageReceive.MESSAGE_ID = strID;
            if (new TOaMessageInfoReceiveLogic().Delete(objMessageReceive))
                new PageBase().WriteLog("删除消息阅读状态", "", new UserLogInfo().UserInfo.USER_NAME + "删除消息阅读状态" + objMessageReceive.ID);

            //消息接收部门表
            TOaMessageInfoDeptVo objMessageDept = new TOaMessageInfoDeptVo();
            objMessageDept.MESSAGE_ID = strID;
            if (new TOaMessageInfoDeptLogic().Delete(objMessageDept))
                new PageBase().WriteLog("删除消息接收部门", "", new UserLogInfo().UserInfo.USER_NAME + "删除消息接收部门" + objMessageDept.ID);
        }
        #endregion

        string strSQL = "";
        #region 新增消息
        if (strID.Length > 0)
        {
            strSQL = "update T_OA_MESSAGE_INFO set MESSAGE_TITLE='{0}',SEND_BY='{1}',SEND_DATE='{2}',ACCEPT_TYPE='{3}',";
            strSQL += "ACCEPT_REALNAMES='{4}',ACCEPT_USERIDS='{5}',ACCEPT_DEPTNAMES='{6}',ACCEPT_DEPTIDS='{7}',";
            strSQL += "MESSAGE_CONTENT='{8}'";
            strSQL += " where ID='{9}'";
            strSQL = string.Format(strSQL, strMessageTitle, strSendBy, strSendDate, (strAcceptType == "全站") ? "1" : "2",
                strAcceptRealNames.TrimEnd('，'), strAcceptUserIDs.TrimEnd(','), strAcceptDeptNames, strAcceptDeptIDs,
                strMessageContent, strID);
        }
        else
        {
            strID = GetSerialNumber("t_oa_messageinfo_id");
            strSQL = "insert into T_OA_MESSAGE_INFO (ID,MESSAGE_TITLE,SEND_BY,SEND_DATE,ACCEPT_TYPE,ACCEPT_REALNAMES,ACCEPT_USERIDS,ACCEPT_DEPTNAMES,ACCEPT_DEPTIDS,MESSAGE_CONTENT)";
            strSQL += " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
            strSQL = string.Format(strSQL, strID, strMessageTitle, strSendBy, strSendDate, (strAcceptType == "全站") ? "1" : "2",
               strAcceptRealNames.TrimEnd('，'), strAcceptUserIDs.TrimEnd(','), strAcceptDeptNames, strAcceptDeptIDs,
               strMessageContent);
        }
        #endregion

        string strAccUserIDs = "";
        if (strAcceptType == "全站")//全站
        {
            TSysUserVo objUser = new TSysUserVo();

            objUser.IS_DEL = "0";
            objUser.IS_HIDE = "0";
            objUser.IS_USE = "1";

            DataTable dtUser = new TSysUserLogic().SelectByTable(objUser);

            for (int j = 0; j < dtUser.Rows.Count; j++)
            {
                // by yinchengyi 2014-10-23  T_OA_MESSAGE_INFO_MAN 没用
                //strSQL += "\r\n";
                //strSQL += "insert into T_OA_MESSAGE_INFO_MAN (ID,MESSAGE_ID,RECEIVER_ID)";
                //strSQL += " values('{0}','{1}','{2}')";
                //strSQL = string.Format(strSQL, GetSerialNumber("t_oa_messageinfoman_id"), strID, dtUser.Rows[j]["ID"].ToString());

                string strErrMsg = cInstanceMsgHandle.SendMsg(strSendByName, dtUser.Rows[j]["REAL_NAME"].ToString(), dtUser.Rows[j]["PHONE_MOBILE"].ToString().Trim(), strMessageContent);

                strSQL += "\r\n";
                strSQL += "insert into T_OA_MESSAGE_INFO_RECEIVE (ID,MESSAGE_ID,RECEIVER,IS_READ,REMARK3)";
                strSQL += " values('{0}','{1}','{2}','{3}','{4}')";
                strSQL = string.Format(strSQL, GetSerialNumber("t_oa_messageinfodel_id"), strID, dtUser.Rows[j]["ID"].ToString(), "0", strErrMsg);

                strAccUserIDs += (strAccUserIDs.Length > 0 ? "," : "") + dtUser.Rows[j]["ID"].ToString();
            }
        }
        else//按部门，按个人分配消息
        {
            // by yinchengyi 2014-10-23  T_OA_MESSAGE_INFO_MAN 没用
            //for (int i = 0; i < strAcceptUserIDs.Split(',').Length; i++)
            //{
            //    if (strAcceptUserIDs.Split(',')[i].Length > 0)
            //    {
            //        strSQL += "\r\n";
            //        strSQL += "insert into T_OA_MESSAGE_INFO_MAN (ID,MESSAGE_ID,RECEIVER_ID)";
            //        strSQL += " values('{0}','{1}','{2}')";
            //        strSQL = string.Format(strSQL, GetSerialNumber("t_oa_messageinfoman_id"), strID, strAcceptUserIDs.Split(',')[i]);
            //    }
            //}

            // by yinchengyi 2014-10-23  T_OA_MESSAGE_INFO_DEPT 没用
            //for (int i = 0; i < strAcceptDeptIDs.Split(',').Length; i++)
            //{
            //    if (strAcceptDeptIDs.Split(',')[i].Length > 0)
            //    {
            //        strSQL += "\r\n";
            //        strSQL += "insert into T_OA_MESSAGE_INFO_DEPT (ID,MESSAGE_ID,DEPT_ID)";
            //        strSQL += " values('{0}','{1}','{2}')";
            //        strSQL = string.Format(strSQL, GetSerialNumber("t_oa_messageinfodept_id"), strID, strAcceptDeptIDs.Split(',')[i]);
            //    }
            //}


            //部门人员添加
            if (strAcceptDeptIDs.Length > 0)
            {
                TSysUserVo objUser = new TSysUserVo();

                objUser.IS_DEL = "0";
                objUser.REMARK4 = strAcceptDeptIDs.Replace(",", "','");
                DataTable dtUserByDept = new TSysUserLogic().SelectByTable_ByDept(objUser, 0, 0);
                for (int j = 0; j < dtUserByDept.Rows.Count; j++)
                {
                    if (!strAcceptUserIDs.Contains(dtUserByDept.Rows[j]["ID"].ToString()))
                        strAcceptUserIDs += (strAcceptUserIDs.Length > 0 ? "," : "") + dtUserByDept.Rows[j]["ID"].ToString();
                }
            }

            for (int i = 0; i < strAcceptUserIDs.Split(',').Length; i++)
            {
                if (strAcceptUserIDs.Split(',')[i].Length > 0)
                {
                    TSysUserVo objUser = new TSysUserVo();
                    objUser.ID = strAcceptUserIDs.Split(',')[i];
                    DataTable dtUserByDept = new TSysUserLogic().SelectByTable_ByDept(objUser, 0, 0);

                    string strErrMsg = "unknown user";

                    if (dtUserByDept.Rows.Count > 0)
                    {
                        strErrMsg = cInstanceMsgHandle.SendMsg(strSendByName, dtUserByDept.Rows[0]["REAL_NAME"].ToString(), dtUserByDept.Rows[0]["PHONE_MOBILE"].ToString().Trim(), strMessageContent);
                    }

                    strSQL += "\r\n";
                    strSQL += "insert into T_OA_MESSAGE_INFO_RECEIVE (ID,MESSAGE_ID,RECEIVER,IS_READ,REMARK3)";
                    strSQL += " values('{0}','{1}','{2}','{3}','{4}')";
                    strSQL = string.Format(strSQL, GetSerialNumber("t_oa_messageinfodel_id"), strID, strAcceptUserIDs.Split(',')[i], "0",strErrMsg);


                }
            }

            strAccUserIDs += (strAccUserIDs.Length > 0 ? "," : "") + strAcceptUserIDs;
        }

        //if (strAccUserIDs.Length > 0)
        //{
        //    string strErrMsg = "";
        //    //new SendMobileMsg().AutoSenMobilMsg(strMessageContent, strSendBy, strAccUserIDs, true, "", ref strErrMsg);
            
        //}

        #region 消息体
        isSuccess = SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        if (isSuccess)
        {
            if (strID.Length > 0)
            {
                new PageBase().WriteLog("编辑短消息信息", "", new UserLogInfo().UserInfo.USER_NAME + "编辑短消息信息" + strID);
            }
            else
            {
                new PageBase().WriteLog("添加短消息信息", "", new UserLogInfo().UserInfo.USER_NAME + "添加短消息信息" + strID);
            }
        }
        #endregion

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}