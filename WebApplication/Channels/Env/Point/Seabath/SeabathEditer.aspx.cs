using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.Seabath;
using i3.BusinessLogic.Channels.Env.Point.Seabath;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_Seabath_SeabathEditer : PageBase
{
    /// <summary>
    /// 功能描述：海水浴场监测点管理
    /// 创建日期：2012-07-10
    /// 创建人  ：刘静楠
    /// </summary>
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
        TEnvPSeabathVo TEnvPointSeaVo = new TEnvPSeabathVo();
        TEnvPointSeaVo.ID = Request["id"].ToString();
        TEnvPointSeaVo.IS_DEL = "0";
        TEnvPSeabathVo TEnvPointSeaVoTemp = new TEnvPSeabathLogic().Details(TEnvPointSeaVo);
        //条件项ID
        string strConditionId = TEnvPointSeaVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
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
        TEnvPSeabathVo TEnvPointSeaVo = autoBindRequest(Request, new TEnvPSeabathVo());
        //TEnvPointSeaVo.ID = GetSerialNumber("seapoint_id");
        TEnvPointSeaVo.IS_DEL = "0";
        //bool isSuccess = new TEnvPSeabathLogic().Create(TEnvPointSeaVo, SerialType.T_ENV_P_SEABATH);
        //if (isSuccess)
        //    WriteLog("添加海水浴场监测点", "", LogInfo.UserInfo.USER_NAME + "添加海水浴场监测点" + TEnvPointSeaVo.ID);
        //return isSuccess == true ? "1" : "0";

        string Meg = new CommonLogic().isExistDatas(TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE, TEnvPointSeaVo.YEAR, TEnvPointSeaVo.SelectMonths, TEnvPSeabathVo.POINT_NAME_FIELD, TEnvPointSeaVo.POINT_NAME, TEnvPSeabathVo.POINT_CODE_FIELD, TEnvPointSeaVo.POINT_CODE, TEnvPSeabathVo.ID_FIELD, TEnvPointSeaVo.ID,0);  

        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPSeabathLogic().Create(TEnvPointSeaVo, SerialType.T_ENV_P_SEABATH);
            if (isSuccess)
            {
                WriteLog("添加海水浴场监测点", "", LogInfo.UserInfo.USER_NAME + "添加海水浴场监测点:" + TEnvPointSeaVo.ID);
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
        TEnvPSeabathVo TEnvPointSeaVo = autoBindRequest(Request, new TEnvPSeabathVo());
        TEnvPointSeaVo.ID = Request["id"].ToString();
        TEnvPointSeaVo.IS_DEL = "0";
        //bool isSuccess = new TEnvPSeabathLogic().Edit(TEnvPointSeaVo);
        //if (isSuccess)
        //    WriteLog("编辑海水浴场监测点", "", LogInfo.UserInfo.USER_NAME + "编辑海水浴场监测点" + TEnvPointSeaVo.ID);
        //return isSuccess == true ? "1" : "0";
        string strMsg = "";
        bool isSuccess = true;
        if (hidYear.Value.Trim() != TEnvPointSeaVo.YEAR || hidMonth.Value.Trim() != TEnvPointSeaVo.MONTH || hidValue.Value.Trim() != TEnvPointSeaVo.POINT_NAME || hidValues.Value.Trim() != TEnvPointSeaVo.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPSeabathVo.T_ENV_P_SEABATH_TABLE, TEnvPointSeaVo.YEAR, TEnvPointSeaVo.MONTH, TEnvPSeabathVo.POINT_NAME_FIELD, TEnvPointSeaVo.POINT_NAME, TEnvPSeabathVo.POINT_CODE_FIELD, TEnvPointSeaVo.POINT_CODE, TEnvPSeabathVo.ID_FIELD, TEnvPointSeaVo.ID, 0); 

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
            isSuccess = new TEnvPSeabathLogic().Edit(TEnvPointSeaVo);
            if (isSuccess)
            {
                WriteLog("编辑海水浴场监测点", "", LogInfo.UserInfo.USER_NAME + "编辑海水浴场监测点" + TEnvPointSeaVo.ID);
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