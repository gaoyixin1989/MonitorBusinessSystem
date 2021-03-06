using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Sea;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Fill.Sea
{
    /// <summary>
    /// 功能：近海海域数据填报监测项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillSeaItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEA_ITEM " + this.BuildWhereStatement(tEnvFillSeaItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeaItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEA_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TEnvFillSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeaItemVo Details(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
           string strSQL = String.Format("select * from  T_ENV_FILL_SEA_ITEM " + this.BuildWhereStatement(tEnvFillSeaItem));
           return SqlHelper.ExecuteObject(new TEnvFillSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeaItemVo> SelectByObject(TEnvFillSeaItemVo tEnvFillSeaItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_ENV_FILL_SEA_ITEM " + this.BuildWhereStatement(tEnvFillSeaItem));
            return SqlHelper.ExecuteObjectList(tEnvFillSeaItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeaItemVo tEnvFillSeaItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_ENV_FILL_SEA_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSeaItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEA_ITEM " + this.BuildWhereStatement(tEnvFillSeaItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <returns></returns>
        public TEnvFillSeaItemVo SelectByObject(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEA_ITEM " + this.BuildWhereStatement(tEnvFillSeaItem);
            return SqlHelper.ExecuteObject(new TEnvFillSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSeaItem, TEnvFillSeaItemVo.T_ENV_FILL_SEA_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeaItem, TEnvFillSeaItemVo.T_ENV_FILL_SEA_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSeaItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeaItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaItemVo tEnvFillSeaItem_UpdateSet, TEnvFillSeaItemVo tEnvFillSeaItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeaItem_UpdateSet, TEnvFillSeaItemVo.T_ENV_FILL_SEA_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSeaItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEA_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            string strSQL = "delete from T_ENV_FILL_SEA_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvFillSeaItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSeaItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSeaItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSeaItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillSeaItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillSeaItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillSeaItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSeaItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSeaItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSeaItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSeaItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSeaItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSeaItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSeaItem.REMARK5.ToString()));
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
            string strSQL = "select * from T_ENV_FILL_SEA_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
    }
}
