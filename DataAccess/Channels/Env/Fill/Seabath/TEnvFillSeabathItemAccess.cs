using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Seabath;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Seabath
{
    /// <summary>
    /// 功能：海水浴场填报
    /// 创建日期：2013-06-25
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillSeabathItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEABATH_ITEM " + this.BuildWhereStatement(tEnvFillSeabathItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathItemVo Details(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH_ITEM " + this.BuildWhereStatement(tEnvFillSeabathItem));
            return SqlHelper.ExecuteObject(new TEnvFillSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeabathItemVo> SelectByObject(TEnvFillSeabathItemVo tEnvFillSeabathItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH_ITEM " + this.BuildWhereStatement(tEnvFillSeabathItem));
            return SqlHelper.ExecuteObjectList(tEnvFillSeabathItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeabathItemVo tEnvFillSeabathItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SEABATH_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSeabathItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeabathItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEABATH_ITEM " + this.BuildWhereStatement(tEnvFillSeabathItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <returns></returns>
        public TEnvFillSeabathItemVo SelectByObject(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEABATH_ITEM " + this.BuildWhereStatement(tEnvFillSeabathItem);
            return SqlHelper.ExecuteObject(new TEnvFillSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSeabathItem, TEnvFillSeabathItemVo.T_ENV_FILL_SEABATH_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabathItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeabathItem, TEnvFillSeabathItemVo.T_ENV_FILL_SEABATH_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSeabathItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabathItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSeabathItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathItemVo tEnvFillSeabathItem_UpdateSet, TEnvFillSeabathItemVo tEnvFillSeabathItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeabathItem_UpdateSet, TEnvFillSeabathItemVo.T_ENV_FILL_SEABATH_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSeabathItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEABATH_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            string strSQL = "delete from T_ENV_FILL_SEABATH_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillSeabathItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSeabathItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSeabathItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSeabathItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillSeabathItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillSeabathItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillSeabathItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSeabathItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSeabathItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSeabathItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSeabathItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSeabathItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSeabathItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSeabathItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }



}
