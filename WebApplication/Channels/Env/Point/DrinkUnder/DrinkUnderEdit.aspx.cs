using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.DrinkUnder;
using i3.BusinessLogic.Channels.Env.Point.DrinkUnder;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：地下饮用水点位信息编辑
/// 创建日期：2013-06-14
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_DrinkUnder_DrinkUnderEdit : PageBase
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
        TEnvPDrinkUnderVo TEnvPDrinkUnder = new TEnvPDrinkUnderVo();
        TEnvPDrinkUnder.ID = Request["id"].ToString();
        TEnvPDrinkUnder.IS_DEL = "0";
        TEnvPDrinkUnderVo TEnvPDrinkUnderTemp = new TEnvPDrinkUnderLogic().Details(TEnvPDrinkUnder);
        //条件项ID
        string strConditionId = TEnvPDrinkUnderTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPDrinkUnderTemp.REMARK1 = strStanderName;

        //获取流域名称
        //string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverVoTemp.VALLEY_ID).DICT_TEXT;
        //TEnvPRiverVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPDrinkUnderTemp.RIVER_ID).DICT_TEXT;
        TEnvPDrinkUnderTemp.REMARK3 = strRiver;

        return ToJson(TEnvPDrinkUnderTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPDrinkUnderVo TEnvPDrinkUnder = autoBindRequest(Request, new TEnvPDrinkUnderVo());
        TEnvPDrinkUnder.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE, TEnvPDrinkUnder.YEAR, TEnvPDrinkUnder.SelectMonths, TEnvPDrinkUnderVo.POINT_NAME_FIELD, TEnvPDrinkUnder.POINT_NAME, TEnvPDrinkUnderVo.POINT_CODE_FIELD, TEnvPDrinkUnder.POINT_CODE, TEnvPDrinkUnderVo.ID_FIELD, TEnvPDrinkUnder.ID, 0);
        if (strMsg=="")
        {
            isSuccess =  new TEnvPDrinkUnderLogic().Create(TEnvPDrinkUnder, SerialType.T_ENV_P_DRINK_UNDER);
            if (isSuccess)
            {
                WriteLog("添加地下饮用水点位信息", "", LogInfo.UserInfo.USER_NAME + "添加地下饮用水点位信息" + TEnvPDrinkUnder.ID);
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
        TEnvPDrinkUnderVo TEnvPDrinkUnder = autoBindRequest(Request, new TEnvPDrinkUnderVo());
        TEnvPDrinkUnder.ID = Request["id"].ToString();
        TEnvPDrinkUnder.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPDrinkUnder.YEAR || hidMonth.Value.Trim() != TEnvPDrinkUnder.MONTH || hidValue.Value.Trim() != TEnvPDrinkUnder.POINT_NAME || hidCode.Value.Trim() != TEnvPDrinkUnder.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPDrinkUnderVo.T_ENV_P_DRINK_UNDER_TABLE, TEnvPDrinkUnder.YEAR, TEnvPDrinkUnder.MONTH, TEnvPDrinkUnderVo.POINT_NAME_FIELD, TEnvPDrinkUnder.POINT_NAME, TEnvPDrinkUnderVo.POINT_CODE_FIELD, TEnvPDrinkUnder.POINT_CODE, TEnvPDrinkUnderVo.ID_FIELD, TEnvPDrinkUnder.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPDrinkUnderLogic().Edit(TEnvPDrinkUnder);
            if (isSuccess)
            {
                WriteLog("编辑地下饮用水监测点", "", LogInfo.UserInfo.USER_NAME + "编辑地下饮用水监测点" + TEnvPDrinkUnder.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}