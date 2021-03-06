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
    /// 功能：用户角色
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserRoleAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserRoleVo tSysUserRole)
        {
            string strSQL = "select Count(*) from T_SYS_USER_ROLE " + this.BuildWhereStatement(tSysUserRole);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserRoleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_ROLE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysUserRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserRoleVo Details(TSysUserRoleVo tSysUserRole)
        {
           string strSQL = String.Format("select * from  T_SYS_USER_ROLE " + this.BuildWhereStatement(tSysUserRole));
           return SqlHelper.ExecuteObject(new TSysUserRoleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserRoleVo> SelectByObject(TSysUserRoleVo tSysUserRole, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_USER_ROLE t " + this.BuildWhereStatement(tSysUserRole));
            return SqlHelper.ExecuteObjectList(tSysUserRole, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserRoleVo tSysUserRole, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_USER_ROLE {0} t ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserRole));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserRoleVo tSysUserRole)
        {
            string strSQL = "select * from T_SYS_USER_ROLE " + this.BuildWhereStatement(tSysUserRole);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <returns></returns>
        public TSysUserRoleVo SelectByObject(TSysUserRoleVo tSysUserRole)
        {
            string strSQL = "select * from T_SYS_USER_ROLE " + this.BuildWhereStatement(tSysUserRole);
            return SqlHelper.ExecuteObject(new TSysUserRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserRoleVo tSysUserRole)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUserRole, TSysUserRoleVo.T_SYS_USER_ROLE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserRoleVo tSysUserRole)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserRole, TSysUserRoleVo.T_SYS_USER_ROLE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysUserRole.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_USER_ROLE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
               
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">USER_ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteByUserId(string User_Id)
        {
            string strWhere = String.Format("delete from T_SYS_USER_ROLE where USER_ID='{0}'", User_Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">USER_ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteByRoleId(string Role_Id)
        {
            string strWhere = String.Format("delete from T_SYS_USER_ROLE where ROLE_ID='{0}'", Role_Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUserRole"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserRoleVo tSysUserRole)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUserRole)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysUserRole.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUserRole.ID.ToString()));
				}	
				//用户编号
				if (!String.IsNullOrEmpty(tSysUserRole.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tSysUserRole.USER_ID.ToString()));
				}	
				//角色编号
				if (!String.IsNullOrEmpty(tSysUserRole.ROLE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_ID = '{0}'", tSysUserRole.ROLE_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysUserRole.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysUserRole.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysUserRole.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUserRole.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysUserRole.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUserRole.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysUserRole.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUserRole.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
