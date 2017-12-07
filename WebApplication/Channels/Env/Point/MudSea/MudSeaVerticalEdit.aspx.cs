using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.MudSea;
using i3.BusinessLogic.Channels.Env.Point.MudSea;

/// <summary>
/// 功能描述：沉积物（海水）垂线编辑
/// 创建日期：2013-06-15
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_MudSea_MudSeaVerticalEdit : PageBase
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
        if (this.Request["oneGridId"] != null)
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
        TEnvPMudSeaVVo TEnvPMudSeaV = new TEnvPMudSeaVVo();
        TEnvPMudSeaV.ID = Request["id"].ToString();
        TEnvPMudSeaVVo TEnvPRiverVTemp = new TEnvPMudSeaVLogic().Details(TEnvPMudSeaV);
        return ToJson(TEnvPRiverVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPMudSeaVVo TEnvPMudSeaV = autoBindRequest(Request, new TEnvPMudSeaVVo());
        TEnvPMudSeaV.ID = GetSerialNumber(SerialType.T_ENV_P_RIVER_V);
        TEnvPMudSeaV.SECTION_ID = this.formId.Value;
        bool isSuccess = new TEnvPMudSeaVLogic().Create(TEnvPMudSeaV);
        if (isSuccess)
            WriteLog("添加沉积物（海水）垂线", "", LogInfo.UserInfo.USER_NAME + "添加沉积物（海水）垂线" + TEnvPMudSeaV.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPMudSeaVVo TEnvPMudSeaV = autoBindRequest(Request, new TEnvPMudSeaVVo());
        TEnvPMudSeaV.ID = Request["id"].ToString();
        bool isSuccess = new TEnvPMudSeaVLogic().Edit(TEnvPMudSeaV);
        if (isSuccess)
            WriteLog("编辑沉积物（海水）垂线", "", LogInfo.UserInfo.USER_NAME + "编辑沉积物（海水）垂线" + TEnvPMudSeaV.ID);
        return isSuccess == true ? "1" : "0";
    }
}