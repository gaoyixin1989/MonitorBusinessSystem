using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Lake;
using i3.BusinessLogic.Channels.Env.Point.Lake;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：湖库断面信息编辑
/// 创建日期：2013-06-14
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_Lake_LakeEdit : PageBase
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
        TEnvPLakeVo TEnvPLake = new TEnvPLakeVo();
        TEnvPLake.ID = Request["id"].ToString();
        TEnvPLake.IS_DEL = "0";
        TEnvPLakeVo TEnvPLakeTemp = new TEnvPLakeLogic().Details(TEnvPLake);
        //条件项ID
        string strConditionId = TEnvPLakeTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPLakeTemp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPLakeTemp.VALLEY_ID).DICT_TEXT;
        TEnvPLakeTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPLakeTemp.RIVER_ID).DICT_TEXT;
        TEnvPLakeTemp.REMARK3 = strRiver;

        return ToJson(TEnvPLakeTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPLakeVo EnvPLake = autoBindRequest(Request, new TEnvPLakeVo());
        EnvPLake.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPLakeVo.T_ENV_P_LAKE_TABLE, EnvPLake.YEAR, EnvPLake.SelectMonths, TEnvPLakeVo.SECTION_NAME_FIELD, EnvPLake.SECTION_NAME, TEnvPLakeVo.SECTION_CODE_FIELD, EnvPLake.SECTION_CODE, TEnvPLakeVo.ID_FIELD, EnvPLake.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPLakeLogic().Create(EnvPLake, SerialType.T_ENV_P_LAKE);
            if (isSuccess)
            {
                WriteLog("添湖库断面信息", "", LogInfo.UserInfo.USER_NAME + "添加湖库断面信息" + EnvPLake.ID);
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
        TEnvPLakeVo TEnvPLake = autoBindRequest(Request, new TEnvPLakeVo());
        TEnvPLake.ID = Request["id"].ToString();
        TEnvPLake.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPLake.YEAR || hidMonth.Value.Trim() != TEnvPLake.MONTH || hidValue.Value.Trim() != TEnvPLake.SECTION_NAME || hidCode.Value.Trim() != TEnvPLake.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPLakeVo.T_ENV_P_LAKE_TABLE, TEnvPLake.YEAR, TEnvPLake.MONTH, TEnvPLakeVo.SECTION_NAME_FIELD, TEnvPLake.SECTION_NAME, TEnvPLakeVo.SECTION_CODE_FIELD, TEnvPLake.SECTION_CODE, TEnvPLakeVo.ID_FIELD, TEnvPLake.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPLakeLogic().Edit(TEnvPLake);
            if (isSuccess)
            {
                WriteLog("编辑湖库断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑湖库断面监测点" + TEnvPLake.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}