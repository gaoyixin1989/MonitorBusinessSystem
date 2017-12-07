using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.BusinessLogic.Channels.OA.ARCHIVES;
using System.Data;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

public partial class Channels_OA_File_FileManageSearchEx :PageBase
{
    /// <summary>
    /// //档案审核（郑州）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        //文档信息销毁
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "destroyDocumentInfo")
        {
            strResult = destroyDocumentInfo();
            Response.Write(strResult);
            Response.End();
        }
        //时间修改
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "ModifyTime")
        {
           ModifyTime();
            Response.Write(strResult);
            Response.End();
        }
    }
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
    /// <summary>
    /// 档案文件销毁
    /// </summary>
    /// <returns></returns>
    protected string destroyDocumentInfo()
    {
        //构造档案文件对象
        TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentVo();
        objDocument.ID = Request.QueryString["document_id"];
        objDocument.IS_DEL = "2";//销毁标识
        objDocument.OPERATOR = LogInfo.UserInfo.ID;
        objDocument.OPERATE_TIME = DateTime.Now.ToString();
        if (new TOaArchivesDocumentLogic().Edit(objDocument))
        {
            WriteLog("销毁档案文件", "", LogInfo.UserInfo.USER_NAME + "销毁档案文件" + objDocument.ID);
            return "1";
        }
        return "0";
    }
    protected void  ModifyTime()
    {
        TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentVo();
        objDocument.UPDATE_DATE = Request.QueryString["UPDATE_DATE"];//颁布时间/修订时间修改
        objDocument.ID=Request.QueryString["ID"];
        bool b = new TOaArchivesDocumentLogic().update(objDocument);
    }
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