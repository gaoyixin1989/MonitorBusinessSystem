using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.NoiseFun;
using i3.BusinessLogic.Channels.Env.Point.NoiseFun;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：功能区噪声点位信息编辑
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_NoiseFun_FunctionNoiseEdit : PageBase
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
        TEnvPNoiseFunctionVo TEnvPNoiseFunction = new TEnvPNoiseFunctionVo();
        TEnvPNoiseFunction.ID = Request["id"].ToString();
        TEnvPNoiseFunction.IS_DEL = "0";
        TEnvPNoiseFunctionVo TEnvPNoiseFunctionTemp = new TEnvPNoiseFunctionLogic().Details(TEnvPNoiseFunction);

        return ToJson(TEnvPNoiseFunctionTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPNoiseFunctionVo TEnvPNoiseFunction = autoBindRequest(Request, new TEnvPNoiseFunctionVo());
        TEnvPNoiseFunction.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE, TEnvPNoiseFunction.YEAR, TEnvPNoiseFunction.SelectMonths, TEnvPNoiseFunctionVo.POINT_NAME_FIELD, TEnvPNoiseFunction.POINT_NAME, TEnvPNoiseFunctionVo.POINT_CODE_FIELD, TEnvPNoiseFunction.POINT_CODE, TEnvPNoiseFunctionVo.ID_FIELD, TEnvPNoiseFunction.ID, 1);
        if (strMsg=="")
        {
            isSuccess = new TEnvPNoiseFunctionLogic().Create(TEnvPNoiseFunction, SerialType.T_ENV_P_NOISE_FUNCTION);
            if (isSuccess)
            {
                WriteLog("添加功能区噪声点位信息", "", LogInfo.UserInfo.USER_NAME + "添加功能区噪声点位信息" + TEnvPNoiseFunction.ID);
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
        TEnvPNoiseFunctionVo TEnvPNoiseFunction = autoBindRequest(Request, new TEnvPNoiseFunctionVo());
        TEnvPNoiseFunction.ID = Request["id"].ToString();
        TEnvPNoiseFunction.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPNoiseFunction.YEAR || hidMonth.Value.Trim() != TEnvPNoiseFunction.MONTH || hidValue.Value.Trim() != TEnvPNoiseFunction.POINT_NAME)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPNoiseFunctionVo.T_ENV_P_NOISE_FUNCTION_TABLE, TEnvPNoiseFunction.YEAR, TEnvPNoiseFunction.MONTH, TEnvPNoiseFunctionVo.POINT_NAME_FIELD, TEnvPNoiseFunction.POINT_NAME, TEnvPNoiseFunctionVo.POINT_CODE_FIELD, TEnvPNoiseFunction.POINT_CODE, TEnvPNoiseFunctionVo.ID_FIELD, TEnvPNoiseFunction.ID, 1);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPNoiseFunctionLogic().Edit(TEnvPNoiseFunction);
            if (isSuccess)
            {
                WriteLog("编辑功能区噪声监测点", "", LogInfo.UserInfo.USER_NAME + "编辑功能区噪声监测点" + TEnvPNoiseFunction.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}