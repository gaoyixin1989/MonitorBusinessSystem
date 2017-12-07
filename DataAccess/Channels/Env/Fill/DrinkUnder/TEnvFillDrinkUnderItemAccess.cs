using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.DrinkUnder;

namespace i3.DataAccess.Channels.Env.Fill.DrinkUnder
{
    /// <summary>
    /// 功能：地下水填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkUnderItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvFillDrinkUnderItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderItemVo Details(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvFillDrinkUnderItem));
            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkUnderItemVo> SelectByObject(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvFillDrinkUnderItem));
            return SqlHelper.ExecuteObjectList(tEnvFillDrinkUnderItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DRINK_UNDER_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDrinkUnderItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvFillDrinkUnderItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkUnderItemVo SelectByObject(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvFillDrinkUnderItem);
            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDrinkUnderItem, TEnvFillDrinkUnderItemVo.T_ENV_FILL_DRINK_UNDER_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkUnderItem, TEnvFillDrinkUnderItemVo.T_ENV_FILL_DRINK_UNDER_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDrinkUnderItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkUnderItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem_UpdateSet, TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkUnderItem_UpdateSet, TEnvFillDrinkUnderItemVo.T_ENV_FILL_DRINK_UNDER_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDrinkUnderItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DRINK_UNDER_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            string strSQL = "delete from T_ENV_FILL_DRINK_UNDER_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillDrinkUnderItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDrinkUnderItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDrinkUnderItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillDrinkUnderItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillDrinkUnderItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillDrinkUnderItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillDrinkUnderItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDrinkUnderItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDrinkUnderItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDrinkUnderItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDrinkUnderItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnderItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDrinkUnderItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
