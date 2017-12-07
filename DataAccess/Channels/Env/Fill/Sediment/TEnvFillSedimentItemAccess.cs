using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Sediment;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Sediment
{
    /// <summary>
    /// 功能：底泥重金属填报
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSedimentItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvFillSedimentItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentItemVo Details(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvFillSedimentItem));
            return SqlHelper.ExecuteObject(new TEnvFillSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSedimentItemVo> SelectByObject(TEnvFillSedimentItemVo tEnvFillSedimentItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvFillSedimentItem));
            return SqlHelper.ExecuteObjectList(tEnvFillSedimentItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSedimentItemVo tEnvFillSedimentItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SEDIMENT_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSedimentItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSedimentItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvFillSedimentItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <returns></returns>
        public TEnvFillSedimentItemVo SelectByObject(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = "select * from T_ENV_FILL_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvFillSedimentItem);
            return SqlHelper.ExecuteObject(new TEnvFillSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSedimentItem, TEnvFillSedimentItemVo.T_ENV_FILL_SEDIMENT_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSedimentItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSedimentItem, TEnvFillSedimentItemVo.T_ENV_FILL_SEDIMENT_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSedimentItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSedimentItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSedimentItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentItemVo tEnvFillSedimentItem_UpdateSet, TEnvFillSedimentItemVo tEnvFillSedimentItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSedimentItem_UpdateSet, TEnvFillSedimentItemVo.T_ENV_FILL_SEDIMENT_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSedimentItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEDIMENT_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            string strSQL = "delete from T_ENV_FILL_SEDIMENT_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillSedimentItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSedimentItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSedimentItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSedimentItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillSedimentItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillSedimentItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillSedimentItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSedimentItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSedimentItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSedimentItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSedimentItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSedimentItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSedimentItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSedimentItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
