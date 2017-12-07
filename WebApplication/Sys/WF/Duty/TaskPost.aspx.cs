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
/// 功能描述：获取职位，仅为工作流环节权限下拉框 弹出grid使用
/// 创建日期：2013-01-09
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_WF_Duty_TaskPost : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        if (Request.Params["Action"] == "GetPost")
        {
            GetPost();
        }
    }

    //获取职位
    private void GetPost()
    {
        string strSortname = "NUM";
        string strSortorder = Request.Params["sortorder"];

        TSysPostVo objVo = new TSysPostVo();
        objVo.IS_DEL = "0";
        objVo.IS_HIDE = "0";
        objVo.SORT_FIELD = strSortname;
        objVo.SORT_TYPE = strSortorder;
        DataTable dt = new TSysPostLogic().SelectByTable(objVo, 0, 0);
        int intTotalCount = new TSysPostLogic().GetSelectResultCount(objVo);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
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

        TSysPostVo objVo = new TSysPostLogic().Details(strValue);

        for (int j = 0; j < lstDict.Count; j++)
        {
            if (lstDict[j].DICT_CODE == objVo.POST_DEPT_ID)
            {
                strResults = lstDict[j].DICT_TEXT;
            }
        }
        
        return strResults;
    }
}