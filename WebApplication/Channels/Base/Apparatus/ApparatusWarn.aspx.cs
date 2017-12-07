using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;


/// <summary>
/// 功能描述：仪器检定报警
/// 创建日期：2013.7.15
/// 创建人  ：潘德军
/// 修改人：魏林 2013-07-24 增加仪器报废报警
/// </summary>
public partial class Channels_Base_Apparatus_ApparatusWarn : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取仪器信息
        if (Request["type"] != null && Request["type"].ToString() == "getApparatusWarn")
        {
            strResult = getApparatusWarn();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取仪器信息
    /// </summary>
    /// <returns></returns>
    private string getApparatusWarn()
    {
        int intTotalCount = 0;
        DataTable dt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseApparatusInfoVo TBaseApparatusInfoVo = new TBaseApparatusInfoVo();
        TBaseApparatusInfoVo.IS_DEL = "0";
        TBaseApparatusInfoVo.SORT_FIELD = strSortname;
        TBaseApparatusInfoVo.SORT_TYPE = strSortorder;

        string strAction = Request.Params["action"];
        if (strAction == "ident")
        {
            //检定报警
            dt = new TBaseApparatusInfoLogic().SelectByTable(TBaseApparatusInfoVo, intPageIndex, intPageSize, 2, 1);
            intTotalCount = new TBaseApparatusInfoLogic().GetSelectResultCount(TBaseApparatusInfoVo, 2, 1);
        }
        else
        {
            //报废报警
            dt = new TBaseApparatusInfoLogic().SelectByTable(TBaseApparatusInfoVo, intPageIndex, intPageSize, 2);
            intTotalCount = dt.Rows.Count;
        }
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }
}