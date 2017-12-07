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

/// <summary>
/// 功能描述：标签编辑
/// 创建时间：2012-12-3
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Rpt_Mark_MarkEdit : PageBase
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
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TRptMarkVo objMark = new TRptMarkLogic().Details(Request["id"].ToString());
        return ToJson(objMark);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TRptMarkVo objMark = autoBindRequest(Request, new TRptMarkVo());
        objMark.ID = GetSerialNumber("Mark_Id");
        bool isSuccess = new TRptMarkLogic().Create(objMark);
        if (isSuccess)
            WriteLog("添加标签", "", LogInfo.UserInfo.USER_NAME + "添加标签" + objMark.ID);
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TRptMarkVo objMark = autoBindRequest(Request, new TRptMarkVo());
        objMark.ID = Request["id"].ToString();
        bool isSuccess = new TRptMarkLogic().Edit(objMark);
        if (isSuccess)
            WriteLog("编辑标签", "", LogInfo.UserInfo.USER_NAME + "编辑标签" + objMark.ID);
        return isSuccess == true ? "1" : "0";
    }
}