using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.RiverTarget;

namespace i3.DataAccess.Channels.Env.Fill.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverTargetItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_TARGET_ITEM " + this.BuildWhereStatement(tEnvFillRiverTargetItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_TARGET_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverTargetItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetItemVo Details(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_TARGET_ITEM " + this.BuildWhereStatement(tEnvFillRiverTargetItem));
            return SqlHelper.ExecuteObject(new TEnvFillRiverTargetItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverTargetItemVo> SelectByObject(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_TARGET_ITEM " + this.BuildWhereStatement(tEnvFillRiverTargetItem));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverTargetItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_TARGET_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverTargetItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_TARGET_ITEM " + this.BuildWhereStatement(tEnvFillRiverTargetItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverTargetItemVo SelectByObject(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_TARGET_ITEM " + this.BuildWhereStatement(tEnvFillRiverTargetItem);
            return SqlHelper.ExecuteObject(new TEnvFillRiverTargetItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverTargetItem, TEnvFillRiverTargetItemVo.T_ENV_FILL_RIVER_TARGET_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverTargetItem, TEnvFillRiverTargetItemVo.T_ENV_FILL_RIVER_TARGET_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverTargetItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverTargetItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem_UpdateSet, TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverTargetItem_UpdateSet, TEnvFillRiverTargetItemVo.T_ENV_FILL_RIVER_TARGET_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverTargetItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_TARGET_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_TARGET_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverTargetItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverTargetItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverTargetItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRiverTargetItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRiverTargetItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRiverTargetItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverTargetItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverTargetItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverTargetItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverTargetItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverTargetItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverTargetItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverTargetItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
