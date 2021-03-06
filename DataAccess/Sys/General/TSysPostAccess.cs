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
    /// 功能：职位管理
    /// 创建日期：2012-10-23
    /// 创建人：潘德军
    /// </summary>
    public class TSysPostAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysPostVo tSysPost)
        {
            string strSQL = "select Count(*) from T_SYS_POST " + this.BuildWhereStatement(tSysPost);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysPostVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_POST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysPostVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysPostVo Details(TSysPostVo tSysPost)
        {
           string strSQL = String.Format("select * from  T_SYS_POST " + this.BuildWhereStatement(tSysPost));
           return SqlHelper.ExecuteObject(new TSysPostVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysPostVo> SelectByObject(TSysPostVo tSysPost, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_SYS_POST " + this.BuildWhereStatement(tSysPost));
            return SqlHelper.ExecuteObjectList(tSysPost, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysPostVo tSysPost, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_SYS_POST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysPost));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress<TSysPostVo>(tSysPost,strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysPostVo tSysPost)
        {
            string strSQL = "select * from T_SYS_POST " + this.BuildWhereStatement(tSysPost);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable_byUser(string strUserId)
        {
            string strSQL = "select * from T_SYS_POST where ID in (select POST_ID from T_SYS_USER_POST where USER_ID='{0}')";
            strSQL = string.Format(strSQL, strUserId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns></returns>
        public TSysPostVo SelectByObject(TSysPostVo tSysPost)
        {
            string strSQL = "select * from T_SYS_POST " + this.BuildWhereStatement(tSysPost);
            return SqlHelper.ExecuteObject(new TSysPostVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysPostVo tSysPost)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysPost, TSysPostVo.T_SYS_POST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysPostVo tSysPost)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysPost, TSysPostVo.T_SYS_POST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysPost.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysPost_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tSysPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysPostVo tSysPost_UpdateSet, TSysPostVo tSysPost_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysPost_UpdateSet, TSysPostVo.T_SYS_POST_TABLE);
            strSQL += this.BuildWhereStatement(tSysPost_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_POST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	    /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysPostVo tSysPost)
        {
            string strSQL = "delete from T_SYS_POST ";
	    strSQL += this.BuildWhereStatement(tSysPost);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 删除职位，同时删除下级所有职位
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteNode(string Id)
        {
            string strSQL = "exec dbo.DeletePostNode '" + Id + "'";

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysPost"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysPostVo tSysPost)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysPost)
            {
			    	
				//角色编号
				if (!String.IsNullOrEmpty(tSysPost.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysPost.ID.ToString()));
				}	
				//职位名
				if (!String.IsNullOrEmpty(tSysPost.POST_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_NAME = '{0}'", tSysPost.POST_NAME.ToString()));
				}	
				//上级职位ID
				if (!String.IsNullOrEmpty(tSysPost.PARENT_POST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PARENT_POST_ID = '{0}'", tSysPost.PARENT_POST_ID.ToString()));
				}	
				//行政级别
				if (!String.IsNullOrEmpty(tSysPost.POST_LEVEL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_LEVEL_ID = '{0}'", tSysPost.POST_LEVEL_ID.ToString()));
				}	
				//所属部门
				if (!String.IsNullOrEmpty(tSysPost.POST_DEPT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_DEPT_ID = '{0}'", tSysPost.POST_DEPT_ID.ToString()));
				}	
				//角色说明
				if (!String.IsNullOrEmpty(tSysPost.ROLE_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ROLE_NOTE = '{0}'", tSysPost.ROLE_NOTE.ToString()));
				}	
				//树深度编号
				if (!String.IsNullOrEmpty(tSysPost.TREE_LEVEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TREE_LEVEL = '{0}'", tSysPost.TREE_LEVEL.ToString()));
				}	
				//排序
				if (!String.IsNullOrEmpty(tSysPost.NUM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tSysPost.NUM.ToString()));
				}	
				//删除标记,1为删除
				if (!String.IsNullOrEmpty(tSysPost.IS_DEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysPost.IS_DEL.ToString()));
				}	
				//创建人ID
				if (!String.IsNullOrEmpty(tSysPost.CREATE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_ID = '{0}'", tSysPost.CREATE_ID.ToString()));
				}	
				//创建时间
				if (!String.IsNullOrEmpty(tSysPost.CREATE_TIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_TIME = '{0}'", tSysPost.CREATE_TIME.ToString()));
				}	
				//隐藏标记,对用户屏蔽
				if (!String.IsNullOrEmpty(tSysPost.IS_HIDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_HIDE = '{0}'", tSysPost.IS_HIDE.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysPost.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysPost.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysPost.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysPost.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysPost.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysPost.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysPost.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysPost.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tSysPost.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysPost.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tSysPost.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysPost.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
