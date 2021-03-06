using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Metal;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Fill.Metal
{
    /// <summary>
    /// 功能：河流底泥数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillMetalItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_METAL_ITEM " + this.BuildWhereStatement(tEnvFillMetalItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMetalItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_METAL_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TEnvFillMetalItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMetalItemVo Details(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
           string strSQL = String.Format("select * from  T_ENV_FILL_METAL_ITEM " + this.BuildWhereStatement(tEnvFillMetalItem));
           return SqlHelper.ExecuteObject(new TEnvFillMetalItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMetalItemVo> SelectByObject(TEnvFillMetalItemVo tEnvFillMetalItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_ENV_FILL_METAL_ITEM " + this.BuildWhereStatement(tEnvFillMetalItem));
            return SqlHelper.ExecuteObjectList(tEnvFillMetalItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMetalItemVo tEnvFillMetalItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_ENV_FILL_METAL_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillMetalItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMetalItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = "select * from T_ENV_FILL_METAL_ITEM " + this.BuildWhereStatement(tEnvFillMetalItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <returns></returns>
        public TEnvFillMetalItemVo SelectByObject(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = "select * from T_ENV_FILL_METAL_ITEM " + this.BuildWhereStatement(tEnvFillMetalItem);
            return SqlHelper.ExecuteObject(new TEnvFillMetalItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillMetalItem, TEnvFillMetalItemVo.T_ENV_FILL_METAL_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMetalItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMetalItem, TEnvFillMetalItemVo.T_ENV_FILL_METAL_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillMetalItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMetalItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillMetalItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMetalItemVo tEnvFillMetalItem_UpdateSet, TEnvFillMetalItemVo tEnvFillMetalItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMetalItem_UpdateSet, TEnvFillMetalItemVo.T_ENV_FILL_METAL_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillMetalItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_METAL_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            string strSQL = "delete from T_ENV_FILL_METAL_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvFillMetalItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillMetalItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillMetalItem)
            {
			    	
				//主键ID
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillMetalItem.ID.ToString()));
				}	
				//饮用水断面数据填报ID
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.SEDIMENT_METAL_FILL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEDIMENT_METAL_FILL_ID = '{0}'", tEnvFillMetalItem.SEDIMENT_METAL_FILL_ID.ToString()));
				}	
				//监测项ID
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillMetalItem.ITEM_ID.ToString()));
				}	
				//监测值
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.ITEM_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillMetalItem.ITEM_VALUE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillMetalItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillMetalItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillMetalItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillMetalItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tEnvFillMetalItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillMetalItem.REMARK5.ToString()));
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
            string strSQL = "select * from T_ENV_FILL_METAL_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
    }
}
