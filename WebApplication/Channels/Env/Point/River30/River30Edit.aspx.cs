using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：双三十废水断面信息编辑
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_River30_River30Edit : PageBase
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
        TEnvPRiver30Vo TEnvPRiver30 = new TEnvPRiver30Vo();
        TEnvPRiver30.ID = Request["id"].ToString();
        TEnvPRiver30.IS_DEL = "0";
        TEnvPRiver30Vo TEnvPRiver30Temp = new TEnvPRiver30Logic().Details(TEnvPRiver30);
        //条件项ID
        string strConditionId = TEnvPRiver30Temp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPRiver30Temp.REMARK1 = strStanderName;

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiver30Temp.VALLEY_ID).DICT_TEXT;
        TEnvPRiver30Temp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPRiver30Temp.RIVER_ID).DICT_TEXT;
        TEnvPRiver30Temp.REMARK3 = strRiver;

        return ToJson(TEnvPRiver30Temp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPRiver30Vo TEnvPRiver30 = autoBindRequest(Request, new TEnvPRiver30Vo());
        TEnvPRiver30.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE, TEnvPRiver30.YEAR, TEnvPRiver30.SelectMonths, TEnvPRiver30Vo.SECTION_NAME_FIELD, TEnvPRiver30.SECTION_NAME, TEnvPRiver30Vo.SECTION_CODE_FIELD, TEnvPRiver30.SECTION_CODE, TEnvPRiver30Vo.ID_FIELD, TEnvPRiver30.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPRiver30Logic().Create(TEnvPRiver30, SerialType.T_ENV_P_RIVER30);
            if (isSuccess)
            {
                WriteLog("添加双三十废水断面信息", "", LogInfo.UserInfo.USER_NAME + "添加双三十废水断面信息" + TEnvPRiver30.ID);
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
        TEnvPRiver30Vo TEnvPRiver30 = autoBindRequest(Request, new TEnvPRiver30Vo());
        TEnvPRiver30.ID = Request["id"].ToString();
        TEnvPRiver30.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPRiver30.YEAR || hidMonth.Value.Trim() != TEnvPRiver30.MONTH || hidValue.Value.Trim() != TEnvPRiver30.SECTION_NAME || hidCode.Value.Trim() != TEnvPRiver30.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE, TEnvPRiver30.YEAR, TEnvPRiver30.MONTH, TEnvPRiver30Vo.SECTION_NAME_FIELD, TEnvPRiver30.SECTION_NAME, TEnvPRiver30Vo.SECTION_CODE_FIELD, TEnvPRiver30.SECTION_CODE, TEnvPRiver30Vo.ID_FIELD, TEnvPRiver30.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPRiver30Logic().Edit(TEnvPRiver30);
            if (isSuccess)
            {
                WriteLog("编辑双三十废水断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑双三十废水断面监测点" + TEnvPRiver30.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}