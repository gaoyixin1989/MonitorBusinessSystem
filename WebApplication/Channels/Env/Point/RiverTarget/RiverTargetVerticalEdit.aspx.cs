using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.RiverTarget;
using i3.BusinessLogic.Channels.Env.Point.RiverTarget;

/// <summary>
/// 功能描述：责任目标垂线编辑
/// 创建日期：2014-01-22
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_RiverTarget_RiverTargetVerticalEdit : PageBase
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
        TEnvPRiverTargetVVo TEnvPRiverTargetV = new TEnvPRiverTargetVVo();
        TEnvPRiverTargetV.ID = Request["id"].ToString();
        TEnvPRiverTargetVVo TEnvPRiverVTemp = new TEnvPRiverTargetVLogic().Details(TEnvPRiverTargetV);
        return ToJson(TEnvPRiverVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPRiverTargetVVo TEnvPRiverTargetV = autoBindRequest(Request, new TEnvPRiverTargetVVo());
        TEnvPRiverTargetV.ID = GetSerialNumber(SerialType.T_ENV_P_RIVER_TARGET_V);
        TEnvPRiverTargetV.SECTION_ID = this.formId.Value;
        bool isSuccess = new TEnvPRiverTargetVLogic().Create(TEnvPRiverTargetV);
        if (isSuccess)
            WriteLog("添加责任目标垂线", "", LogInfo.UserInfo.USER_NAME + "添加责任目标垂线" + TEnvPRiverTargetV.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPRiverTargetVVo TEnvPRiverTargetV = autoBindRequest(Request, new TEnvPRiverTargetVVo());
        TEnvPRiverTargetV.ID = Request["id"].ToString();
        bool isSuccess = new TEnvPRiverTargetVLogic().Edit(TEnvPRiverTargetV);
        if (isSuccess)
            WriteLog("编辑责任目标垂线", "", LogInfo.UserInfo.USER_NAME + "编辑责任目标垂线" + TEnvPRiverTargetV.ID);
        return isSuccess == true ? "1" : "0";
    }
}