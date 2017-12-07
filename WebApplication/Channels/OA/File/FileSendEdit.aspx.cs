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
/// 功能描述：文档文件借阅编辑
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_FileSendEdit : PageBase
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
        TOaArchivesSendVo objArchivesSend = new TOaArchivesSendLogic().Details(Request["id"].ToString());
        return ToJson(objArchivesSend);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaArchivesSendVo objArchivesSend = autoBindRequest(Request, new TOaArchivesSendVo());
        objArchivesSend.LENT_OUT_STATE = "1";//分发
        objArchivesSend.REMARK1 = DateTime.Now.ToString();

        bool isSuccess = new TOaArchivesSendLogic().CreateTrans(objArchivesSend);
        if (isSuccess)
        {
            WriteLog("添加分发记录", "", LogInfo.UserInfo.USER_NAME + "添加分发记录" + objArchivesSend.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TOaArchivesSendVo objArchivesSend = autoBindRequest(Request, new TOaArchivesSendVo());
        objArchivesSend.ID = Request["id"].ToString();
        objArchivesSend.REMARK1 = DateTime.Now.ToString();

        bool isSuccess = new TOaArchivesSendLogic().Edit(objArchivesSend);
        if (isSuccess)
        {
            WriteLog("修改分发记录", "", LogInfo.UserInfo.USER_NAME + "修改分发记录" + objArchivesSend.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
}