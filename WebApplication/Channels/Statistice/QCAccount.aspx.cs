using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using System.Text;

using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.Item;
using Microsoft.Reporting.WebForms;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Sys.Resource;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
/// 功能描述：质控报表（总表）
/// 创建日期：2013-4-22
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Statistice_QCAccount : PageBase
{
    private string strQC_BEGIN_DATE = "", strQC_END_DATE = "", strMONITOR_ID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        //获取企业信息
        if (Request["type"] != null && Request["type"].ToString() == "getQCAccount")
        {
            if (!String.IsNullOrEmpty(Request.Params["QC_BEGIN_DATE"]))
            {
                strQC_BEGIN_DATE = Request.Params["QC_BEGIN_DATE"].Trim();
            }
            if (!String.IsNullOrEmpty(Request.Params["QC_END_DATE"]))
            {
                strQC_END_DATE = Request.Params["QC_END_DATE"];
            }
            if (!String.IsNullOrEmpty(Request.Params["MONITOR_ID"]))
            {
                strMONITOR_ID = Request.Params["MONITOR_ID"];
            }

            strResult = getQCAccountAll();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 显示表内容
    /// </summary>
    /// <returns></returns>
    private string getQCAccountAll()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];

        string strMonitorID = strMONITOR_ID;
        string strStartTime = strQC_BEGIN_DATE, strEndTime = strQC_END_DATE;

        DataTable dtResult = new TMisMonitorResultLogic().getResultWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//原始表
        DataTable dtEmptyOut = new TMisMonitorResultLogic().getQCEmptyOutWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//现场空白
        DataTable dtEmptyBat = new TMisMonitorResultLogic().getQCEmptyBatWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//实验室空白
        DataTable dtTwin = new TMisMonitorResultLogic().getQCTwinWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//平行样
        DataTable dtAdd = new TMisMonitorResultLogic().getQCAddWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//加标
        DataTable dtSt = new TMisMonitorResultLogic().getQCStWitnTimeAndType(strMonitorID, strStartTime, strEndTime);//标样

        DataTable dtAll = DoneWithTableAll(dtResult, dtEmptyOut, dtEmptyBat, dtTwin, dtAdd, dtSt);// 质控总表信息统计
        DataTable dtQcEmptyOut = DoneWithTableEmptyOut(dtEmptyOut);//现场空白
        DataTable dtQcEmptyIn = DoneWithTableEmptyIn(dtEmptyBat);//实验室空白
        DataTable dtQcTwinOut =DoneWithTableTwin(dtTwin,"3");//现场平行
        DataTable dtQcTwinIn = DoneWithTableTwin(dtTwin, "7");//实验室明码平行
        DataTable dtQcTwinPwd = DoneWithTableTwin(dtTwin, "4");//实验室密码平行
        DataTable dtQcAdd = DoneWithTableAdd(dtAdd);//加标
        DataTable dtQcSt = DoneWithTableSt(dtSt);//标样

        for (int i = dtAll.Rows.Count - 1; i >= 0; i--)
        {
            int iLengthSum = 0;
            for (int j = 3; j < dtAll.Columns.Count; j++)
            {
                iLengthSum += dtAll.Rows[i][j].ToString().Trim().Length;
            }
            if (iLengthSum == 0)
            {
                dtAll.Rows.RemoveAt(i);
            }
        }
        for (int i = 0; i < dtAll.Rows.Count; i++)
        {
            dtAll.Rows[i][0] = (i + 1).ToString();
        }

        string strJsonAll = CreateToJson(dtAll, 0);// 质控总表信息统计
        string strJsonEmptyOut = CreateToJson(dtQcEmptyOut, 0);//现场空白
        string strJsonEmptyIn = CreateToJson(dtQcEmptyIn, 0);//实验室空白
        string strJsonTwinOut = CreateToJson(dtQcTwinOut, 0);//现场平行
        string strJsonTwinIn = CreateToJson(dtQcTwinIn, 0);//实验室明码平行
        string strJsonTwinPwd = CreateToJson(dtQcTwinPwd, 0);//实验室密码平行
        string strJsonAdd = CreateToJson(dtQcAdd, 0);//加标
        string strJsonSt = CreateToJson(dtQcSt, 0);//加标

        string strJson = strJsonAll + "|" + strJsonEmptyOut + "|" + strJsonEmptyIn + "|" + strJsonTwinOut + "|" + strJsonTwinIn + "|" + strJsonTwinPwd + "|" + strJsonAdd + "|" + strJsonSt;
        return strJson;
    }

    #region 界面，grid
    /// <summary>
    /// 质控总表信息统计
    /// </summary>
    private DataTable DoneWithTableAll(DataTable dtResult, DataTable dtEmptyOut, DataTable dtEmptyBat, DataTable dtTwin, DataTable dtAdd, DataTable dtSt)
    {
        DataTable dt = new DataTable();
        #region DataTable列
        dt.Columns.Add("NUM", System.Type.GetType("System.String"));//序号
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("ALL_COUNT", System.Type.GetType("System.String"));//样品总数

        dt.Columns.Add("OUT_EMPTY_COUNT", System.Type.GetType("System.String"));//现场空白个数
        dt.Columns.Add("OUT_EMPTY_OK_PER", System.Type.GetType("System.String"));//合格率%

        dt.Columns.Add("IN_EMPTY_COUNT", System.Type.GetType("System.String"));//实验空白个数
        dt.Columns.Add("IN_EMPTY_RANGE", System.Type.GetType("System.String"));//相对偏差范围%
        dt.Columns.Add("IN_EMPTY_OK_PER", System.Type.GetType("System.String"));//合格率%

        dt.Columns.Add("TWIN_COUNT", System.Type.GetType("System.String"));//平行样个数
        dt.Columns.Add("TWIN_PER", System.Type.GetType("System.String"));//样品比例%
        dt.Columns.Add("TWIN_RANGE", System.Type.GetType("System.String"));//相对偏差范围%
        dt.Columns.Add("TWIN_OK_COUNT", System.Type.GetType("System.String"));//合格数
        dt.Columns.Add("TWIN_OK_PER", System.Type.GetType("System.String"));//合格率%

        dt.Columns.Add("ADD_COUNT", System.Type.GetType("System.String"));//加标回收个数
        dt.Columns.Add("ADD_PER", System.Type.GetType("System.String"));//样品比例%
        dt.Columns.Add("ADD_RANGE", System.Type.GetType("System.String"));//回收率范围%
        dt.Columns.Add("ADD_OK_COUNT", System.Type.GetType("System.String"));//合格数
        dt.Columns.Add("ADD_OK_PER", System.Type.GetType("System.String"));//合格率%

        dt.Columns.Add("SELF_ST_COUNT", System.Type.GetType("System.String"));//自配标样个数
        dt.Columns.Add("SELF_ST_PER", System.Type.GetType("System.String"));//样品比例%
        dt.Columns.Add("SELF_ST_RANGE", System.Type.GetType("System.String"));//相对误差范围%
        dt.Columns.Add("SELF_ST_OK_COUNT", System.Type.GetType("System.String"));//合格数
        dt.Columns.Add("SELF_ST_OK_PER", System.Type.GetType("System.String"));//合格率%

        dt.Columns.Add("ST_COUNT", System.Type.GetType("System.String"));//标样个数
        dt.Columns.Add("ST_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        int iNum = 1;
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (strTempItem == dtResult.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtResult.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drResult = null, drEmptyOut = null, drEmptyOutIsOK = null, drEmptyBat = null, drEmptyBatIsOK = null, drTwin = null, drTwinIsOK = null, drAdd = null, drAddIsOK = null, drSt = null, drStIsOK = null;
            GetDrArr(dtResult, dtEmptyOut, dtEmptyBat, dtTwin, dtAdd, dtSt, strTempItem,
                ref drResult, ref  drEmptyOut,ref drEmptyOutIsOK,ref drEmptyBat,ref drEmptyBatIsOK,
                ref drTwin,ref drTwinIsOK,ref drAdd,ref drAddIsOK,ref drSt,ref drStIsOK);
            #endregion

            DataRow dr = dt.NewRow();

            #region cell 赋值
            dr["NUM"] = iNum.ToString();//序号
            dr["TEST_ITEM"] = dtResult.Rows[i]["ITEM_NAME"].ToString();//分析项目
            dr["ALL_COUNT"] = drResult.Length.ToString();//样品总数

            dr["OUT_EMPTY_COUNT"] = GetCount(drEmptyOut);//现场空白个数
            dr["OUT_EMPTY_OK_PER"] = GetOkPer(drEmptyOut, drEmptyOutIsOK, "OUT_EMPTY_OK_PER");//合格率%

            dr["IN_EMPTY_COUNT"] = GetCount(drEmptyBat);//实验空白个数
            dr["IN_EMPTY_RANGE"] = GetRang(drEmptyBat, "QC_EMPTY_OFFSET");//相对偏差范围%
            dr["IN_EMPTY_OK_PER"] = GetOkPer(drEmptyBat, drEmptyBatIsOK, "IN_EMPTY_OK_PER");//合格率%

            dr["TWIN_COUNT"] = GetCount(drTwin);//平行样个数
            dr["TWIN_PER"] = GetPer(drResult, drTwin);//样品比例%
            dr["TWIN_RANGE"] = GetRang(drTwin, "TWIN_OFFSET");//相对偏差范围%
            dr["TWIN_OK_COUNT"] = GetCount(drTwin, drTwinIsOK,"TWIN_OK_COUNT");//合格数
            dr["TWIN_OK_PER"] = GetOkPer(drTwin, drTwinIsOK, "TWIN_OK_PER");//合格率%

            dr["ADD_COUNT"] = GetCount(drAdd);//加标回收个数
            dr["ADD_PER"] = GetPer(drResult, drAdd);//样品比例%
            dr["ADD_RANGE"] = GetRang(drAdd, "ADD_BACK");//回收率范围%
            dr["ADD_OK_COUNT"] = GetCount(drAdd, drAddIsOK, "ADD_OK_COUNT");//合格数
            dr["ADD_OK_PER"] = GetOkPer(drAdd, drAddIsOK, "ADD_OK_PER");//合格率%

            dr["SELF_ST_COUNT"] = "";//自配标样个数
            dr["SELF_ST_PER"] = "";//样品比例%
            dr["SELF_ST_RANGE"] = "";//相对误差范围%
            dr["SELF_ST_OK_COUNT"] = "";//合格数
            dr["SELF_ST_OK_PER"] = "";//合格率%

            dr["ST_COUNT"] = GetCount(drSt);//标样个数
            dr["ST_OK_PER"] = GetOkPer(drSt, drStIsOK, "ST_OK_PER");//合格率%
            #endregion

            dt.Rows.Add(dr);

            iNum++;
        }

        return dt;
    }

    private DataTable DoneWithTableEmptyOut(DataTable dtQC)
    {
        DataTable dt = new DataTable();

        #region DataTable列
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("TEST_BATCH", System.Type.GetType("System.String"));//分析批次
        dt.Columns.Add("TEST_DATE", System.Type.GetType("System.String"));//分析日期

        dt.Columns.Add("RESULT1", System.Type.GetType("System.String"));//现场空白测定值1
        dt.Columns.Add("RESULT2", System.Type.GetType("System.String"));//现场空白测定值2
        dt.Columns.Add("OFFSET", System.Type.GetType("System.String"));//现场空白测定值范围

        dt.Columns.Add("CHECKOUT", System.Type.GetType("System.String"));//方法检出限
        dt.Columns.Add("AlL_QC_COUNT", System.Type.GetType("System.String"));//总空白样数
        dt.Columns.Add("AlL_QC_OK_COUNT", System.Type.GetType("System.String"));//合格空白样数
        dt.Columns.Add("AlL_QC_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "EMPTY_ISOK");
            #endregion

            for (int j = 0; j < drQC.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (j == 0)
                {
                    dr["TEST_ITEM"] = drQC[j]["ITEM_NAME"].ToString();//分析项目
                }
                dr["TEST_BATCH"] = (j+1).ToString(); ;//分析批次
                dr["TEST_DATE"] = GetDateString(drQC[j]["FINISH_DATE"].ToString());//分析日期

                dr["RESULT1"] = drQC[j]["ITEM_RESULT"].ToString();//现场空白测定值1
                dr["RESULT2"] = "";//现场空白测定值2
                if (j == 0)
                    dr["OFFSET"] = GetRang(drQC, "ITEM_RESULT");//现场空白测定值范围

                dr["CHECKOUT"] = drQC[j]["LOWER_CHECKOUT"].ToString();//方法检出限

                if (j == 0)
                {
                    dr["AlL_QC_COUNT"] = GetCount(drQC);//总空白样数
                    dr["AlL_QC_OK_COUNT"] = GetCount(drQC, drQCIsOK, "OUT_EMPTY_OK_PER");//合格空白样数
                    dr["AlL_QC_OK_PER"] = GetOkPer(drQC, drQCIsOK, "OUT_EMPTY_OK_PER");//合格率%
                }
                #endregion

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    private DataTable DoneWithTableEmptyIn(DataTable dtQC)
    {
        DataTable dt = new DataTable();

        #region DataTable列
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("TEST_BATCH", System.Type.GetType("System.String"));//分析批次
        dt.Columns.Add("TEST_DATE", System.Type.GetType("System.String"));//分析日期

        dt.Columns.Add("RESULT1", System.Type.GetType("System.String"));//空白测定值1(A)
        dt.Columns.Add("RESULT2", System.Type.GetType("System.String"));//空白测定值2(A)
        dt.Columns.Add("OFFSET", System.Type.GetType("System.String"));//相对偏差%
        dt.Columns.Add("RANGE", System.Type.GetType("System.String"));//相对偏差范围%

        dt.Columns.Add("CHECKOUT", System.Type.GetType("System.String"));//方法要求值
        dt.Columns.Add("AlL_QC_COUNT", System.Type.GetType("System.String"));//总空白样数
        dt.Columns.Add("AlL_QC_OK_COUNT", System.Type.GetType("System.String"));//合格空白样数
        dt.Columns.Add("AlL_QC_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "QC_EMPTY_ISOK");
            #endregion

            for (int j = 0; j < drQC.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (j == 0)
                {
                    dr["TEST_ITEM"] = drQC[j]["ITEM_NAME"].ToString();//分析项目
                }
                dr["TEST_BATCH"] = (j + 1).ToString(); ;//分析批次
                dr["TEST_DATE"] = GetDateString(drQC[j]["FINISH_DATE"].ToString());//分析日期

                dr["RESULT1"] = drQC[j]["ITEM_RESULT"].ToString();//空白测定值1(A)
                dr["RESULT2"] = "";//空白测定值2(A)
                dr["OFFSET"] = "";//相对偏差%
                dr["RANGE"] = "";//相对偏差范围%

                dr["CHECKOUT"] = drQC[j]["LOWER_CHECKOUT"].ToString();//方法检出限

                if (j == 0)
                {
                    dr["AlL_QC_COUNT"] = GetCount(drQC);//总空白样数
                    dr["AlL_QC_OK_COUNT"] = GetCount(drQC, drQCIsOK,"IN_EMPTY_OK_PER");//合格空白样数
                    dr["AlL_QC_OK_PER"] = GetOkPer(drQC, drQCIsOK, "IN_EMPTY_OK_PER");//合格率%
                }
                #endregion

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    private DataTable DoneWithTableTwin(DataTable dtQC,string strTwinType)
    {
        DataTable dt = new DataTable();

        #region DataTable列
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("TEST_BATCH", System.Type.GetType("System.String"));//分析批次
        dt.Columns.Add("TEST_DATE", System.Type.GetType("System.String"));//分析日期

        dt.Columns.Add("RESULT1", System.Type.GetType("System.String"));//平行双样测定值1
        dt.Columns.Add("RESULT2", System.Type.GetType("System.String"));//平行双样测定值2
        dt.Columns.Add("OFFSET", System.Type.GetType("System.String"));//相对偏差%
        dt.Columns.Add("RANGE", System.Type.GetType("System.String"));//相对偏差范围%

        dt.Columns.Add("AlL_QC_COUNT", System.Type.GetType("System.String"));//总平行样数对
        dt.Columns.Add("AlL_QC_OK_COUNT", System.Type.GetType("System.String"));//合格平行样对
        dt.Columns.Add("AlL_QC_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem_OfTwinAdd(dtQC, strTempItem, strTwinType);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQC, strTempItem, "TWIN_ISOK", strTwinType);
            #endregion

            for (int j = 0; j < drQC.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (j == 0)
                {
                    dr["TEST_ITEM"] = drQC[j]["ITEM_NAME"].ToString();//分析项目
                }
                dr["TEST_BATCH"] = (j + 1).ToString(); ;//分析批次
                dr["TEST_DATE"] = GetDateString(drQC[j]["FINISH_DATE"].ToString());//分析日期

                dr["RESULT1"] = drQC[j]["ITEM_RESULT"].ToString();//平行双样测定值1
                dr["RESULT2"] = drQC[j]["TWIN_RESULT1"].ToString();//平行双样测定值2
                dr["OFFSET"] = drQC[j]["TWIN_OFFSET"].ToString();//相对偏差%
                if (j == 0)
                    dr["RANGE"] = GetRang(drQC, "TWIN_OFFSET"); //相对偏差范围%

                if (j == 0)
                {
                    dr["AlL_QC_COUNT"] = GetCount(drQC);//总平行样数对
                    dr["AlL_QC_OK_COUNT"] = GetCount(drQC, drQCIsOK, "");//合格平行样对
                    dr["AlL_QC_OK_PER"] = GetOkPer(drQC, drQCIsOK, "");//合格率%
                }
                #endregion

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    private DataTable DoneWithTableAdd(DataTable dtQC)
    {
        DataTable dt = new DataTable();

        #region DataTable列
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("TEST_BATCH", System.Type.GetType("System.String"));//分析批次
        dt.Columns.Add("TEST_DATE", System.Type.GetType("System.String"));//分析日期

        dt.Columns.Add("RESULT1", System.Type.GetType("System.String"));//室内加标回收率%
        dt.Columns.Add("RESULT2", System.Type.GetType("System.String"));//现场加标回收率%
        dt.Columns.Add("RANGE", System.Type.GetType("System.String"));//回收率范围%

        dt.Columns.Add("AlL_QC_COUNT", System.Type.GetType("System.String"));//总加标样数
        dt.Columns.Add("AlL_QC_OK_COUNT", System.Type.GetType("System.String"));//合格加标样数
        dt.Columns.Add("AlL_QC_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQCIn = GetDrArrayForItem_OfTwinAdd(dtQC, strTempItem, "6");//室内加标
            DataRow[] drQCOut = GetDrArrayForItem_OfTwinAdd(dtQC, strTempItem, "2");//现场加标
            DataRow[] drQCInIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQC, strTempItem, "ADD_ISOK", "6");//室内加标
            DataRow[] drQCOutIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQC, strTempItem, "ADD_ISOK", "2");//现场加标
            #endregion

            for (int j = 0; j < drQCIn.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (j == 0)
                {
                    dr["TEST_ITEM"] = drQCIn[j]["ITEM_NAME"].ToString();//分析项目
                }
                dr["TEST_BATCH"] = (j + 1).ToString(); ;//分析批次
                dr["TEST_DATE"] = GetDateString(drQCIn[j]["FINISH_DATE"].ToString());//分析日期

                dr["RESULT1"] = drQCIn[j]["ADD_BACK"].ToString();//室内加标回收率%
                
                if (j == 0)
                    dr["RANGE"] = GetRang_ForAdd(drQCIn, drQCOut); //回收率范围%

                if (j == 0)
                {
                    if (drQCIn.Length + drQCOut.Length > 0)
                    {
                        dr["AlL_QC_COUNT"] = (drQCIn.Length + drQCOut.Length).ToString();//总加标样数
                        dr["AlL_QC_OK_COUNT"] = (drQCInIsOK.Length + drQCOutIsOK.Length).ToString();//合格加标样数
                        dr["AlL_QC_OK_PER"] = Math.Round(((decimal)((drQCInIsOK.Length + drQCOutIsOK.Length) * 100) / (drQCIn.Length + drQCOut.Length)), 1).ToString();//合格率%
                    }
                }
                #endregion

                dt.Rows.Add(dr);
            }

            for (int j = 0; j < drQCOut.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (drQCIn.Length == 0)
                {
                    if (j == 0)
                        dr["TEST_ITEM"] = drQCOut[j]["ITEM_NAME"].ToString();//分析项目
                }

                dr["TEST_BATCH"] = (drQCIn.Length + j + 1).ToString();//分析批次
                dr["TEST_DATE"] = GetDateString(drQCOut[j]["FINISH_DATE"].ToString());//分析日期

                dr["RESULT2"] = drQCOut[j]["ADD_BACK"].ToString();//现场加标回收率% 

                if (drQCIn.Length == 0)
                {
                    if (j == 0)
                        dr["RANGE"] = GetRang_ForAdd(drQCIn, drQCOut); //回收率范围%

                    if (j == 0)
                    {
                        if (drQCIn.Length + drQCOut.Length > 0)
                        {
                            dr["AlL_QC_COUNT"] = (drQCIn.Length + drQCOut.Length).ToString();//总加标样数
                            dr["AlL_QC_OK_COUNT"] = (drQCInIsOK.Length + drQCOutIsOK.Length).ToString();//合格加标样数
                            dr["AlL_QC_OK_PER"] = Math.Round(((decimal)((drQCInIsOK.Length + drQCOutIsOK.Length) * 100) / (drQCIn.Length + drQCOut.Length)), 1).ToString();//合格率%
                        }
                    }
                }
                #endregion

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    private DataTable DoneWithTableSt(DataTable dtQC)
    {
        DataTable dt = new DataTable();

        #region DataTable列
        dt.Columns.Add("TEST_ITEM", System.Type.GetType("System.String"));//分析项目
        dt.Columns.Add("TEST_BATCH", System.Type.GetType("System.String"));//分析批次
        dt.Columns.Add("TEST_DATE", System.Type.GetType("System.String"));//分析日期

        dt.Columns.Add("ND", System.Type.GetType("System.String"));//标样浓度
        dt.Columns.Add("RESULT1", System.Type.GetType("System.String"));//标样测定值1
        dt.Columns.Add("RESULT2", System.Type.GetType("System.String"));//标样测定值2

        dt.Columns.Add("AlL_QC_COUNT", System.Type.GetType("System.String"));//总加标样数
        dt.Columns.Add("AlL_QC_OK_COUNT", System.Type.GetType("System.String"));//合格加标样数
        dt.Columns.Add("AlL_QC_OK_PER", System.Type.GetType("System.String"));//合格率%
        #endregion

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "ST_ISOK");
            #endregion

            for (int j = 0; j < drQC.Length; j++)
            {
                DataRow dr = dt.NewRow();

                #region cell 赋值
                if (j == 0)
                {
                    dr["TEST_ITEM"] = drQC[j]["ITEM_NAME"].ToString();//分析项目
                }
                dr["TEST_BATCH"] = (j + 1).ToString(); ;//分析批次
                dr["TEST_DATE"] = GetDateString(drQC[j]["FINISH_DATE"].ToString());//分析日期

                dr["ND"] = drQC[j]["SRC_RESULT"].ToString();//标样浓度
                dr["RESULT1"] = drQC[j]["ITEM_RESULT"].ToString();//标样测定值1
                dr["RESULT2"] = "";//标样测定值2

                if (j == 0)
                {
                    dr["AlL_QC_COUNT"] = GetCount(drQC);//总标样数
                    dr["AlL_QC_OK_COUNT"] = GetCount(drQC, drQCIsOK, "ST_OK_PER");//合格标样数
                    dr["AlL_QC_OK_PER"] = GetOkPer(drQC, drQCIsOK, "ST_OK_PER");//合格率%
                }
                #endregion

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
    #endregion

    #region Excel 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strMonitorID=this.hdMONITOR_ID.Value.Trim();
        string strStartTime = this.hdQC_BEGIN_DATE.Value.Trim();
        string strEndTime=this.hdQC_END_DATE.Value.Trim();

        string strStationName = new TSysDictLogic().GetDictNameByDictCodeAndType("Station_Name","dict_system_base");
        System.DateTime dtNow = System.DateTime.Now;

        DataTable dtResult = new TMisMonitorResultLogic().getResultWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtEmptyOut = new TMisMonitorResultLogic().getQCEmptyOutWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtEmptyBat = new TMisMonitorResultLogic().getQCEmptyBatWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtTwin = new TMisMonitorResultLogic().getQCTwinWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtAdd = new TMisMonitorResultLogic().getQCAddWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtSt = new TMisMonitorResultLogic().getQCStWitnTimeAndType(strMonitorID, strStartTime, strEndTime);

        #region 监测类型,excel表头用
        string strItemTypeName = "",strItemTypeId="";
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (!strItemTypeId.Contains(dtResult.Rows[i]["MONITOR_ID"].ToString()))
            {
                strItemTypeId += (strItemTypeId.Length > 0 ? "，" : "") + dtResult.Rows[i]["MONITOR_ID"].ToString();
                strItemTypeName += (strItemTypeName.Length > 0 ? "，" : "") + dtResult.Rows[i]["MONITOR_TYPE_NAME"].ToString();
            }
        }
        #endregion

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/QCAccount.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        #region 质量控制结果统计表
        // 表头
        ISheet sheet = hssfworkbook.GetSheet("质量控制结果统计表");

        sheet.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet.GetRow(1).GetCell(7).SetCellValue("项目类别：" + strItemTypeName);
        sheet.GetRow(1).GetCell(18).SetCellValue("监测时期：" + strStartTime + "~" + strEndTime);

        // 数据
        FullAllExcel(hssfworkbook, dtResult, dtEmptyOut, dtEmptyBat, dtTwin, dtAdd, dtSt);
        #endregion

        #region 现场空白
        // 表头
        ISheet sheet1 = hssfworkbook.GetSheet("现场空白");

        sheet1.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet1.GetRow(1).GetCell(4).SetCellValue("项目类别：" + strItemTypeName);
        sheet1.GetRow(1).GetCell(9).SetCellValue( strStartTime + "~" + strEndTime);

        // 数据
        FullEmptyOutExcel(hssfworkbook, dtEmptyOut);
        #endregion

        #region 实验室空白
        // 表头
        ISheet sheet2 = hssfworkbook.GetSheet("实验室空白");

        sheet2.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet2.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet2.GetRow(1).GetCell(9).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullEmptyInExcel(hssfworkbook, dtEmptyBat);
        #endregion

        #region 现场密码平行
        // 表头
        ISheet sheet3 = hssfworkbook.GetSheet("现场密码平行");

        sheet3.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet3.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet3.GetRow(1).GetCell(8).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullTwinExcel(hssfworkbook, dtTwin, "3", "现场密码平行");
        #endregion

        #region 实验室明码平行
        // 表头
        ISheet sheet4 = hssfworkbook.GetSheet("实验室明码平行");

        sheet4.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet4.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet4.GetRow(1).GetCell(8).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullTwinExcel(hssfworkbook, dtTwin, "7", "实验室明码平行");
        #endregion

        #region 实验室密码平行
        // 表头
        ISheet sheet5 = hssfworkbook.GetSheet("实验室密码平行");

        sheet5.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet5.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet5.GetRow(1).GetCell(8).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullTwinExcel(hssfworkbook, dtTwin, "4", "实验室密码平行");
        #endregion

        #region 加标回收
        // 表头
        ISheet sheet6 = hssfworkbook.GetSheet("加标回收");

        sheet6.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet6.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet6.GetRow(1).GetCell(8).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullAddExcel(hssfworkbook, dtAdd, "加标回收");
        #endregion

        #region 标样
        // 表头
        ISheet sheet7 = hssfworkbook.GetSheet("标样");

        sheet7.GetRow(1).GetCell(0).SetCellValue("监测单位：" + strStationName);
        sheet7.GetRow(1).GetCell(3).SetCellValue("项目类别：" + strItemTypeName);
        sheet7.GetRow(1).GetCell(8).SetCellValue(strStartTime + "~" + strEndTime);

        // 数据
        FullStExcel(hssfworkbook, dtSt, "标样");
        #endregion

        using (MemoryStream stream = new MemoryStream())
        {
            string strExcelTime = dtNow.Year.ToString() + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');

            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("质量控制结果统计表" + strExcelTime + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }

    private void FullAllExcel(HSSFWorkbook hssfworkbook, DataTable dtResult, DataTable dtEmptyOut, DataTable dtEmptyBat, DataTable dtTwin, DataTable dtAdd, DataTable dtSt)
    {
        string strSheetName = "质量控制结果统计表";

        string strTempItem = "";
        int iNum = 1;
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (strTempItem == dtResult.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtResult.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drResult = null, drEmptyOut = null, drEmptyOutIsOK = null, drEmptyBat = null, drEmptyBatIsOK = null, drTwin = null, drTwinIsOK = null, drAdd = null, drAddIsOK = null, drSt = null, drStIsOK = null;
            GetDrArr(dtResult, dtEmptyOut, dtEmptyBat, dtTwin, dtAdd, dtSt, strTempItem,
                ref drResult, ref  drEmptyOut, ref drEmptyOutIsOK, ref drEmptyBat, ref drEmptyBatIsOK,
                ref drTwin, ref drTwinIsOK, ref drAdd, ref drAddIsOK, ref drSt, ref drStIsOK);
            #endregion

            if (drEmptyBat.Length + drEmptyOut.Length + drTwin.Length + drAdd.Length + drSt.Length == 0)
                continue;

            if (iNum > 1)
                CopyRange(hssfworkbook, strSheetName, 4, iNum + 4 - 1, 25);

            IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 3);

            ICell changingCell = null;

            #region 填值
            changingCell = changingRow.GetCell(0);
            changingCell.SetCellValue(iNum.ToString());//序号
            changingCell = changingRow.GetCell(1);
            changingCell.SetCellValue(dtResult.Rows[i]["ITEM_NAME"].ToString());//分析项目
            changingCell = changingRow.GetCell(2);
            changingCell.SetCellValue(drResult.Length.ToString());//样品总数

            changingCell = changingRow.GetCell(3);
            changingCell.SetCellValue(GetCount(drEmptyOut));//现场空白个数
            changingCell = changingRow.GetCell(4);
            changingCell.SetCellValue(GetOkPer(drEmptyOut, drEmptyOutIsOK, "OUT_EMPTY_OK_PER"));//合格率%

            changingCell = changingRow.GetCell(5);
            changingCell.SetCellValue(GetCount(drEmptyBat));//实验空白个数
            changingCell = changingRow.GetCell(6);
            changingCell.SetCellValue(GetRang(drEmptyBat, "QC_EMPTY_OFFSET"));//相对偏差范围%
            changingCell = changingRow.GetCell(7);
            changingCell.SetCellValue(GetOkPer(drEmptyBat, drEmptyBatIsOK, "IN_EMPTY_OK_PER"));//合格率%

            changingCell = changingRow.GetCell(8);
            changingCell.SetCellValue(GetCount(drTwin));//平行样个数
            changingCell = changingRow.GetCell(9);
            changingCell.SetCellValue(GetPer(drResult, drTwin));//样品比例%
            changingCell = changingRow.GetCell(10);
            changingCell.SetCellValue(GetRang(drTwin, "TWIN_OFFSET"));//相对偏差范围%
            changingCell = changingRow.GetCell(11);
            changingCell.SetCellValue(GetCount(drTwin, drTwinIsOK, "TWIN_OK_COUNT"));//合格数
            changingCell = changingRow.GetCell(12);
            changingCell.SetCellValue(GetOkPer(drTwin, drTwinIsOK, "TWIN_OK_PER"));//合格率%

            changingCell = changingRow.GetCell(13);
            changingCell.SetCellValue(GetCount(drAdd));//加标回收个数
            changingCell = changingRow.GetCell(14);
            changingCell.SetCellValue(GetPer(drResult, drAdd));//样品比例%
            changingCell = changingRow.GetCell(15);
            changingCell.SetCellValue(GetRang(drAdd, "ADD_BACK"));//回收率范围%
            changingCell = changingRow.GetCell(16);
            changingCell.SetCellValue(GetCount(drAdd, drAddIsOK, "ADD_OK_COUNT"));//合格数
            changingCell = changingRow.GetCell(17);
            changingCell.SetCellValue(GetOkPer(drAdd, drAddIsOK, "ADD_OK_PER"));//合格率%

            changingCell = changingRow.GetCell(18);
            changingCell.SetCellValue("");//有证标样个数
            changingCell = changingRow.GetCell(19);
            changingCell.SetCellValue("");//样品比例%
            changingCell = changingRow.GetCell(20);
            changingCell.SetCellValue("");//相对误差范围%
            changingCell = changingRow.GetCell(21);
            changingCell.SetCellValue("");//合格数
            changingCell = changingRow.GetCell(22);
            changingCell.SetCellValue("");//合格率%

            changingCell = changingRow.GetCell(23);
            changingCell.SetCellValue(GetCount(drSt));//标样个数
            changingCell = changingRow.GetCell(24);
            changingCell.SetCellValue(GetOkPer(drSt, drStIsOK, "ST_OK_PER"));//合格率%
            #endregion

            iNum++;
        }
    }

    private void FullEmptyOutExcel(HSSFWorkbook hssfworkbook, DataTable dtQC)
    {
        string strSheetName = "现场空白";

        string strTempItem = "";

        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 3, i + 3, 11);
        }

        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "EMPTY_ISOK");
            #endregion

            ISheet sheetQc = hssfworkbook.GetSheet(strSheetName);

            for (int j = 0; j < drQC.Length; j++)
            {
                int iNum = i + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 3);

                ICell changingCell = null;

                #region cell 赋值
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(dtQC.Rows[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQC.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i,3+ i + drQC.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQC[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue(drQC[j]["ITEM_RESULT"].ToString());//现场空白测定值1
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue("");//现场空白测定值2
                
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(5);

                    changingCell.SetCellValue(GetRang(drQC, "ITEM_RESULT"));//现场空白测定值范围
                    if (drQC.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 5, 5));
                }

                changingCell = changingRow.GetCell(6);
                changingCell.SetCellValue(drQC[j]["LOWER_CHECKOUT"].ToString());//方法检出限

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(7);
                    changingCell.SetCellValue(GetCount(drQC));//总空白样数
                    changingCell = changingRow.GetCell(8);
                    changingCell.SetCellValue(GetCount(drQC, drQCIsOK,"OUT_EMPTY_OK_PER"));//合格空白样数
                    changingCell = changingRow.GetCell(9);
                    changingCell.SetCellValue(GetOkPer(drQC, drQCIsOK, "OUT_EMPTY_OK_PER"));//合格率%

                    if (drQC.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 7, 7));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 8, 8));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 9, 9));
                    }
                }

                if (i == 0)
                {
                    changingCell = changingRow.GetCell(10);
                    changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                    if (dtQC.Rows.Count > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3, 3+dtQC.Rows.Count - 1, 10, 10));
                    }
                }
                #endregion
            }
        }
    }

    private void FullEmptyInExcel(HSSFWorkbook hssfworkbook, DataTable dtQC)
    {
        string strSheetName = "实验室空白";

        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 3, i + 3, 12);
        }

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "QC_EMPTY_ISOK");
            #endregion

            ISheet sheetQc = hssfworkbook.GetSheet(strSheetName);

            for (int j = 0; j < drQC.Length; j++)
            {
                int iNum = i + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 3);

                ICell changingCell = null;

                #region cell 赋值
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(dtQC.Rows[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQC.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQC[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue(drQC[j]["ITEM_RESULT"].ToString());//空白测定值1(A)
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue("");//空白测定值2(A)
                
                changingCell = changingRow.GetCell(5);
                changingCell.SetCellValue("");//相对偏差%
                changingCell = changingRow.GetCell(6);
                changingCell.SetCellValue("");//相对偏差范围%

                changingCell = changingRow.GetCell(7);
                changingCell.SetCellValue(drQC[j]["LOWER_CHECKOUT"].ToString());//方法检出限

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(8);
                    changingCell.SetCellValue(GetCount(drQC));//总空白样数
                    changingCell = changingRow.GetCell(9);
                    changingCell.SetCellValue(GetCount(drQC, drQCIsOK, "IN_EMPTY_OK_PER"));//合格空白样数
                    changingCell = changingRow.GetCell(10);
                    changingCell.SetCellValue(GetOkPer(drQC, drQCIsOK, "IN_EMPTY_OK_PER"));//合格率%

                    if (drQC.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 8, 8));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 9, 9));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 10, 10));
                    }
                }

                if (i == 0)
                {
                    changingCell = changingRow.GetCell(11);
                    changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                    if (dtQC.Rows.Count > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3, 3+dtQC.Rows.Count - 1, 11, 11));
                    }
                }
                #endregion
            }
        }
    }

    private void FullTwinExcel(HSSFWorkbook hssfworkbook, DataTable dtQC, string strTwinType,string strSheetName)
    {
        DataTable dtQCTmp = new DataTable();
        for (int i = 0; i < dtQC.Columns.Count; i++)
        {
            dtQCTmp.Columns.Add(dtQC.Columns[i].ColumnName, System.Type.GetType("System.String"));
        }

        DataRow[] drQCTmp = GetDrArray_OfTwinAdd(dtQC, strTwinType);
        for (int i = 0; i < drQCTmp.Length; i++)
        {
            DataRow drtmp = dtQCTmp.NewRow();
            for (int j = 0; j < dtQC.Columns.Count; j++)
            {
                drtmp[j] = drQCTmp[i][j].ToString();
            }

            dtQCTmp.Rows.Add(drtmp);

            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 3, i + 3 , 11);
        }

        string strTempItem = "";
        for (int i = 0; i < dtQCTmp.Rows.Count; i++)
        {
            if (strTempItem == dtQCTmp.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQCTmp.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem_OfTwinAdd(dtQCTmp, strTempItem, strTwinType);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQCTmp, strTempItem, "TWIN_ISOK", strTwinType);
            #endregion

            ISheet sheetQc = hssfworkbook.GetSheet(strSheetName);

            for (int j = 0; j < drQC.Length; j++)
            {
                int iNum = i + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 3);

                ICell changingCell = null;

                #region cell 赋值
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(dtQCTmp.Rows[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQC.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQC[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue(drQC[j]["ITEM_RESULT"].ToString());//平行双样测定值1
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue(drQC[j]["TWIN_RESULT1"].ToString());//平行双样测定值2

                changingCell = changingRow.GetCell(5);
                changingCell.SetCellValue(drQC[j]["TWIN_OFFSET"].ToString());//平行双样测定值2

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(6);
                    changingCell.SetCellValue(GetRang(drQC, "TWIN_OFFSET"));//相对偏差范围%

                    if (drQC.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 6, 6));
                    }
                }

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(7);
                    changingCell.SetCellValue(GetCount(drQC));//总平行样数对
                    changingCell = changingRow.GetCell(8);
                    changingCell.SetCellValue(GetCount(drQC, drQCIsOK, ""));//合格平行样对
                    changingCell = changingRow.GetCell(9);
                    changingCell.SetCellValue(GetOkPer(drQC, drQCIsOK, ""));//合格率%

                    if (drQC.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 7, 7));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 8, 8));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3+i, 3+i + drQC.Length - 1, 9, 9));
                    }
                }

                if (i == 0)
                {
                    changingCell = changingRow.GetCell(10);
                    changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                    
                    if (drQCTmp.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3, 3 + drQCTmp.Length - 1, 10, 10));
                    }
                }
                #endregion
            }
        }
    }

    private void FullAddExcel(HSSFWorkbook hssfworkbook, DataTable dtQC,  string strSheetName)
    {
        DataRow[] drQCInTmp = GetDrArray_OfTwinAdd(dtQC, "6");
        DataRow[] drQCOutTmp = GetDrArray_OfTwinAdd(dtQC, "2");
        for (int i = 0; i < drQCInTmp.Length + drQCOutTmp.Length; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 4, i + 4, 10);
        }

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQCIn = GetDrArrayForItem_OfTwinAdd(dtQC, strTempItem, "6");//室内加标
            DataRow[] drQCOut = GetDrArrayForItem_OfTwinAdd(dtQC, strTempItem, "2");//现场加标
            DataRow[] drQCInIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQC, strTempItem, "ADD_ISOK", "6");//室内加标
            DataRow[] drQCOutIsOK = GetDrArrayForItemAndOK_OfTwinAdd(dtQC, strTempItem, "ADD_ISOK", "2");//现场加标
            #endregion

            ISheet sheetQc = hssfworkbook.GetSheet(strSheetName);

            for (int j = 0; j < drQCIn.Length; j++)
            {
                int iNum = i + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 4);

                ICell changingCell = null;

                #region cell 赋值
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(dtQC.Rows[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQCIn.Length + drQCOut.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(4+i, 4+i + drQCIn.Length+drQCOut.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQCIn[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue(drQCIn[j]["ADD_BACK"].ToString());//室内加标回收率%
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue("");//现场加标回收率%

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(5);
                    changingCell.SetCellValue(GetRang_ForAdd(drQCIn, drQCOut));//回收率范围%

                    if (drQCIn.Length+drQCOut.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(4+i,4+i + drQCIn.Length+drQCOut.Length - 1, 5, 5));
                    }
                }

                if (drQCIn.Length + drQCOut.Length > 0)
                {
                    if (j == 0)
                    {
                        changingCell = changingRow.GetCell(6);
                        changingCell.SetCellValue((drQCIn.Length + drQCOut.Length).ToString());//总加标样数
                        changingCell = changingRow.GetCell(7);
                        changingCell.SetCellValue((drQCInIsOK.Length + drQCOutIsOK.Length).ToString());//合格加标样数
                        changingCell = changingRow.GetCell(8);
                        changingCell.SetCellValue(Math.Round(((decimal)((drQCInIsOK.Length + drQCOutIsOK.Length) * 100) / (drQCIn.Length + drQCOut.Length)), 1).ToString());//合格率%

                        if (drQCIn.Length + drQCOut.Length > 1)
                        {
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 6, 6));
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 7, 7));
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 8, 8));
                        }
                    }
                }

                if (i == 0)
                {
                    changingCell = changingRow.GetCell(9);
                    changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                    if (drQCInTmp.Length + drQCOutTmp.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(4, 4 + drQCInTmp.Length + drQCOutTmp.Length - 1, 9, 9));
                    }
                }
                #endregion
            }

            for (int j = 0; j < drQCOut.Length; j++)
            {
                int iNum = i + drQCIn.Length + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 4);

                ICell changingCell = null;

                #region cell 赋值
                if (drQCIn.Length == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(dtQC.Rows[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQCIn.Length + drQCOut.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((drQCIn.Length + j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQCOut[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue("");//室内加标回收率%
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue(drQCOut[j]["ADD_BACK"].ToString());//现场加标回收率%

                if (drQCIn.Length == 0)
                {
                    if (j == 0)
                    {
                        changingCell = changingRow.GetCell(5);
                        changingCell.SetCellValue(GetRang_ForAdd(drQCIn, drQCOut));//回收率范围%

                        if (drQCIn.Length + drQCOut.Length > 1)
                        {
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCOut.Length - 1, 5, 5));
                        }
                    }

                    if (j == 0)
                    {
                        changingCell = changingRow.GetCell(6);
                        changingCell.SetCellValue((drQCIn.Length + drQCOut.Length).ToString());//总加标样数
                        changingCell = changingRow.GetCell(7);
                        changingCell.SetCellValue((drQCInIsOK.Length + drQCOutIsOK.Length).ToString());//合格加标样数
                        changingCell = changingRow.GetCell(8);
                        changingCell.SetCellValue(Math.Round(((decimal)((drQCInIsOK.Length + drQCOutIsOK.Length) * 100) / (drQCIn.Length + drQCOut.Length)), 1).ToString());//合格率%

                        if (drQCIn.Length + drQCOut.Length > 1)
                        {
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 6, 6));
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 7, 7));
                            sheetQc.AddMergedRegion(new CellRangeAddress(4 + i, 4 + i + drQCIn.Length + drQCOut.Length - 1, 8, 8));
                        }
                    }

                    if (i == 0)
                    {
                        changingCell = changingRow.GetCell(9);
                        changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                        if (drQCInTmp.Length + drQCOutTmp.Length > 1)
                        {
                            sheetQc.AddMergedRegion(new CellRangeAddress(4, 4 + drQCInTmp.Length + drQCOutTmp.Length - 1, 9, 9));
                        }
                    }
                }
                #endregion
            }
        }
    }

    private void FullStExcel(HSSFWorkbook hssfworkbook, DataTable dtQC, string strSheetName)
    {
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (i > 0)
                CopyRange(hssfworkbook, strSheetName, 3, i + 3, 12);
        }

        string strTempItem = "";
        for (int i = 0; i < dtQC.Rows.Count; i++)
        {
            if (strTempItem == dtQC.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtQC.Rows[i]["ITEM_ID"].ToString();

            #region 构造计算用的DataRow[]
            DataRow[] drQC = GetDrArrayForItem(dtQC, strTempItem);
            DataRow[] drQCIsOK = GetDrArrayForItemAndOK(dtQC, strTempItem, "ST_ISOK");
            #endregion

            ISheet sheetQc = hssfworkbook.GetSheet(strSheetName);

            for (int j = 0; j < drQC.Length; j++)
            {
                int iNum = i + j;

                IRow changingRow = hssfworkbook.GetSheet(strSheetName).GetRow(iNum + 3);

                ICell changingCell = null;

                #region cell 赋值
                if (j == 0)
                {
                    changingCell = changingRow.GetCell(0);
                    changingCell.SetCellValue(drQC[i]["ITEM_NAME"].ToString());//分析项目
                    if (drQC.Length > 1)
                        sheetQc.AddMergedRegion(new CellRangeAddress(3 + i, 3 + i + drQC.Length - 1, 0, 0));
                }

                changingCell = changingRow.GetCell(1);
                changingCell.SetCellValue((j + 1).ToString());//分析批次
                changingCell = changingRow.GetCell(2);
                changingCell.SetCellValue(GetDateString(drQC[j]["FINISH_DATE"].ToString()));//分析日期

                changingCell = changingRow.GetCell(3);
                changingCell.SetCellValue(drQC[j]["SRC_RESULT"].ToString());//标样浓度
                changingCell = changingRow.GetCell(4);
                changingCell.SetCellValue(drQC[j]["ITEM_RESULT"].ToString());//标样测定值1
                changingCell = changingRow.GetCell(5);
                changingCell.SetCellValue("");//标样测定值2

                if (j == 0)
                {
                    changingCell = changingRow.GetCell(6);
                    changingCell.SetCellValue(GetCount(drQC));//总标样数
                    changingCell = changingRow.GetCell(7);
                    changingCell.SetCellValue(GetCount(drQC, drQCIsOK, "ST_OK_PER"));//合格标样数
                    changingCell = changingRow.GetCell(8);
                    changingCell.SetCellValue(GetOkPer(drQC, drQCIsOK, "ST_OK_PER"));//合格率%

                    if (drQC.Length > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3 + i, 3 + i + drQC.Length - 1, 6, 6));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3 + i, 3 + i + drQC.Length - 1, 7, 7));
                        sheetQc.AddMergedRegion(new CellRangeAddress(3 + i, 3 + i + drQC.Length - 1, 8, 8));
                    }
                }

                if (i == 0)
                {
                    changingCell = changingRow.GetCell(9);
                    changingCell.SetCellValue("《环境水质监测质量保证手册》第二版、HJ/T373-2007");//合格判定标准

                    if (dtQC.Rows.Count > 1)
                    {
                        sheetQc.AddMergedRegion(new CellRangeAddress(3, 3 + dtQC.Rows.Count - 1, 9, 9));
                    }
                }
                #endregion
            }
        }
    }
    #endregion

    private void CopyRange(HSSFWorkbook myHSSFWorkBook, string strSheetName, int intFromRowIndex, int intToRowIndex,int intCellCount)
    {
        IRow sourceRow = myHSSFWorkBook.GetSheet(strSheetName).GetRow(intFromRowIndex);
        if (sourceRow != null )
        {
            IRow toRow = null;
            toRow = myHSSFWorkBook.GetSheet(strSheetName).GetRow(intToRowIndex);
            if (toRow == null)
                toRow = myHSSFWorkBook.GetSheet(strSheetName).CreateRow(intToRowIndex);
            for (int i = 0; i < intCellCount; i++)
            {
                ICell sourceCell = sourceRow.GetCell(i);
                ICell toCell = null;
                toCell = toRow.GetCell(i);
                if (toCell == null)
                    toCell = toRow.CreateCell(i);
                toCell.CellStyle = sourceCell.CellStyle;
            }
        }
    }

    #region 构造计算用的DataRow[]
    //总表
    private void GetDrArr(DataTable dtResult, DataTable dtEmptyOut, DataTable dtEmptyBat, DataTable dtTwin, DataTable dtAdd, DataTable dtSt, string strTempItem,
        ref DataRow[] drResult, ref  DataRow[] drEmptyOut,ref DataRow[] drEmptyOutIsOK,ref DataRow[] drEmptyBat,ref DataRow[] drEmptyBatIsOK,
        ref DataRow[] drTwin,ref DataRow[] drTwinIsOK,ref DataRow[] drAdd,ref DataRow[] drAddIsOK,ref DataRow[] drSt,ref DataRow[] drStIsOK)
    {
        drResult = GetDrArrayForItem(dtResult, strTempItem);

        drEmptyOut = GetDrArrayForItem(dtEmptyOut, strTempItem);
        drEmptyOutIsOK = GetDrArrayForItemAndOK(dtEmptyOut, strTempItem, "EMPTY_ISOK");

        drEmptyBat = GetDrArrayForItem(dtEmptyBat, strTempItem);
        drEmptyBatIsOK = GetDrArrayForItemAndOK(dtEmptyBat, strTempItem, "QC_EMPTY_ISOK");

        drTwin = GetDrArrayForItem(dtTwin, strTempItem);
        drTwinIsOK = GetDrArrayForItemAndOK(dtTwin, strTempItem, "TWIN_ISOK");

        drAdd = GetDrArrayForItem(dtAdd, strTempItem);
        drAddIsOK = GetDrArrayForItemAndOK(dtAdd, strTempItem, "ADD_ISOK");

        drSt = GetDrArrayForItem(dtSt, strTempItem);
        drStIsOK = GetDrArrayForItemAndOK(dtSt, strTempItem, "ST_ISOK");
    }

    private DataRow[] GetDrArrayForItem(DataTable dtSrcS, string strTempItem)
    {
        return dtSrcS.Select("ITEM_ID='" + strTempItem + "'");
    }

    private DataRow[] GetDrArrayForItemAndOK(DataTable dtSrcS, string strTempItem, string strOkColName)
    {
        return dtSrcS.Select("ITEM_ID='" + strTempItem + "' and " + strOkColName + "='1'");
    }

    private DataRow[] GetDrArrayForItem_OfTwinAdd(DataTable dtSrcS, string strTempItem, string strTwinType)
    {
        return dtSrcS.Select("ITEM_ID='" + strTempItem + "' and QC_TYPE='" + strTwinType + "'");
    }

    private DataRow[] GetDrArrayForItemAndOK_OfTwinAdd(DataTable dtSrcS, string strTempItem, string strOkColName, string strTwinType)
    {
        return dtSrcS.Select("ITEM_ID='" + strTempItem + "' and QC_TYPE='" + strTwinType + "' and " + strOkColName + "='1'");
    }

    private DataRow[] GetDrArray_OfTwinAdd(DataTable dtSrcS, string strTwinType)
    {
        return dtSrcS.Select("QC_TYPE='" + strTwinType + "'");
    }
    #endregion

    #region 计算函数
    //范围
    private string GetRang(DataRow[] drSource, string strColumeName)
    {
        if (drSource.Length == 0)
            return "";

        if (strColumeName == "QC_EMPTY_OFFSET")//实验空白
            return "";

        DataRow drRangFirst = drSource.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).First();
        DataRow drRangLast = drSource.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).Last();

        string iMax = drRangFirst[strColumeName].ToString();
        string iMin = drRangLast[strColumeName].ToString();
        return (iMin == iMax) ? iMax : (iMin + "~" + iMax);//范围
    }

    private string GetRang_ForAdd(DataRow[] drSourceIn, DataRow[] drSourceOut)
    {
        string strColumeName="ADD_BACK";
        if (drSourceIn.Length + drSourceOut.Length  == 0)
            return "";

        decimal dMin, dMax;
        if (drSourceOut.Length == 0)
        {
            DataRow drRangFirstIn = drSourceIn.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).First();
            DataRow drRangLastIn = drSourceIn.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).Last();

            string iMaxIn = drRangFirstIn[strColumeName].ToString();
            string iMinIn = drRangLastIn[strColumeName].ToString();

            decimal dMaxIn = decimal.Parse(iMaxIn);
            decimal dMinIn = decimal.Parse(iMinIn);

            dMin = dMinIn;
            dMax = dMaxIn;
        }
        else if (drSourceIn.Length == 0)
        {
            DataRow drRangFirstOut = drSourceOut.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).First();
            DataRow drRangLastOut = drSourceOut.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).Last();
            string iMaxOut = drRangFirstOut[strColumeName].ToString();
            string iMinOut = drRangLastOut[strColumeName].ToString();

            decimal dMaxOut = decimal.Parse(iMaxOut);
            decimal dMinOut = decimal.Parse(iMinOut);

            dMin = dMinOut;
            dMax = dMaxOut;
        }
        else
        {
            DataRow drRangFirstIn = drSourceIn.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).First();
            DataRow drRangLastIn = drSourceIn.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).Last();
            DataRow drRangFirstOut = drSourceOut.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).First();
            DataRow drRangLastOut = drSourceOut.Cast<DataRow>().OrderByDescending(x => Convert.ToDouble(x[strColumeName].ToString().Length > 0 ? x[strColumeName].ToString() : "0")).Last();

            string iMaxIn = drRangFirstIn[strColumeName].ToString();
            string iMinIn = drRangLastIn[strColumeName].ToString();
            string iMaxOut = drRangFirstOut[strColumeName].ToString();
            string iMinOut = drRangLastOut[strColumeName].ToString();

            decimal dMaxIn = decimal.Parse(iMaxIn);
            decimal dMinIn = decimal.Parse(iMinIn);
            decimal dMaxOut = decimal.Parse(iMaxOut);
            decimal dMinOut = decimal.Parse(iMinOut);

            dMin = dMinIn > dMinOut ? dMinOut : dMinIn;
            dMax = dMaxIn > dMaxOut ? dMaxIn : dMaxOut;
        }

        return (dMin == dMax) ? dMax.ToString() : (dMin.ToString() + "~" + dMax.ToString());//范围
    }

    //质控数
    private string GetCount(DataRow[] drSource)
    {
        return (drSource.Length > 0) ? drSource.Length.ToString() : "";
    }

    //合格数
    private string GetCount(DataRow[] drSource, DataRow[] drSourceOK, string strColumeName)
    {
        if (strColumeName == "OUT_EMPTY_OK_PER")//现场空白
            return (drSource.Length > 0) ? drSource.Length.ToString() : "";

        if (strColumeName == "IN_EMPTY_OK_PER")//实验空白 
            return (drSource.Length > 0) ? drSource.Length.ToString() : "";

        if (strColumeName == "ST_OK_PER")//标样 
            return (drSource.Length > 0) ? drSource.Length.ToString() : "";

        return (drSource.Length > 0) ? drSourceOK.Length.ToString() : "";
    }

    //样品比例%
    private string GetPer(DataRow[] drResult, DataRow[] drQC)
    {
        if (drResult.Length == 0)
            return "";

        return (drQC.Length > 0) ? Math.Round(((decimal)(drQC.Length * 100) / drResult.Length), 1).ToString() : "";
    }

    //合格率
    private string GetOkPer(DataRow[] drSource, DataRow[] drSourceOk, string strColumeName)
    {
        if (strColumeName == "OUT_EMPTY_OK_PER")//现场空白
            return (drSource.Length > 0) ? "100" : "";

        if (strColumeName == "IN_EMPTY_OK_PER")//实验空白 
            return (drSource.Length > 0) ? "100" : "";

        if (strColumeName == "ST_OK_PER")//标样 
            return (drSource.Length > 0) ? "100" : "";

        return (drSource.Length > 0) ? Math.Round(((decimal)(drSourceOk.Length * 100) / drSource.Length), 1).ToString() : "";
    }

    private string GetDateString(string strdtime)
    {
        if (strdtime.Length > 0)
        {
            DateTime dtime = DateTime.Parse(strdtime.ToString());
            return dtime.Year + "-" + dtime.Month.ToString().PadLeft(2, '0') + "-" + dtime.Day.ToString().PadLeft(2, '0');
        }
        else
            return "";
    }
    #endregion
}