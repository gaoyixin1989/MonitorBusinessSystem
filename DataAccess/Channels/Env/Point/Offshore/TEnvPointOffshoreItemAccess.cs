using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Offshore
{
    /// <summary>
    /// 功能：近岸直排监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointOffshoreItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvPointOffshoreItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreItemVo Details(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvPointOffshoreItem));
            return SqlHelper.ExecuteObject(new TEnvPointOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointOffshoreItemVo> SelectByObject(TEnvPointOffshoreItemVo tEnvPointOffshoreItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvPointOffshoreItem));
            return SqlHelper.ExecuteObjectList(tEnvPointOffshoreItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointOffshoreItemVo tEnvPointOffshoreItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_OFFSHORE_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointOffshoreItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointOffshoreItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = "select * from T_ENV_P_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvPointOffshoreItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <returns></returns>
        public TEnvPointOffshoreItemVo SelectByObject(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = "select * from T_ENV_P_OFFSHORE_ITEM " + this.BuildWhereStatement(tEnvPointOffshoreItem);
            return SqlHelper.ExecuteObject(new TEnvPointOffshoreItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointOffshoreItem, TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointOffshoreItem, TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointOffshoreItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshoreItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointOffshoreItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreItemVo tEnvPointOffshoreItem_UpdateSet, TEnvPointOffshoreItemVo tEnvPointOffshoreItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointOffshoreItem_UpdateSet, TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointOffshoreItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_OFFSHORE_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            string strSQL = "delete from T_ENV_P_OFFSHORE_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPointOffshoreItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointOffshoreItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointOffshoreItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointOffshoreItem.ID.ToString()));
                }
                //近岸直排监测点ID
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPointOffshoreItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPointOffshoreItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPointOffshoreItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointOffshoreItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointOffshoreItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointOffshoreItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointOffshoreItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointOffshoreItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointOffshoreItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
