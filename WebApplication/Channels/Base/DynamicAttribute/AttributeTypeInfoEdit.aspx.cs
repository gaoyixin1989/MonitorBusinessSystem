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
/// 功能描述：属性类别编辑
/// 创建日期：2012-11-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_DynamicAttribute_AttributeTypeInfoEdit : PageBase
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
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(new TBaseMonitorTypeInfoVo());
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseAttributeTypeVo TBaseAttributeTypeVo = new TBaseAttributeTypeVo();
        TBaseAttributeTypeVo.ID = Request["id"].ToString();
        TBaseAttributeTypeVo.IS_DEL = "0";
        TBaseAttributeTypeVo TBaseAttributeTypeVoTemp = new TBaseAttributeTypeLogic().Details(TBaseAttributeTypeVo);
        return ToJson(TBaseAttributeTypeVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseAttributeTypeVo TBaseAttributeTypeVo = autoBindRequest(Request, new TBaseAttributeTypeVo());
        TBaseAttributeTypeVo.ID = GetSerialNumber("AttributeType_Id");
        TBaseAttributeTypeVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeTypeLogic().Create(TBaseAttributeTypeVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("增加属性类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "增加属性类别" + TBaseAttributeTypeVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseAttributeTypeVo TBaseAttributeTypeVo = autoBindRequest(Request, new TBaseAttributeTypeVo());
        TBaseAttributeTypeVo.ID = Request["id"].ToString();
        TBaseAttributeTypeVo.IS_DEL = "0";
        bool isSuccess = new TBaseAttributeTypeLogic().Edit(TBaseAttributeTypeVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改属性类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改属性类别" + TBaseAttributeTypeVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}