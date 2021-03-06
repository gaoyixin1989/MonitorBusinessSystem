using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Offshore;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Fill.Offshore
{
    /// <summary>
    /// 功能：近岸直排数据填报监测项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillOffshoreItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvFillOffshoreItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TEnvFillOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreItemVo Details(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
           string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvFillOffshoreItem));
           return SqlHelper.ExecuteObject(new TEnvFillOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillOffshoreItemVo> SelectByObject(TEnvFillOffshoreItemVo tEnvFillOffshoreItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvFillOffshoreItem));
            return SqlHelper.ExecuteObjectList(tEnvFillOffshoreItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillOffshoreItemVo tEnvFillOffshoreItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_ENV_FILL_OFFSHORE_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillOffshoreItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillOffshoreItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = "select * from T_ENV_FILL_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvFillOffshoreItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <returns></returns>
        public TEnvFillOffshoreItemVo SelectByObject(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = "select * from T_ENV_FILL_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvFillOffshoreItem);
            return SqlHelper.ExecuteObject(new TEnvFillOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillOffshoreItem, TEnvFillOffshoreItemVo.T_ENV_FILL_OFFSHORE_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillOffshoreItem, TEnvFillOffshoreItemVo.T_ENV_FILL_OFFSHORE_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillOffshoreItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshoreItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillOffshoreItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreItemVo tEnvFillOffshoreItem_UpdateSet, TEnvFillOffshoreItemVo tEnvFillOffshoreItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillOffshoreItem_UpdateSet, TEnvFillOffshoreItemVo.T_ENV_FILL_OFFSHORE_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillOffshoreItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_OFFSHORE_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            string strSQL = "delete from T_ENV_FILL_OFFSHORE_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvFillOffshoreItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillOffshoreItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillOffshoreItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillOffshoreItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillOffshoreItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillOffshoreItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillOffshoreItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillOffshoreItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillOffshoreItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillOffshoreItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillOffshoreItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillOffshoreItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillOffshoreItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillOffshoreItem.REMARK5.ToString()));
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
            string strSQL = "select * from T_ENV_FILL_OFFSHORE_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
    }
}
