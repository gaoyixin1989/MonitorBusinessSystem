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

/// <summary>
/// 功能描述：短消息发送页面
/// 创建日期：2012-12-3
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_Message_MessageInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["type"] == null)
        {
            this.UserRealName.Value = LogInfo.UserInfo.REAL_NAME;
            this.UserID.Value = LogInfo.UserInfo.ID;

        }

        if (Request["strid"] != null)
        {
            strID = Request["strid"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strID);            
        }
    }

    //获取数据
    private void GetData(string strID)
    {
        TOaMessageInfoVo objMessage = new TOaMessageInfoLogic().Details(strID);
        objMessage.SEND_BY = new TSysUserLogic().Details(objMessage.SEND_BY).REAL_NAME;
        objMessage.REMARK1 = LogInfo.UserInfo.ID;

        string strJson = ToJson(objMessage);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetDeptItems()
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysDictVo objVo = new TSysDictVo();
        objVo.DICT_TYPE = "dept";
        dt = new TSysDictLogic().SelectByTable(objVo);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的尚未选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMessageId"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSubUserItems(string strPost_Dept, string strMessageId)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysUserVo objUser = new TSysUserVo();
        objUser.IS_DEL = "0";
        dt = new TSysUserLogic().SelectByTableUnderDept(strPost_Dept, 0, 0);

        TOaMessageInfoVo objMessage = new TOaMessageInfoLogic().Details(strMessageId);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();
        if(objMessage.ACCEPT_USERIDS.Length > 0)
        {
            for (int i = 0; i < objMessage.ACCEPT_USERIDS.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + objMessage.ACCEPT_USERIDS.Split(',')[i] + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        Temrow.Delete();
                        dt.AcceptChanges();
                    }
                }
            }
        }

        dtItems = dt.Copy();

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的尚已选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMonitorId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectUserItems(string strMessageId)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        DataTable dtDuty = new DataTable();
        dt = new TSysUserLogic().SelectByTableUnderDept("", 0, 0);

        TOaMessageInfoVo objMessage = new TOaMessageInfoLogic().Details(strMessageId);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (objMessage.ACCEPT_USERIDS.Length > 0)
        {
            for (int i = 0; i < objMessage.ACCEPT_USERIDS.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + objMessage.ACCEPT_USERIDS.Split(',')[i] + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItems.ImportRow(Temrow);
                    }
                }
            }
        }

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }
}