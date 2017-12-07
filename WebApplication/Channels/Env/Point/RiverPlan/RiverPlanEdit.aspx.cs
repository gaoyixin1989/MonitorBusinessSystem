using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverPlan;
using i3.BusinessLogic.Channels.Env.Point.RiverPlan;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：规划断面信息编辑
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverPlan_RiverPlanEdit : PageBase
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
        TEnvPRiverPlanVo TEnvPRiverPlanVo = new TEnvPRiverPlanVo();
        TEnvPRiverPlanVo.ID = Request["id"].ToString();
        TEnvPRiverPlanVo.IS_DEL = "0";
        TEnvPRiverPlanVo TEnvPRiverPlanVoTemp = new TEnvPRiverPlanLogic().Details(TEnvPRiverPlanVo);
        //条件项ID
        string strConditionId = TEnvPRiverPlanVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPRiverPlanVoTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverPlanVoTemp.VALLEY_ID).DICT_TEXT;
        TEnvPRiverPlanVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverPlanVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPRiverPlanVoTemp.REMARK3 = strRiver;

        return ToJson(TEnvPRiverPlanVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    { 
        TEnvPRiverPlanVo TEnvPRiverPlan = autoBindRequest(Request, new TEnvPRiverPlanVo());
        TEnvPRiverPlan.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE, TEnvPRiverPlan.YEAR, TEnvPRiverPlan.SelectMonths, TEnvPRiverPlanVo.SECTION_NAME_FIELD, TEnvPRiverPlan.SECTION_NAME, TEnvPRiverPlanVo.SECTION_CODE_FIELD, TEnvPRiverPlan.SECTION_CODE, TEnvPRiverPlanVo.ID_FIELD, TEnvPRiverPlan.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverPlanLogic().Create(TEnvPRiverPlan, SerialType.T_ENV_P_RIVER_PLAN);
            if (isSuccess)
            {
                WriteLog("添加规划断面信息", "", LogInfo.UserInfo.USER_NAME + "添加规划断面信息" + TEnvPRiverPlan.ID);
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
        TEnvPRiverPlanVo TEnvPRiverPlan = autoBindRequest(Request, new TEnvPRiverPlanVo());
        TEnvPRiverPlan.ID = Request["id"].ToString();
        TEnvPRiverPlan.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPRiverPlan.YEAR || hidMonth.Value.Trim() != TEnvPRiverPlan.MONTH || hidValue.Value.Trim() != TEnvPRiverPlan.SECTION_NAME || hidCode.Value.Trim() != TEnvPRiverPlan.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPRiverPlanVo.T_ENV_P_RIVER_PLAN_TABLE, TEnvPRiverPlan.YEAR, TEnvPRiverPlan.MONTH, TEnvPRiverPlanVo.SECTION_NAME_FIELD, TEnvPRiverPlan.SECTION_NAME, TEnvPRiverPlanVo.SECTION_CODE_FIELD, TEnvPRiverPlan.SECTION_CODE, TEnvPRiverPlanVo.ID_FIELD, TEnvPRiverPlan.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverPlanLogic().Edit(TEnvPRiverPlan);
            if (isSuccess)
            {
                WriteLog("编辑规划断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑规划断面监测点" + TEnvPRiverPlan.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}