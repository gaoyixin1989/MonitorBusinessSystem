using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.General;
using i3.ValueObject;

namespace i3.DataAccess.Sys.General
{
    /// <summary>
    /// 功能：菜单管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// 修改：去除oracle特性，修改SelectByTableEx、GetSelectResultCountEx
    /// </summary>
    public class TSysMenuAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuVo tSysMenu)
        {
            string strSQL = "select Count(*) from T_SYS_MENU " + this.BuildWhereStatement(tSysMenu);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_MENU  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysMenuVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenu">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuVo Details(TSysMenuVo tSysMenu)
        {
           string strSQL = String.Format("select * from  T_SYS_MENU " + this.BuildWhereStatement(tSysMenu));
           return SqlHelper.ExecuteObject(new TSysMenuVo(), strSQL);
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

            string strSQL = String.Format("select t.* from T_SYS_MENU t " + this.BuildWhereStatement(tSysMenu));
            return SqlHelper.ExecuteObjectList(tSysMenu, BuildPagerExpress(strSQL, iIndex, iCount));

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

            string strSQL = " select t.* from T_SYS_MENU t {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysMenu));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress<TSysMenuVo>(tSysMenu, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuVo tSysMenu)
        {
            string strSQL = string.Format("select t.*,u.menu_text as parent_text from (select * from T_SYS_MENU {0}) t  left join T_SYS_MENU u on  t.parent_id = u.id order by t.ORDER_ID", this.BuildWhereStatement(tSysMenu));
            return SqlHelper.ExecuteDataTable(strSQL);
            //string strSQL = "select * from T_SYS_MENU " + this.BuildWhereStatement(tSysMenu) + " order by ORDER_ID";
            //return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <returns></returns>
        public TSysMenuVo SelectByObject(TSysMenuVo tSysMenu)
        {
            string strSQL = "select * from T_SYS_MENU " + this.BuildWhereStatement(tSysMenu);
            return SqlHelper.ExecuteObject(new TSysMenuVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysMenu">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuVo tSysMenu)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysMenu, TSysMenuVo.T_SYS_MENU_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenu">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuVo tSysMenu)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysMenu, TSysMenuVo.T_SYS_MENU_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysMenu.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 条件修改系统菜单配置
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <returns></returns>
        public bool EditData(TSysMenuVo tSysMenu)
        {
            string strSQL =String.Format( "UPDATE  T_SYS_MENU SET IS_SHORTCUT='{0}',MENU_URL='{1}',MENU_TEXT='{2}',MENU_IMGURL='{3}',PARENT_ID='{4}' WHERE ID='{5}'",tSysMenu.IS_SHORTCUT,tSysMenu.MENU_URL,tSysMenu.MENU_TEXT,tSysMenu.MENU_IMGURL,tSysMenu.PARENT_ID,tSysMenu.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_MENU where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }


        /// <summary>
        /// 特定方法
        /// 通过菜单路径获取所有父菜单信息
        /// </summary>
        /// <param name="strUrl">菜单路径</param>
        /// <returns></returns>
        public DataTable GetParentsMenuByUrl(string strUrl)
        {
            string strSQL = @"select * from t_sys_menu where id in (select t.parent_id from t_sys_menu t where menu_url ='{0}')";
            strSQL = string.Format(strSQL, strUrl);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 特定方法
        /// 通过菜单路径得到子功能菜单信息
        /// </summary>
        /// <param name="strUrl">菜单路径</param>
        /// <returns></returns>
        public DataTable GetFunctionMenuByUrl(string strUrl)
        {
            string strSQL = @"select * from t_sys_menu where parent_id in (select t.ID from t_sys_menu t where menu_url ='{0}') and IS_DEL='0'";
            strSQL = string.Format(strSQL, strUrl);
            return SqlHelper.ExecuteDataTable(strSQL);
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
            string strMenuIds = "";
            if (strMainMenuID.Length > 0)
            {
                string strSql = @"exec SelectSubMenu '" + strMainMenuID + "'";
                DataTable dtMenu = SqlHelper.ExecuteDataTable(strSql);
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    strMenuIds += (strMenuIds.Length > 0 ? "," : "") + "'" + dtMenu.Rows[i]["ID"].ToString() + "'";
                }
            }

            string strSQL = @"select * from t_sys_menu  {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysMenu));
            if (strMenuIds.Length > 0)
                strSQL += " and id in (" + strMenuIds + ")";
            else
                strSQL += " and is_hide='0' and is_del='0' and MENU_TYPE='Menu'";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 扩展方法
        /// 获得查询结果总行数，用于分页
        /// 创建日期：2011-04-20 21:20
        /// 创建人  ：郑义
        /// <param name="tSysMenu">对象</param>
        /// <param name="strMainMenuID">主菜单ID</param>  
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TSysMenuVo tSysMenu, string strMainMenuID)
        {
            string strMenuIds = "";
            if (strMainMenuID.Length > 0)
            {
                string strSql = @"exec SelectSubMenu '" + strMainMenuID + "'";
                DataTable dtMenu = SqlHelper.ExecuteDataTable(strSql);
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    strMenuIds += (strMenuIds.Length > 0 ? "," : "") + "'" + dtMenu.Rows[i]["ID"].ToString() + "'";
                }
            }

            string strSQL = @"select count(*) from t_sys_menu  {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysMenu));
            if (strMenuIds.Length > 0)
                strSQL += " and id in (" + strMenuIds + ")";
            else
                strSQL += " and is_hide='0' and is_del='0' and MENU_TYPE='Menu'";

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }



        /// <summary>
        /// 特定方法
        /// 根据指定用户获取合法菜单
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public DataTable GetMenuByUserName(TSysMenuVo tSysMenu,string strUserName)
        {
            string strSQL = @"select * from (select * from T_SYS_MENU {1}) m
                        where m.id in (
                            select MENU_ID from T_SYS_MENU_POST where (RIGHT_TYPE='1' and POST_ID in 
                                (SELECT ID FROM T_SYS_USER  WHERE (USER_NAME = '{0}'))
                              )
                              or (RIGHT_TYPE='2' and POST_ID in
                                    (select POST_ID from T_SYS_USER_POST where USER_ID in 
                                        (SELECT ID FROM T_SYS_USER WHERE (USER_NAME = '{0}'))
                                    )
                                )
                            )
                         order by m.id desc";
            strSQL = string.Format(strSQL, strUserName, BuildWhereStatement(tSysMenu));
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 特定方法
        /// 根据指定用户获取合法菜单
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public DataTable GetMenuByUserID(TSysMenuVo tSysMenu, string strUserID)
        {

            string strSQL = @"select * from (select * from T_SYS_MENU {1}) m
                        where m.id in (
                            select MENU_ID from T_SYS_MENU_POST where (RIGHT_TYPE='1' and POST_ID  = '{0}' 
                              )
                              or (RIGHT_TYPE='2' and POST_ID in
                                    (select POST_ID from T_SYS_USER_POST where USER_ID = '{0}')
                                )
                            )
                         order by m.id desc";

            strSQL = string.Format(strSQL, strUserID, BuildWhereStatement(tSysMenu));
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 排序（Sort） 批量更新数据库 Castle（胡方扬 2012-10-30）
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool UpdateSortByTransaction(string strValue)
        {
            ArrayList arrVo = new ArrayList();
            string[] values = strValue.Split(',');
            foreach (string valueTemp in values)
            {
                string strOrderBy = valueTemp.Split('|')[0];
                string strId = valueTemp.Split('|')[1];
                string strParentId = valueTemp.Split('|')[2];

                string strsql = "UPDATE T_SYS_MENU SET ORDER_ID = '" + strOrderBy + "', PARENT_ID='" + strParentId + "' WHERE ID = '" + strId + "'";
                arrVo.Add(strsql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysMenu"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysMenuVo tSysMenu)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysMenu)
            {
			    	
				//页面中重点显示(9宫格)1为重点展示
				if (!String.IsNullOrEmpty(tSysMenu.IS_IMPORTANT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_IMPORTANT = '{0}'", tSysMenu.IS_IMPORTANT.ToString()));
				}	
				//启用标记,1为启用
				if (!String.IsNullOrEmpty(tSysMenu.IS_USE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_USE = '{0}'", tSysMenu.IS_USE.ToString()));
				}	
				//删除标记,1为删除
				if (!String.IsNullOrEmpty(tSysMenu.IS_DEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysMenu.IS_DEL.ToString()));
				}
                //隐藏标记,1为隐藏，对用户屏蔽
                if (!String.IsNullOrEmpty(tSysMenu.IS_HIDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HIDE = '{0}'", tSysMenu.IS_HIDE.ToString()));
                }	
				//备注
				if (!String.IsNullOrEmpty(tSysMenu.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysMenu.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysMenu.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysMenu.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysMenu.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysMenu.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysMenu.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysMenu.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tSysMenu.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysMenu.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tSysMenu.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysMenu.REMARK5.ToString()));
				}	
				//菜单编号
				if (!String.IsNullOrEmpty(tSysMenu.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysMenu.ID.ToString()));
				}	
				//显示名称
				if (!String.IsNullOrEmpty(tSysMenu.MENU_TEXT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_TEXT = '{0}'", tSysMenu.MENU_TEXT.ToString()));
				}	
				//超链接或地址
				if (!String.IsNullOrEmpty(tSysMenu.MENU_URL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_URL = '{0}'", tSysMenu.MENU_URL.ToString()));
				}	
				//菜单说明
				if (!String.IsNullOrEmpty(tSysMenu.MENU_COMMENT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_COMMENT = '{0}'", tSysMenu.MENU_COMMENT.ToString()));
				}	
				//图片位置(小图标)
				if (!String.IsNullOrEmpty(tSysMenu.MENU_IMGURL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_IMGURL = '{0}'", tSysMenu.MENU_IMGURL.ToString()));
				}	
				//重点展示图片位置
				if (!String.IsNullOrEmpty(tSysMenu.MENU_BIGIMGURL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_BIGIMGURL = '{0}'", tSysMenu.MENU_BIGIMGURL.ToString()));
				}	
				//父节点ID(如果为0,为主节点)
				if (!String.IsNullOrEmpty(tSysMenu.PARENT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PARENT_ID = '{0}'", tSysMenu.PARENT_ID.ToString()));
				}	
				//排序(本父节点内)
				if (!String.IsNullOrEmpty(tSysMenu.ORDER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ORDER_ID = '{0}'", tSysMenu.ORDER_ID.ToString()));
				}
                //菜单类型
                if (!String.IsNullOrEmpty(tSysMenu.MENU_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MENU_TYPE = '{0}'", tSysMenu.MENU_TYPE.ToString()));
                }
                //是否快捷菜单
                if (!String.IsNullOrEmpty(tSysMenu.IS_SHORTCUT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_SHORTCUT = '{0}'", tSysMenu.IS_SHORTCUT.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
