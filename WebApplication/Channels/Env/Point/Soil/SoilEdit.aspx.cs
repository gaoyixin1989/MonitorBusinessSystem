using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Soil;
using i3.BusinessLogic.Channels.Env.Point.Soil;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：土壤点位信息编辑
/// 创建日期：2013-06-14
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_Soil_SoilEdit : PageBase
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
        TEnvPSoilVo TEnvPSoil = new TEnvPSoilVo();
        TEnvPSoil.ID = Request["id"].ToString();
        TEnvPSoil.IS_DEL = "0";
        TEnvPSoilVo TEnvPSoilTemp = new TEnvPSoilLogic().Details(TEnvPSoil);

        return ToJson(TEnvPSoilTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPSoilVo TEnvPSoil = autoBindRequest(Request, new TEnvPSoilVo());
        TEnvPSoil.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPSoilVo.T_ENV_P_SOIL_TABLE, TEnvPSoil.YEAR, TEnvPSoil.SelectMonths, TEnvPSoilVo.POINT_NAME_FIELD, TEnvPSoil.POINT_NAME, TEnvPSoilVo.POINT_CODE_FIELD, TEnvPSoil.POINT_CODE, TEnvPSoilVo.ID_FIELD, TEnvPSoil.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPSoilLogic().Create(TEnvPSoil, SerialType.T_ENV_P_SOIL);
            if (isSuccess)
            {
                WriteLog("添加土壤点位信息", "", LogInfo.UserInfo.USER_NAME + "添加土壤点位信息" + TEnvPSoil.ID);
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
        TEnvPSoilVo TEnvPSoil = autoBindRequest(Request, new TEnvPSoilVo());
        TEnvPSoil.ID = Request["id"].ToString();
        TEnvPSoil.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPSoil.YEAR || hidMonth.Value.Trim() != TEnvPSoil.MONTH || hidValue.Value.Trim() != TEnvPSoil.POINT_NAME || hidCode.Value.Trim() != TEnvPSoil.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPSoilVo.T_ENV_P_SOIL_TABLE, TEnvPSoil.YEAR, TEnvPSoil.MONTH, TEnvPSoilVo.POINT_NAME_FIELD, TEnvPSoil.POINT_NAME, TEnvPSoilVo.POINT_CODE_FIELD, TEnvPSoil.POINT_CODE, TEnvPSoilVo.ID_FIELD, TEnvPSoil.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPSoilLogic().Edit(TEnvPSoil);
            if (isSuccess)
            {
                WriteLog("编辑土壤监测点", "", LogInfo.UserInfo.USER_NAME + "编辑土壤监测点" + TEnvPSoil.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}