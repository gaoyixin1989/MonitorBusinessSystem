using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Sea;
using i3.BusinessLogic.Channels.Env.Point.Sea;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：近岸海域监测点编辑
/// 创建日期：2011-11-20
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Sea_SeaEdit : PageBase
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

    #region//加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPointSeaVo TEnvPointSeaVo = new TEnvPointSeaVo();
        TEnvPointSeaVo.ID = Request["id"].ToString();
        TEnvPointSeaVo.IS_DEL = "0";
        TEnvPointSeaVo TEnvPointSeaVoTemp = new TEnvPointSeaLogic().Details(TEnvPointSeaVo);
        //条件项ID
        string strConditionId = TEnvPointSeaVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        //string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID; 
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strConditionId).STANDARD_NAME;
        TEnvPointSeaVoTemp.REMARK1 = strStanderName;
        return ToJson(TEnvPointSeaVoTemp);
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
        TEnvPointSeaVo TEnvPointSeaVo = autoBindRequest(Request, new TEnvPointSeaVo());
        //TEnvPointSeaVo.ID = GetSerialNumber("seapoint_id");
        TEnvPointSeaVo.IS_DEL = "0";
        string Meg = new CommonLogic().isExistDatas(TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE, TEnvPointSeaVo.YEAR, TEnvPointSeaVo.SelectMonths, TEnvPointSeaVo.POINT_NAME_FIELD, TEnvPointSeaVo.POINT_NAME, TEnvPointSeaVo.POINT_CODE_FIELD, TEnvPointSeaVo.POINT_CODE,TEnvPointSeaVo.ID_FIELD,TEnvPointSeaVo.ID, 0);  
        if (!string.IsNullOrEmpty(Meg))
        {
           
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPointSeaLogic().Create(TEnvPointSeaVo, SerialType.T_ENV_P_SEA);
            if (isSuccess)
            {
                WriteLog("添加近岸海域监测点", "", LogInfo.UserInfo.USER_NAME + "添加近岸海域监测点:" + TEnvPointSeaVo.ID);
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
        string strMsg = "";
        bool isSuccess = true;
        TEnvPointSeaVo TEnvPointSeaVo = autoBindRequest(Request, new TEnvPointSeaVo());
        TEnvPointSeaVo.ID = Request["id"].ToString();
        TEnvPointSeaVo.IS_DEL = "0";
        if (hidYear.Value.Trim() != TEnvPointSeaVo.YEAR || hidMonth.Value.Trim() != TEnvPointSeaVo.MONTH || hidValue.Value.Trim() != TEnvPointSeaVo.POINT_NAME || hidValues.Value.Trim() != TEnvPointSeaVo.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPointSeaVo.T_ENV_POINT_SEA_TABLE, TEnvPointSeaVo.YEAR, TEnvPointSeaVo.MONTH, TEnvPointSeaVo.POINT_NAME_FIELD, TEnvPointSeaVo.POINT_NAME, TEnvPointSeaVo.POINT_CODE_FIELD, TEnvPointSeaVo.POINT_CODE, TEnvPointSeaVo.ID_FIELD, TEnvPointSeaVo.ID, 0); 
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
            isSuccess = new TEnvPointSeaLogic().Edit(TEnvPointSeaVo);
            if (isSuccess)
            {
                WriteLog("编辑近岸海域监测点", "", LogInfo.UserInfo.USER_NAME + "编辑近岸海域监测点" + TEnvPointSeaVo.ID);
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