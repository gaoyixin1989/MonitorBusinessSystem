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
/// 用户选择职位
/// 创建日期：2012-11-20 9:00
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_General_UserEdit_SelPostTree : System.Web.UI.Page
{
    public StringBuilder postData = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindPostTree();
        }
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
    }
    #endregion
}