using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：获取用户，仅为下拉框 弹出grid使用
/// 创建日期：2012-11-15
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_General_SelUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDataDictToControl("dept", this.ddlDept);
        }
        if (Request.Params["Action"] == "GetUsers")
        {
            GetUsers();
        }
    }

    //获取用户
    private void GetUsers()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhDept = (Request.Params["strSrhDept"] != null) ? Request.Params["strSrhDept"] : "";

        TSysUserLogic logicUser = new TSysUserLogic();

        int intTotalCount = logicUser.GetSelectByTableUnderDept(strSrhDept); ;//总计的数据条数
        DataTable dt = logicUser.SelectByTableUnderDept(strSrhDept, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }
}