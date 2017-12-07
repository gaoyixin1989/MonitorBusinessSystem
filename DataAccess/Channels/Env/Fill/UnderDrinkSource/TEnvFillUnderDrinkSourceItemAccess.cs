using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.UnderDrinkSource;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.UnderDrinkSource
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-26
    /// 创建人：
    /// </summary>
    public class TEnvFillUnderdrinkSrcItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_UNDERDRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillUnderdrinkSrcItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillUnderdrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillUnderdrinkSrcItemVo Details(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem));
            return SqlHelper.ExecuteObject(new TEnvFillUnderdrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillUnderdrinkSrcItemVo> SelectByObject(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_UNDERDRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem));
            return SqlHelper.ExecuteObjectList(tEnvFillUnderdrinkSrcItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_UNDERDRINK_SRC_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillUnderdrinkSrcItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = "select * from T_ENV_FILL_UNDERDRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <returns></returns>
        public TEnvFillUnderdrinkSrcItemVo SelectByObject(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = "select * from T_ENV_FILL_UNDERDRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem);
            return SqlHelper.ExecuteObject(new TEnvFillUnderdrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillUnderdrinkSrcItem, TEnvFillUnderdrinkSrcItemVo.T_ENV_FILL_UNDERDRINK_SRC_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillUnderdrinkSrcItem, TEnvFillUnderdrinkSrcItemVo.T_ENV_FILL_UNDERDRINK_SRC_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillUnderdrinkSrcItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillUnderdrinkSrcItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem_UpdateSet, TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillUnderdrinkSrcItem_UpdateSet, TEnvFillUnderdrinkSrcItemVo.T_ENV_FILL_UNDERDRINK_SRC_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_UNDERDRINK_SRC_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            string strSQL = "delete from T_ENV_FILL_UNDERDRINK_SRC_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillUnderdrinkSrcItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillUnderdrinkSrcItem)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillUnderdrinkSrcItem.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillUnderdrinkSrcItem.FILL_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillUnderdrinkSrcItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillUnderdrinkSrcItem.ITEM_VALUE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillUnderdrinkSrcItem.JUDGE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillUnderdrinkSrcItem.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillUnderdrinkSrcItem.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillUnderdrinkSrcItem.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillUnderdrinkSrcItem.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillUnderdrinkSrcItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillUnderdrinkSrcItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

} 
