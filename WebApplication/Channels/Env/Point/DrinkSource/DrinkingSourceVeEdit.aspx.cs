using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.BusinessLogic.Channels.Env.Point.DrinkSource;

/// <summary>
/// 功能描述：饮用水源地垂线编辑管理
/// 创建日期：2013-06-07
/// 创建人  ：魏林
/// </summary>
public partial class Channels_Env_Point_DrinkSource_DrinkingSourceVeEdit : PageBase
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
        TEnvPDrinkSrcVVo TEnvPDrinkSrcV = new TEnvPDrinkSrcVVo();
        TEnvPDrinkSrcV.ID = Request["id"].ToString();
        TEnvPDrinkSrcVVo TEnvPDrinkSrcVTemp = new TEnvPDrinkSrcVLogic().Details(TEnvPDrinkSrcV);
        return ToJson(TEnvPDrinkSrcVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPDrinkSrcVVo TEnvPDrinkSrcV = autoBindRequest(Request, new TEnvPDrinkSrcVVo());
        TEnvPDrinkSrcV.ID = GetSerialNumber(SerialType.T_ENV_P_DRINK_SRC_V);
        TEnvPDrinkSrcV.SECTION_ID = this.formId.Value;
        bool isSuccess = new TEnvPDrinkSrcVLogic().Create(TEnvPDrinkSrcV);
        if (isSuccess)
            WriteLog("添加饮用水源地垂线", "", LogInfo.UserInfo.USER_NAME + "添加饮用水源地垂线" + TEnvPDrinkSrcV.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPDrinkSrcVVo TEnvPDrinkSrcV = autoBindRequest(Request, new TEnvPDrinkSrcVVo());
        TEnvPDrinkSrcV.ID = Request["id"].ToString();
        bool isSuccess = new TEnvPDrinkSrcVLogic().Edit(TEnvPDrinkSrcV);
        if (isSuccess)
            WriteLog("编辑饮用水源地垂线", "", LogInfo.UserInfo.USER_NAME + "编辑饮用水源地垂线" + TEnvPDrinkSrcV.ID);
        return isSuccess == true ? "1" : "0";
    }
}