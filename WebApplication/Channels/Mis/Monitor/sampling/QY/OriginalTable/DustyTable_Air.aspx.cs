using System;
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
/// 创建原因：实现大气采样原始记录表的填写
/// 创建人：魏林
/// 创建时间：2014-09-22 
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_OriginalTable_DustyTable_Air : PageBase
{
    public string  strSortname="",strSortorder="";
    public int intPageIndex = 0, intPageSize = 0;
    protected string strAction = "", strBaseInfor_Id = "", strSubTask_Id = "", strItem_Id = "", strMethold = "", strMetholdID = "", strPurPress = "", strSampleDate = "",
        strCompany_Id = "", strBoiler_Name = "", strFuelType = "", strHeight = "", strPosition = "", strSectionDiameter = "", strSectionArea = "",
        strMechie_Mode = "", strMechie_Code = "", strSample_Diameter = "", strEnv_Temperature = "", strAir_Pressure = "", strGovMethold = "", strMechieWindMea = "", strHumidityMea = "",strWeather="",strWindDrict="";
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
                case "UpdateAttValue":
                    Response.Write(UpdateAttValue());
                    Response.End();
                    break;
                case "getAttInfor":
                    Response.Write(getAttInfor());
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
        objDust.WEATHER = strWeather;
        objDust.WINDDRICT = strWindDrict;
        if (String.IsNullOrEmpty(strBaseInfor_Id))
        {
            objDust.ID = GetSerialNumber("t_mis_DustyBaseInforID");
            if (new TMisMonitorDustinforLogic().Create(objDust))
            {
                TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
                objResultVo.ID = strSubTask_Id;
                objResultVo.REMARK_5 = "Air";
                new TMisMonitorResultLogic().Edit(objResultVo);
                result = objDust.ID;
            }
        }
        else {
            objDust.ID = strBaseInfor_Id;
            if (new TMisMonitorDustinforLogic().Edit(objDust)) {
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
            objResultVo.REMARK_5 = "Air";
            DataTable dtResult = new TMisMonitorResultLogic().SelectByTable(objResultVo);
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objDust = new TMisMonitorDustinforVo();
                objDust.SUBTASK_ID = dtResult.Rows[i]["ID"].ToString();
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
            TMisMonitorDustattributePmVo objDustAtt = new TMisMonitorDustattributePmVo();
            objDustAtt.BASEINFOR_ID = strBaseInfor_Id;
            objDustAtt.SAMPLE_CODE = (GetDustyCoutForBaseInfor() + 1).ToString();
            objDustAtt.ID = GetSerialNumber("t_mis_dutyAttPMInforID");
            if (new TMisMonitorDustattributePmLogic().Create(objDustAtt))
            {
                result = objDustAtt.ID;
            }
        }
        return result;
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
            DataTable dt = new TMisMonitorDustattributePmLogic().SelectByTable(new TMisMonitorDustattributePmVo { BASEINFOR_ID = strBaseInfor_Id });
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
    public bool UpdateAttValue()
    {
        bool blFlag = false;
        if (!String.IsNullOrEmpty(strAttInfor_Id) && !String.IsNullOrEmpty(strUpdateCell))
        {
            blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, strUpdateCell, strUpdateCellValue);
            if (blFlag)
            {
                if (strUpdateCell == "SAMPLE_FWEIGHT" || strUpdateCell == "SAMPLE_EWEIGHT")
                {
                    //样品重量=初重-终重
                    Regex r = new Regex("^\\d+(\\.)?\\d*$");
                    TMisMonitorDustattributePmVo DustattributePmVo = new TMisMonitorDustattributePmVo();
                    DustattributePmVo = new TMisMonitorDustattributePmLogic().Details(strAttInfor_Id);
                    decimal dSAMPLE_FWEIGHT = r.IsMatch(DustattributePmVo.SAMPLE_FWEIGHT) ? decimal.Parse(DustattributePmVo.SAMPLE_FWEIGHT) : 0;
                    decimal dSAMPLE_EWEIGHT = r.IsMatch(DustattributePmVo.SAMPLE_EWEIGHT) ? decimal.Parse(DustattributePmVo.SAMPLE_EWEIGHT) : 0;
                    decimal dSAMPLE_WEIGHT = Math.Abs(dSAMPLE_EWEIGHT - dSAMPLE_FWEIGHT);

                    blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, "SAMPLE_WEIGHT", dSAMPLE_WEIGHT.ToString());
                    if (blFlag)
                    {
                        //样品浓度=样品重量/标况体积
                        decimal dL_STAND = r.IsMatch(DustattributePmVo.L_STAND) ? decimal.Parse(DustattributePmVo.L_STAND) : 0;
                        if (dL_STAND != 0)
                        {
                            if (((dSAMPLE_WEIGHT / dL_STAND) * 1000000) > 1 || ((dSAMPLE_WEIGHT / dL_STAND) * 1000000) == 1)
                            {//何海亮修改
                                blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, "SAMPLE_CONCENT", Math.Round((dSAMPLE_WEIGHT / dL_STAND) * 1000000, 2).ToString());
                            }
                            else
                            {
                                blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, "SAMPLE_CONCENT", Math.Round((dSAMPLE_WEIGHT / dL_STAND) * 1000000, 3).ToString());
                            }
                        }
                    }
                }
                if (strUpdateCell == "SAMPLE_WEIGHT")
                {
                    //样品浓度=样品重量/标况体积
                    Regex r = new Regex("^\\d+(\\.)?\\d*$");
                    TMisMonitorDustattributePmVo DustattributePmVo = new TMisMonitorDustattributePmVo();
                    DustattributePmVo = new TMisMonitorDustattributePmLogic().Details(strAttInfor_Id);
                    decimal dL_STAND = r.IsMatch(DustattributePmVo.L_STAND) ? decimal.Parse(DustattributePmVo.L_STAND) : 0;
                    decimal dSAMPLE_WEIGHT = r.IsMatch(DustattributePmVo.SAMPLE_WEIGHT) ? decimal.Parse(DustattributePmVo.SAMPLE_WEIGHT) : 0;
                    if (dL_STAND != 0)
                    {
                        if (((dSAMPLE_WEIGHT / dL_STAND) * 1000000) > 1 || ((dSAMPLE_WEIGHT / dL_STAND) * 1000000) == 1)//何海亮修改
                        {
                            blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, "SAMPLE_CONCENT", Math.Round((dSAMPLE_WEIGHT / dL_STAND) * 1000000, 2).ToString());

                        }
                        else
                        {
                            blFlag = new TMisMonitorDustattributePmLogic().UpdateCell(strAttInfor_Id, "SAMPLE_CONCENT", Math.Round((dSAMPLE_WEIGHT / dL_STAND) * 1000000, 3).ToString());
                        }
                    }
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
            TMisMonitorDustattributePmVo objDustAtt = new TMisMonitorDustattributePmVo();
            objDustAtt.BASEINFOR_ID = strBaseInfor_Id;
            objDustAtt.SORT_FIELD = "ID";
            objDustAtt.SORT_TYPE = "";
            DataTable objDt = new TMisMonitorDustattributePmLogic().SelectByTable(objDustAtt, intPageIndex, intPageSize);
            int CountNum = new TMisMonitorDustattributePmLogic().GetSelectResultCount(objDustAtt);
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
            if (new TMisMonitorDustattributePmLogic().Delete(strAttInfor_Id))
            {
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
        if (!String.IsNullOrEmpty(Request.Params["strWeather"]))
        {
            strWeather = Request.Params["strWeather"].Trim().ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["strWindDrict"]))
        {
            strWindDrict = Request.Params["strWindDrict"].Trim().ToString();
        }
    }
}