using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Soil;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.Soil
{
    /// <summary>
    /// 功能：土壤
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSoilItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_SOIL_ITEM " + this.BuildWhereStatement(tEnvPSoilItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSoilItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOIL_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSoilItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSoilItemVo Details(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOIL_ITEM " + this.BuildWhereStatement(tEnvPSoilItem));
            return SqlHelper.ExecuteObject(new TEnvPSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSoilItemVo> SelectByObject(TEnvPSoilItemVo tEnvPSoilItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SOIL_ITEM " + this.BuildWhereStatement(tEnvPSoilItem));
            return SqlHelper.ExecuteObjectList(tEnvPSoilItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSoilItemVo tEnvPSoilItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SOIL_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSoilItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSoilItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = "select * from T_ENV_P_SOIL_ITEM " + this.BuildWhereStatement(tEnvPSoilItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <returns></returns>
        public TEnvPSoilItemVo SelectByObject(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = "select * from T_ENV_P_SOIL_ITEM " + this.BuildWhereStatement(tEnvPSoilItem);
            return SqlHelper.ExecuteObject(new TEnvPSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSoilItem, TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoilItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSoilItem, TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSoilItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoilItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSoilItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilItemVo tEnvPSoilItem_UpdateSet, TEnvPSoilItemVo tEnvPSoilItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSoilItem_UpdateSet, TEnvPSoilItemVo.T_ENV_P_SOIL_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSoilItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SOIL_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSoilItemVo tEnvPSoilItem)
        {
            string strSQL = "delete from T_ENV_P_SOIL_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPSoilItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSoilItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSoilItemVo tEnvPSoilItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSoilItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSoilItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSoilItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPSoilItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPSoilItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPSoilItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPSoilItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPSoilItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPSoilItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSoilItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSoilItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSoilItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSoilItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSoilItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSoilItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSoilItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSoilItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSoilItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSoilItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
