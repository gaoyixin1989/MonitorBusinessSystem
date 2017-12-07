using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;


/// <summary>
/// 功能描述：属性配置信息编辑
/// 创建日期：2012-11-07
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeConfigEdit : PageBase
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
        //获取监测类别信息
        if (Request["type"] != null && Request["type"].ToString() == "getMonitorTypeInfo")
        {
            strResult = getMonitorTypeInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取属性类别
        if (Request["type"] != null && Request["type"].ToString() == "getAttributeTypeInfo")
        {
            strResult = getAttributeTypeInfo();
            Response.Write(strResult);
            Response.End();
        }
        //获取属性信息
        if (Request["type"] != null && Request["type"].ToString() == "getAttributeInfo")
        {
            strResult = getAttributeInfo();
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
    /// 获取监测类别信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    public static string getMonitorTypeInfo()
    {
        TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(TBaseMonitorTypeInfoVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取属性类别信息
    /// </summary>
    /// <returns></returns>
    public static string getAttributeTypeInfo()
    {
        TBaseAttributeTypeVo TBaseAttributeTypeVo = new TBaseAttributeTypeVo();
        TBaseAttributeTypeVo.IS_DEL = "0";
        DataTable dt = new TBaseAttributeTypeLogic().SelectByTable(TBaseAttributeTypeVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取属性信息
    /// </summary>
    /// <returns></returns>
    public static string getAttributeInfo()
    {
        TBaseAttributeInfoVo TBaseAttributeInfoVo = new TBaseAttributeInfoVo();
        TBaseAttributeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseAttributeInfoLogic().SelectByTable(TBaseAttributeInfoVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVo = new TBaseAttributeTypeValueVo();
        TBaseAttributeTypeValueVo.ID = Request["id"].ToString();
        TBaseAttributeTypeValueVo.IS_DEL = "0";
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVoTemp = new TBaseAttributeTypeValueLogic().Details(TBaseAttributeTypeValueVo);
        return ToJson(TBaseAttributeTypeValueVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVo = autoBindRequest(Request, new TBaseAttributeTypeValueVo());
        TBaseAttributeTypeValueVo.ID = GetSerialNumber("AttributeConfig_Id");
        TBaseAttributeTypeValueVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeTypeValueLogic().Create(TBaseAttributeTypeValueVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("增加属性配置", "", new PageBase().LogInfo.UserInfo.USER_NAME + "增加属性配置" + TBaseAttributeTypeValueVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseAttributeTypeValueVo TBaseAttributeTypeValueVo = autoBindRequest(Request, new TBaseAttributeTypeValueVo());
        TBaseAttributeTypeValueVo.ID = Request["id"].ToString();
        TBaseAttributeTypeValueVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeTypeValueLogic().Edit(TBaseAttributeTypeValueVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改属性配置", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改属性配置" + TBaseAttributeTypeValueVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}