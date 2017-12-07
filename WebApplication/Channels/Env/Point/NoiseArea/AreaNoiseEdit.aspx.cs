using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.NoiseArea;
using i3.BusinessLogic.Channels.Env.Point.NoiseArea;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：区域环境噪声点位信息编辑
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_NoiseArea_AreaNoiseEdit : PageBase
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
        TEnvPNoiseAreaVo TEnvPNoiseArea = new TEnvPNoiseAreaVo();
        TEnvPNoiseArea.ID = Request["id"].ToString();
        TEnvPNoiseArea.IS_DEL = "0";
        TEnvPNoiseAreaVo TEnvPNoiseAreaTemp = new TEnvPNoiseAreaLogic().Details(TEnvPNoiseArea);

        return ToJson(TEnvPNoiseAreaTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPNoiseAreaVo TEnvPNoiseArea = autoBindRequest(Request, new TEnvPNoiseAreaVo());
        TEnvPNoiseArea.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE, TEnvPNoiseArea.YEAR, TEnvPNoiseArea.SelectMonths, TEnvPNoiseAreaVo.POINT_NAME_FIELD, TEnvPNoiseArea.POINT_NAME, TEnvPNoiseAreaVo.POINT_CODE_FIELD, TEnvPNoiseArea.POINT_CODE, TEnvPNoiseAreaVo.ID_FIELD, TEnvPNoiseArea.ID, 2);
        if (strMsg=="")
        {
            isSuccess = new TEnvPNoiseAreaLogic().Create(TEnvPNoiseArea, SerialType.T_ENV_P_NOISE_AREA);
            if (isSuccess)
            {
                WriteLog("添加道路交通噪声点位信息", "", LogInfo.UserInfo.USER_NAME + "添加道路交通噪声点位信息" + TEnvPNoiseArea.ID);
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
        TEnvPNoiseAreaVo TEnvPNoiseArea = autoBindRequest(Request, new TEnvPNoiseAreaVo());
        TEnvPNoiseArea.ID = Request["id"].ToString();
        TEnvPNoiseArea.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPNoiseArea.YEAR || hidMonth.Value.Trim() != TEnvPNoiseArea.MONTH || hidValue.Value.Trim() != TEnvPNoiseArea.POINT_NAME || hidCode.Value.Trim() != TEnvPNoiseArea.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPNoiseAreaVo.T_ENV_P_NOISE_AREA_TABLE, TEnvPNoiseArea.YEAR, TEnvPNoiseArea.MONTH, TEnvPNoiseAreaVo.POINT_NAME_FIELD, TEnvPNoiseArea.POINT_NAME, TEnvPNoiseAreaVo.POINT_CODE_FIELD, TEnvPNoiseArea.POINT_CODE, TEnvPNoiseAreaVo.ID_FIELD, TEnvPNoiseArea.ID, 2);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPNoiseAreaLogic().Edit(TEnvPNoiseArea);
            if (isSuccess)
            {
                WriteLog("编辑道路交通噪声监测点", "", LogInfo.UserInfo.USER_NAME + "编辑道路交通噪声监测点" + TEnvPNoiseArea.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}