using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.RiverPlan;

namespace i3.DataAccess.Channels.Env.Fill.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverPlanItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_PLAN_ITEM " + this.BuildWhereStatement(tEnvFillRiverPlanItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanItemVo Details(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN_ITEM " + this.BuildWhereStatement(tEnvFillRiverPlanItem));
            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverPlanItemVo> SelectByObject(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN_ITEM " + this.BuildWhereStatement(tEnvFillRiverPlanItem));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverPlanItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_PLAN_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverPlanItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_PLAN_ITEM " + this.BuildWhereStatement(tEnvFillRiverPlanItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverPlanItemVo SelectByObject(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_PLAN_ITEM " + this.BuildWhereStatement(tEnvFillRiverPlanItem);
            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverPlanItem, TEnvFillRiverPlanItemVo.T_ENV_FILL_RIVER_PLAN_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverPlanItem, TEnvFillRiverPlanItemVo.T_ENV_FILL_RIVER_PLAN_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverPlanItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverPlanItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem_UpdateSet, TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverPlanItem_UpdateSet, TEnvFillRiverPlanItemVo.T_ENV_FILL_RIVER_PLAN_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverPlanItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_PLAN_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_PLAN_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverPlanItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverPlanItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverPlanItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRiverPlanItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRiverPlanItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRiverPlanItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverPlanItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverPlanItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverPlanItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverPlanItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverPlanItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverPlanItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverPlanItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
