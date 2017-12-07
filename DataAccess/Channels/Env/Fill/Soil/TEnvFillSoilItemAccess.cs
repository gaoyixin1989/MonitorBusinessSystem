using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Soil;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Soil
{
    /// <summary>
    /// 功能：土壤数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSoilItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SOIL_ITEM " + this.BuildWhereStatement(tEnvFillSoilItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSoilItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSoilItemVo Details(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL_ITEM " + this.BuildWhereStatement(tEnvFillSoilItem));
            return SqlHelper.ExecuteObject(new TEnvFillSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSoilItemVo> SelectByObject(TEnvFillSoilItemVo tEnvFillSoilItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL_ITEM " + this.BuildWhereStatement(tEnvFillSoilItem));
            return SqlHelper.ExecuteObjectList(tEnvFillSoilItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSoilItemVo tEnvFillSoilItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SOIL_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSoilItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSoilItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = "select * from T_ENV_FILL_SOIL_ITEM " + this.BuildWhereStatement(tEnvFillSoilItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <returns></returns>
        public TEnvFillSoilItemVo SelectByObject(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = "select * from T_ENV_FILL_SOIL_ITEM " + this.BuildWhereStatement(tEnvFillSoilItem);
            return SqlHelper.ExecuteObject(new TEnvFillSoilItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSoilItem, TEnvFillSoilItemVo.T_ENV_FILL_SOIL_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoilItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSoilItem, TEnvFillSoilItemVo.T_ENV_FILL_SOIL_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSoilItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoilItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSoilItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilItemVo tEnvFillSoilItem_UpdateSet, TEnvFillSoilItemVo tEnvFillSoilItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSoilItem_UpdateSet, TEnvFillSoilItemVo.T_ENV_FILL_SOIL_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSoilItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SOIL_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            string strSQL = "delete from T_ENV_FILL_SOIL_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillSoilItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSoilItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSoilItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSoilItem.ID.ToString()));
                }
                //填报ID
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillSoilItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillSoilItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillSoilItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSoilItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSoilItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSoilItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSoilItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSoilItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSoilItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSoilItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
