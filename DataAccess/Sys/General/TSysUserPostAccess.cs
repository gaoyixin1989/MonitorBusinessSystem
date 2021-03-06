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
    /// 功能：用户职位表
    /// 创建日期：2012-10-25
    /// 创建人：潘德军
    /// </summary>
    public class TSysUserPostAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserPostVo tSysUserPost)
        {
            string strSQL = "select Count(*) from T_SYS_USER_POST " + this.BuildWhereStatement(tSysUserPost);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserPostVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_POST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysUserPostVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserPostVo Details(TSysUserPostVo tSysUserPost)
        {
           string strSQL = String.Format("select * from  T_SYS_USER_POST " + this.BuildWhereStatement(tSysUserPost));
           return SqlHelper.ExecuteObject(new TSysUserPostVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserPostVo> SelectByObject(TSysUserPostVo tSysUserPost, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_SYS_USER_POST " + this.BuildWhereStatement(tSysUserPost));
            return SqlHelper.ExecuteObjectList(tSysUserPost, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserPostVo tSysUserPost, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_SYS_USER_POST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserPost));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得指定用户的科室主任ID，如果该用户属于多个科室，则出现多个主任
        /// </summary>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public DataTable SelectDeptAdmin_byTable(string strUserId)
        {
            string strSQL = @"select user_id from T_SYS_USER_POST where POST_ID in (
                                select PARENT_POST_ID from T_SYS_POST
                                    where ID in (SELECT     POST_ID FROM         T_SYS_USER_POST where user_id='{0}')) ";
            strSQL = String.Format(strSQL, strUserId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserPostVo tSysUserPost)
        {
            string strSQL = "select * from T_SYS_USER_POST " + this.BuildWhereStatement(tSysUserPost);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <returns></returns>
        public TSysUserPostVo SelectByObject(TSysUserPostVo tSysUserPost)
        {
            string strSQL = "select * from T_SYS_USER_POST " + this.BuildWhereStatement(tSysUserPost);
            return SqlHelper.ExecuteObject(new TSysUserPostVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserPostVo tSysUserPost)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUserPost, TSysUserPostVo.T_SYS_USER_POST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserPostVo tSysUserPost)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserPost, TSysUserPostVo.T_SYS_USER_POST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysUserPost.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserPost_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tSysUserPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserPostVo tSysUserPost_UpdateSet, TSysUserPostVo tSysUserPost_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserPost_UpdateSet, TSysUserPostVo.T_SYS_USER_POST_TABLE);
            strSQL += this.BuildWhereStatement(tSysUserPost_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_USER_POST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysUserPostVo tSysUserPost)
        {
            string strSQL = "delete from T_SYS_USER_POST ";
	    strSQL += this.BuildWhereStatement(tSysUserPost);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
       /// <summary>
       /// 新增用户
       /// </summary>
       /// <param name="strUserId"></param>
       /// <param name="strPostId"></param>
       /// <returns></returns>
        public bool UpdateUserPostInfor(string strUserId, string[] strPostId)
        {
            DataTable dt = new DataTable();
            ArrayList arrVo = new ArrayList();
            foreach (string strpostId in strPostId)
            {
                dt = SqlHelper.ExecuteDataTable(String.Format("SELECT ID FROM dbo.T_SYS_USER_POST WHERE USER_ID='{0}' AND POST_ID ='{1}'",strUserId,strpostId));
                int count=dt.Rows.Count;
                if (count < 1)
                {
                    string postId=GetSerialNumber("user_post_infor");
                    string strSQL =String.Format( " INSERT INTO dbo.T_SYS_USER_POST (ID,USER_ID,POST_ID) VALUES('{0}','{1}','{2}')",postId,strUserId,strpostId);

                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        #endregion


        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUserPost"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserPostVo tSysUserPost)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUserPost)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysUserPost.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUserPost.ID.ToString()));
				}	
				//用户编号
				if (!String.IsNullOrEmpty(tSysUserPost.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tSysUserPost.USER_ID.ToString()));
				}	
				//角色编号
				if (!String.IsNullOrEmpty(tSysUserPost.POST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_ID = '{0}'", tSysUserPost.POST_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysUserPost.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysUserPost.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysUserPost.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUserPost.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysUserPost.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUserPost.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysUserPost.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUserPost.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
