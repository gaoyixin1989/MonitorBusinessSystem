using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using i3.ValueObject.Sys.General;
/// <summary>
/// 功能描述：字典项管理
/// 创建日期：2012-10-25
/// 创建人  ：熊卫华
/// </summary>
public partial class Sys_Resource_DictDataEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strAction = Request["type"];
            string strResult = "";
            switch (strAction)
            {
                //获取字典项信息
                case "getDictInfo":
                    strResult = getDictInfo();
                    Response.Write(strResult);
                    Response.End();
                    break;
                default: break;
            }
        }
    }
    /// <summary>
    /// 获取字典项数据
    /// </summary>
    /// <returns></returns>
    protected string getDictInfo()
    {
        TSysDictVo TSysDictVo = new TSysDictVo();
        //如果不是系统超级管理员登陆，则显示部分字典项
        if (new PageBase().LogInfo.UserInfo.ID != "000000001")
            TSysDictVo.IS_HIDE = "0";

        DataTable dt = new TSysDictLogic().SelectByTable(TSysDictVo);
        return DataTableToJson(dt);
    }
    /// <summary>
    /// 删除字典项数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteData(string strValue)
    {
        bool isSuccess = new TSysDictLogic().deleteByTransaction(strValue);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除字典项数据", "", new UserLogInfo().UserInfo.USER_NAME + "删除字典项数据" + strValue);
            return "1";
        }
        else
            return "0";
    }
    /// <summary>
    /// 新增字典项数据
    /// </summary>
    /// <param name="strParentId">父节点ID</param>
    /// <param name="strDictType">字典项类型</param>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictName">字典项名称</param>
    /// <param name="strRemark">备注</param>
    /// <returns></returns>
    [WebMethod]
    public static string createData(string strParentId, string strDictType, string strDictCode, string strDictName, string strRemark)
    {
        string strDictId = GetSerialNumber("dict_id");
        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.ID = strDictId;
        TSysDictVo.DICT_TYPE = HttpContext.Current.Server.UrlDecode(strDictType);
        TSysDictVo.DICT_TEXT = HttpContext.Current.Server.UrlDecode(strDictName);
        TSysDictVo.DICT_CODE = HttpContext.Current.Server.UrlDecode(strDictCode);
        TSysDictVo.DICT_GROUP = "dict";
        TSysDictVo.PARENT_CODE = strParentId;
        TSysDictVo.AUTO_LOAD = "1";
        TSysDictVo.REMARK = strRemark;

        bool isSuccess = new TSysDictLogic().Create(TSysDictVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("新增字典项数据", "", new UserLogInfo().UserInfo.USER_NAME + "新增字典项数据" + TSysDictVo.ID);
            return strDictId;
        }
        else
            return "0";
    }
    /// <summary>
    /// 编辑字典项数据
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="strDictType">字典项类型</param>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictName">字典项名称</param>
    /// <param name="strRemark">备注</param>
    /// <returns></returns>
    [WebMethod]
    public static string editData(string id, string strDictType, string strDictCode, string strDictName, string strRemark)
    {
        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.ID = id;
        TSysDictVo.DICT_TYPE = HttpContext.Current.Server.UrlDecode(strDictType);
        TSysDictVo.DICT_TEXT = HttpContext.Current.Server.UrlDecode(strDictName);
        TSysDictVo.DICT_CODE = HttpContext.Current.Server.UrlDecode(strDictCode);
        TSysDictVo.REMARK = strRemark;
        bool isSuccess = new TSysDictLogic().Edit(TSysDictVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("编辑字典项数据", "", new UserLogInfo().UserInfo.USER_NAME + "编辑字典项数据" + TSysDictVo.ID);
            return "1";
        }
        else
            return "0";
    }
    /// <summary>
    /// 字典项排序
    /// </summary>
    /// <param name="strValue">排序的内容</param>
    /// <returns></returns>
    [WebMethod]
    public static string sortData(string strValue)
    {
        bool isSuccess = new TSysDictLogic().updateByTransaction(strValue);
        if (isSuccess)
            return "1";
        else
            return "0";
    }
}