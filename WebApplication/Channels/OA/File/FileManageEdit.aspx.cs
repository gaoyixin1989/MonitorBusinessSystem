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

/// <summary>
/// 功能描述：文档文件编辑
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_FileManageEdit : PageBase
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
        // 获取字典项下拉控件Json数据
        if (Request["type"] != null && Request["type"].ToString() == "getDictJson")
        {
            strResult = GetDictJson();
            Response.Write(strResult);
            Response.End();
        }
        // 获取字典项下拉控件Json数据 (查询)
        if (Request["type"] != null && Request["type"].ToString() == "getDictJsonForSearch")
        {
            strResult = GetDictJsonForSearch();
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
        TOaArchivesDocumentVo objArchivesDocument = new TOaArchivesDocumentVo();
        objArchivesDocument.ID = Request["id"].ToString();
        objArchivesDocument = new TOaArchivesDocumentLogic().Details(objArchivesDocument);
        return ToJson(objArchivesDocument);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaArchivesDocumentVo objArchivesDocument = autoBindRequest(Request, new TOaArchivesDocumentVo());
        objArchivesDocument.ID = GetSerialNumber("t_oa_archivesdocument");
        objArchivesDocument.DIRECTORY_ID = Request.QueryString["directory_id"];
        objArchivesDocument.IS_DEL = "0";
        objArchivesDocument.IS_OVER = "0";
        bool isSuccess = new TOaArchivesDocumentLogic().Create(objArchivesDocument);
        if (isSuccess)
        {
            WriteLog("添加档案文件", "", LogInfo.UserInfo.USER_NAME + "添加档案文件" + objArchivesDocument.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TOaArchivesDocumentVo objArchivesDocument = autoBindRequest(Request, new TOaArchivesDocumentVo());
        objArchivesDocument.ID = Request["id"].ToString();
        bool isSuccess = new TOaArchivesDocumentLogic().Edit(objArchivesDocument);
        if (isSuccess)
        {
            WriteLog("编辑档案文件", "", LogInfo.UserInfo.USER_NAME + "编辑档案文件" + objArchivesDocument.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取字典项下拉控件Json数据
    /// </summary>
    /// <returns></returns>
    protected string GetDictJson()
    {
        //字典项类型
        string strDictType = Request.QueryString["dict_type"];
        List<TSysDictVo> listDict = new TSysDictLogic().GetAutoLoadDataListByType(strDictType);
        return ToJson(listDict);
    }

    /// <summary>
    /// 获取字典项下拉控件Json数据
    /// </summary>
    /// <returns></returns>
    protected string GetDictJsonForSearch()
    {
        //字典项类型
        string strDictType = Request.QueryString["dict_type"];
        List<TSysDictVo> listDict = new TSysDictLogic().GetAutoLoadDataListByType(strDictType);
        //插入全部
        listDict.Insert(0, new TSysDictVo() { DICT_TEXT = "全部", DICT_CODE = "" });
        return ToJson(listDict);
    }

}