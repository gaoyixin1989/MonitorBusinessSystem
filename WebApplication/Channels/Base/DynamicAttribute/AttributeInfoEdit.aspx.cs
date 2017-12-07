using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;

/// <summary>
/// 功能描述：属性信息编辑
/// 创建日期：2012-11-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeInfoEdit : PageBase
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
        TBaseAttributeInfoVo TBaseAttributeInfoVo = new TBaseAttributeInfoVo();
        TBaseAttributeInfoVo.ID = Request["id"].ToString();
        TBaseAttributeInfoVo.IS_DEL = "0";
        TBaseAttributeInfoVo TBaseAttributeInfoVoTemp = new TBaseAttributeInfoLogic().Details(TBaseAttributeInfoVo);
        return ToJson(TBaseAttributeInfoVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseAttributeInfoVo TBaseAttributeInfoVo = autoBindRequest(Request, new TBaseAttributeInfoVo());
        TBaseAttributeInfoVo.ID = GetSerialNumber("AttributeInfo_Id");
        TBaseAttributeInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeInfoLogic().Create(TBaseAttributeInfoVo);
        if (isSuccess)
        {
            WriteLog("新增属性信息", "", LogInfo.UserInfo.USER_NAME + "新增属性信息" + TBaseAttributeInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseAttributeInfoVo TBaseAttributeInfoVo = autoBindRequest(Request, new TBaseAttributeInfoVo());
        TBaseAttributeInfoVo.ID = Request["id"].ToString();
        TBaseAttributeInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeInfoLogic().Edit(TBaseAttributeInfoVo);
        if (isSuccess)
        {
            WriteLog("修改属性信息", "", LogInfo.UserInfo.USER_NAME + "修改属性信息" + TBaseAttributeInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}