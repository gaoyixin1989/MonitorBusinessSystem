using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.General;
using i3.DataAccess.Sys.General;

namespace i3.BusinessLogic.Sys.General
{
    /// <summary>
    /// 功能：菜单管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysMenuLogic : LogicBase
    {

        TSysMenuVo tSysMenu = new TSysMenuVo();
        TSysMenuAccess access;

        public TSysMenuLogic()
        {
            access = new TSysMenuAccess();
        }

        public TSysMenuLogic(TSysMenuVo _tSysMenu)
        {
            tSysMenu = _tSysMenu;
            access = new TSysMenuAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuVo tSysMenu)
        {
            return access.GetSelectResultCount(tSysMenu);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenu">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuVo Details(TSysMenuVo tSysMenu)
        {
            return access.Details(tSysMenu);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysMenuVo> SelectByObject(TSysMenuVo tSysMenu, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysMenu, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysMenuVo tSysMenu, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysMenu, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuVo tSysMenu)
        {
            return access.SelectByTable(tSysMenu);
        }
        /// <summary>
        /// 特定方法
        /// 根据指定用户获取合法菜单
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public DataTable GetMenuByUserName(TSysMenuVo tSysMenu,string strUserName)
        {
            return access.GetMenuByUserName(tSysMenu,strUserName);
        }
        /// <summary>
        /// 特定方法
        /// 根据指定用户获取合法菜单
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public DataTable GetMenuByUserID(TSysMenuVo tSysMenu, string strUserID)
        {
            return access.GetMenuByUserID(tSysMenu, strUserID);
        }

        public string GetMenuByUserIDForLogin(string strUserID)
        {
            string strToJson = "";

            DataTable dtUserMenu = null;
            TSysMenuVo menuvo = new TSysMenuVo();
            menuvo.IS_DEL = "0";
            menuvo.IS_USE = "1";
            menuvo.IS_SHORTCUT = "0";
            menuvo.MENU_TYPE = "Menu";
            if (strUserID == "000000001")
            {
                dtUserMenu = new TSysMenuLogic().SelectByTable(menuvo);
            }
            else
            {
                menuvo.IS_HIDE = "0";
                dtUserMenu = new TSysMenuLogic().GetMenuByUserID(menuvo, strUserID);
            }

            var user = new TSysUserLogic().SelectByObject(new TSysUserVo { ID = strUserID });

            DataRow[] drlist = dtUserMenu.Select(" PARENT_ID='0'", "ORDER_ID ASC");
            if (drlist.Length == 0)
                return "";

            foreach (DataRow dr in drlist)
            {
                string strPID = dr["ID"].ToString();
                string strMenuText = dr["MENU_TEXT"].ToString();

                strToJson += strToJson.Length > 0 ? "," : "";
                strToJson += "{";
                strToJson += "\"id\":\"" + strPID + "\",";
                strToJson += "\"MenuName\":\"" + strMenuText + "\",";
                strToJson += "\"MenuID\":\"" + strPID + "\",";
                strToJson += "\"text\":\"" + strMenuText + "\",";
                strToJson += "\"MenuUrl\":null,";
                strToJson += "\"MenuNo\":\"" + strPID + "\",";
                strToJson += "\"MenuParentNo\":null,";

                DataRow[] drChileList = dtUserMenu.Select(" PARENT_ID='" + strPID + "'", "ORDER_ID ASC");

                if (drChileList.Length > 0)
                {
                    strToJson += "\"children\":[";
                    for (int i = 0; i < drChileList.Length; i++)
                    {
                        DataRow drchild = drChileList[i];
                        if (i>0)
                            strToJson += "," ;

                        string strChildID = drchild["ID"].ToString();
                        string strChildMenuText = drchild["MENU_TEXT"].ToString();
                        string strChildMenuUrl = drchild["MENU_URL"].ToString();

                        strToJson += "{";
                        strToJson += "\"id\":\"" + strChildID + "\",";
                        strToJson += "\"MenuName\":\"" + strChildMenuText + "\",";
                        strToJson += "\"MenuID\":\"" + strChildID + "\",";
                        strToJson += "\"text\":\"" + strChildMenuText + "\",";
                        //strToJson += "\"MenuUrl\":\"" + strChildMenuUrl + "\",";
                        strToJson += "\"MenuUrl\":\"" + AddUrlUserParams(strChildMenuUrl,user) + "\",";
                        strToJson += "\"MenuNo\":\"" + strChildID + "\",";
                        strToJson += "\"MenuParentNo\":\"" + strPID + "\"";
                        strToJson += "}";
                    }
                    strToJson += "]";
                }
                strToJson += "}";
            }

            return strToJson;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        /// <remarks>add by lhm</remarks>
        private string AddUrlUserParams(string url, TSysUserVo vo)
        {
            url = url.Replace("{USER_NAME}", vo.USER_NAME);

            return url;
        }


        ///// <summary>
        ///// 功能描述：特定方法 根据指定用户获取合法地图菜单
        ///// 创建日期：2011-05-09 11:20
        ///// 创建人  ：郑义
        ///// </summary>
        ///// <param name="strUserName">用户名</param>
        ///// <returns></returns>
        //public DataTable GetMapMenuByUserName(string strUserName)
        //{
        //    return access.GetMapMenuByUserName(strUserName);
        //}
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <returns></returns>
        public TSysMenuVo SelectByObject(TSysMenuVo tSysMenu)
        {
            return access.SelectByObject(tSysMenu);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuVo tSysMenu)
        {
            return access.Create(tSysMenu);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenu">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuVo tSysMenu)
        {
            return access.Edit(tSysMenu);
        }
                /// <summary>
        /// 条件修改系统菜单配置
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <returns></returns>
        public bool EditData(TSysMenuVo tSysMenu)
        {
            return access.EditData(tSysMenu);
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }
        /// <summary>
        /// 特定方法
        /// 通过菜单路径获取所有父菜单信息
        /// </summary>
        /// <param name="strUrl">菜单路径</param>
        /// <returns></returns>
        public DataTable GetParentsMenuByUrl(string strUrl)
        {
            return access.GetParentsMenuByUrl(strUrl);
        }
        /// <summary>
        /// 特定方法
        /// 通过菜单路径得到子功能菜单信息
        /// </summary>
        /// <param name="strUrl">菜单路径</param>
        /// <returns></returns>
        public DataTable GetFunctionMenuByUrl(string strUrl)
        {
            return access.GetFunctionMenuByUrl(strUrl);
        }
        /// <summary>
        /// 扩展方法
        /// 获取对象DataTable
        /// 创建日期：2011-04-19 13:40
        /// 创建人  ：郑义
        /// 修改日期：2011-04-20 21:10
        /// 修改人  ：郑义
        /// 修改内容:添加菜单ID查询下级(所有下级)
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <param name="strMainMenuID">主菜单ID</param>  
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TSysMenuVo tSysMenu, string strMainMenuID, int iIndex, int iCount)
        {
            return access.SelectByTableEx(tSysMenu,strMainMenuID, iIndex, iCount);
        }
        /// <summary>
        /// 扩展方法
        /// 获得查询结果总行数，用于分页
        /// 创建日期：2011-04-20 21:20
        /// 创建人  ：郑义
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <param name="strMainMenuID">主菜单ID</param>  
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TSysMenuVo tSysMenu, string strMainMenuID)
        {
            return access.GetSelectResultCountEx(tSysMenu,strMainMenuID);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //页面中重点显示(9宫格)1为重点展示
            if (tSysMenu.IS_IMPORTANT.Trim() == "")
            {
                this.Tips.AppendLine("页面中重点显示(9宫格)1为重点展示不能为空");
                return false;
            }
            //启用标记,1为启用
            if (tSysMenu.IS_USE.Trim() == "")
            {
                this.Tips.AppendLine("启用标记,1为启用不能为空");
                return false;
            }
            //删除标记,1为删除
            if (tSysMenu.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记,1为删除不能为空");
                return false;
            }
            //备注
            if (tSysMenu.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysMenu.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysMenu.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysMenu.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysMenu.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysMenu.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //菜单编号
            if (tSysMenu.ID.Trim() == "")
            {
                this.Tips.AppendLine("菜单编号不能为空");
                return false;
            }
            //显示名称
            if (tSysMenu.MENU_TEXT.Trim() == "")
            {
                this.Tips.AppendLine("显示名称不能为空");
                return false;
            }
            //超链接或地址
            if (tSysMenu.MENU_URL.Trim() == "")
            {
                this.Tips.AppendLine("超链接或地址不能为空");
                return false;
            }
            //菜单说明
            if (tSysMenu.MENU_COMMENT.Trim() == "")
            {
                this.Tips.AppendLine("菜单说明不能为空");
                return false;
            }
            //图片位置(小图标)
            if (tSysMenu.MENU_IMGURL.Trim() == "")
            {
                this.Tips.AppendLine("图片位置(小图标)不能为空");
                return false;
            }
            //重点展示图片位置
            if (tSysMenu.MENU_BIGIMGURL.Trim() == "")
            {
                this.Tips.AppendLine("重点展示图片位置不能为空");
                return false;
            }
            //父节点ID(如果为0,为主节点)
            if (tSysMenu.PARENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("父节点ID(如果为0,为主节点)不能为空");
                return false;
            }
            //排序(本父节点内)
            if (tSysMenu.ORDER_ID.Trim() == "")
            {
                this.Tips.AppendLine("排序(本父节点内)不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新排序 Castle （胡方扬）2012-10-30
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool UpdateSortByTransaction(string strValue)
        {
            return access.UpdateSortByTransaction(strValue);
        }

    }
}
