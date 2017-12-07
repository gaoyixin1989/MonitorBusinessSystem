using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverPlan;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverPlanVItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_PLAN_V_ITEM " + this.BuildWhereStatement(tEnvPRiverPlanVItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVItemVo Details(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V_ITEM " + this.BuildWhereStatement(tEnvPRiverPlanVItem));
            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverPlanVItemVo> SelectByObject(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V_ITEM " + this.BuildWhereStatement(tEnvPRiverPlanVItem));
            return SqlHelper.ExecuteObjectList(tEnvPRiverPlanVItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_PLAN_V_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverPlanVItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = "select * from T_ENV_P_RIVER_PLAN_V_ITEM " + this.BuildWhereStatement(tEnvPRiverPlanVItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverPlanVItemVo SelectByObject(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = "select * from T_ENV_P_RIVER_PLAN_V_ITEM " + this.BuildWhereStatement(tEnvPRiverPlanVItem);
            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverPlanVItem, TEnvPRiverPlanVItemVo.T_ENV_P_RIVER_PLAN_V_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverPlanVItem, TEnvPRiverPlanVItemVo.T_ENV_P_RIVER_PLAN_V_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverPlanVItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverPlanVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem_UpdateSet, TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverPlanVItem_UpdateSet, TEnvPRiverPlanVItemVo.T_ENV_P_RIVER_PLAN_V_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverPlanVItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_PLAN_V_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            string strSQL = "delete from T_ENV_P_RIVER_PLAN_V_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPRiverPlanVItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverPlanVItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverPlanVItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPRiverPlanVItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPRiverPlanVItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPRiverPlanVItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverPlanVItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverPlanVItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverPlanVItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverPlanVItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverPlanVItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverPlanVItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
