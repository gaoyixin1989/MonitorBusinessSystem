using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.River30;
using i3.BusinessLogic.Channels.Env.Point.River30;

/// <summary>
/// 功能描述：双三十废水垂线编辑
/// 创建日期：2013-06-17
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_River30_River30VerticalEdit : PageBase
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
        TEnvPRiver30VVo TEnvPRiver30V = new TEnvPRiver30VVo();
        TEnvPRiver30V.ID = Request["id"].ToString();
        TEnvPRiver30VVo TEnvPRiver30VTemp = new TEnvPRiver30VLogic().Details(TEnvPRiver30V);
        return ToJson(TEnvPRiver30VTemp);
    }

    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPRiver30VVo TEnvPRiver30V = autoBindRequest(Request, new TEnvPRiver30VVo());
        TEnvPRiver30V.ID = GetSerialNumber(SerialType.T_ENV_P_RIVER30_V);
        TEnvPRiver30V.SECTION_ID = this.formId.Value;
        bool isSuccess = new TEnvPRiver30VLogic().Create(TEnvPRiver30V);
        if (isSuccess)
            WriteLog("添加双三十废水垂线", "", LogInfo.UserInfo.USER_NAME + "添加双三十废水垂线" + TEnvPRiver30V.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPRiver30VVo TEnvPRiver30V = autoBindRequest(Request, new TEnvPRiver30VVo());
        TEnvPRiver30V.ID = Request["id"].ToString();
        bool isSuccess = new TEnvPRiver30VLogic().Edit(TEnvPRiver30V);
        if (isSuccess)
            WriteLog("编辑双三十废水垂线", "", LogInfo.UserInfo.USER_NAME + "编辑双三十废水垂线" + TEnvPRiver30V.ID);
        return isSuccess == true ? "1" : "0";
    }
}