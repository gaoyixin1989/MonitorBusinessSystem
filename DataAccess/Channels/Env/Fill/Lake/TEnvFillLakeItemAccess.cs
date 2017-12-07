using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Lake;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Lake
{
    /// <summary>
    /// 功能：湖库填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillLakeItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_LAKE_ITEM " + this.BuildWhereStatement(tEnvFillLakeItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillLakeItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillLakeItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillLakeItemVo Details(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE_ITEM " + this.BuildWhereStatement(tEnvFillLakeItem));
            return SqlHelper.ExecuteObject(new TEnvFillLakeItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillLakeItemVo> SelectByObject(TEnvFillLakeItemVo tEnvFillLakeItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE_ITEM " + this.BuildWhereStatement(tEnvFillLakeItem));
            return SqlHelper.ExecuteObjectList(tEnvFillLakeItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillLakeItemVo tEnvFillLakeItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_LAKE_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillLakeItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillLakeItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = "select * from T_ENV_FILL_LAKE_ITEM " + this.BuildWhereStatement(tEnvFillLakeItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <returns></returns>
        public TEnvFillLakeItemVo SelectByObject(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = "select * from T_ENV_FILL_LAKE_ITEM " + this.BuildWhereStatement(tEnvFillLakeItem);
            return SqlHelper.ExecuteObject(new TEnvFillLakeItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillLakeItem, TEnvFillLakeItemVo.T_ENV_FILL_LAKE_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLakeItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillLakeItem, TEnvFillLakeItemVo.T_ENV_FILL_LAKE_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillLakeItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLakeItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillLakeItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeItemVo tEnvFillLakeItem_UpdateSet, TEnvFillLakeItemVo tEnvFillLakeItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillLakeItem_UpdateSet, TEnvFillLakeItemVo.T_ENV_FILL_LAKE_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillLakeItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_LAKE_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            string strSQL = "delete from T_ENV_FILL_LAKE_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillLakeItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillLakeItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillLakeItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillLakeItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillLakeItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillLakeItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillLakeItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillLakeItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillLakeItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillLakeItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillLakeItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillLakeItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillLakeItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillLakeItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
