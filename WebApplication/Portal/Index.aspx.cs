using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
/// <summary>
/// 功能描述：系统主页面
/// 创建日期：2011-04-12 15:30
/// 创建人  ：欧耀翔
/// 描述：将直接打开页面改为选项卡打开
/// 修改日期：2012-11-14
/// 修改人：潘德军
/// </summary>
public partial class Portal_Index : PageBase
{
    protected string TopMenu { get; private set; }
    protected string CenterMenu { get; private set; }
    protected string copyright = "";

    private string NodesData, nodes = "";

    protected string TreeLoad, LoadDiv, LoadNodesData;

    List<string> treenodes = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        TSysConfigVo objSysConfigVo = new TSysConfigVo();
        objSysConfigVo.CONFIG_CODE = "CopyRight";
        objSysConfigVo = new TSysConfigLogic().Details(objSysConfigVo);
        copyright = objSysConfigVo.REMARK;
        //BuildMainTopMemu();

        LoadDivModle();
    }
    /// <summary>
    /// 加载左侧菜单Div项,节点JSON数据， 加载tree数据方法  By Castle (胡方扬) 2012-10-24
    /// </summary>
    private void LoadDivModle()
    {

        if (LogInfo.UserInfo.USER_NAME == "" && LogInfo.UserInfo.ID != "000000001")
            return;
        DataTable dtUserMenu = null;
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.IS_SHORTCUT = "0";
        menuvo.MENU_TYPE = "Menu";
        if (LogInfo.UserInfo.ID == "000000001")
        {
            dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
        }
        else
        {
            menuvo.IS_HIDE = "0";
            //dtUserMenu = new TSysMenuLogic().GetMenuByUserName(menuvo, LogInfo.UserInfo.USER_NAME);
            dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, LogInfo.UserInfo.ID);
        }

        DataTable dt = new DataTable();
        dt = dtUserMenu.Copy();
        dt.Clear();

        DataRow[] drlist = dtUserMenu.Select(" PARENT_ID='0'", "ORDER_ID ASC");

        foreach (DataRow dr in drlist)
        {
            for (int i = 0; i < dtUserMenu.Rows.Count; i++)
            {
                if (dtUserMenu.Rows[i]["PARENT_ID"].ToString() == dr["ID"].ToString())
                {
                    //dt.Rows.Add(dtUserMenu.Rows[i].ItemArray);
                    dt.Rows.Add(dr.ItemArray);
                }
            }
        }

        DataView dv = new DataView(dt);
        dt = dv.ToTable(true);

        int dtCount = dt.Rows.Count;
        if (dtCount > 0)
        {
            //LoadDiv="<div class=\"leftInner\" id=\"mainLeft\"><ul>";
            LoadDiv = "<div class=\"leftInner\" id=\"mainLeft\"><ul>\r\n";
            for (int i = 0; i < dtCount; i++)
            {
                string openclass = "", Strstyle = "";
                if (i == 0)
                {
                    openclass = "class=\"open\"";
                    Strstyle = "style=\"display: block;\"";
                }
                LoadDiv += "<li " + openclass + "> \r\n <p><b>" + dt.Rows[i]["MENU_TEXT"] + "</b></p> \r\n";
                LoadDiv += "   <div class=\"leftCo\" " + Strstyle + "> \r\n";
                LoadDiv += "   <ul id=\"tree" + Convert.ToInt32(dt.Rows[i]["ID"]) + "\" class=\"ztree\"></ul> \r\n";
                LoadDiv += "   </div></li> \r\n";

                TreeLoad += "$.fn.zTree.init($(\"#tree" + Convert.ToInt32(dt.Rows[i]["ID"]) + "\"), setting, zNodes" + Convert.ToInt32(dt.Rows[i]["ID"]) + "); \r\n";

                GetMenuChild(dt.Rows[i]["ID"].ToString());

                NodesData = string.Join(",\r\n", treenodes.ToArray());

                LoadNodesData += " var zNodes" + Convert.ToInt32(dt.Rows[i]["ID"]) + "=[" + NodesData + "];\r\n";
              
                treenodes.Clear();
            }

            LoadDiv += "</ul></div> \r\n";
            LoadDiv += "<div class=\"arrow\" id=\"mainMiddle\"><span></span></div> \r\n";
        }
    }

    /// <summary>
    /// 根据父ID 获取子孙 无限极
    /// </summary>
    /// <param name="ParentId">父ID</param>
    private void GetMenuChild(string ParentId)
    {
        DataTable dtUserMenu = new DataTable();
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.IS_SHORTCUT = "0";
        menuvo.MENU_TYPE = "Menu";
        menuvo.PARENT_ID = ParentId;
        if (LogInfo.UserInfo.ID == "000000001")
        {
            dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
        }
        else
        {
            menuvo.IS_HIDE = "0";
            //dtUserMenu = new TSysMenuLogic().GetMenuByUserName(menuvo, LogInfo.UserInfo.USER_NAME);
            dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, LogInfo.UserInfo.ID);
        }

        DataView dv = new DataView(dtUserMenu);
        dv.Sort = " PARENT_ID,ORDER_ID ASC";
        //排序结果 重载dtUserMenu
        dtUserMenu = dv.ToTable();
        for (int i = 0; i < dtUserMenu.Rows.Count; i++)
        {
            nodes = "{ id:" + Convert.ToInt32(dtUserMenu.Rows[i]["ID"]) + ", pId:" + Convert.ToInt32(dtUserMenu.Rows[i]["PARENT_ID"]) + ", name:'" + dtUserMenu.Rows[i]["MENU_TEXT"] + "'";
            if (!String.IsNullOrEmpty(dtUserMenu.Rows[i]["MENU_URL"].ToString()))
            {
                nodes += ", menuurl:'../" + dtUserMenu.Rows[i]["MENU_URL"] + "'";
                //指定在什么地方打开链接 默认 _ablak（新窗口)、  _selft(当前窗口)
               //nodes += ", target:'mainFrm'";
            }
            if (i == 0)
            {
                nodes += ", open:true";
            }
            nodes += "}";
            treenodes.Add(nodes);
            //循环方法，取当前行的ID值作为父ID 查找儿子，依次循环 达到无限
            GetMenuChild(dtUserMenu.Rows[i]["ID"].ToString());
        }
    }
}
