using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.River30;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.River30
{
    /// <summary>
    /// 功能：双三十水断面数据填报监测项表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// ///       /// modify : 刘静楠
    /// time:2013-6-25
    /// </summary>
    public class TEnvFillRiver30ItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER30_ITEM " + this.BuildWhereStatement(tEnvFillRiver30Item);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30ItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER30_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiver30ItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30ItemVo Details(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER30_ITEM " + this.BuildWhereStatement(tEnvFillRiver30Item));
            return SqlHelper.ExecuteObject(new TEnvFillRiver30ItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiver30ItemVo> SelectByObject(TEnvFillRiver30ItemVo tEnvFillRiver30Item, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER30_ITEM " + this.BuildWhereStatement(tEnvFillRiver30Item));
            return SqlHelper.ExecuteObjectList(tEnvFillRiver30Item, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiver30ItemVo tEnvFillRiver30Item, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER30_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiver30Item));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiver30Item"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER30_ITEM " + this.BuildWhereStatement(tEnvFillRiver30Item);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <returns></returns>
        public TEnvFillRiver30ItemVo SelectByObject(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER30_ITEM " + this.BuildWhereStatement(tEnvFillRiver30Item);
            return SqlHelper.ExecuteObject(new TEnvFillRiver30ItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            tEnvFillRiver30Item.ID = new TSysSerialAccess().GetSerialNumber("FILL_RIVER30_ITEM_ID ");
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiver30Item, TEnvFillRiver30ItemVo.T_ENV_FILL_RIVER30_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30Item">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiver30Item, TEnvFillRiver30ItemVo.T_ENV_FILL_RIVER30_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiver30Item.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30Item_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiver30Item_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30ItemVo tEnvFillRiver30Item_UpdateSet, TEnvFillRiver30ItemVo tEnvFillRiver30Item_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiver30Item_UpdateSet, TEnvFillRiver30ItemVo.T_ENV_FILL_RIVER30_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiver30Item_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER30_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER30_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiver30Item);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER30_ITEM where 1=1 " + where;
            return ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiver30Item"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiver30Item)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiver30Item.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillRiver30Item.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillRiver30Item.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillRiver30Item.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiver30Item.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiver30Item.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiver30Item.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiver30Item.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiver30Item.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiver30Item.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiver30Item.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
