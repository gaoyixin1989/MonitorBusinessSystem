using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using System.Web.UI.HtmlControls;
using System.Web.Services;
/// <summary>
/// 功能描述：菜单管理(添加修改删除排序等功能)
/// 创建日期：2012-10-31
/// 创建人  ：胡方扬
/// 时间：2012-10-31
/// 修改人  ：

/// </summary>
public partial class Sys_General_MenuEdit : PageBase
{
    protected string MenuNodesJson;
    private string nodes, varNodes;
    public List<string> treenodes = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strAction = Request["type"];
            switch (strAction)
            {
                //获取字典项信息
                case "GetMenuChild":
                    GetMenuChild("0", "Menu");
                    MenuNodesJson = varNodes;
                    Response.Write(MenuNodesJson);
                    Response.End();
                    break;
                default: break;
            }
        }
    }
    #region 菜单增、删、改、移动函数
    /// <summary>
    /// 根据父ID 获取子孙 无限极
    /// </summary>
    /// <param name="ParentId">父ID</param>
    public void GetMenuChild(string ParentId, string strMenuType)
    {
        DataTable dtUserMenu = new DataTable();
        TSysMenuVo menuvo = new TSysMenuVo();
        menuvo.IS_DEL = "0";
        menuvo.IS_USE = "1";
        menuvo.MENU_TYPE = strMenuType;
        DataTable dtMenu = new TSysMenuLogic().SelectByTable(menuvo);
        menuvo.PARENT_ID = ParentId;
        if (new PageBase().LogInfo.UserInfo.ID == "000000001")
        {
            dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
        }
        else
        {
            menuvo.IS_HIDE = "0";
            //dtUserMenu = new TSysMenuLogic().GetMenuByUserName(menuvo, LogInfo.UserInfo.USER_NAME);
            dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, new PageBase().LogInfo.UserInfo.ID);
        }

        DataView dv = new DataView(dtUserMenu);
        dv.Sort = " PARENT_ID,ORDER_ID ASC";
        //排序结果 重载dtUserMenu
        dtUserMenu = dv.ToTable();
        for (int i = 0; i < dtUserMenu.Rows.Count; i++)
        {
            //nodes = "{ id:" + Convert.ToInt32(dtUserMenu.Rows[i]["ID"]) + ", pId:" + Convert.ToInt32(dtUserMenu.Rows[i]["PARENT_ID"]) + ", name:'" + dtUserMenu.Rows[i]["MENU_TEXT"] + "',icon:'../../Images/Menu/" + dtUserMenu.Rows[i]["MENU_IMGURL"] + "',IS_SHORTCUT:" + dtUserMenu.Rows[i]["IS_SHORTCUT"] + "";
            //有图标，貌似和加载速度关系不大
            nodes = "{ id:'" + dtUserMenu.Rows[i]["ID"] + "', pId:'" + dtUserMenu.Rows[i]["PARENT_ID"] + "', name:'" + dtUserMenu.Rows[i]["MENU_TEXT"] + "',icon:'../../Images/Menu/" + dtUserMenu.Rows[i]["MENU_IMGURL"] + "',IS_SHORTCUT:" + dtUserMenu.Rows[i]["IS_SHORTCUT"] + "";

            if (dtUserMenu.Rows[i]["PARENT_ID"].ToString() == "0" && i == 0)
            {
                nodes += ", open:true";
            }
            if (!String.IsNullOrEmpty(dtUserMenu.Rows[i]["MENU_URL"].ToString()))
            {
                nodes += ", url:'" + dtUserMenu.Rows[i]["MENU_URL"] + "'";
            }
            nodes += "}";
            treenodes.Add(nodes);
            //循环方法，取当前行的ID值作为父ID 查找儿子，依次循环 达到无限
            GetMenuChild(dtUserMenu.Rows[i]["ID"].ToString(), strMenuType);
        }
        //根节点
        string rootNode = "{id:'0',pId:'-1',name:'系统菜单管理',icon:'../../Images/Menu/home.gif',open:true},\r\n";
        varNodes = "[" + rootNode + String.Join(",\r\n", treenodes.ToArray()) + "]";
    }

    /// <summary>
    /// 添加新菜单
    /// </summary>
    /// <param name="ParentId">父ID</param>
    /// <param name="MenuText">菜单名称</param>
    /// <param name="StrUrl">菜单地址</param>
    /// <param name="Icon">菜单小图标</param>
    /// <param name="IsShutCut">是否快捷菜单</param>
    /// <returns>前台判断是否成功</returns>
    [WebMethod]
    public static string CreateMenuNode(string ParentId, string MenuText, string StrUrl, string Icon, string IsShutCut)
    {
        string result = "false";
        TSysMenuVo objMenuVo = new TSysMenuVo();
        objMenuVo = (TSysMenuVo)BindControlsToObject(objMenuVo);

        objMenuVo.ID = GetSerialNumber("sys_tree_id");

        objMenuVo.PARENT_ID = ParentId;
        objMenuVo.MENU_TEXT = MenuText;
        objMenuVo.MENU_URL = StrUrl;
        objMenuVo.MENU_IMGURL = Icon;
        objMenuVo.IS_SHORTCUT = IsShutCut;
        objMenuVo.MENU_TYPE = "Menu";
        objMenuVo.IS_USE = "1";
        objMenuVo.IS_DEL = "0";
        objMenuVo.IS_HIDE = "0";
        if (new TSysMenuLogic().Create(objMenuVo))
        {
            result = objMenuVo.ID.ToString();
            new PageBase().WriteLog("添加新菜单", "", new UserLogInfo().UserInfo.USER_NAME + "添加新菜单" + objMenuVo.ID);
        }
        return result;
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="Menu_id">菜单ID</param>
    /// <param name="ParentId">父ID</param>
    /// <param name="MenuText">菜单名称</param>
    /// <param name="StrUrl">菜单地址</param>
    /// <param name="Icon">菜单小图标</param>
    /// <param name="IsShutCut">是否快捷菜单</param>
    /// <returns>前台判断是否成功</returns>
    [WebMethod]
    public static string EditMenuNode(string Menu_id, string ParentId, string MenuText, string StrUrl, string Icon, string IsShutCut)
    {
        string result = "false";
        TSysMenuVo objMenuVo = new TSysMenuVo();
        //objMenuVo = (TSysMenuVo)BindControlsToObject(objMenuVo);
        objMenuVo.ID = Menu_id;
        objMenuVo.PARENT_ID = ParentId;
        objMenuVo.MENU_TEXT = MenuText;
        objMenuVo.MENU_URL = StrUrl;
        objMenuVo.MENU_IMGURL = Icon;
        objMenuVo.IS_SHORTCUT = IsShutCut;

        if (new TSysMenuLogic().EditData(objMenuVo))
        {
            result = "true";
            new PageBase().WriteLog("编辑菜单", "", new UserLogInfo().UserInfo.USER_NAME + "编辑菜单" + objMenuVo.ID);
        }
        return result;
    }
    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="Menu_id">菜单ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string DelMenuNode(string Menu_id)
    {
        string result = "false";
        TSysMenuVo objMenuVo = new TSysMenuVo();
        objMenuVo = (TSysMenuVo)BindControlsToObject(objMenuVo);
        if (new TSysMenuLogic().Delete(Menu_id))
        {
            result = "true";
            new PageBase().WriteLog("删除菜单", "", new UserLogInfo().UserInfo.USER_NAME + "删除菜单" + Menu_id);
        }
        return result;
    }


    /// <summary>
    /// 菜单排序
    /// </summary>
    /// <param name="strValue">排序的内容</param>
    /// <returns></returns>
    [WebMethod]
    public static string SortData(string strValue)
    {
        string result = "false";
        if (new TSysMenuLogic().UpdateSortByTransaction(strValue))
            result = "true";
        return result;
    }
    #endregion


}