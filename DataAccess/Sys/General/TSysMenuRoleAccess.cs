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
    /// 功能：角色菜单
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysMenuRoleAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuRoleVo tSysMenuRole)
        {
            string strSQL = "select Count(*) from T_SYS_MENU_ROLE " + this.BuildWhereStatement(tSysMenuRole);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuRoleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_MENU_ROLE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysMenuRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenuRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuRoleVo Details(TSysMenuRoleVo tSysMenuRole)
        {
           string strSQL = String.Format("select * from  T_SYS_MENU_ROLE " + this.BuildWhereStatement(tSysMenuRole));
           return SqlHelper.ExecuteObject(new TSysMenuRoleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysMenuRoleVo> SelectByObject(TSysMenuRoleVo tSysMenuRole, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_MENU_ROLE t " + this.BuildWhereStatement(tSysMenuRole));
            return SqlHelper.ExecuteObjectList(tSysMenuRole, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysMenuRoleVo tSysMenuRole, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_MENU_ROLE t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysMenuRole));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenuRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuRoleVo tSysMenuRole)
        {
            string strSQL = "select * from T_SYS_MENU_ROLE " + this.BuildWhereStatement(tSysMenuRole);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <returns></returns>
        public TSysMenuRoleVo SelectByObject(TSysMenuRoleVo tSysMenuRole)
        {
            string strSQL = "select * from T_SYS_MENU_ROLE " + this.BuildWhereStatement(tSysMenuRole);
            return SqlHelper.ExecuteObject(new TSysMenuRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuRoleVo tSysMenuRole)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysMenuRole, TSysMenuRoleVo.T_SYS_MENU_ROLE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuRoleVo tSysMenuRole)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysMenuRole, TSysMenuRoleVo.T_SYS_MENU_ROLE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysMenuRole.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_MENU_ROLE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ROLEID</param>
        /// <returns>是否成功</returns>
        public bool DeleteByRoleID(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_MENU_ROLE where Role_id='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }

        
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysMenuRole"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysMenuRoleVo tSysMenuRole)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysMenuRole)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysMenuRole.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysMenuRole.ID.ToString()));
				}	
				//角色编号
				if (!String.IsNullOrEmpty(tSysMenuRole.ROLE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_ID = '{0}'", tSysMenuRole.ROLE_ID.ToString()));
				}	
				//菜单编号
				if (!String.IsNullOrEmpty(tSysMenuRole.MENU_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_ID = '{0}'", tSysMenuRole.MENU_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysMenuRole.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysMenuRole.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysMenuRole.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysMenuRole.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysMenuRole.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysMenuRole.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysMenuRole.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysMenuRole.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
