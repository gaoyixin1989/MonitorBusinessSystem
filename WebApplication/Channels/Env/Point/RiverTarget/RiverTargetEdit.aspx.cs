using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverTarget;
using i3.BusinessLogic.Channels.Env.Point.RiverTarget;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：责任目标断面信息编辑
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverTarget_RiverTargetEdit : PageBase
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
        TEnvPRiverTargetVo TEnvPRiverTargetVo = new TEnvPRiverTargetVo();
        TEnvPRiverTargetVo.ID = Request["id"].ToString();
        TEnvPRiverTargetVo.IS_DEL = "0";
        TEnvPRiverTargetVo TEnvPRiverTargetVoTemp = new TEnvPRiverTargetLogic().Details(TEnvPRiverTargetVo);
        //条件项ID
        string strConditionId = TEnvPRiverTargetVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPRiverTargetVoTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverTargetVoTemp.VALLEY_ID).DICT_TEXT;
        TEnvPRiverTargetVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverTargetVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPRiverTargetVoTemp.REMARK3 = strRiver;

        return ToJson(TEnvPRiverTargetVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    { 
        TEnvPRiverTargetVo TEnvPRiverTarget = autoBindRequest(Request, new TEnvPRiverTargetVo());
        TEnvPRiverTarget.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPRiverTargetVo.T_ENV_P_RIVER_TARGET_TABLE, TEnvPRiverTarget.YEAR, TEnvPRiverTarget.SelectMonths, TEnvPRiverTargetVo.SECTION_NAME_FIELD, TEnvPRiverTarget.SECTION_NAME, TEnvPRiverTargetVo.SECTION_CODE_FIELD, TEnvPRiverTarget.SECTION_CODE, TEnvPRiverTargetVo.ID_FIELD, TEnvPRiverTarget.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverTargetLogic().Create(TEnvPRiverTarget, SerialType.T_ENV_P_RIVER_TARGET);
            if (isSuccess)
            {
                WriteLog("添加责任目标断面信息", "", LogInfo.UserInfo.USER_NAME + "添加责任目标断面信息" + TEnvPRiverTarget.ID);
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
        TEnvPRiverTargetVo TEnvPRiverTarget = autoBindRequest(Request, new TEnvPRiverTargetVo());
        TEnvPRiverTarget.ID = Request["id"].ToString();
        TEnvPRiverTarget.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPRiverTarget.YEAR || hidMonth.Value.Trim() != TEnvPRiverTarget.MONTH || hidValue.Value.Trim() != TEnvPRiverTarget.SECTION_NAME || hidCode.Value.Trim() != TEnvPRiverTarget.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPRiverTargetVo.T_ENV_P_RIVER_TARGET_TABLE, TEnvPRiverTarget.YEAR, TEnvPRiverTarget.MONTH, TEnvPRiverTargetVo.SECTION_NAME_FIELD, TEnvPRiverTarget.SECTION_NAME, TEnvPRiverTargetVo.SECTION_CODE_FIELD, TEnvPRiverTarget.SECTION_CODE, TEnvPRiverTargetVo.ID_FIELD, TEnvPRiverTarget.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverTargetLogic().Edit(TEnvPRiverTarget);
            if (isSuccess)
            {
                WriteLog("编辑责任目标断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑责任目标断面监测点" + TEnvPRiverTarget.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}