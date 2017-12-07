using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Collections;

using i3.View;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

public partial class Channels_Base_Search_SearchFunction_print_Summary : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strTaskId = "";
        string strSubTaskID = "";
        string strIsLocal = "";
        //01,水质样品监测结果汇总表;
        //02,环境空气监测结果汇总表
        //03,大气污染源监测结果汇总表;
        //04,固体样品监测结果汇总表  
        string strSheetType = "";

        if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
        {
            strTaskId = Request.QueryString["task_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["subtask_id"]))
        {
            strSubTaskID = Request.QueryString["subtask_id"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["isLocal"]))
        {
            strIsLocal = Request.QueryString["isLocal"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["sheetType"]))
        {
            strSheetType = Request.QueryString["sheetType"].ToString();
        }

        if (strTaskId.Length == 0)
            return;

        printSheet(strTaskId, strSubTaskID, strIsLocal, strSheetType);
    }

    #region 汇总表 通用
    private void printSheet(string strTaskId, string strSubTaskID, string strIsLocal, string strSheetType)
    {
        //动态生成列名
        string strPreColumnName = "";
        //动态生成列名编码
        string strPreColumnName_Src = "";

        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strTaskId);
        string strMonitorID = "";
        if (strSubTaskID.Length > 0)
        {
            strMonitorID = new TMisMonitorSubtaskLogic().Details(strSubTaskID).MONITOR_ID;
        }

        //标题数组
        ArrayList arrTitle = getPrintTitle(strMonitorID, objTask, strSheetType, ref  strPreColumnName, ref  strPreColumnName_Src);
        //结尾数组
        ArrayList arrEnd = getPrintEnd();

        //行转列 前数据表
        DataTable dtResultTotal = new TMisMonitorResultLogic().getTotalItemInfoByTaskID_ForSummary(strTaskId, "", strMonitorID, strIsLocal == "1");
        //行转列 后数据表
        DataTable dtNew = getDatatable("", strPreColumnName, strPreColumnName_Src, dtResultTotal);

        //项目列名数组
        ArrayList arrData = getPrintData(dtNew);

        new ExcelHelper().RenderDataTableToExcel(dtNew, "监测数据汇总表.xls", "../../../../TempFile/DataTotal.xls", "监测数据汇总表", 5, true, arrTitle, arrData, arrEnd);
    }

    //项目列名数组
    private ArrayList getPrintData(DataTable dtNew)
    {
        //列名集合
        ArrayList arrData = new ArrayList();
        for (int i = 0; i < dtNew.Columns.Count; i++)
        {
            arrData.Add(new string[2] { (i + 1).ToString(), dtNew.Columns[i].ColumnName.ToString() });
        }

        return arrData;
    }

    //标题数组
    //01,水质样品监测结果汇总表;
    //02,环境空气监测结果汇总表
    //03,大气污染源监测结果汇总表;
    //04,固体样品监测结果汇总表 
    private ArrayList getPrintTitle(string strMonitorID, TMisMonitorTaskVo objTask, string strSheetType,
        ref string strPreColumnName, ref string strPreColumnName_Src)
    {
        string strExcelTitle = "样品监测结果汇总表";
        string strExcelCode = "ZHJC/JS001";
        string strExcelUnit = "mg/L";

        //动态生成列名
        strPreColumnName = "编号,地点,时间,样品种类";
        //动态生成列名编码
        strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_ASK_DATE,MONITOR_TYPE_NAME";

        if (strSheetType.Length > 0)
        {
            if (strSheetType == "01")
            {
                strExcelTitle = "水质样品监测结果汇总表";
                strExcelCode = "ZHJC/JS001";
                strExcelUnit = "mg/L";

                strPreColumnName = "编号,地点,时间,样品种类";
                strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_ASK_DATE,MONITOR_TYPE_NAME";
            }
            else if (strSheetType == "02")
            {
                strExcelTitle = "环境空气监测结果汇总表";
                strExcelCode = "ZHJC/JS011";
                strExcelUnit = "mg/m3";

                strPreColumnName = "编号,地点,时间,样品种类";
                strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_ASK_DATE,MONITOR_TYPE_NAME";
            }
            else if (strSheetType == "03")
            {
                strExcelTitle = "大气污染源监测结果汇总表";
                strExcelCode = "ZHJC/JS011";
                strExcelUnit = "mg/m3";

                strPreColumnName = "编号,地点,时间,样品种类";
                strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_ASK_DATE,MONITOR_TYPE_NAME";
            }
            else if (strSheetType == "04")
            {
                strExcelTitle = "固体样品监测结果汇总表";
                strExcelCode = "ZHJC/JS001";
                strExcelUnit = "mg/Kg";

                strPreColumnName = "编号,地点,时间,样品种类";
                strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_NAME,SAMPLE_ASK_DATE,MONITOR_TYPE_NAME";
            }
        }

        //标题数组
        ArrayList arrTitle = new ArrayList();

        arrTitle.Add(new string[3] { "2", strExcelTitle, "true" });
        arrTitle.Add(new string[3] { "3", strExcelCode + "                               单位：" + strExcelUnit + "                                 任务编号：" + objTask.TICKET_NUM, "false" });

        return arrTitle;
    }

    //结尾数组
    private ArrayList getPrintEnd()
    {
        //结尾数组
        ArrayList arrEnd = new ArrayList();
        arrEnd.Add(new string[2] { "2", "监测负责人：                                 审核：                                 审定：                                 " });
        arrEnd.Add(new string[2] { "3", "填报科室：中心实验室" });
        arrEnd.Add(new string[2] { "4", DateTime.Now.ToString("yyyy-MM-dd") });

        return arrEnd;
    }
    #endregion

    #region 结果汇总表 行转列
    //获取数据DataTable
    private DataTable getDatatable(string strMonitorType, string strPreColumnName, string strPreColumnName_Src, DataTable dtResult)
    {
        //得到指定监测类别下的所有项目
        string strItemIDs = "";
        string strItemName = "";
        GetItems_UnderMonitor(strMonitorType, dtResult, ref  strItemIDs, ref  strItemName);

        DataTable dt_Result_Return = new DataTable();
        //设置Datatable的列
        AddDatatable_column(strPreColumnName, strItemName, ref  dt_Result_Return);
        //填充Datatable的值
        InsertDataTable_Value(strMonitorType, dtResult, strPreColumnName, strPreColumnName_Src, strItemIDs, ref  dt_Result_Return);

        return dt_Result_Return;
    }

    //得到指定监测类别下的所有项目
    private void GetItems_UnderMonitor(string strMonitorType, DataTable dtResult, ref string strItemIDs, ref string strItemName)
    {
        DataRow[] drs = dtResult.Select();
        if (!string.IsNullOrEmpty(strMonitorType))
        {
            drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
        }

        for (int i = 0; i < drs.Length; i++)
        {
            if (!strItemIDs.Contains(drs[i]["ITEM_ID"].ToString()))
            {
                strItemIDs += (strItemIDs.Length > 0 ? "," : "") + drs[i]["ITEM_ID"].ToString();
                strItemName += (strItemIDs.Length > 0 ? "," : "") + drs[i]["ITEM_NAME"].ToString();
                strItemName += drs[i]["ITEM_UNIT"].ToString().Length > 0 ? ("（" + drs[i]["ITEM_UNIT"].ToString() + "）") : "";
            }
        }
    }

    //设置Datatable的列
    private void AddDatatable_column(string strPreColumnName, string strItemNames, ref DataTable dt)
    {
        if (strPreColumnName.Length > 0)
        {
            string[] arrPreColumnName = strPreColumnName.Split(',');
            for (int i = 0; i < arrPreColumnName.Length; i++)
            {
                dt.Columns.Add(arrPreColumnName[i], System.Type.GetType("System.String"));
            }
        }

        if (strItemNames.Length > 0)
        {
            string[] arrItemNames = strItemNames.Split(',');
            for (int i = 0; i < arrItemNames.Length; i++)
            {
                if (arrItemNames[i].Length > 0)
                    dt.Columns.Add(arrItemNames[i], System.Type.GetType("System.String"));
            }
        }
    }

    //填充Datatable的值
    private void InsertDataTable_Value(string strMonitorType, DataTable dtResult, string strPreColumnName, string strPreColumnName_Src, string strItemIDs, ref DataTable dt_Result_Return)
    {
        //过滤出指定监测类别的Datatable
        DataTable dtTmp = FilterDataTable_byMonitorType(strMonitorType, dtResult);

        string strTempSampleID = "";
        for (int i = 0; i < dtTmp.Rows.Count; i++)
        {
            if (strTempSampleID == dtTmp.Rows[i]["SAMPLE_ID"].ToString())
                continue;
            strTempSampleID = dtTmp.Rows[i]["SAMPLE_ID"].ToString();

            DataRow dr = dt_Result_Return.NewRow();
            DataRow drSrc = dtTmp.Rows[i];

            infullPreColumn_Value(drSrc, strPreColumnName, strPreColumnName_Src, ref dr);
            infullResultColumn_Value(dtTmp, strPreColumnName, strTempSampleID, strItemIDs, ref dr);

            dt_Result_Return.Rows.Add(dr);
        }
    }

    //给前几个固定列赋值，比如“监测日期,监测点位,样品编号,样品描述”
    private void infullPreColumn_Value(DataRow drSrc, string strPreColumnName, string strPreColumnName_Src, ref DataRow dr)
    {
        string[] arrPre = strPreColumnName.Split(',');
        string[] arrPreSrc = strPreColumnName_Src.Split(',');
        for (int i = 0; i < arrPre.Length; i++)
        {
            dr[arrPre[i]] = drSrc[arrPreSrc[i]].ToString().Replace("0:00:00", "");
        }
    }

    //给结果列赋值
    private void infullResultColumn_Value(DataTable dtTmp, string strPreColumnName, string strTempSampleID, string strItemIDs, ref DataRow dr)
    {
        string[] arrPre = strPreColumnName.Split(',');
        string[] arrItemIds = strItemIDs.Split(',');
        int iPreColumnCount = arrPre.Length;

        DataRow[] drSrc = dtTmp.Select("SAMPLE_ID='" + strTempSampleID + "'");

        for (int i = 0; i < drSrc.Length; i++)
        {
            string strTmpItemId = drSrc[i]["ITEM_ID"].ToString();
            //从column中找到对应该item的列，在该列该行填值
            for (int j = 0; j < arrItemIds.Length; j++)
            {
                if (arrItemIds[j] == strTmpItemId)
                {
                    int iColumnIdx = iPreColumnCount + j;//加上前面的固定列
                    dr[iColumnIdx] = drSrc[i]["ITEM_RESULT"].ToString();
                }
            }
        }
    }

    //过滤出指定监测类别的Datatable
    private DataTable FilterDataTable_byMonitorType(string strMonitorType, DataTable dtResult)
    {
        DataRow[] drs = dtResult.Select();
        if (!string.IsNullOrEmpty(strMonitorType))
        {
            drs = dtResult.Select("MONITOR_ID='" + strMonitorType + "'");
        }
        DataTable dtTmp = new DataTable();

        for (int i = 0; i < dtResult.Columns.Count; i++)
        {
            dtTmp.Columns.Add(dtResult.Columns[i].ColumnName, System.Type.GetType("System.String"));
        }

        for (int i = 0; i < drs.Length; i++)
        {
            DataRow drtmp = dtTmp.NewRow();
            for (int j = 0; j < dtResult.Columns.Count; j++)
            {
                drtmp[j] = drs[i][j].ToString();
            }

            dtTmp.Rows.Add(drtmp);
        }

        return dtTmp;
    }
    #endregion
}