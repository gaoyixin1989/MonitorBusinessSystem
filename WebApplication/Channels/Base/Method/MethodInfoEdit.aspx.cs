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

using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;

/// <summary>
/// 功能描述：方法依据新增与编辑界面
/// 创建日期：2011-11-05
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Method_MethodInfoEdit : PageBase
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
        //获取字典项信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict(Request["dictType"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取监测类别信息
        if (Request["type"] != null && Request["type"].ToString() == "getMonitorType")
        {
            strResult = getMonitorType();
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
    /// 获取监测类别信息
    /// </summary>
    /// <returns></returns>
    public string getMonitorType()
    {
        TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(TBaseMonitorTypeInfoVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseMethodInfoVo TBaseMethodInfoVo = new TBaseMethodInfoVo();
        TBaseMethodInfoVo.ID = Request["id"].ToString();
        TBaseMethodInfoVo.IS_DEL = "0";
        TBaseMethodInfoVo TBaseMethodInfoVoTemp = new TBaseMethodInfoLogic().Details(TBaseMethodInfoVo);
        return ToJson(TBaseMethodInfoVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseMethodInfoVo TBaseMethodInfoVo = autoBindRequest(Request, new TBaseMethodInfoVo());
        TBaseMethodInfoVo.ID = GetSerialNumber("method_id");
        TBaseMethodInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseMethodInfoLogic().Create(TBaseMethodInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("新增方法依据", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增方法依据" + TBaseMethodInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseMethodInfoVo TBaseMethodInfoVo = autoBindRequest(Request, new TBaseMethodInfoVo());
        TBaseMethodInfoVo.ID = Request["id"].ToString();
        TBaseMethodInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseMethodInfoLogic().Edit(TBaseMethodInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改方法依据", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改方法依据" + TBaseMethodInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}