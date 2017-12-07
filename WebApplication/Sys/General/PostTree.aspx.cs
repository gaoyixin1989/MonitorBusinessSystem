using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：职位管理
/// 创建日期：2012-10-25 14:10
/// 创建人  ：潘德军
/// </summary>

public partial class Sys_General_PostTree : PageBase
{
    public StringBuilder postData = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindTree();// 绑定树，获取json数据

            if (Request.QueryString["Type"] != null)
            {
                string Result = "";
                string action = Request.QueryString["Type"];
                FunctionCallBack(action, ref Result);
                Response.Write(Result);
                Response.End();
            }
        }
    }

    //回调函数
    protected void FunctionCallBack(string action, ref  string Result)
    {
        switch (action)
        {
            case "AddTreeNode":
                Result = AddTreeNode(Request.QueryString["strAddParentID"], Request.QueryString["strPOST_NAME"], Request.QueryString["strPOST_LEVEL_ID"], Request.QueryString["strPOST_DEPT_ID"], Request.QueryString["strROLE_NOTE"]);
                break;
            case "EditTreeNode":
                Result = EditTreeNode(Request.QueryString["strEditPostID"], Request.QueryString["strPOST_NAME"], Request.QueryString["strPOST_LEVEL_ID"], Request.QueryString["strPOST_DEPT_ID"], Request.QueryString["strROLE_NOTE"]);
                break;
            case "DragTreeNode":
                Result = DragTreeNode(Request.QueryString["strDragNodeID"], Request.QueryString["strTargetNodeID"]);
                break;
            case "DelTreeNode":
                Result = DelTreeNode(Request.QueryString["strDelNodeID"]);
                break;
            case "getDictDept":
                Result = Get_Post_Dict("dept");
                break;
            case "getDictLevel":
                Result = Get_Post_Dict("duty_code");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 获取对应字典项类型的字典项text和code
    /// </summary>
    /// <param name="strDictType">字典项类型</param>
    /// <returns>字典项字符串“text,code|text,code|text,code”</returns>
    private  string Get_Post_Dict(string strDictType)
    {
        string strResult = "";
        TSysDictVo objDictVo = new TSysDictVo();
        objDictVo.DICT_TYPE = strDictType;
        objDictVo.SORT_FIELD = TSysDictVo.ORDER_ID_FIELD;
        objDictVo.SORT_TYPE = ConstValues.SortType.ASC;
        DataTable dt = new TSysDictLogic().SelectByTable(objDictVo, 0, 0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            strResult += (strResult.Length > 0 ? "|" : "") + dr[TSysDictVo.DICT_CODE_FIELD].ToString() + "," + dr[TSysDictVo.DICT_TEXT_FIELD].ToString();
        }

        return strResult;
    }

   /// <summary>
   /// 拖动树节点
   /// </summary>
   /// <param name="strDragNodeID">被拖动的树节点id</param>
   /// <param name="strTargetNodeID">放置的父节点id</param>
   /// <returns>无返回</returns>
    private string DragTreeNode(string strDragNodeID, string strTargetNodeID)
    {
        TSysPostVo objPostVo = new TSysPostVo();
        objPostVo.PARENT_POST_ID = strTargetNodeID;
        objPostVo.ID = strDragNodeID;
        new TSysPostLogic().Edit(objPostVo);

        base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUserPost, strDragNodeID, LogInfo.UserInfo.USER_NAME + "移动职位节点： " + strDragNodeID + " 及其子节点到" + strTargetNodeID + "之下成功!");
        return "";
    }

    /// <summary>
    /// 增加树节点
    /// </summary>
    /// <param name="strParentID">上级职位id</param>
    /// <param name="strPOST_NAME">职位名称</param>
    /// <param name="strPOST_LEVEL_ID">行政级别</param>
    /// <param name="strPOST_DEPT_ID">部门</param>
    /// <param name="strROLE_NOTE">职位说明</param>
    /// <returns>返回新节点id</returns>
    [WebMethod]
    private string AddTreeNode(string strParentID, string strPOST_NAME, string strPOST_LEVEL_ID, string strPOST_DEPT_ID, string strROLE_NOTE)
    {
        string strResult = "";
        TSysPostVo objPostVo = new TSysPostVo();
        objPostVo.ID = GetSerialNumber("t_sys_post_id");
        objPostVo.PARENT_POST_ID = strParentID;
        objPostVo.POST_NAME = strPOST_NAME;
        objPostVo.POST_LEVEL_ID = strPOST_LEVEL_ID;
        objPostVo.POST_DEPT_ID = strPOST_DEPT_ID;
        objPostVo.ROLE_NOTE = strROLE_NOTE;
        objPostVo.IS_DEL = "0";
        objPostVo.CREATE_ID = "";
        objPostVo.CREATE_TIME = System.DateTime.Now.ToShortDateString();
        new TSysPostLogic().Create(objPostVo);
        strResult = objPostVo.ID;

        base.WriteLog(i3.ValueObject.ObjectBase.LogType.AddSysPost, strResult, LogInfo.UserInfo.USER_NAME + "增加职位节点： " + strResult + " 到" + strParentID + "之下成功!");

        return strResult;
    }

    /// <summary>
    /// 编辑树节点
    /// </summary>
    /// <param name="strID">职位id</param>
    /// <param name="strPOST_NAME">职位名称</param>
    /// <param name="strPOST_LEVEL_ID">行政级别</param>
    /// <param name="strPOST_DEPT_ID">部门</param>
    /// <param name="strROLE_NOTE">职位说明</param>
    /// <returns>无返回</returns>
    [WebMethod]
    private string EditTreeNode(string strID, string strPOST_NAME, string strPOST_LEVEL_ID, string strPOST_DEPT_ID, string strROLE_NOTE)
    {
        TSysPostVo objPostVo = new TSysPostVo();
        objPostVo.ID = strID;
        objPostVo.POST_NAME = strPOST_NAME;
        objPostVo.POST_LEVEL_ID = strPOST_LEVEL_ID;
        objPostVo.POST_DEPT_ID = strPOST_DEPT_ID;
        objPostVo.ROLE_NOTE = strROLE_NOTE;
        new TSysPostLogic().Edit(objPostVo);

        base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditSysPost, strID, LogInfo.UserInfo.USER_NAME + "编辑职位： " + strID + " 成功!");

        return "";
    }

    /// <summary>
    /// 删除树节点，同时删除所有子节点
    /// </summary>
    /// <param name="strDelNodeID">职位id</param>
    /// <returns>无返回</returns>
    private string DelTreeNode(string strDelNodeID)
    {
        new TSysPostLogic().DeleteNode(strDelNodeID);

        base.WriteLog(i3.ValueObject.ObjectBase.LogType.DelSysPost, strDelNodeID, LogInfo.UserInfo.USER_NAME + "编辑职位： " + strDelNodeID + " 成功!");
        return "";
    }

    /// <summary>
    /// 绑定树，获取json数据
    /// </summary>
    private void BindTree()
    {
        TSysPostVo objPostVo = new TSysPostVo();
        objPostVo.IS_DEL = "0";
        DataTable dt = new TSysPostLogic().SelectByTable(objPostVo);

        if (dt.Rows.Count < 1)
            return;

        BindTreeRoot(dt, "0", 1);// 绑定根节点，获取json数据
    }

    /// <summary>
    /// 绑定根节点，获取json数据
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="strParentPostID">父节点的PostID</param>
    /// <param name="intTreeLevel">节点的级别</param>
    private void BindTreeRoot(DataTable dt, string strParentPostID, int intTreeLevel)
    {
        DataRow[] ds = dt.Select(TSysPostVo.PARENT_POST_ID_FIELD + "='" + strParentPostID + "'", TSysPostVo.NUM_FIELD + " " + ConstValues.SortType.ASC);
        if (ds.Length < 1)
            return;

        int intChildLevel = intTreeLevel + 1;

        postData.Append("[");
        for (int i = 0; i < ds.Length; i++)
        {
            DataRow dr = ds[i];

            postData.Append("{");
            BindField(dr);// 将数据字段绑定到json
            postData.Append("open:true");
            postData.Append(",");
            postData.Append("drag:false");
            //postData.Append(",");
            //postData.Append("isParent:true");
            //postData.Append(",");
           //postData.Append("icon:" + GetNodeIcon(dt, dr[TSysPostVo.ID_FIELD].ToString(), intTreeLevel));// 判断node的级别，从而决定其icon
            BindTreeNodes(dt, dr[TSysPostVo.ID_FIELD].ToString(), intChildLevel);// 绑定子节点，获取json数据
            postData.Append("}");

            if (i != ds.Length - 1)
            {
                postData.Append(",");
            }
        }
        postData.Append("]");
    }

    /// <summary>
    /// 绑定子节点，获取json数据
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="strParentPostID">父节点的PostID</param>
    /// <param name="intTreeLevel">节点的级别</param>
    private void BindTreeNodes(DataTable dt, string strParentPostID, int intTreeLevel)
    {
        DataRow[] ds = dt.Select(TSysPostVo.PARENT_POST_ID_FIELD + "='" + strParentPostID + "'", TSysPostVo.NUM_FIELD + " " + ConstValues.SortType.ASC);
        if (ds.Length < 1)
            return;

        int intChildLevel = intTreeLevel + 1;

        postData.Append(",children:[");
        for (int i = 0; i < ds.Length; i++)
        {
            DataRow dr = ds[i];

            postData.Append("{");
            BindField(dr);// 将数据字段绑定到json
            postData.Append("open:true");
            postData.Append(",");
            postData.Append("dropRoot:false");
            //postData.Append(",");
            //postData.Append("isParent:true");
            //postData.Append(",");
            //postData.Append("icon:" + GetNodeIcon(dt, dr[TSysPostVo.ID_FIELD].ToString(), intTreeLevel));// 判断node的级别，从而决定其icon
            BindTreeNodes(dt, dr[TSysPostVo.ID_FIELD].ToString(), intChildLevel);// 绑定子节点，获取json数据
            postData.Append("}");

            if (i != ds.Length - 1)
            {
                postData.Append(",");
            }
        }
        postData.Append("]");
    }

    /// <summary>
    /// 将数据字段绑定到json
    /// </summary>
    /// <param name="dr">DataRow</param>
    private void BindField(DataRow dr)
    {
        postData.Append("name:\"" + dr[TSysPostVo.POST_NAME_FIELD].ToString() + "\"");
        postData.Append(",");
        postData.Append("id:\"" + dr[TSysPostVo.ID_FIELD].ToString() + "\"");
        postData.Append(",");
        postData.Append("pId:\"" + dr[TSysPostVo.PARENT_POST_ID_FIELD].ToString() + "\"");
        postData.Append(",");
        postData.Append("POST_DEPT_ID:\"" + dr[TSysPostVo.POST_DEPT_ID_FIELD].ToString() + "\"");
        postData.Append(",");
        postData.Append("POST_LEVEL_ID:\"" + dr[TSysPostVo.POST_LEVEL_ID_FIELD].ToString() + "\"");
        postData.Append(",");
        postData.Append("ROLE_NOTE:\"" + dr[TSysPostVo.ROLE_NOTE_FIELD].ToString() + "\"");
        postData.Append(",");
        //postData.Append("TREE_LEVEL:\"" + dr[TSysPostVo.TREE_LEVEL_FIELD].ToString() + "\"");
        //postData.Append(",");
        //postData.Append("NUM:\"" + dr[TSysPostVo.NUM_FIELD].ToString() + "\"");
        //postData.Append(",");
    }

    /// <summary>
    /// 判断node的级别，从而决定其icon
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="strPostID">当前节点的PostID</param>
    /// <param name="intTreeLevel">当前节点的级别</param>
    /// <returns>节点图标字符串</returns>
    private string GetNodeIcon(DataTable dt, string strPostID, int intTreeLevel)
    {
        if (intTreeLevel == 1)
            return "\"../../Controls/zTree3.4/css/zTreeStyle/img/diy/1_close.png\"";

        int intChildLevel = intTreeLevel + 1;

        DataRow[] ds = dt.Select(TSysPostVo.PARENT_POST_ID_FIELD + "='" + strPostID + "' and " + TSysPostVo.IS_DEL_FIELD + "='0'", TSysPostVo.NUM_FIELD + " " + ConstValues.SortType.ASC);
        if (ds.Length < 1)
            return "\"../../Controls/zTree3.4/css/zTreeStyle/img/diy/2.png\"";
        else
            return "\"../../Controls/zTree3.4/css/zTreeStyle/img/diy/8.png\"";
    }
}