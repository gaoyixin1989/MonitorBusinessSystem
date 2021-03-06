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
    /// 功能：权限管理
    /// 创建日期：2012-10-25
    /// 创建人：潘德军
    /// </summary>
    public class TSysMenuPostAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = "select Count(*) from T_SYS_MENU_POST " + this.BuildWhereStatement(tSysMenuPost);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuPostVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_MENU_POST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TSysMenuPostVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenuPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuPostVo Details(TSysMenuPostVo tSysMenuPost)
        {
           string strSQL = String.Format("select * from  T_SYS_MENU_POST " + this.BuildWhereStatement(tSysMenuPost));
           return SqlHelper.ExecuteObject(new TSysMenuPostVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysMenuPostVo> SelectByObject(TSysMenuPostVo tSysMenuPost, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_SYS_MENU_POST " + this.BuildWhereStatement(tSysMenuPost));
            return SqlHelper.ExecuteObjectList(tSysMenuPost, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysMenuPostVo tSysMenuPost, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_SYS_MENU_POST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysMenuPost));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenuPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = "select * from T_SYS_MENU_POST " + this.BuildWhereStatement(tSysMenuPost);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <returns></returns>
        public TSysMenuPostVo SelectByObject(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = "select * from T_SYS_MENU_POST " + this.BuildWhereStatement(tSysMenuPost);
            return SqlHelper.ExecuteObject(new TSysMenuPostVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysMenuPost, TSysMenuPostVo.T_SYS_MENU_POST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysMenuPost, TSysMenuPostVo.T_SYS_MENU_POST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysMenuPost.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuPost_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tSysMenuPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuPostVo tSysMenuPost_UpdateSet, TSysMenuPostVo tSysMenuPost_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysMenuPost_UpdateSet, TSysMenuPostVo.T_SYS_MENU_POST_TABLE);
            strSQL += this.BuildWhereStatement(tSysMenuPost_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_MENU_POST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysMenuPostVo tSysMenuPost)
        {
            string strSQL = "delete from T_SYS_MENU_POST ";
	    strSQL += this.BuildWhereStatement(tSysMenuPost);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysMenuPost"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysMenuPostVo tSysMenuPost)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysMenuPost)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tSysMenuPost.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysMenuPost.ID.ToString()));
				}	
				//人员ID
				if (!String.IsNullOrEmpty(tSysMenuPost.POST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_ID = '{0}'", tSysMenuPost.POST_ID.ToString()));
				}	
				//人员职位类型,1,人员，2，职位
				if (!String.IsNullOrEmpty(tSysMenuPost.RIGHT_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RIGHT_TYPE = '{0}'", tSysMenuPost.RIGHT_TYPE.ToString()));
				}	
				//菜单编号
				if (!String.IsNullOrEmpty(tSysMenuPost.MENU_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MENU_ID = '{0}'", tSysMenuPost.MENU_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tSysMenuPost.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysMenuPost.REMARK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tSysMenuPost.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysMenuPost.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tSysMenuPost.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysMenuPost.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tSysMenuPost.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysMenuPost.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
