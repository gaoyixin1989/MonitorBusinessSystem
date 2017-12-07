using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;


/// <summary>
/// 功能描述：仪器检定证书编辑页面
/// 创建日期：2011-11-05
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Apparatus_ApparatusInfoCertific : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] == null)
        {
            this.formStatus.Value = "add";
            this.formObjId.Value = this.Request["ApparatusId"].ToString();
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
        TBaseApparatusCertificVo TBaseApparatusCertificVo = new TBaseApparatusCertificVo();
        TBaseApparatusCertificVo.ID = Request["id"].ToString();
        TBaseApparatusCertificVo TBaseApparatusCertificVoTemp = new TBaseApparatusCertificLogic().Details(TBaseApparatusCertificVo);
        return ToJson(TBaseApparatusCertificVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseApparatusCertificVo TBaseApparatusCertificVo = autoBindRequest(Request, new TBaseApparatusCertificVo());
        TBaseApparatusCertificVo.ID = GetSerialNumber("ApparatusFileBak_Id");
        TBaseApparatusCertificVo.APPARATUS_ID = Request["formObjId"].ToString();
        bool isSuccess = new TBaseApparatusCertificLogic().Create(TBaseApparatusCertificVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("增加仪器检定证书", "", new PageBase().LogInfo.UserInfo.USER_NAME + "增加仪器检定证书" + TBaseApparatusCertificVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseApparatusCertificVo TBaseApparatusCertificVo = autoBindRequest(Request, new TBaseApparatusCertificVo());
        TBaseApparatusCertificVo.ID = Request["id"].ToString();
        bool isSuccess = new TBaseApparatusCertificLogic().Edit(TBaseApparatusCertificVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("修改仪器检定证书", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改仪器检定证书" + TBaseApparatusCertificVo.ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}