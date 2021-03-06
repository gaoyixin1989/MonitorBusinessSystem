﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using System.Text.RegularExpressions;

/// <summary>
/// 创建原因：实现清远分析类现场监测项目原始记录表数据保存功能--主要针对 烟尘 也可根据配置配置其他
/// 创建人：胡方扬
/// 创建时间：2013-07-09 14：30
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_OriginalTable_DustyTable : PageBase
{
    public string  strSortname="",strSortorder="";
    public int intPageIndex = 0, intPageSize = 0;
    protected string strAction = "", strBaseInfor_Id = "", strSubTask_Id = "", strItem_Id = "", strMethold = "", strMetholdID = "", strPurPress = "", strSampleDate = "",
        strCompany_Id = "", strBoiler_Name = "", strFuelType = "", strHeight = "", strPosition = "", strSectionDiameter = "", strSectionArea = "",
        strMechie_Mode = "", strMechie_Code = "", strSample_Diameter = "", strEnv_Temperature = "", strAir_Pressure = "", strGovMethold = "", strMechieWindMea = "", strHumidityMea = "";
    protected string strAttInfor_Id = "", strUpdateCell = "", strUpdateCellValue = "", strModelNum = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestParams();
        if (!String.IsNullOrEmpty(strAction)) {

            switch (strAction) { 
                case "SaveBaseInfor":
                    Response.Write(SaveBaseInfor());
                    Response.End();
                    break;
                case "getBaseInfor":
                    Response.Write(getBaseInfor());
                    Response.End();
                    break;
                case "SaveAttInfor":
                    Response.Write(SaveAttInfor());
                    Response.End();
                    break;
                case "getAttInfor":
                    Response.Write(getAttInfor());
                    Response.End();
                    break;
                case "UpdateAttValue":
                    Response.Write(UpdateAttValue());
                    Response.End();
                    break;
                case "getCompanyInfor":
                    Response.Write(getCompanyInfor());
                    Response.End();
                    break;
                case "delAttInfor":
                    Response.Write(delAttInfor());
                    Response.End();
                    break;
                case "AvgAttInfor":
                    Response.Write(AvgAttInfor());
                    Response.End();
                    break;
                default: 
                    break;
            }
        }
    }

    /// <summary>
    /// 创建原因：创建或修改颗粒物原始记录表基础数据
    /// 创建人：胡方扬
    /// 创建日期：2013-07-09
    /// </summary>
    /// <returns></returns>
    public string SaveBaseInfor() {
        string result="";
        TMisMonitorDustinforVo objDust = new TMisMonitorDustinforVo();
        
        objDust.SUBTASK_ID = strSubTask_Id;
        objDust.ITEM_ID = strItem_Id;
        objDust.METHOLD_NAME = strMethold;
        objDust.METHOLD_ID = strMetholdID;
        objDust.PURPOSE = strPurPress;
        objDust.SAMPLE_DATE = strSampleDate;
        objDust.BOILER_NAME = strBoiler_Name;
        objDust.FUEL_TYPE = strFuelType;
        objDust.HEIGHT = strHeight;
        objDust.POSITION = strPosition;
        objDust.SECTION_DIAMETER = strSectionDiameter;
        objDust.SECTION_AREA = strSectionArea;
        objDust.MODUL_NUM = strModelNum;
        objDust.MECHIE_MODEL = strMechie_Mode;
        objDust.MECHIE_CODE = strMechie_Code;
        objDust.SAMPLE_POSITION_DIAMETER = strSample_Diameter;
        objDust.ENV_TEMPERATURE = strEnv_Temperature;
        objDust.AIR_PRESSURE = strAir_Pressure;
        objDust.GOVERM_METHOLD = strGovMethold;
        objDust.MECHIE_WIND_MEASURE = strMechieWindMea;
        objDust.HUMIDITY_MEASURE = strHumidityMea;

        if (String.IsNullOrEmpty(strBaseInfor_Id))
        {
            objDust.ID = GetSerialNumber("t_mis_DustyBaseInforID");
            if (new TMisMonitorDustinforLogic().Create(objDust))
            {
                TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                objResultVo.ID = strSubTask_Id;
                objResultVo.REMARK_5 = "Poll";
                new TMisMonitorResultLogic().Edit(objResultVo);
                result = objDust.ID;
            }
        }
        else {
            objDust.ID = strBaseInfor_Id;
            if (new TMisMonitorDustinforLogic().ObjEditNull(objDust))//huangjinjun update 20141106
            {
                result = strBaseInfor_Id;
            }
        }

        return result;
    }

    /// <summary>
    /// 创建原因：根据子任务ID和监测项目ID返回当前项目的原始记录表基础数据
    /// 创建人：胡方扬
    /// 创建日期：2013-07-09
    /// </summary>
    /// <returns></returns>
    public string getBaseInfor() {
        TMisMonitorDustinforVo objDust = new TMisMonitorDustinforVo();
        objDust.SUBTASK_ID = strSubTask_Id;
        objDust.ITEM_ID = strItem_Id;

        DataTable objDt = new TMisMonitorDustinforLogic().SelectByTable(objDust);
        int iCount = 0;
        if (objDt.Rows.Count > 0)
        {
            iCount = objDt.Rows.Count;
        }
        else
        {
            TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
            objResultVo.SAMPLE_ID = new TMisMonitorResultLogic().Details(strSubTask_Id).SAMPLE_ID;
            objResultVo.REMARK_5 = "Poll";
            DataTable dtResult = new TMisMonitorResultLogic().SelectByTable(objResultVo);
            //烟尘、粉尘、二氧化硫、氮氧化物、油烟：000001827,000001945  000000114,000000108 000000135
            DataRow[] drResult = dtResult.Select("ITEM_ID in('000001827','000001945','000000114','000000108','000000135')");
            for (int i = 0; i < drResult.Length; i++)
            {
                objDust = new TMisMonitorDustinforVo();
                objDust.SUBTASK_ID = drResult[i]["ID"].ToString();
                objDt = new TMisMonitorDustinforLogic().SelectByTable(objDust);
                if (objDt.Rows.Count > 0)
                    break;
            }
        }

        return LigerGridDataToJson(objDt, iCount);
    }

    /// <summary>
    /// 创建原因：根据监测项目基础属性ID，创建初始化属性数据信息
    /// 创建人：胡方扬
    /// 创建时间：2013-07-09
    /// </summary>
    /// <returns></returns>
    public string SaveAttInfor() {
        string result = "";
        if (!String.IsNullOrEmpty(strBaseInfor_Id)) {
            TMisMonitorDustattributeVo objDustAtt = new TMisMonitorDustattributeVo();
            objDustAtt.BASEINFOR_ID = strBaseInfor_Id;
            objDustAtt.SAMPLE_CODE = (GetDustyCoutForBaseInfor() + 1).ToString();
            objDustAtt.ID = GetSerialNumber("t_mis_dutyAttInforID");
            if (new TMisMonitorDustattributeLogic().Create(objDustAtt)) {
                result = objDustAtt.ID;
            }
        }
        return result;
    }
    /// <summary>
    /// 创建原因：计算原始记录表的平均数据
    /// 创建人：魏林
    /// 创建时间：2014-04-16
    /// </summary>
    /// <returns></returns>
    public string AvgAttInfor()
    {
        Regex r = new Regex("^\\d+(\\.)?\\d*$");
        bool iSuccess = true;
        decimal SMOKE_TEMPERATURE = 0, SMOKE_OXYGEN = 0, SMOKE_SPEED = 0, NM_SPEED = 0, L_STAND = 0;
        decimal SMOKE_POTENCY = 0, SMOKE_POTENCY2 = 0, SMOKE_DISCHARGE = 0;
        decimal SMOKE_TEMPERATURE_COut = 0, SMOKE_OXYGEN_COut = 0, SMOKE_SPEED_COut = 0, NM_SPEED_COut = 0, L_STAND_COut = 0, SMOKE_POTENCY_COut = 0, SMOKE_POTENCY2_COut = 0, SMOKE_DISCHARGE_COut = 0;
        int iPRECISION = 0; //平均值的小数位位数
        if (!String.IsNullOrEmpty(strBaseInfor_Id))
        {
            TMisMonitorDustattributeVo objDustAtt = new TMisMonitorDustattributeVo();
            objDustAtt.BASEINFOR_ID = strBaseInfor_Id;
            objDustAtt.SORT_FIELD = "ID";
            DataTable dt = new TMisMonitorDustattributeLogic().SelectByTable(objDustAtt);
            if (dt.Rows[dt.Rows.Count - 1]["SAMPLE_CODE"].ToString() == "平均")
            {
                objDustAtt.SORT_FIELD = "";
                objDustAtt.ID = dt.Rows[dt.Rows.Count - 1]["ID"].ToString();
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (r.IsMatch(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString()))
                        SMOKE_TEMPERATURE += decimal.Parse(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString());
                    else
                    {
                        SMOKE_TEMPERATURE_COut = GetNumber(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString());
                        SMOKE_TEMPERATURE += SMOKE_TEMPERATURE_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_OXYGEN"].ToString()))
                        SMOKE_OXYGEN += decimal.Parse(dt.Rows[i]["SMOKE_OXYGEN"].ToString());
                    else
                    {
                        SMOKE_OXYGEN_COut = GetNumber(dt.Rows[i]["SMOKE_OXYGEN"].ToString());
                        SMOKE_OXYGEN += SMOKE_OXYGEN_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_SPEED"].ToString()))
                        SMOKE_SPEED += decimal.Parse(dt.Rows[i]["SMOKE_SPEED"].ToString());
                    else
                    {
                        SMOKE_SPEED_COut = GetNumber(dt.Rows[i]["SMOKE_SPEED"].ToString());
                        SMOKE_SPEED += SMOKE_SPEED_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["NM_SPEED"].ToString()))
                        NM_SPEED += decimal.Parse(dt.Rows[i]["NM_SPEED"].ToString());
                    else
                    {
                        NM_SPEED_COut = GetNumber(dt.Rows[i]["NM_SPEED"].ToString());
                        NM_SPEED += NM_SPEED_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["L_STAND"].ToString()))
                        L_STAND += decimal.Parse(dt.Rows[i]["L_STAND"].ToString());
                    else
                    {
                        L_STAND_COut = GetNumber(dt.Rows[i]["L_STAND"].ToString());
                        L_STAND += L_STAND_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_POTENCY"].ToString()))
                        SMOKE_POTENCY += decimal.Parse(dt.Rows[i]["SMOKE_POTENCY"].ToString());
                    else
                    {
                        SMOKE_POTENCY_COut = GetNumber(dt.Rows[i]["SMOKE_POTENCY"].ToString());
                        SMOKE_POTENCY += SMOKE_POTENCY_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_POTENCY2"].ToString()))
                        SMOKE_POTENCY2 += decimal.Parse(dt.Rows[i]["SMOKE_POTENCY2"].ToString());
                    else
                    {
                        SMOKE_POTENCY2_COut = GetNumber(dt.Rows[i]["SMOKE_POTENCY2"].ToString());
                        SMOKE_POTENCY2 += SMOKE_POTENCY2_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_DISCHARGE"].ToString()))
                        SMOKE_DISCHARGE += decimal.Parse(dt.Rows[i]["SMOKE_DISCHARGE"].ToString());
                    else
                    {
                        SMOKE_DISCHARGE_COut = GetNumber(dt.Rows[i]["SMOKE_DISCHARGE"].ToString());
                        SMOKE_DISCHARGE += SMOKE_DISCHARGE_COut / 2;
                    }
                    
                }
                iPRECISION = dt.Rows[0]["SMOKE_TEMPERATURE"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_TEMPERATURE"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_TEMPERATURE_COut != 0 && Math.Round(SMOKE_TEMPERATURE / (dt.Rows.Count - 1), iPRECISION) < SMOKE_TEMPERATURE_COut)
                    objDustAtt.SMOKE_TEMPERATURE = SMOKE_TEMPERATURE_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_TEMPERATURE = Math.Round(SMOKE_TEMPERATURE / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_OXYGEN"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_OXYGEN"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_OXYGEN_COut != 0 && Math.Round(SMOKE_OXYGEN / (dt.Rows.Count - 1), iPRECISION) < SMOKE_OXYGEN_COut)
                    objDustAtt.SMOKE_OXYGEN = SMOKE_OXYGEN_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_OXYGEN = Math.Round(SMOKE_OXYGEN / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_SPEED"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_SPEED"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_SPEED_COut != 0 && Math.Round(SMOKE_SPEED / (dt.Rows.Count - 1), iPRECISION) < SMOKE_SPEED_COut)
                    objDustAtt.SMOKE_SPEED = SMOKE_SPEED_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_SPEED = Math.Round(SMOKE_SPEED / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["NM_SPEED"].ToString().Contains('.') ? dt.Rows[0]["NM_SPEED"].ToString().Split('.')[1].Length : 0;
                if (NM_SPEED_COut != 0 && Math.Round(NM_SPEED / (dt.Rows.Count - 1), iPRECISION) < NM_SPEED_COut)
                    objDustAtt.NM_SPEED = NM_SPEED_COut.ToString() + "(L)";
                else
                    objDustAtt.NM_SPEED = Math.Round(NM_SPEED / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["L_STAND"].ToString().Contains('.') ? dt.Rows[0]["L_STAND"].ToString().Split('.')[1].Length : 0;
                if (L_STAND_COut != 0 && Math.Round(L_STAND / (dt.Rows.Count - 1), iPRECISION) < L_STAND_COut)
                    objDustAtt.L_STAND = L_STAND_COut.ToString() + "(L)";
                else
                    objDustAtt.L_STAND = Math.Round(L_STAND / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_POTENCY"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_POTENCY"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_POTENCY_COut != 0 && Math.Round(SMOKE_POTENCY / (dt.Rows.Count - 1), iPRECISION) < SMOKE_POTENCY_COut)
                    objDustAtt.SMOKE_POTENCY = SMOKE_POTENCY_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_POTENCY = Math.Round(SMOKE_POTENCY / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_POTENCY2"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_POTENCY2"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_POTENCY2_COut != 0 && Math.Round(SMOKE_POTENCY2 / (dt.Rows.Count - 1), iPRECISION) < SMOKE_POTENCY2_COut)
                    objDustAtt.SMOKE_POTENCY2 = SMOKE_POTENCY2_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_POTENCY2 = Math.Round(SMOKE_POTENCY2 / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_DISCHARGE"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_DISCHARGE"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_DISCHARGE_COut != 0 && Math.Round(SMOKE_DISCHARGE / (dt.Rows.Count - 1), iPRECISION) < SMOKE_DISCHARGE_COut)
                    objDustAtt.SMOKE_DISCHARGE = SMOKE_DISCHARGE_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_DISCHARGE = Math.Round(SMOKE_DISCHARGE / (dt.Rows.Count - 1), iPRECISION).ToString();

                iSuccess = new TMisMonitorDustattributeLogic().Edit(objDustAtt);
            }
            else
            {
                objDustAtt.SORT_FIELD = "";
                objDustAtt.ID = GetSerialNumber("t_mis_dutyAttInforID");
                objDustAtt.SAMPLE_CODE = "平均";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (r.IsMatch(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString()))
                        SMOKE_TEMPERATURE += decimal.Parse(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString());
                    else
                    {
                        SMOKE_TEMPERATURE_COut = GetNumber(dt.Rows[i]["SMOKE_TEMPERATURE"].ToString());
                        SMOKE_TEMPERATURE += SMOKE_TEMPERATURE_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_OXYGEN"].ToString()))
                        SMOKE_OXYGEN += decimal.Parse(dt.Rows[i]["SMOKE_OXYGEN"].ToString());
                    else
                    {
                        SMOKE_OXYGEN_COut = GetNumber(dt.Rows[i]["SMOKE_OXYGEN"].ToString());
                        SMOKE_OXYGEN += SMOKE_OXYGEN_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_SPEED"].ToString()))
                        SMOKE_SPEED += decimal.Parse(dt.Rows[i]["SMOKE_SPEED"].ToString());
                    else
                    {
                        SMOKE_SPEED_COut = GetNumber(dt.Rows[i]["SMOKE_SPEED"].ToString());
                        SMOKE_SPEED += SMOKE_SPEED_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["NM_SPEED"].ToString()))
                        NM_SPEED += decimal.Parse(dt.Rows[i]["NM_SPEED"].ToString());
                    else
                    {
                        NM_SPEED_COut = GetNumber(dt.Rows[i]["NM_SPEED"].ToString());
                        NM_SPEED += NM_SPEED_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["L_STAND"].ToString()))
                        L_STAND += decimal.Parse(dt.Rows[i]["L_STAND"].ToString());
                    else
                    {
                        L_STAND_COut = GetNumber(dt.Rows[i]["L_STAND"].ToString());
                        L_STAND += L_STAND_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_POTENCY"].ToString()))
                        SMOKE_POTENCY += decimal.Parse(dt.Rows[i]["SMOKE_POTENCY"].ToString());
                    else
                    {
                        SMOKE_POTENCY_COut = GetNumber(dt.Rows[i]["SMOKE_POTENCY"].ToString());
                        SMOKE_POTENCY += SMOKE_POTENCY_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_POTENCY2"].ToString()))
                        SMOKE_POTENCY2 += decimal.Parse(dt.Rows[i]["SMOKE_POTENCY2"].ToString());
                    else
                    {
                        SMOKE_POTENCY2_COut = GetNumber(dt.Rows[i]["SMOKE_POTENCY2"].ToString());
                        SMOKE_POTENCY2 += SMOKE_POTENCY2_COut / 2;
                    }

                    if (r.IsMatch(dt.Rows[i]["SMOKE_DISCHARGE"].ToString()))
                        SMOKE_DISCHARGE += decimal.Parse(dt.Rows[i]["SMOKE_DISCHARGE"].ToString());
                    else
                    {
                        SMOKE_DISCHARGE_COut = GetNumber(dt.Rows[i]["SMOKE_DISCHARGE"].ToString());
                        SMOKE_DISCHARGE += SMOKE_DISCHARGE_COut / 2;
                    }
                }
                iPRECISION = dt.Rows[0]["SMOKE_TEMPERATURE"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_TEMPERATURE"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_TEMPERATURE_COut != 0 && Math.Round(SMOKE_TEMPERATURE / (dt.Rows.Count - 1), iPRECISION) < SMOKE_TEMPERATURE_COut)
                    objDustAtt.SMOKE_TEMPERATURE = SMOKE_TEMPERATURE_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_TEMPERATURE = Math.Round(SMOKE_TEMPERATURE / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_OXYGEN"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_OXYGEN"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_OXYGEN_COut != 0 && Math.Round(SMOKE_OXYGEN / (dt.Rows.Count - 1), iPRECISION) < SMOKE_OXYGEN_COut)
                    objDustAtt.SMOKE_OXYGEN = SMOKE_OXYGEN_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_OXYGEN = Math.Round(SMOKE_OXYGEN / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_SPEED"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_SPEED"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_SPEED_COut != 0 && Math.Round(SMOKE_SPEED / (dt.Rows.Count - 1), iPRECISION) < SMOKE_SPEED_COut)
                    objDustAtt.SMOKE_SPEED = SMOKE_SPEED_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_SPEED = Math.Round(SMOKE_SPEED / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["NM_SPEED"].ToString().Contains('.') ? dt.Rows[0]["NM_SPEED"].ToString().Split('.')[1].Length : 0;
                if (NM_SPEED_COut != 0 && Math.Round(NM_SPEED / (dt.Rows.Count - 1), iPRECISION) < NM_SPEED_COut)
                    objDustAtt.NM_SPEED = NM_SPEED_COut.ToString() + "(L)";
                else
                    objDustAtt.NM_SPEED = Math.Round(NM_SPEED / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["L_STAND"].ToString().Contains('.') ? dt.Rows[0]["L_STAND"].ToString().Split('.')[1].Length : 0;
                if (L_STAND_COut != 0 && Math.Round(L_STAND / (dt.Rows.Count - 1), iPRECISION) < L_STAND_COut)
                    objDustAtt.L_STAND = L_STAND_COut.ToString() + "(L)";
                else
                    objDustAtt.L_STAND = Math.Round(L_STAND / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_POTENCY"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_POTENCY"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_POTENCY_COut != 0 && Math.Round(SMOKE_POTENCY / (dt.Rows.Count - 1), iPRECISION) < SMOKE_POTENCY_COut)
                    objDustAtt.SMOKE_POTENCY = SMOKE_POTENCY_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_POTENCY = Math.Round(SMOKE_POTENCY / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_POTENCY2"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_POTENCY2"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_POTENCY2_COut != 0 && Math.Round(SMOKE_POTENCY2 / (dt.Rows.Count - 1), iPRECISION) < SMOKE_POTENCY2_COut)
                    objDustAtt.SMOKE_POTENCY2 = SMOKE_POTENCY2_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_POTENCY2 = Math.Round(SMOKE_POTENCY2 / (dt.Rows.Count - 1), iPRECISION).ToString();

                iPRECISION = dt.Rows[0]["SMOKE_DISCHARGE"].ToString().Contains('.') ? dt.Rows[0]["SMOKE_DISCHARGE"].ToString().Split('.')[1].Length : 0;
                if (SMOKE_DISCHARGE_COut != 0 && Math.Round(SMOKE_DISCHARGE / (dt.Rows.Count - 1), iPRECISION) < SMOKE_DISCHARGE_COut)
                    objDustAtt.SMOKE_DISCHARGE = SMOKE_DISCHARGE_COut.ToString() + "(L)";
                else
                    objDustAtt.SMOKE_DISCHARGE = Math.Round(SMOKE_DISCHARGE / (dt.Rows.Count - 1), iPRECISION).ToString();

                iSuccess = new TMisMonitorDustattributeLogic().Create(objDustAtt);
            }
            
            //更新结果表
            TMisMonitorDustinforVo objDustinforVo = new TMisMonitorDustinforLogic().Details(strBaseInfor_Id);
            TMisMonitorResultVo objResultVo = new TMisMonitorResultLogic().Details(objDustinforVo.SUBTASK_ID);
            if (objDustinforVo.MODUL_NUM.Length > 0)
                objResultVo.ITEM_RESULT = objDustAtt.SMOKE_POTENCY2;
            else
                objResultVo.ITEM_RESULT = objDustAtt.SMOKE_POTENCY;
            iSuccess = new TMisMonitorResultLogic().Edit(objResultVo);
        }
        return iSuccess ? "1" : "0";
    }

    /// <summary>
    /// 创建原因：获取当前已经存在的属性样品总条数
    /// 创建人：胡方扬
    /// 创建日期：2013-07-09
    /// </summary>
    /// <returns></returns>
    public int GetDustyCoutForBaseInfor() {
        int CountNum = 0;
        if (!String.IsNullOrEmpty(strBaseInfor_Id)) {
            DataTable dt = new TMisMonitorDustattributeLogic().SelectByTable(new TMisMonitorDustattributeVo { BASEINFOR_ID = strBaseInfor_Id });
            CountNum = dt.Rows.Count;
        }

        return CountNum;
    }
    /// <summary>
    /// 创建原因：根据指定监测项目属性ID 更新指定列的数据
    /// 创建人：胡方扬
    /// 创建时间：2013-07-09
    /// </summary>
    /// <returns></returns>
    public bool UpdateAttValue() {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strAttInfor_Id)&&!String.IsNullOrEmpty(strUpdateCell)) {
            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, strUpdateCell, strUpdateCellValue);

            Regex r = new Regex("^\\d+(\\.)?\\d*$");
            TMisMonitorDustattributeVo DustattributeVo = new TMisMonitorDustattributeVo();
            DustattributeVo = new TMisMonitorDustattributeLogic().Details(strAttInfor_Id);
            if (blFlag)
            {
                switch (strUpdateCell)
                {
                    case "FITER_BEGIN_WEIGHT":   //初重
                    case "FITER_AFTER_WEIGHT":   //终重
                        //计算样品重量：终重-初重
                        decimal dWEIGHT_BEGIN = r.IsMatch(DustattributeVo.FITER_BEGIN_WEIGHT) ? decimal.Parse(DustattributeVo.FITER_BEGIN_WEIGHT) : 0;
                        decimal dWEIGHT_END = r.IsMatch(DustattributeVo.FITER_AFTER_WEIGHT) ? decimal.Parse(DustattributeVo.FITER_AFTER_WEIGHT) : 0;
                        decimal dWEIGHT = Math.Abs(dWEIGHT_END - dWEIGHT_BEGIN);
                        blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SAMPLE_WEIGHT", dWEIGHT.ToString());
                        //计算烟尘浓度：（样品重量/标况体积）*1000000
                        decimal dL_STAND = r.IsMatch(DustattributeVo.L_STAND) ? decimal.Parse(DustattributeVo.L_STAND) : 0;
                        decimal dPOTENCY = 0;
                        if (dL_STAND != 0)
                        {
                            dPOTENCY = (dWEIGHT / dL_STAND) * 1000000;
                            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_POTENCY", Math.Round(dPOTENCY, 0).ToString());
                        }
                        //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数
                        decimal dOXYGEN = r.IsMatch(DustattributeVo.SMOKE_OXYGEN) ? decimal.Parse(DustattributeVo.SMOKE_OXYGEN) : 0;
                        TMisMonitorDustinforVo DustinforVo = new TMisMonitorDustinforLogic().Details(DustattributeVo.BASEINFOR_ID);
                        decimal dMODUL_NUM = r.IsMatch(DustinforVo.MODUL_NUM) ? decimal.Parse(DustinforVo.MODUL_NUM) : 0;
                        decimal dPOTENCY2 = 0;
                        if (dMODUL_NUM != 0)
                        {
                            dPOTENCY2 = ((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM;
                            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_POTENCY2", Math.Round(dPOTENCY2, 0).ToString());
                        }
                        //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                        decimal dSPEED = r.IsMatch(DustattributeVo.NM_SPEED) ? decimal.Parse(DustattributeVo.NM_SPEED) : 0;
                        blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_DISCHARGE", Math.Round((dPOTENCY * dSPEED) / 1000000, 3).ToString());
                        break;
                    case "SAMPLE_WEIGHT":        //样品重量
                        //计算烟尘浓度：（样品重量/标况体积）*1000000
                        dL_STAND = r.IsMatch(DustattributeVo.L_STAND) ? decimal.Parse(DustattributeVo.L_STAND) : 0;
                        dWEIGHT = r.IsMatch(DustattributeVo.SAMPLE_WEIGHT) ? decimal.Parse(DustattributeVo.SAMPLE_WEIGHT) : 0;
                        dPOTENCY = 0;
                        if (dL_STAND != 0)
                        {
                            dPOTENCY = (dWEIGHT / dL_STAND) * 1000000;
                            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_POTENCY", Math.Round(dPOTENCY, 0).ToString());
                        }
                        //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数
                        dOXYGEN = r.IsMatch(DustattributeVo.SMOKE_OXYGEN) ? decimal.Parse(DustattributeVo.SMOKE_OXYGEN) : 0;
                        DustinforVo = new TMisMonitorDustinforLogic().Details(DustattributeVo.BASEINFOR_ID);
                        dMODUL_NUM = r.IsMatch(DustinforVo.MODUL_NUM) ? decimal.Parse(DustinforVo.MODUL_NUM) : 0;
                        dPOTENCY2 = 0;
                        if (dMODUL_NUM != 0)
                        {
                            dPOTENCY2 = ((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM;
                            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_POTENCY2", Math.Round(dPOTENCY2, 0).ToString());
                        }
                        //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                        dSPEED = r.IsMatch(DustattributeVo.NM_SPEED) ? decimal.Parse(DustattributeVo.NM_SPEED) : 0;
                        blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_DISCHARGE", Math.Round((dPOTENCY * dSPEED) / 1000000, 3).ToString());
                        break;
                    case "SMOKE_POTENCY":        //烟尘浓度
                        //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数
                        dPOTENCY = r.IsMatch(DustattributeVo.SMOKE_POTENCY) ? decimal.Parse(DustattributeVo.SMOKE_POTENCY) : 0;
                        dOXYGEN = r.IsMatch(DustattributeVo.SMOKE_OXYGEN) ? decimal.Parse(DustattributeVo.SMOKE_OXYGEN) : 0;
                        DustinforVo = new TMisMonitorDustinforLogic().Details(DustattributeVo.BASEINFOR_ID);
                        dMODUL_NUM = r.IsMatch(DustinforVo.MODUL_NUM) ? decimal.Parse(DustinforVo.MODUL_NUM) : 0;
                        dPOTENCY2 = 0;
                        if (dMODUL_NUM != 0)
                        {
                            dPOTENCY2 = ((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM;
                            blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_POTENCY2", Math.Round(dPOTENCY2, 0).ToString());
                        }
                        else
                        {
                            if (DustattributeVo.SAMPLE_CODE == "平均")
                            {
                                //更新结果表
                                TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                                objResultVo.ID = DustinforVo.SUBTASK_ID;
                                objResultVo.ITEM_RESULT = strUpdateCellValue;
                                blFlag = new TMisMonitorResultLogic().Edit(objResultVo);
                            }
                        }
                        //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                        dSPEED = r.IsMatch(DustattributeVo.NM_SPEED) ? decimal.Parse(DustattributeVo.NM_SPEED) : 0;
                        blFlag = new TMisMonitorDustattributeLogic().UpdateCell(strAttInfor_Id, "SMOKE_DISCHARGE", Math.Round((dPOTENCY * dSPEED) / 1000000, 3).ToString());
                        break;
                    case "SMOKE_POTENCY2":
                        DustinforVo = new TMisMonitorDustinforLogic().Details(DustattributeVo.BASEINFOR_ID);
                        dMODUL_NUM = r.IsMatch(DustinforVo.MODUL_NUM) ? decimal.Parse(DustinforVo.MODUL_NUM) : 0;
                        if (dMODUL_NUM != 0 && DustattributeVo.SAMPLE_CODE == "平均")
                        {
                            //更新结果表
                            TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                            objResultVo.ID = DustinforVo.SUBTASK_ID;
                            objResultVo.ITEM_RESULT = strUpdateCellValue;
                            blFlag = new TMisMonitorResultLogic().Edit(objResultVo);
                        }
                        break;
                    default:
                        break;
                }
                
            }
        }
        return blFlag;
    }

    /// <summary>
    /// 创建原因：根据原始记录表基本数据ID获取当前项目的属性表数据信息
    /// 创建人：胡方扬
    /// 创建日期：2013-07-09
    /// </summary>
    /// <returns></returns>
    public string getAttInfor() {
        string result = "";
        if (!String.IsNullOrEmpty(strBaseInfor_Id))
        {
            TMisMonitorDustattributeVo objDustAtt = new TMisMonitorDustattributeVo();
            objDustAtt.BASEINFOR_ID = strBaseInfor_Id;
            objDustAtt.SORT_FIELD = "ID";
            objDustAtt.SORT_TYPE = "desc";
            DataTable objDt = new TMisMonitorDustattributeLogic().SelectByTable(objDustAtt,intPageIndex,intPageSize);
            int CountNum = new TMisMonitorDustattributeLogic().GetSelectResultCount(objDustAtt);
            result= LigerGridDataToJson(objDt,CountNum);
        }
        return result;
    }

    /// <summary>
    /// 创建原因：根据属性表ID进行数据删除
    /// 创建人：胡方扬
    /// 创建日期：2013-07-10
    /// </summary>
    /// <returns></returns>
    public bool delAttInfor() {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strAttInfor_Id)) {
            if (new TMisMonitorDustattributeLogic().Delete(strAttInfor_Id)) {
                blFlag = true;
            }
        }
        return blFlag;
    }
    /// <summary>
    /// 创建原因：根据子任务ID获取企业信息
    /// 创建人：胡方扬
    /// 创建时间：2013-07-09
    /// </summary>
    /// <returns></returns>
    public string getCompanyInfor() {
        string result = "";
        if (!String.IsNullOrEmpty(strSubTask_Id))
        {
            TMisMonitorResultVo objResult = new TMisMonitorResultLogic().Details(strSubTask_Id);
            TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoLogic().Details(objResult.SAMPLE_ID);
            TMisMonitorSubtaskVo objSubTask = new TMisMonitorSubtaskLogic().Details(objSample.SUBTASK_ID);
            TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubTask.TASK_ID);

            TMisMonitorTaskCompanyVo objCompany = new TMisMonitorTaskCompanyVo();
            objCompany.ID = objTask.TESTED_COMPANY_ID;

            DataTable dt = new TMisMonitorTaskCompanyLogic().SelectByTable(objCompany);
            dt.Columns.Add("SAMPLE_CODE");
            dt.Rows[0]["SAMPLE_CODE"] = objSample.SAMPLE_CODE;
            result= LigerGridDataToJson(dt, dt.Rows.Count);
        }

        return result;
    }


    /// <summary>
    /// 创建原因：获取URL参数，赋值给公用变量
    /// 创建人：胡方扬
    /// 创建时间：2013-07-09
    /// </summary>
    protected void GetRequestParams() 
    {
        if (!String.IsNullOrEmpty(Request.Params["sortnamer"]))
        {
            strSortname = Request.Params["sortnamer"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["sortorder"]))
        {
            strSortorder = Request.Params["sortorder"].Trim();
        }
        //当前页面
        if (!String.IsNullOrEmpty(Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(Request.Params["page"].Trim());
        }
        //每页记录数
        if (!String.IsNullOrEmpty(Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(Request.Params["pagesize"].Trim());
        }
        if (!String.IsNullOrEmpty(Request.Params["action"]))
        {
            strAction = Request.Params["action"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strBaseInfor_Id"])) 
        {
            strBaseInfor_Id = Request.Params["strBaseInfor_Id"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSubTask_Id"]))
        {
            strSubTask_Id = Request.Params["strSubTask_Id"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strItem_Id"]))
        {
            strItem_Id = Request.Params["strItem_Id"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMethold"]))
        {
            strMethold = Request.Params["strMethold"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMetholdID"]))
        {
            strMetholdID = Request.Params["strMetholdID"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPurPress"]))
        {
            strPurPress = Request.Params["strPurPress"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSampleDate"]))
        {
            strSampleDate = Request.Params["strSampleDate"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strBoiler_Name"]))
        {
            strBoiler_Name = Request.Params["strBoiler_Name"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strFuelType"]))
        {
            strFuelType = Request.Params["strFuelType"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strHeight"]))
        {
            strHeight = Request.Params["strHeight"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strPosition"]))
        {
            strPosition = Request.Params["strPosition"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSectionDiameter"]))
        {
            strSectionDiameter = Request.Params["strSectionDiameter"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSectionArea"]))
        {
            strSectionArea = Request.Params["strSectionArea"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMechie_Mode"]))
        {
            strMechie_Mode = Request.Params["strMechie_Mode"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMechie_Code"]))
        {
            strMechie_Code = Request.Params["strMechie_Code"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strSample_Diameter"]))
        {
            strSample_Diameter = Request.Params["strSample_Diameter"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strEnv_Temperature"]))
        {
            strEnv_Temperature = Request.Params["strEnv_Temperature"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strAir_Pressure"]))
        {
            strAir_Pressure = Request.Params["strAir_Pressure"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strGovMethold"]))
        {
            strGovMethold = Request.Params["strGovMethold"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strMechieWindMea"]))
        {
            strMechieWindMea = Request.Params["strMechieWindMea"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strHumidityMea"]))
        {
            strHumidityMea = Request.Params["strHumidityMea"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strAttInfor_Id"]))
        {
            strAttInfor_Id = Request.Params["strAttInfor_Id"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strUpdateCell"]))
        {
            strUpdateCell = Request.Params["strUpdateCell"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strUpdateCellValue"]))
        {
            strUpdateCellValue = Request.Params["strUpdateCellValue"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strModelNum"]))
        {
            strModelNum = Request.Params["strModelNum"].Trim().ToString();
        }
    }
}