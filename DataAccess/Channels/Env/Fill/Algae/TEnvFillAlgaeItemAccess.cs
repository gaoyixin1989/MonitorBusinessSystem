using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Algae;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Fill.Algae
{
    /// <summary>
    /// 功能：蓝藻水华数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillAlgaeItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ALGAE_ITEM " + this.BuildWhereStatement(tEnvFillAlgaeItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TEnvFillAlgaeItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeItemVo Details(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
           string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE_ITEM " + this.BuildWhereStatement(tEnvFillAlgaeItem));
           return SqlHelper.ExecuteObject(new TEnvFillAlgaeItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlgaeItemVo> SelectByObject(TEnvFillAlgaeItemVo tEnvFillAlgaeItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE_ITEM " + this.BuildWhereStatement(tEnvFillAlgaeItem));
            return SqlHelper.ExecuteObjectList(tEnvFillAlgaeItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlgaeItemVo tEnvFillAlgaeItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_ENV_FILL_ALGAE_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAlgaeItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlgaeItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = "select * from T_ENV_FILL_ALGAE_ITEM " + this.BuildWhereStatement(tEnvFillAlgaeItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <returns></returns>
        public TEnvFillAlgaeItemVo SelectByObject(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = "select * from T_ENV_FILL_ALGAE_ITEM " + this.BuildWhereStatement(tEnvFillAlgaeItem);
            return SqlHelper.ExecuteObject(new TEnvFillAlgaeItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAlgaeItem, TEnvFillAlgaeItemVo.T_ENV_FILL_ALGAE_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlgaeItem, TEnvFillAlgaeItemVo.T_ENV_FILL_ALGAE_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAlgaeItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgaeItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillAlgaeItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeItemVo tEnvFillAlgaeItem_UpdateSet, TEnvFillAlgaeItemVo tEnvFillAlgaeItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlgaeItem_UpdateSet, TEnvFillAlgaeItemVo.T_ENV_FILL_ALGAE_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAlgaeItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ALGAE_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            string strSQL = "delete from T_ENV_FILL_ALGAE_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvFillAlgaeItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAlgaeItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAlgaeItem)
            {
			    	
				//主键ID
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAlgaeItem.ID.ToString()));
				}	
				//饮用水断面数据填报ID
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.ALGAE_FILL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ALGAE_FILL_ID = '{0}'", tEnvFillAlgaeItem.ALGAE_FILL_ID.ToString()));
				}	
				//监测项ID
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillAlgaeItem.ITEM_ID.ToString()));
				}	
				//监测值
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.ITEM_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillAlgaeItem.ITEM_VALUE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAlgaeItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAlgaeItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAlgaeItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAlgaeItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tEnvFillAlgaeItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAlgaeItem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            string strSQL = "select * from T_ENV_FILL_ALGAE_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
    }
}
