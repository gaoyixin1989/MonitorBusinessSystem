using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Air30;
using i3.BusinessLogic.Channels.Env.Point.Air30;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：双三十废气点位信息编辑
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_Air30_Air30Edit : PageBase
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
        TEnvPAir30Vo TEnvPAir30 = new TEnvPAir30Vo();
        TEnvPAir30.ID = Request["id"].ToString();
        TEnvPAir30.IS_DEL = "0";
        TEnvPAir30Vo TEnvPAir30Temp = new TEnvPAir30Logic().Details(TEnvPAir30);

        return ToJson(TEnvPAir30Temp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPAir30Vo TEnvPAir30 = autoBindRequest(Request, new TEnvPAir30Vo());
        TEnvPAir30.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPAir30Vo.T_ENV_P_AIR30_TABLE, TEnvPAir30.YEAR, TEnvPAir30.SelectMonths, TEnvPAir30Vo.POINT_NAME_FIELD, TEnvPAir30.POINT_NAME, TEnvPAir30Vo.POINT_CODE_FIELD, TEnvPAir30.POINT_CODE, TEnvPAir30Vo.ID_FIELD, TEnvPAir30.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPAir30Logic().Create(TEnvPAir30, SerialType.T_ENV_P_AIR30);
            if (isSuccess)
            {
                WriteLog("添加双三十废气点位信息", "", LogInfo.UserInfo.USER_NAME + "添加双三十废气点位信息" + TEnvPAir30.ID);
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
        TEnvPAir30Vo TEnvPAir30 = autoBindRequest(Request, new TEnvPAir30Vo());
        TEnvPAir30.ID = Request["id"].ToString();
        TEnvPAir30.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPAir30.YEAR || hidMonth.Value.Trim() != TEnvPAir30.MONTH || hidValue.Value.Trim() != TEnvPAir30.POINT_NAME) // || hidCode.Value.Trim() != TEnvPAir30.POINT_CODE
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPAir30Vo.T_ENV_P_AIR30_TABLE, TEnvPAir30.YEAR, TEnvPAir30.MONTH, TEnvPAir30Vo.POINT_NAME_FIELD, TEnvPAir30.POINT_NAME, TEnvPAir30Vo.POINT_CODE_FIELD, TEnvPAir30.POINT_CODE, TEnvPAir30Vo.ID_FIELD, TEnvPAir30.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPAir30Logic().Edit(TEnvPAir30);
            if (isSuccess)
            {
                WriteLog("编辑双三十废气监测点", "", LogInfo.UserInfo.USER_NAME + "编辑双三十废气监测点" + TEnvPAir30.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}