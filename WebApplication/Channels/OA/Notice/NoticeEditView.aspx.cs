using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;
using System.IO;

using i3.ValueObject.Channels.OA.Notice;
using i3.BusinessLogic.Channels.OA.Notice;
using i3.ValueObject.Sys.General;


/// <summary>
/// 功能描述：公告查阅功能功能
/// 创建日期：2013-02-28
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_OA_Notice_NoticeEditView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] != null)
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
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.BUSINESS_ID = this.Request["id"].ToString();
        TOaAttVo.BUSINESS_TYPE = "OA_NOTICE";
        TOaAttVo TOaAttVoTemp = new TOaAttLogic().Details(TOaAttVo);
        string strUrl = TOaAttVoTemp.UPLOAD_PATH;
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        string filePath = mastPath + '\\' + strUrl;

        TOaNoticeVo TOaNoticeVo = new TOaNoticeVo();
        TOaNoticeVo.ID = Request["id"].ToString();
        TOaNoticeVo TOaNoticeVoTemp = new TOaNoticeLogic().Details(TOaNoticeVo);
        return ToJson(TOaNoticeVoTemp);
    }
}