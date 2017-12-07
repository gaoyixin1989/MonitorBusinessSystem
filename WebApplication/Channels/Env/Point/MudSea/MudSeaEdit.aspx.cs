using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.MudSea;
using i3.BusinessLogic.Channels.Env.Point.MudSea;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：沉积物（海水）断面信息编辑
/// 创建日期：2013-06-15
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_MudSea_MudSeaEdit : PageBase
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
        TEnvPMudSeaVo TEnvPMudSea = new TEnvPMudSeaVo();
        TEnvPMudSea.ID = Request["id"].ToString();
        TEnvPMudSea.IS_DEL = "0";
        TEnvPMudSeaVo TEnvPMudSeaTemp = new TEnvPMudSeaLogic().Details(TEnvPMudSea);
        ////条件项ID
        //string strConditionId = TEnvPMudSeaTemp.CONDITION_ID;
        ////根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        ////根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        //TEnvPMudSeaTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPMudSeaTemp.VALLEY_ID).DICT_TEXT;
        TEnvPMudSeaTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPMudSeaTemp.RIVER_ID).DICT_TEXT;
        TEnvPMudSeaTemp.REMARK3 = strRiver;

        return ToJson(TEnvPMudSeaTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPMudSeaVo TEnvPMudSea = autoBindRequest(Request, new TEnvPMudSeaVo());
        TEnvPMudSea.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE, TEnvPMudSea.YEAR, TEnvPMudSea.SelectMonths, TEnvPMudSeaVo.SECTION_NAME_FIELD, TEnvPMudSea.SECTION_NAME, TEnvPMudSeaVo.SECTION_CODE_FIELD, TEnvPMudSea.SECTION_CODE, TEnvPMudSeaVo.ID_FIELD, TEnvPMudSea.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPMudSeaLogic().Create(TEnvPMudSea, SerialType.T_ENV_P_MUD_SEA);
            if (isSuccess)
            {
                WriteLog("添加沉积物（海水）断面信息", "", LogInfo.UserInfo.USER_NAME + "添加沉积物（海水）断面信息" + TEnvPMudSea.ID);
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
        TEnvPMudSeaVo TEnvPMudSea = autoBindRequest(Request, new TEnvPMudSeaVo());
        TEnvPMudSea.ID = Request["id"].ToString();
        TEnvPMudSea.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPMudSea.YEAR || hidMonth.Value.Trim() != TEnvPMudSea.MONTH || hidValue.Value.Trim() != TEnvPMudSea.SECTION_NAME || hidCode.Value.Trim() != TEnvPMudSea.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPMudSeaVo.T_ENV_P_MUD_SEA_TABLE, TEnvPMudSea.YEAR, TEnvPMudSea.MONTH, TEnvPMudSeaVo.SECTION_NAME_FIELD, TEnvPMudSea.SECTION_NAME, TEnvPMudSeaVo.SECTION_CODE_FIELD, TEnvPMudSea.SECTION_CODE, TEnvPMudSeaVo.ID_FIELD, TEnvPMudSea.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPMudSeaLogic().Edit(TEnvPMudSea);
            if (isSuccess)
            {
                WriteLog("编辑沉积物（海水）断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑沉积物（海水）断面监测点" + TEnvPMudSea.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}