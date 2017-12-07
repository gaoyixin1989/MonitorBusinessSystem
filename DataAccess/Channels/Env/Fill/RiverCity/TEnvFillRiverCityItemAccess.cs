using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverCity;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverCityItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_CITY_ITEM " + this.BuildWhereStatement(tEnvFillRiverCityItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverCityItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityItemVo Details(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY_ITEM " + this.BuildWhereStatement(tEnvFillRiverCityItem));
            return SqlHelper.ExecuteObject(new TEnvFillRiverCityItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverCityItemVo> SelectByObject(TEnvFillRiverCityItemVo tEnvFillRiverCityItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY_ITEM " + this.BuildWhereStatement(tEnvFillRiverCityItem));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverCityItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverCityItemVo tEnvFillRiverCityItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_CITY_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverCityItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverCityItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_CITY_ITEM " + this.BuildWhereStatement(tEnvFillRiverCityItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverCityItemVo SelectByObject(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_CITY_ITEM " + this.BuildWhereStatement(tEnvFillRiverCityItem);
            return SqlHelper.ExecuteObject(new TEnvFillRiverCityItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverCityItem, TEnvFillRiverCityItemVo.T_ENV_FILL_RIVER_CITY_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverCityItem, TEnvFillRiverCityItemVo.T_ENV_FILL_RIVER_CITY_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverCityItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCityItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverCityItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityItemVo tEnvFillRiverCityItem_UpdateSet, TEnvFillRiverCityItemVo tEnvFillRiverCityItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverCityItem_UpdateSet, TEnvFillRiverCityItemVo.T_ENV_FILL_RIVER_CITY_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverCityItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_CITY_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_CITY_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverCityItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverCityItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverCityItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverCityItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRiverCityItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRiverCityItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRiverCityItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverCityItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverCityItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverCityItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverCityItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverCityItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverCityItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverCityItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
