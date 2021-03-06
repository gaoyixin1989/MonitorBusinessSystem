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
    /// 功能：在线用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserStatusAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserStatusVo tSysUserStatus)
        {
            string strSQL = "select Count(*) from T_SYS_USER_STATUS " + this.BuildWhereStatement(tSysUserStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserStatusVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_STATUS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysUserStatusVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserStatus">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserStatusVo Details(TSysUserStatusVo tSysUserStatus)
        {
           string strSQL = String.Format("select * from  T_SYS_USER_STATUS " + this.BuildWhereStatement(tSysUserStatus));
           return SqlHelper.ExecuteObject(new TSysUserStatusVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserStatusVo> SelectByObject(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount)
        {

            string strSQL = String.Format("select t.*,rownum rowno from  T_SYS_USER_STATUS t " + this.BuildWhereStatement(tSysUserStatus));
            return SqlHelper.ExecuteObjectList(tSysUserStatus, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount)
        {

            string strSQL = " select t.*,rownum rowno from T_SYS_USER_STATUS t {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserStatus));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserStatus"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserStatusVo tSysUserStatus)
        {
            string strSQL = "select * from T_SYS_USER_STATUS " + this.BuildWhereStatement(tSysUserStatus);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <returns></returns>
        public TSysUserStatusVo SelectByObject(TSysUserStatusVo tSysUserStatus)
        {
            string strSQL = "select * from T_SYS_USER_STATUS " + this.BuildWhereStatement(tSysUserStatus);
            return SqlHelper.ExecuteObject(new TSysUserStatusVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserStatusVo tSysUserStatus)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUserStatus, TSysUserStatusVo.T_SYS_USER_STATUS_TABLE, TSysUserStatusVo.LAST_OPTIME_FIELD);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserStatus">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserStatusVo tSysUserStatus)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserStatus, TSysUserStatusVo.T_SYS_USER_STATUS_TABLE, TSysUserStatusVo.LAST_OPTIME_FIELD);
            strSQL += string.Format(" where ID='{0}' ", tSysUserStatus.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strWhere = String.Format("delete from T_SYS_USER_STATUS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strWhere) > 0 ? true : false;
        }
        /// <summary>
        /// 扩展方法
        /// 获得在线用户DataTable,通过时间
        /// 创建日期：2011-04-20 17:10
        /// 创建人  ：郑义
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="dateTime">时间</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount, DateTime dateTime,string strRealName)
        {
            string strSQL = " select t.*,u.user_name,u.real_name from T_SYS_USER_STATUS t left join T_SYS_USER u on u.id = t.USER_ID {0} and u.real_name like '%{1}%' ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserStatus), strRealName);
            strSQL += String.Format(" and t.Last_optime > '{0}' order by u.order_id", dateTime.ToString());
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 扩展方法
        /// 获得在线用户查询结果总行数，用于分页
        /// 创建日期：2011-04-20 18:30
        /// 创建人  ：郑义
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="dateTime">时间</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TSysUserStatusVo tSysUserStatus, DateTime dateTime, string strRealName)
        {
            string strSQL = "select Count(*) from T_SYS_USER_STATUS t left join T_SYS_USER u on u.id = t.USER_ID {0} and u.real_name like '%{1}%' " ;
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserStatus), strRealName);
            strSQL += String.Format(" and t.Last_optime > to_date('{0}','yyyy-MM-dd HH24:MI:SS')", dateTime.ToString());
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUserStatus"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserStatusVo tSysUserStatus)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUserStatus)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysUserStatus.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUserStatus.ID.ToString()));
				}	
				//用户编号
				if (!String.IsNullOrEmpty(tSysUserStatus.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tSysUserStatus.USER_ID.ToString()));
				}	
				//最后一次访问时间
				if (!String.IsNullOrEmpty(tSysUserStatus.LAST_OPTIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LAST_OPTIME = '{0}'", tSysUserStatus.LAST_OPTIME.ToString()));
				}	
				//最后一次登陆IP
				if (!String.IsNullOrEmpty(tSysUserStatus.LAST_LOGIN_IP.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LAST_LOGIN_IP = '{0}'", tSysUserStatus.LAST_LOGIN_IP.ToString()));
				}	
				//最后一次访问页面
				if (!String.IsNullOrEmpty(tSysUserStatus.LAST_PAGE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LAST_PAGE = '{0}'", tSysUserStatus.LAST_PAGE.ToString()));
				}	
				//最后一次操作记录
				if (!String.IsNullOrEmpty(tSysUserStatus.LAST_OPERATION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LAST_OPERATION = '{0}'", tSysUserStatus.LAST_OPERATION.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysUserStatus.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysUserStatus.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysUserStatus.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUserStatus.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysUserStatus.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUserStatus.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysUserStatus.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUserStatus.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
