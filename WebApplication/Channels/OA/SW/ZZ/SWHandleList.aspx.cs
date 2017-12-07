using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
using System.Data;

/// <summary>
/// 收文办理管理
/// 创建人：魏林
/// 创建时间：2013-07-16
/// </summary>
public partial class Channels_OA_SW_ZZ_SWHandleList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strJson = "";

        if (Request["action"] != null)
        {
            switch (Request["action"].ToString())
            {
                case "getGridInfo":
                    strJson = getGridInfo();
                    break;
                default:
                    break;
            }
            Response.Write(strJson);
            Response.End();
        }
    }

    //获取列表信息
    private string getGridInfo()
    {
        string where = "1=1";
        if (Request["TASKNAME"] != null)
        {
            if (Request["TASKNAME"].ToString().Trim() != "")
            {
                where += " and TASK_NAME like '%" + Request["TASKNAME"].ToString().Trim() + "%'";
            }
        }
        if (Request["SENDUSER"] != null)
        {
            if (Request["SENDUSER"].ToString().Trim() != "")
            {
                where += " and SEND_USER like '%" + Request["SENDUSER"].ToString().Trim() + "%'";
            }
        }
        if (Request["SENDDATE_from"] != null)
        {
            if (Request["SENDDATE_from"].ToString().Trim() != "")
            {
                where += " and SEND_DATE >= '" + Request["SENDDATE_from"].ToString().Trim() + " 00:00:00'";
            }
        }
        if (Request["SENDDATE_to"] != null)
        {
            if (Request["SENDDATE_to"].ToString().Trim() != "")
            {
                where += " and SEND_DATE <= '" + Request["SENDDATE_to"].ToString().Trim() + " 23:59:59'";
            }
        }

        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        DataTable dt = new TOaSwInfoLogic().SelectHandleTable(LogInfo.UserInfo.ID, where, intPageIndex, intPageSize);
        
        string Json = CreateToJson(dt, dt.Rows.Count);
        return Json;
    }
}