using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

/// <summary>
/// 功能描述：实验室质控设置
/// 创建日期：2012-12-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QcSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["resultid"] != null)
        {
            this.resultid.Value = Request["resultid"].ToString();
            this.result.Value = Request["result"].ToString();
            this.emptycount.Value = geQcEmptyCount(Request["resultid"].ToString());
            this.emptyBatId.Value = new TMisMonitorResultLogic().Details(Request["resultid"].ToString()).EMPTY_IN_BAT_ID;
        }

        //计算实验室加标回收率和实验室明码平行相对偏差
        if (Request["status"] != null && Request["status"] == "calculate")
        {
            strResult = getCalculate();
            Response.Write(strResult);
            Response.End();
        }
        //保存数据信息
        if (Request["status"] != null && Request["status"].ToString() == "save")
        {
            strResult = saveQcValue();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取空白个数
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    public string geQcEmptyCount(string strResultId)
    {
        string strEmptyBatId = new TMisMonitorResultLogic().Details(strResultId).EMPTY_IN_BAT_ID;
        if (strEmptyBatId == "") return "";
        string strEmptyCount = new i3.BusinessLogic.Channels.Mis.Monitor.QC.TMisMonitorQcEmptyBatLogic().Details(strEmptyBatId).QC_EMPTY_IN_COUNT;
        return strEmptyCount;
    }
    public string getCalculate()
    {
        //回收率
        string strAddBack = "";
        //平均值
        string strAvgValue = "";
        //偏差
        string strOffSet = "";

        //如果是实验室加标
        if (Request["chkQcAdd"] != null && Request["chkQcAdd"] == "on")
        {
            //测定值
            string strAddResultEx = Request["ADD_RESULT_EX"].ToString();
            //加标量
            string strQcAdd = Request["QC_ADD"].ToString();
            //原始测定值
            string strResult = Request["result"].ToString();
            //计算回收率
            decimal AddBack = 0;
            if (strResult != "")
            {
                AddBack = Math.Abs(decimal.Parse(strAddResultEx) - decimal.Parse(strResult)) / decimal.Parse(strQcAdd) * 100;
                strAddBack = Math.Round(AddBack, 1).ToString();
            }
        }
        //实验室明码平行
        if (Request["chkQcTwin"] != null && Request["chkQcTwin"] == "on")
        {
            string strResultId = Request["resultid"].ToString();
            string strValue1 = Request["TWIN_RESULT1"].ToString();
            string strValue2 = Request["TWIN_RESULT2"].ToString();
            //根据结果ID获取监测监测项目ID
            string strQcTwinValue = new TMisMonitorResultLogic().getQcTwinValue(strResultId, strValue1, strValue2);
            strAvgValue = strQcTwinValue.Split(',')[0];
            strOffSet = strQcTwinValue.Split(',')[1];
        }
        return "{\"strAddBack\":\"" + strAddBack + "\",\"strAvgValue\":\"" + strAvgValue + "\",\"strOffSet\":\"" + strOffSet + "\"}";
    }
    /// <summary>
    /// 保存质控数据
    /// </summary>
    /// <returns></returns>
    public string saveQcValue()
    {
        string strResultId = Request["resultid"].ToString();
        string strEmptyBatId = Request["emptyBatId"].ToString();
        //空白数据
        string chkQcEmpty = Request["chkQcEmpty"] == null ? "" : Request["chkQcEmpty"].ToString();
        string strEmptyValue = Request["QC_EMPTY_IN_RESULT"] == "" ? "0" : Request["QC_EMPTY_IN_RESULT"].ToString();
        string strEmptyCount = Request["QC_EMPTY_IN_COUNT"] == "" ? "0" : Request["QC_EMPTY_IN_COUNT"].ToString();
        //标准样数据
        string chkQcSt = Request["chkQcSt"] == null ? "" : Request["chkQcSt"].ToString();
        string strSrcResult = Request["SRC_RESULT"] == "" ? "0" : Request["SRC_RESULT"].ToString();
        string strStResult = Request["ST_RESULT"] == "" ? "0" : Request["ST_RESULT"].ToString();
        //实验室加标数据
        string chkQcAdd = Request["chkQcAdd"] == null ? "" : Request["chkQcAdd"].ToString();
        string strAddResultEx = Request["ADD_RESULT_EX"] == "" ? "0" : Request["ADD_RESULT_EX"].ToString();
        string strQcAdd = Request["QC_ADD"] == "" ? "0" : Request["QC_ADD"].ToString();
        string strAddBack = Request["ADD_BACK"] == "" ? "0" : Request["ADD_BACK"].ToString();
        //实验室明码平行
        string chkQcTwin = Request["chkQcTwin"] == null ? "" : Request["chkQcTwin"].ToString();
        string strTwinResult1 = Request["TWIN_RESULT1"] == "" ? "0" : Request["TWIN_RESULT1"].ToString();
        string strTwinResult2 = Request["TWIN_RESULT2"] == "" ? "0" : Request["TWIN_RESULT2"].ToString();
        string strTwinAvg = Request["TWIN_AVG"] == "" ? "0" : Request["TWIN_AVG"].ToString();
        string strTwinOffSet = Request["TWIN_OFFSET"] == "" ? "0" : Request["TWIN_OFFSET"].ToString();

        bool isSuccess = new TMisMonitorResultLogic().saveQcValue(strResultId, chkQcEmpty, strEmptyValue, strEmptyCount, chkQcSt,
                                             strSrcResult, strStResult, chkQcAdd, strAddResultEx, strQcAdd, strAddBack,
                                             chkQcTwin, strTwinResult1, strTwinResult2, strTwinAvg, strTwinOffSet);
        return isSuccess == true ? "1" : "0";
    }
}