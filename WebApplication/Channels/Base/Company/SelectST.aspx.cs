using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;

public partial class Channels_Base_Company_SelectST : PageBase
{
    public StringBuilder stData = new StringBuilder();
    public StringBuilder strSelNodeId = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["stType"] != null)
            {
                BindTree(Request.QueryString["stType"], Request.QueryString["MONITOR_ID"]);
                if (Request.QueryString["selNode"] != null && Request.QueryString["selNode"].Length>0)
                    strSelNodeId.Append(Request.QueryString["selNode"]);
                else
                    strSelNodeId.Append("00");
            }
        }
    }

    /// <summary>
    /// 绑定职位树，获取json数据
    /// </summary>
    private void BindTree(string strStType,string strMonitor)
    {
        TBaseEvaluationInfoVo objStVo = new TBaseEvaluationInfoVo();
        objStVo.IS_DEL = "0";
        objStVo.STANDARD_TYPE = strStType;
        objStVo.MONITOR_ID = strMonitor;
        DataTable dt = new TBaseEvaluationInfoLogic().SelectByTable(objStVo);

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
        DataRow[] ds = dt.Select();
        if (ds.Length < 1)
            return;

        stData.Append("[");
        for (int i = 0; i < ds.Length; i++)
        {
            DataRow dr = ds[i];

            string strStCodeAndName = dr[TBaseEvaluationInfoVo.STANDARD_CODE_FIELD].ToString() + " " + dr[TBaseEvaluationInfoVo.STANDARD_NAME_FIELD].ToString();
            stData.Append("{");
            stData.Append("name:\"" + strStCodeAndName + "\"");
            stData.Append(",");
            stData.Append("id:\"st" + dr[TBaseEvaluationInfoVo.ID_FIELD].ToString() + "\"");
            stData.Append(",");
            stData.Append("pId:\"0\"");
            stData.Append(",");
            stData.Append("pCode:\"" + dr[TBaseEvaluationInfoVo.STANDARD_CODE_FIELD].ToString()  + "\"");
            stData.Append(",");
            stData.Append("IsSt:\"1\"");
            stData.Append(",");
            stData.Append("open:false");
            BindTreeNodes(dr[TBaseEvaluationInfoVo.ID_FIELD].ToString(), "0", strStCodeAndName);// 绑定子节点，获取json数据
            stData.Append("}");

            if (i != ds.Length - 1)
            {
                stData.Append(",");
            }
        }
        stData.Append("]");
    }

    /// <summary>
    /// 绑定子节点，获取json数据
    /// </summary>
    /// <param name="strST_ID">标准id</param>
    /// <param name="strParentPostID">父节点的ID</param>
    private void BindTreeNodes(string strST_ID,string strParentID,string strParentName)
    {
        TBaseEvaluationConInfoVo objStCon = new TBaseEvaluationConInfoVo();
        objStCon.IS_DEL = "0";
        objStCon.STANDARD_ID = strST_ID;
        objStCon.PARENT_ID = strParentID;
        DataTable dt = new TBaseEvaluationConInfoLogic().SelectByTable(objStCon);

        DataRow[] ds = dt.Select();
        if (ds.Length < 1)
            return;

        stData.Append(",children:[");
        for (int i = 0; i < ds.Length; i++)
        {
            DataRow dr = ds[i];

            stData.Append("{");
            stData.Append("name:\"" + dr[TBaseEvaluationConInfoVo.CONDITION_NAME_FIELD].ToString() + "\"");
            stData.Append(",");
            stData.Append("id:\"" + dr[TBaseEvaluationConInfoVo.ID_FIELD].ToString() + "\"");
            stData.Append(",");
            stData.Append("pId:\"" + strParentID + "\"");
            stData.Append(",");
            stData.Append("pCode:\"" + strParentName + "\"");
            stData.Append(",");
            stData.Append("IsSt:\"0\"");
            stData.Append(",");
            stData.Append("open:false");
            BindTreeNodes(strST_ID, dr[TBaseEvaluationConInfoVo.ID_FIELD].ToString(), strParentName);// 绑定子节点，获取json数据
            stData.Append("}");

            if (i != ds.Length - 1)
            {
                stData.Append(",");
            }
        }
        stData.Append("]");
    }


}