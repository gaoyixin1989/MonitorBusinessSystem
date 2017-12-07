using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverCity;
using i3.BusinessLogic.Channels.Env.Point.RiverCity;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：城考断面信息编辑
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverCity_RiverCityEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] == null)
        {
            this.formStatus.Value = "add";
        }
        else
        {
            this.formStatus.Value = "update";
            this.formId.Value = this.Request["id"].ToString();
        }

        //获取下拉列表信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict(Request["dictType"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取年度下拉列表数据
        if (Request["type"] != null && Request["type"].ToString() == "getYearInfo")
        {
            strResult = getYearInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取月度下拉列表数据
        if (Request["type"] != null && Request["type"].ToString() == "getMonthInfo")
        {
            strResult = getMonthInfo();
            Response.Write(strResult);
            Response.End();
        }
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
        //增加数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "add")
        {
            strResult = frmAdd();
            Response.Write(strResult);
            Response.End();
        }
        //修改数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "update")
        {
            strResult = frmUpdate();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPRiverCityVo TEnvPRiverCityVo = new TEnvPRiverCityVo();
        TEnvPRiverCityVo.ID = Request["id"].ToString();
        TEnvPRiverCityVo.IS_DEL = "0";
        TEnvPRiverCityVo TEnvPRiverCityVoTemp = new TEnvPRiverCityLogic().Details(TEnvPRiverCityVo);
        //条件项ID
        string strConditionId = TEnvPRiverCityVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPRiverCityVoTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverCityVoTemp.VALLEY_ID).DICT_TEXT;
        TEnvPRiverCityVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverCityVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPRiverCityVoTemp.REMARK3 = strRiver;

        return ToJson(TEnvPRiverCityVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    { 
        TEnvPRiverCityVo TEnvPRiverCity = autoBindRequest(Request, new TEnvPRiverCityVo());
        TEnvPRiverCity.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPRiverCityVo.T_ENV_P_RIVER_CITY_TABLE, TEnvPRiverCity.YEAR, TEnvPRiverCity.SelectMonths, TEnvPRiverCityVo.SECTION_NAME_FIELD, TEnvPRiverCity.SECTION_NAME, TEnvPRiverCityVo.SECTION_CODE_FIELD, TEnvPRiverCity.SECTION_CODE, TEnvPRiverCityVo.ID_FIELD, TEnvPRiverCity.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverCityLogic().Create(TEnvPRiverCity, SerialType.T_ENV_P_RIVER_CITY);
            if (isSuccess)
            {
                WriteLog("添加城考断面信息", "", LogInfo.UserInfo.USER_NAME + "添加城考断面信息" + TEnvPRiverCity.ID);
                strMsg = "数据保存成功";
            }
            else
                strMsg = "数据保存失败";
        }
        
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPRiverCityVo TEnvPRiverCity = autoBindRequest(Request, new TEnvPRiverCityVo());
        TEnvPRiverCity.ID = Request["id"].ToString();
        TEnvPRiverCity.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPRiverCity.YEAR || hidMonth.Value.Trim() != TEnvPRiverCity.MONTH || hidValue.Value.Trim() != TEnvPRiverCity.SECTION_NAME || hidCode.Value.Trim() != TEnvPRiverCity.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPRiverCityVo.T_ENV_P_RIVER_CITY_TABLE, TEnvPRiverCity.YEAR, TEnvPRiverCity.MONTH, TEnvPRiverCityVo.SECTION_NAME_FIELD, TEnvPRiverCity.SECTION_NAME, TEnvPRiverCityVo.SECTION_CODE_FIELD, TEnvPRiverCity.SECTION_CODE, TEnvPRiverCityVo.ID_FIELD, TEnvPRiverCity.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverCityLogic().Edit(TEnvPRiverCity);
            if (isSuccess)
            {
                WriteLog("编辑城考断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑城考断面监测点" + TEnvPRiverCity.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}