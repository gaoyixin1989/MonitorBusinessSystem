using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 权限管理
/// 创建日期：2012-10-26 9:00
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_General_PostMenu : PageBase
{
    public StringBuilder postData = new StringBuilder();
    public StringBuilder MenuData = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindTree();

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
            case "ClickPostNode":
                Result = ClickPostTreeNode(Request.QueryString["ClickPostID"], Request.QueryString["strRightType"]);
                break;
            case "CheckMenuNode":
                Result = SaveCheckMenuNode(Request.QueryString["CheckPostID"], Request.QueryString["strChkMenuNodes"], Request.QueryString["strRightType"]);
                break;
            case "getDictDept":
                Result = Get_Post_Dict("dept");
                break;
            case "changeDdl_DictDept":
                Result = Get_Users(Request.QueryString["strSelDept"]);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 点击职位树节点或者用户列表，获取有权限菜单id字符串
    /// </summary>
    /// <param name="strPostID">职位或者用户id</param>
    /// <param name="strRightType">职位或者用户，1用户，2职位</param>
    /// <returns></returns>
    private string ClickPostTreeNode(string strPostID, string strRightType)
    {
        string strResult = "";

        TSysMenuPostVo objMenuPostVo = new TSysMenuPostVo();
        objMenuPostVo.RIGHT_TYPE = strRightType;
        objMenuPostVo.POST_ID = strPostID;
        DataTable dt = new TSysMenuPostLogic().SelectByTable(objMenuPostVo);

        if (dt.Rows.Count < 1)
            return "";

        for (int i=0;i<dt.Rows.Count;i++)
        {
            DataRow dr = dt.Rows[i];
            strResult += (strResult.Length >0 ? "|":"") + dr[TSysMenuPostVo.MENU_ID_FIELD].ToString();
        }

        return strResult;

    }

    /// <summary>
    /// 权限的菜单树节点选中，保存权限
    /// </summary>
    /// <param name="strPostID">职位或者用户id</param>
    /// <param name="strChkMenuNodes">选择节点id串</param>
    /// <param name="strRightType">职位或者用户，1用户，2职位</param>
    /// <returns></returns>
    private string SaveCheckMenuNode(string strPostID, string strChkMenuNodes,string strRightType)
    {
        string strlogName = "用户";
        if (strRightType == "2")
            strlogName = "职位";

        string strResult = "";

        if (strChkMenuNodes == "")//传入空串，将有关该职位或人员的权限全部删除
        {
            TSysMenuPostVo objMenuPostVoDel = new TSysMenuPostVo();
            objMenuPostVoDel.POST_ID = strPostID;
            objMenuPostVoDel.RIGHT_TYPE = strRightType;
            new TSysMenuPostLogic().Delete(objMenuPostVoDel);
            base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUserRight, objMenuPostVoDel.ID, LogInfo.UserInfo.USER_NAME + "删除" + strlogName + "： " + objMenuPostVoDel.ID + " 的所有树菜单权限成功!");
            return strResult;
        }

        string[] arrChkMenuNodes = strChkMenuNodes.Split('|');

        //查询该该职位或人员原有权限
        TSysMenuPostVo objMenuPostVo = new TSysMenuPostVo();
        objMenuPostVo.RIGHT_TYPE = strRightType;
        objMenuPostVo.POST_ID = strPostID;
        DataTable dt = new TSysMenuPostLogic().SelectByTable(objMenuPostVo);

        //查询不到，说明原来该职位或人员无权限，直接增加即可
        if (dt.Rows.Count < 1)
        {
            for (int i = 0; i < arrChkMenuNodes.Length; i++)
            {
                TSysMenuPostVo objMenuPostVoAdd = new TSysMenuPostVo();
                objMenuPostVoAdd.ID = GetSerialNumber("t_sys_menu_Post_id");
                objMenuPostVoAdd.POST_ID = strPostID;
                objMenuPostVoAdd.RIGHT_TYPE = strRightType;
                objMenuPostVoAdd.MENU_ID = arrChkMenuNodes[i];
                new TSysMenuPostLogic().Create(objMenuPostVoAdd);

                base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUserRight, objMenuPostVoAdd.ID, LogInfo.UserInfo.USER_NAME + "增加" + strlogName + "： " + objMenuPostVo.ID + " 的树菜单权限" + objMenuPostVoAdd.MENU_ID + "成功!");
            }
            return strResult;
        }

        //比对传来的权限字符串和数据库查询结果，进行比较
        //存在于权限字符串，而不存在于数据库的，增加
        //存在于数据库，而不存在于的权限字符串，删除
        string strRights = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strRights += (strRights.Length > 0 ? "|" : "") + dt.Rows[i][TSysMenuPostVo.MENU_ID_FIELD].ToString();
        }

        //存在于权限字符串，而不存在于数据库的，增加
        for (int i = 0; i < arrChkMenuNodes.Length; i++)
        {
            if (!strRights.Contains(arrChkMenuNodes[i]))
            {
                TSysMenuPostVo objMenuPostVoAdd = new TSysMenuPostVo();
                objMenuPostVoAdd.ID = GetSerialNumber("t_sys_menu_Post_id");
                objMenuPostVoAdd.POST_ID = strPostID;
                objMenuPostVoAdd.RIGHT_TYPE = strRightType;
                objMenuPostVoAdd.MENU_ID = arrChkMenuNodes[i];
                new TSysMenuPostLogic().Create(objMenuPostVoAdd);

                base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUserRight, objMenuPostVoAdd.ID, LogInfo.UserInfo.USER_NAME + "增加" + strlogName + "： " + objMenuPostVo.ID + " 的树菜单权限" + objMenuPostVoAdd.MENU_ID + "成功!");
            }
        }

        //存在于数据库，而不存在于的权限字符串，删除
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!strChkMenuNodes.Contains(dt.Rows[i][TSysMenuPostVo.MENU_ID_FIELD].ToString()))
            {
                TSysMenuPostVo objMenuPostVoDel = new TSysMenuPostVo();
                objMenuPostVoDel.POST_ID = strPostID;
                objMenuPostVoDel.RIGHT_TYPE = strRightType;
                objMenuPostVoDel.MENU_ID = dt.Rows[i][TSysMenuPostVo.MENU_ID_FIELD].ToString();
                new TSysMenuPostLogic().Delete(objMenuPostVoDel);

                base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditUserRight, objMenuPostVoDel.ID, LogInfo.UserInfo.USER_NAME + "删除" + strlogName + "： " + objMenuPostVo.ID + " 的树菜单权限" + objMenuPostVoDel.MENU_ID + "成功!");
            }
        }

        return strResult;
    }

    /// <summary>
    /// 获取对应字典项类型的字典项text和code
    /// </summary>
    /// <param name="strDictType">字典项类型</param>
    /// <returns>字典项字符串“text,code|text,code|text,code”</returns>
    private string Get_Post_Dict(string strDictType)
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
    /// 根据选中部门，获取用户数据
    /// </summary>
    /// <param name="strDeptID">部门id</param>
    /// <returns>用户id串</returns>
    private string Get_Users(string strDeptID)
    {
        string strResult = "";
        DataTable dt = new TSysUserLogic().SelectByTableUnderDept(strDeptID, 0, 0);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            strResult += (strResult.Length > 0 ? "|" : "") + dr[TSysUserVo.ID_FIELD].ToString() + "," + dr[TSysUserVo.REAL_NAME_FIELD].ToString();
        }

        return strResult;
    }
    
    #region 绑定树
    /// <summary>
    /// 绑定树
    /// </summary>
    private void BindTree()
    {
        BindPostTree();
        BindMenuTree();
    }

    #region bind post tree
    /// <summary>
    /// 绑定职位树，获取json数据
    /// </summary>
    private void BindPostTree()
    {
        TSysPostVo objPostVo = new TSysPostVo();
        objPostVo.IS_DEL = "0";
        DataTable dt = new TSysPostLogic().SelectByTable(objPostVo);

        if (dt.Rows.Count < 1)
            return;

        BindTreeRoot(dt, "0", 1);// 绑定根节点，获取json数据
    }

    /// <summary>
    /// 绑定职位根节点，获取json数据
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
    /// 绑定职位子节点，获取json数据
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
    #endregion

    #region bind menu tree
    /// <summary>
    /// 绑定菜单树，获取json数据
    /// </summary>
    private void BindMenuTree()
    {
        TSysMenuVo objMenuVo = new TSysMenuVo();
        objMenuVo.IS_DEL = "0";
        objMenuVo.IS_HIDE = "0";
        objMenuVo.IS_USE = "1";
        objMenuVo.SORT_FIELD = TSysMenuVo.ORDER_ID_FIELD;
        objMenuVo.SORT_TYPE = ConstValues.SortType.ASC;
        DataTable dt = new TSysMenuLogic().SelectByTable(objMenuVo, 0, 0);
        if (dt.Rows.Count < 1)
            return;

        string MenuIconPath = "../../Images/Menu/";
        MenuData.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            MenuData.Append("{");
            MenuData.Append("id:\"" + dr[TSysMenuVo.ID_FIELD].ToString() + "\",");
            MenuData.Append("pId:\"" + dr[TSysMenuVo.PARENT_ID_FIELD].ToString() + "\",");
            MenuData.Append("name:\"" + dr[TSysMenuVo.MENU_TEXT_FIELD].ToString() + "\",");
            MenuData.Append("icon:\"" + MenuIconPath + dr[TSysMenuVo.MENU_IMGURL_FIELD].ToString() + "\",");
            MenuData.Append("open:false");
            MenuData.Append("}");

            if (i != dt.Rows.Count - 1)
            {
                MenuData.Append(",");
            }
        }
        MenuData.Append("]");
    }
    #endregion
    #endregion
}