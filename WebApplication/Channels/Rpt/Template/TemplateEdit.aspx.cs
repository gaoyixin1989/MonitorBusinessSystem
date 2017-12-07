using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;

/// <summary>
///  功能描述：模板编辑
///  创建时间：2012-12-3
///  创建人：邵世卓
/// </summary>
public partial class Channels_Rpt_Template_TemplateEdit : PageBase
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
        //获取监测类别
        if (Request["type"] != null && Request["type"].ToString() == "getItemType")
        {
            strResult = getItemType();
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
        TRptTemplateVo objTemplate = new TRptTemplateLogic().Details(Request["id"].ToString());
        return ToJson(objTemplate);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TRptTemplateVo objTemplate = autoBindRequest(Request, new TRptTemplateVo());
        objTemplate.ID = GetSerialNumber("Mark_Id");
        objTemplate.ADD_TIME = DateTime.Now.ToString();
        objTemplate.ADD_USER = LogInfo.UserInfo.ID;
        objTemplate.IS_DEL = "0";
        bool isSuccess = new TRptTemplateLogic().Create(objTemplate);
        if (isSuccess)
        {
            WriteLog("添加模板", "", LogInfo.UserInfo.USER_NAME + "添加模板" + objTemplate.ID);
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TRptTemplateVo objTemplate = autoBindRequest(Request, new TRptTemplateVo());
        objTemplate.ID = Request["id"].ToString();
        bool isSuccess = new TRptTemplateLogic().Edit(objTemplate);
        if (isSuccess)
        {
            WriteLog("编辑模板", "", LogInfo.UserInfo.USER_NAME + "编辑模板" + objTemplate.ID);
        }
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获得监测类别
    /// </summary>
    /// <returns>Json</returns>
    protected string getItemType()
    {
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(new TBaseMonitorTypeInfoVo() { IS_DEL = "0" });
        return DataTableToJson(dt);
    }
}