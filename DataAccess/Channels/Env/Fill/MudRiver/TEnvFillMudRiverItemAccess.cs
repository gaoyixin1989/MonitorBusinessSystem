using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudRiver;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudRiverItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_MUD_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillMudRiverItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillMudRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverItemVo Details(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillMudRiverItem));
            return SqlHelper.ExecuteObject(new TEnvFillMudRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudRiverItemVo> SelectByObject(TEnvFillMudRiverItemVo tEnvFillMudRiverItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillMudRiverItem));
            return SqlHelper.ExecuteObjectList(tEnvFillMudRiverItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudRiverItemVo tEnvFillMudRiverItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_MUD_RIVER_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillMudRiverItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudRiverItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillMudRiverItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <returns></returns>
        public TEnvFillMudRiverItemVo SelectByObject(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_RIVER_ITEM " + this.BuildWhereStatement(tEnvFillMudRiverItem);
            return SqlHelper.ExecuteObject(new TEnvFillMudRiverItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillMudRiverItem, TEnvFillMudRiverItemVo.T_ENV_FILL_MUD_RIVER_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudRiverItem, TEnvFillMudRiverItemVo.T_ENV_FILL_MUD_RIVER_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillMudRiverItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiverItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudRiverItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverItemVo tEnvFillMudRiverItem_UpdateSet, TEnvFillMudRiverItemVo tEnvFillMudRiverItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudRiverItem_UpdateSet, TEnvFillMudRiverItemVo.T_ENV_FILL_MUD_RIVER_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillMudRiverItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_MUD_RIVER_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            string strSQL = "delete from T_ENV_FILL_MUD_RIVER_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillMudRiverItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillMudRiverItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillMudRiverItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillMudRiverItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillMudRiverItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillMudRiverItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillMudRiverItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillMudRiverItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillMudRiverItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillMudRiverItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillMudRiverItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillMudRiverItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillMudRiverItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillMudRiverItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
