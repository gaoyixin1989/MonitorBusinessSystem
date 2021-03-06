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
    /// 功能：角色管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysRoleAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysRoleVo tSysRole)
        {
            string strSQL = "select Count(*) from T_SYS_ROLE " + this.BuildWhereStatement(tSysRole);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysRoleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_ROLE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysRoleVo Details(TSysRoleVo tSysRole)
        {
           string strSQL = String.Format("select * from  T_SYS_ROLE " + this.BuildWhereStatement(tSysRole));
           return SqlHelper.ExecuteObject(new TSysRoleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysRoleVo> SelectByObject(TSysRoleVo tSysRole, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_ROLE t " + this.BuildWhereStatement(tSysRole));
            return SqlHelper.ExecuteObjectList(tSysRole, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysRoleVo tSysRole, int iIndex, int iCount)
        {
            string strSQL = " select t.* from (select * from T_SYS_ROLE  )  t {0}  order by id desc";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysRole));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysRoleVo tSysRole)
        {
            string strSQL = "select * from T_SYS_ROLE t " + this.BuildWhereStatement(tSysRole);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <returns></returns>
        public TSysRoleVo SelectByObject(TSysRoleVo tSysRole)
        {
            string strSQL = "select * from T_SYS_ROLE " + this.BuildWhereStatement(tSysRole);
            return SqlHelper.ExecuteObject(new TSysRoleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysRoleVo tSysRole)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysRole, TSysRoleVo.T_SYS_ROLE_TABLE, TSysRoleVo.CREATE_TIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysRoleVo tSysRole)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysRole, TSysRoleVo.T_SYS_ROLE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysRole.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_ROLE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysRole"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysRoleVo tSysRole)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysRole)
            {
			    	
				//是否为用户唯一
				if (!String.IsNullOrEmpty(tSysRole.USER_ONLY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ONLY = '{0}'", tSysRole.USER_ONLY.ToString()));
				}	
				//启用标记,1为启用
				if (!String.IsNullOrEmpty(tSysRole.IS_USE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_USE = '{0}'", tSysRole.IS_USE.ToString()));
				}	
				//删除标记,1为删除
				if (!String.IsNullOrEmpty(tSysRole.IS_DEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysRole.IS_DEL.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysRole.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysRole.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysRole.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysRole.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysRole.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysRole.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysRole.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysRole.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tSysRole.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysRole.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tSysRole.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysRole.REMARK5.ToString()));
				}	
				//角色编号
				if (!String.IsNullOrEmpty(tSysRole.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysRole.ID.ToString()));
				}	
				//角色名称
				if (!String.IsNullOrEmpty(tSysRole.ROLE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_NAME like '%{0}%'", tSysRole.ROLE_NAME.ToString()));
				}	
				//角色类型
				if (!String.IsNullOrEmpty(tSysRole.ROLE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_TYPE = '{0}'", tSysRole.ROLE_TYPE.ToString()));
				}	
				//角色说明
				if (!String.IsNullOrEmpty(tSysRole.ROLE_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_NOTE = '{0}'", tSysRole.ROLE_NOTE.ToString()));
				}	
				//创建人ID
				if (!String.IsNullOrEmpty(tSysRole.CREATE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysRole.CREATE_ID.ToString()));
				}	
				//创建时间
				if (!String.IsNullOrEmpty(tSysRole.CREATE_TIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysRole.CREATE_TIME.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
