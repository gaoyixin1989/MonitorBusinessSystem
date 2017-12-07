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
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

/// <summary>
/// 功能描述：实验室质控设置
/// 创建日期：2012-12-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_QcEditing_QY : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["ID"] != null)
        {
            this.hidID.Value = Request["ID"].ToString();
        }
        if (Request["QC_TYPE"] != null)
        {
            this.hidQC_TYPE.Value = Request["QC_TYPE"].ToString();
        }

        //获取数据
        if (Request["status"] != null && Request["status"] == "getdata")
        {
            strResult = getData();
            Response.Write(strResult);
            Response.End();
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

    public string getData()
    {
        string objStr = "";
        string strSampleCode = "";
        string strItemResult = "";
        switch (this.hidQC_TYPE.Value)
        {
            case "5": 
                TMisMonitorQcEmptyBatVo QcEmptyBatVo = new TMisMonitorQcEmptyBatLogic().Details(this.hidID.Value);
                objStr = "{\"strQcEmptyResult\":\"" + QcEmptyBatVo.QC_EMPTY_IN_RESULT + "\",\"strValue1\":\"" + QcEmptyBatVo.REMARK1 + "\",\"strValue2\":\"" + QcEmptyBatVo.REMARK2 + "\",\"strValue3\":\"" + QcEmptyBatVo.REMARK3 + "\",\"strEmptyCount\":\"" + QcEmptyBatVo.QC_EMPTY_IN_COUNT + "\"}";
                break;
            case "8":
                TMisMonitorQcStVo QcStVo = new TMisMonitorQcStLogic().Details(this.hidID.Value);
                objStr = "{\"strValue1\":\"" + QcStVo.REMARK1 + "\",\"strValue2\":\"" + QcStVo.REMARK2 + "\",\"strValue3\":\"" + QcStVo.REMARK3 + "\",\"strStCount\":\"" + QcStVo.ST_RESULT + "\"}";
                break;
            case "6":
                TMisMonitorQcAddVo QcAddVo = new TMisMonitorQcAddLogic().Details(this.hidID.Value);
                TMisMonitorResultVo AddResultVo = new TMisMonitorResultLogic().Details(QcAddVo.RESULT_ID_SRC);
                strItemResult = AddResultVo.ITEM_RESULT;
                TMisMonitorSampleInfoVo AddSampleVo = new TMisMonitorSampleInfoLogic().Details(AddResultVo.SAMPLE_ID);
                strSampleCode = AddSampleVo.SAMPLE_CODE;
                objStr = "{\"strSampleCode\":\"" + strSampleCode + "\",\"strItemResult\":\"" + strItemResult + "\",\"strAddResult\":\"" + QcAddVo.ADD_RESULT_EX + "\",\"strQcAdd\":\"" + QcAddVo.QC_ADD + "\",\"strAddBack\":\"" + QcAddVo.ADD_BACK + "\"}";
                break;
            case "7":
                TMisMonitorQcTwinVo QcTwinVo = new TMisMonitorQcTwinLogic().Details(this.hidID.Value);
                TMisMonitorResultVo TwinResultVo = new TMisMonitorResultLogic().Details(QcTwinVo.RESULT_ID_SRC);
                TMisMonitorSampleInfoVo TwinSampleVo = new TMisMonitorSampleInfoLogic().Details(TwinResultVo.SAMPLE_ID);
                strSampleCode = TwinSampleVo.SAMPLE_CODE;
                objStr = "{\"strSrcResultID\":\"" + QcTwinVo.RESULT_ID_SRC + "\",\"strSampleCode\":\"" + strSampleCode + "\",\"strResult1\":\"" + QcTwinVo.TWIN_RESULT1 + "\",\"strResult2\":\"" + QcTwinVo.TWIN_RESULT2 + "\",\"strAvg\":\"" + QcTwinVo.TWIN_AVG + "\",\"strOffset\":\"" + QcTwinVo.TWIN_OFFSET + "\"}";
                break;
        }
        
        return objStr;
    }
    
    public string getCalculate()
    {
        string objStr = "";
        //空白平均值
        string strEmptyValue = "";
        string strEmptyCount = "";
        //标准样平均值
        string strSrcValue = "";
        string strSrcCount = "";
        //回收率
        string strAddBack = "";
        //平均值
        string strAvgValue = "";
        //偏差
        string strOffSet = "";

        //如果是实验室空白
        if (this.hidQC_TYPE.Value == "5")
        {
            //空白个数值
            string strValue1 = Request["QC_EMPTY_IN_VALUE1"].ToString().Trim();
            string strValue2 = Request["QC_EMPTY_IN_VALUE2"].ToString().Trim();
            string strValue3 = Request["QC_EMPTY_IN_VALUE3"].ToString().Trim();
            //空白个数
            strEmptyCount = (int.Parse(strValue1 == "" ? "0" : "1") + int.Parse(strValue2 == "" ? "0" : "1") + int.Parse(strValue3 == "" ? "0" : "1")).ToString();

            //计算平均值
            if (strEmptyCount != "0")
                strEmptyValue = ((decimal.Parse(strValue1 == "" ? "0" : strValue1) + decimal.Parse(strValue2 == "" ? "0" : strValue2) + decimal.Parse(strValue3 == "" ? "0" : strValue3)) / int.Parse(strEmptyCount)).ToString();

            objStr = "{\"strEmptyCount\":\"" + strEmptyCount + "\",\"strEmptyValue\":\"" + strEmptyValue + "\"}";
        }
        //如果是标准样
        if (this.hidQC_TYPE.Value == "8")
        {
            //标准样个数值
            string strValue1 = Request["SRC_IN_VALUE1"].ToString().Trim();
            string strValue2 = Request["SRC_IN_VALUE2"].ToString().Trim();
            string strValue3 = Request["SRC_IN_VALUE3"].ToString().Trim();
            //标准样个数
            strSrcCount = (int.Parse(strValue1 == "" ? "0" : "1") + int.Parse(strValue2 == "" ? "0" : "1") + int.Parse(strValue3 == "" ? "0" : "1")).ToString();
            
            //计算平均值
            if (strSrcCount != "0")
                strSrcValue = ((decimal.Parse(strValue1 == "" ? "0" : strValue1) + decimal.Parse(strValue2 == "" ? "0" : strValue2) + decimal.Parse(strValue3 == "" ? "0" : strValue3)) / int.Parse(strSrcCount)).ToString();

            objStr = "{\"strSrcCount\":\"" + strSrcCount + "\",\"strSrcValue\":\"" + strSrcValue + "\"}";
        }

        //如果是实验室加标
        if (this.hidQC_TYPE.Value == "6")
        {
            //测定值
            string strAddResultEx = Request["ADD_RESULT_EX"].ToString();
            //加标量
            string strQcAdd = Request["QC_ADD"].ToString();
            //原始测定值
            string strResult = Request["ADD_RESULT"].ToString();
            //计算回收率
            decimal AddBack = 0;
            if (strResult != "" && strResult != "0" && strQcAdd !="")
            {
                AddBack = Math.Abs(decimal.Parse(strAddResultEx) - decimal.Parse(strResult)) / decimal.Parse(strQcAdd) * 100;
                strAddBack = Math.Round(AddBack, 1).ToString();
            }

            objStr = "{\"strAddBack\":\"" + strAddBack + "\"}";
        }
        //实验室明码平行
        if (this.hidQC_TYPE.Value == "7")
        {
            string strResultId = Request["hidSrcResultID"].ToString();
            string strValue1 = Request["TWIN_RESULT1"].ToString();
            string strValue2 = Request["TWIN_RESULT2"].ToString();
            if (strResultId != "" && strValue1 != "" && strValue2 != "")
            {
                //根据结果ID获取监测监测项目ID
                string strQcTwinValue = new TMisMonitorResultLogic().getQcTwinValueEx(strResultId, strValue1, strValue2);
                strAvgValue = strQcTwinValue.Split(',')[0];
                strOffSet = strQcTwinValue.Split(',')[1];
            }

            objStr = "{\"strAvgValue\":\"" + strAvgValue + "\",\"strOffSet\":\"" + strOffSet + "\"}";
        }
        return objStr;
    }
    /// <summary>
    /// 保存质控数据
    /// </summary>
    /// <returns></returns>
    public string saveQcValue()
    {
        bool isSuccess = false;
        if (this.hidQC_TYPE.Value == "5")
        {
            //空白数据
            string strEmptyValue1 = Request["QC_EMPTY_IN_VALUE1"] == "" ? "0" : Request["QC_EMPTY_IN_VALUE1"].ToString();
            string strEmptyValue2 = Request["QC_EMPTY_IN_VALUE2"] == "" ? "0" : Request["QC_EMPTY_IN_VALUE2"].ToString();
            string strEmptyValue3 = Request["QC_EMPTY_IN_VALUE3"] == "" ? "0" : Request["QC_EMPTY_IN_VALUE3"].ToString();
            string strEmptyValue = Request["QC_EMPTY_IN_RESULT"] == "" ? "0" : Request["QC_EMPTY_IN_RESULT"].ToString();
            string strEmptyCount = this.dEmptyCount.Value.Trim() == "" ? "0" : this.dEmptyCount.Value.Trim();

            TMisMonitorQcEmptyBatVo QcEmptyBatVo = new TMisMonitorQcEmptyBatVo();
            QcEmptyBatVo.ID = this.hidID.Value;
            QcEmptyBatVo.QC_EMPTY_IN_RESULT = strEmptyValue;
            QcEmptyBatVo.REMARK1 = strEmptyValue1;
            QcEmptyBatVo.REMARK2 = strEmptyValue2;
            QcEmptyBatVo.REMARK3 = strEmptyValue3;
            QcEmptyBatVo.QC_EMPTY_IN_COUNT = strEmptyCount;
            isSuccess = new TMisMonitorQcEmptyBatLogic().Edit(QcEmptyBatVo);
        }
        if (this.hidQC_TYPE.Value == "8")
        {
            //标准样数据
            string strSRC_IN_VALUE1 = Request["SRC_IN_VALUE1"] == "" ? "0" : Request["SRC_IN_VALUE1"].ToString();
            string strSRC_IN_VALUE2 = Request["SRC_IN_VALUE2"] == "" ? "0" : Request["SRC_IN_VALUE2"].ToString();
            string strSRC_IN_VALUE3 = Request["SRC_IN_VALUE3"] == "" ? "0" : Request["SRC_IN_VALUE3"].ToString();
            string strStResult = this.dSrcCount.Value.Trim() == "" ? "0" : this.dSrcCount.Value.Trim();

            TMisMonitorQcStVo QcStVo = new TMisMonitorQcStVo();
            QcStVo.ID = this.hidID.Value;
            QcStVo.REMARK1 = strSRC_IN_VALUE1;
            QcStVo.REMARK2 = strSRC_IN_VALUE2;
            QcStVo.REMARK3 = strSRC_IN_VALUE3;
            QcStVo.ST_RESULT = strStResult;
            isSuccess = new TMisMonitorQcStLogic().Edit(QcStVo);
        }
        if (this.hidQC_TYPE.Value == "6")
        {
            //实验室加标数据
            string strAddResultEx = Request["ADD_RESULT_EX"] == "" ? "0" : Request["ADD_RESULT_EX"].ToString();
            string strQcAdd = Request["QC_ADD"] == "" ? "0" : Request["QC_ADD"].ToString();
            string strAddBack = Request["ADD_BACK"] == "" ? "0" : Request["ADD_BACK"].ToString();

            TMisMonitorQcAddVo QcAddVo = new TMisMonitorQcAddVo();
            QcAddVo.ID = this.hidID.Value;
            QcAddVo.ADD_RESULT_EX = strAddResultEx;
            QcAddVo.QC_ADD = strQcAdd;
            QcAddVo.ADD_BACK = strAddBack;
            isSuccess = new TMisMonitorQcAddLogic().Edit(QcAddVo);
        }
        if (this.hidQC_TYPE.Value == "7")
        {
            //实验室明码平行
            string strTwinResult1 = Request["TWIN_RESULT1"] == "" ? "0" : Request["TWIN_RESULT1"].ToString();
            string strTwinResult2 = Request["TWIN_RESULT2"] == "" ? "0" : Request["TWIN_RESULT2"].ToString();
            string strTwinAvg = Request["TWIN_AVG"] == "" ? "0" : Request["TWIN_AVG"].ToString();
            string strTwinOffSet = Request["TWIN_OFFSET"] == "" ? "0" : Request["TWIN_OFFSET"].ToString();

            TMisMonitorQcTwinVo QcTwinVo = new TMisMonitorQcTwinVo();
            QcTwinVo.ID = this.hidID.Value;
            QcTwinVo.TWIN_RESULT1 = strTwinResult1;
            QcTwinVo.TWIN_RESULT2 = strTwinResult2;
            QcTwinVo.TWIN_AVG = strTwinAvg;
            QcTwinVo.TWIN_OFFSET = strTwinOffSet;
            isSuccess = new TMisMonitorQcTwinLogic().Edit(QcTwinVo);
            if (isSuccess)
            {
                TMisMonitorResultVo ResultVo = new TMisMonitorResultVo();
                ResultVo.ID = this.hidSrcResultID.Value;
                ResultVo.ITEM_RESULT = strTwinAvg;
                isSuccess = new TMisMonitorResultLogic().Edit(ResultVo);
            }
        }
        return isSuccess == true ? "1" : "0"; 
    }
}