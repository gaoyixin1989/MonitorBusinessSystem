using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using System.IO;
using System.Text;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：预监测任务查询
/// 创建时间：2013-5-6
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_PlanTaskSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetPointMonitorInfor")
            {
                Response.Write(GetPointMonitorInfor());
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetPendingPlanList")
            {
                Response.Write(GetPendingPlanList());
                Response.End();
            }
            //构造委托年份
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetContratYearHistory")
            {
                Response.Write(GetContratYearHistory());
                Response.End();
            }
            //获取字典项
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetDict")
            {
                Response.Write(GetDict(Request.QueryString["type"]));
                Response.End();
            }
            //获取监测计划监测类型负责人信息
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "GetContractDutyUser")
            {
                Response.Write(GetContractDutyUser());
                Response.End();
            }
        }
    }

    /// <summary>
    /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2012-12-21
    /// </summary>
    /// <returns></returns>
    private string GetPointMonitorInfor()
    {
        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = Request.QueryString["task_id"].ToString();
        dt = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, Request.QueryString["strPlanId"].ToString());
        return PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
    }

    /// <summary>
    /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的）2013-04-01
    /// </summary>
    /// <returns></returns>
    private string GetPendingPlanList()
    {
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        TMisContractVo objItemContract = new TMisContractVo();

        //分页参数
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);
        //查询条件
        if (!string.IsNullOrEmpty(Request.QueryString["strQulickly"]))
        {
            objItemContract.ISQUICKLY = Request.QueryString["strQulickly"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strProjectName"]))
        {
            objItemContract.PROJECT_NAME = Request.QueryString["strProjectName"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strContractCode"]))
        {
            objItemContract.CONTRACT_CODE = Request.QueryString["strContractCode"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strContratYear"]))
        {
            objItemContract.CONTRACT_YEAR = Request.QueryString["strContratYear"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strContractType"]))
        {
            objItemContract.CONTRACT_TYPE = Request.QueryString["strContractType"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strCompanyNameFrim"]))
        {
            objItemContract.TESTED_COMPANY_ID = Request.QueryString["strCompanyNameFrim"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strAreaIdFrim"]))
        {
            objItemContract.REMARK4 = Request.QueryString["strAreaIdFrim"].ToString();
        }
        //状态
        string strQcStatus = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["strQcStatus"]))
        {
            strQcStatus = Request.QueryString["strQcStatus"].ToString();
        }
        //任务单号
        string strTaskCode = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["strTaskCode"]))
        {
            strTaskCode = Request.QueryString["strTaskCode"].ToString();
        }
        objItemContract.CONTRACT_STATUS = "9";
        string strDate = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["strDate"]))
        {
            strDate = Request.QueryString["strDate"].ToString();
        }
        if (!String.IsNullOrEmpty(strDate))
        {
            string[] dates = strDate.Split('-');
            objItems.PLAN_YEAR = dates[0].ToString();
            objItems.PLAN_MONTH = dates[1].ToString();
            objItems.PLAN_DAY = dates[2].ToString();
        }
        if (!String.IsNullOrEmpty(Request.QueryString["strType"]))
        {
            bool flag = Convert.ToBoolean(Request.QueryString["strType"]);

            dt = new TMisContractPlanLogic().SelectByTableForPlanTask(objItems, objItemContract, strQcStatus, flag, strTaskCode, intPageIdx, intPagesize);
            int CountNum = new TMisContractPlanLogic().SelectCountForPlanTask(objItems, objItemContract, strQcStatus, flag, strTaskCode);
            return LigerGridDataToJson(dt, CountNum);
        }
        return "";
    }

    /// <summary>
    /// 根据系统时间，自动生成委托年度（当前年度之前5年）历史数据使用
    /// </summary>
    /// <returns></returns>
    public string GetContratYearHistory()
    {
        string result = "";
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("YEAR", typeof(string)));
        for (int i = 0; i < 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = i.ToString();
            if (i == 0)
            {
                dr["YEAR"] = DateTime.Now.ToString("yyyy");
            }
            else
            {
                dr["YEAR"] = DateTime.Now.AddYears(-i).ToString("yyyy");
            }
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        result = i3.View.PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string GetDict(string strDictType)
    {
        if (!string.IsNullOrEmpty(strDictType))
        {
            return i3.View.PageBase.getDictJsonString(strDictType);
        }
        return "";
    }

    /// <summary>
    /// 获取监测计划监测类型负责人信息
    /// </summary>
    /// <returns></returns>
    private string GetContractDutyUser()
    {
        DataTable dt = new DataTable();
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
        {
            objItems.CONTRACT_ID = Request.QueryString["task_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strMonitorId"]))
        {
            objItems.MONITOR_TYPE_ID = Request.QueryString["strMonitorId"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strPlanId"]))
        {
            objItems.CONTRACT_PLAN_ID = Request.QueryString["strPlanId"].ToString();
        }
        dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
        return PageBase.LigerGridDataToJson(dt, dt.Rows.Count);
    }
}