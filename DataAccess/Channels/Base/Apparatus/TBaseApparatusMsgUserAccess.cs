using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器定期维护自动提醒人员
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusMsgUserAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_MSG_USER " + this.BuildWhereStatement(tBaseApparatusMsgUser);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgUserVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG_USER  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseApparatusMsgUserVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgUserVo Details(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
           string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG_USER " + this.BuildWhereStatement(tBaseApparatusMsgUser));
           return SqlHelper.ExecuteObject(new TBaseApparatusMsgUserVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusMsgUserVo> SelectByObject(TBaseApparatusMsgUserVo tBaseApparatusMsgUser, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG_USER " + this.BuildWhereStatement(tBaseApparatusMsgUser));
            return SqlHelper.ExecuteObjectList(tBaseApparatusMsgUser, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusMsgUserVo tBaseApparatusMsgUser, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_APPARATUS_MSG_USER {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusMsgUser));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusMsgUser"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = "select * from T_BASE_APPARATUS_MSG_USER " + this.BuildWhereStatement(tBaseApparatusMsgUser);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <returns></returns>
        public TBaseApparatusMsgUserVo SelectByObject(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = "select * from T_BASE_APPARATUS_MSG_USER " + this.BuildWhereStatement(tBaseApparatusMsgUser);
            return SqlHelper.ExecuteObject(new TBaseApparatusMsgUserVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseApparatusMsgUser, TBaseApparatusMsgUserVo.T_BASE_APPARATUS_MSG_USER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusMsgUser, TBaseApparatusMsgUserVo.T_BASE_APPARATUS_MSG_USER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseApparatusMsgUser.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsgUser_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusMsgUser_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgUserVo tBaseApparatusMsgUser_UpdateSet, TBaseApparatusMsgUserVo tBaseApparatusMsgUser_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusMsgUser_UpdateSet, TBaseApparatusMsgUserVo.T_BASE_APPARATUS_MSG_USER_TABLE);
            strSQL += this.BuildWhereStatement(tBaseApparatusMsgUser_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_APPARATUS_MSG_USER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            string strSQL = "delete from T_BASE_APPARATUS_MSG_USER ";
	    strSQL += this.BuildWhereStatement(tBaseApparatusMsgUser);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusMsgUser"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusMsgUser)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusMsgUser.ID.ToString()));
				}	
				//USERID
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.USERID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USERID = '{0}'", tBaseApparatusMsgUser.USERID.ToString()));
				}	
				//备份1
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusMsgUser.REMARK1.ToString()));
				}	
				//备份2
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusMsgUser.REMARK2.ToString()));
				}	
				//备份3
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusMsgUser.REMARK3.ToString()));
				}	
				//备份4
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusMsgUser.REMARK4.ToString()));
				}	
				//备份5
				if (!String.IsNullOrEmpty(tBaseApparatusMsgUser.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusMsgUser.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
