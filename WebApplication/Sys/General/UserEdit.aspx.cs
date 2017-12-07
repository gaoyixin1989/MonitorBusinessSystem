using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using i3.View;
using System.Web.Services;

using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述：用户编辑
/// 创建日期：2011-04-11 18:00
/// 创建人  ：郑义
/// 修改时间：2011-04-14 17:00
/// 修改人  ：郑义
/// 修改内容：更改所有符合开发规范
/// 修改时间：2012-11-19
/// 修改人：潘德军
/// 修改内容：操作模式修改，重构代码
/// </summary>
public partial class Sys_General_UserEdit:PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["strid"] != null)
        {
            strID = this.Request["strid"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strID);
        }
    }

    //获取数据
    private void GetData(string strID)
    {
        TSysUserVo objVo = new TSysUserLogic().Details(strID);
        objVo.USER_PWD = "";
        string strPostNames = "";
        string strPostIDs = "";
        GetPostStr(strID, ref strPostNames, ref strPostIDs);

        objVo.REMARK1 = strPostNames;
        objVo.REMARK2 = strPostIDs;

        string strJson = ToJson(objVo);

        Response.Write(strJson);
        Response.End();
    }

    private void GetPostStr(string strID,ref string strPostNames,ref string strPostIDs)
    {
        TSysUserPostVo objUserPost = new TSysUserPostVo();
        objUserPost.USER_ID = strID;
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(objUserPost);

        TSysPostVo objPost = new TSysPostVo();
        objPost.IS_DEL = "0";
        //objPost.IS_HIDE = "0";
        DataTable dtPost = new TSysPostLogic().SelectByTable(objPost);

        for (int i = 0; i < dtUserPost.Rows.Count; i++)
        {
            for (int j = 0; j < dtPost.Rows.Count; j++)
            {
                if (dtPost.Rows[j]["ID"].ToString() == dtUserPost.Rows[i]["POST_ID"].ToString())
                {
                    strPostNames += (strPostNames.Length > 0 ? "，" : "") + dtPost.Rows[j]["POST_NAME"].ToString();
                    strPostIDs += (strPostIDs.Length > 0 ? "，" : "") + dtPost.Rows[j]["ID"].ToString();
                }
            }
        }
    }
}