using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.Solid;

namespace i3.DataAccess.Channels.Env.Fill.Solid
{
    /// <summary>
    /// 功能：固废数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSolidItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SOLID_ITEM " + this.BuildWhereStatement(tEnvFillSolidItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSolidItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSolidItemVo Details(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID_ITEM " + this.BuildWhereStatement(tEnvFillSolidItem));
            return SqlHelper.ExecuteObject(new TEnvFillSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSolidItemVo> SelectByObject(TEnvFillSolidItemVo tEnvFillSolidItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID_ITEM " + this.BuildWhereStatement(tEnvFillSolidItem));
            return SqlHelper.ExecuteObjectList(tEnvFillSolidItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSolidItemVo tEnvFillSolidItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SOLID_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSolidItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSolidItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = "select * from T_ENV_FILL_SOLID_ITEM " + this.BuildWhereStatement(tEnvFillSolidItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <returns></returns>
        public TEnvFillSolidItemVo SelectByObject(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = "select * from T_ENV_FILL_SOLID_ITEM " + this.BuildWhereStatement(tEnvFillSolidItem);
            return SqlHelper.ExecuteObject(new TEnvFillSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSolidItem, TEnvFillSolidItemVo.T_ENV_FILL_SOLID_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolidItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSolidItem, TEnvFillSolidItemVo.T_ENV_FILL_SOLID_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSolidItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolidItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSolidItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidItemVo tEnvFillSolidItem_UpdateSet, TEnvFillSolidItemVo tEnvFillSolidItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSolidItem_UpdateSet, TEnvFillSolidItemVo.T_ENV_FILL_SOLID_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSolidItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SOLID_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            string strSQL = "delete from T_ENV_FILL_SOLID_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillSolidItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSolidItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSolidItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSolidItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillSolidItem.FILL_ID.ToString()));
                }
                //监测项ID
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillSolidItem.ITEM_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillSolidItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSolidItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSolidItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSolidItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSolidItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSolidItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSolidItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSolidItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
