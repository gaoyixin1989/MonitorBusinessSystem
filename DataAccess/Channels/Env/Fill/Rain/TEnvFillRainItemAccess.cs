using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Rain;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Fill.Rain
{
    /// <summary>
    /// 功能：降水数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
     /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillRainItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RAIN_ITEM " + this.BuildWhereStatement(tEnvFillRainItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRainItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RAIN_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TEnvFillRainItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRainItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRainItemVo Details(TEnvFillRainItemVo tEnvFillRainItem)
        {
           string strSQL = String.Format("select * from  T_ENV_FILL_RAIN_ITEM " + this.BuildWhereStatement(tEnvFillRainItem));
           return SqlHelper.ExecuteObject(new TEnvFillRainItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRainItemVo> SelectByObject(TEnvFillRainItemVo tEnvFillRainItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_ENV_FILL_RAIN_ITEM " + this.BuildWhereStatement(tEnvFillRainItem));
            return SqlHelper.ExecuteObjectList(tEnvFillRainItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRainItemVo tEnvFillRainItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_ENV_FILL_RAIN_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRainItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRainItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = "select * from T_ENV_FILL_RAIN_ITEM " + this.BuildWhereStatement(tEnvFillRainItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <returns></returns>
        public TEnvFillRainItemVo SelectByObject(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = "select * from T_ENV_FILL_RAIN_ITEM " + this.BuildWhereStatement(tEnvFillRainItem);
            return SqlHelper.ExecuteObject(new TEnvFillRainItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRainItem, TEnvFillRainItemVo.T_ENV_FILL_RAIN_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRainItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRainItem, TEnvFillRainItemVo.T_ENV_FILL_RAIN_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRainItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRainItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillRainItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainItemVo tEnvFillRainItem_UpdateSet, TEnvFillRainItemVo tEnvFillRainItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRainItem_UpdateSet, TEnvFillRainItemVo.T_ENV_FILL_RAIN_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRainItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RAIN_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRainItemVo tEnvFillRainItem)
        {
            string strSQL = "delete from T_ENV_FILL_RAIN_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvFillRainItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion


        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRainItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRainItemVo tEnvFillRainItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRainItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRainItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRainItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRainItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRainItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillRainItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRainItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillRainItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillRainItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRainItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRainItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRainItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRainItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRainItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRainItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRainItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRainItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRainItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRainItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRainItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRainItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRainItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRainItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion



        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            string strSQL = "select * from T_ENV_FILL_RAIN_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
    }
}
