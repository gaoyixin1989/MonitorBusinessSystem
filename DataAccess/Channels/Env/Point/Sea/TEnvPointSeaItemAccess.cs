using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Point.Sea;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Sea
{
    /// <summary>
    /// 功能：近海海域监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointSeaItemAccess : SqlHelper 
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEA_ITEM " + this.BuildWhereStatement(tEnvPointSeaItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointSeaItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEA_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointSeaItemVo Details(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEA_ITEM " + this.BuildWhereStatement(tEnvPointSeaItem));
            return SqlHelper.ExecuteObject(new TEnvPointSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointSeaItemVo> SelectByObject(TEnvPointSeaItemVo tEnvPointSeaItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEA_ITEM " + this.BuildWhereStatement(tEnvPointSeaItem));
            return SqlHelper.ExecuteObjectList(tEnvPointSeaItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointSeaItemVo tEnvPointSeaItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEA_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointSeaItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = "select * from T_ENV_P_SEA_ITEM " + this.BuildWhereStatement(tEnvPointSeaItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <returns></returns>
        public TEnvPointSeaItemVo SelectByObject(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = "select * from T_ENV_P_SEA_ITEM " + this.BuildWhereStatement(tEnvPointSeaItem);
            return SqlHelper.ExecuteObject(new TEnvPointSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointSeaItem, TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointSeaItem, TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointSeaItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSeaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaItemVo tEnvPointSeaItem_UpdateSet, TEnvPointSeaItemVo tEnvPointSeaItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointSeaItem_UpdateSet, TEnvPointSeaItemVo.T_ENV_POINT_SEA_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointSeaItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEA_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            string strSQL = "delete from T_ENV_P_SEA_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPointSeaItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointSeaItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointSeaItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointSeaItem.ID.ToString()));
                }
                //近海海域监测点ID
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPointSeaItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPointSeaItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPointSeaItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointSeaItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointSeaItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointSeaItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointSeaItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointSeaItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointSeaItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
