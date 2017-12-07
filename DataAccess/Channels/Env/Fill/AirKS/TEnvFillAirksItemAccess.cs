using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirKS;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.AirKS
{

    /// <summary>
    /// 功能：环境空气(科室)填报监测项目
    /// 创建日期：2013-07-03
    /// 创建人：刘静楠
    public class TEnvFillAirksItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIRKS_ITEM " + this.BuildWhereStatement(tEnvFillAirksItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirksItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirksItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirksItemVo Details(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS_ITEM " + this.BuildWhereStatement(tEnvFillAirksItem));
            return SqlHelper.ExecuteObject(new TEnvFillAirksItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirksItemVo> SelectByObject(TEnvFillAirksItemVo tEnvFillAirksItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIRKS_ITEM " + this.BuildWhereStatement(tEnvFillAirksItem));
            return SqlHelper.ExecuteObjectList(tEnvFillAirksItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirksItemVo tEnvFillAirksItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIRKS_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAirksItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirksItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = "select * from T_ENV_FILL_AIRKS_ITEM " + this.BuildWhereStatement(tEnvFillAirksItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <returns></returns>
        public TEnvFillAirksItemVo SelectByObject(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = "select * from T_ENV_FILL_AIRKS_ITEM " + this.BuildWhereStatement(tEnvFillAirksItem);
            return SqlHelper.ExecuteObject(new TEnvFillAirksItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAirksItem, TEnvFillAirksItemVo.T_ENV_FILL_AIRKS_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirksItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirksItem, TEnvFillAirksItemVo.T_ENV_FILL_AIRKS_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAirksItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirksItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirksItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksItemVo tEnvFillAirksItem_UpdateSet, TEnvFillAirksItemVo tEnvFillAirksItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirksItem_UpdateSet, TEnvFillAirksItemVo.T_ENV_FILL_AIRKS_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAirksItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIRKS_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            string strSQL = "delete from T_ENV_FILL_AIRKS_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillAirksItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAirksItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAirksItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAirksItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillAirksItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillAirksItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillAirksItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillAirksItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillAirksItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAirksItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAirksItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAirksItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAirksItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAirksItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAirksItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
