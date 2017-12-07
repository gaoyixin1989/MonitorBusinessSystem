using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Dust;
using i3.BusinessLogic.Channels.Env.Point.Dust;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：降尘监测点信息编辑
/// 创建日期：2011-11-12
/// 创建人  ：熊卫华
/// modify by 刘静楠
/// </summary>
public partial class Channels_Env_Point_Dust_DustEdit : PageBase
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
        TEnvPointDustVo TEnvPointDustVo = new TEnvPointDustVo();
        TEnvPointDustVo.ID = Request["id"].ToString();
        TEnvPointDustVo.IS_DEL = "0";
        TEnvPointDustVo TEnvPointDustVoTemp = new TEnvPointDustLogic().Details(TEnvPointDustVo);
        return ToJson(TEnvPointDustVoTemp);
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
        TEnvPointDustVo TEnvPointDustVo = autoBindRequest(Request, new TEnvPointDustVo());
        TEnvPointDustVo.IS_DEL = "0";

        string Meg = new CommonLogic().isExistDatas(TEnvPointDustVo.T_ENV_POINT_DUST_TABLE, TEnvPointDustVo.YEAR, TEnvPointDustVo.SelectMonths, TEnvPointDustVo.POINT_NAME_FIELD, TEnvPointDustVo.POINT_NAME, TEnvPointDustVo.POINT_CODE_FIELD, TEnvPointDustVo.POINT_CODE, TEnvPointDustVo.ID_FIELD, TEnvPointDustVo.ID,0); 
        if (!string.IsNullOrEmpty(Meg))
        { 
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPointDustLogic().Create(TEnvPointDustVo, SerialType.T_ENV_P_DUST);
            if (isSuccess)
            {
                WriteLog("添加降尘监测点", "", LogInfo.UserInfo.USER_NAME + "添加降尘监测点:" + TEnvPointDustVo.ID);
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
        TEnvPointDustVo TEnvPointDustVo = autoBindRequest(Request, new TEnvPointDustVo());
        TEnvPointDustVo.ID = Request["id"].ToString();
        TEnvPointDustVo.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = true;
        if (hidYear.Value.Trim() != TEnvPointDustVo.YEAR || hidMonth.Value.Trim() != TEnvPointDustVo.MONTH || hidValue.Value.Trim() != TEnvPointDustVo.POINT_NAME || hidValues.Value.Trim() != TEnvPointDustVo.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPointDustVo.T_ENV_POINT_DUST_TABLE, TEnvPointDustVo.YEAR, TEnvPointDustVo.MONTH, TEnvPointDustVo.POINT_NAME_FIELD, TEnvPointDustVo.POINT_NAME, TEnvPointDustVo.POINT_CODE_FIELD, TEnvPointDustVo.POINT_CODE, TEnvPointDustVo.ID_FIELD, TEnvPointDustVo.ID, 0); 
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
            isSuccess = new TEnvPointDustLogic().Edit(TEnvPointDustVo);
            if (isSuccess)
            {
                WriteLog("编辑降尘监测点", "", LogInfo.UserInfo.USER_NAME + "编辑降尘监测点" + TEnvPointDustVo.ID);
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