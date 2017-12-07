using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 功能描述：河流断面信息编辑
/// 创建日期：2011-11-13
/// 创建人  ：熊卫华
/// 修改人 : 魏林 2013-06-13
/// </summary>
public partial class Channels_Env_Point_River_RiverEdit : PageBase
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
        TEnvPRiverVo TEnvPRiver = new TEnvPRiverVo();
        TEnvPRiver.ID = Request["id"].ToString();
        TEnvPRiver.IS_DEL = "0";
        TEnvPRiverVo TEnvPRiverVoTemp = new TEnvPRiverLogic().Details(TEnvPRiver);
        //条件项ID
        string strConditionId = TEnvPRiverVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPRiverVoTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverVoTemp.VALLEY_ID).DICT_TEXT;
        TEnvPRiverVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiverVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPRiverVoTemp.REMARK3 = strRiver;
        //类型
        if (!string.IsNullOrEmpty(TEnvPRiverVoTemp.REMARK4))
        {
            TSysDictVo vo = new TSysDictVo();
            vo.DICT_TEXT = TEnvPRiverVoTemp.REMARK4;
            vo.DICT_TYPE = "ENV_RIVER";
            TSysDictVo vos = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(vo);
            TEnvPRiverVoTemp.REMARK4 = vos.DICT_TEXT;
        }
        return ToJson(TEnvPRiverVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    { 
        TEnvPRiverVo TEnvPRiver = autoBindRequest(Request, new TEnvPRiverVo());
        TEnvPRiver.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPRiverVo.T_ENV_P_RIVER_TABLE, TEnvPRiver.YEAR, TEnvPRiver.SelectMonths, TEnvPRiverVo.SECTION_NAME_FIELD, TEnvPRiver.SECTION_NAME, TEnvPRiverVo.SECTION_CODE_FIELD, TEnvPRiver.SECTION_CODE, TEnvPRiverVo.ID_FIELD, TEnvPRiver.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverLogic().Create(TEnvPRiver, SerialType.T_ENV_P_RIVER);
            if (isSuccess)
            {
                WriteLog("添加河流断面信息", "", LogInfo.UserInfo.USER_NAME + "添加河流断面信息" + TEnvPRiver.ID);
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
        TEnvPRiverVo TEnvPRiver = autoBindRequest(Request, new TEnvPRiverVo());
        TEnvPRiver.ID = Request["id"].ToString();
        TEnvPRiver.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPRiver.YEAR || hidMonth.Value.Trim() != TEnvPRiver.MONTH || hidValue.Value.Trim() != TEnvPRiver.SECTION_NAME || hidCode.Value.Trim() != TEnvPRiver.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPRiverVo.T_ENV_P_RIVER_TABLE, TEnvPRiver.YEAR, TEnvPRiver.MONTH, TEnvPRiverVo.SECTION_NAME_FIELD, TEnvPRiver.SECTION_NAME, TEnvPRiverVo.SECTION_CODE_FIELD, TEnvPRiver.SECTION_CODE, TEnvPRiverVo.ID_FIELD, TEnvPRiver.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPRiverLogic().Edit(TEnvPRiver);
            if (isSuccess)
            {
                WriteLog("编辑河流断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑河流断面监测点" + TEnvPRiver.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}