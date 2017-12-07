using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.MudRiver;
using i3.BusinessLogic.Channels.Env.Point.MudRiver;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：沉积物（河流）断面信息编辑
/// 创建日期：2013-06-15
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_MudRiver_MudRiverEdit : PageBase
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
        TEnvPMudRiverVo TEnvPMudRiver = new TEnvPMudRiverVo();
        TEnvPMudRiver.ID = Request["id"].ToString();
        TEnvPMudRiver.IS_DEL = "0";
        TEnvPMudRiverVo TEnvPMudRiverTemp = new TEnvPMudRiverLogic().Details(TEnvPMudRiver);
        ////条件项ID
        //string strConditionId = TEnvPMudRiverTemp.CONDITION_ID;
        ////根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        ////根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        //TEnvPMudRiverTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPMudRiverTemp.VALLEY_ID).DICT_TEXT;
        TEnvPMudRiverTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPMudRiverTemp.RIVER_ID).DICT_TEXT;
        TEnvPMudRiverTemp.REMARK3 = strRiver;

        return ToJson(TEnvPMudRiverTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPMudRiverVo TEnvPMudRiver = autoBindRequest(Request, new TEnvPMudRiverVo());
        TEnvPMudRiver.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPMudRiverVo.T_ENV_P_MUD_RIVER_TABLE, TEnvPMudRiver.YEAR, TEnvPMudRiver.SelectMonths, TEnvPMudRiverVo.SECTION_NAME_FIELD, TEnvPMudRiver.SECTION_NAME, TEnvPMudRiverVo.SECTION_CODE_FIELD, TEnvPMudRiver.SECTION_CODE, TEnvPMudRiverVo.ID_FIELD, TEnvPMudRiver.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPMudRiverLogic().Create(TEnvPMudRiver, SerialType.T_ENV_P_MUD_RIVER);
            if (isSuccess)
            {
                WriteLog("添加沉积物（河流）断面信息", "", LogInfo.UserInfo.USER_NAME + "添加沉积物（河流）断面信息" + TEnvPMudRiver.ID);
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
        TEnvPMudRiverVo TEnvPMudRiver = autoBindRequest(Request, new TEnvPMudRiverVo());
        TEnvPMudRiver.ID = Request["id"].ToString();
        TEnvPMudRiver.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPMudRiver.YEAR || hidMonth.Value.Trim() != TEnvPMudRiver.MONTH || hidValue.Value.Trim() != TEnvPMudRiver.SECTION_NAME || hidCode.Value.Trim() != TEnvPMudRiver.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPMudRiverVo.T_ENV_P_MUD_RIVER_TABLE, TEnvPMudRiver.YEAR, TEnvPMudRiver.MONTH, TEnvPMudRiverVo.SECTION_NAME_FIELD, TEnvPMudRiver.SECTION_NAME, TEnvPMudRiverVo.SECTION_CODE_FIELD, TEnvPMudRiver.SECTION_CODE, TEnvPMudRiverVo.ID_FIELD, TEnvPMudRiver.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPMudRiverLogic().Edit(TEnvPMudRiver);
            if (isSuccess)
            {
                WriteLog("编辑沉积物（河流)断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑沉积物（河流）断面监测点" + TEnvPMudRiver.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}