using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：饮用水源地断面信息编辑管理
/// 创建日期：2013-06-07
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_DrinkSource_DrinkingSourceEdit : PageBase
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
        TEnvPDrinkSrcVo TEnvPDrinkSrc = new TEnvPDrinkSrcVo();
        TEnvPDrinkSrc.ID = Request["id"].ToString();
        TEnvPDrinkSrc.IS_DEL = "0";
        TEnvPDrinkSrcVo TEnvPDrinkSrcVoTemp = new TEnvPDrinkSrcLogic().Details(TEnvPDrinkSrc);
        ////条件项ID
        //string strConditionId = TEnvPointDrinkingVoTemp.CONDITION_ID;
        ////根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        ////根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        //TEnvPointDrinkingVoTemp.REMARK1 = strStanderName;

        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPDrinkSrcVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPDrinkSrcVoTemp.REMARK3 = strRiver;

        return ToJson(TEnvPDrinkSrcVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        string flag=Request["flag"].ToString();
        TEnvPDrinkSrcVo TEnvPDrinkSrc = autoBindRequest(Request, new TEnvPDrinkSrcVo());
        TEnvPDrinkSrc.IS_DEL = "0";
        if (flag != "")
        {
            TEnvPDrinkSrc.NUM = flag;//flag=surface为地表饮用水源地；
        }
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().IsExistData(TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE, TEnvPDrinkSrc.YEAR, TEnvPDrinkSrc.SelectMonths, TEnvPDrinkSrcVo.SECTION_NAME_FIELD, TEnvPDrinkSrc.SECTION_NAME, TEnvPDrinkSrcVo.SECTION_CODE_FIELD, TEnvPDrinkSrc.SECTION_CODE, TEnvPDrinkSrcVo.ID_FIELD, TEnvPDrinkSrc.ID, flag);
        if (strMsg=="")
        {
            isSuccess = new TEnvPDrinkSrcLogic().Create(TEnvPDrinkSrc, SerialType.T_ENV_P_DRINK_SRC);
            if (isSuccess)
            {
                WriteLog("添加饮用水源地断面信息", "", LogInfo.UserInfo.USER_NAME + "添加饮用水源地断面信息" + TEnvPDrinkSrc.ID);
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
        TEnvPDrinkSrcVo TEnvPDrinkSrc = autoBindRequest(Request, new TEnvPDrinkSrcVo());
        TEnvPDrinkSrc.ID = Request["id"].ToString();
        TEnvPDrinkSrc.IS_DEL = "0";
        TEnvPDrinkSrc.NUM = Request["flag"].ToString();
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPDrinkSrc.YEAR || hidMonth.Value.Trim() != TEnvPDrinkSrc.MONTH || hidValue.Value.Trim() != TEnvPDrinkSrc.SECTION_NAME || hidCode.Value.Trim() != TEnvPDrinkSrc.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().IsExistData(TEnvPDrinkSrcVo.T_ENV_P_DRINK_SRC_TABLE, TEnvPDrinkSrc.YEAR, TEnvPDrinkSrc.MONTH, TEnvPDrinkSrcVo.SECTION_NAME_FIELD, TEnvPDrinkSrc.SECTION_NAME, TEnvPDrinkSrcVo.SECTION_CODE_FIELD, TEnvPDrinkSrc.SECTION_CODE, TEnvPDrinkSrcVo.ID_FIELD, TEnvPDrinkSrc.ID, TEnvPDrinkSrc.NUM);
        }

        if (strMsg=="")
        {
            isSuccess =  new TEnvPDrinkSrcLogic().Edit(TEnvPDrinkSrc);
            if (isSuccess)
            {
                WriteLog("编辑饮用水源地断面信息", "", LogInfo.UserInfo.USER_NAME + "编辑饮用水源地断面信息" + TEnvPDrinkSrc.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}