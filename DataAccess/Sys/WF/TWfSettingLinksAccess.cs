using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：流程节点连接线
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingLinksAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_LINKS " + this.BuildWhereStatement(tWfSettingLinks);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingLinksVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_LINKS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingLinksVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingLinks">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingLinksVo Details(TWfSettingLinksVo tWfSettingLinks)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_LINKS " + this.BuildWhereStatement(tWfSettingLinks));
           return SqlHelper.ExecuteObject(new TWfSettingLinksVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingLinksVo> SelectByObject(TWfSettingLinksVo tWfSettingLinks, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_LINKS " + this.BuildWhereStatement(tWfSettingLinks));
            return SqlHelper.ExecuteObjectList(tWfSettingLinks, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingLinksVo tWfSettingLinks, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_LINKS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingLinks));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingLinks"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = "select * from T_WF_SETTING_LINKS " + this.BuildWhereStatement(tWfSettingLinks);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <returns></returns>
        public TWfSettingLinksVo SelectByObject(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = "select * from T_WF_SETTING_LINKS " + this.BuildWhereStatement(tWfSettingLinks);
            return SqlHelper.ExecuteObject(new TWfSettingLinksVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingLinks, TWfSettingLinksVo.T_WF_SETTING_LINKS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingLinks">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingLinks, TWfSettingLinksVo.T_WF_SETTING_LINKS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingLinks.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingLinks_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingLinks_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingLinksVo tWfSettingLinks_UpdateSet, TWfSettingLinksVo tWfSettingLinks_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingLinks_UpdateSet, TWfSettingLinksVo.T_WF_SETTING_LINKS_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingLinks_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_LINKS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingLinksVo tWfSettingLinks)
        {
            string strSQL = "delete from T_WF_SETTING_LINKS ";
	    strSQL += this.BuildWhereStatement(tWfSettingLinks);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingLinks"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingLinksVo tWfSettingLinks)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingLinks)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingLinks.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingLinks.ID.ToString()));
				}	
				//连接编号
				if (!String.IsNullOrEmpty(tWfSettingLinks.WF_LINK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_LINK_ID = '{0}'", tWfSettingLinks.WF_LINK_ID.ToString()));
				}	
				//流程编号
				if (!String.IsNullOrEmpty(tWfSettingLinks.WF_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingLinks.WF_ID.ToString()));
				}	
				//起始环节编号
				if (!String.IsNullOrEmpty(tWfSettingLinks.START_TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND START_TASK_ID = '{0}'", tWfSettingLinks.START_TASK_ID.ToString()));
				}	
				//结束环节编号
				if (!String.IsNullOrEmpty(tWfSettingLinks.END_TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND END_TASK_ID = '{0}'", tWfSettingLinks.END_TASK_ID.ToString()));
				}	
				//条件描述
				if (!String.IsNullOrEmpty(tWfSettingLinks.CONDITION_DES.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_DES = '{0}'", tWfSettingLinks.CONDITION_DES.ToString()));
				}	
				//文字简述
				if (!String.IsNullOrEmpty(tWfSettingLinks.NOTE_DES.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NOTE_DES = '{0}'", tWfSettingLinks.NOTE_DES.ToString()));
				}	
				//命令描述
				if (!String.IsNullOrEmpty(tWfSettingLinks.CMD_DES.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CMD_DES = '{0}'", tWfSettingLinks.CMD_DES.ToString()));
				}	
				//优先级
				if (!String.IsNullOrEmpty(tWfSettingLinks.PRIORITY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRIORITY = '{0}'", tWfSettingLinks.PRIORITY.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
