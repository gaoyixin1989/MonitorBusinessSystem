using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Company;
using i3.BusinessLogic.Channels.Base.Company;

/// <summary>
/// 功能描述：企业信息增加与编辑
/// 创建日期：2011-11-06
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Company_CompanyInfoEdit : PageBase
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
        //获取行业信息
        if (Request["type"] != null && Request["type"].ToString() == "getIndustry")
        {
            strResult = getIndustry();
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
    /// 获取行业信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    public static string getIndustry()
    {
        DataTable dt = new i3.BusinessLogic.Channels.Base.Industry.TBaseIndustryInfoLogic().SelectByTable(new i3.ValueObject.Channels.Base.Industry.TBaseIndustryInfoVo {IS_DEL="0" });
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseCompanyInfoVo TBaseCompanyInfoVo = new TBaseCompanyInfoVo();
        TBaseCompanyInfoVo.ID = Request["id"].ToString();
        TBaseCompanyInfoVo.IS_DEL = "0";
        TBaseCompanyInfoVo TBaseCompanyInfoVoTemp = new TBaseCompanyInfoLogic().Details(TBaseCompanyInfoVo);
        return ToJson(TBaseCompanyInfoVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseCompanyInfoVo TBaseCompanyInfoVo = autoBindRequest(Request, new TBaseCompanyInfoVo());
        TBaseCompanyInfoVo.ID = GetSerialNumber("Company_Id");
        TBaseCompanyInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseCompanyInfoLogic().Create(TBaseCompanyInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("新增企业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增企业信息" + TBaseCompanyInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseCompanyInfoVo TBaseCompanyInfoVo = autoBindRequest(Request, new TBaseCompanyInfoVo());
        TBaseCompanyInfoVo.ID = Request["id"].ToString();
        TBaseCompanyInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseCompanyInfoLogic().Edit(TBaseCompanyInfoVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改企业信息", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改企业信息" + TBaseCompanyInfoVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}