using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.River;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.River
{
    /// <summary>
    /// 功能：河流填报
    /// 创建日期：2013-06-18
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillRiverItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverItemVo Details(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillRiverItem));
            return SqlHelper.ExecuteObject(new TEnvFillRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverItemVo> SelectByObject(TEnvFillRiverItemVo tEnvFillRiverItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillRiverItem));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverItemVo tEnvFillRiverItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillRiverItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverItemVo SelectByObject(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillRiverItem);
            return SqlHelper.ExecuteObject(new TEnvFillRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverItem, TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverItem, TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverItemVo tEnvFillRiverItem_UpdateSet, TEnvFillRiverItemVo tEnvFillRiverItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverItem_UpdateSet, TEnvFillRiverItemVo.T_ENV_FILL_RIVER_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRiverItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRiverItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRiverItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
