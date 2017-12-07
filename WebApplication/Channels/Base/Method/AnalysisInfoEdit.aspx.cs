using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;

/// <summary>
/// 功能描述：分析方法编辑界面
/// 创建日期：2011-11-05
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Method_AnalysisInfoEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] == null)
        {
            this.formStatus.Value = "add";
            this.formObjId.Value = this.Request["MethodId"].ToString();
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
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseMethodAnalysisVo TBaseMethodAnalysisVo = new TBaseMethodAnalysisVo();
        TBaseMethodAnalysisVo.ID = Request["id"].ToString();
        TBaseMethodAnalysisVo TBaseMethodAnalysisVoTemp = new TBaseMethodAnalysisLogic().Details(TBaseMethodAnalysisVo);
        return ToJson(TBaseMethodAnalysisVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseMethodAnalysisVo TBaseMethodAnalysisVo = autoBindRequest(Request, new TBaseMethodAnalysisVo());
        TBaseMethodAnalysisVo.ID = GetSerialNumber("Analysis_Id");
        TBaseMethodAnalysisVo.METHOD_ID = Request["formObjId"].ToString();
        TBaseMethodAnalysisVo.IS_DEL = "0";
        bool isSuccess = new TBaseMethodAnalysisLogic().Create(TBaseMethodAnalysisVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("新增分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增分析方法" + TBaseMethodAnalysisVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseMethodAnalysisVo TBaseMethodAnalysisVo = autoBindRequest(Request, new TBaseMethodAnalysisVo());
        TBaseMethodAnalysisVo.ID = Request["id"].ToString();
        bool isSuccess = new TBaseMethodAnalysisLogic().Edit(TBaseMethodAnalysisVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改分析方法", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改分析方法" + TBaseMethodAnalysisVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}