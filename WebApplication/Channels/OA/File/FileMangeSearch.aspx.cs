using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.OA.ARCHIVES;
using System.Data;
using i3.BusinessLogic.Channels.OA.ARCHIVES;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

public partial class Channels_OA_File_FileMangeSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //文档信息获取
        string strResult = "";
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getDocumentInfo")
        {
            strResult = getDocumentInfo();
            Response.Write(strResult);
            Response.End();
        }
        //借阅状态
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getBorrowStatus")
        {
            strResult = getBorrowStatus();
            Response.Write(strResult);
            Response.End();
        }
        // 获取字典项下拉控件Json数据
        if (Request["action"] != null && Request["action"].ToString() == "getDictJson")
        {
            strResult = GetDictJson();
            Response.Write(strResult);
            Response.End();
        }
        // 获取字典项下拉控件Json数据
        if (Request["action"] != null && Request["action"].ToString() == "getDictJsonForSearch")
        {
            strResult = GetDictJson();
            Response.Write(strResult);
            Response.End();
        }
    }

    #region 档案文件管理
    /// <summary>
    /// 获取档案文件
    /// </summary>
    /// <returns></returns>
    protected string getDocumentInfo()
    {
        int intTotalCount = 0;
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //档案文件
        TOaArchivesDocumentVo objArchivesDocument = new TOaArchivesDocumentVo();
        objArchivesDocument.SORT_FIELD = strSortname;
        objArchivesDocument.SORT_TYPE = strSortorder;
        //档案编号
        if (!string.IsNullOrEmpty(Request.QueryString["document_code"]))
            objArchivesDocument.DOCUMENT_CODE = Request.QueryString["document_code"];
        //保存类型
        if (!string.IsNullOrEmpty(Request.QueryString["save_type"]))
            objArchivesDocument.SAVE_TYPE = Request.QueryString["save_type"];
        //档案名称
        if (!string.IsNullOrEmpty(Request.QueryString["document_name"]))
            objArchivesDocument.DOCUMENT_NAME = Request.QueryString["document_name"];
        //主题词/关键字
        if (!string.IsNullOrEmpty(Request.QueryString["p_key"]))
            objArchivesDocument.P_KEY = Request.QueryString["p_key"];
        //存放位置
        if (!string.IsNullOrEmpty(Request.QueryString["document_location"]))
            objArchivesDocument.DOCUMENT_LOCATION = Request.QueryString["document_location"];
        intTotalCount = new TOaArchivesDocumentLogic().SelectByTableForSearchCount(objArchivesDocument);
        DataTable dtFile = new TOaArchivesDocumentLogic().SelectTableForSearch(objArchivesDocument, intPageIndex, intPageSize);
        return CreateToJson(dtFile, intTotalCount);
    }
    #endregion
    /// <summary>
    ///  获取档案文件借阅状态
    /// </summary>
    /// <returns></returns>
    protected string getBorrowStatus()
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
            return drBorrow[0]["LENT_OUT_STATE"].ToString();
        }
        return "";
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