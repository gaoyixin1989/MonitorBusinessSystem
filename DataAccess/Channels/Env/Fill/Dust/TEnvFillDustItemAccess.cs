using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Dust;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Dust
{
    /// <summary>
    /// 功能：降尘填报监测项目
    /// 创建日期：2013-06-21
    /// 创建人：刘静楠
    /// 
    /// </summary>
    public class TEnvFillDustItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DUST_ITEM " + this.BuildWhereStatement(tEnvFillDustItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDustItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DUST_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDustItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDustItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDustItemVo Details(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DUST_ITEM " + this.BuildWhereStatement(tEnvFillDustItem));
            return SqlHelper.ExecuteObject(new TEnvFillDustItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDustItemVo> SelectByObject(TEnvFillDustItemVo tEnvFillDustItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DUST_ITEM " + this.BuildWhereStatement(tEnvFillDustItem));
            return SqlHelper.ExecuteObjectList(tEnvFillDustItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDustItemVo tEnvFillDustItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DUST_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDustItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDustItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = "select * from T_ENV_FILL_DUST_ITEM " + this.BuildWhereStatement(tEnvFillDustItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <returns></returns>
        public TEnvFillDustItemVo SelectByObject(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = "select * from T_ENV_FILL_DUST_ITEM " + this.BuildWhereStatement(tEnvFillDustItem);
            return SqlHelper.ExecuteObject(new TEnvFillDustItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDustItem, TEnvFillDustItemVo.T_ENV_FILL_DUST_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDustItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDustItem, TEnvFillDustItemVo.T_ENV_FILL_DUST_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDustItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDustItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDustItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustItemVo tEnvFillDustItem_UpdateSet, TEnvFillDustItemVo tEnvFillDustItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDustItem_UpdateSet, TEnvFillDustItemVo.T_ENV_FILL_DUST_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDustItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DUST_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDustItemVo tEnvFillDustItem)
        {
            string strSQL = "delete from T_ENV_FILL_DUST_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillDustItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDustItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDustItemVo tEnvFillDustItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDustItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDustItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDustItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillDustItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillDustItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillDustItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillDustItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillDustItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillDustItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillDustItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillDustItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillDustItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillDustItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDustItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDustItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDustItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDustItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDustItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDustItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDustItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDustItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDustItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDustItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
