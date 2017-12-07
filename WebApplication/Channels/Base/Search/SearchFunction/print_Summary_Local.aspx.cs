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
using i3.BusinessLogic.Channels.Env.Fill.NoiseArea;
using i3.BusinessLogic.Channels.Env.Fill.NoiseFun;
using i3.BusinessLogic.Channels.Env.Fill.NoiseRoad;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;

public partial class Channels_Base_Search_SearchFunction_print_Summary_Local : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strTaskId = "";
        string strSubTaskID = "";
        string strIsLocal = "";
        //01,水质现场监测结果汇总表;
        //02,锅炉（窖）大气污染物监测结果汇总表
        //10,噪声/振动监测结果(社会生活环境噪声)汇总表;
        //11,噪声/振动监测结果(社会生活环境噪声)汇总表(倍频带)
        //12,噪声/振动监测结果(工业企业厂界噪声)汇总表    
        //13,噪声/振动监测结果(声环境质量噪声)汇总表    
        //14,噪声/振动监测结果(建筑施工场界噪声)汇总表    
        //15,噪声/振动监测结果(城市区域环境振动)汇总表  
        //21,交通噪声汇总表  
        //22,区域噪声汇总表
        //23,功能区噪声汇总表  
        //09,降水量监测结果汇总表 
        string strSheetType = "";
        string strYear="";
        string strMonth="";
        string strPointID = "";

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
        if (!string.IsNullOrEmpty(Request.QueryString["strYear"]))
        {
            strYear = Request.QueryString["strYear"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strMonth"]))
        {
            strMonth = Request.QueryString["strMonth"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["strPointID"]))
        {
            strPointID = Request.QueryString["strPointID"].ToString();
        }

        if ( strTaskId.Length == 0)
            return;

        string strExSheetType = "21,22,23";
        if (strSheetType.Length >0 && strExSheetType.Contains(strSheetType))
        {
            GetEnvNoise_YearAndMonth(strTaskId, ref strYear, ref strMonth);
            PrintLocal_EnvNoise(strYear, strMonth, strPointID, strSheetType);
        }
        else
        {
            printLocal(strTaskId, strSubTaskID, strIsLocal, strSheetType);
        }
    }

    private void GetEnvNoise_YearAndMonth(string strTaskId, ref string strYear, ref string strMonth)
    {
        if (strTaskId.Length > 0 && strYear.Length == 0 & strMonth.Length==0)
        {
            string strPlanId = new TMisMonitorTaskLogic().Details(strTaskId).PLAN_ID.Trim();
            //获取计划信息
            TMisContractPlanVo TMisContractPlanVo = new TMisContractPlanLogic().Details(strPlanId);
            strYear = TMisContractPlanVo.PLAN_YEAR;
            strMonth = TMisContractPlanVo.PLAN_MONTH;
        }
    }

    #region 汇总表 通用
    private void printLocal(string strTaskId, string strSubTaskID, string strIsLocal, string strSheetType)
    {
        //动态生成列名
        string strPreColumnName = "";
        //动态生成列名编码
        string strPreColumnName_Src = "";

        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(strTaskId);
        string strMonitorID = "";
        if (!string.IsNullOrEmpty(strSubTaskID) && strSubTaskID.Length > 0)
        {
            strMonitorID = new TMisMonitorSubtaskLogic().Details(strSubTaskID).MONITOR_ID;
        }

        //标题数组
        ArrayList arrTitle = getPrintLocalTitle(strMonitorID, objTask, strSheetType, ref  strPreColumnName, ref  strPreColumnName_Src);
        //结尾数组
        ArrayList arrEnd = getPrintLocalEnd();

        //行转列 前数据表
        DataTable dtResultTotal = new TMisMonitorResultLogic().getTotalItemInfoByTaskID_ForSummary(strTaskId, "", strMonitorID, strIsLocal == "1");
        //特殊表的数据处理
        doWithDataForSheet(strSheetType, ref dtResultTotal);
        GetRemarkData(ref dtResultTotal);
        //行转列 后数据表
        DataTable dtNew = getDatatable("", strPreColumnName, strPreColumnName_Src, dtResultTotal);
        

        //项目列名数组
        ArrayList arrData = getPrintLocalData(dtNew);

        new ExcelHelper().RenderDataTableToExcel(dtNew, "监测数据汇总表.xls", "../../../../TempFile/DataTotal.xls", "监测数据汇总表", 5, true, arrTitle, arrData, arrEnd);
    }

    private void GetRemarkData(ref DataTable dtNew)
    {
        //dtNew.Columns.Add("REMARK4");
        //dtNew.Columns.Add("REMARK5");
        for (int i = 0; i < dtNew.Rows.Count; i++)
        {
            dtNew.Rows[i]["REMARK5"] = (i + 1).ToString();//序号，供选用
        }
    }

    //特殊表的数据处理
    private void doWithDataForSheet(string strSheetType, ref DataTable dt)
    {
        //02,锅炉（窖）大气污染物监测结果汇总表
        //10,噪声/振动监测结果(社会生活环境噪声)汇总表
        //11,噪声/振动监测结果(社会生活环境噪声)汇总表(倍频带)
        //12,噪声/振动监测结果(工业企业厂界噪声)汇总表    
        //13,噪声/振动监测结果(声环境质量噪声)汇总表    
        //14,噪声/振动监测结果(建筑施工场界噪声)汇总表    
        //15,噪声/振动监测结果(城市区域环境振动)汇总表  
        string strSrhSheetType = "02,10,11,12,13,14,15";
        if (strSrhSheetType.Contains(strSheetType))
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strSDate = dt.Rows[i]["SAMPLE_NAME"].ToString().Replace(dt.Rows[i]["POINT_NAME"].ToString(), "");
                if (strSDate.Length > 0)
                {
                    dt.Rows[i]["SAMPLE_ASK_DATE"] = strSDate.Trim();
                }
            }
        }
    }

    //项目列名数组
    private ArrayList getPrintLocalData(DataTable dtNew)
    {
        //列名集合
        ArrayList arrData = new ArrayList();
        for (int i = 0; i < dtNew.Columns.Count; i++)
        {
            arrData.Add(new string[2] { (i+1 ).ToString(), dtNew.Columns[i].ColumnName.ToString() });
        }

        return arrData;
    }

    //标题数组
    //01,水质现场监测结果汇总表;
    //02,锅炉（窖）大气污染物监测结果汇总表
    //10,噪声/振动监测结果(社会生活环境噪声)汇总表;
    //11,噪声/振动监测结果(社会生活环境噪声)汇总表(倍频带)
    //12,噪声/振动监测结果(工业企业厂界噪声)汇总表    
    //13,噪声/振动监测结果(声环境质量噪声)汇总表    
    //14,噪声/振动监测结果(建筑施工场界噪声)汇总表    
    //15,噪声/振动监测结果(城市区域环境振动)汇总表  
    //09,降水量监测结果汇总表  
    private ArrayList getPrintLocalTitle(string strMonitorID, TMisMonitorTaskVo objTask, string strSheetType,
        ref string strPreColumnName, ref string strPreColumnName_Src)
    {
        string strExcelTitle = "现场监测结果汇总表";
        string strExcelCode = "受控编号:ZHJC/JS004";
        if (strSheetType.Length > 0)
        {
            if (strSheetType == "01")
            {
                strExcelTitle = "水质现场监测结果汇总表";
                strExcelCode = "受控编号:ZHJC/JS004";
                //动态生成列名
                strPreColumnName = "编号,监测时间,采样位置,种类";
                //动态生成列名编码
                strPreColumnName_Src = "SAMPLE_CODE,SAMPLE_ASK_DATE,SAMPLE_NAME,MONITOR_TYPE_NAME";
            }
            else if (strSheetType == "02")
            {
                strExcelTitle = "锅炉（窖）大气污染物监测结果汇总表";
                strExcelCode = "受控编号:ZHJC/JS015";
                strPreColumnName = "原始记录编号,单位名称,地点,监测日期";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,POINT_NAME,SAMPLE_ASK_DATE";
            }
            else if (strSheetType == "10")
            {
                strExcelTitle = "噪声/振动监测结果(社会生活环境噪声)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "11")//(倍频带)
            {
                strExcelTitle = "噪声/振动监测结果(社会生活环境噪声)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "12")
            {
                strExcelTitle = "噪声/振动监测结果(工业企业厂界噪声)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "13")
            {
                strExcelTitle = "噪声/振动监测结果(声环境质量噪声)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "14")
            {
                strExcelTitle = "噪声/振动监测结果(建筑施工场界噪声)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "15")
            {
                strExcelTitle = "噪声/振动监测结果(城市区域环境振动)汇总表";
                strExcelCode = "ZHJC/JS021";
                strPreColumnName = "原始记录编号,单位名称,监测日期,监测位置";
                strPreColumnName_Src = "REMARK2,COMPANY_NAME,SAMPLE_ASK_DATE,POINT_NAME";
            }
            else if (strSheetType == "09")
            {
                strExcelTitle = "降水量监测结果汇总表";
                strExcelCode = "ZHJC/JS009";
                strPreColumnName = "编号,采样点位,采样日期（月 日 时 分---月 日 时 分),降水类型";
                strPreColumnName_Src = "REMARK5,POINT_NAME,SAMPLE_ASK_DATE,REMARK4";
            }
            else if (strMonitorID == "000000003")
            {
                strExcelTitle = "底质现场监测结果汇总表";
            }
        }

        //标题数组
        ArrayList arrTitle = new ArrayList();

        arrTitle.Add(new string[3] { "2", strExcelTitle, "true" });
        arrTitle.Add(new string[3] { "3", strExcelCode , "false" });
        arrTitle.Add(new string[3] { "4", "科室：现场监测室" + "                                                                  任务编号：" + objTask.TICKET_NUM, "false" });

        return arrTitle;

    }

    //结尾数组
    private ArrayList getPrintLocalEnd()
    {
        //结尾数组
        ArrayList arrEnd = new ArrayList();
        arrEnd.Add(new string[2] { "2", "抄报：                                   审核：                                    审定："});
        arrEnd.Add(new string[2] { "3", "                                                                                                                                     填报日期：" + System.DateTime.Now.ToShortDateString() });
       
        return arrEnd;
    }
    #endregion

    #region 汇总表 交通噪声\区域噪声\功能区噪声
    //21,交通噪声汇总表  
    //22,区域噪声汇总表
    //23,功能区噪声汇总表  
    private void PrintLocal_EnvNoise(string strYear, string strMonth, string strPointID, string strSheetType)
    {
        DataTable dtSrcFill = new DataTable();
        DataTable dtSrcResult = new DataTable();
        
        if (strSheetType == "21")
        {
            dtSrcFill = new TEnvFillNoiseRoadLogic().SelectFill_ForSummary(strYear, strMonth, strPointID);
            dtSrcResult = new TEnvFillNoiseRoadLogic().SelectResult_ForSummary(strYear, strMonth, strPointID);
        }
        else if (strSheetType == "22")
        {
            dtSrcFill = new TEnvFillNoiseAreaLogic().SelectFill_ForSummary(strYear, strMonth, strPointID);
            dtSrcResult = new TEnvFillNoiseAreaLogic().SelectResult_ForSummary(strYear, strMonth, strPointID);
        }
        else if (strSheetType == "23")
        {
            dtSrcFill = new TEnvFillNoiseFunctionLogic().SelectFill_ForSummary(strYear, strMonth, strPointID);
            dtSrcResult = new TEnvFillNoiseFunctionLogic().SelectResult_ForSummary(strYear, strMonth, strPointID);
        }

        //标题数组
        ArrayList arrTitle = getPrintLocalTitle_EnvNoise(strYear, strMonth, strSheetType);
        //结尾数组
        ArrayList arrEnd = new ArrayList();

        DataTable dtNew=new DataTable();
        getPrintLocalColumn_EnvNoise(strSheetType, ref dtNew);

        getPrintLocalData_EnvNoise(dtSrcFill, dtSrcResult, strSheetType, ref dtNew);

        //项目列名数组
        ArrayList arrData = getPrintLocalData(dtNew);

        new ExcelHelper().RenderDataTableToExcel(dtNew, "监测数据汇总表.xls", "../../../../TempFile/DataTotal-EnvNoise.xls", "监测数据汇总表", 3, true, arrTitle, arrData, arrEnd);
    }

    //数据DataTable构建列
    //21,交通噪声汇总表  
    //22,区域噪声汇总表
    //23,功能区噪声汇总表 
    private void getPrintLocalColumn_EnvNoise( string strSheetType, ref DataTable dtNew)
    {
        if (strSheetType == "21")//交通噪声汇总表
        {
            dtNew.Columns.Add("测点编号", System.Type.GetType("System.String"));
            dtNew.Columns.Add("Leq", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L10", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L50", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L90", System.Type.GetType("System.String"));
            dtNew.Columns.Add("SD", System.Type.GetType("System.String"));
            dtNew.Columns.Add("测量时间", System.Type.GetType("System.String"));
            dtNew.Columns.Add("DATE", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TIME", System.Type.GetType("System.String"));
            dtNew.Columns.Add("车流量（辆/h）", System.Type.GetType("System.String"));
        }
        if (strSheetType == "22")//区域噪声汇总表
        {
            dtNew.Columns.Add("测点编号", System.Type.GetType("System.String"));
            dtNew.Columns.Add("Leq", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L10", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L50", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L90", System.Type.GetType("System.String"));
            dtNew.Columns.Add("SD", System.Type.GetType("System.String"));
            dtNew.Columns.Add("测量时间", System.Type.GetType("System.String"));
            dtNew.Columns.Add("DATE", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TIME", System.Type.GetType("System.String"));
            dtNew.Columns.Add("声源代码", System.Type.GetType("System.String"));
        }
        if (strSheetType == "23")//功能区噪声汇总表
        {
            dtNew.Columns.Add("序号", System.Type.GetType("System.String"));
            dtNew.Columns.Add("Leq", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L10", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L50", System.Type.GetType("System.String"));
            dtNew.Columns.Add("L90", System.Type.GetType("System.String"));
            dtNew.Columns.Add("SD", System.Type.GetType("System.String"));
            dtNew.Columns.Add("测量时间", System.Type.GetType("System.String"));
            dtNew.Columns.Add("DATE", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TIME", System.Type.GetType("System.String"));
            dtNew.Columns.Add("车流量", System.Type.GetType("System.String"));
        }
    }

    //数据DataTable构建数据
    //21,交通噪声汇总表  
    //22,区域噪声汇总表
    //23,功能区噪声汇总表  
    private void getPrintLocalData_EnvNoise(DataTable dtSrcFill, DataTable dtSrcResult, string strSheetType, ref DataTable dtNew)
    {
        if (strSheetType == "21")//交通噪声汇总表
        {
            #region 交通噪声汇总表
            for (int i = 0; i < dtSrcFill.Rows.Count; i++)
            {
                DataRow dr = dtNew.NewRow();

                DataRow drSrcF = dtSrcFill.Rows[i];

                dr["测点编号"] = drSrcF["测点编号"].ToString();
                dr["测量时间"] = drSrcF["测量时间"].ToString();
                dr["DATE"] = drSrcF["DATE"].ToString();
                dr["TIME"] = drSrcF["TIME"].ToString();

                DataRow[] drSrcReS = dtSrcResult.Select("FILL_ID='" + drSrcF["ID"].ToString() + "'");
                for (int j = 0; j < drSrcReS.Length; j++)
                {
                    DataRow drSrcRe = drSrcReS[j];
                    if (drSrcRe["ITEM_NAME"].ToString().ToLower()=="leq")
                    {
                        dr["Leq"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq10")
                    {
                        dr["L10"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq50")
                    {
                        dr["L50"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq90")
                    {
                        dr["L90"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "sd")
                    {
                        dr["SD"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower().Contains("车流量"))
                    {
                        dr["车流量（辆/h）"] = drSrcRe["ITEM_VALUE"].ToString();
                    }
                }

                dtNew.Rows.Add(dr);
            }
            #endregion
        }
        if (strSheetType == "22")//区域噪声汇总表
        {
            #region 区域噪声汇总表
            for (int i = 0; i < dtSrcFill.Rows.Count; i++)
            {
                DataRow dr = dtNew.NewRow();

                DataRow drSrcF = dtSrcFill.Rows[i];

                dr["测点编号"] = drSrcF["测点编号"].ToString();
                dr["测量时间"] = drSrcF["测量时间"].ToString();
                dr["DATE"] = drSrcF["DATE"].ToString();
                dr["TIME"] = drSrcF["TIME"].ToString();
                dr["声源代码"] = drSrcF["声源代码"].ToString();

                DataRow[] drSrcReS = dtSrcResult.Select("FILL_ID='" + drSrcF["ID"].ToString() + "'");
                for (int j = 0; j < drSrcReS.Length; j++)
                {
                    DataRow drSrcRe = drSrcReS[j];
                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq")
                    {
                        dr["Leq"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq10")
                    {
                        dr["L10"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq50")
                    {
                        dr["L50"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq90")
                    {
                        dr["L90"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower()=="sd")
                    {
                        dr["SD"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    
                }

                dtNew.Rows.Add(dr);
            }
            #endregion
        }
        if (strSheetType == "23")//功能区噪声汇总表
        {
            #region 功能区噪声汇总表
            for (int i = 0; i < dtSrcFill.Rows.Count; i++)
            {
                DataRow dr = dtNew.NewRow();

                DataRow drSrcF = dtSrcFill.Rows[i];

                //dr["序号"] = (i+1).ToString();
                dr["序号"] = drSrcF["POINT_NAME"].ToString();
                dr["测量时间"] = drSrcF["测量时间"].ToString();
                dr["DATE"] = drSrcF["DATE"].ToString();
                dr["TIME"] = drSrcF["TIME"].ToString();

                DataRow[] drSrcReS = dtSrcResult.Select("FILL_ID='" + drSrcF["ID"].ToString() + "'");
                for (int j = 0; j < drSrcReS.Length; j++)
                {
                    DataRow drSrcRe = drSrcReS[j];
                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq")
                    {
                        dr["Leq"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq10")
                    {
                        dr["L10"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq50")
                    {
                        dr["L50"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "leq90")
                    {
                        dr["L90"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower() == "sd")
                    {
                        dr["SD"] = drSrcRe["ITEM_VALUE"].ToString();
                    }

                    if (drSrcRe["ITEM_NAME"].ToString().ToLower().Contains("车流量"))
                    {
                        dr["车流量"] = drSrcRe["ITEM_VALUE"].ToString();
                    }
                }

                dtNew.Rows.Add(dr);
            }
            #endregion
        }
    }

    //标题数组
    //21,交通噪声汇总表  
    //22,区域噪声汇总表
    //23,功能区噪声汇总表  
    private ArrayList getPrintLocalTitle_EnvNoise(string strYear, string strMonth, string strSheetType)
    {
        string strExcelTitle = "现场监测结果汇总表";
        string strExcelCode = "受控编号:ZHJC/JS004";
       
        if (strSheetType == "21")
        {
            strExcelTitle = strYear + "年交通噪声汇总表";
            strExcelCode = "受控编号:ZHJC/JS018                                                                      填报日期："+System.DateTime.Now.ToShortDateString();
        }
        else if (strSheetType == "22")
        {
            strExcelTitle = strYear + "年声环境质量噪声汇总表";
            strExcelCode = "受控编号:ZHJC/JS017                                                                      填报日期：" + System.DateTime.Now.ToShortDateString();
        }
        else if (strSheetType == "23")
        {
            strExcelTitle = strYear + "年" + GetQuarter(strMonth) + "季度功能区噪声监测结果汇总表";
            strExcelCode = "受控编号:ZHJC/JS019                                                                      填报日期：" + System.DateTime.Now.ToShortDateString();
        }

        //标题数组
        ArrayList arrTitle = new ArrayList();

        arrTitle.Add(new string[3] { "1", strExcelTitle, "true" });
        arrTitle.Add(new string[3] { "2", strExcelCode, "false" });

        return arrTitle;

    }

    private string GetQuarter(string strMonth)
    {
        string strQ = "1";
        string strM = strMonth.StartsWith("0") ? strMonth.Replace("0", "") : strMonth;
        if (strM == "1" || strM == "2" || strM == "3")
        {
            strQ = "1";
        }
        if (strM == "4" || strM == "5" || strM == "6")
        {
            strQ = "2";
        }
        if (strM == "7" || strM == "8" || strM == "9")
        {
            strQ = "3";
        }
        if (strM == "10" || strM == "11" || strM == "12")
        {
            strQ = "4";
        }
        return strQ;
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