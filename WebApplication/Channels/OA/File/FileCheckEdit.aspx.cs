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
/// 功能描述：文档文件修订
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_FileCheckEdit : PageBase
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
        TOaArchivesCheckVo objCheck = new TOaArchivesCheckVo();
        objCheck.ID = Request["id"].ToString();
        objCheck = new TOaArchivesCheckLogic().Details(objCheck);
        //时间 格式
        try
        {
            objCheck.UPDATE_DATE = DateTime.Parse(objCheck.UPDATE_DATE).ToString("yyyy-MM-dd");
        }
        catch { }
        return ToJson(objCheck);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaArchivesCheckVo objCheck = autoBindRequest(Request, new TOaArchivesCheckVo());
        objCheck.ID = GetSerialNumber("t_oa_archivescheck");
        objCheck.IS_DESTROY = "0";
        objCheck.REMARK1 = LogInfo.UserInfo.ID;
        objCheck.REMARK2 = DateTime.Now.ToString();
        bool isSuccess = new TOaArchivesCheckLogic().Create(objCheck);
        if (isSuccess)
        {
            WriteLog("添加修订记录", "", LogInfo.UserInfo.USER_NAME + "添加修订记录" + objCheck.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
}