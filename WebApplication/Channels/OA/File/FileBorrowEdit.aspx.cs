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
public partial class Channels_OA_File_FileBorrowEdit : PageBase
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
        TOaArchivesBorrowVo objArchivesBorrow = new TOaArchivesBorrowVo();
        objArchivesBorrow.ID = Request["id"].ToString();
        objArchivesBorrow = new TOaArchivesBorrowLogic().Details(objArchivesBorrow);
        return ToJson(objArchivesBorrow);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaArchivesBorrowVo objArchivesBorrow = autoBindRequest(Request, new TOaArchivesBorrowVo());
        objArchivesBorrow.ID = GetSerialNumber("t_oa_archivesborrow");
        objArchivesBorrow.LENT_OUT_STATE = "1";
        objArchivesBorrow.REMARK1 = DateTime.Now.ToString();
        //判断是否已借出
        if (IsBorrow())
        {
            return "2";
        }
        bool isSuccess = new TOaArchivesBorrowLogic().Create(objArchivesBorrow);
        if (isSuccess)
        {
            WriteLog("添加借出记录", "", LogInfo.UserInfo.USER_NAME + "添加借出记录" + objArchivesBorrow.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TOaArchivesBorrowVo objArchivesBorrow = autoBindRequest(Request, new TOaArchivesBorrowVo());
        objArchivesBorrow.ID = Request["id"].ToString();
        objArchivesBorrow.REMARK1 = DateTime.Now.ToString();

        bool isSuccess = new TOaArchivesBorrowLogic().Edit(objArchivesBorrow);
        if (isSuccess)
        {
            WriteLog("修改借出记录", "", LogInfo.UserInfo.USER_NAME + "修改借出记录" + objArchivesBorrow.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }

    //借出判断
    protected bool IsBorrow()
    {
        //构建对象
        TOaArchivesBorrowVo objArchivesBorrow = new TOaArchivesBorrowVo();
        //文档ID
        objArchivesBorrow.DOCUMENT_ID = Request.QueryString["document_id"];
        //取第一条数据即最后添加的数据
        DataTable dtBorrow = new TOaArchivesBorrowLogic().SelectByTable(objArchivesBorrow);
        DataRow[] drBorrow = new TOaArchivesBorrowLogic().SelectByTable(objArchivesBorrow).Select("1=1", " ID desc");
        if (drBorrow.Length > 0)
        {
            return drBorrow[0]["LENT_OUT_STATE"].ToString() == "1" ? true : false;
        }
        return false;
    }
}