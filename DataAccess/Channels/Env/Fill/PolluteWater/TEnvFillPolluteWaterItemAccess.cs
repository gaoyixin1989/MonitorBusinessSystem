using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteWater;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_POLLUTE_WATER_ITEM " + this.BuildWhereStatement(tEnvFillPolluteWaterItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterItemVo Details(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER_ITEM " + this.BuildWhereStatement(tEnvFillPolluteWaterItem));
            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteWaterItemVo> SelectByObject(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_POLLUTE_WATER_ITEM " + this.BuildWhereStatement(tEnvFillPolluteWaterItem));
            return SqlHelper.ExecuteObjectList(tEnvFillPolluteWaterItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_POLLUTE_WATER_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPolluteWaterItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_WATER_ITEM " + this.BuildWhereStatement(tEnvFillPolluteWaterItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteWaterItemVo SelectByObject(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = "select * from T_ENV_FILL_POLLUTE_WATER_ITEM " + this.BuildWhereStatement(tEnvFillPolluteWaterItem);
            return SqlHelper.ExecuteObject(new TEnvFillPolluteWaterItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPolluteWaterItem, TEnvFillPolluteWaterItemVo.T_ENV_FILL_POLLUTE_WATER_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteWaterItem, TEnvFillPolluteWaterItemVo.T_ENV_FILL_POLLUTE_WATER_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPolluteWaterItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteWaterItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem_UpdateSet, TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPolluteWaterItem_UpdateSet, TEnvFillPolluteWaterItemVo.T_ENV_FILL_POLLUTE_WATER_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPolluteWaterItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_POLLUTE_WATER_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            string strSQL = "delete from T_ENV_FILL_POLLUTE_WATER_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillPolluteWaterItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPolluteWaterItem)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPolluteWaterItem.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillPolluteWaterItem.FILL_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillPolluteWaterItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.UPLINE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLINE = '{0}'", tEnvFillPolluteWaterItem.UPLINE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.DOWNLINE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOWNLINE = '{0}'", tEnvFillPolluteWaterItem.DOWNLINE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.UOM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UOM = '{0}'", tEnvFillPolluteWaterItem.UOM.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tEnvFillPolluteWaterItem.STANDARD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillPolluteWaterItem.ITEM_VALUE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.IS_STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_STANDARD = '{0}'", tEnvFillPolluteWaterItem.IS_STANDARD.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.WATER_PER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_PER = '{0}'", tEnvFillPolluteWaterItem.WATER_PER.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPolluteWaterItem.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPolluteWaterItem.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPolluteWaterItem.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPolluteWaterItem.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillPolluteWaterItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPolluteWaterItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
