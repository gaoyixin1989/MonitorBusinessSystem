using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteAir;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_POLLUTE_AIR_ITEM " + this.BuildWhereStatement(tEnvFillPolluteAirItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirItemVo Details(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR_ITEM " + this.BuildWhereStatement(tEnvFillPolluteAirItem));
            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteAirItemVo> SelectByObject(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_AIR_ITEM " + this.BuildWhereStatement(tEnvFillPolluteAirItem));
            return SqlHelper.ExecuteObjectList(tEnvFillPolluteAirItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_POLLUTE_AIR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPolluteAirItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_AIR_ITEM " + this.BuildWhereStatement(tEnvFillPolluteAirItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteAirItemVo SelectByObject(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_AIR_ITEM " + this.BuildWhereStatement(tEnvFillPolluteAirItem);
            return SqlHelper.ExecuteObject(new TEnvFillPolluteAirItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPolluteAirItem, TEnvFillPolluteAirItemVo.T_ENV_FILL_POLLUTE_AIR_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteAirItem, TEnvFillPolluteAirItemVo.T_ENV_FILL_POLLUTE_AIR_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPolluteAirItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteAirItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem_UpdateSet, TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteAirItem_UpdateSet, TEnvFillPolluteAirItemVo.T_ENV_FILL_POLLUTE_AIR_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPolluteAirItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_POLLUTE_AIR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            string strSQL = "delete from T_ENV_FILL_POLLUTE_AIR_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillPolluteAirItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPolluteAirItem)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPolluteAirItem.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillPolluteAirItem.FILL_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillPolluteAirItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillPolluteAirItem.ITEM_VALUE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.UP_LINE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_LINE = '{0}'", tEnvFillPolluteAirItem.UP_LINE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.DOWN_LINE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOWN_LINE = '{0}'", tEnvFillPolluteAirItem.DOWN_LINE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.UOM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UOM = '{0}'", tEnvFillPolluteAirItem.UOM.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tEnvFillPolluteAirItem.STANDARD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.OQTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OQTY = '{0}'", tEnvFillPolluteAirItem.OQTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.POLLUTEPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POLLUTEPER = '{0}'", tEnvFillPolluteAirItem.POLLUTEPER.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.POLLUTECALPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POLLUTECALPER = '{0}'", tEnvFillPolluteAirItem.POLLUTECALPER.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.IS_STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_STANDARD = '{0}'", tEnvFillPolluteAirItem.IS_STANDARD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.AIRQTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIRQTY = '{0}'", tEnvFillPolluteAirItem.AIRQTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPolluteAirItem.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPolluteAirItem.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPolluteAirItem.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPolluteAirItem.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteAirItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPolluteAirItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
