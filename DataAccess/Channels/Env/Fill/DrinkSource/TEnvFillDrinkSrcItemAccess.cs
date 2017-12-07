using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.DrinkSource;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.DrinkSource
{
    /// <summary>
    /// 功能：饮用水源地数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkSrcItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillDrinkSrcItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcItemVo Details(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillDrinkSrcItem));
            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkSrcItemVo> SelectByObject(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillDrinkSrcItem));
            return SqlHelper.ExecuteObjectList(tEnvFillDrinkSrcItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DRINK_SRC_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDrinkSrcItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillDrinkSrcItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkSrcItemVo SelectByObject(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_SRC_ITEM " + this.BuildWhereStatement(tEnvFillDrinkSrcItem);
            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDrinkSrcItem, TEnvFillDrinkSrcItemVo.T_ENV_FILL_DRINK_SRC_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkSrcItem, TEnvFillDrinkSrcItemVo.T_ENV_FILL_DRINK_SRC_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDrinkSrcItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkSrcItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem_UpdateSet, TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkSrcItem_UpdateSet, TEnvFillDrinkSrcItemVo.T_ENV_FILL_DRINK_SRC_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDrinkSrcItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DRINK_SRC_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            string strSQL = "delete from T_ENV_FILL_DRINK_SRC_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillDrinkSrcItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDrinkSrcItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDrinkSrcItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillDrinkSrcItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillDrinkSrcItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillDrinkSrcItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillDrinkSrcItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDrinkSrcItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDrinkSrcItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDrinkSrcItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDrinkSrcItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrcItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDrinkSrcItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
