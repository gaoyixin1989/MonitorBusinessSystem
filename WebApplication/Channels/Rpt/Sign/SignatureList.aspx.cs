using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：签名管理
/// 创建时间：2012-12-3
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_Sign_SignatureList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "getSignInfo")
                {
                    getSignInfo();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Action"]) && Request.QueryString["Action"] == "getDeptInfo")
                {
                    getDeptInfo();
                }
            }
        }
    }

    /// <summary>
    ///  获取部门信息
    /// </summary>
    protected void getDeptInfo()
    {
        DataTable dt = new TSysDictLogic().SelectByTable(new TSysDictVo()
        {
            DICT_TYPE = "dept",
            SORT_FIELD = TSysDictVo.ORDER_ID_FIELD,
            SORT_TYPE = SortDirection.Ascending.ToString()
        });
        //插入一条 全部部门 
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr["DICT_TEXT"] = "所有部门";
            dr["DICT_CODE"] = "";
            dt.Rows.InsertAt(dr, 0);
        }
        string strJson = CreateToJson(dt, dt.Rows.Count);
        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 获取签名信息
    /// </summary>
    protected void getSignInfo()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        //部门ID
        string strDept = !string.IsNullOrEmpty(Request.QueryString["deptId"]) ? Request.QueryString["deptId"].ToString() : "";

        TRptSignatureVo objSign = new TRptSignatureVo();
        objSign.SORT_FIELD = !string.IsNullOrEmpty(strSortname) ? strSortname : TRptSignatureVo.ID_FIELD;
        objSign.SORT_TYPE = strSortorder;
        //自定义查询使用
        intTotalCount = new TRptSignatureLogic().GetSelectResultCountByDept(objSign, strDept);
        dt = new TRptSignatureLogic().SelectByTableByDept(objSign, strDept, intPageIndex, intPageSize);

        string strJson = LigerGridDataToJson(dt, intTotalCount);
        Response.Write(strJson);
        Response.End();
    }

    /// <summary>
    /// 删除印章信息
    /// </summary>
    /// <param name="strValue">印章ID</param>
    /// <returns>1:success else fail</returns>
    [WebMethod]
    public static string deleteSign(string strValue)
    {
        if (!string.IsNullOrEmpty(strValue))
        {
            if (new TRptSignatureLogic().Delete(strValue))
            {
                new PageBase().WriteLog("删除印章", "", new UserLogInfo().UserInfo.USER_NAME + "删除印章" + strValue);
                return "1";
            }
        }
        return "0";
    }
}