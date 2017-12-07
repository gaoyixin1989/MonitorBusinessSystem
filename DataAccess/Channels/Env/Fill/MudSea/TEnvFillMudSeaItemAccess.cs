using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudSea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudSeaItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_MUD_SEA_ITEM " + this.BuildWhereStatement(tEnvFillMudSeaItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillMudSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaItemVo Details(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA_ITEM " + this.BuildWhereStatement(tEnvFillMudSeaItem));
            return SqlHelper.ExecuteObject(new TEnvFillMudSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudSeaItemVo> SelectByObject(TEnvFillMudSeaItemVo tEnvFillMudSeaItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA_ITEM " + this.BuildWhereStatement(tEnvFillMudSeaItem));
            return SqlHelper.ExecuteObjectList(tEnvFillMudSeaItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudSeaItemVo tEnvFillMudSeaItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_MUD_SEA_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillMudSeaItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_SEA_ITEM " + this.BuildWhereStatement(tEnvFillMudSeaItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <returns></returns>
        public TEnvFillMudSeaItemVo SelectByObject(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_SEA_ITEM " + this.BuildWhereStatement(tEnvFillMudSeaItem);
            return SqlHelper.ExecuteObject(new TEnvFillMudSeaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillMudSeaItem, TEnvFillMudSeaItemVo.T_ENV_FILL_MUD_SEA_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudSeaItem, TEnvFillMudSeaItemVo.T_ENV_FILL_MUD_SEA_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillMudSeaItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSeaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaItemVo tEnvFillMudSeaItem_UpdateSet, TEnvFillMudSeaItemVo tEnvFillMudSeaItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudSeaItem_UpdateSet, TEnvFillMudSeaItemVo.T_ENV_FILL_MUD_SEA_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillMudSeaItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_MUD_SEA_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            string strSQL = "delete from T_ENV_FILL_MUD_SEA_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillMudSeaItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillMudSeaItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillMudSeaItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillMudSeaItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillMudSeaItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillMudSeaItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillMudSeaItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillMudSeaItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillMudSeaItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillMudSeaItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillMudSeaItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillMudSeaItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillMudSeaItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillMudSeaItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
