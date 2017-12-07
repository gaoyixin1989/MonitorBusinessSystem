using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;

using i3.BusinessLogic.Channels.OA.ARCHIVES;
using i3.ValueObject.Channels.OA.ARCHIVES;
using System.Data;

/// <summary>
/// 功能描述：档案管理
/// 创建时间：20123-1-10
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_FileManage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "getFileInfo")
        {
            strResult = getFileInfo();
            Response.Write(strResult);
            Response.End();
        }
        //新增
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "addRoot")
        {
            strResult = addRoot();
            Response.Write(strResult);
            Response.End();
        }
        //删除
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "delRoot")
        {
            strResult = delRoot();
            Response.Write(strResult);
            Response.End();
        }
        //更新
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "editRoot")
        {
            strResult = editRoot();
            Response.Write(strResult);
            Response.End();
        }
        //文档信息获取
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDocumentInfo")
        {
            strResult = getDocumentInfo();
            Response.Write(strResult);
            Response.End();
        }
        //文档信息废止
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "delDocumentInfo")
        {
            strResult = delDocumentInfo();
            Response.Write(strResult);
            Response.End();
        }
        //文档信息销毁
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "destroyDocumentInfo")
        {
            strResult = destroyDocumentInfo();
            Response.Write(strResult);
            Response.End();
        }
        //借阅管理
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getBorrowDocument")
        {
            strResult = getDocumentBorrow();
            Response.Write(strResult);
            Response.End();
        }
        //借阅状态
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getBorrowStatus")
        {
            strResult = getBorrowStatus();
            Response.Write(strResult);
            Response.End();
        }
        //借阅信息删除
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteFile")
        {
            strResult = deleteFile();
            Response.Write(strResult);
            Response.End();
        }
        //分发管理
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getSendDocument")
        {
            strResult = getSendDocument();
            Response.Write(strResult);
            Response.End();
        }
        //借阅信息删除
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteSendFile")
        {
            strResult = deleteSendFile();
            Response.Write(strResult);
            Response.End();
        }
        //修订管理
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getCheckDocument")
        {
            strResult = getCheckDocument();
            Response.Write(strResult);
            Response.End();
        }
        //修订删除
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteCheckFile")
        {
            strResult = deleteCheckFile();
            Response.Write(strResult);
            Response.End();
        }
        //查新管理
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getUpdateDocument")
        {
            strResult = getUpdateDocument();
            Response.Write(strResult);
            Response.End();
        }
        //查新删除
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteUpdateFile")
        {
            strResult = deleteUpdateFile();
            Response.Write(strResult);
            Response.End();
        }
        //档案文件废止历史
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDelDocument")
        {
            strResult = getDelDocument();
            Response.Write(strResult);
            Response.End();
        }
        //档案文件销毁历史
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getDestroyDocument")
        {
            strResult = getDestroyDocument();
            Response.Write(strResult);
            Response.End();
        }
        if (!IsPostBack)
        { }
    }

    #region 档案目录管理
    /// <summary>
    /// 获取档案文件目录
    /// </summary>
    /// <returns></returns>
    protected string getFileInfo()
    {
        TOaArchivesDirectoryVo objArchiverDirectory = new TOaArchivesDirectoryVo();
        objArchiverDirectory.IS_USE = "0";
        DataTable dtDirectory = new TOaArchivesDirectoryLogic().SelectByTable(objArchiverDirectory);

        return LigerGridTreeDataToJson(dtDirectory, "PARENT_ID='0'", "ID", "PARENT_ID", "DIRECTORY_NAME");
    }
    /// <summary>
    /// 添加文档目录
    /// </summary>
    /// <returns>返回Json</returns>
    protected string addRoot()
    {
        TOaArchivesDirectoryVo objArchiverDirectory = new TOaArchivesDirectoryVo();
        TOaArchivesDirectoryLogic objArchiverDirectoryLogic = new TOaArchivesDirectoryLogic();
        objArchiverDirectory.PARENT_ID = Request.QueryString["parent_id"];
        objArchiverDirectory.IS_USE = "0";
        //生成子目录序号
        try
        {
            objArchiverDirectory.NUM = (Int32.Parse(objArchiverDirectoryLogic.getNum(objArchiverDirectory)) + 1).ToString();
        }
        catch
        {
            objArchiverDirectory.NUM = "1";
        }
        objArchiverDirectory.DIRECTORY_NAME = "新建文件夹" + objArchiverDirectory.NUM;
        objArchiverDirectory.ID = GetSerialNumber("t_oa_archives_directoty_id");
        if (objArchiverDirectoryLogic.Create(objArchiverDirectory))
        {
            WriteLog("添加文件目录", "", LogInfo.UserInfo.USER_NAME + "添加文件目录" + objArchiverDirectory.ID);
        }
        return ToJson(objArchiverDirectory);
    }
    /// <summary>
    /// 更新节点
    /// </summary>
    /// <returns></returns>
    protected string editRoot()
    {
        TOaArchivesDirectoryVo objArchiverDirectory = new TOaArchivesDirectoryVo();
        TOaArchivesDirectoryLogic objArchiverDirectoryLogic = new TOaArchivesDirectoryLogic();
        objArchiverDirectory.ID = Request.QueryString["id"];
        objArchiverDirectory.IS_USE = "0";
        objArchiverDirectory.DIRECTORY_NAME = Request.QueryString["name"];
        if (objArchiverDirectoryLogic.Edit(objArchiverDirectory))
        {
            WriteLog("编辑文件目录", "", LogInfo.UserInfo.USER_NAME + "编辑文件目录" + objArchiverDirectory.ID);
            return "true";
        }
        return "false";
    }
    /// <summary>
    /// 删除文档目录（级联删除其所有子目录）
    /// </summary>
    /// <returns></returns>
    protected string delRoot()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            TOaArchivesDirectoryVo objArchiverDirectory = new TOaArchivesDirectoryVo();
            objArchiverDirectory.IS_USE = "0";
            DataTable dtDirectory = new TOaArchivesDirectoryLogic().SelectByTable(objArchiverDirectory);
            string strAllID = GetAllSonByParent(dtDirectory, "PARENT_ID='" + Request.QueryString["id"] + "'", "ID", "PARENT_ID", "DIRECTORY_NAME");
            strAllID = strAllID.Length > 0 ? (Request.QueryString["id"] + "," + strAllID) : Request.QueryString["id"];
            if (strAllID != "" && new TOaArchivesDirectoryLogic().DeleteTran(strAllID))
            {
                WriteLog("删除文档目录及其子目录", "", LogInfo.UserInfo.USER_NAME + "删除文档目录及其子目录");
                return "true";
            }
        }
        return "false";
    }

    #region 递归获取所有子节点的ID
    /// <summary>
    /// 功能描述：递归获取所有子节点的ID
    /// 创建时间：2013-1-10
    /// 创建人：邵世卓
    /// </summary>
    /// <param name="dt">DataTable数据集</param>
    /// <param name="strFilter">select过滤条件</param>
    /// <param name="strParentID">父级ID项</param>
    /// <param name="strSonID">子级ID项(parent_id)</param>
    /// <returns>Json</returns>
    /// <summary>
    public string GetAllSonByParent(DataTable dt, string strFilter, string ID_Name, string ParentID_Name, string strFileName)
    {
        string listbanktype = MoveArea(dt, strFilter, ID_Name, ParentID_Name, strFileName);
        if (!string.IsNullOrEmpty(listbanktype))
            return listbanktype.Remove(listbanktype.LastIndexOf(","));
        return "";
    }

    /// <summary>
    /// 递归获取子级题库类型
    /// </summary>
    /// <param name="dtArea">数据集</param>
    /// <param name="strFilter">过滤字段</param>
    /// <returns></returns>
    public string MoveArea(DataTable objItemTb, string strFilter, string ID_Name, string ParentID_Name, string strFileName)
    {
        var rowParent = objItemTb.Select(strFilter);
        if (rowParent.Count() == 0)
        {
            return null;
        }
        string listChilren = "";
        for (int i = 0; i < rowParent.Count(); i++)
        {
            Dictionary<string, object> diTree = new Dictionary<string, object>();
            string id = rowParent[i][ID_Name].ToString();

            var next = objItemTb.Select(" " + ParentID_Name + " = '" + rowParent[i][ID_Name].ToString() + "'");

            listChilren += id + ",";
            string childrenRows = MoveArea(objItemTb, " " + ParentID_Name + " = '" + rowParent[i][ID_Name].ToString() + "'", ID_Name, ParentID_Name, strFileName);
            if (childrenRows != null)
            {
                listChilren += childrenRows;
            }
        }
        return listChilren;
    }
    #endregion
    #endregion

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

        if (!string.IsNullOrEmpty(Request.QueryString["root_id"]))
        {
            //档案文件
            TOaArchivesDocumentVo objArchivesDocument = new TOaArchivesDocumentVo();
            objArchivesDocument.SORT_FIELD = strSortname;
            objArchivesDocument.SORT_TYPE = strSortorder;

            objArchivesDocument.DIRECTORY_ID = Request.QueryString["root_id"];
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
            DataTable dtFile = new TOaArchivesDocumentLogic().SelectByTableForSearch(objArchivesDocument, intPageIndex, intPageSize);
            return CreateToJson(dtFile, intTotalCount);
        }
        return "";
    }

    /// <summary>
    /// 档案文件废止
    /// </summary>
    /// <returns></returns>
    protected string delDocumentInfo()
    {
        //构造档案文件对象
        TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentVo();
        objDocument.ID = Request.QueryString["document_id"];
        objDocument.IS_OVER = "1";//废止标识
        objDocument.OPERATOR = LogInfo.UserInfo.ID;
        objDocument.OPERATE_TIME = DateTime.Now.ToString();

        if (new TOaArchivesDocumentLogic().Edit(objDocument))
        {
            WriteLog("废止档案文件", "", LogInfo.UserInfo.USER_NAME + "废止档案文件" + objDocument.ID);
            return "1";
        }
        return "0";
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
        objDocument.IS_DEL = "1";//销毁标识
        objDocument.OPERATOR = LogInfo.UserInfo.ID;
        objDocument.OPERATE_TIME = DateTime.Now.ToString();

        if (new TOaArchivesDocumentLogic().Edit(objDocument))
        {
            WriteLog("销毁档案文件", "", LogInfo.UserInfo.USER_NAME + "销毁档案文件" + objDocument.ID);
            return "1";
        }
        return "0";
    }
    #endregion

    #region 借阅
    /// <summary>
    /// 获取档案借阅管理
    /// </summary>
    /// <returns></returns>
    protected string getDocumentBorrow()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaArchivesBorrowVo objBorrow = new TOaArchivesBorrowVo();
        objBorrow.SORT_FIELD = strSortname;
        objBorrow.SORT_TYPE = strSortorder;

        objBorrow.DOCUMENT_ID = Request.QueryString["document_id"].ToString();
        intTotalCount = new TOaArchivesBorrowLogic().GetSelectResultCountForSearch(objBorrow);
        dt = new TOaArchivesBorrowLogic().SelectByTableForSearch(objBorrow, intPageIndex, intPageSize);

        return CreateToJson(dt, intTotalCount);
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
    /// 档案借阅记录删除
    /// </summary>
    /// <returns></returns>
    protected string deleteFile()
    {
        //构建对象
        TOaArchivesBorrowVo objArchivesBorrow = new TOaArchivesBorrowVo();
        //删除文档借阅记录
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteFile")
        {
            WriteLog("档案文件借阅记录删除", "", LogInfo.UserInfo.USER_NAME + "档案文件借阅记录删除");
            return new TOaArchivesBorrowLogic().Delete(Request.QueryString["borrow_id"]) ? "1" : "0";
        }
        return "0";
    }
    #endregion

    #region 分发
    protected string getSendDocument()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaArchivesSendVo objSend = new TOaArchivesSendVo();
        objSend.SORT_FIELD = strSortname;
        objSend.SORT_TYPE = strSortorder;

        objSend.DOCUMENT_ID = Request.QueryString["document_id"].ToString();
        intTotalCount = new TOaArchivesSendLogic().GetSelectResultCountForSearch(objSend);
        dt = new TOaArchivesSendLogic().SelectByTableForSearch(objSend, intPageIndex, intPageSize);

        return CreateToJson(dt, intTotalCount);
    }

    /// <summary>
    /// 档案分发记录删除
    /// </summary>
    /// <returns></returns>
    protected string deleteSendFile()
    {
        //构建对象
        TOaArchivesSendVo objArchivesSend = new TOaArchivesSendVo();
        //删除文档借阅记录
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "deleteSendFile")
        {
            WriteLog("档案文件分发记录删除", "", LogInfo.UserInfo.USER_NAME + "档案文件分发记录删除");
            return new TOaArchivesSendLogic().Delete(Request.QueryString["send_id"]) ? "1" : "0";
        }
        return "0";
    }
    #endregion

    #region 修订
    /// <summary>
    /// 获取档案修订管理
    /// </summary>
    /// <returns></returns>
    protected string getCheckDocument()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaArchivesCheckVo objCheck = new TOaArchivesCheckVo();
        objCheck.SORT_FIELD = strSortname;
        objCheck.SORT_TYPE = strSortorder;

        objCheck.DOCUMENT_ID = Request.QueryString["document_id"].ToString();
        objCheck.IS_DESTROY = "0";
        intTotalCount = new TOaArchivesCheckLogic().GetSelectResultCountForSearch(objCheck);
        dt = new TOaArchivesCheckLogic().SelectByTableForSearch(objCheck, intPageIndex, intPageSize);

        return CreateToJson(dt, intTotalCount);
    }
    /// <summary>
    /// 删除修订记录
    /// </summary>
    /// <returns></returns>
    protected string deleteCheckFile()
    {
        //构造档案文件对象
        TOaArchivesCheckVo objCheck = new TOaArchivesCheckVo();
        objCheck.ID = Request.QueryString["check_id"];
        objCheck.IS_DESTROY = "1";//销毁标识
        objCheck.REMARK1 = LogInfo.UserInfo.ID;
        objCheck.REMARK2 = DateTime.Now.ToString();

        if (new TOaArchivesCheckLogic().Edit(objCheck))
        {
            WriteLog("删除修订记录", "", LogInfo.UserInfo.USER_NAME + "删除修订记录" + objCheck.ID);
            return "1";
        }
        return "0";
    }
    #endregion

    #region 查新
    /// <summary>
    /// 获取档案借阅管理
    /// </summary>
    /// <returns></returns>
    protected string getUpdateDocument()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaArchivesUpdateVo objUpdate = new TOaArchivesUpdateVo();
        objUpdate.SORT_FIELD = strSortname;
        objUpdate.SORT_TYPE = strSortorder;

        objUpdate.BEFORE_NAME = Request.QueryString["document_id"].ToString();
        objUpdate.IS_DEL = "0";
        intTotalCount = new TOaArchivesUpdateLogic().GetSelectResultCountForSearch(objUpdate);
        dt = new TOaArchivesUpdateLogic().SelectByTableForSearch(objUpdate, intPageIndex, intPageSize);

        return CreateToJson(dt, intTotalCount);
    }

    /// <summary>
    /// 删除查新记录
    /// </summary>
    /// <returns></returns>
    protected string deleteUpdateFile()
    {
        //构造档案文件对象
        TOaArchivesUpdateVo objUpdate = new TOaArchivesUpdateVo();
        objUpdate.ID = Request.QueryString["update_id"];
        objUpdate.IS_DEL = "1";//销毁标识
        objUpdate.REMARK1 = LogInfo.UserInfo.ID;
        objUpdate.REMARK2 = DateTime.Now.ToString();

        if (new TOaArchivesUpdateLogic().Edit(objUpdate))
        {
            WriteLog("删除查新记录", "", LogInfo.UserInfo.USER_NAME + "删除查新记录" + objUpdate.ID);
            return "1";
        }
        return "0";
    }
    #endregion

    #region 废止
    /// <summary>
    /// 删除废止记录
    /// </summary>
    /// <returns></returns>
    protected string getDelDocument()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (!string.IsNullOrEmpty(Request.QueryString["root_id"]))
        {
            TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentVo();
            objDocument.SORT_FIELD = strSortname;
            objDocument.SORT_TYPE = strSortorder;

            objDocument.IS_OVER = "1";
            objDocument.IS_DEL = "0";
            objDocument.DIRECTORY_ID = Request.QueryString["root_id"];
            intTotalCount = new TOaArchivesDocumentLogic().GetSelectResultCount(objDocument);
            dt = new TOaArchivesDocumentLogic().SelectByTableForDelete(objDocument, intPageIndex, intPageSize);

            return CreateToJson(dt, intTotalCount);
        }
        return "";
    }
    #endregion

    #region 销毁
    /// <summary>
    /// 删除销毁记录
    /// </summary>
    /// <returns></returns>
    protected string getDestroyDocument()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (!string.IsNullOrEmpty(Request.QueryString["root_id"]))
        {
            TOaArchivesDocumentVo objDocument = new TOaArchivesDocumentVo();
            objDocument.SORT_FIELD = strSortname;
            objDocument.SORT_TYPE = strSortorder;

            objDocument.IS_DEL = "1";
            objDocument.DIRECTORY_ID = Request.QueryString["root_id"];
            intTotalCount = new TOaArchivesDocumentLogic().GetSelectResultCount(objDocument);
            dt = new TOaArchivesDocumentLogic().SelectByTableForDelete(objDocument, intPageIndex, intPageSize);

            return CreateToJson(dt, intTotalCount);
        }
        return "";
    }
    #endregion
}