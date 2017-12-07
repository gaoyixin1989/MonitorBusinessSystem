using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：获取用户，仅为工作流环节权限下拉框 弹出grid使用
/// 创建日期：2013-01-09
/// 创建人  ：潘德军
/// </summary>

public partial class Sys_WF_Duty_TaskUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        if (Request.Params["Action"] == "GetUsers")
        {
            GetUsers();
        }
    }

    //获取用户
    private void GetUsers()
    {
        string strSortname = "ORDER_ID";
        string strSortorder = Request.Params["sortorder"];

        TSysUserVo objVo = new TSysUserVo();
        objVo.IS_DEL = "0";
        objVo.IS_USE = "1";
        objVo.IS_HIDE = "0";
        objVo.SORT_FIELD = strSortname;
        objVo.SORT_TYPE = strSortorder;
        DataTable dt = new TSysUserLogic().SelectByTable_ByDept(objVo, 0, 0);
        int intTotalCount = new TSysUserLogic().GetSelectResultCount_ByDept(objVo);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取职位信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getPostName(string strValue)
    {
        string strResults = "";

        DataTable dt = new TSysPostLogic().SelectByTable_byUser(strValue);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strResults += (strResults.Length > 0 ? "，" : "") + dt.Rows[i]["POST_NAME"].ToString();
        }
        return strResults;
    }

    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDeptName(string strValue)
    {
        string strResults = "";

        List<TSysDictVo> lstDict = new TSysDictLogic().GetDataDictListByType("dept");

        DataTable dt = new TSysPostLogic().SelectByTable_byUser(strValue);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strDeptCode = dt.Rows[i]["POST_DEPT_ID"].ToString();
            if (strDeptCode.Length > 0)
            {
                for (int j = 0; j < lstDict.Count; j++)
                {
                    if (lstDict[j].DICT_CODE == strDeptCode)
                    {
                        if (strResults.IndexOf(lstDict[j].DICT_TEXT) < 0)
                            strResults += (strResults.Length > 0 ? "，" : "") + lstDict[j].DICT_TEXT;
                    }
                }
            }

        }
        return strResults;
    }
}