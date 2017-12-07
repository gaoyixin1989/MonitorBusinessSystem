using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Monitor;
using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor;
using i3.View;
using i3.ValueObject;
using i3.BusinessLogic.Sys.Resource;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.RPT;

/// <summary>
/// 功能： "数据汇总表--现场室"功能
/// 创建人：潘德军
/// 创建时间： 2013.8.12
/// </summary>
public partial class Channels_Base_Search_SearchZZ_SummarySheet_Local : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectTask")
        {
            selectTask();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "selectsubTask")
        {
            selectsubTask();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "GetSheetType")
        {
            GetSheetType();
        }
    }

    /// <summary>
    /// 获取监测任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //任务单号
        string strTICKET_NUM = !string.IsNullOrEmpty(Request.QueryString["SrhTICKET_NUM"]) ? Request.QueryString["SrhTICKET_NUM"].ToString() : "";

        //构造查询对象
        TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
        TMisMonitorTaskLogic objTaskLogic = new TMisMonitorTaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorTaskVo.ID_FIELD;

        objTask.SORT_FIELD = "ID";
        objTask.SORT_TYPE = "desc";
        objTask.TICKET_NUM = strTICKET_NUM;

        string strJson = "";
        int intTotalCount = objTaskLogic.GetSelectResultCount_ForSummary(objTask, true);//总计的数据条数
        DataTable dt = objTaskLogic.SelectByTable_ForSummary(objTask, true, intPageIdx, intPagesize);

        strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取监测子任务列表信息
    /// </summary>
    /// <returns></returns>
    protected void selectsubTask()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //构造查询对象
        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskVo();
        TMisMonitorSubtaskLogic objSubTaskLogic = new TMisMonitorSubtaskLogic();
        if (strSortname == null || strSortname.Length == 0)
            strSortname = TMisMonitorSubtaskVo.ID_FIELD;

        objSubTask.SORT_FIELD = strSortname;
        objSubTask.SORT_TYPE = strSortorder;
        objSubTask.TASK_ID = !string.IsNullOrEmpty(Request.QueryString["task_id"]) ? Request.QueryString["task_id"].ToString() : "";

        string strJson = "";
        DataTable dt = objSubTaskLogic.SelectByTable_ForSummary(objSubTask, true, 0, 0);
        DataTable dtRe = doWithSubtaskData(dt);

        strJson = CreateToJson(dtRe, dtRe.Rows.Count);

        Response.Write(strJson);
        Response.End();
    }

    private DataTable doWithSubtaskData(DataTable dt)
    {
        DataTable dtRe = new DataTable();
        string strMonitorIDs = "";
        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dtRe.Columns.Add(dt.Columns[j].ColumnName);
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strMonitorIDs.Contains(dt.Rows[i]["MONITOR_ID"].ToString()))
            {
                strMonitorIDs += "," + dt.Rows[i]["MONITOR_ID"].ToString();
                DataRow dr = dtRe.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[dt.Columns[j].ColumnName] = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                }
                dtRe.Rows.Add(dr);
            }
        }
        for (int i = 0; i < dtRe.Rows.Count; i++)
        {
            if (dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString().Length > 0)
            {
                string strUsername = new TSysUserLogic().Details(dtRe.Rows[i]["SAMPLING_MANAGER_ID"].ToString()).REAL_NAME;
                if (!dtRe.Rows[i]["SAMPLING_MAN"].ToString().Contains(strUsername))
                    dtRe.Rows[i]["SAMPLING_MAN"] = strUsername + (dtRe.Rows[i]["SAMPLING_MAN"].ToString().Length > 0 ? "，" : "") + dtRe.Rows[i]["SAMPLING_MAN"].ToString();
            }
        }
        dtRe.AcceptChanges();

        return dtRe;
    }

    //01,水质现场监测结果汇总表;
    //02,锅炉（窖）大气污染物监测结果汇总表
    //10,噪声/振动监测结果(社会生活环境噪声)汇总表;
    //11,噪声/振动监测结果(社会生活环境噪声)汇总表(倍频带)
    //12,噪声/振动监测结果(工业企业厂界噪声)汇总表    
    //13,噪声/振动监测结果(声环境质量噪声)汇总表    
    //14,噪声/振动监测结果(建筑施工场界噪声)汇总表    
    //15,噪声/振动监测结果(城市区域环境振动)汇总表  
    //09,降水量监测结果汇总表  
    //21,交通噪声汇总表  
    //22,区域噪声汇总表
    //23,功能区噪声汇总表  
    protected void GetSheetType()
    {
        string strSubTaskID = !string.IsNullOrEmpty(Request.QueryString["subTaskID"]) ? Request.QueryString["subTaskID"].ToString() : "";
        if (strSubTaskID.Length == 0)
            return;

        TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(strSubTaskID);
        string strMonitorID = objSubTask.MONITOR_ID;

        DataTable dtRe = new DataTable();
        dtRe.Columns.Add("id");
        dtRe.Columns.Add("text");

        string strSheetType01 = "000000001,EnvDrinkingSource,EnvReservoir,EnvRiver,EnvMudRiver";//水和废水,饮用水源地,底泥,湖库,河流
        string strSheetType02 = "000000002,EnvAir";//气和废气,环境空气
        string strSheetType1X = "000000004";//噪声
        string strSheetType15 = "000000006";//振动
        string strSheetType09 = "EnvRain";//降水
        string strSheetType21 = "EnvRoadNoise";//交通噪声
        string strSheetType22 = "AreaNoise";//区域噪声
        string strSheetType23 = "FunctionNoise";//功能区噪声

        if (strMonitorID.Length > 0 && strSheetType01.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "01";
            dr["text"] = "水质现场监测结果汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType02.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "02";
            dr["text"] = "锅炉（窖）大气污染物监测结果汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType1X.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "10";
            dr["text"] = "噪声/振动监测结果(社会生活环境噪声)汇总表";
            dtRe.Rows.Add(dr);
            dr = dtRe.NewRow();
            dr["id"] = "11";
            dr["text"] = "噪声/振动监测结果(社会生活环境噪声)汇总表(倍频带)";
            dtRe.Rows.Add(dr);
            dr = dtRe.NewRow();
            dr["id"] = "12";
            dr["text"] = "噪声/振动监测结果(工业企业厂界噪声)汇总表";
            dtRe.Rows.Add(dr);
            dr = dtRe.NewRow();
            dr["id"] = "13";
            dr["text"] = "噪声/振动监测结果(声环境质量噪声)汇总表";
            dtRe.Rows.Add(dr);
            dr = dtRe.NewRow();
            dr["id"] = "14";
            dr["text"] = "噪声/振动监测结果(建筑施工场界噪声)汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType15.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "15";
            dr["text"] = "噪声/振动监测结果(城市区域环境振动)汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType09.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "09";
            dr["text"] = "降水量监测结果汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType21.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "21";
            dr["text"] = "交通噪声汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType22.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "22";
            dr["text"] = "区域噪声汇总表";
            dtRe.Rows.Add(dr);
        }
        if (strMonitorID.Length > 0 && strSheetType23.Contains(strMonitorID))
        {
            DataRow dr = dtRe.NewRow();
            dr["id"] = "23";
            dr["text"] = "功能区噪声汇总表";
            dtRe.Rows.Add(dr);
        }

        dtRe.AcceptChanges();

        string strJson = DataTableToJson(dtRe);

        Response.Write(strJson);
        Response.End();
    }
}