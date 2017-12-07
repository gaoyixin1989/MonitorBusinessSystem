using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.ValueObject.Channels.Env.Point.Environment;
using i3.View;
using i3.BusinessLogic.Channels.Env.Point.Environment;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_Environment_EnvironmentEditer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
    }
    #region//加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPAirVo TEnvPointRainVo = new TEnvPAirVo();
        TEnvPointRainVo.ID = Request["id"].ToString();
        TEnvPointRainVo.IS_DEL = "0";
        TEnvPAirVo TEnvPointRainVoTemp = new T_ENV_P_AIR().Details(TEnvPointRainVo);
        //条件项ID
        //string strConditionId = TEnvPointRainVoTemp.CONDITION_ID;
        ////根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        ////根据评价标准ID获取评价标准名称
        //string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        //TEnvPointRainVoTemp.REMARK1 = strStanderName;
        return ToJson(TEnvPointRainVoTemp);
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
        TEnvPAirVo TEnvPointRainVo = autoBindRequest(Request, new TEnvPAirVo());
        //TEnvPointRainVo.ID = GetSerialNumber("rainpoint_id");
        TEnvPointRainVo.IS_DEL = "0";

        string Meg = new CommonLogic().isExistDatas(TEnvPAirVo.T_ENV_P_AIR_TABLE, TEnvPointRainVo.YEAR, TEnvPointRainVo.SelectMonths, TEnvPAirVo.POINT_NAME_FIELD, TEnvPointRainVo.POINT_NAME, TEnvPAirVo.POINT_CODE_FIELD, TEnvPointRainVo.POINT_CODE, TEnvPAirVo.ID_FIELD, TEnvPointRainVo.ID,0);
        //string Meg = new CommonLogic().isExistDatas(TEnvPAirVo.T_ENV_P_AIR_TABLE, TEnvPointRainVo.YEAR, TEnvPointRainVo.SelectMonths, TEnvPAirVo.POINT_CODE_FIELD, TEnvPointRainVo.POINT_CODE);
        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new T_ENV_P_AIR().Create(TEnvPointRainVo, SerialType.T_ENV_P_AIR);
            if (isSuccess)
            {
                WriteLog("添加环境空气监测点", "", LogInfo.UserInfo.USER_NAME + "添加环境空气监测点:" + TEnvPointRainVo.ID);
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
        TEnvPAirVo TEnvPointRainVo = autoBindRequest(Request, new TEnvPAirVo());
        TEnvPointRainVo.ID = Request["id"].ToString();
        TEnvPointRainVo.IS_DEL = "0";

        string Meg = "";
        bool isSuccess = true;
        if (hidYear.Value.Trim() != TEnvPointRainVo.YEAR || hidMonth.Value.Trim() != TEnvPointRainVo.MONTH || hidValue.Value.Trim() != TEnvPointRainVo.POINT_NAME || hidValues.Value.Trim() != TEnvPointRainVo.POINT_CODE)
        {
            //验证数据是否重复
             Meg = new CommonLogic().isExistDatas(TEnvPAirVo.T_ENV_P_AIR_TABLE, TEnvPointRainVo.YEAR, TEnvPointRainVo.SelectMonths, TEnvPAirVo.POINT_NAME_FIELD, TEnvPointRainVo.POINT_NAME, TEnvPAirVo.POINT_CODE_FIELD, TEnvPointRainVo.POINT_CODE, TEnvPAirVo.ID_FIELD, TEnvPointRainVo.ID, 0);
            if (string.IsNullOrEmpty(Meg))
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
            isSuccess = new T_ENV_P_AIR().Edit(TEnvPointRainVo);
            if (isSuccess)
            {
                WriteLog("编辑环境空气监测点", "", LogInfo.UserInfo.USER_NAME + "编辑环境空气监测点" + TEnvPointRainVo.ID);
                Meg = "数据更新成功";
            }
            else
            {
                Meg = "数据更新失败";
            }
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + Meg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + Meg + "\"}";


    }
    #endregion

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
}