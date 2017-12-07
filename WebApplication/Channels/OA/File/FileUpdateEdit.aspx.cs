using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.OA.ARCHIVES;
using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using System.Data;

/// <summary>
/// 功能描述：文档文件查新编辑
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_FileUpdateEdit : PageBase
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
        //档案文件编号赋值
        if (!string.IsNullOrEmpty(Request.QueryString["document_id"]))
        {
            this.document_id.Value = Request.QueryString["document_id"];
            TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentLogic().Details(Request.QueryString["document_id"]);
            this.document_code.Value = objDocument.DOCUMENT_CODE;
            this.document_name.Value = objDocument.DOCUMENT_NAME;
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
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TOaArchivesUpdateVo objUpdate = new TOaArchivesUpdateVo();
        objUpdate.ID = Request["id"].ToString();
        objUpdate = new TOaArchivesUpdateLogic().Details(objUpdate);
        //时间 格式
        try
        {
            objUpdate.UPDATE_TIME = DateTime.Parse(objUpdate.UPDATE_TIME).ToString("yyyy-MM-dd");
        }
        catch { }
        return ToJson(objUpdate);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaArchivesUpdateVo objUpdate = autoBindRequest(Request, new TOaArchivesUpdateVo());
        if (string.IsNullOrEmpty(Request.QueryString["document_id"]))
            return "0";
        objUpdate.ID = GetSerialNumber("t_oa_archivesupdate");
        objUpdate.BEFORE_NAME = Request.QueryString["document_id"];
        objUpdate.IS_DEL = "0";
        objUpdate.REMARK1 = LogInfo.UserInfo.ID;
        objUpdate.REMARK2 = DateTime.Now.ToString();
        bool isSuccess = new TOaArchivesUpdateLogic().Create(objUpdate);
        if (isSuccess)
        {
            WriteLog("添加查新记录", "", LogInfo.UserInfo.USER_NAME + "添加查新记录" + objUpdate.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
}