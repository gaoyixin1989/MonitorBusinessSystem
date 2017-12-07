using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.BusinessLogic.Channels.Env.Point.Estuaries;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：入海河口断面信息编辑
/// 创建日期：2011-11-19
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Estuaries_EstuariesEdit : PageBase
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
            strResult = getYearInfo(5, 5);
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

    #region// 加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPointEstuariesVo TEnvPointEstuariesVo = new TEnvPointEstuariesVo();
        TEnvPointEstuariesVo.ID = Request["id"].ToString();
        TEnvPointEstuariesVo.IS_DEL = "0";
        TEnvPointEstuariesVo TEnvPointEstuariesVoTemp = new TEnvPointEstuariesLogic().Details(TEnvPointEstuariesVo);
        //条件项ID
        string strConditionId = TEnvPointEstuariesVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID; 
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPointEstuariesVoTemp.REMARK1 = strStanderName;
        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPointEstuariesVoTemp.VALLEY_ID).DICT_TEXT;
        TEnvPointEstuariesVoTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPointEstuariesVoTemp.RIVER_ID).DICT_TEXT;
        TEnvPointEstuariesVoTemp.REMARK3 = strRiver;
        return ToJson(TEnvPointEstuariesVoTemp);
    }
    #endregion

    #region//增加数据
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        string strMsg = "";
        bool isSuccess = true;
        TEnvPointEstuariesVo TEnvPointEstuariesVo = autoBindRequest(Request, new TEnvPointEstuariesVo());
        //TEnvPointEstuariesVo.ID = GetSerialNumber("estuariespoint_id");
        TEnvPointEstuariesVo.IS_DEL = "0";
        string Meg = new CommonLogic().isExistDatas(TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE, TEnvPointEstuariesVo.YEAR, TEnvPointEstuariesVo.SelectMonths, TEnvPointEstuariesVo.SECTION_NAME_FIELD, TEnvPointEstuariesVo.SECTION_NAME, TEnvPointEstuariesVo.SECTION_CODE_FIELD, TEnvPointEstuariesVo.SECTION_CODE, TEnvPointEstuariesVo.ID_FIELD, TEnvPointEstuariesVo.ID, 0); 
        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPointEstuariesLogic().Create(TEnvPointEstuariesVo, SerialType.T_ENV_P_ESTUARIES);
            if (isSuccess)
            {
                WriteLog("添加入海河口监测点", "", LogInfo.UserInfo.USER_NAME + "添加入海河口监测点:" + TEnvPointEstuariesVo.ID);
                strMsg = "数据保存成功";
            }
            else
                strMsg = "数据保存失败";
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
    #endregion

    #region//修改数据
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPointEstuariesVo TEnvPointEstuariesVo = autoBindRequest(Request, new TEnvPointEstuariesVo());
        TEnvPointEstuariesVo.ID = Request["id"].ToString();
        TEnvPointEstuariesVo.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = true;
        if (hidYear.Value.Trim() != TEnvPointEstuariesVo.YEAR || hidMonth.Value.Trim() != TEnvPointEstuariesVo.MONTH || hidValue.Value.Trim() != TEnvPointEstuariesVo.SECTION_NAME || hidValues.Value.Trim() != TEnvPointEstuariesVo.SECTION_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE, TEnvPointEstuariesVo.YEAR, TEnvPointEstuariesVo.MONTH, TEnvPointEstuariesVo.SECTION_NAME_FIELD, TEnvPointEstuariesVo.SECTION_NAME, TEnvPointEstuariesVo.SECTION_CODE_FIELD, TEnvPointEstuariesVo.SECTION_CODE, TEnvPointEstuariesVo.ID_FIELD, TEnvPointEstuariesVo.ID, 0);
            if (string.IsNullOrEmpty(strMsg))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }

        if (isSuccess)
        {
            isSuccess = new TEnvPointEstuariesLogic().Edit(TEnvPointEstuariesVo);
            if (isSuccess)
            {
                WriteLog("编辑入海河口监测点", "", LogInfo.UserInfo.USER_NAME + "编辑入海河口监测点" + TEnvPointEstuariesVo.ID);
                strMsg = "数据更新成功";
            }
            else
            {
                strMsg = "数据更新失败";
            }
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
    #endregion
}