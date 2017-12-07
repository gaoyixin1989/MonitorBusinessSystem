using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.PayFor;

namespace i3.DataAccess.Channels.Env.Fill.PayFor
{
    /// <summary>
    /// 功能：生态补偿数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillPayforItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvFillPayforItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPayforItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPayforItemVo Details(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvFillPayforItem));
            return SqlHelper.ExecuteObject(new TEnvFillPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPayforItemVo> SelectByObject(TEnvFillPayforItemVo tEnvFillPayforItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvFillPayforItem));
            return SqlHelper.ExecuteObjectList(tEnvFillPayforItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPayforItemVo tEnvFillPayforItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_PAYFOR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPayforItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPayforItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = "select * from T_ENV_FILL_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvFillPayforItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象</param>
        /// <returns></returns>
        public TEnvFillPayforItemVo SelectByObject(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = "select * from T_ENV_FILL_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvFillPayforItem);
            return SqlHelper.ExecuteObject(new TEnvFillPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPayforItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPayforItem, TEnvFillPayforItemVo.T_ENV_FILL_PAYFOR_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayforItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPayforItem, TEnvFillPayforItemVo.T_ENV_FILL_PAYFOR_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPayforItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayforItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPayforItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforItemVo tEnvFillPayforItem_UpdateSet, TEnvFillPayforItemVo tEnvFillPayforItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPayforItem_UpdateSet, TEnvFillPayforItemVo.T_ENV_FILL_PAYFOR_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPayforItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_PAYFOR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            string strSQL = "delete from T_ENV_FILL_PAYFOR_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillPayforItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPayforItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPayforItemVo tEnvFillPayforItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPayforItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPayforItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillPayforItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillPayforItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillPayforItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillPayforItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPayforItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPayforItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPayforItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPayforItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillPayforItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPayforItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
