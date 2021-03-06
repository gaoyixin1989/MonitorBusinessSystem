﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcVItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_DRINK_SRC_V_ITEM " + this.BuildWhereStatement(tEnvPDrinkSrcVItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVItemVo Details(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V_ITEM " + this.BuildWhereStatement(tEnvPDrinkSrcVItem));
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVItemVo> SelectByObject(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DRINK_SRC_V_ITEM " + this.BuildWhereStatement(tEnvPDrinkSrcVItem));
            return SqlHelper.ExecuteObjectList(tEnvPDrinkSrcVItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DRINK_SRC_V_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDrinkSrcVItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC_V_ITEM " + this.BuildWhereStatement(tEnvPDrinkSrcVItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVItemVo SelectByObject(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = "select * from T_ENV_P_DRINK_SRC_V_ITEM " + this.BuildWhereStatement(tEnvPDrinkSrcVItem);
            return SqlHelper.ExecuteObject(new TEnvPDrinkSrcVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkSrcVItem, TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrcVItem, TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDrinkSrcVItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrcVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem_UpdateSet, TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkSrcVItem_UpdateSet, TEnvPDrinkSrcVItemVo.T_ENV_P_DRINK_SRC_V_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrcVItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DRINK_SRC_V_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            string strSQL = "delete from T_ENV_P_DRINK_SRC_V_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPDrinkSrcVItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDrinkSrcVItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDrinkSrcVItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPDrinkSrcVItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPDrinkSrcVItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPDrinkSrcVItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDrinkSrcVItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDrinkSrcVItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDrinkSrcVItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDrinkSrcVItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDrinkSrcVItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDrinkSrcVItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
