using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Solid;
using i3.BusinessLogic.Channels.Env.Point.Solid;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：固废点位信息编辑
/// 创建日期：2013-06-14
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_Solid_SolidEdit : PageBase
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
        TEnvPSolidVo TEnvPSolid = new TEnvPSolidVo();
        TEnvPSolid.ID = Request["id"].ToString();
        TEnvPSolid.IS_DEL = "0";
        TEnvPSolidVo TEnvPSolidTemp = new TEnvPSolidLogic().Details(TEnvPSolid);

        return ToJson(TEnvPSolidTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPSolidVo TEnvPSolid = autoBindRequest(Request, new TEnvPSolidVo());
        TEnvPSolid.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPSolidVo.T_ENV_P_SOLID_TABLE, TEnvPSolid.YEAR, TEnvPSolid.SelectMonths, TEnvPSolidVo.POINT_NAME_FIELD, TEnvPSolid.POINT_NAME, TEnvPSolidVo.POINT_CODE_FIELD, TEnvPSolid.POINT_CODE, TEnvPSolidVo.ID_FIELD, TEnvPSolid.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPSolidLogic().Create(TEnvPSolid, SerialType.T_ENV_P_SOLID);
            if (isSuccess)
            {
                WriteLog("添加固废点位信息", "", LogInfo.UserInfo.USER_NAME + "添加固废点位信息" + TEnvPSolid.ID);
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
        TEnvPSolidVo TEnvPSolid = autoBindRequest(Request, new TEnvPSolidVo());
        TEnvPSolid.ID = Request["id"].ToString();
        TEnvPSolid.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPSolid.YEAR || hidMonth.Value.Trim() != TEnvPSolid.MONTH || hidValue.Value.Trim() != TEnvPSolid.POINT_NAME || hidCode.Value.Trim() != TEnvPSolid.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPSolidVo.T_ENV_P_SOLID_TABLE, TEnvPSolid.YEAR, TEnvPSolid.MONTH, TEnvPSolidVo.POINT_NAME_FIELD, TEnvPSolid.POINT_NAME, TEnvPSolidVo.POINT_CODE_FIELD, TEnvPSolid.POINT_CODE, TEnvPSolidVo.ID_FIELD, TEnvPSolid.ID, 0);
        }

        if (strMsg == "")
        {
            isSuccess = new TEnvPSolidLogic().Edit(TEnvPSolid);
            if (isSuccess)
            {
                WriteLog("编辑固废监测点", "", LogInfo.UserInfo.USER_NAME + "编辑固废监测点" + TEnvPSolid.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}