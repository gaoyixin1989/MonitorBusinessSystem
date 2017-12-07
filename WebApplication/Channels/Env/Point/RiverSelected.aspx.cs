using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 功能描述：河流选择功能
/// 创建日期：2012-11-15
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_RiverSelected : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
        initFirstList();
    }

    public void initFirstList()
    {
        string strDictId = "";
        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.DICT_CODE = "watershed";
        DataTable dt = new TSysDictLogic().SelectByTable(TSysDictVo);
        if (dt.Rows.Count == 0) return;

        strDictId = dt.Rows[0]["ID"].ToString();
        TSysDictVo TSysDictVoTemp = new TSysDictVo();
        TSysDictVoTemp.PARENT_CODE = strDictId;
        DataTable objTable = new TSysDictLogic().SelectByTable(TSysDictVoTemp);


        this.oneList.DataSource = objTable;
        this.oneList.DataTextField = "DICT_TEXT";
        this.oneList.DataValueField = "ID";
        this.oneList.DataBind();
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.PARENT_CODE = Request["PARENT_CODE"].ToString();
        DataTable dt = new TSysDictLogic().SelectByTable(TSysDictVo);
        return DataTableToJson(dt);
    }
}