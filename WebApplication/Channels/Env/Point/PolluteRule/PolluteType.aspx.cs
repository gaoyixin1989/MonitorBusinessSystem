using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_PolluteRule_PolluteType : PageBase
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
        {
            this.formId.Value = this.Request["oneGridId"].ToString();
        }
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
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPPolluteTypeVo TEnvPRiverV = new TEnvPPolluteTypeVo();
        TEnvPRiverV.ID = Request["id"].ToString(); 
        TEnvPPolluteTypeVo TEnvPRiverVTemp = new TEnvPPolluteTypeLogic().Details(TEnvPRiverV);
        return ToJson(TEnvPRiverVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        bool isSuccess = true;
        TEnvPPolluteTypeVo TEnvPPolluteType = autoBindRequest(Request, new TEnvPPolluteTypeVo());
        TEnvPPolluteType.ID = GetSerialNumber(SerialType.T_ENV_POINT_POLLUTETYPE);
        TEnvPPolluteType.SATAIONS_ID = this.formId.Value;
        TEnvPPolluteType.IS_DEL = "0";
        isSuccess = new TEnvPPolluteTypeLogic().Create(TEnvPPolluteType);
        if (isSuccess)
        {
            WriteLog("添加类别", "", LogInfo.UserInfo.USER_NAME + "添加类别" + TEnvPPolluteType.ID);
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        bool isSuccess = true;
        TEnvPPolluteTypeVo TEnvPRiverV = autoBindRequest(Request, new TEnvPPolluteTypeVo());
        TEnvPRiverV.ID = Request["id"].ToString();
        TEnvPRiverV.IS_DEL = "0";
        isSuccess = new TEnvPPolluteTypeLogic().Edit(TEnvPRiverV);
        if (isSuccess)
            WriteLog("编辑类别", "", LogInfo.UserInfo.USER_NAME + "编辑类别" + TEnvPRiverV.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
}