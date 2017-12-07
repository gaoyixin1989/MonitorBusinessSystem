using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.OA.ARCHIVES;
using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Channels.OA.EMPLOYE;

/// <summary>
/// 功能描述：人员选择
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_OA_File_SelectUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        // 获取用户信息
        if (Request["type"] != null && Request["type"].ToString() == "getUserInfo")
        {
            strResult = getUserInfo();
            Response.Write(strResult);
            Response.End();
        }
        // 获取部门
        if (Request["type"] != null && Request["type"].ToString() == "getDept")
        {
            strResult = getDept();
            Response.Write(strResult);
            Response.End();
        }
        // 获取用户信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    protected string getUserInfo()
    {
        int intTotalCount = 0;
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TOaEmployeInfoVo objEmploye = new TOaEmployeInfoVo();
        objEmploye.SORT_FIELD = strSortname;
        objEmploye.SORT_TYPE = strSortorder;
        objEmploye.DEPART = !string.IsNullOrEmpty(Request.QueryString["dept"]) ? Request.QueryString["dept"] : "";
        intTotalCount = new TOaEmployeInfoLogic().GetSelectResultCount(objEmploye);
        return CreateToJson(new TOaEmployeInfoLogic().SelectByTable(objEmploye, 0, 0), intTotalCount);
    }

    /// <summary>
    ///  获取部门
    /// </summary>
    /// <returns></returns>
    protected string getDept()
    {
        List<TSysDictVo> objDict = new TSysDictLogic().GetDataDictListByType("dept");
        objDict.Insert(0, new TSysDictVo() { DICT_TEXT = "所有", DICT_CODE = "" });
        return ToJson(objDict);
    }

    /// <summary>
    ///  获取在职状态
    /// </summary>
    /// <returns></returns>
    protected string getDict()
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(Request.QueryString["dict_code"], Request.QueryString["dict_type"]);
    }

}