using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverCity;
using i3.BusinessLogic.Channels.Env.Point.RiverCity;

/// <summary>
/// 功能描述：城考垂线编辑
/// 创建日期：2014-01-22
/// 创建人  ：魏林

/// </summary>
public partial class Channels_Env_Point_RiverCity_RiverCityVerticalEdit : PageBase
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
        if(this.Request["oneGridId"]!=null)
            this.formId.Value = this.Request["oneGridId"].ToString();
        //获取下拉列表信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict(Request["dictType"].ToString());
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
        TEnvPRiverCityVVo TEnvPRiverCityV = new TEnvPRiverCityVVo();
        TEnvPRiverCityV.ID = Request["id"].ToString();
        TEnvPRiverCityVVo TEnvPRiverVTemp = new TEnvPRiverCityVLogic().Details(TEnvPRiverCityV);
        return ToJson(TEnvPRiverVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPRiverCityVVo TEnvPRiverCityV = autoBindRequest(Request, new TEnvPRiverCityVVo());
        TEnvPRiverCityV.ID = GetSerialNumber(SerialType.T_ENV_P_RIVER_CITY_V);
        TEnvPRiverCityV.SECTION_ID = this.formId.Value;
        bool isSuccess = new TEnvPRiverCityVLogic().Create(TEnvPRiverCityV);
        if (isSuccess)
            WriteLog("添加城考垂线", "", LogInfo.UserInfo.USER_NAME + "添加城考垂线" + TEnvPRiverCityV.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPRiverCityVVo TEnvPRiverCityV = autoBindRequest(Request, new TEnvPRiverCityVVo());
        TEnvPRiverCityV.ID = Request["id"].ToString();
        bool isSuccess = new TEnvPRiverCityVLogic().Edit(TEnvPRiverCityV);
        if (isSuccess)
            WriteLog("编辑城考垂线", "", LogInfo.UserInfo.USER_NAME + "编辑城考垂线" + TEnvPRiverCityV.ID);
        return isSuccess == true ? "1" : "0";
    }
}