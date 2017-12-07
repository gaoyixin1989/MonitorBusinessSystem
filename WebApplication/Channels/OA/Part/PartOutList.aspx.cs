using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;

/// <summary>
/// 功能描述：物料入库历史记录明细
/// 创建日期：2013-02-02
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_OA_Part_PartOutList : PageBase
{
    string strPartId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestUrlParame();

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData_PART_CODE")
        {
            string strResult = frmLoadData_PART_CODE();
            Response.Write(strResult);
            Response.End();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData_PART_NAME")
        {
            string strResult = frmLoadData_PART_NAME();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取URL参数
    /// </summary>
    public void GetRequestUrlParame()
    {
        if (!String.IsNullOrEmpty(Request.Params["strPartId"].ToString()))
        {
            strPartId = Request.Params["strPartId"].Trim();
        }

    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData_PART_CODE()
    {
        if (strPartId.Length == 0)
            return "";

        TOaPartInfoVo objPart = new TOaPartInfoLogic().Details(strPartId);

        return objPart.PART_CODE;
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData_PART_NAME()
    {
        if (strPartId.Length == 0)
            return "";

        TOaPartInfoVo objPart = new TOaPartInfoLogic().Details(strPartId);

        return objPart.PART_NAME;
    }
}