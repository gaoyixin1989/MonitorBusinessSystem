using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverCity;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverCityVItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_CITY_V_ITEM " + this.BuildWhereStatement(tEnvPRiverCityVItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverCityVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVItemVo Details(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V_ITEM " + this.BuildWhereStatement(tEnvPRiverCityVItem));
            return SqlHelper.ExecuteObject(new TEnvPRiverCityVItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverCityVItemVo> SelectByObject(TEnvPRiverCityVItemVo tEnvPRiverCityVItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V_ITEM " + this.BuildWhereStatement(tEnvPRiverCityVItem));
            return SqlHelper.ExecuteObjectList(tEnvPRiverCityVItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverCityVItemVo tEnvPRiverCityVItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_CITY_V_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverCityVItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverCityVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = "select * from T_ENV_P_RIVER_CITY_V_ITEM " + this.BuildWhereStatement(tEnvPRiverCityVItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverCityVItemVo SelectByObject(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = "select * from T_ENV_P_RIVER_CITY_V_ITEM " + this.BuildWhereStatement(tEnvPRiverCityVItem);
            return SqlHelper.ExecuteObject(new TEnvPRiverCityVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverCityVItem, TEnvPRiverCityVItemVo.T_ENV_P_RIVER_CITY_V_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverCityVItem, TEnvPRiverCityVItemVo.T_ENV_P_RIVER_CITY_V_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverCityVItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverCityVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVItemVo tEnvPRiverCityVItem_UpdateSet, TEnvPRiverCityVItemVo tEnvPRiverCityVItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverCityVItem_UpdateSet, TEnvPRiverCityVItemVo.T_ENV_P_RIVER_CITY_V_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverCityVItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_CITY_V_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            string strSQL = "delete from T_ENV_P_RIVER_CITY_V_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPRiverCityVItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverCityVItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverCityVItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverCityVItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPRiverCityVItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPRiverCityVItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPRiverCityVItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverCityVItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverCityVItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverCityVItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverCityVItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverCityVItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverCityVItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
